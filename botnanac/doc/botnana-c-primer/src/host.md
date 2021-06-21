# 上位控制器

假設我們使用的上位控制器是一台安裝了 Ubuntu 或是 Debian Linux 的電腦。下圖為上位控制器中的 C 程式和 Botnana 控制器的 tasks 的關係，

```
       ws://192.168.7.2:3012
Host ----------------------------> user task --> position[] --> NC task
```

上位控制器要求 NC task 執行 drive-pp 後，會使用命令 queue 傳新的位置給 user task，user task 將新的位置放進 positions 後。而 NC task 則從 positions 中取得新的
位置並要求驅動器走到新的位置。

以下各節說明如何部署前一章的 Real-time script，以及使用修改用過 drive-pp.c 來送出新的命令。

## 部署腳本

在 examples/drive-pp.c 中我們使用 `program_line` 將腳本程式一行行的放進 Program 這個資料結構中。再以 `program_deploy` 部署腳本到 Botnana 控制器。
在部署前我們先執行了 `-work marker -work` 清除了之前部署的腳本。當腳本程式很大時，我們可以將腳本程式放進一個檔案中，再以程式將這檔案的內容一行一行讀出並放進
Program 資料結構中，然後進行部署。

以下是我們要部署的腳本程式 drive-pp.fs 的內容，
```
\
\ Queue
\

: array ( capacity -- )
    create cells allot
    does> ( n -- a ) swap cells + ;
10 constant #position        \ 陣列大小為 10
#position array position[]   \ 定義陣列名稱為 position[]
variable front#   0 front# ! \ queue 前端的索引
variable back#    0 back# !  \ queue 尾端的索引
\ 判斷 queue 是否是空的
: empty? ( -- f )   front# @  back# @  = ;
\ 判斷 queue 是否是滿的
: full? ( -- f )   back# @  1 +  #position mod  front# @  = ;
\ 將 position 放進 queue 中。如果無法放進去，傳一訊息給上位控制器。
: queue ( position -- )
    full? not if
        back# @  position[] !
        back# @  1 +  #position mod  back# !
    else
        ." log|full!" cr
    then ;
\ 從 queue 中取出一個值。如果 queue 是空的，回傳的 f 值為 0 (false)，否則回傳那個值以及 -1 (true)。
: dequeue ( -- position true | false)
    empty? not if
        front# @  position[] @
        front# @  1 +  #position mod  front# !
        true
    else
        false
    then ;

\
\ NC task
\

: drive-pp
    1 1 reset-fault
    1 1 until-no-fault
    1 1 drive-on
    1 1 until-drive-on
    1000 ms
    hm  1 1 op-mode!
    33  1 1 homing-method!
    until-no-requests
    1 1 go
    1 1 until-target-reached
    ." log|homed" cr
    pp  1 1 op-mode!
    1000  1 1 profile-v!
    until-no-requests
    begin
        begin
            dequeue not              \ 從 positions 中拿出一個位置，
        while                        \ 如果positions 是空的
            pause                    \ 把 CPU 使用權交給其他 tasks
                                     \ 注意這個 pause 是必須的。如果不把 CPU 使用
                                     \ 權交出去，那麼，另一個 task 就不能執行 queue
                                     \ 放進新的位置，於是這兒就形成一個忙碌迴圈，
                                     \ 又因為這個指令是在最高優先的 real-time 環境下
                                     \ 執行，整個控制器就會當在這兒。直到控制器更高優先
                                     \ 權的 watch dog thread 超時強迫 tasks 停止。 
        repeat
        ( position )  1 1 target-p!  \ 設定新的目標位置竹為從 positions 得到的位置
        1 1 go                       \ 移動至新的位置
        1 1 until-target-reached     \ 等待到達目標位置
    again
;
```

## C 的主程式

修改 examples/drive_pp.c，使其使用 program_new_from_file 從 `./drive-pp.fs` 讀取腳本，並且在 NC task 執行 `drive-pp` 這指令後透過 `evaluate-script`
要求 user task 執行 `queue` 指令，放三組位置到 `position[]` 中。

```
int main()
{
    int ws_open = 0;
    int is_finished = 0;
    int deployed_ok = 0;
    int real_position = 0;
    int target_position = 0;

    // connect to motion server
    struct Botnana * botnana = botnana_new("192.168.7.2");
    botnana_set_on_open_cb(botnana, (void *) & ws_open, on_ws_open_cb);
    botnana_set_on_error_cb(botnana, (void *) & ws_open, on_ws_error_cb);
    //botnana_set_on_message_cb(botnana, on_message_cb);
    //botnana_set_on_send_cb(botnana, on_send_cb);
    botnana_set_tag_cb(botnana, "end-of-program", 0, (void *) & is_finished, end_of_program);
    botnana_set_tag_cb(botnana, "deployed", 0, (void *) & deployed_ok, deployed_cb);
    botnana_set_tag_cb(botnana, "real_position.1.1", 0,  (void *) & real_position, real_position_cb);
    botnana_set_tag_cb(botnana, "target_position.1.1", 0,  (void *) & target_position, target_position_cb);
    botnana_set_tag_cb(botnana, "log", 0, NULL, log_cb);
    botnana_set_tag_cb(botnana, "error", 0, NULL ,error_cb);

    botnana_connect(botnana);
    while (ws_open == 0)
    {
        sleep(1);
    }

    // Create a new program with name 'main', using library from file ./drive-pp.fs.
    struct Program * pm = program_new_with_file("main", "./drive-pp.fs");
    // The main programe will execute drive-pp.
    program_line(pm, "drive-pp");

    // Deploy program to motion server
    botnana_abort_program(botnana);
    script_evaluate(botnana, "-work marker -work");
    program_deploy(botnana, pm);

    while (deployed_ok == 0)
    {
        sleep(1);
    }

    // Run program
    program_run(botnana, pm);

    // Request user task to push positions into queue
    script_evaluate(botnana, "10000 queue");
    script_evaluate(botnana, "-10000 queue");
    script_evaluate(botnana, "20000 queue");

    while (1)
    {
        script_evaluate(botnana, "1 .slave");
        printf("target position: %d, real position: %d, is_finished: %d\n",target_position, real_position, is_finished);
        sleep(1);
    }
    return 0;

}
```

## Callbacks

在 `./drive-pp.fs` 中有兩處以 `tag|value` 的方式回傳了訊息給上位控制器。

1. `." log|full!" cr`
2. `." log|homed\" cr`

Forth 指令 `."` 會輸出之後到 `"` 的字串。而之後的 `cr` 會輸出一個換行。這些字串會傳回到上位控制器然後被解析。在這範例中我們只簡單印出這兩個訊息。因為原來的 drive-pp.c 中已經設了 tag `log` 的 callback function。在此不作處理。不過要記得如果送位置送得太快超過了 `position[]` 的大小會導致那個位置被拋棄。這問題依需求的不同有不同的解法，因此在此不深入討論。

另外，如果我們還需要取得極限開關的狀態，參考 Botnana Book 的 Real-time script API 中 EtherCAT 指令集一節中有關 `.slave` 和 `.slave-diff` 的說明，其中的 tag `digital_inputs` 就有極限開關的資訊。但這部份必須參考使用的驅動器的文件，才知道極限開關在哪一個 bits。以 Panasonic A6B 為例，

TODO
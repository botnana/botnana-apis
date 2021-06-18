# 多工

以下是我們計畫採用的多工架構。使用了兩個 tasks，其中一個 task 進行馬達的初始化及回歸原點後，不斷的從一個 queue 中拿新的位置，並要求
控制品走到這個位置。如果 queue 是空的，就一直等待直到 queue 內有資料。另一個 task 則接收上位控制器傳來的位置，將它放進 queue 中。
如果 queue 滿了放不進去，就發出一個 log，放棄那筆資料。

以下是這個多工的架構圖：

```
   NC task  <-- queue <-- user task
```

Botnana 控制器內建 5 個 tasks，其中有個 NC Task 適合用來處理長時間的工作。另外有兩個 user tasks 用來處理上位控制的指令。User tasks
執行的指令應都能在一週實時週期內完成 (指令中沒有無法結束的迴圈也不會執行 pause 轉移 CPU 控制權給其他的 tasks)。

以下是原來的 drive-pp.c 的流程圖，

```
(start) --> 初始化馬達 --> 回原點 -> 設為 pp 模式 --> 走到 10000 的位置 --> (end of program)
```

為了能不斷給新的位置，以下是新的流程圖，

```
NC Task:

(start) --> 初始化馬達 --> 回原點 -> 設為 pp 模式 -+-> 使用 dequue 指令從一個 queue 中得到下一個位置 -> 走到那個位置 -+
                                               ^                                                           |
                                               |                                                           v
                                               +-----------------------------------------------------------+
```

以下為 User Task 要做的事：

* 以 queue 指令放進新的位置。

首先，我們使用一個大小為 10 的陣列作為 queue 來存放位置。我們使用「rtForth 入門」中字典那章中索引從 0 開始的陣列。

```
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
        ." log|full!"
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
```

然後，修改 drive-pp 的程式如下：


```
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
    ." log|homed\" cr
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
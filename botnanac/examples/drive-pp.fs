\
\ Queue
\ +-------+------+--------+--------+--------+-----+
\ | front | back | cell 0 | cell 1 | cell 2 | ... |
\ +-------+------+--------+--------+--------+-----+

: create-queue ( capacity -- )
    create 0 , 0 , cells allot ;
\    does> ( n -- a ) swap 2 +  cells + ;
10 constant #position        \ 陣列大小為 10
#position create-queue pp[]   \ 定義陣列名稱為 position[]
#position create-queue pv[]
variable dequeue_front 0 dequeue_front !
variable dequeue_back 0 dequeue_back !
\ variable front#   0 front# ! \ queue 前端的索引
\ variable back#    0 back# !  \ queue 尾端的索引

: front@ ( queue -- front )  @ ;
: back@ ( queue -- back )   cell+ @ ;

\ 判斷 queue 是否是空的
\ : empty? ( -- f )   front# @  back# @  = ;
: empty? ( queue -- f ) dup front@  swap back@ = ;

\ 判斷 queue 是否是滿的
\ : full? ( -- f )   back# @  1 +  #position mod  front# @  = ;
: full? ( queue -- f ) dup back@ 1 + #position mod swap front@ = ;

\ 將 position 放進 queue 中。如果無法放進去，傳一訊息給上位控制器。
\ : queue ( position -- )
\    full? not if
\        back# @  position[] !
\        back# @  1 +  #position mod  back# !
\    else
\        ." log|full!" cr
\    then ;
: queue ( queue position -- )
    swap dup 
    full? not if
        dup -rot dup back@ 2 + cells + !
        dup back@ 1 + #position mod swap cell+ ! 
    else
        ." log|full!" cr
    then ;

\ 從 queue 中取出一個值。如果 queue 是空的，回傳的 f 值為 0 (false)，否則回傳那個值以及 -1 (true)。
\ : dequeue ( -- position true | false)
\    empty? not if
\        front# @  position[] @
\        front# @  1 +  #position mod  front# !
\        true
\    else
\        false
\    then ;

\ 從 queue 中取出一個值。如果 queue 是空的，回傳的 f 值為 0 (false)，否則回傳那個值以及 -1 (true)。
\ 並且將front、back以外的資料都往前放一個位置
: dequeue ( -- position true | false ) 
    dup front@ 2 + dequeue_front ! 
    dup back@ 2 + dequeue_back ! 
    dup 
    empty? not if 
        dup 
        dup front@  2 + cells + @ true rot ( ans true p ) 
        begin dequeue_front @ dequeue_back @ < 
        while dup dup dequeue_front @ 1 + cells + @ swap dequeue_front @ cells + ! 
        dequeue_front @ 1 + dequeue_front ! 
        repeat 
        dup cell+ @ 1 - swap cell+ ! 
    else
        drop false
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
            pp[] dequeue not              \ 從 positions 中拿出一個位置，
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

: drive-pv
    1 1 reset-fault
    1 1 until-no-fault
    1 1 drive-on
    1 1 until-drive-on
    1000 ms 
    
    ." log|homed" cr
    pv  1 1 op-mode!
    -10000  1 1 target-v!
    until-no-requests
    begin
        begin
           pv[] dequeue not              \ 從 positions 中拿出一個位置，
        while                        \ 如果positions 是空的
            pause                    \ 把 CPU 使用權交給其他 tasks
                                     \ 注意這個 pause 是必須的。如果不把 CPU 使用
                                     \ 權交出去，那麼，另一個 task 就不能執行 queue
                                     \ 放進新的位置，於是這兒就形成一個忙碌迴圈，
                                     \ 又因為這個指令是在最高優先的 real-time 環境下
                                     \ 執行，整個控制器就會當在這兒。直到控制器更高優先
                                     \ 權的 watch dog thread 超時強迫 tasks 停止。 
        repeat
        1 1 target-v!
     again
;


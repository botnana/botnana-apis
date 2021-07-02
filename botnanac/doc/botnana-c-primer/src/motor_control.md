# 馬達控制

本章節將介紹如何使用4drive配合4queue指令控制馬達。

首先介紹queue的操作指令，都是以下列指令進行操作。

```
queue_name  operation  parameter  channel  slave_num  4queue
```
queue_name：queue的名字。

operation：想對馬達操作的指令，總共有：啟動(serveron)、關閉(serveroff)、pp模式(pp)、pv模式(pv)、回歸原點模式(hm)、等待到達(inpos)、指定加速度(acc)、指定減速度(dec)、減速暫停馬達(haltslow)、直接暫停馬達(haltquick)、結束暫停(haltend)、停止馬達(stop)。

parameter：配合operation指定的參數，適用於pp、pv，若是其他的operation則此欄位隨意填入數字即可。

channel：該馬達的管道編號。

slave_num：連結該馬達的從站編號。

4queue：此為固定的指令，表示將前四項參數放入queue中。

* 指令範例(以下範例都以編號1從站的編號1馬達進行示範，並且queue_name都為"pp[]")

```
\ 開啟馬達
pp[] serveron 0 1 1 4queue
\ 此處的0可為任意數
```
```
\ 關閉馬達
pp[] serveroff 0 1 1 4queuue
\ 此處的0可為任意數
```
```
\ 回歸原點模式
pp[] hm 0 1 1 4queue
pp[] inpos 0 1 1 4queue
\ 此兩行指令的0皆可為任意數
\ 下hm模式的命令後需要再下inpos等待馬達到達定位的命令
\ 可以一次下多顆馬達的hm指令在下多顆馬達的inpos指令
```
```
\ pp模式，假設馬達的目標位置為10000
pp[] pp 10000 1 1 4queue
pp[] inpos 0 1 1 4queue
\ inpos指令的0可為任意數
\ 下pp模式的命令後若下了inpos等待馬達到達定位的命令，則在到達定位之前不再接收其他指令
\ 可以一次下多顆馬達的pp指令在下多顆馬達的inpos指令
```
```
\ pv模式，假設馬達的速度為10000
pp[] pv 10000 1 1 4queue
```
```
\ acc指定加速度，假設加速度為10000
pp[] acc 10000 1 1 4queue
```
```
\ dec指定減速度，假設減速度為10000
pp[] dec 10000 1 1 4queue
```
```
\ 以減速模式暫停馬達
pp[] haltslow 0 1 1 4queue
\ 此處的0可為任意數
```
```
\ 直接暫停馬達
pp[] haltquick 0 1 1 4queue
\ 此處的0可為任意數
```
```
\ 結束馬達的暫停
pp[] haltend 0 1 1 4queue
\ 此處的0可為任意數
\ 此指令用於馬達被haltslow或是haltquick指令暫停後，想從暫停狀態恢復時
```
```
\ 停止馬達
pp[] stop 0 1 1 4queue
pp[] serveron 0 1 1 4queue
\ 此處的0可為任意數
\ 下此指令後，當前執行的指令以及儲存在queue中的指令都會直接清除
\ stop指令後需要執行serveron重新啟動馬達
```
* queue的構成

```
\ queue的基本元素
\ 最前面兩項為front、back，分別儲存從cell0開始算的最前端及最後端的索引值
\ 從back之後開始依序放入元素
\ +-------+------+--------+--------+--------+-----+
\ | front | back | cell 0 | cell 1 | cell 2 | ... |
\ +-------+------+--------+--------+--------+-----+

: create-queue ( capacity -- )
    create 0 , 0 , cells allot ;
100 constant #position        \ 陣列大小為 100
-1 constant serveroff           \ 將關閉指令命名為serveroff並賦值-1
0 constant serveron             \ 將開啟指令命名為serveron並賦值0
-2 constant inpos             \ 將等待抵達指令命名為inpos並賦值-2
#position create-queue pp[]   \ 定義陣列名稱為 pp[]
variable dequeue_front 0 dequeue_front ! \ dequeue指令用的變數
variable dequeue_back 0 dequeue_back !   \ dequeue指令用的變數
variable slave 0 slave !      \ 儲存4queue裡面的slave_num
variable num 0 num !          \ 儲存4queue裡面的channel

\ 取得queue前端及尾端的索引
: front@ ( queue -- front )  @ ;
: back@ ( queue -- back )   cell+ @ ;

\ 判斷 queue 是否是空的，若為空回傳真值(-1)，反之回傳0
: empty? ( queue -- f ) dup front@  swap back@ = ;

\ 判斷 queue 是否是滿的，若為滿回傳真值(-1)，反之回傳0
: full? ( queue -- f ) dup back@ 1 + #position mod swap front@ = ;


\ 放一個元素進入queue中，此指令是為了方便2queue的建立。如果因為queue已滿無法放進去，傳一訊息給上位控制器。
: queue ( queue position -- )
    swap dup 
    full? not if
        dup -rot dup back@ 2 + cells + !
        dup back@ 1 + #position mod swap cell+ ! 
    else
        ." log|full!" cr
    then ;

\ 一次將兩個元素放進 queue 中。此指令為方便4queue的建立
: 2queue ( queue mode position -- )
    -rot 2dup queue
    drop swap queue ;

\ 一次將四個元素放進 queue 中。
: 4queue ( queue mode position slave n -- )
    pp[] -rot 2queue 2queue ;

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
```

4drive 的程式如下：


```
: 4drive
    ." log|homed" cr
    begin 
        
        begin
            pp[] dequeue not					\ 從queue裡取出ch，若queue為空則繼續重新dequeue
        while
            pause 
        repeat
        pp[] dequeue drop swap				   \ 從queue裡取出slave
        slave !								   \ 將slave存入slave
        num !								   \ 將channel存數num

        pp[] dequeue drop                           \ 從queue裡取出mode
        pp[] dequeue drop swap                      \ 從queue裡取出position

        case 
            serveroff of
                num @ slave @ reset-fault           \ 清除馬達的異警
                num @ slave @ until-no-fault        \ 等待直到馬達的異警被清除
                num @ slave @ drive-off             \ 關閉馬達
                100 ms 
            endof
            serveron of
                num @ slave @ reset-fault
                num @ slave @ until-no-fault
                num @ slave @ drive-on              \ 將馬達致能 (servo on, operation enabled)
                num @ slave @ until-drive-on        \ 等待從站一的第一顆馬達完成致能
                1000 ms 
            endof
            inpos of
                num @ slave @ until-target-reached  \ 等待馬達到達指定位置
            endof
			acc of
                num @ slave @ profile-a1!			\ 設定加速度
            endof
            dec of
                num @ slave @ profile-a2!			\ 設定減速度
            endof
            pp of
                pp  num @ slave @ op-mode!          \ 設馬達的模式為點到點運動 (pp mode)
                100000  num @ slave @ profile-v!    \ 設馬達的速度 (profile velocity) 為 100000
                until-no-requests                   \ 等待之前的 SDO 設定完成
                num @ slave @ target-p!             \ 設定馬達的目標位置 (target position)
                num @ slave @ go                    \ 執行
            endof
            pv of
                pv  num @ slave @ op-mode!
                num @ slave @ target-v!
                until-no-requests
            endof
            hm of
                hm  num @ slave @ op-mode!          \ 設馬達的模式為回原點 (hm mode)
                33 num @ slave @ homing-method!     \ 設回原點的方式是第 33 號
                until-no-requests
                num @ slave @ go
            endof
			stop of
                num @ slave @ drive-stop			\ 停止馬達
                pp[] clear 							\ 清空queue
            endof
            haltslow of
                1 0 $605D num @ sdo-download-i16
                until-no-requests
                num @ slave @ +drive-halt
            endof
            haltquick of
                2 0 $605D num @ sdo-download-i16
                until-no-requests
                num @ slave @ +drive-halt
            endof
            haltend of
                num @ slave @ -drive-halt
            endof    
            drop 
        endcase
    again
;
##### ###### 
```
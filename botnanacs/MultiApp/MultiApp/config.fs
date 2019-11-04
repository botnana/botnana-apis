\ 若 alias 存在就取得該 slave number 並存到 2var 中
: config-device ( 2var ch alias -- )
    dup ec-alias? if
        ec-a>n rot 2!                           ( )
    else
        ." error|Alias :" . ." not found." cr   ( 2var ch )
        drop drop                               ( )
    then
;



\ 宣告記錄周邊裝置位址的變數，並指定該裝置的 channel 和 alias
2variable torque-drive torque-drive 1 101 config-device             \ 下壓測高馬達驅動器(Panasonic)
2variable rotate-drive rotate-drive 1 102 config-device             \ 測平邊轉盤馬達驅動器(Oriental)
2variable cylinder-dout cylinder-dout 13 301 config-device          \ 模具置中夾具氣壓缸
2variable rcon-gateway rcon-gateway 1 10 config-device              \ IAI RCON-GW-EC 閘道器

\ 宣告紀錄軸編號的變數
variable torque-axis 1 torque-axis !                                \ 下壓測高軸
variable rotate-axis 2 rotate-axis !                                \ 測平邊轉盤軸

\ 宣告紀錄軸組編號的變數
variable torque-group 1 torque-group !                              \ 下壓測高軸組



\ timers
variable timer-len

\ timer-constant <name>，用來配置一個計時器，並宣告該計時器編號為常數<name>
: timer-constant ( -- )
    timer-len @ 1+ dup timer-len ! constant
;

\ timer-array <name-len name-start>，用來一次配置 n 個計時器，並宣告該群計時器總數 n 為常數<name-len>、起始編號為常數<name-start>
: timer-array ( n -- )
    dup constant timer-len @ dup 1+ constant + timer-len !
;



\ flip-flops
variable ff-len

\ ff-constant <name>，用來配置一個正反器，並宣告該正反器編號為常數<name>
: ff-constant
    ff-len @ 1+ dup ff-len ! dup constant reset-ff
;

\ ff-array <name-start>，用來一次配置 n 個正反器，並宣告該群正反器起始編號為常數<name-start>
: ff-array ( n -- )
    ff-len @ dup 1+ dup constant                ( n len start )
    -rot + dup ff-len ! 1+ swap                 ( end start )
    do                                          ( )
        i reset-ff
    loop
;



\ 宣告 System Ready 正反器
ff-constant ff-system-ready-hl
    1 ff-system-ready-hl ff-type!
    5000000 ff-system-ready-hl ff-hold!

\ System Ready?
: system-ready? ( -- t )
    ff-system-ready-hl ff-triggered-uc?     \ 正反器是否觸發?
;



variable torque-ready
variable feeder-ready
variable rcon-ready

\ 取得 SFC 狀態
: .sfc-info ( -- )
    ." system_ready|" system-ready? 0 .r cr
    ." torque_ready|" torque-ready @ 0 .r cr
    ." feeder_ready|" feeder-ready @ 0 .r cr
    ." rcon_ready|" rcon-ready @ 0 .r cr
;
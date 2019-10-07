\ 檢查周邊裝置是否正確的旗標
variable device-config-ok device-config-ok on
: config-device ( 2var-addr ch alias -- )
    dup ec-alias? if                        \ 若 alias 存在
        ec-a>n                                  \ 由該 alias 取得對應的 slave NO.
        rot 2!                                  \ 將 channel NO. 和 slave NO. 存入變數
    else
        ." error|Alias: " . ." not found." cr
        drop drop device-config-ok off          \ 註記周邊裝置檢查不正確
    then
;

\ 指定馬達驅動器的 channel 和 alias
2variable torque-drive torque-drive 1 101 config-device

\ 定義運動軸的編號
variable axis-no 1 axis-no !

\ 定義軸組的編號
variable group-no 1 group-no !

\ 宣告正反器
1 constant ff-system-ready-hl   1 ff-system-ready-hl ff-type!       \ system ready high-level trigger flip-flop
    5000000 ff-system-ready-hl ff-hold!
    
\ System Ready?
: system-ready? ( -- t )
    ff-system-ready-hl ff-triggered-uc?         \ system ready high-level trigger flip-flop 是否觸發?
;

\ 取得 SFC 狀態
: poll ( -- )
    ." system_ready|" system-ready? 0 .r cr
;

\ 取得周邊裝置的資訊
: .device-infos ( -- )
    ." device_infos|"
    torque-drive 2@
    0 .r ." ."              \ slave number
    0 .r ." ."              \ channel number
    axis-no @ 0 .r ." ."    \ axis number
    group-no @ 0 .r         \ group number
    cr 
;
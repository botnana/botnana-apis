\ ==================== SFC ====================
\
\                  manager-init
\                       |
\                       + manager-devices-ok?
\                       |
\                       v
\             manager-activate-sfcs
\                       |
\                       + manager-activate-sfcs-done?
\                       |
\                       v
\                  manager-forth
\

\ -------------------- steps --------------------
\ 初始化
: manager-init ( -- )
    \ do nothing
;
step manager-init

\ 啟動相關的 SFC
: manager-activate-sfcs ( -- )
    ['] feeder-init +step
    ['] param-proc-idle +step
;
step manager-activate-sfcs

\ 每個週期檢查是否滿足所有 system ready 的條件
: manager-forth ( -- )
    ec-ready?                           \ EtherCAT ready?
    ff-system-ready-hl ff-forth-uc      \ 更新 system-ready flip-flop
;
step manager-forth



\ -------------------- transitions --------------------
\ 周邊硬體配置正確?
: manager-devices-ok? ( -- t )
    device-config-ok @ if                   \ 若需要的從站皆存在
        drive-device 2@ ec-drive?               \ 且從站 drive-device 為驅動器
        cylinder-device 2@ ec-dout? and         \ 且從站 cylinder-device 為數位輸出
    else
        false
    then
;
transition manager-devices-ok?

\ 啟動相關 SFC 完成?
: manager-activate-sfcs-done? ( -- t )
    true
;
transition manager-activate-sfcs-done?



\ -------------------- links --------------------
' manager-init          ' manager-devices-ok?           --> ' manager-devices-ok?           ' manager-activate-sfcs -->
' manager-activate-sfcs ' manager-activate-sfcs-done?   --> ' manager-activate-sfcs-done?   ' manager-forth         -->



' manager-init +step        \ 啟動 manager SFC
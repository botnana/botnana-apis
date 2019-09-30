\ ==================== SFC ====================
\
\                  manager-init
\                       |
\                       + manager-init-done?
\                       |
\                       v
\             manager-activate-sfcs
\                       |
\                       + manager-activate-sfcs-done?
\                       |
\                       v
\            manager-system-not-ready
\                       |
\                       + manager-system-ready?
\                       |
\                       v
\              manager-system-ready
\                       |
\                       + manager-system-not-ready?
\                       |
\                       v
\            manager-system-not-ready
\

variable manager-init-done
variable manager-activate-sfcs-done



\ -------------------- steps --------------------
\ 初始化
: manager-init ( -- )
    manager-init-done @ not if
        \ do nothing
        manager-init-done on
    then
;
step manager-init

\ 啟動相關的 SFC
: manager-activate-sfcs ( -- )
    manager-activate-sfcs-done @ not if
        ['] feeder-init +step
        ['] param-proc-idle +step
        manager-activate-sfcs-done on
    then
;
step manager-activate-sfcs

\ System not ready state
: manager-system-not-ready ( -- )
    system-ready off
;
step manager-system-not-ready

\ System ready state
: manager-system-ready ( -- )
    system-ready on
;
step manager-system-ready



\ -------------------- transitions --------------------
\ 初始化完成且周邊硬體配置正確?
: manager-init-done? ( -- bool )
    device-config-ok @ if                   \ 若需要的從站皆存在
        drive-device 2@ ec-drive?               \ 且從站 drive-device 為驅動器
        cylinder-device 2@ ec-dout? and         \ 且從站 cylinder-device 為數位輸出
    else
        false
    then

    manager-init-done @ and
;
transition manager-init-done?

\ 啟動相關 SFC 完成?
: manager-activate-sfcs-done? ( -- bool )
    ['] manager-activate-sfcs elapsed 5000 >        \ 啟動相關的 SFC 後等待一段延遲才允許進入 system ready
;
transition manager-activate-sfcs-done?

\ System ready?
: manager-system-ready? ( -- bool )
    ec-ready?
;
transition manager-system-ready?

\ System not ready?
: manager-system-not-ready? ( -- bool )
    manager-system-ready? not
;
transition manager-system-not-ready?



\ -------------------- links --------------------
' manager-init              ' manager-init-done?            --> ' manager-init-done?            ' manager-activate-sfcs     -->
' manager-activate-sfcs     ' manager-activate-sfcs-done?   --> ' manager-activate-sfcs-done?   ' manager-system-not-ready  -->
' manager-system-not-ready  ' manager-system-ready?         --> ' manager-system-ready?         ' manager-system-ready      -->
' manager-system-ready      ' manager-system-not-ready?     --> ' manager-system-not-ready?     ' manager-system-not-ready  -->



' manager-init +step        \ 啟動 manager SFC
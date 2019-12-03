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
\                  manager-forth
\

variable manager-init-done



\ -------------------- steps --------------------
\ 初始化
: manager-init ( -- )
    \ you can do something here.
    manager-init-done on
;
step manager-init

\ 啟動相關的 SFC
: manager-activate-sfcs ( -- )
    ['] torque-sfc +step
    ['] feeder-sfc +step
    ['] rcon-sfc +step
;
step manager-activate-sfcs

\ 每個週期檢查是否滿足所有 system ready 的條件
: manager-forth ( -- )
    ec-ready?                           \ EtherCAT ready?
    ff-system-ready-hl ff-forth-uc      \ 更新 system-ready flip-flop
;
step manager-forth



\ -------------------- transitions --------------------
\ 初始化完成?
: manager-init-done? ( -- t )
    manager-init-done @
;
transition manager-init-done?

\ 啟動相關 SFC 完成?
: manager-activate-sfcs-done? ( -- t )
    true
;
transition manager-activate-sfcs-done?



\ -------------------- links --------------------
' manager-init          ' manager-init-done?            --> ' manager-init-done?            ' manager-activate-sfcs -->
' manager-activate-sfcs ' manager-activate-sfcs-done?   --> ' manager-activate-sfcs-done?   ' manager-forth         -->

' manager-init +step        \ 啟動 manager SFC
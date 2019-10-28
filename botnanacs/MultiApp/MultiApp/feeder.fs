\ 宣告計時器
timer-constant cylinder-on-timer                        \ 模具置中夾具氣壓缸 ON 持續時間
timer-constant cylinder-off-timer                       \ 模具置中夾具氣壓缸 OFF 持續時間
timer-constant feeder-settling-timer                    \ 測平邊轉盤馬達穩定時間

\ 定義 Touch Probe function
variable tp-func $0033 tp-func !

\ 設定氣壓缸 ON 的時間 [ms]
variable cylinder-on-duration
: cylinder-on-duration! ( ms -- )
    dup cylinder-on-timer timer-dur-ms! cylinder-on-duration !
;
1000 cylinder-on-duration!

\ 設定氣壓缸 OFF 的時間 [ms]
variable cylinder-off-duration
: cylinder-off-duration! ( ms -- )
    dup cylinder-off-timer timer-dur-ms! cylinder-off-duration !
;
1000 cylinder-off-duration!

\ 設定轉盤馬達穩定時間 [ms]
variable feeder-settling-duration
: feeder-settling-duration! ( ms -- )
    dup feeder-settling-timer timer-dur-ms! feeder-settling-duration !
;
500 feeder-settling-duration!

\ 設定旋轉盤用來尋找模具平邊的旋轉距離 [pulse] 保證能使平邊能被偵測的距離
variable feeder-rotation-distance
: feeder-rotation-distance! ( d -- )
    feeder-rotation-distance !
;
72000 feeder-rotation-distance!

\ 設定旋轉盤用來尋找模具平邊的旋轉速度 [pulse/s] 除馬達的最高速度限制還要滿足觸發訊號維持 2 ms
variable feeder-rotation-speed
: feeder-rotation-speed! ( v -- )
    feeder-rotation-speed !
;
10000 feeder-rotation-speed!

\ 搜尋平邊失敗後重新嘗試的次數
variable feeder-retry-count-max
: feeder-retry-count-max! ( n -- )
    feeder-retry-count-max !
;
1 feeder-retry-count-max!

\ 定義供給機是否在做動中
variable feeder-running

\ 供給機流程開始旗標
variable feeder-accepted

\ 記錄供給機開始動作的時間，用來計算每一趟的時間
variable feeder-start-time

\ 用來判斷是否有偵測到平邊的資訊
variable tp-detected-1
variable tp-detected-2
variable tp-detected-position-1
variable tp-detected-position-2
variable tp-prev-position-1
variable tp-prev-position-2

\ 讓供給機 SFC 緊急停止的旗標 
variable feeder-ems-flag
: feeder-ems-flag? ( -- t )
    feeder-ems-flag @
;

\ 緊急停止流程
: ems-feeder ( -- )
    feeder-ems-flag on
;

\ 解除緊急停止狀態
: release-feeder ( -- )
    feeder-running @ not if
        feeder-ems-flag off
    then
;

\ 建造 touch probe status SDO request 資料結構
sdo-request tp-status

\ touch probe status SDO request 命令
: tp-status-cmd ( -- )
    0 $60b9 tp-status sdo-request.slv-no @ sdo-upload-u16
;

\ touch probe status SDO request 成功後的處理
: tp-status-success ( -- )
    ." sdo_0_24761." tp-status sdo-request.slv-no @ dup 0 .r ." |" sdo-data@ 0 .r cr
;

\ touch probe status SDO request 失敗後的處理
: tp-status-error ( -- )
    ." sdo_0_24761." tp-status sdo-request.slv-no @ 0 .r ." |--" cr
;

\ 嘗試將 tp-status sdo-request 放入 sdo-reqs 佇列中
: sdo-upload-tp-status ( -- )
    system-ready?               \ 若 system ready
    sdo-reqs-space@ 0> and      \ 且 sdo-reqs 佇列尚有空間
    if
        tp-status sdo-reqs-push-uncheck
    then
;

\ 啟動供給機流程
: start-feeder ( -- )
    system-ready?                   \ system ready
    rotate-drive 2@ drive-on? and   \ 且轉盤馬達驅動器已 drive on
    feeder-ems-flag? not and        \ 且不是 EMS 中
    feeder-ready @ and              \ 且 feeder SFC ready
    feeder-running @ not and        \ 且 feeder SFC 閒置中
    if
        feeder-accepted on              \ 開始 feeder 流程
        mtime feeder-start-time !       \ 紀錄開始時間
    else
        system-ready? not if
            ." error|System not ready(start-feeder)" cr
        then
        rotate-drive 2@ drive-on? not if
            ." error|Not Drive ON(start-feeder)" cr
        then
        feeder-ems-flag? if
            ." error|In EMS(start-feeder)" cr
        then
        feeder-ready @ not if
            ." error|Feeder SFC Not Ready(start-feeder)" cr
        then
        feeder-running @ if
            ." error|Feeder Running(start-feeder)" cr
        then
    then
;

\ 轉盤移動命令
: feeder-jog ( F: position speed -- )
    rotate-drive 2@ drive-on?       \ 轉盤馬達驅動器 servo on 中
    feeder-running @ not and        \ 且 feeder SFC 閒置中
    if 
        rotate-axis @ +interpolator
        rotate-axis @ interpolator-reached? if
            rotate-axis @ interpolator-v!
        else
            fdrop
        then
        rotate-axis @ axis-cmd-p!
    else
        rotate-drive 2@ drive-on? not if
            ." error|Not Drive ON(start-feeder)" cr
        then
        feeder-running @ if
            ." error|Feeder Running(start-feeder)" cr
        then
        fdrop fdrop
    then
;

\ 轉盤往正轉方向移動
: feeder-jog+ ( F: speed -- )
    2147481647e fswap feeder-jog
;

\ 轉盤往返轉方向移動
: feeder-jog- ( F: speed -- )
    -2147481647e fswap feeder-jog
;

\ 轉盤停止移動命令
: feeder-jog-stop ( -- )
    rotate-axis @ -interpolator
;

\ 取得供給機 SFC 的執行狀態
: .feeder ( -- )
    ." feeder_running|" feeder-running @ 0 .r
    ." |feeder_ems|" feeder-ems-flag @ 0 .r
    ." |tp_detected_1|" tp-detected-1 @ 0 .r
    ." |tp_detected_2|" tp-detected-2 @ 0 .r
    ." |tp_detected_position_1|" tp-detected-position-1 @ 0 .r
    ." |tp_detected_position_2|" tp-detected-position-2 @ 0 .r
    cr  
;

\ 取得供給機 SFC 的參數設定值    
: .feeder-para ( -- )
    ." cylinder_on_duration|" cylinder-on-duration @ 0 .r
    ." |cylinder_off_duration|" cylinder-off-duration @ 0 .r
    ." |feeder_rotation_distance|" feeder-rotation-distance @ 0 .r
    ." |feeder_rotation_speed|" feeder-rotation-speed @ 0 .r
    ." |feeder_settling_duration|" feeder-settling-duration @ 0 .r
    ." |feeder_retry_count_max|" feeder-retry-count-max @ 0 .r
    cr  
;

\ 取得裝置資訊 : feeder-infos|(rotate-slave).(rotate-channel).(cylinder-slave).(cylinder-channel)
: .feeder-infos ( -- )
    ." feeder_infos|"
    rotate-drive 2@
    0 .r ." ."                      \ rotate slave number
    0 .r ." ."                      \ rotate channel number
    cylinder-dout 2@
    0 .r ." ."                      \ cylinder slave number
    0 .r cr                         \ cylinder channel number
;



\ ==================== SFC ====================
\
\                  feeder-sfc
\                      |
\                      + feeder-devices-ok?
\                      |
\                      v
\                 feeder-init
\                      |
\                      + feeder-init-done?
\                      |
\                      v
\                 feeder-idle
\                      |
\                      + feeder-accepted?
\                      |
\                      v
\                feeder-forth
\                      |
\                      + feeder-forth-done?
\                      |
\                      v
\                 feeder-end
\                      |
\                      + feeder-end-done?
\                      |
\                      v
\                 feeder-idle
\

variable feeder-init-done
variable feeder-error
variable feeder-forth-step
variable feeder-retry-count



\ -------------------- steps --------------------
\ feeder SFC 的入口
: feeder-sfc ( -- )
    \ do nothing
;
step feeder-sfc

\ 初始化
: feeder-init ( -- )
    feeder-init-done @ not
    system-ready? and
    if
        tp-func @ rotate-drive 2@ drive-tp!                         \ 設定轉盤馬達驅動器的 touch prob function
        rotate-drive 2@ swap drop tp-status sdo-request.slv-no !    \ 設定 tp-status sdo-request 的欄位內容
        ['] tp-status-cmd tp-status sdo-request.'cmd !
        ['] tp-status-success tp-status sdo-request.'success !
        ['] tp-status-error tp-status sdo-request.'error !
        feeder-init-done on
    then
;
step feeder-init

\ 閒置中
: feeder-idle ( -- )
    feeder-ready on
    feeder-running off
    feeder-error off
    0 feeder-forth-step !
    0 feeder-retry-count !
;
step feeder-idle

\ 運行流程
: feeder-forth ( -- )
    feeder-running on
    
    system-ready?                   \ system ready
    rotate-drive 2@ drive-on? and   \ 且轉盤馬達驅動器 servo on 中
    feeder-ems-flag? not and        \ 且不是 EMS 中
    if
        feeder-forth-step @
        case
            \ 準備工作
            0 of
                1 feeder-retry-count +!         \ 計數 retry 次數
                feeder-accepted off
                cylinder-dout 2@ ec-dout@       \ 若氣壓缸為 ON
                if
                    0 cylinder-dout 2@ ec-dout!     \ 收回氣壓缸
                    cylinder-off-timer 0timer       \ 重置 timer
                    1 feeder-forth-step +!          \ 切換至下一步
                else
                    2 feeder-forth-step !           \ 切換至啟動氣壓缸
                then
            endof

            \ 等待氣壓缸重置
            1 of
                cylinder-off-timer timer-expired?   \ 若氣壓缸重置完成
                if
                    1 feeder-forth-step +!              \ 切換至下一步
                then
            endof

            \ 啟動氣壓缸
            2 of
                1 cylinder-dout 2@ ec-dout!     \ 啟動氣壓缸
                cylinder-on-timer 0timer        \ 重置 timer
                1 feeder-forth-step +!          \ 切換至下一步
            endof

            \ 等待氣壓缸啟動
            3 of
                cylinder-on-timer timer-expired?    \ 若氣壓缸啟動完成
                if
                    1 feeder-forth-step +!              \ 切換至下一步
                then
            endof

            \ 收回氣壓缸
            4 of
                0 cylinder-dout 2@ ec-dout!     \ 收回氣壓缸
                cylinder-off-timer 0timer       \ 重置 timer
                1 feeder-forth-step +!          \ 切換至下一步
            endof

            \ 等待氣壓缸收回
            5 of
                cylinder-off-timer timer-expired?   \ 若氣壓缸收回完成
                if
                    1 feeder-forth-step +!              \ 切換至下一步
                then
            endof

            \ 檢查是不是已經在平邊
            6 of
                rotate-axis @ feeder-rotation-speed @ s>f interpolator-v!
                
                rotate-drive 2@ drive-dins@ $1000000 and 0=     \ 在平邊上
                if
                    rotate-axis @ +interpolator
                    rotate-axis @ axis-demand-p@ fdup 0e f> if -1.0e else 1.0e then
                    feeder-rotation-distance @ s>f f* f+ rotate-axis @ axis-cmd-p!
                    1 feeder-forth-step +!          \ 切換至下一步
                else
                    8 feeder-forth-step !           \ 切換至旋轉模具
                then
            endof

            \ 等待離開平邊
            7 of
                rotate-drive 2@ drive-dins@ $1000000 and 0<>    \ 離開平邊了
                if
                    1 feeder-forth-step +!          \ 切換至下一步
                else
                    rotate-axis @ interpolator-reached?                         \ 若已到目標
                    if
                        feeder-retry-count @ feeder-retry-count-max @ <             \ 若 retry 次數未到
                        if
                            0 feeder-forth-step !                                       \ 重新嘗試
                        else
                            feeder-error on                                             \ 偵測失敗, 離開 feeder-forth
                        then
                    then
                then
            endof

            \ 旋轉模具
            \ 假如目前位置大於 0, 就往負向運動。反之就往正方向運動 (避免位置運算時溢位問題)
            8 of
                tp-detected-1 off
                tp-detected-2 off
                rotate-drive 2@ drive-rpdo1@ tp-prev-position-1 !
                rotate-drive 2@ drive-rpdo2@ tp-prev-position-2 !
                rotate-axis @ +interpolator
                rotate-axis @ axis-demand-p@ fdup 0e f> if -1.0e else 1.0e then
                feeder-rotation-distance @ s>f f* f+ rotate-axis @ axis-cmd-p!
                1 feeder-forth-step +!              \ 切換至下一步
            endof

            \ 偵測模具平邊
            \ 以雷射光偵測，如果是平邊沒有遮擋到雷射光，DIN 訊號會是 OFF
            \ 所以偵測平邊一定是先 ON -> OFF -> ON
            \ 因為每次旋轉都會跟前一次的位置區間不同，所以直接比對 latched position, 如果不一樣表示有新的 latched position
            9 of
                tp-detected-1 @ not if                                      \ tp1 未完成
                    rotate-drive 2@ drive-rpdo1@ dup tp-detected-position-1 ! 
                    tp-prev-position-1 @ <> tp-detected-1 !                      \ 若位置有變化表示有觸發
                then
    
                tp-detected-1 @ tp-detected-2 @ not and if                  \ tp1 完成且 tp2 未完成
                    rotate-drive 2@ drive-rpdo2@ dup tp-detected-position-2 !  
                    tp-prev-position-2 @ <> tp-detected-2 !                      \ 若位置有變化表示有觸發
                then

                tp-detected-1 @ tp-detected-2 @ and                          \ 若 tp1 和 tp2 皆已觸發
                if
                    1 feeder-forth-step +!                                      \ 切換至下一步
                else
                    rotate-axis @ interpolator-reached?                         \ 若已到目標
                    if
                        feeder-retry-count @ feeder-retry-count-max @ <             \ 若 retry 次數未到
                        if
                            0 feeder-forth-step !                                       \ 重新嘗試
                        else
                            feeder-error on                                             \ 偵測失敗, 離開 feeder-forth
                        then
                    then
                then
            endof

            \ 定位至平邊
            10 of
                rotate-axis @ +interpolator
                tp-detected-position-1 @ tp-detected-position-2 @ + 2 / s>f rotate-axis @ axis-cmd-p!
                1 feeder-forth-step +!                                      \ 切換至下一步
            endof

            \ 等待定位至平邊
            11 of
                rotate-axis @ interpolator-reached?                         \ 若已到目標
                if
                    rotate-axis @ -interpolator                                 \ 關閉插植器
                    feeder-settling-timer 0timer                                \ 重置 timer
                    1 feeder-forth-step +!                                      \ 切換至下一步
                then
            endof

            \ 等待馬達實際位置穩定
            12 of
                feeder-settling-timer timer-expired?                        \ 等待馬達實際位置穩定完成
                if
                    -1 feeder-forth-step !                                      \ feeder-forth 完成
                then
            endof
        endcase
    else
        rotate-axis @ -interpolator     \ 關閉插植器
        0 cylinder-dout 2@ ec-dout!     \ 收回氣壓缸
        feeder-error on                 \ 放棄流程, 離開 feeder-forth
    then
;
step feeder-forth

\ 流程結束, 輸出結果
: feeder-end ( -- )
    system-ready? not if
        ." error|System not ready(feeder-end)" cr
    then
    rotate-drive 2@ drive-on? not if
        ." error|Unexpected drive off(feeder-end)" cr
    then
    feeder-ems-flag? if
        ." error|Feeder EMS(feeder-end)" cr
    then
    feeder-error @ if
        ." error|Feeder Error(feeder-end)" cr
    then
    ." feeder_operation_ms|" mtime feeder-start-time @ - 0 .r cr
;
step feeder-end



\ -------------------- transitions --------------------

\ 硬體配置正確?
: feeder-devices-ok? ( -- t )
    rotate-drive 2@ ec-drive?
    cylinder-dout 2@ ec-dout? and if
        true exit
    else
        rotate-drive 2@ ec-drive? not if
            ." error|Feeder SFC : Not a drive." cr
        then
        cylinder-dout 2@ ec-dout? not if
            ." error|Feeder SFC : Not a dout." cr
        then
    then
    ['] feeder-sfc -step
    false
;
transition feeder-devices-ok?

\ 初始化是否完成?
: feeder-init-done? ( -- t )
    rotate-drive 2@ drive-tp@ tp-func @ =           \ 檢查 touch prob function 正確
;
transition feeder-init-done?

\ 是否開始流程?
: feeder-accepted? ( -- t )
    feeder-accepted @
;
transition feeder-accepted?

\ 流程是否結束?
: feeder-forth-done? ( -- t )
    feeder-error @                                  \ 發生 error
    feeder-forth-step @ -1 = or                     \ 或 feeder-forth 完成
;
transition feeder-forth-done?

\ 輸出結果完成?
: feeder-end-done? ( -- t )
    true
;
transition feeder-end-done?



\ -------------------- links --------------------
' feeder-sfc    ' feeder-devices-ok?    --> ' feeder-devices-ok?    ' feeder-init   -->
' feeder-init   ' feeder-init-done?     --> ' feeder-init-done?     ' feeder-idle   -->
' feeder-idle   ' feeder-accepted?      --> ' feeder-accepted?      ' feeder-forth  -->
' feeder-forth  ' feeder-forth-done?    --> ' feeder-forth-done?    ' feeder-end    -->
' feeder-end    ' feeder-end-done?      --> ' feeder-end-done?      ' feeder-idle   -->
\ 供給機 SFC 介面
\ .feeder ( -- ) 取得供給機 SFC 的執行狀態
\ .feeder-para ( -- ) 取得供給機 SFC 的參數設定值
\ cylinder-on-duration! ( ms -- ) 設定氣壓缸 ON 的時間
\ cylinder-off-duration! ( ms -- ) 設定氣壓缸 OFF 的時間
\ cylinder-off-duration! ( ms -- ) 設定氣壓缸 OFF 的時間
\ feeder-rotation-distance! ( distance -- ) 設定旋轉盤用來尋找模具平邊的旋轉距離 [pulse]                                            
\ feeder-rotation-speed!    ( speed -- ) 設定旋轉盤用來尋找模具平邊的旋轉速度 [pulse/s]
\ start-feeder   ( -- ) 啟動供給機流程 
\ ems-feeder     ( -- ) 緊急停止供給機流程
\ release-feeder ( -- ) 解除供給機的緊急停止狀態
\ feeder-jog+ ( F: speed -- ) 轉盤往正方向運動
\ feeder-jog+ ( F: speed -- ) 轉盤往負方向運動
\ feeder-jog-stop ( -- ) 轉盤停止運動


\ 定義運動軸的編號
variable  axis-index 1 axis-index !

\ 定義旋轉盤驅動器的編號
2variable drive-device 1 1 drive-device 2!

\ 定義 Touch Probe 的設定值
variable  tp-func $2717 tp-func !

\ 定義控制氣壓缸的數位輸出編號
2variable cylinder-device 1 3 cylinder-device 2!

\ 氣壓缸 ON 的時間 [ms]
variable cylinder-on-duration 1000 cylinder-on-duration !

\ 氣壓缸 OFF 的時間 [ms]
variable cylinder-off-duration 1000 cylinder-off-duration ! 

\ 定義供給機是否在做動中
variable feeder-running

\ 記錄供給機開始動作的時間，用來計算每一趟的時間
variable feeder-start-time

\ 當啟動時，如果氣壓缸事處在 ON 狀態，用來將氣壓缸切換到 OFF 的時間 [ms]
variable feeder-prepare-time

\ 設定旋轉盤用來尋找模具平邊的旋轉距離 [pulse]
variable feeder-rotation-distance 20000 feeder-rotation-distance ! 

\ 設定旋轉盤用來尋找模具平邊的旋轉速度 [pulse/s]
variable feeder-rotation-speed 1000000 feeder-rotation-speed ! 

\ 用來判斷是否有偵測到平邊的資訊
variable tp1-detected
variable tp2-detected
variable tp1-detected-position
variable tp2-detected-position
variable tp1-prev-position
variable tp2-prev-position

\ 讓供給機 SFC 緊急停止的旗標 
variable feeder-ems-flag
: feeder-ems-flag?
    feeder-ems-flag @ 0<>
    ;

\ cylinder-on-duration! ( ms -- ) 設定氣壓缸 ON 的時間
: cylinder-on-duration!
    cylinder-on-duration !
    ;
    
\ cylinder-off-duration! ( ms -- ) 設定氣壓缸 OFF 的時間
: cylinder-off-duration!
    cylinder-off-duration !
    ;

\ feeder-rotation-distance! ( distance -- ) 設定旋轉盤用來尋找模具平邊的旋轉距離 [pulse] 
\ 保證能使平邊能被偵測的距離                                           
: feeder-rotation-distance!
    feeder-rotation-distance !
    ;

\ feeder-rotation-speed!    ( speed -- ) 設定旋轉盤用來尋找模具平邊的旋轉速度 [pulse/s]
\ 除馬達的最高速度限制還要滿足觸發訊號維持 2 ms
: feeder-rotation-speed!
    feeder-rotation-speed !
    ;

\ .feeder ( -- ) 取得供給機 SFC 的執行狀態
: .feeder
    ." feeder_running|" feeder-running @ 0 .r
    ." |feeder_ems|" feeder-ems-flag @ 0 .r
    ." |tp1_detected|" tp1-detected @ 0 .r
    ." |tp2_detected|" tp2-detected @ 0 .r
    ." |tp1_detected_position|" tp1-detected-position @ 0 .r
    ." |tp2_detected_position|" tp2-detected-position @ 0 .r
    cr  
    ;

\ .feeder-para ( -- ) 取得供給機 SFC 的參數設定值    
: .feeder-para
    ." cylinder_on_duration|" cylinder-on-duration @ 0 .r
    ." |cylinder_off_duration|" cylinder-off-duration @ 0 .r
    ." |feeder_rotation_distance|" feeder-rotation-distance @ 0 .r
    ." |feeder_rotation_speed|" feeder-rotation-speed @ 0 .r
    cr  
    ;    
    
variable feeder-accepted    
variable feeder-prepared

\ 設定 touch probe function
: feeder-init
    tp-func @ drive-device 2@ drive-tp!
;
step feeder-init

\ 檢查 touch probe function 是否已經更改成功
: feeder-inited?
    drive-device 2@ drive-tp@ tp-func @ =
;
transition feeder-inited?

\ Feeder Idle
: feeder-idle
    ;
step feeder-idle

\ 是否接受啟動命令    
: feeder-accepted?
    feeder-accepted @
    ;    
transition feeder-accepted?
    
\ 做動前的準備程序    
: feeder-prepare
    feeder-running on
    feeder-accepted off
    tp1-detected off
    tp2-detected off
    drive-device 2@ drive-rpdo1@ tp1-prev-position !
    drive-device 2@ drive-rpdo2@ tp2-prev-position !
   
    \ 確保氣壓缸是 OFF 狀態
    0 cylinder-device 2@ ec-dout!    
    feeder-prepare-time @ cylinder-off-duration @ >= if
        feeder-prepared on
    else
        mtime feeder-start-time @ - feeder-prepare-time !
    then     
    ;    

step feeder-prepare

\  氣壓缸是否在 OFF 狀態
: feeder-prepared?
    feeder-ems-flag? feeder-prepared @ or
    ;  

transition feeder-prepared?   

\ 氣壓缸作動
: feeder-cylinder-on
    feeder-ems-flag? not if
        1 cylinder-device 2@ ec-dout!
    then
    ;    
step feeder-cylinder-on

\ 等待氣壓缸作動完成    
: feeder-cylinder-on?
    feeder-ems-flag?
    ['] feeder-cylinder-on elapsed cylinder-on-duration @ > or
    ;    
transition feeder-cylinder-on?   

\ 收回氣壓缸    
: feeder-cylinder-off
    0 cylinder-device 2@ ec-dout!
    ; 
step feeder-cylinder-off    

\ 等待氣壓缸收回    
: feeder-cylinder-off?
    feeder-ems-flag? 
    ['] feeder-cylinder-off elapsed cylinder-off-duration @ > or
    ;    
transition feeder-cylinder-off?  

\ 模組開始旋轉
\ 假如目前位置大於 0, 就往負向運動。反之就往正方向運動 (避免位置運算時溢位問題)
: feeder-rotate
    feeder-ems-flag? not if 
        axis-index @ +interpolator
        axis-index @ feeder-rotation-speed @ s>f interpolator-v!
        axis-index @ axis-cmd-p@ fdup 0e f> if -1.0e else 1.0e then
        feeder-rotation-distance @ s>f f* f+ axis-index @ axis-cmd-p!
    then
    ;
step feeder-rotate

\ 旋轉後進入偵測平邊
: feeder-rotated?
    true 
;
transition feeder-rotated?

variable move-to-center-once
\ 偵測模具平邊
\ 以雷射光偵測，如果是平邊沒有遮擋到雷射光，就當做 ON
\ 所以偵測平邊一定是先 OFF -> ON -> OFF
\ 因為每次旋轉都會跟前一次的位置區間不同，所以直接比對 latched position, 如果不一樣表示有新的 latched position
: feeder-mold-detect
    move-to-center-once off
    
    tp1-detected @ not if 
        drive-device 2@ drive-rpdo1@ dup tp1-detected-position ! 
        tp1-prev-position @ <> tp1-detected !
    then
    
    tp1-detected @ tp2-detected @ not and  if 
        drive-device 2@ drive-rpdo2@ dup tp2-detected-position !  
        tp2-prev-position @ <> tp2-detected !
        axis-index @ dup -interpolator +interpolator 
    then
    
    ;
step feeder-mold-detect

\ 偵測到平邊就停下來
: feeder-mold-detected?
    feeder-ems-flag?
    axis-index @ interpolator-reached? or
    tp1-detected @ tp2-detected @ and or
    ;
transition feeder-mold-detected?

\ 如果有偵測到平邊，
\ 就轉到兩個偵測到的位置中間，如果沒有就保持原來的位置
: feeder-move-to-center
     feeder-ems-flag? not
     move-to-center-once @ not and
     tp1-detected @ and
     tp2-detected @ and
     if
        tp1-detected-position @ tp2-detected-position @ + 2 / s>f axis-index @ axis-cmd-p! 
        move-to-center-once on
     else
        tp1-detected @ tp2-detected @ and not if
            ." error|Feeder Error!!" cr
        then
     then
     ;
step feeder-move-to-center

\ 是否已經轉到目標位置
: feeder-on-center?
     feeder-ems-flag? axis-index @ interpolator-reached? or
    ;
transition feeder-on-center?

\ 計算時間, 更新狀態
: feeder-complete
    axis-index @ -interpolator
    feeder-running off
    ." feeder_operation_ms|" mtime feeder-start-time @ - 0 .r cr
    feeder-ems-flag? if
        ." error|Feeder EMS!!" cr
    then
    ; 
step feeder-complete

\ 完成後進入 feeder idle
: feeder-completed?
    true
    ;   
transition  feeder-completed?

\ start-feeder   ( -- ) 啟動供給機流程
: start-feeder
    drive-device 2@ drive-on? 
    feeder-ems-flag? not and
    feeder-running @ not and
    if 
        feeder-prepared off
        feeder-accepted on
        mtime feeder-start-time !
        \ 設定需要等待氣缸 OFF 的時間
        cylinder-device 2@ ec-dout@ if
            0 feeder-prepare-time !
        else
            cylinder-off-duration @ feeder-prepare-time !
        then
        
    else
        feeder-ems-flag?  if 
            ." error|Feeder EMS!!" cr
        then
        
        drive-device 2@ drive-on? not if 
            ." error|Drive OFF!!" cr
        then 
        
        feeder-running if 
            ." error|Feeder Running!!" cr
        then
        
    then
    ;

\ ems-feeder     ( -- ) 緊急停止供給機流程
: ems-feeder
    feeder-ems-flag on
    ;

\ release-feeder ( -- ) 解除供給機的緊急停止狀態
: release-feeder
    feeder-running @ not if
        feeder-ems-flag off
    then
    ;

: feeder-jog ( F: position speed -- )
    drive-device 2@ drive-on? if 
        axis-index @ +interpolator
        axis-index @ interpolator-reached? if
            axis-index @ interpolator-v!
        else
            fdrop
        then
        axis-index @ axis-cmd-p!
    else
        ." error|Drive OFF!!" cr
    then
    ;

: feeder-jog+ ( F: speed -- )
    2147481647e fswap feeder-jog
    ;

: feeder-jog- ( F: speed -- )
    -2147481647e fswap feeder-jog
    ;

: feeder-jog-stop ( -- )
    axis-index @ -interpolator
    ;    
    
\ 鏈結 SFC 流程
' feeder-init           ' feeder-inited?        -->
' feeder-inited?        ' feeder-idle           -->
' feeder-idle           ' feeder-accepted?      -->
' feeder-accepted?      ' feeder-prepare        -->
' feeder-prepare        ' feeder-prepared?      -->
' feeder-prepared?      ' feeder-cylinder-on    -->
' feeder-cylinder-on    ' feeder-cylinder-on?   -->
' feeder-cylinder-on?   ' feeder-cylinder-off   -->
' feeder-cylinder-off   ' feeder-cylinder-off?  --> 
' feeder-cylinder-off?  ' feeder-rotate         -->   
' feeder-rotate         ' feeder-rotated?       -->
' feeder-rotated?       ' feeder-mold-detect    -->
' feeder-mold-detect    ' feeder-mold-detected? -->
' feeder-mold-detected? ' feeder-move-to-center -->
' feeder-move-to-center ' feeder-on-center?     -->
' feeder-on-center?     ' feeder-complete       -->
' feeder-complete       ' feeder-completed?     -->
' feeder-completed?     ' feeder-idle           -->

\ 啟動 Step feeder-idle
' feeder-init +step
    
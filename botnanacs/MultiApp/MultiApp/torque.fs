variable tq-threshold 100 tq-threshold !
fvariable speed-change-distance 0.25e mm speed-change-distance f!

variable tq-stopping
variable tq-clock-start
fvariable tq-press-p1
fvariable tq-press-p2
fvariable tq-press-v1
fvariable tq-press-v2
fvariable tq-release-p
fvariable tq-release-v

: tq-press-params! ( F: p1 p2 v1 v2 -- )
    tq-press-v2 f!
    tq-press-v1 f!
    tq-press-p2 f!
    tq-press-p1 f!
;

: tq-release-params! ( F: p v -- )
    tq-release-v f!
    tq-release-p f!
;

: tq-press-go ( F: p1 p2 v1 v2 -- )
	system-ready?											\ system ready
	torque-drive 2@ drive-on? and							\ 且該馬達驅動器 servo on
	if
		tq-press-params!                    					\ 將  p1 p2 v1 v2 存到變數

		mtime tq-clock-start !              					\ 紀錄起始時間 [ms]    

		torque-group @ group! +group 0path gstart  				\ 啟動軸組，清除路徑，啟動軸組加減速
		tq-press-v1 f@ vcmd!                                	\ 設定 V1 為整個路徑的運動速度命令，實際速度會送到 path feedrate 與 group vmax 限制
		tq-stopping off

		mcs                                 					\ 以目前機械座標為運動起點

		1 path-id!                                       		\ 設定 path id
		tq-press-v1 f@ feedrate!                         		\ 設定 path feedrate V1
		tq-press-p1 f@ speed-change-distance f@ f+ line1d		\ 插入第 1 個路徑，目標位置 P1 + speed-change-distance (P1 往上 speed-change-distance 的地方)

		tq-press-v2 f@ feedrate!                            	\ 設定 path feedrate V2
		tq-press-p1 f@ line1d                               	\ 插入第 2 個路徑，目標位置 P1，速度變化的地方，扭力會有急遽的變化，用此路徑緩衝

		2 path-id!                                       		\ 設定 path id
		tq-press-v2 f@ feedrate!                            	\ 設定 path feedrate V2  
		tq-press-p2 f@ line1d                               	\ 插入第 3 個路徑，目標位置 P2

		pause pause                                      		\ 將控制權交出等待下一個周期
																\ 兩個周期不做事，讓加減速啟動進入運動狀態

		begin
			torque-group @ group! gstop? not 					\ 檢查該軸是否運動中
		while
			next-path-id@ 2 =                 					\ path id = 2
			torque-drive 2@ real-tq@ tq-threshold @ < and       \ 且 real torque 的絕對值小於門檻值
			tq-stopping @ not and                               \ 且 tq-stopping = false
			if
				gstop                                        	\ 命令該軸組依加減速停止 (停止後關閉軸組加減速)
				tq-stopping on
			then
			pause                                               \ 將控制權交出等待下一個周期
		repeat

		gstop 0path -group                                      \ 關閉該軸組加減速，清除路徑，關閉軸組
		." travel_time|" mtime tq-clock-start @ - . ." ms" cr   \ 計算經過時間，並送出資訊
	else
		fdrop fdrop fdrop fdrop								\ 將堆疊上的資料丟掉
		system-ready? not if
			." error|System not ready(tq-press-go)" cr
		then
		torque-drive 2@ drive-on? not if
			." error|Not servo on(tq-press-go)" cr
		then
	then
;

: tq-press-stop ( -- )
	torque-group @ group! gstop
;

: tq-release-go ( F: p v -- )
	system-ready?									\ system ready
	torque-drive 2@ drive-on? and					\ 且該馬達驅動器 servo on
	if
    	tq-release-params!                             	\ 將  p, v 存到變數      

		tq-release-v f@ torque-axis @ interpolator-v!   \ 設定插值器速度
		torque-axis @ +interpolator                     \ 開啟插值器
		tq-release-p f@ torque-axis @ axis-cmd-p!       \ 軸移動

		pause pause

		begin
			torque-axis @ interpolator-reached? not
		while
			pause
		repeat

		torque-axis @ -interpolator                    	\ 停止後關閉插值器
	else
		fdrop fdrop										\ 將堆疊上的資料丟掉
		system-ready? not if
			." error|System not ready(tq-release-go)" cr
		then
		torque-drive 2@ drive-on? not if
			." error|Not servo on(tq-release-go)" cr
		then
	then
;

: tq-release-stop ( -- )
	torque-axis @ -interpolator
;

\ 取得裝置資訊
: .torque-infos ( -- )
    ." torque_slv_ch_ax_grp|"
    torque-drive 2@
    0 .r ." ."                      \ torque slave number
    0 .r ." ."                      \ torque channel number
    torque-axis @ 0 .r ." ."        \ torque axis number
    torque-group @ 0 .r cr          \ torque group number
;



\ ==================== SFC ====================
\
\				   torque-sfc
\                      |
\                      + torque-devices-ok?
\                      |
\                      v
\                 torque-init
\                      |
\                      + torque-init-done?
\                      |
\                      v
\                 torque-forth
\

variable torque-init-done
variable rt-info-output-enabled


\ -------------------- steps --------------------
: torque-sfc ( -- )
	\ do nothing
;
step torque-sfc

: torque-init ( -- )
	system-ready? if
		\ you can do something here.
		torque-ready on
		torque-init-done on
	then
;
step torque-init

: torque-forth ( -- )
	rt-info-output-enabled @ if
		." rt_real_torque|" torque-drive 2@ real-tq@ 0 .r cr			\ 啟動後，每個周期都送出實際扭力的封包，封包範例: rt_real_torque|2
	then
;
step torque-forth



\ -------------------- transitions --------------------
: torque-devices-ok? ( -- t )
	torque-drive 2@ ec-drive? if
		true exit
	else
        ." error|Torque SFC : Not a drive." cr
	then
	['] torque-sfc -step
	false
;
transition torque-devices-ok?

: torque-init-done? ( -- t )
	torque-init-done @
;
transition torque-init-done?



\ -------------------- links --------------------
' torque-sfc 	' torque-devices-ok?	--> ' torque-devices-ok?	' torque-init 	-->
' torque-init	' torque-init-done? 	--> ' torque-init-done? 	' torque-forth 	-->
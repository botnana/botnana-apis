variable tq-limit1 1000 tq-limit1 !
variable tq-limit2 100 tq-limit2 !
fvariable tq-max-ferr1 1.0e mm tq-max-ferr1 f!
fvariable tq-max-ferr2 0.25e mm tq-max-ferr2 f!
fvariable tq-max-ferr-curr

variable tq-go
variable tq-busy
variable tq-clock-start
variable sdo-max-torque-busy
variable sdo-max-torque-error
fvariable tq-press-p1
fvariable tq-press-p2
fvariable tq-press-v1
fvariable tq-press-v2
fvariable tq-release-p
fvariable tq-release-v

$0 $6072 torque-drive @ sdo sdo-max-torque sdo-as-download-u16

: sdo-max-torque-cb ( sdo -- )
	sdo.slv @ sdo-error? sdo-max-torque-error !
	sdo-max-torque-busy off
;

' sdo-max-torque-cb sdo-max-torque sdo.'cb !

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
	0labels

	system-ready?											\ system ready
	torque-drive 2@ drive-on? and							\ 且該馬達驅動器 servo on
	tq-busy @ not and 										\ 且 tq not busy
	if
		tq-press-params!                    					\ 將堆疊上的參數存到變數
	else
		fdrop fdrop fdrop fdrop								\ 將堆疊上的資料丟掉
		system-ready? not if
			." error|System not ready(tq-press-go)" cr
		then
		torque-drive 2@ drive-on? not if
			." error|Not servo on(tq-press-go)" cr
		then
		tq-busy @ if
			." error|Tq busy(tq-press-go)" cr
		then
		exit
	then

	[ 100 ] call											\ 開始流程

	tq-limit1 @ [ 1 ] call tq-go @ and not if				\ 設定 max torque 並等待指令完成
		[ 101 ] call exit									\ 若 max torque 設定失敗或 tq-go 被關閉了就結束流程
	then
	
	torque-group @ group! mcs								\ 以目前機械座標為運動起點
	tq-press-p2 f@ line1d                               	\ 插入路徑，目標位置 P2

	torque-cp @ 1 mcs-p@ tq-press-p1 f@ f- fabs cp-as-stop	\ 設定位於 P1 的 stop control point

	tq-press-v1 f@ vcmd!                                	\ 設定運動速度為 V1
	gstart													\ 啟動軸組加減速

	tq-max-ferr1 f@ [ 10 ] call 							\ 等待至軸組運動停止
	tq-go @ not if [ 101 ] call exit then 					\ 若 tq-go 關閉了就結束流程

	tq-limit2 @ [ 1 ] call tq-go @ and not if				\ 設定 max torque 並等待指令完成
		[ 101 ] call exit									\ 若 max torque 設定失敗或 tq-go 被關閉了就結束流程
	then

	torque-group @ group!
	tq-press-v2 f@ vcmd!                                	\ 設定運動速度為 V2
	gstart													\ 啟動軸組加減速

	tq-max-ferr2 f@ [ 10 ] call 							\ 等待至軸組運動停止
	[ 101 ] call											\ 結束流程
	exit

	\ 設定 Max Torque SDO 並等待指令完成
	[ 1 ] label ( limit -- t )
		sdo-max-torque ll-unlinked? if
			sdo-max-torque sdo.value !
			sdo-max-torque send-sdo-uncheck
			sdo-max-torque-error off
			sdo-max-torque-busy on

			begin
				sdo-max-torque-busy @
			while
				pause
			repeat

			sdo-max-torque-error @ if
				." error|Set max torque failed. sdo-max-torque error." cr
			then

			sdo-max-torque-error @ not
		else
			drop
			." error|Set max torque failed. sdo-max-torque has been send." cr
			false
		then
	exit

	\ 等待至軸組運動停止，過程中若 ferr 超過 max-ferr 就下出停止軸組的命令
	[ 10 ] label ( F: max-ferr -- )
		tq-max-ferr-curr f!

		pause pause                                    	\ 將控制權交出等待下一個周期
														\ 兩個周期不做事，讓加減速啟動進入運動狀態
		begin
			torque-group @ group! gstop? not				\ 若軸組還在運動中		
		while
			tq-go @											\ 若流程未進入離開階段
			torque-axis @ axis-ferr@ fabs tq-max-ferr-curr f@ f> and if	\ 且 ferr 超過 max-ferr
				gstop                                        	\ 命令該軸組依加減速停止 (停止後關閉軸組加減速)
				tq-go off
			then
			pause
		repeat
	exit

	\ 開始流程
	[ 100 ] label ( -- )
		tq-go on
		tq-busy on
		torque-group @ group! +group 0path 				\ 啟動軸組，清除路徑
		mtime tq-clock-start !              			\ 紀錄起始時間 [ms]
	exit

	\ 結束流程
	[ 101 ] label ( -- )
		torque-group @ group! gstop 0path -group		\ 關閉該軸組加減速，清除路徑，關閉軸組

		torque-axis @ 0axis-ferr						\ 清除軸的落後誤差
		5 ms											\ 等待馬達整定
		tq-limit1 @ [ 1 ] call drop						\ 設定 max torque 為 tq-limit1

		tq-go off
		tq-busy off
		." travel_time|" mtime tq-clock-start @ - . ." ms" cr   \ 計算經過時間，並送出資訊
	exit
;

: tq-press-stop ( -- )
	torque-group @ group! gstop
	tq-go off
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



: -work ( -- )
	sdo-max-torque withdraw-sdo
	-work
;
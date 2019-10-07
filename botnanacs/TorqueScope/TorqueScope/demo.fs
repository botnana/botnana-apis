variable tq-threshold 100 tq-threshold !
fvariable speed-change-distance 0.25e mm speed-change-distance f!
variable stopping

variable clock-start

fvariable press-p1
fvariable press-p2
fvariable press-v1
fvariable press-v2
fvariable release-p
fvariable release-v

: press-params! ( F: p1 p2 v1 v2 -- )
    press-v2 f!
    press-v1 f!
    press-p2 f!
    press-p1 f!
;

: release-params! ( F: p v -- )
    release-v f!
    release-p f!
;

: press-go ( F: p1 p2 v1 v2 -- )
    mtime clock-start !                 \ 紀錄起始時間 [ms]

	press-params!                       \ 將  p1 p2 v1 v2 存到變數      

    +coordinator                        \ 啟動軸組控制
	group-no @ group! 0path +group      \ 將第一個軸組設定為當前軸組，清除軸組路徑，啟動軸組

    mcs                                 \ 以目前機械座標為運動起點

    1 path-id!                                       \ 設定 path id
    press-v1 f@ feedrate!                            \ 設定 path feedrate V1
    press-p1 f@ speed-change-distance f@ f+ line1d   \ 插入第 1 個路徑，目標位置 P1 + speed-change-distance (P1 往上 speed-change-distance 的地方)

	press-v2 f@ feedrate!                            \ 設定 path feedrate V2
	press-p1 f@ line1d                               \ 插入第 2 個路徑，目標位置 P1，速度變化的地方，扭力會有急遽的變化，用此路徑緩衝

	2 path-id!                                       \ 設定 path id
    press-v2 f@ feedrate!                            \ 設定 path feedrate V2  
    press-p2 f@ line1d                               \ 插入第 3 個路徑，目標位置 P2

    press-v1 f@ vcmd!                                \ 設定 V1 為整個路徑的運動速度命令，實際速度會送到 path feedrate 與 group vmax 限制
    gstart                                           \ 啟動軸組加減速

	pause pause                                      \ 將控制權交出等待下一個周期
	                                                 \ 兩個周期不做事，讓加減速啟動進入運動狀態

    begin
        job-stop? not                                       \ 檢查是否運動中      
    while
        group-no @ group! next-path-id@ 2 =                 \ path id = 2
        torque-drive 2@ real-tq@ tq-threshold @ < and       \ 且 real torque 的絕對值小於門檻值
        stopping @ not and                                  \ 且 stopping = false
        if
            stop-job                                        \ 命令運動停止，依加減速停止 (停止後關閉軸組加減速)
            true stopping !
        then
		pause                                               \ 將控制權交出等待下一個周期
    repeat

    stop-job                                                \ 關閉軸組加減速                                             
    reset-job											    \ 清除路經資訊
    group-no @ group! -group                                \ 關閉軸組運動
    false stopping !
	." travel_time|" mtime clock-start @ - . ." ms" cr      \ 計算經過時間，並送出資訊
;

: release-go ( F: p v -- )
    release-params!                             \ 將  p, v 存到變數      

    +coordinator                                \ 啟動軸組控制
    release-v f@ axis-no @ interpolator-v!      \ 設定插值器速度
	axis-no @ +interpolator                     \ 開啟插值器
    release-p f@ axis-no @ axis-cmd-p!          \ 軸移動

	pause pause

    begin
        job-stop? not
    while
        pause
    repeat

    stop-job                           \ 停止後關閉插值器
;
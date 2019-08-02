\ 宣告要使用的正反器
1 constant ff-ax-5-hend-re  3 ff-ax-5-hend-re ff-type!      \ axis 5 HEND rising-edge trigger flip-flop
2 constant ff-ax-5-alm-re  3 ff-ax-5-alm-re ff-type!        \ axis 5 ALM rising-edge trigger flip-flop
3 constant ff-ax-5-alm-fe  4 ff-ax-5-alm-fe ff-type!        \ axis 5 ALM falling-edge trigger flip-flop
4 constant ff-ax-5-move-re  3 ff-ax-5-move-re ff-type!      \ axis 5 MOVE rising-edge trigger flip-flop
5 constant ff-ax-5-res-hl  1 ff-ax-5-res-hl ff-type!        \ axis 5 RES high-level tirgger flip-flop
    10000 ff-ax-5-res-hl ff-hold!


\ 更新所有的正反器
: ffs-forth ( -- )
    5 ax-hend? ff-ax-5-hend-re ff-forth-uc      \ 更新 axis 5 HEND 正反器
    5 ax-alm?                                   \ 更新 axis 5 ALM 正反器
    dup ff-ax-5-alm-re ff-forth-uc
    ff-ax-5-alm-fe ff-forth-uc
    5 ax-move? ff-ax-5-move-re ff-forth-uc      \ 更新 axis 5 MOVE 正反器
    5 ax-res? ff-ax-5-res-hl ff-forth-uc        \ 更新 axis 5 RES 正反器
;

\ 使用此命令達成簡單的 PLC
: plc-forth ( -- )
    ff-ax-5-hend-re ff-triggered-uc?            \ 檢查 axis 5 HEND rising-edge 正反器
    if
        5 -ax-home
    then

    ff-ax-5-alm-fe ff-triggered-uc?             \ 檢查 axis 5 ALM falling-edge 正反器
    ff-ax-5-res-hl ff-triggered-uc? or          \ 檢查 axis 5 RES high-level 正反器
    if
        5 -ax-res
        5 -ax-stp
    then

    ff-ax-5-move-re ff-triggered-uc?            \ 檢查 axis 5 MOVE rising-edge 正反器
    ff-ax-5-alm-re ff-triggered-uc? or          \ 檢查 axis 5 ALM rising-edge 正反器
    if
        5 -ax-cstr
        5 -ax-home
    then
;

\ 檢查從站的資訊正確才啟動 SFC
: start-sfc ( xt -- )
    rcon-gw-ch-slv ec-gateway?
    if
        rcon-gw-ch-slv gateway-in-len@ 144 =
        rcon-gw-ch-slv gateway-out-len@ 144 = and
        if
            +step
            exit
        then
    then
    drop
;

\ ==================== SFC ====================
\
\                   rcon-init
\                       |
\                       + rcon-init-done?
\                       |
\                       v
\                   rcon-forth
\

variable rcon-init-done

\ -------------------- steps --------------------
\ 等待 gateway-ready 後進行初始化
: rcon-init ( -- )
    rcon-gw-ch-slv gateway-ready?
    if
        +gw-mon
        true rcon-ready !
        true rcon-init-done !
    then
;
step rcon-init

\ 處理每個周期都要進行的工作
: rcon-forth ( -- )
    ffs-forth
    plc-enabled @ if plc-forth then
;
step rcon-forth

\ ----------------- transitions -----------------
\ 初始化完成?
: rcon-init-done? ( -- flag )
    rcon-init-done @
;
transition rcon-init-done?

\ -------------------- links --------------------
' rcon-init ' rcon-init-done? --> ' rcon-init-done? ' rcon-forth -->



' rcon-init start-sfc                       \ 啟動 SFC

true sfc-ready !                            \ SFC 全部載入完成

marker -nc
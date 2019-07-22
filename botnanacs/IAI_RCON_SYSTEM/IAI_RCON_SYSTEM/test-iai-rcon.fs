1 1 2constant gateway-ch-slv

\ flip-flops
1 constant ff-ax-5-hend  3 ff-ax-5-hend ff-type!        \ axis 5 HEND rising-edge trigger flip-flop
2 constant ff-ax-5-alm  4 ff-ax-5-alm ff-type!          \ axis 5 ALM falling-edge trigger flip-flop

\ ---------- output domain ----------
\ header
0 constant gw-cs-start-w
2 constant gw-cs-len-w
\ for channel
8  constant pos-spec-start-w
2  constant pos-spec-len-w
10 constant pos-width-start-w
2  constant pos-width-len-w
12 constant spd-start-w
1  constant spd-len-w
13 constant ac/dec-start-w
1  constant ac/dec-len-w
14 constant curr-limit-start-w
1  constant curr-limit-len-w
15 constant cs-start-w
1  constant cs-len-w

\ ---------- input domain ----------
\ header
0 constant gw-ss-start-w
2 constant gw-ss-len-w
\ for channel
8  constant pres-pos-start-w
2  constant pres-pos-len-w
10 constant cmd-curr-start-w
2  constant cmd-curr-len-w
12 constant pres-spd-start-w
1  constant pres-spd-len-w
\ 13 not available
14 constant alm-code-start-w
1  constant alm-code-len-w
15 constant ss-start-w
1  constant ss-len-w

8 constant ch-offset-w

: w-out-le! ( data w-len w-start -- )
    2 * swap 2 * swap gateway-ch-slv gateway-out-le!
;

: w-out-le@ ( w-len w-start -- )
    2 * swap 2 * swap gateway-ch-slv gateway-out-le@
;

: w-in-le@ ( w-len w-start -- )
    2 * swap 2 * swap gateway-ch-slv gateway-in-le@
;



\ ========== gateway address configurations ==========
\ gateway control signal
: gw-cs! ( cs -- )
    gw-cs-len-w gw-cs-start-w w-out-le!
;
: gw-cs@ ( -- cs )
    gw-cs-len-w gw-cs-start-w w-out-le@
;
$8000 constant gw-mon
: +gw-mon  gw-mon gw-cs@ or gw-cs! ;
: -gw-mon  gw-mon invert gw-cs@ and gw-cs! ;

\ show gateway output data
: .gw-out
    gw-cs@
    ." GW_MON|" dup gw-mon and 0<> 0 .r ." |" cr
    drop
;

\ status signal
: gw-ss@ ( -- ss )
    gw-ss-len-w gw-ss-start-w w-in-le@
;
$100 constant gw-semg
$400 constant gw-alml
$800 constant gw-almh
$1000 constant gw-mod
$2000 constant gw-errt
$4000 constant gw-lerc
$8000 constant gw-run
: gw-almc@  1 0 1 1 gateway-in-le@ ;
: gw-semg?  gw-ss@ gw-semg and 0<> ;
: gw-alml?  gw-ss@ gw-alml and 0<> ;
: gw-almh?  gw-ss@ gw-almh and 0<> ;
: gw-mod?  gw-ss@ gw-mod and 0<> ;
: gw-errt?  gw-ss@ gw-errt and 0<> ;
: gw-lerc?  gw-ss@ gw-lerc and 0<> ;
: gw-run?  gw-ss@ gw-run and 0<> ;
: gw-lnk@  2 2 1 1 gateway-in-le@ ;

\ show gateway input data
: .gw-in
    gw-ss@
    ." GW_ALMC|" gw-almc@ 0 .r ." |" cr
    ." GW_SEMG|" dup gw-semg and 0<> 0 .r ." |" cr
    ." GW_ALML|" dup gw-alml and 0<> 0 .r ." |" cr
    ." GW_ALMH|" dup gw-almh and 0<> 0 .r ." |" cr
    ." GW_MOD|" dup gw-mod and 0<> 0 .r ." |" cr
    ." GW_ERRT|" dup gw-errt and 0<> 0 .r ." |" cr
    ." GW_LERC|" dup gw-lerc and 0<> 0 .r ." |" cr
    ." GW_RUN|" dup gw-run and 0<> 0 .r ." |" cr
    ." GW_LNK|" gw-lnk@ 0 .r ." |" cr
    drop
;

: .gw
    ." control signals:" cr
    .gw-out cr
    ." status signals:" cr
    .gw-in cr
;



\ ========== data address configurations : direct numerical control ==========
\ axis position specified data
: ax-pos-spec! ( data ax-idx -- )
    pos-spec-len-w swap ch-offset-w * pos-spec-start-w + w-out-le!
;
: ax-pos-spec@ ( ax-idx -- data )
    pos-spec-len-w swap ch-offset-w * pos-spec-start-w + w-out-le@
;

\ axis positioning width
: ax-pos-width! ( data ax-idx -- )
    pos-width-len-w swap ch-offset-w * pos-width-start-w + w-out-le!
;
: ax-pos-width@ ( ax-idx -- data )
    pos-width-len-w swap ch-offset-w * pos-width-start-w + w-out-le@
;

\ axis speed
: ax-spd! ( data ax-idx -- )
    spd-len-w swap ch-offset-w * spd-start-w + w-out-le!
;
: ax-spd@ ( ax-idx -- data )
    spd-len-w swap ch-offset-w * spd-start-w + w-out-le@
;

\ asix acceleration/deceleration
: ax-ac/dec! ( data ax-idx -- )
    ac/dec-len-w swap ch-offset-w * ac/dec-start-w + w-out-le!
;
: ax-ac/dec@ ( ax-idx -- data )
    ac/dec-len-w swap ch-offset-w * ac/dec-start-w + w-out-le@
;

\ axis pushing current limit value
: ax-curr-limit! ( data ax-idx -- )
    curr-limit-len-w swap ch-offset-w * curr-limit-start-w + w-out-le!
;
: ax-curr-limit@ ( ax-idx -- data )
    curr-limit-len-w swap ch-offset-w * curr-limit-start-w + w-out-le@
;

\ axis control signal
: ax-cs! ( cs ax-idx -- )
    cs-len-w swap ch-offset-w * cs-start-w + w-out-le!
;
: ax-cs@ ( ax-idx -- cs )
    cs-len-w swap ch-offset-w * cs-start-w + w-out-le@
;
$1 constant ax-cstr
$2 constant ax-home
$4 constant ax-stp
$8 constant ax-res
$10 constant ax-son
$20 constant ax-jisl
$40 constant ax-jvel
$80 constant ax-jog-
$100 constant ax-jog+
$1000 constant ax-push
$2000 constant ax-dir
$4000 constant ax-inc
$8000 constant ax-bkrl
: +ax-cstr ( ax-idx -- ) dup ax-cs@ ax-cstr or swap ax-cs! ;
: -ax-cstr ( ax-idx -- ) dup ax-cs@ ax-cstr invert and swap ax-cs! ;
: +ax-home ( ax-idx -- ) dup ax-cs@ ax-home or swap ax-cs! ;
: -ax-home ( ax-idx -- ) dup ax-cs@ ax-home invert and swap ax-cs! ;
: +ax-stp ( ax-idx -- ) dup ax-cs@ ax-stp or swap ax-cs! ;
: -ax-stp ( ax-idx -- ) dup ax-cs@ ax-stp invert and swap ax-cs! ;
: +ax-res ( ax-idx -- ) dup ax-cs@ ax-res or swap ax-cs! ;
: -ax-res ( ax-idx -- ) dup ax-cs@ ax-res invert and swap ax-cs! ;
: +ax-son ( ax-idx -- ) dup ax-cs@ ax-son or swap ax-cs! ;
: -ax-son ( ax-idx -- ) dup ax-cs@ ax-son invert and swap ax-cs! ;
: +ax-jisl ( ax-idx -- ) dup ax-cs@ ax-jisl or swap ax-cs! ;
: -ax-jisl ( ax-idx -- ) dup ax-cs@ ax-jisl invert and swap ax-cs! ;
: +ax-jvel ( ax-idx -- ) dup ax-cs@ ax-jvel or swap ax-cs! ;
: -ax-jvel ( ax-idx -- ) dup ax-cs@ ax-jvel invert and swap ax-cs! ;
: +ax-jog- ( ax-idx -- ) dup ax-cs@ ax-jog- or swap ax-cs! ;
: -ax-jog- ( ax-idx -- ) dup ax-cs@ ax-jog- invert and swap ax-cs! ;
: +ax-jog+ ( ax-idx -- ) dup ax-cs@ ax-jog+ or swap ax-cs! ;
: -ax-jog+ ( ax-idx -- ) dup ax-cs@ ax-jog+ invert and swap ax-cs! ;
: +ax-push ( ax-idx -- ) dup ax-cs@ ax-push or swap ax-cs! ;
: -ax-push ( ax-idx -- ) dup ax-cs@ ax-push invert and swap ax-cs! ;
: +ax-dir ( ax-idx -- ) dup ax-cs@ ax-dir or swap ax-cs! ;
: -ax-dir ( ax-idx -- ) dup ax-cs@ ax-dir invert and swap ax-cs! ;
: +ax-inc ( ax-idx -- ) dup ax-cs@ ax-inc or swap ax-cs! ;
: -ax-inc ( ax-idx -- ) dup ax-cs@ ax-inc invert and swap ax-cs! ;
: +ax-bkrl ( ax-idx -- ) dup ax-cs@ ax-bkrl or swap ax-cs! ;
: -ax-bkrl ( ax-idx -- ) dup ax-cs@ ax-bkrl invert and swap ax-cs! ;

\ show axis output data
: .ax-out ( ax-idx -- )
    ." AX_PSD|" dup ax-pos-spec@ 0 .r ." |" cr
    ." AX_PW|" dup ax-pos-width@ 0 .r ." |" cr
    ." AX_SPD|" dup ax-spd@ 0 .r ." |" cr
    ." AX_ACDEC|" dup ax-ac/dec@ 0 .r ." |" cr
    ." AX_PCLV|" dup ax-curr-limit@ 0 .r ." |" cr
    ax-cs@
    ." AX_CSTR|" dup ax-cstr and 0<> 0 .r ." |" cr
    ." AX_HOME|" dup ax-home and 0<> 0 .r ." |" cr
    ." AX_STP|" dup ax-stp and 0<> 0 .r ." |" cr
    ." AX_RES|" dup ax-res and 0<> 0 .r ." |" cr
    ." AX_SON|" dup ax-son and 0<> 0 .r ." |" cr
    ." AX_JISL|" dup ax-jisl and 0<> 0 .r ." |" cr
    ." AX_JVEL|" dup ax-jvel and 0<> 0 .r ." |" cr
    ." AX_JOG-|" dup ax-jog- and 0<> 0 .r ." |" cr
    ." AX_JOG+|" dup ax-jog+ and 0<> 0 .r ." |" cr
    ." AX_PUSH|" dup ax-push and 0<> 0 .r ." |" cr
    ." AX_DIR|" dup ax-dir and 0<> 0 .r ." |" cr
    ." AX_INC|" dup ax-inc and 0<> 0 .r ." |" cr
    ." AX_BKRL|" dup ax-bkrl and 0<> 0 .r ." |" cr
    drop
;

\ axis present position data
: ax-pres-pos@ ( ax-idx -- )
    pres-pos-len-w swap ch-offset-w * pres-pos-start-w + w-in-le@
;

\ axis motor command current value
: ax-cmd-curr@ ( ax-idx -- )
    cmd-curr-len-w swap ch-offset-w * cmd-curr-start-w + w-in-le@
;

\ axis present speed
: ax-pres-spd@ ( ax-idx -- )
    pres-spd-len-w swap ch-offset-w * pres-spd-start-w + w-in-le@
;

\ axis alarm code
: ax-alm-code@ ( ax-idx -- )
    alm-code-len-w swap ch-offset-w * alm-code-start-w + w-in-le@
;

\ axis status signal
: ax-ss@ ( ax-idx -- ss )
    ss-len-w swap ch-offset-w * ss-start-w + w-in-le@
;

\ axis status signals
$1 constant ax-pend
$2 constant ax-hend
$4 constant ax-move
$8 constant ax-alm
$10 constant ax-sv
$20 constant ax-psfl
$40 constant ax-load
$80 constant ax-alml
$100 constant ax-mend
$200 constant ax-wend
$400 constant ax-modes
$800 constant ax-pzone
$1000 constant ax-zone1
$2000 constant ax-zone2
$4000 constant ax-crdy
$8000 constant ax-emgs
: ax-pend? ( ax-idx -- ) ax-ss@ ax-pend and 0<> ;
: ax-hend? ( ax-idx -- ) ax-ss@ ax-hend and 0<> ;
: ax-move? ( ax-idx -- ) ax-ss@ ax-move and 0<> ;
: ax-alm? ( ax-idx -- ) ax-ss@ ax-alm and 0<> ;
: ax-sv? ( ax-idx -- ) ax-ss@ ax-sv and 0<> ;
: ax-psfl? ( ax-idx -- ) ax-ss@ ax-psfl and 0<> ;
: ax-load? ( ax-idx -- ) ax-ss@ ax-load and 0<> ;
: ax-alml? ( ax-idx -- ) ax-ss@ ax-alml and 0<> ;
: ax-mend? ( ax-idx -- ) ax-ss@ ax-mend and 0<> ;
: ax-wend? ( ax-idx -- ) ax-ss@ ax-wend and 0<> ;
: ax-modes? ( ax-idx -- ) ax-ss@ ax-modes and 0<> ;
: ax-pzone? ( ax-idx -- ) ax-ss@ ax-pzone and 0<> ;
: ax-zone1? ( ax-idx -- ) ax-ss@ ax-zone1 and 0<> ;
: ax-zone2? ( ax-idx -- ) ax-ss@ ax-zone2 and 0<> ;
: ax-crdy? ( ax-idx -- ) ax-ss@ ax-crdy and 0<> ;
: ax-emgs? ( ax-idx -- ) ax-ss@ ax-emgs and 0<> ;

\ show axis input data
: .ax-in ( ax-idx -- )
    ." AX_PPD|" dup ax-pres-pos@ 0 .r ." |" cr
    ." AX_MCCV|" dup ax-cmd-curr@ 0 .r ." |" cr
    ." AX_PSPD|" dup ax-pres-spd@ 0 .r ." |" cr
    ." AX_ALMC|" dup ax-alm-code@ 0 .r ." |" cr
    ax-ss@
    ." AX_PEND|" dup ax-pend and 0<> 0 .r ." |" cr
    ." AX_HEND|" dup ax-hend and 0<> 0 .r ." |" cr
    ." AX_MOVE|" dup ax-move and 0<> 0 .r ." |" cr
    ." AX_ALM|" dup ax-alm and 0<> 0 .r ." |" cr
    ." AX_SV|" dup ax-sv and 0<> 0 .r ." |" cr
    ." AX_PSFL|" dup ax-psfl and 0<> 0 .r ." |" cr
    ." AX_LOAD|" dup ax-load and 0<> 0 .r ." |" cr
    ." AX_ALML|" dup ax-alml and 0<> 0 .r ." |" cr
    ." AX_MEND|" dup ax-mend and 0<> 0 .r ." |" cr
    ." AX_WEND|" dup ax-wend and 0<> 0 .r ." |" cr
    ." AX_MODES|" dup ax-modes and 0<> 0 .r ." |" cr
    ." AX_PZONE|" dup ax-pzone and 0<> 0 .r ." |" cr
    ." AX_ZONE1|" dup ax-zone1 and 0<> 0 .r ." |" cr
    ." AX_ZONE2|" dup ax-zone2 and 0<> 0 .r ." |" cr
    ." AX_CRDY|" dup ax-crdy and 0<> 0 .r ." |" cr
    ." AX_EMGS|" dup ax-emgs and 0<> 0 .r ." |" cr
    drop
;

: .ax ( ax-idx -- )
    ." control signals:" cr
    dup .ax-out cr
    ." status signals:" cr
    .ax-in cr
;



: start-sfc ( xt -- )
    \ TODO: is-gateway?
    gateway-ch-slv gateway-in-len@ 144 =
    gateway-ch-slv gateway-out-len@ 144 = and
    if
        +step
    else
        drop
    then
;



\ ========== SFC ==========
\
\           rcon-init
\               |
\               + rcon-init-done?
\               |
\               v
\           rcon-forth
\

variable sfc-ready
variable rcon-init-done
variable plc-enabled

\ 更新所有的正反器
: ffs-forth ( -- )
    5 ax-hend? ff-ax-5-hend ff-forth-uc         \ 更新 axis 5 hend 正反器
    5 ax-alm? ff-ax-5-alm ff-forth-uc           \ 更新 axis 5 alm 正反器
;

\ 目前只做 axis 5 的 PLC
: plc-forth ( -- )
    ff-ax-5-hend ff-triggered-uc?               \ 檢查 axis 5 hend 正反器
    if
        5 -ax-home
    then

    ff-ax-5-alm ff-triggered-uc?                \ 檢查 axis 5 alm 正反器
    if
        5 -ax-res
    then
;

: poll ( ax-idx -- )
    rcon-init-done @
    if
        ." GW_control|" gw-cs@ 0 .r ." |"
        ." GW_status|" gw-ss@ 0 .r ." |"
        ." AX_control|" dup ax-cs@ 0 .r ." |"
        ." AX_PSD|" dup ax-pos-spec@ 0 .r ." |"
        ." AX_PW|" dup ax-pos-width@ 0 .r ." |"
        ." AX_SPD|" dup ax-spd@ 0 .r ." |"
        ." AX_ACDEC|" dup ax-ac/dec@ 0 .r ." |"
        ." AX_PCLV|" dup ax-curr-limit@ 0 .r ." |"
        ." AX_status|" dup ax-ss@ 0 .r ." |"
        ." AX_PPD|" dup ax-pres-pos@ 0 .r ." |"
        ." AX_MCCV|" dup ax-cmd-curr@ 0 .r ." |"
        ." AX_PSPD|" dup ax-pres-spd@ 0 .r ." |"
        ." AX_ALMC|" dup ax-alm-code@ 0 .r ." |"
    then
    drop
    ." SFC_ready|" sfc-ready @ 0 .r ." |"
    ." RCON_ready|" rcon-init-done @ 0 .r ." |"
    ." PLC_enabled|" plc-enabled @ 0 .r ." |"
;

\ ---------- steps ----------
\ 等待 gateway-ready 後初始化
: rcon-init ( -- )
    gateway-ch-slv gateway-ready?
    if
        +gw-mon
        true rcon-init-done !
    then
;
step rcon-init

: rcon-forth ( -- )
    ffs-forth
    plc-enabled @ if plc-forth then
;
step rcon-forth

\ ---------- transitions ----------
: rcon-init-done? ( -- flag )
    rcon-init-done @
;
transition rcon-init-done?

\ ---------- links ----------
' rcon-init ' rcon-init-done? --> ' rcon-init-done? ' rcon-forth -->



' rcon-init start-sfc                       \ 啟動 SFC

true sfc-ready !                            \ SFC 全部載入完成
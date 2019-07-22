variable sfc-ready
variable rcon-ready
variable plc-enabled

: config-slave ( slave alias -- ) ( input buff: name )
    dup ec-alias?               \ 若 alias 存在
    if
        swap drop ec-a>n            \ 由該 alias 取得對應的 slave NO.
    else
        drop                        \ 使用給定的 slave NO.
    then
    create , does> @
;

\ RCON-GW-EC 所在的 channel NO. 和 slave NO.
1 10 config-slave rcon-gw-slv
1 rcon-gw-slv 2constant rcon-gw-ch-slv

\ 將 gateway-out-le! 命令包裝成以 word 為單位進行操作
: w-out-le! ( data w-len w-start -- )
    2 * swap 2 * swap rcon-gw-ch-slv gateway-out-le!
;

\ 將 gateway-out-le@ 命令包裝成以 word 為單位進行操作
: w-out-le@ ( w-len w-start -- )
    2 * swap 2 * swap rcon-gw-ch-slv gateway-out-le@
;

\ 將 gateway-in-le@ 命令包裝成以 word 為單位進行操作
: w-in-le@ ( w-len w-start -- )
    2 * swap 2 * swap rcon-gw-ch-slv gateway-in-le@
;

\ Gateway output domain 中資料對應的位址與長度
\ Fixed region
0 constant gw-cs-start-w
2 constant gw-cs-len-w

\ Data region (Direct numerical control mode)
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

\ Gateway input domain 中資料對應的位址與長度
\ Fixed region
0 constant gw-ss-start-w
2 constant gw-ss-len-w

\ Data region (Direct numerical control mode)
8  constant pres-pos-start-w
2  constant pres-pos-len-w
10 constant cmd-curr-start-w
2  constant cmd-curr-len-w
12 constant pres-spd-start-w
1  constant pres-spd-len-w
\ 13 未使用
14 constant alm-code-start-w
1  constant alm-code-len-w
15 constant ss-start-w
1  constant ss-len-w

\ word offset per axis, Direct numerical control mode 每個 axis 佔 8 個 word
8 constant ax-offset-w



\ Set gateway control signal
: gw-cs! ( cs -- )
    gw-cs-len-w gw-cs-start-w w-out-le!
;

\ Fetch gateway control signal
: gw-cs@ ( -- cs )
    gw-cs-len-w gw-cs-start-w w-out-le@
;

\ Gateway control signal 內容
$8000 constant gw-mon
: +gw-mon  gw-mon gw-cs@ or gw-cs! ;
: -gw-mon  gw-mon invert gw-cs@ and gw-cs! ;



\ Fetch Gateway status signal
: gw-ss@ ( -- ss )
    gw-ss-len-w gw-ss-start-w w-in-le@
;

\ Gateway status signal 內容
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



\ Set axis position specified data
: ax-pos-spec! ( data ax-idx -- )
    pos-spec-len-w swap ax-offset-w * pos-spec-start-w + w-out-le!
;

\ Fetch axis position specified data
: ax-pos-spec@ ( ax-idx -- data )
    pos-spec-len-w swap ax-offset-w * pos-spec-start-w + w-out-le@
;

\ Set positioning width
: ax-pos-width! ( data ax-idx -- )
    pos-width-len-w swap ax-offset-w * pos-width-start-w + w-out-le!
;

\ Fetch positioning width
: ax-pos-width@ ( ax-idx -- data )
    pos-width-len-w swap ax-offset-w * pos-width-start-w + w-out-le@
;

\ Set axis speed
: ax-spd! ( data ax-idx -- )
    spd-len-w swap ax-offset-w * spd-start-w + w-out-le!
;

\ Fetch axis speed
: ax-spd@ ( ax-idx -- data )
    spd-len-w swap ax-offset-w * spd-start-w + w-out-le@
;

\ Set asix acceleration/deceleration
: ax-ac/dec! ( data ax-idx -- )
    ac/dec-len-w swap ax-offset-w * ac/dec-start-w + w-out-le!
;

\ Fetch asix acceleration/deceleration
: ax-ac/dec@ ( ax-idx -- data )
    ac/dec-len-w swap ax-offset-w * ac/dec-start-w + w-out-le@
;

\ Set axis pushing current limit value
: ax-curr-limit! ( data ax-idx -- )
    curr-limit-len-w swap ax-offset-w * curr-limit-start-w + w-out-le!
;

\ Fetch axis pushing current limit value
: ax-curr-limit@ ( ax-idx -- data )
    curr-limit-len-w swap ax-offset-w * curr-limit-start-w + w-out-le@
;

\ Set axis control signal
: ax-cs! ( cs ax-idx -- )
    cs-len-w swap ax-offset-w * cs-start-w + w-out-le!
;

\ Fetch axis control signal
: ax-cs@ ( ax-idx -- cs )
    cs-len-w swap ax-offset-w * cs-start-w + w-out-le@
;

\ Axis control signal 內容
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
: ax-cstr? ( ax-idx -- ) ax-cs@ ax-cstr and 0<> ;
: +ax-home ( ax-idx -- ) dup ax-cs@ ax-home or swap ax-cs! ;
: -ax-home ( ax-idx -- ) dup ax-cs@ ax-home invert and swap ax-cs! ;
: ax-home? ( ax-idx -- ) ax-cs@ ax-home and 0<> ;
: +ax-stp ( ax-idx -- ) dup ax-cs@ ax-stp or swap ax-cs! ;
: -ax-stp ( ax-idx -- ) dup ax-cs@ ax-stp invert and swap ax-cs! ;
: ax-stp? ( ax-idx -- ) ax-cs@ ax-stp and 0<> ;
: +ax-res ( ax-idx -- ) dup ax-cs@ ax-res or swap ax-cs! ;
: -ax-res ( ax-idx -- ) dup ax-cs@ ax-res invert and swap ax-cs! ;
: ax-res? ( ax-idx -- ) ax-cs@ ax-res and 0<> ;
: +ax-son ( ax-idx -- ) dup ax-cs@ ax-son or swap ax-cs! ;
: -ax-son ( ax-idx -- ) dup ax-cs@ ax-son invert and swap ax-cs! ;
: ax-son? ( ax-idx -- ) ax-cs@ ax-son and 0<> ;
: +ax-jisl ( ax-idx -- ) dup ax-cs@ ax-jisl or swap ax-cs! ;
: -ax-jisl ( ax-idx -- ) dup ax-cs@ ax-jisl invert and swap ax-cs! ;
: ax-jisl? ( ax-idx -- ) ax-cs@ ax-jisl and 0<> ;
: +ax-jvel ( ax-idx -- ) dup ax-cs@ ax-jvel or swap ax-cs! ;
: -ax-jvel ( ax-idx -- ) dup ax-cs@ ax-jvel invert and swap ax-cs! ;
: ax-jvel? ( ax-idx -- ) ax-cs@ ax-jvel and 0<> ;
: +ax-jog- ( ax-idx -- ) dup ax-cs@ ax-jog- or swap ax-cs! ;
: -ax-jog- ( ax-idx -- ) dup ax-cs@ ax-jog- invert and swap ax-cs! ;
: ax-jog-? ( ax-idx -- ) ax-cs@ ax-jog- and 0<> ;
: +ax-jog+ ( ax-idx -- ) dup ax-cs@ ax-jog+ or swap ax-cs! ;
: -ax-jog+ ( ax-idx -- ) dup ax-cs@ ax-jog+ invert and swap ax-cs! ;
: ax-jog+? ( ax-idx -- ) ax-cs@ ax-jog+ and 0<> ;
: +ax-push ( ax-idx -- ) dup ax-cs@ ax-push or swap ax-cs! ;
: -ax-push ( ax-idx -- ) dup ax-cs@ ax-push invert and swap ax-cs! ;
: ax-push? ( ax-idx -- ) ax-cs@ ax-push and 0<> ;
: +ax-dir ( ax-idx -- ) dup ax-cs@ ax-dir or swap ax-cs! ;
: -ax-dir ( ax-idx -- ) dup ax-cs@ ax-dir invert and swap ax-cs! ;
: ax-dir? ( ax-idx -- ) ax-cs@ ax-dir and 0<> ;
: +ax-inc ( ax-idx -- ) dup ax-cs@ ax-inc or swap ax-cs! ;
: -ax-inc ( ax-idx -- ) dup ax-cs@ ax-inc invert and swap ax-cs! ;
: ax-inc? ( ax-idx -- ) ax-cs@ ax-inc and 0<> ;
: +ax-bkrl ( ax-idx -- ) dup ax-cs@ ax-bkrl or swap ax-cs! ;
: -ax-bkrl ( ax-idx -- ) dup ax-cs@ ax-bkrl invert and swap ax-cs! ;
: ax-bkrl? ( ax-idx -- ) ax-cs@ ax-bkrl and 0<> ;



\ Fetch axis present position data
: ax-pres-pos@ ( ax-idx -- )
    pres-pos-len-w swap ax-offset-w * pres-pos-start-w + w-in-le@
;

\ Fetch axis motor command current value
: ax-cmd-curr@ ( ax-idx -- )
    cmd-curr-len-w swap ax-offset-w * cmd-curr-start-w + w-in-le@
;

\ Fetch axis present speed
: ax-pres-spd@ ( ax-idx -- )
    pres-spd-len-w swap ax-offset-w * pres-spd-start-w + w-in-le@
;

\ Fetch axis alarm code
: ax-alm-code@ ( ax-idx -- )
    alm-code-len-w swap ax-offset-w * alm-code-start-w + w-in-le@
;

\ Fetch axis status signal
: ax-ss@ ( ax-idx -- ss )
    ss-len-w swap ax-offset-w * ss-start-w + w-in-le@
;

\ Axis status signal 內容
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



\ Print gateway input/output data
: .gw ( -- )
    ." GW_control|" gw-cs@ 0 .r ." |"
    ." GW_status|" gw-ss@ 0 .r ." |"
;

\ Print axis input/output data
: .ax ( ax-idx -- )
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
    drop
;

: poll ( ax-idx -- )
    rcon-ready @
    if
        .gw
        .ax
    else
        drop
    then
    ." SFC_ready|" sfc-ready @ 0 .r ." |"
    ." RCON_ready|" rcon-ready @ 0 .r ." |"
    ." PLC_enabled|" plc-enabled @ 0 .r ." |"
;
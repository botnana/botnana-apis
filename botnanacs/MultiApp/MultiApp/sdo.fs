\ 此處的 sdo-request 指令供使用者建造自己的 SDO request 資料結構，確定欄位設定正確後可以
\ 使用 sdo-reqs-push-uncheck 將 SDO request 放入佇列中，由 SDO SFC 依序處理。
\
\ 使用範例:
\   建造一個 SDO request 資料結構名為 my-req
\
\       sdo-request my-req
\
\   定義 my-req 要處理的 SDO 操作
\
\       : upload-my-req
\           my-req sdo-request.slv-no @ $1 $1018 sdo-upload-u32
\       ;
\
\   定義 my-req 的 SDO 操作成功後要進行的動作
\
\       variable my-data
\       : my-req-success
\        my-req sdo-request.slv-no @ sdo-data@ my-data !
\       ;
\
\    設定 my-req 的欄位內容
\
\       ' upload-my-req my-req sdo-request.'cmd !
\       ' my-req-success my-req sdo-request.'seccess !
\       1 my-req sdo-request.slv-no !
\
\   也可以設定 my-req 的 SDO 操作失敗後要進行的動作，預設為空指令(noop)
\   確定佇列尚有空間(sdo-reqs-space > 0)後將 my-req 放入佇列中等待 SDO SFC 處理
\
\       my-req sdo-req-push-uncheck
\



\ 定義 SDO request 資料結構的欄位，其中 'cmd, 'success, 'error 為某個指令( -- )的令牌
0
1 cells +field sdo-request.'cmd
1 cells +field sdo-request.'success
1 cells +field sdo-request.'error
1 cells +field sdo-request.slv-no
constant /sdo-request

\ sdo-request <name>，用來建造一個 sdo-request 資料結構
: sdo-request ( -- )
    create here /sdo-request allot      ( addr )
    ['] noop swap                       ( 'noop addr )
    2dup sdo-request.'cmd !
    2dup sdo-request.'success !
    sdo-request.'error !                ( )
;

\ SDO requests queue
variable sdo-reqs-cap 128 sdo-reqs-cap !
variable sdo-reqs-len
variable sdo-reqs-start
create sdo-reqs sdo-reqs-cap @ cells allot

\ 取得 sdo-reqs queue 剩餘的空間
: sdo-reqs-space@ ( -- space )
    sdo-reqs-cap @ sdo-reqs-len @ -
;

\ 清除 sdo-reqs queue
: 0sdo-reqs ( -- )
    0 sdo-reqs-len !
;

\ 將一個 sdo-request 從 sdo-reqs queue 的尾端放入
\ 需確定 req 指向一個 sdo-request 結構，且 req.slv-no 對應的從站存在，且 sdo-reqs-space > 0
: sdo-reqs-push-uncheck ( req -- )
    sdo-reqs sdo-reqs-start @ sdo-reqs-len @ + sdo-reqs-cap @ mod cells + !
    1 sdo-reqs-len +!
;

\ 從 sdo-reqs queue 的開頭拿出一個 sdo-request，需確定 sdo-reqs-len > 0
: sdo-reqs-pop-uncheck ( -- req )
    sdo-reqs sdo-reqs-start @ cells + @
    -1 sdo-reqs-len +!
    sdo-reqs-start dup @ 1+ sdo-reqs-cap @ mod swap !
;



\ ==================== SFC ====================
\
\                    sdo-init
\                       |
\                       + sdo-init-done?
\                       |
\                       v
\                    sdo-idle
\                       |
\                       + sdo-has-req?
\                       |
\                       v
\                    sdo-forth
\                       |
\                       + sdo-forth-done?
\                       |
\                       v
\                    sdo-idle
\

variable sdo-init-done
variable sdo-forth-step
variable sdo-curr-req

\ -------------------- steps --------------------
\ 初始化
: sdo-init ( -- )
    sdo-init-done @ not
    system-ready? and
    if
        0sdo-reqs
        sdo-init-done on
    then
;
step sdo-init

\ 閒置中
: sdo-idle ( -- )
    0 sdo-forth-step !
;
step sdo-idle

\ 運行處理 SDO request 的流程
: sdo-forth ( -- )
    sdo-forth-step @
    case
        \ 從 sdo-reqs queue 拿出一個 sdo-request 並執行 'cmd
        0 of
            sdo-reqs-pop-uncheck dup sdo-curr-req !     ( req )
            sdo-request.'cmd @ execute
            1 sdo-forth-step +!                         \ 切換至下一步
        endof

        \ 等待至 sdo-busy? = false
        1 of
            sdo-curr-req @ sdo-request.slv-no @ sdo-busy? not
            if
                1 sdo-forth-step +!                     \ 切換至下一步
            then
        endof

        \ 根據 sdo-error? 執行 'error 或 'success
        2 of
            sdo-curr-req @ dup sdo-request.slv-no @ sdo-error?
            if
                sdo-request.'error @ execute
            else
                sdo-request.'success @ execute
            then
            1 sdo-forth-step +!                         \ 切換至下一步
        endof

        \ 檢查 sdo-reqs queue 中是否還有 sdo-request
        3 of
            sdo-reqs-len @ 0>
            if
                0 sdo-forth-step !                      \ 重新執行流程
            else
                -1 sdo-forth-step !                     \ 離開流程
            then
        endof
    endcase
;
step sdo-forth



\ -------------------- transitions --------------------
\ 初始化完成?
: sdo-init-done? ( -- t )
    sdo-init-done @
;
transition sdo-init-done?

\ 是否有 request ?
: sdo-has-req? ( -- t )
    sdo-reqs-len @ 0 >
;
transition sdo-has-req?

\ 處理 SDO request 的流程是否結束?
: sdo-forth-done? ( -- t )
    sdo-forth-step @ -1 =
;
transition sdo-forth-done?



\ -------------------- links --------------------
' sdo-init  ' sdo-init-done?    --> ' sdo-init-done?    ' sdo-idle  -->
' sdo-idle  ' sdo-has-req?      --> ' sdo-has-req?      ' sdo-forth -->
' sdo-forth ' sdo-forth-done?   --> ' sdo-forth-done?   ' sdo-idle  -->
\ SDO UPLOAD SFC


\        +------------+
\  +-----+    Idle    |
\  |     +-----+------+
\  |           |
\  |         --+--  Receive request ?
\  |           |
\  |  +--------+----------+
\  |  | Reset param index | 
\  |  +--------+----------+
\  |           |
\  |         --+-- Y  
\  |           |
\  |  +--------+----------+
\  |  |  Send SDO Command +------+   
\  |  +--------+----------+      |
\  |           |                 |
\  |         --+-- Wait SDO      |
\  |           |                 |
\  |  +--------+----------+      |
\  |  |  Generate Message |      |
\  |  |   param index +1  |      |
\  |  +---+---------------+      |
\  |      |                      |
\  | All params ok?  -+          |
\  |      | Y         | N        |
\  |      |           |          |
\  +------+           +----------+


\ 宣告要處理的暫存器資訊，總計有 14 個  
1 constant params-len
variable param-index 
create params-once params-len cells allot
create object-slave 1 ,
create object-index $60B9 ,
create object-subindex 0 , 
create 'params-request ' sdo-upload-u16 ,
                       
\ Fetch parameter
: param@ ( index addr -- value )
  swap cells + @
;

\ Set parameter
: param! ( value index addr -- )
  swap cells + !
;                       

\ 回傳暫存器的位址資訊
variable temp-index
: object-address  ( index -- subindex index slave )
    temp-index !
    temp-index @ object-subindex  param@
    temp-index @ object-index     param@
    temp-index @ object-slave     param@
    ;

\ IDLE Step    
: param-proc-idle
	;

\ 接收要更新暫存器啟動命令
variable param-request-allowed
: send-param-request
  true param-request-allowed !
  ;
    
\ 是否收到更新的命令    
: wait-param-request? 
	param-request-allowed @
    ;

\ Reset param index    
: reset-param-index
    0 param-index !
    0 param-request-allowed !
    ;    

\ To command loop    
: to-command-loop?
    true
    ;

\ 以 param-index 判斷是否完成所有的參數 
: upload-finished?
    param-index @ params-len >=
    ;

\ 以 param-index 判斷是否完成所有的參數 
: upload-not-finished?
    upload-finished? not
    ;    

\ 依據目前的 param index 送出 SDO Requst    
: send-upload-command
    param-index @ params-once param@ not if
        param-index @ object-address param-index @ 'params-request param@ execute
        true param-index @ params-once param!
    then
    ;

\ 等待從站回應 SDO Request
: wait-sdo-data? 
    waiting-requests? not
    ;    
    
\ 收到資料後，送出訊息給 user task，將 param-index +1
: post-upload-command     
    false param-index @ params-once param!
     ." sdo."  param-index @ object-subindex param@ 0 .r
     ." ."     param-index @ object-index    param@ 0 .r 
     ." ."     param-index @ object-slave    param@ 0 .r
     param-index @ object-slave param@ sdo-error? if   
          ." |--" cr   
     else
         ." |" param-index @ object-slave param@  sdo-data@ 0 .r cr 
     then   
    param-index @ 1+  param-index !
    ;    
   

\ 宣告 SFC Step    
step param-proc-idle
step reset-param-index
step send-upload-command
step post-upload-command    
   
\ 宣告 SFC Transition
transition wait-param-request?
transition to-command-loop?
transition wait-sdo-data?    
transition upload-finished? 
transition upload-not-finished?
    
\ 連結 SFC
   
' param-proc-idle      ' wait-param-request? -->    
' wait-param-request?  ' reset-param-index   -->   
' reset-param-index    ' to-command-loop? -->
' to-command-loop?     ' send-upload-command -->
' send-upload-command  ' wait-sdo-data?  --> 
' wait-sdo-data?       ' post-upload-command --> 
' post-upload-command  ' upload-finished? -->
' post-upload-command  ' upload-not-finished? -->
' upload-finished?     ' param-proc-idle -->
' upload-not-finished? ' send-upload-command  -->

\ 啟動 SFC
\ ' param-proc-idle +step

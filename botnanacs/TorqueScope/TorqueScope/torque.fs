\ ==================== SFC ====================
\
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
: torque-init ( -- )
	torque-init-done @ not
	system-ready? and
	if
		\ You can do something here

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
: torque-init-done? ( -- t )
	torque-init-done @
;
transition torque-init-done?



\ -------------------- links --------------------
' torque-init ' torque-init-done? --> ' torque-init-done? ' torque-forth -->
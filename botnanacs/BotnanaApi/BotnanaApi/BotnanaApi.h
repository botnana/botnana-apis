#pragma once

extern "C"
{

	// 定義 callback function 的形態
	typedef void(*HandleMessage)(const char *str);

	// Library version
	__declspec(dllexport) const char * library_version_dll(const char * ip);

	// New Botnana
	// @ip : Botnana 的 IP 位置
	__declspec(dllexport) struct Botnana * botnana_new_dll(const char * ip);

	// Connect with Botnana Control
	// @botnana: Botnana Control descriptor
	__declspec(dllexport) void botnana_connect_dll(struct Botnana *botnana);

	// Disconnect with Botnana Control
	// @botnana: Botnana Control descriptor
	__declspec(dllexport) void botnana_disconnect_dll(struct Botnana *botnana);


	// Set WS on_open callback function 
	// @botnana: Botnana Control descriptor
	// @cb: callback function
	__declspec(dllexport) void botnana_set_on_open_cb_dll(struct Botnana *botnana, HandleMessage cb);

	// Set WS on_error callback function 
	// @botnana: Botnana Control descriptor
	// @cb: callback function
	__declspec(dllexport) void botnana_set_on_error_cb_dll(struct Botnana *botnana, HandleMessage cb);

	// 送出 real time command (直接送到 motion server)
	// @botnana: Botnana Control descriptor
	// @script : 命令內容
	__declspec(dllexport) void script_evaluate_dll(struct Botnana *botnana, const char *script);

	// 將 real time command 送到緩衝區
	// @botnana: Botnana Control descriptor
	// @script : 命令內容
	__declspec(dllexport) void send_script(struct Botnana *botnana, const char *script);

	// Flush scripts buffer
	//
	// @botnana: Botnana Control descriptor
	__declspec(dllexport) void flush_scripts_buffer_dll(struct Botnana * botnana);

	// Set scripts pop count
	//
	// @botnana: Botnana Control descriptor
	// @count: command count
	__declspec(dllexport) void set_scripts_pop_count_dll(struct Botnana * botnana, uint32_t count);

	// Set poll interval
	//
	// @botnana: Botnana Control descriptor
	// @interval: poll interval [ms]
	__declspec(dllexport) void set_poll_interval_ms_dll(struct Botnana * botnana, uint64_t interval);

	// 設定接收到預設資訊時的 callback function
	// @botnana: Botnana Control descriptor
	// @event: 資訊名稱
	// @count: 最多可以呼叫此 callback function 的次數，設定 0 表示永遠都會呼叫此 callback function
	// @cb: 當收到 event 時要執行的 callback function
	__declspec(dllexport) void botnana_set_tag_cb_dll(struct Botnana *botnana,
		const char *tag,
		int count,
		HandleMessage cb);

	// Set on_message callback function
	// @botnana: Botnana Control descriptor
	// @cb: 當送出命令時或將送出的命令的回傳給此callback function
	__declspec(dllexport) void botnana_set_on_send_cb_dll(struct Botnana *botnana,
		HandleMessage cb);

	// Set on_message callback function
	// @botnana: Botnana Control descriptor
	// @cb: 當送出命令時或將送出的命令的回傳給此callback function
	__declspec(dllexport) void botnana_set_on_message_cb_dll(struct Botnana *botnana,
		HandleMessage cb);

	// 建立一個新的 real time program
	// name: program 名稱
	__declspec(dllexport) struct Program *program_new_dll(const char *name);

	// 將  real time command (cmd) 放到 program 中
	// cmd: real time script command
	__declspec(dllexport) void program_line_dll(struct Program *pm,
		const char *cmd);

	// 清除program 內容
	// cmd: real time script command
	__declspec(dllexport) void program_clear_dll(struct Program *pm);

	// 將定義好的program 傳送到 Botnana
	__declspec(dllexport) void program_deploy_dll(struct Botnana *botnana,
		struct Program *pm);

	// 執行傳送到 Botnana 上的 real time program
	__declspec(dllexport) void program_run_dll(struct Botnana *botnana, struct Program *pm);

	// 中止目前執行中的 real time program
	__declspec(dllexport) void botnana_abort_program_dll(struct Botnana *botnana);

	// JSON-API: version.get
	__declspec(dllexport) void botnana_version_get(
		struct Botnana * botnana);

	// JSON-API: config.slave.get
	__declspec(dllexport) void configure_slave_get(
		struct Botnana * botnana,
		uint32_t alias,
		uint32_t position,
		uint32_t channel);

	// JSON-API: config.slave.set : homing_method
	__declspec(dllexport) void configure_slave_set_homing_method(
		struct Botnana * botnana,
		uint32_t alias,
		uint32_t position,
		uint32_t channel,
		int32_t value);

	// JSON-API: config.slave.set : homing_speed_1
	__declspec(dllexport) void configure_slave_set_homing_speed_1(
		struct Botnana * botnana,
		uint32_t alias,
		uint32_t position,
		uint32_t channel,
		int32_t value);

	// JSON-API: config.slave.set : homing_speed_2
	__declspec(dllexport) void configure_slave_set_homing_speed_2(
		struct Botnana * botnana,
		uint32_t alias,
		uint32_t position,
		uint32_t channel,
		int32_t value);

	// JSON-API: config.slave.set : homing_acceleration
	__declspec(dllexport) void configure_slave_set_homing_acceleration(
		struct Botnana * botnana,
		uint32_t alias,
		uint32_t position,
		uint32_t channel,
		int32_t value);

	// JSON-API: config.slave.set : profile_velocity
	__declspec(dllexport) void configure_slave_set_profile_velocity(
		struct Botnana * botnana,
		uint32_t alias,
		uint32_t position,
		uint32_t channel,
		int32_t value);

	// JSON-API: config.slave.set : profile_acceleration
	__declspec(dllexport) void configure_slave_set_profile_acceleration(
		struct Botnana * botnana,
		uint32_t alias,
		uint32_t position,
		uint32_t channel,
		int32_t value);

	// JSON-API: config.slave.set : profile_deceleration
	__declspec(dllexport) void configure_slave_set_profile_deceleration(
		struct Botnana * botnana,
		uint32_t alias,
		uint32_t position,
		uint32_t channel,
		int32_t value);

	// JSON-API: config.slave.set : pdo_digital_inputs
	__declspec(dllexport) void configure_slave_set_pdo_digital_inputs(
		struct Botnana * botnana,
		uint32_t alias,
		uint32_t position,
		uint32_t channel,
		int32_t value);

	// JSON-API: config.slave.set : pdo_demand_position
	__declspec(dllexport) void configure_slave_set_pdo_demand_position(
		struct Botnana * botnana,
		uint32_t alias,
		uint32_t position,
		uint32_t channel,
		int32_t value);

	// JSON-API: config.slave.set : pdo_demand_velocity
	__declspec(dllexport) void configure_slave_set_pdo_demand_velocity(
		struct Botnana * botnana,
		uint32_t alias,
		uint32_t position,
		uint32_t channel,
		int32_t value);

	// JSON-API: config.slave.set : pdo_demand_torque
	__declspec(dllexport) void configure_slave_set_pdo_demand_torque(
		struct Botnana * botnana,
		uint32_t alias,
		uint32_t position,
		uint32_t channel,
		int32_t value);

	// JSON-API: config.slave.set : pdo_real_velocity
	__declspec(dllexport) void configure_slave_set_pdo_real_velocity(
		struct Botnana * botnana,
		uint32_t alias,
		uint32_t position,
		uint32_t channel,
		int32_t value);

	// JSON-API: config.slave.set : pdo_real_torque
	__declspec(dllexport) void configure_slave_set_pdo_real_torque(
		struct Botnana * botnana,
		uint32_t alias,
		uint32_t position,
		uint32_t channel,
		int32_t value);

	// JSON-API: config.motion.get
	__declspec(dllexport) void configure_motion_get(struct Botnana * botnana);

	// JSON-API: period_us of config.motion.set
	__declspec(dllexport) void configure_motion_set_period_us(struct Botnana * botnana, uint32_t value);

	// JSON-API: group_capacity of config.motion.set
	__declspec(dllexport)  void configure_motion_set_group_capacity(struct Botnana * botnana, uint32_t value);

	// JSON-API: axis_capacity of config.motion.set
	__declspec(dllexport) void configure_motion_set_axis_capacity(struct Botnana * botnana, uint32_t value);

	// JSON-API: config.group.get
	__declspec(dllexport) void configure_group_get(struct Botnana * botnana, uint32_t position);

	// JSON-API: name of config.group.set
	__declspec(dllexport) int32_t configure_group_set_name(
		struct Botnana * botnana,
		uint32_t position,
		const char *  value);

	// JSON-API: type as 1D of config.group.set
	__declspec(dllexport) void configure_group_set_type_as_1d(
		struct Botnana * botnana,
		uint32_t position,
		uint32_t a1);

	// JSON-API: type as 2D of config.group.set
	__declspec(dllexport) void configure_group_set_type_as_2d(
		struct Botnana * botnana,
		uint32_t position,
		uint32_t a1,
		uint32_t a2);

	// JSON-API: type as 3D of config.group.set
	__declspec(dllexport) 	void configure_group_set_type_as_3d(
		struct Botnana * botnana,
		uint32_t position,
		uint32_t a1,
		uint32_t a2,
		uint32_t a3);

	// JSON-API: type as SINE of config.group.set
	__declspec(dllexport) void configure_group_set_type_as_sine(
		struct Botnana * botnana,
		uint32_t position,
		uint32_t a1);

	// JSON-API: vmax of config.group.set
	__declspec(dllexport) void configure_group_set_vmax(
		struct Botnana * botnana,
		uint32_t position,
		double  value);

	// JSON-API: amax of config.group.set
	__declspec(dllexport) void configure_group_set_amax(
		struct Botnana * botnana,
		uint32_t position,
		double  value);

	// JSON-API: jmax of config.group.set
	__declspec(dllexport) void configure_group_set_jmax(
		struct Botnana * botnana,
		uint32_t position,
		double  value);

	// JSON-API: config.axis.get
	__declspec(dllexport) void configure_axis_get(struct Botnana * botnana, uint32_t position);

	// JSON-API: name of config.axis.set
	__declspec(dllexport) int32_t configure_axis_set_name(struct Botnana * botnana, uint32_t position, const char * name);

	// JSON-API: home_offset of config.axis.set
	__declspec(dllexport) void configure_axis_set_home_offset(struct Botnana * botnana, uint32_t position, double value);

	// JSON-API: encoder_length_unit as Meter of config.axis.set
	__declspec(dllexport) void configure_axis_set_encoder_length_unit_as_meter(struct Botnana * botnana, uint32_t position);

	// JSON-API: encoder_length_unit as Revolution of config.axis.set
	__declspec(dllexport) void configure_axis_set_encoder_length_unit_as_revolution(struct Botnana * botnana, uint32_t position);

	// JSON-API: encoder_length_unit as pulse of config.axis.set
	__declspec(dllexport) void configure_axis_set_encoder_length_unit_as_pulse(struct Botnana * botnana, uint32_t position);

	// JSON-API: encoder_ppu of config.axis.set
	__declspec(dllexport) void configure_axis_set_encoder_ppu(struct Botnana * botnana, uint32_t position, double value);

	// JSON-API: ext_encoder_ppu of config.axis.set
	__declspec(dllexport) void configure_axis_set_ext_encoder_ppu(struct Botnana * botnana, uint32_t position, double value);

	// JSON-API: encoder_direction of config.axis.set
	__declspec(dllexport) void configure_axis_set_encoder_direction(struct Botnana * botnana, uint32_t position, int32_t value);

	// JSON-API: ext_encoder_direction of config.axis.set
	__declspec(dllexport) void configure_axis_set_ext_encoder_direction(struct Botnana * botnana, uint32_t position, int32_t value);

	// JSON-API: closed_loop_filter of config.axis.set
	__declspec(dllexport) void configure_axis_set_closed_loop_filter(struct Botnana * botnana, uint32_t position, double value);

	// JSON-API: max_position_deviation of config.axis.set
	__declspec(dllexport) void configure_axis_set_max_position_deviation(struct Botnana * botnana, uint32_t position, double value);

	// JSON-API: vmax of config.axis.set
	__declspec(dllexport) void configure_axis_set_vmax(struct Botnana * botnana, uint32_t position, double value);

	// JSON-API: amax of config.axis.set
	__declspec(dllexport) void configure_axis_set_amax(struct Botnana * botnana, uint32_t position, double value);

	// JSON-API: drive_alias of config.axis.set
	__declspec(dllexport) void configure_axis_set_drive_alias(struct Botnana * botnana, uint32_t position, int32_t value);

	// JSON-API: drive_slave_position of config.axis.set
	__declspec(dllexport) void configure_axis_set_drive_slave_position(struct Botnana * botnana, uint32_t position, int32_t value);

	// JSON-API: drive_channel of config.axis.set
	__declspec(dllexport) void configure_axis_set_drive_channel(struct Botnana * botnana, uint32_t position, int32_t value);

	// JSON-API: ext_encoder_alias of config.axis.set
	__declspec(dllexport) void configure_axis_set_ext_encoder_alias(struct Botnana * botnana, uint32_t position, int32_t value);

	// JSON-API: ext_encoder_slave_position of config.axis.set
	__declspec(dllexport) void configure_axis_set_ext_encoder_slave_position(struct Botnana * botnana, uint32_t position, int32_t value);

	// JSON-API: ext_encoder_channel of config.axis.set
	__declspec(dllexport) void configure_axis_set_ext_encoder_channel(struct Botnana * botnana, uint32_t position, int32_t value);

	// JSON-API: configuration.save
	__declspec(dllexport) void configure_save(struct Botnana *botnana);

	// JSON-API: reboot
	__declspec(dllexport) void botnana_reboot(struct Botnana *botnana);

	// JSON-API: poweroff
	__declspec(dllexport) void botnana_poweroff(struct Botnana *botnana);

}
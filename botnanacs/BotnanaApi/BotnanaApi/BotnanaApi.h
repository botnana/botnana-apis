#pragma once
#include <stdint.h>
extern "C"
{

	// 定義 callback function 的形態
	// @ pointer: 回傳使用者設定的指標
	// @ str: 回傳的訊息
	typedef void(*HandleMessage)(void * pointer, const char *str);

	// 定義 tag name callback function 的形態
	// tag 會有以下 3 種型態
	// tag_name     : position = 0, channel = 0
	// tag_name.x   : position = x, channel = 0
	// tag_name.y.x : position = x, channel = y
	// @ pointer    : 回傳使用者設定的指標
	// @ position   : 如果有此欄位 position > 0 
	// @ channel    : 如果有此欄位 channel > 0
	// @ str: 回傳的訊息
	typedef void(*TagNameHandleMessage)(void * pointer, uint32_t position, uint32_t channel, const char *str);

	// Library version
	__declspec(dllexport) const char * library_version_dll(void);

	// New Botnana
	// @ip : Botnana 的 IP 位置
	__declspec(dllexport) struct Botnana * botnana_new_dll(const char * ip);

	// Connect with Botnana Control
	// @botnana: Botnana Control descriptor
	__declspec(dllexport) void botnana_connect_dll(struct Botnana *botnana);

	// Disconnect with Botnana Control
	// @botnana: Botnana Control descriptor
	__declspec(dllexport) void botnana_disconnect_dll(struct Botnana *botnana);

	// Set IP
	// @botnana: Botnana Control descriptor
	// @ip: IP of Motion Server 
	__declspec(dllexport) const char * botnana_set_ip_dll(struct Botnana *botnana, const char * ip);

	// Set Port
	// @botnana: Botnana Control descriptor
	// @port: Port of Motion Server 
	__declspec(dllexport) uint16_t botnana_set_port_dll(struct Botnana *botnana, uint16_t port);

	// URL of motion server
	// @botnana: Botnana Control descriptor
	__declspec(dllexport) const char * botnana_url_dll(struct Botnana *botnana);

	// Set WS on_open callback function 
	// @botnana: Botnana Control descriptor
	// @pointer: callback function 執行時要回傳的指標
	// @cb: callback function
	__declspec(dllexport) void botnana_set_on_open_cb_dll(struct Botnana *botnana, void * pointer, HandleMessage cb);

	// Set WS on_error callback function 
	// @botnana: Botnana Control descriptor
	// @pointer: callback function 執行時要回傳的指標
	// @cb: callback function
	__declspec(dllexport) void botnana_set_on_error_cb_dll(struct Botnana *botnana, void * pointer, HandleMessage cb);

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

	// Send Message (Raw message)
	//
	// @botnana: Botnana Control descriptor
	// @msg: message
	__declspec(dllexport) void botnana_send_message_dll(struct Botnana * botnana, const char * msg);

	// 設定接收到預設資訊時的 callback function
	// @botnana: Botnana Control descriptor
	// @event: 資訊名稱
	// @count: 最多可以呼叫此 callback function 的次數，設定 0 表示永遠都會呼叫此 callback function
	// @pointer: callback function 執行時要回傳的指標
	// @cb: 當收到 event 時要執行的 callback function
	__declspec(dllexport) void botnana_set_tag_cb_dll(struct Botnana *botnana,
		const char *tag,
		int count,
		void * pointer,
		HandleMessage cb);

	// 設定接收到預設資訊時的 callback function
	// @botnana: Botnana Control descriptor
	// @name: name of tag
	// @count: 最多可以呼叫此 callback function 的次數，設定 0 表示永遠都會呼叫此 callback function
	// @pointer: callback function 執行時要回傳的指標
	// @cb: 當收到 event 時要執行的 callback function
	__declspec(dllexport) void botnana_set_tagname_cb_dll(struct Botnana *botnana,
		const char *name,
		int count,
		void * pointer,
		TagNameHandleMessage cb);

	// Set on_message callback function
	// @botnana: Botnana Control descriptor
	// @pointer: callback function 執行時要回傳的指標
	// @cb: 當送出命令時或將送出的命令的回傳給此callback function
	__declspec(dllexport) void botnana_set_on_send_cb_dll(struct Botnana *botnana, void * pointer,
		HandleMessage cb);

	// Set on_message callback function
	// @botnana: Botnana Control descriptor
	// @pointer: callback function 執行時要回傳的指標
	// @cb: 當送出命令時或將送出的命令的回傳給此callback function
	__declspec(dllexport) void botnana_set_on_message_cb_dll(struct Botnana *botnana, void * pointer,
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

	// Configuration changes are made through the Botnana Control HMI.

	// JSON-API: config.motion.get
	__declspec(dllexport) void configure_motion_get(struct Botnana * botnana);

	// JSON-API: config.group.get
	__declspec(dllexport) void configure_group_get(struct Botnana * botnana, uint32_t position);

	// JSON-API: config.axis.get
	__declspec(dllexport) void configure_axis_get(struct Botnana * botnana, uint32_t position);

	// JSON-API: reboot
	__declspec(dllexport) void botnana_reboot(struct Botnana *botnana);

	// JSON-API: poweroff
	__declspec(dllexport) void botnana_poweroff(struct Botnana *botnana);

}
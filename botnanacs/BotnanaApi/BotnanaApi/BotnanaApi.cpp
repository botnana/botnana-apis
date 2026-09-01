// BotnanaApi.cpp : 定義 DLL 應用程式的匯出函式。
//

#include "stdafx.h"
#include <stdio.h>
#include "botnana.h"
#include "BotnanaApi.h"

extern "C" {

	// Library version
	__declspec(dllexport) const char * library_version_dll(void) {
		return library_version();
	}

	// New Botnana
	// @ip : Botnana 的 IP 位置
	__declspec(dllexport) struct Botnana * botnana_new_dll(const char * ip) {
		return botnana_new(ip);
	}

	// Connect with Botnana Control
	// @botnana: Botnana Control descriptor
	__declspec(dllexport) void botnana_connect_dll(struct Botnana *botnana) {
		botnana_connect(botnana);
	}

	// Disconnect with Botnana Control
	// @botnana: Botnana Control descriptor
	__declspec(dllexport) void botnana_disconnect_dll(struct Botnana *botnana) {
		botnana_disconnect(botnana);
	}

	// Set IP
	// @botnana: Botnana Control descriptor
	// @ip: IP of Motion Server 
	__declspec(dllexport) const char * botnana_set_ip_dll(struct Botnana *botnana, const char * ip) {
		return botnana_set_ip(botnana, ip);
	}

	// Set Port
	// @botnana: Botnana Control descriptor
	// @port: Port of Motion Server 
	__declspec(dllexport) uint16_t botnana_set_port_dll(struct Botnana *botnana, uint16_t port) {
		return botnana_set_port(botnana, port);
	}

	// URL of motion server
	// @botnana: Botnana Control descriptor
	__declspec(dllexport) const char * botnana_url_dll(struct Botnana *botnana) {
		return botnana_url(botnana);
	}

	// Set WS on_open callback function 
	// @botnana: Botnana Control descriptor
	// @cb: callback function
	__declspec(dllexport) void botnana_set_on_open_cb_dll(struct Botnana *botnana, void * pointer, HandleMessage cb) {
		botnana_set_on_open_cb(botnana, pointer, cb);
	}

	// Set WS on_error callback function 
	// @botnana: Botnana Control descriptor
	// @cb: callback function
	__declspec(dllexport) void botnana_set_on_error_cb_dll(struct Botnana *botnana, void * pointer, HandleMessage cb) {
		botnana_set_on_error_cb(botnana, pointer, cb);
	}

	// 送出 real time command
	// script : 命令內容
	__declspec(dllexport) void script_evaluate_dll(struct Botnana * botnana,
		const char * script) {
		script_evaluate(botnana, script);
	}

	// 將 real time command 送到緩衝區
	// @botnana: Botnana Control descriptor
	// @script : 命令內容
	__declspec(dllexport) void send_script(struct Botnana *botnana, const char *script) {
		send_script_to_buffer(botnana, script);
	}

	// Flush scripts buffer
	//
	// @botnana: Botnana Control descriptor
	__declspec(dllexport) void flush_scripts_buffer_dll(struct Botnana * botnana) {
		flush_scripts_buffer(botnana);
	}

	// Set scripts pop count
	//
	// @botnana: Botnana Control descriptor
	// @count: command count
	__declspec(dllexport) void set_scripts_pop_count_dll(struct Botnana * botnana, uint32_t count) {
		set_scripts_pop_count(botnana, count);
	}

	// Set poll interval
	//
	// @botnana: Botnana Control descriptor
	// @interval: poll interval [ms]
	__declspec(dllexport) void set_poll_interval_ms_dll(struct Botnana * botnana, uint64_t interval) {
		set_poll_interval_ms(botnana, interval);
	}

	// Send Message (Raw message)
	//
	// @botnana: Botnana Control descriptor
	// @msg: message
	__declspec(dllexport) void botnana_send_message_dll(struct Botnana * botnana, const char * msg) {
		botnana_send_message(botnana, msg);
	}

	// 設定接收到預設資訊時的 callback function
	// event: 資訊名稱 
	// count: 最多可以呼叫此 callback function 的次數，設定 0 表示永遠都會呼叫此 callback function
	// hm: 當收到 event 時要執行的 callback function
	__declspec(dllexport) void botnana_set_tag_cb_dll(struct Botnana * botnana,
		const char * tag,
		int count,
		void * pointer,
		HandleMessage hm) {
		botnana_set_tag_cb(botnana, tag, count, pointer, hm);
	}

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
		TagNameHandleMessage cb) {
		botnana_set_tagname_cb(botnana, name, count, pointer, cb);
	}

	// 設定debug 時要接收訊息的 callback function
	// hm: 當送出命令時或將送出的命令的回傳給此callback function
	__declspec(dllexport) void botnana_set_on_send_cb_dll(struct Botnana * botnana, void * pointer,
		HandleMessage hm) {
		botnana_set_on_send_cb(botnana, pointer, hm);
	}

	// 設定debug 時要接收訊息的 callback function
	// hm: 當送出命令時或將送出的命令的回傳給此callback function
	__declspec(dllexport) void botnana_set_on_message_cb_dll(struct Botnana * botnana, void * pointer,
		HandleMessage hm) {
		botnana_set_on_message_cb(botnana, pointer, hm);
	}

	// 建立一個新的 real time program
	// name: program 名稱
	__declspec(dllexport) struct Program * program_new_dll(const char * name) {
		return program_new(name);
	}

	// 將  real time command (cmd) 放到 program 中
	// cmd: real time script command  
	__declspec(dllexport) void program_line_dll(struct Program * pm,
		const char * cmd) {
		program_line(pm, cmd);
	}

	// 清除program 內容
	// cmd: real time script command  
	__declspec(dllexport) void program_clear_dll(struct Program * pm) {
		program_clear(pm);
	}


	// 將定義好的program 傳送到 Botnana
	__declspec(dllexport) void  program_deploy_dll(struct Botnana * botnana,
		struct Program * pm) {
		program_deploy(botnana, pm);
	}

	// 執行傳送到 Botnana 上的 real time program
	__declspec(dllexport) void program_run_dll(struct Botnana * botnana, struct Program * pm) {
		program_run(botnana, pm);
	}

	// 中止目前執行中的 real time program
	__declspec(dllexport) void  botnana_abort_program_dll(struct Botnana * botnana) {
		botnana_abort_program(botnana);
	}

	// JSON-API: version.get
	__declspec(dllexport) void botnana_version_get(
		struct Botnana * botnana) {
		version_get(botnana);
	}

	// JSON-API: config.slave.get
	__declspec(dllexport) void configure_slave_get(
		struct Botnana * botnana,
		uint32_t alias,
		uint32_t position,
		uint32_t channel) {
		config_slave_get(botnana, alias, position, channel);
	}

	// Configuration changes are made through the Botnana Control HMI.

	// JSON-API: config.motion.get
	__declspec(dllexport) void configure_motion_get(struct Botnana * botnana) {
		config_motion_get(botnana);
	}

	// JSON-API: config.group.get
	__declspec(dllexport) void configure_group_get(struct Botnana * botnana, uint32_t position) {
		config_group_get(botnana, position);
	}

	// JSON-API: config.axis.get
	__declspec(dllexport) void configure_axis_get(struct Botnana * botnana, uint32_t position) {
		config_axis_get(botnana, position);
	}

	// reboot
	__declspec(dllexport) void botnana_reboot(struct Botnana *botnana) {
		reboot(botnana);
	}

	// poweroff
	__declspec(dllexport) void botnana_poweroff(struct Botnana *botnana) {
		poweroff(botnana);
	}

}



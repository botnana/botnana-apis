// BotnanaApi.cpp : 定義 DLL 應用程式的匯出函式。
//

#include "stdafx.h"
#include <stdio.h>
#include "botnana.h"
#include "BotnanaApi.h"

// 定義 callback function 的形態
//typedef void(*HandleMessage)(const char* str);

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
	__declspec(dllexport) void botnana_connect_dll(struct Botnana *botnana){
		botnana_connect(botnana);
	}

	// Disconnect with Botnana Control
	// @botnana: Botnana Control descriptor
	__declspec(dllexport) void botnana_disconnect_dll(struct Botnana *botnana) {
		botnana_disconnect(botnana);
	}

	// Set WS on_open callback function 
	// @botnana: Botnana Control descriptor
	// @cb: callback function
	__declspec(dllexport) void botnana_set_on_open_cb_dll(struct Botnana *botnana, HandleMessage cb) {
		botnana_set_on_open_cb(botnana, cb);
	}

	// Set WS on_error callback function 
	// @botnana: Botnana Control descriptor
	// @cb: callback function
	__declspec(dllexport) void botnana_set_on_error_cb_dll(struct Botnana *botnana, HandleMessage cb) {
		botnana_set_on_error_cb(botnana, cb);
	}
	
	// 送出 real time command
	// script : 命令內容
	__declspec(dllexport) void script_evaluate_dll(struct Botnana * botnana,
		const char * script) {
		script_evaluate(botnana, script);
	}

	// 設定接收到預設資訊時的 callback function
	// event: 資訊名稱 
	// count: 最多可以呼叫此 callback function 的次數，設定 0 表示永遠都會呼叫此 callback function
	// hm: 當收到 event 時要執行的 callback function
	__declspec(dllexport) void botnana_set_tag_cb_dll(struct Botnana * botnana,
		const char * tag,
		int count,
		HandleMessage hm) {
		botnana_set_tag_cb(botnana, tag, count, hm);
	}

	// 設定debug 時要接收訊息的 callback function
	// hm: 當送出命令時或將送出的命令的回傳給此callback function
	__declspec(dllexport) void botnana_set_on_send_cb_dll(struct Botnana * botnana,
		HandleMessage hm) {
		botnana_set_on_send_cb(botnana, hm);
	}

	// 設定debug 時要接收訊息的 callback function
	// hm: 當送出命令時或將送出的命令的回傳給此callback function
	__declspec(dllexport) void botnana_set_on_message_cb_dll(struct Botnana * botnana,
		HandleMessage hm) {
		botnana_set_on_message_cb(botnana, hm);
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

}



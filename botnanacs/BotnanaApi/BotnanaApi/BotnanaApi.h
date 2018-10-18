#pragma once

extern "C" {

	// �w�q callback function ���κA
	typedef void(*HandleMessage)(const char* str);

	// �s�u�� Botnana
	// address : Botnana �� IP ��m
	// on_error_cb: ��s�u���~�ɷ|�I�s��call function, 
	__declspec(dllexport) struct Botnana * botnana_connect_dll(const char * address, HandleMessage on_error_cb);

	// �e�X real time command
	// script : �R�O���e
	__declspec(dllexport) void script_evaluate_dll(struct Botnana * botnana, const char * script);

	// �]�w������w�]��T�ɪ� callback function
	// event: ��T�W�� 
	// count: �̦h�i�H�I�s�� callback function �����ơA�]�w 0 ��ܥû����|�I�s�� callback function
	// hm: ���� event �ɭn���檺 callback function
	__declspec(dllexport) void botnana_set_tag_cb_dll(struct Botnana * botnana,
		const char * tag,
		int count,
		HandleMessage hm);

	// �]�wdebug �ɭn�����T���� callback function
	// hm: ��e�X�R�O�ɩαN�e�X���R�O���^�ǵ���callback function
	__declspec(dllexport) void botnana_set_on_send_cb_dll(struct Botnana * botnana,
		HandleMessage hm);

	// �]�wdebug �ɭn�����T���� callback function
	// hm: ��e�X�R�O�ɩαN�e�X���R�O���^�ǵ���callback function
	__declspec(dllexport) void botnana_set_on_message_cb_dll(struct Botnana * botnana,
		HandleMessage hm);

	// �إߤ@�ӷs�� real time program
	// name: program �W��
	__declspec(dllexport) struct Program * program_new_dll(const char * name);

	// �N  real time command (cmd) ��� program ��
	// cmd: real time script command  
	__declspec(dllexport) void program_line_dll(struct Program * pm,
		const char * cmd);

	// �M��program ���e
	// cmd: real time script command  
	__declspec(dllexport) void program_clear_dll(struct Program * pm);


	// �N�w�q�n��program �ǰe�� Botnana
	__declspec(dllexport) void  program_deploy_dll(struct Botnana * botnana,
		struct Program * pm);

	// ����ǰe�� Botnana �W�� real time program
	__declspec(dllexport) void program_run_dll(struct Botnana * botnana, struct Program * pm);

	// ����ثe���椤�� real time program
	__declspec(dllexport) void  botnana_abort_program_dll(struct Botnana * botnana);

}
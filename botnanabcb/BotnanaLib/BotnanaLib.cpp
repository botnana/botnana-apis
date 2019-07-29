#include "BotnanaLib.h"

class BotnanaLib;

BotnanaLib::BotnanaLib()
{
	innerBotnana = botnana_new_dll("192.168.7.2");
}

BotnanaLib::BotnanaLib(const char *ip)
{
	innerBotnana = botnana_new_dll(ip);
}

BotnanaLib::~BotnanaLib()
{
	delete innerBotnana;
}

void BotnanaLib::SetWsIP(const char *ip)
{
	botnana_set_ip_dll(innerBotnana, ip);
}

void BotnanaLib::SetWsPort(unsigned short port)
{
	botnana_set_port_dll(innerBotnana, port);
}

void BotnanaLib::Connect()
{
	botnana_connect_dll(innerBotnana);
}

void BotnanaLib::Disconnect()
{
	botnana_disconnect_dll(innerBotnana);
}

void BotnanaLib::SetOnOpenCB(void * pointer, HandleMessage cb)
{
	botnana_set_on_open_cb_dll(innerBotnana, pointer, cb);
}

void BotnanaLib::SetOnErrorCB(void * pointer, HandleMessage cb)
{
	botnana_set_on_error_cb_dll(innerBotnana, pointer, cb);
}

void BotnanaLib::SetOnMessageCB(void * pointer, HandleMessage cb)
{
	botnana_set_on_message_cb_dll(innerBotnana, pointer, cb);
}

void BotnanaLib::SetOnSendCB(void * pointer, HandleMessage cb)
{
	botnana_set_on_send_cb_dll(innerBotnana, pointer, cb);
}

void BotnanaLib::SetTagCB(const char *tag, int count, void * pointer, HandleMessage cb)
{
	botnana_set_tag_cb_dll(innerBotnana, tag, count, pointer, cb);
}

void BotnanaLib::SetTagNameCB(const char *tag, int count, void * pointer, TagNameHandleMessage cb)
{
	botnana_set_tagname_cb_dll(innerBotnana, tag, count, pointer, cb);
}

void BotnanaLib::EvaluateScript(const char *str)
{
	script_evaluate_dll(innerBotnana, str);
}



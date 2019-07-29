#ifndef BotnanaLib_h
#define BotnanaLib_h

#include "BotnanaApi.h"

struct Botnana;

//class BotnanaLib;

class BotnanaLib
{
   public:
	  BotnanaLib(const char *ip);

	  BotnanaLib();

	  ~BotnanaLib();

	  // 透過 WebSocket 連線到 Botnana Control
	  void Connect();

	  // Disconnect
	  void Disconnect();

	  // 設定 Botnana Control 的連線 IP
	  void SetWsIP(const char *ip);

	  // 設定 Botnana Control 的連線 Port
	  void SetWsPort(unsigned short port);

	  // 設定 WebSocket 連線時的 Callback function
	  // @ pointer  : cb 呼叫時要回傳的指標
	  // @ cb       : Callback function
	  void SetOnOpenCB(void * pointer, HandleMessage cb);

	  // 設定 WebSocket 斷線時的 Callback function
	  // @ pointer  : cb 呼叫時要回傳的指標
	  // @ cb       : Callback function
	  void SetOnErrorCB(void * pointer, HandleMessage cb);

	  // 設定 WebSocket 收到訊息時的 Callback function
	  // @ pointer  : cb 呼叫時要回傳的指標
	  // @ cb       : Callback function
	  void SetOnMessageCB(void * pointer, HandleMessage cb);

	  // 設定 WebSocket 送出到指令時的 Callback function
	  // @ pointer  : cb 呼叫時要回傳的指標
	  // @ cb       : Callback function
	  void SetOnSendCB(void * pointer, HandleMessage cb);

	  // 設定 WebSocket 收到指定標籤名稱的 Callback function
	  // @ pointer  : cb 呼叫時要回傳的指標
	  // @ cb       : Callback function
	  void SetTagCB(const char *tag, int count, void * pointer, HandleMessage cb);

	  // 設定 WebSocket 收到指定標籤名稱的 Callback function
	  // @ pointer  : cb 呼叫時要回傳的指標
	  // @ cb       : Callback function
	  void SetTagNameCB(const char *tag, int count, void * pointer, TagNameHandleMessage cb);

	  // 送出 rtForth 指令 (立即執行)
	  // @ str: rtForth 指令
	  void EvaluateScript(const char *str);

   private:
	  Botnana * innerBotnana;
};

#endif
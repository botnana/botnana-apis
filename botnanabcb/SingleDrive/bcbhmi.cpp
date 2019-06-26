//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "bcbhmi.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"

// on error callback
void on_error(void * ptr, const char * str){
    TForm1 * form = (TForm1 *) ptr;
   form->wsOpened = false;
	ShowMessage(str);
}

// on open callback
void on_open(void * ptr, const char * str){
   TForm1 * form = (TForm1 *) ptr;
   form->wsOpened = true;
}

// websocket 回傳資料時的 callback function
void handle_message(void * ptr, const char * str){
	TForm1 * form = (TForm1 *) ptr;
	form->message = AnsiString(str);
}

// 拿到 real_posiiton 資料時的 callback function
void real_position_cb(void * pointer, uint32_t position, uint32_t channel, const char * str){
	if (position == 1 && channel == 1) {
		TForm1 * form = (TForm1 *) pointer;
		form->realPosition = AnsiString(str);
	}
}

// 拿到 target_posiiton 資料時的 callback function
void target_position_cb(void * pointer, uint32_t position, uint32_t channel, const char * str){
	if (position == 1 && channel == 1) {
		TForm1 * form = (TForm1 *) pointer;
		form->targetPosition = AnsiString(str);
	}
}

// 拿到 status_word.1 資料時的 callback function
void status_word_cb(void * pointer, uint32_t position, uint32_t channel, const char *str){
    TForm1 * form = (TForm1 *) pointer;
	if (position == 1 && channel == 1) {
		int status = StrToInt(AnsiString(str));
		// 依據 status word 的定義取得 servo_on, fault, target reached 資訊
		// 可以驅動器手冊 0x6041 的定義
		form->servoOn = (status & 0x4) != 0;
		form->hasFault = (status & 0x8) != 0;
		form->targetReached = (status & 0x400) == 0x400;
	}
}

// 拿到 operation_mode  資料時的 callback function
void operation_mode_cb(void * pointer, uint32_t position, uint32_t channel, const char * str){
	TForm1 * form = (TForm1 *) pointer;
	if (position == 1 && channel == 1) {
		int mode = StrToInt(AnsiString(str));
		if (mode == 1){
			form->operationMode = AnsiString("PP");
		} else if (mode == 6){
			form->operationMode = AnsiString("HM");
		} else {
			form->operationMode = AnsiString("Other");
		}
	}
}


TForm1 *Form1;
//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner)
	: TForm(Owner)
{

}
//---------------------------------------------------------------------------
void __fastcall TForm1::FormCreate(TObject *Sender)
{
	// 設定連線 IP 與 Port
	botnana.SetWsIP("192.168.7.2");
	botnana.SetWsPort(3012);

	// 設定 WebSocket OnOpen, OnError 與 OnMessage 的 Callback
	botnana.SetOnErrorCB((void *)this, on_error);
	botnana.SetOnOpenCB((void *)this, on_open);
	botnana.SetOnMessageCB((void *)this, handle_message);

	// 設定接收到特定資料時的 callback function
	botnana.SetTagCB("real_position", 0, (void *)this, real_position_cb);
	botnana.SetTagCB("target_position", 0, (void *)this,target_position_cb);
	botnana.SetTagCB("status_word", 0, (void *)this,status_word_cb);
	botnana.SetTagCB("operation_mode", 0, (void *)this,operation_mode_cb);

	// 連線
	botnana.Connect();
}
//---------------------------------------------------------------------------

bool hasSlaveUpdated = false;
void __fastcall TForm1::Timer1Timer(TObject *Sender)
{
	EditRealPosition->Text = realPosition;
	EditTargetPosition->Text = targetPosition;
	EditOPMode->Text = operationMode;
	MemoMessage->Text = message;
	RadioServoOn->Checked = servoOn;
	RadioServoOff->Checked = ! servoOn;
	RadioFault->Checked = hasFault;
	RadioTargetReached->Checked = targetReached;
	if (wsOpened)
	{
		if (hasSlaveUpdated){
			botnana.EvaluateScript("1 .slave-diff");
		} else {
			botnana.EvaluateScript("1 .slave");
			hasSlaveUpdated = true;
		}

	}
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button1Click(TObject *Sender)
{
	// 清除驅動器異警
	botnana.EvaluateScript("1 1 reset-fault");
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button4Click(TObject *Sender)
{
	// drive off
	botnana.EvaluateScript("1 1 drive-off");
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button3Click(TObject *Sender)
{
	// new set-point of PP mode, start-homing of HM mode
	botnana.EvaluateScript("1 1 go");
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button2Click(TObject *Sender)
{
	// drive on
	botnana.EvaluateScript("1 1 drive-on");
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button5Click(TObject *Sender)
{
	// set profile velocity and target position
	botnana.EvaluateScript("10000 1 1 profile-v! -20000 1 1 target-p!");
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button7Click(TObject *Sender)
{
	// set profile velocity and target position
	botnana.EvaluateScript("5000 1  1 profile-v! 20000 1 1 target-p!");
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button6Click(TObject *Sender)
{
	// Set operation mode to HM
	botnana.EvaluateScript("hm 1 1 op-mode!");
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button8Click(TObject *Sender)
{
	// Set operation mode to PP
	botnana.EvaluateScript("pp 1 1 op-mode!");
}
//---------------------------------------------------------------------------



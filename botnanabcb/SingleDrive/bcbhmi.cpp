//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "bcbhmi.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"

#include "BotnanaApi.h"

static struct Botnana * btn;

// on error callback
void on_error(const char * str){
	ShowMessage(str);
}

bool wsOnOpened = false;
// on open callback
void on_open(const char * str){
   wsOnOpened = true;
}

// websocket 回傳資料時的 callback function
static AnsiString message;
void handle_message(const char * str){
	message = AnsiString(str);
}

// 拿到 real_posiiton.1 資料時的 callback function
static AnsiString real_position;
void real_position_cb(const char * str){
	real_position = AnsiString(str);
}

// 拿到 target_posiiton.1 資料時的 callback function
static AnsiString target_position;
void target_position_cb(const char * str){
	target_position = AnsiString(str);
}

// 拿到 status_word.1 資料時的 callback function
static BOOL servo_on = false;
static BOOL has_fault = false;
static BOOL target_reached = false;
void status_word_cb(const char *str){
	int status = StrToInt(AnsiString(str));
	// 依據 status word 的定義取得 servo_on, fault, target reached 資訊
	// 可以驅動器手冊 0x6041 的定義
	servo_on = (status & 0x4) != 0;
	has_fault = (status & 0x8) != 0;
	target_reached = (status & 0x400) == 0x400;
}

// 拿到 operation_mode.1 資料時的 callback function
static AnsiString operation_mode;
void operation_mode_cb(const char * str){
	int mode = StrToInt(AnsiString(str));
	if (mode == 1){
		operation_mode = AnsiString("PP");
	} else if (mode == 6){
		operation_mode = AnsiString("HM");
	} else {
	  operation_mode = AnsiString("Other");
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
	// 連線到 Botnana A2, 設定接收 WebSocket 資料時的callback function
	btn = botnana_new_dll("192.168.7.2");
	botnana_set_on_error_cb_dll(btn, on_error);
	botnana_set_on_open_cb_dll(btn, on_open);

	// 設定接收到特定資料時的 callback function
	botnana_set_tag_cb_dll(btn, "real_position.1.1", 0, real_position_cb);
	botnana_set_tag_cb_dll(btn, "target_position.1.1", 0, target_position_cb);
	botnana_set_tag_cb_dll(btn, "status_word.1.1", 0, status_word_cb);
	botnana_set_tag_cb_dll(btn, "operation_mode.1.1", 0, operation_mode_cb);
	botnana_set_on_message_cb_dll(btn,handle_message);
	botnana_connect_dll(btn);
}
//---------------------------------------------------------------------------

bool hasSlaveUpdated = false;
void __fastcall TForm1::Timer1Timer(TObject *Sender)
{
	EditRealPosition->Text = real_position;
	EditTargetPosition->Text = target_position;
	EditOPMode->Text = operation_mode;
	MemoMessage->Text = message;
	RadioServoOn->Checked = servo_on;
	RadioServoOff->Checked = ! servo_on;
	RadioFault->Checked = has_fault;
	RadioTargetReached->Checked = target_reached;
	if (wsOnOpened)
	{
		if (hasSlaveUpdated){
			script_evaluate_dll(btn, "1 .slave-diff");
		} else {
			script_evaluate_dll(btn, "1 .slave");
            hasSlaveUpdated = true;
		}

    }
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button1Click(TObject *Sender)
{
	// 清除驅動器異警
	script_evaluate_dll(btn, "1 1 reset-fault");
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button4Click(TObject *Sender)
{
	// drive off
	script_evaluate_dll(btn, "1 1 drive-off");
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button3Click(TObject *Sender)
{
	// new set-point of PP mode, start-homing of HM mode
	script_evaluate_dll(btn, "1 1 go");
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button2Click(TObject *Sender)
{
	// drive on
	script_evaluate_dll(btn, "1 1 drive-on");
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button5Click(TObject *Sender)
{
	// set profile velocity and target position
	script_evaluate_dll(btn, "10000 1 1 profile-v! -20000 1 1 target-p!");
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button7Click(TObject *Sender)
{
	// set profile velocity and target position
	script_evaluate_dll(btn, "5000 1  1 profile-v! 20000 1 1 target-p!");
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button6Click(TObject *Sender)
{
	// Set operation mode to HM
	script_evaluate_dll(btn, "hm 1 1 op-mode!");
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button8Click(TObject *Sender)
{
	// Set operation mode to PP
	script_evaluate_dll(btn, "pp 1 1 op-mode!");
}
//---------------------------------------------------------------------------



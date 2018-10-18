//---------------------------------------------------------------------------

#ifndef bcbhmiH
#define bcbhmiH
//---------------------------------------------------------------------------
#include <System.Classes.hpp>
#include <Vcl.Controls.hpp>
#include <Vcl.StdCtrls.hpp>
#include <Vcl.Forms.hpp>
#include <Vcl.ExtCtrls.hpp>
//---------------------------------------------------------------------------
class TForm1 : public TForm
{
__published:	// IDE-managed Components
	TEdit *EditRealPosition;
	TTimer *Timer1;
	TLabel *Label1;
	TMemo *MemoMessage;
	TRadioButton *RadioServoOn;
	TRadioButton *RadioServoOff;
	TRadioButton *RadioFault;
	TPanel *Panel1;
	TPanel *Panel2;
	TButton *Button1;
	TButton *Button2;
	TButton *Button3;
	TButton *Button4;
	TLabel *Label2;
	TEdit *EditTargetPosition;
	TButton *Button5;
	TButton *Button7;
	TPanel *Panel3;
	TRadioButton *RadioTargetReached;
	TButton *Button6;
	TButton *Button8;
	TEdit *EditOPMode;
	TLabel *Label3;
	void __fastcall FormCreate(TObject *Sender);
	void __fastcall Timer1Timer(TObject *Sender);
	void __fastcall Button1Click(TObject *Sender);
	void __fastcall Button4Click(TObject *Sender);
	void __fastcall Button3Click(TObject *Sender);
	void __fastcall Button2Click(TObject *Sender);
	void __fastcall Button5Click(TObject *Sender);
	void __fastcall Button7Click(TObject *Sender);
	void __fastcall Button6Click(TObject *Sender);
	void __fastcall Button8Click(TObject *Sender);
private:	// User declarations

public:		// User declarations
	__fastcall TForm1(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TForm1 *Form1;
//---------------------------------------------------------------------------
#endif

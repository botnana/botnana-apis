object Form1: TForm1
  Left = 0
  Top = 0
  Caption = 'Single Drive'
  ClientHeight = 531
  ClientWidth = 656
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -19
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 23
  object Label1: TLabel
    Left = 53
    Top = 278
    Width = 105
    Height = 23
    Caption = 'Real Position'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -19
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label2: TLabel
    Left = 53
    Top = 221
    Width = 124
    Height = 23
    Caption = 'Target Position'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -19
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
  end
  object Label3: TLabel
    Left = 53
    Top = 184
    Width = 134
    Height = 23
    Caption = 'Operation Mode'
  end
  object EditRealPosition: TEdit
    Left = 206
    Top = 275
    Width = 121
    Height = 31
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -19
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 0
  end
  object MemoMessage: TMemo
    Left = 53
    Top = 336
    Width = 553
    Height = 137
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Tahoma'
    Font.Style = []
    Lines.Strings = (
      'MemoMessage')
    ParentFont = False
    TabOrder = 1
  end
  object Panel1: TPanel
    Left = 48
    Top = 48
    Width = 238
    Height = 41
    TabOrder = 2
    object RadioServoOn: TRadioButton
      Left = 16
      Top = 16
      Width = 113
      Height = 17
      Caption = 'Servo On'
      DoubleBuffered = False
      ParentDoubleBuffered = False
      TabOrder = 0
    end
    object RadioServoOff: TRadioButton
      Left = 125
      Top = 16
      Width = 113
      Height = 17
      Caption = 'Servo Off'
      DoubleBuffered = False
      ParentDoubleBuffered = False
      TabOrder = 1
    end
  end
  object Panel2: TPanel
    Left = 288
    Top = 48
    Width = 73
    Height = 41
    TabOrder = 3
    object RadioFault: TRadioButton
      Left = 4
      Top = 16
      Width = 73
      Height = 17
      Caption = 'Fault'
      TabOrder = 0
    end
  end
  object Button1: TButton
    Left = 359
    Top = 112
    Width = 163
    Height = 33
    Caption = 'Reset Fault'
    TabOrder = 4
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 48
    Top = 112
    Width = 139
    Height = 33
    Caption = 'Servo On'
    TabOrder = 5
    OnClick = Button2Click
  end
  object Button3: TButton
    Left = 392
    Top = 296
    Width = 75
    Height = 34
    Caption = 'Go'
    TabOrder = 6
    OnClick = Button3Click
  end
  object Button4: TButton
    Left = 206
    Top = 112
    Width = 139
    Height = 33
    Caption = 'Servo Off'
    TabOrder = 7
    OnClick = Button4Click
  end
  object EditTargetPosition: TEdit
    Left = 206
    Top = 218
    Width = 121
    Height = 31
    TabOrder = 8
  end
  object Button5: TButton
    Left = 392
    Top = 215
    Width = 235
    Height = 34
    Caption = 'Set Target P to -20000'
    TabOrder = 9
    OnClick = Button5Click
  end
  object Button7: TButton
    Left = 392
    Top = 255
    Width = 235
    Height = 34
    Caption = 'Set Target P to 20000'
    TabOrder = 10
    OnClick = Button7Click
  end
  object Panel3: TPanel
    Left = 359
    Top = 48
    Width = 178
    Height = 41
    TabOrder = 11
    object RadioTargetReached: TRadioButton
      Left = 12
      Top = 16
      Width = 161
      Height = 17
      Caption = 'TargetReached'
      TabOrder = 0
    end
  end
  object Button6: TButton
    Left = 392
    Top = 176
    Width = 89
    Height = 33
    Caption = 'HM'
    TabOrder = 12
    OnClick = Button6Click
  end
  object Button8: TButton
    Left = 503
    Top = 176
    Width = 82
    Height = 31
    Caption = 'PP'
    TabOrder = 13
    OnClick = Button8Click
  end
  object EditOPMode: TEdit
    Left = 206
    Top = 181
    Width = 121
    Height = 31
    TabOrder = 14
  end
  object Timer1: TTimer
    Interval = 50
    OnTimer = Timer1Timer
    Left = 472
    Top = 480
  end
end

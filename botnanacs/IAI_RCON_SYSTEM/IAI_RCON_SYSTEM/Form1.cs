using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Globalization;
using BotnanaLib;

namespace IAI_RCON_SYSTEM
{
    public partial class Form1 : Form
    {
        private static Botnana bot;

        private static Boolean sfcLoaded = false;
        private static Boolean wsReady = false;
        private static Boolean sfcReady = false;
        private static Boolean rconReady = false;
        private static Boolean plcEnabled = false;
        private static int currAxis = 5;

        // gateway control signal
        private static Boolean gwMON = false;

        // gateway status signal
        private static Byte gwALMC = 0;
        private static Boolean gwSEMG = false;
        private static Boolean gwALML = false;
        private static Boolean gwALMH = false;
        private static Boolean gwMOD = false;
        private static Boolean gwERRT = false;
        private static Boolean gwLERC = false;
        private static Boolean gwRUN = false;
        private static UInt16 gwLNK = 0;

        // axis control signal
        private static Boolean axCSTR = false;
        private static Boolean axHOME = false;
        private static Boolean axSTP = false;
        private static Boolean axRES = false;
        private static Boolean axSON = false;
        private static Boolean axJISL = false;
        private static Boolean axJVEL = false;
        private static Boolean axJOGN = false;
        private static Boolean axJOGP = false;
        private static Boolean axPUSH = false;
        private static Boolean axDIR = false;
        private static Boolean axINC = false;
        private static Boolean axBKRL = false;

        // axis control parameter
        private static Int32 axPSD = 0;
        private static Int32 axPW = 0;
        private static UInt16 axSPD = 0;
        private static UInt16 axACDEC = 0;
        private static UInt16 axPCLV = 0;

        private static Boolean axPSDFocus = false;
        private static Boolean axPWFocus = false;
        private static Boolean axSPDFocus = false;
        private static Boolean axACDECFocus = false;
        private static Boolean axPCLVFocus = false;

        // axis status signal
        private static Boolean axPEND = false;
        private static Boolean axHEND = false;
        private static Boolean axMOVE = false;
        private static Boolean axALM = false;
        private static Boolean axSV = false;
        private static Boolean axPSFL = false;
        private static Boolean axLOAD = false;
        private static Boolean axALML = false;
        private static Boolean axMEND = false;
        private static Boolean axWEND = false;
        private static Boolean axMODES = false;
        private static Boolean axPZONE = false;
        private static Boolean axZONE1 = false;
        private static Boolean axZONE2 = false;
        private static Boolean axCRDY = false;
        private static Boolean axEMGS = false;

        // axis status parameter
        private static Int32 axPPD = 0;
        private static Int32 axMCCV = 0;
        private static Int16 axPSPD = 0;
        private static UInt16 axALMC = 0;

        private static HandleMessage onWSOpen = new HandleMessage(OnWSOpenCB);
        private static void OnWSOpenCB(IntPtr ptr, string data)
        {
            bot.EvaluateScript(".user-para");
            wsReady = true;
        }

        private static HandleMessage onWSError = new HandleMessage(OnWSErrorCB);
        private static void OnWSErrorCB(IntPtr ptr, string data)
        {
            wsReady = false;
            sfcReady = false;
            rconReady = false;
            new Thread(() => System.Windows.Forms.MessageBox.Show("WS error : " + data)).Start();
        }

        private static HandleMessage onUserParameter = new HandleMessage(OnUserParameterCB);
        private static void OnUserParameterCB(IntPtr ptr, string str)
        {
            int para = Int32.Parse(str);
            switch (para)
            {
                case 0:
                    // 設定 user parameter 為 0x10
                    // 如果此範例重新執行不會再載入以下 SFC
                    bot.EvaluateScript("$10 user-para!");
                    // 清除SFC 邏輯，載入 SFC 時會造成 real time cycle overrun，所以要暫時 ignore-overrun
                    // 載入後再執行 `reset-overrun`
                    bot.EvaluateScript("0sfc ignore-overrun");
                    // 要留意載入順序
                    bot.LoadSFC(@"..\..\rcon-system.fs");
                    bot.LoadSFC(@"..\..\plc.fs");
                    // SFC 設置完成後下個週期 reset overrun
                    bot.EvaluateScript("1 ms reset-overrun");
                    break;
                default:
                    break;
            }
            sfcLoaded = true;
        }

        private static HandleMessage onSFCReady = new HandleMessage(OnSFCReadyCB);
        private static void OnSFCReadyCB(IntPtr ptr, string str)
        {
            if (Int32.Parse(str) == -1) { sfcReady = true; } else { sfcReady = false; rconReady = false; }
        }

        private static HandleMessage onRCONReady = new HandleMessage(OnRCONReadyCB);
        private static void OnRCONReadyCB(IntPtr ptr, string str)
        {
            if (Int32.Parse(str) == -1) { rconReady = true; } else { rconReady = false; }
        }

        private static HandleMessage onPLCEnabled = new HandleMessage(OnPLCEnabledCB);
        private static void OnPLCEnabledCB (IntPtr ptr, string str)
        {
            if (Int32.Parse(str) == -1) { plcEnabled = true; } else { plcEnabled = false; }
        }

        private static HandleMessage onGWControl = new HandleMessage(OnGWControlCB);
        private static void OnGWControlCB(IntPtr ptr, string str)
        {
            int control = int.Parse(str);
            gwMON = (control & 0x8000) != 0;
        }

        private static HandleMessage onGWStatus = new HandleMessage(OnGWStatusCB);
        private static void OnGWStatusCB(IntPtr ptr, string str)
        {
            int status = int.Parse(str);
            gwALMC = (Byte)(status & 0xff);
            gwSEMG = (status & 0x100) != 0;
            gwSEMG = (status & 0x400) != 0;
            gwALMH = (status & 0x800) != 0;
            gwMOD = (status & 0x1000) != 0;
            gwERRT = (status & 0x2000) != 0;
            gwLERC = (status & 0x4000) != 0;
            gwRUN = (status & 0x8000) != 0;
            gwLNK = (UInt16)((status & 0xffff0000) >> 16);
        }

        private static HandleMessage onAXControl = new HandleMessage(OnAXControlCB);
        private static void OnAXControlCB(IntPtr ptr, string str)
        {
            int control = int.Parse(str);
            axCSTR = (control & 0x1) != 0;
            axHOME = (control & 0x2) != 0;
            axSTP = (control & 0x4) != 0;
            axRES = (control & 0x8) != 0;
            axSON = (control & 0x10) != 0;
            axJISL = (control & 0x20) != 0;
            axJVEL = (control & 0x40) != 0;
            axJOGN = (control & 0x80) != 0;
            axJOGP = (control & 0x100) != 0;
            axPUSH = (control & 0x1000) != 0;
            axDIR = (control & 0x2000) != 0;
            axINC = (control & 0x4000) != 0;
            axBKRL = (control & 0x8000) != 0;
        }

        private static HandleMessage onAXPSD = new HandleMessage(OnAXPSDCB);
        private static void OnAXPSDCB(IntPtr ptr, string str)
        {
            axPSD = Int32.Parse(str);
        }

        private static HandleMessage onAXPW = new HandleMessage(OnAXPWCB);
        private static void OnAXPWCB(IntPtr ptr, string str)
        {
            axPW = Int32.Parse(str);
        }

        private static HandleMessage onAXSPD = new HandleMessage(OnAXSPDCB);
        private static void OnAXSPDCB(IntPtr ptr, string str)
        {
            axSPD = UInt16.Parse(str);
        }

        private static HandleMessage onAXACDEC = new HandleMessage(OnAXACDECCB);
        private static void OnAXACDECCB(IntPtr ptr, string str)
        {
            axACDEC = UInt16.Parse(str);
        }

        private static HandleMessage onAXPCLV = new HandleMessage(OnAXPCLVCB);
        private static void OnAXPCLVCB(IntPtr ptr, string str)
        {
            axPCLV = UInt16.Parse(str);
        }

        private static HandleMessage onAXStatus = new HandleMessage(OnAXStatusCB);
        private static void OnAXStatusCB(IntPtr ptr, string str)
        {
            int status = int.Parse(str);
            axPEND = (status & 0x1) != 0;
            axHEND = (status & 0x2) != 0;
            axMOVE = (status & 0x4) != 0;
            axALM = (status & 0x8) != 0;
            axSV = (status & 0x10) != 0;
            axPSFL = (status & 0x20) != 0;
            axLOAD = (status & 0x40) != 0;
            axALML = (status & 0x80) != 0;
            axMEND = (status & 0x100) != 0;
            axWEND = (status & 0x200) != 0;
            axMODES = (status & 0x400) != 0;
            axPZONE = (status & 0x800) != 0;
            axZONE1 = (status & 0x1000) != 0;
            axZONE2 = (status & 0x2000) != 0;
            axCRDY = (status & 0x4000) != 0;
            axEMGS = (status & 0x8000) != 0;
        }

        private static HandleMessage onAXPPD = new HandleMessage(OnAXPPDCB);
        private static void OnAXPPDCB(IntPtr ptr, string str)
        {
            axPPD = Int32.Parse(str);
        }

        private static HandleMessage onAXMCCV = new HandleMessage(OnAXMCCVCB);
        private static void OnAXMCCVCB(IntPtr ptr, string str)
        {
            axMCCV = Int32.Parse(str);
        }

        private static HandleMessage onAXPSPD = new HandleMessage(OnAXPSPDCB);
        private static void OnAXPSPDCB(IntPtr ptr, string str)
        {
            axPSPD = (Int16)Int32.Parse(str);
        }

        private static HandleMessage onAXALMC = new HandleMessage(OnAXALMCCB);
        private static void OnAXALMCCB(IntPtr ptr, string str)
        {
            axALMC = UInt16.Parse(str);
        }

        public Form1()
        {
            InitializeComponent();
            bot = new Botnana("192.168.7.2");
            bot.SetOnOpenCB(IntPtr.Zero, onWSOpen);
            bot.SetOnErrorCB(IntPtr.Zero, onWSError);
            bot.SetTagCB("user_parameter", 0, IntPtr.Zero, onUserParameter);
            bot.SetTagCB("RCON_ready", 0, IntPtr.Zero, onRCONReady);
            bot.SetTagCB("SFC_ready", 0, IntPtr.Zero, onSFCReady);
            bot.SetTagCB("PLC_enabled", 0, IntPtr.Zero, onPLCEnabled);
            bot.SetTagCB("GW_control", 0, IntPtr.Zero, onGWControl);
            bot.SetTagCB("GW_status", 0, IntPtr.Zero, onGWStatus);
            bot.SetTagCB("AX_control", 0, IntPtr.Zero, onAXControl);
            bot.SetTagCB("AX_PSD", 0, IntPtr.Zero, onAXPSD);
            bot.SetTagCB("AX_PW", 0, IntPtr.Zero, onAXPW);
            bot.SetTagCB("AX_SPD", 0, IntPtr.Zero, onAXSPD);
            bot.SetTagCB("AX_ACDEC", 0, IntPtr.Zero, onAXACDEC);
            bot.SetTagCB("AX_PCLV", 0, IntPtr.Zero, onAXPCLV);
            bot.SetTagCB("AX_status", 0, IntPtr.Zero, onAXStatus);
            bot.SetTagCB("AX_PPD", 0, IntPtr.Zero, onAXPPD);
            bot.SetTagCB("AX_MCCV", 0, IntPtr.Zero, onAXMCCV);
            bot.SetTagCB("AX_PSPD", 0, IntPtr.Zero, onAXPSPD);
            bot.SetTagCB("AX_ALMC", 0, IntPtr.Zero, onAXALMC);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Process thisProc = Process.GetCurrentProcess();
            thisProc.PriorityClass = ProcessPriorityClass.RealTime;

            bot.Connect();
            Thread.Sleep(500);

            timer1.Interval = 50;
            timer1.Enabled = true;
        }

        private void buttonWSControl_Click(object sender, EventArgs e)
        {
            if (wsReady) { bot.Disconnect(); } else { bot.Connect(); }
        }

        private void buttonReboot_Click(object sender, EventArgs e)
        {
            bot.Reboot();
        }

        private void buttonPLCEnable_Click(object sender, EventArgs e)
        {
            if (plcEnabled) { bot.EvaluateScript("false plc-enabled !"); } else { bot.EvaluateScript("true plc-enabled !"); }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 更新 labelAPPState 顯式
            if (!wsReady)
            {
                labelAPPState.Text = "Connection Not Ready";
                labelAPPState.BackColor = Color.Red;
            } else if (!sfcReady)
            {
                labelAPPState.Text = "SFC Not Ready";
                labelAPPState.BackColor = Color.Red;
            } else if (!rconReady)
            {
                labelAPPState.Text = "RCON Not Ready";
                labelAPPState.BackColor = Color.Red;
            } else
            {
                labelAPPState.Text = "RCON Ready";
                labelAPPState.BackColor = Color.Green;
            }

            // 更新 buttonWSControl 顯式
            if (wsReady) { buttonWSControl.Text = "Disconnect"; } else { buttonWSControl.Text = "Connect"; }

            // 更新 buttonPLCEnable 顯示
            if (plcEnabled) { buttonPLCEnable.Text = "Disable PLC"; } else { buttonPLCEnable.Text = "Enable PLC"; }

            // 更新 gateway control signal 顯示
            if (gwMON) { ledGWMON.ForeColor = Color.Green; } else { ledGWMON.ForeColor = Color.Gray; }

            // 更新 gateway status signal 顯示
            textBoxGWALMC.Text = "0x" + gwALMC.ToString("X2");
            if (gwSEMG) { ledGWSEMG.ForeColor = Color.Red; } else { ledGWSEMG.ForeColor = Color.LightGray; }
            if (gwSEMG) { ledGWALML.ForeColor = Color.Red; } else { ledGWALML.ForeColor = Color.LightGray; }
            if (gwALMH) { ledGWALMH.ForeColor = Color.Red; } else { ledGWALMH.ForeColor = Color.LightGray; }
            if (gwMOD) { ledGWMOD.ForeColor = Color.Red; } else { ledGWMOD.ForeColor = Color.LightGray; }
            if (gwERRT) { ledGWERRT.ForeColor = Color.Red; } else { ledGWERRT.ForeColor = Color.LightGray; }
            if (gwLERC) { ledGWLERC.ForeColor = Color.Red; } else { ledGWLERC.ForeColor = Color.LightGray; }
            if (gwRUN) { ledGWRUN.ForeColor = Color.Red; } else { ledGWRUN.ForeColor = Color.LightGray; }
            textBoxGWLNK.Text = "0x" + gwLNK.ToString("X4");

            // 更新 axis control signal 顯示
            if (axCSTR) { ledAXCSTR.ForeColor = Color.Green; } else { ledAXCSTR.ForeColor = Color.Gray; }
            if (axHOME) { ledAXHOME.ForeColor = Color.Green; } else { ledAXHOME.ForeColor = Color.Gray; }
            if (axSTP) { ledAXSTP.ForeColor = Color.Green; } else { ledAXSTP.ForeColor = Color.Gray; }
            if (axRES) { ledAXRES.ForeColor = Color.Green; } else { ledAXRES.ForeColor = Color.Gray; }
            if (axSON) { ledAXSON.ForeColor = Color.Green; } else { ledAXSON.ForeColor = Color.Gray; }
            if (axJISL) { ledAXJISL.ForeColor = Color.Green; } else { ledAXJISL.ForeColor = Color.Gray; }
            if (axJVEL) { ledAXJVEL.ForeColor = Color.Green; } else { ledAXJVEL.ForeColor = Color.Gray; }
            if (axJOGN) { ledAXJOGN.ForeColor = Color.Green; } else { ledAXJOGN.ForeColor = Color.Gray; }
            if (axJOGP) { ledAXJOGP.ForeColor = Color.Green; } else { ledAXJOGP.ForeColor = Color.Gray; }
            if (axPUSH) { ledAXPUSH.ForeColor = Color.Green; } else { ledAXPUSH.ForeColor = Color.Gray; }
            if (axDIR) { ledAXDIR.ForeColor = Color.Green; } else { ledAXDIR.ForeColor = Color.Gray; }
            if (axINC) { ledAXINC.ForeColor = Color.Green; } else { ledAXINC.ForeColor = Color.Gray; }
            if (axBKRL) { ledAXBKRL.ForeColor = Color.Green; } else { ledAXBKRL.ForeColor = Color.Gray; }

            // 更新 axis control parameter 顯示
            if (!axPSDFocus) { textBoxAXPSD.Text = axPSD.ToString(); }
            if (!axPWFocus) { textBoxAXPW.Text = axPW.ToString(); }
            if (!axSPDFocus) { textBoxAXSPD.Text = axSPD.ToString(); }
            if (!axACDECFocus) { textBoxAXACDEC.Text = axACDEC.ToString(); }
            if (!axPCLVFocus) { textBoxAXPCLV.Text = axPCLV.ToString(); }

            // 更新 axis status signal 顯示
            if (axPEND) { ledAXPEND.ForeColor = Color.Red; } else { ledAXPEND.ForeColor = Color.LightGray; }
            if (axHEND) { ledAXHEND.ForeColor = Color.Red; } else { ledAXHEND.ForeColor = Color.LightGray; }
            if (axMOVE) { ledAXMOVE.ForeColor = Color.Red; } else { ledAXMOVE.ForeColor = Color.LightGray; }
            if (axALM) { ledAXALM.ForeColor = Color.Red; } else { ledAXALM.ForeColor = Color.LightGray; }
            if (axSV) { ledAXSV.ForeColor = Color.Red; } else { ledAXSV.ForeColor = Color.LightGray; }
            if (axPSFL) { ledAXPSFL.ForeColor = Color.Red; } else { ledAXPSFL.ForeColor = Color.LightGray; }
            if (axLOAD) { ledAXLOAD.ForeColor = Color.Red; } else { ledAXLOAD.ForeColor = Color.LightGray; }
            if (axALML) { ledAXALML.ForeColor = Color.Red; } else { ledAXALML.ForeColor = Color.LightGray; }
            if (axMEND) { ledAXMEND.ForeColor = Color.Red; } else { ledAXMEND.ForeColor = Color.LightGray; }
            if (axWEND) { ledAXWEND.ForeColor = Color.Red; } else { ledAXWEND.ForeColor = Color.LightGray; }
            if (axMODES) { ledAXMODES.ForeColor = Color.Red; } else { ledAXMODES.ForeColor = Color.LightGray; }
            if (axPZONE) { ledAXPZONE.ForeColor = Color.Red; } else { ledAXPZONE.ForeColor = Color.LightGray; }
            if (axZONE1) { ledAXZONE1.ForeColor = Color.Red; } else { ledAXZONE1.ForeColor = Color.LightGray; }
            if (axZONE2) { ledAXZONE2.ForeColor = Color.Red; } else { ledAXZONE2.ForeColor = Color.LightGray; }
            if (axCRDY) { ledAXCRDY.ForeColor = Color.Red; } else { ledAXCRDY.ForeColor = Color.LightGray; }
            if (axEMGS) { ledAXEMGS.ForeColor = Color.Red; } else { ledAXEMGS.ForeColor = Color.LightGray; }

            // 更新 axis status parameter 顯示
            textBoxAXPPD.Text = axPPD.ToString();
            textBoxAXMCCV.Text = axMCCV.ToString();
            textBoxAXPSPD.Text = axPSPD.ToString();
            textBoxAXALMC.Text = "0x" + axALMC.ToString("X2");

            // 定期詢問控制器狀態和資料
            if (wsReady && sfcLoaded)
            {
                bot.EvaluateScript(currAxis.ToString() + " poll");
            }
        }

        private void comboBoxSelectedAX_SelectedIndexChanged(object sender, EventArgs e)
        {
            currAxis = ((ComboBox)sender).SelectedIndex;
        }

        private void ledGWMON_Click(object sender, EventArgs e)
        {
            if (gwMON) { bot.EvaluateScript("-gw-mon"); } else { bot.EvaluateScript("+gw-mon"); }
        }

        private void textBoxAXPSD_MouseDown(object sender, MouseEventArgs e)
        {
            axPSDFocus = true;
        }

        private void textBoxAXPSD_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            bot.EvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-pos-spec!");
            axPSDFocus = false;
        }

        private void textBoxAXPSD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox tb = (TextBox)sender;
                bot.EvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-pos-spec!");
                axPSDFocus = false;
            }
        }

        private void textBoxAXPW_MouseDown(object sender, MouseEventArgs e)
        {
            axPWFocus = true;
        }

        private void textBoxAXPW_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            bot.EvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-pos-width!");
            axPWFocus = false;
        }

        private void textBoxAXPW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox tb = (TextBox)sender;
                bot.EvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-pos-width!");
                axPWFocus = false;
            }
        }

        private void textBoxAXSPD_MouseDown(object sender, MouseEventArgs e)
        {
            axSPDFocus = true;
        }

        private void textBoxAXSPD_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            bot.EvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-spd!");
            axSPDFocus = false;
        }

        private void textBoxAXSPD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox tb = (TextBox)sender;
                bot.EvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-spd!");
                axSPDFocus = false;
            }
        }

        private void textBoxAXACDEC_MouseDown(object sender, MouseEventArgs e)
        {
            axACDECFocus = true;
        }

        private void textBoxAXACDEC_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            bot.EvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-ac/dec!");
            axACDECFocus = false;
        }

        private void textBoxAXACDEC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox tb = (TextBox)sender;
                bot.EvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-ac/dec!");
                axACDECFocus = false;
            }
        }

        private void textBoxAXPCLV_MouseDown(object sender, MouseEventArgs e)
        {
            axPCLVFocus = true;
        }

        private void textBoxAXPCLV_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            bot.EvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-curr-limit!");
            axPCLVFocus = false;
        }

        private void textBoxAXPCLV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox tb = (TextBox)sender;
                bot.EvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-curr-limit!");
                axPCLVFocus = false;
            }
        }

        private void ledAXCSTR_Click(object sender, EventArgs e)
        {
            if (axCSTR) { bot.EvaluateScript(currAxis.ToString() + " -ax-cstr"); } else { bot.EvaluateScript(currAxis.ToString() + " +ax-cstr"); }
        }

        private void ledAXHOME_Click(object sender, EventArgs e)
        {
            if (axHOME) { bot.EvaluateScript(currAxis.ToString() + " -ax-home"); } else { bot.EvaluateScript(currAxis.ToString() + " +ax-home"); }
        }

        private void ledAXSTP_Click(object sender, EventArgs e)
        {
            if (axSTP) { bot.EvaluateScript(currAxis.ToString() + " -ax-stp"); } else { bot.EvaluateScript(currAxis.ToString() + " +ax-stp"); }
        }

        private void ledAXRES_Click(object sender, EventArgs e)
        {
            if (axRES) { bot.EvaluateScript(currAxis.ToString() + " -ax-res"); } else { bot.EvaluateScript(currAxis.ToString() + " +ax-res"); }
        }

        private void ledAXSON_Click(object sender, EventArgs e)
        {
            if (axSON) { bot.EvaluateScript(currAxis.ToString() + " -ax-son"); } else { bot.EvaluateScript(currAxis.ToString() + " +ax-son"); }
        }

        private void ledAXJISL_Click(object sender, EventArgs e)
        {
            if (axJISL) { bot.EvaluateScript(currAxis.ToString() + " -ax-jisl"); } else { bot.EvaluateScript(currAxis.ToString() + " +ax-jisl"); }
        }

        private void ledAXJVEL_Click(object sender, EventArgs e)
        {
            if (axJVEL) { bot.EvaluateScript(currAxis.ToString() + " -ax-jvel"); } else { bot.EvaluateScript(currAxis.ToString() + " +ax-jvel"); }
        }

        private void ledAXJOGN_Click(object sender, EventArgs e)
        {
            if (axJOGN) { bot.EvaluateScript(currAxis.ToString() + " -ax-jog-"); } else { bot.EvaluateScript(currAxis.ToString() + " +ax-jog-"); }
        }

        private void ledAXJOGP_Click(object sender, EventArgs e)
        {
            if (axJOGP) { bot.EvaluateScript(currAxis.ToString() + " -ax-jog+"); } else { bot.EvaluateScript(currAxis.ToString() + " +ax-jog+"); }
        }

        private void ledAXPUSH_Click(object sender, EventArgs e)
        {
            if (axPUSH) { bot.EvaluateScript(currAxis.ToString() + " -ax-push"); } else { bot.EvaluateScript(currAxis.ToString() + " +ax-push"); }
        }

        private void ledAXDIR_Click(object sender, EventArgs e)
        {
            if (axDIR) { bot.EvaluateScript(currAxis.ToString() + " -ax-dir"); } else { bot.EvaluateScript(currAxis.ToString() + " +ax-dir"); }
        }

        private void ledAXINC_Click(object sender, EventArgs e)
        {
            if (axINC) { bot.EvaluateScript(currAxis.ToString() + " -ax-inc"); } else { bot.EvaluateScript(currAxis.ToString() + " +ax-inc"); }
        }

        private void ledAXBKRL_Click(object sender, EventArgs e)
        {
            if (axBKRL) { bot.EvaluateScript(currAxis.ToString() + " -ax-bkrl"); } else { bot.EvaluateScript(currAxis.ToString() + " +ax-bkrl"); }
        }
    }
}

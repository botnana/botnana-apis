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

        private static Boolean SFCLoaded = false;
        private static Boolean WS_ready = false;
        private static Boolean SFC_ready = false;
        private static Boolean RCON_ready = false;
        private static Boolean PLC_enabled = false;
        private static int currAxis = 5;

        // gateway control signal
        private static Boolean GW_MON = false;

        // gateway status signal
        private static Byte GW_ALMC = 0;
        private static Boolean GW_SEMG = false;
        private static Boolean GW_ALML = false;
        private static Boolean GW_ALMH = false;
        private static Boolean GW_MOD = false;
        private static Boolean GW_ERRT = false;
        private static Boolean GW_LERC = false;
        private static Boolean GW_RUN = false;
        private static UInt16 GW_LNK = 0;

        // axis control signal
        private static Boolean AX_CSTR = false;
        private static Boolean AX_HOME = false;
        private static Boolean AX_STP = false;
        private static Boolean AX_RES = false;
        private static Boolean AX_SON = false;
        private static Boolean AX_JISL = false;
        private static Boolean AX_JVEL = false;
        private static Boolean AX_JOGN = false;
        private static Boolean AX_JOGP = false;
        private static Boolean AX_PUSH = false;
        private static Boolean AX_DIR = false;
        private static Boolean AX_INC = false;
        private static Boolean AX_BKRL = false;

        // axis control parameter
        private static Int32 AX_PSD = 0;
        private static Int32 AX_PW = 0;
        private static UInt16 AX_SPD = 0;
        private static UInt16 AX_ACDEC = 0;
        private static UInt16 AX_PCLV = 0;

        private static Boolean AX_PSD_focus = false;
        private static Boolean AX_PW_focus = false;
        private static Boolean AX_SPD_focus = false;
        private static Boolean AX_ACDEC_focus = false;
        private static Boolean AX_PCLV_focus = false;

        // axis status signal
        private static Boolean AX_PEND = false;
        private static Boolean AX_HEND = false;
        private static Boolean AX_MOVE = false;
        private static Boolean AX_ALM = false;
        private static Boolean AX_SV = false;
        private static Boolean AX_PSFL = false;
        private static Boolean AX_LOAD = false;
        private static Boolean AX_ALML = false;
        private static Boolean AX_MEND = false;
        private static Boolean AX_WEND = false;
        private static Boolean AX_MODES = false;
        private static Boolean AX_PZONE = false;
        private static Boolean AX_ZONE1 = false;
        private static Boolean AX_ZONE2 = false;
        private static Boolean AX_CRDY = false;
        private static Boolean AX_EMGS = false;

        // axis status parameter
        private static Int32 AX_PPD = 0;
        private static Int32 AX_MCCV = 0;
        private static Int16 AX_PSPD = 0;
        private static UInt16 AX_ALMC = 0;

        private static HandleMessage onWSOpen = new HandleMessage(onWSOpenCB);
        private static void onWSOpenCB(IntPtr ptr, string data)
        {
            WS_ready = true;
        }

        private static HandleMessage onWSError = new HandleMessage(onWSErrorCB);
        private static void onWSErrorCB(IntPtr ptr, string data)
        {
            WS_ready = false;
            SFC_ready = false;
            RCON_ready = false;
            new Thread(() => System.Windows.Forms.MessageBox.Show("WS error : " + data)).Start();
        }

        private static HandleMessage onUserParameter = new HandleMessage(onUserParameterCB);
        private static void onUserParameterCB(IntPtr ptr, string str)
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
                    // 等待 SFC 設置完成
                    Thread.Sleep(500);
                    bot.EvaluateScript("reset-overrun");
                    break;
                default:
                    break;
            }
            SFCLoaded = true;
        }

        private static HandleMessage onSFCReady = new HandleMessage(onSFCReadyCB);
        private static void onSFCReadyCB(IntPtr ptr, string str)
        {
            if (Int32.Parse(str) == -1) { SFC_ready = true; } else { SFC_ready = false; RCON_ready = false; }
        }

        private static HandleMessage onRCONReady = new HandleMessage(onRCONReadyCB);
        private static void onRCONReadyCB(IntPtr ptr, string str)
        {
            if (Int32.Parse(str) == -1) { RCON_ready = true; } else { RCON_ready = false; }
        }

        private static HandleMessage onPLCEnabled = new HandleMessage(onPLCEnabledCB);
        private static void onPLCEnabledCB (IntPtr ptr, string str)
        {
            if (Int32.Parse(str) == -1) { PLC_enabled = true; } else { PLC_enabled = false; }
        }

        private static HandleMessage onGWControl = new HandleMessage(onGWControlCB);
        private static void onGWControlCB(IntPtr ptr, string str)
        {
            int control = int.Parse(str);
            GW_MON = (control & 0x8000) != 0;
        }

        private static HandleMessage onGWStatus = new HandleMessage(onGWStatusCB);
        private static void onGWStatusCB(IntPtr ptr, string str)
        {
            int status = int.Parse(str);
            GW_ALMC = (Byte)(status & 0xff);
            GW_SEMG = (status & 0x100) != 0;
            GW_ALML = (status & 0x400) != 0;
            GW_ALMH = (status & 0x800) != 0;
            GW_MOD = (status & 0x1000) != 0;
            GW_ERRT = (status & 0x2000) != 0;
            GW_LERC = (status & 0x4000) != 0;
            GW_RUN = (status & 0x8000) != 0;
            GW_LNK = (UInt16)((status & 0xffff0000) >> 16);
        }

        private static HandleMessage onAXControl = new HandleMessage(onAXControlCB);
        private static void onAXControlCB(IntPtr ptr, string str)
        {
            int control = int.Parse(str);
            AX_CSTR = (control & 0x1) != 0;
            AX_HOME = (control & 0x2) != 0;
            AX_STP = (control & 0x4) != 0;
            AX_RES = (control & 0x8) != 0;
            AX_SON = (control & 0x10) != 0;
            AX_JISL = (control & 0x20) != 0;
            AX_JVEL = (control & 0x40) != 0;
            AX_JOGN = (control & 0x80) != 0;
            AX_JOGP = (control & 0x100) != 0;
            AX_PUSH = (control & 0x1000) != 0;
            AX_DIR = (control & 0x2000) != 0;
            AX_INC = (control & 0x4000) != 0;
            AX_BKRL = (control & 0x8000) != 0;
        }

        private static HandleMessage onAXPSD = new HandleMessage(onAXPSDCB);
        private static void onAXPSDCB(IntPtr ptr, string str)
        {
            AX_PSD = Int32.Parse(str);
        }

        private static HandleMessage onAXPW = new HandleMessage(onAXPWCB);
        private static void onAXPWCB(IntPtr ptr, string str)
        {
            AX_PW = Int32.Parse(str);
        }

        private static HandleMessage onAXSPD = new HandleMessage(onAXSPDCB);
        private static void onAXSPDCB(IntPtr ptr, string str)
        {
            AX_SPD = UInt16.Parse(str);
        }

        private static HandleMessage onAXACDEC = new HandleMessage(onAXACDECCB);
        private static void onAXACDECCB(IntPtr ptr, string str)
        {
            AX_ACDEC = UInt16.Parse(str);
        }

        private static HandleMessage onAXPCLV = new HandleMessage(onAXPCLVCB);
        private static void onAXPCLVCB(IntPtr ptr, string str)
        {
            AX_PCLV = UInt16.Parse(str);
        }

        private static HandleMessage onAXStatus = new HandleMessage(onAXStatusCB);
        private static void onAXStatusCB(IntPtr ptr, string str)
        {
            int status = int.Parse(str);
            AX_PEND = (status & 0x1) != 0;
            AX_HEND = (status & 0x2) != 0;
            AX_MOVE = (status & 0x4) != 0;
            AX_ALM = (status & 0x8) != 0;
            AX_SV = (status & 0x10) != 0;
            AX_PSFL = (status & 0x20) != 0;
            AX_LOAD = (status & 0x40) != 0;
            AX_ALML = (status & 0x80) != 0;
            AX_MEND = (status & 0x100) != 0;
            AX_WEND = (status & 0x200) != 0;
            AX_MODES = (status & 0x400) != 0;
            AX_PZONE = (status & 0x800) != 0;
            AX_ZONE1 = (status & 0x1000) != 0;
            AX_ZONE2 = (status & 0x2000) != 0;
            AX_CRDY = (status & 0x4000) != 0;
            AX_EMGS = (status & 0x8000) != 0;
        }

        private static HandleMessage onAXPPD = new HandleMessage(onAXPPDCB);
        private static void onAXPPDCB(IntPtr ptr, string str)
        {
            AX_PPD = Int32.Parse(str);
        }

        private static HandleMessage onAXMCCV = new HandleMessage(onAXMCCVCB);
        private static void onAXMCCVCB(IntPtr ptr, string str)
        {
            AX_MCCV = Int32.Parse(str);
        }

        private static HandleMessage onAXPSPD = new HandleMessage(onAXPSPDCB);
        private static void onAXPSPDCB(IntPtr ptr, string str)
        {
            AX_PSPD = (Int16)Int32.Parse(str);
        }

        private static HandleMessage onAXALMC = new HandleMessage(onAXALMCCB);
        private static void onAXALMCCB(IntPtr ptr, string str)
        {
            AX_ALMC = UInt16.Parse(str);
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
            bot.EvaluateScript(".user-para");

            timer1.Interval = 50;
            timer1.Enabled = true;
        }

        private void buttonWSControl_Click(object sender, EventArgs e)
        {
            if (WS_ready) { bot.Disconnect(); } else { bot.Connect(); }
        }

        private void buttonReboot_Click(object sender, EventArgs e)
        {
            bot.Reboot();
        }

        private void buttonPLCEnable_Click(object sender, EventArgs e)
        {
            if (PLC_enabled) { bot.EvaluateScript("false plc-enabled !"); } else { bot.EvaluateScript("true plc-enabled !"); }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 更新 labelAPPState 顯式
            if (!WS_ready)
            {
                labelAPPState.Text = "Connection Not Ready";
                labelAPPState.BackColor = Color.Red;
            } else if (!SFC_ready)
            {
                labelAPPState.Text = "SFC Not Ready";
                labelAPPState.BackColor = Color.Red;
            } else if (!RCON_ready)
            {
                labelAPPState.Text = "RCON Not Ready";
                labelAPPState.BackColor = Color.Red;
            } else
            {
                labelAPPState.Text = "RCON Ready";
                labelAPPState.BackColor = Color.Green;
            }

            // 更新 buttonWSControl 顯式
            if (WS_ready) { buttonWSControl.Text = "Disconnect"; } else { buttonWSControl.Text = "Connect"; }

            // 更新 buttonPLCEnable 顯示
            if (PLC_enabled) { buttonPLCEnable.Text = "Disable PLC"; } else { buttonPLCEnable.Text = "Enable PLC"; }

            // 更新 gateway control signal 顯示
            if (GW_MON) { ledGWMON.ForeColor = Color.Green; } else { ledGWMON.ForeColor = Color.Gray; }

            // 更新 gateway status signal 顯示
            textBoxGWALMC.Text = "0x" + GW_ALMC.ToString("X2");
            if (GW_SEMG) { ledGWSEMG.ForeColor = Color.Red; } else { ledGWSEMG.ForeColor = Color.LightGray; }
            if (GW_ALML) { ledGWALML.ForeColor = Color.Red; } else { ledGWALML.ForeColor = Color.LightGray; }
            if (GW_ALMH) { ledGWALMH.ForeColor = Color.Red; } else { ledGWALMH.ForeColor = Color.LightGray; }
            if (GW_MOD) { ledGWMOD.ForeColor = Color.Red; } else { ledGWMOD.ForeColor = Color.LightGray; }
            if (GW_ERRT) { ledGWERRT.ForeColor = Color.Red; } else { ledGWERRT.ForeColor = Color.LightGray; }
            if (GW_LERC) { ledGWLERC.ForeColor = Color.Red; } else { ledGWLERC.ForeColor = Color.LightGray; }
            if (GW_RUN) { ledGWRUN.ForeColor = Color.Red; } else { ledGWRUN.ForeColor = Color.LightGray; }
            textBoxGWLNK.Text = "0x" + GW_LNK.ToString("X4");

            // 更新 axis control signal 顯示
            if (AX_CSTR) { ledAXCSTR.ForeColor = Color.Green; } else { ledAXCSTR.ForeColor = Color.Gray; }
            if (AX_HOME) { ledAXHOME.ForeColor = Color.Green; } else { ledAXHOME.ForeColor = Color.Gray; }
            if (AX_STP) { ledAXSTP.ForeColor = Color.Green; } else { ledAXSTP.ForeColor = Color.Gray; }
            if (AX_RES) { ledAXRES.ForeColor = Color.Green; } else { ledAXRES.ForeColor = Color.Gray; }
            if (AX_SON) { ledAXSON.ForeColor = Color.Green; } else { ledAXSON.ForeColor = Color.Gray; }
            if (AX_JISL) { ledAXJISL.ForeColor = Color.Green; } else { ledAXJISL.ForeColor = Color.Gray; }
            if (AX_JVEL) { ledAXJVEL.ForeColor = Color.Green; } else { ledAXJVEL.ForeColor = Color.Gray; }
            if (AX_JOGN) { ledAXJOGN.ForeColor = Color.Green; } else { ledAXJOGN.ForeColor = Color.Gray; }
            if (AX_JOGP) { ledAXJOGP.ForeColor = Color.Green; } else { ledAXJOGP.ForeColor = Color.Gray; }
            if (AX_PUSH) { ledAXPUSH.ForeColor = Color.Green; } else { ledAXPUSH.ForeColor = Color.Gray; }
            if (AX_DIR) { ledAXDIR.ForeColor = Color.Green; } else { ledAXDIR.ForeColor = Color.Gray; }
            if (AX_INC) { ledAXINC.ForeColor = Color.Green; } else { ledAXINC.ForeColor = Color.Gray; }
            if (AX_BKRL) { ledAXBKRL.ForeColor = Color.Green; } else { ledAXBKRL.ForeColor = Color.Gray; }

            // 更新 axis control parameter 顯示
            if (!AX_PSD_focus) { textBoxAXPSD.Text = AX_PSD.ToString(); }
            if (!AX_PW_focus) { textBoxAXPW.Text = AX_PW.ToString(); }
            if (!AX_SPD_focus) { textBoxAXSPD.Text = AX_SPD.ToString(); }
            if (!AX_ACDEC_focus) { textBoxAXACDEC.Text = AX_ACDEC.ToString(); }
            if (!AX_PCLV_focus) { textBoxAXPCLV.Text = AX_PCLV.ToString(); }

            // 更新 axis status signal 顯示
            if (AX_PEND) { ledAXPEND.ForeColor = Color.Red; } else { ledAXPEND.ForeColor = Color.LightGray; }
            if (AX_HEND) { ledAXHEND.ForeColor = Color.Red; } else { ledAXHEND.ForeColor = Color.LightGray; }
            if (AX_MOVE) { ledAXMOVE.ForeColor = Color.Red; } else { ledAXMOVE.ForeColor = Color.LightGray; }
            if (AX_ALM) { ledAXALM.ForeColor = Color.Red; } else { ledAXALM.ForeColor = Color.LightGray; }
            if (AX_SV) { ledAXSV.ForeColor = Color.Red; } else { ledAXSV.ForeColor = Color.LightGray; }
            if (AX_PSFL) { ledAXPSFL.ForeColor = Color.Red; } else { ledAXPSFL.ForeColor = Color.LightGray; }
            if (AX_LOAD) { ledAXLOAD.ForeColor = Color.Red; } else { ledAXLOAD.ForeColor = Color.LightGray; }
            if (AX_ALML) { ledAXALML.ForeColor = Color.Red; } else { ledAXALML.ForeColor = Color.LightGray; }
            if (AX_MEND) { ledAXMEND.ForeColor = Color.Red; } else { ledAXMEND.ForeColor = Color.LightGray; }
            if (AX_WEND) { ledAXWEND.ForeColor = Color.Red; } else { ledAXWEND.ForeColor = Color.LightGray; }
            if (AX_MODES) { ledAXMODES.ForeColor = Color.Red; } else { ledAXMODES.ForeColor = Color.LightGray; }
            if (AX_PZONE) { ledAXPZONE.ForeColor = Color.Red; } else { ledAXPZONE.ForeColor = Color.LightGray; }
            if (AX_ZONE1) { ledAXZONE1.ForeColor = Color.Red; } else { ledAXZONE1.ForeColor = Color.LightGray; }
            if (AX_ZONE2) { ledAXZONE2.ForeColor = Color.Red; } else { ledAXZONE2.ForeColor = Color.LightGray; }
            if (AX_CRDY) { ledAXCRDY.ForeColor = Color.Red; } else { ledAXCRDY.ForeColor = Color.LightGray; }
            if (AX_EMGS) { ledAXEMGS.ForeColor = Color.Red; } else { ledAXEMGS.ForeColor = Color.LightGray; }

            // 更新 axis status parameter 顯示
            textBoxAXPPD.Text = AX_PPD.ToString();
            textBoxAXMCCV.Text = AX_MCCV.ToString();
            textBoxAXPSPD.Text = AX_PSPD.ToString();
            textBoxAXALMC.Text = "0x" + AX_ALMC.ToString("X2");

            // 定期詢問控制器狀態和資料
            if ( WS_ready && SFCLoaded)
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
            if (GW_MON) { bot.EvaluateScript("-gw-mon"); } else { bot.EvaluateScript("+gw-mon"); }
        }

        private void textBoxAXPSD_MouseDown(object sender, MouseEventArgs e)
        {
            AX_PSD_focus = true;
        }

        private void textBoxAXPSD_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            bot.EvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-pos-spec!");
            AX_PSD_focus = false;
        }

        private void textBoxAXPSD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox tb = (TextBox)sender;
                bot.EvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-pos-spec!");
                AX_PSD_focus = false;
            }
        }

        private void textBoxAXPW_MouseDown(object sender, MouseEventArgs e)
        {
            AX_PW_focus = true;
        }

        private void textBoxAXPW_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            bot.EvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-pos-width!");
            AX_PW_focus = false;
        }

        private void textBoxAXPW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox tb = (TextBox)sender;
                bot.EvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-pos-width!");
                AX_PW_focus = false;
            }
        }

        private void textBoxAXSPD_MouseDown(object sender, MouseEventArgs e)
        {
            AX_SPD_focus = true;
        }

        private void textBoxAXSPD_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            bot.EvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-spd!");
            AX_SPD_focus = false;
        }

        private void textBoxAXSPD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox tb = (TextBox)sender;
                bot.EvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-spd!");
                AX_SPD_focus = false;
            }
        }

        private void textBoxAXACDEC_MouseDown(object sender, MouseEventArgs e)
        {
            AX_ACDEC_focus = true;
        }

        private void textBoxAXACDEC_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            bot.EvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-ac/dec!");
            AX_ACDEC_focus = false;
        }

        private void textBoxAXACDEC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox tb = (TextBox)sender;
                bot.EvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-ac/dec!");
                AX_ACDEC_focus = false;
            }
        }

        private void textBoxAXPCLV_MouseDown(object sender, MouseEventArgs e)
        {
            AX_PCLV_focus = true;
        }

        private void textBoxAXPCLV_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            bot.EvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-curr-limit!");
            AX_PCLV_focus = false;
        }

        private void textBoxAXPCLV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox tb = (TextBox)sender;
                bot.EvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-curr-limit!");
                AX_PCLV_focus = false;
            }
        }

        private void ledAXCSTR_Click(object sender, EventArgs e)
        {
            if (AX_CSTR) { bot.EvaluateScript(currAxis.ToString() + " -ax-cstr"); } else { bot.EvaluateScript(currAxis.ToString() + " +ax-cstr"); }
        }

        private void ledAXHOME_Click(object sender, EventArgs e)
        {
            if (AX_HOME) { bot.EvaluateScript(currAxis.ToString() + " -ax-home"); } else { bot.EvaluateScript(currAxis.ToString() + " +ax-home"); }
        }

        private void ledAXSTP_Click(object sender, EventArgs e)
        {
            if (AX_STP) { bot.EvaluateScript(currAxis.ToString() + " -ax-stp"); } else { bot.EvaluateScript(currAxis.ToString() + " +ax-stp"); }
        }

        private void ledAXRES_Click(object sender, EventArgs e)
        {
            if (AX_RES) { bot.EvaluateScript(currAxis.ToString() + " -ax-res"); } else { bot.EvaluateScript(currAxis.ToString() + " +ax-res"); }
        }

        private void ledAXSON_Click(object sender, EventArgs e)
        {
            if (AX_SON) { bot.EvaluateScript(currAxis.ToString() + " -ax-son"); } else { bot.EvaluateScript(currAxis.ToString() + " +ax-son"); }
        }

        private void ledAXJISL_Click(object sender, EventArgs e)
        {
            if (AX_JISL) { bot.EvaluateScript(currAxis.ToString() + " -ax-jisl"); } else { bot.EvaluateScript(currAxis.ToString() + " +ax-jisl"); }
        }

        private void ledAXJVEL_Click(object sender, EventArgs e)
        {
            if (AX_JVEL) { bot.EvaluateScript(currAxis.ToString() + " -ax-jvel"); } else { bot.EvaluateScript(currAxis.ToString() + " +ax-jvel"); }
        }

        private void ledAXJOGN_Click(object sender, EventArgs e)
        {
            if (AX_JOGN) { bot.EvaluateScript(currAxis.ToString() + " -ax-jog-"); } else { bot.EvaluateScript(currAxis.ToString() + " +ax-jog-"); }
        }

        private void ledAXJOGP_Click(object sender, EventArgs e)
        {
            if (AX_JOGP) { bot.EvaluateScript(currAxis.ToString() + " -ax-jog+"); } else { bot.EvaluateScript(currAxis.ToString() + " +ax-jog+"); }
        }

        private void ledAXPUSH_Click(object sender, EventArgs e)
        {
            if (AX_PUSH) { bot.EvaluateScript(currAxis.ToString() + " -ax-push"); } else { bot.EvaluateScript(currAxis.ToString() + " +ax-push"); }
        }

        private void ledAXDIR_Click(object sender, EventArgs e)
        {
            if (AX_DIR) { bot.EvaluateScript(currAxis.ToString() + " -ax-dir"); } else { bot.EvaluateScript(currAxis.ToString() + " +ax-dir"); }
        }

        private void ledAXINC_Click(object sender, EventArgs e)
        {
            if (AX_INC) { bot.EvaluateScript(currAxis.ToString() + " -ax-inc"); } else { bot.EvaluateScript(currAxis.ToString() + " +ax-inc"); }
        }

        private void ledAXBKRL_Click(object sender, EventArgs e)
        {
            if (AX_BKRL) { bot.EvaluateScript(currAxis.ToString() + " -ax-bkrl"); } else { bot.EvaluateScript(currAxis.ToString() + " +ax-bkrl"); }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using BotnanaLib;

namespace AIO
{
    public partial class Form1 : Form
    {
        private Botnana bot;

        // 定義台達 AIO 模組的站號
        private const string DeltaAinSlavePosStr = "4";
        private const string DeltaAoutSlavePosStr = "5";
        private int maxSlavePos = Math.Max(int.Parse(DeltaAinSlavePosStr), int.Parse(DeltaAoutSlavePosStr));

        // 台達 AIO 模組，每一個管道都需要 Enable 才會電壓輸出或是電壓量測
        //
        // 台達 AO 模組，每一個管道可以選擇輸出的訊號模式，模式設定是透過 SDO 指令設定
        // CH 1 Range Mode Selection object 0x2000:0x1, type: u16
        // CH 2 Range Mode Selection object 0x2000:0x2, type: u16
        // CH 3 Range Mode Selection object 0x2000:0x3, type: u16
        // CH 4 Range Mode Selection object 0x2000:0x4, type: u16
        // 設定值的意義如下 :
        //     0 : 0 ~ 5V (數值轉換: 0 -> 0V, 65535 -> 5V)
        //     1 : 0 ~ 10V (數值轉換: 0 -> 0V, 65535 -> 10V)
        //     2 : -5 ~ 5V (數值轉換: 0 -> -5V, 65535 -> 5V)
        //     3 : -10 ~ 10V (數值轉換: 0 -> -10V, 65535 -> 10V)
        //     4 : 4 ~ 20 mA (此範例沒有實做)
        //     5 : 0 ~ 20 mA (此範例沒有實做)
        //     6 : 0 ~ 24 mA (此範例沒有實做) 
        // Mode 設定後，該管道必須重新 Enable 才會輸出正確的電壓值。
        //
        // 台達 AI 模組，則是用一個暫存器設定所有管道的量測範圍，也是透過 SDO 指令設定
        // Range Mode Selection object 0x2000:0x0, type: u16
        // 設定值的意義如下 : (EC8124S0 與 EC8124D0 會不太一樣)
        //     0 : -5 ~ 5V    (數值轉換: 0-> 0V, 32767 -> 5V, -32768 -> -5V)
        //     1 : -10 ~ 10V  (數值轉換: 0-> 0V, 32767 -> 10V, -32768 -> -10V)

        private int wsState = 0;
        private HandleMessage onWSError;
        public void OnWSErrorCallback(IntPtr dataPtr, string data)
        {
            wsState = 0;
        }

        private HandleMessage onWSOpen;
        public void OnWSOpenCallback(IntPtr dataPtr, string data)
        {
            wsState = 2;
        }

        private int slavesCount = 0;
        private HandleMessage onSlavesResponding;
        private void OnSlavesRespondingCallback(IntPtr dataPtr, string str)
        {
            slavesCount = int.Parse(str);
        }

        private int slavesState = 0;
        private HandleMessage onSlavesState;
        private void OnSlavesStateCallback(IntPtr dataPtr, string str)
        {
            slavesState = int.Parse(str);
        }

        private HandleMessage onErrorMessage;
        private void OnErrorMessageCallback(IntPtr dataPtr, string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("Error|" + str)).Start();
        }

        private Int16[] ains = new Int16[5];
        private int[] ainsEnabled = new int[5];
        private int[] aouts = new int[5];
        private int[] aoutsEnabled = new int[5];
        private int[] aoutsMode = new int[5];
        private int ainMode = 0;

        private HandleMessage[] onAins = new HandleMessage[5];
        private HandleMessage[] onAouts = new HandleMessage[5];
        private HandleMessage[] onAinsEnabled = new HandleMessage[5];
        private HandleMessage[] onAoutsEnabled = new HandleMessage[5];
        private HandleMessage[] onAoutsMode = new HandleMessage[5];
        private HandleMessage onAinMode;

        private void OnAin1Callback(IntPtr dataPtr, string str)
        {
            // 台達的文件定義 AI 輸入值的形態為 UInt16
            // 但是在換算電壓值時使用 Int16 時比較方便，所以會有以下型別轉換。
            ains[1] = (Int16)UInt16.Parse(str);
        }
        private void OnAin2Callback(IntPtr dataPtr, string str)
        {
            ains[2] = (Int16)UInt16.Parse(str);
        }
        private void OnAin3Callback(IntPtr dataPtr, string str)
        {
            ains[3] = (Int16)UInt16.Parse(str);
        }
        private void OnAin4Callback(IntPtr dataPtr, string str)
        {
            ains[4] = (Int16)UInt16.Parse(str);
        }
        private void OnAout1Callback(IntPtr dataPtr, string str)
        {
            aouts[1] = int.Parse(str);
        }
        private void OnAout2Callback(IntPtr dataPtr, string str)
        {
            aouts[2] = int.Parse(str);
        }
        private void OnAout3Callback(IntPtr dataPtr, string str)
        {
            aouts[3] = int.Parse(str);
        }
        private void OnAout4Callback(IntPtr dataPtr, string str)
        {
            aouts[4] = int.Parse(str);
        }

        private void OnAin1EnabledCallback(IntPtr dataPtr, string str)
        {
            ainsEnabled[1] = int.Parse(str);
        }

        private void OnAin2EnabledCallback(IntPtr dataPtr, string str)
        {
            ainsEnabled[2] = int.Parse(str);
        }

        private void OnAin3EnabledCallback(IntPtr dataPtr, string str)
        {
            ainsEnabled[3] = int.Parse(str);
        }

        private void OnAin4EnabledCallback(IntPtr dataPtr, string str)
        {
            ainsEnabled[4] = int.Parse(str);
        }

        private void OnAout1EnabledCallback(IntPtr dataPtr, string str)
        {
            aoutsEnabled[1] = int.Parse(str);
        }
        private void OnAout2EnabledCallback(IntPtr dataPtr, string str)
        {
            aoutsEnabled[2] = int.Parse(str);
        }
        private void OnAout3EnabledCallback(IntPtr dataPtr, string str)
        {
            aoutsEnabled[3] = int.Parse(str);
        }
        private void OnAout4EnabledCallback(IntPtr dataPtr, string str)
        {
            aoutsEnabled[4] = int.Parse(str);
        }

        private bool hasAoutsMode = false;
        private void OnAout1ModeCallback(IntPtr dataPtr, string str)
        {
            int mode = int.Parse(str);
            if (mode >= 0 && mode <= 3)
            {
                aoutsMode[1] = mode;
            }
        }
        private void OnAout2ModeCallback(IntPtr dataPtr, string str)
        {
            int mode = int.Parse(str);
            if (mode >= 0 && mode <= 3)
            {
                aoutsMode[2] = mode;
            }
        }
        private void OnAout3ModeCallback(IntPtr dataPtr, string str)
        {
            int mode = int.Parse(str);
            if (mode >= 0 && mode <= 3)
            {
                aoutsMode[3] = mode;
            }
        }
        private void OnAout4ModeCallback(IntPtr dataPtr, string str)
        {
            int mode = int.Parse(str);
            if (mode >= 0 && mode <= 3)
            {
                aoutsMode[4] = mode;
                hasAoutsMode = true;
            }
        }

        private bool hasAinMode = false;
        private void OnAinModeCallback(IntPtr dataPtr, string str)
        {
            int mode = int.Parse(str);
            if (mode >= 0 && mode <= 1)
            {
                ainMode = mode;
                hasAinMode = true;
            }
        }

        private HandleMessage onUserParameter;
        private void OnUserParameterCallback(IntPtr dataPtr, string str)
        {
            int para = Int32.Parse(str);
            switch (para)
            {
                case 0:
                    // 設定 user parameter 為 0x10
                    // 如果此範例重新執行不會在載入以下 SFC
                    //EvaluateScript("$10 user-para!");
                    // 清除SFC 邏輯，載入 SFC 時會造成 real time cycle overrun，所以要暫時 ignore-overrun
                    // 載入後再執行 `reset-overrun`
                    bot.EvaluateScript("0sfc ignore-overrun");
                    bot.EvaluateScript("-work marker -work");
                    bot.LoadSFC(@"SDOUploadList.sfc");
                    // 等待 SFC 設置完成
                    Thread.Sleep(100);
                    // 設定 SDOUploadList.sfc 中的 AIN 與 AOUT 模組的站號
                    bot.EvaluateScript(DeltaAinSlavePosStr + " ain-slave-pos! " + DeltaAoutSlavePosStr + " aout-slave-pos! ");
                    // send-param-request 是要求 AIN, AOUT 模組送回目前模式設定
                    // 回傳的資料範例如下:
                    // aout1_mode|3
                    // aout2_mode|1
                    // aout3_mode|0
                    // aout4_mode|0
                    // ain_mode|1
                    bot.EvaluateScript("reset-overrun send-param-request");
                    break;
                default:
                    break;
            }

        }


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Process thisProc = Process.GetCurrentProcess();
            thisProc.PriorityClass = ProcessPriorityClass.RealTime;

            bot = new Botnana("192.168.7.2");

            // WS 連線錯誤就呼叫 OnWSErrorCallback
            onWSError = new HandleMessage(OnWSErrorCallback);
            bot.SetOnErrorCB(IntPtr.Zero, onWSError);

            // WS 連線成功就呼叫 OnWSOpenCallback
            onWSOpen = new HandleMessage(OnWSOpenCallback);
            bot.SetOnOpenCB(IntPtr.Zero, onWSOpen);

            // 收到 tag = slaves_responding 就呼叫 OnSlavesRespondingCallback
            onSlavesResponding = new HandleMessage(OnSlavesRespondingCallback);
            bot.SetTagCB(@"slaves_responding", 0, IntPtr.Zero, onSlavesResponding);

            // 收到 tag = al_states 就呼叫 OnSlavesStateCallback
            onSlavesState = new HandleMessage(OnSlavesStateCallback);
            bot.SetTagCB(@"al_states", 0, IntPtr.Zero, onSlavesState);

            // 收到 tag = error 就呼叫 OnErrorMessageCallback
            onErrorMessage = new HandleMessage(OnErrorMessageCallback);
            bot.SetTagCB(@"error", 0, IntPtr.Zero, onErrorMessage);

            // 收到 tag = ain.1.4 就呼叫 OnAin1Callback
            // ain.1.4 表示第 4 個從站第 1 管道的 AIN
            onAins[1] = new HandleMessage(OnAin1Callback);
            bot.SetTagCB(@"ain.1." + DeltaAinSlavePosStr, 0, IntPtr.Zero, onAins[1]);
            onAins[2] = new HandleMessage(OnAin2Callback);
            bot.SetTagCB(@"ain.2." + DeltaAinSlavePosStr, 0, IntPtr.Zero, onAins[2]);
            onAins[3] = new HandleMessage(OnAin3Callback);
            bot.SetTagCB(@"ain.3." + DeltaAinSlavePosStr, 0, IntPtr.Zero, onAins[3]);
            onAins[4] = new HandleMessage(OnAin4Callback);
            bot.SetTagCB(@"ain.4." + DeltaAinSlavePosStr, 0, IntPtr.Zero, onAins[4]);

            // 收到 tag = aout.1.4 就呼叫 OnAout1Callback
            // aout.1.5 表示第 5 個從站第 1 管道的 Aout
            onAouts[1] = new HandleMessage(OnAout1Callback);
            bot.SetTagCB(@"aout.1." + DeltaAoutSlavePosStr, 0, IntPtr.Zero, onAouts[1]);
            onAouts[2] = new HandleMessage(OnAout2Callback);
            bot.SetTagCB(@"aout.2." + DeltaAoutSlavePosStr, 0, IntPtr.Zero, onAouts[2]);
            onAouts[3] = new HandleMessage(OnAout3Callback);
            bot.SetTagCB(@"aout.3." + DeltaAoutSlavePosStr, 0, IntPtr.Zero, onAouts[3]);
            onAouts[4] = new HandleMessage(OnAout4Callback);
            bot.SetTagCB(@"aout.4." + DeltaAoutSlavePosStr, 0, IntPtr.Zero, onAouts[4]);

            // 收到 tag = ain_enabled.1.4 就呼叫 OnAin1EnabledCallback
            // ain_enabled.1.4 表示第 4 個從站第 1 管道的 AIN Enabled
            onAinsEnabled[1] = new HandleMessage(OnAin1EnabledCallback);
            bot.SetTagCB(@"ain_enabled.1." + DeltaAinSlavePosStr, 0, IntPtr.Zero, onAinsEnabled[1]);
            onAinsEnabled[2] = new HandleMessage(OnAin2EnabledCallback);
            bot.SetTagCB(@"ain_enabled.2." + DeltaAinSlavePosStr, 0, IntPtr.Zero, onAinsEnabled[2]);
            onAinsEnabled[3] = new HandleMessage(OnAin3EnabledCallback);
            bot.SetTagCB(@"ain_enabled.3." + DeltaAinSlavePosStr, 0, IntPtr.Zero, onAinsEnabled[3]);
            onAinsEnabled[4] = new HandleMessage(OnAin4EnabledCallback);
            bot.SetTagCB(@"ain_enabled.4." + DeltaAinSlavePosStr, 0, IntPtr.Zero, onAinsEnabled[4]);

            // 收到 tag = aout_enabled.1.5 就呼叫 OnAout1EnabledCallback
            // aout_enabled.1.5 表示第 5 個從站第 1 管道的 AOUT Enabled
            onAoutsEnabled[1] = new HandleMessage(OnAout1EnabledCallback);
            bot.SetTagCB(@"aout_enabled.1." + DeltaAoutSlavePosStr, 0, IntPtr.Zero, onAoutsEnabled[1]);
            onAoutsEnabled[2] = new HandleMessage(OnAout2EnabledCallback);
            bot.SetTagCB(@"aout_enabled.2." + DeltaAoutSlavePosStr, 0, IntPtr.Zero, onAoutsEnabled[2]);
            onAoutsEnabled[3] = new HandleMessage(OnAout3EnabledCallback);
            bot.SetTagCB(@"aout_enabled.3." + DeltaAoutSlavePosStr, 0, IntPtr.Zero, onAoutsEnabled[3]);
            onAoutsEnabled[4] = new HandleMessage(OnAout4EnabledCallback);
            bot.SetTagCB(@"aout_enabled.4." + DeltaAoutSlavePosStr, 0, IntPtr.Zero, onAoutsEnabled[4]);

            // 收到 tag = aout1_mode 就呼叫 OnAout1ModeCallback
            onAoutsMode[1] = new HandleMessage(OnAout1ModeCallback);
            bot.SetTagCB(@"aout1_mode", 0, IntPtr.Zero, onAoutsMode[1]);
            onAoutsMode[2] = new HandleMessage(OnAout2ModeCallback);
            bot.SetTagCB(@"aout2_mode", 0, IntPtr.Zero, onAoutsMode[2]);
            onAoutsMode[3] = new HandleMessage(OnAout3ModeCallback);
            bot.SetTagCB(@"aout3_mode", 0, IntPtr.Zero, onAoutsMode[3]);
            onAoutsMode[4] = new HandleMessage(OnAout4ModeCallback);
            bot.SetTagCB(@"aout4_mode", 0, IntPtr.Zero, onAoutsMode[4]);
            onAinMode = new HandleMessage(OnAinModeCallback);
            bot.SetTagCB(@"ain_mode", 0, IntPtr.Zero, onAinMode);

            // 收到 tag = user_parameter 就呼叫 OnUserParameterCallback
            onUserParameter = new HandleMessage(OnUserParameterCallback);
            bot.SetTagCB(@"user_parameter", 0, IntPtr.Zero, onUserParameter);

            // 進行連線
            wsState = 1;
            bot.Connect();

            timer1.Interval = 50;
            timer1.Enabled = true;
            timer2.Interval = 1000;
            timer2.Enabled = true;

            radioAin1Enabled.AutoCheck = false;
            radioAin2Enabled.AutoCheck = false;
            radioAin3Enabled.AutoCheck = false;
            radioAin4Enabled.AutoCheck = false;
            radioAout1Enabled.AutoCheck = false;
            radioAout2Enabled.AutoCheck = false;
            radioAout3Enabled.AutoCheck = false;
            radioAout4Enabled.AutoCheck = false;
            comboAout1Mode.SelectedIndex = 0;
            comboAout2Mode.SelectedIndex = 0;
            comboAout3Mode.SelectedIndex = 0;
            comboAout4Mode.SelectedIndex = 0;
            comboAinMode.SelectedIndex = 0;
            groupAinStatus.Text = "AIN Status (Slave " + DeltaAinSlavePosStr + ")";
            groupAoutStatus.Text = "AOUT Status (Slave " + DeltaAoutSlavePosStr + ")";
        }

        private bool slavesHasUpdated = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (slavesCount >= maxSlavePos && slavesState == 8)
            {
                if (slavesHasUpdated)
                {
                    // 此命令 "4 5 .slave-diff .slave-diff" 會只回傳與上次回傳不同的資料
                    // 資訊回傳範例
                    // ain.3.4|65535|ain.4.4|65535
                    // aout.1.5|42598
                    bot.EvaluateScript(DeltaAinSlavePosStr + " " + DeltaAoutSlavePosStr + " .slave-diff .slave-diff");
                }
                else
                {
                    // ".user-para" 傳回 user_parameter|0
                    //
                    // "4 5 .slave .slave" 會只回傳 slave 的資訊
                    // 資訊回傳範例
                    // vendor.4|0x00001A05|product.4|0x00008124|description.4| R1-EC8124 4-ch 16-bit Single-ended/10kHz/Voltage or Current Mod
                    // |ec_alias.4|0|slave_state.4|0x23|device_type.4|0x00040191|ain_enabled.1.4|1|ain_enabled.2.4|1|ain_enabled.3.4|1
                    // |ain_enabled.4.4|1|ain.1.4|32767|ain.2.4|2|ain.3.4|0|ain.4.4|0
                    // vendor.5|0x00001A05|product.5|0x00009144|description.5|R1-EC9144 4-ch 16-bit Sigle-ended/Voltage or Current Mode D/ A
                    // |ec_alias.5|0|slave_state.5|0x23|device_type.5|0x00080191|aout_enabled.1.5|1|aout_enabled.2.5|1|aout_enabled.3.5|1
                    // |aout_enabled.4.5|1|aout.1.5|65535|aout.2.5|0|aout.3.5|0|aout.4.5|0
                    bot.EvaluateScript(DeltaAinSlavePosStr + " " + DeltaAoutSlavePosStr + " .slave .slave .user-para");
                    slavesHasUpdated = true;
                }
            }
            textAin1Raw.Text = ains[1].ToString();
            textAin1.Text = AinConvert(ains[1]);
            textAin2Raw.Text = ains[2].ToString();
            textAin2.Text = AinConvert(ains[2]);
            textAin3Raw.Text = ains[3].ToString();
            textAin3.Text = AinConvert(ains[3]);
            textAin4Raw.Text = ains[4].ToString();
            textAin4.Text = AinConvert(ains[4]);
            textAout1Raw.Text = aouts[1].ToString();
            textAout2Raw.Text = aouts[2].ToString();
            textAout3Raw.Text = aouts[3].ToString();
            textAout4Raw.Text = aouts[4].ToString();
            radioAin1Enabled.Checked = ainsEnabled[1] != 0;
            radioAin2Enabled.Checked = ainsEnabled[2] != 0;
            radioAin3Enabled.Checked = ainsEnabled[3] != 0;
            radioAin4Enabled.Checked = ainsEnabled[4] != 0;
            radioAout1Enabled.Checked = aoutsEnabled[1] != 0;
            radioAout2Enabled.Checked = aoutsEnabled[2] != 0;
            radioAout3Enabled.Checked = aoutsEnabled[3] != 0;
            radioAout4Enabled.Checked = aoutsEnabled[4] != 0;

            if (hasAoutsMode)
            {
                comboAout1Mode.SelectedIndex = aoutsMode[1];
                comboAout2Mode.SelectedIndex = aoutsMode[2];
                comboAout3Mode.SelectedIndex = aoutsMode[3];
                comboAout4Mode.SelectedIndex = aoutsMode[4];
                textAout1.Text = AoutConvert(1, aouts[1]);
                textAout2.Text = AoutConvert(2, aouts[2]);
                textAout3.Text = AoutConvert(3, aouts[3]);
                textAout4.Text = AoutConvert(4, aouts[4]);
                hasAoutsMode = false;
            }

            if (hasAinMode)
            {
                comboAinMode.SelectedIndex = ainMode;
                hasAinMode = false;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            // ".ec-links" 此命令會只回傳 EtherCAT 的連線狀態
            // slaves_responding|5|al_states|8|link_up|1|input_wc|2|output_wc|2|input_wc_state|1
            // |output_wc_state|1|input_wc_error|179|output_wc_error|213|waiting_sdos_len|0
            // 
            // slaves_responding 表示有多少從站連線
            // al_states: 8 表示所有從站都是在 Operation 狀態 
            bot.EvaluateScript(".ec-links");
            textSlavesCount.Text = slavesCount.ToString();
            textSlavesState.Text = slavesState.ToString();

            if (wsState == 2)
            {
                buttonWS.BackColor = Color.SpringGreen;
            }
            else if (wsState == 1)
            {
                buttonWS.BackColor = Color.Gold;
            }
            else
            {
                buttonWS.BackColor = Color.IndianRed;
            }
        }

        // Enable/Disable Ain Channel
        private void radioAin1Enabled_Click(object sender, EventArgs e)
        {
            if (radioAin1Enabled.Checked)
            {
                bot.EvaluateScript("1 " + DeltaAinSlavePosStr + " -ec-ain");
            }
            else
            {
                bot.EvaluateScript("1 " + DeltaAinSlavePosStr + " +ec-ain");
            }
        }
        private void radioAin2Enabled_Click(object sender, EventArgs e)
        {
            if (radioAin2Enabled.Checked)
            {
                bot.EvaluateScript("2 " + DeltaAinSlavePosStr + " -ec-ain");
            }
            else
            {
                bot.EvaluateScript("2 " + DeltaAinSlavePosStr + " +ec-ain");
            }
        }
        private void radioAin3Enabled_Click(object sender, EventArgs e)
        {
            if (radioAin3Enabled.Checked)
            {
                bot.EvaluateScript("3 " + DeltaAinSlavePosStr + " -ec-ain");
            }
            else
            {
                bot.EvaluateScript("3 " + DeltaAinSlavePosStr + " +ec-ain");
            }
        }
        private void radioAin4Enabled_Click(object sender, EventArgs e)
        {
            if (radioAin4Enabled.Checked)
            {
                bot.EvaluateScript("4 " + DeltaAinSlavePosStr + " -ec-ain");
            }
            else
            {
                bot.EvaluateScript("4 " + DeltaAinSlavePosStr + " +ec-ain");
            }
        }

        // Disable/Enable Aout Channel
        private void radioAout1Enabled_Click(object sender, EventArgs e)
        {
            if (radioAout1Enabled.Checked)
            {
                bot.EvaluateScript("1 " + DeltaAoutSlavePosStr + " -ec-aout");
            }
            else
            {
                bot.EvaluateScript("1 " + DeltaAoutSlavePosStr + " +ec-aout");
            }
        }
        private void radioAout2Enabled_Click(object sender, EventArgs e)
        {
            if (radioAout2Enabled.Checked)
            {
                bot.EvaluateScript("2 " + DeltaAoutSlavePosStr + " -ec-aout");
            }
            else
            {
                bot.EvaluateScript("2 " + DeltaAoutSlavePosStr + " +ec-aout");
            }
        }
        private void radioAout3Enabled_Click(object sender, EventArgs e)
        {
            if (radioAout3Enabled.Checked)
            {
                bot.EvaluateScript("3 " + DeltaAoutSlavePosStr + " -ec-aout");
            }
            else
            {
                bot.EvaluateScript("3 " + DeltaAoutSlavePosStr + " +ec-aout");
            }
        }
        private void radioAout4Enabled_Click(object sender, EventArgs e)
        {
            if (radioAout4Enabled.Checked)
            {
                bot.EvaluateScript("4 " + DeltaAoutSlavePosStr + " -ec-aout");
            }
            else
            {
                bot.EvaluateScript("4 " + DeltaAoutSlavePosStr + " +ec-aout");
            }
        }

        // AIN 電壓值轉換
        private float[] ainsFullScale = new float[2] { 10.0f, 20.0f };
        private String AinConvert(int raw)
        {
            int index = comboAinMode.SelectedIndex;
            float value = (raw / 65535.0f) * ainsFullScale[index];
            return value.ToString("N2");
        }

        // AOUT 電壓值轉換
        private float[] aoutsFullScale = new float[4] { 5.0f, 10.0f, 10.0f, 20.0f };
        private float[] aoutsOffset = new float[4] { 0f, 0f, -5.0f, -10.0f };
        private int AoutModeSelected(uint ch)
        {
            int index = 0;
            if (ch == 1)
            {
                index = comboAout1Mode.SelectedIndex;
            }
            else if (ch == 2)
            {
                index = comboAout2Mode.SelectedIndex;
            }
            else if (ch == 3)
            {
                index = comboAout3Mode.SelectedIndex;
            }
            else if (ch == 4)
            {
                index = comboAout4Mode.SelectedIndex;
            }
            return index;
        }
        private String AoutConvert(uint ch, int raw)
        {
            int index = AoutModeSelected(ch);
            float value = (raw / 65535.0f * aoutsFullScale[index]) + aoutsOffset[index];
            return value.ToString("N2");
        }

        // 設定 AOUT 輸出
        private void set_aout(uint ch, String data)
        {
            float value;
            int index = AoutModeSelected(ch);

            if (float.TryParse(data, out value))
            {
                if (value >= aoutsFullScale[index] + aoutsOffset[index])
                {
                    value = aoutsFullScale[index] + aoutsOffset[index];
                }
                else if (value <= aoutsOffset[index])
                {
                    value = aoutsOffset[index];
                }

                UInt16 output = Convert.ToUInt16(65535.0 / aoutsFullScale[index] * (value - aoutsOffset[index]));
                // 由電壓值算出 U16 的輸出值，設定輸出值
                bot.EvaluateScript(output.ToString() + " " + ch.ToString() + " " + DeltaAoutSlavePosStr + " ec-aout!");
            }
        }
        private void textAout1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                set_aout(1, textAout1.Text);
            }
        }
        private void textAout1_Leave(object sender, EventArgs e)
        {
            set_aout(1, textAout1.Text);
        }
        private void textAout2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                set_aout(2, textAout2.Text);
            }
        }
        private void textAout2_Leave(object sender, EventArgs e)
        {
            set_aout(2, textAout2.Text);
        }
        private void textAout3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                set_aout(3, textAout3.Text);
            }
        }
        private void textAout3_Leave(object sender, EventArgs e)
        {
            set_aout(3, textAout3.Text);
        }
        private void textAout4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                set_aout(4, textAout4.Text);
            }
        }
        private void textAout4_Leave(object sender, EventArgs e)
        {
            set_aout(4, textAout4.Text);
        }

        // 切換 AIN 模式
        private void comboAinMode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            bot.EvaluateScript(comboAinMode.SelectedIndex.ToString() + " 0 $2000 " + DeltaAinSlavePosStr + " sdo-download-u16");
            ainMode = comboAinMode.SelectedIndex;
            hasAinMode = true;
        }

        // 切換 AOUT 模式
        private void comboAout1Mode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            bot.EvaluateScript(comboAout1Mode.SelectedIndex.ToString() + " 1 $2000 " + DeltaAoutSlavePosStr + " sdo-download-u16  1 " + DeltaAoutSlavePosStr + " -ec-aout");
            aoutsMode[1] = comboAout1Mode.SelectedIndex;
            hasAoutsMode = true;
        }
        private void comboAout2Mode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            bot.EvaluateScript(comboAout2Mode.SelectedIndex.ToString() + " 2 $2000 " + DeltaAoutSlavePosStr + " sdo-download-u16 2 " + DeltaAoutSlavePosStr + " -ec-aout");
            aoutsMode[2] = comboAout2Mode.SelectedIndex;
            hasAoutsMode = true;
        }
        private void comboAout3Mode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            bot.EvaluateScript(comboAout3Mode.SelectedIndex.ToString() + " 3 $2000 " + DeltaAoutSlavePosStr + " sdo-download-u16 3 " + DeltaAoutSlavePosStr + " -ec-aout");
            aoutsMode[3] = comboAout3Mode.SelectedIndex;
            hasAoutsMode = true;
        }
        private void comboAout4Mode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            bot.EvaluateScript(comboAout4Mode.SelectedIndex.ToString() + " 4 $2000 " + DeltaAoutSlavePosStr + " sdo-download-u16 4 " + DeltaAoutSlavePosStr + " -ec-aout");
            aoutsMode[4] = comboAout4Mode.SelectedIndex;
            hasAoutsMode = true;
        }

        // WS 連線控制
        private void buttonWS_Click(object sender, EventArgs e)
        {
            if (wsState == 2)
            {
                bot.Disconnect();
            }
            else if (wsState == 0)
            {
                bot.Connect();
            }
            wsState = 1;

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using BotnanaLib;

namespace PositionComparsionPanaA6B
{
    public partial class Form1 : Form
    {
        private Botnana bot;

        private HandleMessage onWSError;
        public void OnWSErrorCallback(IntPtr dataPtr, string data)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("WS error : " + data)).Start();
        }

        private int messageCount = 0;
        private HandleMessage onMessage;
        public void OnMessageCallback(IntPtr dataPtr, string data)
        {
            //MessageBox.Show("On message : " + data);
            messageCount++;
            if (messageCount > 256)
            {
                messageCount = 0;
            }
        }


        // 取得 userParameter
        private bool has_updated = false;
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

                    bot.LoadSFC(@"..\..\positionComparsion.sfc");
                    // 等待 SFC 設置完成
                    has_updated = true;
                    Thread.Sleep(10);
                    bot.EvaluateScript("reset-overrun");
                    break;
                default:
                    break;
            }

        }


        // 收到 error 的處置
        private HandleMessage onError;
        private void OnErrorCallback(IntPtr dataPtr, string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("error|" + str)).Start();
        }

        private HandleMessage onPr3411;
        private void OnPr3411Callback(IntPtr dataPtr, string str)
        {
            UInt32 number;
            if (UInt32.TryParse(str, out number))
            {
                textPr3411.Text = number.ToString("X8");
            }
            else
            {
                textPr3411.Text = str;
            }
        }

        private HandleMessage onPr3444;
        private void OnPr3444Callback(IntPtr dataPtr, string str)
        {
            textPr3444.Text = str;
        }

        private HandleMessage onPr3445;
        private void OnPr3445Callback(IntPtr dataPtr, string str)
        {
            textPr3445.Text = str;
        }

        private HandleMessage onPr3447;
        private void OnPr3447Callback(IntPtr dataPtr, string str)
        {
            textPr3447.Text = str;
        }

        private HandleMessage onPr3448;
        private void OnPr3448Callback(IntPtr dataPtr, string str)
        {
            textPr3448.Text = str;
        }

        private HandleMessage onPr3449;
        private void OnPr3449Callback(IntPtr dataPtr, string str)
        {
            textPr3449.Text = str;
        }

        private HandleMessage onPr3450;
        private void OnPr3450Callback(IntPtr dataPtr, string str)
        {
            textPr3450.Text = str;
        }

        private HandleMessage onPr3451;
        private void OnPr3451Callback(IntPtr dataPtr, string str)
        {
            textPr3451.Text = str;
        }

        private HandleMessage onPr3452;
        private void OnPr3452Callback(IntPtr dataPtr, string str)
        {
            textPr3452.Text = str;
        }

        private HandleMessage onPr3453;
        private void OnPr3453Callback(IntPtr dataPtr, string str)
        {
            textPr3453.Text = str;
        }

        private HandleMessage onPr3454;
        private void OnPr3454Callback(IntPtr dataPtr, string str)
        {
            textPr3454.Text = str;
        }

        private HandleMessage onPr3455;
        private void OnPr3455Callback(IntPtr dataPtr, string str)
        {
            textPr3455.Text = str;
        }

        private HandleMessage onPr3456;
        private void OnPr3456Callback(IntPtr dataPtr, string str)
        {
            textPr3456.Text = str;
        }

        private HandleMessage onPr3457;
        private void OnPr3457Callback(IntPtr dataPtr, string str)
        {
            UInt32 number;
            if (UInt32.TryParse(str, out number))
            {
                textPr3457.Text = number.ToString("X8");
            }
            else
            {
                textPr3457.Text = str;
            }
        }

        private HandleMessage onRealPosition;
        private void OnRealPositionCallback(IntPtr dataPtr, string str)
        {
            textRealPosition.Text = str;
        }

        private HandleMessage onTargetPosition;
        private void OnTargetPositionCallback(IntPtr dataPtr, string str)
        {
            textTargetPosition.Text = str;
        }

        private HandleMessage onPDSState;
        private void OnPDSStateCallback(IntPtr dataPtr, string str)
        {
            textPDSState.Text = str;
        }

        private HandleMessage onOperationMode;
        private void OnOperationModeCallback(IntPtr dataPtr, string str)
        {
            textOperationMode.Text = str;
        }

        private HandleMessage onHomingMethod;
        private void OnHomingMethodCallback(IntPtr dataPtr, string str)
        {
            textHomingMethod.Text = str;
        }

        private HandleMessage onHomingSpeed2;
        private void OnHomingSpeed2Callback(IntPtr dataPtr, string str)
        {
            textHomingSpeed2.Text = str;
        }

        private HandleMessage onProfileVelocity;
        private void OnProfileVelocityCallback(IntPtr dataPtr, string str)
        {
            textProfileVelocity.Text = str;
        }

        private HandleMessage onProfileAcc;
        private void OnProfileAccCallback(IntPtr dataPtr, string str)
        {
            textProfileAcc.Text = str;
        }

        private int slavesResponding = 0;
        private HandleMessage onSlavesResponding;
        private void OnSlavesRespondingCallback(IntPtr dataPtr, string str)
        {
            slavesResponding = int.Parse(str);
            textSlavesResponding.Text = str;
        }

        private HandleMessage onWaitingSDOs;
        private void OnWaitingSDOsCallback(IntPtr dataPtr, string str)
        {
            textWaitingSDOs.Text = str;
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

            onWSError = new HandleMessage(OnWSErrorCallback);
            bot.SetOnErrorCB(IntPtr.Zero, onWSError);

            onMessage = new HandleMessage(OnMessageCallback);
            bot.SetOnMessageCB(IntPtr.Zero, onMessage);

            onUserParameter = new HandleMessage(OnUserParameterCallback);
            bot.SetTagCB(@"user_parameter", 0, IntPtr.Zero, onUserParameter);

            onError = new HandleMessage(OnErrorCallback);
            bot.SetTagCB(@"error", 0, IntPtr.Zero, onError);
            onPr3411 = new HandleMessage(OnPr3411Callback);
            bot.SetTagCB(@"pr3411 ", 0, IntPtr.Zero, onPr3411);
            onPr3444 = new HandleMessage(OnPr3444Callback);
            bot.SetTagCB(@"pr3444 ", 0, IntPtr.Zero, onPr3444);
            onPr3445 = new HandleMessage(OnPr3445Callback);
            bot.SetTagCB(@"pr3445 ", 0, IntPtr.Zero, onPr3445);
            onPr3447 = new HandleMessage(OnPr3447Callback);
            bot.SetTagCB(@"pr3447 ", 0, IntPtr.Zero, onPr3447);
            onPr3448 = new HandleMessage(OnPr3448Callback);
            bot.SetTagCB(@"pr3448 ", 0, IntPtr.Zero, onPr3448);
            onPr3449 = new HandleMessage(OnPr3449Callback);
            bot.SetTagCB(@"pr3449 ", 0, IntPtr.Zero, onPr3449);
            onPr3450 = new HandleMessage(OnPr3450Callback);
            bot.SetTagCB(@"pr3450 ", 0, IntPtr.Zero, onPr3450);
            onPr3451 = new HandleMessage(OnPr3451Callback);
            bot.SetTagCB(@"pr3451 ", 0, IntPtr.Zero, onPr3451);
            onPr3452 = new HandleMessage(OnPr3452Callback);
            bot.SetTagCB(@"pr3452 ", 0, IntPtr.Zero, onPr3452);
            onPr3453 = new HandleMessage(OnPr3453Callback);
            bot.SetTagCB(@"pr3453 ", 0, IntPtr.Zero, onPr3453);
            onPr3454 = new HandleMessage(OnPr3454Callback);
            bot.SetTagCB(@"pr3454 ", 0, IntPtr.Zero, onPr3454);
            onPr3455 = new HandleMessage(OnPr3455Callback);
            bot.SetTagCB(@"pr3455 ", 0, IntPtr.Zero, onPr3455);
            onPr3456 = new HandleMessage(OnPr3456Callback);
            bot.SetTagCB(@"pr3456 ", 0, IntPtr.Zero, onPr3456);
            onPr3457 = new HandleMessage(OnPr3457Callback);
            bot.SetTagCB(@"pr3457 ", 0, IntPtr.Zero, onPr3457);

            onSlavesResponding = new HandleMessage(OnSlavesRespondingCallback);
            bot.SetTagCB(@"slaves_responding", 0, IntPtr.Zero, onSlavesResponding);

            onWaitingSDOs = new HandleMessage(OnWaitingSDOsCallback);
            bot.SetTagCB(@"waiting_sdos_len", 0, IntPtr.Zero, onWaitingSDOs);

            onOperationMode = new HandleMessage(OnOperationModeCallback);
            bot.SetTagCB(@"operation_mode.1.1", 0, IntPtr.Zero, onOperationMode);

            onHomingMethod = new HandleMessage(OnHomingMethodCallback);
            bot.SetTagCB(@"homing_method.1.1", 0, IntPtr.Zero, onHomingMethod);

            onHomingSpeed2 = new HandleMessage(OnHomingSpeed2Callback);
            bot.SetTagCB(@"homing_speed_2.1.1", 0, IntPtr.Zero, onHomingSpeed2);

            onProfileVelocity = new HandleMessage(OnProfileVelocityCallback);
            bot.SetTagCB(@"profile_velocity.1.1", 0, IntPtr.Zero, onProfileVelocity);

            onProfileAcc = new HandleMessage(OnProfileAccCallback);
            bot.SetTagCB(@"profile_acceleration.1.1", 0, IntPtr.Zero, onProfileAcc);

            onRealPosition = new HandleMessage(OnRealPositionCallback);
            bot.SetTagCB(@"real_position.1.1", 0, IntPtr.Zero, onRealPosition);

            onTargetPosition = new HandleMessage(OnTargetPositionCallback);
            bot.SetTagCB(@"target_position.1.1", 0, IntPtr.Zero, onTargetPosition);

            onPDSState = new HandleMessage(OnPDSStateCallback);
            bot.SetTagCB(@"pds_state.1.1", 0, IntPtr.Zero, onPDSState);

            bot.Connect();

            timer1.Interval = 50;
            timer1.Enabled = true;
            timer2.Interval = 200;
            timer2.Enabled = true;
        }

        private bool has_slave_info = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            labMessageCount.Text = messageCount.ToString("X2");
            if (slavesResponding > 0)
            {
                if (has_slave_info)
                {
                    bot.EvaluateScript(@"1 .slave-diff");
                }
                else
                {
                    bot.EvaluateScript(@"1 .slave");
                }
            }

            if (!has_updated)
            {
                // 要求  Botnana Control 送出 user parameter 訊息
                bot.EvaluateScript(@".user-para");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"send-param-request");
        }

        private void btnServoOn_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"1 1 drive-on");
        }

        private void btnServoOFF_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"1 1 drive-off");
        }

        private void btnResetFault_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"1 1 reset-fault");
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"1 1 go");
        }

        private void btnHM_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"hm 1 1 op-mode!");
        }

        private void btnPP_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"pp 1 1 op-mode!");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            bot.EvaluateScript(@".ec-links");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(textTargetPositionCMD.Text + @" 1 1 target-p!");
            textTargetPositionCMD.Text = "";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(textHomingMethodCMD.Text + @" 1 1 homing-method!");
            textHomingMethodCMD.Text = "";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(textHomingSpeed2CMD.Text + @" 1 1 homing-v2!");
            textHomingSpeed2CMD.Text = "";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(textProfileVelocityCMD.Text + @" 1 1 profile-v!");
            textProfileVelocityCMD.Text = "";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(textProfileAccCMD.Text + @" dup 1 1 profile-a1! 1 1 profile-a2!");
            textProfileAccCMD.Text = "";
        }

        private string old_pr3411;
        private void textPr3411_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                old_pr3411 = textPr3411.Text;
                bot.EvaluateScript(@"$" + textPr3411.Text + @" 0 $3411 1 sdo-download-u32");
            }
        }

        private void textPr3411_Enter(object sender, EventArgs e)
        {
            old_pr3411 = textPr3411.Text;
        }

        private void textPr3411_Leave(object sender, EventArgs e)
        {
            textPr3411.Text = old_pr3411;
        }

        private string old_pr3444;
        private void textPr3444_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UInt16 number;
                if (UInt16.TryParse(textPr3444.Text, out number))
                {
                    if (number <= 32767)
                    {
                        old_pr3444 = textPr3444.Text;
                        bot.EvaluateScript(textPr3444.Text + @" 0 $3444 1 sdo-download-u16");
                    }
                    else
                    {
                        textPr3444.Text = old_pr3444;
                    }
                }
                else
                {
                    textPr3444.Text = old_pr3444;
                }
            }
        }

        private void textPr3444_Enter(object sender, EventArgs e)
        {
            old_pr3444 = textPr3444.Text;
        }

        private void textPr3444_Leave(object sender, EventArgs e)
        {
            textPr3444.Text = old_pr3444;
        }

        private string old_pr3445;
        private void textPr3445_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                byte number;
                if (byte.TryParse(textPr3445.Text, out number))
                {
                    if (number <= 7)
                    {
                        old_pr3445 = textPr3445.Text;
                        bot.EvaluateScript(textPr3445.Text + @" 0 $3445 1 sdo-download-u16");
                    }
                    else
                    {
                        textPr3445.Text = old_pr3445;
                    }
                }
                else
                {
                    textPr3445.Text = old_pr3445;
                }
            }
        }

        private void textPr3445_Enter(object sender, EventArgs e)
        {
            old_pr3445 = textPr3445.Text;
        }

        private void textPr3445_Leave(object sender, EventArgs e)
        {
            textPr3445.Text = old_pr3445;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"1 ec-save");
        }

        private string old_pr3447;
        private void textPr3447_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                byte number;
                if (byte.TryParse(textPr3447.Text, out number))
                {
                    if (number <= 1)
                    {
                        old_pr3447 = textPr3447.Text;
                        bot.EvaluateScript(textPr3447.Text + @" 0 $3447 1 sdo-download-u16");
                    }
                    else
                    {
                        textPr3447.Text = old_pr3447;
                    }
                }
                else
                {
                    textPr3447.Text = old_pr3447;
                }
            }
        }

        private void textPr3447_Enter(object sender, EventArgs e)
        {
            old_pr3447 = textPr3447.Text;
        }

        private void textPr3447_Leave(object sender, EventArgs e)
        {
            textPr3447.Text = old_pr3447;
        }

        private string old_pr3448;
        private void textPr3448_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Int32 number;
                if (Int32.TryParse(textPr3448.Text, out number))
                {
                    old_pr3448 = textPr3448.Text;
                    bot.EvaluateScript(textPr3448.Text + @" 0 $3448 1 sdo-download-i32");
                }
                else
                {
                    textPr3448.Text = old_pr3448;
                }
            }
        }

        private void textPr3448_Enter(object sender, EventArgs e)
        {
            old_pr3448 = textPr3448.Text;
        }

        private void textPr3448_Leave(object sender, EventArgs e)
        {
            textPr3448.Text = old_pr3448;
        }

        private string old_pr3449;
        private void textPr3449_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Int32 number;
                if (Int32.TryParse(textPr3449.Text, out number))
                {
                    old_pr3449 = textPr3449.Text;
                    bot.EvaluateScript(textPr3449.Text + @" 0 $3449 1 sdo-download-i32");
                }
                else
                {
                    textPr3449.Text = old_pr3449;
                }
            }
        }

        private void textPr3449_Enter(object sender, EventArgs e)
        {
            old_pr3449 = textPr3449.Text;
        }

        private void textPr3449_Leave(object sender, EventArgs e)
        {
            textPr3449.Text = old_pr3449;
        }

        private string old_pr3450;
        private void textPr3450_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Int32 number;
                if (Int32.TryParse(textPr3450.Text, out number))
                {
                    old_pr3450 = textPr3450.Text;
                    bot.EvaluateScript(textPr3450.Text + @" 0 $3450 1 sdo-download-i32");
                }
                else
                {
                    textPr3450.Text = old_pr3450;
                }
            }
        }

        private void textPr3450_Enter(object sender, EventArgs e)
        {
            old_pr3450 = textPr3450.Text;
        }

        private void textPr3450_Leave(object sender, EventArgs e)
        {
            textPr3450.Text = old_pr3450;
        }

        private string old_pr3451;
        private void textPr3451_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Int32 number;
                if (Int32.TryParse(textPr3451.Text, out number))
                {
                    old_pr3451 = textPr3451.Text;
                    bot.EvaluateScript(textPr3451.Text + @" 0 $3451 1 sdo-download-i32");
                }
                else
                {
                    textPr3451.Text = old_pr3451;
                }
            }
        }

        private void textPr3451_Enter(object sender, EventArgs e)
        {
            old_pr3451 = textPr3451.Text;
        }

        private void textPr3451_Leave(object sender, EventArgs e)
        {
            textPr3451.Text = old_pr3451;
        }

        private string old_pr3452;
        private void textPr3452_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Int32 number;
                if (Int32.TryParse(textPr3452.Text, out number))
                {
                    old_pr3452 = textPr3452.Text;
                    bot.EvaluateScript(textPr3452.Text + @" 0 $3452 1 sdo-download-i32");
                }
                else
                {
                    textPr3452.Text = old_pr3452;
                }
            }
        }

        private void textPr3452_Enter(object sender, EventArgs e)
        {
            old_pr3452 = textPr3452.Text;
        }

        private void textPr3452_Leave(object sender, EventArgs e)
        {
            textPr3452.Text = old_pr3452;
        }

        private string old_pr3453;
        private void textPr3453_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Int32 number;
                if (Int32.TryParse(textPr3453.Text, out number))
                {
                    old_pr3453 = textPr3453.Text;
                    bot.EvaluateScript(textPr3453.Text + @" 0 $3453 1 sdo-download-i32");
                }
                else
                {
                    textPr3453.Text = old_pr3453;
                }
            }
        }

        private void textPr3453_Enter(object sender, EventArgs e)
        {
            old_pr3453 = textPr3453.Text;
        }

        private void textPr3453_Leave(object sender, EventArgs e)
        {
            textPr3453.Text = old_pr3453;
        }

        private string old_pr3454;
        private void textPr3454_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Int32 number;
                if (Int32.TryParse(textPr3454.Text, out number))
                {
                    old_pr3454 = textPr3454.Text;
                    bot.EvaluateScript(textPr3454.Text + @" 0 $3454 1 sdo-download-i32");
                }
                else
                {
                    textPr3454.Text = old_pr3454;
                }
            }
        }

        private void textPr3454_Enter(object sender, EventArgs e)
        {
            old_pr3454 = textPr3454.Text;
        }

        private void textPr3454_Leave(object sender, EventArgs e)
        {
            textPr3454.Text = old_pr3454;
        }

        private string old_pr3455;
        private void textPr3455_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Int32 number;
                if (Int32.TryParse(textPr3455.Text, out number))
                {
                    old_pr3455 = textPr3455.Text;
                    bot.EvaluateScript(textPr3455.Text + @" 0 $3455 1 sdo-download-i32");
                }
                else
                {
                    textPr3455.Text = old_pr3455;
                }
            }
        }

        private void textPr3455_Enter(object sender, EventArgs e)
        {
            old_pr3455 = textPr3455.Text;
        }

        private void textPr3455_Leave(object sender, EventArgs e)
        {
            textPr3455.Text = old_pr3455;
        }

        private string old_pr3456;
        private void textPr3456_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Int16 number;
                if (Int16.TryParse(textPr3456.Text, out number))
                {
                    old_pr3456 = textPr3456.Text;
                    bot.EvaluateScript(textPr3456.Text + @" 0 $3456 1 sdo-download-i16");
                }
                else
                {
                    textPr3456.Text = old_pr3456;
                }
            }
        }

        private void textPr3456_Enter(object sender, EventArgs e)
        {
            old_pr3456 = textPr3456.Text;
        }

        private void textPr3456_Leave(object sender, EventArgs e)
        {
            textPr3456.Text = old_pr3456;
        }

        private string old_pr3457;
        private void textPr3457_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UInt32 number;
                if (UInt32.TryParse(textPr3457.Text, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out number))
                {
                    old_pr3457 = textPr3457.Text;
                    bot.EvaluateScript(@"$" + textPr3457.Text + @" 0 $3457 1 sdo-download-u32");
                }
                else
                {
                    textPr3457.Text = old_pr3457;
                }
            }
        }

        private void textPr3457_Enter(object sender, EventArgs e)
        {
            old_pr3457 = textPr3457.Text;
        }

        private void textPr3457_Leave(object sender, EventArgs e)
        {
            textPr3457.Text = old_pr3457;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(".user-para");
        }
    }
}

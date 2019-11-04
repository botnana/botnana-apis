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
using BotnanaLib;

namespace MultiApp
{
    public partial class FormApp : Form
    {
        private delegate void Appdeg();

        public static Botnana bot;

        private FormTorque formTorque;
        private FormFeeder formFeeder;
        private FormRCON formRCON;

        public static bool stopPolling = false;
        private static bool rebooting = false;
        private bool hasSFC = false;

        private static int webSocketState = 0;
        private int slavesCount = 0;
        private int errorsLen;

        private HandleMessage onWSOpen;
        private HandleMessage onWSError;
        private HandleMessage onMessage;
        private HandleMessage onErrorMessage;
        private HandleMessage onUserParameter;
        private HandleMessage onECReady;
        private HandleMessage onSlavesResponding;
        private HandleMessage onSystemReady;

        public static void BotEvaluateScript(string str)
        {
            if (webSocketState == 2 && !rebooting) bot.EvaluateScript(str);
        }

        public FormApp()
        {
            InitializeComponent();

            bot = new Botnana("192.168.7.2");

            // 啟動終端機，可幫助除錯，若不使用可不啟動。
            RunConsole();

            // On WebSocket open callback.
            onWSOpen = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                webSocketState = 2;
                rebooting = false;
                BeginInvoke(new Appdeg(() =>
                {
                    buttonWSState.Text = "WebSocket ready";
                    buttonWSState.BackColor = Color.FromArgb(0x53, 0xFF, 0x53);
                }));
                // 送出 .user-para 命令，讓為回傳的訊息去觸發 OnUserParameterCallback
                // 若重複執行從 32 開始，因為需要 .device-infos 取得周邊裝置的資訊
                // .user-para 回應的訊息範例如下:
                // user_parameter|0
                bot.EvaluateScript(@"user-para@ 32 min user-para! .user-para");
                Console.WriteLine("WebSocket connected.");
            });
            bot.SetOnOpenCB(IntPtr.Zero, onWSOpen);

            // On WebSocket error callback.
            onWSError = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                webSocketState = 0;
                slavesCount = 0;
                hasSFC = false;
                BeginInvoke(new Appdeg(() => {
                    buttonWSState.Text = "WebSocket not ready";
                    buttonWSState.BackColor = Color.FromArgb(0xFF, 0x2D, 0x2D);
                    buttonECState.Text = "EtherCAT not ready(" + slavesCount.ToString() + ")";
                    buttonECState.BackColor = Color.FromArgb(0xFF, 0x2D, 0x2D);
                    buttonSystemReady.Text = "System not ready";
                    buttonSystemReady.BackColor = Color.FromArgb(0xFF, 0x2D, 0x2D);
                }));
                formTorque.Reset();
                formFeeder.Reset();
                formRCON.Reset();
                Console.WriteLine("WS error : " + str);
            });
            bot.SetOnErrorCB(IntPtr.Zero, onWSError);

            // On Message callback.
            onMessage = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                Console.WriteLine("OnMessage : " + str);
            });
            bot.SetOnMessageCB(IntPtr.Zero, onMessage);

            // On Error tag callback
            onErrorMessage = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                if (errorsLen < 3)
                {
                    errorsLen += 1;
                    new Thread(() =>
                    {
                        System.Windows.Forms.MessageBox.Show("Error|" + str);
                        errorsLen -= 1;
                    }).Start();
                }
                Console.WriteLine("Error|" + str);
            });
            bot.SetTagCB(@"error", 0, IntPtr.Zero, onErrorMessage);

            // On user_parameter tag callback.
            onUserParameter = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                switch (Int32.Parse(str))
                {
                    case 0:
                        Console.WriteLine("OnUserParameterCallback 0");
                        if (webSocketState == 2 && !rebooting)
                        {
                            // 設定 user parameter 為 16，如果此範例重新執行不會再載入以下 SFC
                            bot.EvaluateScript("16 user-para!");
                            // 清除SFC 邏輯，載入 SFC 時會造成 real time cycle overrun，所以要暫時 ignore-overrun
                            bot.EvaluateScript(@"ignore-overrun 0sfc -work marker -work");
                            bot.LoadSFC(@"config.fs");
                            bot.LoadSFC(@"sdo.fs");
                            bot.LoadSFC(@"torque.fs");
                            bot.LoadSFC(@"feeder.fs");
                            bot.LoadSFC(@"rcon.fs");
                            bot.LoadSFC(@"manager.fs");
                            bot.EvaluateScript(@"marker -app .user-para");
                        }
                        break;
                    case 16:
                        Console.WriteLine("OnUserParameterCallback 16");
                        // 載入後執行 `reset-overrun`
                        BotEvaluateScript(@"reset-overrun 32 user-para! .user-para");
                        break;
                    case 32:
                        Console.WriteLine("OnUserParameterCallback 32");
                        formTorque.Initialize();
                        formFeeder.Initialize();
                        formRCON.Initialize();
                        BotEvaluateScript(@"+coordinator 64 user-para! .user-para");
                        break;
                    case 64:
                        Console.WriteLine("OnUserParameterCallback 64");
                        hasSFC = true;
                        break;
                    default:
                        break;
                }
            });
            bot.SetTagCB(@"user_parameter", 0, IntPtr.Zero, onUserParameter);

            // On ec_ready tag callback.
            onECReady = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                if (str == "1")
                {
                    BeginInvoke(new Appdeg(() => {
                        buttonECState.Text = "EtherCAT ready(" + slavesCount.ToString() + ")";
                        buttonECState.BackColor = Color.FromArgb(0x53, 0xFF, 0x53);
                    }));
                }
                else
                {
                    BeginInvoke(new Appdeg(() => {
                        buttonECState.Text = "EtherCAT not ready(" + slavesCount.ToString() + ")";
                        buttonECState.BackColor = Color.FromArgb(0xFF, 0x2D, 0x2D);
                    }));
                }
            });
            bot.SetTagCB(@"ec_ready", 0, IntPtr.Zero, onECReady);

            // On slaves_responding tag callback.
            onSlavesResponding = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                slavesCount = int.Parse(str);
            });
            bot.SetTagCB(@"slaves_responding", 0, IntPtr.Zero, onSlavesResponding);

            // On system_ready tag callback.
            onSystemReady = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                if (str == "-1")
                {
                    BeginInvoke(new Appdeg(() => {
                        buttonSystemReady.Text = "System ready";
                        buttonSystemReady.BackColor = Color.FromArgb(0x53, 0xFF, 0x53);
                    }));
                }
                else
                {
                    BeginInvoke(new Appdeg(() => {
                        buttonSystemReady.Text = "System not ready";
                        buttonSystemReady.BackColor = Color.FromArgb(0xFF, 0x2D, 0x2D);
                    }));
                }
            });
            bot.SetTagCB(@"system_ready", 0, IntPtr.Zero, onSystemReady);

            formTorque = new FormTorque();
            formFeeder = new FormFeeder();
            formRCON = new FormRCON();
        }

        private void FormApp_Load(object sender, EventArgs e)
        {
            tabPageTorque.Controls.Add(formTorque);
            tabPageFeeder.Controls.Add(formFeeder);
            tabPageRCON.Controls.Add(formRCON);
            formTorque.Awake();
            bot.Connect();
            timer1s.Enabled = true;
        }

        private void tabControlApp_SelectedIndexChanged(object sender, EventArgs e)
        {
            formTorque.Sleep();
            formFeeder.Sleep();
            formRCON.Sleep();
            switch ((sender as TabControl).SelectedIndex)
            {
                case 0:
                    formTorque.Awake();
                    tabPageTorque.Show();
                    break;
                case 1:
                    formFeeder.Awake();
                    tabPageFeeder.Show();
                    break;
                case 2:
                    formRCON.Awake();
                    tabPageRCON.Show();
                    break;
                default:
                    break;
            }
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        [System.Runtime.InteropServices.DllImport("Kernel32")]
        public static extern void FreeConsole();

        private void RunConsole()
        {
            AllocConsole();
            Console.Beep();
            ConsoleColor oriColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("* Don't close this console window or the application will also close.");
            Console.WriteLine();
            Console.ForegroundColor = oriColor;
        }

        private void timer1s_Tick(object sender, EventArgs e)
        {
            switch (webSocketState)
            {
                case 0:
                    bot.Connect();
                    buttonWSState.BackColor = Color.FromArgb(0xFF, 0xFF, 0x37);
                    webSocketState = 1;
                    Console.WriteLine("Trying to connect to Botnana.");
                    break;
                case 2:
                    if (!stopPolling)
                    {
                        string cmd = null;
                        cmd += @".ec-links ";
                        if (hasSFC) cmd += @".sfc-info ";
                        BotEvaluateScript(cmd);
                    }
                    break;
                default:
                    break;
            }
        }

        private void buttonReloadSFC_Click(object sender, EventArgs e)
        {
            BotEvaluateScript(@"0 user-para! .user-para");
            hasSFC = false;
            formTorque.Reset();
            formFeeder.Reset();
            formRCON.Reset();
        }

        private bool preventEvaluate = false;
        private void textBoxScript_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey) preventEvaluate = true;
        }

        private void textBoxScript_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                preventEvaluate = false;
            }
            else if (e.KeyCode == Keys.Enter && !preventEvaluate)
            {
                BotEvaluateScript((sender as TextBox).Text);
                (sender as TextBox).Text = "";
            }
        }

        private void buttonStopPolling_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Text == "Stop Polling")
            {
                stopPolling = true;
                BotEvaluateScript("false rt-info-output-enabled !");
                (sender as Button).Text = "Start Polling";
            }
            else
            {
                stopPolling = false;
                (sender as Button).Text = "Stop Polling";
            }
        }

        private void buttonEMS_Click(object sender, EventArgs e)
        {
            // ems-job   緊急停止軸組運動
            // ems-job   緊急停止軸組運動
            // kill-nc   移除 background task 內的工作
            bot.EvaluateScript(@"ems-job kill-nc");
            formTorque.EmergencyStop();
            formFeeder.EmergencyStop();
            formRCON.EmergencyStop();
        }
        
        private void buttonReboot_Click(object sender, EventArgs e)
        {
            rebooting = true;
            stopPolling = false;
            buttonStopPolling.Text = "Stop Polling";
            bot.Reboot();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BotnanaLib;
using BotnanaClassLib;

namespace BotnanaClassLibTest
{
    public partial class Test : Form
    {
        private Botnana bot;

        private HandleMessage onWSOpen;
        private HandleMessage onWSError;
        private HandleMessage onMessage;
        private HandleMessage onErrorMessage;
        private delegate void Deg();

        private int webSocketState = 0;

        public Test()
        {
            InitializeComponent();

            RunConsole();

            bot = new Botnana("192.168.7.2");

            onWSOpen = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                webSocketState = 2;
                BeginInvoke(new Deg(() =>
                {
                    sdoControl1.Awake();
                    driveControl1.Awake();
                    axisControl1.Awake();
                }));
                Console.WriteLine("WS connected.");
            });
            bot.SetOnOpenCB(IntPtr.Zero, onWSOpen);

            onWSError = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                webSocketState = 0;
                BeginInvoke(new Deg(() =>
                {
                    sdoControl1.Sleep();
                    driveControl1.Sleep();
                    axisControl1.Sleep();
                }));
                Console.WriteLine("WS error : " + str);
            });
            bot.SetOnErrorCB(IntPtr.Zero, onWSError);

            onMessage = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                Console.WriteLine("OnMessage : " + str);
            });
            bot.SetOnMessageCB(IntPtr.Zero, onMessage);

            onErrorMessage = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                Console.WriteLine("Error|" + str);
            });
            bot.SetTagCB(@"error", 0, IntPtr.Zero, onErrorMessage);

            sdoControl1.InitializeBotnana(bot);
            axisControl1.InitializeBotnana(bot);
            realTimeScriptControl1.InitializeBotnana(bot);
            driveControl1.InitializeBotnana(bot);
        }

        private void Test_Load(object sender, EventArgs e)
        {
            timer1.Interval = 100;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (webSocketState)
            {
                case 0:
                    bot.Connect();
                    webSocketState = 1;
                    Console.WriteLine("Trying to connect to Botnana.");
                    break;
                case 2:
                    // Do something after ready.
                    break;
                default:
                    break;
            }
        }

        private void buttonDisableCoordinator_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript("-coordinator");
        }

        private void buttonEnableCoordinator_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript("+coordinator");
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
    }
}

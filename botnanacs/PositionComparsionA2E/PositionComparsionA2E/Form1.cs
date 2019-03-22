using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using BotnanaLib;

namespace PositionComparsionA2E
{
    public partial class Form1 : Form
    {
        private Botnana bot;

        private HandleMessage onWSError;
        public void OnWSErrorCallback(string data)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("WS error : " + data)).Start();
        }

        private int messageCount = 0;
        private HandleMessage onMessage;
        public void OnMessageCallback(string data)
        {
            //MessageBox.Show("On message : " + data);
            messageCount++;
        }

        private HandleMessage onRealPosition;
        private void OnRealPositionCallback(string str)
        {
            textRealPosition.Text = str;
        }

        private HandleMessage onTargetPosition;
        private void OnTargetPositionCallback(string str)
        {
            textTargetPosition.Text = str;
        }

        private HandleMessage onPDSState;
        private void OnPDSStateCallback(string str)
        {
            textPDSState.Text = str;
        }

        private HandleMessage onOperationMode;
        private void OnOperationModeCallback(string str)
        {
            textOperationMode.Text = str;
        }

        private HandleMessage onHomingMethod;
        private void OnHomingMethodCallback(string str)
        {
            textHomingMethod.Text = str;
        }

        private HandleMessage onHomingSpeed2;
        private void OnHomingSpeed2Callback(string str)
        {
            textHomingSpeed2.Text = str;
        }

        private HandleMessage onProfileVelocity;
        private void OnProfileVelocityCallback(string str)
        {
            textProfileVelocity.Text = str;
        }

        private HandleMessage onProfileAcc;
        private void OnProfileAccCallback(string str)
        {
            textProfileAcc.Text = str;
        }



        private int slavesResponding = 0;
        private HandleMessage onSlavesResponding;
        private void OnSlavesRespondingCallback(string str)
        {
            slavesResponding = int.Parse(str);
            textSlavesResponding.Text = str;
        }

        private HandleMessage onWaitingSDOs;
        private void OnWaitingSDOsCallback(string str)
        {
            textWaitingSDOs.Text = str;
        }

        private HandleMessage onError;
        private void OnErrorCallback(string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("error|" + str)).Start(); ;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Process thisProc = Process.GetCurrentProcess();
            thisProc.PriorityClass = ProcessPriorityClass.RealTime;

            bot = new Botnana();
            onWSError = new HandleMessage(OnWSErrorCallback);
            bot.Connect("192.168.7.2", onWSError);
            onMessage = new HandleMessage(OnMessageCallback);
            bot.SetOnMessageCB(onMessage);

            onSlavesResponding = new HandleMessage(OnSlavesRespondingCallback);
            bot.SetTagCB($"slaves_responding", 0, onSlavesResponding);

            onWaitingSDOs = new HandleMessage(OnWaitingSDOsCallback);
            bot.SetTagCB($"waiting_sdos_len", 0, onWaitingSDOs);


            onOperationMode = new HandleMessage(OnOperationModeCallback);
            bot.SetTagCB($"operation_mode.1.1", 0, onOperationMode);

            onHomingMethod = new HandleMessage(OnHomingMethodCallback);
            bot.SetTagCB($"homing_method.1.1", 0, onHomingMethod);

            onHomingSpeed2 = new HandleMessage(OnHomingSpeed2Callback);
            bot.SetTagCB($"homing_speed_2.1.1", 0, onHomingSpeed2);

            onProfileVelocity = new HandleMessage(OnProfileVelocityCallback);
            bot.SetTagCB($"profile_velocity.1.1", 0, onProfileVelocity);

            onProfileAcc = new HandleMessage(OnProfileAccCallback);
            bot.SetTagCB($"profile_acceleration.1.1", 0, onProfileAcc);

            onRealPosition = new HandleMessage(OnRealPositionCallback);
            bot.SetTagCB($"real_position.1.1", 0, onRealPosition);

            onTargetPosition = new HandleMessage(OnTargetPositionCallback);
            bot.SetTagCB($"target_position.1.1", 0, onTargetPosition);

            onPDSState = new HandleMessage(OnPDSStateCallback);
            bot.SetTagCB($"pds_state.1.1", 0, onPDSState);

            onError = new HandleMessage(OnErrorCallback);
            bot.SetTagCB($"error", 0, onError);


            timer1.Interval = 50;
            timer1.Enabled = true;
            timer2.Interval = 500;
            timer2.Enabled = true;

        }

       

        private bool inited = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (slavesResponding > 0)
            {
                if (inited)
                {
                    bot.EvaluateScript("1 .slave-diff");
                }
                else
                {
                    bot.EvaluateScript("1 .slave");
                    inited = true;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript("hm 1 1 op-mode!");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript("pp 1 1 op-mode!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript("1 1 drive-on");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript("1 1 drive-off");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript("1 1 drive-stop");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript("1 1 reset-fault");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript("1 1 go");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(textTargetPositionCMD.Text + " 1 1 target-p!");
            textTargetPositionCMD.Text = "";
        }
        
        private void button9_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript("1 1 real-p@ 0 $2539 1 sdo-download-i32 3 0 $253A 1 sdo-download-u32 $3E80132 0 $253B 1 sdo-download-u32");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript("$3E80133 0 $253B 1 sdo-download-u32");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript("$3E80132 0 $253B 1 sdo-download-u32");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript("50 0 $250B 1 sdo-download-u32 " 
                                + textCMP1.Text + " 0 $250C 1 sdo-download-i32 "
                                + textCMP2.Text + " 0 $250C 1 sdo-download-i32 "
                                + textCMP3.Text + " 0 $250C 1 sdo-download-i32");
        }
        
        private void button16_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(textProfileAccCMD.Text + " dup 1 1 profile-a1! 1 1 profile-a2!");
            textProfileAccCMD.Text = "";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(textProfileVelocityCMD.Text + " 1 1 profile-v!");
            textProfileVelocityCMD.Text = "";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(textHomingSpeed2CMD.Text + " 1 1 homing-v2!");
            textHomingSpeed2CMD.Text = "";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(textHomingMethodCMD.Text + " 1 1 homing-method!");
            textHomingMethodCMD.Text = "";
        }

        private void timer2_Tick_1(object sender, EventArgs e)
        {
            bot.EvaluateScript(".ec-links");
        }
    }
}

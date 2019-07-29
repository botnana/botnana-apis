using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using BotnanaLib;

namespace SingleDrive
{
    public partial class Form1 : Form
    {

        private Botnana bot;

        // 因為會有垃圾收集的關係，所以callback 要這樣宣告
        private HandleMessage onWsErrorCallback;
        private void OnWsErrorCB(IntPtr dataPtr, string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("WS error : " + str)).Start();
        }

        private HandleMessage onMessageCallback;
        private int messageIndex = 0;
        private void HandleMessageCB(IntPtr dataPtr, string str)
        {
            messageIndex += 1;
            if (messageIndex > 0xFF)
            {
                messageIndex = 0;
            }
        }

        private HandleMessage slavesRespondingCallback;
        private int slaveCount = 0;
        private void SlavesRespondingCB(IntPtr dataPtr, string str)
        {
            slaveCount = Int32.Parse(str);
        }

        private HandleMessage statusWordCallback;
        private bool driveFault = false;
        private bool targetReached = false;
        private bool servoOn = false;
        private bool quickStopRequest = false;
        private void StatusWordCB(IntPtr dataPtr, string str)
        {
            int code = Convert.ToInt32(str, 16);
            driveFault = (code & 8) != 0;
            targetReached = (code & 0x400) != 0;
            servoOn = (code & 4) != 0;
            quickStopRequest = (code & 0x20) == 0;
        }

        private HandleMessage digitalInputsCallback;
        private bool driveOrg = false;
        private bool driveNl = false;
        private bool drivePl = false;
        private void DigitalInputsCB(IntPtr dataPtr, string str)
        {
            int dins = Convert.ToInt32(str, 16);
            driveOrg = (dins & 0x4) != 0;
            driveNl = (dins & 0x1) != 0;
            drivePl = (dins & 0x2) != 0;
        }

        private HandleMessage realPositionCallback;
        private string realPositionStr = "0";
        private void RealPositionCB(IntPtr dataPtr, string str)
        {
            realPositionStr = str;
        }

        private HandleMessage targetPositionCallback;
        private string targetPositionStr = "0";
        private void TargetPositionCB(IntPtr dataPtr, string str)
        {
            targetPositionStr = str;
        }

        private HandleMessage opModeCallback;
        private string opModeStr = "Other";
        private void OpModeCB(IntPtr dataPtr, string str)
        {
            int mode = Int32.Parse(str);
            switch (mode)
            {
                case 1:
                    opModeStr = "PP";
                    break;
                case 6:
                    opModeStr = "HM";
                    break;
                case 8:
                    opModeStr = "CSP";
                    break;
                default:
                    opModeStr = "Other";
                    break;
            }
        }

        private HandleMessage profileVelocityCallback;
        private string profileVelocityStr = "0";
        private void ProfileVelocityCB(IntPtr dataPtr, string str)
        {
            profileVelocityStr = str;
        }

        private HandleMessage profileAccelerationCallback;
        private string profileAccelerationStr = "0";
        private void ProfileAccelerationCB(IntPtr dataPtr, string str)
        {
            profileAccelerationStr = str;
        }

        private HandleMessage deployedCallback;
        private void DeployedCB(IntPtr dataPtr, string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("Deployed : " + str)).Start();
        }

        private HandleMessage endOfProgramCallback;
        private void EndOfProgramCB(IntPtr dataPtr, string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("End-of-program : " + str)).Start();
        }

        private HandleMessage errorCallback;
        private void ErrorCB(IntPtr dataPtr, string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show(str)).Start();
        }

        private Boolean profileVelocityChanging = false;
        private Boolean profileAccelerationChanging = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bot = new Botnana("192.168.7.2");

            onWsErrorCallback = new HandleMessage(OnWsErrorCB);
            bot.SetOnErrorCB(IntPtr.Zero, onWsErrorCallback);

            onMessageCallback = new HandleMessage(HandleMessageCB);
            bot.SetOnMessageCB(IntPtr.Zero, onMessageCallback);

            slavesRespondingCallback = new HandleMessage(SlavesRespondingCB);
            bot.SetTagCB("slaves_responding", 0, IntPtr.Zero, slavesRespondingCallback);

            statusWordCallback = new HandleMessage(StatusWordCB);
            bot.SetTagCB("status_word.1.1", 0, IntPtr.Zero, statusWordCallback);

            realPositionCallback = new HandleMessage(RealPositionCB);
            bot.SetTagCB("real_position.1.1", 0, IntPtr.Zero, realPositionCallback);

            targetPositionCallback = new HandleMessage(TargetPositionCB);
            bot.SetTagCB("target_position.1.1", 0, IntPtr.Zero, targetPositionCallback);

            opModeCallback = new HandleMessage(OpModeCB);
            bot.SetTagCB("operation_mode.1.1", 0, IntPtr.Zero, opModeCallback);

            digitalInputsCallback = new HandleMessage(DigitalInputsCB);
            bot.SetTagCB("digital_inputs.1.1", 0, IntPtr.Zero, digitalInputsCallback);

            profileVelocityCallback = new HandleMessage(ProfileVelocityCB);
            bot.SetTagCB("profile_velocity.1.1", 0, IntPtr.Zero, profileVelocityCallback);

            profileAccelerationCallback = new HandleMessage(ProfileAccelerationCB);
            bot.SetTagCB("profile_acceleration.1.1", 0, IntPtr.Zero, profileAccelerationCallback);

            deployedCallback = new HandleMessage(DeployedCB);
            bot.SetTagCB("deployed", 0, IntPtr.Zero, deployedCallback);

            endOfProgramCallback = new HandleMessage(EndOfProgramCB);
            bot.SetTagCB("end-of-program", 0, IntPtr.Zero, endOfProgramCallback);

            errorCallback = new HandleMessage(ErrorCB);
            bot.SetTagCB("error", 0, IntPtr.Zero, errorCallback);

            bot.Connect();
            Thread.Sleep(1000);

            bot.EvaluateScript(".ec-links");
            bot.EvaluateScript("1 .slave");
            timer1.Interval = 50;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textSlaveCount.Text = slaveCount.ToString();
            if (slaveCount > 0)
            {
                bot.EvaluateScript("1 .slave-diff");
                textSlaveCount.Text = slaveCount.ToString();
                if (profileVelocityChanging == false)
                {
                    textProfileVelocity.Text = profileVelocityStr;
                }
                if (profileAccelerationChanging == false)
                {
                    textProfileAcceleration.Text = profileAccelerationStr;
                }
                textRealPosition.Text = realPositionStr;
                textTargetPosition.Text = targetPositionStr;
                textOPMode.Text = opModeStr;
                radioFault.Checked = driveFault;
                radioTargetReached.Checked = targetReached;
                radioServoOn.Checked = !quickStopRequest & servoOn;
                radioServoStop.Checked = quickStopRequest & servoOn;
                radioOrg.Checked = driveOrg;
                radioPosLimit.Checked = drivePl;
                radioNegLimit.Checked = driveNl;
                labCount.Text = messageIndex.ToString("X2");
            }
        }

        private void buttonResetFault_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript("1 1 reset-fault 1 1 drive-stop");
        }

        private void buttonServoOn_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript("pp 1 1 op-mode! 1 1 drive-on");
        }

        private void buttonServoOff_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript("1 1 drive-off");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript("1 1 drive-stop");
        }

        private void buttonSetTargetPosition_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(textNextTargetPosition.Text + " 1 1 target-p!");
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript("1 1 go");
        }

        private void buttonDeploy_Click(object sender, EventArgs e)
        {
            int p1 = Int32.Parse(textP1.Text);
            int p2 = Int32.Parse(textP2.Text);
            int p3 = Int32.Parse(textP3.Text);

            //deploy 後要等待 deployed|ok 訊息回傳後才能執行 pm_run(botnana, program);
            bot.ClearProgram();
            bot.AddProgramLine("pp 1 1 op-mode!");
            bot.AddProgramLine("until-no-requests");
            //program_line_dll(program, " begin -1 while");    // 如果要p1->p2->p3->p1...運動,就移除此行註解
            bot.AddProgramLine(p1.ToString() + " 1 1 target-p!");
            bot.AddProgramLine(" 1 1 go 1 1 until-target-reached");
            bot.AddProgramLine(p2.ToString() + " 1 1 target-p!");
            bot.AddProgramLine(" 1 1 go 1 1 until-target-reached");
            bot.AddProgramLine(p3.ToString() + " 1 1 target-p!");
            bot.AddProgramLine(" 1 1 go 1 1 until-target-reached");
            //program_line_dll(program, " repeat");           // 如果要p1->p2->p3->p1...運動,就移除此行註解
            bot.EvaluateScript("-work marker -work");          // 清除先前定義的program
            bot.DeployProgram();
            buttonPMAbort.Enabled = true;
            buttonRun.Enabled = true;
        }

        private void buttonPMAbort_Click(object sender, EventArgs e)
        {
            bot.AbortProgram();
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            bot.RunProgram();
        }

        private void buttonEvaluate_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(textEvalute.Text);
            textEvalute.ResetText();
        }

        private void textProfileVelocity_Enter(object sender, EventArgs e)
        {
            profileVelocityChanging = true;
        }

        private void textProfileVelocity_Leave(object sender, EventArgs e)
        {
            bot.EvaluateScript(textProfileVelocity.Text + " 1 1 profile-v!");
            profileVelocityChanging = false;
        }

        private void textProfileAcceleration_Enter(object sender, EventArgs e)
        {
            profileAccelerationChanging = true;
        }

        private void textProfileAcceleration_Leave(object sender, EventArgs e)
        {
            bot.EvaluateScript(textProfileAcceleration.Text + " 1 1 profile-a1!");
            bot.EvaluateScript(textProfileAcceleration.Text + " 1 1 profile-a2!");
            profileAccelerationChanging = false;
        }
    }
}

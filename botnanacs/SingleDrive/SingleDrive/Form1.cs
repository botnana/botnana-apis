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
        private HandleMessage on_ws_error_callback;
        private void on_ws_error_cb(IntPtr dataPtr, string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("WS error : " + str)).Start();
        }

        private HandleMessage on_message_callback;
        private int message_index = 0;
        private void handle_message_cb(IntPtr dataPtr, string str)
        {
            message_index += 1;
            if (message_index > 0xFF)
            {
                message_index = 0;
            }
        }

        private HandleMessage slaves_responding_callback;
        private int slave_count = 0;
        private void slaves_responding_cb(IntPtr dataPtr, string str)
        {
            slave_count = Int32.Parse(str);
        }

        private HandleMessage status_word_callback;
        private bool drive_fault = false;
        private bool target_reached = false;
        private bool servo_on = false;
        private bool quick_stop_request = false;
        private void status_word_cb(IntPtr dataPtr, string str)
        {
            int code = Convert.ToInt32(str, 16);
            drive_fault = (code & 8) != 0;
            target_reached = (code & 0x400) != 0;
            servo_on = (code & 4) != 0;
            quick_stop_request = (code & 0x20) == 0;
        }

        private HandleMessage digital_inputs_callback;
        private bool drive_org = false;
        private bool drive_nl = false;
        private bool drive_pl = false;
        private void digital_inputs_cb(IntPtr dataPtr, string str)
        {
            int dins = Convert.ToInt32(str, 16);
            drive_org = (dins & 0x4) != 0;
            drive_nl = (dins & 0x1) != 0;
            drive_pl = (dins & 0x2) != 0;
        }

        private HandleMessage real_position_callback;
        private string real_position_str = "0";
        private void real_position_cb(IntPtr dataPtr, string str)
        {
            real_position_str = str;
        }

        private HandleMessage target_position_callback;
        private string target_position_str = "0";
        private void target_position_cb(IntPtr dataPtr, string str)
        {
            target_position_str = str;
        }

        private HandleMessage op_mode_callback;
        private string op_mode_str = "Other";
        private void op_mode_cb(IntPtr dataPtr, string str)
        {
            int mode = Int32.Parse(str);
            switch (mode)
            {
                case 1:
                    op_mode_str = "PP";
                    break;
                case 6:
                    op_mode_str = "HM";
                    break;
                case 8:
                    op_mode_str = "CSP";
                    break;
                default:
                    op_mode_str = "Other";
                    break;
            }
        }

        private HandleMessage profile_velocity_callback;
        private string profile_velocity_str = "0";
        private void profile_velocity_cb(IntPtr dataPtr, string str)
        {
            profile_velocity_str = str;
        }

        private HandleMessage profile_acceleration_callback;
        private string profile_acceleration_str = "0";
        private void profile_acceleration_cb(IntPtr dataPtr, string str)
        {
            profile_acceleration_str = str;
        }

        private HandleMessage deployed_callback;
        private void deployed_cb(IntPtr dataPtr, string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("Deployed : " + str)).Start();
        }

        private HandleMessage end_of_program_callback;
        private void end_of_program_cb(IntPtr dataPtr, string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("End-of-program : " + str)).Start();
        }

        private HandleMessage error_callback;
        private void error_cb(IntPtr dataPtr, string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show(str)).Start();
        }

        private Boolean profile_velocity_changing = false;
        private Boolean profile_acceleration_changing = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bot = new Botnana("192.168.7.2");

            on_ws_error_callback = new HandleMessage(on_ws_error_cb);
            bot.SetOnErrorCB(IntPtr.Zero, on_ws_error_callback);
            on_message_callback = new HandleMessage(handle_message_cb);
            bot.SetOnMessageCB(IntPtr.Zero, on_message_callback);
            slaves_responding_callback = new HandleMessage(slaves_responding_cb);
            bot.SetTagCB("slaves_responding", 0, IntPtr.Zero, slaves_responding_callback);
            status_word_callback = new HandleMessage(status_word_cb);
            bot.SetTagCB("status_word.1.1", 0, IntPtr.Zero, status_word_callback);
            real_position_callback = new HandleMessage(real_position_cb);
            bot.SetTagCB("real_position.1.1", 0, IntPtr.Zero, real_position_callback);
            target_position_callback = new HandleMessage(target_position_cb);
            bot.SetTagCB("target_position.1.1", 0, IntPtr.Zero, target_position_callback);
            op_mode_callback = new HandleMessage(op_mode_cb);
            bot.SetTagCB("operation_mode.1.1", 0, IntPtr.Zero, op_mode_callback);
            digital_inputs_callback = new HandleMessage(digital_inputs_cb);
            bot.SetTagCB("digital_inputs.1.1", 0, IntPtr.Zero, digital_inputs_callback);
            profile_velocity_callback = new HandleMessage(profile_velocity_cb);
            bot.SetTagCB("profile_velocity.1.1", 0, IntPtr.Zero, profile_velocity_callback);
            profile_acceleration_callback = new HandleMessage(profile_acceleration_cb);
            bot.SetTagCB("profile_acceleration.1.1", 0, IntPtr.Zero, profile_acceleration_callback);
            deployed_callback = new HandleMessage(deployed_cb);
            bot.SetTagCB("deployed", 0, IntPtr.Zero, deployed_callback);
            end_of_program_callback = new HandleMessage(end_of_program_cb);
            bot.SetTagCB("end-of-program", 0, IntPtr.Zero, end_of_program_callback);
            error_callback = new HandleMessage(error_cb);
            bot.SetTagCB("error", 0, IntPtr.Zero, error_callback);
            bot.Connect();
            Thread.Sleep(1000);

            bot.EvaluateScript(".ec-links");
            bot.EvaluateScript("1 .slave");
            timer1.Interval = 50;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textSlaveCount.Text = slave_count.ToString();
            if (slave_count > 0)
            {
                bot.EvaluateScript("1 .slave-diff");
                textSlaveCount.Text = slave_count.ToString();
                if (profile_velocity_changing == false)
                {
                    textProfileVelocity.Text = profile_velocity_str;
                }
                if (profile_acceleration_changing == false)
                {
                    textProfileAcceleration.Text = profile_acceleration_str;
                }
                textRealPosition.Text = real_position_str;
                textTargetPosition.Text = target_position_str;
                textOPMode.Text = op_mode_str;
                radioFault.Checked = drive_fault;
                radioTargetReached.Checked = target_reached;
                radioServoOn.Checked = !quick_stop_request & servo_on;
                radioServoStop.Checked = quick_stop_request & servo_on;
                radioOrg.Checked = drive_org;
                radioPosLimit.Checked = drive_pl;
                radioNegLimit.Checked = drive_nl;
                labCount.Text = message_index.ToString("X2");
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
            profile_velocity_changing = true;
        }

        private void textProfileVelocity_Leave(object sender, EventArgs e)
        {
            bot.EvaluateScript(textProfileVelocity.Text + " 1 1 profile-v!");
            profile_velocity_changing = false;
        }

        private void textProfileAcceleration_Enter(object sender, EventArgs e)
        {
            profile_acceleration_changing = true;
        }

        private void textProfileAcceleration_Leave(object sender, EventArgs e)
        {
            bot.EvaluateScript(textProfileAcceleration.Text + " 1 1 profile-a1!");
            bot.EvaluateScript(textProfileAcceleration.Text + " 1 1 profile-a2!");
            profile_acceleration_changing = false;
        }
    }
}

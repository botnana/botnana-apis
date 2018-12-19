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

namespace SingleDrive
{
    public partial class Form1 : Form
    {

        // 因為要傳遞給C函式庫，所以要特別宣告是(CallingConvention.Cdecl)
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void HandleMessage(string str);

        [DllImport(@"..\..\BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr botnana_connect_dll(string address, HandleMessage on_error_cb);

        [DllImport(@"..\..\BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void script_evaluate_dll(IntPtr desc, string script);

        [DllImport(@"..\..\BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_set_tag_cb_dll(IntPtr desc, string tag, int count, HandleMessage hm);

        [DllImport(@"..\..\BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_set_on_message_cb_dll(IntPtr desc, HandleMessage hm);

        [DllImport(@"..\..\BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr program_new_dll(string name);

        [DllImport(@"..\..\BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void program_line_dll(IntPtr pm, string cmd);

        [DllImport(@"..\..\BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void program_clear_dll(IntPtr pm);

        [DllImport(@"..\..\BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void program_deploy_dll(IntPtr botnana, IntPtr pm);

        [DllImport(@"..\..\BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void program_run_dll(IntPtr botnana, IntPtr pm);

        [DllImport(@"..\..\BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_abort_program_dll(IntPtr botnana);



        private static IntPtr botnana;
        private static IntPtr program;

        // 因為會有垃圾收集的關係，所以callback 要這樣宣告
        private static HandleMessage on_ws_error_callback = new HandleMessage(on_ws_error_cb);
        private static void on_ws_error_cb(string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("WS error : " + str)).Start();
        }

        private static HandleMessage on_message_callback = new HandleMessage(handle_message_cb);
        private static int message_index = 0;
        private static void handle_message_cb(string str)
        {
            message_index += 1;
            if (message_index > 0xFF)
            {
                message_index = 0;
            }
        }

        private static HandleMessage slaves_responding_callback = new HandleMessage(slaves_responding_cb);
        static int slave_count = 0;
        private static void slaves_responding_cb(string str)
        {
            slave_count = Int32.Parse(str);
        }

        private static HandleMessage status_word_callback = new HandleMessage(status_word_cb);
        private static bool drive_fault = false;
        private static bool target_reached = false;
        private static bool servo_on = false;
        private static bool quick_stop_request = false;
        private static void status_word_cb(string str)
        {
            int code = Convert.ToInt32(str, 16);
            drive_fault = (code & 8) != 0;
            target_reached = (code & 0x400) != 0;
            servo_on = (code & 4) != 0;
            quick_stop_request = (code & 0x20) == 0;
        }

        private static HandleMessage digital_inputs_callback = new HandleMessage(digital_inputs_cb);
        private static bool drive_org = false;
        private static bool drive_nl = false;
        private static bool drive_pl = false;
        private static void digital_inputs_cb(string str)
        {
            int dins = Convert.ToInt32(str, 16);
            drive_org = (dins & 0x4) != 0;
            drive_nl = (dins & 0x1) != 0;
            drive_pl = (dins & 0x2) != 0;
        }
               
        private static HandleMessage real_position_callback = new HandleMessage(real_position_cb);
        private static string real_position_str = "0";
        private static void real_position_cb(string str)
        {
            real_position_str = str;
        }

        private static HandleMessage target_position_callback = new HandleMessage(target_position_cb);
        private static string target_position_str = "0";
        private static void target_position_cb(string str)
        {
            target_position_str = str;
        }

        private static HandleMessage op_mode_callback = new HandleMessage(op_mode_cb);
        private static string op_mode_str = "Other";
        private static void op_mode_cb(string str)
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

        private static HandleMessage profile_velocity_callback = new HandleMessage(profile_velocity_cb);
        private static string profile_velocity_str = "0";
        private static void profile_velocity_cb(string str)
        {
            profile_velocity_str = str;
        }

        private static HandleMessage profile_acceleration_callback = new HandleMessage(profile_acceleration_cb);
        private static string profile_acceleration_str = "0";
        private static void profile_acceleration_cb(string str)
        {
            profile_acceleration_str = str;
        }


        private static HandleMessage deployed_callback = new HandleMessage(deployed_cb);
        private static void deployed_cb(string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("Deployed : " + str)).Start();
        }

        private static HandleMessage end_of_program_callback = new HandleMessage(end_of_program_cb);
        private static void end_of_program_cb(string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("End-of-program : " + str)).Start();
        }

        private static HandleMessage error_callback = new HandleMessage(error_cb);
        private static void error_cb(string str)
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
            botnana = botnana_connect_dll("192.168.7.2", on_ws_error_callback);
            program = program_new_dll("SingleDrive");
            botnana_set_on_message_cb_dll(botnana, on_message_callback);
            botnana_set_tag_cb_dll(botnana, "slaves_responding", 0, slaves_responding_callback);
            botnana_set_tag_cb_dll(botnana, "status_word.1.1", 0, status_word_callback);
            botnana_set_tag_cb_dll(botnana, "real_position.1.1", 0, real_position_callback);
            botnana_set_tag_cb_dll(botnana, "target_position.1.1", 0, target_position_callback);
            botnana_set_tag_cb_dll(botnana, "operation_mode.1.1", 0, op_mode_callback);
            botnana_set_tag_cb_dll(botnana, "digital_inputs.1.1", 0, digital_inputs_callback);
            botnana_set_tag_cb_dll(botnana, "profile_velocity.1.1", 0, profile_velocity_callback);
            botnana_set_tag_cb_dll(botnana, "profile_acceleration.1.1", 0, profile_acceleration_callback);
            botnana_set_tag_cb_dll(botnana, "deployed", 0, deployed_callback);
            botnana_set_tag_cb_dll(botnana, "end-of-program", 0, end_of_program_callback);
            botnana_set_tag_cb_dll(botnana, "error", 0, error_callback);
            script_evaluate_dll(botnana, ".ec-links");
            script_evaluate_dll(botnana, "1 .slave");
            timer1.Interval = 50;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textSlaveCount.Text = slave_count.ToString();
            if (slave_count > 0)
            {
                script_evaluate_dll(botnana, "1 .slave-diff");
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
            script_evaluate_dll(botnana, "1 1 reset-fault 1 1 drive-stop");
        }

        private void buttonServoOn_Click(object sender, EventArgs e)
        {
            script_evaluate_dll(botnana, "pp 1 1 op-mode! 1 1 drive-on");
        }

        private void buttonServoOff_Click(object sender, EventArgs e)
        {
            script_evaluate_dll(botnana, "1 1 drive-off");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            script_evaluate_dll(botnana, "1 1 drive-stop");
        }

        private void buttonSetTargetPosition_Click(object sender, EventArgs e)
        {
            script_evaluate_dll(botnana, textNextTargetPosition.Text + " 1 1 target-p!");
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            script_evaluate_dll(botnana, "1 1 go");
        }

        private void buttonDeploy_Click(object sender, EventArgs e)
        {
            int p1 = Int32.Parse(textP1.Text);
            int p2 = Int32.Parse(textP2.Text);
            int p3 = Int32.Parse(textP3.Text);

            //deploy 後要等待 deployed|ok 訊息回傳後才能執行 pm_run(botnana, program);
            program_clear_dll(program);
            program_line_dll(program, "pp 1 1 op-mode!");
            program_line_dll(program, "until-no-requests");
            //program_line_dll(program, " begin -1 while");    // 如果要p1->p2->p3->p1...運動,就移除此行註解
            program_line_dll(program, p1.ToString() + " 1 1 target-p!");
            program_line_dll(program, " 1 1 go 1 1 until-target-reached");
            program_line_dll(program, p2.ToString() + " 1 1 target-p!");
            program_line_dll(program, " 1 1 go 1 1 until-target-reached");
            program_line_dll(program, p3.ToString() + " 1 1 target-p!");
            program_line_dll(program, " 1 1 go 1 1 until-target-reached");
            //program_line_dll(program, " repeat");           // 如果要p1->p2->p3->p1...運動,就移除此行註解
            script_evaluate_dll(botnana, "-work marker -work");          // 清除先前定義的program
            program_deploy_dll(botnana, program);
            buttonPMAbort.Enabled = true;
            buttonRun.Enabled = true;
        }

        private void buttonPMAbort_Click(object sender, EventArgs e)
        {
            botnana_abort_program_dll(botnana);
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            program_run_dll(botnana, program);
        }

        private void buttonEvaluate_Click(object sender, EventArgs e)
        {
            script_evaluate_dll(botnana, textEvalute.Text);
            textEvalute.ResetText();
        }

        private void textProfileVelocity_Enter(object sender, EventArgs e)
        {
            profile_velocity_changing = true;
        }

        private void textProfileVelocity_Leave(object sender, EventArgs e)
        {
            script_evaluate_dll(botnana, textProfileVelocity.Text + " 1 1 profile-v!");
            profile_velocity_changing = false;
        }

        private void textProfileAcceleration_Enter(object sender, EventArgs e)
        {
            profile_acceleration_changing = true;
        }

        private void textProfileAcceleration_Leave(object sender, EventArgs e)
        {
            script_evaluate_dll(botnana, textProfileAcceleration.Text + " 1 1 profile-a1!");
            script_evaluate_dll(botnana, textProfileAcceleration.Text + " 1 1 profile-a2!");
            profile_acceleration_changing = false;
        }
    }
}

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace AxisGroup
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
        private static void handle_message_cb(string str)
        {
            //new Thread(() => System.Windows.Forms.MessageBox.Show(str)).Start();
        }

        private static HandleMessage mcs_callback = new HandleMessage(mcs_cb);
        private static double[] mcs_positions;
        private static void mcs_cb(string str)
        {
            int i = 0;
            String[] elements = Regex.Split(str, @",");
            foreach (var element in elements)
            {
                mcs_positions[i] = Convert.ToDouble(element)*1000.0;
                i++;
            }
        }

        private static HandleMessage pcs_callback = new HandleMessage(pcs_cb);
        private static double[] pcs_positions;
        private static void pcs_cb(string str)
        {
            int i = 0;
            String[] elements = Regex.Split(str, @",");
            foreach (var element in elements)
            {
                pcs_positions[i] = Convert.ToDouble(element)*1000.0;
                i++;
            }
        }

        private static HandleMessage pva_callback = new HandleMessage(pva_cb);
        private static double[] pva;
        private static void pva_cb(string str)
        {
            int i = 0;
            String[] elements = Regex.Split(str, @",");
            foreach (var element in elements)
            {
                pva[i] = Convert.ToDouble(element);
                i++;
            }
        }

        private static HandleMessage move_length_callback = new HandleMessage(move_length_cb);
        private static double move_length;
        private static void move_length_cb(string str)
        {
            move_length  = Convert.ToDouble(str);
        }

        private static HandleMessage path_id_callback = new HandleMessage(path_id_cb);
        private static int path_id;
        private static void path_id_cb(string str)
        {
            path_id = Int32.Parse(str);
        }

        private static HandleMessage servo_on_callback = new HandleMessage(servo_on_cb);
        private static int servo_on;
        private static void servo_on_cb(string str)
        {
            servo_on = Int32.Parse(str);
        }

        private static HandleMessage motion_state_callback = new HandleMessage(motion_state_cb);
        private static int motion_state;
        private static void motion_state_cb(string str)
        {
            motion_state = Int32.Parse(str);
        }

        private static HandleMessage axis_real_position_1_callback = new HandleMessage(axis_real_position_1_cb);
        private static double axis_real_position_1;
        private static void axis_real_position_1_cb(string str)
        {
            
            axis_real_position_1 = Double.Parse(str);
        }

        private static HandleMessage axis_real_position_2_callback = new HandleMessage(axis_real_position_2_cb);
        private static double axis_real_position_2;
        private static void axis_real_position_2_cb(string str)
        {
            axis_real_position_2 = Double.Parse(str);
        }

        private static HandleMessage axis_real_position_3_callback = new HandleMessage(axis_real_position_3_cb);
        private static double axis_real_position_3;
        private static void axis_real_position_3_cb(string str)
        {
           axis_real_position_3 = Double.Parse(str);
        }

        private static HandleMessage axis_homed_1_callback = new HandleMessage(axis_homed_1_cb);
        private static int axis_homed_1;
        private static void axis_homed_1_cb(string str)
        {
            axis_homed_1 = Int32.Parse(str);
        }

        private static HandleMessage axis_homed_2_callback = new HandleMessage(axis_homed_2_cb);
        private static int axis_homed_2;
        private static void axis_homed_2_cb(string str)
        {
            axis_homed_2 = Int32.Parse(str);
        }

        private static HandleMessage axis_homed_3_callback = new HandleMessage(axis_homed_3_cb);
        private static int axis_homed_3;
        private static void axis_homed_3_cb(string str)
        {
            axis_homed_3 = Int32.Parse(str);
        }

        private static HandleMessage nc_owner_callback = new HandleMessage(nc_owner_cb);
        private static int nc_owner;
        private static void nc_owner_cb(string str)
        {
            nc_owner = Int32.Parse(str);
        }

        private static HandleMessage nc_suspended_callback = new HandleMessage(nc_suspended_cb);
        private static int nc_suspended;
        private static void nc_suspended_cb(string str)
        {
            nc_suspended = Int32.Parse(str);
        }

        private static HandleMessage devices_ok_callback = new HandleMessage(devices_ok_cb);
        private static int devices_ok;
        private static void devices_ok_cb(string str)
        {
            devices_ok = Int32.Parse(str);
        }

        private static Boolean config_request;

        private static HandleMessage rapid_travels_rate_callback = new HandleMessage(rapid_travels_rate_cb);
        private static double rapid_travels_rate;
        private static void rapid_travels_rate_cb(string str)
        {
            rapid_travels_rate = Double.Parse(str);
            config_request = true;
        }

        private static HandleMessage machining_rate_callback = new HandleMessage(machining_rate_cb);
        private static double machining_rate;
        private static void machining_rate_cb(string str)
        {
            machining_rate = Double.Parse(str);
            config_request = true;
        }

        private static HandleMessage axis_homing_v1x_callback = new HandleMessage(axis_homing_v1x_cb);
        private static UInt32 axis_homing_v1x;
        private static void axis_homing_v1x_cb(string str)
        {
            axis_homing_v1x = UInt32.Parse(str);
            config_request = true;
        }
        private static HandleMessage axis_homing_v1y_callback = new HandleMessage(axis_homing_v1y_cb);
        private static UInt32 axis_homing_v1y;
        private static void axis_homing_v1y_cb(string str)
        {
            axis_homing_v1y = UInt32.Parse(str);
            config_request = true;
        }

        private static HandleMessage axis_homing_v1z_callback = new HandleMessage(axis_homing_v1z_cb);
        private static UInt32 axis_homing_v1z;
        private static void axis_homing_v1z_cb(string str)
        {
            axis_homing_v1z = UInt32.Parse(str);
            config_request = true;
        }

        private static HandleMessage axis_homing_v2x_callback = new HandleMessage(axis_homing_v2x_cb);
        private static UInt32 axis_homing_v2x;
        private static void axis_homing_v2x_cb(string str)
        {
            axis_homing_v2x = UInt32.Parse(str);
            config_request = true;
        }
        private static HandleMessage axis_homing_v2y_callback = new HandleMessage(axis_homing_v2y_cb);
        private static UInt32 axis_homing_v2y;
        private static void axis_homing_v2y_cb(string str)
        {
            axis_homing_v2y = UInt32.Parse(str);
            config_request = true;
        }

        private static HandleMessage axis_homing_v2z_callback = new HandleMessage(axis_homing_v2z_cb);
        private static UInt32 axis_homing_v2z;
        private static void axis_homing_v2z_cb(string str)
        {
            axis_homing_v2z = UInt32.Parse(str);
            config_request = true;
        }

        private static HandleMessage axis_homing_methodx_callback = new HandleMessage(axis_homing_methodx_cb);
        private static UInt32 axis_homing_methodx;
        private static void axis_homing_methodx_cb(string str)
        {
            axis_homing_methodx = UInt32.Parse(str);
            config_request = true;
        }
        private static HandleMessage axis_homing_methody_callback = new HandleMessage(axis_homing_methody_cb);
        private static UInt32 axis_homing_methody;
        private static void axis_homing_methody_cb(string str)
        {
            axis_homing_methody = UInt32.Parse(str);
            config_request = true;
        }
        private static HandleMessage axis_homing_methodz_callback = new HandleMessage(axis_homing_methodz_cb);
        private static UInt32 axis_homing_methodz;
        private static void axis_homing_methodz_cb(string str)
        {
            axis_homing_methodz = UInt32.Parse(str);
            config_request = true;
        }
               
        private static HandleMessage error_callback = new HandleMessage(error_cb);
        private static void error_cb(string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("error|" + str)).Start();
        }

        private static HandleMessage log_callback = new HandleMessage(log_cb);
        private static void log_cb(string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("log|" + str)).Start();

        }

        private static HandleMessage deployed_callback = new HandleMessage(deployed_cb);
        private static void deployed_cb(string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("deployed|" + str)).Start();

        }

        private static void load_sfc(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path, Encoding.UTF8);
            foreach (string line in lines)
            {
                script_evaluate_dll(botnana, line);
            }
            Thread.Sleep(100);
        }

        private static HandleMessage user_parameter_callback = new HandleMessage(user_parameter_cb);
        private static void user_parameter_cb(string str)
        {
            int para = Int32.Parse(str);
            switch (para)
            {
                case 0:
                    script_evaluate_dll(botnana, "$10 user-para!");
                    script_evaluate_dll(botnana, "0sfc");
                    load_sfc(@"..\..\config.sfc");
                    load_sfc(@"..\..\servo_on_off.sfc");
                    load_sfc(@"..\..\homing.sfc");
                    load_sfc(@"..\..\motion_state.sfc");
                    load_sfc(@"..\..\manager.sfc");
                    break;
                default:
                    break;
            }
            script_evaluate_dll(botnana, ".config");

        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            botnana = botnana_connect_dll("192.168.7.2", on_ws_error_callback);
            program = program_new_dll("3dtest");
            botnana_set_on_message_cb_dll(botnana, on_message_callback);
            script_evaluate_dll(botnana, ".user-para");
            botnana_set_tag_cb_dll(botnana, "MCS.1", 0, mcs_callback);
            botnana_set_tag_cb_dll(botnana, "PCS.1", 0, pcs_callback);
            botnana_set_tag_cb_dll(botnana, "pva.1", 0, pva_callback);
            botnana_set_tag_cb_dll(botnana, "move_length.1", 0, move_length_callback);
            botnana_set_tag_cb_dll(botnana, "path_id.1", 0, path_id_callback);
            botnana_set_tag_cb_dll(botnana, "servo_on", 0, servo_on_callback);
            botnana_set_tag_cb_dll(botnana, "motion_state", 0, motion_state_callback);
            botnana_set_tag_cb_dll(botnana, "axis_corrected_position.1", 0, axis_real_position_1_callback);
            botnana_set_tag_cb_dll(botnana, "axis_corrected_position.2", 0, axis_real_position_2_callback);
            botnana_set_tag_cb_dll(botnana, "axis_corrected_position.3", 0, axis_real_position_3_callback);
            botnana_set_tag_cb_dll(botnana, "axis_homed.1", 0, axis_homed_1_callback);
            botnana_set_tag_cb_dll(botnana, "axis_homed.2", 0, axis_homed_2_callback);
            botnana_set_tag_cb_dll(botnana, "axis_homed.3", 0, axis_homed_3_callback);
            botnana_set_tag_cb_dll(botnana, "nc_owner", 0, nc_owner_callback);
            botnana_set_tag_cb_dll(botnana, "nc_suspended", 0, nc_suspended_callback);
            botnana_set_tag_cb_dll(botnana, "devices_ok", 0, devices_ok_callback);

            botnana_set_tag_cb_dll(botnana, "rapid_travels_rate", 0, rapid_travels_rate_callback);
            botnana_set_tag_cb_dll(botnana, "machining_rate", 0, machining_rate_callback);
            botnana_set_tag_cb_dll(botnana, "axis_homing_v1.1", 0, axis_homing_v1x_callback);
            botnana_set_tag_cb_dll(botnana, "axis_homing_v1.2", 0, axis_homing_v1y_callback);
            botnana_set_tag_cb_dll(botnana, "axis_homing_v1.3", 0, axis_homing_v1z_callback);
            botnana_set_tag_cb_dll(botnana, "axis_homing_v2.1", 0, axis_homing_v2x_callback);
            botnana_set_tag_cb_dll(botnana, "axis_homing_v2.2", 0, axis_homing_v2y_callback);
            botnana_set_tag_cb_dll(botnana, "axis_homing_v2.3", 0, axis_homing_v2z_callback);
            botnana_set_tag_cb_dll(botnana, "axis_homing_method.1", 0, axis_homing_methodx_callback);
            botnana_set_tag_cb_dll(botnana, "axis_homing_method.2", 0, axis_homing_methody_callback);
            botnana_set_tag_cb_dll(botnana, "axis_homing_method.3", 0, axis_homing_methodz_callback);
            
            botnana_set_tag_cb_dll(botnana, "error", 0, error_callback);
            botnana_set_tag_cb_dll(botnana, "log", 0, log_callback);

            botnana_set_tag_cb_dll(botnana, "user_parameter", 0, user_parameter_callback);

            botnana_set_tag_cb_dll(botnana, "deployed", 0, deployed_callback);
           


            mcs_positions = new double[3];
            pcs_positions = new double[3];
            pva = new double[3];


            DataGridViewRowCollection rows = dataGridView1.Rows;
            rows.Add(new Object[] { "1","92", "0.0","0.0","0.0", "900.0"});
            rows.Add(new Object[] { "2","01", "10", null, null , "500"});
            rows.Add(new Object[] { "3","01", "20", null, null, "500" });
            rows.Add(new Object[] { "4","01", "30", null, null, "500" });
            rows.Add(new Object[] { "5","01", "40", null, null, "500" });
            rows.Add(new Object[] { "6","01", "50", null, null, "600" });
            rows.Add(new Object[] { "7","01", "60", null, null, "700" });
            rows.Add(new Object[] { "8","01", "70", null, null, "800" });
            rows.Add(new Object[] { "9","01", "80", null, null, "900" });
            rows.Add(new Object[] { "10","01", "90", null, null, "1000" });
            

            timer1.Interval = 50;
            timer1.Enabled = true;
            timer2.Interval = 500;
            timer2.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            script_evaluate_dll(botnana, "1 .group 1 .axis 2 .axis 3 .axis");
            labelMCS1.Text = mcs_positions[0].ToString("F4");
            labelMCS2.Text = mcs_positions[1].ToString("F4");
            labelMCS3.Text = mcs_positions[2].ToString("F4");
            labelPCS1.Text = pcs_positions[0].ToString("F4");
            labelPCS2.Text = pcs_positions[1].ToString("F4");
            labelPCS3.Text = pcs_positions[2].ToString("F4");
            textNextP.Text = (pva[0] * 1000.0).ToString("F1");
            textNextV.Text = (pva[1]*1000.0*60.0).ToString("F1");
            textPathP.Text = (move_length * 1000.0 ).ToString("F1");
            textAxisP1.Text = (axis_real_position_1 * 1000.0).ToString("F4");
            textAxisP2.Text = (axis_real_position_2 * 1000.0).ToString("F4");
            textAxisP3.Text = (axis_real_position_3 * 1000.0).ToString("F4");
        }

        private void btnJoggingGo_Click(object sender, EventArgs e)
        {
            if (servo_on == 0)
            {
                new Thread(() => System.Windows.Forms.MessageBox.Show("Not Servo ON !!")).Start();
            }
            else
            {
                string mcs_x = mcs_positions[0].ToString("F4");
                string mcs_y = mcs_positions[1].ToString("F4");
                string mcs_z = mcs_positions[2].ToString("F4");

                if (txtJogX.Text.Length > 0)
                {
                    mcs_x = txtJogX.Text;
                    txtJogX.Text = "";
                }

                if (txtJogY.Text.Length > 0)
                {
                    mcs_y = txtJogY.Text;
                    txtJogY.Text = "";
                }

                if (txtJogZ.Text.Length > 0)
                {
                    mcs_z = txtJogZ.Text;
                    txtJogZ.Text = "";
                }
                script_evaluate_dll(botnana, mcs_x + "e mm "+ mcs_y + "e mm "+ mcs_z + "e mm start-jogging");
            }
        }

        private void btnJogStop_Click(object sender, EventArgs e)
        {
            script_evaluate_dll(botnana, "stop-motion");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {

            if (motion_state == 3)
            {
                new Thread(() => System.Windows.Forms.MessageBox.Show("Motion in Machining !!")).Start();
            }
            else
            {
                //deploy 後要等待 deployed|ok 訊息回傳後才能執行 pm_run(botnana, program);
                program_clear_dll(program);
                //program_line_dll(program, ".user-para");



                if (dataGridView1[2, 0].Value == null || dataGridView1[3, 0].Value == null || dataGridView1[4, 0].Value == null)
                {
                    new Thread(() => System.Windows.Forms.MessageBox.Show("Invalid G92")).Start();
                }
                else
                {
                    double[] positions = new double[3];
                    double[] next_positions = new double[3];
                    double feedrate = 100.0;
                    positions[0] = double.Parse(dataGridView1[2, 0].Value.ToString());
                    positions[1] = double.Parse(dataGridView1[3, 0].Value.ToString());
                    positions[2] = double.Parse(dataGridView1[4, 0].Value.ToString());

                    script_evaluate_dll(botnana, "-nc marker -nc");          // 清除先前定義的program


                    program_line_dll(program, "1 group! 0path +group");
                    program_line_dll(program, "start-job");
                    program_line_dll(program, "900.0e mm/min vcmd!");
                    program_line_dll(program, feedrate.ToString("F2") + "e mm/min feedrate!");
                    program_line_dll(program, "1 path-id! 92 path-mode!");
                    program_line_dll(program, positions[0].ToString("F5") + "e mm " + positions[1].ToString("F5") + "e mm " + positions[2].ToString("F5") + "e mm move3d");

                    for (int i = 1; i < 10; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            next_positions[j] = positions[j];

                            if (dataGridView1[2 + j, i].Value != null)
                            {
                                next_positions[j] = double.Parse(dataGridView1[2 + j, i].Value.ToString());

                            }
                        }

                        if (dataGridView1[5, i].Value != null)
                        {
                            feedrate = double.Parse(dataGridView1[5, i].Value.ToString());
                            program_line_dll(program, "1 group! " + feedrate.ToString("F2") + "e mm/min feedrate!");
                        }

                        if (next_positions[0] != positions[0]
                        || next_positions[1] != positions[1]
                        || next_positions[2] != positions[2])
                        {

                            int mode = Int32.Parse(dataGridView1[1, i].Value.ToString());
                            positions[0] = next_positions[0];
                            positions[1] = next_positions[1];
                            positions[2] = next_positions[2];

                            //MessageBox.Show(positions[0].ToString() + "e mm " + positions[1].ToString() + "e mm " + positions[2].ToString() + "e mm");
                            program_line_dll(program, "1 group! ");
                            program_line_dll(program, i.ToString() + @" path-id! " + mode.ToString() + @" path-mode!");
                            program_line_dll(program, positions[0].ToString("F5") + "e mm " + positions[1].ToString("F5") + "e mm " + positions[2].ToString("F5") + "e mm line3d");

                        }

                    }

                    program_line_dll(program, "begin 1 group! gend? not while pause repeat");
                    program_line_dll(program, "1 group! -group  true machining-finished !");
                    program_deploy_dll(botnana, program);
                }
            }
        }

        private void btnProgramRun_Click(object sender, EventArgs e)
        {
            if (servo_on == 0)
            {
                new Thread(() => System.Windows.Forms.MessageBox.Show("Not Servo ON !!")).Start();
            }else if (motion_state != 0)
            {
                new Thread(() => System.Windows.Forms.MessageBox.Show("Not Motion IDLE !!")).Start();
            }
            else
            {
                script_evaluate_dll(botnana, "start-machining");
                if (nc_suspended == 0)
                {
                    program_run_dll(botnana, program);
                }
            }
        }

        private void btnProgramAbort_Click(object sender, EventArgs e)
        {
            if (motion_state != 0)
            {
                new Thread(() => System.Windows.Forms.MessageBox.Show("Not Motion IDLE !!")).Start();
            }
            else
            {
                script_evaluate_dll(botnana, "reset-machining -nc marker -nc");
            }
        }

        private void btnProgramResume_Click(object sender, EventArgs e)
        {
            script_evaluate_dll(botnana, "start-machining");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (path_id > 0) {
                dataGridView1.CurrentCell = dataGridView1.Rows[path_id].Cells[0];
            }
            script_evaluate_dll(botnana, ".servo-on .motion-state .nc-task");
            if (servo_on == 0) {
                btnServoOn.BackColor = SystemColors.Control;
                btnServoOff.BackColor = Color.Red;
            } else
            {
                btnServoOn.BackColor = Color.LightGreen;
                btnServoOff.BackColor = SystemColors.Control;
            }

            textNCOwner.Text = nc_owner.ToString();
            textNCSuspended.Text = nc_suspended.ToString();
            textMotionState.Text = motion_state.ToString();
            textAxis1Homed.Text = axis_homed_1.ToString();
            textAxis2Homed.Text = axis_homed_2.ToString();
            textAxis3Homed.Text = axis_homed_3.ToString();
            if (config_request)
            {
                textDevicesOk.Text = devices_ok.ToString();
                textRapidTravelsRate.Text = (rapid_travels_rate * 1000 * 60).ToString("F1");
                textMachiningRate.Text = (machining_rate * 1000 * 60).ToString("F1");
                textHomingV1X.Text = axis_homing_v1x.ToString();
                textHomingV1Y.Text = axis_homing_v1y.ToString();
                textHomingV1Z.Text = axis_homing_v1z.ToString();
                textHomingV2X.Text = axis_homing_v2x.ToString();
                textHomingV2Y.Text = axis_homing_v2y.ToString();
                textHomingV2Z.Text = axis_homing_v2z.ToString();
                textHomingMethodX.Text = axis_homing_methodx.ToString();
                textHomingMethodY.Text = axis_homing_methody.ToString();
                textHomingMethodZ.Text = axis_homing_methodz.ToString();
                config_request = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            script_evaluate_dll(botnana, "+servo-on");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            script_evaluate_dll(botnana, "-servo-on");
        }
            
        private void btnHomingStart_Click(object sender, EventArgs e)
        {
            string script = "";
            if (cbHoming1stX.Checked) {
                script += @" 1 1 +homing-axis";
                cbHoming1stX.Checked = false;
            }

            if (cbHoming1stY.Checked)
            {
                script += @" 2 1 +homing-axis";
                cbHoming1stY.Checked = false;
            }

            if (cbHoming1stZ.Checked)
            {
                script += @" 3 1 +homing-axis";
                cbHoming1stZ.Checked = false;
            }

            if (cbHoming2ndX.Checked)
            {
                script += @" 1 2 +homing-axis";
                cbHoming2ndX.Checked = false;
            }

            if (cbHoming2ndY.Checked)
            {
                script += @" 2 2 +homing-axis";
                cbHoming2ndY.Checked = false;
            }

            if (cbHoming2ndZ.Checked)
            {
                script += @" 3 2 +homing-axis";
                cbHoming2ndZ.Checked = false;
            }

            script_evaluate_dll(botnana, script + @" start-homing");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            script_evaluate_dll(botnana, @" ems-stop -nc marker -nc");
        }


        private string oldTextHomingV1X;
        private string oldTextHomingV1Y;
        private string oldTextHomingV1Z;
        private string oldTextHomingV2X;
        private string oldTextHomingV2Y;
        private string oldTextHomingV2Z;
        private string oldTextHomingMethodX;
        private string oldTextHomingMethodY;
        private string oldTextHomingMethodZ;
        private string oldRapidTravelsRate;
        private string oldMachiningRate;



        private void textHomingV1X_Setting()
        {
            if (oldTextHomingV1X != textHomingV1X.Text)
            {
                Int32 v;
                if (Int32.TryParse(textHomingV1X.Text, out v))
                {
                    if (v > 0)
                    {
                        oldTextHomingV1X = textHomingV1X.Text;
                        script_evaluate_dll(botnana, oldTextHomingV1X + @" 1  axis-homing-v1! .config");
                    }
                    else
                    {
                        textHomingV1X.Text = oldTextHomingV1X;
                    }
                }
                else
                {
                    textHomingV1X.Text = oldTextHomingV1X;
                }
            }
        }

        private void textHomingV1Y_Setting()
        {
            if (oldTextHomingV1Y != textHomingV1Y.Text)
            {
                Int32 v;
                if (Int32.TryParse(textHomingV1Y.Text, out v))
                {
                    if (v > 0)
                    {
                        oldTextHomingV1Y = textHomingV1Y.Text;
                        script_evaluate_dll(botnana, oldTextHomingV1Y + @" 2  axis-homing-v1! .config");
                    }
                    else
                    {
                        textHomingV1Y.Text = oldTextHomingV1Y;
                    }
                }
                else
                {
                    textHomingV1Y.Text = oldTextHomingV1Y;
                }
            }
        }

        private void textHomingV1Z_Setting()
        {
            if (oldTextHomingV1Z != textHomingV1Z.Text)
            {
                Int32 v;
                if (Int32.TryParse(textHomingV1Z.Text, out v))
                {
                    if (v > 0)
                    {
                        oldTextHomingV1Z = textHomingV1Z.Text;
                        script_evaluate_dll(botnana, oldTextHomingV1Z + @" 3  axis-homing-v1! .config");
                    }
                    else
                    {
                        textHomingV1Z.Text = oldTextHomingV1Z;
                    }
                }
                else
                {
                    textHomingV1Z.Text = oldTextHomingV1Z;
                }
            }
        }


        private void textHomingV2X_Setting()
        {
            if (oldTextHomingV2X != textHomingV2X.Text)
            {
                Int32 v;
                if (Int32.TryParse(textHomingV2X.Text, out v))
                {
                    if (v > 0)
                    {
                        oldTextHomingV2X = textHomingV2X.Text;
                        script_evaluate_dll(botnana, oldTextHomingV2X + @" 1  axis-homing-v2! .config");
                    }
                    else
                    {
                        textHomingV2X.Text = oldTextHomingV2X;
                    }
                }
                else
                {
                    textHomingV2X.Text = oldTextHomingV2X;
                }
            }
        }

        private void textHomingV2Y_Setting()
        {
            if (oldTextHomingV2Y != textHomingV2Y.Text)
            {
                Int32 v;
                if (Int32.TryParse(textHomingV2Y.Text, out v))
                {
                    if (v > 0)
                    {
                        oldTextHomingV2Y = textHomingV2Y.Text;
                        script_evaluate_dll(botnana, oldTextHomingV2Y + @" 2  axis-homing-v2! .config");
                    }
                    else
                    {
                        textHomingV2Y.Text = oldTextHomingV2Y;
                    }
                }
                else
                {
                    textHomingV2Y.Text = oldTextHomingV2Y;
                }
            }
        }

        private void textHomingV2Z_Setting()
        {
            if (oldTextHomingV2Z != textHomingV2Z.Text)
            {
                Int32 v;
                if (Int32.TryParse(textHomingV2Z.Text, out v))
                {
                    if (v > 0)
                    {
                        oldTextHomingV2Z = textHomingV2Z.Text;
                        script_evaluate_dll(botnana, oldTextHomingV2Z + @" 3  axis-homing-v2! .config");
                    }
                    else
                    {
                        textHomingV2Z.Text = oldTextHomingV2Z;
                    }
                }
                else
                {
                    textHomingV2Z.Text = oldTextHomingV2Z;
                }
            }
        }


        private void textHomingV1X_Enter(object sender, EventArgs e)
        {
            oldTextHomingV1X = textHomingV1X.Text;
        }

        
        private void textHomingV1X_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar== (char) Keys.Enter)
            {
                textHomingV1X_Setting();
            }
        }

        private void textHomingV1X_Leave(object sender, EventArgs e)
        {
            textHomingV1X_Setting();
        }

        private void textHomingV1Y_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textHomingV1Y_Setting();
            }
        }

        private void textHomingV1Y_Enter(object sender, EventArgs e)
        {
            oldTextHomingV1Y = textHomingV1Y.Text;
        }

        private void textHomingV1Y_Leave(object sender, EventArgs e)
        {
            textHomingV1Y_Setting();
        }

        private void textHomingV1Z_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textHomingV1Z_Setting();
            }
        }

        private void textHomingV1Z_Enter(object sender, EventArgs e)
        {
            oldTextHomingV1Z = textHomingV1Z.Text;
        }

        private void textHomingV1Z_Leave(object sender, EventArgs e)
        {
            textHomingV1Z_Setting();
        }

        private void textHomingV2X_Enter(object sender, EventArgs e)
        {
            oldTextHomingV2Z = textHomingV2Z.Text;
        }

        private void textHomingV2Y_Enter(object sender, EventArgs e)
        {
            oldTextHomingV2Z = textHomingV2Z.Text;
        }

        private void textHomingV2Z_Enter(object sender, EventArgs e)
        {
            oldTextHomingV2Z = textHomingV2Z.Text;
        }

        private void textHomingV2X_Leave(object sender, EventArgs e)
        {
            textHomingV2X_Setting();
        }

        private void textHomingV2Y_Leave(object sender, EventArgs e)
        {
            textHomingV2Y_Setting();
        }

        private void textHomingV2Z_Leave(object sender, EventArgs e)
        {
            textHomingV2Z_Setting();
        }

        private void textHomingV2X_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textHomingV2X_Setting();
            }
        }

        private void textHomingV2Y_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textHomingV2Y_Setting();
            }
        }

        private void textHomingV2Z_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textHomingV2Z_Setting();
            }
        }


        private void textHomingMethodX_Setting()
        {
            if (oldTextHomingMethodX != textHomingMethodX.Text)
            {
                Int32 v;
                if (Int32.TryParse(textHomingMethodX.Text, out v))
                {
                    if (v > 0)
                    {
                        oldTextHomingMethodX = textHomingMethodX.Text;
                        script_evaluate_dll(botnana, oldTextHomingMethodX + @" 1  axis-homing-method! .config");
                    }
                    else
                    {
                        textHomingMethodX.Text = oldTextHomingMethodX;
                    }
                }
                else
                {
                    textHomingMethodX.Text = oldTextHomingMethodX;
                }
            }
        }

        private void textHomingMethodY_Setting()
        {
            if (oldTextHomingMethodY != textHomingMethodY.Text)
            {
                Int32 v;
                if (Int32.TryParse(textHomingMethodY.Text, out v))
                {
                    if (v > 0)
                    {
                        oldTextHomingMethodY = textHomingMethodY.Text;
                        script_evaluate_dll(botnana, oldTextHomingMethodY + @" 2  axis-homing-method! .config");
                    }
                    else
                    {
                        textHomingMethodY.Text = oldTextHomingMethodY;
                    }
                }
                else
                {
                    textHomingMethodY.Text = oldTextHomingMethodY;
                }
            }
        }

        private void textHomingMethodZ_Setting()
        {
            if (oldTextHomingMethodZ != textHomingMethodZ.Text)
            {
                Int32 v;
                if (Int32.TryParse(textHomingMethodZ.Text, out v))
                {
                    if (v > 0)
                    {
                        oldTextHomingMethodZ = textHomingMethodZ.Text;
                        script_evaluate_dll(botnana, oldTextHomingMethodZ + @" 3  axis-homing-method! .config");
                    }
                    else
                    {
                        textHomingMethodZ.Text = oldTextHomingMethodZ;
                    }
                }
                else
                {
                    textHomingMethodZ.Text = oldTextHomingMethodZ;
                }
            }
        }


        private void textHomingMethodX_Enter(object sender, EventArgs e)
        {
            oldTextHomingMethodX = textHomingMethodX.Text;
        }

        private void textHomingMethodY_Enter(object sender, EventArgs e)
        {
            oldTextHomingMethodY = textHomingMethodY.Text;
        }

        private void textHomingMethodZ_Enter(object sender, EventArgs e)
        {
            oldTextHomingMethodZ = textHomingMethodZ.Text;
        }

        private void textHomingMethodX_Leave(object sender, EventArgs e)
        {
            textHomingMethodX_Setting();
        }

        private void textHomingMethodY_Leave(object sender, EventArgs e)
        {
            textHomingMethodY_Setting();
        }

        private void textHomingMethodZ_Leave(object sender, EventArgs e)
        {
            textHomingMethodZ_Setting();
        }

        private void textHomingMethodX_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textHomingMethodX_Setting();
            }
        }

        private void textHomingMethodY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textHomingMethodY_Setting();
            }
        }

        private void textHomingMethodZ_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textHomingMethodZ_Setting();
            }
        }

        private void textRapidTravelsRate_setting()
        {
            if (oldRapidTravelsRate != textRapidTravelsRate.Text)
            {
                Double v;
                if (Double.TryParse(textRapidTravelsRate.Text, out v))
                {
                    if (v > 0)
                    {
                        oldRapidTravelsRate = textRapidTravelsRate.Text;
                        script_evaluate_dll(botnana, oldRapidTravelsRate + @"e mm/min rapid-travels-rate! .config");
                    }
                    else
                    {
                        textRapidTravelsRate.Text = oldRapidTravelsRate;
                    }
                }
                else
                {
                    textRapidTravelsRate.Text = oldRapidTravelsRate;
                }
            }
        }

        private void textMachiningRate_setting()
        {
            if (oldMachiningRate != textMachiningRate.Text)
            {
                Double v;
                if (Double.TryParse(textMachiningRate.Text, out v))
                {
                    if (v > 0)
                    {
                        oldMachiningRate = textMachiningRate.Text;
                        script_evaluate_dll(botnana, oldMachiningRate + @"e mm/min machining-rate! .config");
                    }
                    else
                    {
                        textMachiningRate.Text = oldMachiningRate;
                    }
                }
                else
                {
                    textMachiningRate.Text = oldMachiningRate;
                }
            }
        }

        private void textRapidTravelsRate_Enter(object sender, EventArgs e)
        {
            oldRapidTravelsRate = textRapidTravelsRate.Text;
        }

        private void textMachiningRate_Enter(object sender, EventArgs e)
        {
            oldMachiningRate = textMachiningRate.Text;
        }

        private void textRapidTravelsRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textRapidTravelsRate_setting();
            }
        }

        private void textMachiningRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textMachiningRate_setting();
            }
        }

        private void textRapidTravelsRate_Leave(object sender, EventArgs e)
        {
            textRapidTravelsRate_setting();
        }

        private void textMachiningRate_Leave(object sender, EventArgs e)
        {
            textMachiningRate_setting();
        }
    }
}

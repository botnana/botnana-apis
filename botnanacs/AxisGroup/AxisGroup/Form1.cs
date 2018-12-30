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

        // 更新 MCS 座標系
        private static HandleMessage mcs_callback = new HandleMessage(mcs_cb);
        private static double[] mcsPositions;
        private static void mcs_cb(string str)
        {
            int i = 0;
            String[] elements = Regex.Split(str, @",");
            foreach (var element in elements)
            {
                mcsPositions[i] = Convert.ToDouble(element)*1000.0;
                i++;
            }
        }

        // 更新 PCS 座標系
        private static HandleMessage pcs_callback = new HandleMessage(pcs_cb);
        private static double[] pcsPositions;
        private static void pcs_cb(string str)
        {
            int i = 0;
            String[] elements = Regex.Split(str, @",");
            foreach (var element in elements)
            {
                pcsPositions[i] = Convert.ToDouble(element)*1000.0;
                i++;
            }
        }

        // 更新軸組 PVA 資訊 
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

        // 更新目前在加工總路徑上的位置
        private static HandleMessage moveLength_callback = new HandleMessage(moveLength_cb);
        private static double moveLength;
        private static void moveLength_cb(string str)
        {
            moveLength  = Convert.ToDouble(str);
        }

        // 取得 NC 路徑編號
        private static HandleMessage pathId_callback = new HandleMessage(pathId_cb);
        private static int pathId;
        private static void pathId_cb(string str)
        {
            pathId = Int32.Parse(str);
        }

        // 取得 Servo On 狀態
        private static HandleMessage servoOn_callback = new HandleMessage(servoOn_cb);
        private static int servoOn;
        private static void servoOn_cb(string str)
        {
            servoOn = Int32.Parse(str);
        }

        // 運動狀態
        private static HandleMessage motionState_callback = new HandleMessage(motionState_cb);
        private static int motionState;
        private static void motionState_cb(string str)
        {
            motionState = Int32.Parse(str);
        }

        // 運動軸實際位置
        private static double[] axisRealPositions;
        private static HandleMessage axisRealPositionX_callback = new HandleMessage(axisRealPositionX_cb);
        private static HandleMessage axisRealPositionY_callback = new HandleMessage(axisRealPositionY_cb);
        private static HandleMessage axisRealPositionZ_callback = new HandleMessage(axisRealPositionZ_cb);
        private static void axisRealPositionX_cb(string str)
        {

            axisRealPositions[0] = Double.Parse(str);
        }
        private static void axisRealPositionY_cb(string str)
        {
            axisRealPositions[1] = Double.Parse(str);
        }
        private static void axisRealPositionZ_cb(string str)
        {
            axisRealPositions[2] = Double.Parse(str);
        }

        // 運動軸回歸機械原點的結果
        private static int[] axisHomed;
        private static HandleMessage axisHomedX_callback = new HandleMessage(axisHomedX_cb);
        private static HandleMessage axisHomedY_callback = new HandleMessage(axisHomedY_cb);
        private static HandleMessage axisHomedZ_callback = new HandleMessage(axisHomedZ_cb);
        private static void axisHomedX_cb(string str)
        {
            axisHomed[0] = Int32.Parse(str);
        }
        private static void axisHomedY_cb(string str)
        {
            axisHomed[1] = Int32.Parse(str);
        }
        private static void axisHomedZ_cb(string str)
        {
            axisHomed[1] = Int32.Parse(str);
        }

        // 呼叫 NC Task 的前景 Task ID 
        private static int ncOwner;
        private static HandleMessage ncOwner_callback = new HandleMessage(ncOwner_cb);
        private static void ncOwner_cb(string str)
        {
            ncOwner = Int32.Parse(str);
        }

        // NC Task 是否暫停的旗標
        private static HandleMessage ncSuspended_callback = new HandleMessage(ncSuspended_cb);
        private static int ncSuspended;
        private static void ncSuspended_cb(string str)
        {
            ncSuspended = Int32.Parse(str);
        }

        // SFC 邏輯中，硬體裝置檢查後的結果
        private static HandleMessage devicesOk_callback = new HandleMessage(devicesOk_cb);
        private static int devicesOk;
        private static void devicesOk_cb(string str)
        {
            devicesOk = Int32.Parse(str);
        }

        // SFC 監控機制所收集到的異警等級訊息
        private static HandleMessage monitorFailed_callback = new HandleMessage(monitorFailed_cb);
        private static int monitorFailed;
        private static void monitorFailed_cb(string str)
        {
            monitorFailed = Int32.Parse(str);
        }

        // SFC 參數部分
        private static Boolean configUpdated;
        private static double rapidTravelsRate;
        private static double machiningRate;
        private static UInt32[] axisHomingV1;
        private static UInt32[] axisHomingV2;
        private static UInt32[] axisHomingMethod;

        private static HandleMessage rapidTravelsRate_callback = new HandleMessage(rapidTravelsRate_cb);
        private static void rapidTravelsRate_cb(string str)
        {
            rapidTravelsRate = Double.Parse(str);
            configUpdated = true;
        }
        private static HandleMessage machiningRate_callback = new HandleMessage(machiningRate_cb);
        private static void machiningRate_cb(string str)
        {
            machiningRate = Double.Parse(str);
            configUpdated = true;
        }

        private static HandleMessage axisHomingV1X_callback = new HandleMessage(axisHomingV1X_cb);
        private static HandleMessage axisHomingV1Y_callback = new HandleMessage(axisHomingV1Y_cb);
        private static HandleMessage axisHomingV1Z_callback = new HandleMessage(axisHomingV1Z_cb);
        private static void axisHomingV1X_cb(string str)
        {
            axisHomingV1[0] = UInt32.Parse(str);
            configUpdated = true;
        }
        private static void axisHomingV1Y_cb(string str)
        {
            axisHomingV1[1] = UInt32.Parse(str);
            configUpdated = true;
        }
        private static void axisHomingV1Z_cb(string str)
        {
            axisHomingV1[2] = UInt32.Parse(str);
            configUpdated = true;
        }

        private static HandleMessage axisHomingV2X_callback = new HandleMessage(axisHomingV2X_cb);
        private static HandleMessage axisHomingV2Y_callback = new HandleMessage(axisHomingV2Y_cb);
        private static HandleMessage axisHomingV2Z_callback = new HandleMessage(axisHomingV2Z_cb);
        private static void axisHomingV2X_cb(string str)
        {
            axisHomingV2[0] = UInt32.Parse(str);
            configUpdated = true;
        }
        private static void axisHomingV2Y_cb(string str)
        {
            axisHomingV2[1] = UInt32.Parse(str);
            configUpdated = true;
        }
        private static void axisHomingV2Z_cb(string str)
        {
            axisHomingV2[2] = UInt32.Parse(str);
            configUpdated = true;
        }

        private static HandleMessage axisHomingMethodX_callback = new HandleMessage(axisHomingMethodX_cb);
        private static HandleMessage axisHomingMethodY_callback = new HandleMessage(axisHomingMethodY_cb);
        private static HandleMessage axisHomingMethodZ_callback = new HandleMessage(axisHomingMethodZ_cb);
        private static void axisHomingMethodX_cb(string str)
        {
            axisHomingMethod[0] = UInt32.Parse(str);
            configUpdated = true;
        }
        private static void axisHomingMethodY_cb(string str)
        {
            axisHomingMethod[1] = UInt32.Parse(str);
            configUpdated = true;
        }
        private static void axisHomingMethodZ_cb(string str)
        {
            axisHomingMethod[2] = UInt32.Parse(str);
            configUpdated = true;
        }
        
        // 收到 error 的處置
        private static HandleMessage error_callback = new HandleMessage(error_cb);
        private static void error_cb(string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("error|" + str)).Start();
        }

        // 收到 log 的處置
        private static HandleMessage log_callback = new HandleMessage(log_cb);
        private static void log_cb(string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("log|" + str)).Start();

        }

        // 目前 Botnana Control 有無 NC 程式 
        private static Boolean hasProgram = false;

        // 收到 deployed 的處置
        private static HandleMessage deployed_callback = new HandleMessage(deployed_cb);
        private static void deployed_cb(string str)
        {
            if (str == "ok")
            {
                hasProgram = true;
            }
        }

        // 載入單一 SFC 檔案 
        private static void load_sfc(string path)
        {
            string sfc = System.IO.File.ReadAllText(path, Encoding.UTF8);
            script_evaluate_dll(botnana, sfc);
        }

        // 取得 userParameter
        private static HandleMessage userParameter_callback = new HandleMessage(userParameter_cb);
        private static Boolean hasSFC;
        private static void userParameter_cb(string str)
        {
            int para = Int32.Parse(str);
            switch (para)
            {
                case 0:
                    // 設定 user parameter 為 0x10
                    // 如果此範例重新執行不會在載入以下 SFC
                    script_evaluate_dll(botnana, "$10 user-para!");
                    // 清除SFC 邏輯，載入 SFC 時會造成 real time cycle overrun，所以要暫時 ignore-overrun
                    // 載入後再執行 `reset-overrun`
                    script_evaluate_dll(botnana, "0sfc ignore-overrun");
                    load_sfc(@"..\..\config.sfc");
                    load_sfc(@"..\..\servo_on_off.sfc");
                    load_sfc(@"..\..\homing.sfc");
                    load_sfc(@"..\..\motion_state.sfc");
                    load_sfc(@"..\..\manager.sfc");
                    // 等待 SFC 設置完成
                    Thread.Sleep(10);
                    script_evaluate_dll(botnana, "reset-overrun");
                    break;
                default:
                    break;
            }
            hasSFC = true;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mcsPositions = new double[3];
            pcsPositions = new double[3];
            pva = new double[3];
            axisHomed = new int[3];
            axisRealPositions = new double[3];
            axisHomingV1 = new UInt32[3];
            axisHomingV2 = new UInt32[3];
            axisHomingMethod = new UInt32[3];

            // 首先要連線到 Botnana Control, 當收到 WS 連線錯誤, 就呼叫 on_error_callback
            botnana = botnana_connect_dll("192.168.7.2", on_ws_error_callback);
            // 創建 NC program             
            program = program_new_dll("3dtest");
            // 當收到 Botnana Control 的訊息, 就呼叫 on_message_callback
            botnana_set_on_message_cb_dll(botnana, on_message_callback);
            // 定義收到信息中指定的 Tag 時，所要呼叫的 callback 信息
            botnana_set_tag_cb_dll(botnana, "MCS.1", 0, mcs_callback);
            botnana_set_tag_cb_dll(botnana, "PCS.1", 0, pcs_callback);
            botnana_set_tag_cb_dll(botnana, "pva.1", 0, pva_callback);
            botnana_set_tag_cb_dll(botnana, "move_length.1", 0, moveLength_callback);
            botnana_set_tag_cb_dll(botnana, "path_id.1", 0, pathId_callback);
            botnana_set_tag_cb_dll(botnana, "servo_on", 0, servoOn_callback);
            botnana_set_tag_cb_dll(botnana, "motion_state", 0, motionState_callback);
            botnana_set_tag_cb_dll(botnana, "axis_corrected_position.1", 0, axisRealPositionX_callback);
            botnana_set_tag_cb_dll(botnana, "axis_corrected_position.2", 0, axisRealPositionY_callback);
            botnana_set_tag_cb_dll(botnana, "axis_corrected_position.3", 0, axisRealPositionZ_callback);
            botnana_set_tag_cb_dll(botnana, "axis_homed.1", 0, axisHomedX_callback);
            botnana_set_tag_cb_dll(botnana, "axis_homed.2", 0, axisHomedY_callback);
            botnana_set_tag_cb_dll(botnana, "axis_homed.3", 0, axisHomedZ_callback);
            botnana_set_tag_cb_dll(botnana, "nc_owner", 0, ncOwner_callback);
            botnana_set_tag_cb_dll(botnana, "nc_suspended", 0, ncSuspended_callback);
            botnana_set_tag_cb_dll(botnana, "devices_ok", 0, devicesOk_callback);
            botnana_set_tag_cb_dll(botnana, "monitor_failed", 0, monitorFailed_callback);
            botnana_set_tag_cb_dll(botnana, "rapid_travels_rate", 0, rapidTravelsRate_callback);
            botnana_set_tag_cb_dll(botnana, "machining_rate", 0, machiningRate_callback);
            botnana_set_tag_cb_dll(botnana, "axis_homing_v1.1", 0, axisHomingV1X_callback);
            botnana_set_tag_cb_dll(botnana, "axis_homing_v1.2", 0, axisHomingV1Y_callback);
            botnana_set_tag_cb_dll(botnana, "axis_homing_v1.3", 0, axisHomingV1Z_callback);
            botnana_set_tag_cb_dll(botnana, "axis_homing_v2.1", 0, axisHomingV2X_callback);
            botnana_set_tag_cb_dll(botnana, "axis_homing_v2.2", 0, axisHomingV2Y_callback);
            botnana_set_tag_cb_dll(botnana, "axis_homing_v2.3", 0, axisHomingV2Z_callback);
            botnana_set_tag_cb_dll(botnana, "axis_homing_method.1", 0, axisHomingMethodX_callback);
            botnana_set_tag_cb_dll(botnana, "axis_homing_method.2", 0, axisHomingMethodY_callback);
            botnana_set_tag_cb_dll(botnana, "axis_homing_method.3", 0, axisHomingMethodZ_callback);
            botnana_set_tag_cb_dll(botnana, "error", 0, error_callback);
            botnana_set_tag_cb_dll(botnana, "log", 0, log_callback);
            botnana_set_tag_cb_dll(botnana, "user_parameter", 0, userParameter_callback);
            botnana_set_tag_cb_dll(botnana, "deployed", 0, deployed_callback);

            // 要求  Botnana Control 送出 user parameter 訊息
            script_evaluate_dll(botnana, ".user-para");
            
            // 初始化 NC program 內容
            DataGridViewRowCollection rows = ncProgram.Rows;
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
            
            // 設置 timer
            timer1.Interval = 50;
            timer1.Enabled = true;
            timer2.Interval = 500;
            timer2.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 取得 group 1, axis 1, axis 2, axis 3 狀態
            script_evaluate_dll(botnana, "1 .group 1 .axis 2 .axis 3 .axis");

            // 更新視窗原件內容
            labelMCS1.Text = mcsPositions[0].ToString("F4");
            labelMCS2.Text = mcsPositions[1].ToString("F4");
            labelMCS3.Text = mcsPositions[2].ToString("F4");
            labelPCS1.Text = pcsPositions[0].ToString("F4");
            labelPCS2.Text = pcsPositions[1].ToString("F4");
            labelPCS3.Text = pcsPositions[2].ToString("F4");
            textNextP.Text = (pva[0] * 1000.0).ToString("F1");
            textNextV.Text = (pva[1]*1000.0*60.0).ToString("F1");
            textPathP.Text = (moveLength * 1000.0 ).ToString("F1");
            textAxisP1.Text = (axisRealPositions[0] * 1000.0).ToString("F4");
            textAxisP2.Text = (axisRealPositions[1] * 1000.0).ToString("F4");
            textAxisP3.Text = (axisRealPositions[2] * 1000.0).ToString("F4");
        }

        // Start Jogging 指令　
        private void btnJoggingGo_Click(object sender, EventArgs e)
        {
            // 如果沒有設定的運動軸，就將目標設定為目前位置
            string mcs_x = mcsPositions[0].ToString("F4");
            string mcs_y = mcsPositions[1].ToString("F4");
            string mcs_z = mcsPositions[2].ToString("F4");

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

        // 停止運動
        private void btnJogStop_Click(object sender, EventArgs e)
        {
            script_evaluate_dll(botnana, "stop-motion");
        }

        
        // 送出 NC 程式到 Botnana Control
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (motionState == 3) {
                new Thread(() => System.Windows.Forms.MessageBox.Show("Motion in Machining !!")).Start();
            } else if(ncSuspended == 1) {
                new Thread(() => System.Windows.Forms.MessageBox.Show("Machining Feed Hold!!")).Start();
            } else if (hasProgram)
            {
                new Thread(() => System.Windows.Forms.MessageBox.Show("Has program !!")).Start();
            }
            else
            {
                //deploy 後要等待 deployed|ok 訊息回傳後才能執行 pm_run(botnana, program);
                program_clear_dll(program);

                // 判斷 G92
                if (ncProgram[2, 0].Value == null || ncProgram[3, 0].Value == null || ncProgram[4, 0].Value == null)
                {
                    new Thread(() => System.Windows.Forms.MessageBox.Show("Invalid G92")).Start();
                }
                else
                {
                    double[] positions = new double[3];
                    double[] next_positions = new double[3];
                    double feedrate = 0.015;
                    positions[0] = double.Parse(ncProgram[2, 0].Value.ToString());
                    positions[1] = double.Parse(ncProgram[3, 0].Value.ToString());
                    positions[2] = double.Parse(ncProgram[4, 0].Value.ToString());

                    // 清除先前定義的program
                    script_evaluate_dll(botnana, "-nc marker -nc");          
                    
                    // 切換到 Group 1，清除先前路徑
                    program_line_dll(program, "1 group! 0path ");

                    // 設定 path id 與 mode
                    program_line_dll(program, "1 path-id! 92 path-mode!");

                    // G92 對應的指令
                    program_line_dll(program, positions[0].ToString("F5") + "e mm " + positions[1].ToString("F5") + "e mm " + positions[2].ToString("F5") + "e mm move3d");

                    for (int i = 1; i < 10; i++)
                    {
                        // 從表格取得目標位置
                        for (int j = 0; j < 3; j++)
                        {
                            next_positions[j] = positions[j];

                            if (ncProgram[2 + j, i].Value != null)
                            {
                                next_positions[j] = double.Parse(ncProgram[2 + j, i].Value.ToString());

                            }
                        }

                        // 從表格取得 feedrate
                        if (ncProgram[5, i].Value != null)
                        {
                            feedrate = double.Parse(ncProgram[5, i].Value.ToString());
                            program_line_dll(program, "1 group! " + feedrate.ToString("F2") + "e mm/min feedrate!");
                        }

                        // 如果是新的目標位置，就插入直線路徑
                        // 如果需要圓弧就要使用 `helix3d` 指令
                        if (next_positions[0] != positions[0]
                        || next_positions[1] != positions[1]
                        || next_positions[2] != positions[2])
                        {

                            // 取得路徑上的運動模式 (例如:加工或是非加工)
                            int mode = Int32.Parse(ncProgram[1, i].Value.ToString());
                            positions[0] = next_positions[0];
                            positions[1] = next_positions[1];
                            positions[2] = next_positions[2];

                            program_line_dll(program, "1 group! ");
                            // 設定 path id and mode
                            program_line_dll(program, i.ToString() + @" path-id! " + mode.ToString() + @" path-mode!");
                            // 插入直線路徑
                            program_line_dll(program, positions[0].ToString("F5") + "e mm " + positions[1].ToString("F5") + "e mm " + positions[2].ToString("F5") + "e mm line3d");

                        }

                    }

                    // 等待 Group 1 走到路徑終點
                    program_line_dll(program, "begin 1 group! gend? not while pause repeat");
                    // 通知 machining state NC 程式執行完畢
                    program_line_dll(program, "true machining-finished !");
                    
                    // 送到 NC Task 編譯 
                    program_deploy_dll(botnana, program);
                }
            }
        }

        // 通知 NC Task 開始/繼續 解譯與執行程式
        private void btnProgramRun_Click(object sender, EventArgs e)
        {
            if (! hasProgram)
            {
                new Thread(() => System.Windows.Forms.MessageBox.Show("No program !!")).Start();
            }
            else
            {
                script_evaluate_dll(botnana, "start-machining");
                if (ncSuspended == 0)
                {
                    program_run_dll(botnana, program);
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            // 更新 NC program 上的行號指令
            if (pathId > 0) {
                ncProgram.CurrentCell = ncProgram.Rows[pathId].Cells[0];
            }

            //  載入 SFC 後才發送取得狀態的指令
            if (hasSFC)
            {
                string cmd;
                cmd = ".servo-on .motion-state .nc-task .monitor";
                if (paraSetting || devicesOk == 0)
                {
                    cmd += " .config";
                    paraSetting = false;
                }
                script_evaluate_dll(botnana, cmd);
            }

            if (servoOn == 0) {
                btnServoOn.BackColor = SystemColors.Control;
                btnServoOff.BackColor = Color.Red;
            } else {
                btnServoOn.BackColor = Color.LightGreen;
                btnServoOff.BackColor = SystemColors.Control;
            }

            textNCOwner.Text = ncOwner.ToString();
            textNCSuspended.Text = ncSuspended.ToString();
            textMotionState.Text = motionState.ToString();
            textAxis1Homed.Text = axisHomed[0].ToString();
            textAxis2Homed.Text = axisHomed[1].ToString();
            textAxis3Homed.Text = axisHomed[2].ToString();
            textMonitorFailed.Text = monitorFailed.ToString();
            if (configUpdated)
            {
                textDevicesOk.Text = devicesOk.ToString();
                textRapidTravelsRate.Text = (rapidTravelsRate * 1000 * 60).ToString("F1");
                textMachiningRate.Text = (machiningRate * 1000 * 60).ToString("F1");
                textHomingV1X.Text = axisHomingV1[0].ToString();
                textHomingV1Y.Text = axisHomingV1[1].ToString();
                textHomingV1Z.Text = axisHomingV1[2].ToString();
                textHomingV2X.Text = axisHomingV2[0].ToString();
                textHomingV2Y.Text = axisHomingV2[1].ToString();
                textHomingV2Z.Text = axisHomingV2[2].ToString();
                textHomingMethodX.Text = axisHomingMethod[0].ToString();
                textHomingMethodY.Text = axisHomingMethod[1].ToString();
                textHomingMethodZ.Text = axisHomingMethod[2].ToString();
                configUpdated = false;
            }
        }

        // 發出 SFC Servo On 的要求 
        private void button1_Click(object sender, EventArgs e)
        {
            script_evaluate_dll(botnana, "+servo-on");
        }

        // 發出 SFC Servo Off 的要求 
        private void button2_Click(object sender, EventArgs e)
        {
            script_evaluate_dll(botnana, "-servo-on");
        }

        // 發出 SFC Start Homing 的要求 
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

        // 發出 SFC 緊急停止的要求
        private void button1_Click_1(object sender, EventArgs e)
        {
            // 一併清除Botnana Control 內的 NC program
            script_evaluate_dll(botnana, @" ems-stop -nc marker -nc");
            hasProgram = false;
        }

        // 處理 SFC 中的參數
        private Boolean paraSetting;
        private void textHomingV1X_Setting()
        {
            Int32 v;
            if (Int32.TryParse(textHomingV1X.Text, out v))
            {
                if (v > 0)
                {
                    script_evaluate_dll(botnana, textHomingV1X.Text + @" 1  axis-homing-v1!");
                }
            }
            paraSetting = true;
        }

        private void textHomingV1Y_Setting()
        {
            Int32 v;
            if (Int32.TryParse(textHomingV1Y.Text, out v))
            {
                if (v > 0)
                {
                    script_evaluate_dll(botnana, textHomingV1Y.Text + @" 2  axis-homing-v1!");
                }
            }
            paraSetting = true;
        }

        private void textHomingV1Z_Setting()
        {
            Int32 v;
            if (Int32.TryParse(textHomingV1Z.Text, out v))
            {
                if (v > 0)
                {
                    script_evaluate_dll(botnana, textHomingV1Z.Text + @" 3  axis-homing-v1!");
                }
                    
            }
            paraSetting = true;
        }
        
        private void textHomingV2X_Setting()
        {
            Int32 v;
            if (Int32.TryParse(textHomingV2X.Text, out v))
            {
                if (v > 0)
                {
                    script_evaluate_dll(botnana, textHomingV2X.Text + @" 1  axis-homing-v2!");
                }
            }
            paraSetting = true;
        }

        private void textHomingV2Y_Setting()
        {
            Int32 v;
            if (Int32.TryParse(textHomingV2Y.Text, out v))
            {
                if (v > 0)
                {
                    script_evaluate_dll(botnana, textHomingV2Y.Text + @" 2  axis-homing-v2!");
                }
            }
            paraSetting = true;
        }

        private void textHomingV2Z_Setting()
        {
            Int32 v;
            if (Int32.TryParse(textHomingV2Z.Text, out v))
            {
                if (v > 0)
                {
                    script_evaluate_dll(botnana, textHomingV2Z.Text + @" 3  axis-homing-v2!");
                }
            }
            paraSetting = true;
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

        private void textHomingV1Z_Leave(object sender, EventArgs e)
        {
            textHomingV1Z_Setting();
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
            Int32 v;
            if (Int32.TryParse(textHomingMethodX.Text, out v))
            {
                if (v > 0)
                {
                    script_evaluate_dll(botnana, textHomingMethodX.Text + @" 1  axis-homing-method!");
                }
            }
            paraSetting = true;
        }

        private void textHomingMethodY_Setting()
        {
            Int32 v;
            if (Int32.TryParse(textHomingMethodY.Text, out v))
            {
                if (v > 0)
                {
                    script_evaluate_dll(botnana, textHomingMethodY.Text + @" 2  axis-homing-method!");
                }
            }
            paraSetting = true;
        }

        private void textHomingMethodZ_Setting()
        {
            Int32 v;
            if (Int32.TryParse(textHomingMethodZ.Text, out v))
            {
                if (v > 0)
                {
                    script_evaluate_dll(botnana, textHomingMethodZ.Text + @" 3  axis-homing-method!");
                }
            }
            paraSetting = true;
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
            Double v;
            if (Double.TryParse(textRapidTravelsRate.Text, out v))
            {
                if (v > 0)
                {
                    script_evaluate_dll(botnana, textRapidTravelsRate.Text.Trim() + @"e mm/min rapid-travels-rate!");
                }
            }
            paraSetting = true;
        }

        private void textMachiningRate_setting()
        {
            Double v;
            if (Double.TryParse(textMachiningRate.Text, out v))
            {
                if (v > 0)
                {
                    script_evaluate_dll(botnana, textMachiningRate.Text.Trim() + @"e mm/min machining-rate! .config");
                }
            }
            paraSetting = true;
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

        // failure Ack.
        private void button2_Click_1(object sender, EventArgs e)
        {
            script_evaluate_dll(botnana, @"ack-monitor");
        }

        // reset program 
        private void btnProgramReset_Click(object sender, EventArgs e)
        {
            if (motionState != 0)
            {
                new Thread(() => System.Windows.Forms.MessageBox.Show("Not Motion IDLE !!")).Start();
            }
            else
            {
                script_evaluate_dll(botnana, "reset-machining -nc marker -nc");
                hasProgram = false;
            }
        }
    }
}

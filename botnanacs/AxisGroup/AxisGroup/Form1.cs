using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using BotnanaLib;

namespace AxisGroup
{
    public partial class Form1 : Form
    {
        private static Botnana bot;

        // 因為會有垃圾收集的關係，所以callback 要這樣宣告
        private static HandleMessage onWSError = new HandleMessage(onWSErrorCB);
        private static void onWSErrorCB(string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("WS error : " + str)).Start();
        }

        private static HandleMessage onMessage = new HandleMessage(OnMessageCB);
        private static void OnMessageCB(string str)
        {
            //new Thread(() => System.Windows.Forms.MessageBox.Show(str)).Start();
        }

        // 更新 MCS 座標系
        private static HandleMessage onMcs = new HandleMessage(OnMcsCB);
        private static double[] mcsPositions;
        private static void OnMcsCB(string str)
        {
            int i = 0;
            String[] elements = Regex.Split(str, @",");
            foreach (var element in elements)
            {
                mcsPositions[i] = Convert.ToDouble(element) * 1000.0;
                i++;
            }
        }

        // 更新 PCS 座標系
        private static HandleMessage onPcs = new HandleMessage(OnPcsCB);
        private static double[] pcsPositions;
        private static void OnPcsCB(string str)
        {
            int i = 0;
            String[] elements = Regex.Split(str, @",");
            foreach (var element in elements)
            {
                pcsPositions[i] = Convert.ToDouble(element) * 1000.0;
                i++;
            }
        }

        // 更新軸組 PVA 資訊 
        private static HandleMessage onPva = new HandleMessage(OnPvaCB);
        private static double[] pva;
        private static void OnPvaCB(string str)
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
        private static HandleMessage onMoveLength = new HandleMessage(OnMoveLengthCB);
        private static double moveLength;
        private static void OnMoveLengthCB(string str)
        {
            moveLength = Convert.ToDouble(str);
        }

        // 取得 NC 路徑編號
        private static HandleMessage onPathId = new HandleMessage(OnPathIdCB);
        private static int pathId;
        private static void OnPathIdCB(string str)
        {
            pathId = Int32.Parse(str);
        }

        // 取得 Servo On 狀態
        private static HandleMessage onServoOn = new HandleMessage(OnServoOnCB);
        private static int servoOn;
        private static void OnServoOnCB(string str)
        {
            servoOn = Int32.Parse(str);
        }

        // 運動狀態
        private static HandleMessage onMotionState = new HandleMessage(onMotionStateCB);
        private static int motionState;
        private static void onMotionStateCB(string str)
        {
            motionState = Int32.Parse(str);
        }

        // 運動軸實際位置
        private static double[] axisRealPositions;
        private static HandleMessage onAxisRealPositionX = new HandleMessage(OnAxisRealPositionXCB);
        private static HandleMessage onAxisRealPositionY = new HandleMessage(OnAxisRealPositionYCB);
        private static HandleMessage onAxisRealPositionZ = new HandleMessage(OnAxisRealPositionZCB);
        private static void OnAxisRealPositionXCB(string str)
        {

            axisRealPositions[0] = Double.Parse(str);
        }
        private static void OnAxisRealPositionYCB(string str)
        {
            axisRealPositions[1] = Double.Parse(str);
        }
        private static void OnAxisRealPositionZCB(string str)
        {
            axisRealPositions[2] = Double.Parse(str);
        }

        // 運動軸回歸機械原點的結果
        private static int[] axisHomed;
        private static HandleMessage onAxisHomedX = new HandleMessage(OnAxisHomedXCB);
        private static HandleMessage onAxisHomedY = new HandleMessage(OnAxisHomedYCB);
        private static HandleMessage onAxisHomedZ = new HandleMessage(OnAxisHomedZCB);
        private static void OnAxisHomedXCB(string str)
        {
            axisHomed[0] = Int32.Parse(str);
        }
        private static void OnAxisHomedYCB(string str)
        {
            axisHomed[1] = Int32.Parse(str);
        }
        private static void OnAxisHomedZCB(string str)
        {
            axisHomed[2] = Int32.Parse(str);
        }

        // 呼叫 NC Task 的前景 Task ID 
        private static int ncOwner;
        private static HandleMessage onNcOwner = new HandleMessage(OnNcOwnerCB);
        private static void OnNcOwnerCB(string str)
        {
            ncOwner = Int32.Parse(str);
        }

        // NC Task 是否暫停的旗標
        private static HandleMessage onNcSuspended = new HandleMessage(OnNcSuspendedCB);
        private static int ncSuspended;
        private static void OnNcSuspendedCB(string str)
        {
            ncSuspended = Int32.Parse(str);
        }

        // SFC 邏輯中，硬體裝置檢查後的結果
        private static HandleMessage onDevicesOk = new HandleMessage(OnDevicesOkCB);
        private static int devicesOk;
        private static void OnDevicesOkCB(string str)
        {
            devicesOk = Int32.Parse(str);
        }

        // SFC 監控機制所收集到的異警等級訊息
        private static HandleMessage onMonitorFailed = new HandleMessage(OnMonitorFailedCB);
        private static int monitorFailed;
        private static void OnMonitorFailedCB(string str)
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

        private static HandleMessage onRapidTravelsRate = new HandleMessage(OnRapidTravelsRateCB);
        private static void OnRapidTravelsRateCB(string str)
        {
            rapidTravelsRate = Double.Parse(str);
            configUpdated = true;
        }
        private static HandleMessage onMachiningRate = new HandleMessage(OnMachiningRateCB);
        private static void OnMachiningRateCB(string str)
        {
            machiningRate = Double.Parse(str);
            configUpdated = true;
        }

        private static HandleMessage onAxisHomingV1X = new HandleMessage(OnAxisHomingV1XCB);
        private static HandleMessage onAxisHomingV1Y = new HandleMessage(OnAxisHomingV1YCB);
        private static HandleMessage onAxisHomingV1Z = new HandleMessage(OnAxisHomingV1ZCB);
        private static void OnAxisHomingV1XCB(string str)
        {
            axisHomingV1[0] = UInt32.Parse(str);
            configUpdated = true;
        }
        private static void OnAxisHomingV1YCB(string str)
        {
            axisHomingV1[1] = UInt32.Parse(str);
            configUpdated = true;
        }
        private static void OnAxisHomingV1ZCB(string str)
        {
            axisHomingV1[2] = UInt32.Parse(str);
            configUpdated = true;
        }

        private static HandleMessage onAxisHomingV2X = new HandleMessage(OnAxisHomingV2XCB);
        private static HandleMessage onAxisHomingV2Y = new HandleMessage(OnAxisHomingV2YCB);
        private static HandleMessage onAxisHomingV2Z = new HandleMessage(OnAxisHomingV2ZCB);
        private static void OnAxisHomingV2XCB(string str)
        {
            axisHomingV2[0] = UInt32.Parse(str);
            configUpdated = true;
        }
        private static void OnAxisHomingV2YCB(string str)
        {
            axisHomingV2[1] = UInt32.Parse(str);
            configUpdated = true;
        }
        private static void OnAxisHomingV2ZCB(string str)
        {
            axisHomingV2[2] = UInt32.Parse(str);
            configUpdated = true;
        }

        private static HandleMessage onAxisHomingMethodX = new HandleMessage(OnAxisHomingMethodXCB);
        private static HandleMessage onAxisHomingMethodY = new HandleMessage(OnAxisHomingMethodYCB);
        private static HandleMessage onAxisHomingMethodZ = new HandleMessage(OnAxisHomingMethodZCB);
        private static void OnAxisHomingMethodXCB(string str)
        {
            axisHomingMethod[0] = UInt32.Parse(str);
            configUpdated = true;
        }
        private static void OnAxisHomingMethodYCB(string str)
        {
            axisHomingMethod[1] = UInt32.Parse(str);
            configUpdated = true;
        }
        private static void OnAxisHomingMethodZCB(string str)
        {
            axisHomingMethod[2] = UInt32.Parse(str);
            configUpdated = true;
        }

        // 收到 error 的處置
        private static HandleMessage onError = new HandleMessage(onErrorCB);
        private static void onErrorCB(string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("error|" + str)).Start();
        }

        // 收到 log 的處置
        private static HandleMessage onLog = new HandleMessage(OnLogCB);
        private static void OnLogCB(string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("log|" + str)).Start();

        }

        // 目前 Botnana Control 有無 NC 程式 
        private static Boolean hasProgram = false;

        // 收到 deployed 的處置
        private static HandleMessage onDeployed = new HandleMessage(OnDeployedCB);
        private static void OnDeployedCB(string str)
        {
            if (str == "ok")
            {
                hasProgram = true;
            }
        }

        // 取得 userParameter
        private static HandleMessage onUserParameter = new HandleMessage(OnUserParameterCB);
        private static Boolean hasSFC;
        private static void OnUserParameterCB(string str)
        {
            int para = Int32.Parse(str);
            switch (para)
            {
                case 0:
                    // 設定 user parameter 為 0x10
                    // 如果此範例重新執行不會在載入以下 SFC
                    bot.EvaluateScript("$10 user-para!");
                    // 清除SFC 邏輯，載入 SFC 時會造成 real time cycle overrun，所以要暫時 ignore-overrun
                    // 載入後再執行 `reset-overrun`
                    bot.EvaluateScript("0sfc ignore-overrun");
                    bot.LoadSFC(@"..\..\config.sfc");
                    bot.LoadSFC(@"..\..\servo_on_off.sfc");
                    bot.LoadSFC(@"..\..\homing.sfc");
                    bot.LoadSFC(@"..\..\motion_state.sfc");
                    bot.LoadSFC(@"..\..\manager.sfc");
                    // 等待 SFC 設置完成
                    Thread.Sleep(500);
                    bot.EvaluateScript("reset-overrun");
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
            Process thisProc = Process.GetCurrentProcess();
            thisProc.PriorityClass = ProcessPriorityClass.RealTime;
            mcsPositions = new double[3];
            pcsPositions = new double[3];
            pva = new double[3];
            axisHomed = new int[3];
            axisRealPositions = new double[3];
            axisHomingV1 = new UInt32[3];
            axisHomingV2 = new UInt32[3];
            axisHomingMethod = new UInt32[3];

            // 首先要連線到 Botnana Control, 當收到 WS 連線錯誤, 就呼叫 on_error_callback
            bot = new Botnana("192.168.7.2");
            bot.SetOnErrorCB(onWSError);

            // 當收到 Botnana Control 的訊息, 就呼叫 on_message_callback
            bot.SetOnMessageCB(onMessage);
            // 定義收到信息中指定的 Tag 時，所要呼叫的 callback 信息
            bot.SetTagCB("MCS.1", 0, onMcs);
            bot.SetTagCB("PCS.1", 0, onPcs);
            bot.SetTagCB("pva.1", 0, onPva);
            bot.SetTagCB("move_length.1", 0, onMoveLength);
            bot.SetTagCB("path_id.1", 0, onPathId);
            bot.SetTagCB("servo_on", 0, onServoOn);
            bot.SetTagCB("motion_state", 0, onMotionState);
            bot.SetTagCB("axis_corrected_position.1", 0, onAxisRealPositionX);
            bot.SetTagCB("axis_corrected_position.2", 0, onAxisRealPositionY);
            bot.SetTagCB("axis_corrected_position.3", 0, onAxisRealPositionZ);
            bot.SetTagCB("axis_homed.1", 0, onAxisHomedX);
            bot.SetTagCB("axis_homed.2", 0, onAxisHomedY);
            bot.SetTagCB("axis_homed.3", 0, onAxisHomedZ);
            bot.SetTagCB("nc_owner", 0, onNcOwner);
            bot.SetTagCB("nc_suspended", 0, onNcSuspended);
            bot.SetTagCB("devices_ok", 0, onDevicesOk);
            bot.SetTagCB("monitor_failed", 0, onMonitorFailed);
            bot.SetTagCB("rapid_travels_rate", 0, onRapidTravelsRate);
            bot.SetTagCB("machining_rate", 0, onMachiningRate);
            bot.SetTagCB("axis_homing_v1.1", 0, onAxisHomingV1X);
            bot.SetTagCB("axis_homing_v1.2", 0, onAxisHomingV1Y);
            bot.SetTagCB("axis_homing_v1.3", 0, onAxisHomingV1Z);
            bot.SetTagCB("axis_homing_v2.1", 0, onAxisHomingV2X);
            bot.SetTagCB("axis_homing_v2.2", 0, onAxisHomingV2Y);
            bot.SetTagCB("axis_homing_v2.3", 0, onAxisHomingV2Z);
            bot.SetTagCB("axis_homing_method.1", 0, onAxisHomingMethodX);
            bot.SetTagCB("axis_homing_method.2", 0, onAxisHomingMethodY);
            bot.SetTagCB("axis_homing_method.3", 0, onAxisHomingMethodZ);
            bot.SetTagCB("error", 0, onError);
            bot.SetTagCB("log", 0, onLog);
            bot.SetTagCB("user_parameter", 0, onUserParameter);
            bot.SetTagCB("deployed", 0, onDeployed);

            bot.Connect();
            Thread.Sleep(1000);
            // 要求  Botnana Control 送出 user parameter 訊息
            bot.EvaluateScript(".user-para");

            // 初始化 NC program 內容
            DataGridViewRowCollection rows = ncProgram.Rows;
            rows.Add(new Object[] { "1", "92", "0.0", "0.0", "0.0", "900.0" });
            rows.Add(new Object[] { "2", "01", "10", null, null, "500" });
            rows.Add(new Object[] { "3", "01", "20", null, null, "500" });
            rows.Add(new Object[] { "4", "01", "30", null, null, "500" });
            rows.Add(new Object[] { "5", "01", "40", null, null, "500" });
            rows.Add(new Object[] { "6", "01", "50", null, null, "600" });
            rows.Add(new Object[] { "7", "01", "60", null, null, "700" });
            rows.Add(new Object[] { "8", "01", "70", null, null, "800" });
            rows.Add(new Object[] { "9", "01", "80", null, null, "900" });
            rows.Add(new Object[] { "10", "01", "90", null, null, "1000" });

            // 設置 timer
            timer1.Interval = 50;
            timer1.Enabled = true;
            timer2.Interval = 500;
            timer2.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 取得 group 1, axis 1, axis 2, axis 3 狀態
            bot.EvaluateScript("1 .group 1 .axis 2 .axis 3 .axis");

            // 更新視窗原件內容
            labelMCS1.Text = mcsPositions[0].ToString("F4");
            labelMCS2.Text = mcsPositions[1].ToString("F4");
            labelMCS3.Text = mcsPositions[2].ToString("F4");
            labelPCS1.Text = pcsPositions[0].ToString("F4");
            labelPCS2.Text = pcsPositions[1].ToString("F4");
            labelPCS3.Text = pcsPositions[2].ToString("F4");
            textNextP.Text = (pva[0] * 1000.0).ToString("F1");
            textNextV.Text = (pva[1] * 1000.0 * 60.0).ToString("F1");
            textPathP.Text = (moveLength * 1000.0).ToString("F1");
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
            bot.EvaluateScript(mcs_x + "e mm " + mcs_y + "e mm " + mcs_z + "e mm start-jogging");
        }

        // 停止運動
        private void btnJogStop_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript("stop-motion");
        }


        // 送出 NC 程式到 Botnana Control
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (motionState == 3)
            {
                new Thread(() => System.Windows.Forms.MessageBox.Show("Motion in Machining !!")).Start();
            }
            else if (ncSuspended == 1)
            {
                new Thread(() => System.Windows.Forms.MessageBox.Show("Machining Feed Hold!!")).Start();
            }
            else if (hasProgram)
            {
                new Thread(() => System.Windows.Forms.MessageBox.Show("Has program !!")).Start();
            }
            else
            {
                //deploy 後要等待 deployed|ok 訊息回傳後才能執行 pm_run(botnana, program);
                bot.ClearProgram();

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
                    bot.EvaluateScript("-nc marker -nc");

                    // 切換到 Group 1，清除先前路徑
                    bot.AddProgramLine("1 group! 0path ");

                    // 設定 path id 與 mode
                    bot.AddProgramLine("1 path-id! 92 path-mode!");

                    // G92 對應的指令
                    bot.AddProgramLine(positions[0].ToString("F5") + "e mm " + positions[1].ToString("F5") + "e mm " + positions[2].ToString("F5") + "e mm move3d");

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
                            bot.AddProgramLine("1 group! " + feedrate.ToString("F2") + "e mm/min feedrate!");
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

                            bot.AddProgramLine("1 group! ");
                            // 設定 path id and mode
                            bot.AddProgramLine(i.ToString() + @" path-id! " + mode.ToString() + @" path-mode!");
                            // 插入直線路徑
                            bot.AddProgramLine(positions[0].ToString("F5") + "e mm " + positions[1].ToString("F5") + "e mm " + positions[2].ToString("F5") + "e mm line3d");

                        }

                    }

                    // 等待 Group 1 走到路徑終點
                    bot.AddProgramLine("begin 1 group! gend? not while pause repeat");
                    // 通知 machining state NC 程式執行完畢
                    bot.AddProgramLine("true machining-finished !");

                    // 送到 NC Task 編譯 
                    bot.DeployProgram();
                }
            }
        }

        // 通知 NC Task 開始/繼續 解譯與執行程式
        private void btnProgramRun_Click(object sender, EventArgs e)
        {
            if (!hasProgram)
            {
                new Thread(() => System.Windows.Forms.MessageBox.Show("No program !!")).Start();
            }
            else
            {
                bot.EvaluateScript("start-machining");
                if (ncSuspended == 0)
                {
                    bot.RunProgram();
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            // 更新 NC program 上的行號指令
            if (pathId > 0)
            {
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
                bot.EvaluateScript(cmd);
            }

            if (servoOn == 0)
            {
                btnServoOn.BackColor = SystemColors.Control;
                btnServoOff.BackColor = Color.Red;
            }
            else
            {
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
            bot.EvaluateScript("+servo-on");
        }

        // 發出 SFC Servo Off 的要求 
        private void button2_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript("-servo-on");
        }

        // 發出 SFC Start Homing 的要求 
        private void btnHomingStart_Click(object sender, EventArgs e)
        {
            string script = "";
            if (cbHoming1stX.Checked)
            {
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

            bot.EvaluateScript(script + @" start-homing");
        }

        // 發出 SFC 緊急停止的要求
        private void button1_Click_1(object sender, EventArgs e)
        {
            // 一併清除Botnana Control 內的 NC program
            bot.EvaluateScript(@" ems-stop -nc marker -nc");
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
                    bot.EvaluateScript(textHomingV1X.Text + @" 1  axis-homing-v1!");
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
                    bot.EvaluateScript(textHomingV1Y.Text + @" 2  axis-homing-v1!");
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
                    bot.EvaluateScript(textHomingV1Z.Text + @" 3  axis-homing-v1!");
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
                    bot.EvaluateScript(textHomingV2X.Text + @" 1  axis-homing-v2!");
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
                    bot.EvaluateScript(textHomingV2Y.Text + @" 2  axis-homing-v2!");
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
                    bot.EvaluateScript(textHomingV2Z.Text + @" 3  axis-homing-v2!");
                }
            }
            paraSetting = true;
        }

        private void textHomingV1X_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
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
                    bot.EvaluateScript(textHomingMethodX.Text + @" 1  axis-homing-method!");
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
                    bot.EvaluateScript(textHomingMethodY.Text + @" 2  axis-homing-method!");
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
                    bot.EvaluateScript(textHomingMethodZ.Text + @" 3  axis-homing-method!");
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
                    bot.EvaluateScript(textRapidTravelsRate.Text.Trim() + @"e mm/min rapid-travels-rate!");
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
                    bot.EvaluateScript(textMachiningRate.Text.Trim() + @"e mm/min machining-rate! .config");
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
            bot.EvaluateScript(@"ack-monitor");
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
                bot.EvaluateScript("reset-machining -nc marker -nc");
                hasProgram = false;
            }
        }
    }
}

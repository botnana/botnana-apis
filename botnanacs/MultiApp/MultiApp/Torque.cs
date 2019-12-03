using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Globalization;
using BotnanaLib;
using System.Collections;

namespace MultiApp
{
    public partial class FormTorque : Form
    {        
        private int torqueDriveSlave = 0;
        private int torqueDriveChannel = 0;
        private int torqueAxisNumber = 0;
        private int torqueGroupNumber = 0;

        private double axisPosition;
        private double feedbackPosition;
        private double FollowingError() { return axisPosition - feedbackPosition; }

        private delegate void Torquedeg();

        private const int QueueCapacity = 2000;
        private Queue<int> torqueQueue = new Queue<int>(QueueCapacity);

        private Thread torqueThread;
        private ManualResetEvent pauseEvent;
        private int[] torqueArray = new int[QueueCapacity];

        private HandleMessage onRTRealTorque;
        private bool plotEnabled = false;
        private void OnRTRealTorqueCallback(IntPtr dataPtr, string str)
        {
            if (plotEnabled)
            {
                int real_torque = int.Parse(str);
                if (torqueQueue.Count >= QueueCapacity)
                {
                    torqueQueue.Dequeue();
                    torqueQueue.Enqueue(real_torque);
                }
                else
                {
                    torqueQueue.Enqueue(real_torque);
                }
            }
        }

        private HandleMessage onTorqueInfo;
        private HandleMessage onTorqueReady;
        private HandleTagNameMessage onRealPosition;
        private HandleTagNameMessage onRealTorque;
        private HandleTagNameMessage onDigitalInputs;
        private HandleTagNameMessage onTargetPosition;
        private HandleTagNameMessage onPDSState;
        private HandleTagNameMessage onDriveStatus;
        private HandleTagNameMessage onOperationMode;
        private HandleTagNameMessage onHomingMethod;
        private HandleTagNameMessage onHomingSpeed1;
        private HandleTagNameMessage onHomingSpeed2;
        private HandleTagNameMessage onHomingAcc;
        private HandleTagNameMessage onAxisPosition;
        private HandleTagNameMessage onFeedbackPosition;
        private HandleTagNameMessage onEncoderLengthUnit;
        private HandleTagNameMessage onEncoderPPU;
        private HandleTagNameMessage onDriveAlias;
        private HandleTagNameMessage onDriveSlavePosition;
        private HandleTagNameMessage onDriveChannel;
        private HandleTagNameMessage onAxisVmax;
        private HandleTagNameMessage onAxisAmax;
        private HandleTagNameMessage onGroupType;
        private HandleTagNameMessage onGroupMapping;
        private HandleTagNameMessage onGroupVmax;
        private HandleTagNameMessage onGroupAmax;
        private HandleTagNameMessage onGroupJmax;
        private HandleMessage onTravelTime;

        public void Reset()
        {
            torqueDriveSlave = 0;
            torqueDriveChannel = 0;
            torqueAxisNumber = 0;
            torqueGroupNumber = 0;
            BeginInvoke(new Torquedeg(() =>
            {
                buttonTorqueReady.Text = "Torque not ready";
                buttonTorqueReady.BackColor = Color.FromArgb(0xFF, 0x2D, 0x2D);
            }));
        }

        public void Initialize()
        {
            // 取得裝置資訊
            FormApp.BotEvaluateScript(@".torque-infos");
        }

        public void Awake()
        {
            timer1.Enabled = true;
        }

        public void Sleep()
        {
            FormApp.BotEvaluateScript("false rt-info-output-enabled !");
            timer1.Enabled = false;
        }

        public void EmergencyStop()
        {
            // 緊急停止不檢查連線狀態，嘗試送出命令
            FormApp.bot.EvaluateScript(torqueDriveChannel.ToString() + @" " + torqueDriveSlave.ToString() + @" drive-off");
        }

        public FormTorque()
        {
            InitializeComponent();
            TopLevel = false;
            Visible = true;
            FormBorderStyle = FormBorderStyle.None;

            Process thisProc = Process.GetCurrentProcess();
            thisProc.PriorityClass = ProcessPriorityClass.RealTime;

            // On rt_real_torque tag callback.
            onRTRealTorque = new HandleMessage(OnRTRealTorqueCallback);
            FormApp.bot.SetTagCB(@"rt_real_torque", 0, IntPtr.Zero, onRTRealTorque);

            // On devices info tag callback.
            onTorqueInfo = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                string[] values = str.Split('.');
                torqueDriveSlave = int.Parse(values[0]);
                torqueDriveChannel = int.Parse(values[1]);
                torqueAxisNumber = int.Parse(values[2]);
                torqueGroupNumber = int.Parse(values[3]);

                string cmd = null;
                if (torqueDriveSlave != 0)
                {
                    // 1 .slave 回應的訊息範例如下:
                    // vendor.1|0x000001DD|product.1|0x10305070|description.1||ec_alias.1|0|slave_state.1|0x23
                    // |device_type.1|0x04020192|profile_deceleration.1.1|100000|profile_acceleration.1.1|100000
                    // |profile_velocity.1.1|50000|operation_mode.1.1|6|home_offset.1.1|0|homing_method.1.1|35
                    // |homing_speed_1.1.1|10000|homing_speed_2.1.1|1000|homing_acceleration.1.1|50000
                    // |supported_drive_mode.1.1|0x000003ED|target_velocity.1.1|0|drive_polarity.1.1|0x00
                    // |min_position_limit.1.1|-2147483648|max_position_limit.1.1|2147483647|target_torque.1.1|0
                    // |torque_slope.1.1|200|max_motor_speed.1.1|5000|drive_homed.1.1|0|drive_douts.1.1|0
                    // |drive_douts_mask.1.1|0|control_word.1.1|0x0000|target_position.1.1|0|status_word.1.1|0x0250
                    // |real_position.1.1|0|digital_inputs.1.1|0x00000004|real_torque.1.1|0|pds_state.1.1|Switch On Disabled
                    // |pds_goal.1.1|Switch On Disabled
                    cmd += torqueDriveSlave.ToString() + @" .slave ";
                }
                if (torqueAxisNumber != 0)
                {
                    // "1 .axiscfg" 回應的訊息範例如下:
                    //  axis_name.1|A2|axis_home_offset.1|0.0000000|encoder_length_unit.1|Meter
                    // |encoder_ppu.1|1000000.00000|encoder_direction.1|1|ext_encoder_ppu.1|60000.00000
                    // |ext_encoder_direction.1|-1|closed_loop_filter.1|15.0|max_position_deviation.1|0.001000
                    // |drive_alias.1 |0|drive_slave_position.1|1|drive_channel.1|1|ext_encoder_alias.1|0
                    // |ext_encoder_slave_position.1|0|ext_encoder_channel.1|0|axis_amax.1|5.00000
                    // |axis_vmax.1|0.10000|axis_vff.1|0.00|axis_vfactor.1|1.00000|axis_aff.1|0.00
                    // |axis_afactor.1|1.00000
                    // 其中 encoder_length_unit 是設定 Encoder 解析度的單位距離
                    //      encoder_ppu 是設定 Encoder 單位距離有幾個脈波數
                    //      axis_vmax 是運動軸的最大速度 [m/s]
                    //      axis_amax 是運動軸的最大加速度 [m/s^2]
                    //      drive_alias, drive_slave_position, drive_channel 用來指定對應的驅動器，
                    //      如果 drive_alias > 0 就採用  drive_alias 與 drive_channel，
                    //      如果 drive_alias == 0 就採用 drive_slave_position 與 drive_channel，
                    //      如果沒有對應的驅動器，該運動軸及為虛擬軸
                    cmd += torqueAxisNumber.ToString() + @" .axiscfg ";
                }
                if (torqueGroupNumber != 0)
                {
                    // "1 .grpcfg" 回應的訊息範例如下:
                    // group_name.1|G1X|group_type.1|1D|group_mapping.1|1|group_vmax.1|0.05000
                    // |group_amax.1|2.00000|group_jmax.1|40.00000
                    // 其中 group_type 是設定軸組型態
                    //      group_mapping 是設定軸組會使用哪幾個運動軸
                    //      group_vmax 是運動軸的最加速度 [m/s]
                    //      group_amax 是運動軸的最大加速度 [m/s^2]
                    //      group_jmax 是運動軸的最大加加速度 [m/s^3]
                    cmd += torqueGroupNumber.ToString() + @" .grpcfg ";
                }
                FormApp.BotEvaluateScript(cmd);
            });
            FormApp.bot.SetTagCB(@"torque_slv_ch_ax_grp", 0, IntPtr.Zero, onTorqueInfo);

            // On torque_ready tag callback.
            onTorqueReady = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                if (str == "-1")
                {
                    BeginInvoke(new Torquedeg(() =>
                    {
                        buttonTorqueReady.Text = "Torque ready";
                        buttonTorqueReady.BackColor = Color.FromArgb(0x53, 0xFF, 0x53);
                    }));
                }
                else
                {
                    BeginInvoke(new Torquedeg(() =>
                    {
                        buttonTorqueReady.Text = "Torque not ready";
                        buttonTorqueReady.BackColor = Color.FromArgb(0xFF, 0x2D, 0x2D);
                    }));
                }
            });
            FormApp.bot.SetTagCB(@"torque_ready", 0, IntPtr.Zero, onTorqueReady);

            // On real_position tag name callback.
            onRealPosition = new HandleTagNameMessage((IntPtr dataPtr, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == torqueDriveSlave && ch == torqueDriveChannel) BeginInvoke(new Torquedeg(() => { textRealPosition.Text = str; }));
            });
            FormApp.bot.SetTagNameCB(@"real_position", 0, IntPtr.Zero, onRealPosition);

            // On real_torque tag name callback.
            onRealTorque = new HandleTagNameMessage((IntPtr dataPtr, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == torqueDriveSlave && ch == torqueDriveChannel) BeginInvoke(new Torquedeg(() => { textRealTorque.Text = str; }));
            });
            FormApp.bot.SetTagNameCB(@"real_torque", 0, IntPtr.Zero, onRealTorque);

            // On digital_inputs tag name callback.
            onDigitalInputs = new HandleTagNameMessage((IntPtr dataPtr, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == torqueDriveSlave && ch == torqueDriveChannel)
                {
                    string din = string.Format("0x{0:X8}", Convert.ToInt32(str, 16));
                    BeginInvoke(new Torquedeg(() => { textDigitalInputs.Text = din; }));
                }
            });
            FormApp.bot.SetTagNameCB("digital_inputs", 0, IntPtr.Zero, onDigitalInputs);

            // On target_position tag name callback.
            onTargetPosition = new HandleTagNameMessage((IntPtr dataPtr, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == torqueDriveSlave && ch == torqueDriveChannel) BeginInvoke(new Torquedeg(() => { textTargetPosition.Text = str; }));
            });
            FormApp.bot.SetTagNameCB(@"target_position", 0, IntPtr.Zero, onTargetPosition);

            // On pds_state tag name callback.
            onPDSState = new HandleTagNameMessage((IntPtr dataPtr, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == torqueDriveSlave && ch == torqueDriveChannel)
                {
                    switch (str)
                    {
                        case "Operation Enabled":
                            BeginInvoke(new Torquedeg(() =>
                            {
                                textPDSState.Text = str;
                                buttonDriveOn.BackColor = Color.FromArgb(0x53, 0xFF, 0x53);
                                buttonDriveOff.BackColor = SystemColors.Control;
                                buttonResetFault.BackColor = SystemColors.Control;
                            }));
                            break;
                        case "Fault":
                            BeginInvoke(new Torquedeg(() =>
                            {
                                textPDSState.Text = str;
                                buttonDriveOn.BackColor = SystemColors.Control;
                                buttonDriveOff.BackColor = Color.FromArgb(0xFF, 0x2D, 0x2D);
                                buttonResetFault.BackColor = Color.FromArgb(0xFF, 0x2D, 0x2D);
                            }));
                            break;
                        default:
                            BeginInvoke(new Torquedeg(() =>
                            {
                                textPDSState.Text = str;
                                buttonDriveOn.BackColor = SystemColors.Control;
                                buttonDriveOff.BackColor = Color.FromArgb(0xFF, 0x2D, 0x2D);
                                buttonResetFault.BackColor = SystemColors.Control;
                            }));
                            buttonDriveOn.BackColor = SystemColors.Control;
                            buttonDriveOff.BackColor = Color.FromArgb(0xFF, 0x2D, 0x2D);
                            buttonResetFault.BackColor = SystemColors.Control;
                            break;
                    }
                }
            });
            FormApp.bot.SetTagNameCB(@"pds_state", 0, IntPtr.Zero, onPDSState);

            //On status_word tag name callback.
            onDriveStatus = new HandleTagNameMessage((IntPtr dataPtr, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == torqueDriveSlave && ch == torqueDriveChannel) BeginInvoke(new Torquedeg(() => { textDriveStatus.Text = str; }));
            });
            FormApp.bot.SetTagNameCB(@"status_word", 0, IntPtr.Zero, onDriveStatus);

            // On operation_mode tag name callback.
            onOperationMode = new HandleTagNameMessage((IntPtr dataPtr, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == torqueDriveSlave && ch == torqueDriveChannel)
                {
                    switch (str)
                    {
                        case "1":
                            BeginInvoke(new Torquedeg(() => { textOperationMode.Text = "PP"; }));
                            break;
                        case "6":
                            BeginInvoke(new Torquedeg(() => { textOperationMode.Text = "HM"; }));
                            break;
                        case "8":
                            BeginInvoke(new Torquedeg(() => { textOperationMode.Text = "CSP"; }));
                            break;
                        default:
                            break;
                    }
                }
            });
            FormApp.bot.SetTagNameCB(@"operation_mode", 0, IntPtr.Zero, onOperationMode);

            // On homing_method tag name callback.
            onHomingMethod = new HandleTagNameMessage((IntPtr dataPtr, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == torqueDriveSlave && ch == torqueDriveChannel) BeginInvoke(new Torquedeg(() => { textHomingMethod.Text = str; }));
            });
            FormApp.bot.SetTagNameCB(@"homing_method", 0, IntPtr.Zero, onHomingMethod);

            // On homing_speed_1 tag name callback.
            onHomingSpeed1 = new HandleTagNameMessage((IntPtr dataPtr, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == torqueDriveSlave && ch == torqueDriveChannel) BeginInvoke(new Torquedeg(() => { textHomingSpeed1.Text = str; }));
            });
            FormApp.bot.SetTagNameCB(@"homing_speed_1", 0, IntPtr.Zero, onHomingSpeed1);

            // On homing_speed_2 tag name callback.
            onHomingSpeed2 = new HandleTagNameMessage((IntPtr dataPtr, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == torqueDriveSlave && ch == torqueDriveChannel) BeginInvoke(new Torquedeg(() => { textHomingSpeed2.Text = str; }));
            });
            FormApp.bot.SetTagNameCB(@"homing_speed_2", 0, IntPtr.Zero, onHomingSpeed2);

            // On homing_acceleration tag name callback.
            onHomingAcc = new HandleTagNameMessage((IntPtr dataPtr, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == torqueDriveSlave && ch == torqueDriveChannel) BeginInvoke(new Torquedeg(() => { textHomingAcc.Text = str; }));
            });
            FormApp.bot.SetTagNameCB(@"homing_acceleration", 0, IntPtr.Zero, onHomingAcc);

            // On axis_demand_position tag name callback.
            onAxisPosition = new HandleTagNameMessage((IntPtr dataPtr, UInt32 n, UInt32 _, string str) =>
            {
                if (n == torqueAxisNumber) 
                {
                    axisPosition = double.Parse(str) * 1000.0;
                    string axisPos = axisPosition.ToString("F3");
                    BeginInvoke(new Torquedeg(() => { textAxisPosition.Text = axisPos; }));
                }
            });
            FormApp.bot.SetTagNameCB(@"axis_demand_position", 0, IntPtr.Zero, onAxisPosition);

            // On feedback_position tag name callback.
            onFeedbackPosition = new HandleTagNameMessage((IntPtr dataPtr, UInt32 n, UInt32 _, string str) =>
            {
                if (n == torqueAxisNumber)
                {
                    feedbackPosition = double.Parse(str) * 1000.0;
                    string feedbackPos = feedbackPosition.ToString("F3");
                    BeginInvoke(new Torquedeg(() => { textFeedbackPosition.Text = feedbackPos; }));
                }
            });
            FormApp.bot.SetTagNameCB(@"feedback_position", 0, IntPtr.Zero, onFeedbackPosition);

            // On encoder_length_unit tag name callback.
            onEncoderLengthUnit = new HandleTagNameMessage((IntPtr dataPtr, UInt32 n, UInt32 _, string str) =>
            {
                if (n == torqueAxisNumber) BeginInvoke(new Torquedeg(() => { labelEncoderResolution.Text = "pulse/" + str.Substring(0, 1).ToLower(); }));
            });
            FormApp.bot.SetTagNameCB(@"encoder_length_unit", 0, IntPtr.Zero, onEncoderLengthUnit);

            // On encoder_ppu tag name callback.
            onEncoderPPU = new HandleTagNameMessage((IntPtr dataPtr, UInt32 n, UInt32 _, string str) =>
            {
                if (n == torqueAxisNumber) BeginInvoke(new Torquedeg(() => { textEncoderPPU.Text = str; }));
            });
            FormApp.bot.SetTagNameCB(@"encoder_ppu", 0, IntPtr.Zero, onEncoderPPU);

            // On drive_alias tag name callback.
            onDriveAlias = new HandleTagNameMessage((IntPtr dataPtr, UInt32 n, UInt32 _, string str) =>
            {
                if (n == torqueAxisNumber) BeginInvoke(new Torquedeg(() => { textDriveAlias.Text = str; }));
            });
            FormApp.bot.SetTagNameCB(@"drive_alias", 0, IntPtr.Zero, onDriveAlias);

            // On drive_slave_position tag name callback.
            onDriveSlavePosition = new HandleTagNameMessage((IntPtr dataPtr, UInt32 n, UInt32 _, string str) =>
            {
                if (n == torqueAxisNumber) BeginInvoke(new Torquedeg(() => { textDriveSlavePosition.Text = str; }));
            });
            FormApp.bot.SetTagNameCB(@"drive_slave_position", 0, IntPtr.Zero, onDriveSlavePosition);

            // On drive_channel tag name callback.
            onDriveChannel = new HandleTagNameMessage((IntPtr dataPtr, UInt32 n, UInt32 _, string str) =>
            {
                if (n == torqueAxisNumber) BeginInvoke(new Torquedeg(() => { textDriveChannel.Text = str; }));
            });
            FormApp.bot.SetTagNameCB(@"drive_channel", 0, IntPtr.Zero, onDriveChannel);

            // On axis_vmax tag name callback.
            onAxisVmax = new HandleTagNameMessage((IntPtr dataPtr, UInt32 n, UInt32 _, string str) =>
            {
                if (n == torqueAxisNumber)
                {
                    string vmax = (double.Parse(str) * 1000.0 * 60.0).ToString("F1");
                    BeginInvoke(new Torquedeg(() => { textAxisVmax.Text = vmax; }));
                }
            });
            FormApp.bot.SetTagNameCB(@"axis_vmax", 0, IntPtr.Zero, onAxisVmax);

            // On axis_amax tag name callback.
            onAxisAmax = new HandleTagNameMessage((IntPtr dataPtr, UInt32 n, UInt32 _, string str) =>
            {
                if (n == torqueAxisNumber) BeginInvoke(new Torquedeg(() => { textAxisAmax.Text = str; }));
            });
            FormApp.bot.SetTagNameCB(@"axis_amax", 0, IntPtr.Zero, onAxisAmax);

            // On group_type tag name callback.
            onGroupType = new HandleTagNameMessage((IntPtr dataPtr, UInt32 n, UInt32 _, string str) =>
            {
                if (n == torqueGroupNumber) BeginInvoke(new Torquedeg(() => { textGroupType.Text = str; }));
            });
            FormApp.bot.SetTagNameCB(@"group_type", 0, IntPtr.Zero, onGroupType);

            // On group_mapping tag name callback.
            onGroupMapping = new HandleTagNameMessage((IntPtr dataPtr, UInt32 n, UInt32 _, string str) =>
            {
                if (n == torqueGroupNumber) BeginInvoke(new Torquedeg(() => { textGroupMapping.Text = str; }));
            });
            FormApp.bot.SetTagNameCB(@"group_mapping", 0, IntPtr.Zero, onGroupMapping);

            // On group_vmax tag name callback.
            onGroupVmax = new HandleTagNameMessage((IntPtr dataPtr, UInt32 n, UInt32 _, string str) =>
            {
                if (n == torqueGroupNumber)
                {
                    string vmax = (double.Parse(str) * 1000.0 * 60.0).ToString("F1");
                    BeginInvoke(new Torquedeg(() => { textGroupVmax.Text = vmax; }));
                }
            });
            FormApp.bot.SetTagNameCB(@"group_vmax", 0, IntPtr.Zero, onGroupVmax);

            // On group_amax tag name callback.
            onGroupAmax = new HandleTagNameMessage((IntPtr dataPtr, UInt32 n, UInt32 _, string str) =>
            {
                if (n == torqueGroupNumber) BeginInvoke(new Torquedeg(() => { textGroupAmax.Text = str; }));
            });
            FormApp.bot.SetTagNameCB(@"group_amax", 0, IntPtr.Zero, onGroupAmax);

            // On group_jmax tag name callback.
            onGroupJmax = new HandleTagNameMessage((IntPtr dataPtr, UInt32 n, UInt32 _, string str) =>
            {
                if (n == torqueGroupNumber) BeginInvoke(new Torquedeg(() => { textGroupJmax.Text = str; }));
            });
            FormApp.bot.SetTagNameCB(@"group_jmax", 0, IntPtr.Zero, onGroupJmax);

            // On travel_time tag callback.
            onTravelTime = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                new Thread(() => System.Windows.Forms.MessageBox.Show("TravelTime|" + str)).Start();
            });
            FormApp.bot.SetTagCB(@"travel_time", 0, IntPtr.Zero, onTravelTime);
        }

        private void FormTorque_Load(object sender, EventArgs e)
        {
            chartTorque.ChartAreas[0].AxisX.Minimum = 0;
            chartTorque.ChartAreas[0].AxisX.Maximum = QueueCapacity;

            timer1.Interval = 10;
            torqueThread = new Thread(new ThreadStart(this.getTorqueInformation));
            pauseEvent = new ManualResetEvent(false);
            torqueThread.IsBackground = true;
            torqueThread.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textFollowingError.Text = FollowingError().ToString("F3");
            if (Math.Abs(FollowingError()) < 2.0)
            {
                textFollowingError.ForeColor = Color.Black;
            }
            else
            {
                textFollowingError.ForeColor = Color.Red;
            }
            if (torqueDriveSlave != 0)
            {
                // 回傳有差異的參數
                if (!FormApp.stopPolling) FormApp.BotEvaluateScript(torqueDriveSlave.ToString() + @" .slave-diff");
            }
            if (torqueAxisNumber != 0)
            {
                // "1 .axis" 回應的訊息範例如下:
                // axis_command_position.1|0.0000000|axis_demand_position.1|0.0000000|axis_corrected_position.1|0.0000000
                // |encoder_position.1|0.0000000|external_encoder_position.1|0.0000000|feedback_position.1|0.0000000
                // |position_correction.1|0.0000000|following_error.1|0.0000000|axis_interpolator_enabled.1|0|axis_homed.1
                // |0|axis_velocity.1|0.000000|axis_acceleration.1|0.000000
                // 其中 axis_command_position 是運動的目標位置
                //      axis_demand_position 是依加減速機制算出來當下應該要走到的位置
                if (!FormApp.stopPolling) FormApp.BotEvaluateScript(torqueAxisNumber.ToString() + @" .axis");
            }
        }

        private void UpdateTorqueChart()
        {
            // 更新即時的扭力輸出圖
            chartTorque.Series[0].Points.Clear();
            Array.Copy(torqueQueue.ToArray(), 0, torqueArray, 0, torqueQueue.Count);
            for (int i = 0; i < torqueQueue.Count; i++)
            {
                chartTorque.Series[0].Points.AddY(torqueArray[i]);
            }
        }

        private void getTorqueInformation()
        {
            // 固定時間來檢查是否要更新即時的扭力輸出圖
            while (true)
            {
                pauseEvent.WaitOne(Timeout.Infinite);
                if (chartTorque.IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)delegate { UpdateTorqueChart(); });
                }
                SpinWait.SpinUntil(() => false, 250);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 讓 torque.sfc 中的 SFC ，停止每個 real time 周期都送出 real torque。
            FormApp.BotEvaluateScript("false rt-info-output-enabled !");
            pauseEvent.Reset();
            plotEnabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 讓 torque.sfc 中的 SFC ，每個 real time 周期都送出 real torque (用來畫圖)。
            FormApp.BotEvaluateScript("true rt-info-output-enabled !");
            pauseEvent.Set();
            plotEnabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 切換驅動器到 CSP Mode, 避免切換時跳異警，檢查或後誤差是否太大
            if (Math.Abs(FollowingError()) < 0.05)
            {
                FormApp.BotEvaluateScript(@"csp " + torqueDriveChannel.ToString() + @" " + torqueDriveSlave.ToString() + @" op-mode!");
            }
            else
            {
                new Thread(() => System.Windows.Forms.MessageBox.Show("Following Error Too Large")).Start();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 切換驅動器到 HM Mode,
            FormApp.BotEvaluateScript(@"hm " + torqueDriveChannel.ToString() + @" " + torqueDriveSlave.ToString() + @" op-mode!");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FormApp.BotEvaluateScript(@"pp " + torqueDriveChannel.ToString() + @" " + torqueDriveSlave.ToString() + @" op-mode!");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // 驅動器在 HM Mode 下, 如果要開始回 Home 要下 go 命令
            FormApp.BotEvaluateScript(torqueDriveChannel.ToString() + @" " + torqueDriveSlave.ToString() + @" go");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // 驅動器 Drive On 前檢查 following error
            if (Math.Abs(FollowingError()) < 0.05)
            {
                FormApp.BotEvaluateScript(torqueDriveChannel.ToString() + @" " + torqueDriveSlave.ToString() + @" drive-on");
            }
            else
            {
                new Thread(() => System.Windows.Forms.MessageBox.Show("Following Error Too Large")).Start();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // 命令驅動器暫停
            FormApp.BotEvaluateScript(torqueDriveChannel.ToString() + @" " + torqueDriveSlave.ToString() + @" +drive-halt");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // 命令驅動器繼續運作
            FormApp.BotEvaluateScript(torqueDriveChannel.ToString() + @" " + torqueDriveSlave.ToString() + @" -drive-halt");
        }

        private void buttonPull_Click(object sender, EventArgs e)
        {
            // 使用 torque.fs 內的 tq-release-go 指令
            // tq-release-go 是以指定的速度移動 textReleaseV 到目標點位置 textReleaseP
            double p, v;
            bool pass = true;
            if (!double.TryParse(textReleaseP.Text, out p))
            {
                textReleaseP.Text = "Error";
                textReleaseP.ForeColor = Color.Red;
                pass = false;
            }
            if (!double.TryParse(textReleaseV.Text, out v) || v < 0)
            {
                textReleaseV.Text = "Error";
                textReleaseV.ForeColor = Color.Red;
                pass = false;
            }

            // deploy ... ;deploy 是用來將指令送到 NC Background task 執行，因為 tq-release-go 指令會等待位置到達後做後續的設定
            // 如果不送到 NC Background task 執行，則此 user task 會沒有回應。
            if (pass)
            {
                FormApp.BotEvaluateScript("deploy " + textReleaseP.Text + @"e mm " + textReleaseV.Text + @"e mm/min tq-release-go ;deploy");
            }
        }

        private void buttonPush_Click(object sender, EventArgs e)
        {
            // 使用 torque.fs 內的 tq-press-go 指令
            // tq-press-go 是使用 1D 軸組運動
            //
            //       |
            //       |   速度:     textPushV1
            //       v
            //       |   距離: textSpeedChangeDistance, 速度: textPushV2
            //       v   目標位置: textPushP1
            //       |   在此區段偵測實際扭力是否小於 textTorqueThreshold，如果條件成立，就會停止運動，反之則是到達 textPushP2 後停止運動 
            //       |   速度:     textPushV2  
            //       v   目標位置: textPushP2

            bool pass = true;
            int torqueThreshold = 0;
            double speedChangeDist = 0.0;
            double p1 = 0.0;
            double p2 = 0.0;
            double v1 = 0.0;
            double v2 = 0.0;
            if (!int.TryParse(textTorqueLimit2.Text, out torqueThreshold))
            {
                textTorqueLimit2.Text = "Error";
                textTorqueLimit2.ForeColor = Color.Red;
                pass = false;
            }

            if (!double.TryParse(textFollowingErrorLimit2.Text, out speedChangeDist) || speedChangeDist < 0.001)
            {
                textFollowingErrorLimit2.Text = "Error";
                textFollowingErrorLimit2.ForeColor = Color.Red;
                pass = false;
            }

            if (!double.TryParse(textPushP1.Text, out p1))
            {
                textPushP1.Text = "Error";
                textPushP1.ForeColor = Color.Red;
                pass = false;
            }

            if (!double.TryParse(textPushP2.Text, out p2) || (p1 - p2) < (speedChangeDist + 0.001))
            {
                textPushP2.Text = "Error";
                textPushP2.ForeColor = Color.Red;
                pass = false;
            }

            if (!double.TryParse(textPushV1.Text, out v1) || v1 < 0.0)
            {
                textPushV1.Text = "Error";
                textPushV1.ForeColor = Color.Red;
                pass = false;
            }

            if (!double.TryParse(textPushV2.Text, out v2) || v2 < 0.0)
            {
                textPushV2.Text = "Error";
                textPushV2.ForeColor = Color.Red;
                pass = false;
            }

            // deploy ... ;deploy 是用來將指令送到 NC Background task 執行，因為 tq-press-go 指令會偵測扭力並等待位置到達後做後續的設定
            // 如果不送到 NC Background task 執行，則此 user task 會沒有回應。
            if (pass)
            {
                string cmd = textTorqueLimit1.Text + " tq-limit1 ! " + textTorqueLimit2.Text + " tq-limit2 ! " + textFollowingErrorLimit1.Text + "e mm tq-max-ferr1 f! " + textFollowingErrorLimit2.Text + "e mm tq-max-ferr2 f!";
                cmd += " deploy " + textPushP1.Text + @"e mm " + textPushP2.Text + @"e mm " + textPushV1.Text + @"e mm/min " + textPushV2.Text + @"e mm/min tq-press-go ;deploy";
                FormApp.BotEvaluateScript(cmd);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // 停止軸組運動指令
            FormApp.BotEvaluateScript(@"tq-press-stop");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            // 清除軸落後誤差 (以實際位置修正目標位置)
            FormApp.BotEvaluateScript(torqueAxisNumber.ToString() + @" 0axis-ferr");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // 驅動器 OFF
            FormApp.BotEvaluateScript(torqueDriveChannel.ToString() + @" " + torqueDriveSlave.ToString() + @" drive-off");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            // 當驅動器發生異警時，要先清除異警
            FormApp.BotEvaluateScript(torqueDriveChannel.ToString() + @" " + torqueDriveSlave.ToString() + @" reset-fault");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            // 停止軸組運動
            FormApp.BotEvaluateScript(@"tq-release-stop");
        }

        private void textHomingMethod_Leave(object sender, EventArgs e)
        {
            // 設定回 Home 模式
            // textHomingMethod.Text = 1 : 往負方向運動，碰到極限開關後，反轉找第一個 pulse index
            // textHomingMethod.Text = 2 : 往正方向運動，碰到極限開關後，反轉找第一個 pulse index
            FormApp.BotEvaluateScript(textHomingMethod.Text + @" " + torqueDriveChannel.ToString() + @" " + torqueDriveSlave.ToString() + @" homing-method!");
        }
        
        private void textHomingSpeed1_Leave(object sender, EventArgs e)
        {
            // 設定回 Home 模式中找極限開關的速度，單位通常是 pulse/s，不同廠牌驅動器可能會不同
            FormApp.BotEvaluateScript(textHomingSpeed1.Text + @" " + torqueDriveChannel.ToString() + @" " + torqueDriveSlave.ToString() + @" homing-v1!");
        }
        
        private void textHomingSpeed2_Leave(object sender, EventArgs e)
        {
            // 設定回 Home 模式中找 pulse index 的速度，單位通常是 pulse/s，不同廠牌驅動器可能會不同
            FormApp.BotEvaluateScript(textHomingSpeed2.Text + @" " + torqueDriveChannel.ToString() + @" " + torqueDriveSlave.ToString() + @" homing-v2!");
        }
        
        private void textHomingAcc_Leave(object sender, EventArgs e)
        {
            // 設定回 Home 模式中的加速度，單位通常是 pulse/s^2，不同廠牌驅動器可能會不同
            FormApp.BotEvaluateScript(textHomingAcc.Text + @" " + torqueDriveChannel.ToString() + @" " + torqueDriveSlave.ToString() + @" homing-a!");
        }
        
        private void textEncoderPPU_Leave(object sender, EventArgs e)
        {
            // 設定單位距離有幾個 encoder pulse 數量
            FormApp.BotEvaluateScript(textEncoderPPU.Text + @"e " + torqueAxisNumber.ToString() + @" enc-ppu! " + torqueAxisNumber.ToString() + @" .axiscfg");
        }
        
        private void textAxisVmax_Leave(object sender, EventArgs e)
        {
            // 設定運動軸的最大速度 (主站使用的是 SI 單位，線性軸是 m/s)，設定完成後取回軸設定參數 (.axiscfg)，
            // 用來看參數是否設定正確
            FormApp.BotEvaluateScript(textAxisVmax.Text + @"e mm/min " + torqueAxisNumber.ToString() + @" axis-vmax! " + torqueAxisNumber.ToString() + @" .axiscfg");
        }
        
        private void textAxisAmax_Leave(object sender, EventArgs e)
        {
            // 設定運動軸的最大加速度 (主站使用的是 SI 單位，線性軸是 m/s^2)，設定完成後取回軸設定參數 (.axiscfg)，
            // 用來看參數是否設定正確  
            FormApp.BotEvaluateScript(textAxisAmax.Text + @"e " + torqueAxisNumber.ToString() + @" axis-amax! " + torqueAxisNumber.ToString() + @" .axiscfg");
        }
        
        private void textDriveAlias_Leave(object sender, EventArgs e)
        {
            // 設定運動軸的對應的驅動器 alias，如果設定為 0，表示依 slave position，如果沒有對應的驅動器，則是虛擬運動軸。
            // 設定完成後取回軸設定參數 (.axiscfg)，用來看參數是否設定正確
            FormApp.BotEvaluateScript(textDriveAlias.Text + @" " + torqueAxisNumber.ToString() + @" drv-alias! " + torqueAxisNumber.ToString() + @" .axiscfg");
        }
        
        private void textDriveSlavePosition_Leave(object sender, EventArgs e)
        {
            // 設定運動軸的對應的驅動器 slave position，如果沒有對應的驅動器，則是虛擬運動軸。
            // 設定完成後取回軸設定參數 (.axiscfg)，用來看參數是否設定正確
            FormApp.BotEvaluateScript(textDriveSlavePosition.Text + @" " + torqueAxisNumber.ToString() + @" drv-slave! " + torqueAxisNumber.ToString() + @" .axiscfg");
        }
        
        private void textDriveChannel_Leave(object sender, EventArgs e)
        {
            // 設定運動軸的對應的驅動器的 Channel，如果沒有對應的驅動器，則是虛擬運動軸。
            // 設定完成後取回軸設定參數 (.axiscfg)，用來看參數是否設定正確
            FormApp.BotEvaluateScript(textDriveChannel.Text + @" " + torqueAxisNumber.ToString() + @" drv-channel! " + torqueAxisNumber.ToString() + @" .axiscfg");
        }
        
        private void textGroupMapping_Leave(object sender, EventArgs e)
        {
            // 設定軸組對應的運動軸，設定完成後取回軸組設定參數 (.grpcfg)，用來看參數是否設定正確
            FormApp.BotEvaluateScript(textGroupMapping.Text + @" " + torqueGroupNumber.ToString() + @" gmap! " + torqueGroupNumber.ToString() + @" .grpcfg");
        }
        
        private void textGroupVmax_Leave(object sender, EventArgs e)
        {
            // 設定軸組對應的運動軸的最大速度，設定完成後取回軸組設定參數 (.grpcfg)，用來看參數是否設定正確
            FormApp.BotEvaluateScript(textGroupVmax.Text + @"e mm/min " + torqueGroupNumber.ToString() + @" gvmax! " + torqueGroupNumber.ToString() + @" .grpcfg");
        }
        
        private void textGroupAmax_Leave(object sender, EventArgs e)
        {
            // 設定軸組對應的運動軸的最大加速度，設定完成後取回軸組設定參數 (.grpcfg)，用來看參數是否設定正確
            FormApp.BotEvaluateScript(textGroupAmax.Text + @"e " + torqueGroupNumber.ToString() + @" gamax! " + torqueGroupNumber.ToString() + @" .grpcfg");
        }
        
        private void textGroupJmax_Leave(object sender, EventArgs e)
        {
            // 設定軸組對應的運動軸的最大加加速度，設定完成後取回軸組設定參數 (.grpcfg)，用來看參數是否設定正確
            FormApp.BotEvaluateScript(textGroupJmax.Text + @"e " + torqueGroupNumber.ToString() + @" gjmax! " + torqueGroupNumber.ToString() + @" .grpcfg");
        }

        private void textTorqueLimit2_Leave(object sender, EventArgs e)
        {
            textTorqueLimit2.ForeColor = Color.Black;
        }

        private void textFollowingErrorLimit2_Leave(object sender, EventArgs e)
        {
            textFollowingErrorLimit2.ForeColor = Color.Black;
        }

        private void textPushP1_Leave(object sender, EventArgs e)
        {
            textPushP1.ForeColor = Color.Black;
        }

        private void textPushP2_Leave(object sender, EventArgs e)
        {
            textPushP2.ForeColor = Color.Black;
        }

        private void textPushV1_Leave(object sender, EventArgs e)
        {
            textPushV1.ForeColor = Color.Black;
        }

        private void textPushV2_Leave(object sender, EventArgs e)
        {
            textPushV2.ForeColor = Color.Black;
        }

        private void textReleaseP_Leave(object sender, EventArgs e)
        {
            textReleaseP.ForeColor = Color.Black;
        }

        private void textReleaseV_Leave(object sender, EventArgs e)
        {
            textReleaseV.ForeColor = Color.Black;
        }
    }
}

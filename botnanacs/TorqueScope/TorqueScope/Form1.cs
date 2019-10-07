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

namespace TorqueScope
{
    public partial class Form1 : Form
    {
        private Botnana bot;
        private int torqueDriveSlave = 0;
        private int torqueDriveChannel = 0;
        private int torqueAxisNumber = 0;
        private int torqueGroupNumber = 0;

        private const int QueueCapacity = 2000;
        private Queue<int> torqueQueue = new Queue<int>(QueueCapacity);

        private Thread torqueThread;
        private ManualResetEvent pauseEvent;
        private int[] torqueArray = new int[QueueCapacity];

        private int wsState = 0;
        private HandleMessage onWSError;
        private void OnWSErrorCallback(IntPtr dataPtr, string data)
        {
            wsState = 0;
            new Thread(() => System.Windows.Forms.MessageBox.Show("WS error : " + data)).Start();
        }

        private HandleMessage onWSOpen;
        private void OnWSOpenCallback(IntPtr dataPtr, string data)
        {
            wsState = 2;
            // 送出 .user-para 命令，讓為回傳的訊息去觸發 OnUserParameterCallback
            // 若重複執行從 32 開始，因為需要 .device-infos 取得周邊裝置的資訊
            // .user-para 回應的訊息範例如下:
            // user_parameter|0
            bot.EvaluateScript(@"user-para@ 32 min user-para! .user-para");
        }

        private int messageCount = 0;
        private HandleMessage onMessage;
        private void OnMessageCallback(IntPtr dataPtr, string data)
        {
            //MessageBox.Show("On message : " + data);
            messageCount++;
            if (messageCount > 256)
            {
                messageCount = 0;
            }
        }

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

        private HandleTagNameMessage onRealTorque;
        private Int32 realTorque;
        private void OnRealTorqueCallback(IntPtr dataPtr, UInt32 slv, UInt32 ch, string str)
        {
            if (slv == torqueDriveSlave && ch == torqueDriveChannel)
            {
                realTorque = Int32.Parse(str);
            }
        }

        // 取得 userParameter
        private HandleMessage onUserParameter;
        private bool hasSFC = false;
        private void OnUserParameterCallback(IntPtr dataPtr, string str)
        {
            int para = Int32.Parse(str);
            switch (para)
            {
                case 0:
                    // 設定 user parameter 為 16
                    // 如果此範例重新執行不會再載入以下 SFC
                    bot.EvaluateScript("16 user-para!");
                    // 清除SFC 邏輯，載入 SFC 時會造成 real time cycle overrun，所以要暫時 ignore-overrun
                    //  載入後再執行 `reset-overrun`
                    bot.EvaluateScript(@"0sfc ignore-overrun -work marker -work");
                    bot.LoadSFC(@"config.fs");
                    bot.LoadSFC(@"demo.fs");
                    bot.LoadSFC(@"torque.fs");
                    bot.LoadSFC(@"manager.fs");
                    bot.EvaluateScript(@"marker -torque .user-para");
                    //new Thread(() => System.Windows.Forms.MessageBox.Show("OnUserParameterCallback 0")).Start();
                    break;
                case 16:
                    bot.EvaluateScript(@"reset-overrun 32 user-para! .user-para");
                    //new Thread(() => System.Windows.Forms.MessageBox.Show("OnUserParameterCallback 16")).Start();
                    break;
                case 32:
                    //new Thread(() => System.Windows.Forms.MessageBox.Show("OnUserParameterCallback 32")).Start();
                    bot.EvaluateScript(@".device-infos .ec-links 64 user-para! .user-para");
                    break;
                case 64:
                    //new Thread(() => System.Windows.Forms.MessageBox.Show("OnUserParameterCallback 64")).Start();
                    string cmd = null;
                    // +coordinator : 啟動軸組控制的功能
                    cmd += @"+coordinator ";
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
                    bot.EvaluateScript(cmd);
                    hasSFC = true;
                    break;
                default:
                    break;
            }
        }

        private HandleTagNameMessage onRealPosition;
        private int realPosition;
        private void OnRealPositionCallback(IntPtr dataPtr, UInt32 slv, UInt32 ch, string str)
        {
            if (slv == torqueDriveSlave && ch == torqueDriveChannel)
            {
                realPosition = int.Parse(str);
            }
        }

        private HandleTagNameMessage onDigitalInputs;
        private int digitalInputs;
        private void OnDigitalInputsCallback(IntPtr dataPtr, UInt32 slv, UInt32 ch, string str)
        {
            if (slv == torqueDriveSlave && ch == torqueDriveChannel)
            {
                digitalInputs = Convert.ToInt32(str, 16);
            }
        }

        private HandleTagNameMessage onTargetPosition;
        private int targetPosition;
        private void OnTargetPositionCallback(IntPtr dataPtr, UInt32 slv, UInt32 ch, string str)
        {
            if (slv == torqueDriveSlave && ch == torqueDriveChannel)
            {
                targetPosition = int.Parse(str);
            }
        }

        private HandleTagNameMessage onPDSState;
        private string pdsState = "--";
        private void OnPDSStateCallback(IntPtr dataPtr, UInt32 slv, UInt32 ch, string str)
        {
            if (slv == torqueDriveSlave && ch == torqueDriveChannel)
            {
                pdsState = str;
            }
        }

        private HandleTagNameMessage onDriveStatus;
        private string driveStatus = "--";
        private void OnDriveStatusCallback(IntPtr dataPtr, UInt32 slv, UInt32 ch, string str)
        {
            if (slv == torqueDriveSlave && ch == torqueDriveChannel)
            {
                driveStatus = str;
            }
        }

        private HandleTagNameMessage onOperationMode;
        private string operationMode = "--";
        private void OnOperationModeCallback(IntPtr dataPtr, UInt32 slv, UInt32 ch, string str)
        {
            if (slv == torqueDriveSlave && ch == torqueDriveChannel)
            {
                switch (str)
                {
                    case "1":
                        operationMode = "PP";
                        break;
                    case "6":
                        operationMode = "HM";
                        break;
                    case "8":
                        operationMode = "CSP";
                        break;
                    default:
                        break;
                }
            }
        }

        private HandleTagNameMessage onHomingMethod;
        private int homingMethod = 0;
        private void OnHomingMethodCallback(IntPtr dataPtr, UInt32 slv, UInt32 ch, string str)
        {
            if (slv == torqueDriveSlave && ch == torqueDriveChannel)
            {
                homingMethod = int.Parse(str);
            }
        }

        private HandleTagNameMessage onHomingSpeed1;
        private int homingSpeed1;
        private void OnHomingSpeed1Callback(IntPtr dataPtr, UInt32 slv, UInt32 ch, string str)
        {
            if (slv == torqueDriveSlave && ch == torqueDriveChannel)
            {
                homingSpeed1 = int.Parse(str);
            }
        }

        private HandleTagNameMessage onHomingSpeed2;
        private int homingSpeed2;
        private void OnHomingSpeed2Callback(IntPtr dataPtr, UInt32 slv, UInt32 ch, string str)
        {
            if (slv == torqueDriveSlave && ch == torqueDriveChannel)
            {
                homingSpeed2 = int.Parse(str);
            }
        }

        private HandleTagNameMessage onHomingAcc;
        private int homingAcc;
        private void OnHomingAccCallback(IntPtr dataPtr, UInt32 slv, UInt32 ch, string str)
        {
            if (slv == torqueDriveSlave && ch == torqueDriveChannel)
            {
                homingAcc = int.Parse(str);
            }
        }

        private HandleTagNameMessage onProfileVelocity;
        private int profileVelocity;
        private void OnProfileVelocityCallback(IntPtr dataPtr, UInt32 slv, UInt32 ch, string str)
        {
            if (slv == torqueDriveSlave && ch == torqueDriveChannel)
            {
                profileVelocity = int.Parse(str);
            }
        }

        private HandleTagNameMessage onProfileAcc;
        private int profileAcc;
        private void OnProfileAccCallback(IntPtr dataPtr, UInt32 slv, UInt32 ch, string str)
        {
            if (slv == torqueDriveSlave && ch == torqueDriveChannel)
            {
                profileAcc = int.Parse(str);
            }
        }

        private int slavesCount = 0;
        private HandleMessage onSlavesResponding;
        private void OnSlavesRespondingCallback(IntPtr dataPtr, string str)
        {
            slavesCount = int.Parse(str);
        }

        private int slavesState = 0;
        private HandleMessage onSlavesState;
        private void OnSlavesStateCallback(IntPtr dataPtr, string str)
        {
            slavesState = int.Parse(str);
        }

        private HandleMessage onWaitingSDOs;
        private int waitingSDOs;
        private void OnWaitingSDOsCallback(IntPtr dataPtr, string str)
        {
            waitingSDOs = int.Parse(str);
        }

        private HandleMessage onErrorMessage;
        private void OnErrorMessageCallback(IntPtr dataPtr, string data)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("Error|" + data)).Start();
        }

        private HandleTagNameMessage onAxisPosition;
        private double axisPosition;
        private void OnAxisPositionCallback(IntPtr dataPtr, UInt32 n, UInt32 _, string str)
        {
            if (n == torqueAxisNumber)
            {
                axisPosition = double.Parse(str) * 1000.0;
            }
        }

        private HandleTagNameMessage onFeedbackPosition;
        private double feedbackPosition;
        private void OnFeedbackPositionCallback(IntPtr dataPtr, UInt32 n, UInt32 _, string str)
        {
            if (n == torqueAxisNumber)
            {
                feedbackPosition = double.Parse(str) * 1000.0;
            }
        }

        private double FollowingError()
        {
            return axisPosition - feedbackPosition;
        }

        private HandleTagNameMessage onEncoderLengthUnit;
        private string encoderLengthUnit = "m";
        private void OnEncoderLengthUnitCallback(IntPtr dataPtr, UInt32 n, UInt32 _, string str)
        {
            if (n == torqueAxisNumber)
            {
                encoderLengthUnit = str;
            }
        }

        private HandleTagNameMessage onEncoderPPU;
        private double encoderPPU = 0.0;
        private void OnEncoderPPUCallback(IntPtr dataPtr, UInt32 n, UInt32 _, string str)
        {
            if (n == torqueAxisNumber)
            {
                encoderPPU = double.Parse(str);
            }
        }

        private HandleTagNameMessage onDriveAlias;
        private int driveAlias;
        private void OnDriveAliasCallback(IntPtr dataPtr, UInt32 n, UInt32 _, string str)
        {
            if (n == torqueAxisNumber)
            {
                driveAlias = int.Parse(str);
            }
        }

        private HandleTagNameMessage onDriveSlavePosition;
        private int driveSlavePosition;
        private void OnDriveSlavePositionCallback(IntPtr dataPtr, UInt32 n, UInt32 _, string str)
        {
            if (n == torqueAxisNumber)
            {
                driveSlavePosition = int.Parse(str);
            }
        }

        private HandleTagNameMessage onDriveChannel;
        private int driveChannel;
        private void OnDriveChannelCallback(IntPtr dataPtr, UInt32 n, UInt32 _, string str)
        {
            if (n == torqueAxisNumber)
            {
                driveChannel = int.Parse(str);
            }
        }

        private HandleTagNameMessage onAxisVmax;
        private double axisVmax = 0.0;
        private void OnAxisVmaxCallback(IntPtr dataPtr, UInt32 n, UInt32 _, string str)
        {
            if (n == torqueAxisNumber)
            {
                axisVmax = double.Parse(str) * 1000.0 * 60.0;
            }
        }

        private HandleTagNameMessage onAxisAmax;
        private double axisAmax = 0.0;
        private void OnAxisAmaxCallback(IntPtr dataPtr, UInt32 n, UInt32 _, string str)
        {
            if (n == torqueAxisNumber)
            {
                axisAmax = double.Parse(str);
            }
        }

        private HandleTagNameMessage onGroupType;
        private string groupType = "--";
        private void OnGroupTypeCallback(IntPtr dataPtr, UInt32 n, UInt32 _, string str)
        {
            if (n == torqueGroupNumber)
            {
                groupType = str;
            }
        }

        private HandleTagNameMessage onGroupMapping;
        private string groupMapping = "--";
        private void OnGroupMappingCallback(IntPtr dataPtr, UInt32 n, UInt32 _, string str)
        {
            if (n == torqueGroupNumber)
            {
                groupMapping = str;
            }
        }


        private HandleTagNameMessage onGroupVmax;
        private double groupVmax = 0.0;
        private void OnGroupVmaxCallback(IntPtr dataPtr, UInt32 n, UInt32 _, string str)
        {
            if (n == torqueGroupNumber)
            {
                groupVmax = double.Parse(str) * 1000.0 * 60.0;
            }
        }

        private HandleTagNameMessage onGroupAmax;
        private double groupAmax = 0.0;
        private void OnGroupAmaxCallback(IntPtr dataPtr, UInt32 n, UInt32 _, string str)
        {
            if (n == torqueGroupNumber)
            {
                groupAmax = double.Parse(str);
            }
        }

        private HandleTagNameMessage onGroupJmax;
        private double groupJmax = 0.0;
        private void OnGroupJmaxCallback(IntPtr dataPtr, UInt32 n, UInt32 _, string str)
        {
            if (n == torqueGroupNumber)
            {
                groupJmax = double.Parse(str);
            }
        }

        private HandleMessage onTravelTime;
        private void OnTravelTimeCallback(IntPtr dataPtr, string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("TravelTime|" + str)).Start();
        }

        private HandleMessage onDeviceInfos;
        private void OnDeviceInfos(IntPtr dataPtr, string str)
        {
            string[] values = str.Split('.');
            torqueDriveSlave = int.Parse(values[0]);
            torqueDriveChannel = int.Parse(values[1]);
            torqueAxisNumber = int.Parse(values[2]);
            torqueGroupNumber = int.Parse(values[3]);
        }

        private HandleMessage onSystemReady;
        private Boolean systemReady = false;
        private void OnSystemReady(IntPtr dataPtr, string str)
        {
            systemReady = (int.Parse(str) != 0);
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Process thisProc = Process.GetCurrentProcess();
            thisProc.PriorityClass = ProcessPriorityClass.RealTime;

            bot = new Botnana("192.168.7.2");

            onWSError = new HandleMessage(OnWSErrorCallback);
            bot.SetOnErrorCB(IntPtr.Zero, onWSError);
            onWSOpen = new HandleMessage(OnWSOpenCallback);
            bot.SetOnOpenCB(IntPtr.Zero, onWSOpen);
            onMessage = new HandleMessage(OnMessageCallback);
            bot.SetOnMessageCB(IntPtr.Zero, onMessage);

            onUserParameter = new HandleMessage(OnUserParameterCallback);
            bot.SetTagCB(@"user_parameter", 0, IntPtr.Zero, onUserParameter);
            onRTRealTorque = new HandleMessage(OnRTRealTorqueCallback);
            bot.SetTagCB(@"rt_real_torque", 0, IntPtr.Zero, onRTRealTorque);
            onSlavesResponding = new HandleMessage(OnSlavesRespondingCallback);
            bot.SetTagCB(@"slaves_responding", 0, IntPtr.Zero, onSlavesResponding);
            onSlavesState = new HandleMessage(OnSlavesStateCallback);
            bot.SetTagCB(@"al_states", 0, IntPtr.Zero, onSlavesState);
            onWaitingSDOs = new HandleMessage(OnWaitingSDOsCallback);
            bot.SetTagCB(@"waiting_sdos_len", 0, IntPtr.Zero, onWaitingSDOs);
            onPDSState = new HandleTagNameMessage(OnPDSStateCallback);
            bot.SetTagNameCB(@"pds_state", 0, IntPtr.Zero, onPDSState);
            onDriveStatus = new HandleTagNameMessage(OnDriveStatusCallback);
            bot.SetTagNameCB(@"status_word", 0, IntPtr.Zero, onDriveStatus);
            onOperationMode = new HandleTagNameMessage(OnOperationModeCallback);
            bot.SetTagNameCB(@"operation_mode", 0, IntPtr.Zero, onOperationMode);
            onHomingMethod = new HandleTagNameMessage(OnHomingMethodCallback);
            bot.SetTagNameCB(@"homing_method", 0, IntPtr.Zero, onHomingMethod);
            onHomingSpeed1 = new HandleTagNameMessage(OnHomingSpeed1Callback);
            bot.SetTagNameCB(@"homing_speed_1", 0, IntPtr.Zero, onHomingSpeed1);
            onHomingSpeed2 = new HandleTagNameMessage(OnHomingSpeed2Callback);
            bot.SetTagNameCB(@"homing_speed_2", 0, IntPtr.Zero, onHomingSpeed2);
            onHomingAcc = new HandleTagNameMessage(OnHomingAccCallback);
            bot.SetTagNameCB(@"homing_acceleration", 0, IntPtr.Zero, onHomingAcc);
            onProfileVelocity = new HandleTagNameMessage(OnProfileVelocityCallback);
            bot.SetTagNameCB(@"profile_velocity", 0, IntPtr.Zero, onProfileVelocity);
            onProfileAcc = new HandleTagNameMessage(OnProfileAccCallback);
            bot.SetTagNameCB(@"profile_acceleration", 0, IntPtr.Zero, onProfileAcc);
            onRealPosition = new HandleTagNameMessage(OnRealPositionCallback);
            bot.SetTagNameCB(@"real_position", 0, IntPtr.Zero, onRealPosition);
            onRealTorque = new HandleTagNameMessage(OnRealTorqueCallback);
            bot.SetTagNameCB(@"real_torque", 0, IntPtr.Zero, onRealTorque);
            onDigitalInputs = new HandleTagNameMessage(OnDigitalInputsCallback);
            bot.SetTagNameCB("digital_inputs", 0, IntPtr.Zero, onDigitalInputs);
            onTargetPosition = new HandleTagNameMessage(OnTargetPositionCallback);
            bot.SetTagNameCB(@"target_position", 0, IntPtr.Zero, onTargetPosition);
            onErrorMessage = new HandleMessage(OnErrorMessageCallback);
            bot.SetTagCB(@"error", 0, IntPtr.Zero, onErrorMessage);
            onAxisPosition = new HandleTagNameMessage(OnAxisPositionCallback);
            bot.SetTagNameCB(@"axis_demand_position", 0, IntPtr.Zero, onAxisPosition);
            onFeedbackPosition = new HandleTagNameMessage(OnFeedbackPositionCallback);
            bot.SetTagNameCB(@"feedback_position", 0, IntPtr.Zero, onFeedbackPosition);
            onEncoderLengthUnit = new HandleTagNameMessage(OnEncoderLengthUnitCallback);
            bot.SetTagNameCB(@"encoder_length_unit", 0, IntPtr.Zero, onEncoderLengthUnit);
            onEncoderPPU = new HandleTagNameMessage(OnEncoderPPUCallback);
            bot.SetTagNameCB(@"encoder_ppu", 0, IntPtr.Zero, onEncoderPPU);
            onDriveAlias = new HandleTagNameMessage(OnDriveAliasCallback);
            bot.SetTagNameCB(@"drive_alias", 0, IntPtr.Zero, onDriveAlias);
            onDriveSlavePosition = new HandleTagNameMessage(OnDriveSlavePositionCallback);
            bot.SetTagNameCB(@"drive_slave_position", 0, IntPtr.Zero, onDriveSlavePosition);
            onDriveChannel = new HandleTagNameMessage(OnDriveChannelCallback);
            bot.SetTagNameCB(@"drive_channel", 0, IntPtr.Zero, onDriveChannel);
            onAxisVmax = new HandleTagNameMessage(OnAxisVmaxCallback);
            bot.SetTagNameCB(@"axis_vmax", 0, IntPtr.Zero, onAxisVmax);
            onAxisAmax = new HandleTagNameMessage(OnAxisAmaxCallback);
            bot.SetTagNameCB(@"axis_amax", 0, IntPtr.Zero, onAxisAmax);
            onGroupType = new HandleTagNameMessage(OnGroupTypeCallback);
            bot.SetTagNameCB(@"group_type", 0, IntPtr.Zero, onGroupType);
            onGroupMapping = new HandleTagNameMessage(OnGroupMappingCallback);
            bot.SetTagNameCB(@"group_mapping", 0, IntPtr.Zero, onGroupMapping);
            onGroupVmax = new HandleTagNameMessage(OnGroupVmaxCallback);
            bot.SetTagNameCB(@"group_vmax", 0, IntPtr.Zero, onGroupVmax);
            onGroupAmax = new HandleTagNameMessage(OnGroupAmaxCallback);
            bot.SetTagNameCB(@"group_amax", 0, IntPtr.Zero, onGroupAmax);
            onGroupJmax = new HandleTagNameMessage(OnGroupJmaxCallback);
            bot.SetTagNameCB(@"group_jmax", 0, IntPtr.Zero, onGroupJmax);
            onTravelTime = new HandleMessage(OnTravelTimeCallback);
            bot.SetTagCB(@"travel_time", 0, IntPtr.Zero, onTravelTime);
            onDeviceInfos = new HandleMessage(OnDeviceInfos);
            bot.SetTagCB(@"device_infos", 1, IntPtr.Zero, onDeviceInfos);
            onSystemReady = new HandleMessage(OnSystemReady);
            bot.SetTagCB(@"system_ready", 0, IntPtr.Zero, onSystemReady);

            chartTorque.ChartAreas[0].AxisX.Minimum = 0;
            chartTorque.ChartAreas[0].AxisX.Maximum = QueueCapacity;

            timer1.Interval = 10;
            timer1.Enabled = true;
            timer2.Interval = 500;
            timer2.Enabled = true;
            torqueThread = new Thread(new ThreadStart(this.getTorqueInformation));
            pauseEvent = new ManualResetEvent(false);
            torqueThread.IsBackground = true;
            torqueThread.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labMessageCount.Text = messageCount.ToString("X2");
            textSlavesCount.Text = slavesCount.ToString();
            textSlavesState.Text = slavesState.ToString();

            textPDSState.Text = pdsState;
            textOperationMode.Text = operationMode;
            textRealPosition.Text = realPosition.ToString();
            textTargetPosition.Text = targetPosition.ToString();
            textRealTorque.Text = realTorque.ToString();

            textDriveStatus.Text = driveStatus;
            textDigitalInputs.Text = string.Format("0x{0:X8}", digitalInputs);

            textAxisPosition.Text = axisPosition.ToString("F3");
            textFeedbackPosition.Text = feedbackPosition.ToString("F3");
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
                bot.EvaluateScript(torqueDriveSlave.ToString() + @" .slave-diff");
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
                bot.EvaluateScript(torqueAxisNumber.ToString() + @" .axis");
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
            bot.EvaluateScript("false rt-info-output-enabled !");
            pauseEvent.Reset();
            plotEnabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 讓 torque.sfc 中的 SFC ，每個 real time 周期都送出 real torque (用來畫圖)。
            bot.EvaluateScript("true rt-info-output-enabled !");
            pauseEvent.Set();
            plotEnabled = true;
        }

        private void textScript_Leave(object sender, EventArgs e)
        {
            // 送出手動輸入的 forth 指令
            bot.EvaluateScript(textScript.Text);
            textScript.Text = "";
        }

        private void textScript_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bot.EvaluateScript(textScript.Text);
                textScript.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 切換驅動器到 CSP Mode, 避免切換時跳異警，檢查或後誤差是否太大
            if (Math.Abs(FollowingError()) < 0.05)
            {
                bot.EvaluateScript(@"csp " + torqueDriveChannel.ToString() + @" " + torqueDriveChannel.ToString() + @" op-mode!");
            }
            else
            {
                new Thread(() => System.Windows.Forms.MessageBox.Show("Following Error Too Large")).Start();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 切換驅動器到 HM Mode,
            bot.EvaluateScript(@"hm " + torqueDriveChannel.ToString() + @" " + torqueDriveChannel.ToString() + @" op-mode!");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"pp " + torqueDriveChannel.ToString() + @" " + torqueDriveChannel.ToString() + @" op-mode!");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // 驅動器在 HM Mode 下, 如果要開始回 Home 要下 go 命令
            bot.EvaluateScript(torqueDriveChannel.ToString() + @" " + torqueDriveChannel.ToString() + @" go");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // 驅動器 Drive On 前檢查 following error
            if (Math.Abs(FollowingError()) < 0.05)
            {
                bot.EvaluateScript(torqueDriveChannel.ToString() + @" " + torqueDriveChannel.ToString() + @" drive-on");
            }
            else
            {
                new Thread(() => System.Windows.Forms.MessageBox.Show("Following Error Too Large")).Start();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // 命令驅動器暫停
            bot.EvaluateScript(torqueDriveChannel.ToString() + @" " + torqueDriveChannel.ToString() + @" +drive-halt");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // 命令驅動器繼續運作
            bot.EvaluateScript(torqueDriveChannel.ToString() + @" " + torqueDriveChannel.ToString() + @" -drive-halt");
        }

        private void buttonPull_Click(object sender, EventArgs e)
        {
            // 使用 demo.fs 內的 release-go 指令
            // release-go 是以指定的速度移動 textReleaseV 到目標點位置 textReleaseP
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

            // deploy ... ;deploy 是用來將指令送到 NC Background task 執行，因為 release-go 指令會等待位置到達後做後續的設定
            // 如果不送到 NC Background task 執行，則此 user task 會沒有回應。
            if (pass)
            {
                bot.EvaluateScript("deploy " + textReleaseP.Text + @"e mm " + textReleaseV.Text + @"e mm/min release-go ;deploy");
            }
        }

        private void buttonPush_Click(object sender, EventArgs e)
        {
            // 使用 demo.fs 內的 press-go 指令
            // press-go 是使用 1D 軸組運動
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
            if (!int.TryParse(textTorqueThreshold.Text, out torqueThreshold))
            {
                textTorqueThreshold.Text = "Error";
                textTorqueThreshold.ForeColor = Color.Red;
                pass = false;
            }

            if (!double.TryParse(textSpeedChangeDistance.Text, out speedChangeDist) || speedChangeDist < 0.001)
            {
                textSpeedChangeDistance.Text = "Error";
                textSpeedChangeDistance.ForeColor = Color.Red;
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

            // deploy ... ;deploy 是用來將指令送到 NC Background task 執行，因為 press-go 指令會偵測扭力並等待位置到達後做後續的設定
            // 如果不送到 NC Background task 執行，則此 user task 會沒有回應。
            if (pass)
            {
                string cmd = textTorqueThreshold.Text + " tq-threshold ! " + textSpeedChangeDistance.Text + "e mm speed-change-distance f!";
                cmd += " deploy " + textPushP1.Text + @"e mm " + textPushP2.Text + @"e mm " + textPushV1.Text + @"e mm/min " + textPushV2.Text + @"e mm/min press-go ;deploy";
                bot.EvaluateScript(cmd);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // 停止軸組運動指令
            bot.EvaluateScript(@"stop-job");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            // ems-job   緊急停止軸組運動
            // drive-off 將驅動器 drive-off
            // kill-nc   移除 background task 內的工作
            // resume    resume background task
            bot.EvaluateScript(@"ems-job " + torqueDriveChannel.ToString() + @" " + torqueDriveChannel.ToString() + @" drive-off kill-nc");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            string cmd = null;
            if (hasSFC) { cmd += @" poll"; }

            // ".ec-links" 此命令會只回傳 EtherCAT 的連線狀態
            // slaves_responding|5|al_states|8|link_up|1|input_wc|2|output_wc|2|input_wc_state|1
            // |output_wc_state|1|input_wc_error|179|output_wc_error|213|waiting_sdos_len|0
            // 
            // slaves_responding 表示有多少從站連線
            // al_states: 8 表示所有從站都是在 Operation 狀態 
            cmd += @" .ec-links";

            bot.EvaluateScript(cmd);

            if (!homingMethodAccessed)
            {
                textHomingMethod.Text = homingMethod.ToString();
            }
            if (!homingSpeed1Accessed)
            {
                textHomingSpeed1.Text = homingSpeed1.ToString();
            }
            if (!homingSpeed2Accessed)
            {
                textHomingSpeed2.Text = homingSpeed2.ToString();
            }

            if (!homingAccAccessed)
            {
                textHomingAcc.Text = homingAcc.ToString();
            }

            if (!encoderPPUAccessed)
            {
                textEncoderPPU.Text = encoderPPU.ToString("F1");
            }
            labelEncoderResolution.Text = "pulse/" + encoderLengthUnit.Substring(0, 1).ToLower();

            if (!axisVmaxAccessed)
            {
                textAxisVmax.Text = axisVmax.ToString("F1");
            }
            if (!axisAmaxAccessed)
            {
                textAxisAmax.Text = axisAmax.ToString("F1");
            }
            if (!driveAliasAccessed)
            {
                textDriveAlias.Text = driveAlias.ToString();
            }
            if (!driveSlavePositionAccessed)
            {
                textDriveSlavePosition.Text = driveSlavePosition.ToString();
            }
            if (!driveChannelAccessed)
            {
                textDriveChannel.Text = driveChannel.ToString();
            }

            textGroupType.Text = groupType;

            if (!groupMappingAccessed)
            {
                textGroupMapping.Text = groupMapping;
            }
            if (!groupVmaxAccessed)
            {
                textGroupVmax.Text = groupVmax.ToString("F1");
            }
            if (!groupAmaxAccessed)
            {
                textGroupAmax.Text = groupAmax.ToString("F1");
            }
            if (!groupJmaxAccessed)
            {
                textGroupJmax.Text = groupJmax.ToString("F1");
            }

            // 處理 WebSocket 的連線狀態
            if (wsState == 2)
            {
                buttonWSState.BackColor = Color.LightGreen;
            }
            else if (wsState == 1)
            {
                buttonWSState.BackColor = Color.Gold;
            }
            else
            {
                // 自動連線
                bot.Connect();
                wsState = 1;
                buttonWSState.BackColor = Color.IndianRed;
            }

            // 處理 System Ready 狀態顯示
            if (systemReady)
            {
                buttonSystemState.BackColor = Color.LightGreen;
            } else
            {
                buttonSystemState.BackColor = Color.IndianRed;
            }

            switch (pdsState)
            {
                case "Operation Enabled":
                    buttonDriveOn.BackColor = Color.LightGreen;
                    buttonDriveOff.BackColor = SystemColors.Control;
                    buttonResetFault.BackColor = SystemColors.Control;
                    break;
                case "Fault":
                    buttonDriveOn.BackColor = SystemColors.Control;
                    buttonDriveOff.BackColor = Color.IndianRed;
                    buttonResetFault.BackColor = Color.IndianRed;
                    break;
                default:
                    buttonDriveOn.BackColor = SystemColors.Control;
                    buttonDriveOff.BackColor = Color.IndianRed;
                    buttonResetFault.BackColor = SystemColors.Control;
                    break;

            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            // 清除軸組落後誤差 (以實際位置修正目標位置)
            bot.EvaluateScript(torqueGroupNumber.ToString() + @" 0axis-ferr");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // 驅動器 OFF
            bot.EvaluateScript(torqueDriveChannel.ToString() + @" " + torqueDriveChannel.ToString() + @" drive-off");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            // 當驅動器發生異警時，要先清除異警
            bot.EvaluateScript(torqueDriveChannel.ToString() + @" " + torqueDriveChannel.ToString() + @" reset-fault");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            // 停止軸組運動
            bot.EvaluateScript(@"stop-job");
        }

        private bool homingMethodAccessed = false;
        private void textHomingMethod_Enter(object sender, EventArgs e)
        {
            homingMethodAccessed = true;
        }

        private void textHomingMethod_Leave(object sender, EventArgs e)
        {
            // 設定回 Home 模式
            // textHomingMethod.Text = 1 : 往負方向運動，碰到極限開關後，反轉找第一個 pulse index
            // textHomingMethod.Text = 2 : 往正方向運動，碰到極限開關後，反轉找第一個 pulse index
            bot.EvaluateScript(textHomingMethod.Text + @" " + torqueDriveChannel.ToString() + @" " + torqueDriveChannel.ToString() + @" homing-method!");
            homingMethodAccessed = false;
        }

        private bool homingSpeed1Accessed = false;
        private void textHomingSpeed1_Leave(object sender, EventArgs e)
        {
            // 設定回 Home 模式中找極限開關的速度，單位通常是 pulse/s，不同廠牌驅動器可能會不同
            bot.EvaluateScript(textHomingSpeed1.Text + @" " + torqueDriveChannel.ToString() + @" " + torqueDriveChannel.ToString() + @" homing-v1!");
            homingSpeed1Accessed = false;
        }

        private void textHomingSpeed1_Enter(object sender, EventArgs e)
        {
            homingSpeed1Accessed = true;
        }

        private bool homingSpeed2Accessed = false;
        private void textHomingSpeed2_Leave(object sender, EventArgs e)
        {
            // 設定回 Home 模式中找 pulse index 的速度，單位通常是 pulse/s，不同廠牌驅動器可能會不同
            bot.EvaluateScript(textHomingSpeed2.Text + @" " + torqueDriveChannel.ToString() + @" " + torqueDriveChannel.ToString() + @" homing-v2!");
            homingSpeed2Accessed = false;
        }

        private void textHomingSpeed2_Enter(object sender, EventArgs e)
        {
            homingSpeed2Accessed = true;
        }

        private bool homingAccAccessed = false;
        private void textHomingAcc_Leave(object sender, EventArgs e)
        {
            // 設定回 Home 模式中的加速度，單位通常是 pulse/s^2，不同廠牌驅動器可能會不同
            bot.EvaluateScript(textHomingAcc.Text + @" " + torqueDriveChannel.ToString() + @" " + torqueDriveChannel.ToString() + @" homing-a!");
            homingAccAccessed = false;
        }

        private void textHomingAcc_Enter(object sender, EventArgs e)
        {
            homingAccAccessed = true;
        }

        private bool encoderPPUAccessed = false;
        private void textEncoderPPU_Leave(object sender, EventArgs e)
        {
            // 設定單位距離有幾個 encoder pulse 數量
            bot.EvaluateScript(textEncoderPPU.Text + @"e " + torqueAxisNumber.ToString() + @" enc-ppu! " + torqueAxisNumber.ToString() + @" .axiscfg");
            encoderPPUAccessed = false;
        }

        private void textEncoderPPU_Enter(object sender, EventArgs e)
        {
            encoderPPUAccessed = true;
        }

        private bool axisVmaxAccessed = false;
        private void textAxisVmax_Leave(object sender, EventArgs e)
        {
            // 設定運動軸的最大速度 (主站使用的是 SI 單位，線性軸是 m/s)，設定完成後取回軸設定參數 (.axiscfg)，
            // 用來看參數是否設定正確
            bot.EvaluateScript(textAxisVmax.Text + @"e mm/min " + torqueAxisNumber.ToString() + @" axis-vmax! " + torqueAxisNumber.ToString() + @" .axiscfg");
            axisVmaxAccessed = false;
        }

        private void textAxisVmax_Enter(object sender, EventArgs e)
        {
            axisVmaxAccessed = true;
        }

        private bool axisAmaxAccessed = false;
        private void textAxisAmax_Leave(object sender, EventArgs e)
        {
            // 設定運動軸的最大加速度 (主站使用的是 SI 單位，線性軸是 m/s^2)，設定完成後取回軸設定參數 (.axiscfg)，
            // 用來看參數是否設定正確  
            bot.EvaluateScript(textAxisAmax.Text + @"e " + torqueAxisNumber.ToString() + @" axis-amax! " + torqueAxisNumber.ToString() + @" .axiscfg");
            axisAmaxAccessed = false;
        }

        private void textAxisAmax_Enter(object sender, EventArgs e)
        {
            axisAmaxAccessed = true;
        }

        private bool driveAliasAccessed = false;
        private void textDriveAlias_Leave(object sender, EventArgs e)
        {
            // 設定運動軸的對應的驅動器 alias，如果設定為 0，表示依 slave position，如果沒有對應的驅動器，則是虛擬運動軸。
            // 設定完成後取回軸設定參數 (.axiscfg)，用來看參數是否設定正確
            bot.EvaluateScript(textDriveAlias.Text + @" " + torqueAxisNumber.ToString() + @" drv-alias! " + torqueAxisNumber.ToString() + @" .axiscfg");
            driveAliasAccessed = false;
        }

        private void textDriveAlias_Enter(object sender, EventArgs e)
        {
            driveAliasAccessed = true;
        }

        private bool driveSlavePositionAccessed = false;
        private void textDriveSlavePosition_Leave(object sender, EventArgs e)
        {
            // 設定運動軸的對應的驅動器 slave position，如果沒有對應的驅動器，則是虛擬運動軸。
            // 設定完成後取回軸設定參數 (.axiscfg)，用來看參數是否設定正確
            bot.EvaluateScript(textDriveSlavePosition.Text + @" " + torqueAxisNumber.ToString() + @" drv-slave! " + torqueAxisNumber.ToString() + @" .axiscfg");
            driveSlavePositionAccessed = false;
        }

        private void textDriveSlavePosition_Enter(object sender, EventArgs e)
        {
            driveSlavePositionAccessed = true;
        }

        private bool driveChannelAccessed = false;
        private void textDriveChannel_Leave(object sender, EventArgs e)
        {
            // 設定運動軸的對應的驅動器的 Channel，如果沒有對應的驅動器，則是虛擬運動軸。
            // 設定完成後取回軸設定參數 (.axiscfg)，用來看參數是否設定正確
            bot.EvaluateScript(textDriveChannel.Text + @" " + torqueAxisNumber.ToString() + @" drv-channel! " + torqueAxisNumber.ToString() + @" .axiscfg");
            driveChannelAccessed = false;
        }

        private void textDriveChannel_Enter(object sender, EventArgs e)
        {
            driveChannelAccessed = true;
        }

        private bool groupMappingAccessed = false;
        private void textGroupMapping_Leave(object sender, EventArgs e)
        {
            // 設定軸組對應的運動軸，設定完成後取回軸組設定參數 (.grpcfg)，用來看參數是否設定正確
            bot.EvaluateScript(textGroupMapping.Text + @" " + torqueGroupNumber.ToString() + @" gmap! " + torqueGroupNumber.ToString() + @" .grpcfg");
            groupMappingAccessed = false;
        }

        private void textGroupMapping_Enter(object sender, EventArgs e)
        {
            groupMappingAccessed = true;
        }

        private bool groupVmaxAccessed = false;
        private void textGroupVmax_Leave(object sender, EventArgs e)
        {
            // 設定軸組對應的運動軸的最大速度，設定完成後取回軸組設定參數 (.grpcfg)，用來看參數是否設定正確
            bot.EvaluateScript(textGroupVmax.Text + @"e mm/min " + torqueGroupNumber.ToString() + @" gvmax! " + torqueGroupNumber.ToString() + @" .grpcfg");
            groupVmaxAccessed = false;
        }

        private void textGroupVmax_Enter(object sender, EventArgs e)
        {
            groupVmaxAccessed = true;
        }

        private bool groupAmaxAccessed = false;
        private void textGroupAmax_Leave(object sender, EventArgs e)
        {
            // 設定軸組對應的運動軸的最大加速度，設定完成後取回軸組設定參數 (.grpcfg)，用來看參數是否設定正確
            bot.EvaluateScript(textGroupAmax.Text + @"e " + torqueGroupNumber.ToString() + @" gamax! " + torqueGroupNumber.ToString() + @" .grpcfg");
            groupAmaxAccessed = false;
        }

        private void textGroupAmax_Enter(object sender, EventArgs e)
        {
            groupAmaxAccessed = true;
        }

        private bool groupJmaxAccessed = false;
        private void textGroupJmax_Leave(object sender, EventArgs e)
        {
            // 設定軸組對應的運動軸的最大加加速度，設定完成後取回軸組設定參數 (.grpcfg)，用來看參數是否設定正確
            bot.EvaluateScript(textGroupJmax.Text + @"e " + torqueGroupNumber.ToString() + @" gjmax! " + torqueGroupNumber.ToString() + @" .grpcfg");
            groupJmaxAccessed = false;
        }

        private void textGroupJmax_Enter(object sender, EventArgs e)
        {
            groupJmaxAccessed = true;
        }

        private void textTorqueThreshold_Leave(object sender, EventArgs e)
        {
            textTorqueThreshold.ForeColor = Color.Black;
        }

        private void textSpeedChangeDistance_Leave(object sender, EventArgs e)
        {
            textSpeedChangeDistance.ForeColor = Color.Black;
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

        private void buttonReloadSFC_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"0sfc 0 user-para! .user-para");
            hasSFC = false;
            systemReady = false;
        }
    }
}

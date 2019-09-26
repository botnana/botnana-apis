using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using BotnanaLib;

namespace MoldFeeder
{
    public partial class Form1 : Form
    {
        const string DriveChannelSlave = @" 1 1 ";
        const string DriveTag = @".1.1";
        const string DriveSlaveTag = @".1";
        const string CylinderChannelSlave = @" 1 3 ";
        const string CylinderTag = @".1.3";
        const string AxisIndex = @" 1 ";
        
        private Botnana bot;


        // 處理 WS Error or close 時的事件
        private int wsState = 0;
        private HandleMessage onWSError;
        public void OnWSErrorCallback(IntPtr dataPtr, string data)
        {
            wsState = 0;
        }

        // 處理 WS Open 時的事件
        private HandleMessage onWSOpen;
        public void OnWSOpenCallback(IntPtr dataPtr, string data)
        {
            wsState = 2;
            // 送出 .user-para 命令，讓為回傳的訊息去觸發 OnUserParameterCallback
            bot.EvaluateScript(".user-para");
        }

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
                    bot.LoadSFC(@"../../sfc.fs");
                    bot.LoadSFC(@"../../sdo_upload.fs");
                    bot.LoadSFC(@"../../manager.fs");
                    bot.EvaluateScript(@"marker -feeder .user-para");
                    //new Thread(() => System.Windows.Forms.MessageBox.Show("OnUserParameterCallback 0")).Start();
                    break;
                case 16:
                    bot.EvaluateScript(@"reset-overrun 32 user-para! .user-para");
                    //new Thread(() => System.Windows.Forms.MessageBox.Show("OnUserParameterCallback 16")).Start();
                    break;
                case 32:
                    // Thread(() => System.Windows.Forms.MessageBox.Show("OnUserParameterCallback 32")).Start();
                    bot.EvaluateScript(@".feeder-para");
                    hasSFC = true;
                    break;
                default:
                    break;
            }
        }
        
        // 收到 Error 的 tag 時的處置
        private HandleMessage onErrorMessage;
        private int errorsLen = 0;
        private void OnErrorMessageCallback(IntPtr dataPtr, string str)
        {
            if (errorsLen < 3)
            {
                new Thread(() => System.Windows.Forms.MessageBox.Show("Error|" + str)).Start();
                errorsLen += 1;
            }
        }

        private HandleMessage onSlaves;
        private int slavesLen = 0;
        private void OnSlaves(IntPtr dataPtr, string str)
        {
            slavesLen = int.Parse(str);
        }

        private HandleMessage onEcReady;
        private Boolean ecReady = false;
        private void OnEcReady(IntPtr dataPtr, string str)
        {
            ecReady = (int.Parse(str) != 0);
        }

        private HandleMessage onEncoderPosition;
        private int encoderPosition = 0;
        private void OnEncoderPosition(IntPtr dataPtr, string str)
        {
            encoderPosition = int.Parse(str);
        }
        
        private HandleMessage onTargetPosition;
        private int targetPosition = 0;
        private void OnTargetPosition(IntPtr dataPtr, string str)
        {
            targetPosition = int.Parse(str);
        }

        private Boolean isDriveOn = false;
        private Boolean isDriveFault = true;
        private HandleMessage onDriveStatus;
        private void OnDriveStatus(IntPtr dataPtr, string str)
        {
            int code = Convert.ToInt32(str, 16);
            isDriveFault = (code & 0x48) == 8;
            isDriveOn = (code & 0x6F) == 0x27;
        }

        private int operationMode = 0;
        private HandleMessage onOperationMode;
        private void OnOperationMode(IntPtr dataPtr, string str)
        {
            operationMode = int.Parse(str);
        }

        private Boolean driveExt1 = false;
        private Boolean driveExt2 = false;
        private Boolean driveDin0 = false;
        private Boolean driveDin1 = false;
        private Boolean driveDin2 = false;
        private Boolean driveDin3 = false;
        private HandleMessage onDriveDigitalInputs;
        private void OnDriveDigitalInputs(IntPtr dataPtr, string str)
        {
            int code = Convert.ToInt32(str, 16);
            driveExt1 = (code & 0x10000) != 0;
            driveExt2 = (code & 0x20000) != 0;
            driveDin0 = (code & 0x1000000) != 0;
            driveDin1 = (code & 0x2000000) != 0;
            driveDin2 = (code & 0x4000000) != 0;
            driveDin3 = (code & 0x8000000) != 0;
        }

        private HandleMessage onCylinder;
        private Boolean cylinderOn = false;
        private void OnCylinder(IntPtr dataPtr, string str)
        {
            cylinderOn = int.Parse(str)!= 0;
        }

        
        private HandleMessage onTouchProbe;
        private Boolean enableTP1 = false;
        private Boolean enableTP2 = false;
        private Boolean tp1TriggerAction = false;
        private Boolean tp2TriggerAction = false;
        private Boolean tp1TriggerSelection = false;
        private Boolean tp2TriggerSelection = false;
        private Boolean tp1UpEdgeAction = false;
        private Boolean tp2UpEdgeAction = false;
        private Boolean tp1DownEdgeAction = false;
        private Boolean tp2DownEdgeAction = false;
        private void OnTouchProbe(IntPtr dataPtr, string str)
        {
            int tmp = int.Parse(str);
            enableTP1 = (tmp & 1) != 0;
            enableTP2 = (tmp & 0x100) != 0;
            tp1TriggerAction = (tmp & 0x2) != 0;
            tp2TriggerAction = (tmp & 0x200) != 0;
            tp1TriggerSelection = (tmp & 0x4) != 0;
            tp2TriggerSelection = (tmp & 0x400) != 0;
            tp1UpEdgeAction = (tmp & 0x10) != 0;
            tp2UpEdgeAction = (tmp & 0x1000) != 0;
            tp1DownEdgeAction = (tmp & 0x20) != 0;
            tp2DownEdgeAction = (tmp & 0x2000) != 0;
        }

        private HandleMessage onTP1LatchPosition;
        private int tp1LatchPosition = 123;
        private void OnTP1LatchPosition(IntPtr dataPtr, string str)
        {
            tp1LatchPosition = int.Parse(str);
        }

        private HandleMessage onTP2LatchPosition;
        private int tp2LatchPosition = 123;
        private void OnTP2LatchPosition(IntPtr dataPtr, string str)
        {
            tp2LatchPosition = int.Parse(str);
        }

        private HandleMessage onCylinderOnMs;
        private int cylinderOnMs = 0;
        private Boolean hasCylinderOnMs = false;
        private void OnCylinderOnMs(IntPtr dataPtr, string str)
        {
            cylinderOnMs = int.Parse(str);
            hasCylinderOnMs = true;
        }

        private HandleMessage onCylinderOffMs;
        private int cylinderOffMs = 0;
        private Boolean hasCylinderOffMs = false;
        private void OnCylinderOffMs(IntPtr dataPtr, string str)
        {
            cylinderOffMs = int.Parse(str);
            hasCylinderOffMs = true;
        }

        private HandleMessage onFeederRunning;
        private Boolean feederRunning = false;
        private void OnFeederRunning(IntPtr dataPtr, string str)
        {
            feederRunning = (int.Parse(str) != 0);
        }

        private HandleMessage onFeederEMS;
        private Boolean feederEMS = true;
        private void OnFeederEMS(IntPtr dataPtr, string str)
        {
            feederEMS = (int.Parse(str) != 0);
        }

        private HandleMessage onFeederOperationMs;
        private int feederOperationMs = 0;
        private void OnFeederOperationMs(IntPtr dataPtr, string str)
        {
            feederOperationMs = int.Parse(str);
        }

        private HandleMessage onRotationDistance;
        private int rotationDistance = 0;
        private Boolean hasRotationDistance = false;
        private void OnRotationDistance(IntPtr dataPtr, string str)
        {
            rotationDistance = int.Parse(str);
            hasRotationDistance = true;
        }

        private HandleMessage onRotationSpeed;
        private int rotationSpeed = 0;
        private Boolean hasRotationSpeed = false;
        private void OnRotationSpeed(IntPtr dataPtr, string str)
        {
            rotationSpeed = int.Parse(str);
            hasRotationSpeed = true;
        }

        private HandleMessage onTPStatus;
        private Boolean tp1Enabled = false;
        private Boolean tp2Enabled = false;
        private void OnTPStatus(IntPtr dataPtr, string str)
        {
            int code;
            if (Int32.TryParse(str, out code))
            {
                tp1Enabled = (code & 0x1) != 0;
                tp2Enabled = (code & 0x100) != 0;
            }
        }

        private HandleMessage onTP1Detected;
        private Boolean tp1Detected = false;
        private void OnTP1Detected(IntPtr dataPtr, string str)
        {
            tp1Detected = (int.Parse(str) != 0);
        }

        private HandleMessage onTP2Detected;
        private Boolean tp2Detected = false;
        private void OnTP2Detected(IntPtr dataPtr, string str)
        {
            tp2Detected = (int.Parse(str) != 0);
        }

        private HandleMessage onTP1DetectedPosition;
        private int tp1DetectedPosition = 0;
        private void OnTP1DetectedPosition(IntPtr dataPtr, string str)
        {
            tp1DetectedPosition = int.Parse(str);
        }

        private HandleMessage onTP2DetectedPosition;
        private int tp2DetectedPosition = 0;
        private void OnTP2DetectedPosition(IntPtr dataPtr, string str)
        {
            tp2DetectedPosition = int.Parse(str);
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            bot = new Botnana("192.168.7.2");

            // WS 連線錯誤就呼叫 OnWSErrorCallback
            onWSError = new HandleMessage(OnWSErrorCallback);
            bot.SetOnErrorCB(IntPtr.Zero, onWSError);

            // WS 連線成功就呼叫 OnWSOpenCallback
            onWSOpen = new HandleMessage(OnWSOpenCallback);
            bot.SetOnOpenCB(IntPtr.Zero, onWSOpen);

            onUserParameter = new HandleMessage(OnUserParameterCallback);
            bot.SetTagCB(@"user_parameter", 0, IntPtr.Zero, onUserParameter);

            // 收到 tag = error 就呼叫 OnErrorMessageCallback
            onErrorMessage = new HandleMessage(OnErrorMessageCallback);
            bot.SetTagCB(@"error", 0, IntPtr.Zero, onErrorMessage);

            onSlaves = new HandleMessage(OnSlaves);
            bot.SetTagCB(@"slaves_responding", 0, IntPtr.Zero, onSlaves);

            onEcReady = new HandleMessage(OnEcReady);
            bot.SetTagCB(@"ec_ready", 0, IntPtr.Zero, onEcReady);

            onEncoderPosition = new HandleMessage(OnEncoderPosition);
            bot.SetTagCB(@"real_position"+DriveTag, 0, IntPtr.Zero, onEncoderPosition);

            onTargetPosition = new HandleMessage(OnTargetPosition);
            bot.SetTagCB(@"target_position" + DriveTag, 0, IntPtr.Zero, onTargetPosition);

            onDriveStatus = new HandleMessage(OnDriveStatus);
            bot.SetTagCB(@"status_word" + DriveTag, 0, IntPtr.Zero, onDriveStatus);

            onOperationMode = new HandleMessage(OnOperationMode);
            bot.SetTagCB(@"operation_mode" + DriveTag, 0, IntPtr.Zero, onOperationMode);

            onDriveDigitalInputs = new HandleMessage(OnDriveDigitalInputs);
            bot.SetTagCB(@"digital_inputs" + DriveTag, 0, IntPtr.Zero, onDriveDigitalInputs);

            onTouchProbe = new HandleMessage(OnTouchProbe);
            bot.SetTagCB(@"touch_probe_function" + DriveTag, 0, IntPtr.Zero, onTouchProbe);

            onCylinder = new HandleMessage(OnCylinder);
            bot.SetTagCB(@"dout"+CylinderTag, 0, IntPtr.Zero, onCylinder);

            onTP1LatchPosition = new HandleMessage(OnTP1LatchPosition);
            bot.SetTagCB(@"tp1_positive_value" + DriveTag, 0, IntPtr.Zero, onTP1LatchPosition);
            
            onTP2LatchPosition = new HandleMessage(OnTP2LatchPosition);
            bot.SetTagCB(@"tp2_negative_value" + DriveTag, 0, IntPtr.Zero, onTP2LatchPosition);

            onCylinderOnMs = new HandleMessage(OnCylinderOnMs);
            bot.SetTagCB(@"cylinder_on_duration", 0, IntPtr.Zero, onCylinderOnMs);

            onCylinderOffMs = new HandleMessage(OnCylinderOffMs);
            bot.SetTagCB(@"cylinder_off_duration", 0, IntPtr.Zero, onCylinderOffMs);

            onRotationDistance = new HandleMessage(OnRotationDistance);
            bot.SetTagCB(@"feeder_rotation_distance", 0, IntPtr.Zero, onRotationDistance);

            onRotationSpeed = new HandleMessage(OnRotationSpeed);
            bot.SetTagCB(@"feeder_rotation_speed", 0, IntPtr.Zero, onRotationSpeed);

            onFeederRunning = new HandleMessage(OnFeederRunning);
            bot.SetTagCB(@"feeder_running", 0, IntPtr.Zero, onFeederRunning);

            onFeederEMS = new HandleMessage(OnFeederEMS);
            bot.SetTagCB(@"feeder_ems", 0, IntPtr.Zero, onFeederEMS);

            onFeederOperationMs = new HandleMessage(OnFeederOperationMs);
            bot.SetTagCB(@"feeder_operation_ms", 0, IntPtr.Zero, onFeederOperationMs);

            onTPStatus = new HandleMessage(OnTPStatus);
            bot.SetTagCB(@"sdo.0.24761"+ DriveSlaveTag, 0, IntPtr.Zero, onTPStatus);

            onTP1Detected = new HandleMessage(OnTP1Detected);
            bot.SetTagCB(@"tp1_detected", 0, IntPtr.Zero, onTP1Detected);

            onTP2Detected = new HandleMessage(OnTP2Detected);
            bot.SetTagCB(@"tp2_detected", 0, IntPtr.Zero, onTP2Detected);

            onTP1DetectedPosition = new HandleMessage(OnTP1DetectedPosition);
            bot.SetTagCB(@"tp1_detected_position", 0, IntPtr.Zero, onTP1DetectedPosition);

            onTP2DetectedPosition = new HandleMessage(OnTP2DetectedPosition);
            bot.SetTagCB(@"tp2_detected_position", 0, IntPtr.Zero, onTP2DetectedPosition);
        }

        private void timerSlow_Tick(object sender, EventArgs e)
        {
            // 處理 WebSocket 的連線狀態
            if (wsState == 2)
            {
                buttonWs.BackColor = Color.SpringGreen;
            }
            else if (wsState == 1)
            {
                buttonWs.BackColor = Color.Gold;
            }
            else
            {
                // 自動連線
                bot.Connect();
                wsState = 1;
                buttonWs.BackColor = Color.IndianRed;
            }


            if (ecReady)
            {
                buttonEtherCAT.BackColor = Color.SpringGreen;
            }
            else
            {
                buttonEtherCAT.BackColor = Color.IndianRed;
            }
            buttonEtherCAT.Text = @"ECAT(" + slavesLen.ToString() + @")";
            
            // 當有錯誤發生時，需要 Acked 才會顯示下一個錯誤訊息
            if (errorsLen == 0)
            {
                buttonError.BackColor = Color.SpringGreen;
                buttonError.Text = "Acked";
            }
            else
            {
                buttonError.BackColor = Color.IndianRed;
                buttonError.Text = "Error";
            }

            // 當有錯誤發生時，需要 Acked 才會顯示下一個錯誤訊息
            if (hasSFC)
            {
                buttonHasSFC.BackColor = Color.SpringGreen;
            }
            else
            {
                buttonHasSFC.BackColor = Color.IndianRed;
            }
        }

        private void buttonErrorAck_Click(object sender, EventArgs e)
        {
            errorsLen = 0;
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void textEncoderPosition_TextChanged(object sender, EventArgs e)
        {

        }

        private Boolean hasSlaves = false;
        private void timerPoll_Tick(object sender, EventArgs e)
        {
            string cmd = null;
            if (slavesLen > 0)
            {
                
                if (hasSlaves)
                {
                    for (int i = 1; i <= slavesLen; i++)
                    {
                        cmd += (i.ToString() + @" .slave-diff ");
                    }
                }
                else
                {
                    for (int i = 1; i <= slavesLen; i++)
                    {
                        cmd += (i.ToString() + @" .slave ");
                    }
                    hasSlaves = true;
                }
            }
            cmd += @" .feeder .ec-links";
            bot.EvaluateScript(cmd);

            textEncoderPosition.Text = encoderPosition.ToString();
            textTargetPosition.Text = targetPosition.ToString();
            radioDriveOn.Checked = isDriveOn;
            radioDriveFault.Checked = isDriveFault;
            textOperationMode.Text = operationMode.ToString();
            textTP1LatchPosition.Text = tp1LatchPosition.ToString();
            textTP2LatchPosition.Text = tp2LatchPosition.ToString();


            radioDriveDin0.Checked = driveDin0;
            radioDriveDin1.Checked = driveDin1;
            radioDriveDin2.Checked = driveDin2;
            radioDriveDin3.Checked = driveDin3;
            radioDriveExt1.Checked = driveExt1;
            radioDriveExt2.Checked = driveExt2;
            radioCylinder.Checked = cylinderOn;
            radioEnableTP1.Checked = enableTP1;
            radioEnableTP2.Checked = enableTP2;
            radioTP1TriggerAction.Checked = tp1TriggerAction;
            radioTP2TriggerAction.Checked = tp2TriggerAction;
            radioTP1TriggerSelection.Checked = tp1TriggerSelection;
            radioTP2TriggerSelection.Checked = tp2TriggerSelection;
            radioTP1UpEdgeAction.Checked = tp1UpEdgeAction;
            radioTP2UpEdgeAction.Checked = tp2UpEdgeAction;
            radioTP1DownEdgeAction.Checked = tp1DownEdgeAction;
            radioTP2DownEdgeAction.Checked = tp2DownEdgeAction;
            radioTP1Enabled.Checked = tp1Enabled;
            radioTP2Enabled.Checked = tp2Enabled;
            radioTP1Detected.Checked = tp1Detected;
            radioTP2Detected.Checked = tp2Detected;
            textTP1DetectedPosition.Text = tp1DetectedPosition.ToString();
            textTP2DetectedPosition.Text = tp2DetectedPosition.ToString();

            if (hasCylinderOnMs)
            {
                textCylinderOnMs.Text = cylinderOnMs.ToString();
                hasCylinderOnMs = false;
            }

            if (hasCylinderOffMs)
            {
                textCylinderOffMs.Text = cylinderOffMs.ToString();
                hasCylinderOffMs = false;
            }

            if (hasRotationDistance)
            {
                textRotationDistance.Text = rotationDistance.ToString();
                hasRotationDistance = false;
            }

            if (hasRotationSpeed)
            {
                textRotationSpeed.Text = rotationSpeed.ToString();
                hasRotationSpeed = false;
            }


            radioFeederRunning.Checked = feederRunning;
            radioFeederEMS.Checked = feederEMS;


            textFeederOperationTime.Text = feederOperationMs.ToString();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"+coordinator 1 0axis-ferr" + DriveChannelSlave + @"real-p@" + DriveChannelSlave + @"target-p! csp" + DriveChannelSlave + @"op-mode!" + DriveChannelSlave + @"drive-on");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(DriveChannelSlave + @"drive-off");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(DriveChannelSlave + @"reset-fault");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bot.Reboot();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"0sfc 0 user-para! .user-para ");
            hasSFC = false;
        }

        private void buttonJogPositive_MouseDown(object sender, MouseEventArgs e)
        {
            bot.EvaluateScript(textJogSpeed.Text + @"e feeder-jog+");
        }

        private void buttonJogPositive_MouseUp(object sender, MouseEventArgs e)
        {
            bot.EvaluateScript(@"feeder-jog-stop");
        }

        private void buttonJogNegative_MouseUp(object sender, MouseEventArgs e)
        {
            bot.EvaluateScript(@"feeder-jog-stop");
        }

        private void buttonJogNegative_MouseDown(object sender, MouseEventArgs e)
        {
            bot.EvaluateScript(textJogSpeed.Text + @"e feeder-jog-");
        }

        private void buttonCylinderOn_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"1" + CylinderChannelSlave + @"ec-dout!");
        }

        private void buttonCylinderOff_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"0" + CylinderChannelSlave + @"ec-dout!");
        }

        private void buttonEvaluate_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(textEvaluate.Text);
            textEvaluate.Text = "";
        }
              
        private void textCylinderOnMs_Leave(object sender, EventArgs e)
        {
            bot.EvaluateScript(textCylinderOnMs.Text + @" cylinder-on-duration! .feeder-para");
        }

        private void textCylinderOnMs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bot.EvaluateScript(textCylinderOnMs.Text + @" cylinder-on-duration! .feeder-para");
            }
        }

        private void textCylinderOffMs_Leave(object sender, EventArgs e)
        {
            bot.EvaluateScript(textCylinderOffMs.Text + @" cylinder-off-duration! .feeder-para");
        }

        private void textCylinderOffMs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bot.EvaluateScript(textCylinderOffMs.Text + @" cylinder-off-duration! .feeder-para");
            }
        }

        private void buttonStartFeeder_Click(object sender, EventArgs e)
        {
            feederOperationMs = 0;
            bot.EvaluateScript(@"start-feeder");
        }

        private void textRotationDistance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bot.EvaluateScript(textRotationDistance.Text + @" feeder-rotation-distance! .feeder-para");
            }
        }

        private void textRotationDistance_Leave(object sender, EventArgs e)
        {
            bot.EvaluateScript(textRotationDistance.Text + @" feeder-rotation-distance! .feeder-para");
        }

        private void textRotationSpeed_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bot.EvaluateScript(textRotationSpeed.Text + @" feeder-rotation-speed! .feeder-para");
            }
        }

        private void textRotationSpeed_Leave(object sender, EventArgs e)
        {
            bot.EvaluateScript(textRotationSpeed.Text + @" feeder-rotation-speed! .feeder-para");
        }

        private void buttonFeederEMS_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"ems-feeder");
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void buttonReleaseFeeder_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"release-feeder");
        }

        private void buttonJogPositive_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"send-param-request");
        }

        private Boolean sdoRequestSuspended = false;
        private void timer1s_Tick(object sender, EventArgs e)
        {
            if (hasSFC & !sdoRequestSuspended)
            {
                bot.EvaluateScript(@"send-param-request");
            }
         }

        private void buttonSdoRequest_Click(object sender, EventArgs e)
        {
            if (sdoRequestSuspended)
            {
                sdoRequestSuspended = false;
                buttonSdoRequest.Text = @"SDO Suspend";
            }
            else
            {
                sdoRequestSuspended = true;
                buttonSdoRequest.Text = @"SDO Resume";
            }
        }

       

       
       

       
    }
}

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
        private Botnana bot;
        private int driveDeviceSlave = 0;
        private int driveDeviceChannel = 0;
        private int cylinderDeviceSlave = 0;
        private int cylinderDeviceChannel = 0;

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
            // 若重複執行從 32 開始，因為需要 .devices-info 取得周邊裝置的資訊
            bot.EvaluateScript("user-para@ 32 min user-para! .user-para");
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
                    bot.LoadSFC(@"../../feeder.fs");
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
                    //new Thread(() => System.Windows.Forms.MessageBox.Show("OnUserParameterCallback 32")).Start();
                    bot.EvaluateScript(@".devices-info 64 user-para! .user-para");
                    break;
                case 64:
                    //new Thread(() => System.Windows.Forms.MessageBox.Show("OnUserParameterCallback 64")).Start();
                    string cmd = null;
                    for (int i = 1; i <= slavesLen; i++)
                    {
                        cmd += (i.ToString() + @" .slave ");
                    }
                    cmd += @".feeder-para ";
                    bot.EvaluateScript(cmd);
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
        
        private HandleTagNameMessage onEncoderPosition;
        private int encoderPosition = 0;
        private void OnEncoderPosition(IntPtr dataPtr, UInt32 slv, UInt32 ch, String str)
        {
            if (slv == driveDeviceSlave && ch == driveDeviceChannel)
            {
                encoderPosition = int.Parse(str);
            }
        }
        
        private HandleTagNameMessage onTargetPosition;
        private int targetPosition = 0;
        private void OnTargetPosition(IntPtr dataPtr, UInt32 slv, UInt32 ch, string str)
        {
            if (slv == driveDeviceSlave && ch == driveDeviceChannel)
            {
                targetPosition = int.Parse(str);
            }
        }

        private Boolean isDriveOn = false;
        private Boolean isDriveFault = true;
        private HandleTagNameMessage onDriveStatus;
        private void OnDriveStatus(IntPtr dataPtr, UInt32 slv, UInt32 ch, string str)
        {
            if (slv == driveDeviceSlave && ch == driveDeviceChannel)
            {
                int code = Convert.ToInt32(str, 16);
                isDriveFault = (code & 0x48) == 8;
                isDriveOn = (code & 0x6F) == 0x27;
            }
        }

        private int operationMode = 0;
        private HandleTagNameMessage onOperationMode;
        private void OnOperationMode(IntPtr dataPtr, UInt32 slv, UInt32 ch, string str)
        {
            if (slv == driveDeviceSlave && ch == driveDeviceChannel)
            {
                operationMode = int.Parse(str);
            }
        }

        private Boolean driveExt1 = false;
        private Boolean driveExt2 = false;
        private Boolean driveDin0 = false;
        private Boolean driveDin1 = false;
        private Boolean driveDin2 = false;
        private Boolean driveDin3 = false;
        private HandleTagNameMessage onDriveDigitalInputs;
        private void OnDriveDigitalInputs(IntPtr dataPtr, UInt32 slv, UInt32 ch, string str)
        {
            if (slv == driveDeviceSlave && ch == driveDeviceChannel)
            {
                int code = Convert.ToInt32(str, 16);
                driveExt1 = (code & 0x10000) != 0;
                driveExt2 = (code & 0x20000) != 0;
                driveDin0 = (code & 0x1000000) != 0;
                driveDin1 = (code & 0x2000000) != 0;
                driveDin2 = (code & 0x4000000) != 0;
                driveDin3 = (code & 0x8000000) != 0;
            }
        }

        private HandleTagNameMessage onCylinder;
        private Boolean cylinderOn = false;
        private void OnCylinder(IntPtr dataPtr, UInt32 pos, UInt32 ch, string str)
        {
            if (pos == cylinderDeviceSlave && ch == cylinderDeviceChannel)
            {
                cylinderOn = int.Parse(str) != 0;
            }
        }

        
        private HandleTagNameMessage onTouchProbe;
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
        private void OnTouchProbe(IntPtr dataPtr, UInt32 slv, UInt32 ch, string str)
        {
            if (slv == driveDeviceSlave && ch == driveDeviceChannel)
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
        }

        private HandleTagNameMessage onTPLatchPosition1;
        private int tpLatchPosition1 = 123;
        private void OnTPLatchPosition1(IntPtr dataPtr, UInt32 slv, UInt32 ch, string str)
        {
            if (slv == driveDeviceSlave && ch == driveDeviceChannel)
            {
                tpLatchPosition1 = int.Parse(str);
            }
        }

        private HandleTagNameMessage onTPLatchPosition2;
        private int tpLatchPosition2 = 123;
        private void OnTPLatchPosition2(IntPtr dataPtr, UInt32 slv, UInt32 ch, string str)
        {
            if (slv == driveDeviceSlave && ch == driveDeviceChannel)
            {
                tpLatchPosition2 = int.Parse(str);
            }
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

        private HandleMessage onSettlingDuration;
        private int settlingDuration = 0;
        private Boolean hasSettlingDuration = false;
        private void OnSettlingDuration(IntPtr dataPtr, string str)
        {
            settlingDuration = int.Parse(str);
            hasSettlingDuration = true;
        }

        private HandleMessage onRetryCountMax;
        private int retryCountMax = 0;
        private Boolean hasRetryCountMax = false;
        private void OnRetryCountMax(IntPtr dataPtr, string str)
        {
            retryCountMax = int.Parse(str);
            hasRetryCountMax = true;
        }

        private HandleMessage onDriveDeviceSlave;
        private void OnDriveDeviceSlave(IntPtr dataPtr, string str)
        {
            driveDeviceSlave = int.Parse(str);
        }

        private HandleMessage onDriveDeviceChannel;
        private void OnDriveDeviceChannel(IntPtr dataPtr, string str)
        {
            driveDeviceChannel= int.Parse(str);
        }

        private HandleMessage onCylinderDeviceSlave;
        private void OnCylinderDeviceSlave(IntPtr dataPtr, string str)
        {
            cylinderDeviceSlave = int.Parse(str);
        }

        private HandleMessage onCylinderDeviceChannel;
        private void OnCylinderDeviceChannel(IntPtr dataPtr, string str)
        {
            cylinderDeviceChannel = int.Parse(str);
        }

        private HandleTagNameMessage onTPStatus;
        private Boolean tp1Enabled = false;
        private Boolean tp2Enabled = false;
        private void OnTPStatus(IntPtr dataPtr, UInt32 slv, UInt32 ch, string str)
        {
            if (slv == driveDeviceSlave)
            {
                int code;
                if (Int32.TryParse(str, out code))
                {
                    tp1Enabled = (code & 0x1) != 0;
                    tp2Enabled = (code & 0x100) != 0;
                }
            }
        }

        private HandleMessage onTPDetected1;
        private Boolean tpDetected1 = false;
        private void OnTPDetected1(IntPtr dataPtr, string str)
        {
            tpDetected1 = (int.Parse(str) != 0);
        }

        private HandleMessage onTPDetected2;
        private Boolean tpDetected2 = false;
        private void OnTPDetected2(IntPtr dataPtr, string str)
        {
            tpDetected2 = (int.Parse(str) != 0);
        }

        private HandleMessage onTPDetectedPosition1;
        private int tpDetectedPosition1 = 0;
        private void OnTPDetectedPosition1(IntPtr dataPtr, string str)
        {
            tpDetectedPosition1 = int.Parse(str);
        }

        private HandleMessage onTPDetectedPosition2;
        private int tpDetectedPosition2 = 0;
        private void OnTPDetectedPosition2(IntPtr dataPtr, string str)
        {
            tpDetectedPosition2 = int.Parse(str);
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
            
            onEncoderPosition = new HandleTagNameMessage(OnEncoderPosition);
            bot.SetTagNameCB(@"real_position", 0, IntPtr.Zero, onEncoderPosition);

            onTargetPosition = new HandleTagNameMessage(OnTargetPosition);
            bot.SetTagNameCB(@"target_position", 0, IntPtr.Zero, onTargetPosition);

            onDriveStatus = new HandleTagNameMessage(OnDriveStatus);
            bot.SetTagNameCB(@"status_word", 0, IntPtr.Zero, onDriveStatus);

            onOperationMode = new HandleTagNameMessage(OnOperationMode);
            bot.SetTagNameCB(@"operation_mode", 0, IntPtr.Zero, onOperationMode);

            onDriveDigitalInputs = new HandleTagNameMessage(OnDriveDigitalInputs);
            bot.SetTagNameCB(@"digital_inputs", 0, IntPtr.Zero, onDriveDigitalInputs);

            onTouchProbe = new HandleTagNameMessage(OnTouchProbe);
            bot.SetTagNameCB(@"touch_probe_function", 0, IntPtr.Zero, onTouchProbe);

            onCylinder = new HandleTagNameMessage(OnCylinder);
            bot.SetTagNameCB(@"dout", 0, IntPtr.Zero, onCylinder);

            onTPLatchPosition1 = new HandleTagNameMessage(OnTPLatchPosition1);
            bot.SetTagNameCB(@"tp_position_value_1", 0, IntPtr.Zero, onTPLatchPosition1);
            
            onTPLatchPosition2 = new HandleTagNameMessage(OnTPLatchPosition2);
            bot.SetTagNameCB(@"tp_position_value_2", 0, IntPtr.Zero, onTPLatchPosition2);

            onCylinderOnMs = new HandleMessage(OnCylinderOnMs);
            bot.SetTagCB(@"cylinder_on_duration", 0, IntPtr.Zero, onCylinderOnMs);

            onCylinderOffMs = new HandleMessage(OnCylinderOffMs);
            bot.SetTagCB(@"cylinder_off_duration", 0, IntPtr.Zero, onCylinderOffMs);

            onRotationDistance = new HandleMessage(OnRotationDistance);
            bot.SetTagCB(@"feeder_rotation_distance", 0, IntPtr.Zero, onRotationDistance);

            onRotationSpeed = new HandleMessage(OnRotationSpeed);
            bot.SetTagCB(@"feeder_rotation_speed", 0, IntPtr.Zero, onRotationSpeed);

            onSettlingDuration = new HandleMessage(OnSettlingDuration);
            bot.SetTagCB(@"feeder_settling_duration", 0, IntPtr.Zero, onSettlingDuration);

            onRetryCountMax = new HandleMessage(OnRetryCountMax);
            bot.SetTagCB(@"feeder_retry_count_max", 0, IntPtr.Zero, onRetryCountMax);

            onDriveDeviceSlave = new HandleMessage(OnDriveDeviceSlave);
            bot.SetTagCB(@"drive_device_slave", 1, IntPtr.Zero, onDriveDeviceSlave);

            onDriveDeviceChannel = new HandleMessage(OnDriveDeviceChannel);
            bot.SetTagCB(@"drive_device_channel", 1, IntPtr.Zero, onDriveDeviceChannel);

            onCylinderDeviceSlave = new HandleMessage(OnCylinderDeviceSlave);
            bot.SetTagCB(@"cylinder_device_slave", 1, IntPtr.Zero, onCylinderDeviceSlave);

            onCylinderDeviceChannel = new HandleMessage(OnCylinderDeviceChannel);
            bot.SetTagCB(@"cylinder_device_channel", 1, IntPtr.Zero, onCylinderDeviceChannel);

            onFeederRunning = new HandleMessage(OnFeederRunning);
            bot.SetTagCB(@"feeder_running", 0, IntPtr.Zero, onFeederRunning);

            onFeederEMS = new HandleMessage(OnFeederEMS);
            bot.SetTagCB(@"feeder_ems", 0, IntPtr.Zero, onFeederEMS);

            onFeederOperationMs = new HandleMessage(OnFeederOperationMs);
            bot.SetTagCB(@"feeder_operation_ms", 0, IntPtr.Zero, onFeederOperationMs);

            onTPStatus = new HandleTagNameMessage(OnTPStatus);
            bot.SetTagNameCB(@"sdo_0_24761", 0, IntPtr.Zero, onTPStatus);

            onTPDetected1 = new HandleMessage(OnTPDetected1);
            bot.SetTagCB(@"tp_detected_1", 0, IntPtr.Zero, onTPDetected1);

            onTPDetected2 = new HandleMessage(OnTPDetected2);
            bot.SetTagCB(@"tp_detected_2", 0, IntPtr.Zero, onTPDetected2);

            onTPDetectedPosition1 = new HandleMessage(OnTPDetectedPosition1);
            bot.SetTagCB(@"tp_detected_position_1", 0, IntPtr.Zero, onTPDetectedPosition1);

            onTPDetectedPosition2 = new HandleMessage(OnTPDetectedPosition2);
            bot.SetTagCB(@"tp_detected_position_2", 0, IntPtr.Zero, onTPDetectedPosition2);

            timerSlow.Enabled = true;
            timerPoll.Enabled = true;
            timer1s.Enabled = true;
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

        private Boolean hasSlaves = false;
        private void timerPoll_Tick(object sender, EventArgs e)
        {
            string cmd = null;

            for (int i = 1; i <= slavesLen; i++)
            {
                cmd += (i.ToString() + @" .slave-diff ");
            }

            if (hasSFC) { cmd += @" .feeder "; }

            cmd += @" .ec-links";
            bot.EvaluateScript(cmd);

            textEncoderPosition.Text = encoderPosition.ToString();
            textTargetPosition.Text = targetPosition.ToString();
            radioDriveOn.Checked = isDriveOn;
            radioDriveFault.Checked = isDriveFault;
            textOperationMode.Text = operationMode.ToString();
            textTPLatchPosition1.Text = tpLatchPosition1.ToString();
            textTPLatchPosition2.Text = tpLatchPosition2.ToString();


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
            radioTPDetected1.Checked = tpDetected1;
            radioTPDetected2.Checked = tpDetected2;
            textTPDetectedPosition1.Text = tpDetectedPosition1.ToString();
            textTPDetectedPosition2.Text = tpDetectedPosition2.ToString();

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

            if (hasSettlingDuration)
            {
                textBoxSettlingDurationMs.Text = settlingDuration.ToString();
                hasSettlingDuration = false;
            }

            if (hasRetryCountMax)
            {
                textBoxRetryCountMax.Text = retryCountMax.ToString();
                hasRetryCountMax = false;
            }

            radioFeederRunning.Checked = feederRunning;
            radioFeederEMS.Checked = feederEMS;


            textFeederOperationTime.Text = feederOperationMs.ToString();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"+coordinator 1 0axis-ferr drive-device 2@ real-p@ drive-device 2@ target-p! csp drive-device 2@ op-mode! drive-device 2@ drive-on");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"drive-device 2@ drive-off");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"drive-device 2@ reset-fault");
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
            bot.EvaluateScript(@"1 cylinder-device 2@ ec-dout!");
        }

        private void buttonCylinderOff_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"0 cylinder-device 2@ ec-dout!");
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

        private void textBoxSettlingDurationMs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bot.EvaluateScript(((TextBox)sender).Text + @" feeder-settling-duration! .feeder-para");
            }
        }

        private void textBoxSettlingDurationMs_Leave(object sender, EventArgs e)
        {
            bot.EvaluateScript(((TextBox)sender).Text + @" feeder-settling-duration! .feeder-para");
        }

        private void textBoxRetryCountMax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bot.EvaluateScript(((TextBox)sender).Text + @" feeder-retry-count-max ! .feeder-para");
            }
        }

        private void textBoxRetryCountMax_Leave(object sender, EventArgs e)
        {
            bot.EvaluateScript(((TextBox)sender).Text + @" feeder-retry-count-max ! .feeder-para");
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

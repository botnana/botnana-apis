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

namespace MultiApp
{
    public partial class FormFeeder : Form
    {
        private Boolean feederReady = false;

        private int driveDeviceSlave = 0;
        private int driveDeviceChannel = 0;
        private int cylinderDeviceSlave = 0;
        private int cylinderDeviceChannel = 0;

        private delegate void Feederdeg();

        private HandleMessage onFeederInfo;
        private HandleMessage onFeederReady;
        private HandleTagNameMessage onEncoderPosition;
        private HandleTagNameMessage onTargetPosition;
        private HandleTagNameMessage onPDSState;
        private HandleTagNameMessage onOperationMode;
        private HandleTagNameMessage onDriveDigitalInputs;
        private HandleTagNameMessage onCylinder;
        private HandleTagNameMessage onTouchProbe;
        private HandleTagNameMessage onTPLatchPosition1;
        private HandleTagNameMessage onTPLatchPosition2;
        private HandleMessage onCylinderOnMs;
        private HandleMessage onCylinderOffMs;
        private HandleMessage onFeederRunning;
        private HandleMessage onFeederEMS;
        private HandleMessage onFeederOperationMs;
        private HandleMessage onRotationDistance;
        private HandleMessage onRotationSpeed;
        private HandleMessage onSettlingDuration;
        private HandleMessage onRetryCountMax;
        private HandleTagNameMessage onTPStatus;
        private HandleMessage onTPDetected1;
        private HandleMessage onTPDetected2;
        private HandleMessage onTPDetectedPosition1;
        private HandleMessage onTPDetectedPosition2;

        public void Reset()
        {
            driveDeviceSlave = 0;
            driveDeviceChannel = 0;
            cylinderDeviceSlave = 0;
            cylinderDeviceChannel = 0;
            feederReady = false;
            BeginInvoke(new Feederdeg(() =>
            {
                buttonFeederReady.Text = "Feeder not ready";
                buttonFeederReady.BackColor = Color.FromArgb(0xFF, 0x2D, 0x2D);
            }));
        }

        public void Initialize()
        {
            // 取得裝置資訊
            FormApp.BotEvaluateScript(@".feeder-infos .feeder-para");
        }

        public void Awake()
        {
            timerPoll.Enabled = true;
            timer1s.Enabled = true;
        }

        public void Sleep()
        {
            timerPoll.Enabled = false;
            timer1s.Enabled = false;
        }

        public void EmergencyStop()
        {
            // 緊急停止不檢查連線狀態，嘗試送出命令
            FormApp.bot.EvaluateScript(@"ems-feeder");
        }

        public FormFeeder()
        {
            InitializeComponent();
            TopLevel = false;
            Visible = true;
            FormBorderStyle = FormBorderStyle.None;

            // On feeder_infos tag callback.
            onFeederInfo = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                string[] values = str.Split('.');
                driveDeviceSlave = int.Parse(values[0]);
                driveDeviceChannel = int.Parse(values[1]);
                cylinderDeviceSlave = int.Parse(values[2]);
                cylinderDeviceChannel = int.Parse(values[3]);

                string cmd = "";
                if (driveDeviceSlave != 0) cmd += driveDeviceSlave.ToString() + @" .slave ";
                if (cylinderDeviceSlave != 0) cmd += cylinderDeviceSlave.ToString() + @" .slave ";
                FormApp.BotEvaluateScript(cmd);
            });
            FormApp.bot.SetTagCB(@"feeder_infos", 0, IntPtr.Zero, onFeederInfo);

            // On feeder_ready tag callback.
            onFeederReady = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                feederReady = (int.Parse(str) != 0);
                if (feederReady)
                {
                    BeginInvoke(new Feederdeg(() =>
                    {
                        buttonFeederReady.Text = "Feeder ready";
                        buttonFeederReady.BackColor = Color.FromArgb(0x53, 0xFF, 0x53);
                    }));
                }
                else
                {
                    BeginInvoke(new Feederdeg(() =>
                    {
                        buttonFeederReady.Text = "Feeder not ready";
                        buttonFeederReady.BackColor = Color.FromArgb(0xFF, 0x2D, 0x2D);
                    }));
                }
            });
            FormApp.bot.SetTagCB(@"feeder_ready", 0, IntPtr.Zero, onFeederReady);

            // On real_position tag name callback.
            onEncoderPosition = new HandleTagNameMessage((IntPtr dataPtr, UInt32 slv, UInt32 ch, String str) =>
            {
                if (slv == driveDeviceSlave && ch == driveDeviceChannel) BeginInvoke(new Feederdeg(() => { textEncoderPosition.Text = str; }));
            });
            FormApp.bot.SetTagNameCB(@"real_position", 0, IntPtr.Zero, onEncoderPosition);

            // On target_position tag name callback.
            onTargetPosition = new HandleTagNameMessage((IntPtr dataPtr, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == driveDeviceSlave && ch == driveDeviceChannel) BeginInvoke(new Feederdeg(() => { textTargetPosition.Text = str; }));
            });
            FormApp.bot.SetTagNameCB(@"target_position", 0, IntPtr.Zero, onTargetPosition);

            // On pds_state tag name callback.
            onPDSState = new HandleTagNameMessage((IntPtr dataPtr, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == driveDeviceSlave && ch == driveDeviceChannel) BeginInvoke(new Feederdeg(() => { textPDSState.Text = str; }));
            });
            FormApp.bot.SetTagNameCB(@"pds_state", 0, IntPtr.Zero, onPDSState);

            // On operation_mode tag name callback.
            onOperationMode = new HandleTagNameMessage((IntPtr dataPtr, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == driveDeviceSlave && ch == driveDeviceChannel) BeginInvoke(new Feederdeg(() => { textOperationMode.Text = str; }));
            });
            FormApp.bot.SetTagNameCB(@"operation_mode", 0, IntPtr.Zero, onOperationMode);

            // On digital_inputs tag name callback.
            onDriveDigitalInputs = new HandleTagNameMessage((IntPtr dataPtr, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == driveDeviceSlave && ch == driveDeviceChannel)
                {
                    int code = Convert.ToInt32(str, 16);
                    bool driveExt1 = (code & 0x10000) != 0;
                    bool driveExt2 = (code & 0x20000) != 0;
                    bool driveDin0 = (code & 0x1000000) != 0;
                    bool driveDin1 = (code & 0x2000000) != 0;
                    bool driveDin2 = (code & 0x4000000) != 0;
                    bool driveDin3 = (code & 0x8000000) != 0;
                    BeginInvoke(new Feederdeg(() =>
                    {
                        radioDriveDin0.Checked = driveDin0;
                        radioDriveDin1.Checked = driveDin1;
                        radioDriveDin2.Checked = driveDin2;
                        radioDriveDin3.Checked = driveDin3;
                        radioDriveExt1.Checked = driveExt1;
                        radioDriveExt2.Checked = driveExt2;
                    }));
                }
            });
            FormApp.bot.SetTagNameCB(@"digital_inputs", 0, IntPtr.Zero, onDriveDigitalInputs);

            // On dout tag name callback.
            onCylinder = new HandleTagNameMessage((IntPtr dataPtr, UInt32 pos, UInt32 ch, string str) =>
            {
                if (pos == cylinderDeviceSlave && ch == cylinderDeviceChannel) BeginInvoke(new Feederdeg(() => { radioCylinder.Checked = int.Parse(str) != 0; }));
            });
            FormApp.bot.SetTagNameCB(@"dout", 0, IntPtr.Zero, onCylinder);

            // On touch_probe_function tag name callback.
            onTouchProbe = new HandleTagNameMessage((IntPtr dataPtr, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == driveDeviceSlave && ch == driveDeviceChannel)
                {
                    int tmp = int.Parse(str);
                    bool enableTP1 = (tmp & 1) != 0;
                    bool enableTP2 = (tmp & 0x100) != 0;
                    bool tp1TriggerAction = (tmp & 0x2) != 0;
                    bool tp2TriggerAction = (tmp & 0x200) != 0;
                    bool tp1TriggerSelection = (tmp & 0x4) != 0;
                    bool tp2TriggerSelection = (tmp & 0x400) != 0;
                    bool tp1UpEdgeAction = (tmp & 0x10) != 0;
                    bool tp2UpEdgeAction = (tmp & 0x1000) != 0;
                    bool tp1DownEdgeAction = (tmp & 0x20) != 0;
                    bool tp2DownEdgeAction = (tmp & 0x2000) != 0;
                    BeginInvoke(new Feederdeg(() =>
                    {
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
                    }));
                }
            });
            FormApp.bot.SetTagNameCB(@"touch_probe_function", 0, IntPtr.Zero, onTouchProbe);

            // On tp_position_value_1 tag name callback.
            onTPLatchPosition1 = new HandleTagNameMessage((IntPtr dataPtr, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == driveDeviceSlave && ch == driveDeviceChannel) BeginInvoke(new Feederdeg(() => { textTPLatchPosition1.Text = str; }));
            });
            FormApp.bot.SetTagNameCB(@"tp_position_value_1", 0, IntPtr.Zero, onTPLatchPosition1);

            // On tp_position_value_2 tag name callback.
            onTPLatchPosition2 = new HandleTagNameMessage((IntPtr dataPtr, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == driveDeviceSlave && ch == driveDeviceChannel) BeginInvoke(new Feederdeg(() => { textTPLatchPosition2.Text = str; }));
            });
            FormApp.bot.SetTagNameCB(@"tp_position_value_2", 0, IntPtr.Zero, onTPLatchPosition2);

            // On cylinder_on_duration tag name callback.
            onCylinderOnMs = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                BeginInvoke(new Feederdeg(() => { textCylinderOnMs.Text = str; }));
            });
            FormApp.bot.SetTagCB(@"cylinder_on_duration", 0, IntPtr.Zero, onCylinderOnMs);

            // On cylinder_off_duration tag name callback.
            onCylinderOffMs = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                BeginInvoke(new Feederdeg(() => { textCylinderOffMs.Text = str; }));
            });
            FormApp.bot.SetTagCB(@"cylinder_off_duration", 0, IntPtr.Zero, onCylinderOffMs);

            // On feeder_running tag name callback.
            onFeederRunning = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                BeginInvoke(new Feederdeg(() => { radioFeederRunning.Checked = int.Parse(str) != 0; }));
            });
            FormApp.bot.SetTagCB(@"feeder_running", 0, IntPtr.Zero, onFeederRunning);

            // On feeder_ems tag name callback.
            onFeederEMS = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                BeginInvoke(new Feederdeg(() => { radioFeederEMS.Checked = int.Parse(str) != 0; }));
            });
            FormApp.bot.SetTagCB(@"feeder_ems", 0, IntPtr.Zero, onFeederEMS);

            // On feeder_operation_ms tag name callback.
            onFeederOperationMs = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                BeginInvoke(new Feederdeg(() => { textFeederOperationTime.Text = str; }));
            });
            FormApp.bot.SetTagCB(@"feeder_operation_ms", 0, IntPtr.Zero, onFeederOperationMs);

            // On feeder_rotation_distance tag name callback.
            onRotationDistance = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                BeginInvoke(new Feederdeg(() => { textRotationDistance.Text = str; }));
            });
            FormApp.bot.SetTagCB(@"feeder_rotation_distance", 0, IntPtr.Zero, onRotationDistance);

            // On feeder_rotation_speed tag name callback.
            onRotationSpeed = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                BeginInvoke(new Feederdeg(() => { textRotationSpeed.Text = str; }));
            });
            FormApp.bot.SetTagCB(@"feeder_rotation_speed", 0, IntPtr.Zero, onRotationSpeed);

            // On feeder_settling_duration tag name callback.
            onSettlingDuration = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                BeginInvoke(new Feederdeg(() => { textBoxSettlingDurationMs.Text = str; }));
            });
            FormApp.bot.SetTagCB(@"feeder_settling_duration", 0, IntPtr.Zero, onSettlingDuration);

            // On feeder_retry_count_max tag name callback.
            onRetryCountMax = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                BeginInvoke(new Feederdeg(() => { textBoxRetryCountMax.Text = str; }));
            });
            FormApp.bot.SetTagCB(@"feeder_retry_count_max", 0, IntPtr.Zero, onRetryCountMax);

            // On sdo_0_24761 tag name callback.
            onTPStatus = new HandleTagNameMessage((IntPtr dataPtr, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == driveDeviceSlave)
                {
                    int code;
                    if (Int32.TryParse(str, out code))
                    {
                        bool tp1Enabled = (code & 0x1) != 0;
                        bool tp2Enabled = (code & 0x100) != 0;
                        BeginInvoke(new Feederdeg(() =>
                        {
                            radioTP1Enabled.Checked = tp1Enabled;
                            radioTP2Enabled.Checked = tp2Enabled;
                        }));
                    }
                }
            });
            FormApp.bot.SetTagNameCB(@"sdo_0_24761", 0, IntPtr.Zero, onTPStatus);

            // On tp_detected_1 tag name callback.
            onTPDetected1 = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                BeginInvoke(new Feederdeg(() => { radioTPDetected1.Checked = int.Parse(str) != 0; }));
            });
            FormApp.bot.SetTagCB(@"tp_detected_1", 0, IntPtr.Zero, onTPDetected1);

            // On tp_detected_2 tag name callback.
            onTPDetected2 = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                BeginInvoke(new Feederdeg(() => { radioTPDetected2.Checked = int.Parse(str) != 0; }));
            });
            FormApp.bot.SetTagCB(@"tp_detected_2", 0, IntPtr.Zero, onTPDetected2);

            // On tp_detected_position_1 tag name callback.
            onTPDetectedPosition1 = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                BeginInvoke(new Feederdeg(() => { textTPDetectedPosition1.Text = str; }));
            });
            FormApp.bot.SetTagCB(@"tp_detected_position_1", 0, IntPtr.Zero, onTPDetectedPosition1);

            // On tp_detected_position_2 tag name callback.
            onTPDetectedPosition2 = new HandleMessage((IntPtr dataPtr, string str) =>
            {
                BeginInvoke(new Feederdeg(() => { textTPDetectedPosition2.Text = str; }));
            });
            FormApp.bot.SetTagCB(@"tp_detected_position_2", 0, IntPtr.Zero, onTPDetectedPosition2);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timerPoll.Interval = 50;
            timer1s.Interval = 1000;
        }
        
        private void timerPoll_Tick(object sender, EventArgs e)
        {
            string cmd = "";
            if (driveDeviceSlave != 0 && driveDeviceChannel != 0) cmd += (driveDeviceSlave.ToString() + @" .slave-diff ");
            if (cylinderDeviceSlave != 0 && cylinderDeviceChannel != 0) cmd += (cylinderDeviceSlave.ToString() + @" .slave-diff ");
            if (feederReady) cmd += @".feeder ";
            if (!FormApp.stopPolling) FormApp.BotEvaluateScript(cmd);
        }

        private Boolean sdoRequestSuspended = false;
        private void timer1s_Tick(object sender, EventArgs e)
        {
            if (feederReady && !sdoRequestSuspended && !FormApp.stopPolling) FormApp.BotEvaluateScript(@"sdo-upload-tp-status");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormApp.BotEvaluateScript(@"rotate-axis @ 0axis-ferr drive-device 2@ real-p@ drive-device 2@ target-p! csp drive-device 2@ op-mode! drive-device 2@ drive-on");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormApp.BotEvaluateScript(@"drive-device 2@ drive-off");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormApp.BotEvaluateScript(@"drive-device 2@ reset-fault");
        }

        private void buttonJogPositive_MouseDown(object sender, MouseEventArgs e)
        {
            FormApp.BotEvaluateScript(textJogSpeed.Text + @"e feeder-jog+");
        }

        private void buttonJogPositive_MouseUp(object sender, MouseEventArgs e)
        {
            FormApp.BotEvaluateScript(@"feeder-jog-stop");
        }

        private void buttonJogNegative_MouseUp(object sender, MouseEventArgs e)
        {
            FormApp.BotEvaluateScript(@"feeder-jog-stop");
        }

        private void buttonJogNegative_MouseDown(object sender, MouseEventArgs e)
        {
            FormApp.BotEvaluateScript(textJogSpeed.Text + @"e feeder-jog-");
        }

        private void buttonCylinderOn_Click(object sender, EventArgs e)
        {
            FormApp.BotEvaluateScript(@"1 cylinder-device 2@ ec-dout!");
        }

        private void buttonCylinderOff_Click(object sender, EventArgs e)
        {
            FormApp.BotEvaluateScript(@"0 cylinder-device 2@ ec-dout!");
        }

        private void textCylinderOnMs_Leave(object sender, EventArgs e)
        {
            FormApp.BotEvaluateScript(textCylinderOnMs.Text + @" cylinder-on-duration! .feeder-para");
        }

        private void textCylinderOnMs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FormApp.BotEvaluateScript(textCylinderOnMs.Text + @" cylinder-on-duration! .feeder-para");
            }
        }

        private void textCylinderOffMs_Leave(object sender, EventArgs e)
        {
            FormApp.BotEvaluateScript(textCylinderOffMs.Text + @" cylinder-off-duration! .feeder-para");
        }

        private void textCylinderOffMs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FormApp.BotEvaluateScript(textCylinderOffMs.Text + @" cylinder-off-duration! .feeder-para");
            }
        }

        private void buttonStartFeeder_Click(object sender, EventArgs e)
        {
            textFeederOperationTime.Text = "0";
            FormApp.BotEvaluateScript(@"start-feeder");
        }

        private void textRotationDistance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FormApp.BotEvaluateScript(textRotationDistance.Text + @" feeder-rotation-distance! .feeder-para");
            }
        }

        private void textRotationDistance_Leave(object sender, EventArgs e)
        {
            FormApp.BotEvaluateScript(textRotationDistance.Text + @" feeder-rotation-distance! .feeder-para");
        }

        private void textRotationSpeed_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FormApp.BotEvaluateScript(textRotationSpeed.Text + @" feeder-rotation-speed! .feeder-para");
            }
        }

        private void textRotationSpeed_Leave(object sender, EventArgs e)
        {
            FormApp.BotEvaluateScript(textRotationSpeed.Text + @" feeder-rotation-speed! .feeder-para");
        }

        private void textBoxSettlingDurationMs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FormApp.BotEvaluateScript(((TextBox)sender).Text + @" feeder-settling-duration! .feeder-para");
            }
        }

        private void textBoxSettlingDurationMs_Leave(object sender, EventArgs e)
        {
            FormApp.BotEvaluateScript(((TextBox)sender).Text + @" feeder-settling-duration! .feeder-para");
        }

        private void textBoxRetryCountMax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FormApp.BotEvaluateScript(((TextBox)sender).Text + @" feeder-retry-count-max ! .feeder-para");
            }
        }

        private void textBoxRetryCountMax_Leave(object sender, EventArgs e)
        {
            FormApp.BotEvaluateScript(((TextBox)sender).Text + @" feeder-retry-count-max ! .feeder-para");
        }

        private void buttonFeederEMS_Click(object sender, EventArgs e)
        {
            // 緊急停止不檢查連線狀態，嘗試送出命令。
            FormApp.bot.EvaluateScript(@"ems-feeder");
        }

        private void buttonReleaseFeeder_Click(object sender, EventArgs e)
        {
            FormApp.BotEvaluateScript(@"release-feeder");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FormApp.BotEvaluateScript(@"send-param-request");
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

        private void buttonJog_Click(object sender, EventArgs e)
        {
            FormApp.BotEvaluateScript(textJogTarget.Text + @"e " + textJogSpeed.Text + @"e feeder-jog");
        }

        private void buttonJogStop_Click(object sender, EventArgs e)
        {
            FormApp.BotEvaluateScript(@"feeder-jog-stop");
        }
    }
}

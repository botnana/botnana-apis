using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BotnanaLib;
using System.Threading;

namespace ManualPulseGenerator
{
    public partial class Form1 : Form
    {
        private Botnana botnana;

        private int wsState = 0;
        private HandleMessage onWsErrorCallback;
        private void OnWsErrorCB(IntPtr dataPtr, string str)
        {
            wsState = 0;
        }

        private HandleMessage onWsOpenCallback;
        private void OnWsOpenCB(IntPtr dataPtr, string str)
        {
            wsState = 2;
           botnana.EvaluateScript(".user-para");
        }

        private int isEcReady = 0;
        private HandleMessage onEcReady;
        private void OnEcReadyCB(IntPtr dataPtr, string str)
        {
            isEcReady = int.Parse(str);
        }

        private int slavesLen = 0;
        private Boolean firstSlavesLen = true;
        private HandleMessage onSlavesLen;
        private void OnSlavesLenCB(IntPtr dataPtr, string str)
        {
            slavesLen = int.Parse(str);
            if  (firstSlavesLen)
            {
                botnana.SendScript("1 .axiscfg");

                for (int i = 1; i<= slavesLen; i++)
                {
                    botnana.SendScript(i.ToString() + " .slave");
                }
                firstSlavesLen = false;
            }
        }

        private int errorMessageLen = 0;
        private HandleMessage onErrorMessage;
        private void OnErrorMessageCB(IntPtr dataPtr, string str)
        {
            errorMessageLen += 1;
            //new Thread(() => System.Windows.Forms.MessageBox.Show("Error|" + str)).Start();
        }

        private int mpgEncoder = 0;
        private HandleMessage onMPGEncoder;
        private void OnMPGEncoderCB(IntPtr dataPtr, string str)
        {
            mpgEncoder = int.Parse(str);
        }

        private Boolean mpgOn = false;
        private int mpgRate = 1;
        private HandleMessage onMPGDis;
        private void OnMPGDisCB(IntPtr ptr, string str)
        {
            int dis = int.Parse(str);
            mpgOn = (dis & 0x1) > 0;

            if ((dis & 0x4) > 0)
            {
                mpgRate = 10;
            }
            else if ((dis & 0x8) > 0)
            {
                mpgRate = 100;
            }
            else
            {
                mpgRate = 1;
            }
        }

        private int driveRealPosition = 0;
        private HandleMessage onDriveRealPosition;
        private void OnDriveRealPositionCB(IntPtr dataPtr, string str)
        {
            driveRealPosition = int.Parse(str);
        }

        private int driveTargetPosition = 0;
        private HandleMessage onDriveTargetPosition;
        private void OnDriveTargetPositionCB(IntPtr dataPtr, string str)
        {
            driveTargetPosition = int.Parse(str);
        }
        
        private Boolean driveFault = true;
        private Boolean driveOn = false;
        private HandleMessage onDriveStatus;
        private void OnDriveStatusCB(IntPtr dataPtr, string str)
        {
            int code = Convert.ToInt32(str, 16);
            driveFault = (code & 0x48) == 8;
            driveOn = (code & 0x6F) == 0x27;
        }

        private int driveMode = 0;
        private HandleMessage onDriveMode;
        private void OnDriveModeCB(IntPtr dataPtr, string str)
        {
            driveMode = int.Parse(str);
        }

        private Boolean coordinatorEnabled = false;
        private HandleMessage onCoordinatorEnabled;
        private void OnCoordinatorEnabledCB(IntPtr dataPtr, string str)
        {
            coordinatorEnabled = int.Parse(str) == 1;
        }

        private Boolean axisEnabled = false;
        private HandleMessage onAxisEnabled;
        private void OnAxisEnabledCB(IntPtr dataPtr, string str)
        {
            axisEnabled = int.Parse(str) == 1;
        }

        private Boolean axisReached = false;
        private HandleMessage onAxisReached;
        private void OnAxisReachedCB(IntPtr dataPtr, string str)
        {
            axisReached = int.Parse(str) == 1;
        }

        private Double axisCommand = 0.0;
        private HandleMessage onAxisCommand;
        private void OnAxisCommandCB(IntPtr dataPtr, string str)
        {
            axisCommand = Double.Parse(str)*1000;
        }

        private Double axisDemand = 0.0;
        private HandleMessage onAxisDemand;
        private void OnAxisDemandCB(IntPtr dataPtr, string str)
        {
            axisDemand = Double.Parse(str)*1000;
        }

        private Double axisFeedback = 0.0;
        private HandleMessage onAxisFeedback;
        private void OnAxisFeedbackCB(IntPtr dataPtr, string str)
        {
            axisFeedback = Double.Parse(str)*1000;
        }

        private Double axisFollowingError = 0.0;
        private HandleMessage onAxisFollowingError;
        private void OnAxisFollowingErrorCB(IntPtr dataPtr, string str)
        {
            axisFollowingError = Double.Parse(str)*1000;
        }

        private int mpgSelected = 0;
        private HandleMessage onMpgSelected;
        private void OnMpgSelectedCB(IntPtr dataPtr, string str)
        {
            mpgSelected = int.Parse(str);
        }

        private Double mpgSfcRate = 0;
        private HandleMessage onMpgSfcRate;
        private void OnMpgSfcRateCB(IntPtr dataPtr, string str)
        {
            mpgSfcRate = Double.Parse(str)*1000000;
        }

        private Boolean sfcCondition = false;
        private HandleMessage onSfcCondition;
        private void OnSfcConditionCB(IntPtr dataPtr, string str)
        {
            sfcCondition = int.Parse(str) != 0;
        }


        // 取得 userParameter
        private HandleMessage onUserParameter;
        private Boolean hasSFC = false;
        private void OnUserParameterCB(IntPtr dataPtr, string str)
        {
            int para = Int32.Parse(str);
            switch (para)
            {
                case 0:
                    // 設定 user parameter 為 0x10
                    // 如果此範例重新執行不會再載入以下 SFC
                    //botnana.EvaluateScript("$10 user-para!");
                    // 清除SFC 邏輯，載入 SFC 時會造成 real time cycle overrun，所以要暫時 ignore-overrun
                    // 載入後再執行 `reset-overrun`
                    botnana.EvaluateScript("0sfc ignore-overrun -work marker -work ");
                    botnana.LoadSFC(@"mpg.sfc");
                    botnana.LoadSFC(@"manager.sfc");
                    // 等待 SFC 設置完成
                    botnana.SendScript("reset-overrun");
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
            botnana = new Botnana("192.168.7.2");

            // 設定 callback function
            onWsErrorCallback = new HandleMessage(OnWsErrorCB);
            botnana.SetOnErrorCB(IntPtr.Zero, onWsErrorCallback);

            onWsOpenCallback = new HandleMessage(OnWsOpenCB);
            botnana.SetOnOpenCB(IntPtr.Zero, onWsOpenCallback);

            onErrorMessage = new HandleMessage(OnErrorMessageCB);
            botnana.SetTagCB("error", 0, IntPtr.Zero, onErrorMessage);

            onEcReady = new HandleMessage(OnEcReadyCB);
            botnana.SetTagCB("ec_ready", 0, IntPtr.Zero, onEcReady);

            onSlavesLen = new HandleMessage(OnSlavesLenCB);
            botnana.SetTagCB("slaves_responding", 0, IntPtr.Zero, onSlavesLen);

            onMPGEncoder = new HandleMessage(OnMPGEncoderCB);
            botnana.SetTagCB("real_position.1.3", 0, IntPtr.Zero, onMPGEncoder);

            onMPGDis = new HandleMessage(OnMPGDisCB);
            botnana.SetTagCB("din_wd.1.4", 0, IntPtr.Zero, onMPGDis);

            onDriveRealPosition = new HandleMessage(OnDriveRealPositionCB);
            botnana.SetTagCB("real_position.1.1", 0, IntPtr.Zero, onDriveRealPosition);

            onDriveTargetPosition = new HandleMessage(OnDriveTargetPositionCB);
            botnana.SetTagCB("target_position.1.1", 0, IntPtr.Zero, onDriveTargetPosition);

            onDriveStatus = new HandleMessage(OnDriveStatusCB);
            botnana.SetTagCB("status_word.1.1", 0, IntPtr.Zero, onDriveStatus);

            onDriveMode = new HandleMessage(OnDriveModeCB);
            botnana.SetTagCB("operation_mode.1.1", 0, IntPtr.Zero, onDriveMode);

            onCoordinatorEnabled = new HandleMessage(OnCoordinatorEnabledCB);
            botnana.SetTagCB("coordinator_enabled", 0, IntPtr.Zero, onCoordinatorEnabled);

            onAxisEnabled = new HandleMessage(OnAxisEnabledCB);
            botnana.SetTagCB("axis_interpolator_enabled.1", 0, IntPtr.Zero, onAxisEnabled);

            onAxisReached = new HandleMessage(OnAxisReachedCB);
            botnana.SetTagCB("axis_interpolator_reached.1", 0, IntPtr.Zero, onAxisReached);

            onAxisFeedback = new HandleMessage(OnAxisFeedbackCB);
            botnana.SetTagCB("feedback_position.1", 0, IntPtr.Zero, onAxisFeedback);

            onAxisDemand = new HandleMessage(OnAxisDemandCB);
            botnana.SetTagCB("axis_demand_position.1", 0, IntPtr.Zero, onAxisDemand);

            onAxisCommand = new HandleMessage(OnAxisCommandCB);
            botnana.SetTagCB("axis_command_position.1", 0, IntPtr.Zero, onAxisCommand);

            onAxisFollowingError = new HandleMessage(OnAxisFollowingErrorCB);
            botnana.SetTagCB("following_error.1", 0, IntPtr.Zero, onAxisFollowingError);

            onUserParameter = new HandleMessage(OnUserParameterCB);
            botnana.SetTagCB("user_parameter", 0, IntPtr.Zero, onUserParameter);

            onMpgSelected = new HandleMessage(OnMpgSelectedCB);
            botnana.SetTagCB("mpg_selected", 0, IntPtr.Zero, onMpgSelected);

            onMpgSfcRate = new HandleMessage(OnMpgSfcRateCB);
            botnana.SetTagCB("mpg_rate", 0, IntPtr.Zero, onMpgSfcRate);

            onSfcCondition = new HandleMessage(OnSfcConditionCB);
            botnana.SetTagCB("condition_ok", 0, IntPtr.Zero, onSfcCondition);

            timer1.Interval = 50;
            timer1.Enabled = true;
            timerSlow.Interval = 1000;
            timerSlow.Enabled = true;
        }

        private void timerSlow_Tick(object sender, EventArgs e)
        {
            textErrorCount.Text = errorMessageLen.ToString();
            // 設定 WebSocket 連線狀態的顏色 
            if (wsState == 2)
            {
                buttonWebSocket.BackColor = Color.SpringGreen;
            }
            else if (wsState == 1)
            {
                buttonWebSocket.BackColor = Color.Gold;
            }
            else
            {
                // 嘗試重新連線
                buttonWebSocket.BackColor = Color.IndianRed;
                botnana.Connect();
                wsState = 1;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 要求狀態回傳
            botnana.SendScript(".ec-links 1 .axis");
            botnana.SendScript(".coordinator");

            for (int i = 1; i <= slavesLen; i++)
            {
                botnana.SendScript(i.ToString() + " .slave-diff");
            }

            if (hasSFC)
            {
                botnana.SendScript(".sfc-manager");
                botnana.SendScript(".mpg");
            }

            // 依據狀態更新畫面
            textSlavesLen.Text = slavesLen.ToString();
            if (isEcReady > 0)
            {
                groupEtherCAT.ForeColor = Color.Black;
            }
            else
            {
                groupEtherCAT.ForeColor = Color.IndianRed;
            }

            radioDriveFault.Checked = driveFault;
            radioDriveOn.Checked = driveOn;
            textDriveMode.Text = driveMode.ToString();
            textDriveRealPosition.Text = driveRealPosition.ToString();
            textDriveTargetPosition.Text = driveTargetPosition.ToString();

            textMPGEncoder.Text = mpgEncoder.ToString();
            radioMPGOn.Checked = mpgOn;
            textMPGRate.Text = "X"+mpgRate.ToString();

            radioCoordinator.Checked = coordinatorEnabled;

            radioAxisInterpolator.Checked = axisEnabled;
            radioAxisReached.Checked = axisReached;
            textAxisFeedback.Text = axisFeedback.ToString("F3");
            textAxisDemand.Text = axisDemand.ToString("F3");
            textAxisCommand.Text = axisCommand.ToString("F3");
            textAxisFollowingError.Text = axisFollowingError.ToString("F3");

            radioSfcConfition.Checked = sfcCondition;
            textMpgSelected.Text = mpgSelected.ToString();
            textMpgSfcRate.Text = mpgSfcRate.ToString("F2");

        }

        private void buttonServoOn_Click(object sender, EventArgs e)
        {
            // 啟動同動功能
            // 清除落後誤差
            // 將驅動器切換到 CSP 模式
            // 將驅動器 drive on
            botnana.SendScript("+coordinator 1 0axis-ferr csp 1 1 op-mode! 1 1 drive-on");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // 關閉同動功能
            // 清除落後誤差
            // 將驅動器切換到 CSP 模式
            // 將驅動器 drive off
            botnana.SendScript("-coordinator 1 1 drive-off");
        }

        private void buttonDriveResetFault_Click(object sender, EventArgs e)
        {
            // 清除驅動器異警
            botnana.SendScript("1 1 reset-fault");
            errorMessageLen = 0;
        }

        private void buttonDriveStop_Click(object sender, EventArgs e)
        {
            // 同動功能緊急停止
            // 關閉同動功能
            // 命令驅動器停止
            botnana.SendScript("ems-job -coordinator 1 1 drive-stop");
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using BotnanaLib;

namespace Group2DAndMpg
{
    public partial class Form1 : Form
    {

        private Botnana bot;

        private Thread pathThread;
        private ManualResetEvent pauseEvent;
        private Thread velocityThread;

        private const int QueueCapacity = 2000;
        private Queue<float> velocityQueue = new Queue<float>(QueueCapacity);
        private float[] velocityArray = new float[QueueCapacity];

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
                    bot.EvaluateScript(@"0sfc ignore-overrun");
                    bot.LoadSFC(@"../../group2d.sfc");
                    bot.LoadSFC(@"../../mpg.sfc");
                    bot.LoadSFC(@"../../manager.sfc");
                    bot.EvaluateScript(@"marker -axes .user-para");
                    break;
                case 16:
                    bot.EvaluateScript(@"reset-overrun 32 user-para! .user-para");
                    break;
                case 32:
                    hasSFC = true;
                    break;
                default:
                    break;
            }
        }


        // 是否有啟動同動功能
        private Boolean coordinatorEnabled = false;
        private HandleMessage onCoordinatorEnabled;
        private void OnCoordinatorEnabled(IntPtr dataPtr, string str)
        {
            coordinatorEnabled = (int.Parse(str) != 0);
        }

        // 收到 Error 的 tag 時的處置
        private HandleMessage onErrorMessage;
        private bool hasAcked = true;
        private void OnErrorMessageCallback(IntPtr dataPtr, string str)
        {
            if (hasAcked)
            {
                new Thread(() => System.Windows.Forms.MessageBox.Show("Error|" + str)).Start();
                hasAcked = false;
            }
        }

        private HandleMessage onGroupNextV;
        private void OnGroupNextV(IntPtr dataPtr, string str)
        {
            float v = float.Parse(str)*1000*60;
            if (velocityQueue.Count >= QueueCapacity)
            {
                velocityQueue.Dequeue();
                velocityQueue.Enqueue(v);
            }
            else
            {
                velocityQueue.Enqueue(v);
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


        private Boolean[] isServoDrives = new Boolean[5];
        private HandleTagNameMessage onDeviceType;
        private void OnDeviceType(IntPtr dataPtr, UInt32 position, UInt32 channel, string str)
        {
            if (position <= 4)
            {
                isServoDrives[position] = ( (Convert.ToInt32(str, 16) & 0x20192) == 0x20192);
            }
        }

        private Boolean[] isDriveOns = new Boolean[5];
        private Boolean[] isDriveFaults = new Boolean[5];
        private HandleTagNameMessage onDriveStatus;
        private void OnDriveStatus(IntPtr dataPtr, UInt32 position, UInt32 channel, string str)
        {
            if (position <= 4) 
            {
                int code = Convert.ToInt32(str, 16);
                isDriveFaults[position] = (code & 0x48) == 8;
                isDriveOns[position] = (code & 0x6F) == 0x27;
            }
        }


        private float[] axesCmd = new float[5];
        private float[] axesFeedback = new float[5];
        private float[] axesFollowingErr = new float[5];

        private HandleTagNameMessage onAxesCmd;
        private void OnAxesCmd(IntPtr dataPtr, UInt32 position, UInt32 channel, string str)
        {
            if (position <= 4)
            {
                axesCmd[position] = float.Parse(str)*1000.0f;
            }
        }

        private HandleTagNameMessage onAxesFeedback;
        private void OnAxesFeedback(IntPtr dataPtr, UInt32 position, UInt32 channel, string str)
        {
            if (position <= 4)
            {
                axesFeedback[position] = float.Parse(str) * 1000.0f;
            }
        }

        private HandleTagNameMessage onAxesFollowingErr;
        private void OnAxesFollowingErr(IntPtr dataPtr, UInt32 position, UInt32 channel, string str)
        {
            if (position <= 4)
            {
                axesFollowingErr[position] = float.Parse(str) * 1000.0f;
            }
        }


        private int mpgEncoder = 0;
        private HandleMessage onMPGEncoder;
        private void OnMPGEncoder(IntPtr dataPtr, string str)
        {
            mpgEncoder = int.Parse(str);
        }

        private int mpgAxis = 0;
        private int mpgRate = 1;
        private HandleMessage onMPGDis;
        private void OnMPGDisCB(IntPtr ptr, string str)
        {
            int dis = int.Parse(str);
            if ((dis & 0x1) > 0)
            {
                mpgAxis = 3;
            }
            else if ((dis & 0x2) > 0)
            {
                mpgAxis = 4;
            }
            else
            {
                mpgAxis = 0;
            }

            if ((dis & 0x4) > 0)
            {
                mpgRate = 1;
            }
            else if ((dis & 0x8) > 0)
            {
                mpgRate = 10;
            }
            else if ((dis & 0x10) > 0)
            {
                mpgRate = 100;
            }
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

            onCoordinatorEnabled = new HandleMessage(OnCoordinatorEnabled);
            bot.SetTagCB(@"coordinator_enabled", 0, IntPtr.Zero, onCoordinatorEnabled);

            // 收到 tag = error 就呼叫 OnErrorMessageCallback
            onErrorMessage = new HandleMessage(OnErrorMessageCallback);
            bot.SetTagCB(@"error", 0, IntPtr.Zero, onErrorMessage);

            // 收到 tag = error 就呼叫 OnErrorMessageCallback
            onGroupNextV = new HandleMessage(OnGroupNextV);
            bot.SetTagCB(@"group_next_v", 0, IntPtr.Zero, onGroupNextV);

            onSlaves = new HandleMessage(OnSlaves);
            bot.SetTagCB(@"slaves_responding", 0, IntPtr.Zero, onSlaves);

            onEcReady = new HandleMessage(OnEcReady);
            bot.SetTagCB(@"ec_ready", 0, IntPtr.Zero, onEcReady);
            
            onDeviceType = new HandleTagNameMessage(OnDeviceType);
            bot.SetTagNameCB(@"device_type", 0, IntPtr.Zero, onDeviceType);

            onDriveStatus = new HandleTagNameMessage(OnDriveStatus);
            bot.SetTagNameCB(@"status_word", 0, IntPtr.Zero, onDriveStatus);

            onAxesCmd = new HandleTagNameMessage(OnAxesCmd);
            bot.SetTagNameCB(@"axis_command_position", 0, IntPtr.Zero, onAxesCmd);

            onAxesFeedback = new HandleTagNameMessage(OnAxesFeedback);
            bot.SetTagNameCB(@"feedback_position", 0, IntPtr.Zero, onAxesFeedback);

            onAxesFollowingErr = new HandleTagNameMessage(OnAxesFollowingErr);
            bot.SetTagNameCB(@"following_error", 0, IntPtr.Zero, onAxesFollowingErr);

            onMPGEncoder = new HandleMessage(OnMPGEncoder);
            bot.SetTagCB("real_position.1.6", 0, IntPtr.Zero, onMPGEncoder);

            onMPGDis = new HandleMessage(OnMPGDisCB);
            bot.SetTagCB("din_wd.1.7", 0, IntPtr.Zero, onMPGDis);

            
            chartPath.ChartAreas[0].AxisX.Minimum = -10.0;
            chartPath.ChartAreas[0].AxisX.Maximum = 160.0;
            chartVelocity.ChartAreas[0].AxisX.Minimum = 0;
            chartVelocity.ChartAreas[0].AxisX.Maximum = 2000;
            chartVelocity.ChartAreas[0].AxisY.Minimum = -500;
            chartVelocity.ChartAreas[0].AxisY.Maximum = 2000;
            chartVelocity.Series[0].BorderWidth = 3;
            chartVelocity.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartVelocity.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            chartVelocity.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chartVelocity.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
            
            pathThread = new Thread(new ThreadStart(this.getPathInformation));
            velocityThread = new Thread(new ThreadStart(this.getVelocityInformation));
            pauseEvent = new ManualResetEvent(true);
            pathThread.IsBackground = true;
            pathThread.Start();
            velocityThread.IsBackground = true;
            velocityThread.Start();
            InitPathChart();
            InitVelocityChart();

          
        }

        private void InitVelocityChart()
        {
            for(int i=0; i< 2000; i++){
                float v = (float) (1000.0 * Math.Cos(i * 2.0 * Math.PI / 2000.0) + 900.0);
                velocityQueue.Enqueue(v);
            }
        }

        private void InitPathChart()
        {
            chartPath.Series[0].Points.Clear();
            chartPath.Series[0].Points.AddXY(0, 0);
            chartPath.Series[0].Points.AddXY(25, 0);
            chartPath.Series[0].Points.AddXY(25, -50);
            chartPath.Series[0].Points.AddXY(75, -50);
            chartPath.Series[0].Points.AddXY(100, -50);
            for (int i = 0; i <= 180; i++)
            {
                double theta = (i - 90.0) * Math.PI / 180.0;
                chartPath.Series[0].Points.AddXY(100 + 50*Math.Cos(theta), 0+50*Math.Sin(theta));
            }
            chartPath.Series[0].Points.AddXY(100, 50);
            chartPath.Series[0].Points.AddXY(75, 50);
            chartPath.Series[0].Points.AddXY(25, 50);
            chartPath.Series[0].Points.AddXY(25, 0);
            chartPath.Series[0].Points.AddXY(0, 0);
            chartPath.Series[0].BorderWidth = 3;

            chartPath.Series[3].Points.Clear();
            chartPath.Series[3].Points.AddXY(75, 50);
            chartPath.Series[3].Points.AddXY(25, 50);
            chartPath.Series[3].BorderWidth = 5;
            chartPath.Series[3].Color = Color.IndianRed;
            
            chartPath.Series[1].Points.Clear();
            chartPath.Series[1].Points.AddXY(0, 0);
            chartPath.Series[1].Points.AddXY(25, 0);
            chartPath.Series[1].Points.AddXY(25, -50);
            chartPath.Series[1].Points.AddXY(75, -50);
            chartPath.Series[1].Points.AddXY(100, -50);
            chartPath.Series[1].Points.AddXY(150, 0);
            chartPath.Series[1].Points.AddXY(100, 50);
            chartPath.Series[1].Points.AddXY(75, 50);
            chartPath.Series[1].Points.AddXY(25, 50);
            chartPath.Series[1].Points.AddXY(25, 0);
            chartPath.Series[1].MarkerSize = 10;

            chartPath.Series[2].Points.Clear();
            chartPath.Series[2].Points.AddXY(0, 0);
            chartPath.Series[2].MarkerSize = 15;
        }
        
        
        private void UpdatePathChart()
        {
            chartPath.Series[2].Points.Clear();
            chartPath.Series[2].Points.AddXY(axesCmd[1], axesCmd[2]);
        }

        private void getPathInformation()
        {
            // 固定時間來檢查是否要更新即時的扭力輸出圖
            while (true)
            {
                //pauseEvent.WaitOne(Timeout.Infinite);
                if (chartPath.IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)delegate { UpdatePathChart(); });
                }
                SpinWait.SpinUntil(() => false, 150);
            }
        }

        private void UpdateVelocityChart()
        {
            // 更新即時的速度輸出圖
            chartVelocity.Series[0].Points.Clear();
            int len = velocityQueue.Count;
            Array.Copy(velocityQueue.ToArray(), 0, velocityArray, 0, len);
            for( int i=1; i <=len; i++)
            {
                chartVelocity.Series[0].Points.AddY(velocityArray[len-i]);
            }
        }

        private void getVelocityInformation()
        {
            // 固定時間來檢查是否要更新即時的速度輸出圖
            while (true)
            {
                //pauseEvent.WaitOne(Timeout.Infinite);
                if (chartVelocity.IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)delegate { UpdateVelocityChart(); });
                }
                SpinWait.SpinUntil(() => false, 250);
            }
        }

        private void timerWs_Tick(object sender, EventArgs e)
        {
            bot.EvaluateScript(@".ec-links");

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

            // 顯示同動是否有啟動
            if (coordinatorEnabled)
            {
                buttonCoordinator.BackColor = Color.SpringGreen;
            }
            else
            {
                buttonCoordinator.BackColor = Color.IndianRed;
            }

            // 當有錯誤發生時，需要 Acked 才會顯示下一個錯誤訊息
            if (hasAcked)
            {
                buttonErrorAck.BackColor = Color.SpringGreen;
                buttonErrorAck.Text = "Acked";
            }
            else
            {
                buttonErrorAck.BackColor = Color.IndianRed;
                buttonErrorAck.Text = "Error";
            }

            if (hasSFC)
            {
                buttonReloadSfc.BackColor = Color.SpringGreen;
            }
            else
            {
                buttonReloadSfc.BackColor = Color.IndianRed;
            }

            textSlaves.Text = slavesLen.ToString();
            if (ecReady) {
                textSlaves.BackColor = Color.SpringGreen;
            } else {
                textSlaves.BackColor = Color.IndianRed;
            }

            Boolean allDriveOn = true;
            for (int i = 1; i <= 4; i++)
            {
                if (!isDriveOns[i])
                {
                    allDriveOn = false;
                    break;
                }
            }
            if (allDriveOn && !coordinatorEnabled)
            {
                bot.EvaluateScript(@"+coordinator");
            }

            if (isDriveOns[1])
            {
                buttonOn1.Text = @"ON";
                buttonOn1.BackColor = Color.SpringGreen;
            }
            else
            {
                buttonOn1.Text = @"OFF";
                buttonOn1.BackColor = Color.Gold;
            }
            if (isDriveOns[2])
            {
                buttonOn2.Text = @"ON";
                buttonOn2.BackColor = Color.SpringGreen;
            }
            else
            {
                buttonOn2.Text = @"OFF";
                buttonOn2.BackColor = Color.Gold;
            }
            if (isDriveOns[3])
            {
                buttonOn3.Text = @"ON";
                buttonOn3.BackColor = Color.SpringGreen;
            }
            else
            {
                buttonOn3.Text = @"OFF";
                buttonOn3.BackColor = Color.Gold;
            }
            if (isDriveOns[4])
            {
                buttonOn4.Text = @"ON";
                buttonOn4.BackColor = Color.SpringGreen;
            }
            else
            {
                buttonOn4.Text = @"OFF";
                buttonOn4.BackColor = Color.Gold;
            }

            if (isDriveFaults[1])
            {
                buttonFault1.BackColor = Color.IndianRed;
            }
            else
            {
                buttonFault1.BackColor = Color.SpringGreen;
            }
            if (isDriveFaults[2])
            {
                buttonFault2.BackColor = Color.IndianRed;
            }
            else
            {
                buttonFault2.BackColor = Color.SpringGreen;
            }
            if (isDriveFaults[3])
            {
                buttonFault3.BackColor = Color.IndianRed;
            }
            else
            {
                buttonFault3.BackColor = Color.SpringGreen;
            }
            if (isDriveFaults[4])
            {
                buttonFault4.BackColor = Color.IndianRed;
            }
            else
            {
                buttonFault4.BackColor = Color.SpringGreen;
            }

        }

        private void buttonReloadSfc_Click(object sender, EventArgs e)
        {
            // 重新載入 SFC
            bot.EvaluateScript(@"0sfc -work marker -work 0 user-para! .user-para");
            hasSFC = false;
        }

        private Boolean hasSlaves = false;
        private void timerLoop_Tick(object sender, EventArgs e)
        {
            // 要求回傳同動與運動軸的狀態
            bot.EvaluateScript(@".coordinator 1 .axis 2 .axis 3 .axis 4 .axis");

            if (slavesLen > 0)
            {
                string cmd = null;
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
                bot.EvaluateScript(cmd);

            }

            textAxis1Cmd.Text = axesCmd[1].ToString("F3");
            textAxis2Cmd.Text = axesCmd[2].ToString("F3");
            textAxis3Cmd.Text = axesCmd[3].ToString("F3");
            textAxis4Cmd.Text = axesCmd[4].ToString("F3");
            textAxis1Feedback.Text = axesFeedback[1].ToString("F3");
            textAxis2Feedback.Text = axesFeedback[2].ToString("F3");
            textAxis3Feedback.Text = axesFeedback[3].ToString("F3");
            textAxis4Feedback.Text = axesFeedback[4].ToString("F3");
            textAxis1FollowingErr.Text = axesFollowingErr[1].ToString("F3");
            textAxis2FollowingErr.Text = axesFollowingErr[2].ToString("F3");
            textAxis3FollowingErr.Text = axesFollowingErr[3].ToString("F3");
            textAxis4FollowingErr.Text = axesFollowingErr[4].ToString("F3");
            textMPGAxis.Text = mpgAxis.ToString();
            textMPGEncoder.Text = mpgEncoder.ToString();
            textMPGRate.Text = mpgRate.ToString();
            
        }

        private void buttonCoordinator_Click(object sender, EventArgs e)
        {
            // 開啟/關閉同動
            if (coordinatorEnabled)
            {
                bot.EvaluateScript(@"-coordinator");
            }
            else
            {
                bot.EvaluateScript(@"+coordinator");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"group-prepare ' group-loop +step");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hasAcked = true;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"0sfc ems-job -coordinator");
            hasSFC = false;

        }


        private void button3_Click(object sender, EventArgs e)
        {
            string cmd = @"-coordinator";
            for (int i = 1; i <= 4; i++)
            {
                if (isServoDrives[i])
                {    
                    cmd += (@"  1 " +  i.ToString() + @" reset-fault ");
                }
            }
            bot.EvaluateScript(cmd);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string cmd = @"-coordinator";
            for (int i = 1; i <= 4; i++)
            {
                if (isServoDrives[i])
                {
                    cmd += (@" 1 " + i.ToString() + @" over over real-p@ -rot target-p! ");
                    cmd += (@" csp  1 " + i.ToString() + @" op-mode! ");
                    cmd += (@" 1 " + i.ToString() + @" drive-on ");
                }
            }
            bot.EvaluateScript(cmd);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string cmd = @"-coordinator";
            for (int i = 1; i <= 4; i++)
            {
                if (isServoDrives[i])
                {
                    cmd += (@" 1 " + i.ToString() + @" drive-off ");
                }
            }
            bot.EvaluateScript(cmd);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"1 group! 1000.0e mm/min vcmd!");
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"1 group! 1500.0e mm/min vcmd!");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"1 group! 2000.0e mm/min vcmd!");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"1 group! -500.0e mm/min vcmd!");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"1 group! 0.0e mm/min vcmd!");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            bot.Poweroff();
            ecReady = false;
            wsState = 0;
            hasSFC = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bot.Reboot();
            ecReady = false;
            wsState = 0;
            hasSFC = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(@"1 group! 500.0e mm/min vcmd!");
        }

       

      

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BotnanaLib;

namespace AxesMove
{
    public partial class Form1 : Form
    {
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
                    bot.EvaluateScript(@"0sfc ignore-overrun");
                    bot.LoadSFC(@"axes-move.sfc");
                    bot.EvaluateScript(@"marker -axes .user-para");                
                    break;
                case 16:
                    hasSFC = true;
                    bot.EvaluateScript(@"reset-overrun 32 user-para!");
                    break;
                case 32:
                    hasSFC = true;
                    break;
                default:
                    break;
            }
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

        // 區段位置的設定內容，index 0 不使用
        private Boolean[,] axisSectionPosModifies = new Boolean[3, 6];
        private TextBox[,] textAxisSectionPositions = new TextBox[3, 6];
        private double[,] axisSectionPositions = new double[3,6];

        // 當取得 axis_section_p.x 時，要處置的方法，此時 x 是 callback function 回傳的 position
        private HandleTagNameMessage onAxisSectionPositions;
        private void OnAxisSectionPositions(IntPtr dataPtr, UInt32 position, UInt32 channel, string str)
        {
            // 將 m 轉換成 mm
            axisSectionPositions[position, channel] = double.Parse(str)*1000.0;
        }

        // 區段速度的設定內容，index 0 不使用
        private Boolean[,] axisSectionVelModifies = new Boolean[3, 6];
        private double[,] axisSectionVecloities = new double[3,6];
        private TextBox[,] textAxisSectionVecloities = new TextBox[3, 6];

        // 當取得 axis_section_v.x 時，要處置的方法，此時 x 是 callback function 回傳的 position
        private HandleTagNameMessage onAxisSectionVecloities;
        private void OnAxisSectionVecloities(IntPtr dataPtr, UInt32 position, UInt32 channel, string str)
        {
            // 將 m/s 轉換成 mm/min
            axisSectionVecloities[position, channel] = double.Parse(str)*1000.0*60.0;
        }

        // 運動軸是否在執行區段的運動 
        private Boolean[] axisRunnings = new Boolean[3];

        // 當取得 axis_running.x 時，要處置的方法，此時 x 是 callback function 回傳的 position
        private HandleTagNameMessage onAxisRunnings;
        private void OnAxisRunnings(IntPtr dataPtr, UInt32 position, UInt32 channel, string str)
        {
            axisRunnings[position] = (int.Parse(str) != 0);
        }

        // 運動軸的目標位置
        private double[] axisCmds = new double[3];

        // 當取得 axis_command_position.x 時，要處置的方法，此時 x 是 callback function 回傳的 position
        private HandleTagNameMessage onAxisCmds;
        private void OnAxisCmds(IntPtr dataPtr, UInt32 position, UInt32 channel, string str)
        {
            // 將 m 轉換成 mm
            axisCmds[position] = double.Parse(str) * 1000.0;
        }

        // 運動軸在目前時刻所規畫的命令位置
        private double[] axisDemands = new double[3];

        // 當取得 axis_demand_position.x 時，要處置的方法，此時 x 是 callback function 回傳的 position
        private HandleTagNameMessage onAxisDemands;
        private void OnAxisDemands(IntPtr dataPtr, UInt32 position, UInt32 channel, string str)
        {
            // 將 m 轉換成 mm
            axisDemands[position] = double.Parse(str) * 1000.0;
        }

        // 運動軸的回授位置
        private double[] axisFeedbacks = new double[3];

        // 當取得 feedback_position.x 時，要處置的方法，此時 x 是 callback function 回傳的 position
        private HandleTagNameMessage onAxisFeedbacks;
        private void OnAxisFeedbacks(IntPtr dataPtr, UInt32 position, UInt32 channel, string str)
        {
            // 將 m 轉換成 mm
            axisFeedbacks[position] = double.Parse(str) * 1000.0;
        }

        // 運動軸的是否已經到達目標位置
        private Boolean[] axisReacheds = new Boolean[3];
        private HandleTagNameMessage onAxisReacheds;
        private void OnAxisReacheds(IntPtr dataPtr, UInt32 position, UInt32 channel, string str)
        {
            axisReacheds[position] = (int.Parse(str) != 0);
        }

        // 運動軸的是否有開啟運動功能
        private Boolean[] axisInterpolatorEnables = new Boolean[3];
        private HandleTagNameMessage onAxisInterpolatorEnables;
        private void OnAxisInterpolatorEnables(IntPtr dataPtr, UInt32 position, UInt32 channel, string str)
        {
            axisInterpolatorEnables[position] = (int.Parse(str) != 0);
        }

        // 是否有啟動同動功能
        private Boolean coordinatorEnabled = false;
        private HandleMessage onCoordinatorEnabled;
        private void OnCoordinatorEnabled(IntPtr dataPtr, string str)
        {
            coordinatorEnabled = (int.Parse(str) != 0);
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

            // 收到 tag = error 就呼叫 OnErrorMessageCallback
            onErrorMessage = new HandleMessage(OnErrorMessageCallback);
            bot.SetTagCB(@"error", 0, IntPtr.Zero, onErrorMessage);

            onUserParameter = new HandleMessage(OnUserParameterCallback);
            bot.SetTagCB(@"user_parameter", 0, IntPtr.Zero, onUserParameter);

            onAxisSectionPositions = new HandleTagNameMessage(OnAxisSectionPositions);
            bot.SetTagNameCB(@"axis_section_p", 0, IntPtr.Zero, onAxisSectionPositions);

            onAxisSectionVecloities = new HandleTagNameMessage(OnAxisSectionVecloities);
            bot.SetTagNameCB(@"axis_section_v", 0, IntPtr.Zero, onAxisSectionVecloities);

            onAxisRunnings = new HandleTagNameMessage(OnAxisRunnings);
            bot.SetTagNameCB(@"axis_running", 0, IntPtr.Zero, onAxisRunnings);

            onAxisCmds = new HandleTagNameMessage(OnAxisCmds);
            bot.SetTagNameCB(@"axis_command_position", 0, IntPtr.Zero, onAxisCmds);

            onAxisDemands = new HandleTagNameMessage(OnAxisDemands);
            bot.SetTagNameCB(@"axis_demand_position", 0, IntPtr.Zero, onAxisDemands);

            onAxisFeedbacks = new HandleTagNameMessage(OnAxisFeedbacks);
            bot.SetTagNameCB(@"feedback_position", 0, IntPtr.Zero, onAxisFeedbacks);

            onAxisReacheds = new HandleTagNameMessage(OnAxisReacheds);
            bot.SetTagNameCB(@"axis_interpolator_reached", 0, IntPtr.Zero, onAxisReacheds);

            onAxisInterpolatorEnables = new HandleTagNameMessage(OnAxisInterpolatorEnables);
            bot.SetTagNameCB(@"axis_interpolator_enabled", 0, IntPtr.Zero, onAxisInterpolatorEnables);

            onCoordinatorEnabled = new HandleMessage(OnCoordinatorEnabled);
            bot.SetTagCB(@"coordinator_enabled", 0, IntPtr.Zero, onCoordinatorEnabled);

            // 將區段位置與速度的參數元件放到一個陣列內
            textAxisSectionPositions[1, 1] = textAxis1P1;
            textAxisSectionPositions[1, 2] = textAxis1P2;
            textAxisSectionPositions[1, 3] = textAxis1P3;
            textAxisSectionPositions[1, 4] = textAxis1P4;
            textAxisSectionPositions[1, 5] = textAxis1P5;
            textAxisSectionVecloities[1, 1] = textAxis1V1;
            textAxisSectionVecloities[1, 2] = textAxis1V2;
            textAxisSectionVecloities[1, 3] = textAxis1V3;
            textAxisSectionVecloities[1, 4] = textAxis1V4;
            textAxisSectionVecloities[1, 5] = textAxis1V5;
            textAxisSectionPositions[2, 1] = textAxis2P1;
            textAxisSectionPositions[2, 2] = textAxis2P2;
            textAxisSectionPositions[2, 3] = textAxis2P3;
            textAxisSectionPositions[2, 4] = textAxis2P4;
            textAxisSectionPositions[2, 5] = textAxis2P5;
            textAxisSectionVecloities[2, 1] = textAxis2V1;
            textAxisSectionVecloities[2, 2] = textAxis2V2;
            textAxisSectionVecloities[2, 3] = textAxis2V3;
            textAxisSectionVecloities[2, 4] = textAxis2V4;
            textAxisSectionVecloities[2, 5] = textAxis2V5;

            timerWSCheck.Interval = 1000;
            timerWSCheck.Enabled = true;
            timerLoop.Interval = 50;
            timerLoop.Enabled = true;
        }

        private void timerWSCheck_Tick(object sender, EventArgs e)
        {
            // 處理 WebSocket 的連線狀態
            if (wsState == 2)
            {
                buttonWS.BackColor = Color.SpringGreen;
            }
            else if (wsState == 1)
            {
                buttonWS.BackColor = Color.Gold;
            }
            else
            {
                // 自動連線
                bot.Connect();
                wsState = 1;
                buttonWS.BackColor = Color.IndianRed;
            }

            // 當有錯誤發生時，需要 Acked 才會顯示下一個錯誤訊息
            if (hasAcked)
            {
                buttonErrorAck.BackColor = Color.SpringGreen;
                buttonErrorAck.Text = "Acked";
            } else
            {
                buttonErrorAck.BackColor = Color.IndianRed;
                buttonErrorAck.Text = "Error";
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

            if (hasSFC)
            {
                buttonSFCReload.BackColor = Color.SpringGreen;
            }
            else
            {
                buttonSFCReload.BackColor = Color.IndianRed;
            }


        }

        private void timerLoop_Tick(object sender, EventArgs e)
        {
            // 要求回傳同動與運動軸的狀態
            bot.EvaluateScript(@".coordinator 1 .axis 2 .axis");
            if (hasSFC)
            {
                // 要求回傳 SFC 的區段位置與速度參數與目前的執行狀態
                bot.EvaluateScript(@".axis1-sections .axis1-status .axis2-sections .axis2-status");
            }

            // 更新區段位置與速度參數
            for (int i = 1; i <= 2; i++) {
                for (int j = 1; j <= 5; j++)
                {
                    if (!axisSectionPosModifies[i, j])
                    {
                        textAxisSectionPositions[i, j].Text = axisSectionPositions[i, j].ToString("F3");
                    }
                    if (!axisSectionVelModifies[i, j])
                    {
                        textAxisSectionVecloities[i, j].Text = axisSectionVecloities[i, j].ToString("F1");
                    }
                }
             }

            // 其它狀態顯示           
            textAxis1Cmd.Text = axisCmds[1].ToString("F3");
            textAxis1Demand.Text = axisDemands[1].ToString("F3");
            textAxis1Feedback.Text = axisFeedbacks[1].ToString("F3");
            radioAxis1Running.Checked = axisRunnings[1];
            radioAxis1Reached.Checked = axisReacheds[1];
            radioAxis1Interpolator.Checked = axisInterpolatorEnables[1];
            textAxis2Cmd.Text = axisCmds[2].ToString("F3");
            textAxis2Demand.Text = axisDemands[2].ToString("F3");
            textAxis2Feedback.Text = axisFeedbacks[2].ToString("F3");
            radioAxis2Running.Checked = axisRunnings[2];
            radioAxis2Running.Checked = axisRunnings[2];
            radioAxis2Reached.Checked = axisReacheds[2];
        }
             

        private void buttonCoordinator_Click(object sender, EventArgs e)
        {
            // 開啟/關閉同動
            if (coordinatorEnabled)
            {
                bot.EvaluateScript(@"-coordinator");
            } else
            {
                bot.EvaluateScript(@"+coordinator");
            }
        }

        private void ErrorAck_Click(object sender, EventArgs e)
        {
            // Ack. Error
            hasAcked = true;
        }

        private void buttonAxis1Start_Click(object sender, EventArgs e)
        {
            // 啟動第一軸運動
            uint len = uint.Parse(textAxis1SectionLen.Text);
            bot.EvaluateScript(String.Format(@"{0} start-axis1", len));
        }

        private void buttonAxis1Stop_Click(object sender, EventArgs e)
        {
            // 停止第一軸運動
            bot.EvaluateScript(@"stop-axis1");
        }

        private void ChangeAxisSectionPosition(uint no, uint index, string str)
        {
            // 修改區段位置參數
            float value;
            if (float.TryParse(str, out value))
            {
                
                bot.EvaluateScript(String.Format(@"{0}e mm {1} axis{2}-p!", str, index, no));
                axisSectionPosModifies[no, index] = false;
            }
        }

        private void ChangeAxisSectionVelocity(uint no, uint index, string str)
        {
            // 修改區段速度參數
            float value;
            if (float.TryParse(str, out value))
            {
                bot.EvaluateScript(String.Format(@"{0}e mm/min {1} axis{2}-v!", str, index, no));
                axisSectionVelModifies[no, index] = false;
            }
        }

        private void textAxis1P1_Leave(object sender, EventArgs e)
        {
            ChangeAxisSectionPosition(1, 1, textAxis1P1.Text);
        }

        private void textAxis1P1_Enter(object sender, EventArgs e)
        {
            axisSectionPosModifies[1, 1] = true;
        }

        private void textAxis1P1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChangeAxisSectionPosition(1, 1, textAxis1P1.Text);
            }
        }

        private void textAxis1P2_Leave(object sender, EventArgs e)
        {
            ChangeAxisSectionPosition(1, 2, textAxis1P2.Text);
        }

        private void textAxis1P2_Enter(object sender, EventArgs e)
        {
            axisSectionPosModifies[1, 2] = true;
        }

        private void textAxis1P2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChangeAxisSectionPosition(1, 2, textAxis1P2.Text);
            }
        }
        private void textAxis1P3_Leave(object sender, EventArgs e)
        {
            ChangeAxisSectionPosition(1, 3, textAxis1P3.Text);
        }

        private void textAxis1P3_Enter(object sender, EventArgs e)
        {
            axisSectionPosModifies[1, 3] = true;
        }

        private void textAxis1P3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChangeAxisSectionPosition(1, 3, textAxis1P3.Text);
            }
        }

        private void textAxis1P4_Leave(object sender, EventArgs e)
        {
            ChangeAxisSectionPosition(1, 4, textAxis1P4.Text);
        }

        private void textAxis1P4_Enter(object sender, EventArgs e)
        {
            axisSectionPosModifies[1, 4] = true;
        }

        private void textAxis1P4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChangeAxisSectionPosition(1, 4, textAxis1P4.Text);
            }
        }

        private void textAxis1P5_Leave(object sender, EventArgs e)
        {
            ChangeAxisSectionPosition(1, 5, textAxis1P5.Text);
        }

        private void textAxis1P5_Enter(object sender, EventArgs e)
        {
            axisSectionPosModifies[1, 5] = true;
        }

        private void textAxis1P5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChangeAxisSectionPosition(1, 5, textAxis1P5.Text);
            }
        }


        private void textAxis1V1_Leave(object sender, EventArgs e)
        {
            ChangeAxisSectionVelocity(1, 1, textAxis1V1.Text);
        }

        private void textAxis1V1_Enter(object sender, EventArgs e)
        {
            axisSectionVelModifies[1, 1] = true;
        }

        private void textAxis1V1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChangeAxisSectionVelocity(1, 1, textAxis1V1.Text);

            }
        }

        private void textAxis1V2_Leave(object sender, EventArgs e)
        {
            ChangeAxisSectionVelocity(1, 2, textAxis1V2.Text);
        }

        private void textAxis1V2_Enter(object sender, EventArgs e)
        {
            axisSectionVelModifies[1, 2] = true;
        }

        private void textAxis1V2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChangeAxisSectionVelocity(1, 2, textAxis1V2.Text);
            }
        }
        private void textAxis1V3_Leave(object sender, EventArgs e)
        {
            ChangeAxisSectionVelocity(1, 3, textAxis1V3.Text);
        }

        private void textAxis1V3_Enter(object sender, EventArgs e)
        {
            axisSectionVelModifies[1, 3] = true;
        }

        private void textAxis1V3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChangeAxisSectionVelocity(1, 3, textAxis1V3.Text);
            }
        }

        private void textAxis1V4_Leave(object sender, EventArgs e)
        {
            ChangeAxisSectionVelocity(1, 4, textAxis1V4.Text);
        }

        private void textAxis1V4_Enter(object sender, EventArgs e)
        {
            axisSectionVelModifies[1, 4] = true;
        }

        private void textAxis1V4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChangeAxisSectionVelocity(1, 4, textAxis1V4.Text);
            }
        }

        private void textAxis1V5_Leave(object sender, EventArgs e)
        {
            ChangeAxisSectionVelocity(1, 5, textAxis1V5.Text);
        }

        private void textAxis1V5_Enter(object sender, EventArgs e)
        {
            axisSectionVelModifies[1, 5] = true;
        }

        private void textAxis1V5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChangeAxisSectionVelocity(1, 5, textAxis1V5.Text);
            }
        }

        private void buttonAxis2Start_Click(object sender, EventArgs e)
        {
            // 啟動第二軸運動
            uint len = uint.Parse(textAxis2SectionLen.Text);
            bot.EvaluateScript(String.Format(@"{0} start-axis2",len));
        }

        private void buttonAxis2Stop_Click(object sender, EventArgs e)
        {
            // 停止第二軸運動
            bot.EvaluateScript(@"stop-axis2");
        }

        private void textAxis2P1_Leave(object sender, EventArgs e)
        {
            ChangeAxisSectionPosition(2, 1, textAxis2P1.Text);
        }

        private void textAxis2P1_Enter(object sender, EventArgs e)
        {
            axisSectionPosModifies[2, 1] = true;
        }

        private void textAxis2P1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChangeAxisSectionPosition(2, 1, textAxis2P1.Text);
            }
        }

        private void textAxis2P2_Leave(object sender, EventArgs e)
        {
            ChangeAxisSectionPosition(2, 2, textAxis2P2.Text);
        }

        private void textAxis2P2_Enter(object sender, EventArgs e)
        {
            axisSectionPosModifies[2, 2] = true;
        }

        private void textAxis2P2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChangeAxisSectionPosition(2, 2, textAxis2P2.Text);
            }
        }
        private void textAxis2P3_Leave(object sender, EventArgs e)
        {
            ChangeAxisSectionPosition(2, 3, textAxis2P3.Text);
        }

        private void textAxis2P3_Enter(object sender, EventArgs e)
        {
            axisSectionPosModifies[2, 3] = true;
        }

        private void textAxis2P3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChangeAxisSectionPosition(2, 3, textAxis2P3.Text);
            }
        }

        private void textAxis2P4_Leave(object sender, EventArgs e)
        {
            ChangeAxisSectionPosition(2, 4, textAxis2P4.Text);
        }

        private void textAxis2P4_Enter(object sender, EventArgs e)
        {
            axisSectionPosModifies[2, 4] = true;
        }

        private void textAxis2P4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChangeAxisSectionPosition(2, 4, textAxis2P4.Text);
            }
        }

        private void textAxis2P5_Leave(object sender, EventArgs e)
        {
            ChangeAxisSectionPosition(2, 5, textAxis2P5.Text);
        }

        private void textAxis2P5_Enter(object sender, EventArgs e)
        {
            axisSectionPosModifies[2, 5] = true;
        }

        private void textAxis2P5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChangeAxisSectionPosition(2, 5, textAxis2P5.Text);
            }
        }
        
        private void textAxis2V1_Leave(object sender, EventArgs e)
        {
            ChangeAxisSectionVelocity(2, 1, textAxis2V1.Text);
        }

        private void textAxis2V1_Enter(object sender, EventArgs e)
        {
            axisSectionVelModifies[2, 1] = true;
        }

        private void textAxis2V1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               ChangeAxisSectionVelocity(2, 1, textAxis1V1.Text);
            }
        }

        private void textAxis2V2_Leave(object sender, EventArgs e)
        {
            ChangeAxisSectionVelocity(2, 2, textAxis2V2.Text);
        }

        private void textAxis2V2_Enter(object sender, EventArgs e)
        {
            axisSectionVelModifies[2, 2] = true;
        }

        private void textAxis2V2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChangeAxisSectionVelocity(2, 2, textAxis2V2.Text);
            }
        }
        private void textAxis2V3_Leave(object sender, EventArgs e)
        {
            ChangeAxisSectionVelocity(2, 3, textAxis2V3.Text);
        }

        private void textAxis2V3_Enter(object sender, EventArgs e)
        {
            axisSectionVelModifies[2, 3] = true;
        }

        private void textAxis2V3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChangeAxisSectionVelocity(2, 3, textAxis2V3.Text);
            }
        }

        private void textAxis2V4_Leave(object sender, EventArgs e)
        {
            ChangeAxisSectionVelocity(2, 4, textAxis2V4.Text);
        }

        private void textAxis2V4_Enter(object sender, EventArgs e)
        {
            axisSectionVelModifies[2, 4] = true;
        }

        private void textAxis2V4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChangeAxisSectionVelocity(2, 4, textAxis2V4.Text);
            }
        }

        private void textAxis2V5_Leave(object sender, EventArgs e)
        {
            ChangeAxisSectionVelocity(2, 5, textAxis2V5.Text);
        }

        private void textAxis2V5_Enter(object sender, EventArgs e)
        {
            axisSectionVelModifies[2, 5] = true;
        }

        private void textAxis2V5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChangeAxisSectionVelocity(2, 5, textAxis2V5.Text);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // 重新載入 SFC
            bot.EvaluateScript(@"0sfc -work marker -work 0 user-para! .user-para");
            hasSFC = false;
        }
    }
}

namespace ManualPulseGenerator
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonWebSocket = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timerSlow = new System.Windows.Forms.Timer(this.components);
            this.groupEtherCAT = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textSlavesLen = new System.Windows.Forms.TextBox();
            this.textErrorCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textMPGRate = new System.Windows.Forms.TextBox();
            this.radioMPGOn = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.textMPGEncoder = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textDriveMode = new System.Windows.Forms.TextBox();
            this.radioDriveFault = new System.Windows.Forms.RadioButton();
            this.radioDriveOn = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.textDriveTargetPosition = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textDriveRealPosition = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonDriveStop = new System.Windows.Forms.Button();
            this.buttonDriveResetFault = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonServoOn = new System.Windows.Forms.Button();
            this.radioCoordinator = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioAxisReached = new System.Windows.Forms.RadioButton();
            this.textAxisFollowingError = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textAxisCommand = new System.Windows.Forms.TextBox();
            this.radioAxisInterpolator = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.textAxisDemand = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textAxisFeedback = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.radioSfcConfition = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.textMpgSfcRate = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textMpgSelected = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupEtherCAT.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonWebSocket
            // 
            this.buttonWebSocket.Location = new System.Drawing.Point(10, 27);
            this.buttonWebSocket.Margin = new System.Windows.Forms.Padding(4);
            this.buttonWebSocket.Name = "buttonWebSocket";
            this.buttonWebSocket.Size = new System.Drawing.Size(116, 31);
            this.buttonWebSocket.TabIndex = 0;
            this.buttonWebSocket.Text = "WebSocket";
            this.buttonWebSocket.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timerSlow
            // 
            this.timerSlow.Tick += new System.EventHandler(this.timerSlow_Tick);
            // 
            // groupEtherCAT
            // 
            this.groupEtherCAT.Controls.Add(this.label1);
            this.groupEtherCAT.Controls.Add(this.textSlavesLen);
            this.groupEtherCAT.Location = new System.Drawing.Point(12, 12);
            this.groupEtherCAT.Name = "groupEtherCAT";
            this.groupEtherCAT.Size = new System.Drawing.Size(204, 54);
            this.groupEtherCAT.TabIndex = 1;
            this.groupEtherCAT.TabStop = false;
            this.groupEtherCAT.Text = "EtherCAT";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Slaves Len";
            // 
            // textSlavesLen
            // 
            this.textSlavesLen.Location = new System.Drawing.Point(116, 24);
            this.textSlavesLen.Name = "textSlavesLen";
            this.textSlavesLen.Size = new System.Drawing.Size(78, 27);
            this.textSlavesLen.TabIndex = 0;
            this.textSlavesLen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textErrorCount
            // 
            this.textErrorCount.Location = new System.Drawing.Point(117, 85);
            this.textErrorCount.Name = "textErrorCount";
            this.textErrorCount.Size = new System.Drawing.Size(77, 27);
            this.textErrorCount.TabIndex = 2;
            this.textErrorCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Error Count";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textMPGRate);
            this.groupBox1.Controls.Add(this.radioMPGOn);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textMPGEncoder);
            this.groupBox1.Location = new System.Drawing.Point(12, 217);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(204, 121);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MPG";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Rate";
            // 
            // textMPGRate
            // 
            this.textMPGRate.Location = new System.Drawing.Point(114, 76);
            this.textMPGRate.Name = "textMPGRate";
            this.textMPGRate.Size = new System.Drawing.Size(79, 27);
            this.textMPGRate.TabIndex = 7;
            this.textMPGRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // radioMPGOn
            // 
            this.radioMPGOn.AutoSize = true;
            this.radioMPGOn.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioMPGOn.Location = new System.Drawing.Point(7, 26);
            this.radioMPGOn.Name = "radioMPGOn";
            this.radioMPGOn.Size = new System.Drawing.Size(48, 20);
            this.radioMPGOn.TabIndex = 6;
            this.radioMPGOn.TabStop = true;
            this.radioMPGOn.Text = "ON";
            this.radioMPGOn.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Encoder";

            // 
            // textMPGEncoder
            // 
            this.textMPGEncoder.Location = new System.Drawing.Point(115, 49);
            this.textMPGEncoder.Name = "textMPGEncoder";
            this.textMPGEncoder.Size = new System.Drawing.Size(78, 27);
            this.textMPGEncoder.TabIndex = 4;
            this.textMPGEncoder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textDriveMode);
            this.groupBox2.Controls.Add(this.radioDriveFault);
            this.groupBox2.Controls.Add(this.radioDriveOn);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textDriveTargetPosition);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textDriveRealPosition);
            this.groupBox2.Location = new System.Drawing.Point(12, 69);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(204, 142);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Drive";

            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 16);
            this.label7.TabIndex = 15;
            this.label7.Text = "Operation Mode";

            // 
            // textDriveMode
            // 
            this.textDriveMode.Location = new System.Drawing.Point(116, 49);
            this.textDriveMode.Name = "textDriveMode";
            this.textDriveMode.Size = new System.Drawing.Size(78, 27);
            this.textDriveMode.TabIndex = 14;
            this.textDriveMode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // radioDriveFault
            // 
            this.radioDriveFault.AutoCheck = false;
            this.radioDriveFault.AutoSize = true;
            this.radioDriveFault.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioDriveFault.Location = new System.Drawing.Point(116, 26);
            this.radioDriveFault.Name = "radioDriveFault";
            this.radioDriveFault.Size = new System.Drawing.Size(57, 20);
            this.radioDriveFault.TabIndex = 13;
            this.radioDriveFault.TabStop = true;
            this.radioDriveFault.Text = "Fault";
            this.radioDriveFault.UseVisualStyleBackColor = true;
            // 
            // radioDriveOn
            // 
            this.radioDriveOn.AutoCheck = false;
            this.radioDriveOn.AutoSize = true;
            this.radioDriveOn.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioDriveOn.Location = new System.Drawing.Point(7, 26);
            this.radioDriveOn.Name = "radioDriveOn";
            this.radioDriveOn.Size = new System.Drawing.Size(48, 20);
            this.radioDriveOn.TabIndex = 12;
            this.radioDriveOn.TabStop = true;
            this.radioDriveOn.Text = "ON";
            this.radioDriveOn.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 16);
            this.label6.TabIndex = 9;
            this.label6.Text = "Target Position";
            // 
            // textDriveTargetPosition
            // 
            this.textDriveTargetPosition.Location = new System.Drawing.Point(116, 107);
            this.textDriveTargetPosition.Name = "textDriveTargetPosition";
            this.textDriveTargetPosition.Size = new System.Drawing.Size(78, 27);
            this.textDriveTargetPosition.TabIndex = 8;
            this.textDriveTargetPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "Real Position";
            // 
            // textDriveRealPosition
            // 
            this.textDriveRealPosition.Location = new System.Drawing.Point(116, 78);
            this.textDriveRealPosition.Name = "textDriveRealPosition";
            this.textDriveRealPosition.Size = new System.Drawing.Size(78, 27);
            this.textDriveRealPosition.TabIndex = 6;
            this.textDriveRealPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonDriveStop);
            this.groupBox3.Controls.Add(this.buttonDriveResetFault);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.buttonServoOn);
            this.groupBox3.Location = new System.Drawing.Point(532, 147);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 191);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Drive Control";
            // 
            // buttonDriveStop
            // 
            this.buttonDriveStop.Location = new System.Drawing.Point(13, 138);
            this.buttonDriveStop.Name = "buttonDriveStop";
            this.buttonDriveStop.Size = new System.Drawing.Size(89, 35);
            this.buttonDriveStop.TabIndex = 3;
            this.buttonDriveStop.Text = "Stop";
            this.buttonDriveStop.UseVisualStyleBackColor = true;
            this.buttonDriveStop.Click += new System.EventHandler(this.buttonDriveStop_Click);
            // 
            // buttonDriveResetFault
            // 
            this.buttonDriveResetFault.Location = new System.Drawing.Point(13, 99);
            this.buttonDriveResetFault.Name = "buttonDriveResetFault";
            this.buttonDriveResetFault.Size = new System.Drawing.Size(89, 35);
            this.buttonDriveResetFault.TabIndex = 2;
            this.buttonDriveResetFault.Text = "Reset Fault";
            this.buttonDriveResetFault.UseVisualStyleBackColor = true;
            this.buttonDriveResetFault.Click += new System.EventHandler(this.buttonDriveResetFault_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 60);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 35);
            this.button1.TabIndex = 1;
            this.button1.Text = "Drive Off";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // buttonServoOn
            // 
            this.buttonServoOn.Location = new System.Drawing.Point(13, 21);
            this.buttonServoOn.Name = "buttonServoOn";
            this.buttonServoOn.Size = new System.Drawing.Size(89, 35);
            this.buttonServoOn.TabIndex = 0;
            this.buttonServoOn.Text = "Drive On";
            this.buttonServoOn.UseVisualStyleBackColor = true;
            this.buttonServoOn.Click += new System.EventHandler(this.buttonServoOn_Click);
            // 
            // radioCoordinator
            // 
            this.radioCoordinator.AutoCheck = false;
            this.radioCoordinator.AutoSize = true;
            this.radioCoordinator.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioCoordinator.Location = new System.Drawing.Point(10, 64);
            this.radioCoordinator.Name = "radioCoordinator";
            this.radioCoordinator.Size = new System.Drawing.Size(101, 20);
            this.radioCoordinator.TabIndex = 12;
            this.radioCoordinator.TabStop = true;
            this.radioCoordinator.Text = "Coordinator";
            this.radioCoordinator.UseVisualStyleBackColor = true;
            
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.radioAxisReached);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.textAxisFollowingError);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.textAxisCommand);
            this.groupBox5.Controls.Add(this.radioAxisInterpolator);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.textAxisDemand);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.textAxisFeedback);
            this.groupBox5.Location = new System.Drawing.Point(222, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(304, 177);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Axis";
            // 
            // radioAxisReached
            // 
            this.radioAxisReached.AutoCheck = false;
            this.radioAxisReached.AutoSize = true;
            this.radioAxisReached.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioAxisReached.Location = new System.Drawing.Point(125, 24);
            this.radioAxisReached.Name = "radioAxisReached";
            this.radioAxisReached.Size = new System.Drawing.Size(80, 20);
            this.radioAxisReached.TabIndex = 20;
            this.radioAxisReached.TabStop = true;
            this.radioAxisReached.Text = "Reached";
            this.radioAxisReached.UseVisualStyleBackColor = true;
            
            // 
            // textAxisFollowingError
            // 
            this.textAxisFollowingError.Location = new System.Drawing.Point(125, 145);
            this.textAxisFollowingError.Name = "textAxisFollowingError";
            this.textAxisFollowingError.Size = new System.Drawing.Size(95, 27);
            this.textAxisFollowingError.TabIndex = 19;
            this.textAxisFollowingError.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 148);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(107, 16);
            this.label12.TabIndex = 18;
            this.label12.Text = "Following Error";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 58);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 16);
            this.label11.TabIndex = 17;
            this.label11.Text = "Command Pos.";
            // 
            // textAxisCommand
            // 
            this.textAxisCommand.Location = new System.Drawing.Point(125, 55);
            this.textAxisCommand.Name = "textAxisCommand";
            this.textAxisCommand.Size = new System.Drawing.Size(95, 27);
            this.textAxisCommand.TabIndex = 16;
            this.textAxisCommand.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            
            // 
            // radioAxisInterpolator
            // 
            this.radioAxisInterpolator.AutoCheck = false;
            this.radioAxisInterpolator.AutoSize = true;
            this.radioAxisInterpolator.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioAxisInterpolator.Location = new System.Drawing.Point(3, 24);
            this.radioAxisInterpolator.Name = "radioAxisInterpolator";
            this.radioAxisInterpolator.Size = new System.Drawing.Size(99, 20);
            this.radioAxisInterpolator.TabIndex = 12;
            this.radioAxisInterpolator.TabStop = true;
            this.radioAxisInterpolator.Text = "Interpolator";
            this.radioAxisInterpolator.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 16);
            this.label9.TabIndex = 9;
            this.label9.Text = "Demand Pos.";
            // 
            // textAxisDemand
            // 
            this.textAxisDemand.Location = new System.Drawing.Point(125, 85);
            this.textAxisDemand.Name = "textAxisDemand";
            this.textAxisDemand.Size = new System.Drawing.Size(95, 27);
            this.textAxisDemand.TabIndex = 8;
            this.textAxisDemand.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 118);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 16);
            this.label10.TabIndex = 7;
            this.label10.Text = "Feedback Pos.";
            // 
            // textAxisFeedback
            // 
            this.textAxisFeedback.Location = new System.Drawing.Point(125, 115);
            this.textAxisFeedback.Name = "textAxisFeedback";
            this.textAxisFeedback.Size = new System.Drawing.Size(95, 27);
            this.textAxisFeedback.TabIndex = 6;
            this.textAxisFeedback.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label18);
            this.groupBox6.Controls.Add(this.radioSfcConfition);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.textMpgSfcRate);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Controls.Add(this.textMpgSelected);
            this.groupBox6.Location = new System.Drawing.Point(222, 200);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(304, 120);
            this.groupBox6.TabIndex = 10;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "MPG SFC";
            // 
            // radioSfcConfition
            // 
            this.radioSfcConfition.AutoSize = true;
            this.radioSfcConfition.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioSfcConfition.Location = new System.Drawing.Point(9, 23);
            this.radioSfcConfition.Name = "radioSfcConfition";
            this.radioSfcConfition.Size = new System.Drawing.Size(45, 20);
            this.radioSfcConfition.TabIndex = 9;
            this.radioSfcConfition.TabStop = true;
            this.radioSfcConfition.Text = "Ok";
            this.radioSfcConfition.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 16);
            this.label8.TabIndex = 8;
            this.label8.Text = "Rate";
            // 
            // textMpgSfcRate
            // 
            this.textMpgSfcRate.Location = new System.Drawing.Point(125, 76);
            this.textMpgSfcRate.Name = "textMpgSfcRate";
            this.textMpgSfcRate.Size = new System.Drawing.Size(95, 27);
            this.textMpgSfcRate.TabIndex = 7;
            this.textMpgSfcRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 52);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 16);
            this.label13.TabIndex = 5;
            this.label13.Text = "Axis";
            // 
            // textMpgSelected
            // 
            this.textMpgSelected.Location = new System.Drawing.Point(125, 49);
            this.textMpgSelected.Name = "textMpgSelected";
            this.textMpgSelected.Size = new System.Drawing.Size(95, 27);
            this.textMpgSelected.TabIndex = 4;
            this.textMpgSelected.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(226, 88);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(32, 16);
            this.label14.TabIndex = 12;
            this.label14.Text = "mm";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(226, 118);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(32, 16);
            this.label15.TabIndex = 13;
            this.label15.Text = "mm";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(226, 148);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(32, 16);
            this.label16.TabIndex = 14;
            this.label16.Text = "mm";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(226, 58);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(32, 16);
            this.label17.TabIndex = 15;
            this.label17.Text = "mm";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(226, 79);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 16);
            this.label18.TabIndex = 15;
            this.label18.Text = "um/pulse";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.radioCoordinator);
            this.groupBox7.Controls.Add(this.buttonWebSocket);
            this.groupBox7.Controls.Add(this.label2);
            this.groupBox7.Controls.Add(this.textErrorCount);
            this.groupBox7.Location = new System.Drawing.Point(532, 12);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(200, 129);
            this.groupBox7.TabIndex = 11;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Misc";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 354);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupEtherCAT);
            this.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupEtherCAT.ResumeLayout(false);
            this.groupEtherCAT.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonWebSocket;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timerSlow;
        private System.Windows.Forms.GroupBox groupEtherCAT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textSlavesLen;
        private System.Windows.Forms.TextBox textErrorCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textMPGEncoder;
        private System.Windows.Forms.RadioButton radioMPGOn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textMPGRate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textDriveTargetPosition;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textDriveRealPosition;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonServoOn;
        private System.Windows.Forms.RadioButton radioDriveFault;
        private System.Windows.Forms.RadioButton radioDriveOn;
        private System.Windows.Forms.Button buttonDriveResetFault;
        private System.Windows.Forms.Button buttonDriveStop;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textDriveMode;
        private System.Windows.Forms.RadioButton radioCoordinator;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioAxisReached;
        private System.Windows.Forms.TextBox textAxisFollowingError;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textAxisCommand;
        private System.Windows.Forms.RadioButton radioAxisInterpolator;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textAxisDemand;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textAxisFeedback;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textMpgSfcRate;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textMpgSelected;
        private System.Windows.Forms.RadioButton radioSfcConfition;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBox7;
    }
}


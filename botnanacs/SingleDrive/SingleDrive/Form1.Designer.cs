namespace SingleDrive
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
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textP1 = new System.Windows.Forms.TextBox();
            this.textP2 = new System.Windows.Forms.TextBox();
            this.buttonPMAbort = new System.Windows.Forms.Button();
            this.textP3 = new System.Windows.Forms.TextBox();
            this.buttonRun = new System.Windows.Forms.Button();
            this.buttonDeploy = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textEvalute = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonEvaluate = new System.Windows.Forms.Button();
            this.textSlaveCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.buttonServoOn = new System.Windows.Forms.Button();
            this.buttonServoOff = new System.Windows.Forms.Button();
            this.buttonResetFault = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioServoStop = new System.Windows.Forms.RadioButton();
            this.textOPMode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textTargetPosition = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textProfileAcceleration = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textProfileVelocity = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textNextTargetPosition = new System.Windows.Forms.TextBox();
            this.buttonSetTargetPosition = new System.Windows.Forms.Button();
            this.buttonGo = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textRealPosition = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.radioFault = new System.Windows.Forms.RadioButton();
            this.radioTargetReached = new System.Windows.Forms.RadioButton();
            this.radioServoOn = new System.Windows.Forms.RadioButton();
            this.labCount = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioPosLimit = new System.Windows.Forms.RadioButton();
            this.radioNegLimit = new System.Windows.Forms.RadioButton();
            this.radioOrg = new System.Windows.Forms.RadioButton();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.Location = new System.Drawing.Point(29, 81);
            this.button1.Margin = new System.Windows.Forms.Padding(5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 41);
            this.button1.TabIndex = 41;
            this.button1.Text = "Drive Stop";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 133);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 16);
            this.label8.TabIndex = 57;
            this.label8.Text = "P3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 16);
            this.label4.TabIndex = 56;
            this.label4.Text = "P2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 16);
            this.label3.TabIndex = 55;
            this.label3.Text = "P1";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.textP1);
            this.groupBox5.Controls.Add(this.textP2);
            this.groupBox5.Controls.Add(this.buttonPMAbort);
            this.groupBox5.Controls.Add(this.textP3);
            this.groupBox5.Controls.Add(this.buttonRun);
            this.groupBox5.Controls.Add(this.buttonDeploy);
            this.groupBox5.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox5.Location = new System.Drawing.Point(499, 284);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(426, 175);
            this.groupBox5.TabIndex = 81;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Real Time Progam P2P";
            this.groupBox5.Enter += new System.EventHandler(this.groupBox5_Enter);
            // 
            // textP1
            // 
            this.textP1.Location = new System.Drawing.Point(52, 30);
            this.textP1.Margin = new System.Windows.Forms.Padding(5);
            this.textP1.Name = "textP1";
            this.textP1.Size = new System.Drawing.Size(110, 27);
            this.textP1.TabIndex = 52;
            this.textP1.Text = "0";
            // 
            // textP2
            // 
            this.textP2.Location = new System.Drawing.Point(52, 79);
            this.textP2.Margin = new System.Windows.Forms.Padding(5);
            this.textP2.Name = "textP2";
            this.textP2.Size = new System.Drawing.Size(110, 27);
            this.textP2.TabIndex = 53;
            this.textP2.Text = "1000";
            // 
            // buttonPMAbort
            // 
            this.buttonPMAbort.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonPMAbort.Location = new System.Drawing.Point(223, 122);
            this.buttonPMAbort.Margin = new System.Windows.Forms.Padding(5);
            this.buttonPMAbort.Name = "buttonPMAbort";
            this.buttonPMAbort.Size = new System.Drawing.Size(148, 42);
            this.buttonPMAbort.TabIndex = 57;
            this.buttonPMAbort.Text = "Abort";
            this.buttonPMAbort.UseVisualStyleBackColor = true;
            this.buttonPMAbort.Click += new System.EventHandler(this.buttonPMAbort_Click);
            // 
            // textP3
            // 
            this.textP3.Location = new System.Drawing.Point(52, 124);
            this.textP3.Margin = new System.Windows.Forms.Padding(5);
            this.textP3.Name = "textP3";
            this.textP3.Size = new System.Drawing.Size(110, 27);
            this.textP3.TabIndex = 54;
            this.textP3.Text = "5000";
            // 
            // buttonRun
            // 
            this.buttonRun.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonRun.Location = new System.Drawing.Point(223, 77);
            this.buttonRun.Margin = new System.Windows.Forms.Padding(5);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(148, 41);
            this.buttonRun.TabIndex = 56;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // buttonDeploy
            // 
            this.buttonDeploy.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonDeploy.Location = new System.Drawing.Point(223, 30);
            this.buttonDeploy.Margin = new System.Windows.Forms.Padding(5);
            this.buttonDeploy.Name = "buttonDeploy";
            this.buttonDeploy.Size = new System.Drawing.Size(148, 37);
            this.buttonDeploy.TabIndex = 55;
            this.buttonDeploy.Text = "Deploy";
            this.buttonDeploy.UseVisualStyleBackColor = true;
            this.buttonDeploy.Click += new System.EventHandler(this.buttonDeploy_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(23, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 16);
            this.label2.TabIndex = 48;
            this.label2.Text = "Next Target Position";
            // 
            // textEvalute
            // 
            this.textEvalute.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textEvalute.Location = new System.Drawing.Point(8, 30);
            this.textEvalute.Margin = new System.Windows.Forms.Padding(5);
            this.textEvalute.Name = "textEvalute";
            this.textEvalute.Size = new System.Drawing.Size(294, 27);
            this.textEvalute.TabIndex = 43;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textEvalute);
            this.groupBox3.Controls.Add(this.buttonEvaluate);
            this.groupBox3.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox3.Location = new System.Drawing.Point(497, 475);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(428, 85);
            this.groupBox3.TabIndex = 79;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Motion Evaluate";
            // 
            // buttonEvaluate
            // 
            this.buttonEvaluate.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonEvaluate.Location = new System.Drawing.Point(312, 30);
            this.buttonEvaluate.Margin = new System.Windows.Forms.Padding(5);
            this.buttonEvaluate.Name = "buttonEvaluate";
            this.buttonEvaluate.Size = new System.Drawing.Size(89, 27);
            this.buttonEvaluate.TabIndex = 44;
            this.buttonEvaluate.Text = "Send";
            this.buttonEvaluate.UseVisualStyleBackColor = true;
            this.buttonEvaluate.Click += new System.EventHandler(this.buttonEvaluate_Click);
            // 
            // textSlaveCount
            // 
            this.textSlaveCount.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textSlaveCount.Location = new System.Drawing.Point(158, 30);
            this.textSlaveCount.Margin = new System.Windows.Forms.Padding(5);
            this.textSlaveCount.Name = "textSlaveCount";
            this.textSlaveCount.Size = new System.Drawing.Size(55, 27);
            this.textSlaveCount.TabIndex = 71;
            this.textSlaveCount.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(20, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 70;
            this.label1.Text = "Slave Count";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.button1);
            this.groupBox6.Controls.Add(this.buttonServoOn);
            this.groupBox6.Controls.Add(this.buttonServoOff);
            this.groupBox6.Controls.Add(this.buttonResetFault);
            this.groupBox6.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox6.Location = new System.Drawing.Point(40, 425);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(426, 142);
            this.groupBox6.TabIndex = 82;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Drive Control";
            // 
            // buttonServoOn
            // 
            this.buttonServoOn.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonServoOn.Location = new System.Drawing.Point(28, 30);
            this.buttonServoOn.Margin = new System.Windows.Forms.Padding(5);
            this.buttonServoOn.Name = "buttonServoOn";
            this.buttonServoOn.Size = new System.Drawing.Size(108, 41);
            this.buttonServoOn.TabIndex = 38;
            this.buttonServoOn.Text = "Drive ON";
            this.buttonServoOn.UseVisualStyleBackColor = true;
            this.buttonServoOn.Click += new System.EventHandler(this.buttonServoOn_Click);
            // 
            // buttonServoOff
            // 
            this.buttonServoOff.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonServoOff.Location = new System.Drawing.Point(193, 79);
            this.buttonServoOff.Margin = new System.Windows.Forms.Padding(5);
            this.buttonServoOff.Name = "buttonServoOff";
            this.buttonServoOff.Size = new System.Drawing.Size(107, 41);
            this.buttonServoOff.TabIndex = 39;
            this.buttonServoOff.Text = "Drive OFF";
            this.buttonServoOff.UseVisualStyleBackColor = true;
            this.buttonServoOff.Click += new System.EventHandler(this.buttonServoOff_Click);
            // 
            // buttonResetFault
            // 
            this.buttonResetFault.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonResetFault.Location = new System.Drawing.Point(193, 30);
            this.buttonResetFault.Margin = new System.Windows.Forms.Padding(5);
            this.buttonResetFault.Name = "buttonResetFault";
            this.buttonResetFault.Size = new System.Drawing.Size(109, 43);
            this.buttonResetFault.TabIndex = 40;
            this.buttonResetFault.Text = "Reset Fault";
            this.buttonResetFault.UseVisualStyleBackColor = true;
            this.buttonResetFault.Click += new System.EventHandler(this.buttonResetFault_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textSlaveCount);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox2.Location = new System.Drawing.Point(40, 36);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(426, 77);
            this.groupBox2.TabIndex = 78;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "EtherCAT Status";
            // 
            // radioServoStop
            // 
            this.radioServoStop.AutoCheck = false;
            this.radioServoStop.AutoSize = true;
            this.radioServoStop.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioServoStop.Location = new System.Drawing.Point(178, 30);
            this.radioServoStop.Margin = new System.Windows.Forms.Padding(5);
            this.radioServoStop.Name = "radioServoStop";
            this.radioServoStop.Size = new System.Drawing.Size(93, 20);
            this.radioServoStop.TabIndex = 68;
            this.radioServoStop.TabStop = true;
            this.radioServoStop.Text = "Drive Stop";
            this.radioServoStop.UseVisualStyleBackColor = true;
            this.radioServoStop.CheckedChanged += new System.EventHandler(this.radioServoStop_CheckedChanged);
            // 
            // textOPMode
            // 
            this.textOPMode.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textOPMode.Location = new System.Drawing.Point(158, 157);
            this.textOPMode.Margin = new System.Windows.Forms.Padding(5);
            this.textOPMode.Name = "textOPMode";
            this.textOPMode.Size = new System.Drawing.Size(55, 27);
            this.textOPMode.TabIndex = 67;
            this.textOPMode.Text = "None";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(29, 160);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 16);
            this.label5.TabIndex = 66;
            this.label5.Text = "OP Mode";
            // 
            // textTargetPosition
            // 
            this.textTargetPosition.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textTargetPosition.Location = new System.Drawing.Point(158, 228);
            this.textTargetPosition.Margin = new System.Windows.Forms.Padding(5);
            this.textTargetPosition.Name = "textTargetPosition";
            this.textTargetPosition.Size = new System.Drawing.Size(120, 27);
            this.textTargetPosition.TabIndex = 65;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textProfileAcceleration);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.textProfileVelocity);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.textNextTargetPosition);
            this.groupBox4.Controls.Add(this.buttonSetTargetPosition);
            this.groupBox4.Controls.Add(this.buttonGo);
            this.groupBox4.Location = new System.Drawing.Point(497, 36);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(428, 193);
            this.groupBox4.TabIndex = 80;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Drive P2P";
            // 
            // textProfileAcceleration
            // 
            this.textProfileAcceleration.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textProfileAcceleration.Location = new System.Drawing.Point(167, 151);
            this.textProfileAcceleration.Margin = new System.Windows.Forms.Padding(5);
            this.textProfileAcceleration.Name = "textProfileAcceleration";
            this.textProfileAcceleration.Size = new System.Drawing.Size(179, 27);
            this.textProfileAcceleration.TabIndex = 52;
            this.textProfileAcceleration.Text = "0";
            this.textProfileAcceleration.Enter += new System.EventHandler(this.textProfileAcceleration_Enter);
            this.textProfileAcceleration.Leave += new System.EventHandler(this.textProfileAcceleration_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label10.Location = new System.Drawing.Point(23, 160);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(132, 16);
            this.label10.TabIndex = 51;
            this.label10.Text = "Profile Acceleration";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // textProfileVelocity
            // 
            this.textProfileVelocity.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textProfileVelocity.Location = new System.Drawing.Point(167, 114);
            this.textProfileVelocity.Margin = new System.Windows.Forms.Padding(5);
            this.textProfileVelocity.Name = "textProfileVelocity";
            this.textProfileVelocity.Size = new System.Drawing.Size(179, 27);
            this.textProfileVelocity.TabIndex = 50;
            this.textProfileVelocity.Text = "0";
            this.textProfileVelocity.Enter += new System.EventHandler(this.textProfileVelocity_Enter);
            this.textProfileVelocity.Leave += new System.EventHandler(this.textProfileVelocity_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.Location = new System.Drawing.Point(23, 123);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 16);
            this.label9.TabIndex = 49;
            this.label9.Text = "Profile Velocity";
            // 
            // textNextTargetPosition
            // 
            this.textNextTargetPosition.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textNextTargetPosition.Location = new System.Drawing.Point(194, 30);
            this.textNextTargetPosition.Margin = new System.Windows.Forms.Padding(5);
            this.textNextTargetPosition.Name = "textNextTargetPosition";
            this.textNextTargetPosition.Size = new System.Drawing.Size(179, 27);
            this.textNextTargetPosition.TabIndex = 45;
            this.textNextTargetPosition.Text = "0";
            // 
            // buttonSetTargetPosition
            // 
            this.buttonSetTargetPosition.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonSetTargetPosition.Location = new System.Drawing.Point(151, 74);
            this.buttonSetTargetPosition.Margin = new System.Windows.Forms.Padding(5);
            this.buttonSetTargetPosition.Name = "buttonSetTargetPosition";
            this.buttonSetTargetPosition.Size = new System.Drawing.Size(109, 30);
            this.buttonSetTargetPosition.TabIndex = 46;
            this.buttonSetTargetPosition.Text = "Set";
            this.buttonSetTargetPosition.UseVisualStyleBackColor = true;
            this.buttonSetTargetPosition.Click += new System.EventHandler(this.buttonSetTargetPosition_Click);
            // 
            // buttonGo
            // 
            this.buttonGo.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonGo.Location = new System.Drawing.Point(270, 74);
            this.buttonGo.Margin = new System.Windows.Forms.Padding(5);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(107, 30);
            this.buttonGo.TabIndex = 47;
            this.buttonGo.Text = "Go";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(30, 235);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 16);
            this.label6.TabIndex = 64;
            this.label6.Text = "Target Position";
            // 
            // textRealPosition
            // 
            this.textRealPosition.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textRealPosition.Location = new System.Drawing.Point(158, 191);
            this.textRealPosition.Margin = new System.Windows.Forms.Padding(5);
            this.textRealPosition.Name = "textRealPosition";
            this.textRealPosition.Size = new System.Drawing.Size(120, 27);
            this.textRealPosition.TabIndex = 63;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(30, 194);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 16);
            this.label7.TabIndex = 62;
            this.label7.Text = "Real Position";
            // 
            // radioFault
            // 
            this.radioFault.AutoCheck = false;
            this.radioFault.AutoSize = true;
            this.radioFault.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioFault.Location = new System.Drawing.Point(312, 30);
            this.radioFault.Margin = new System.Windows.Forms.Padding(5);
            this.radioFault.Name = "radioFault";
            this.radioFault.Size = new System.Drawing.Size(57, 20);
            this.radioFault.TabIndex = 61;
            this.radioFault.TabStop = true;
            this.radioFault.Text = "Fault";
            this.radioFault.UseVisualStyleBackColor = true;
            // 
            // radioTargetReached
            // 
            this.radioTargetReached.AutoCheck = false;
            this.radioTargetReached.AutoSize = true;
            this.radioTargetReached.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioTargetReached.Location = new System.Drawing.Point(32, 103);
            this.radioTargetReached.Margin = new System.Windows.Forms.Padding(5);
            this.radioTargetReached.Name = "radioTargetReached";
            this.radioTargetReached.Size = new System.Drawing.Size(120, 20);
            this.radioTargetReached.TabIndex = 60;
            this.radioTargetReached.TabStop = true;
            this.radioTargetReached.Text = "TargetReached";
            this.radioTargetReached.UseVisualStyleBackColor = true;
            // 
            // radioServoOn
            // 
            this.radioServoOn.AutoCheck = false;
            this.radioServoOn.AutoSize = true;
            this.radioServoOn.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioServoOn.Location = new System.Drawing.Point(32, 30);
            this.radioServoOn.Margin = new System.Windows.Forms.Padding(5);
            this.radioServoOn.Name = "radioServoOn";
            this.radioServoOn.Size = new System.Drawing.Size(87, 20);
            this.radioServoOn.TabIndex = 59;
            this.radioServoOn.TabStop = true;
            this.radioServoOn.Text = "Drive ON";
            this.radioServoOn.UseVisualStyleBackColor = true;
            // 
            // labCount
            // 
            this.labCount.AutoSize = true;
            this.labCount.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labCount.Location = new System.Drawing.Point(947, 544);
            this.labCount.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labCount.Name = "labCount";
            this.labCount.Size = new System.Drawing.Size(16, 16);
            this.labCount.TabIndex = 76;
            this.labCount.Text = "1";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioPosLimit);
            this.groupBox1.Controls.Add(this.radioNegLimit);
            this.groupBox1.Controls.Add(this.radioOrg);
            this.groupBox1.Controls.Add(this.radioServoStop);
            this.groupBox1.Controls.Add(this.textOPMode);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textTargetPosition);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textRealPosition);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.radioFault);
            this.groupBox1.Controls.Add(this.radioTargetReached);
            this.groupBox1.Controls.Add(this.radioServoOn);
            this.groupBox1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(40, 127);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(426, 292);
            this.groupBox1.TabIndex = 77;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Drive Status";
            // 
            // radioPosLimit
            // 
            this.radioPosLimit.AutoCheck = false;
            this.radioPosLimit.AutoSize = true;
            this.radioPosLimit.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioPosLimit.Location = new System.Drawing.Point(312, 65);
            this.radioPosLimit.Margin = new System.Windows.Forms.Padding(5);
            this.radioPosLimit.Name = "radioPosLimit";
            this.radioPosLimit.Size = new System.Drawing.Size(89, 20);
            this.radioPosLimit.TabIndex = 71;
            this.radioPosLimit.TabStop = true;
            this.radioPosLimit.Text = "Pos. Limit";
            this.radioPosLimit.UseVisualStyleBackColor = true;
            // 
            // radioNegLimit
            // 
            this.radioNegLimit.AutoCheck = false;
            this.radioNegLimit.AutoSize = true;
            this.radioNegLimit.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioNegLimit.Location = new System.Drawing.Point(178, 65);
            this.radioNegLimit.Margin = new System.Windows.Forms.Padding(5);
            this.radioNegLimit.Name = "radioNegLimit";
            this.radioNegLimit.Size = new System.Drawing.Size(93, 20);
            this.radioNegLimit.TabIndex = 70;
            this.radioNegLimit.TabStop = true;
            this.radioNegLimit.Text = "Neg. Limit";
            this.radioNegLimit.UseVisualStyleBackColor = true;
            // 
            // radioOrg
            // 
            this.radioOrg.AutoCheck = false;
            this.radioOrg.AutoSize = true;
            this.radioOrg.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.radioOrg.Location = new System.Drawing.Point(32, 65);
            this.radioOrg.Margin = new System.Windows.Forms.Padding(5);
            this.radioOrg.Name = "radioOrg";
            this.radioOrg.Size = new System.Drawing.Size(58, 20);
            this.radioOrg.TabIndex = 69;
            this.radioOrg.TabStop = true;
            this.radioOrg.Text = "ORG";
            this.radioOrg.UseVisualStyleBackColor = true;
            this.radioOrg.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1104, 694);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.labCount);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox textP1;
        private System.Windows.Forms.TextBox textP2;
        private System.Windows.Forms.Button buttonPMAbort;
        private System.Windows.Forms.TextBox textP3;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Button buttonDeploy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textEvalute;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonEvaluate;
        private System.Windows.Forms.TextBox textSlaveCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button buttonServoOn;
        private System.Windows.Forms.Button buttonServoOff;
        private System.Windows.Forms.Button buttonResetFault;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioServoStop;
        private System.Windows.Forms.TextBox textOPMode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textTargetPosition;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textNextTargetPosition;
        private System.Windows.Forms.Button buttonSetTargetPosition;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textRealPosition;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton radioFault;
        private System.Windows.Forms.RadioButton radioTargetReached;
        private System.Windows.Forms.RadioButton radioServoOn;
        private System.Windows.Forms.Label labCount;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioOrg;
        private System.Windows.Forms.RadioButton radioPosLimit;
        private System.Windows.Forms.RadioButton radioNegLimit;
        private System.Windows.Forms.TextBox textProfileVelocity;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textProfileAcceleration;
    }
}


namespace MoldFeeder
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerSlow = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonError = new System.Windows.Forms.Button();
            this.buttonEtherCAT = new System.Windows.Forms.Button();
            this.buttonWs = new System.Windows.Forms.Button();
            this.buttonHasSFC = new System.Windows.Forms.Button();
            this.buttonErrorAck = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioTP2DownEdgeAction = new System.Windows.Forms.RadioButton();
            this.radioTP1DownEdgeAction = new System.Windows.Forms.RadioButton();
            this.radioTP2UpEdgeAction = new System.Windows.Forms.RadioButton();
            this.radioTP1UpEdgeAction = new System.Windows.Forms.RadioButton();
            this.radioTP2TriggerSelection = new System.Windows.Forms.RadioButton();
            this.radioTP1TriggerSelection = new System.Windows.Forms.RadioButton();
            this.radioTP2TriggerAction = new System.Windows.Forms.RadioButton();
            this.radioTP1TriggerAction = new System.Windows.Forms.RadioButton();
            this.radioEnableTP2 = new System.Windows.Forms.RadioButton();
            this.radioEnableTP1 = new System.Windows.Forms.RadioButton();
            this.radioDriveDin3 = new System.Windows.Forms.RadioButton();
            this.radioDriveDin2 = new System.Windows.Forms.RadioButton();
            this.radioDriveDin1 = new System.Windows.Forms.RadioButton();
            this.radioDriveDin0 = new System.Windows.Forms.RadioButton();
            this.textTP1LatchPosition = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textTP2LatchPosition = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.radioTP2Enabled = new System.Windows.Forms.RadioButton();
            this.radioDriveExt2 = new System.Windows.Forms.RadioButton();
            this.radioTP1Enabled = new System.Windows.Forms.RadioButton();
            this.radioDriveExt1 = new System.Windows.Forms.RadioButton();
            this.radioDriveFault = new System.Windows.Forms.RadioButton();
            this.radioDriveOn = new System.Windows.Forms.RadioButton();
            this.textTargetPosition = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textEncoderPosition = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textOperationMode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timerPoll = new System.Windows.Forms.Timer(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textJogSpeed = new System.Windows.Forms.TextBox();
            this.buttonJogNegative = new System.Windows.Forms.Button();
            this.buttonJogPositive = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.buttonCylinderOff = new System.Windows.Forms.Button();
            this.buttonCylinderOn = new System.Windows.Forms.Button();
            this.radioCylinder = new System.Windows.Forms.RadioButton();
            this.groupSystem = new System.Windows.Forms.GroupBox();
            this.buttonEvaluate = new System.Windows.Forms.Button();
            this.textEvaluate = new System.Windows.Forms.TextBox();
            this.groupSFC = new System.Windows.Forms.GroupBox();
            this.label23 = new System.Windows.Forms.Label();
            this.textBoxRetryCountMax = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.textBoxSettlingDurationMs = new System.Windows.Forms.TextBox();
            this.buttonSdoRequest = new System.Windows.Forms.Button();
            this.buttonReleaseFeeder = new System.Windows.Forms.Button();
            this.buttonFeederEMS = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textRotationSpeed = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.textRotationDistance = new System.Windows.Forms.TextBox();
            this.buttonStartFeeder = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textCylinderOffMs = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textCylinderOnMs = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.textTP2DetectedPosition = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.textTP1DetectedPosition = new System.Windows.Forms.TextBox();
            this.radioTP2Detected = new System.Windows.Forms.RadioButton();
            this.radioTP1Detected = new System.Windows.Forms.RadioButton();
            this.radioFeederEMS = new System.Windows.Forms.RadioButton();
            this.radioFeederRunning = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textFeederOperationTime = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.timer1s = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupSystem.SuspendLayout();
            this.groupSFC.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerSlow
            // 
            this.timerSlow.Interval = 500;
            this.timerSlow.Tick += new System.EventHandler(this.timerSlow_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonError);
            this.groupBox1.Controls.Add(this.buttonEtherCAT);
            this.groupBox1.Controls.Add(this.buttonWs);
            this.groupBox1.Controls.Add(this.buttonHasSFC);
            this.groupBox1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(481, 57);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "System Status";
            // 
            // buttonError
            // 
            this.buttonError.Location = new System.Drawing.Point(371, 21);
            this.buttonError.Name = "buttonError";
            this.buttonError.Size = new System.Drawing.Size(91, 30);
            this.buttonError.TabIndex = 3;
            this.buttonError.Text = "Error";
            this.buttonError.UseVisualStyleBackColor = true;
            // 
            // buttonEtherCAT
            // 
            this.buttonEtherCAT.Location = new System.Drawing.Point(133, 21);
            this.buttonEtherCAT.Name = "buttonEtherCAT";
            this.buttonEtherCAT.Size = new System.Drawing.Size(91, 30);
            this.buttonEtherCAT.TabIndex = 2;
            this.buttonEtherCAT.Text = "EtherCAT";
            this.buttonEtherCAT.UseVisualStyleBackColor = true;
            // 
            // buttonWs
            // 
            this.buttonWs.Location = new System.Drawing.Point(14, 21);
            this.buttonWs.Name = "buttonWs";
            this.buttonWs.Size = new System.Drawing.Size(91, 30);
            this.buttonWs.TabIndex = 0;
            this.buttonWs.Text = "WebSocket";
            this.buttonWs.UseVisualStyleBackColor = true;
            // 
            // buttonHasSFC
            // 
            this.buttonHasSFC.Location = new System.Drawing.Point(252, 21);
            this.buttonHasSFC.Name = "buttonHasSFC";
            this.buttonHasSFC.Size = new System.Drawing.Size(91, 30);
            this.buttonHasSFC.TabIndex = 1;
            this.buttonHasSFC.Text = "SFC";
            this.buttonHasSFC.UseVisualStyleBackColor = true;
            // 
            // buttonErrorAck
            // 
            this.buttonErrorAck.Location = new System.Drawing.Point(14, 37);
            this.buttonErrorAck.Name = "buttonErrorAck";
            this.buttonErrorAck.Size = new System.Drawing.Size(91, 30);
            this.buttonErrorAck.TabIndex = 0;
            this.buttonErrorAck.Text = "ErrorAck";
            this.buttonErrorAck.UseVisualStyleBackColor = true;
            this.buttonErrorAck.Click += new System.EventHandler(this.buttonErrorAck_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioTP2DownEdgeAction);
            this.groupBox3.Controls.Add(this.radioTP1DownEdgeAction);
            this.groupBox3.Controls.Add(this.radioTP2UpEdgeAction);
            this.groupBox3.Controls.Add(this.radioTP1UpEdgeAction);
            this.groupBox3.Controls.Add(this.radioTP2TriggerSelection);
            this.groupBox3.Controls.Add(this.radioTP1TriggerSelection);
            this.groupBox3.Controls.Add(this.radioTP2TriggerAction);
            this.groupBox3.Controls.Add(this.radioTP1TriggerAction);
            this.groupBox3.Controls.Add(this.radioEnableTP2);
            this.groupBox3.Controls.Add(this.radioEnableTP1);
            this.groupBox3.Controls.Add(this.radioDriveDin3);
            this.groupBox3.Controls.Add(this.radioDriveDin2);
            this.groupBox3.Controls.Add(this.radioDriveDin1);
            this.groupBox3.Controls.Add(this.radioDriveDin0);
            this.groupBox3.Controls.Add(this.textTP1LatchPosition);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.textTP2LatchPosition);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.radioTP2Enabled);
            this.groupBox3.Controls.Add(this.radioDriveExt2);
            this.groupBox3.Controls.Add(this.radioTP1Enabled);
            this.groupBox3.Controls.Add(this.radioDriveExt1);
            this.groupBox3.Controls.Add(this.radioDriveFault);
            this.groupBox3.Controls.Add(this.radioDriveOn);
            this.groupBox3.Controls.Add(this.textTargetPosition);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.textEncoderPosition);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.textOperationMode);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox3.Location = new System.Drawing.Point(12, 79);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(481, 411);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Drive Status";
            // 
            // radioTP2DownEdgeAction
            // 
            this.radioTP2DownEdgeAction.AutoCheck = false;
            this.radioTP2DownEdgeAction.AutoSize = true;
            this.radioTP2DownEdgeAction.Location = new System.Drawing.Point(282, 197);
            this.radioTP2DownEdgeAction.Name = "radioTP2DownEdgeAction";
            this.radioTP2DownEdgeAction.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioTP2DownEdgeAction.Size = new System.Drawing.Size(179, 20);
            this.radioTP2DownEdgeAction.TabIndex = 33;
            this.radioTP2DownEdgeAction.TabStop = true;
            this.radioTP2DownEdgeAction.Text = "TP 2 Down Edge Action";
            this.radioTP2DownEdgeAction.UseVisualStyleBackColor = true;
            // 
            // radioTP1DownEdgeAction
            // 
            this.radioTP1DownEdgeAction.AutoCheck = false;
            this.radioTP1DownEdgeAction.AutoSize = true;
            this.radioTP1DownEdgeAction.Location = new System.Drawing.Point(44, 195);
            this.radioTP1DownEdgeAction.Name = "radioTP1DownEdgeAction";
            this.radioTP1DownEdgeAction.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioTP1DownEdgeAction.Size = new System.Drawing.Size(179, 20);
            this.radioTP1DownEdgeAction.TabIndex = 32;
            this.radioTP1DownEdgeAction.TabStop = true;
            this.radioTP1DownEdgeAction.Text = "TP 1 Down Edge Action";
            this.radioTP1DownEdgeAction.UseVisualStyleBackColor = true;
            // 
            // radioTP2UpEdgeAction
            // 
            this.radioTP2UpEdgeAction.AutoCheck = false;
            this.radioTP2UpEdgeAction.AutoSize = true;
            this.radioTP2UpEdgeAction.Location = new System.Drawing.Point(301, 170);
            this.radioTP2UpEdgeAction.Name = "radioTP2UpEdgeAction";
            this.radioTP2UpEdgeAction.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioTP2UpEdgeAction.Size = new System.Drawing.Size(160, 20);
            this.radioTP2UpEdgeAction.TabIndex = 31;
            this.radioTP2UpEdgeAction.TabStop = true;
            this.radioTP2UpEdgeAction.Text = "TP 2 Up Edge Action";
            this.radioTP2UpEdgeAction.UseVisualStyleBackColor = true;
            // 
            // radioTP1UpEdgeAction
            // 
            this.radioTP1UpEdgeAction.AutoCheck = false;
            this.radioTP1UpEdgeAction.AutoSize = true;
            this.radioTP1UpEdgeAction.Location = new System.Drawing.Point(63, 168);
            this.radioTP1UpEdgeAction.Name = "radioTP1UpEdgeAction";
            this.radioTP1UpEdgeAction.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioTP1UpEdgeAction.Size = new System.Drawing.Size(160, 20);
            this.radioTP1UpEdgeAction.TabIndex = 30;
            this.radioTP1UpEdgeAction.TabStop = true;
            this.radioTP1UpEdgeAction.Text = "TP 1 Up Edge Action";
            this.radioTP1UpEdgeAction.UseVisualStyleBackColor = true;
            // 
            // radioTP2TriggerSelection
            // 
            this.radioTP2TriggerSelection.AutoCheck = false;
            this.radioTP2TriggerSelection.AutoSize = true;
            this.radioTP2TriggerSelection.Location = new System.Drawing.Point(295, 143);
            this.radioTP2TriggerSelection.Name = "radioTP2TriggerSelection";
            this.radioTP2TriggerSelection.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioTP2TriggerSelection.Size = new System.Drawing.Size(166, 20);
            this.radioTP2TriggerSelection.TabIndex = 29;
            this.radioTP2TriggerSelection.TabStop = true;
            this.radioTP2TriggerSelection.Text = "TP 2 Trigger Selection";
            this.radioTP2TriggerSelection.UseVisualStyleBackColor = true;
            // 
            // radioTP1TriggerSelection
            // 
            this.radioTP1TriggerSelection.AutoCheck = false;
            this.radioTP1TriggerSelection.AutoSize = true;
            this.radioTP1TriggerSelection.Location = new System.Drawing.Point(57, 141);
            this.radioTP1TriggerSelection.Name = "radioTP1TriggerSelection";
            this.radioTP1TriggerSelection.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioTP1TriggerSelection.Size = new System.Drawing.Size(166, 20);
            this.radioTP1TriggerSelection.TabIndex = 28;
            this.radioTP1TriggerSelection.TabStop = true;
            this.radioTP1TriggerSelection.Text = "TP 1 Trigger Selection";
            this.radioTP1TriggerSelection.UseVisualStyleBackColor = true;
            // 
            // radioTP2TriggerAction
            // 
            this.radioTP2TriggerAction.AutoCheck = false;
            this.radioTP2TriggerAction.AutoSize = true;
            this.radioTP2TriggerAction.Location = new System.Drawing.Point(310, 116);
            this.radioTP2TriggerAction.Name = "radioTP2TriggerAction";
            this.radioTP2TriggerAction.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioTP2TriggerAction.Size = new System.Drawing.Size(151, 20);
            this.radioTP2TriggerAction.TabIndex = 27;
            this.radioTP2TriggerAction.TabStop = true;
            this.radioTP2TriggerAction.Text = "TP 2 Trigger Action";
            this.radioTP2TriggerAction.UseVisualStyleBackColor = true;
            // 
            // radioTP1TriggerAction
            // 
            this.radioTP1TriggerAction.AutoCheck = false;
            this.radioTP1TriggerAction.AutoSize = true;
            this.radioTP1TriggerAction.Location = new System.Drawing.Point(72, 114);
            this.radioTP1TriggerAction.Name = "radioTP1TriggerAction";
            this.radioTP1TriggerAction.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioTP1TriggerAction.Size = new System.Drawing.Size(151, 20);
            this.radioTP1TriggerAction.TabIndex = 26;
            this.radioTP1TriggerAction.TabStop = true;
            this.radioTP1TriggerAction.Text = "TP 1 Trigger Action";
            this.radioTP1TriggerAction.UseVisualStyleBackColor = true;
            // 
            // radioEnableTP2
            // 
            this.radioEnableTP2.AutoCheck = false;
            this.radioEnableTP2.AutoSize = true;
            this.radioEnableTP2.Location = new System.Drawing.Point(359, 89);
            this.radioEnableTP2.Name = "radioEnableTP2";
            this.radioEnableTP2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioEnableTP2.Size = new System.Drawing.Size(102, 20);
            this.radioEnableTP2.TabIndex = 25;
            this.radioEnableTP2.TabStop = true;
            this.radioEnableTP2.Text = "Enable TP 2";
            this.radioEnableTP2.UseVisualStyleBackColor = true;
            // 
            // radioEnableTP1
            // 
            this.radioEnableTP1.AutoCheck = false;
            this.radioEnableTP1.AutoSize = true;
            this.radioEnableTP1.Location = new System.Drawing.Point(121, 87);
            this.radioEnableTP1.Name = "radioEnableTP1";
            this.radioEnableTP1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioEnableTP1.Size = new System.Drawing.Size(102, 20);
            this.radioEnableTP1.TabIndex = 24;
            this.radioEnableTP1.TabStop = true;
            this.radioEnableTP1.Text = "Enable TP 1";
            this.radioEnableTP1.UseVisualStyleBackColor = true;
            // 
            // radioDriveDin3
            // 
            this.radioDriveDin3.AutoCheck = false;
            this.radioDriveDin3.AutoSize = true;
            this.radioDriveDin3.Location = new System.Drawing.Point(240, 50);
            this.radioDriveDin3.Name = "radioDriveDin3";
            this.radioDriveDin3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioDriveDin3.Size = new System.Drawing.Size(61, 20);
            this.radioDriveDin3.TabIndex = 23;
            this.radioDriveDin3.TabStop = true;
            this.radioDriveDin3.Text = "DIN3";
            this.radioDriveDin3.UseVisualStyleBackColor = true;
            // 
            // radioDriveDin2
            // 
            this.radioDriveDin2.AutoCheck = false;
            this.radioDriveDin2.AutoSize = true;
            this.radioDriveDin2.Location = new System.Drawing.Point(162, 50);
            this.radioDriveDin2.Name = "radioDriveDin2";
            this.radioDriveDin2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioDriveDin2.Size = new System.Drawing.Size(61, 20);
            this.radioDriveDin2.TabIndex = 22;
            this.radioDriveDin2.TabStop = true;
            this.radioDriveDin2.Text = "DIN2";
            this.radioDriveDin2.UseVisualStyleBackColor = true;
            // 
            // radioDriveDin1
            // 
            this.radioDriveDin1.AutoCheck = false;
            this.radioDriveDin1.AutoSize = true;
            this.radioDriveDin1.Location = new System.Drawing.Point(84, 50);
            this.radioDriveDin1.Name = "radioDriveDin1";
            this.radioDriveDin1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioDriveDin1.Size = new System.Drawing.Size(61, 20);
            this.radioDriveDin1.TabIndex = 21;
            this.radioDriveDin1.TabStop = true;
            this.radioDriveDin1.Text = "DIN1";
            this.radioDriveDin1.UseVisualStyleBackColor = true;
            // 
            // radioDriveDin0
            // 
            this.radioDriveDin0.AutoCheck = false;
            this.radioDriveDin0.AutoSize = true;
            this.radioDriveDin0.Location = new System.Drawing.Point(6, 50);
            this.radioDriveDin0.Name = "radioDriveDin0";
            this.radioDriveDin0.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioDriveDin0.Size = new System.Drawing.Size(61, 20);
            this.radioDriveDin0.TabIndex = 20;
            this.radioDriveDin0.TabStop = true;
            this.radioDriveDin0.Text = "DIN0";
            this.radioDriveDin0.UseVisualStyleBackColor = true;
            // 
            // textTP1LatchPosition
            // 
            this.textTP1LatchPosition.Location = new System.Drawing.Point(134, 265);
            this.textTP1LatchPosition.Name = "textTP1LatchPosition";
            this.textTP1LatchPosition.ReadOnly = true;
            this.textTP1LatchPosition.Size = new System.Drawing.Size(89, 27);
            this.textTP1LatchPosition.TabIndex = 19;
            this.textTP1LatchPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 270);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 16);
            this.label5.TabIndex = 18;
            this.label5.Text = "TP1 Latch Position";
            // 
            // textTP2LatchPosition
            // 
            this.textTP2LatchPosition.Location = new System.Drawing.Point(372, 265);
            this.textTP2LatchPosition.Name = "textTP2LatchPosition";
            this.textTP2LatchPosition.ReadOnly = true;
            this.textTP2LatchPosition.Size = new System.Drawing.Size(89, 27);
            this.textTP2LatchPosition.TabIndex = 17;
            this.textTP2LatchPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(237, 270);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 16);
            this.label4.TabIndex = 16;
            this.label4.Text = "TP2 Latch Position";
            // 
            // radioTP2Enabled
            // 
            this.radioTP2Enabled.AutoCheck = false;
            this.radioTP2Enabled.AutoSize = true;
            this.radioTP2Enabled.Location = new System.Drawing.Point(351, 224);
            this.radioTP2Enabled.Name = "radioTP2Enabled";
            this.radioTP2Enabled.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioTP2Enabled.Size = new System.Drawing.Size(110, 20);
            this.radioTP2Enabled.TabIndex = 15;
            this.radioTP2Enabled.TabStop = true;
            this.radioTP2Enabled.Text = "TP 2 Enabled";
            this.radioTP2Enabled.UseVisualStyleBackColor = true;
            // 
            // radioDriveExt2
            // 
            this.radioDriveExt2.AutoCheck = false;
            this.radioDriveExt2.AutoSize = true;
            this.radioDriveExt2.Location = new System.Drawing.Point(398, 52);
            this.radioDriveExt2.Name = "radioDriveExt2";
            this.radioDriveExt2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioDriveExt2.Size = new System.Drawing.Size(63, 20);
            this.radioDriveExt2.TabIndex = 14;
            this.radioDriveExt2.TabStop = true;
            this.radioDriveExt2.Text = "EXT2";
            this.radioDriveExt2.UseVisualStyleBackColor = true;
            // 
            // radioTP1Enabled
            // 
            this.radioTP1Enabled.AutoCheck = false;
            this.radioTP1Enabled.AutoSize = true;
            this.radioTP1Enabled.Location = new System.Drawing.Point(113, 222);
            this.radioTP1Enabled.Name = "radioTP1Enabled";
            this.radioTP1Enabled.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioTP1Enabled.Size = new System.Drawing.Size(110, 20);
            this.radioTP1Enabled.TabIndex = 10;
            this.radioTP1Enabled.TabStop = true;
            this.radioTP1Enabled.Text = "TP 1 Enabled";
            this.radioTP1Enabled.UseVisualStyleBackColor = true;
            // 
            // radioDriveExt1
            // 
            this.radioDriveExt1.AutoCheck = false;
            this.radioDriveExt1.AutoSize = true;
            this.radioDriveExt1.Location = new System.Drawing.Point(318, 50);
            this.radioDriveExt1.Name = "radioDriveExt1";
            this.radioDriveExt1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioDriveExt1.Size = new System.Drawing.Size(63, 20);
            this.radioDriveExt1.TabIndex = 8;
            this.radioDriveExt1.TabStop = true;
            this.radioDriveExt1.Text = "EXT1";
            this.radioDriveExt1.UseVisualStyleBackColor = true;
            // 
            // radioDriveFault
            // 
            this.radioDriveFault.AutoCheck = false;
            this.radioDriveFault.AutoSize = true;
            this.radioDriveFault.Location = new System.Drawing.Point(86, 24);
            this.radioDriveFault.Name = "radioDriveFault";
            this.radioDriveFault.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioDriveFault.Size = new System.Drawing.Size(57, 20);
            this.radioDriveFault.TabIndex = 7;
            this.radioDriveFault.TabStop = true;
            this.radioDriveFault.Text = "Fault";
            this.radioDriveFault.UseVisualStyleBackColor = true;
            // 
            // radioDriveOn
            // 
            this.radioDriveOn.AutoCheck = false;
            this.radioDriveOn.AutoSize = true;
            this.radioDriveOn.Location = new System.Drawing.Point(22, 22);
            this.radioDriveOn.Name = "radioDriveOn";
            this.radioDriveOn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioDriveOn.Size = new System.Drawing.Size(45, 20);
            this.radioDriveOn.TabIndex = 6;
            this.radioDriveOn.TabStop = true;
            this.radioDriveOn.Text = "On";
            this.radioDriveOn.UseVisualStyleBackColor = true;
            // 
            // textTargetPosition
            // 
            this.textTargetPosition.Location = new System.Drawing.Point(237, 368);
            this.textTargetPosition.Name = "textTargetPosition";
            this.textTargetPosition.ReadOnly = true;
            this.textTargetPosition.Size = new System.Drawing.Size(100, 27);
            this.textTargetPosition.TabIndex = 5;
            this.textTargetPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(121, 373);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Target Position";
            // 
            // textEncoderPosition
            // 
            this.textEncoderPosition.Location = new System.Drawing.Point(237, 339);
            this.textEncoderPosition.Name = "textEncoderPosition";
            this.textEncoderPosition.ReadOnly = true;
            this.textEncoderPosition.Size = new System.Drawing.Size(100, 27);
            this.textEncoderPosition.TabIndex = 3;
            this.textEncoderPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(109, 344);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Encoder Position";
            // 
            // textOperationMode
            // 
            this.textOperationMode.Location = new System.Drawing.Point(237, 310);
            this.textOperationMode.Name = "textOperationMode";
            this.textOperationMode.ReadOnly = true;
            this.textOperationMode.Size = new System.Drawing.Size(100, 27);
            this.textOperationMode.TabIndex = 1;
            this.textOperationMode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(113, 315);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Operation Mode";
            // 
            // timerPoll
            // 
            this.timerPoll.Interval = 50;
            this.timerPoll.Tick += new System.EventHandler(this.timerPoll_Tick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.textJogSpeed);
            this.groupBox4.Controls.Add(this.buttonJogNegative);
            this.groupBox4.Controls.Add(this.buttonJogPositive);
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Controls.Add(this.button2);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox4.Location = new System.Drawing.Point(499, 157);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(314, 179);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Drive Control";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(224, 136);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 16);
            this.label7.TabIndex = 9;
            this.label7.Text = "pulse/s";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(38, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 8;
            this.label6.Text = "Jog Speed";
            // 
            // textJogSpeed
            // 
            this.textJogSpeed.Location = new System.Drawing.Point(116, 131);
            this.textJogSpeed.Name = "textJogSpeed";
            this.textJogSpeed.Size = new System.Drawing.Size(91, 27);
            this.textJogSpeed.TabIndex = 7;
            this.textJogSpeed.Text = "10000";
            this.textJogSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // buttonJogNegative
            // 
            this.buttonJogNegative.Location = new System.Drawing.Point(116, 79);
            this.buttonJogNegative.Name = "buttonJogNegative";
            this.buttonJogNegative.Size = new System.Drawing.Size(91, 30);
            this.buttonJogNegative.TabIndex = 6;
            this.buttonJogNegative.Text = "JOG-";
            this.buttonJogNegative.UseVisualStyleBackColor = true;
            this.buttonJogNegative.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonJogNegative_MouseDown);
            this.buttonJogNegative.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonJogNegative_MouseUp);
            // 
            // buttonJogPositive
            // 
            this.buttonJogPositive.Location = new System.Drawing.Point(19, 79);
            this.buttonJogPositive.Name = "buttonJogPositive";
            this.buttonJogPositive.Size = new System.Drawing.Size(91, 30);
            this.buttonJogPositive.TabIndex = 5;
            this.buttonJogPositive.Text = "JOG+";
            this.buttonJogPositive.UseVisualStyleBackColor = true;
            this.buttonJogPositive.Click += new System.EventHandler(this.buttonJogPositive_Click);
            this.buttonJogPositive.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonJogPositive_MouseDown);
            this.buttonJogPositive.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonJogPositive_MouseUp);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(213, 30);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(91, 30);
            this.button3.TabIndex = 2;
            this.button3.Text = "Drive Reset";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(116, 29);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 30);
            this.button2.TabIndex = 1;
            this.button2.Text = "Drive Off";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(19, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "Drive On";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button5.Location = new System.Drawing.Point(116, 37);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(91, 30);
            this.button5.TabIndex = 4;
            this.button5.Text = "Reload SFC";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(213, 37);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(91, 30);
            this.button4.TabIndex = 3;
            this.button4.Text = "Reboot";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.buttonCylinderOff);
            this.groupBox5.Controls.Add(this.buttonCylinderOn);
            this.groupBox5.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox5.Location = new System.Drawing.Point(499, 81);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(314, 70);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Cylinder Control";
            // 
            // buttonCylinderOff
            // 
            this.buttonCylinderOff.Location = new System.Drawing.Point(109, 26);
            this.buttonCylinderOff.Name = "buttonCylinderOff";
            this.buttonCylinderOff.Size = new System.Drawing.Size(91, 30);
            this.buttonCylinderOff.TabIndex = 23;
            this.buttonCylinderOff.Text = "OFF";
            this.buttonCylinderOff.UseVisualStyleBackColor = true;
            this.buttonCylinderOff.Click += new System.EventHandler(this.buttonCylinderOff_Click);
            // 
            // buttonCylinderOn
            // 
            this.buttonCylinderOn.Location = new System.Drawing.Point(12, 26);
            this.buttonCylinderOn.Name = "buttonCylinderOn";
            this.buttonCylinderOn.Size = new System.Drawing.Size(91, 30);
            this.buttonCylinderOn.TabIndex = 22;
            this.buttonCylinderOn.Text = "On";
            this.buttonCylinderOn.UseVisualStyleBackColor = true;
            this.buttonCylinderOn.Click += new System.EventHandler(this.buttonCylinderOn_Click);
            // 
            // radioCylinder
            // 
            this.radioCylinder.AutoCheck = false;
            this.radioCylinder.AutoSize = true;
            this.radioCylinder.Location = new System.Drawing.Point(9, 23);
            this.radioCylinder.Name = "radioCylinder";
            this.radioCylinder.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioCylinder.Size = new System.Drawing.Size(48, 20);
            this.radioCylinder.TabIndex = 21;
            this.radioCylinder.TabStop = true;
            this.radioCylinder.Text = "ON";
            this.radioCylinder.UseVisualStyleBackColor = true;
            // 
            // groupSystem
            // 
            this.groupSystem.Controls.Add(this.buttonErrorAck);
            this.groupSystem.Controls.Add(this.buttonEvaluate);
            this.groupSystem.Controls.Add(this.textEvaluate);
            this.groupSystem.Controls.Add(this.button4);
            this.groupSystem.Controls.Add(this.button5);
            this.groupSystem.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupSystem.Location = new System.Drawing.Point(499, 344);
            this.groupSystem.Name = "groupSystem";
            this.groupSystem.Size = new System.Drawing.Size(314, 146);
            this.groupSystem.TabIndex = 10;
            this.groupSystem.TabStop = false;
            this.groupSystem.Text = "System";
            // 
            // buttonEvaluate
            // 
            this.buttonEvaluate.Location = new System.Drawing.Point(213, 84);
            this.buttonEvaluate.Name = "buttonEvaluate";
            this.buttonEvaluate.Size = new System.Drawing.Size(91, 30);
            this.buttonEvaluate.TabIndex = 6;
            this.buttonEvaluate.Text = "Evaluate";
            this.buttonEvaluate.UseVisualStyleBackColor = true;
            this.buttonEvaluate.Click += new System.EventHandler(this.buttonEvaluate_Click);
            // 
            // textEvaluate
            // 
            this.textEvaluate.Location = new System.Drawing.Point(12, 86);
            this.textEvaluate.Name = "textEvaluate";
            this.textEvaluate.Size = new System.Drawing.Size(195, 27);
            this.textEvaluate.TabIndex = 5;
            // 
            // groupSFC
            // 
            this.groupSFC.Controls.Add(this.label23);
            this.groupSFC.Controls.Add(this.textBoxRetryCountMax);
            this.groupSFC.Controls.Add(this.label20);
            this.groupSFC.Controls.Add(this.label21);
            this.groupSFC.Controls.Add(this.textBoxSettlingDurationMs);
            this.groupSFC.Controls.Add(this.buttonSdoRequest);
            this.groupSFC.Controls.Add(this.buttonReleaseFeeder);
            this.groupSFC.Controls.Add(this.buttonFeederEMS);
            this.groupSFC.Controls.Add(this.label14);
            this.groupSFC.Controls.Add(this.label15);
            this.groupSFC.Controls.Add(this.textRotationSpeed);
            this.groupSFC.Controls.Add(this.label16);
            this.groupSFC.Controls.Add(this.label17);
            this.groupSFC.Controls.Add(this.textRotationDistance);
            this.groupSFC.Controls.Add(this.buttonStartFeeder);
            this.groupSFC.Controls.Add(this.label10);
            this.groupSFC.Controls.Add(this.label11);
            this.groupSFC.Controls.Add(this.textCylinderOffMs);
            this.groupSFC.Controls.Add(this.label8);
            this.groupSFC.Controls.Add(this.label9);
            this.groupSFC.Controls.Add(this.textCylinderOnMs);
            this.groupSFC.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupSFC.Location = new System.Drawing.Point(824, 203);
            this.groupSFC.Name = "groupSFC";
            this.groupSFC.Size = new System.Drawing.Size(308, 287);
            this.groupSFC.TabIndex = 11;
            this.groupSFC.TabStop = false;
            this.groupSFC.Text = "SFC Control";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label23.Location = new System.Drawing.Point(11, 175);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(116, 16);
            this.label23.TabIndex = 30;
            this.label23.Text = "Max Retry Count";
            // 
            // textBoxRetryCountMax
            // 
            this.textBoxRetryCountMax.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxRetryCountMax.Location = new System.Drawing.Point(133, 170);
            this.textBoxRetryCountMax.Name = "textBoxRetryCountMax";
            this.textBoxRetryCountMax.Size = new System.Drawing.Size(103, 27);
            this.textBoxRetryCountMax.TabIndex = 29;
            this.textBoxRetryCountMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxRetryCountMax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRetryCountMax_KeyDown);
            this.textBoxRetryCountMax.Leave += new System.EventHandler(this.textBoxRetryCountMax_Leave);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label20.Location = new System.Drawing.Point(243, 87);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(26, 16);
            this.label20.TabIndex = 28;
            this.label20.Text = "ms";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label21.Location = new System.Drawing.Point(40, 87);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(91, 16);
            this.label21.TabIndex = 27;
            this.label21.Text = "Settling Time";
            // 
            // textBoxSettlingDurationMs
            // 
            this.textBoxSettlingDurationMs.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxSettlingDurationMs.Location = new System.Drawing.Point(133, 82);
            this.textBoxSettlingDurationMs.Name = "textBoxSettlingDurationMs";
            this.textBoxSettlingDurationMs.Size = new System.Drawing.Size(103, 27);
            this.textBoxSettlingDurationMs.TabIndex = 26;
            this.textBoxSettlingDurationMs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxSettlingDurationMs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSettlingDurationMs_KeyDown);
            this.textBoxSettlingDurationMs.Leave += new System.EventHandler(this.textBoxSettlingDurationMs_Leave);
            // 
            // buttonSdoRequest
            // 
            this.buttonSdoRequest.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonSdoRequest.Location = new System.Drawing.Point(17, 249);
            this.buttonSdoRequest.Name = "buttonSdoRequest";
            this.buttonSdoRequest.Size = new System.Drawing.Size(130, 31);
            this.buttonSdoRequest.TabIndex = 25;
            this.buttonSdoRequest.Text = "SDO Suspend";
            this.buttonSdoRequest.UseVisualStyleBackColor = true;
            this.buttonSdoRequest.Click += new System.EventHandler(this.buttonSdoRequest_Click);
            // 
            // buttonReleaseFeeder
            // 
            this.buttonReleaseFeeder.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonReleaseFeeder.Location = new System.Drawing.Point(153, 249);
            this.buttonReleaseFeeder.Name = "buttonReleaseFeeder";
            this.buttonReleaseFeeder.Size = new System.Drawing.Size(130, 31);
            this.buttonReleaseFeeder.TabIndex = 24;
            this.buttonReleaseFeeder.Text = "Release Feeder";
            this.buttonReleaseFeeder.UseVisualStyleBackColor = true;
            this.buttonReleaseFeeder.Click += new System.EventHandler(this.buttonReleaseFeeder_Click);
            // 
            // buttonFeederEMS
            // 
            this.buttonFeederEMS.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonFeederEMS.Location = new System.Drawing.Point(153, 208);
            this.buttonFeederEMS.Name = "buttonFeederEMS";
            this.buttonFeederEMS.Size = new System.Drawing.Size(130, 31);
            this.buttonFeederEMS.TabIndex = 23;
            this.buttonFeederEMS.Text = "EMS Feeder";
            this.buttonFeederEMS.UseVisualStyleBackColor = true;
            this.buttonFeederEMS.Click += new System.EventHandler(this.buttonFeederEMS_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label14.Location = new System.Drawing.Point(243, 146);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(51, 16);
            this.label14.TabIndex = 22;
            this.label14.Text = "pulse/s";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label15.Location = new System.Drawing.Point(24, 146);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(103, 16);
            this.label15.TabIndex = 21;
            this.label15.Text = "Rotation Speed";
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // textRotationSpeed
            // 
            this.textRotationSpeed.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textRotationSpeed.Location = new System.Drawing.Point(133, 141);
            this.textRotationSpeed.Name = "textRotationSpeed";
            this.textRotationSpeed.Size = new System.Drawing.Size(103, 27);
            this.textRotationSpeed.TabIndex = 20;
            this.textRotationSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textRotationSpeed.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textRotationSpeed_KeyDown);
            this.textRotationSpeed.Leave += new System.EventHandler(this.textRotationSpeed_Leave);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label16.Location = new System.Drawing.Point(243, 117);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(41, 16);
            this.label16.TabIndex = 19;
            this.label16.Text = "pulse";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label17.Location = new System.Drawing.Point(8, 117);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(119, 16);
            this.label17.TabIndex = 18;
            this.label17.Text = "Rotation Distance";
            // 
            // textRotationDistance
            // 
            this.textRotationDistance.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textRotationDistance.Location = new System.Drawing.Point(134, 112);
            this.textRotationDistance.Name = "textRotationDistance";
            this.textRotationDistance.Size = new System.Drawing.Size(103, 27);
            this.textRotationDistance.TabIndex = 17;
            this.textRotationDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textRotationDistance.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textRotationDistance_KeyDown);
            this.textRotationDistance.Leave += new System.EventHandler(this.textRotationDistance_Leave);
            // 
            // buttonStartFeeder
            // 
            this.buttonStartFeeder.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonStartFeeder.Location = new System.Drawing.Point(17, 208);
            this.buttonStartFeeder.Name = "buttonStartFeeder";
            this.buttonStartFeeder.Size = new System.Drawing.Size(130, 31);
            this.buttonStartFeeder.TabIndex = 16;
            this.buttonStartFeeder.Text = "Start Feeder";
            this.buttonStartFeeder.UseVisualStyleBackColor = true;
            this.buttonStartFeeder.Click += new System.EventHandler(this.buttonStartFeeder_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label10.Location = new System.Drawing.Point(243, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(26, 16);
            this.label10.TabIndex = 15;
            this.label10.Text = "ms";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label11.Location = new System.Drawing.Point(40, 58);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 16);
            this.label11.TabIndex = 14;
            this.label11.Text = "Cylinder Off";
            // 
            // textCylinderOffMs
            // 
            this.textCylinderOffMs.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textCylinderOffMs.Location = new System.Drawing.Point(133, 53);
            this.textCylinderOffMs.Name = "textCylinderOffMs";
            this.textCylinderOffMs.Size = new System.Drawing.Size(103, 27);
            this.textCylinderOffMs.TabIndex = 13;
            this.textCylinderOffMs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textCylinderOffMs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textCylinderOffMs_KeyDown);
            this.textCylinderOffMs.Leave += new System.EventHandler(this.textCylinderOffMs_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(243, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 16);
            this.label8.TabIndex = 12;
            this.label8.Text = "ms";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.Location = new System.Drawing.Point(42, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 16);
            this.label9.TabIndex = 11;
            this.label9.Text = "Cylinder On";
            // 
            // textCylinderOnMs
            // 
            this.textCylinderOnMs.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textCylinderOnMs.Location = new System.Drawing.Point(134, 24);
            this.textCylinderOnMs.Name = "textCylinderOnMs";
            this.textCylinderOnMs.Size = new System.Drawing.Size(103, 27);
            this.textCylinderOnMs.TabIndex = 10;
            this.textCylinderOnMs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textCylinderOnMs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textCylinderOnMs_KeyDown);
            this.textCylinderOnMs.Leave += new System.EventHandler(this.textCylinderOnMs_Leave);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label19);
            this.groupBox6.Controls.Add(this.textTP2DetectedPosition);
            this.groupBox6.Controls.Add(this.label18);
            this.groupBox6.Controls.Add(this.textTP1DetectedPosition);
            this.groupBox6.Controls.Add(this.radioTP2Detected);
            this.groupBox6.Controls.Add(this.radioTP1Detected);
            this.groupBox6.Controls.Add(this.radioFeederEMS);
            this.groupBox6.Controls.Add(this.radioFeederRunning);
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Controls.Add(this.textFeederOperationTime);
            this.groupBox6.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox6.Location = new System.Drawing.Point(824, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(308, 185);
            this.groupBox6.TabIndex = 12;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "SFC Status";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label19.Location = new System.Drawing.Point(16, 119);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(146, 16);
            this.label19.TabIndex = 23;
            this.label19.Text = "TP2 Detected Position";
            // 
            // textTP2DetectedPosition
            // 
            this.textTP2DetectedPosition.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textTP2DetectedPosition.Location = new System.Drawing.Point(170, 114);
            this.textTP2DetectedPosition.Name = "textTP2DetectedPosition";
            this.textTP2DetectedPosition.ReadOnly = true;
            this.textTP2DetectedPosition.Size = new System.Drawing.Size(90, 27);
            this.textTP2DetectedPosition.TabIndex = 22;
            this.textTP2DetectedPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label18.Location = new System.Drawing.Point(16, 86);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(146, 16);
            this.label18.TabIndex = 21;
            this.label18.Text = "TP1 Detected Position";
            // 
            // textTP1DetectedPosition
            // 
            this.textTP1DetectedPosition.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textTP1DetectedPosition.Location = new System.Drawing.Point(170, 81);
            this.textTP1DetectedPosition.Name = "textTP1DetectedPosition";
            this.textTP1DetectedPosition.ReadOnly = true;
            this.textTP1DetectedPosition.Size = new System.Drawing.Size(90, 27);
            this.textTP1DetectedPosition.TabIndex = 20;
            this.textTP1DetectedPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // radioTP2Detected
            // 
            this.radioTP2Detected.AutoCheck = false;
            this.radioTP2Detected.AutoSize = true;
            this.radioTP2Detected.Location = new System.Drawing.Point(133, 57);
            this.radioTP2Detected.Name = "radioTP2Detected";
            this.radioTP2Detected.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioTP2Detected.Size = new System.Drawing.Size(110, 20);
            this.radioTP2Detected.TabIndex = 19;
            this.radioTP2Detected.TabStop = true;
            this.radioTP2Detected.Text = "TP2 Detected";
            this.radioTP2Detected.UseVisualStyleBackColor = true;
            // 
            // radioTP1Detected
            // 
            this.radioTP1Detected.AutoCheck = false;
            this.radioTP1Detected.AutoSize = true;
            this.radioTP1Detected.Location = new System.Drawing.Point(16, 57);
            this.radioTP1Detected.Name = "radioTP1Detected";
            this.radioTP1Detected.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioTP1Detected.Size = new System.Drawing.Size(110, 20);
            this.radioTP1Detected.TabIndex = 18;
            this.radioTP1Detected.TabStop = true;
            this.radioTP1Detected.Text = "TP1 Detected";
            this.radioTP1Detected.UseVisualStyleBackColor = true;
            // 
            // radioFeederEMS
            // 
            this.radioFeederEMS.AutoCheck = false;
            this.radioFeederEMS.AutoSize = true;
            this.radioFeederEMS.Location = new System.Drawing.Point(187, 31);
            this.radioFeederEMS.Name = "radioFeederEMS";
            this.radioFeederEMS.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioFeederEMS.Size = new System.Drawing.Size(56, 20);
            this.radioFeederEMS.TabIndex = 17;
            this.radioFeederEMS.TabStop = true;
            this.radioFeederEMS.Text = "EMS";
            this.radioFeederEMS.UseVisualStyleBackColor = true;
            // 
            // radioFeederRunning
            // 
            this.radioFeederRunning.AutoCheck = false;
            this.radioFeederRunning.AutoSize = true;
            this.radioFeederRunning.Location = new System.Drawing.Point(45, 31);
            this.radioFeederRunning.Name = "radioFeederRunning";
            this.radioFeederRunning.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioFeederRunning.Size = new System.Drawing.Size(80, 20);
            this.radioFeederRunning.TabIndex = 16;
            this.radioFeederRunning.TabStop = true;
            this.radioFeederRunning.Text = "Running";
            this.radioFeederRunning.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label12.Location = new System.Drawing.Point(266, 151);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 16);
            this.label12.TabIndex = 15;
            this.label12.Text = "ms";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label13.Location = new System.Drawing.Point(56, 151);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(106, 16);
            this.label13.TabIndex = 14;
            this.label13.Text = "Operation Time";
            // 
            // textFeederOperationTime
            // 
            this.textFeederOperationTime.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textFeederOperationTime.Location = new System.Drawing.Point(170, 146);
            this.textFeederOperationTime.Name = "textFeederOperationTime";
            this.textFeederOperationTime.ReadOnly = true;
            this.textFeederOperationTime.Size = new System.Drawing.Size(90, 27);
            this.textFeederOperationTime.TabIndex = 13;
            this.textFeederOperationTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioCylinder);
            this.groupBox2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox2.Location = new System.Drawing.Point(499, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(314, 57);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cylinder Status";
            // 
            // timer1s
            // 
            this.timer1s.Interval = 1000;
            this.timer1s.Tick += new System.EventHandler(this.timer1s_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 497);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupSFC);
            this.Controls.Add(this.groupSystem);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupSystem.ResumeLayout(false);
            this.groupSystem.PerformLayout();
            this.groupSFC.ResumeLayout(false);
            this.groupSFC.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerSlow;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonWs;
        private System.Windows.Forms.Button buttonErrorAck;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioDriveExt1;
        private System.Windows.Forms.RadioButton radioDriveFault;
        private System.Windows.Forms.RadioButton radioDriveOn;
        private System.Windows.Forms.TextBox textTargetPosition;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textEncoderPosition;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textOperationMode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textTP1LatchPosition;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textTP2LatchPosition;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioTP2Enabled;
        private System.Windows.Forms.RadioButton radioDriveExt2;
        private System.Windows.Forms.RadioButton radioTP1Enabled;
        private System.Windows.Forms.Timer timerPoll;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.RadioButton radioDriveDin3;
        private System.Windows.Forms.RadioButton radioDriveDin2;
        private System.Windows.Forms.RadioButton radioDriveDin1;
        private System.Windows.Forms.RadioButton radioDriveDin0;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button buttonHasSFC;
        private System.Windows.Forms.Button buttonJogNegative;
        private System.Windows.Forms.Button buttonJogPositive;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textJogSpeed;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button buttonCylinderOff;
        private System.Windows.Forms.Button buttonCylinderOn;
        private System.Windows.Forms.RadioButton radioCylinder;
        private System.Windows.Forms.GroupBox groupSystem;
        private System.Windows.Forms.RadioButton radioEnableTP2;
        private System.Windows.Forms.RadioButton radioEnableTP1;
        private System.Windows.Forms.RadioButton radioTP2TriggerAction;
        private System.Windows.Forms.RadioButton radioTP1TriggerAction;
        private System.Windows.Forms.RadioButton radioTP2DownEdgeAction;
        private System.Windows.Forms.RadioButton radioTP1DownEdgeAction;
        private System.Windows.Forms.RadioButton radioTP2UpEdgeAction;
        private System.Windows.Forms.RadioButton radioTP1UpEdgeAction;
        private System.Windows.Forms.RadioButton radioTP2TriggerSelection;
        private System.Windows.Forms.RadioButton radioTP1TriggerSelection;
        private System.Windows.Forms.Button buttonEvaluate;
        private System.Windows.Forms.TextBox textEvaluate;
        private System.Windows.Forms.GroupBox groupSFC;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textCylinderOnMs;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textCylinderOffMs;
        private System.Windows.Forms.Button buttonStartFeeder;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textFeederOperationTime;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textRotationSpeed;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textRotationDistance;
        private System.Windows.Forms.Button buttonFeederEMS;
        private System.Windows.Forms.RadioButton radioFeederEMS;
        private System.Windows.Forms.RadioButton radioFeederRunning;
        private System.Windows.Forms.Button buttonReleaseFeeder;
        private System.Windows.Forms.Button buttonEtherCAT;
        private System.Windows.Forms.Button buttonError;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Timer timer1s;
        private System.Windows.Forms.Button buttonSdoRequest;
        private System.Windows.Forms.RadioButton radioTP2Detected;
        private System.Windows.Forms.RadioButton radioTP1Detected;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textTP2DetectedPosition;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textTP1DetectedPosition;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBoxSettlingDurationMs;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox textBoxRetryCountMax;
    }
}


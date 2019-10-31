namespace BotnanaClassLib
{
    partial class AxisControl
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

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBoxAxis = new System.Windows.Forms.GroupBox();
            this.textBoxAxisNumber = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tabControlAxis = new System.Windows.Forms.TabControl();
            this.tabPageAxisOperation = new System.Windows.Forms.TabPage();
            this.groupBoxAxisControl = new System.Windows.Forms.GroupBox();
            this.buttonClearFollowingErr = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonJog = new System.Windows.Forms.Button();
            this.textBoxTargetPos = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.buttonJogN = new System.Windows.Forms.Button();
            this.buttonJogP = new System.Windows.Forms.Button();
            this.textBoxVelocityLimit = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.groupBoxAxisStatus = new System.Windows.Forms.GroupBox();
            this.textBoxOutputPulse = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.textBoxFollowingError = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.textBoxFeedbackPos = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.textBoxEncoderPos = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.textBoxDemandPos = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.tabPageAxisConfig = new System.Windows.Forms.TabPage();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxAxisName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxEncoderUnit = new System.Windows.Forms.ComboBox();
            this.textBoxAfactor = new System.Windows.Forms.TextBox();
            this.textBoxVmax = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.textBoxAff = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxIgnorableDistance = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxCloseLoopFilter = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxDriveAlias = new System.Windows.Forms.TextBox();
            this.textBoxMaxPosDeviation = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxVfactor = new System.Windows.Forms.TextBox();
            this.textBoxDriveSlavePos = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxVff = new System.Windows.Forms.TextBox();
            this.textBoxDriveChannel = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textBoxAmax = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textBoxExtEncoderChannel = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBoxExtEncoderSlavePos = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxExtEncoderAlias = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxExtEncoderDirection = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxEncoderPPU = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxEncoderDirection = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxExtEncoderPPU = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxHomeOffset = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBoxAxis.SuspendLayout();
            this.tabControlAxis.SuspendLayout();
            this.tabPageAxisOperation.SuspendLayout();
            this.groupBoxAxisControl.SuspendLayout();
            this.groupBoxAxisStatus.SuspendLayout();
            this.tabPageAxisConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxAxis
            // 
            this.groupBoxAxis.Controls.Add(this.textBoxAxisNumber);
            this.groupBoxAxis.Controls.Add(this.label14);
            this.groupBoxAxis.Controls.Add(this.tabControlAxis);
            this.groupBoxAxis.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBoxAxis.Location = new System.Drawing.Point(3, 3);
            this.groupBoxAxis.Name = "groupBoxAxis";
            this.groupBoxAxis.Size = new System.Drawing.Size(337, 487);
            this.groupBoxAxis.TabIndex = 0;
            this.groupBoxAxis.TabStop = false;
            this.groupBoxAxis.Text = "Axis";
            // 
            // textBoxAxisNumber
            // 
            this.textBoxAxisNumber.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxAxisNumber.Location = new System.Drawing.Point(262, 22);
            this.textBoxAxisNumber.Name = "textBoxAxisNumber";
            this.textBoxAxisNumber.Size = new System.Drawing.Size(50, 25);
            this.textBoxAxisNumber.TabIndex = 6;
            this.textBoxAxisNumber.Text = "1";
            this.textBoxAxisNumber.TextChanged += new System.EventHandler(this.textBoxAxisNumber_TextChanged);
            this.textBoxAxisNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxAxisNumber_KeyDown);
            this.textBoxAxisNumber.Leave += new System.EventHandler(this.textBoxAxisNumber_Leave);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label14.Location = new System.Drawing.Point(171, 25);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(85, 17);
            this.label14.TabIndex = 5;
            this.label14.Text = "Axis Number";
            // 
            // tabControlAxis
            // 
            this.tabControlAxis.Controls.Add(this.tabPageAxisOperation);
            this.tabControlAxis.Controls.Add(this.tabPageAxisConfig);
            this.tabControlAxis.Location = new System.Drawing.Point(6, 26);
            this.tabControlAxis.Name = "tabControlAxis";
            this.tabControlAxis.SelectedIndex = 0;
            this.tabControlAxis.Size = new System.Drawing.Size(327, 455);
            this.tabControlAxis.TabIndex = 0;
            // 
            // tabPageAxisOperation
            // 
            this.tabPageAxisOperation.Controls.Add(this.groupBoxAxisControl);
            this.tabPageAxisOperation.Controls.Add(this.groupBoxAxisStatus);
            this.tabPageAxisOperation.Location = new System.Drawing.Point(4, 28);
            this.tabPageAxisOperation.Name = "tabPageAxisOperation";
            this.tabPageAxisOperation.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAxisOperation.Size = new System.Drawing.Size(319, 423);
            this.tabPageAxisOperation.TabIndex = 0;
            this.tabPageAxisOperation.Text = "Operation";
            this.tabPageAxisOperation.UseVisualStyleBackColor = true;
            // 
            // groupBoxAxisControl
            // 
            this.groupBoxAxisControl.Controls.Add(this.buttonClearFollowingErr);
            this.groupBoxAxisControl.Controls.Add(this.buttonStop);
            this.groupBoxAxisControl.Controls.Add(this.buttonJog);
            this.groupBoxAxisControl.Controls.Add(this.textBoxTargetPos);
            this.groupBoxAxisControl.Controls.Add(this.label31);
            this.groupBoxAxisControl.Controls.Add(this.buttonJogN);
            this.groupBoxAxisControl.Controls.Add(this.buttonJogP);
            this.groupBoxAxisControl.Controls.Add(this.textBoxVelocityLimit);
            this.groupBoxAxisControl.Controls.Add(this.label30);
            this.groupBoxAxisControl.Location = new System.Drawing.Point(6, 206);
            this.groupBoxAxisControl.Name = "groupBoxAxisControl";
            this.groupBoxAxisControl.Size = new System.Drawing.Size(307, 210);
            this.groupBoxAxisControl.TabIndex = 1;
            this.groupBoxAxisControl.TabStop = false;
            this.groupBoxAxisControl.Text = "Control";
            // 
            // buttonClearFollowingErr
            // 
            this.buttonClearFollowingErr.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonClearFollowingErr.Location = new System.Drawing.Point(66, 169);
            this.buttonClearFollowingErr.Name = "buttonClearFollowingErr";
            this.buttonClearFollowingErr.Size = new System.Drawing.Size(206, 25);
            this.buttonClearFollowingErr.TabIndex = 59;
            this.buttonClearFollowingErr.Text = "Clear Following Error";
            this.buttonClearFollowingErr.UseVisualStyleBackColor = true;
            this.buttonClearFollowingErr.Click += new System.EventHandler(this.buttonClearFollowingErr_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonStop.Location = new System.Drawing.Point(66, 97);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(100, 25);
            this.buttonStop.TabIndex = 58;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonJog
            // 
            this.buttonJog.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonJog.Location = new System.Drawing.Point(172, 97);
            this.buttonJog.Name = "buttonJog";
            this.buttonJog.Size = new System.Drawing.Size(100, 25);
            this.buttonJog.TabIndex = 57;
            this.buttonJog.Text = "JOG";
            this.buttonJog.UseVisualStyleBackColor = true;
            this.buttonJog.Click += new System.EventHandler(this.buttonJog_Click);
            // 
            // textBoxTargetPos
            // 
            this.textBoxTargetPos.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxTargetPos.Location = new System.Drawing.Point(172, 57);
            this.textBoxTargetPos.Name = "textBoxTargetPos";
            this.textBoxTargetPos.Size = new System.Drawing.Size(100, 25);
            this.textBoxTargetPos.TabIndex = 56;
            this.textBoxTargetPos.Text = "0.0";
            this.textBoxTargetPos.TextChanged += new System.EventHandler(this.textBoxTargetPos_TextChanged);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label31.Location = new System.Drawing.Point(32, 60);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(75, 17);
            this.label31.TabIndex = 55;
            this.label31.Text = "Target Pos.";
            // 
            // buttonJogN
            // 
            this.buttonJogN.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonJogN.Location = new System.Drawing.Point(172, 128);
            this.buttonJogN.Name = "buttonJogN";
            this.buttonJogN.Size = new System.Drawing.Size(100, 25);
            this.buttonJogN.TabIndex = 54;
            this.buttonJogN.Text = "JOG-";
            this.buttonJogN.UseVisualStyleBackColor = true;
            this.buttonJogN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonJogN_MouseDown);
            this.buttonJogN.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonJogN_MouseUp);
            // 
            // buttonJogP
            // 
            this.buttonJogP.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonJogP.Location = new System.Drawing.Point(66, 128);
            this.buttonJogP.Name = "buttonJogP";
            this.buttonJogP.Size = new System.Drawing.Size(100, 25);
            this.buttonJogP.TabIndex = 53;
            this.buttonJogP.Text = "JOG+";
            this.buttonJogP.UseVisualStyleBackColor = true;
            this.buttonJogP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonJogP_MouseDown);
            this.buttonJogP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonJogP_MouseUp);
            // 
            // textBoxVelocityLimit
            // 
            this.textBoxVelocityLimit.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxVelocityLimit.Location = new System.Drawing.Point(172, 26);
            this.textBoxVelocityLimit.Name = "textBoxVelocityLimit";
            this.textBoxVelocityLimit.Size = new System.Drawing.Size(100, 25);
            this.textBoxVelocityLimit.TabIndex = 22;
            this.textBoxVelocityLimit.Text = "0.0";
            this.textBoxVelocityLimit.TextChanged += new System.EventHandler(this.textBoxVelocityLimit_TextChanged);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label30.Location = new System.Drawing.Point(32, 29);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(89, 17);
            this.label30.TabIndex = 21;
            this.label30.Text = "Velocity Limit";
            // 
            // groupBoxAxisStatus
            // 
            this.groupBoxAxisStatus.Controls.Add(this.textBoxOutputPulse);
            this.groupBoxAxisStatus.Controls.Add(this.label29);
            this.groupBoxAxisStatus.Controls.Add(this.textBoxFollowingError);
            this.groupBoxAxisStatus.Controls.Add(this.label27);
            this.groupBoxAxisStatus.Controls.Add(this.textBoxFeedbackPos);
            this.groupBoxAxisStatus.Controls.Add(this.label26);
            this.groupBoxAxisStatus.Controls.Add(this.textBoxEncoderPos);
            this.groupBoxAxisStatus.Controls.Add(this.label25);
            this.groupBoxAxisStatus.Controls.Add(this.textBoxDemandPos);
            this.groupBoxAxisStatus.Controls.Add(this.label24);
            this.groupBoxAxisStatus.Location = new System.Drawing.Point(6, 6);
            this.groupBoxAxisStatus.Name = "groupBoxAxisStatus";
            this.groupBoxAxisStatus.Size = new System.Drawing.Size(307, 194);
            this.groupBoxAxisStatus.TabIndex = 0;
            this.groupBoxAxisStatus.TabStop = false;
            this.groupBoxAxisStatus.Text = "Status";
            // 
            // textBoxOutputPulse
            // 
            this.textBoxOutputPulse.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxOutputPulse.Location = new System.Drawing.Point(172, 150);
            this.textBoxOutputPulse.Name = "textBoxOutputPulse";
            this.textBoxOutputPulse.ReadOnly = true;
            this.textBoxOutputPulse.Size = new System.Drawing.Size(100, 25);
            this.textBoxOutputPulse.TabIndex = 18;
            this.textBoxOutputPulse.Text = "--";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label29.Location = new System.Drawing.Point(32, 153);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(88, 17);
            this.label29.TabIndex = 17;
            this.label29.Text = "Output Pulse";
            // 
            // textBoxFollowingError
            // 
            this.textBoxFollowingError.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxFollowingError.Location = new System.Drawing.Point(172, 119);
            this.textBoxFollowingError.Name = "textBoxFollowingError";
            this.textBoxFollowingError.ReadOnly = true;
            this.textBoxFollowingError.Size = new System.Drawing.Size(100, 25);
            this.textBoxFollowingError.TabIndex = 16;
            this.textBoxFollowingError.Text = "--";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label27.Location = new System.Drawing.Point(32, 122);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(99, 17);
            this.label27.TabIndex = 15;
            this.label27.Text = "Following Error";
            // 
            // textBoxFeedbackPos
            // 
            this.textBoxFeedbackPos.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxFeedbackPos.Location = new System.Drawing.Point(172, 88);
            this.textBoxFeedbackPos.Name = "textBoxFeedbackPos";
            this.textBoxFeedbackPos.ReadOnly = true;
            this.textBoxFeedbackPos.Size = new System.Drawing.Size(100, 25);
            this.textBoxFeedbackPos.TabIndex = 14;
            this.textBoxFeedbackPos.Text = "--";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label26.Location = new System.Drawing.Point(32, 91);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(93, 17);
            this.label26.TabIndex = 13;
            this.label26.Text = "Feedback Pos.";
            // 
            // textBoxEncoderPos
            // 
            this.textBoxEncoderPos.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxEncoderPos.Location = new System.Drawing.Point(172, 57);
            this.textBoxEncoderPos.Name = "textBoxEncoderPos";
            this.textBoxEncoderPos.ReadOnly = true;
            this.textBoxEncoderPos.Size = new System.Drawing.Size(100, 25);
            this.textBoxEncoderPos.TabIndex = 12;
            this.textBoxEncoderPos.Text = "--";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label25.Location = new System.Drawing.Point(32, 60);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(85, 17);
            this.label25.TabIndex = 11;
            this.label25.Text = "Encoder Pos.";
            // 
            // textBoxDemandPos
            // 
            this.textBoxDemandPos.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxDemandPos.Location = new System.Drawing.Point(172, 26);
            this.textBoxDemandPos.Name = "textBoxDemandPos";
            this.textBoxDemandPos.ReadOnly = true;
            this.textBoxDemandPos.Size = new System.Drawing.Size(100, 25);
            this.textBoxDemandPos.TabIndex = 10;
            this.textBoxDemandPos.Text = "--";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label24.Location = new System.Drawing.Point(32, 29);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(88, 17);
            this.label24.TabIndex = 9;
            this.label24.Text = "Denamd Pos.";
            // 
            // tabPageAxisConfig
            // 
            this.tabPageAxisConfig.AutoScroll = true;
            this.tabPageAxisConfig.AutoScrollMargin = new System.Drawing.Size(0, 25);
            this.tabPageAxisConfig.Controls.Add(this.buttonSave);
            this.tabPageAxisConfig.Controls.Add(this.textBoxAxisName);
            this.tabPageAxisConfig.Controls.Add(this.label1);
            this.tabPageAxisConfig.Controls.Add(this.comboBoxEncoderUnit);
            this.tabPageAxisConfig.Controls.Add(this.textBoxAfactor);
            this.tabPageAxisConfig.Controls.Add(this.textBoxVmax);
            this.tabPageAxisConfig.Controls.Add(this.label18);
            this.tabPageAxisConfig.Controls.Add(this.label22);
            this.tabPageAxisConfig.Controls.Add(this.textBoxAff);
            this.tabPageAxisConfig.Controls.Add(this.label19);
            this.tabPageAxisConfig.Controls.Add(this.label8);
            this.tabPageAxisConfig.Controls.Add(this.textBoxIgnorableDistance);
            this.tabPageAxisConfig.Controls.Add(this.label10);
            this.tabPageAxisConfig.Controls.Add(this.textBoxCloseLoopFilter);
            this.tabPageAxisConfig.Controls.Add(this.label9);
            this.tabPageAxisConfig.Controls.Add(this.textBoxDriveAlias);
            this.tabPageAxisConfig.Controls.Add(this.textBoxMaxPosDeviation);
            this.tabPageAxisConfig.Controls.Add(this.label23);
            this.tabPageAxisConfig.Controls.Add(this.label11);
            this.tabPageAxisConfig.Controls.Add(this.textBoxVfactor);
            this.tabPageAxisConfig.Controls.Add(this.textBoxDriveSlavePos);
            this.tabPageAxisConfig.Controls.Add(this.label21);
            this.tabPageAxisConfig.Controls.Add(this.label12);
            this.tabPageAxisConfig.Controls.Add(this.textBoxVff);
            this.tabPageAxisConfig.Controls.Add(this.textBoxDriveChannel);
            this.tabPageAxisConfig.Controls.Add(this.label20);
            this.tabPageAxisConfig.Controls.Add(this.textBoxAmax);
            this.tabPageAxisConfig.Controls.Add(this.label17);
            this.tabPageAxisConfig.Controls.Add(this.textBoxExtEncoderChannel);
            this.tabPageAxisConfig.Controls.Add(this.label16);
            this.tabPageAxisConfig.Controls.Add(this.textBoxExtEncoderSlavePos);
            this.tabPageAxisConfig.Controls.Add(this.label15);
            this.tabPageAxisConfig.Controls.Add(this.textBoxExtEncoderAlias);
            this.tabPageAxisConfig.Controls.Add(this.label13);
            this.tabPageAxisConfig.Controls.Add(this.textBoxExtEncoderDirection);
            this.tabPageAxisConfig.Controls.Add(this.label7);
            this.tabPageAxisConfig.Controls.Add(this.textBoxEncoderPPU);
            this.tabPageAxisConfig.Controls.Add(this.label6);
            this.tabPageAxisConfig.Controls.Add(this.textBoxEncoderDirection);
            this.tabPageAxisConfig.Controls.Add(this.label5);
            this.tabPageAxisConfig.Controls.Add(this.textBoxExtEncoderPPU);
            this.tabPageAxisConfig.Controls.Add(this.label4);
            this.tabPageAxisConfig.Controls.Add(this.label3);
            this.tabPageAxisConfig.Controls.Add(this.textBoxHomeOffset);
            this.tabPageAxisConfig.Controls.Add(this.label2);
            this.tabPageAxisConfig.Location = new System.Drawing.Point(4, 28);
            this.tabPageAxisConfig.Name = "tabPageAxisConfig";
            this.tabPageAxisConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAxisConfig.Size = new System.Drawing.Size(319, 423);
            this.tabPageAxisConfig.TabIndex = 1;
            this.tabPageAxisConfig.Text = "Config";
            this.tabPageAxisConfig.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonSave.Location = new System.Drawing.Point(178, 14);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(100, 25);
            this.buttonSave.TabIndex = 52;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxAxisName
            // 
            this.textBoxAxisName.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxAxisName.Location = new System.Drawing.Point(178, 45);
            this.textBoxAxisName.Name = "textBoxAxisName";
            this.textBoxAxisName.Size = new System.Drawing.Size(100, 25);
            this.textBoxAxisName.TabIndex = 8;
            this.textBoxAxisName.Text = "--";
            this.textBoxAxisName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxAxisName_KeyDown);
            this.textBoxAxisName.Leave += new System.EventHandler(this.textBoxAxisName_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(23, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Name";
            // 
            // comboBoxEncoderUnit
            // 
            this.comboBoxEncoderUnit.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBoxEncoderUnit.FormattingEnabled = true;
            this.comboBoxEncoderUnit.Items.AddRange(new object[] {
            "Meter",
            "Revolution",
            "Pulse",
            "UserDefine"});
            this.comboBoxEncoderUnit.Location = new System.Drawing.Point(178, 200);
            this.comboBoxEncoderUnit.Name = "comboBoxEncoderUnit";
            this.comboBoxEncoderUnit.Size = new System.Drawing.Size(100, 25);
            this.comboBoxEncoderUnit.TabIndex = 51;
            this.comboBoxEncoderUnit.Text = "--";
            this.comboBoxEncoderUnit.SelectedIndexChanged += new System.EventHandler(this.comboBoxEncoderUnit_SelectedIndexChanged);
            // 
            // textBoxAfactor
            // 
            this.textBoxAfactor.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxAfactor.Location = new System.Drawing.Point(178, 696);
            this.textBoxAfactor.Name = "textBoxAfactor";
            this.textBoxAfactor.Size = new System.Drawing.Size(100, 25);
            this.textBoxAfactor.TabIndex = 50;
            this.textBoxAfactor.Text = "--";
            this.textBoxAfactor.TextChanged += new System.EventHandler(this.textBoxAfactor_TextChanged);
            this.textBoxAfactor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxAfactor_KeyDown);
            this.textBoxAfactor.Leave += new System.EventHandler(this.textBoxAfactor_Leave);
            // 
            // textBoxVmax
            // 
            this.textBoxVmax.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxVmax.Location = new System.Drawing.Point(178, 541);
            this.textBoxVmax.Name = "textBoxVmax";
            this.textBoxVmax.Size = new System.Drawing.Size(100, 25);
            this.textBoxVmax.TabIndex = 40;
            this.textBoxVmax.Text = "--";
            this.textBoxVmax.TextChanged += new System.EventHandler(this.textBoxVmax_TextChanged);
            this.textBoxVmax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxVmax_KeyDown);
            this.textBoxVmax.Leave += new System.EventHandler(this.textBoxVmax_Leave);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label18.Location = new System.Drawing.Point(23, 544);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(42, 17);
            this.label18.TabIndex = 39;
            this.label18.Text = "Vmax";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label22.Location = new System.Drawing.Point(23, 699);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(52, 17);
            this.label22.TabIndex = 49;
            this.label22.Text = "Afactor";
            this.label22.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBoxAff
            // 
            this.textBoxAff.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxAff.Location = new System.Drawing.Point(178, 665);
            this.textBoxAff.Name = "textBoxAff";
            this.textBoxAff.Size = new System.Drawing.Size(100, 25);
            this.textBoxAff.TabIndex = 48;
            this.textBoxAff.Text = "--";
            this.textBoxAff.TextChanged += new System.EventHandler(this.textBoxAff_TextChanged);
            this.textBoxAff.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxAff_KeyDown);
            this.textBoxAff.Leave += new System.EventHandler(this.textBoxAff_Leave);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label19.Location = new System.Drawing.Point(23, 513);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(121, 17);
            this.label19.TabIndex = 41;
            this.label19.Text = "Ignorable Distance";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(23, 451);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 17);
            this.label8.TabIndex = 21;
            this.label8.Text = "Close Loop Filter";
            // 
            // textBoxIgnorableDistance
            // 
            this.textBoxIgnorableDistance.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxIgnorableDistance.Location = new System.Drawing.Point(178, 510);
            this.textBoxIgnorableDistance.Name = "textBoxIgnorableDistance";
            this.textBoxIgnorableDistance.Size = new System.Drawing.Size(100, 25);
            this.textBoxIgnorableDistance.TabIndex = 42;
            this.textBoxIgnorableDistance.Text = "--";
            this.textBoxIgnorableDistance.TextChanged += new System.EventHandler(this.textBoxIgnorableDistance_TextChanged);
            this.textBoxIgnorableDistance.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxIgnorableDistance_KeyDown);
            this.textBoxIgnorableDistance.Leave += new System.EventHandler(this.textBoxIgnorableDistance_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label10.Location = new System.Drawing.Point(23, 79);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 17);
            this.label10.TabIndex = 25;
            this.label10.Text = "Drive Alias";
            // 
            // textBoxCloseLoopFilter
            // 
            this.textBoxCloseLoopFilter.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxCloseLoopFilter.Location = new System.Drawing.Point(178, 448);
            this.textBoxCloseLoopFilter.Name = "textBoxCloseLoopFilter";
            this.textBoxCloseLoopFilter.Size = new System.Drawing.Size(100, 25);
            this.textBoxCloseLoopFilter.TabIndex = 22;
            this.textBoxCloseLoopFilter.Text = "--";
            this.textBoxCloseLoopFilter.TextChanged += new System.EventHandler(this.textBoxCloseLoopFilter_TextChanged);
            this.textBoxCloseLoopFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxCloseLoopFilter_KeyDown);
            this.textBoxCloseLoopFilter.Leave += new System.EventHandler(this.textBoxCloseLoopFilter_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.Location = new System.Drawing.Point(23, 482);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(147, 17);
            this.label9.TabIndex = 23;
            this.label9.Text = "Max Position Deviation";
            // 
            // textBoxDriveAlias
            // 
            this.textBoxDriveAlias.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxDriveAlias.Location = new System.Drawing.Point(178, 76);
            this.textBoxDriveAlias.Name = "textBoxDriveAlias";
            this.textBoxDriveAlias.Size = new System.Drawing.Size(100, 25);
            this.textBoxDriveAlias.TabIndex = 26;
            this.textBoxDriveAlias.Text = "--";
            this.textBoxDriveAlias.TextChanged += new System.EventHandler(this.textBoxDriveAlias_TextChanged);
            this.textBoxDriveAlias.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxDriveAlias_KeyDown);
            this.textBoxDriveAlias.Leave += new System.EventHandler(this.textBoxDriveAlias_Leave);
            // 
            // textBoxMaxPosDeviation
            // 
            this.textBoxMaxPosDeviation.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxMaxPosDeviation.Location = new System.Drawing.Point(178, 479);
            this.textBoxMaxPosDeviation.Name = "textBoxMaxPosDeviation";
            this.textBoxMaxPosDeviation.Size = new System.Drawing.Size(100, 25);
            this.textBoxMaxPosDeviation.TabIndex = 24;
            this.textBoxMaxPosDeviation.Text = "--";
            this.textBoxMaxPosDeviation.TextChanged += new System.EventHandler(this.textBoxMaxPosDeviation_TextChanged);
            this.textBoxMaxPosDeviation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxMaxPosDeviation_KeyDown);
            this.textBoxMaxPosDeviation.Leave += new System.EventHandler(this.textBoxMaxPosDeviation_Leave);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label23.Location = new System.Drawing.Point(23, 668);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(25, 17);
            this.label23.TabIndex = 47;
            this.label23.Text = "Aff";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label11.Location = new System.Drawing.Point(23, 110);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(126, 17);
            this.label11.TabIndex = 27;
            this.label11.Text = "Drive Slave Position";
            // 
            // textBoxVfactor
            // 
            this.textBoxVfactor.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxVfactor.Location = new System.Drawing.Point(178, 634);
            this.textBoxVfactor.Name = "textBoxVfactor";
            this.textBoxVfactor.Size = new System.Drawing.Size(100, 25);
            this.textBoxVfactor.TabIndex = 46;
            this.textBoxVfactor.Text = "--";
            this.textBoxVfactor.TextChanged += new System.EventHandler(this.textBoxVfactor_TextChanged);
            this.textBoxVfactor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxVfactor_KeyDown);
            this.textBoxVfactor.Leave += new System.EventHandler(this.textBoxVfactor_Leave);
            // 
            // textBoxDriveSlavePos
            // 
            this.textBoxDriveSlavePos.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxDriveSlavePos.Location = new System.Drawing.Point(178, 107);
            this.textBoxDriveSlavePos.Name = "textBoxDriveSlavePos";
            this.textBoxDriveSlavePos.Size = new System.Drawing.Size(100, 25);
            this.textBoxDriveSlavePos.TabIndex = 28;
            this.textBoxDriveSlavePos.Text = "--";
            this.textBoxDriveSlavePos.TextChanged += new System.EventHandler(this.textBoxDriveSlavePos_TextChanged);
            this.textBoxDriveSlavePos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxDriveSlavePos_KeyDown);
            this.textBoxDriveSlavePos.Leave += new System.EventHandler(this.textBoxDriveSlavePos_Leave);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label21.Location = new System.Drawing.Point(23, 637);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(52, 17);
            this.label21.TabIndex = 45;
            this.label21.Text = "Vfactor";
            this.label21.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label12.Location = new System.Drawing.Point(23, 141);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(93, 17);
            this.label12.TabIndex = 29;
            this.label12.Text = "Drive Channel";
            // 
            // textBoxVff
            // 
            this.textBoxVff.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxVff.Location = new System.Drawing.Point(178, 603);
            this.textBoxVff.Name = "textBoxVff";
            this.textBoxVff.Size = new System.Drawing.Size(100, 25);
            this.textBoxVff.TabIndex = 44;
            this.textBoxVff.Text = "--";
            this.textBoxVff.TextChanged += new System.EventHandler(this.textBoxVff_TextChanged);
            this.textBoxVff.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxVff_KeyDown);
            this.textBoxVff.Leave += new System.EventHandler(this.textBoxVff_Leave);
            // 
            // textBoxDriveChannel
            // 
            this.textBoxDriveChannel.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxDriveChannel.Location = new System.Drawing.Point(178, 138);
            this.textBoxDriveChannel.Name = "textBoxDriveChannel";
            this.textBoxDriveChannel.Size = new System.Drawing.Size(100, 25);
            this.textBoxDriveChannel.TabIndex = 30;
            this.textBoxDriveChannel.Text = "--";
            this.textBoxDriveChannel.TextChanged += new System.EventHandler(this.textBoxDriveChannel_TextChanged);
            this.textBoxDriveChannel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxDriveChannel_KeyDown);
            this.textBoxDriveChannel.Leave += new System.EventHandler(this.textBoxDriveChannel_Leave);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label20.Location = new System.Drawing.Point(23, 606);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(25, 17);
            this.label20.TabIndex = 43;
            this.label20.Text = "Vff";
            // 
            // textBoxAmax
            // 
            this.textBoxAmax.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxAmax.Location = new System.Drawing.Point(178, 572);
            this.textBoxAmax.Name = "textBoxAmax";
            this.textBoxAmax.Size = new System.Drawing.Size(100, 25);
            this.textBoxAmax.TabIndex = 38;
            this.textBoxAmax.Text = "--";
            this.textBoxAmax.TextChanged += new System.EventHandler(this.textBoxAmax_TextChanged);
            this.textBoxAmax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxAmax_KeyDown);
            this.textBoxAmax.Leave += new System.EventHandler(this.textBoxAmax_Leave);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label17.Location = new System.Drawing.Point(23, 575);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(42, 17);
            this.label17.TabIndex = 37;
            this.label17.Text = "Amax";
            // 
            // textBoxExtEncoderChannel
            // 
            this.textBoxExtEncoderChannel.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxExtEncoderChannel.Location = new System.Drawing.Point(178, 417);
            this.textBoxExtEncoderChannel.Name = "textBoxExtEncoderChannel";
            this.textBoxExtEncoderChannel.Size = new System.Drawing.Size(100, 25);
            this.textBoxExtEncoderChannel.TabIndex = 36;
            this.textBoxExtEncoderChannel.Text = "--";
            this.textBoxExtEncoderChannel.TextChanged += new System.EventHandler(this.textBoxExtEncoderChannel_TextChanged);
            this.textBoxExtEncoderChannel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxExtEncoderChannel_KeyDown);
            this.textBoxExtEncoderChannel.Leave += new System.EventHandler(this.textBoxExtEncoderChannel_Leave);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label16.Location = new System.Drawing.Point(23, 420);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(131, 17);
            this.label16.TabIndex = 35;
            this.label16.Text = "Ext Encoder Channel";
            // 
            // textBoxExtEncoderSlavePos
            // 
            this.textBoxExtEncoderSlavePos.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxExtEncoderSlavePos.Location = new System.Drawing.Point(178, 386);
            this.textBoxExtEncoderSlavePos.Name = "textBoxExtEncoderSlavePos";
            this.textBoxExtEncoderSlavePos.Size = new System.Drawing.Size(100, 25);
            this.textBoxExtEncoderSlavePos.TabIndex = 34;
            this.textBoxExtEncoderSlavePos.Text = "--";
            this.textBoxExtEncoderSlavePos.TextChanged += new System.EventHandler(this.textBoxExtEncoderSlavePos_TextChanged);
            this.textBoxExtEncoderSlavePos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxExtEncoderSlavePos_KeyDown);
            this.textBoxExtEncoderSlavePos.Leave += new System.EventHandler(this.textBoxExtEncoderSlavePos_Leave);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label15.Location = new System.Drawing.Point(23, 389);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(140, 17);
            this.label15.TabIndex = 33;
            this.label15.Text = "Ext Encoder Slave Pos.";
            // 
            // textBoxExtEncoderAlias
            // 
            this.textBoxExtEncoderAlias.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxExtEncoderAlias.Location = new System.Drawing.Point(178, 355);
            this.textBoxExtEncoderAlias.Name = "textBoxExtEncoderAlias";
            this.textBoxExtEncoderAlias.Size = new System.Drawing.Size(100, 25);
            this.textBoxExtEncoderAlias.TabIndex = 32;
            this.textBoxExtEncoderAlias.Text = "--";
            this.textBoxExtEncoderAlias.TextChanged += new System.EventHandler(this.textBoxExtEncoderAlias_TextChanged);
            this.textBoxExtEncoderAlias.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxExtEncoderAlias_KeyDown);
            this.textBoxExtEncoderAlias.Leave += new System.EventHandler(this.textBoxExtEncoderAlias_Leave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label13.Location = new System.Drawing.Point(23, 358);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(109, 17);
            this.label13.TabIndex = 31;
            this.label13.Text = "Ext Encoder Alias";
            // 
            // textBoxExtEncoderDirection
            // 
            this.textBoxExtEncoderDirection.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxExtEncoderDirection.Location = new System.Drawing.Point(178, 324);
            this.textBoxExtEncoderDirection.Name = "textBoxExtEncoderDirection";
            this.textBoxExtEncoderDirection.Size = new System.Drawing.Size(100, 25);
            this.textBoxExtEncoderDirection.TabIndex = 20;
            this.textBoxExtEncoderDirection.Text = "--";
            this.textBoxExtEncoderDirection.TextChanged += new System.EventHandler(this.textBoxExtEncoderDirection_TextChanged);
            this.textBoxExtEncoderDirection.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxExtEncoderDirection_KeyDown);
            this.textBoxExtEncoderDirection.Leave += new System.EventHandler(this.textBoxExtEncoderDirection_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(23, 327);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(136, 17);
            this.label7.TabIndex = 19;
            this.label7.Text = "Ext Encoder Direction";
            // 
            // textBoxEncoderPPU
            // 
            this.textBoxEncoderPPU.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxEncoderPPU.Location = new System.Drawing.Point(178, 231);
            this.textBoxEncoderPPU.Name = "textBoxEncoderPPU";
            this.textBoxEncoderPPU.Size = new System.Drawing.Size(100, 25);
            this.textBoxEncoderPPU.TabIndex = 18;
            this.textBoxEncoderPPU.Text = "--";
            this.textBoxEncoderPPU.TextChanged += new System.EventHandler(this.textBoxEncoderPPU_TextChanged);
            this.textBoxEncoderPPU.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxEncoderPPU_KeyDown);
            this.textBoxEncoderPPU.Leave += new System.EventHandler(this.textBoxEncoderPPU_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(23, 234);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 17);
            this.label6.TabIndex = 17;
            this.label6.Text = "Encoder PPU";
            // 
            // textBoxEncoderDirection
            // 
            this.textBoxEncoderDirection.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxEncoderDirection.Location = new System.Drawing.Point(178, 262);
            this.textBoxEncoderDirection.Name = "textBoxEncoderDirection";
            this.textBoxEncoderDirection.Size = new System.Drawing.Size(100, 25);
            this.textBoxEncoderDirection.TabIndex = 16;
            this.textBoxEncoderDirection.Text = "--";
            this.textBoxEncoderDirection.TextChanged += new System.EventHandler(this.textBoxEncoderDirection_TextChanged);
            this.textBoxEncoderDirection.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxEncoderDirection_KeyDown);
            this.textBoxEncoderDirection.Leave += new System.EventHandler(this.textBoxEncoderDirection_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(23, 265);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "Encoder Direction";
            // 
            // textBoxExtEncoderPPU
            // 
            this.textBoxExtEncoderPPU.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxExtEncoderPPU.Location = new System.Drawing.Point(178, 293);
            this.textBoxExtEncoderPPU.Name = "textBoxExtEncoderPPU";
            this.textBoxExtEncoderPPU.Size = new System.Drawing.Size(100, 25);
            this.textBoxExtEncoderPPU.TabIndex = 14;
            this.textBoxExtEncoderPPU.Text = "--";
            this.textBoxExtEncoderPPU.TextChanged += new System.EventHandler(this.textBoxExtEncoderPPU_TextChanged);
            this.textBoxExtEncoderPPU.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxExtEncoderPPU_KeyDown);
            this.textBoxExtEncoderPPU.Leave += new System.EventHandler(this.textBoxExtEncoderPPU_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(23, 296);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Ext Encoder PPU";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(23, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Encoder Unit";
            // 
            // textBoxHomeOffset
            // 
            this.textBoxHomeOffset.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxHomeOffset.Location = new System.Drawing.Point(178, 169);
            this.textBoxHomeOffset.Name = "textBoxHomeOffset";
            this.textBoxHomeOffset.Size = new System.Drawing.Size(100, 25);
            this.textBoxHomeOffset.TabIndex = 10;
            this.textBoxHomeOffset.Text = "--";
            this.textBoxHomeOffset.TextChanged += new System.EventHandler(this.textBoxHomeOffset_TextChanged);
            this.textBoxHomeOffset.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxHomeOffset_KeyDown);
            this.textBoxHomeOffset.Leave += new System.EventHandler(this.textBoxHomeOffset_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(23, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Home Offset";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // AxisControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxAxis);
            this.Name = "AxisControl";
            this.Size = new System.Drawing.Size(344, 495);
            this.groupBoxAxis.ResumeLayout(false);
            this.groupBoxAxis.PerformLayout();
            this.tabControlAxis.ResumeLayout(false);
            this.tabPageAxisOperation.ResumeLayout(false);
            this.groupBoxAxisControl.ResumeLayout(false);
            this.groupBoxAxisControl.PerformLayout();
            this.groupBoxAxisStatus.ResumeLayout(false);
            this.groupBoxAxisStatus.PerformLayout();
            this.tabPageAxisConfig.ResumeLayout(false);
            this.tabPageAxisConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxAxis;
        private System.Windows.Forms.GroupBox groupBoxAxisControl;
        private System.Windows.Forms.GroupBox groupBoxAxisStatus;
        private System.Windows.Forms.TextBox textBoxAxisNumber;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TabControl tabControlAxis;
        private System.Windows.Forms.TabPage tabPageAxisOperation;
        private System.Windows.Forms.TabPage tabPageAxisConfig;
        private System.Windows.Forms.TextBox textBoxHomeOffset;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAxisName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxEncoderPPU;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxEncoderDirection;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxExtEncoderPPU;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxExtEncoderDirection;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxAfactor;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox textBoxAff;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox textBoxVfactor;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBoxVff;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBoxIgnorableDistance;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBoxVmax;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBoxAmax;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBoxDriveChannel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxDriveSlavePos;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxDriveAlias;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxMaxPosDeviation;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxCloseLoopFilter;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxExtEncoderChannel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxExtEncoderAlias;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBoxExtEncoderSlavePos;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox comboBoxEncoderUnit;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxOutputPulse;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox textBoxFollowingError;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox textBoxFeedbackPos;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox textBoxEncoderPos;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox textBoxDemandPos;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button buttonJogN;
        private System.Windows.Forms.Button buttonJogP;
        private System.Windows.Forms.TextBox textBoxVelocityLimit;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox textBoxTargetPos;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonJog;
        private System.Windows.Forms.Button buttonClearFollowingErr;
    }
}

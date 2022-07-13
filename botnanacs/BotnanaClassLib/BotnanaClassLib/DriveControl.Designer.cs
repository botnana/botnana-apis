namespace BotnanaClassLib
{
    partial class DriveControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxProfileVelocity = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textBoxHMSpeed1 = new System.Windows.Forms.TextBox();
            this.textBoxTorqueSlope = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxProfileDeceleration = new System.Windows.Forms.TextBox();
            this.textBoxHMSpeed2 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxProfileAcceleration = new System.Windows.Forms.TextBox();
            this.textBoxHMAcceleration = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxHMMethod = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonGo = new System.Windows.Forms.Button();
            this.buttonDriveHaltOff = new System.Windows.Forms.Button();
            this.buttonDriveHaltOn = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.buttonResetFault = new System.Windows.Forms.Button();
            this.textBoxTargetVelocity = new System.Windows.Forms.TextBox();
            this.buttonServoOff = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.buttonServoOn = new System.Windows.Forms.Button();
            this.textBoxTargetPosition = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxOPMode = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxRealTorque = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.textBoxDigitalInput = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxRealPos = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxStatusWord = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxControlWord = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPDSState = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.textBoxAlias = new System.Windows.Forms.TextBox();
            this.textBoxSlavePos = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxChannel = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonClearFollowingErr = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.textBoxAlias);
            this.groupBox1.Controls.Add(this.textBoxSlavePos);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBoxChannel);
            this.groupBox1.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 745);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Drive";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonClearFollowingErr);
            this.groupBox3.Controls.Add(this.panel1);
            this.groupBox3.Controls.Add(this.buttonGo);
            this.groupBox3.Controls.Add(this.buttonDriveHaltOff);
            this.groupBox3.Controls.Add(this.buttonDriveHaltOn);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.buttonResetFault);
            this.groupBox3.Controls.Add(this.textBoxTargetVelocity);
            this.groupBox3.Controls.Add(this.buttonServoOff);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.buttonServoOn);
            this.groupBox3.Controls.Add(this.textBoxTargetPosition);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.comboBoxOPMode);
            this.groupBox3.Location = new System.Drawing.Point(6, 286);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(283, 454);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Control";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoScrollMargin = new System.Drawing.Size(0, 25);
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.textBoxProfileVelocity);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.textBoxHMSpeed1);
            this.panel1.Controls.Add(this.textBoxTorqueSlope);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.textBoxProfileDeceleration);
            this.panel1.Controls.Add(this.textBoxHMSpeed2);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.textBoxProfileAcceleration);
            this.panel1.Controls.Add(this.textBoxHMAcceleration);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.textBoxHMMethod);
            this.panel1.Controls.Add(this.label9);
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(0, 152);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(283, 158);
            this.panel1.TabIndex = 45;
            // 
            // textBoxProfileVelocity
            // 
            this.textBoxProfileVelocity.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxProfileVelocity.Location = new System.Drawing.Point(159, 127);
            this.textBoxProfileVelocity.Name = "textBoxProfileVelocity";
            this.textBoxProfileVelocity.Size = new System.Drawing.Size(100, 25);
            this.textBoxProfileVelocity.TabIndex = 46;
            this.textBoxProfileVelocity.Text = "--";
            this.textBoxProfileVelocity.TextChanged += new System.EventHandler(this.textBoxProfileVelocity_TextChanged);
            this.textBoxProfileVelocity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxProfileVelocity_KeyDown);
            this.textBoxProfileVelocity.Leave += new System.EventHandler(this.textBoxProfileVelocity_Leave);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label20.Location = new System.Drawing.Point(19, 130);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(97, 17);
            this.label20.TabIndex = 45;
            this.label20.Text = "Profile Velocity";
            // 
            // textBoxHMSpeed1
            // 
            this.textBoxHMSpeed1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxHMSpeed1.Location = new System.Drawing.Point(159, 34);
            this.textBoxHMSpeed1.Name = "textBoxHMSpeed1";
            this.textBoxHMSpeed1.Size = new System.Drawing.Size(100, 25);
            this.textBoxHMSpeed1.TabIndex = 28;
            this.textBoxHMSpeed1.Text = "--";
            this.textBoxHMSpeed1.TextChanged += new System.EventHandler(this.textBoxHMSpeed1_TextChanged);
            this.textBoxHMSpeed1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxHMSpeed1_KeyDown);
            this.textBoxHMSpeed1.Leave += new System.EventHandler(this.textBoxHMSpeed1_Leave);
            // 
            // textBoxTorqueSlope
            // 
            this.textBoxTorqueSlope.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxTorqueSlope.Location = new System.Drawing.Point(159, 220);
            this.textBoxTorqueSlope.Name = "textBoxTorqueSlope";
            this.textBoxTorqueSlope.Size = new System.Drawing.Size(100, 25);
            this.textBoxTorqueSlope.TabIndex = 44;
            this.textBoxTorqueSlope.Text = "--";
            this.textBoxTorqueSlope.TextChanged += new System.EventHandler(this.textBoxTorqueSlope_TextChanged);
            this.textBoxTorqueSlope.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxTorqueSlope_KeyDown);
            this.textBoxTorqueSlope.Leave += new System.EventHandler(this.textBoxTorqueSlope_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label10.Location = new System.Drawing.Point(19, 37);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 17);
            this.label10.TabIndex = 27;
            this.label10.Text = "HM Speed-1";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label19.Location = new System.Drawing.Point(19, 223);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(87, 17);
            this.label19.TabIndex = 43;
            this.label19.Text = "Torque Slope";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label11.Location = new System.Drawing.Point(19, 68);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 17);
            this.label11.TabIndex = 29;
            this.label11.Text = "HM Speed-2";
            // 
            // textBoxProfileDeceleration
            // 
            this.textBoxProfileDeceleration.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxProfileDeceleration.Location = new System.Drawing.Point(159, 189);
            this.textBoxProfileDeceleration.Name = "textBoxProfileDeceleration";
            this.textBoxProfileDeceleration.Size = new System.Drawing.Size(100, 25);
            this.textBoxProfileDeceleration.TabIndex = 36;
            this.textBoxProfileDeceleration.Text = "--";
            this.textBoxProfileDeceleration.TextChanged += new System.EventHandler(this.textBoxProfileDeceleration_TextChanged);
            this.textBoxProfileDeceleration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxProfileDeceleration_KeyDown);
            this.textBoxProfileDeceleration.Leave += new System.EventHandler(this.textBoxProfileDeceleration_Leave);
            // 
            // textBoxHMSpeed2
            // 
            this.textBoxHMSpeed2.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxHMSpeed2.Location = new System.Drawing.Point(159, 65);
            this.textBoxHMSpeed2.Name = "textBoxHMSpeed2";
            this.textBoxHMSpeed2.Size = new System.Drawing.Size(100, 25);
            this.textBoxHMSpeed2.TabIndex = 30;
            this.textBoxHMSpeed2.Text = "--";
            this.textBoxHMSpeed2.TextChanged += new System.EventHandler(this.textBoxHMSpeed2_TextChanged);
            this.textBoxHMSpeed2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxHMSpeed2_KeyDown);
            this.textBoxHMSpeed2.Leave += new System.EventHandler(this.textBoxHMSpeed2_Leave);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label14.Location = new System.Drawing.Point(19, 192);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(125, 17);
            this.label14.TabIndex = 35;
            this.label14.Text = "Profile Deceleration";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label12.Location = new System.Drawing.Point(19, 99);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(108, 17);
            this.label12.TabIndex = 31;
            this.label12.Text = "HM Acceleration";
            // 
            // textBoxProfileAcceleration
            // 
            this.textBoxProfileAcceleration.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxProfileAcceleration.Location = new System.Drawing.Point(159, 158);
            this.textBoxProfileAcceleration.Name = "textBoxProfileAcceleration";
            this.textBoxProfileAcceleration.Size = new System.Drawing.Size(100, 25);
            this.textBoxProfileAcceleration.TabIndex = 34;
            this.textBoxProfileAcceleration.Text = "--";
            this.textBoxProfileAcceleration.TextChanged += new System.EventHandler(this.textBoxProfileAcceleration_TextChanged);
            this.textBoxProfileAcceleration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxProfileAcceleration_KeyDown);
            this.textBoxProfileAcceleration.Leave += new System.EventHandler(this.textBoxProfileAcceleration_Leave);
            // 
            // textBoxHMAcceleration
            // 
            this.textBoxHMAcceleration.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxHMAcceleration.Location = new System.Drawing.Point(159, 96);
            this.textBoxHMAcceleration.Name = "textBoxHMAcceleration";
            this.textBoxHMAcceleration.Size = new System.Drawing.Size(100, 25);
            this.textBoxHMAcceleration.TabIndex = 32;
            this.textBoxHMAcceleration.Text = "--";
            this.textBoxHMAcceleration.TextChanged += new System.EventHandler(this.textBoxHMAcceleration_TextChanged);
            this.textBoxHMAcceleration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxHMAcceleration_KeyDown);
            this.textBoxHMAcceleration.Leave += new System.EventHandler(this.textBoxHMAcceleration_Leave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label13.Location = new System.Drawing.Point(19, 161);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(123, 17);
            this.label13.TabIndex = 33;
            this.label13.Text = "Profile Acceleration";
            // 
            // textBoxHMMethod
            // 
            this.textBoxHMMethod.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxHMMethod.Location = new System.Drawing.Point(159, 3);
            this.textBoxHMMethod.Name = "textBoxHMMethod";
            this.textBoxHMMethod.Size = new System.Drawing.Size(100, 25);
            this.textBoxHMMethod.TabIndex = 22;
            this.textBoxHMMethod.Text = "--";
            this.textBoxHMMethod.TextChanged += new System.EventHandler(this.textBoxHMMethod_TextChanged);
            this.textBoxHMMethod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxHMMethod_KeyDown);
            this.textBoxHMMethod.Leave += new System.EventHandler(this.textBoxHMMethod_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.Location = new System.Drawing.Point(19, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 17);
            this.label9.TabIndex = 21;
            this.label9.Text = "HM Method";
            // 
            // buttonGo
            // 
            this.buttonGo.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonGo.Location = new System.Drawing.Point(35, 385);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(100, 25);
            this.buttonGo.TabIndex = 18;
            this.buttonGo.Text = "GO";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // buttonDriveHaltOff
            // 
            this.buttonDriveHaltOff.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonDriveHaltOff.Location = new System.Drawing.Point(146, 354);
            this.buttonDriveHaltOff.Name = "buttonDriveHaltOff";
            this.buttonDriveHaltOff.Size = new System.Drawing.Size(100, 25);
            this.buttonDriveHaltOff.TabIndex = 17;
            this.buttonDriveHaltOff.Text = "-Drive Halt";
            this.buttonDriveHaltOff.UseVisualStyleBackColor = true;
            this.buttonDriveHaltOff.Click += new System.EventHandler(this.buttonDriveHaltOff_Click);
            // 
            // buttonDriveHaltOn
            // 
            this.buttonDriveHaltOn.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonDriveHaltOn.Location = new System.Drawing.Point(35, 354);
            this.buttonDriveHaltOn.Name = "buttonDriveHaltOn";
            this.buttonDriveHaltOn.Size = new System.Drawing.Size(100, 25);
            this.buttonDriveHaltOn.TabIndex = 16;
            this.buttonDriveHaltOn.Text = "+Drive Halt";
            this.buttonDriveHaltOn.UseVisualStyleBackColor = true;
            this.buttonDriveHaltOn.Click += new System.EventHandler(this.buttonDriveHaltOn_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label17.Location = new System.Drawing.Point(20, 122);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(93, 17);
            this.label17.TabIndex = 41;
            this.label17.Text = "Target Torque";
            // 
            // buttonResetFault
            // 
            this.buttonResetFault.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonResetFault.Location = new System.Drawing.Point(146, 385);
            this.buttonResetFault.Name = "buttonResetFault";
            this.buttonResetFault.Size = new System.Drawing.Size(100, 25);
            this.buttonResetFault.TabIndex = 15;
            this.buttonResetFault.Text = "Reset Fault";
            this.buttonResetFault.UseVisualStyleBackColor = true;
            this.buttonResetFault.Click += new System.EventHandler(this.buttonResetFault_Click);
            // 
            // textBoxTargetVelocity
            // 
            this.textBoxTargetVelocity.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxTargetVelocity.Location = new System.Drawing.Point(160, 88);
            this.textBoxTargetVelocity.Name = "textBoxTargetVelocity";
            this.textBoxTargetVelocity.Size = new System.Drawing.Size(100, 25);
            this.textBoxTargetVelocity.TabIndex = 40;
            this.textBoxTargetVelocity.Text = "--";
            this.textBoxTargetVelocity.TextChanged += new System.EventHandler(this.textBoxTargetVelocity_TextChanged);
            this.textBoxTargetVelocity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxTargetVelocity_KeyDown);
            this.textBoxTargetVelocity.Leave += new System.EventHandler(this.textBoxTargetVelocity_Leave);
            // 
            // buttonServoOff
            // 
            this.buttonServoOff.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonServoOff.Location = new System.Drawing.Point(146, 323);
            this.buttonServoOff.Name = "buttonServoOff";
            this.buttonServoOff.Size = new System.Drawing.Size(100, 25);
            this.buttonServoOff.TabIndex = 14;
            this.buttonServoOff.Text = "Servo OFF";
            this.buttonServoOff.UseVisualStyleBackColor = true;
            this.buttonServoOff.Click += new System.EventHandler(this.buttonServoOff_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label16.Location = new System.Drawing.Point(20, 91);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(98, 17);
            this.label16.TabIndex = 39;
            this.label16.Text = "Target Velocity";
            // 
            // buttonServoOn
            // 
            this.buttonServoOn.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonServoOn.Location = new System.Drawing.Point(35, 323);
            this.buttonServoOn.Name = "buttonServoOn";
            this.buttonServoOn.Size = new System.Drawing.Size(100, 25);
            this.buttonServoOn.TabIndex = 13;
            this.buttonServoOn.Text = "Servo ON";
            this.buttonServoOn.UseVisualStyleBackColor = true;
            this.buttonServoOn.Click += new System.EventHandler(this.buttonServoOn_Click);
            // 
            // textBoxTargetPosition
            // 
            this.textBoxTargetPosition.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxTargetPosition.Location = new System.Drawing.Point(160, 57);
            this.textBoxTargetPosition.Name = "textBoxTargetPosition";
            this.textBoxTargetPosition.Size = new System.Drawing.Size(100, 25);
            this.textBoxTargetPosition.TabIndex = 38;
            this.textBoxTargetPosition.Text = "--";
            this.textBoxTargetPosition.TextChanged += new System.EventHandler(this.textBoxTargetPosition_TextChanged);
            this.textBoxTargetPosition.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxTargetPosition_KeyDown);
            this.textBoxTargetPosition.Leave += new System.EventHandler(this.textBoxTargetPosition_Leave);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label15.Location = new System.Drawing.Point(20, 60);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(99, 17);
            this.label15.TabIndex = 37;
            this.label15.Text = "Target Position";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(20, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 17);
            this.label6.TabIndex = 12;
            this.label6.Text = "Operation Mode";
            // 
            // comboBoxOPMode
            // 
            this.comboBoxOPMode.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBoxOPMode.FormattingEnabled = true;
            this.comboBoxOPMode.Location = new System.Drawing.Point(160, 26);
            this.comboBoxOPMode.Name = "comboBoxOPMode";
            this.comboBoxOPMode.Size = new System.Drawing.Size(75, 25);
            this.comboBoxOPMode.TabIndex = 11;
            this.comboBoxOPMode.Text = "--";
            this.comboBoxOPMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxOPMode_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxRealTorque);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.textBoxDigitalInput);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBoxRealPos);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBoxStatusWord);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textBoxControlWord);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBoxPDSState);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Location = new System.Drawing.Point(6, 57);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(283, 223);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Status";
            // 
            // textBoxRealTorque
            // 
            this.textBoxRealTorque.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxRealTorque.Location = new System.Drawing.Point(160, 88);
            this.textBoxRealTorque.Name = "textBoxRealTorque";
            this.textBoxRealTorque.ReadOnly = true;
            this.textBoxRealTorque.Size = new System.Drawing.Size(100, 25);
            this.textBoxRealTorque.TabIndex = 22;
            this.textBoxRealTorque.Text = "--";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label18.Location = new System.Drawing.Point(20, 91);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(79, 17);
            this.label18.TabIndex = 21;
            this.label18.Text = "Real Torque";
            // 
            // textBoxDigitalInput
            // 
            this.textBoxDigitalInput.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxDigitalInput.Location = new System.Drawing.Point(160, 119);
            this.textBoxDigitalInput.Name = "textBoxDigitalInput";
            this.textBoxDigitalInput.ReadOnly = true;
            this.textBoxDigitalInput.Size = new System.Drawing.Size(100, 25);
            this.textBoxDigitalInput.TabIndex = 20;
            this.textBoxDigitalInput.Text = "--";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(20, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 17);
            this.label4.TabIndex = 19;
            this.label4.Text = "Digital Input";
            // 
            // textBoxRealPos
            // 
            this.textBoxRealPos.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxRealPos.Location = new System.Drawing.Point(160, 57);
            this.textBoxRealPos.Name = "textBoxRealPos";
            this.textBoxRealPos.ReadOnly = true;
            this.textBoxRealPos.Size = new System.Drawing.Size(100, 25);
            this.textBoxRealPos.TabIndex = 18;
            this.textBoxRealPos.Text = "--";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(20, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "Real Position";
            // 
            // textBoxStatusWord
            // 
            this.textBoxStatusWord.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxStatusWord.Location = new System.Drawing.Point(160, 181);
            this.textBoxStatusWord.Name = "textBoxStatusWord";
            this.textBoxStatusWord.ReadOnly = true;
            this.textBoxStatusWord.Size = new System.Drawing.Size(100, 25);
            this.textBoxStatusWord.TabIndex = 16;
            this.textBoxStatusWord.Text = "--";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(20, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Status Word";
            // 
            // textBoxControlWord
            // 
            this.textBoxControlWord.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxControlWord.Location = new System.Drawing.Point(160, 150);
            this.textBoxControlWord.Name = "textBoxControlWord";
            this.textBoxControlWord.ReadOnly = true;
            this.textBoxControlWord.Size = new System.Drawing.Size(100, 25);
            this.textBoxControlWord.TabIndex = 14;
            this.textBoxControlWord.Text = "--";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(20, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 17);
            this.label1.TabIndex = 13;
            this.label1.Text = "Control Word";
            // 
            // textBoxPDSState
            // 
            this.textBoxPDSState.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxPDSState.Location = new System.Drawing.Point(110, 26);
            this.textBoxPDSState.Name = "textBoxPDSState";
            this.textBoxPDSState.ReadOnly = true;
            this.textBoxPDSState.Size = new System.Drawing.Size(150, 25);
            this.textBoxPDSState.TabIndex = 12;
            this.textBoxPDSState.Text = "--";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label24.Location = new System.Drawing.Point(20, 29);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(67, 17);
            this.label24.TabIndex = 11;
            this.label24.Text = "PDS State";
            // 
            // textBoxAlias
            // 
            this.textBoxAlias.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxAlias.Location = new System.Drawing.Point(53, 26);
            this.textBoxAlias.Name = "textBoxAlias";
            this.textBoxAlias.Size = new System.Drawing.Size(35, 25);
            this.textBoxAlias.TabIndex = 22;
            this.textBoxAlias.Text = "0";
            this.textBoxAlias.TextChanged += new System.EventHandler(this.textBoxAlias_TextChanged);
            this.textBoxAlias.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxAlias_KeyDown);
            this.textBoxAlias.Leave += new System.EventHandler(this.textBoxAlias_Leave);
            // 
            // textBoxSlavePos
            // 
            this.textBoxSlavePos.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxSlavePos.Location = new System.Drawing.Point(142, 26);
            this.textBoxSlavePos.Name = "textBoxSlavePos";
            this.textBoxSlavePos.Size = new System.Drawing.Size(35, 25);
            this.textBoxSlavePos.TabIndex = 26;
            this.textBoxSlavePos.Text = "1";
            this.textBoxSlavePos.TextChanged += new System.EventHandler(this.textBoxSlavePos_TextChanged);
            this.textBoxSlavePos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSlavePos_KeyDown);
            this.textBoxSlavePos.Leave += new System.EventHandler(this.textBoxSlavePos_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(11, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 17);
            this.label5.TabIndex = 21;
            this.label5.Text = "Alias";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(185, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 17);
            this.label7.TabIndex = 23;
            this.label7.Text = "Channel";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(97, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 17);
            this.label8.TabIndex = 25;
            this.label8.Text = "Slave";
            // 
            // textBoxChannel
            // 
            this.textBoxChannel.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxChannel.Location = new System.Drawing.Point(249, 26);
            this.textBoxChannel.Name = "textBoxChannel";
            this.textBoxChannel.Size = new System.Drawing.Size(35, 25);
            this.textBoxChannel.TabIndex = 24;
            this.textBoxChannel.Text = "1";
            this.textBoxChannel.TextChanged += new System.EventHandler(this.textBoxChannel_TextChanged);
            this.textBoxChannel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxChannel_KeyDown);
            this.textBoxChannel.Leave += new System.EventHandler(this.textBoxChannel_Leave);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttonClearFollowingErr
            // 
            this.buttonClearFollowingErr.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonClearFollowingErr.Location = new System.Drawing.Point(35, 418);
            this.buttonClearFollowingErr.Name = "buttonClearFollowingErr";
            this.buttonClearFollowingErr.Size = new System.Drawing.Size(211, 25);
            this.buttonClearFollowingErr.TabIndex = 60;
            this.buttonClearFollowingErr.Text = "Clear Following Error";
            this.buttonClearFollowingErr.UseVisualStyleBackColor = true;
            this.buttonClearFollowingErr.Click += new System.EventHandler(this.buttonClearFollowingErr_Click);
            // 
            // DriveControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "DriveControl";
            this.Size = new System.Drawing.Size(303, 751);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxDigitalInput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxRealPos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxStatusWord;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxControlWord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPDSState;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxOPMode;
        private System.Windows.Forms.TextBox textBoxSlavePos;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxChannel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxAlias;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxTorqueSlope;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBoxProfileDeceleration;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBoxProfileAcceleration;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxHMAcceleration;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxHMSpeed2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxHMSpeed1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.Button buttonDriveHaltOff;
        private System.Windows.Forms.Button buttonDriveHaltOn;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button buttonResetFault;
        private System.Windows.Forms.TextBox textBoxTargetVelocity;
        private System.Windows.Forms.Button buttonServoOff;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button buttonServoOn;
        private System.Windows.Forms.TextBox textBoxTargetPosition;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBoxHMMethod;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxRealTorque;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxProfileVelocity;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonClearFollowingErr;
    }
}

namespace AIO
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.textSlavesState = new System.Windows.Forms.TextBox();
            this.labelSlavesCount = new System.Windows.Forms.Label();
            this.textSlavesCount = new System.Windows.Forms.TextBox();
            this.textAin1Raw = new System.Windows.Forms.TextBox();
            this.textAin2Raw = new System.Windows.Forms.TextBox();
            this.textAin3Raw = new System.Windows.Forms.TextBox();
            this.textAin4Raw = new System.Windows.Forms.TextBox();
            this.radioAin1Enabled = new System.Windows.Forms.RadioButton();
            this.radioAin2Enabled = new System.Windows.Forms.RadioButton();
            this.radioAin3Enabled = new System.Windows.Forms.RadioButton();
            this.radioAin4Enabled = new System.Windows.Forms.RadioButton();
            this.groupAinStatus = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textAin4 = new System.Windows.Forms.TextBox();
            this.textAin3 = new System.Windows.Forms.TextBox();
            this.textAin2 = new System.Windows.Forms.TextBox();
            this.textAin1 = new System.Windows.Forms.TextBox();
            this.comboAinMode = new System.Windows.Forms.ComboBox();
            this.groupAoutStatus = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.textAout1Raw = new System.Windows.Forms.TextBox();
            this.textAout2Raw = new System.Windows.Forms.TextBox();
            this.textAout3Raw = new System.Windows.Forms.TextBox();
            this.textAout4Raw = new System.Windows.Forms.TextBox();
            this.radioAout1Enabled = new System.Windows.Forms.RadioButton();
            this.radioAout4Enabled = new System.Windows.Forms.RadioButton();
            this.radioAout3Enabled = new System.Windows.Forms.RadioButton();
            this.radioAout2Enabled = new System.Windows.Forms.RadioButton();
            this.textAout1 = new System.Windows.Forms.TextBox();
            this.textAout2 = new System.Windows.Forms.TextBox();
            this.comboAout1Mode = new System.Windows.Forms.ComboBox();
            this.comboAout2Mode = new System.Windows.Forms.ComboBox();
            this.comboAout3Mode = new System.Windows.Forms.ComboBox();
            this.comboAout4Mode = new System.Windows.Forms.ComboBox();
            this.textAout3 = new System.Windows.Forms.TextBox();
            this.textAout4 = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonWS = new System.Windows.Forms.Button();
            this.groupAinStatus.SuspendLayout();
            this.groupAoutStatus.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 34);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "State";
            // 
            // textSlavesState
            // 
            this.textSlavesState.Location = new System.Drawing.Point(179, 28);
            this.textSlavesState.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textSlavesState.Name = "textSlavesState";
            this.textSlavesState.ReadOnly = true;
            this.textSlavesState.Size = new System.Drawing.Size(57, 27);
            this.textSlavesState.TabIndex = 11;
            // 
            // labelSlavesCount
            // 
            this.labelSlavesCount.AutoSize = true;
            this.labelSlavesCount.Location = new System.Drawing.Point(9, 34);
            this.labelSlavesCount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelSlavesCount.Name = "labelSlavesCount";
            this.labelSlavesCount.Size = new System.Drawing.Size(50, 16);
            this.labelSlavesCount.TabIndex = 10;
            this.labelSlavesCount.Text = "Count ";
            // 
            // textSlavesCount
            // 
            this.textSlavesCount.Location = new System.Drawing.Point(64, 28);
            this.textSlavesCount.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textSlavesCount.Name = "textSlavesCount";
            this.textSlavesCount.ReadOnly = true;
            this.textSlavesCount.Size = new System.Drawing.Size(55, 27);
            this.textSlavesCount.TabIndex = 9;
            // 
            // textAin1Raw
            // 
            this.textAin1Raw.Location = new System.Drawing.Point(59, 54);
            this.textAin1Raw.Name = "textAin1Raw";
            this.textAin1Raw.ReadOnly = true;
            this.textAin1Raw.Size = new System.Drawing.Size(79, 27);
            this.textAin1Raw.TabIndex = 13;
            this.textAin1Raw.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textAin2Raw
            // 
            this.textAin2Raw.Location = new System.Drawing.Point(59, 83);
            this.textAin2Raw.Name = "textAin2Raw";
            this.textAin2Raw.ReadOnly = true;
            this.textAin2Raw.Size = new System.Drawing.Size(79, 27);
            this.textAin2Raw.TabIndex = 14;
            this.textAin2Raw.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textAin3Raw
            // 
            this.textAin3Raw.Location = new System.Drawing.Point(59, 112);
            this.textAin3Raw.Name = "textAin3Raw";
            this.textAin3Raw.ReadOnly = true;
            this.textAin3Raw.Size = new System.Drawing.Size(79, 27);
            this.textAin3Raw.TabIndex = 15;
            this.textAin3Raw.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textAin4Raw
            // 
            this.textAin4Raw.Location = new System.Drawing.Point(59, 141);
            this.textAin4Raw.Name = "textAin4Raw";
            this.textAin4Raw.ReadOnly = true;
            this.textAin4Raw.Size = new System.Drawing.Size(79, 27);
            this.textAin4Raw.TabIndex = 16;
            this.textAin4Raw.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // radioAin1Enabled
            // 
            this.radioAin1Enabled.AutoCheck = false;
            this.radioAin1Enabled.AutoSize = true;
            this.radioAin1Enabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioAin1Enabled.Location = new System.Drawing.Point(15, 26);
            this.radioAin1Enabled.Name = "radioAin1Enabled";
            this.radioAin1Enabled.Size = new System.Drawing.Size(114, 20);
            this.radioAin1Enabled.TabIndex = 17;
            this.radioAin1Enabled.TabStop = true;
            this.radioAin1Enabled.Text = "CH 1 Enabled";
            this.radioAin1Enabled.UseVisualStyleBackColor = true;
            this.radioAin1Enabled.Click += new System.EventHandler(this.radioAin1Enabled_Click);
            // 
            // radioAin2Enabled
            // 
            this.radioAin2Enabled.AutoCheck = false;
            this.radioAin2Enabled.AutoSize = true;
            this.radioAin2Enabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioAin2Enabled.Location = new System.Drawing.Point(15, 54);
            this.radioAin2Enabled.Name = "radioAin2Enabled";
            this.radioAin2Enabled.Size = new System.Drawing.Size(114, 20);
            this.radioAin2Enabled.TabIndex = 18;
            this.radioAin2Enabled.TabStop = true;
            this.radioAin2Enabled.Text = "CH 2 Enabled";
            this.radioAin2Enabled.UseVisualStyleBackColor = true;
            this.radioAin2Enabled.Click += new System.EventHandler(this.radioAin2Enabled_Click);
            // 
            // radioAin3Enabled
            // 
            this.radioAin3Enabled.AutoCheck = false;
            this.radioAin3Enabled.AutoSize = true;
            this.radioAin3Enabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioAin3Enabled.Location = new System.Drawing.Point(15, 82);
            this.radioAin3Enabled.Name = "radioAin3Enabled";
            this.radioAin3Enabled.Size = new System.Drawing.Size(114, 20);
            this.radioAin3Enabled.TabIndex = 19;
            this.radioAin3Enabled.TabStop = true;
            this.radioAin3Enabled.Text = "CH 3 Enabled";
            this.radioAin3Enabled.UseVisualStyleBackColor = true;
            this.radioAin3Enabled.Click += new System.EventHandler(this.radioAin3Enabled_Click);
            // 
            // radioAin4Enabled
            // 
            this.radioAin4Enabled.AutoCheck = false;
            this.radioAin4Enabled.AutoSize = true;
            this.radioAin4Enabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioAin4Enabled.Location = new System.Drawing.Point(15, 110);
            this.radioAin4Enabled.Name = "radioAin4Enabled";
            this.radioAin4Enabled.Size = new System.Drawing.Size(114, 20);
            this.radioAin4Enabled.TabIndex = 20;
            this.radioAin4Enabled.TabStop = true;
            this.radioAin4Enabled.Text = "CH 4 Enabled";
            this.radioAin4Enabled.UseVisualStyleBackColor = true;
            this.radioAin4Enabled.Click += new System.EventHandler(this.radioAin4Enabled_Click);
            // 
            // groupAinStatus
            // 
            this.groupAinStatus.Controls.Add(this.label8);
            this.groupAinStatus.Controls.Add(this.label7);
            this.groupAinStatus.Controls.Add(this.label6);
            this.groupAinStatus.Controls.Add(this.label5);
            this.groupAinStatus.Controls.Add(this.label4);
            this.groupAinStatus.Controls.Add(this.label1);
            this.groupAinStatus.Controls.Add(this.textAin4);
            this.groupAinStatus.Controls.Add(this.textAin3);
            this.groupAinStatus.Controls.Add(this.textAin2);
            this.groupAinStatus.Controls.Add(this.textAin1);
            this.groupAinStatus.Controls.Add(this.textAin1Raw);
            this.groupAinStatus.Controls.Add(this.textAin2Raw);
            this.groupAinStatus.Controls.Add(this.textAin3Raw);
            this.groupAinStatus.Controls.Add(this.textAin4Raw);
            this.groupAinStatus.Location = new System.Drawing.Point(12, 88);
            this.groupAinStatus.Name = "groupAinStatus";
            this.groupAinStatus.Size = new System.Drawing.Size(253, 189);
            this.groupAinStatus.TabIndex = 21;
            this.groupAinStatus.TabStop = false;
            this.groupAinStatus.Text = "AIN (Slave 4)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 144);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 16);
            this.label8.TabIndex = 37;
            this.label8.Text = "CH 4";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 115);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 16);
            this.label7.TabIndex = 36;
            this.label7.Text = "CH 3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 16);
            this.label6.TabIndex = 35;
            this.label6.Text = "CH 2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 16);
            this.label5.TabIndex = 34;
            this.label5.Text = "CH 1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(152, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 16);
            this.label4.TabIndex = 33;
            this.label4.Text = "Votlage";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 16);
            this.label1.TabIndex = 32;
            this.label1.Text = "Raw Data";
            // 
            // textAin4
            // 
            this.textAin4.Location = new System.Drawing.Point(155, 141);
            this.textAin4.Name = "textAin4";
            this.textAin4.ReadOnly = true;
            this.textAin4.Size = new System.Drawing.Size(71, 27);
            this.textAin4.TabIndex = 31;
            this.textAin4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textAin3
            // 
            this.textAin3.Location = new System.Drawing.Point(155, 112);
            this.textAin3.Name = "textAin3";
            this.textAin3.ReadOnly = true;
            this.textAin3.Size = new System.Drawing.Size(71, 27);
            this.textAin3.TabIndex = 30;
            this.textAin3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textAin2
            // 
            this.textAin2.Location = new System.Drawing.Point(155, 83);
            this.textAin2.Name = "textAin2";
            this.textAin2.ReadOnly = true;
            this.textAin2.Size = new System.Drawing.Size(71, 27);
            this.textAin2.TabIndex = 29;
            this.textAin2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textAin1
            // 
            this.textAin1.Location = new System.Drawing.Point(155, 54);
            this.textAin1.Name = "textAin1";
            this.textAin1.ReadOnly = true;
            this.textAin1.Size = new System.Drawing.Size(71, 27);
            this.textAin1.TabIndex = 28;
            this.textAin1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // comboAinMode
            // 
            this.comboAinMode.FormattingEnabled = true;
            this.comboAinMode.Items.AddRange(new object[] {
            "-5 ~ 5V",
            "-10V ~ 10V"});
            this.comboAinMode.Location = new System.Drawing.Point(115, 131);
            this.comboAinMode.Name = "comboAinMode";
            this.comboAinMode.Size = new System.Drawing.Size(94, 24);
            this.comboAinMode.TabIndex = 27;
            this.comboAinMode.SelectionChangeCommitted += new System.EventHandler(this.comboAinMode_SelectionChangeCommitted);
            // 
            // groupAoutStatus
            // 
            this.groupAoutStatus.Controls.Add(this.label15);
            this.groupAoutStatus.Controls.Add(this.label11);
            this.groupAoutStatus.Controls.Add(this.label12);
            this.groupAoutStatus.Controls.Add(this.label13);
            this.groupAoutStatus.Controls.Add(this.label14);
            this.groupAoutStatus.Controls.Add(this.textAout1Raw);
            this.groupAoutStatus.Controls.Add(this.textAout2Raw);
            this.groupAoutStatus.Controls.Add(this.textAout3Raw);
            this.groupAoutStatus.Controls.Add(this.textAout4Raw);
            this.groupAoutStatus.Location = new System.Drawing.Point(280, 88);
            this.groupAoutStatus.Name = "groupAoutStatus";
            this.groupAoutStatus.Size = new System.Drawing.Size(325, 189);
            this.groupAoutStatus.TabIndex = 22;
            this.groupAoutStatus.TabStop = false;
            this.groupAoutStatus.Text = "AOUT (Slave 5)";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(60, 23);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(69, 16);
            this.label15.TabIndex = 42;
            this.label15.Text = "Raw Data";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 144);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 16);
            this.label11.TabIndex = 41;
            this.label11.Text = "CH 4";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(16, 115);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 16);
            this.label12.TabIndex = 40;
            this.label12.Text = "CH 3";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(16, 86);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 16);
            this.label13.TabIndex = 39;
            this.label13.Text = "CH 2";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(16, 57);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 16);
            this.label14.TabIndex = 38;
            this.label14.Text = "CH 1";
            // 
            // textAout1Raw
            // 
            this.textAout1Raw.Location = new System.Drawing.Point(63, 54);
            this.textAout1Raw.Name = "textAout1Raw";
            this.textAout1Raw.ReadOnly = true;
            this.textAout1Raw.Size = new System.Drawing.Size(75, 27);
            this.textAout1Raw.TabIndex = 13;
            this.textAout1Raw.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textAout2Raw
            // 
            this.textAout2Raw.Location = new System.Drawing.Point(63, 83);
            this.textAout2Raw.Name = "textAout2Raw";
            this.textAout2Raw.ReadOnly = true;
            this.textAout2Raw.Size = new System.Drawing.Size(75, 27);
            this.textAout2Raw.TabIndex = 14;
            this.textAout2Raw.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textAout3Raw
            // 
            this.textAout3Raw.Location = new System.Drawing.Point(63, 112);
            this.textAout3Raw.Name = "textAout3Raw";
            this.textAout3Raw.ReadOnly = true;
            this.textAout3Raw.Size = new System.Drawing.Size(75, 27);
            this.textAout3Raw.TabIndex = 15;
            this.textAout3Raw.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textAout4Raw
            // 
            this.textAout4Raw.Location = new System.Drawing.Point(63, 141);
            this.textAout4Raw.Name = "textAout4Raw";
            this.textAout4Raw.ReadOnly = true;
            this.textAout4Raw.Size = new System.Drawing.Size(75, 27);
            this.textAout4Raw.TabIndex = 16;
            this.textAout4Raw.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // radioAout1Enabled
            // 
            this.radioAout1Enabled.AutoCheck = false;
            this.radioAout1Enabled.AutoSize = true;
            this.radioAout1Enabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioAout1Enabled.Location = new System.Drawing.Point(6, 51);
            this.radioAout1Enabled.Name = "radioAout1Enabled";
            this.radioAout1Enabled.Size = new System.Drawing.Size(59, 20);
            this.radioAout1Enabled.TabIndex = 17;
            this.radioAout1Enabled.TabStop = true;
            this.radioAout1Enabled.Text = "CH 1";
            this.radioAout1Enabled.UseVisualStyleBackColor = true;
            this.radioAout1Enabled.Click += new System.EventHandler(this.radioAout1Enabled_Click);
            // 
            // radioAout4Enabled
            // 
            this.radioAout4Enabled.AutoCheck = false;
            this.radioAout4Enabled.AutoSize = true;
            this.radioAout4Enabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioAout4Enabled.Location = new System.Drawing.Point(6, 135);
            this.radioAout4Enabled.Name = "radioAout4Enabled";
            this.radioAout4Enabled.Size = new System.Drawing.Size(59, 20);
            this.radioAout4Enabled.TabIndex = 20;
            this.radioAout4Enabled.TabStop = true;
            this.radioAout4Enabled.Text = "CH 4";
            this.radioAout4Enabled.UseVisualStyleBackColor = true;
            this.radioAout4Enabled.Click += new System.EventHandler(this.radioAout4Enabled_Click);
            // 
            // radioAout3Enabled
            // 
            this.radioAout3Enabled.AutoCheck = false;
            this.radioAout3Enabled.AutoSize = true;
            this.radioAout3Enabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioAout3Enabled.Location = new System.Drawing.Point(6, 107);
            this.radioAout3Enabled.Name = "radioAout3Enabled";
            this.radioAout3Enabled.Size = new System.Drawing.Size(59, 20);
            this.radioAout3Enabled.TabIndex = 19;
            this.radioAout3Enabled.TabStop = true;
            this.radioAout3Enabled.Text = "CH 3";
            this.radioAout3Enabled.UseVisualStyleBackColor = true;
            this.radioAout3Enabled.Click += new System.EventHandler(this.radioAout3Enabled_Click);
            // 
            // radioAout2Enabled
            // 
            this.radioAout2Enabled.AutoCheck = false;
            this.radioAout2Enabled.AutoSize = true;
            this.radioAout2Enabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioAout2Enabled.Location = new System.Drawing.Point(6, 79);
            this.radioAout2Enabled.Name = "radioAout2Enabled";
            this.radioAout2Enabled.Size = new System.Drawing.Size(59, 20);
            this.radioAout2Enabled.TabIndex = 18;
            this.radioAout2Enabled.TabStop = true;
            this.radioAout2Enabled.Text = "CH 2";
            this.radioAout2Enabled.UseVisualStyleBackColor = true;
            this.radioAout2Enabled.Click += new System.EventHandler(this.radioAout2Enabled_Click);
            // 
            // textAout1
            // 
            this.textAout1.Location = new System.Drawing.Point(223, 47);
            this.textAout1.Name = "textAout1";
            this.textAout1.Size = new System.Drawing.Size(84, 27);
            this.textAout1.TabIndex = 23;
            this.textAout1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textAout1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAout1_KeyDown);
            this.textAout1.Leave += new System.EventHandler(this.textAout1_Leave);
            // 
            // textAout2
            // 
            this.textAout2.Location = new System.Drawing.Point(223, 75);
            this.textAout2.Name = "textAout2";
            this.textAout2.Size = new System.Drawing.Size(84, 27);
            this.textAout2.TabIndex = 24;
            this.textAout2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textAout2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAout2_KeyDown);
            this.textAout2.Leave += new System.EventHandler(this.textAout2_Leave);
            // 
            // comboAout1Mode
            // 
            this.comboAout1Mode.FormattingEnabled = true;
            this.comboAout1Mode.Items.AddRange(new object[] {
            "0 ~ 5V",
            "0 ~ 10V",
            "-5 ~ 5V",
            "-10V ~ 10V"});
            this.comboAout1Mode.Location = new System.Drawing.Point(114, 50);
            this.comboAout1Mode.Name = "comboAout1Mode";
            this.comboAout1Mode.Size = new System.Drawing.Size(103, 24);
            this.comboAout1Mode.TabIndex = 26;
            this.comboAout1Mode.SelectionChangeCommitted += new System.EventHandler(this.comboAout1Mode_SelectionChangeCommitted);
            // 
            // comboAout2Mode
            // 
            this.comboAout2Mode.FormattingEnabled = true;
            this.comboAout2Mode.Items.AddRange(new object[] {
            "0 ~ 5V",
            "0 ~ 10V",
            "-5 ~ 5V",
            "-10V ~ 10V"});
            this.comboAout2Mode.Location = new System.Drawing.Point(114, 78);
            this.comboAout2Mode.Name = "comboAout2Mode";
            this.comboAout2Mode.Size = new System.Drawing.Size(103, 24);
            this.comboAout2Mode.TabIndex = 27;
            this.comboAout2Mode.SelectionChangeCommitted += new System.EventHandler(this.comboAout2Mode_SelectionChangeCommitted);
            // 
            // comboAout3Mode
            // 
            this.comboAout3Mode.FormattingEnabled = true;
            this.comboAout3Mode.Items.AddRange(new object[] {
            "0 ~ 5V",
            "0 ~ 10V",
            "-5 ~ 5V",
            "-10V ~ 10V"});
            this.comboAout3Mode.Location = new System.Drawing.Point(114, 106);
            this.comboAout3Mode.Name = "comboAout3Mode";
            this.comboAout3Mode.Size = new System.Drawing.Size(103, 24);
            this.comboAout3Mode.TabIndex = 28;
            this.comboAout3Mode.SelectionChangeCommitted += new System.EventHandler(this.comboAout3Mode_SelectionChangeCommitted);
            // 
            // comboAout4Mode
            // 
            this.comboAout4Mode.FormattingEnabled = true;
            this.comboAout4Mode.Items.AddRange(new object[] {
            "0 ~ 5V",
            "0 ~ 10V",
            "-5 ~ 5V",
            "-10V ~ 10V"});
            this.comboAout4Mode.Location = new System.Drawing.Point(114, 134);
            this.comboAout4Mode.Name = "comboAout4Mode";
            this.comboAout4Mode.Size = new System.Drawing.Size(103, 24);
            this.comboAout4Mode.TabIndex = 29;
            this.comboAout4Mode.SelectionChangeCommitted += new System.EventHandler(this.comboAout4Mode_SelectionChangeCommitted);
            // 
            // textAout3
            // 
            this.textAout3.Location = new System.Drawing.Point(223, 103);
            this.textAout3.Name = "textAout3";
            this.textAout3.Size = new System.Drawing.Size(84, 27);
            this.textAout3.TabIndex = 30;
            this.textAout3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textAout3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAout3_KeyDown);
            this.textAout3.Leave += new System.EventHandler(this.textAout3_Leave);
            // 
            // textAout4
            // 
            this.textAout4.Location = new System.Drawing.Point(223, 131);
            this.textAout4.Name = "textAout4";
            this.textAout4.Size = new System.Drawing.Size(84, 27);
            this.textAout4.TabIndex = 31;
            this.textAout4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textAout4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAout4_KeyDown);
            this.textAout4.Leave += new System.EventHandler(this.textAout4_Leave);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.textSlavesCount);
            this.groupBox3.Controls.Add(this.labelSlavesCount);
            this.groupBox3.Controls.Add(this.textSlavesState);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(253, 66);
            this.groupBox3.TabIndex = 32;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "EtherCAT Slaves";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.radioAin1Enabled);
            this.groupBox4.Controls.Add(this.radioAin2Enabled);
            this.groupBox4.Controls.Add(this.radioAin3Enabled);
            this.groupBox4.Controls.Add(this.radioAin4Enabled);
            this.groupBox4.Controls.Add(this.comboAinMode);
            this.groupBox4.Location = new System.Drawing.Point(12, 287);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(253, 183);
            this.groupBox4.TabIndex = 33;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "AIN Setting";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 16);
            this.label3.TabIndex = 28;
            this.label3.Text = "Mode";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.radioAout1Enabled);
            this.groupBox5.Controls.Add(this.textAout4);
            this.groupBox5.Controls.Add(this.radioAout4Enabled);
            this.groupBox5.Controls.Add(this.textAout3);
            this.groupBox5.Controls.Add(this.radioAout2Enabled);
            this.groupBox5.Controls.Add(this.radioAout3Enabled);
            this.groupBox5.Controls.Add(this.textAout2);
            this.groupBox5.Controls.Add(this.comboAout1Mode);
            this.groupBox5.Controls.Add(this.textAout1);
            this.groupBox5.Controls.Add(this.comboAout4Mode);
            this.groupBox5.Controls.Add(this.comboAout2Mode);
            this.groupBox5.Controls.Add(this.comboAout3Mode);
            this.groupBox5.Location = new System.Drawing.Point(280, 287);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(325, 183);
            this.groupBox5.TabIndex = 34;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "AOUT Setting";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(220, 23);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(57, 16);
            this.label16.TabIndex = 34;
            this.label16.Text = "Votlage";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(48, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 16);
            this.label9.TabIndex = 32;
            this.label9.Text = "Enabled";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(118, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 16);
            this.label10.TabIndex = 31;
            this.label10.Text = "Mode";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonWS);
            this.groupBox1.Location = new System.Drawing.Point(280, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(325, 66);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "WebSocket";
            // 
            // buttonWS
            // 
            this.buttonWS.BackColor = System.Drawing.Color.IndianRed;
            this.buttonWS.Location = new System.Drawing.Point(32, 26);
            this.buttonWS.Name = "buttonWS";
            this.buttonWS.Size = new System.Drawing.Size(67, 27);
            this.buttonWS.TabIndex = 1;
            this.buttonWS.UseVisualStyleBackColor = false;
            this.buttonWS.Click += new System.EventHandler(this.buttonWS_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 493);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupAoutStatus);
            this.Controls.Add(this.groupAinStatus);
            this.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupAinStatus.ResumeLayout(false);
            this.groupAinStatus.PerformLayout();
            this.groupAoutStatus.ResumeLayout(false);
            this.groupAoutStatus.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textSlavesState;
        private System.Windows.Forms.Label labelSlavesCount;
        private System.Windows.Forms.TextBox textSlavesCount;
        private System.Windows.Forms.TextBox textAin1Raw;
        private System.Windows.Forms.TextBox textAin2Raw;
        private System.Windows.Forms.TextBox textAin3Raw;
        private System.Windows.Forms.TextBox textAin4Raw;
        private System.Windows.Forms.RadioButton radioAin1Enabled;
        private System.Windows.Forms.RadioButton radioAin2Enabled;
        private System.Windows.Forms.RadioButton radioAin3Enabled;
        private System.Windows.Forms.RadioButton radioAin4Enabled;
        private System.Windows.Forms.GroupBox groupAinStatus;
        private System.Windows.Forms.GroupBox groupAoutStatus;
        private System.Windows.Forms.RadioButton radioAout1Enabled;
        private System.Windows.Forms.RadioButton radioAout4Enabled;
        private System.Windows.Forms.TextBox textAout1Raw;
        private System.Windows.Forms.RadioButton radioAout3Enabled;
        private System.Windows.Forms.TextBox textAout2Raw;
        private System.Windows.Forms.RadioButton radioAout2Enabled;
        private System.Windows.Forms.TextBox textAout3Raw;
        private System.Windows.Forms.TextBox textAout4Raw;
        private System.Windows.Forms.TextBox textAout1;
        private System.Windows.Forms.TextBox textAout2;
        private System.Windows.Forms.ComboBox comboAout1Mode;
        private System.Windows.Forms.ComboBox comboAout2Mode;
        private System.Windows.Forms.ComboBox comboAout3Mode;
        private System.Windows.Forms.ComboBox comboAout4Mode;
        private System.Windows.Forms.ComboBox comboAinMode;
        private System.Windows.Forms.TextBox textAin4;
        private System.Windows.Forms.TextBox textAin3;
        private System.Windows.Forms.TextBox textAin2;
        private System.Windows.Forms.TextBox textAin1;
        private System.Windows.Forms.TextBox textAout3;
        private System.Windows.Forms.TextBox textAout4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonWS;
    }
}


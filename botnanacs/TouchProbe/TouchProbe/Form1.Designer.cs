namespace TouchProbe
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
            this.label1 = new System.Windows.Forms.Label();
            this.textSDOAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textSDOBusy = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textSDOError = new System.Windows.Forms.TextBox();
            this.textSDOData = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textTp1Pos1 = new System.Windows.Forms.TextBox();
            this.textTp1Pos2 = new System.Windows.Forms.TextBox();
            this.textTp2Pos2 = new System.Windows.Forms.TextBox();
            this.textTp2Pos1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.radioTp1Enabled = new System.Windows.Forms.RadioButton();
            this.radioTp2Enabled = new System.Windows.Forms.RadioButton();
            this.radioTp1HasRising = new System.Windows.Forms.RadioButton();
            this.radioTp2HasRising = new System.Windows.Forms.RadioButton();
            this.radioTp1HasFalling = new System.Windows.Forms.RadioButton();
            this.radioTp2HasFalling = new System.Windows.Forms.RadioButton();
            this.radioTp2Cont = new System.Windows.Forms.RadioButton();
            this.radioTp1Cont = new System.Windows.Forms.RadioButton();
            this.radioTp2Enable = new System.Windows.Forms.RadioButton();
            this.radioTp1Enable = new System.Windows.Forms.RadioButton();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.radioTp1Rising = new System.Windows.Forms.RadioButton();
            this.radioTp2Rising = new System.Windows.Forms.RadioButton();
            this.radioTp2Falling = new System.Windows.Forms.RadioButton();
            this.radioTp1Falling = new System.Windows.Forms.RadioButton();
            this.label13 = new System.Windows.Forms.Label();
            this.textRealPosition = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textDigitalInputs = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.textOPMode = new System.Windows.Forms.TextBox();
            this.textStatusWord = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonStartHoming = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textEvalute = new System.Windows.Forms.TextBox();
            this.buttonEvaluate = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(22, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Address (Hex)";
            // 
            // textSDOAddress
            // 
            this.textSDOAddress.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textSDOAddress.Location = new System.Drawing.Point(128, 21);
            this.textSDOAddress.Name = "textSDOAddress";
            this.textSDOAddress.Size = new System.Drawing.Size(89, 27);
            this.textSDOAddress.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(22, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Error";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(235, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "Data";
            // 
            // textSDOBusy
            // 
            this.textSDOBusy.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textSDOBusy.Location = new System.Drawing.Point(286, 62);
            this.textSDOBusy.Name = "textSDOBusy";
            this.textSDOBusy.Size = new System.Drawing.Size(89, 27);
            this.textSDOBusy.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(234, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "Busy";
            // 
            // textSDOError
            // 
            this.textSDOError.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textSDOError.Location = new System.Drawing.Point(128, 58);
            this.textSDOError.Name = "textSDOError";
            this.textSDOError.Size = new System.Drawing.Size(89, 27);
            this.textSDOError.TabIndex = 8;
            // 
            // textSDOData
            // 
            this.textSDOData.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textSDOData.Location = new System.Drawing.Point(286, 21);
            this.textSDOData.Name = "textSDOData";
            this.textSDOData.Size = new System.Drawing.Size(89, 27);
            this.textSDOData.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(185, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Touch Probe 1";
            // 
            // textTp1Pos1
            // 
            this.textTp1Pos1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textTp1Pos1.Location = new System.Drawing.Point(188, 153);
            this.textTp1Pos1.Name = "textTp1Pos1";
            this.textTp1Pos1.Size = new System.Drawing.Size(89, 27);
            this.textTp1Pos1.TabIndex = 1;
            // 
            // textTp1Pos2
            // 
            this.textTp1Pos2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textTp1Pos2.Location = new System.Drawing.Point(188, 194);
            this.textTp1Pos2.Name = "textTp1Pos2";
            this.textTp1Pos2.Size = new System.Drawing.Size(89, 27);
            this.textTp1Pos2.TabIndex = 1;
            // 
            // textTp2Pos2
            // 
            this.textTp2Pos2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textTp2Pos2.Location = new System.Drawing.Point(333, 194);
            this.textTp2Pos2.Name = "textTp2Pos2";
            this.textTp2Pos2.Size = new System.Drawing.Size(89, 27);
            this.textTp2Pos2.TabIndex = 13;
            // 
            // textTp2Pos1
            // 
            this.textTp2Pos1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textTp2Pos1.Location = new System.Drawing.Point(333, 153);
            this.textTp2Pos1.Name = "textTp2Pos1";
            this.textTp2Pos1.Size = new System.Drawing.Size(89, 27);
            this.textTp2Pos1.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(330, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "Touch Probe 2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(20, 156);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(138, 16);
            this.label8.TabIndex = 14;
            this.label8.Text = "Rising Edge Position";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.Location = new System.Drawing.Point(20, 197);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(141, 16);
            this.label9.TabIndex = 15;
            this.label9.Text = "Falling Edge Position";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label10.Location = new System.Drawing.Point(20, 49);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 16);
            this.label10.TabIndex = 16;
            this.label10.Text = "Enabled";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label11.Location = new System.Drawing.Point(20, 81);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(112, 16);
            this.label11.TabIndex = 17;
            this.label11.Text = "Has Rising Edge";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label12.Location = new System.Drawing.Point(20, 118);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(115, 16);
            this.label12.TabIndex = 18;
            this.label12.Text = "Has Falling Edge";
            // 
            // radioTp1Enabled
            // 
            this.radioTp1Enabled.AutoCheck = false;
            this.radioTp1Enabled.AutoSize = true;
            this.radioTp1Enabled.Location = new System.Drawing.Point(222, 52);
            this.radioTp1Enabled.Name = "radioTp1Enabled";
            this.radioTp1Enabled.Size = new System.Drawing.Size(14, 13);
            this.radioTp1Enabled.TabIndex = 19;
            this.radioTp1Enabled.TabStop = true;
            this.radioTp1Enabled.UseVisualStyleBackColor = true;
            // 
            // radioTp2Enabled
            // 
            this.radioTp2Enabled.AutoCheck = false;
            this.radioTp2Enabled.AutoSize = true;
            this.radioTp2Enabled.Location = new System.Drawing.Point(363, 52);
            this.radioTp2Enabled.Name = "radioTp2Enabled";
            this.radioTp2Enabled.Size = new System.Drawing.Size(14, 13);
            this.radioTp2Enabled.TabIndex = 20;
            this.radioTp2Enabled.TabStop = true;
            this.radioTp2Enabled.UseVisualStyleBackColor = true;
            // 
            // radioTp1HasRising
            // 
            this.radioTp1HasRising.AutoCheck = false;
            this.radioTp1HasRising.AutoSize = true;
            this.radioTp1HasRising.Location = new System.Drawing.Point(222, 84);
            this.radioTp1HasRising.Name = "radioTp1HasRising";
            this.radioTp1HasRising.Size = new System.Drawing.Size(14, 13);
            this.radioTp1HasRising.TabIndex = 21;
            this.radioTp1HasRising.TabStop = true;
            this.radioTp1HasRising.UseVisualStyleBackColor = true;
            // 
            // radioTp2HasRising
            // 
            this.radioTp2HasRising.AutoCheck = false;
            this.radioTp2HasRising.AutoSize = true;
            this.radioTp2HasRising.Location = new System.Drawing.Point(363, 84);
            this.radioTp2HasRising.Name = "radioTp2HasRising";
            this.radioTp2HasRising.Size = new System.Drawing.Size(14, 13);
            this.radioTp2HasRising.TabIndex = 22;
            this.radioTp2HasRising.TabStop = true;
            this.radioTp2HasRising.UseVisualStyleBackColor = true;
            // 
            // radioTp1HasFalling
            // 
            this.radioTp1HasFalling.AutoCheck = false;
            this.radioTp1HasFalling.AutoSize = true;
            this.radioTp1HasFalling.Location = new System.Drawing.Point(222, 121);
            this.radioTp1HasFalling.Name = "radioTp1HasFalling";
            this.radioTp1HasFalling.Size = new System.Drawing.Size(14, 13);
            this.radioTp1HasFalling.TabIndex = 23;
            this.radioTp1HasFalling.TabStop = true;
            this.radioTp1HasFalling.UseVisualStyleBackColor = true;
            // 
            // radioTp2HasFalling
            // 
            this.radioTp2HasFalling.AutoCheck = false;
            this.radioTp2HasFalling.AutoSize = true;
            this.radioTp2HasFalling.Location = new System.Drawing.Point(363, 121);
            this.radioTp2HasFalling.Name = "radioTp2HasFalling";
            this.radioTp2HasFalling.Size = new System.Drawing.Size(14, 13);
            this.radioTp2HasFalling.TabIndex = 24;
            this.radioTp2HasFalling.TabStop = true;
            this.radioTp2HasFalling.UseVisualStyleBackColor = true;
            // 
            // radioTp2Cont
            // 
            this.radioTp2Cont.AutoCheck = false;
            this.radioTp2Cont.AutoSize = true;
            this.radioTp2Cont.Location = new System.Drawing.Point(313, 84);
            this.radioTp2Cont.Name = "radioTp2Cont";
            this.radioTp2Cont.Size = new System.Drawing.Size(14, 13);
            this.radioTp2Cont.TabIndex = 39;
            this.radioTp2Cont.TabStop = true;
            this.radioTp2Cont.UseVisualStyleBackColor = true;
            this.radioTp2Cont.Click += new System.EventHandler(this.radioTp2Cont_Click);
            // 
            // radioTp1Cont
            // 
            this.radioTp1Cont.AutoCheck = false;
            this.radioTp1Cont.AutoSize = true;
            this.radioTp1Cont.Location = new System.Drawing.Point(184, 84);
            this.radioTp1Cont.Name = "radioTp1Cont";
            this.radioTp1Cont.Size = new System.Drawing.Size(14, 13);
            this.radioTp1Cont.TabIndex = 38;
            this.radioTp1Cont.TabStop = true;
            this.radioTp1Cont.UseVisualStyleBackColor = true;
            this.radioTp1Cont.Click += new System.EventHandler(this.radioTp1Cont_Click);
            // 
            // radioTp2Enable
            // 
            this.radioTp2Enable.AutoCheck = false;
            this.radioTp2Enable.AutoSize = true;
            this.radioTp2Enable.Location = new System.Drawing.Point(313, 52);
            this.radioTp2Enable.Name = "radioTp2Enable";
            this.radioTp2Enable.Size = new System.Drawing.Size(14, 13);
            this.radioTp2Enable.TabIndex = 37;
            this.radioTp2Enable.TabStop = true;
            this.radioTp2Enable.UseVisualStyleBackColor = true;
            this.radioTp2Enable.Click += new System.EventHandler(this.radioTp2Enable_Click);
            // 
            // radioTp1Enable
            // 
            this.radioTp1Enable.AutoCheck = false;
            this.radioTp1Enable.AutoSize = true;
            this.radioTp1Enable.Location = new System.Drawing.Point(184, 51);
            this.radioTp1Enable.Name = "radioTp1Enable";
            this.radioTp1Enable.Size = new System.Drawing.Size(14, 13);
            this.radioTp1Enable.TabIndex = 36;
            this.radioTp1Enable.TabStop = true;
            this.radioTp1Enable.UseVisualStyleBackColor = true;
            this.radioTp1Enable.Click += new System.EventHandler(this.radioTp1Enable_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label14.Location = new System.Drawing.Point(22, 81);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 16);
            this.label14.TabIndex = 34;
            this.label14.Text = "Continuous";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label15.Location = new System.Drawing.Point(22, 49);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 16);
            this.label15.TabIndex = 33;
            this.label15.Text = "Enable";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label17.Location = new System.Drawing.Point(22, 118);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(84, 16);
            this.label17.TabIndex = 31;
            this.label17.Text = "Rising Edge";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label18.Location = new System.Drawing.Point(275, 23);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(100, 16);
            this.label18.TabIndex = 28;
            this.label18.Text = "Touch Probe 2";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label19.Location = new System.Drawing.Point(461, 50);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(100, 16);
            this.label19.TabIndex = 25;
            this.label19.Text = "Touch Probe 1";
            // 
            // radioTp1Rising
            // 
            this.radioTp1Rising.AutoCheck = false;
            this.radioTp1Rising.AutoSize = true;
            this.radioTp1Rising.Location = new System.Drawing.Point(184, 121);
            this.radioTp1Rising.Name = "radioTp1Rising";
            this.radioTp1Rising.Size = new System.Drawing.Size(14, 13);
            this.radioTp1Rising.TabIndex = 42;
            this.radioTp1Rising.TabStop = true;
            this.radioTp1Rising.UseVisualStyleBackColor = true;
            this.radioTp1Rising.Click += new System.EventHandler(this.radioTp1Rising_Click);
            // 
            // radioTp2Rising
            // 
            this.radioTp2Rising.AutoCheck = false;
            this.radioTp2Rising.AutoSize = true;
            this.radioTp2Rising.Location = new System.Drawing.Point(313, 121);
            this.radioTp2Rising.Name = "radioTp2Rising";
            this.radioTp2Rising.Size = new System.Drawing.Size(14, 13);
            this.radioTp2Rising.TabIndex = 43;
            this.radioTp2Rising.TabStop = true;
            this.radioTp2Rising.UseVisualStyleBackColor = true;
            this.radioTp2Rising.Click += new System.EventHandler(this.radioTp2Rising_Click);
            // 
            // radioTp2Falling
            // 
            this.radioTp2Falling.AutoCheck = false;
            this.radioTp2Falling.AutoSize = true;
            this.radioTp2Falling.Location = new System.Drawing.Point(313, 159);
            this.radioTp2Falling.Name = "radioTp2Falling";
            this.radioTp2Falling.Size = new System.Drawing.Size(14, 13);
            this.radioTp2Falling.TabIndex = 46;
            this.radioTp2Falling.TabStop = true;
            this.radioTp2Falling.UseVisualStyleBackColor = true;
            this.radioTp2Falling.Click += new System.EventHandler(this.radioTp2Falling_Click);
            // 
            // radioTp1Falling
            // 
            this.radioTp1Falling.AutoCheck = false;
            this.radioTp1Falling.AutoSize = true;
            this.radioTp1Falling.Location = new System.Drawing.Point(184, 159);
            this.radioTp1Falling.Name = "radioTp1Falling";
            this.radioTp1Falling.Size = new System.Drawing.Size(14, 13);
            this.radioTp1Falling.TabIndex = 45;
            this.radioTp1Falling.TabStop = true;
            this.radioTp1Falling.UseVisualStyleBackColor = true;
            this.radioTp1Falling.Click += new System.EventHandler(this.radioTp1Falling_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label13.Location = new System.Drawing.Point(23, 156);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(83, 16);
            this.label13.TabIndex = 44;
            this.label13.Text = "Faling Edge";
            // 
            // textRealPosition
            // 
            this.textRealPosition.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textRealPosition.Location = new System.Drawing.Point(343, 21);
            this.textRealPosition.Name = "textRealPosition";
            this.textRealPosition.Size = new System.Drawing.Size(89, 27);
            this.textRealPosition.TabIndex = 1;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label16.Location = new System.Drawing.Point(235, 24);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(90, 16);
            this.label16.TabIndex = 14;
            this.label16.Text = "Real Position";
            // 
            // textDigitalInputs
            // 
            this.textDigitalInputs.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textDigitalInputs.Location = new System.Drawing.Point(343, 59);
            this.textDigitalInputs.Name = "textDigitalInputs";
            this.textDigitalInputs.Size = new System.Drawing.Size(89, 27);
            this.textDigitalInputs.TabIndex = 1;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label20.Location = new System.Drawing.Point(234, 62);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(93, 16);
            this.label20.TabIndex = 14;
            this.label20.Text = "Digital Inputs";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label21.Location = new System.Drawing.Point(22, 24);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(67, 16);
            this.label21.TabIndex = 49;
            this.label21.Text = "OP Mode";
            // 
            // textOPMode
            // 
            this.textOPMode.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textOPMode.Location = new System.Drawing.Point(128, 21);
            this.textOPMode.Name = "textOPMode";
            this.textOPMode.Size = new System.Drawing.Size(89, 27);
            this.textOPMode.TabIndex = 48;
            // 
            // textStatusWord
            // 
            this.textStatusWord.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textStatusWord.Location = new System.Drawing.Point(128, 54);
            this.textStatusWord.Name = "textStatusWord";
            this.textStatusWord.Size = new System.Drawing.Size(89, 27);
            this.textStatusWord.TabIndex = 48;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label22.Location = new System.Drawing.Point(22, 62);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(84, 16);
            this.label22.TabIndex = 49;
            this.label22.Text = "Status Word";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonStartHoming);
            this.groupBox1.Controls.Add(this.textRealPosition);
            this.groupBox1.Controls.Add(this.textStatusWord);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.textDigitalInputs);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.textOPMode);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(478, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(462, 151);
            this.groupBox1.TabIndex = 50;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Drive Status and Control";
            // 
            // buttonStartHoming
            // 
            this.buttonStartHoming.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonStartHoming.Location = new System.Drawing.Point(287, 102);
            this.buttonStartHoming.Name = "buttonStartHoming";
            this.buttonStartHoming.Size = new System.Drawing.Size(148, 30);
            this.buttonStartHoming.TabIndex = 47;
            this.buttonStartHoming.Text = "Start Homing";
            this.buttonStartHoming.UseVisualStyleBackColor = true;
            this.buttonStartHoming.Click += new System.EventHandler(this.buttonStartHoming_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textSDOAddress);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textSDOData);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textSDOError);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textSDOBusy);
            this.groupBox2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox2.Location = new System.Drawing.Point(14, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(443, 100);
            this.groupBox2.TabIndex = 51;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SDOs Information";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.radioTp2Enabled);
            this.groupBox3.Controls.Add(this.radioTp1Enabled);
            this.groupBox3.Controls.Add(this.radioTp1HasRising);
            this.groupBox3.Controls.Add(this.radioTp2HasRising);
            this.groupBox3.Controls.Add(this.radioTp1HasFalling);
            this.groupBox3.Controls.Add(this.radioTp2HasFalling);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.textTp1Pos1);
            this.groupBox3.Controls.Add(this.textTp2Pos1);
            this.groupBox3.Controls.Add(this.textTp1Pos2);
            this.groupBox3.Controls.Add(this.textTp2Pos2);
            this.groupBox3.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox3.Location = new System.Drawing.Point(14, 129);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(443, 242);
            this.groupBox3.TabIndex = 52;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Touch Probe Function Status";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.radioTp2Falling);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.radioTp1Falling);
            this.groupBox4.Controls.Add(this.radioTp2Enable);
            this.groupBox4.Controls.Add(this.radioTp2Rising);
            this.groupBox4.Controls.Add(this.radioTp1Enable);
            this.groupBox4.Controls.Add(this.radioTp1Rising);
            this.groupBox4.Controls.Add(this.radioTp1Cont);
            this.groupBox4.Controls.Add(this.radioTp2Cont);
            this.groupBox4.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox4.Location = new System.Drawing.Point(478, 177);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(462, 194);
            this.groupBox4.TabIndex = 53;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Touch Probe Function Setting";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(147, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 16);
            this.label4.TabIndex = 28;
            this.label4.Text = "Touch Probe 1";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textEvalute);
            this.groupBox5.Controls.Add(this.buttonEvaluate);
            this.groupBox5.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox5.Location = new System.Drawing.Point(482, 390);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(458, 85);
            this.groupBox5.TabIndex = 80;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Script Evaluate";
            // 
            // textEvalute
            // 
            this.textEvalute.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textEvalute.Location = new System.Drawing.Point(8, 30);
            this.textEvalute.Margin = new System.Windows.Forms.Padding(5);
            this.textEvalute.Name = "textEvalute";
            this.textEvalute.Size = new System.Drawing.Size(343, 27);
            this.textEvalute.TabIndex = 43;
            // 
            // buttonEvaluate
            // 
            this.buttonEvaluate.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonEvaluate.Location = new System.Drawing.Point(361, 28);
            this.buttonEvaluate.Margin = new System.Windows.Forms.Padding(5);
            this.buttonEvaluate.Name = "buttonEvaluate";
            this.buttonEvaluate.Size = new System.Drawing.Size(89, 27);
            this.buttonEvaluate.TabIndex = 44;
            this.buttonEvaluate.Text = "Send";
            this.buttonEvaluate.UseVisualStyleBackColor = true;
            this.buttonEvaluate.Click += new System.EventHandler(this.buttonEvaluate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 554);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textSDOAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textSDOBusy;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textSDOError;
        private System.Windows.Forms.TextBox textSDOData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textTp1Pos1;
        private System.Windows.Forms.TextBox textTp1Pos2;
        private System.Windows.Forms.TextBox textTp2Pos2;
        private System.Windows.Forms.TextBox textTp2Pos1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton radioTp1Enabled;
        private System.Windows.Forms.RadioButton radioTp2Enabled;
        private System.Windows.Forms.RadioButton radioTp1HasRising;
        private System.Windows.Forms.RadioButton radioTp2HasRising;
        private System.Windows.Forms.RadioButton radioTp1HasFalling;
        private System.Windows.Forms.RadioButton radioTp2HasFalling;
        private System.Windows.Forms.RadioButton radioTp2Cont;
        private System.Windows.Forms.RadioButton radioTp1Cont;
        private System.Windows.Forms.RadioButton radioTp2Enable;
        private System.Windows.Forms.RadioButton radioTp1Enable;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.RadioButton radioTp1Rising;
        private System.Windows.Forms.RadioButton radioTp2Rising;
        private System.Windows.Forms.RadioButton radioTp2Falling;
        private System.Windows.Forms.RadioButton radioTp1Falling;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textRealPosition;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textDigitalInputs;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textOPMode;
        private System.Windows.Forms.TextBox textStatusWord;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonStartHoming;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox textEvalute;
        private System.Windows.Forms.Button buttonEvaluate;
    }
}


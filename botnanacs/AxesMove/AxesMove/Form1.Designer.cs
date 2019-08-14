namespace AxesMove
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
            this.timerWSCheck = new System.Windows.Forms.Timer(this.components);
            this.buttonWS = new System.Windows.Forms.Button();
            this.textAxis1P1 = new System.Windows.Forms.TextBox();
            this.textAxis1P2 = new System.Windows.Forms.TextBox();
            this.textAxis1P3 = new System.Windows.Forms.TextBox();
            this.textAxis1P4 = new System.Windows.Forms.TextBox();
            this.textAxis1P5 = new System.Windows.Forms.TextBox();
            this.textAxis1V1 = new System.Windows.Forms.TextBox();
            this.textAxis1V2 = new System.Windows.Forms.TextBox();
            this.textAxis1V3 = new System.Windows.Forms.TextBox();
            this.textAxis1V4 = new System.Windows.Forms.TextBox();
            this.textAxis1V5 = new System.Windows.Forms.TextBox();
            this.timerLoop = new System.Windows.Forms.Timer(this.components);
            this.textAxis1Cmd = new System.Windows.Forms.TextBox();
            this.textAxis1Demand = new System.Windows.Forms.TextBox();
            this.textAxis1Feedback = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.radioAxis1Interpolator = new System.Windows.Forms.RadioButton();
            this.radioAxis1Reached = new System.Windows.Forms.RadioButton();
            this.radioAxis1Running = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonAxis1Stop = new System.Windows.Forms.Button();
            this.buttonAxis1Start = new System.Windows.Forms.Button();
            this.textAxis1SectionLen = new System.Windows.Forms.TextBox();
            this.buttonCoordinator = new System.Windows.Forms.Button();
            this.buttonErrorAck = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.radioAxis2Interpolator = new System.Windows.Forms.RadioButton();
            this.radioAxis2Reached = new System.Windows.Forms.RadioButton();
            this.radioAxis2Running = new System.Windows.Forms.RadioButton();
            this.textAxis2Cmd = new System.Windows.Forms.TextBox();
            this.textAxis2Feedback = new System.Windows.Forms.TextBox();
            this.textAxis2Demand = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label54 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.buttonAxis2Stop = new System.Windows.Forms.Button();
            this.buttonAxis2Start = new System.Windows.Forms.Button();
            this.textAxis2SectionLen = new System.Windows.Forms.TextBox();
            this.textAxis2P1 = new System.Windows.Forms.TextBox();
            this.textAxis2P2 = new System.Windows.Forms.TextBox();
            this.textAxis2V5 = new System.Windows.Forms.TextBox();
            this.textAxis2P3 = new System.Windows.Forms.TextBox();
            this.textAxis2V4 = new System.Windows.Forms.TextBox();
            this.textAxis2P4 = new System.Windows.Forms.TextBox();
            this.textAxis2V3 = new System.Windows.Forms.TextBox();
            this.textAxis2P5 = new System.Windows.Forms.TextBox();
            this.textAxis2V2 = new System.Windows.Forms.TextBox();
            this.textAxis2V1 = new System.Windows.Forms.TextBox();
            this.buttonSFCReload = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerWSCheck
            // 
            this.timerWSCheck.Tick += new System.EventHandler(this.timerWSCheck_Tick);
            // 
            // buttonWS
            // 
            this.buttonWS.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonWS.Location = new System.Drawing.Point(718, 25);
            this.buttonWS.Name = "buttonWS";
            this.buttonWS.Size = new System.Drawing.Size(96, 27);
            this.buttonWS.TabIndex = 0;
            this.buttonWS.Text = "WS State";
            this.buttonWS.UseVisualStyleBackColor = true;
            // 
            // textAxis1P1
            // 
            this.textAxis1P1.Location = new System.Drawing.Point(36, 26);
            this.textAxis1P1.Name = "textAxis1P1";
            this.textAxis1P1.Size = new System.Drawing.Size(75, 27);
            this.textAxis1P1.TabIndex = 2;
            this.textAxis1P1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textAxis1P1.Enter += new System.EventHandler(this.textAxis1P1_Enter);
            this.textAxis1P1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAxis1P1_KeyDown);
            this.textAxis1P1.Leave += new System.EventHandler(this.textAxis1P1_Leave);
            // 
            // textAxis1P2
            // 
            this.textAxis1P2.Location = new System.Drawing.Point(36, 54);
            this.textAxis1P2.Name = "textAxis1P2";
            this.textAxis1P2.Size = new System.Drawing.Size(75, 27);
            this.textAxis1P2.TabIndex = 3;
            this.textAxis1P2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textAxis1P2.Enter += new System.EventHandler(this.textAxis1P2_Enter);
            this.textAxis1P2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAxis1P2_KeyDown);
            this.textAxis1P2.Leave += new System.EventHandler(this.textAxis1P2_Leave);
            // 
            // textAxis1P3
            // 
            this.textAxis1P3.Location = new System.Drawing.Point(36, 82);
            this.textAxis1P3.Name = "textAxis1P3";
            this.textAxis1P3.Size = new System.Drawing.Size(75, 27);
            this.textAxis1P3.TabIndex = 4;
            this.textAxis1P3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textAxis1P3.Enter += new System.EventHandler(this.textAxis1P3_Enter);
            this.textAxis1P3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAxis1P3_KeyDown);
            this.textAxis1P3.Leave += new System.EventHandler(this.textAxis1P3_Leave);
            // 
            // textAxis1P4
            // 
            this.textAxis1P4.Location = new System.Drawing.Point(36, 110);
            this.textAxis1P4.Name = "textAxis1P4";
            this.textAxis1P4.Size = new System.Drawing.Size(75, 27);
            this.textAxis1P4.TabIndex = 5;
            this.textAxis1P4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textAxis1P4.Enter += new System.EventHandler(this.textAxis1P4_Enter);
            this.textAxis1P4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAxis1P4_KeyDown);
            this.textAxis1P4.Leave += new System.EventHandler(this.textAxis1P4_Leave);
            // 
            // textAxis1P5
            // 
            this.textAxis1P5.Location = new System.Drawing.Point(36, 138);
            this.textAxis1P5.Name = "textAxis1P5";
            this.textAxis1P5.Size = new System.Drawing.Size(75, 27);
            this.textAxis1P5.TabIndex = 6;
            this.textAxis1P5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textAxis1P5.Enter += new System.EventHandler(this.textAxis1P5_Enter);
            this.textAxis1P5.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAxis1P5_KeyDown);
            this.textAxis1P5.Leave += new System.EventHandler(this.textAxis1P5_Leave);
            // 
            // textAxis1V1
            // 
            this.textAxis1V1.Location = new System.Drawing.Point(201, 26);
            this.textAxis1V1.Name = "textAxis1V1";
            this.textAxis1V1.Size = new System.Drawing.Size(75, 27);
            this.textAxis1V1.TabIndex = 7;
            this.textAxis1V1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textAxis1V1.Enter += new System.EventHandler(this.textAxis1V1_Enter);
            this.textAxis1V1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAxis1V1_KeyDown);
            this.textAxis1V1.Leave += new System.EventHandler(this.textAxis2V1_Leave);
            // 
            // textAxis1V2
            // 
            this.textAxis1V2.Location = new System.Drawing.Point(201, 54);
            this.textAxis1V2.Name = "textAxis1V2";
            this.textAxis1V2.Size = new System.Drawing.Size(75, 27);
            this.textAxis1V2.TabIndex = 8;
            this.textAxis1V2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textAxis1V2.Enter += new System.EventHandler(this.textAxis1V2_Enter);
            this.textAxis1V2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAxis1V2_KeyDown);
            this.textAxis1V2.Leave += new System.EventHandler(this.textAxis1V2_Leave);
            // 
            // textAxis1V3
            // 
            this.textAxis1V3.Location = new System.Drawing.Point(201, 82);
            this.textAxis1V3.Name = "textAxis1V3";
            this.textAxis1V3.Size = new System.Drawing.Size(75, 27);
            this.textAxis1V3.TabIndex = 9;
            this.textAxis1V3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textAxis1V3.Enter += new System.EventHandler(this.textAxis1V3_Enter);
            this.textAxis1V3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAxis1V3_KeyDown);
            this.textAxis1V3.Leave += new System.EventHandler(this.textAxis1V3_Leave);
            // 
            // textAxis1V4
            // 
            this.textAxis1V4.Location = new System.Drawing.Point(201, 110);
            this.textAxis1V4.Name = "textAxis1V4";
            this.textAxis1V4.Size = new System.Drawing.Size(75, 27);
            this.textAxis1V4.TabIndex = 10;
            this.textAxis1V4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textAxis1V4.Enter += new System.EventHandler(this.textAxis1V4_Enter);
            this.textAxis1V4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAxis1V4_KeyDown);
            this.textAxis1V4.Leave += new System.EventHandler(this.textAxis1V4_Leave);
            // 
            // textAxis1V5
            // 
            this.textAxis1V5.Location = new System.Drawing.Point(201, 138);
            this.textAxis1V5.Name = "textAxis1V5";
            this.textAxis1V5.Size = new System.Drawing.Size(75, 27);
            this.textAxis1V5.TabIndex = 11;
            this.textAxis1V5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textAxis1V5.Enter += new System.EventHandler(this.textAxis1V5_Enter);
            this.textAxis1V5.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAxis1V5_KeyDown);
            this.textAxis1V5.Leave += new System.EventHandler(this.textAxis1V5_Leave);
            // 
            // timerLoop
            // 
            this.timerLoop.Tick += new System.EventHandler(this.timerLoop_Tick);
            // 
            // textAxis1Cmd
            // 
            this.textAxis1Cmd.Location = new System.Drawing.Point(72, 21);
            this.textAxis1Cmd.Name = "textAxis1Cmd";
            this.textAxis1Cmd.Size = new System.Drawing.Size(85, 27);
            this.textAxis1Cmd.TabIndex = 12;
            this.textAxis1Cmd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textAxis1Demand
            // 
            this.textAxis1Demand.Location = new System.Drawing.Point(72, 49);
            this.textAxis1Demand.Name = "textAxis1Demand";
            this.textAxis1Demand.Size = new System.Drawing.Size(85, 27);
            this.textAxis1Demand.TabIndex = 13;
            this.textAxis1Demand.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textAxis1Feedback
            // 
            this.textAxis1Feedback.Location = new System.Drawing.Point(72, 77);
            this.textAxis1Feedback.Name = "textAxis1Feedback";
            this.textAxis1Feedback.Size = new System.Drawing.Size(85, 27);
            this.textAxis1Feedback.TabIndex = 14;
            this.textAxis1Feedback.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.radioAxis1Interpolator);
            this.groupBox1.Controls.Add(this.radioAxis1Reached);
            this.groupBox1.Controls.Add(this.radioAxis1Running);
            this.groupBox1.Controls.Add(this.textAxis1Cmd);
            this.groupBox1.Controls.Add(this.textAxis1Feedback);
            this.groupBox1.Controls.Add(this.textAxis1Demand);
            this.groupBox1.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(347, 112);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Axis 1 Status";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(163, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 16);
            this.label6.TabIndex = 27;
            this.label6.Text = "mm";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(163, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 16);
            this.label5.TabIndex = 26;
            this.label5.Text = "mm";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(163, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 16);
            this.label4.TabIndex = 25;
            this.label4.Text = "mm";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 16);
            this.label3.TabIndex = 24;
            this.label3.Text = "Feedback";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 23;
            this.label2.Text = "Demand";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 22;
            this.label1.Text = "Target";
            // 
            // radioAxis1Interpolator
            // 
            this.radioAxis1Interpolator.AutoCheck = false;
            this.radioAxis1Interpolator.AutoSize = true;
            this.radioAxis1Interpolator.Location = new System.Drawing.Point(212, 77);
            this.radioAxis1Interpolator.Name = "radioAxis1Interpolator";
            this.radioAxis1Interpolator.Size = new System.Drawing.Size(99, 20);
            this.radioAxis1Interpolator.TabIndex = 17;
            this.radioAxis1Interpolator.TabStop = true;
            this.radioAxis1Interpolator.Text = "Interpolator";
            this.radioAxis1Interpolator.UseVisualStyleBackColor = true;
            // 
            // radioAxis1Reached
            // 
            this.radioAxis1Reached.AutoCheck = false;
            this.radioAxis1Reached.AutoSize = true;
            this.radioAxis1Reached.Location = new System.Drawing.Point(212, 51);
            this.radioAxis1Reached.Name = "radioAxis1Reached";
            this.radioAxis1Reached.Size = new System.Drawing.Size(80, 20);
            this.radioAxis1Reached.TabIndex = 16;
            this.radioAxis1Reached.TabStop = true;
            this.radioAxis1Reached.Text = "Reached";
            this.radioAxis1Reached.UseVisualStyleBackColor = true;
            // 
            // radioAxis1Running
            // 
            this.radioAxis1Running.AutoCheck = false;
            this.radioAxis1Running.AutoSize = true;
            this.radioAxis1Running.Location = new System.Drawing.Point(212, 22);
            this.radioAxis1Running.Name = "radioAxis1Running";
            this.radioAxis1Running.Size = new System.Drawing.Size(80, 20);
            this.radioAxis1Running.TabIndex = 15;
            this.radioAxis1Running.TabStop = true;
            this.radioAxis1Running.Text = "Running";
            this.radioAxis1Running.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label27);
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.buttonAxis1Stop);
            this.groupBox2.Controls.Add(this.buttonAxis1Start);
            this.groupBox2.Controls.Add(this.textAxis1SectionLen);
            this.groupBox2.Controls.Add(this.textAxis1P1);
            this.groupBox2.Controls.Add(this.textAxis1P2);
            this.groupBox2.Controls.Add(this.textAxis1V5);
            this.groupBox2.Controls.Add(this.textAxis1P3);
            this.groupBox2.Controls.Add(this.textAxis1V4);
            this.groupBox2.Controls.Add(this.textAxis1P4);
            this.groupBox2.Controls.Add(this.textAxis1V3);
            this.groupBox2.Controls.Add(this.textAxis1P5);
            this.groupBox2.Controls.Add(this.textAxis1V2);
            this.groupBox2.Controls.Add(this.textAxis1V1);
            this.groupBox2.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox2.Location = new System.Drawing.Point(12, 132);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(347, 228);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Axis 1 Operation";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(8, 184);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(82, 16);
            this.label27.TabIndex = 43;
            this.label27.Text = "Section 1 ~ ";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(281, 149);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(60, 16);
            this.label26.TabIndex = 42;
            this.label26.Text = "mm/min";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(281, 113);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(60, 16);
            this.label25.TabIndex = 41;
            this.label25.Text = "mm/min";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(282, 85);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(60, 16);
            this.label24.TabIndex = 40;
            this.label24.Text = "mm/min";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(281, 57);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(60, 16);
            this.label23.TabIndex = 39;
            this.label23.Text = "mm/min";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(282, 29);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(60, 16);
            this.label22.TabIndex = 38;
            this.label22.Text = "mm/min";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(168, 141);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(27, 16);
            this.label21.TabIndex = 37;
            this.label21.Text = "V5";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(168, 113);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(27, 16);
            this.label20.TabIndex = 36;
            this.label20.Text = "V4";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(168, 85);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(27, 16);
            this.label19.TabIndex = 35;
            this.label19.Text = "V3";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(168, 57);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(27, 16);
            this.label18.TabIndex = 34;
            this.label18.Text = "V2";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(168, 29);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(27, 16);
            this.label17.TabIndex = 33;
            this.label17.Text = "V1";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(117, 141);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(32, 16);
            this.label16.TabIndex = 32;
            this.label16.Text = "mm";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(117, 113);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(32, 16);
            this.label15.TabIndex = 31;
            this.label15.Text = "mm";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(117, 85);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(32, 16);
            this.label14.TabIndex = 30;
            this.label14.Text = "mm";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(117, 57);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 16);
            this.label13.TabIndex = 29;
            this.label13.Text = "mm";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(117, 29);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 16);
            this.label12.TabIndex = 28;
            this.label12.Text = "mm";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 141);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(24, 16);
            this.label11.TabIndex = 27;
            this.label11.Text = "P5";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 113);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(24, 16);
            this.label10.TabIndex = 26;
            this.label10.Text = "P4";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 85);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 16);
            this.label9.TabIndex = 25;
            this.label9.Text = "P3";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 16);
            this.label8.TabIndex = 24;
            this.label8.Text = "P2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 16);
            this.label7.TabIndex = 23;
            this.label7.Text = "P1";
            // 
            // buttonAxis1Stop
            // 
            this.buttonAxis1Stop.Location = new System.Drawing.Point(224, 181);
            this.buttonAxis1Stop.Name = "buttonAxis1Stop";
            this.buttonAxis1Stop.Size = new System.Drawing.Size(82, 27);
            this.buttonAxis1Stop.TabIndex = 14;
            this.buttonAxis1Stop.Text = "Stop";
            this.buttonAxis1Stop.UseVisualStyleBackColor = true;
            this.buttonAxis1Stop.Click += new System.EventHandler(this.buttonAxis1Stop_Click);
            // 
            // buttonAxis1Start
            // 
            this.buttonAxis1Start.Location = new System.Drawing.Point(136, 181);
            this.buttonAxis1Start.Name = "buttonAxis1Start";
            this.buttonAxis1Start.Size = new System.Drawing.Size(82, 27);
            this.buttonAxis1Start.TabIndex = 13;
            this.buttonAxis1Start.Text = "Start";
            this.buttonAxis1Start.UseVisualStyleBackColor = true;
            this.buttonAxis1Start.Click += new System.EventHandler(this.buttonAxis1Start_Click);
            // 
            // textAxis1SectionLen
            // 
            this.textAxis1SectionLen.Location = new System.Drawing.Point(96, 181);
            this.textAxis1SectionLen.Name = "textAxis1SectionLen";
            this.textAxis1SectionLen.Size = new System.Drawing.Size(34, 27);
            this.textAxis1SectionLen.TabIndex = 12;
            this.textAxis1SectionLen.Text = "1";
            this.textAxis1SectionLen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // buttonCoordinator
            // 
            this.buttonCoordinator.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonCoordinator.Location = new System.Drawing.Point(718, 60);
            this.buttonCoordinator.Name = "buttonCoordinator";
            this.buttonCoordinator.Size = new System.Drawing.Size(96, 27);
            this.buttonCoordinator.TabIndex = 18;
            this.buttonCoordinator.Text = "Coordinator";
            this.buttonCoordinator.UseVisualStyleBackColor = true;
            this.buttonCoordinator.Click += new System.EventHandler(this.buttonCoordinator_Click);
            // 
            // buttonErrorAck
            // 
            this.buttonErrorAck.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonErrorAck.Location = new System.Drawing.Point(718, 97);
            this.buttonErrorAck.Name = "buttonErrorAck";
            this.buttonErrorAck.Size = new System.Drawing.Size(96, 27);
            this.buttonErrorAck.TabIndex = 19;
            this.buttonErrorAck.Text = "Acked";
            this.buttonErrorAck.UseVisualStyleBackColor = true;
            this.buttonErrorAck.Click += new System.EventHandler(this.ErrorAck_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label33);
            this.groupBox3.Controls.Add(this.label32);
            this.groupBox3.Controls.Add(this.label31);
            this.groupBox3.Controls.Add(this.label28);
            this.groupBox3.Controls.Add(this.label29);
            this.groupBox3.Controls.Add(this.label30);
            this.groupBox3.Controls.Add(this.radioAxis2Interpolator);
            this.groupBox3.Controls.Add(this.radioAxis2Reached);
            this.groupBox3.Controls.Add(this.radioAxis2Running);
            this.groupBox3.Controls.Add(this.textAxis2Cmd);
            this.groupBox3.Controls.Add(this.textAxis2Feedback);
            this.groupBox3.Controls.Add(this.textAxis2Demand);
            this.groupBox3.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox3.Location = new System.Drawing.Point(365, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(347, 112);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Axis 2 Status";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(168, 81);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(32, 16);
            this.label33.TabIndex = 36;
            this.label33.Text = "mm";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(168, 52);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(32, 16);
            this.label32.TabIndex = 35;
            this.label32.Text = "mm";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(168, 26);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(32, 16);
            this.label31.TabIndex = 34;
            this.label31.Text = "mm";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(6, 80);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(68, 16);
            this.label28.TabIndex = 33;
            this.label28.Text = "Feedback";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(6, 53);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(61, 16);
            this.label29.TabIndex = 32;
            this.label29.Text = "Demand";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(6, 24);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(48, 16);
            this.label30.TabIndex = 31;
            this.label30.Text = "Target";
            // 
            // radioAxis2Interpolator
            // 
            this.radioAxis2Interpolator.AutoCheck = false;
            this.radioAxis2Interpolator.AutoSize = true;
            this.radioAxis2Interpolator.Location = new System.Drawing.Point(225, 76);
            this.radioAxis2Interpolator.Name = "radioAxis2Interpolator";
            this.radioAxis2Interpolator.Size = new System.Drawing.Size(99, 20);
            this.radioAxis2Interpolator.TabIndex = 19;
            this.radioAxis2Interpolator.TabStop = true;
            this.radioAxis2Interpolator.Text = "Interpolator";
            this.radioAxis2Interpolator.UseVisualStyleBackColor = true;
            // 
            // radioAxis2Reached
            // 
            this.radioAxis2Reached.AutoCheck = false;
            this.radioAxis2Reached.AutoSize = true;
            this.radioAxis2Reached.Location = new System.Drawing.Point(226, 51);
            this.radioAxis2Reached.Name = "radioAxis2Reached";
            this.radioAxis2Reached.Size = new System.Drawing.Size(80, 20);
            this.radioAxis2Reached.TabIndex = 18;
            this.radioAxis2Reached.TabStop = true;
            this.radioAxis2Reached.Text = "Reached";
            this.radioAxis2Reached.UseVisualStyleBackColor = true;
            // 
            // radioAxis2Running
            // 
            this.radioAxis2Running.AutoCheck = false;
            this.radioAxis2Running.AutoSize = true;
            this.radioAxis2Running.Location = new System.Drawing.Point(225, 29);
            this.radioAxis2Running.Name = "radioAxis2Running";
            this.radioAxis2Running.Size = new System.Drawing.Size(80, 20);
            this.radioAxis2Running.TabIndex = 15;
            this.radioAxis2Running.TabStop = true;
            this.radioAxis2Running.Text = "Running";
            this.radioAxis2Running.UseVisualStyleBackColor = true;
            // 
            // textAxis2Cmd
            // 
            this.textAxis2Cmd.Location = new System.Drawing.Point(77, 21);
            this.textAxis2Cmd.Name = "textAxis2Cmd";
            this.textAxis2Cmd.Size = new System.Drawing.Size(85, 27);
            this.textAxis2Cmd.TabIndex = 12;
            // 
            // textAxis2Feedback
            // 
            this.textAxis2Feedback.Location = new System.Drawing.Point(77, 77);
            this.textAxis2Feedback.Name = "textAxis2Feedback";
            this.textAxis2Feedback.Size = new System.Drawing.Size(85, 27);
            this.textAxis2Feedback.TabIndex = 14;
            // 
            // textAxis2Demand
            // 
            this.textAxis2Demand.Location = new System.Drawing.Point(77, 49);
            this.textAxis2Demand.Name = "textAxis2Demand";
            this.textAxis2Demand.Size = new System.Drawing.Size(85, 27);
            this.textAxis2Demand.TabIndex = 13;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label54);
            this.groupBox4.Controls.Add(this.label49);
            this.groupBox4.Controls.Add(this.label50);
            this.groupBox4.Controls.Add(this.label51);
            this.groupBox4.Controls.Add(this.label52);
            this.groupBox4.Controls.Add(this.label53);
            this.groupBox4.Controls.Add(this.label44);
            this.groupBox4.Controls.Add(this.label45);
            this.groupBox4.Controls.Add(this.label46);
            this.groupBox4.Controls.Add(this.label47);
            this.groupBox4.Controls.Add(this.label48);
            this.groupBox4.Controls.Add(this.label39);
            this.groupBox4.Controls.Add(this.label40);
            this.groupBox4.Controls.Add(this.label41);
            this.groupBox4.Controls.Add(this.label42);
            this.groupBox4.Controls.Add(this.label43);
            this.groupBox4.Controls.Add(this.label34);
            this.groupBox4.Controls.Add(this.label35);
            this.groupBox4.Controls.Add(this.label36);
            this.groupBox4.Controls.Add(this.label37);
            this.groupBox4.Controls.Add(this.label38);
            this.groupBox4.Controls.Add(this.buttonAxis2Stop);
            this.groupBox4.Controls.Add(this.buttonAxis2Start);
            this.groupBox4.Controls.Add(this.textAxis2SectionLen);
            this.groupBox4.Controls.Add(this.textAxis2P1);
            this.groupBox4.Controls.Add(this.textAxis2P2);
            this.groupBox4.Controls.Add(this.textAxis2V5);
            this.groupBox4.Controls.Add(this.textAxis2P3);
            this.groupBox4.Controls.Add(this.textAxis2V4);
            this.groupBox4.Controls.Add(this.textAxis2P4);
            this.groupBox4.Controls.Add(this.textAxis2V3);
            this.groupBox4.Controls.Add(this.textAxis2P5);
            this.groupBox4.Controls.Add(this.textAxis2V2);
            this.groupBox4.Controls.Add(this.textAxis2V1);
            this.groupBox4.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox4.Location = new System.Drawing.Point(365, 130);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(347, 230);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Axis 2 Operation";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(23, 188);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(82, 16);
            this.label54.TabIndex = 48;
            this.label54.Text = "Section 1 ~ ";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(279, 154);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(60, 16);
            this.label49.TabIndex = 47;
            this.label49.Text = "mm/min";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(279, 118);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(60, 16);
            this.label50.TabIndex = 46;
            this.label50.Text = "mm/min";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(280, 90);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(60, 16);
            this.label51.TabIndex = 45;
            this.label51.Text = "mm/min";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(279, 62);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(60, 16);
            this.label52.TabIndex = 44;
            this.label52.Text = "mm/min";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(280, 34);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(60, 16);
            this.label53.TabIndex = 43;
            this.label53.Text = "mm/min";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(161, 143);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(27, 16);
            this.label44.TabIndex = 42;
            this.label44.Text = "V5";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(161, 115);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(27, 16);
            this.label45.TabIndex = 41;
            this.label45.Text = "V4";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(161, 87);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(27, 16);
            this.label46.TabIndex = 40;
            this.label46.Text = "V3";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(161, 59);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(27, 16);
            this.label47.TabIndex = 39;
            this.label47.Text = "V2";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(161, 31);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(27, 16);
            this.label48.TabIndex = 38;
            this.label48.Text = "V1";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(117, 143);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(32, 16);
            this.label39.TabIndex = 37;
            this.label39.Text = "mm";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(117, 115);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(32, 16);
            this.label40.TabIndex = 36;
            this.label40.Text = "mm";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(117, 87);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(32, 16);
            this.label41.TabIndex = 35;
            this.label41.Text = "mm";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(117, 59);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(32, 16);
            this.label42.TabIndex = 34;
            this.label42.Text = "mm";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(117, 31);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(32, 16);
            this.label43.TabIndex = 33;
            this.label43.Text = "mm";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(6, 143);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(24, 16);
            this.label34.TabIndex = 32;
            this.label34.Text = "P5";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(6, 115);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(24, 16);
            this.label35.TabIndex = 31;
            this.label35.Text = "P4";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(6, 87);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(24, 16);
            this.label36.TabIndex = 30;
            this.label36.Text = "P3";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(6, 59);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(24, 16);
            this.label37.TabIndex = 29;
            this.label37.Text = "P2";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(6, 31);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(24, 16);
            this.label38.TabIndex = 28;
            this.label38.Text = "P1";
            // 
            // buttonAxis2Stop
            // 
            this.buttonAxis2Stop.Location = new System.Drawing.Point(252, 183);
            this.buttonAxis2Stop.Name = "buttonAxis2Stop";
            this.buttonAxis2Stop.Size = new System.Drawing.Size(82, 27);
            this.buttonAxis2Stop.TabIndex = 14;
            this.buttonAxis2Stop.Text = "Stop";
            this.buttonAxis2Stop.UseVisualStyleBackColor = true;
            this.buttonAxis2Stop.Click += new System.EventHandler(this.buttonAxis2Stop_Click);
            // 
            // buttonAxis2Start
            // 
            this.buttonAxis2Start.Location = new System.Drawing.Point(164, 184);
            this.buttonAxis2Start.Name = "buttonAxis2Start";
            this.buttonAxis2Start.Size = new System.Drawing.Size(82, 27);
            this.buttonAxis2Start.TabIndex = 13;
            this.buttonAxis2Start.Text = "Start";
            this.buttonAxis2Start.UseVisualStyleBackColor = true;
            this.buttonAxis2Start.Click += new System.EventHandler(this.buttonAxis2Start_Click);
            // 
            // textAxis2SectionLen
            // 
            this.textAxis2SectionLen.Location = new System.Drawing.Point(111, 186);
            this.textAxis2SectionLen.Name = "textAxis2SectionLen";
            this.textAxis2SectionLen.Size = new System.Drawing.Size(34, 27);
            this.textAxis2SectionLen.TabIndex = 12;
            this.textAxis2SectionLen.Text = "1";
            this.textAxis2SectionLen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textAxis2P1
            // 
            this.textAxis2P1.Location = new System.Drawing.Point(36, 28);
            this.textAxis2P1.Name = "textAxis2P1";
            this.textAxis2P1.Size = new System.Drawing.Size(75, 27);
            this.textAxis2P1.TabIndex = 2;
            this.textAxis2P1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textAxis2P1.Enter += new System.EventHandler(this.textAxis2P1_Enter);
            this.textAxis2P1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAxis2P1_KeyDown);
            this.textAxis2P1.Leave += new System.EventHandler(this.textAxis2P1_Leave);
            // 
            // textAxis2P2
            // 
            this.textAxis2P2.Location = new System.Drawing.Point(36, 56);
            this.textAxis2P2.Name = "textAxis2P2";
            this.textAxis2P2.Size = new System.Drawing.Size(75, 27);
            this.textAxis2P2.TabIndex = 3;
            this.textAxis2P2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textAxis2P2.Enter += new System.EventHandler(this.textAxis2P2_Enter);
            this.textAxis2P2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAxis2P2_KeyDown);
            this.textAxis2P2.Leave += new System.EventHandler(this.textAxis2P2_Leave);
            // 
            // textAxis2V5
            // 
            this.textAxis2V5.Location = new System.Drawing.Point(199, 143);
            this.textAxis2V5.Name = "textAxis2V5";
            this.textAxis2V5.Size = new System.Drawing.Size(75, 27);
            this.textAxis2V5.TabIndex = 11;
            this.textAxis2V5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textAxis2V5.Enter += new System.EventHandler(this.textAxis2V5_Enter);
            this.textAxis2V5.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAxis2V5_KeyDown);
            this.textAxis2V5.Leave += new System.EventHandler(this.textAxis2V5_Leave);
            // 
            // textAxis2P3
            // 
            this.textAxis2P3.Location = new System.Drawing.Point(36, 84);
            this.textAxis2P3.Name = "textAxis2P3";
            this.textAxis2P3.Size = new System.Drawing.Size(75, 27);
            this.textAxis2P3.TabIndex = 4;
            this.textAxis2P3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textAxis2P3.Enter += new System.EventHandler(this.textAxis2P3_Enter);
            this.textAxis2P3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAxis2P3_KeyDown);
            this.textAxis2P3.Leave += new System.EventHandler(this.textAxis2P3_Leave);
            // 
            // textAxis2V4
            // 
            this.textAxis2V4.Location = new System.Drawing.Point(199, 115);
            this.textAxis2V4.Name = "textAxis2V4";
            this.textAxis2V4.Size = new System.Drawing.Size(75, 27);
            this.textAxis2V4.TabIndex = 10;
            this.textAxis2V4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textAxis2V4.Enter += new System.EventHandler(this.textAxis2V4_Enter);
            this.textAxis2V4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAxis2V4_KeyDown);
            this.textAxis2V4.Leave += new System.EventHandler(this.textAxis2V4_Leave);
            // 
            // textAxis2P4
            // 
            this.textAxis2P4.Location = new System.Drawing.Point(36, 112);
            this.textAxis2P4.Name = "textAxis2P4";
            this.textAxis2P4.Size = new System.Drawing.Size(75, 27);
            this.textAxis2P4.TabIndex = 5;
            this.textAxis2P4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textAxis2P4.Enter += new System.EventHandler(this.textAxis2P4_Enter);
            this.textAxis2P4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAxis2P4_KeyDown);
            this.textAxis2P4.Leave += new System.EventHandler(this.textAxis2P4_Leave);
            // 
            // textAxis2V3
            // 
            this.textAxis2V3.Location = new System.Drawing.Point(199, 87);
            this.textAxis2V3.Name = "textAxis2V3";
            this.textAxis2V3.Size = new System.Drawing.Size(75, 27);
            this.textAxis2V3.TabIndex = 9;
            this.textAxis2V3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textAxis2V3.Enter += new System.EventHandler(this.textAxis2V3_Enter);
            this.textAxis2V3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAxis2V3_KeyDown);
            this.textAxis2V3.Leave += new System.EventHandler(this.textAxis2V3_Leave);
            // 
            // textAxis2P5
            // 
            this.textAxis2P5.Location = new System.Drawing.Point(36, 140);
            this.textAxis2P5.Name = "textAxis2P5";
            this.textAxis2P5.Size = new System.Drawing.Size(75, 27);
            this.textAxis2P5.TabIndex = 6;
            this.textAxis2P5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textAxis2P5.Enter += new System.EventHandler(this.textAxis2P5_Enter);
            this.textAxis2P5.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAxis2P5_KeyDown);
            this.textAxis2P5.Leave += new System.EventHandler(this.textAxis2P5_Leave);
            // 
            // textAxis2V2
            // 
            this.textAxis2V2.Location = new System.Drawing.Point(199, 59);
            this.textAxis2V2.Name = "textAxis2V2";
            this.textAxis2V2.Size = new System.Drawing.Size(75, 27);
            this.textAxis2V2.TabIndex = 8;
            this.textAxis2V2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textAxis2V2.Enter += new System.EventHandler(this.textAxis2V2_Enter);
            this.textAxis2V2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAxis2V2_KeyDown);
            this.textAxis2V2.Leave += new System.EventHandler(this.textAxis2V2_Leave);
            // 
            // textAxis2V1
            // 
            this.textAxis2V1.Location = new System.Drawing.Point(199, 31);
            this.textAxis2V1.Name = "textAxis2V1";
            this.textAxis2V1.Size = new System.Drawing.Size(75, 27);
            this.textAxis2V1.TabIndex = 7;
            this.textAxis2V1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textAxis2V1.Enter += new System.EventHandler(this.textAxis2V1_Enter);
            this.textAxis2V1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAxis2V1_KeyDown);
            this.textAxis2V1.Leave += new System.EventHandler(this.textAxis2V1_Leave);
            // 
            // buttonSFCReload
            // 
            this.buttonSFCReload.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonSFCReload.Location = new System.Drawing.Point(718, 132);
            this.buttonSFCReload.Name = "buttonSFCReload";
            this.buttonSFCReload.Size = new System.Drawing.Size(96, 27);
            this.buttonSFCReload.TabIndex = 22;
            this.buttonSFCReload.Text = "Reload SFC";
            this.buttonSFCReload.UseVisualStyleBackColor = true;
            this.buttonSFCReload.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 376);
            this.Controls.Add(this.buttonSFCReload);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.buttonErrorAck);
            this.Controls.Add(this.buttonCoordinator);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonWS);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerWSCheck;
        private System.Windows.Forms.Button buttonWS;
        private System.Windows.Forms.TextBox textAxis1P1;
        private System.Windows.Forms.TextBox textAxis1P2;
        private System.Windows.Forms.TextBox textAxis1P3;
        private System.Windows.Forms.TextBox textAxis1P4;
        private System.Windows.Forms.TextBox textAxis1P5;
        private System.Windows.Forms.TextBox textAxis1V1;
        private System.Windows.Forms.TextBox textAxis1V2;
        private System.Windows.Forms.TextBox textAxis1V3;
        private System.Windows.Forms.TextBox textAxis1V4;
        private System.Windows.Forms.TextBox textAxis1V5;
        private System.Windows.Forms.Timer timerLoop;
        private System.Windows.Forms.TextBox textAxis1Cmd;
        private System.Windows.Forms.TextBox textAxis1Demand;
        private System.Windows.Forms.TextBox textAxis1Feedback;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonAxis1Start;
        private System.Windows.Forms.TextBox textAxis1SectionLen;
        private System.Windows.Forms.Button buttonCoordinator;
        private System.Windows.Forms.Button buttonErrorAck;
        private System.Windows.Forms.Button buttonAxis1Stop;
        private System.Windows.Forms.RadioButton radioAxis1Running;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioAxis2Running;
        private System.Windows.Forms.TextBox textAxis2Cmd;
        private System.Windows.Forms.TextBox textAxis2Feedback;
        private System.Windows.Forms.TextBox textAxis2Demand;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonAxis2Stop;
        private System.Windows.Forms.Button buttonAxis2Start;
        private System.Windows.Forms.TextBox textAxis2SectionLen;
        private System.Windows.Forms.TextBox textAxis2P1;
        private System.Windows.Forms.TextBox textAxis2P2;
        private System.Windows.Forms.TextBox textAxis2V5;
        private System.Windows.Forms.TextBox textAxis2P3;
        private System.Windows.Forms.TextBox textAxis2V4;
        private System.Windows.Forms.TextBox textAxis2P4;
        private System.Windows.Forms.TextBox textAxis2V3;
        private System.Windows.Forms.TextBox textAxis2P5;
        private System.Windows.Forms.TextBox textAxis2V2;
        private System.Windows.Forms.TextBox textAxis2V1;
        private System.Windows.Forms.RadioButton radioAxis1Interpolator;
        private System.Windows.Forms.RadioButton radioAxis1Reached;
        private System.Windows.Forms.RadioButton radioAxis2Interpolator;
        private System.Windows.Forms.RadioButton radioAxis2Reached;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Button buttonSFCReload;
    }
}


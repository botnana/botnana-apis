namespace Group2DAndMpg
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title4 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.chartPath = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartVelocity = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timerWs = new System.Windows.Forms.Timer(this.components);
            this.buttonWs = new System.Windows.Forms.Button();
            this.buttonReloadSfc = new System.Windows.Forms.Button();
            this.timerLoop = new System.Windows.Forms.Timer(this.components);
            this.buttonCoordinator = new System.Windows.Forms.Button();
            this.buttonErrorAck = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textSlaves = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonResetFault = new System.Windows.Forms.Button();
            this.buttonDriveOn = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonOn4 = new System.Windows.Forms.Button();
            this.buttonOn3 = new System.Windows.Forms.Button();
            this.buttonOn2 = new System.Windows.Forms.Button();
            this.buttonOn1 = new System.Windows.Forms.Button();
            this.buttonFault4 = new System.Windows.Forms.Button();
            this.buttonFault3 = new System.Windows.Forms.Button();
            this.buttonFault2 = new System.Windows.Forms.Button();
            this.buttonFault1 = new System.Windows.Forms.Button();
            this.textAxis4FollowingErr = new System.Windows.Forms.TextBox();
            this.textAxis4Feedback = new System.Windows.Forms.TextBox();
            this.textAxis4Cmd = new System.Windows.Forms.TextBox();
            this.textAxis3FollowingErr = new System.Windows.Forms.TextBox();
            this.textAxis2FollowingErr = new System.Windows.Forms.TextBox();
            this.textAxis1FollowingErr = new System.Windows.Forms.TextBox();
            this.textAxis3Feedback = new System.Windows.Forms.TextBox();
            this.textAxis2Feedback = new System.Windows.Forms.TextBox();
            this.textAxis1Feedback = new System.Windows.Forms.TextBox();
            this.textAxis3Cmd = new System.Windows.Forms.TextBox();
            this.textAxis2Cmd = new System.Windows.Forms.TextBox();
            this.textAxis1Cmd = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Axis = new System.Windows.Forms.Label();
            this.textMPGAxis = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textMPGRate = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textMPGEncoder = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.Motion = new System.Windows.Forms.GroupBox();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button7 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartPath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartVelocity)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.Motion.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartPath
            // 
            chartArea3.Name = "ChartArea1";
            this.chartPath.ChartAreas.Add(chartArea3);
            legend3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend3.IsTextAutoFit = false;
            legend3.Name = "Legend1";
            legend3.TableStyle = System.Windows.Forms.DataVisualization.Charting.LegendTableStyle.Tall;
            this.chartPath.Legends.Add(legend3);
            this.chartPath.Location = new System.Drawing.Point(541, 23);
            this.chartPath.Name = "chartPath";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series6.LabelAngle = 90;
            series6.Legend = "Legend1";
            series6.Name = "Path";
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series7.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series7.Legend = "Legend1";
            series7.Name = "Node";
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series8.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series8.LabelBorderWidth = 10;
            series8.Legend = "Legend1";
            series8.Name = "Current Position";
            series8.YValuesPerPoint = 2;
            series9.ChartArea = "ChartArea1";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series9.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series9.Legend = "Legend1";
            series9.Name = "Feedrate Limit";
            this.chartPath.Series.Add(series6);
            this.chartPath.Series.Add(series7);
            this.chartPath.Series.Add(series8);
            this.chartPath.Series.Add(series9);
            this.chartPath.Size = new System.Drawing.Size(854, 388);
            this.chartPath.TabIndex = 0;
            this.chartPath.Text = "chart1";
            title3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title3.Name = "Path Plan";
            title3.Text = "Path Plan";
            this.chartPath.Titles.Add(title3);
            // 
            // chartVelocity
            // 
            chartArea4.Name = "ChartArea1";
            this.chartVelocity.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chartVelocity.Legends.Add(legend4);
            this.chartVelocity.Location = new System.Drawing.Point(541, 431);
            this.chartVelocity.Name = "chartVelocity";
            series10.ChartArea = "ChartArea1";
            series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series10.IsVisibleInLegend = false;
            series10.Legend = "Legend1";
            series10.Name = "Series1";
            this.chartVelocity.Series.Add(series10);
            this.chartVelocity.Size = new System.Drawing.Size(854, 286);
            this.chartVelocity.TabIndex = 1;
            this.chartVelocity.Text = "Velocity";
            title4.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title4.Name = "Title1";
            title4.Text = "Velocity";
            this.chartVelocity.Titles.Add(title4);
            // 
            // timerWs
            // 
            this.timerWs.Enabled = true;
            this.timerWs.Interval = 1000;
            this.timerWs.Tick += new System.EventHandler(this.timerWs_Tick);
            // 
            // buttonWs
            // 
            this.buttonWs.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonWs.Location = new System.Drawing.Point(263, 26);
            this.buttonWs.Name = "buttonWs";
            this.buttonWs.Size = new System.Drawing.Size(129, 31);
            this.buttonWs.TabIndex = 2;
            this.buttonWs.Text = "WebSocket";
            this.buttonWs.UseVisualStyleBackColor = true;
            // 
            // buttonReloadSfc
            // 
            this.buttonReloadSfc.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReloadSfc.Location = new System.Drawing.Point(354, 30);
            this.buttonReloadSfc.Name = "buttonReloadSfc";
            this.buttonReloadSfc.Size = new System.Drawing.Size(165, 37);
            this.buttonReloadSfc.TabIndex = 3;
            this.buttonReloadSfc.Text = "Reload SFC";
            this.buttonReloadSfc.UseVisualStyleBackColor = true;
            this.buttonReloadSfc.Click += new System.EventHandler(this.buttonReloadSfc_Click);
            // 
            // timerLoop
            // 
            this.timerLoop.Enabled = true;
            this.timerLoop.Interval = 50;
            this.timerLoop.Tick += new System.EventHandler(this.timerLoop_Tick);
            // 
            // buttonCoordinator
            // 
            this.buttonCoordinator.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCoordinator.Location = new System.Drawing.Point(6, 30);
            this.buttonCoordinator.Name = "buttonCoordinator";
            this.buttonCoordinator.Size = new System.Drawing.Size(165, 37);
            this.buttonCoordinator.TabIndex = 4;
            this.buttonCoordinator.Text = "Coordinator";
            this.buttonCoordinator.UseVisualStyleBackColor = true;
            this.buttonCoordinator.Click += new System.EventHandler(this.buttonCoordinator_Click);
            // 
            // buttonErrorAck
            // 
            this.buttonErrorAck.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonErrorAck.Location = new System.Drawing.Point(180, 30);
            this.buttonErrorAck.Name = "buttonErrorAck";
            this.buttonErrorAck.Size = new System.Drawing.Size(165, 37);
            this.buttonErrorAck.TabIndex = 6;
            this.buttonErrorAck.Text = "Acked";
            this.buttonErrorAck.UseVisualStyleBackColor = true;
            this.buttonErrorAck.Click += new System.EventHandler(this.button2_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(354, 116);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(165, 37);
            this.button2.TabIndex = 7;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textSlaves);
            this.groupBox1.Controls.Add(this.buttonWs);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(529, 69);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Net";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "EtherCAT";
            // 
            // textSlaves
            // 
            this.textSlaves.Location = new System.Drawing.Point(125, 26);
            this.textSlaves.Name = "textSlaves";
            this.textSlaves.Size = new System.Drawing.Size(75, 31);
            this.textSlaves.TabIndex = 0;
            this.textSlaves.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 23);
            this.label4.TabIndex = 14;
            this.label4.Text = "Fault";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 23);
            this.label2.TabIndex = 12;
            this.label2.Text = "Servo";
            // 
            // buttonResetFault
            // 
            this.buttonResetFault.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonResetFault.Location = new System.Drawing.Point(6, 73);
            this.buttonResetFault.Name = "buttonResetFault";
            this.buttonResetFault.Size = new System.Drawing.Size(165, 37);
            this.buttonResetFault.TabIndex = 10;
            this.buttonResetFault.Text = "Reset Fault";
            this.buttonResetFault.UseVisualStyleBackColor = true;
            this.buttonResetFault.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonDriveOn
            // 
            this.buttonDriveOn.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDriveOn.Location = new System.Drawing.Point(354, 73);
            this.buttonDriveOn.Name = "buttonDriveOn";
            this.buttonDriveOn.Size = new System.Drawing.Size(165, 37);
            this.buttonDriveOn.TabIndex = 11;
            this.buttonDriveOn.Text = "Drive On";
            this.buttonDriveOn.UseVisualStyleBackColor = true;
            this.buttonDriveOn.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(180, 73);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(165, 37);
            this.button5.TabIndex = 12;
            this.button5.Text = "Drive Off";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.buttonOn4);
            this.groupBox3.Controls.Add(this.buttonOn3);
            this.groupBox3.Controls.Add(this.buttonOn2);
            this.groupBox3.Controls.Add(this.buttonOn1);
            this.groupBox3.Controls.Add(this.buttonFault4);
            this.groupBox3.Controls.Add(this.buttonFault3);
            this.groupBox3.Controls.Add(this.buttonFault2);
            this.groupBox3.Controls.Add(this.buttonFault1);
            this.groupBox3.Controls.Add(this.textAxis4FollowingErr);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.textAxis4Feedback);
            this.groupBox3.Controls.Add(this.textAxis4Cmd);
            this.groupBox3.Controls.Add(this.textAxis3FollowingErr);
            this.groupBox3.Controls.Add(this.textAxis2FollowingErr);
            this.groupBox3.Controls.Add(this.textAxis1FollowingErr);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.textAxis3Feedback);
            this.groupBox3.Controls.Add(this.textAxis2Feedback);
            this.groupBox3.Controls.Add(this.textAxis1Feedback);
            this.groupBox3.Controls.Add(this.textAxis3Cmd);
            this.groupBox3.Controls.Add(this.textAxis2Cmd);
            this.groupBox3.Controls.Add(this.textAxis1Cmd);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(6, 87);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(529, 247);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Axes";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 208);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 23);
            this.label7.TabIndex = 46;
            this.label7.Text = "Lag";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 23);
            this.label6.TabIndex = 45;
            this.label6.Text = "Feedback";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 23);
            this.label5.TabIndex = 44;
            this.label5.Text = "Command";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 23);
            this.label3.TabIndex = 43;
            this.label3.Text = "ID";
            // 
            // buttonOn4
            // 
            this.buttonOn4.Location = new System.Drawing.Point(424, 59);
            this.buttonOn4.Name = "buttonOn4";
            this.buttonOn4.Size = new System.Drawing.Size(95, 31);
            this.buttonOn4.TabIndex = 42;
            this.buttonOn4.UseVisualStyleBackColor = true;
            // 
            // buttonOn3
            // 
            this.buttonOn3.Location = new System.Drawing.Point(324, 59);
            this.buttonOn3.Name = "buttonOn3";
            this.buttonOn3.Size = new System.Drawing.Size(95, 31);
            this.buttonOn3.TabIndex = 41;
            this.buttonOn3.UseVisualStyleBackColor = true;
            // 
            // buttonOn2
            // 
            this.buttonOn2.Location = new System.Drawing.Point(224, 59);
            this.buttonOn2.Name = "buttonOn2";
            this.buttonOn2.Size = new System.Drawing.Size(95, 31);
            this.buttonOn2.TabIndex = 40;
            this.buttonOn2.UseVisualStyleBackColor = true;
            // 
            // buttonOn1
            // 
            this.buttonOn1.Location = new System.Drawing.Point(124, 59);
            this.buttonOn1.Name = "buttonOn1";
            this.buttonOn1.Size = new System.Drawing.Size(95, 31);
            this.buttonOn1.TabIndex = 39;
            this.buttonOn1.Text = "OFF";
            this.buttonOn1.UseVisualStyleBackColor = true;
            // 
            // buttonFault4
            // 
            this.buttonFault4.Location = new System.Drawing.Point(424, 95);
            this.buttonFault4.Name = "buttonFault4";
            this.buttonFault4.Size = new System.Drawing.Size(95, 31);
            this.buttonFault4.TabIndex = 38;
            this.buttonFault4.UseVisualStyleBackColor = true;
            // 
            // buttonFault3
            // 
            this.buttonFault3.Location = new System.Drawing.Point(324, 95);
            this.buttonFault3.Name = "buttonFault3";
            this.buttonFault3.Size = new System.Drawing.Size(95, 31);
            this.buttonFault3.TabIndex = 37;
            this.buttonFault3.UseVisualStyleBackColor = true;
            // 
            // buttonFault2
            // 
            this.buttonFault2.Location = new System.Drawing.Point(224, 95);
            this.buttonFault2.Name = "buttonFault2";
            this.buttonFault2.Size = new System.Drawing.Size(95, 31);
            this.buttonFault2.TabIndex = 36;
            this.buttonFault2.UseVisualStyleBackColor = true;
            // 
            // buttonFault1
            // 
            this.buttonFault1.Location = new System.Drawing.Point(124, 95);
            this.buttonFault1.Name = "buttonFault1";
            this.buttonFault1.Size = new System.Drawing.Size(96, 31);
            this.buttonFault1.TabIndex = 35;
            this.buttonFault1.UseVisualStyleBackColor = true;
            // 
            // textAxis4FollowingErr
            // 
            this.textAxis4FollowingErr.Location = new System.Drawing.Point(424, 203);
            this.textAxis4FollowingErr.Name = "textAxis4FollowingErr";
            this.textAxis4FollowingErr.Size = new System.Drawing.Size(96, 31);
            this.textAxis4FollowingErr.TabIndex = 34;
            this.textAxis4FollowingErr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textAxis4Feedback
            // 
            this.textAxis4Feedback.Location = new System.Drawing.Point(424, 167);
            this.textAxis4Feedback.Name = "textAxis4Feedback";
            this.textAxis4Feedback.Size = new System.Drawing.Size(96, 31);
            this.textAxis4Feedback.TabIndex = 33;
            this.textAxis4Feedback.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textAxis4Cmd
            // 
            this.textAxis4Cmd.Location = new System.Drawing.Point(424, 131);
            this.textAxis4Cmd.Name = "textAxis4Cmd";
            this.textAxis4Cmd.Size = new System.Drawing.Size(96, 31);
            this.textAxis4Cmd.TabIndex = 32;
            this.textAxis4Cmd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textAxis3FollowingErr
            // 
            this.textAxis3FollowingErr.Location = new System.Drawing.Point(324, 203);
            this.textAxis3FollowingErr.Name = "textAxis3FollowingErr";
            this.textAxis3FollowingErr.Size = new System.Drawing.Size(96, 31);
            this.textAxis3FollowingErr.TabIndex = 31;
            this.textAxis3FollowingErr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textAxis2FollowingErr
            // 
            this.textAxis2FollowingErr.Location = new System.Drawing.Point(224, 203);
            this.textAxis2FollowingErr.Name = "textAxis2FollowingErr";
            this.textAxis2FollowingErr.Size = new System.Drawing.Size(96, 31);
            this.textAxis2FollowingErr.TabIndex = 30;
            this.textAxis2FollowingErr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textAxis1FollowingErr
            // 
            this.textAxis1FollowingErr.Location = new System.Drawing.Point(124, 203);
            this.textAxis1FollowingErr.Name = "textAxis1FollowingErr";
            this.textAxis1FollowingErr.Size = new System.Drawing.Size(96, 31);
            this.textAxis1FollowingErr.TabIndex = 29;
            this.textAxis1FollowingErr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textAxis3Feedback
            // 
            this.textAxis3Feedback.Location = new System.Drawing.Point(324, 167);
            this.textAxis3Feedback.Name = "textAxis3Feedback";
            this.textAxis3Feedback.Size = new System.Drawing.Size(96, 31);
            this.textAxis3Feedback.TabIndex = 28;
            this.textAxis3Feedback.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textAxis2Feedback
            // 
            this.textAxis2Feedback.Location = new System.Drawing.Point(224, 167);
            this.textAxis2Feedback.Name = "textAxis2Feedback";
            this.textAxis2Feedback.Size = new System.Drawing.Size(96, 31);
            this.textAxis2Feedback.TabIndex = 27;
            this.textAxis2Feedback.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textAxis1Feedback
            // 
            this.textAxis1Feedback.Location = new System.Drawing.Point(124, 167);
            this.textAxis1Feedback.Name = "textAxis1Feedback";
            this.textAxis1Feedback.Size = new System.Drawing.Size(96, 31);
            this.textAxis1Feedback.TabIndex = 26;
            this.textAxis1Feedback.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textAxis3Cmd
            // 
            this.textAxis3Cmd.Location = new System.Drawing.Point(324, 131);
            this.textAxis3Cmd.Name = "textAxis3Cmd";
            this.textAxis3Cmd.Size = new System.Drawing.Size(96, 31);
            this.textAxis3Cmd.TabIndex = 25;
            this.textAxis3Cmd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textAxis2Cmd
            // 
            this.textAxis2Cmd.Location = new System.Drawing.Point(224, 131);
            this.textAxis2Cmd.Name = "textAxis2Cmd";
            this.textAxis2Cmd.Size = new System.Drawing.Size(96, 31);
            this.textAxis2Cmd.TabIndex = 24;
            this.textAxis2Cmd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textAxis1Cmd
            // 
            this.textAxis1Cmd.Location = new System.Drawing.Point(124, 131);
            this.textAxis1Cmd.Name = "textAxis1Cmd";
            this.textAxis1Cmd.Size = new System.Drawing.Size(96, 31);
            this.textAxis1Cmd.TabIndex = 23;
            this.textAxis1Cmd.Text = "123.456";
            this.textAxis1Cmd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(441, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(22, 23);
            this.label9.TabIndex = 22;
            this.label9.Text = "4";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(353, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(22, 23);
            this.label10.TabIndex = 21;
            this.label10.Text = "3";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(155, 27);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(22, 23);
            this.label11.TabIndex = 19;
            this.label11.Text = "1";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(277, 27);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(22, 23);
            this.label12.TabIndex = 20;
            this.label12.Text = "2";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Axis);
            this.groupBox4.Controls.Add(this.textMPGAxis);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.textMPGRate);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.textMPGEncoder);
            this.groupBox4.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(6, 340);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(529, 71);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "MPG";
            // 
            // Axis
            // 
            this.Axis.AutoSize = true;
            this.Axis.Location = new System.Drawing.Point(24, 28);
            this.Axis.Name = "Axis";
            this.Axis.Size = new System.Drawing.Size(50, 23);
            this.Axis.TabIndex = 10;
            this.Axis.Text = "Axis";
            // 
            // textMPGAxis
            // 
            this.textMPGAxis.Location = new System.Drawing.Point(80, 28);
            this.textMPGAxis.Name = "textMPGAxis";
            this.textMPGAxis.Size = new System.Drawing.Size(51, 31);
            this.textMPGAxis.TabIndex = 9;
            this.textMPGAxis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(147, 31);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 23);
            this.label13.TabIndex = 8;
            this.label13.Text = "Rate";
            // 
            // textMPGRate
            // 
            this.textMPGRate.Location = new System.Drawing.Point(205, 28);
            this.textMPGRate.Name = "textMPGRate";
            this.textMPGRate.Size = new System.Drawing.Size(70, 31);
            this.textMPGRate.TabIndex = 7;
            this.textMPGRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(297, 31);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(87, 23);
            this.label14.TabIndex = 5;
            this.label14.Text = "Encoder";
            // 
            // textMPGEncoder
            // 
            this.textMPGEncoder.Location = new System.Drawing.Point(405, 28);
            this.textMPGEncoder.Name = "textMPGEncoder";
            this.textMPGEncoder.Size = new System.Drawing.Size(103, 31);
            this.textMPGEncoder.TabIndex = 4;
            this.textMPGEncoder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(6, 75);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(165, 37);
            this.button3.TabIndex = 15;
            this.button3.Text = "1000";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(177, 75);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(165, 37);
            this.button4.TabIndex = 16;
            this.button4.Text = "1500";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(354, 75);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(165, 37);
            this.button6.TabIndex = 17;
            this.button6.Text = "2000";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // Motion
            // 
            this.Motion.Controls.Add(this.button9);
            this.Motion.Controls.Add(this.button8);
            this.Motion.Controls.Add(this.buttonCoordinator);
            this.Motion.Controls.Add(this.buttonErrorAck);
            this.Motion.Controls.Add(this.buttonReloadSfc);
            this.Motion.Controls.Add(this.buttonDriveOn);
            this.Motion.Controls.Add(this.button5);
            this.Motion.Controls.Add(this.buttonResetFault);
            this.Motion.Controls.Add(this.button2);
            this.Motion.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Motion.Location = new System.Drawing.Point(12, 417);
            this.Motion.Name = "Motion";
            this.Motion.Size = new System.Drawing.Size(523, 173);
            this.Motion.TabIndex = 18;
            this.Motion.TabStop = false;
            this.Motion.Text = "Control";
            // 
            // button9
            // 
            this.button9.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.Location = new System.Drawing.Point(6, 116);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(165, 37);
            this.button9.TabIndex = 14;
            this.button9.Text = "Reboot";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button8
            // 
            this.button8.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.Location = new System.Drawing.Point(180, 116);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(165, 37);
            this.button8.TabIndex = 13;
            this.button8.Text = "Shutdown";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(6, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(165, 37);
            this.button1.TabIndex = 19;
            this.button1.Text = "-500";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button10);
            this.groupBox2.Controls.Add(this.button7);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 596);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(523, 121);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Change Velocity Command (mm/min)";
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.Location = new System.Drawing.Point(177, 32);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(165, 37);
            this.button7.TabIndex = 20;
            this.button7.Text = "0";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("DFKai-SB", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(305, 760);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(678, 64);
            this.label8.TabIndex = 21;
            this.label8.Text = "動程科技股份有限公司";
            // 
            // button10
            // 
            this.button10.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button10.Location = new System.Drawing.Point(352, 32);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(165, 37);
            this.button10.TabIndex = 22;
            this.button10.Text = "500";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1407, 844);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Motion);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chartVelocity);
            this.Controls.Add(this.chartPath);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartPath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartVelocity)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.Motion.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartPath;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartVelocity;
        private System.Windows.Forms.Timer timerWs;
        private System.Windows.Forms.Button buttonWs;
        private System.Windows.Forms.Button buttonReloadSfc;
        private System.Windows.Forms.Timer timerLoop;
        private System.Windows.Forms.Button buttonCoordinator;
        private System.Windows.Forms.Button buttonErrorAck;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textSlaves;
        private System.Windows.Forms.Button buttonResetFault;
        private System.Windows.Forms.Button buttonDriveOn;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textAxis4FollowingErr;
        private System.Windows.Forms.TextBox textAxis4Feedback;
        private System.Windows.Forms.TextBox textAxis4Cmd;
        private System.Windows.Forms.TextBox textAxis3FollowingErr;
        private System.Windows.Forms.TextBox textAxis2FollowingErr;
        private System.Windows.Forms.TextBox textAxis1FollowingErr;
        private System.Windows.Forms.TextBox textAxis3Feedback;
        private System.Windows.Forms.TextBox textAxis2Feedback;
        private System.Windows.Forms.TextBox textAxis1Feedback;
        private System.Windows.Forms.TextBox textAxis3Cmd;
        private System.Windows.Forms.TextBox textAxis2Cmd;
        private System.Windows.Forms.TextBox textAxis1Cmd;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label Axis;
        private System.Windows.Forms.TextBox textMPGAxis;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textMPGRate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textMPGEncoder;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button buttonFault4;
        private System.Windows.Forms.Button buttonFault3;
        private System.Windows.Forms.Button buttonFault2;
        private System.Windows.Forms.Button buttonFault1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonOn4;
        private System.Windows.Forms.Button buttonOn3;
        private System.Windows.Forms.Button buttonOn2;
        private System.Windows.Forms.Button buttonOn1;
        private System.Windows.Forms.GroupBox Motion;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
    }
}


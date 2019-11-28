namespace BotnanaClassLibTest
{
    partial class Test
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
            this.driveControl1 = new BotnanaClassLib.DriveControl();
            this.axisControl1 = new BotnanaClassLib.AxisControl();
            this.sdoControl1 = new BotnanaClassLib.SDOControl();
            this.realTimeScriptControl1 = new BotnanaClassLib.RealTimeScriptControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonEnableCoordinator = new System.Windows.Forms.Button();
            this.buttonDisableCoordinator = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // driveControl1
            // 
            this.driveControl1.Location = new System.Drawing.Point(12, 12);
            this.driveControl1.Name = "driveControl1";
            this.driveControl1.Size = new System.Drawing.Size(303, 751);
            this.driveControl1.TabIndex = 0;
            // 
            // axisControl1
            // 
            this.axisControl1.Location = new System.Drawing.Point(321, 12);
            this.axisControl1.Name = "axisControl1";
            this.axisControl1.Size = new System.Drawing.Size(344, 495);
            this.axisControl1.TabIndex = 1;
            // 
            // sdoControl1
            // 
            this.sdoControl1.Location = new System.Drawing.Point(671, 12);
            this.sdoControl1.Name = "sdoControl1";
            this.sdoControl1.Size = new System.Drawing.Size(264, 509);
            this.sdoControl1.TabIndex = 2;
            // 
            // realTimeScriptControl1
            // 
            this.realTimeScriptControl1.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.realTimeScriptControl1.Location = new System.Drawing.Point(387, 542);
            this.realTimeScriptControl1.Margin = new System.Windows.Forms.Padding(4);
            this.realTimeScriptControl1.Name = "realTimeScriptControl1";
            this.realTimeScriptControl1.Size = new System.Drawing.Size(364, 197);
            this.realTimeScriptControl1.TabIndex = 3;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttonEnableCoordinator
            // 
            this.buttonEnableCoordinator.Location = new System.Drawing.Point(800, 590);
            this.buttonEnableCoordinator.Name = "buttonEnableCoordinator";
            this.buttonEnableCoordinator.Size = new System.Drawing.Size(100, 25);
            this.buttonEnableCoordinator.TabIndex = 4;
            this.buttonEnableCoordinator.Text = "+Coordinator";
            this.buttonEnableCoordinator.UseVisualStyleBackColor = true;
            this.buttonEnableCoordinator.Click += new System.EventHandler(this.buttonEnableCoordinator_Click);
            // 
            // buttonDisableCoordinator
            // 
            this.buttonDisableCoordinator.Location = new System.Drawing.Point(800, 621);
            this.buttonDisableCoordinator.Name = "buttonDisableCoordinator";
            this.buttonDisableCoordinator.Size = new System.Drawing.Size(100, 25);
            this.buttonDisableCoordinator.TabIndex = 5;
            this.buttonDisableCoordinator.Text = "-Coordinator";
            this.buttonDisableCoordinator.UseVisualStyleBackColor = true;
            this.buttonDisableCoordinator.Click += new System.EventHandler(this.buttonDisableCoordinator_Click);
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 768);
            this.Controls.Add(this.buttonDisableCoordinator);
            this.Controls.Add(this.buttonEnableCoordinator);
            this.Controls.Add(this.realTimeScriptControl1);
            this.Controls.Add(this.sdoControl1);
            this.Controls.Add(this.axisControl1);
            this.Controls.Add(this.driveControl1);
            this.Name = "Test";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Test_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private BotnanaClassLib.DriveControl driveControl1;
        private BotnanaClassLib.AxisControl axisControl1;
        private BotnanaClassLib.SDOControl sdoControl1;
        private BotnanaClassLib.RealTimeScriptControl realTimeScriptControl1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonEnableCoordinator;
        private System.Windows.Forms.Button buttonDisableCoordinator;
    }
}


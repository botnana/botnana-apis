namespace MultiApp
{
    partial class FormApp
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
            this.tabControlApp = new System.Windows.Forms.TabControl();
            this.tabPageTorque = new System.Windows.Forms.TabPage();
            this.tabPageFeeder = new System.Windows.Forms.TabPage();
            this.tabPageRCON = new System.Windows.Forms.TabPage();
            this.groupBoxSystemControl = new System.Windows.Forms.GroupBox();
            this.buttonReboot = new System.Windows.Forms.Button();
            this.buttonEMS = new System.Windows.Forms.Button();
            this.buttonStopPolling = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxScript = new System.Windows.Forms.TextBox();
            this.buttonReloadSFC = new System.Windows.Forms.Button();
            this.groupBoxSystemStatus = new System.Windows.Forms.GroupBox();
            this.buttonSystemReady = new System.Windows.Forms.Button();
            this.buttonECState = new System.Windows.Forms.Button();
            this.buttonWSState = new System.Windows.Forms.Button();
            this.timer1s = new System.Windows.Forms.Timer(this.components);
            this.tabControlApp.SuspendLayout();
            this.groupBoxSystemControl.SuspendLayout();
            this.groupBoxSystemStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlApp
            // 
            this.tabControlApp.Controls.Add(this.tabPageTorque);
            this.tabControlApp.Controls.Add(this.tabPageFeeder);
            this.tabControlApp.Controls.Add(this.tabPageRCON);
            this.tabControlApp.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabControlApp.Location = new System.Drawing.Point(218, 12);
            this.tabControlApp.Name = "tabControlApp";
            this.tabControlApp.Padding = new System.Drawing.Point(20, 3);
            this.tabControlApp.SelectedIndex = 0;
            this.tabControlApp.Size = new System.Drawing.Size(1147, 752);
            this.tabControlApp.TabIndex = 0;
            this.tabControlApp.SelectedIndexChanged += new System.EventHandler(this.tabControlApp_SelectedIndexChanged);
            // 
            // tabPageTorque
            // 
            this.tabPageTorque.Location = new System.Drawing.Point(4, 29);
            this.tabPageTorque.Name = "tabPageTorque";
            this.tabPageTorque.Size = new System.Drawing.Size(1139, 719);
            this.tabPageTorque.TabIndex = 2;
            this.tabPageTorque.Text = "Torque";
            this.tabPageTorque.UseVisualStyleBackColor = true;
            // 
            // tabPageFeeder
            // 
            this.tabPageFeeder.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabPageFeeder.Location = new System.Drawing.Point(4, 29);
            this.tabPageFeeder.Name = "tabPageFeeder";
            this.tabPageFeeder.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFeeder.Size = new System.Drawing.Size(1139, 719);
            this.tabPageFeeder.TabIndex = 1;
            this.tabPageFeeder.Text = "Mold Feeder";
            this.tabPageFeeder.UseVisualStyleBackColor = true;
            // 
            // tabPageRCON
            // 
            this.tabPageRCON.Location = new System.Drawing.Point(4, 29);
            this.tabPageRCON.Name = "tabPageRCON";
            this.tabPageRCON.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRCON.Size = new System.Drawing.Size(1139, 719);
            this.tabPageRCON.TabIndex = 0;
            this.tabPageRCON.Text = "RCON System";
            this.tabPageRCON.UseVisualStyleBackColor = true;
            // 
            // groupBoxSystemControl
            // 
            this.groupBoxSystemControl.Controls.Add(this.buttonReboot);
            this.groupBoxSystemControl.Controls.Add(this.buttonEMS);
            this.groupBoxSystemControl.Controls.Add(this.buttonStopPolling);
            this.groupBoxSystemControl.Controls.Add(this.label2);
            this.groupBoxSystemControl.Controls.Add(this.textBoxScript);
            this.groupBoxSystemControl.Controls.Add(this.buttonReloadSFC);
            this.groupBoxSystemControl.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBoxSystemControl.Location = new System.Drawing.Point(12, 207);
            this.groupBoxSystemControl.Name = "groupBoxSystemControl";
            this.groupBoxSystemControl.Size = new System.Drawing.Size(200, 553);
            this.groupBoxSystemControl.TabIndex = 1;
            this.groupBoxSystemControl.TabStop = false;
            this.groupBoxSystemControl.Text = "System Control";
            // 
            // buttonReboot
            // 
            this.buttonReboot.BackColor = System.Drawing.SystemColors.Control;
            this.buttonReboot.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonReboot.Location = new System.Drawing.Point(20, 86);
            this.buttonReboot.Name = "buttonReboot";
            this.buttonReboot.Size = new System.Drawing.Size(160, 40);
            this.buttonReboot.TabIndex = 21;
            this.buttonReboot.Text = "Reboot";
            this.buttonReboot.UseVisualStyleBackColor = false;
            this.buttonReboot.Click += new System.EventHandler(this.buttonReboot_Click);
            // 
            // buttonEMS
            // 
            this.buttonEMS.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEMS.ForeColor = System.Drawing.Color.Red;
            this.buttonEMS.Location = new System.Drawing.Point(39, 448);
            this.buttonEMS.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEMS.Name = "buttonEMS";
            this.buttonEMS.Size = new System.Drawing.Size(111, 69);
            this.buttonEMS.TabIndex = 20;
            this.buttonEMS.Text = "EMS";
            this.buttonEMS.UseVisualStyleBackColor = true;
            this.buttonEMS.Click += new System.EventHandler(this.buttonEMS_Click);
            // 
            // buttonStopPolling
            // 
            this.buttonStopPolling.BackColor = System.Drawing.SystemColors.Control;
            this.buttonStopPolling.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonStopPolling.Location = new System.Drawing.Point(20, 132);
            this.buttonStopPolling.Name = "buttonStopPolling";
            this.buttonStopPolling.Size = new System.Drawing.Size(160, 40);
            this.buttonStopPolling.TabIndex = 5;
            this.buttonStopPolling.Text = "Stop Polling";
            this.buttonStopPolling.UseVisualStyleBackColor = false;
            this.buttonStopPolling.Click += new System.EventHandler(this.buttonStopPolling_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(6, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Real Time Script :";
            // 
            // textBoxScript
            // 
            this.textBoxScript.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxScript.Location = new System.Drawing.Point(8, 212);
            this.textBoxScript.Multiline = true;
            this.textBoxScript.Name = "textBoxScript";
            this.textBoxScript.Size = new System.Drawing.Size(184, 198);
            this.textBoxScript.TabIndex = 4;
            this.textBoxScript.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxScript_KeyDown);
            this.textBoxScript.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxScript_KeyUp);
            // 
            // buttonReloadSFC
            // 
            this.buttonReloadSFC.BackColor = System.Drawing.SystemColors.Control;
            this.buttonReloadSFC.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonReloadSFC.Location = new System.Drawing.Point(20, 40);
            this.buttonReloadSFC.Name = "buttonReloadSFC";
            this.buttonReloadSFC.Size = new System.Drawing.Size(160, 40);
            this.buttonReloadSFC.TabIndex = 4;
            this.buttonReloadSFC.Text = "Reload SFC";
            this.buttonReloadSFC.UseVisualStyleBackColor = false;
            this.buttonReloadSFC.Click += new System.EventHandler(this.buttonReloadSFC_Click);
            // 
            // groupBoxSystemStatus
            // 
            this.groupBoxSystemStatus.Controls.Add(this.buttonSystemReady);
            this.groupBoxSystemStatus.Controls.Add(this.buttonECState);
            this.groupBoxSystemStatus.Controls.Add(this.buttonWSState);
            this.groupBoxSystemStatus.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBoxSystemStatus.Location = new System.Drawing.Point(12, 12);
            this.groupBoxSystemStatus.Name = "groupBoxSystemStatus";
            this.groupBoxSystemStatus.Size = new System.Drawing.Size(200, 189);
            this.groupBoxSystemStatus.TabIndex = 2;
            this.groupBoxSystemStatus.TabStop = false;
            this.groupBoxSystemStatus.Text = "System Status";
            // 
            // buttonSystemReady
            // 
            this.buttonSystemReady.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.buttonSystemReady.Enabled = false;
            this.buttonSystemReady.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonSystemReady.Location = new System.Drawing.Point(20, 126);
            this.buttonSystemReady.Name = "buttonSystemReady";
            this.buttonSystemReady.Size = new System.Drawing.Size(160, 40);
            this.buttonSystemReady.TabIndex = 4;
            this.buttonSystemReady.Text = "System not ready";
            this.buttonSystemReady.UseVisualStyleBackColor = false;
            // 
            // buttonECState
            // 
            this.buttonECState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.buttonECState.Enabled = false;
            this.buttonECState.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonECState.Location = new System.Drawing.Point(20, 80);
            this.buttonECState.Name = "buttonECState";
            this.buttonECState.Size = new System.Drawing.Size(160, 40);
            this.buttonECState.TabIndex = 3;
            this.buttonECState.Text = "EtherCAT not ready(0)";
            this.buttonECState.UseVisualStyleBackColor = false;
            // 
            // buttonWSState
            // 
            this.buttonWSState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.buttonWSState.Enabled = false;
            this.buttonWSState.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonWSState.Location = new System.Drawing.Point(20, 34);
            this.buttonWSState.Name = "buttonWSState";
            this.buttonWSState.Size = new System.Drawing.Size(160, 40);
            this.buttonWSState.TabIndex = 0;
            this.buttonWSState.Text = "WebSocket not ready";
            this.buttonWSState.UseVisualStyleBackColor = false;
            // 
            // timer1s
            // 
            this.timer1s.Interval = 1000;
            this.timer1s.Tick += new System.EventHandler(this.timer1s_Tick);
            // 
            // FormApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1377, 776);
            this.Controls.Add(this.groupBoxSystemStatus);
            this.Controls.Add(this.groupBoxSystemControl);
            this.Controls.Add(this.tabControlApp);
            this.Name = "FormApp";
            this.Text = "App";
            this.Load += new System.EventHandler(this.FormApp_Load);
            this.tabControlApp.ResumeLayout(false);
            this.groupBoxSystemControl.ResumeLayout(false);
            this.groupBoxSystemControl.PerformLayout();
            this.groupBoxSystemStatus.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlApp;
        private System.Windows.Forms.TabPage tabPageRCON;
        private System.Windows.Forms.TabPage tabPageFeeder;
        private System.Windows.Forms.TabPage tabPageTorque;
        private System.Windows.Forms.GroupBox groupBoxSystemControl;
        private System.Windows.Forms.GroupBox groupBoxSystemStatus;
        private System.Windows.Forms.Button buttonWSState;
        private System.Windows.Forms.Timer timer1s;
        private System.Windows.Forms.Button buttonECState;
        private System.Windows.Forms.Button buttonReloadSFC;
        private System.Windows.Forms.TextBox textBoxScript;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonStopPolling;
        private System.Windows.Forms.Button buttonSystemReady;
        private System.Windows.Forms.Button buttonEMS;
        private System.Windows.Forms.Button buttonReboot;
    }
}


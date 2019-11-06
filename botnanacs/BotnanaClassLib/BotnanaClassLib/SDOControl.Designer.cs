namespace BotnanaClassLib
{
    partial class SDOControl
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
            this.groupBoxSDOControl = new System.Windows.Forms.GroupBox();
            this.buttonECSave = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxSDOOperation = new System.Windows.Forms.ComboBox();
            this.buttonSDOExecute = new System.Windows.Forms.Button();
            this.textBoxSDOValueC = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxSDOSubIndexC = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSDOIndexC = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxSDOSlaveNumber = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBoxSDOStatus = new System.Windows.Forms.GroupBox();
            this.textBoxSDOValueS = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxSDOErrorS = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxSDOBusyS = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxSDOSubIndexS = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxSDOIndexS = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBoxSDOControl.SuspendLayout();
            this.groupBoxSDOStatus.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSDOControl
            // 
            this.groupBoxSDOControl.Controls.Add(this.buttonECSave);
            this.groupBoxSDOControl.Controls.Add(this.label6);
            this.groupBoxSDOControl.Controls.Add(this.comboBoxSDOOperation);
            this.groupBoxSDOControl.Controls.Add(this.buttonSDOExecute);
            this.groupBoxSDOControl.Controls.Add(this.textBoxSDOValueC);
            this.groupBoxSDOControl.Controls.Add(this.label4);
            this.groupBoxSDOControl.Controls.Add(this.textBoxSDOSubIndexC);
            this.groupBoxSDOControl.Controls.Add(this.label3);
            this.groupBoxSDOControl.Controls.Add(this.textBoxSDOIndexC);
            this.groupBoxSDOControl.Controls.Add(this.label1);
            this.groupBoxSDOControl.Controls.Add(this.label12);
            this.groupBoxSDOControl.Controls.Add(this.label13);
            this.groupBoxSDOControl.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBoxSDOControl.Location = new System.Drawing.Point(6, 260);
            this.groupBoxSDOControl.Name = "groupBoxSDOControl";
            this.groupBoxSDOControl.Size = new System.Drawing.Size(244, 238);
            this.groupBoxSDOControl.TabIndex = 2;
            this.groupBoxSDOControl.TabStop = false;
            this.groupBoxSDOControl.Text = "Control";
            // 
            // buttonECSave
            // 
            this.buttonECSave.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonECSave.Location = new System.Drawing.Point(72, 198);
            this.buttonECSave.Name = "buttonECSave";
            this.buttonECSave.Size = new System.Drawing.Size(100, 25);
            this.buttonECSave.TabIndex = 13;
            this.buttonECSave.Text = "Save";
            this.buttonECSave.UseVisualStyleBackColor = true;
            this.buttonECSave.Click += new System.EventHandler(this.buttonECSave_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(21, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "Operation";
            // 
            // comboBoxSDOOperation
            // 
            this.comboBoxSDOOperation.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBoxSDOOperation.FormattingEnabled = true;
            this.comboBoxSDOOperation.Items.AddRange(new object[] {
            "upload u8",
            "upload u16",
            "upload u32",
            "upload i8",
            "upload i16",
            "upload i32",
            "download u8",
            "download u16",
            "download u32",
            "download i8",
            "download i16",
            "download i32"});
            this.comboBoxSDOOperation.Location = new System.Drawing.Point(104, 127);
            this.comboBoxSDOOperation.Name = "comboBoxSDOOperation";
            this.comboBoxSDOOperation.Size = new System.Drawing.Size(125, 25);
            this.comboBoxSDOOperation.TabIndex = 9;
            this.comboBoxSDOOperation.Text = "upload u8";
            // 
            // buttonSDOExecute
            // 
            this.buttonSDOExecute.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonSDOExecute.Location = new System.Drawing.Point(72, 167);
            this.buttonSDOExecute.Name = "buttonSDOExecute";
            this.buttonSDOExecute.Size = new System.Drawing.Size(100, 25);
            this.buttonSDOExecute.TabIndex = 8;
            this.buttonSDOExecute.Text = "Execute";
            this.buttonSDOExecute.UseVisualStyleBackColor = true;
            this.buttonSDOExecute.Click += new System.EventHandler(this.buttonSDOExecute_Click);
            // 
            // textBoxSDOValueC
            // 
            this.textBoxSDOValueC.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxSDOValueC.Location = new System.Drawing.Point(119, 90);
            this.textBoxSDOValueC.Name = "textBoxSDOValueC";
            this.textBoxSDOValueC.Size = new System.Drawing.Size(100, 25);
            this.textBoxSDOValueC.TabIndex = 7;
            this.textBoxSDOValueC.TextChanged += new System.EventHandler(this.textBoxSDOValueC_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(21, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Value";
            // 
            // textBoxSDOSubIndexC
            // 
            this.textBoxSDOSubIndexC.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxSDOSubIndexC.Location = new System.Drawing.Point(119, 59);
            this.textBoxSDOSubIndexC.Name = "textBoxSDOSubIndexC";
            this.textBoxSDOSubIndexC.Size = new System.Drawing.Size(100, 25);
            this.textBoxSDOSubIndexC.TabIndex = 5;
            this.textBoxSDOSubIndexC.TextChanged += new System.EventHandler(this.textBoxSDOSubIndexC_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(21, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Sub-index";
            // 
            // textBoxSDOIndexC
            // 
            this.textBoxSDOIndexC.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxSDOIndexC.Location = new System.Drawing.Point(119, 28);
            this.textBoxSDOIndexC.Name = "textBoxSDOIndexC";
            this.textBoxSDOIndexC.Size = new System.Drawing.Size(100, 25);
            this.textBoxSDOIndexC.TabIndex = 3;
            this.textBoxSDOIndexC.TextChanged += new System.EventHandler(this.textBoxSDOIndexC_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(21, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Index";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label12.Location = new System.Drawing.Point(96, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(22, 17);
            this.label12.TabIndex = 11;
            this.label12.Text = "0x";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label13.Location = new System.Drawing.Point(96, 62);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(22, 17);
            this.label13.TabIndex = 12;
            this.label13.Text = "0x";
            // 
            // textBoxSDOSlaveNumber
            // 
            this.textBoxSDOSlaveNumber.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxSDOSlaveNumber.Location = new System.Drawing.Point(125, 26);
            this.textBoxSDOSlaveNumber.Name = "textBoxSDOSlaveNumber";
            this.textBoxSDOSlaveNumber.Size = new System.Drawing.Size(100, 25);
            this.textBoxSDOSlaveNumber.TabIndex = 4;
            this.textBoxSDOSlaveNumber.Text = "1";
            this.textBoxSDOSlaveNumber.TextChanged += new System.EventHandler(this.textBoxSDOSlaveNumber_TextChanged);
            this.textBoxSDOSlaveNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSDOSlaveNumber_KeyDown);
            this.textBoxSDOSlaveNumber.Leave += new System.EventHandler(this.textBoxSDOSlaveNumber_Leave);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label14.Location = new System.Drawing.Point(27, 29);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(90, 17);
            this.label14.TabIndex = 3;
            this.label14.Text = "Slave number";
            // 
            // groupBoxSDOStatus
            // 
            this.groupBoxSDOStatus.Controls.Add(this.textBoxSDOValueS);
            this.groupBoxSDOStatus.Controls.Add(this.label11);
            this.groupBoxSDOStatus.Controls.Add(this.textBoxSDOErrorS);
            this.groupBoxSDOStatus.Controls.Add(this.label10);
            this.groupBoxSDOStatus.Controls.Add(this.textBoxSDOBusyS);
            this.groupBoxSDOStatus.Controls.Add(this.label7);
            this.groupBoxSDOStatus.Controls.Add(this.textBoxSDOSubIndexS);
            this.groupBoxSDOStatus.Controls.Add(this.label8);
            this.groupBoxSDOStatus.Controls.Add(this.textBoxSDOIndexS);
            this.groupBoxSDOStatus.Controls.Add(this.label9);
            this.groupBoxSDOStatus.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBoxSDOStatus.Location = new System.Drawing.Point(6, 59);
            this.groupBoxSDOStatus.Name = "groupBoxSDOStatus";
            this.groupBoxSDOStatus.Size = new System.Drawing.Size(244, 195);
            this.groupBoxSDOStatus.TabIndex = 1;
            this.groupBoxSDOStatus.TabStop = false;
            this.groupBoxSDOStatus.Text = "Status";
            // 
            // textBoxSDOValueS
            // 
            this.textBoxSDOValueS.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxSDOValueS.Location = new System.Drawing.Point(119, 152);
            this.textBoxSDOValueS.Name = "textBoxSDOValueS";
            this.textBoxSDOValueS.ReadOnly = true;
            this.textBoxSDOValueS.Size = new System.Drawing.Size(100, 25);
            this.textBoxSDOValueS.TabIndex = 17;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label11.Location = new System.Drawing.Point(21, 155);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 17);
            this.label11.TabIndex = 16;
            this.label11.Text = "Value";
            // 
            // textBoxSDOErrorS
            // 
            this.textBoxSDOErrorS.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxSDOErrorS.Location = new System.Drawing.Point(119, 121);
            this.textBoxSDOErrorS.Name = "textBoxSDOErrorS";
            this.textBoxSDOErrorS.ReadOnly = true;
            this.textBoxSDOErrorS.Size = new System.Drawing.Size(100, 25);
            this.textBoxSDOErrorS.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label10.Location = new System.Drawing.Point(21, 124);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 17);
            this.label10.TabIndex = 14;
            this.label10.Text = "Error";
            // 
            // textBoxSDOBusyS
            // 
            this.textBoxSDOBusyS.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxSDOBusyS.Location = new System.Drawing.Point(119, 90);
            this.textBoxSDOBusyS.Name = "textBoxSDOBusyS";
            this.textBoxSDOBusyS.ReadOnly = true;
            this.textBoxSDOBusyS.Size = new System.Drawing.Size(100, 25);
            this.textBoxSDOBusyS.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(21, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 17);
            this.label7.TabIndex = 12;
            this.label7.Text = "Busy";
            // 
            // textBoxSDOSubIndexS
            // 
            this.textBoxSDOSubIndexS.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxSDOSubIndexS.Location = new System.Drawing.Point(119, 59);
            this.textBoxSDOSubIndexS.Name = "textBoxSDOSubIndexS";
            this.textBoxSDOSubIndexS.ReadOnly = true;
            this.textBoxSDOSubIndexS.Size = new System.Drawing.Size(100, 25);
            this.textBoxSDOSubIndexS.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(21, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 17);
            this.label8.TabIndex = 10;
            this.label8.Text = "Sub-index";
            // 
            // textBoxSDOIndexS
            // 
            this.textBoxSDOIndexS.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxSDOIndexS.Location = new System.Drawing.Point(119, 28);
            this.textBoxSDOIndexS.Name = "textBoxSDOIndexS";
            this.textBoxSDOIndexS.ReadOnly = true;
            this.textBoxSDOIndexS.Size = new System.Drawing.Size(100, 25);
            this.textBoxSDOIndexS.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.Location = new System.Drawing.Point(21, 31);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 17);
            this.label9.TabIndex = 8;
            this.label9.Text = "Index";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxSDOSlaveNumber);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.groupBoxSDOStatus);
            this.groupBox1.Controls.Add(this.groupBoxSDOControl);
            this.groupBox1.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 504);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SDO";
            // 
            // SDOControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "SDOControl";
            this.Size = new System.Drawing.Size(264, 509);
            this.groupBoxSDOControl.ResumeLayout(false);
            this.groupBoxSDOControl.PerformLayout();
            this.groupBoxSDOStatus.ResumeLayout(false);
            this.groupBoxSDOStatus.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxSDOControl;
        private System.Windows.Forms.TextBox textBoxSDOSlaveNumber;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxSDOOperation;
        private System.Windows.Forms.Button buttonSDOExecute;
        private System.Windows.Forms.TextBox textBoxSDOValueC;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxSDOSubIndexC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSDOIndexC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBoxSDOStatus;
        private System.Windows.Forms.TextBox textBoxSDOValueS;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxSDOErrorS;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxSDOBusyS;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxSDOSubIndexS;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxSDOIndexS;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonECSave;
    }
}

namespace RecrodNetMeetting
{
    partial class FrmRnm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRnm));
            this.gbRecordStyle = new System.Windows.Forms.GroupBox();
            this.rbOuter = new System.Windows.Forms.RadioButton();
            this.rbInner = new System.Windows.Forms.RadioButton();
            this.rbMix = new System.Windows.Forms.RadioButton();
            this.btnOpenRecordFileFold = new System.Windows.Forms.Button();
            this.lbRecordTime = new System.Windows.Forms.Label();
            this.btnRecord = new System.Windows.Forms.Button();
            this.panelSetting = new System.Windows.Forms.Panel();
            this.panelChart = new System.Windows.Forms.Panel();
            this.gbRecordStyle.SuspendLayout();
            this.panelSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbRecordStyle
            // 
            this.gbRecordStyle.Controls.Add(this.rbOuter);
            this.gbRecordStyle.Controls.Add(this.rbInner);
            this.gbRecordStyle.Controls.Add(this.rbMix);
            this.gbRecordStyle.Location = new System.Drawing.Point(12, 3);
            this.gbRecordStyle.Name = "gbRecordStyle";
            this.gbRecordStyle.Size = new System.Drawing.Size(191, 53);
            this.gbRecordStyle.TabIndex = 7;
            this.gbRecordStyle.TabStop = false;
            this.gbRecordStyle.Text = "录音模式";
            // 
            // rbOuter
            // 
            this.rbOuter.AutoSize = true;
            this.rbOuter.Location = new System.Drawing.Point(118, 22);
            this.rbOuter.Name = "rbOuter";
            this.rbOuter.Size = new System.Drawing.Size(50, 21);
            this.rbOuter.TabIndex = 6;
            this.rbOuter.TabStop = true;
            this.rbOuter.Text = "外录";
            this.rbOuter.UseVisualStyleBackColor = true;
            this.rbOuter.CheckedChanged += new System.EventHandler(this.rbMix_CheckedChanged);
            // 
            // rbInner
            // 
            this.rbInner.AutoSize = true;
            this.rbInner.Location = new System.Drawing.Point(62, 22);
            this.rbInner.Name = "rbInner";
            this.rbInner.Size = new System.Drawing.Size(50, 21);
            this.rbInner.TabIndex = 5;
            this.rbInner.TabStop = true;
            this.rbInner.Text = "内录";
            this.rbInner.UseVisualStyleBackColor = true;
            this.rbInner.CheckedChanged += new System.EventHandler(this.rbMix_CheckedChanged);
            // 
            // rbMix
            // 
            this.rbMix.AutoSize = true;
            this.rbMix.Location = new System.Drawing.Point(6, 22);
            this.rbMix.Name = "rbMix";
            this.rbMix.Size = new System.Drawing.Size(50, 21);
            this.rbMix.TabIndex = 4;
            this.rbMix.TabStop = true;
            this.rbMix.Text = "混录";
            this.rbMix.UseVisualStyleBackColor = true;
            this.rbMix.CheckedChanged += new System.EventHandler(this.rbMix_CheckedChanged);
            // 
            // btnOpenRecordFileFold
            // 
            this.btnOpenRecordFileFold.AutoSize = true;
            this.btnOpenRecordFileFold.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOpenRecordFileFold.Image = global::RecrodNetMeetting.Properties.Resources.FolderOpenLightBlue;
            this.btnOpenRecordFileFold.Location = new System.Drawing.Point(310, 24);
            this.btnOpenRecordFileFold.Name = "btnOpenRecordFileFold";
            this.btnOpenRecordFileFold.Size = new System.Drawing.Size(22, 22);
            this.btnOpenRecordFileFold.TabIndex = 6;
            this.btnOpenRecordFileFold.UseVisualStyleBackColor = true;
            this.btnOpenRecordFileFold.Click += new System.EventHandler(this.btnOpenRecordFileFold_Click);
            // 
            // lbRecordTime
            // 
            this.lbRecordTime.AutoSize = true;
            this.lbRecordTime.Location = new System.Drawing.Point(96, 12);
            this.lbRecordTime.Name = "lbRecordTime";
            this.lbRecordTime.Size = new System.Drawing.Size(43, 17);
            this.lbRecordTime.TabIndex = 5;
            this.lbRecordTime.Text = "label1";
            // 
            // btnRecord
            // 
            this.btnRecord.AutoSize = true;
            this.btnRecord.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRecord.Image = global::RecrodNetMeetting.Properties.Resources.Microphone_icon;
            this.btnRecord.Location = new System.Drawing.Point(12, 12);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(78, 78);
            this.btnRecord.TabIndex = 4;
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // panelSetting
            // 
            this.panelSetting.Controls.Add(this.gbRecordStyle);
            this.panelSetting.Controls.Add(this.btnOpenRecordFileFold);
            this.panelSetting.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSetting.Location = new System.Drawing.Point(0, 94);
            this.panelSetting.Name = "panelSetting";
            this.panelSetting.Size = new System.Drawing.Size(344, 63);
            this.panelSetting.TabIndex = 8;
            // 
            // panelChart
            // 
            this.panelChart.Location = new System.Drawing.Point(96, 40);
            this.panelChart.Name = "panelChart";
            this.panelChart.Size = new System.Drawing.Size(236, 48);
            this.panelChart.TabIndex = 9;
            // 
            // FrmRnm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 157);
            this.Controls.Add(this.panelChart);
            this.Controls.Add(this.panelSetting);
            this.Controls.Add(this.lbRecordTime);
            this.Controls.Add(this.btnRecord);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRnm";
            this.Text = "Form1";
            this.gbRecordStyle.ResumeLayout(false);
            this.gbRecordStyle.PerformLayout();
            this.panelSetting.ResumeLayout(false);
            this.panelSetting.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox gbRecordStyle;
        private RadioButton rbOuter;
        private RadioButton rbInner;
        private RadioButton rbMix;
        private Button btnOpenRecordFileFold;
        private Label lbRecordTime;
        private Button btnRecord;
        private Panel panelSetting;
        private Panel panelChart;
    }
}
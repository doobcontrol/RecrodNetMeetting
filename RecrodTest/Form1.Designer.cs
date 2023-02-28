namespace RecrodTest
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
            this.btnRecord = new System.Windows.Forms.Button();
            this.lbRecordTime = new System.Windows.Forms.Label();
            this.btnOpenRecordFileFold = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRecord
            // 
            this.btnRecord.Location = new System.Drawing.Point(12, 12);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(75, 23);
            this.btnRecord.TabIndex = 0;
            this.btnRecord.Text = "开始";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // lbRecordTime
            // 
            this.lbRecordTime.AutoSize = true;
            this.lbRecordTime.Location = new System.Drawing.Point(93, 17);
            this.lbRecordTime.Name = "lbRecordTime";
            this.lbRecordTime.Size = new System.Drawing.Size(41, 12);
            this.lbRecordTime.TabIndex = 1;
            this.lbRecordTime.Text = "label1";
            // 
            // btnOpenRecordFileFold
            // 
            this.btnOpenRecordFileFold.AutoSize = true;
            this.btnOpenRecordFileFold.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOpenRecordFileFold.Image = global::RecrodTest.Properties.Resources.FolderOpenLightBlue;
            this.btnOpenRecordFileFold.Location = new System.Drawing.Point(320, 26);
            this.btnOpenRecordFileFold.Name = "btnOpenRecordFileFold";
            this.btnOpenRecordFileFold.Size = new System.Drawing.Size(22, 22);
            this.btnOpenRecordFileFold.TabIndex = 2;
            this.btnOpenRecordFileFold.UseVisualStyleBackColor = true;
            this.btnOpenRecordFileFold.Click += new System.EventHandler(this.btnOpenRecordFileFold_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 48);
            this.Controls.Add(this.btnOpenRecordFileFold);
            this.Controls.Add(this.lbRecordTime);
            this.Controls.Add(this.btnRecord);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "内录程序";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Label lbRecordTime;
        private System.Windows.Forms.Button btnOpenRecordFileFold;
    }
}


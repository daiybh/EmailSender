namespace EmailSender
{
    partial class OperateForm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblEmailSent = new System.Windows.Forms.Label();
            this.lblEmailInQueue = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTotalEmails = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.chbShowLog = new System.Windows.Forms.CheckBox();
            this.EmailTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtLog);
            this.groupBox1.Location = new System.Drawing.Point(24, 214);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(633, 443);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "邮件日志";
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(6, 19);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(621, 418);
            this.txtLog.TabIndex = 0;
            this.txtLog.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "已经发送数：";
            // 
            // lblEmailSent
            // 
            this.lblEmailSent.AutoSize = true;
            this.lblEmailSent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEmailSent.Location = new System.Drawing.Point(112, 76);
            this.lblEmailSent.Name = "lblEmailSent";
            this.lblEmailSent.Size = new System.Drawing.Size(14, 13);
            this.lblEmailSent.TabIndex = 4;
            this.lblEmailSent.Text = "0";
            // 
            // lblEmailInQueue
            // 
            this.lblEmailInQueue.AutoSize = true;
            this.lblEmailInQueue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEmailInQueue.Location = new System.Drawing.Point(112, 105);
            this.lblEmailInQueue.Name = "lblEmailInQueue";
            this.lblEmailInQueue.Size = new System.Drawing.Size(14, 13);
            this.lblEmailInQueue.TabIndex = 6;
            this.lblEmailInQueue.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "队列邮件数：";
            // 
            // lblTotalEmails
            // 
            this.lblTotalEmails.AutoSize = true;
            this.lblTotalEmails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalEmails.Location = new System.Drawing.Point(112, 46);
            this.lblTotalEmails.Name = "lblTotalEmails";
            this.lblTotalEmails.Size = new System.Drawing.Size(14, 13);
            this.lblTotalEmails.TabIndex = 8;
            this.lblTotalEmails.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "总邮件数：";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(245, 41);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 9;
            this.btnLoad.Text = "刷新";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(111, 148);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 46);
            this.btnSend.TabIndex = 13;
            this.btnSend.Text = "开始";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(241, 148);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 46);
            this.btnPause.TabIndex = 14;
            this.btnPause.Text = "暂停";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(366, 148);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 46);
            this.btnStop.TabIndex = 15;
            this.btnStop.Text = "结束";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // chbShowLog
            // 
            this.chbShowLog.AutoSize = true;
            this.chbShowLog.Checked = true;
            this.chbShowLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbShowLog.Location = new System.Drawing.Point(496, 164);
            this.chbShowLog.Name = "chbShowLog";
            this.chbShowLog.Size = new System.Drawing.Size(74, 17);
            this.chbShowLog.TabIndex = 16;
            this.chbShowLog.Text = "显示日志";
            this.chbShowLog.UseVisualStyleBackColor = true;
            // 
            // EmailTimer
            // 
            this.EmailTimer.Tick += new System.EventHandler(this.EmailTimer_Tick);
            // 
            // OperateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chbShowLog);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.lblTotalEmails);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblEmailInQueue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblEmailSent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "OperateForm";
            this.Size = new System.Drawing.Size(681, 678);
            this.Load += new System.EventHandler(this.OperateForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblEmailSent;
        private System.Windows.Forms.Label lblEmailInQueue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTotalEmails;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.CheckBox chbShowLog;
        private System.Windows.Forms.Timer EmailTimer;
    }
}

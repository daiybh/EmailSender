namespace EmailSender
{
    partial class MainForm
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
            this.statubar = new System.Windows.Forms.StatusStrip();
            this.status_EmailServerStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.status_workstatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.topmenu = new System.Windows.Forms.MenuStrip();
            this.menu_file = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_config = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_aboutus = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnMain = new System.Windows.Forms.Button();
            this.btnConifg = new System.Windows.Forms.Button();
            this.btnEmailTemplate = new System.Windows.Forms.Button();
            this.statubar.SuspendLayout();
            this.topmenu.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statubar
            // 
            this.statubar.BackColor = System.Drawing.SystemColors.ControlLight;
            this.statubar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statubar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.statubar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status_EmailServerStatus,
            this.status_workstatus});
            this.statubar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statubar.Location = new System.Drawing.Point(0, 686);
            this.statubar.Name = "statubar";
            this.statubar.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statubar.Size = new System.Drawing.Size(850, 22);
            this.statubar.TabIndex = 1;
            // 
            // status_EmailServerStatus
            // 
            this.status_EmailServerStatus.Name = "status_EmailServerStatus";
            this.status_EmailServerStatus.Size = new System.Drawing.Size(72, 17);
            this.status_EmailServerStatus.Text = "Server empty";
            // 
            // status_workstatus
            // 
            this.status_workstatus.Name = "status_workstatus";
            this.status_workstatus.Size = new System.Drawing.Size(29, 17);
            this.status_workstatus.Text = "Stop";
            // 
            // topmenu
            // 
            this.topmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_file,
            this.menu_config,
            this.menu_aboutus});
            this.topmenu.Location = new System.Drawing.Point(0, 0);
            this.topmenu.Name = "topmenu";
            this.topmenu.Size = new System.Drawing.Size(850, 24);
            this.topmenu.TabIndex = 2;
            this.topmenu.Text = "topmenu";
            // 
            // menu_file
            // 
            this.menu_file.Name = "menu_file";
            this.menu_file.Size = new System.Drawing.Size(43, 20);
            this.menu_file.Text = "文件";
            // 
            // menu_config
            // 
            this.menu_config.Name = "menu_config";
            this.menu_config.Size = new System.Drawing.Size(43, 20);
            this.menu_config.Text = "配置";
            this.menu_config.Click += new System.EventHandler(this.menu_config_Click);
            // 
            // menu_aboutus
            // 
            this.menu_aboutus.Name = "menu_aboutus";
            this.menu_aboutus.Size = new System.Drawing.Size(43, 20);
            this.menu_aboutus.Text = "关于";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnEmailTemplate);
            this.splitContainer1.Panel1.Controls.Add(this.btnMain);
            this.splitContainer1.Panel1.Controls.Add(this.btnConifg);
            this.splitContainer1.Panel1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.splitContainer1.Size = new System.Drawing.Size(850, 656);
            this.splitContainer1.SplitterDistance = 171;
            this.splitContainer1.TabIndex = 3;
            // 
            // btnMain
            // 
            this.btnMain.Location = new System.Drawing.Point(12, 23);
            this.btnMain.Name = "btnMain";
            this.btnMain.Size = new System.Drawing.Size(144, 47);
            this.btnMain.TabIndex = 2;
            this.btnMain.Text = "操作";
            this.btnMain.UseVisualStyleBackColor = true;
            this.btnMain.Click += new System.EventHandler(this.btnMain_Click);
            // 
            // btnConifg
            // 
            this.btnConifg.Location = new System.Drawing.Point(11, 129);
            this.btnConifg.Name = "btnConifg";
            this.btnConifg.Size = new System.Drawing.Size(144, 47);
            this.btnConifg.TabIndex = 1;
            this.btnConifg.Text = "配置服务器";
            this.btnConifg.UseVisualStyleBackColor = true;
            this.btnConifg.Click += new System.EventHandler(this.btnConifg_Click);
            // 
            // btnEmailTemplate
            // 
            this.btnEmailTemplate.Location = new System.Drawing.Point(11, 76);
            this.btnEmailTemplate.Name = "btnEmailTemplate";
            this.btnEmailTemplate.Size = new System.Drawing.Size(144, 47);
            this.btnEmailTemplate.TabIndex = 3;
            this.btnEmailTemplate.Text = "邮件模板";
            this.btnEmailTemplate.UseVisualStyleBackColor = true;
            this.btnEmailTemplate.Click += new System.EventHandler(this.btnEmailTemplate_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 708);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statubar);
            this.Controls.Add(this.topmenu);
            this.MainMenuStrip = this.topmenu;
            this.Name = "MainForm";
            this.Text = "邮件群发工具";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statubar.ResumeLayout(false);
            this.statubar.PerformLayout();
            this.topmenu.ResumeLayout(false);
            this.topmenu.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statubar;
        private System.Windows.Forms.ToolStripStatusLabel status_EmailServerStatus;
        private System.Windows.Forms.ToolStripStatusLabel status_workstatus;
        private System.Windows.Forms.MenuStrip topmenu;
        private System.Windows.Forms.ToolStripMenuItem menu_file;
        private System.Windows.Forms.ToolStripMenuItem menu_config;
        private System.Windows.Forms.ToolStripMenuItem menu_aboutus;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnConifg;
        private System.Windows.Forms.Button btnMain;
        private System.Windows.Forms.Button btnEmailTemplate;
    }
}


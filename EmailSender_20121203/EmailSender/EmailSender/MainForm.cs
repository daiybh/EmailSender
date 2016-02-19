using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.Data.Odbc;

namespace EmailSender
{
    public partial class MainForm : Form
    {
        ServerConfigForm _configForm;
        EmailTemplateForm _templateForm;
        OperateForm _operateForm;
        RecipientsForm _recipientsForm;
                
        public MainForm()
        {
            InitializeComponent();
        }
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            _configForm = new ServerConfigForm();
            if (splitContainer1.Panel2.Height > _configForm.Height)
            {
                _configForm.Width = splitContainer1.Panel2.Width;
                _configForm.Height = splitContainer1.Panel2.Height;
            }

            _templateForm = new EmailTemplateForm();
            if (splitContainer1.Panel2.Height > _templateForm.Height)
            {
                _templateForm.Width = splitContainer1.Panel2.Width;
                _templateForm.Height = splitContainer1.Panel2.Height;
                _templateForm.Visible = false;
            }

            _operateForm = new OperateForm();
            if (splitContainer1.Panel2.Height > _operateForm.Height)
            {
                _operateForm.Width = splitContainer1.Panel2.Width;
                _operateForm.Height = splitContainer1.Panel2.Height;
                _operateForm.Visible = false;
            }

            _recipientsForm = new RecipientsForm();
            if (splitContainer1.Panel2.Height > _recipientsForm.Height)
            {
                _recipientsForm.Width = splitContainer1.Panel2.Width;
                _recipientsForm.Height = splitContainer1.Panel2.Height;
                _recipientsForm.Visible = false;
            }
            
            splitContainer1.Panel2.Controls.Add(_configForm);
            splitContainer1.Panel2.Controls.Add(_templateForm);
            splitContainer1.Panel2.Controls.Add(_operateForm);
            splitContainer1.Panel2.Controls.Add(_recipientsForm);

            //splitContainer1.Panel2.Controls[0].set
        }

        private void btnEmailTemplate_Click(object sender, EventArgs e)
        {
            HideAllForms();
            _templateForm.Visible = true;
           
        }

        private void HideAllForms()
        {
            foreach (Control c in splitContainer1.Panel2.Controls)
            {
                c.Visible = false;
            }
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            HideAllForms();
            _operateForm.Visible = true;
        }

        private void btnConifg_Click(object sender, EventArgs e)
        {
            HideAllForms();
            _configForm.Visible = true;
        }

        private void menu_config_Click(object sender, EventArgs e)
        {
            HideAllForms();
            _configForm.Visible = true;
        }

        private void btnEmailList_Click(object sender, EventArgs e)
        {
            HideAllForms();
            _recipientsForm.Visible = true;
        }
    }
}
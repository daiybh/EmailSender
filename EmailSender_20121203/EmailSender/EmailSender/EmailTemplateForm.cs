using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace EmailSender
{
    public partial class EmailTemplateForm : UserControl
    {
        string _subject = "subject.txt";
        string _content = "content.html";
        public EmailTemplateForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                StreamWriter writer = new StreamWriter(_subject);
                writer.Write(txtSubject.Text);
                writer.Close();

                StreamWriter contentwriter = new StreamWriter(_content);
                contentwriter.WriteLine(txtContent.Text);
                contentwriter.Close();
                MessageBox.Show("±£´æ³É¹¦£¡");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EmailTemplateForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(_subject))
                {
                    txtSubject.Text = File.ReadAllText(_subject);
                }
                if (File.Exists(_content))
                {
                    txtContent.Text = File.ReadAllText(_content);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

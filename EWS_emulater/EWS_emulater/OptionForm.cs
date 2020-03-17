using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EWS_emulater
{
    public partial class OptionForm : Form
    {
        public event TcpConnectHandle TcpConnectEvt;
        public delegate void TcpConnectHandle();

        public OptionForm()
        {
            InitializeComponent();
        }

        private void OptionForm_Load(object sender, EventArgs e)
        {
            this.txtEWSPort.Text = Properties.Settings.Default.Port;

            if (Properties.Settings.Default.EWSStatus)
            {
                this.radioButton1.Checked = true;
            }
            else
            {
                this.radioButton2.Checked = true;
            }

            if (Properties.Settings.Default.LoginStatus == "0")
            {
                this.radioButton4.Checked = true;
            }
            else if (Properties.Settings.Default.LoginStatus == "1")
            {
                this.radioButton3.Checked = true;
            }
            else
            {
                this.radioButton5.Checked = true;
            }

            this.numericUpDown1.Value = int.Parse(Properties.Settings.Default.NorTime);
            this.numericUpDown2.Value = int.Parse(Properties.Settings.Default.SpcTime);

            this.textBox1.Text = Properties.Settings.Default.db_ip;
            this.textBox2.Text = Properties.Settings.Default.db_port;
            this.textBox3.Text = Properties.Settings.Default.db_id;
            this.textBox4.Text = Properties.Settings.Default.db_pw;
            this.textBox5.Text = Properties.Settings.Default.db_sid;
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Port = this.txtEWSPort.Text;

            if (radioButton1.Checked)
            {
                Properties.Settings.Default.EWSStatus = true;
            }
            else
            {
                Properties.Settings.Default.EWSStatus = false;
            }

            if (radioButton4.Checked)
            {
                Properties.Settings.Default.LoginStatus = "0";
            }
            else if(radioButton3.Checked)
            {
                Properties.Settings.Default.LoginStatus = "1";
            }
            else
            {
                Properties.Settings.Default.LoginStatus = "2";
            }

            Properties.Settings.Default.NorTime = this.numericUpDown1.Value.ToString();
            Properties.Settings.Default.SpcTime = this.numericUpDown2.Value.ToString();
            Properties.Settings.Default.db_ip = this.textBox1.Text;
            Properties.Settings.Default.db_port = this.textBox2.Text;
            Properties.Settings.Default.db_id = this.textBox3.Text;
            Properties.Settings.Default.db_pw = this.textBox4.Text;
            Properties.Settings.Default.db_sid = this.textBox5.Text;
            Properties.Settings.Default.Save();

            if (TcpConnectEvt != null)
            {
                this.TcpConnectEvt();
            }

            this.Close();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
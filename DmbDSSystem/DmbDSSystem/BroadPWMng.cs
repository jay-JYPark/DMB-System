using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DmbDSSystem
{
    public partial class BroadPWMng : Form
    {
        public string Password
        {
            get { return this.password.Text; }
            set { this.password.Text = value; }
        }

        public string RePassword
        {
            get { return this.repassword.Text; }
            set { this.repassword.Text = value; }
        }

        public BroadPWMng()
        {
            InitializeComponent();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void cancleBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void password_TextChanged(object sender, EventArgs e)
        {
            if (this.password.Text != string.Empty && this.repassword.Text != string.Empty)
            {
                okBtn.Enabled = true;
            }
            else
            {
                okBtn.Enabled = false;
            }
        }

        private void repassword_TextChanged(object sender, EventArgs e)
        {
            if (this.password.Text != string.Empty && this.repassword.Text != string.Empty)
            {
                okBtn.Enabled = true;
            }
            else
            {
                okBtn.Enabled = false;
            }
        }

        private void repassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.password.Text != string.Empty && this.repassword.Text != string.Empty)
            {
                if (e.KeyChar == (char)13)
                {
                    okBtn_Click(sender, e);
                }
            }
        }

        private void password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.password.Text != string.Empty && this.repassword.Text != string.Empty)
            {
                if (e.KeyChar == (char)13)
                {
                    okBtn_Click(sender, e);
                }
            }
        }
    }
}
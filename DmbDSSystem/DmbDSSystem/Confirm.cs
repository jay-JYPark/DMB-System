using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DmbDSSystem
{
    public partial class Confirm : Form
    {
        public Confirm()
        {
            InitializeComponent();
        }

        private void ConfirmOkBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void ConfirmCancleBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void ConfirmTB_TextChanged(object sender, EventArgs e)
        {
            if (ConfirmTB.Text == "")
            {
                ConfirmOkBtn.Enabled = false;
            }
            else
            {
                ConfirmOkBtn.Enabled = true;
            }
        }

        /// <summary>
        /// 암호 넣은 텍스트박스의 Text를 반환한다.
        /// </summary>
        /// <returns>
        /// 리턴값은 String
        /// </returns>
        public string getpasswordText()
        {
            return ConfirmTB.Text;
        }

        /// <summary>
        /// 암호 넣는 텍스트박스의 Text를 get, set 한다.
        /// </summary>
        public string password
        {
            get
            {
                return ConfirmTB.Text;
            }
            set
            {
                ConfirmTB.Text = value;
            }
        }

        private void ConfirmTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.ConfirmTB.Text != string.Empty)
            {
                if (e.KeyChar == (char)13)
                {
                    ConfirmOkBtn_Click(sender, e);
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DmbDSSystem
{
    public partial class VersionForm : Form
    {
        public VersionForm()
        {
            InitializeComponent();
        }

        public VersionForm(string titleStr, string verStr) : this()
        {
            this.VerTB.Text += verStr;
            this.MainLB.Text = titleStr;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        private void VersionForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainLB_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WarningLB_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
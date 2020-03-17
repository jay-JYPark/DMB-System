using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EWSSystem
{
    public partial class ProgramInfoForm : Form
    {

        /// <summary>
        /// 기본 생성자
        /// </summary>
        public ProgramInfoForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 일반 생성자
        /// </summary>
        /// <param name="titleStr"></param>
        /// <param name="verStr"></param>
        public ProgramInfoForm(string titleStr, string verStr)
            : this()
        {
            this.VerTB.Text += verStr;
            this.MainLB.Text = titleStr;
        }


    }
}

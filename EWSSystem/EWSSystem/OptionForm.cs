using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace EWSSystem
{
    public partial class OptionForm : Form
    {
        /// <summary>
        /// 기본 생성자
        /// </summary>
        public OptionForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 폼이 로드될 때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionForm_Load(object sender, EventArgs e)
        {
            //Config 값 UI 설정 ->수정이 필요함.. 로드시 오래 걸림..

            //TCP Server 
            this.txtPort.Text = ConfigurationManager.AppSettings["PORT"];
            //MUX 기본 
            this.txtMUXTcId.Text = ConfigurationManager.AppSettings["MUXTcId"];
            this.txtMUXRetry.Text = ConfigurationManager.AppSettings["MUXRetry"];

            //특수메세지
            this.txtSpcMaxId.Text = ConfigurationManager.AppSettings["SpcMaxId"];
            this.txtSpcRepeatNum.Text = ConfigurationManager.AppSettings["SpcRepeatNum"];
            this.txtSpcWaitTime.Text = ConfigurationManager.AppSettings["SpcWaitTime"];
            this.txtSpcCycle.Text = ConfigurationManager.AppSettings["SpcCycle"];

            //일반메세지
            this.txtNorMaxId.Text = ConfigurationManager.AppSettings["NorMaxId"];
            this.txtNorProcNum.Text = ConfigurationManager.AppSettings["NorProcNum"];
            this.txtNorTransTime.Text = ConfigurationManager.AppSettings["NorTransTime"];
            this.txtNorCycle.Text = ConfigurationManager.AppSettings["NorCycle"];
            this.txtNorWaitTime.Text = ConfigurationManager.AppSettings["NorWaitTime"];
            this.txtNorNotAcceptProcTime.Text = ConfigurationManager.AppSettings["NorNotAcceptProcTime"];
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            //ConfigurationManager.AppSettings["NorMaxId"] = "10000";
            this.Close();
        }
    }
}

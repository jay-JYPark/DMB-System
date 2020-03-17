using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.Ports;

using ADEng.Library.DMB;
using TcpCommon;

namespace DmbDSSystem
{
    public partial class OptionDlg : Form
    {
        #region event
        public event SetDSTypeHandle SetDSType;
        public event BroadSelectedEventHandle BroadSelect;
        #endregion

        #region delegate
        public delegate void SetDSTypeHandle();
        public delegate void BroadSelectedEventHandle();
        private delegate void TcpCliDisConnectMessageHandle();
        #endregion

        #region Instance
        private SocketClient socketclt = null;
        private EventLogMng eventLogMng = null;
        private DataManager datamng = null;
        private Confirm confirm = null;
        private PassWord password = null;
        private BroadPWMng broadpwmng = null;
        #endregion

        #region Variable
        private bool broadSelState = false;
        #endregion

        public OptionDlg()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            datamng = DataManager.getInstance();
            socketclt = SocketClient.getInstance();
            this.socketclt.tcpDiconnectedEvt += new SocketClient.TcpClientDisConnectedEventHandle(socketclt_tcpDiconnectedEvt);
            this.BroadInit();

            this.txtEWSipAddr.Text = Properties.Settings.Default.cnfEWSIP;
            this.txtEWSPort.Text = Properties.Settings.Default.cnfEWSPort;
            this.txtSFipAddr.Text = TcpProfileMng.LstNavTcpProfileData[0].IpAddr;
            this.txtPort.Text = TcpProfileMng.LstNavTcpProfileData[0].Port.ToString();
            this.txtSpSfipAddr.Text = TcpProfileMng.LstSpTcpProfileData[0].IpAddr;
            this.txtSpSfPort.Text = TcpProfileMng.LstSpTcpProfileData[0].Port.ToString();

            foreach (TreeNode tn in this.BroadTV.Nodes)
            {
                if (tn.Name == Properties.Settings.Default.cnfBroad.ToString())
                {
                    tn.Checked = true;
                }
            }

            eventLogMng = new EventLogMng();

            if (this.socketclt.ConState)
            {
                TcpConBtn.Enabled = false;
                TcpDisconBtn.Enabled = true;
            }
            else
            {
                TcpConBtn.Enabled = true;
                TcpDisconBtn.Enabled = false;
            }

            if (this.AdminCB.Checked == true)
            {
                this.BroadTV.Enabled = true;
            }
            else
            {
                this.BroadTV.Enabled = false;
            }

            base.OnLoad(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.socketclt.tcpDiconnectedEvt -= new SocketClient.TcpClientDisConnectedEventHandle(socketclt_tcpDiconnectedEvt);

            if (this.broadSelState)
            {
                if (BroadSelect != null)
                {
                    BroadSelect();
                }
            }

            base.OnClosing(e);
        }

        #region UI 사용자 이벤트
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            this.Save();
            this.Close();
        }

        private void txtSFipAddr_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtPort_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtEWSipAddr_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtEWSPort_TextChanged(object sender, EventArgs e)
        {
        }

        private void TcpConBtn_Click(object sender, EventArgs e)
        {
            try
            {
                this.socketclt = SocketClient.getInstance();

                if (!this.socketclt.ConState)
                {
                    if (!tcpCltStart())
                    {
                        MessageBox.Show("연결에 실패하였습니다.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;                        
                    }
                    else
                    {
                        TcpConBtn.Enabled = false;
                        TcpDisconBtn.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("이미 연결상태 입니다.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("TcpConBtn_Click - " + ex.Message);
            }
        }

        private void TcpDisconBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.socketclt.tcpClientStop())
                {
                    TcpConBtn.Enabled = true;
                    TcpDisconBtn.Enabled = false;
                }
                else
                {
                    MessageBox.Show("종료에 실패하였습니다.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("TcpDisconBtn_Click - " + ex.Message);
            }
        }
        #endregion

        #region TCP Start
        private Boolean tcpCltStart()
        {
            try
            {
                socketclt = SocketClient.getInstance();

                if (!socketclt.socketClientInit(this.txtEWSipAddr.Text.ToString(), this.txtEWSPort.Text.ToString()))
                {
                    return false;
                }

                this.socketclt.ConState = true;
                this.socketclt.recieveThread();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        private void Save()
        {
            Properties.Settings.Default.cnfEWSIP = this.txtEWSipAddr.Text;
            Properties.Settings.Default.cnfEWSPort = this.txtEWSPort.Text;

            foreach (TreeNode tn in this.BroadTV.Nodes)
            {
                if (tn.Checked)
                {
                    Properties.Settings.Default.cnfBroad = int.Parse(tn.Name);

                    if (SetDSType != null)
                        SetDSType();
                }
            }

            Properties.Settings.Default.Save();

            TcpProfileMng.LstNavTcpProfileData[0].IpAddr = this.txtSFipAddr.Text;
            TcpProfileMng.LstNavTcpProfileData[0].Port = int.Parse(this.txtPort.Text);
            TcpProfileMng.LstSpTcpProfileData[0].IpAddr = this.txtSpSfipAddr.Text;
            TcpProfileMng.LstSpTcpProfileData[0].Port = int.Parse(this.txtSpSfPort.Text);
            TcpProfileMng.SaveNavProfileConfig();
            TcpProfileMng.SaveSpProfileConfig();
        }

        void socketclt_tcpDiconnectedEvt()
        {
            try
            {
                if (this.TcpConBtn.InvokeRequired && this.TcpDisconBtn.InvokeRequired)
                {
                    BeginInvoke(new TcpCliDisConnectMessageHandle(tcpClientDisconnectChange));
                }
                else
                {
                    this.tcpClientDisconnectChange();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("socketclt_tcpDiconnectedEvt - " + ex.Message);
            }
        }

        private void tcpClientDisconnectChange()
        {
            try
            {
                this.TcpConBtn.Enabled = true;
                this.TcpDisconBtn.Enabled = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void BroadInit()
        {
            foreach (stage st in this.datamng.BroadList)
            {
                this.BroadTV.Nodes.Add(st.ID.ToString(), st.Name.ToString());
                this.BroadTV.Nodes[st.ID.ToString()].Checked = false;
            }
        }

        private void BroadTV_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Checked)
                {
                    foreach (TreeNode tn in this.BroadTV.Nodes)
                    {
                        if (tn.Name != e.Node.Name)
                        {
                            tn.Checked = false;
                            this.broadSelState = true;
                        }
                    }
                }
                else
                {
                    e.Node.Checked = true;
                }
            }
        }

        private void AdminCB_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.AdminCB.Checked == true)
                {
                    while (true)
                    {
                        confirm = new Confirm();
                        confirm.ShowDialog(this);

                        if (confirm.DialogResult == DialogResult.OK)
                        {
                            password = new PassWord();

                            if (password.ConfirmPassWord(confirm.getpasswordText()) == true)
                            {
                                this.BroadTV.Enabled = true;
                                break;
                            }
                            else
                            {
                                if (MessageBox.Show("암호가 정확하지 않습니다.\n다시 시도하겠습니까?", this.Text, MessageBoxButtons.RetryCancel, MessageBoxIcon.Information) == DialogResult.Retry)
                                {
                                }
                                else
                                {
                                    this.AdminCB.Checked = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            this.AdminCB.Checked = false;
                            break;
                        }
                    }
                }
                else
                {
                    this.BroadTV.Enabled = false;
                }

                confirm = null;
                password = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("OptionDlg.AdminCB_CheckedChanged - " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            while (true)
            {
                confirm = new Confirm();
                confirm.ShowDialog(this);

                if (confirm.DialogResult == DialogResult.OK)
                {
                    password = new PassWord();

                    if (password.ConfirmPassWord(confirm.getpasswordText()) == true)
                    {
                        while (true)
                        {
                            broadpwmng = new BroadPWMng();
                            broadpwmng.ShowDialog(this);

                            if (broadpwmng.DialogResult == DialogResult.OK)
                            {
                                if (broadpwmng.Password.Trim() == broadpwmng.RePassword.Trim())
                                {
                                    Properties.Settings.Default.BroadPW = broadpwmng.Password.Trim();
                                    Properties.Settings.Default.Save();

                                    MessageBox.Show("암호를 변경하였습니다.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                                else
                                {
                                    MessageBox.Show("암호를 확인하세요.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("암호가 정확하지 않습니다.\n다시 시도하겠습니까?", this.Text, MessageBoxButtons.RetryCancel, MessageBoxIcon.Information) == DialogResult.Retry)
                        {
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }
}
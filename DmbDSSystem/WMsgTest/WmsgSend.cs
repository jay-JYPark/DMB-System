using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using ADEng.Library.DMB;
using adeng.comm;
using DmbProtocol;
using TcpCommon;

namespace WMsgTest
{
    public partial class WmsgSend : Form
    {
        private enum ProtoCode1
        {
            전국 = 1, 정부지정 = 2, 행정동 = 3, 특수수신기 = 4
        };

        private enum Destflag
        {
            지역 = 0, 장비 = 1, 그룹 = 2
        };

        private ControlRequest controlrequest = null;
        private DmbTypeDisasterDlg dmbTypeDisasterDlg = null;
        private DmbTypeDevice dmbTypeDevice = null;
        private DataManager datamng = null;
        private RecvMsgs recvform = null;
        private Thread sendMsgTD = null;
        private delegate void InvokeWmsgSendTest();
        private delegate void InvokeSetText(string _str);
        private delegate void InvokeSetTextByte(byte[] _byte);
        private uint Priority = 0;

        public WmsgSend()
        {
            controlrequest = new ControlRequest();
            datamng = DataManager.getInstance();
            recvform = RecvMsgs.getInstance();
            recvform.MainTextBox = string.Empty;
            recvform.Show();

            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            recvform.Hide();

            base.OnClosing(e);
        }

        /// <summary>
        /// 닫기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (this.sendMsgTD != null)
            {
                if (this.sendMsgTD.IsAlive)
                {
                    this.sendMsgTD.Abort();
                }
            }

            this.Close();
        }

        /// <summary>
        /// SF로 발령 메시지 전송
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.wMessageVaildator())
            {
                return;
            }

            if (this.textBox6.Text.ToString() != "")
            {
                string device = string.Empty;

                foreach (DeviceInfo di in datamng.DeviceInfoList)
                {
                    if (di.ID.ToString() == textBox6.Tag.ToString())
                    {
                        device = di.Name.ToString();
                    }
                }

                controlrequest.MakeMWMSGRG((uint)1, uint.Parse(this.textBox6.Tag.ToString()), device);
            }

            this.SetWMsgInit();
            //재난지역형식이 전국으로 고정.
            controlrequest.MakeWMSG(false, this.Priority, uint.Parse(this.textBox1.Tag.ToString()),
                (uint)ProtoCode1.전국, controlrequest.MapmsgrgList, this.textBox2.Text.ToString(),
                datamng.CurUser, true, this.textBox3.Text.ToString(), this.textBox4.Text.ToString(), checkBox1.Checked, checkBox2.Checked, checkBox3.Checked);
            Properties.Settings.Default.totalityFlag = false;
            controlrequest.Processing("재난 발령");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //지역
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.dmbTypeDevice == null)
            {
                this.dmbTypeDevice = new DmbTypeDevice();
                this.dmbTypeDevice.delegateSelectDeviceListViewItem += new DmbTypeDevice.DelegateSelectDeviceListViewItem(dmbTypeDevice_delegateSelectDeviceListViewItem);
            }

            if (this.dmbTypeDevice.ShowDialog() == DialogResult.Cancel)
            {
                this.dmbTypeDevice = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.dmbTypeDisasterDlg == null)
            {
                this.dmbTypeDisasterDlg = new DmbTypeDisasterDlg();
                this.dmbTypeDisasterDlg.delegateSelectDisasterListViewItem +=
                      new DmbTypeDisasterDlg.DelegateSelectDisasterListViewItem(selectDisaster);
            }

            if (this.dmbTypeDisasterDlg.ShowDialog() == DialogResult.Cancel)
            {
                this.dmbTypeDisasterDlg = null;
            }
        }

        /// <summary>
        /// 재난 코드 선택 시 재난 정보를 텍스트 박스에 삽입
        /// </summary>
        /// <param name="_item"></param>
        private void selectDisaster(ListViewItem _item)
        {
            if (_item.SubItems.Count < 1)
            {
                return;
            }

            textBox1.Tag = _item.Name;
            textBox1.Text = _item.SubItems[2].Text.ToString() + "(" + _item.SubItems[3].Text.ToString() + ")";
        }

        void dmbTypeDevice_delegateSelectDeviceListViewItem(ListViewItem _item)
        {
            if (_item.SubItems.Count < 1)
            {
                return;
            }

            textBox6.Tag = _item.Name;
            textBox6.Text = _item.SubItems[2].Text.ToString() + "(" + _item.SubItems[3].Text.ToString() + ")";
        }

        private void SetWMsgInit()
        {
            //우선순위 셋팅
            this.Priority = (rdoUnKnwon.Checked) ? (uint)1 : (rdoNormal.Checked) ? (uint)2 : (rdoFast.Checked) ? (uint)3 : (uint)4;
        }

        /// <summary>
        /// WMessage 구성을 위한 데이터 검증
        /// </summary>
        private Boolean wMessageVaildator()
        {
            //임시로 막아놓음.
            //if (this.controlrequest.MapmsgrgList < 1)
            //{
            //    MessageBox.Show("지역을 선택하세요");
            //    return true;
            //}

            //if (this.textBox6.Text.Length < 1)
            //{
            //    MessageBox.Show("단말을 선택하세요");
            //    return true;
            //}

            if (this.checkBox1.Checked == false && this.checkBox2.Checked == false && this.checkBox3.Checked == false)
            {
                MessageBox.Show("발령 구분을 한가지 이상 선택 해 주세요", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }

            if (this.textBox1.Text.Length < 1)
            {
                MessageBox.Show("재난 종류를 선택하세요", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }

            if (this.checkBox1.Checked == true && this.textBox2.Text.Trim().Length < 1)
            {
                MessageBox.Show("DMB 단문을 입력해 주세요", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
                return true;
            }

            if (this.checkBox2.Checked == true && this.textBox3.Text.Trim().Length < 1)
            {
                MessageBox.Show("TTS 단문을 입력해 주세요", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox3.Focus();
                return true;
            }

            if (this.checkBox3.Checked == true && this.textBox4.Text.Trim().Length < 1)
            {
                MessageBox.Show("CBS 단문을 입력해 주세요", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox4.Focus();
                return true;
            }

            if (this.textBox2.Text.Length > WMessage.cLen_message)
            {
                MessageBox.Show("DMB 단문제한을 초과하였습니다.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
                return true;
            }

            if (this.textBox3.Text.Length > WMessage.cLen_ttsMsg)
            {
                MessageBox.Show("TTS 단문제한을 초과하였습니다.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox3.Focus();
                return true;
            }

            if (this.textBox4.Text.Length > WMessage.cLen_smsMsg)
            {
                MessageBox.Show("CBS 단문제한을 초과하였습니다.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox4.Focus();
                return true;
            }

            if (this.checkBox1.Checked == false && this.textBox2.Text.Trim().Length > 0)
            {
                this.textBox2.Text = string.Empty;
            }

            if (this.checkBox2.Checked == false && this.textBox3.Text.Trim().Length > 0)
            {
                this.textBox3.Text = string.Empty;
            }

            if (this.checkBox3.Checked == false && this.textBox4.Text.Trim().Length > 0)
            {
                this.textBox4.Text = string.Empty;
            }
            return false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox2.Text.Length > 0 && this.checkBox1.Checked == false)
            {
                this.checkBox1.Checked = true;
            }
            else if (this.textBox2.Text.Length < 1)
            {
                this.checkBox1.Checked = false;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox3.Text.Length > 0 && this.checkBox2.Checked == false)
            {
                this.checkBox2.Checked = true;
            }
            else if (this.textBox3.Text.Length < 1)
            {
                this.checkBox2.Checked = false;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox4.Text.Length > 0 && this.checkBox3.Checked == false)
            {
                this.checkBox3.Checked = true;
            }
            else if (this.textBox4.Text.Length < 1)
            {
                this.checkBox3.Checked = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked == false)
            {
                this.textBox2.Text = string.Empty;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox2.Checked == false)
            {
                this.textBox3.Text = string.Empty;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox3.Checked == false)
            {
                this.textBox4.Text = string.Empty;
            }
        }

        /// <summary>
        /// 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            this.textBox6.Text = string.Empty;
            this.textBox1.Text = string.Empty;
            this.tblCnt.Text = "10";
            this.tblInterval.Text = "1000";

            if (this.checkBox1.Checked == true)
            {
                checkBox1.Checked = false;
            }
            if (this.checkBox2.Checked == true)
            {
                checkBox2.Checked = false;
            }
            if (this.checkBox3.Checked == true)
            {
                checkBox3.Checked = false;
            }
            if (this.rdoUnKnwon.Checked == false)
            {
                this.rdoUnKnwon.Checked = true;
            }
            if (this.cbRepeat.Checked == true)
            {
                this.cbRepeat.Checked = false;
            }
        }
        
        /// <summary>
        /// 임시로 만든 WMessage로 TCP로 송신한다.
        /// </summary>
        /// <param name="wmessage"></param>
        private void TcpSendWmsg(WMessage wmessage)
        {
            try
            {
                if (wmessage.SendPart != WMessage.E_sendPart.SMS)
                {
                    SocketClient soc = SocketClient.getInstance();

                    if (((wmessage.SOPT_TTS) && (wmessage.SOPT_DMB)))
                    {
                        //일반 발령
                        Proto02 p02 = ProtoMng.GetPObject("02") as Proto02;
                        byte[] body = p02.BodyMake(wmessage);
                        byte[] totproto = p02.totMake(body);

                        soc.tcpClientSndMsg(totproto);

                        if (this.recvform.InvokeRequired)
                        {
                            this.Invoke(new InvokeSetTextByte(this.PrintSendMsg), new object[] { totproto });
                        }
                        else
                        {
                            this.PrintSendMsg(totproto);
                        }

                        //특수수신기 발령
                        Proto01 p01 = ProtoMng.GetPObject("01") as Proto01;
                        byte[] devbody = p01.BodyMake(wmessage);
                        byte[] devtotproto = p01.totMake(devbody);

                        soc.tcpClientSndMsg(devtotproto);

                        if (this.recvform.InvokeRequired)
                        {
                            this.Invoke(new InvokeSetTextByte(this.PrintSendMsg), new object[] { devtotproto });
                        }
                        else
                        {
                            this.PrintSendMsg(devtotproto);
                        }
                    }
                    else if (wmessage.SOPT_DMB)
                    {
                        //일반 발령
                        Proto02 p02 = ProtoMng.GetPObject("02") as Proto02;
                        byte[] body = p02.BodyMake(wmessage);
                        byte[] totproto = p02.totMake(body);

                        soc.tcpClientSndMsg(totproto);

                        if (this.recvform.InvokeRequired)
                        {
                            this.Invoke(new InvokeSetTextByte(this.PrintSendMsg), new object[] { totproto });
                        }
                        else
                        {
                            this.PrintSendMsg(totproto);
                        }
                    }
                    else if (wmessage.SOPT_CONTROL || wmessage.SOPT_STOREDMESSAGE || wmessage.SOPT_WARNING || wmessage.SOPT_TTS)
                    {
                        //특수수신기 발령
                        Proto01 p01 = ProtoMng.GetPObject("01") as Proto01;
                        byte[] devbody = p01.BodyMake(wmessage);
                        byte[] devtotproto = p01.totMake(devbody);

                        soc.tcpClientSndMsg(devtotproto);

                        if (this.recvform.InvokeRequired)
                        {
                            this.Invoke(new InvokeSetTextByte(this.PrintSendMsg), new object[] { devtotproto });
                        }
                        else
                        {
                            this.PrintSendMsg(devtotproto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("TcpSendWmsg - " + ex.Message);
            }
        }

        /// <summary>
        /// 편성으로 바로 TCP 전송
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TcpSendBtn_Click(object sender, EventArgs e)
        {
            if (this.TcpSendBtn.InvokeRequired)
            {
                this.Invoke(new InvokeWmsgSendTest(this.TcpSendWMsg), new object[] { });
            }
            else
            {
                this.TcpSendWMsg();
            }
        }

        private void TcpSendWMsg()
        {
            if (this.TcpSendBtn.Text == "편성 TCP전송")
            {
                this.sendMsgTD = new Thread(new ThreadStart(this.TcpSendWMsgM));
                this.sendMsgTD.IsBackground = true;
                this.sendMsgTD.Start();

                if (this.TcpSendBtn.InvokeRequired)
                {
                    this.Invoke(new InvokeSetText(this.SetText), new object[] { "전송 그만!" });
                }
                else
                {
                    this.TcpSendBtn.Text = "전송 그만!";
                }
            }
            else
            {
                if (this.sendMsgTD != null)
                {
                    if (this.sendMsgTD.IsAlive)
                    {
                        this.sendMsgTD.Abort();
                    }
                }

                if (this.TcpSendBtn.InvokeRequired)
                {
                    this.Invoke(new InvokeSetText(this.SetText), new object[] { "편성 TCP전송" });
                }
                else
                {
                    this.TcpSendBtn.Text = "편성 TCP전송";
                }
            }
        }

        private void TcpSendWMsgM()
        {
            try
            {
                if (this.wMessageTcpVaildator())
                {
                    return;
                }

                WMessage wmsg = new WMessage();
                List<mapMessageRegion> mapmsgrList = new List<mapMessageRegion>();

                if (this.textBox6.Text.ToString() != string.Empty) //단말 선택했으면..
                {
                    string device = string.Empty;

                    foreach (DeviceInfo di in datamng.DeviceInfoList)
                    {
                        if (di.ID.ToString() == textBox6.Tag.ToString())
                        {
                            device = di.Name.ToString();
                        }
                    }

                    mapmsgrList.Add(controlrequest.RTMakeMWMSGRG((uint)1, uint.Parse(this.textBox6.Tag.ToString()), device)); //단말 셋팅
                    wmsg.MapTarget = mapmsgrList;
                    wmsg.TkRegion = (uint)ProtoCode1.특수수신기;
                    wmsg.RCount = 1;
                }
                else
                {
                    wmsg.TkRegion = (uint)ProtoCode1.전국;
                    wmsg.RCount = 0;
                }

                this.SetWMsgInit();
                wmsg.TkPriority = this.Priority;//우선순위 셋팅
                wmsg.BoolControl = false;
                wmsg.TkDisaster = uint.Parse(this.textBox1.Tag.ToString());//재난코드
                wmsg.FkUser = 1;
                wmsg.BoolProcessing = true;
                wmsg.DDateTime = DateTime.Now;
                wmsg.SendID = 5555; //재난형식을 위한 고정값
                wmsg.SendMode = 0x00;
                wmsg.SDevice_마을앰프 = true;
                wmsg.SDevice_상황실 = true;
                wmsg.SDevice_유관기관 = true;
                wmsg.ID = (uint)(wmsg.DDateTime.Year + wmsg.DDateTime.Month + wmsg.DDateTime.Day + wmsg.DDateTime.Hour + wmsg.DDateTime.Minute + wmsg.DDateTime.Second);

                if (this.checkBox1.Checked == true)
                {
                    wmsg.SOPT_DMB = true;
                    wmsg.Message = this.textBox2.Text.ToString();
                }
                if (this.checkBox2.Checked == true)
                {
                    wmsg.SOPT_TTS = true;
                    wmsg.TTSMsg = this.textBox3.Text.ToString();
                }
                if (this.checkBox3.Checked == true)
                {
                    wmsg.SOPT_SMS = true;
                    wmsg.SMSMsg = this.textBox4.Text.ToString();
                }

                if (cbRepeat.Checked)
                {
                    for (int i = 0; i < Convert.ToInt32(tblCnt.Text); i++)
                    {
                        wmsg.DDateTime = DateTime.Now;
                        wmsg.ID += (uint)1;
                        this.TcpSendWmsg(wmsg);
                        System.Threading.Thread.Sleep((Convert.ToInt32(tblInterval.Text)) * 1000);
                    }
                }
                else
                {
                    this.TcpSendWmsg(wmsg);
                }

                if (this.TcpSendBtn.InvokeRequired)
                {
                    this.Invoke(new InvokeSetText(this.SetText), new object[] { "편성 TCP전송" });
                }
                else
                {
                    this.TcpSendBtn.Text = "편성 TCP전송";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("TcpSendBtn_Click - " + ex.Message);
            }
        }

        private void SetText(string _str)
        {
            this.TcpSendBtn.Text = _str;
        }

        private void PrintSendMsg(byte[] totalbyte)
        {
            try
            {
                string stmp = ProtoMng.Byte2Hex(totalbyte);
                string tmpstmp = " >> [" + stmp + "]\r\nSend Message Length >> " + totalbyte.Length + "\r\n";

                if (recvform.Visible)
                {
                    recvform.MainTextBox += tmpstmp;
                    recvform.SetTBScroll();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("WmsgSend.PrintSendMsg - " + ex.Message);
            }
        }

        private Boolean wMessageTcpVaildator()
        {
            if (this.checkBox1.Checked == false && this.checkBox2.Checked == false && this.checkBox3.Checked == false)
            {
                MessageBox.Show("발령 구분을 한가지 이상 선택 해 주세요", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            if (this.checkBox1.Checked == true && this.textBox2.Text.Trim().Length < 1)
            {
                MessageBox.Show("DMB 단문을 입력해 주세요", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
                return true;
            }

            if (this.checkBox2.Checked == true && this.textBox3.Text.Trim().Length < 1)
            {
                MessageBox.Show("TTS 단문을 입력해 주세요", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox3.Focus();
                return true;
            }

            if (this.checkBox3.Checked == true && this.textBox4.Text.Trim().Length < 1)
            {
                MessageBox.Show("CBS 단문을 입력해 주세요", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox4.Focus();
                return true;
            }
            if (this.textBox2.Text.Length > WMessage.cLen_message)
            {
                MessageBox.Show("DMB 단문제한을 초과하였습니다.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
                return true;
            }

            if (this.textBox3.Text.Length > WMessage.cLen_ttsMsg)
            {
                MessageBox.Show("TTS 단문제한을 초과하였습니다.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox3.Focus();
                return true;
            }

            if (this.textBox4.Text.Length > WMessage.cLen_smsMsg)
            {
                MessageBox.Show("CBS 단문제한을 초과하였습니다.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox4.Focus();
                return true;
            }
            if (this.checkBox1.Checked == false && this.textBox2.Text.Trim().Length > 0)
            {
                this.textBox2.Text = string.Empty;
            }

            if (this.checkBox2.Checked == false && this.textBox3.Text.Trim().Length > 0)
            {
                this.textBox3.Text = string.Empty;
            }

            if (this.checkBox3.Checked == false && this.textBox4.Text.Trim().Length > 0)
            {
                this.textBox4.Text = string.Empty;
            }
            return false;
        }

        public void initSet()
        {
            this.textBox6.Text = string.Empty;
            this.textBox1.Text = string.Empty;
            this.rdoUnKnwon.Checked = true;
            this.textBox2.Text = string.Empty;
            this.textBox3.Text = string.Empty;
            this.tblCnt.Text = "10";
            this.tblInterval.Text = "1000";
            this.cbRepeat.Checked = false;
        }

        private void cbRepeat_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbRepeat.Checked)
            {
                this.tblCnt.Enabled = true;
                this.tblInterval.Enabled = true;
            }
            else if (!this.cbRepeat.Checked)
            {
                this.tblCnt.Enabled = false;
                this.tblInterval.Enabled = false;
            }
        }
    }
}
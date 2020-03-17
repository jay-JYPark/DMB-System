using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using TcpCommon;
using DmbProtocol;

namespace WMsgTest
{
    public partial class RecvMsgs : Form
    {
        SocketClient soc = SocketClient.getInstance();
        private static RecvMsgs instance = null;
        private static Mutex mutex = new Mutex();

        private delegate void WriteRecieve1MessageHandle(string strMsg);
        private delegate void TcpCliDisConnectMessageHandle(string strMsg);
        private delegate void TcpClientRecievedMsgEventHandle(byte[] rcvMsg);
        private delegate void MaintextboxInsert(string Msg);
        private delegate void MaintextboxScroll();

        public string MainTextBox
        {
            get { return this.MainTB.Text; }
            set { this.MainTB.Text = value; }
        }

        public RecvMsgs()
        {
            soc.tcpCltRcvEvt += new SocketClient.TcpClientRecievedMsgEventHandle(soc_tcpCltRcvEvt);
            soc.tcpConnectedEvt += new SocketClient.TcpClientConnectedEventHandle(soc_tcpConnectedEvt);
            soc.tcpDiconnectedEvt += new SocketClient.TcpClientDisConnectedEventHandle(soc_tcpDiconnectedEvt);

            InitializeComponent();
        }

        public static RecvMsgs getInstance()
        {
            mutex.WaitOne();

            if (instance == null)
            {
                instance = new RecvMsgs();
            }

            mutex.ReleaseMutex();

            return instance;
        }  
        
        protected override void OnClosing(CancelEventArgs e)
        {
            soc.tcpCltRcvEvt -= new SocketClient.TcpClientRecievedMsgEventHandle(soc_tcpCltRcvEvt);
            soc.tcpConnectedEvt -= new SocketClient.TcpClientConnectedEventHandle(soc_tcpConnectedEvt);
            soc.tcpDiconnectedEvt -= new SocketClient.TcpClientDisConnectedEventHandle(soc_tcpDiconnectedEvt);

            base.OnClosing(e);
        }

        void soc_tcpDiconnectedEvt()
        {
            try
            {
                //String tmpMsg = String.Format("[연결이 종료되었습니다.]");

                //if (this.MainTB.InvokeRequired)
                //{
                //    BeginInvoke(new TcpCliDisConnectMessageHandle(tcpClientDisconnectChange), tmpMsg);
                //}
                //else
                //{
                //    this.tcpClientDisconnectChange(tmpMsg);
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine("soc_tcpDiconnectedEvt - " + ex.Message);
            }
        }

        void soc_tcpConnectedEvt()
        {
            try
            {
                //String tmpMsg = String.Format("[서버에 연결되었습니다.]");

                //if (this.MainTB.InvokeRequired)
                //{
                //    BeginInvoke(new WriteRecieve1MessageHandle(writeTextBox), tmpMsg);
                //}
                //else
                //{
                //    this.writeTextBox(tmpMsg);
                //}

                //this.SetTBScroll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("soc_tcpConnectedEvt - " + ex.Message);
            }
        }

        void soc_tcpCltRcvEvt(byte[] rcvMsg)
        {
            try
            {
                //if (this.MainTB.InvokeRequired)
                //{
                //    BeginInvoke(new TcpClientRecievedMsgEventHandle(RecievedMsg), rcvMsg);
                //}
                //else
                //{
                //    this.RecievedMsg(rcvMsg);
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine("soc_tcpCltRcvEvt - " + ex.Message);
            }
        }

        private void tcpClientDisconnectChange(string strMsg)
        {
            try
            {
                this.MainTB.Text += strMsg + "\r\n";
                this.SetTBScroll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("RecvMsgs.tcpClientDisconnectChange - " + ex.Message);
            }
        }

        public void writeTextBox(string strMsg)
        {
            try
            {
                if (this.MainTB.InvokeRequired)
                {
                    this.Invoke(new MaintextboxInsert(this.maintextboxInsert), new object[] { strMsg });
                }
                else
                {
                    this.maintextboxInsert(strMsg);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("RecvMsgs.writeTextBox - " + ex.Message);
            }
        }

        private void maintextboxInsert(string msg)
        {
            this.MainTB.Text += msg + "\r\n";
        }

        private void RecievedMsg(byte[] rmsg)
        {
            try
            {
                string smsg = ProtoMng.Byte2Hex(rmsg);
                this.MainTB.Text += " << [" + smsg + "]\r\nReceive Message Length >> " + rmsg.Length + "\r\n";
                this.SetTBScroll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("RecvMsgs.RecievedMsg - " + ex.Message);
            }
        }

        private void ResetLB_Click(object sender, EventArgs e)
        {
            try
            {
                this.MainTB.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine("RecvMsgs.ResetLB_Click - " + ex.Message);
            }
        }

        public void SetTBScroll()
        {
            try
            {
                if (this.MainTB.InvokeRequired)
                {
                    this.Invoke(new MaintextboxScroll(this.maintextboxscroll), new object[] { });
                }
                else
                {
                    this.maintextboxscroll();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SetTBScroll - " + ex.Message);
            }
        }

        private void maintextboxscroll()
        {
            this.MainTB.SelectionStart = this.MainTB.Text.Length;
            this.MainTB.SelectionLength = 0;
            this.MainTB.ScrollToCaret();
        }
    }
}
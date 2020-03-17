using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TcpCommon
{
    public class SocketClient
    {
        TcpClient tcpClient;
        Thread tcpClientRcvThread;
        Boolean onRecieve = true;
        private bool conState = false;

        private static SocketClient instance = null;
        private static Mutex mutex = new Mutex();

        public event TcpClientConnectedEventHandle tcpConnectedEvt;
        public event TcpClientDisConnectedEventHandle tcpDiconnectedEvt;
        public event TcpClientRecievedMsgEventHandle tcpCltRcvEvt;

        public delegate void TcpClientConnectedEventHandle();
        public delegate void TcpClientDisConnectedEventHandle();
        public delegate void TcpClientRecievedMsgEventHandle(byte[] rcvMsg);

        public SocketClient()
        {
        }

        public bool ConState
        {
            get { return this.conState; }
            set { this.conState = value; }
        }

        public static SocketClient getInstance()
        {
            mutex.WaitOne();

            if (instance == null)
            {
                instance = new SocketClient();
            }

            mutex.ReleaseMutex();

            return instance;
        }

        public Boolean socketClientInit(String ipaddress, String iPort)
        {
            try
            {
                IPEndPoint srvEndip = new IPEndPoint(IPAddress.Parse(ipaddress), int.Parse(iPort));
                tcpClient = new TcpClient();
                tcpClient.Connect(srvEndip);

                this.conState = true;
                return true;
            }
            catch (Exception e) 
            {
                Console.WriteLine("socketClientInit - " + e.Message);

                return false;
            }
        }

        public Boolean tcpClientStop()
        {
            try
            {
                if (tcpClientRcvThread != null)
                {
                    tcpClientRcvThread.Abort();
                }
                tcpClient.Close();

                this.conState = false;
                return true;
            }
            catch (Exception e) 
            {
                Console.WriteLine("tcpClientStop - " + e.Message);

                return false;
            }
        }

        public void recieveThread()
        {
            tcpClientRcvThread = new Thread(new ThreadStart(tcpClientThread));
            tcpClientRcvThread.IsBackground = true;
            tcpClientRcvThread.Start();
        }

        private void tcpClientThread()
        {
            try
            {
                if (tcpConnectedEvt != null)
                {
                    tcpConnectedEvt();
                }

                onRecieve = true;
                NetworkStream rcvStream = tcpClient.GetStream();

                while (onRecieve)
                {
                    byte[] rcvMsg = new byte[1024];
                    int tSize = rcvStream.Read(rcvMsg, 0, rcvMsg.Length);
                    
                    if (tSize > 0)
                    {
                        if (tcpCltRcvEvt != null)
                        {
                            Array.Resize<byte>(ref rcvMsg, tSize);
                            tcpCltRcvEvt(rcvMsg);
                        }
                    }
                    else
                    {
                        onRecieve = false;
                    }
                }

                tcpClientStop();
            }
            catch (Exception e) 
            {
                Console.WriteLine("tcpClientThread - " + e.Message);

                onRecieve = false;
                tcpClientStop();
                
                if (tcpDiconnectedEvt != null)
                {
                    tcpDiconnectedEvt();
                }
            }
        }

        public void tcpClientSndMsg(byte[] sndMsg)
        {
            try
            {
                NetworkStream sndStream = tcpClient.GetStream();
                sndStream.Write(sndMsg, 0, sndMsg.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
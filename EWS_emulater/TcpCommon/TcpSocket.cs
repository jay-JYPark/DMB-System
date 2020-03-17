using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace EWS_emulater
{
    public class TcpSocket
    {
        TcpListener tcpLisner;
        TcpClient acceptClient;
        NetworkStream rcvStream;
        Thread threadStartListner;
        Boolean listnerFlag = false;
        Boolean acceptFalg = false;

        public event TcpSrvMsgEventHandler tcpMsgEvt;
        public event TcpSrvRcvMsgEventHandler tcpRcvMsgEvt;
        public event TcpConnectedEventHandler tcpConnected;
        public event TcpDisconnectedEventHandler tcpDisconnected;
        
        public delegate void TcpSrvMsgEventHandler(object sender, String rcvMsg, Boolean sMsgFlag);
        public delegate void TcpSrvRcvMsgEventHandler(object sender, byte[] rcvMsg);
        public delegate void TcpConnectedEventHandler(object sender);
        public delegate void TcpDisconnectedEventHandler(object sender);

        public TcpSocket()
        {
        }

        public Boolean socketServerInit(int iPort)
        {
            try
            {
                tcpLisner = new TcpListener(new IPEndPoint(IPAddress.Any, iPort));
                tcpLisner.Start();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("TcpSocket.socketServerInit - " + ex.Message);
                return false;
            }
        }

        public Boolean tcsSrvLsnStop()
        {
            try
            {
                rcvStream.Close();
                tcpLisner.Stop();
                threadStartListner.Abort();
                tcpDisconnected(this);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("TcpSocket.tcsSrvLsnStop - " + ex.Message);
                return false;
            }
        }

        public void startLisner()
        {
            try
            {
                threadStartListner = new Thread(new ThreadStart(tcpListnning));
                threadStartListner.IsBackground = true;
                threadStartListner.Start();
            }
            catch(Exception ex)
            {
                Console.WriteLine("TcpSocket.startLisner - " + ex.Message);
                return;
            }
        }

        private void tcpListnning()
        {
            try
            {
                tcpConnected(this);
                listnerFlag = true;
                
                while (listnerFlag)
                {
                    acceptClient = tcpLisner.AcceptTcpClient();
                    String acceptMsg = ((IPEndPoint)acceptClient.Client.RemoteEndPoint).Address.ToString();
                    
                    if (tcpMsgEvt != null)
                    {
                        tcpMsgEvt(this, acceptMsg, true);
                    }

                    acceptFalg = true;
                    
                    while (acceptFalg)
                    {
                        try
                        {
                            rcvStream = acceptClient.GetStream();
                            byte[] rcvMsg = new byte[1024];
                            int tSize = rcvStream.Read(rcvMsg, 0, rcvMsg.Length);
                            
                            if (tSize > 0)
                            {
                                if (tcpRcvMsgEvt != null)
                                {
                                    Array.Resize<byte>(ref rcvMsg, tSize);
                                    tcpRcvMsgEvt(this, rcvMsg);
                                }
                            }
                            else
                            {
                                acceptFalg = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("TcpSocket.tcpListnning - " + ex.Message);
                            rcvStream.Close();
                            acceptFalg = false;
                        }
                    }

                    if (tcpMsgEvt != null)
                    {
                        tcpMsgEvt(this, acceptMsg, false);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("TcpSocket.tcpListnning - " + ex.Message);
                listnerFlag = false;
                tcsSrvLsnStop();
            }
        }

        public void tcpSendMsg(byte[] sndMsg)
        {
            try
            {
                if (acceptClient.Connected)
                {
                    NetworkStream sndStream = acceptClient.GetStream();
                    sndStream.Write(sndMsg, 0, sndMsg.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("TcpSocket.tcpSendMsg - " + ex.Message);
            }
        }
    }
}
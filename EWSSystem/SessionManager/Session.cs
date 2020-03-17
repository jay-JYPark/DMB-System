using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.IO;
using System.Diagnostics;
using TextLog;

namespace TcpServer
{
    public class Session
    {
        #region 멤버 변수
        public string id { get; set; }//구분을 위한 아이디(PKID)
        public string ip { get; set; }
        public int port { get; set; }
        public string connTime { get; set; }
        public bool connStatus { get; set; }
        public bool isLogin { get; set; }
        public string connLocation { get; set; }

        public TcpClient tcpClient { get; set; }
        public NetworkStream netStream { get; set; }
        public byte[] rBuffer { get; set; }
        public byte[] sBuffer { get; set; }
        public SessionManager sessionManager { get; set; }

        private Thread receiveTD = null;
        private Thread conCheckTD = null;
        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        public Session()
        {

        }


        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="port">포트</param>
        /// <param name="connLocation">접속 장소</param>
        public Session(string id, string sessionIp, int port, string connLocation, TcpClient tcpClient, SessionManager sessionManager)
        {
            string strNow = DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss");

            this.id = id;
            this.ip = sessionIp;
            this.port = port;
            this.connTime = strNow; //DateTime.Now.ToString();
            this.connStatus = true;
            this.isLogin = true;//////
            this.connLocation = connLocation;
            //this.lastCheckTIme = DateTime.Now.ToString();
            //this.checkStatusNum = 0;
            this.tcpClient = tcpClient;
            this.netStream = this.tcpClient.GetStream();
            this.sessionManager = sessionManager;

            //Thread 시작
            // 생성된 스트림에서 정보받아오기 대기하기 스레드 스타트
            try
            {
                Thread receiveTD = new Thread(new ThreadStart(ReceiveStart));
                receiveTD.Start();

                Thread conCheckTD = new Thread(new ThreadStart(conCheckStart));
                conCheckTD.Start();
            }
            catch (ThreadAbortException threadEx)
            {
                Debug.WriteLine("TCPServer.Session.Session (78 line)| " + threadEx.Message);
                Log.WriteLog("TCPServer.Session.Session (79 line)| " + threadEx.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("TCPServer.Session.Session (83 line)| " + ex.Message);
                Log.WriteLog("TCPServer.Session.Session (84 line)| " + ex.Message);
            }
        }


        /// <summary>
        ///  리시브 스레드
        /// </summary>
        private void ReceiveStart()
        {
            try
            {
                while (true)
                {
                    rBuffer = new byte[1024];

                    int revLen = netStream.Read(rBuffer, 0, rBuffer.Length);

                    if (revLen > 0)
                    {
                        this.sessionManager.ReceiveData(this.id, this.ip, rBuffer);
                    }
                    else if (revLen == 0)
                    {
                        this.Disconnect();
                        //세션 메니저에  DeleteSession 알림
                        this.sessionManager.DeleteSession(this.id, this.ip);
                    }
                }
            }
            catch (SocketException sEx)
            {
                this.sessionManager.DeleteSession(this.id, this.ip);
                this.Disconnect();

                Debug.WriteLine("TCPServer.Session.ReceiveStart (116 line)| " + sEx.Message);
                Log.WriteLog("TCPServer.Session.ReceiveStart (117 line)| " + sEx.Message);
            }
            catch (Exception ex)
            {
                this.sessionManager.DeleteSession(this.id, this.ip);
                this.Disconnect();

                Debug.WriteLine("TCPServer.Session.ReceiveStart (124 line)| " + ex.Message);
                Log.WriteLog("TCPServer.Session.ReceiveStart (125 line)| " + ex.Message);
            }
        }


        /// <summary>
        /// 커넥션 체크 스레드 (클라이언트와의 연결을 체크하기위함)
        /// </summary>
        private void conCheckStart()
        {
            while (true)
            {
                try
                {
                    netStream.Write(new byte[5], 0, 5);
                }
                catch (SocketException sEx)
                {
                    this.sessionManager.DeleteSession(this.id, this.ip);
                    this.Disconnect();

                    Debug.WriteLine("TCPServer.Session.conCheckStart() | " + sEx.Message);
                    Log.WriteLog("TCPServer.Session.conCheckStart() | " + sEx.Message);
                }
                catch (Exception ex)
                {
                    this.sessionManager.DeleteSession(this.id, this.ip);
                    this.Disconnect();

                    Debug.WriteLine("TCPServer.Session.conCheckStart() | " + ex.Message);
                    Log.WriteLog("TCPServer.Session.conCheckStart() | " + ex.Message);
                }

                Thread.Sleep(3000); //3초 간격으로 테스트
            }
        }

        /// <summary>
        /// 클라이언트에 data를 보낸다.
        /// </summary>
        /// <param name="sBuffer">보낼 데이터</param>
        public void SendData(byte[] sBuffer)
        {
            try
            {
                netStream.Write(sBuffer, 0, sBuffer.Length);
            }
            catch (SocketException sEx)
            {
                this.sessionManager.DeleteSession(this.id, this.ip);
                this.Disconnect();

                Debug.WriteLine("TCPServer.Session.SendData() | " + sEx.Message);
                Log.WriteLog("TCPServer.Session.SendData() | " + sEx.Message);
            }
            catch (Exception ex)
            {
                this.sessionManager.DeleteSession(this.id, this.ip);
                this.Disconnect();


                Debug.WriteLine("TCPServer.Session.SendData() | " + ex.Message);
                Log.WriteLog("TCPServer.Session.SendData() | " + ex.Message);
            }
        }


        /// <summary>
        /// 접속 종료
        /// </summary>
        public void Disconnect()
        {
            try
            {
                if (netStream != null)
                {
                    this.netStream.Close();
                    this.tcpClient.Client.Close();
                }
                if (receiveTD != null)
                {
                    receiveTD.Abort();
                    conCheckTD.Abort();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("TCPServer.Session.Disconnect() | " + ex.Message);
                Log.WriteLog("TCPServer.Session.Disconnect() | " + ex.Message);
            }
        }

        #region Log 관련
        //public void Log(String strMsg)
        //{
        //    string m_strLogPrefix = @"C:\LOG\PROJECT\LOG";
        //    string m_strLogExt = @".LOG";
        //    DateTime dtNow = DateTime.Now;
        //    string strDate = dtNow.ToString("yyyyMMdd");
        //    string strPath = String.Format("{0}{1}{2}", m_strLogPrefix, strDate, m_strLogExt);
        //    string strDir = Path.GetDirectoryName(strPath);
        //    DirectoryInfo diDir = new DirectoryInfo(strDir);

        //    if (!diDir.Exists)
        //    {
        //        diDir.Create();
        //        diDir = new DirectoryInfo(strDir);  // 아래에 있는 if (diDir.Exists)은 Directory 생성전 상태를 나타내므로 다시 DirectoryInfo object를 생성.
        //    }

        //    if (diDir.Exists)
        //    {
        //        System.IO.StreamWriter swStream = File.AppendText(strPath);
        //        string strLog = String.Format("{0}: {1}", dtNow.ToString("hhmmss"), strMsg);
        //        swStream.WriteLine(strLog);
        //        swStream.Close(); ;
        //    }
        //}
        #endregion
    }
}

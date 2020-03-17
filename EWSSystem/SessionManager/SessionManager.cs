using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Configuration;
using TextLog;
using System.Diagnostics;

namespace TcpServer
{
    public class SessionManager
    {
        #region 멤버변수
        public short uniqueSessionId { get; set; } //session 접속 시, 고유 ID : 0~999까지
        public string ip { get; set; }
        public int port { get; set; }
        public TcpListener tcpListener { get; set; }
        public Dictionary<string, Session> sessionList { get; set; }
        #endregion

        #region 쓰레드
        Thread listeningTD { get; set; }
        #endregion

        #region 이벤트

        //서버가 정상적으로 시작되었을 때
        public delegate void TcpServerStartHandler();
        public event TcpServerStartHandler evtTcpServerStart;

        //서버가 종료되었을 때
        public delegate void TcpServerStopHandler();
        public event TcpServerStopHandler evtTcpServerStop;

        //클라이언트가 접속되었을 때
        public delegate void TcpConnectClientHandler(Session session);
        public event TcpConnectClientHandler evtConnectClient;

        //클라이언트의 접속이 끊겼을 때
        public delegate void TcpDisconnectClientHandler(string id, string ip);
        public event TcpDisconnectClientHandler evtDisconnectClient;

        //클라이언트에서 데이터가 왔을 때
        public delegate void TcpReceiveDataHandler(string id, string ip, byte[] rBuffer);
        public event TcpReceiveDataHandler evtReceiveData;
        #endregion


        /// <summary>
        /// 기본 생성자
        /// </summary>
        public SessionManager()
        {
            this.uniqueSessionId = 0;
        }


        /// <summary>
        /// 서버 시작 : 서버 정보 세팅, 리스팅 스타트 쓰레드 동작 
        /// </summary>
        /// <returns></returns>
        public void Start(string ip, int port)
        {
            //1. 서버 세팅
            SetServerInfo(ip, port);

            //2. 리스닝하며 클라이언트 받아들이는 스레드  Start
            try
            {
                listeningTD = new Thread(new ThreadStart(ListeningStart));
                if (listeningTD != null)
                    listeningTD.Start();

                //3. 서버 시작 Event 발생
                this.evtTcpServerStart();
            }
            catch (ThreadStartException threadEx)
            {
                Debug.WriteLine("SessionManager.Start (ThreadStart)- " + threadEx.Message);
                Log.WriteLog("TCPServer.SessionManager.Start.ThreadExeption(83 line)| " + threadEx.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("TCPServer.SessionManager.Start - " + ex.Message);
                Log.WriteLog("TCPServer.SessionManager.Start.Exeption(88 line)| " + ex.Message);
            }
        }


        /// <summary>
        ///  서버 정보 세팅
        /// </summary>
        public void SetServerInfo(string ip, int port)
        {
            this.ip = ip;
            this.port = port;

            IPAddress ipAddress = IPAddress.Any;
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, this.port);
            tcpListener = new TcpListener(ipEndPoint);

            //클라이언트 리스트 생성
            sessionList = new Dictionary<string, Session>();
        }


        /// <summary>
        /// 리스닝하며 어셉트 스레드
        /// </summary>
        public void ListeningStart()
        {
            try
            {
                //리스너 스타트
                tcpListener.Start();

                while (true)
                {
                    if (tcpListener.Pending())
                    {
                        //리스너는 클라이언트를 받아들인다.
                        TcpClient tcpClient = tcpListener.AcceptTcpClient();

                        if (tcpClient == null)
                            continue;

                        //정상적으로 받아들여졌다면 SessionList에 추가한다.
                        AddToSessionList(tcpClient);
                    }
                    else Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                this.Stop();

                Debug.WriteLine("TCPServer.SessionManager.ListeningStart(140 line) " + ex.Message);
                Log.WriteLog("TCPServer.SessionManager.ListeningStart(141 line)| " + ex.Message);

                this.evtTcpServerStop();// 이벤트 발생  -> 폼에 알리기 
            }
        }


        /// <summary>
        /// 클라이언트 접속
        /// </summary>
        /// <param name="tcpClient"></param>
        public void AddToSessionList(TcpClient tcpClient)
        {
            try
            {
                // 클라이언트 리스트에 추가
                string sessionIp = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString();
                //int sessionId = this.sessionList.Count;
                string sessionId = GetSessionId();
                Session session = new Session(sessionId
                                                               , sessionIp
                                                               , Convert.ToInt32(ConfigurationManager.AppSettings["TCPPort"])
                                                               , "소방방재청"
                                                               , tcpClient, this);
                this.sessionList.Add(sessionId, session);

                this.evtConnectClient(session); //클라이언트 연결 -> 폼에 출력
            }
            catch (Exception ex)
            {
                Debug.WriteLine("TCPServer.SessionManager.AddToSessionList ( 171 line) " + ex.Message);
                Log.WriteLog("TCPServer.SessionManager.AddToSessionList (172 line)| " + ex.Message);
            }
        }

        /// <summary>
        /// 세션 고유 아이디 만들기
        /// </summary>
        /// <returns></returns>
        public string GetSessionId()
        {
            string sessionId = "";

            sessionId = DateTime.Now.ToString("yyyyMMddHHmmss");

            if (uniqueSessionId == 1000)
            {
                this.uniqueSessionId = 0;
                sessionId += uniqueSessionId.ToString();
            }
            else
            {
                sessionId += uniqueSessionId.ToString();
            }

            uniqueSessionId++;

            return sessionId;
        }


        /// <summary>
        /// 클라이언트 데이터 받기
        /// </summary>
        /// <param name="session">클라이언트</param>
        public void ReceiveData(string id, string ip, byte[] rBuffer)
        {
            try
            {
                this.evtReceiveData(id, ip, rBuffer); //클라이언트 데이터 받기 -> 폼에 출력
            }
            catch (Exception ex)
            {
                Debug.WriteLine("TCPServer.SessionManager.ReceiveData (214 line)| " + ex.Message);
                Log.WriteLog("TCPServer.SessionManager.ReceiveData (215 line)| " + ex.Message);
            }
        }



        /// <summary>
        /// 클라이언트 연결 종료
        /// </summary>
        /// <param name="session">클라이언트</param>
        public void DeleteSession(string id, string ip)
        {
            this.sessionList.Remove(id);
            this.evtDisconnectClient(id, ip); //클라이언트 연결 끊김 -> 폼에 출력
        }



        /// <summary>
        /// 서버 종료 : 스트림과 클라이언트 종점을 닫는다.
        /// </summary>
        /// <returns></returns>
        public void Stop()
        {
            try
            {
                //모든 소켓 Disconnect
                List<Session> sList = new List<Session>(sessionList.Values);
                foreach (Session s in sList)
                {
                    s.Disconnect();
                }

                //서버 리스너 종료
                this.tcpListener.Stop();

                //서버 강제 종료
                if (listeningTD != null)
                {
                    this.listeningTD.Abort();
                }
            }
            catch (ThreadAbortException threadEx)
            {
                Debug.WriteLine("TCPServer.SessionManager.Stop (259 line)| " + threadEx.Message);
                Log.WriteLog("TCPServer.SessionManager.Stop (260 line)| " + threadEx.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("TCPServer.SessionManager.Stop (264 line)| " + ex.Message);
                Log.WriteLog("TCPServer.SessionManager.Stop (265 line)| " + ex.Message);
            }
        }


    }
}

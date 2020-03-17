using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TcpServer;
using System.Net;
using DmbProtocol;
using MUX;
using System.Configuration;
using System.Threading;
using System.IO;
using System.Diagnostics;
using ADEng.Library;
using TextLog;


namespace EWSSystem
{
    public partial class MainViewForm : Form
    {
        #region 멤버 변수
        public SessionManager sessionManager { get; set; }
        public MUXManager muxManager { get; set; }
        public MUXScheduler spcMUXScheduler { get; set; }
        public MUXScheduler norMUXScheduler { get; set; }
        //public MUXMsg muxMsg { get; set; }
        public OptionDialog optForm { get; set; }
        #endregion

        #region  대리자
        //TCP 서버 및 클라이언트 이벤트 대리자
        private delegate void InvokeTcpServerManagerStart();
        private delegate void InvokeTcpServerManagerStop();
        private delegate void InvokeTcpSessionConnect(Session session);
        private delegate void InvokeTcpSessionDisconnect(string id, string ip);
        private delegate void InvokeTcpSessionReceive(string id, string ip, byte[] rBuffer);

        //메세지 관련 
        //특수 
        //private delegate void InvokeStartSendToMUX(string genId, string startDateTime);
        private delegate void InvokeNowSendToMUX(string sessionId, string genId, string nowDateTime);
        private delegate void InvokeFinishSendToMUX(string sessionId, string genId, string nowDateTime);
        private delegate void InvokeErrorSendToMUX(string sessionId, string genId, string nowDateTime);

        //일반
        private delegate void InvokeNowNorSendToMUX(string sessionId, string genId, string nowDateTime);
        private delegate void InvokeFinishNorSendToMUX(string sessionId, string genId, string nowDateTime);
        private delegate void InvokeErrorNorSendToMUX(string sessionId, string genId, string nowDateTime);

        //MUX 상태 변경
        private delegate void InvokeChangeMUXStatus(string muxIp, bool isNormal);
        //MUX 상태 변경
        //static public  delegate void InvokeUpdateMUXInfo();

        //오라클 접속
        private ADEng.Library.oracleDAC odec = null;
        #endregion


        /// <summary>
        /// 기본 생성자
        /// </summary>
        public MainViewForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 메인 View 폼이 로드될 때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainViewForm_Load(object sender, EventArgs e)
        {
            try
            {
                //0. 오라클 접속 변수 얻어오기
                odec = DbConn.GetDbConn();

                // 멤버 객체 할당
                sessionManager = new SessionManager();
                muxManager = new MUXManager();
                spcMUXScheduler = new MUXScheduler();
                norMUXScheduler = new MUXScheduler();


                // 서버 자신의 아이피 설정
                IPAddress[] addr = Dns.GetHostAddresses(Dns.GetHostName());
                sessionManager.ip = addr[0].ToString();


                #region 3. 서버 이벤트 등록 및 시작
                // 이벤트 등록
                //TCP 관련
                sessionManager.evtTcpServerStart += new SessionManager.TcpServerStartHandler(TcpServerStart);
                sessionManager.evtTcpServerStop += new SessionManager.TcpServerStopHandler(TcpServerStop);
                sessionManager.evtConnectClient += new SessionManager.TcpConnectClientHandler(TcpSessionConnect);
                sessionManager.evtDisconnectClient += new SessionManager.TcpDisconnectClientHandler(TcpSessionDisconnect);
                sessionManager.evtReceiveData += new SessionManager.TcpReceiveDataHandler(TcpSessionData);

                //MUX 메세지 관련
                //특수
                //spcMUXScheduler.evtStartSendToMUX += new MUXScheduler.StartSendToMUX(StartSendToMUX);
                spcMUXScheduler.evtFinishSendToMUX += new MUXScheduler.FinishSendToMUX(FinishSendToMUX);
                spcMUXScheduler.evtNowSendToMUX += new MUXScheduler.NowSendToMUX(NowSendToMUX);
                spcMUXScheduler.evtErrorSendToMUX += new MUXScheduler.ErrorSendToMUX(ErrorSendToMUX);

                //일반
                norMUXScheduler.evtFinishNorSendToMUX += new MUXScheduler.FinishNorSendToMUX(FinishNorSendToMUX);
                norMUXScheduler.evtNowNorSendToMUX += new MUXScheduler.NowNorSendToMUX(NowNorSendToMUX);
                //norMUXScheduler.evtNorErrorSendToMUX += new MUXScheduler.NorErrorSendToMUX(NorErrorSendToMUX);

                //MUX 상태
                muxManager.evtChangeMUXStatus += new MUXManager.ChangeMUXStatus(ChangeMUXStatus);


                //서버 포트 세팅 및 서버 가동 시작
                string strPort = ConfigurationManager.AppSettings["TCPPort"];
                sessionManager.Start(sessionManager.ip, Convert.ToInt32(strPort));


                // MUX 인포를 DB에서 읽어 와 세팅한다.
                StringBuilder sBuilder = new StringBuilder(100);
                sBuilder.Append(" SELECT  useflag , muxid, muxdesc, status, ip ");
                sBuilder.Append(" FROM muxInfo ");

                DataTable dTable = new DataTable();

                if (odec.openDb())
                {
                    dTable = odec.getDataTable(sBuilder.ToString(), "muxInfo");
                }


                if (dTable.Rows.Count > 0)
                {
                    int i = 0;
                    string strUse = "사용";
                    string strStatus = "정상";
                    foreach (DataRow dRow in dTable.Rows)
                    {
                        if (dRow[0].ToString().Equals(0))
                        {
                            strUse = "미사용";
                        }
                        if (dRow[3].ToString().Equals(0))
                        {
                            strStatus = "이상";
                        }


                        ListViewItem lvItem = new ListViewItem(new string[] {  strUse
                                                                                                       ,dRow[1].ToString()
                                                                                                       , dRow[2].ToString()
                                                                                                       , strStatus
                                                                                                       , dRow[4].ToString()
                                                                                    });
                        this.lvMUXInfos.Items.Insert(i, lvItem);
                        i++;
                    }
                }



                #endregion
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EWSSystem.MainViewForm.MainViewForm_Load()| " + ex.Message);
                Log.WriteLog("EWSSystem.MainViewForm.MainViewForm_Load()| " + ex.Message);
            }
        }


        #region MUX 상태 변경되었을 때
        void ChangeMUXStatus(string muxIp, bool isNormal)
        {
            try
            {
                this.Invoke(new InvokeChangeMUXStatus(this.ChangeMUXStatusStart), new object[] { muxIp, isNormal });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EWSSystem.MainViewForm.ChangeMUXStatus()| " + ex.Message);
                Log.WriteLog("EWSSystem.MainViewForm.ChangeMUXStatus()| " + ex.Message);
            }
        }

        public void ChangeMUXStatusStart(string muxIp, bool isNormal)
        {
            string strMuxStatus = "정상";

            if (isNormal == false)
            {
                strMuxStatus = "비정상";
            }

            for (int i = 0; i < this.lvMUXInfos.Items.Count; i++)
            {
                if (this.lvMUXInfos.Items[i].SubItems[4].Equals(muxIp))
                {
                    this.lvMUXInfos.Items[i].SubItems[3].Text = strMuxStatus;
                }
            }
        }
        #endregion


        #region 일반 메세지 전송 중 -> 폼 출력
        void NowNorSendToMUX(string sessionId, string genId, string startDateTime)
        {
            try
            {
                this.Invoke(new InvokeNowNorSendToMUX(this.NowNorSendToMUXStart), new object[] { sessionId, genId, startDateTime });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EWSSystem.MainViewForm.NowNorSendToMUX()| " + ex.Message);
                Log.WriteLog("EWSSystem.MainViewForm.NowNorSendToMUX()| " + ex.Message);
            }
        }


        public void NowNorSendToMUXStart(string sessionId, string genId, string nowDateTime)
        {
            for (int i = 0; i < lvNorMsg.Items.Count; i++)
            {
                //현재 프로세싱 중인 것 "전송중" 출력
                if (lvNorMsg.Items[i].SubItems[0].Text.Equals(genId))
                {
                    //최초 전송이라면
                    if (this.lvNorMsg.Items[i].SubItems[4].Text.Equals(""))
                    {
                        try
                        {
                            //DB 상태값 업데이트: '전송중'
                            if (odec.openDb())
                            {
                                StringBuilder sBuilder = new StringBuilder();
                                sBuilder.Append(" UPDATE message SET tstarttime = (:1) , status = (:2)  WHERE msgid = (:3) AND msgtype=(:4) ");
                                List<oracle_parameter> inner_parameters = new List<oracle_parameter>();

                                inner_parameters.Add(
                                   new oracle_parameter("tstarttime", oracle_parameter.OracleDataType.Varchar2, DateTime.Now.ToString("yyyyMMddHHmmss")
                                                                   , ParameterDirection.Input));
                                inner_parameters.Add(
                                    new oracle_parameter("status", oracle_parameter.OracleDataType.Int32, 1, ParameterDirection.Input)); //전송중
                                inner_parameters.Add(
                                    new oracle_parameter("msgid", oracle_parameter.OracleDataType.Int32, Convert.ToInt32(genId), ParameterDirection.Input));
                                inner_parameters.Add(
                                    new oracle_parameter("msgtype", oracle_parameter.OracleDataType.Int32, 0, ParameterDirection.Input));

                                int result = odec.WorkSql(sBuilder.ToString(), inner_parameters, "message");
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("EWSSystem.MainViewForm.NowNorSendToMUXStart(DB-Update) " + ex.Message);
                            Log.WriteLog("EWSSystem.MainViewForm.NowNorSendToMUXStart(DB-Update)| " + ex.Message);
                        }

                        //전송시작 시간 출력
                        this.lvNorMsg.Items[i].SubItems[4].Text = nowDateTime;
                        //상태, 색깔, 최근전송 시간 출력
                        this.lvNorMsg.Items[i].SubItems[3].Text = "전송중";
                        this.lvNorMsg.Items[i].BackColor = Color.GreenYellow;
                        this.lvNorMsg.Items[i].SubItems[5].Text = nowDateTime;

                        //송출 서버에 전송중 data Send
                        //(프로토콜 만들기)
                        try
                        {
                            Protocol11 p11 = new Protocol11();
                            TimeSpan timespan = DateTime.Now.Subtract(new DateTime(1970, 1, 1, 9, 0, 0));
                            p11.transferTime = (int)timespan.TotalSeconds;
                            p11.generationId = Convert.ToInt32(genId);
                            byte[] resultBytes = new byte[1];
                            resultBytes[0] = Convert.ToByte('1'); //성공
                            p11.bodyData = resultBytes;

                            //(송출 서버에 보내기)
                            byte[] tempBuff = p11.MakeData();
                            if (tempBuff[0] != 0)
                            {
                                sessionManager.sessionList[sessionId].SendData(tempBuff);
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("EWSSystem.MainViewForm.NowNorSendToMUXStart (Protocol) " + ex.Message);
                            Log.WriteLog("EWSSystem.MainViewForm.NowNorSendToMUXStart (Protocol)| " + ex.Message);
                        }
                    }
                    else //최초 전송이 아니면
                    {
                        this.lvNorMsg.Items[i].SubItems[3].Text = "전송중";
                        this.lvNorMsg.Items[i].BackColor = Color.GreenYellow;
                        this.lvNorMsg.Items[i].SubItems[5].Text = nowDateTime;
                    }
                }
                //나머지 "대기" 출력
                else if (lvNorMsg.Items[i].SubItems[0].Text != genId)
                {
                    this.lvNorMsg.Items[i].SubItems[3].Text = "대기";
                    this.lvNorMsg.Items[i].BackColor = Color.White;
                }
            }
        }


        #endregion


        #region 일반 메세지 종료시
        void FinishNorSendToMUX(string sessionId, string genId, string endDateTime)
        {
            try
            {
                this.Invoke(new InvokeFinishNorSendToMUX(this.FinishNorSendToMUXStart), new object[] { sessionId, genId, endDateTime });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EWSSystem.MainViewForm.FinishNorSendToMUX () " + ex.Message);
                Log.WriteLog("EWSSystem.MainViewForm.FinishNorSendToMUX ()| " + ex.Message);
            }
        }

        public void FinishNorSendToMUXStart(string sessionId, string genId, string nowDateTime)
        {
            for (int i = 0; i < lvNorMsg.Items.Count; i++)
            {
                if (lvNorMsg.Items[i].SubItems[0].Text.Equals(genId))
                {
                    //리스트에서 삭제
                    this.lvNorMsg.Items[i].Remove();

                    try
                    {
                        //DB 상태값 업데이트: '종료'
                        if (odec.openDb())
                        {
                            StringBuilder sBuilder = new StringBuilder();
                            sBuilder.Append(" UPDATE message SET tendtime = (:1) , status = (:2)  WHERE msgid = (:3) AND msgType = (:4) ");
                            List<oracle_parameter> inner_parameters = new List<oracle_parameter>();

                            inner_parameters.Add(
                               new oracle_parameter("tendtime", oracle_parameter.OracleDataType.Varchar2, DateTime.Now.ToString("yyyyMMddHHmmss")
                                                               , ParameterDirection.Input));
                            inner_parameters.Add(
                                new oracle_parameter("status", oracle_parameter.OracleDataType.Int32, 2, ParameterDirection.Input)); //종료
                            inner_parameters.Add(
                                new oracle_parameter("msgid", oracle_parameter.OracleDataType.Int32, Convert.ToInt32(genId), ParameterDirection.Input));
                            inner_parameters.Add(
                               new oracle_parameter("msgtype", oracle_parameter.OracleDataType.Int32, 0, ParameterDirection.Input));

                            int result = odec.WorkSql(sBuilder.ToString(), inner_parameters, "message");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("EWSSystem.MainViewForm.FinishNorSendToMUXStart (DB-Update) " + ex.Message);
                        Log.WriteLog("EWSSystem.MainViewForm.FinishNorSendToMUXStart (DB-Update)| " + ex.Message);
                    }

                    //송출 서버에 종료 data Send
                    //(프로토콜 만들기)
                    try
                    {
                        Protocol12 p12 = new Protocol12();
                        TimeSpan timespan = DateTime.Now.Subtract(new DateTime(1970, 1, 1, 9, 0, 0));
                        p12.transferTime = (int)timespan.TotalSeconds;
                        p12.generationId = Convert.ToInt32(genId);
                        byte[] resultBytes = new byte[1];
                        resultBytes[0] = Convert.ToByte('3'); //종료
                        p12.bodyData = resultBytes;

                        //(송출 서버에 보내기)
                        byte[] tempBuff = p12.MakeData();
                        if (tempBuff[0] != 0)
                        {
                            sessionManager.sessionList[sessionId].SendData(tempBuff);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("EWSSystem.MainViewForm.FinishNorSendToMUXStart (Protocol) " + ex.Message);
                        Log.WriteLog("EWSSystem.MainViewForm.FinishNorSendToMUXStart (Protocol)| " + ex.Message);
                    }
                }
            }
        }
        #endregion


        #region 일반 메세지 에러시
        void ErrorNorSendToMUX(string sessionId, string genId, string endDateTime)
        {
            try
            {
                this.Invoke(new InvokeErrorNorSendToMUX(this.ErrorNorSendToMUXStart), new object[] { sessionId, genId, endDateTime });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EWSSystem.MainViewForm.ErrorNorSendToMUX()| " + ex.Message);
                Log.WriteLog("EWSSystem.MainViewForm.ErrorNorSendToMUX()| " + ex.Message);
            }
        }

        public void ErrorNorSendToMUXStart(string sessionId, string genId, string endDateTime)
        {
            for (int i = 0; i < lvNorMsg.Items.Count; i++)
            {
                if (lvNorMsg.Items[i].SubItems[0].Text.Equals(genId))
                {
                    //리스트에서 삭제
                    this.lvNorMsg.Items[i].Remove();

                    //송출 서버에 종료 data Send
                    //(프로토콜 만들기)
                    try
                    {
                        Protocol12 p12 = new Protocol12();
                        TimeSpan timespan = DateTime.Now.Subtract(new DateTime(1970, 1, 1, 9, 0, 0));
                        p12.transferTime = (int)timespan.TotalSeconds;
                        p12.generationId = Convert.ToInt32(genId);

                        byte[] resultBytes = new byte[1];
                        resultBytes[0] = Convert.ToByte('0'); //실패

                        p12.bodyData = resultBytes;

                        //(송출 서버에 보내기)
                        byte[] tempBuff = p12.MakeData();
                        if (tempBuff[0] != 0)
                        {
                            sessionManager.sessionList[sessionId].SendData(tempBuff);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("EWSSystem.MainViewForm.ErrorNorSendToMUXStart()| " + ex.Message);
                        Log.WriteLog("EWSSystem.MainViewForm.ErrorNorSendToMUXStart()| " + ex.Message);
                    }
                }
            }
        }
        #endregion


        #region  특수 메세지 전송 시작 -> 폼 출력 (현재 사용 하지 않음)
        //public void StartSendToMUX(string genId, string startDateTime)
        //{
        //    try
        //    {
        //        this.Invoke(new InvokeStartSendToMUX(this.StartSendToMUXStart), new object[] { genId, startDateTime });
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("MainViewForm.TcpServerStart - " + ex.Message);
        //    }
        //}

        //public void StartSendToMUXStart(string genId, string startDateTime)
        //{
        //    for (int i = 0; i < lvSpcMsg.Items.Count; i++)
        //    {
        //        if (lvSpcMsg.Items[i].SubItems[0].Text.Equals(genId))
        //        {
        //            this.lvSpcMsg.Items[i].SubItems[3].Text = "전송 준비";
        //            //this.lvSpcMsg.Items[i].BackColor = Color.GreenYellow;
        //            this.lvSpcMsg.Items[i].SubItems[4].Text = startDateTime;
        //            this.lvSpcMsg.Items[i].SubItems[5].Text = startDateTime;
        //        }
        //    }
        //}
        #endregion


        #region 특수 메세지 전송 중 -> 폼 출력
        public void NowSendToMUX(string sessionId, string genId, string nowDateTime)
        {
            try
            {
                this.Invoke(new InvokeNowSendToMUX(this.NowSendToMUXStart), new object[] { sessionId, genId, nowDateTime });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EWSSystem.MainViewForm.NowSendToMUX()| " + ex.Message);
                Log.WriteLog("EWSSystem.MainViewForm.NowSendToMUX()| " + ex.Message);
            }
        }

        public void NowSendToMUXStart(string sessionId, string genId, string nowDateTime)
        {
            for (int i = 0; i < lvSpcMsg.Items.Count; i++)
            {
                //현재 프로세싱 중인 것 "전송중" 출력
                if (lvSpcMsg.Items[i].SubItems[0].Text.Equals(genId))
                {
                    //최초 전송이라면
                    if (this.lvSpcMsg.Items[i].SubItems[4].Text.Equals(""))
                    {
                        try
                        {
                            //DB 상태값 업데이트: '전송중'
                            if (odec.openDb())
                            {
                                StringBuilder sBuilder = new StringBuilder();
                                sBuilder.Append(" UPDATE message SET tstarttime = (:1) , status = (:2)  WHERE msgid = (:3) AND msgtype=(:4) ");
                                List<oracle_parameter> inner_parameters = new List<oracle_parameter>();

                                inner_parameters.Add(
                                   new oracle_parameter("tstarttime", oracle_parameter.OracleDataType.Varchar2, DateTime.Now.ToString("yyyyMMddHHmmss")
                                                                   , ParameterDirection.Input));
                                inner_parameters.Add(
                                    new oracle_parameter("status", oracle_parameter.OracleDataType.Int32, 1, ParameterDirection.Input)); //전송중
                                inner_parameters.Add(
                                    new oracle_parameter("msgid", oracle_parameter.OracleDataType.Int32, Convert.ToInt32(genId), ParameterDirection.Input));
                                inner_parameters.Add(
                                    new oracle_parameter("msgtype", oracle_parameter.OracleDataType.Int32, 1, ParameterDirection.Input));

                                int result = odec.WorkSql(sBuilder.ToString(), inner_parameters, "message");
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("EWSSystem.MainViewForm.NowSendToMUXStart(DB-Update)| " + ex.Message);
                            Log.WriteLog("EWSSystem.MainViewForm.NowSendToMUXStart(DB-Update)| " + ex.Message);
                        }


                        //전송시작 시간 출력
                        this.lvSpcMsg.Items[i].SubItems[4].Text = nowDateTime;
                        //상태, 색깔, 최근전송 시간 출력
                        this.lvSpcMsg.Items[i].SubItems[3].Text = "전송중";
                        this.lvSpcMsg.Items[i].BackColor = Color.GreenYellow;
                        this.lvSpcMsg.Items[i].SubItems[5].Text = nowDateTime;

                        //송출 서버에 전송중 data Send
                        //(프로토콜 만들기)
                        try
                        {
                            Protocol11 p11 = new Protocol11();
                            TimeSpan timespan = DateTime.Now.Subtract(new DateTime(1970, 1, 1, 9, 0, 0));
                            p11.transferTime = (int)timespan.TotalSeconds;
                            p11.generationId = Convert.ToInt32(genId);
                            byte[] resultBytes = new byte[1];
                            resultBytes[0] = Convert.ToByte('1'); //성공
                            p11.bodyData = resultBytes;

                            //(송출 서버에 보내기)
                            byte[] tempBuff = p11.MakeData();
                            if (tempBuff[0] != 0)
                            {
                                sessionManager.sessionList[sessionId].SendData(tempBuff);
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("EWSSystem.MainViewForm.NowSendToMUXStart(Protocol)| " + ex.Message);
                            Log.WriteLog("EWSSystem.MainViewForm.NowSendToMUXStart(Protocol)| " + ex.Message);
                        }
                    }
                    else //최초 전송이 아니면
                    {
                        this.lvSpcMsg.Items[i].SubItems[3].Text = "전송중";
                        this.lvSpcMsg.Items[i].BackColor = Color.GreenYellow;
                        this.lvSpcMsg.Items[i].SubItems[5].Text = nowDateTime;
                    }
                }
                //나머지 "대기" 출력
                else if (lvSpcMsg.Items[i].SubItems[0].Text != genId)
                {
                    this.lvSpcMsg.Items[i].SubItems[3].Text = "대기";
                    this.lvSpcMsg.Items[i].BackColor = Color.White;
                }
            }
        }
        #endregion


        #region  특수 메세지 전송 종료 -> 폼 제거
        void FinishSendToMUX(string sessionId, string genId, string nowDateTime)
        {
            try
            {
                this.Invoke(new InvokeFinishSendToMUX(this.FinishSendToMUXStart), new object[] { sessionId, genId, nowDateTime });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EWSSystem.MainViewForm.FinishSendToMUX()| " + ex.Message);
                Log.WriteLog("EWSSystem.MainViewForm.FinishSendToMUX()| " + ex.Message);
            }
        }

        public void FinishSendToMUXStart(string sessionId, string genId, string nowDateTime)
        {
            for (int i = 0; i < lvSpcMsg.Items.Count; i++)
            {
                if (lvSpcMsg.Items[i].SubItems[0].Text.Equals(genId))
                {
                    //리스트에서 삭제
                    this.lvSpcMsg.Items[i].Remove();

                    try
                    {
                        //DB 상태값 업데이트: '종료'
                        if (odec.openDb())
                        {
                            StringBuilder sBuilder = new StringBuilder();
                            sBuilder.Append(" UPDATE message SET tendtime = (:1) , status = (:2)  WHERE msgid = (:3) AND msgtype = (:4)");
                            List<oracle_parameter> inner_parameters = new List<oracle_parameter>();

                            inner_parameters.Add(
                               new oracle_parameter("tendtime", oracle_parameter.OracleDataType.Varchar2, DateTime.Now.ToString("yyyyMMddHHmmss")
                                                               , ParameterDirection.Input));
                            inner_parameters.Add(
                                new oracle_parameter("status", oracle_parameter.OracleDataType.Int32, 2, ParameterDirection.Input)); //종료
                            inner_parameters.Add(
                                new oracle_parameter("msgid", oracle_parameter.OracleDataType.Int32, Convert.ToInt32(genId), ParameterDirection.Input));
                            inner_parameters.Add(
                               new oracle_parameter("msgtype", oracle_parameter.OracleDataType.Int32, 1, ParameterDirection.Input));

                            int result = odec.WorkSql(sBuilder.ToString(), inner_parameters, "message");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("EWSSystem.MainViewForm.FinishSendToMUXStart(DB-Update)| " + ex.Message);
                        Log.WriteLog("EWSSystem.MainViewForm.FinishSendToMUXStart(DB-Update)| " + ex.Message);
                    }


                    //송출 서버에 종료 data Send
                    //(프로토콜 만들기)
                    try
                    {
                        Protocol11 p11 = new Protocol11();
                        TimeSpan timespan = DateTime.Now.Subtract(new DateTime(1970, 1, 1, 9, 0, 0));
                        p11.transferTime = (int)timespan.TotalSeconds;
                        p11.generationId = Convert.ToInt32(genId);
                        byte[] resultBytes = new byte[1];
                        resultBytes[0] = Convert.ToByte('3'); //종료
                        p11.bodyData = resultBytes;

                        //(송출 서버에 보내기)
                        byte[] tempBuff = p11.MakeData();
                        if (tempBuff[0] != 0)
                        {
                            sessionManager.sessionList[sessionId].SendData(tempBuff);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("MainViewForm.FinishSendToMUXStart (송출전송)- " + ex.Message);
                    }

                    #region 종료 메세지 폼 출력 (현재 사용하지 않음.)
                    //this.lvSpcMsg.Items[i].SubItems[3].Text = "종료";
                    //this.lvSpcMsg.Items[i].SubItems[3].ForeColor = Color.LightGray;
                    //this.lvSpcMsg.Items[i].SubItems[5].Text = endDateTime;
                    //Thread.Sleep(1000);
                    //this.lvSpcMsg.Items[i].Remove();
                    #endregion
                }
            }
        }
        #endregion


        #region 특수 메세지 에러발생 -> 폼에서 제거
        void ErrorSendToMUX(string sessionId, string genId, string nowDateTime)
        {
            try
            {
                this.Invoke(new InvokeErrorSendToMUX(this.ErrorSendToMUXStart), new object[] { sessionId, genId, nowDateTime });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EWSSystem.MainViewForm.ErrorSendToMUX()| " + ex.Message);
                Log.WriteLog("EWSSystem.MainViewForm.ErrorSendToMUX()| " + ex.Message);
            }
        }

        public void ErrorSendToMUXStart(string sessionId, string genId, string nowDateTime)
        {
            for (int i = 0; i < lvSpcMsg.Items.Count; i++)
            {
                if (lvSpcMsg.Items[i].SubItems[0].Text.Equals(genId))
                {
                    //리스트에서 삭제
                    this.lvSpcMsg.Items[i].Remove();

                    //송출 서버에 종료 data Send
                    //(프로토콜 만들기)
                    try
                    {
                        Protocol11 p11 = new Protocol11();
                        TimeSpan timespan = DateTime.Now.Subtract(new DateTime(1970, 1, 1, 9, 0, 0));
                        p11.transferTime = (int)timespan.TotalSeconds;
                        p11.generationId = Convert.ToInt32(genId);

                        byte[] resultBytes = new byte[1];
                        resultBytes[0] = Convert.ToByte('0'); //실패

                        p11.bodyData = resultBytes;

                        //(송출 서버에 보내기)
                        byte[] tempBuff = p11.MakeData();
                        if (tempBuff[0] != 0)
                        {
                            sessionManager.sessionList[sessionId].SendData(tempBuff);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("EWSSystem.MainViewForm.ErrorSendToMUXStart()| " + ex.Message);
                        Log.WriteLog("EWSSystem.MainViewForm.ErrorSendToMUXStart()| " + ex.Message);
                    }
                }
            }
        }
        #endregion


        #region 서버 시작시 -> 폼 출력
        public void TcpServerStart()
        {
            try
            {
                this.Invoke(new InvokeTcpServerManagerStart(this.TcpServerStartSet), new object[] { });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EWSSystem.MainViewForm.TcpServerStart()| " + ex.Message);
                Log.WriteLog("EWSSystem.MainViewForm.TcpServerStart()| " + ex.Message);
            }
        }

        public void TcpServerStartSet()
        {
            this.lbServerStatus.Text = "정상";
        }
        #endregion


        #region 서버 종료시 -> 폼 출력
        public void TcpServerStop()
        {
            try
            {
                this.Invoke(new InvokeTcpServerManagerStop(this.TcpServerStopSet), new object[] { });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EWSSystem.MainViewForm.TcpServerStop()| " + ex.Message);
                Log.WriteLog("EWSSystem.MainViewForm.TcpServerStop()| " + ex.Message);
            }
        }

        public void TcpServerStopSet()
        {
            this.lbServerStatus.Text = "종료";
        }
        #endregion


        #region Session 이 접속시 -> 폼 출력
        public void TcpSessionConnect(Session session)
        {
            try
            {
                this.Invoke(new InvokeTcpSessionConnect(this.TcpSessionConnectStart), new object[] { session });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EWSSystem.MainViewForm.TcpSessionConnect()| " + ex.Message);
                Log.WriteLog("EWSSystem.MainViewForm.TcpSessionConnect()| " + ex.Message);
            }
        }


        //클라이언트 접속 MainViewForm에 적용 표시
        public void TcpSessionConnectStart(Session session)
        {
            string strIsLogin = "대기";
            string strConnStatus = "";

            //if (session.isLogin) strIsLogin = "성공";
            //else strIsLogin = "실패";

            if (session.isLogin) strConnStatus = "연결";
            else strConnStatus = "연결끊김";

            // 클라이언트 정보 출력
            //IP, Port 접속여부, 상태, 로그인, 구분
            //session.id.ToString()
            ListViewItem lvItem = new ListViewItem(new string[] { session.id.ToString()
                                                                                        , session.ip
                                                                                        , session.port.ToString()
                                                                                        , session.connTime
                                                                                        , strConnStatus
                                                                                        , strIsLogin
                                                                                        , session.connLocation });


            this.lvSession.Items.Add(lvItem);
        }
        #endregion


        #region Session 이 접속 종료시 -> 폼 출력

        // TcpClient 가 접속종료되면 MainViewForm 에  표시
        public void TcpSessionDisconnect(string id, string ip)
        {
            try
            {
                this.Invoke(new InvokeTcpSessionDisconnect(this.TcpSessionDisconnectStart), new object[] { id, ip });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EWSSystem.MainViewForm.TcpSessionDisconnect()| " + ex.Message);
                Log.WriteLog("EWSSystem.MainViewForm.TcpSessionDisconnect()| " + ex.Message);
            }
        }


        //클라이언트 접속 종료 MainViewForm 에 적용 표시
        public void TcpSessionDisconnectStart(string id, string ip)
        {
            // 같은 id가 들어오면 리스트에서 제거
            for (int i = 0; i < this.lvSession.Items.Count; i++)
            {
                if (lvSession.Items[i].SubItems[0].Text == id)
                {
                    lvSession.Items.RemoveAt(i);
                }
            }
        }
        #endregion


        #region Session 에서 데이터 수신시 -> 폼 출력
        public void TcpSessionData(string id, string ip, byte[] rBuffer)
        {
            try
            {
                this.Invoke(new InvokeTcpSessionReceive(this.TcpSessionReceiveStart), new object[] { id, ip, rBuffer });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EWSSystem.MainViewForm.TcpSessionData()| " + ex.Message);
                Log.WriteLog("EWSSystem.MainViewForm.TcpSessionData()| " + ex.Message);
            }
        }


        //클라이언트 데이터 수신시, MainViewForm 에 적용 표시
        public void TcpSessionReceiveStart(string id, string ip, byte[] rBuffer)
        {
            //폼의 받은 데이터란에 출력
            string revData = ProtocolBase.Byte2Hex(rBuffer);

            // 명령어 
            byte command = rBuffer[2];

            // 송출서버에 보낼 응답 byte Array
            byte[] buff = null;

            try
            {
                //command type 에 따른 프로토콜의 객체를 얻어온다.
                switch (command)
                {
                    //로그인 메세지 이면
                    case 0x01:
                        #region 로그인
                        try
                        {
                            //1. 파싱
                            Protocol01 p01 = (Protocol01)ProtocolBase.GetObject(command);
                            p01.ParseData(rBuffer);

                            //2. 데이터가 적합한지 체크
                            byte chkResult = CheckLoginData(p01);

                            //송출 서버로 보내줄 데이터 만들기
                            p01.result = chkResult;
                            buff = p01.MakeData();


                            if (buff[0] != 0)
                            {
                                //3. 송출 서버에 응답  
                                sessionManager.sessionList[id].SendData(buff);
                                //로그인이 정상적으로 이루어 졌다면
                                if (chkResult.Equals(0))
                                {
                                    //4. 리스트에 로그인 상태 출력
                                    SetLvSessionList(id, chkResult);
                                }
                                else //로그인 프로토콜, Version 에러나 Filler 에러가 났다면
                                {
                                    //연결 끊기
                                    this.sessionManager.DeleteSession(id, ip);
                                }
                            }
                            else //로그인 데이터 파싱 오류가 났다면
                            {
                                //연결 끊기
                                this.sessionManager.DeleteSession(id, ip);
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("EWSSystem.MainViewForm.TcpSessionReceiveStart(0x01-로그인)| " + ex.Message);
                            Log.WriteLog("EWSSystem.MainViewForm.TcpSessionReceiveStart(0x01-로그인)| " + ex.Message);
                        }
                        break;
                        #endregion


                    //특수수신기 메세지 이면
                    case 0x11:
                        #region 특수메세지
                        Protocol11 p11 = null;
                        try
                        {
                            //1. 파싱
                            p11 = (Protocol11)ProtocolBase.GetObject(command);
                            p11.ParseData(rBuffer);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("EWSSystem.MainViewForm.TcpSessionReceiveStart(0x11-특수-파싱)| " + ex.Message);
                            Log.WriteLog("EWSSystem.MainViewForm.TcpSessionReceiveStart(0x11-특수-파싱)| " + ex.Message);
                        }

                        try
                        {
                            //1-1. DB 저장
                            if (odec.openDb())
                            {
                                StringBuilder sBuilder = new StringBuilder();
                                sBuilder.Append("INSERT INTO message VALUES(:1, :2, :3, :4, :5, :6, :7) ");
                                List<oracle_parameter> inner_parameters = new List<oracle_parameter>();

                                inner_parameters.Add(
                                    new oracle_parameter("msgId", oracle_parameter.OracleDataType.Int32, p11.generationId, ParameterDirection.Input));
                                inner_parameters.Add(
                                    new oracle_parameter("msgtype", oracle_parameter.OracleDataType.Int32, 1, ParameterDirection.Input));
                                inner_parameters.Add(
                                    new oracle_parameter("sourcedept", oracle_parameter.OracleDataType.Varchar2, "소방방재청", ParameterDirection.Input));
                                inner_parameters.Add(
                                    new oracle_parameter("revtime", oracle_parameter.OracleDataType.Varchar2, DateTime.Now.ToString("yyyyMMddHHmmss")
                                                                    , ParameterDirection.Input));
                                inner_parameters.Add(
                                    new oracle_parameter("status", oracle_parameter.OracleDataType.Int32, 0, ParameterDirection.Input)); //대기
                                inner_parameters.Add(
                                    new oracle_parameter("tStartTime", oracle_parameter.OracleDataType.Varchar2, "", ParameterDirection.Input));
                                inner_parameters.Add(
                                    new oracle_parameter("tEndTime", oracle_parameter.OracleDataType.Varchar2, "", ParameterDirection.Input));

                                int i = odec.WorkSql(sBuilder.ToString(), inner_parameters, "message");
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("EWSSystem.MainViewForm.TcpSessionReceiveStart(0x11-특수-DB Insert)| " + ex.Message);
                            Log.WriteLog("EWSSystem.MainViewForm.TcpSessionReceiveStart(0x11-특수-DB Insert)| " + ex.Message);
                        }
                        //2. 리스트에 특수 메세지 수신 출력
                        //  리스트뷰 순서 : ID, 발령기관, 수신시간,  상태, 송출시작 시간, 최근 송출시간
                        //  최초 송출 시 -> 송출 시작 시간, 상태에 값을 채워줄 것임.
                        //  송출 시 -> 최근 송출 시간 에 값을 채워줄 것임.
                        //(시간 설정)
                        string strSpcNow = DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss");

                        //(발령기관)
                        string strSpcProvider = "";
                        if (p11.providerId.Equals(0))
                        {
                            strSpcProvider = "소방방재청";
                        }

                        ListViewItem lvISpctem = new ListViewItem(new string[] { p11.generationId.ToString()
                                                                                                    , strSpcProvider
                                                                                                    , strSpcNow
                                                                                                    , "대기"
                                                                                                    , ""
                                                                                                    , ""
                          });
                        this.lvSpcMsg.Items.Add(lvISpctem);
                        try
                        {
                            //3. 먹스 메세지로 만들기
                            MUXMsg spcMUXMessage = MUXMsg.MakeMUXMsg(id, p11);

                            //4. 특수 메세지 큐에 쌓기
                            if (spcMUXMessage != null)
                            {
                                spcMUXScheduler.AddSpcQueue(spcMUXMessage);
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("EWSSystem.MainViewForm.TcpSessionReceiveStart(0x11 - 특수 - Mux)| " + ex.Message);
                            Log.WriteLog("EWSSystem.MainViewForm.TcpSessionReceiveStart(0x11 - 특수 - Mux)| " + ex.Message);
                        }
                        break;
                        #endregion


                    //일반수신기 메세지 이면
                    case 0x12:
                        #region 일반 메세지
                        Protocol12 p12 = null;
                        try
                        {
                            //1. 파싱
                            p12 = (Protocol12)ProtocolBase.GetObject(command);
                            p12.ParseData(rBuffer);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("EWSSystem.MainViewForm.TcpSessionReceiveStart(0x12-일반 - 파싱)| " + ex.Message);
                            Log.WriteLog("EWSSystem.MainViewForm.TcpSessionReceiveStart(0x12-일반 - 파싱)| " + ex.Message);
                        }

                        try
                        {
                            //1-1. DB 저장
                            if (odec.openDb())
                            {
                                StringBuilder sBuilder = new StringBuilder();
                                sBuilder.Append("INSERT INTO message VALUES(:1, :2, :3, :4, :5, :6, :7) ");
                                List<oracle_parameter> inner_parameters = new List<oracle_parameter>();

                                inner_parameters.Add(
                                    new oracle_parameter("msgId", oracle_parameter.OracleDataType.Int32, p12.generationId, ParameterDirection.Input));
                                inner_parameters.Add(
                                    new oracle_parameter("msgtype", oracle_parameter.OracleDataType.Int32, 0, ParameterDirection.Input));
                                inner_parameters.Add(
                                    new oracle_parameter("sourcedept", oracle_parameter.OracleDataType.Varchar2, "소방방재청", ParameterDirection.Input));
                                inner_parameters.Add(
                                    new oracle_parameter("revtime", oracle_parameter.OracleDataType.Varchar2, DateTime.Now.ToString("yyyyMMddHHmmss")
                                                                    , ParameterDirection.Input));
                                inner_parameters.Add(
                                    new oracle_parameter("status", oracle_parameter.OracleDataType.Int32, 0, ParameterDirection.Input)); //대기
                                inner_parameters.Add(
                                    new oracle_parameter("tStartTime", oracle_parameter.OracleDataType.Varchar2, "", ParameterDirection.Input));
                                inner_parameters.Add(
                                    new oracle_parameter("tEndTime", oracle_parameter.OracleDataType.Varchar2, "", ParameterDirection.Input));

                                int i = odec.WorkSql(sBuilder.ToString(), inner_parameters, "message");
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("EWSSystem.MainViewForm.TcpSessionReceiveStart(0x12-일반 - DB Insert)| " + ex.Message);
                            Log.WriteLog("EWSSystem.MainViewForm.TcpSessionReceiveStart(0x12-일반 -  DB Insert)| " + ex.Message);
                        }

                        //2. 리스트에 특수 메세지 수신 출력
                        //(발령시간)
                        string strNorNow = DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss");

                        //(발령기관)
                        string strNorProvider = "";
                        if (p12.providerId.Equals(0))
                        {
                            strNorProvider = "소방방재청";
                        }

                        ListViewItem lvNorItem = new ListViewItem(new string[] { p12.generationId.ToString()
                                                                                                    , strNorProvider
                                                                                                    , strNorNow
                                                                                                    , "대기"
                                                                                                    , ""
                                                                                                    , ""
                                                                                                    });
                        this.lvNorMsg.Items.Add(lvNorItem);

                        try
                        {
                            //3. 먹스 메세지로 만들기
                            MUXMsg norMUXMessage = MUXMsg.MakeMUXMsg(id, p12);

                            //4. 일반 메세지 큐에 쌓기
                            norMUXScheduler.AddNorQueue(norMUXMessage);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("EWSSystem.MainViewForm.TcpSessionReceiveStart(0x12 - 일반 - Mux)| " + ex.Message);
                            Log.WriteLog("EWSSystem.MainViewForm.TcpSessionReceiveStart(0x12-일반 -  Mux)| " + ex.Message);
                        }
                        break;
                        #endregion


                    //EWS 시스템 상테 체크
                    case 0x21:
                        #region EWS 시스템
                        try
                        {
                            //1. 파싱
                            Protocol21 p21 = (Protocol21)ProtocolBase.GetObject(command);
                            p21.ParseData(rBuffer);

                            //2. 데이터를 넣는다.
                            p21.status = '1';

                            //3.  송출 서버에 응답  
                            buff = p21.MakeData();
                            sessionManager.sessionList[id].SendData(buff);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("EWSSystem.MainViewForm.TcpSessionReceiveStart(0x21- EWS 상태)| " + ex.Message);
                            Log.WriteLog("EWSSystem.MainViewForm.TcpSessionReceiveStart(0x21- EWS 상태)| " + ex.Message);
                        }
                        break;
                        #endregion


                    //MUX 상태 체크
                    case 0x22:
                        #region MUX 상태
                        try
                        {
                            //1. 파싱
                            Protocol22 p22 = (Protocol22)ProtocolBase.GetObject(command);
                            p22.ParseData(rBuffer);

                            //2. MUX 요청 후, 결과값 받기
                            bool resMUX = muxManager.CheckMUXOnce();
                            char charRes = '1'; //정상
                            if (resMUX == false) charRes = '0';//비정상

                            //3. 송출 서버에 응답
                            p22.status = Convert.ToChar(charRes);
                            buff = p22.MakeData();
                            sessionManager.sessionList[id].SendData(buff);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("EWSSystem.MainViewForm.TcpSessionReceiveStart(0x22 - MUX 상태)| " + ex.Message);
                            Log.WriteLog("EWSSystem.MainViewForm.TcpSessionReceiveStart(0x22 - MUX 상태)| " + ex.Message);
                        }
                        break;
                        #endregion


                    //발령 취소 
                    case 0x31:
                        #region 발령취소
                        try
                        {
                            //1. 파싱
                            Protocol31 p31 = (Protocol31)ProtocolBase.GetObject(command);
                            p31.ParseData(rBuffer);

                            //2. MUX 요청 후, 결과값 받기기
                            bool resMUX = muxManager.RequestMsgCancelToMUX(false, rBuffer);

                            //3. 송출 서버에 응답
                            p31.result = BitConverter.GetBytes(resMUX)[0];
                            buff = p31.MakeData();
                            sessionManager.sessionList[id].SendData(buff);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("EWSSystem.MainViewForm.TcpSessionReceiveStart(0x31 - 발령취소)| " + ex.Message);
                            Log.WriteLog("EWSSystem.MainViewForm.TcpSessionReceiveStart(0x31 - 발령취소)| " + ex.Message);
                        }
                        break;
                        #endregion
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainViewForm.TcpSessionReceiveStart - " + ex.Message);
            }

        }


        /// <summary>
        /// 로그인 데이터 체크 결과를 송출 서버 리스트 뷰에 출력
        /// </summary>
        /// <param name="id"></param>
        /// <param name="chkResult"></param>
        private void SetLvSessionList(string id, byte chkResult)
        {
            for (int i = 0; i < lvSession.Items.Count; i++)
            {
                if (lvSession.Items[i].SubItems[0].Text.Equals(id))
                {
                    switch (chkResult)
                    {
                        //성공
                        case 0:
                            this.lvSession.Items[i].SubItems[5].Text = "로그인";
                            break;
                    }
                }
            }
        }


        //로그인 데이터가 올바른지 판단
        /// <summary>
        /// 로그인 데이터가 올바른지 판단한다.
        /// </summary>
        /// <param name="p01"></param>
        /// <returns></returns>
        private byte CheckLoginData(Protocol01 p01)
        {
            try
            {
                //1. 버전이 0xFF 이 아니면
                if (!p01.version.Equals(0xFF))
                {
                    //버전 에러 리턴
                    return Convert.ToByte(1);
                }

                //리틀 엔디안과 빅엔디안 값이 같으면
                byte[] tmpByte = BitConverter.GetBytes(p01.filler1);
                byte[] tmpByte2 = BitConverter.GetBytes(p01.filler2);
                Array.Reverse(tmpByte2); //역순으로 바꾼다.

                if (BitConverter.ToInt32(tmpByte, 0).CompareTo(BitConverter.ToInt32(tmpByte2, 0)) != 0)
                {
                    //필러 에러 리턴
                    return Convert.ToByte(2);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EWSSystem.MainViewForm.CheckLoginData()| " + ex.Message);
                Log.WriteLog("EWSSystem.MainViewForm.CheckLoginData()| " + ex.Message);

                //버전 에러 리턴
                return Convert.ToByte(1);
            }

            // 성공 리턴
            return Convert.ToByte(0);
        }
        #endregion


        //(테스트 용)리스트 개수 확인
        private void label6_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.sessionManager.sessionList.Count.ToString());
        }

    }
}

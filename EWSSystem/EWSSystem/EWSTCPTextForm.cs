using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using DmbProtocol; //Dmb 송출 시스템의 Data 수신 및 응답을 위한 Protocol
using TcpServer; // 송출 시스템과의 Tcp 연결을 위해 필요한 Protocol

namespace EWSSystem
{
    public partial class TCPServerTestForm : Form
    {

        //#region 멤버 변수
        //public SessionManager sessionManager { get; set; }
        //public MUXManager muxManager { get; set; }
        //public MUXScheduler spcMUXScheduler { get; set; }
        //public MUXScheduler norMUXScheduler { get; set; }
        //#endregion


        //#region  대리자
        ////TCP 서버 및 클라이언트 이벤트 대리자
        //private delegate void InvokeTcpServerManagerStart();
        //private delegate void InvokeTcpServerManagerStop();
        //private delegate void InvokeTcpSessionConnect(Session session);
        //private delegate void InvokeTcpSessionDisconnect(string id, string ip);
        //private delegate void InvokeTcpSessionReceive(string id, string ip, byte[] rBuffer);


        //// 송출서버로 부터 받은 수신 데이터 파싱 이벤트 대리자
        //private delegate void Invokeparse_SetLoginEvt(string sId, byte[] buffer);
        //private delegate void Invokeparse_SpecialMsgEvt(string sId, byte[] buffer);
        //private delegate void Invokeparse_NormalMsgEvt(string sId, byte[] buffer);
        //private delegate void Invokeparse_EwsStateEvt(string sId, byte[] buffer);
        //private delegate void Invokeparse_MuxStateEvt(string sId, byte[] buffer);
        //private delegate void Invokeparse_MsgCancleEvt(string sId, byte[] buffer);
        //#endregion


        ///// <summary>
        ///// 기본 생성자
        ///// </summary>
        public TCPServerTestForm()
        {
            //InitializeComponent();
        }


        ///// <summary>
        ///// 폼이 로드될 때
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        private void TCPServerTestForm_Load(object sender, EventArgs e)
        {
            #region 테스트
            //int testInt = 5;
            //byte[] tempByte = new byte[4];
            //tempByte = BitConverter.GetBytes(testInt);
            //int testInt2 = BitConverter.ToInt32(tempByte, 0);
            #endregion

            //    try
        //    {
        //        #region 1. 변수 생성
        //        sessionManager = new SessionManager();
        //        muxManager = new MUXManager();
        //        spcMUXScheduler = new MUXScheduler();
        //        norMUXScheduler = new MUXScheduler();
        //        #endregion


        //        #region 2. IP, Port 설정
        //        //  IP 콤보박스에 리스트로 추가
        //        IPAddress[] addr = Dns.GetHostAddresses(Dns.GetHostName());
        //        for (int i = 0; i < addr.Length; i++)
        //        {
        //            this.cbxServerIp.Items.Add(addr[i].ToString());
        //        }
        //        this.cbxServerIp.SelectedIndex = 0;

        //        // 포트 세팅
        //        this.txtServerPort.Text = "9000";
        //        #endregion


        //        #region 3. 서버 이벤트 등록 및 시작
        //        //이벤트 등록
        //        sessionManager.evtTcpServerStart += new SessionManager.TcpServerStartHandler(TcpServerStart);
        //        sessionManager.evtTcpServerStop += new SessionManager.TcpServerStopHandler(TcpServerStop);
        //        sessionManager.evtConnectClient += new SessionManager.TcpConnectClientHandler(TcpSessionConnect);
        //        sessionManager.evtDisconnectClient += new SessionManager.TcpDisconnectClientHandler(TcpSessionDisconnect);
        //        sessionManager.evtReceiveData += new SessionManager.TcpReceiveDataHandler(TcpSessionData);

        //        sessionManager.Start(this.cbxServerIp.SelectedText, Convert.ToInt32(this.txtServerPort.Text));
        //        #endregion


        //        #region 4. 송출로 부터 이벤트 수신시 파싱 이벤트 등록 (현재 사용 안함.)
        //        //parse.evtSetLogin += new Parse.SetLoginHandle(parse_evtSetLogin);
        //        //parse.evtSpecialMsg += new Parse.SpecialMsgHandle(parse_evtSpecialMsg);
        //        //parse.evtNormalMsg += new Parse.NormalMsgHandle(parse_evtNormalMsg);
        //        //parse.evtEwsState += new Parse.EwsStateHandle(parse_evtEwsState);
        //        //parse.evtMuxState += new Parse.MuxStateHandle(parse_evtMuxState);
        //        //parse.evtMsgCancle += new Parse.MsgCancleHandle(parse_evtMsgCancle);
        //        #endregion

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("EWSSystemForm.TCPServerTestForm_Load - " + ex.Message);
        //   }
        }


        //#region 송출로부터 수신 데이터(로그인) 파싱 후 -> 폼 출력
        //void parse_evtSetLogin(string sId, byte[] buffer)
        //{
        //    try
        //    {
        //        this.Invoke(new Invokeparse_SetLoginEvt(this.parse_evtSetLoginStart), new object[] { sId, buffer });
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("EWSSystemForm.parse_evtSetLogin - " + ex.Message);
        //    }
        //}

        ////폼에 로그인 데이터  출력 -> MUX 전송 -> TCP send
        //public void parse_evtSetLoginStart(string sId, byte[] buffer)
        //{
        //    this.lbReceiveData.Items.Add("로그인 데이터가 파싱되었습니다.");

        //    //MUX Manager 클래스의 함수 부르기 
        //    byte[] tmpBuff = muxManager.RequesLoginMsgToMUX();
        //    this.lbReceiveData.Items.Add("로그인 데이터 전송이 요청되었습니다.");

        //    // TCP로 송출 서버에 응답 보내기
        //    sessionManager.sessionList[sId].SendData(tmpBuff);
        //    this.lbReceiveData.Items.Add("로그인 데이터 송신되었습니다.");
        //}
        //#endregion


        //#region 송출로부터 수신 데이터(EWS 상태) 파싱 후 -> 폼 출력
        //void parse_evtEwsState(string sId, byte[] buffer)
        //{
        //    try
        //    {
        //        this.Invoke(new Invokeparse_SetLoginEvt(this.parse_evtEwsStateStart), new object[] { sId, buffer });
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("EWSSystemForm.parse_evtSetLogin - " + ex.Message);
        //    }
        //}

        ////폼에 EWS 상태 데이터  출력 -> MUX 전송 -> TCP send
        //void parse_evtEwsStateStart(string sId, byte[] buffer)
        //{
        //    this.lbReceiveData.Items.Add("EWS 상태 데이터가 파싱되었습니다.");

        //    byte[] tmpBuff = this.muxManager.RequesEWSStatusMsgToMUX(buffer);
        //    this.lbReceiveData.Items.Add("EWS 상태 데이터 전송이 요청되었습니다.");

        //    this.sessionManager.sessionList[sId].SendData(tmpBuff);
        //    this.lbReceiveData.Items.Add("EWS 상태 데이터가 송신되었습니다.");

        //}
        //#endregion


        //#region 송출로부터 수신 데이터( MUX 상태) 파싱 후 -> 폼 출력
        //void parse_evtMuxState(string sId, byte[] buffer)
        //{
        //    try
        //    {
        //        this.Invoke(new Invokeparse_SetLoginEvt(this.parse_evtMuxStateStart), new object[] { sId, buffer });
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("EWSSystemForm.parse_evtSetLogin - " + ex.Message);
        //    }
        //}

        ////폼에 MUX 상태 데이터  출력 -> MUX 전송 -> TCP send
        //void parse_evtMuxStateStart(string sId, byte[] buffer)
        //{
        //    this.lbReceiveData.Items.Add("MUX 상태 데이터가 파싱되었습니다.");

        //    //MUX 로 보내기
        //    byte[] tmpBuff = this.muxManager.RequesMUXStatusMsgToMUX();
        //    this.lbReceiveData.Items.Add("MUX 상태 데이터가 MUX 로 전송되었습니다.");

        //    //TCP로 보내기
        //    this.sessionManager.sessionList[sId].SendData(tmpBuff);
        //    this.lbReceiveData.Items.Add("MUX 상태 데이터가 TCP 로 전송되었습니다.");
        //}
        //#endregion


        //#region 송출로부터 수신 데이터(발령취소) 파싱 후 -> 폼 출력
        //void parse_evtMsgCancle(string sId, byte[] buffer)
        //{
        //    try
        //    {
        //        this.Invoke(new Invokeparse_SetLoginEvt(this.parse_evtMsgCancleStart), new object[] { sId, buffer });
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("EWSSystemForm.parse_evtSetLogin - " + ex.Message);
        //    }
        //}

        ////폼에 발령취소 데이터  출력 -> MUX 전송 -> TCP send
        //void parse_evtMsgCancleStart(string sId, byte[] buffer)
        //{
        //    this.lbReceiveData.Items.Add("발령 취소 데이터가 파싱되었습니다.");

        //    //byte[] tmpBuffer = this.muxManager.RequestMsgCancelToMUX(buffer);
        //    this.lbReceiveData.Items.Add("발령 취소 데이터가 MUX로 전송되었습니다.");

        //    //this.sessionManager.sessionList[sId].SendData(tmpBuffer);
        //    this.lbReceiveData.Items.Add("발령 취소 데이터가 TCP 로 전송되었습니다.");
        //}
        //#endregion


        ////송출로부터 수신 데이터(특수 메세지) 파싱 후 -> 폼 출력
        //void parse_evtSpecialMsg(int sId, byte[] buffer)
        //{
        //    throw new NotImplementedException();
        //}

        ////송출로부터 수신 데이터(일반 메세지) 파싱 후 -> 폼 출력
        //void parse_evtNormalMsg(int sId, byte[] buffer)
        //{
        //    throw new NotImplementedException();
        //}


        //#region 서버 시작시 -> 폼 출력
        //public void TcpServerStart()
        //{
        //    try
        //    {
        //        this.Invoke(new InvokeTcpServerManagerStart(this.TcpServerStartSet), new object[] { });
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("EWSSystemForm.TcpServerStart - " + ex.Message);
        //    }
        //}

        //public void TcpServerStartSet()
        //{
        //    this.lbReceiveData.Items.Add("서버가 시작되었습니다.");
        //}
        //#endregion


        //#region 서버 종료시 -> 폼 출력
        //public void TcpServerStop()
        //{
        //    try
        //    {
        //        this.Invoke(new InvokeTcpServerManagerStop(this.TcpServerStopSet), new object[] { });
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("EWSSystemForm.TcpServerStop - " + ex.Message);
        //    }
        //}

        //public void TcpServerStopSet()
        //{
        //    this.lbReceiveData.Items.Add("서버가 종료되었습니다.");
        //}
        //#endregion


        //#region Session 이 접속시 -> 폼 출력
        //public void TcpSessionConnect(Session session)
        //{
        //    try
        //    {
        //        this.Invoke(new InvokeTcpSessionConnect(this.TcpSessionConnectStart), new object[] { session });
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("EWSSystemForm.TcpSessionConnect - " + ex.Message);
        //    }
        //}

        ////클라이언트 접속 EWSSystemForm에 적용 표시
        //public void TcpSessionConnectStart(Session session)
        //{

        //    string strIsLogin = null;
        //    string strConnStatus = null;

        //    if (session.isLogin) strIsLogin = "성공";
        //    else strIsLogin = "실패";

        //    if (session.isLogin) strConnStatus = "연결";
        //    else strConnStatus = "연결끊김";



        //    // 클라이언트 정보 출력
        //    ListViewItem lvItem = new ListViewItem(new string[] { session.id.ToString()
        //                                                                                , session.ip
        //                                                                                , session.port.ToString()
        //                                                                                , session.connTime
        //                                                                                , strConnStatus
        //                                                                                , strIsLogin
        //                                                                                , session.connLocation });


        //    this.lvSessionInfo.Items.Add(lvItem);

        //    //받았다는 알림 출력
        //    this.lbReceiveData.Items.Add("[Client IP:" + session.ip + "]클라이언트가 접속하였습니다.");
        //}
        //#endregion


        //#region Session 이 접속 종료시 -> 폼 출력

        //// TcpClinet 가 접속종료되면 EWSSystemForm 에  표시
        //public void TcpSessionDisconnect(string id, string ip)
        //{
        //    try
        //    {
        //        this.Invoke(new InvokeTcpSessionDisconnect(this.TcpSessionDisconnectStart), new object[] { id, ip });
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("EWSSystemForm.TcpSessionDisconnect - " + ex.Message);
        //    }
        //}


        ////클라이언트 접속 종료 EWSSystemForm 에 적용 표시
        //public void TcpSessionDisconnectStart(string id, string ip)
        //{
        //    // 같은 ip가 들어오면 리스트에서 제거
        //    for (int i = 0; i < lvSessionInfo.Items.Count; i++)
        //    {
        //        if (lvSessionInfo.Items[i].SubItems[0].Text == id)
        //        {
        //            lvSessionInfo.Items.RemoveAt(i);
        //        }
        //    }

        //    //폼에 출력
        //    this.lbReceiveData.Items.Add("[Client IP:" + ip + ", ID: " + id + "  ]클라이언트의 접속이 끊겼습니다.");
        //}
        //#endregion


        //#region Session 에서 데이터 수신시 -> 폼 출력
        ////public void TcpSessionData(int id, string ip, string strReceiveData)
        //public void TcpSessionData(string id, string ip, byte[] rBuffer)
        //{
        //    try
        //    {
        //        this.Invoke(new InvokeTcpSessionReceive(this.TcpSessionReceiveStart), new object[] { id, ip, rBuffer });
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("EWSSystemForm.TcpSessionData - " + ex.Message);
        //    }
        //}


        ////public void TcpSessionReceiveStart(int id, string ip, string strReceiveData)
        //public void TcpSessionReceiveStart(string id, string ip, byte[] rBuffer)
        //{
        //    //폼의 받은 데이터란에 출력
        //    string revData = ProtocolBase.Byte2Hex(rBuffer);
        //    this.lbReceiveData.Items.Add("[Client IP:" + ip + ", ID: " + id + "  ] " + revData);

        //    // 명령어 
        //    byte command = rBuffer[2];

        //    // 송출서버에 보낼 응답 byte Array
        //    byte[] buff = null;

        //    try
        //    {
        //        //command type 에 따른 프로토콜의 객체를 얻어온다.
        //        switch (command)
        //        {
        //            //로그인 메세지 이면
        //            case 0x01:
        //                //1. 파싱
        //                Protocol01 p01 = (Protocol01)ProtocolBase.GetObject(command);
        //                p01.ParseData(rBuffer);

        //                //2. 데이터가 잘 들어가 있는지 체크
        //                byte chkResult = CheckLoginData(p01);
        //                p01.result = chkResult;

        //                //3. 송출 서버에 응답  
        //                buff = p01.MakeData();
        //                sessionManager.sessionList[id].SendData(buff);
        //                break;


        //            //특수수신기 메세지 이면
        //            case 0x11:
        //                //1. 파싱
        //                Protocol11 p11 = (Protocol11)ProtocolBase.GetObject(command);
        //                p11.ParseData(rBuffer);

        //                //2. 먹스 메세지로 만들기
        //                MUXMessage spcMUXMessage = muxManager.MakeMUXMessege(p11);

        //                //3. 특수 메세지 큐에 쌓기
        //                spcMUXScheduler.AddSpcQueue(spcMUXMessage);
        //                break;



        //            //일반수신기 메세지 이면
        //            case 0x12:
        //                //1. 파싱
        //                Protocol12 p12 = (Protocol12)ProtocolBase.GetObject(command);
        //                p12.ParseData(rBuffer);

        //                //2. 먹스 메세지로 만들기
        //                MUXMessage norMUXMessage = muxManager.MakeMUXMessege(p12);

        //                //3. 일반 메세지 큐에 쌓기
        //                norMUXScheduler.AddNorQueue(norMUXMessage);
        //                break;



        //            //EWS 시스템 상테 체크
        //            case 0x21:
        //                //1. 파싱
        //                Protocol21 p21 = (Protocol21)ProtocolBase.GetObject(command);
        //                p21.ParseData(rBuffer);

        //                //2. 정상 데이터를 넣는다.
        //                p21.status = '1';

        //                //3.  송출 서버에 응답  
        //                buff = p21.MakeData();
        //                sessionManager.sessionList[id].SendData(buff);
        //                break;


        //            //MUX 시스템 상태 체크
        //            case 0x22:
        //                // 함수 파악이 덜 된 상황
        //                break;


        //            //발령 취소 
        //            case 0x31:
        //                //1. 파싱
        //                Protocol31 p31 = (Protocol31)ProtocolBase.GetObject(command);
        //                p31.ParseData(rBuffer);

        //                //2. MUX 요청 후, 결과값 받기기
        //                bool resMUX = muxManager.RequestMsgCancelToMUX(false, rBuffer);

        //                //3. 송출 서버에 응답
        //                p31.result = BitConverter.GetBytes(resMUX)[0];
        //                buff = p31.MakeData();
        //                sessionManager.sessionList[id].SendData(buff);
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("EWSSystemForm.TcpSessionReceiveStart - " + ex.Message);
        //    }

        //}

        ////로그인 데이터가 올바른지 판단
        //private byte CheckLoginData(Protocol01 p01)
        //{
        //    try
        //    {
        //        //1. 버전이 0xFF 이 아니면
        //        if (!p01.version.Equals(0xFF))
        //        {
        //            //버전 에러 리턴
        //            return Convert.ToByte(1);
        //        }

        //        //리틀 엔디안과 빅엔디안 값이 같으면
        //        byte[] tmpByte = BitConverter.GetBytes(p01.filler1);
        //        byte[] tmpByte2 = BitConverter.GetBytes(p01.filler2);
        //        Array.Reverse(tmpByte2); //역순으로 바꾼다.

        //        if (BitConverter.ToInt32(tmpByte, 0).CompareTo(BitConverter.ToInt32(tmpByte2, 0)) != 0)
        //        {
        //            //필러 에러 리턴
        //            return Convert.ToByte(2);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("EWSSystemForm.CheckLoginData - " + ex.Message);
        //        //버전 에러 리턴
        //        return Convert.ToByte(1);
        //    }

        //    // 성공 리턴
        //    return Convert.ToByte(0);
        //}
        //#endregion


        ///// <summary>
        ///// 서버 종료 버튼을 눌렀을 때
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        private void btnServerStop_Click(object sender, EventArgs e)
        {
        //    this.sessionManager.Stop();
        }


        ///// <summary>
        ///// 포트 적용 버튼 눌렀을 때
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        private void btnSetPort_Click(object sender, EventArgs e)
        {
        //    try
        //    {
        //        this.sessionManager.Stop();

        //        //입력 값 유효성 체크
        //        bool isValid = CheckPort(this.txtServerPort.Text);
        //        if (isValid)
        //        {
        //            int port = Convert.ToInt32(this.txtServerPort.Text);
        //            this.sessionManager.Start(this.cbxServerIp.SelectedText, port);
        //        }
        //        else
        //        {
        //            MessageBox.Show("올바른  port (1024 ~ 65535)를 입력해 주세요.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("EWSSystemForm.btnSetPort_Click - " + ex.Message);
        //    }
        }


        ///// <summary>
        ///// 입력 값 유효성 체크
        ///// </summary>
        ///// <param name="port"></param>
        ///// <returns></returns>
        //public bool CheckPort(string port)
        //{
        //    //범위체크 : 1024부터 65535
        //    int intPort = 0;

        //    try
        //    {
        //        intPort = Convert.ToInt32(port);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("EWSSystemForm.CheckPort - " + ex.Message);
        //        return false;
        //    }

        //    if ((intPort < 1024) || (intPort > 65535)) return false;

        //    return true;
        //}


        ///// <summary>
        ///// 서버 시작 버튼 클릭 시,
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        private void btnServerStart_Click(object sender, EventArgs e)
        {
        //    this.sessionManager.Start(this.cbxServerIp.SelectedText, Convert.ToInt32(this.txtServerPort.Text));
        }


        ///// <summary>
        ///// 포트 적용 버튼 클릭 시,
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        private void txtServerPort_KeyPress(object sender, KeyPressEventArgs e)
        {
        //    try
        //    {
        //        //숫자 입력 체크
        //        if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
        //        {
        //            e.Handled = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("EWSSystemForm.txtServerPort_KeyPress - " + ex.Message);
        //    }
        }

    }
}

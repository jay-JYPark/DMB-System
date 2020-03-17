using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Timers;
using System.IO.Ports;

using ADEng.dmbcomm;
using ADEng.Library.DMB;
using ADEng.Control.DMB;
using ADEng.DeviceControlP;
using TcpCommon;
using DmbProtocol;
using adeng.comm;
using DMBBIZ;
using DMBBIZ.DmbBiz;
using DMBBIZ.DmbDefine;
using DMBBIZ.DmbProtocol;
using DMBBIZ.DmbType;
using DMBFND;
using DMBFND.DmbCtrl;
using DMBFND.DmbDb;
using DMBFND.DmbLogging;

namespace DmbDSSystem
{
    public partial class MainForm : Form
    {
        #region delegate
        private delegate void SetDSTypeHandle();
        private delegate void WriteRecieve1MessageHandle(String strMsg);
        private delegate void TcpCliDisConnectMessageHandle();
        private delegate void TcpClientRecievedMsgEventHandle(byte[] rcvMsg);
        private delegate void InvokeEWSTCPState(bool state);
        private delegate void InvokerSystemCheckUpdate(List<ServerInfo> _serverInfo);
        private delegate void InvokerSplashClose();
        private delegate void InvokerReconnectInit(Object sender);
        private delegate void InvokerDisconnectInit(Object sender);
        private delegate void InvokerOptiondlg_BroadSelect();
        private delegate void InvokerCheckSelectEvt(string checkStr);
        private delegate void InvokerEWSStatusEvt(string checkStr);
        private delegate void InvokerMUXStatusEvt(string checkStr);
        private delegate void InvokeTestModulOpen();
        private delegate void InvokePrintsendMsg(byte[] totmsg);
        private delegate void InvokeSplshclose();
        private delegate void InvokeDeviceUpdate(List<DeviceInfo> _deviceList);
        private delegate void InvokeTcpCheck();
        #endregion

        #region Instance
        private MainViewForm mainviewform = null;
        private OptionDlg optiondlg = null;
        private VersionForm versionform = null;
        private DataManager datamanager = null;
        private SocketClient socketClt = null;
        private Secession secession = null;
        private SpcMsgProto spcmsgMng = null;
        private SerialMng serialMng = null;
        private TcpCheck tcpcheck = null;
        private RecvMng recvmng = null;
        private ToolStripStatusLabel tcptoolstripStatusLB = new ToolStripStatusLabel();
        private ToolStripStatusLabel naviToolstripStatusLB = new ToolStripStatusLabel();
        private ToolStripStatusLabel spToolstripStatusLB = new ToolStripStatusLabel();
        private DmbNetSessionClientMng forNaviSoc = null;
        private DmbNetSessionClientMng forSpSoc = null;
        private LogManager logMng = null;
        #endregion

        #region Variable
        private String serverIp = String.Empty;         // 서버플랫폼의 IP
        private String serverPort = String.Empty;       // 서버플랫폼의 접속 포트번호
        private System.Timers.Timer timer = new System.Timers.Timer();
        private string MuxIP = "192.168.10.200";
        private StringBuilder versionStr = new StringBuilder("");
        private string mainVersionStr = " <2016/07/26> Ver 1.0.0";
        #endregion

        #region 생성자
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        #region OnLoad
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                // 재실행 방지 코드
                bool executeProc;
                Mutex mutex = new Mutex(true, Application.ProductName, out executeProc);

                if (!executeProc)
                {
                    //MessageBox.Show("방송 DMB송출 시스템이 이미 실행중입니다.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.Dispose();
                    //Application.Exit();
                    //return;
                }

                DmbBizActivator.Active(DmbDefineActivatorCode.ForProv);
                TcpProfileMng.LoadNavProfileConfig();
                TcpProfileMng.LoadSpProfileConfig();
                this.SetServerState(); //서버상태 부분
                
                this.logMng = new LogManager();
                this.logMng.Dir_Mng();

                DmbProtocolCmdP protoP = DmbProtocolFactory.CreateDmbProtocol(DmbDefineCmd.CmdP) as DmbProtocolCmdP;
                protoP.IP = DMBBIZ.DmbUtility.DmbUtilityMng.IDmbEtcUtility.GetIPv4();
                this.forNaviSoc = new DmbNetSessionClientMng();
                this.forNaviSoc.PollingDatas = DmbProtocolFactory.MakeFrame(protoP);

                foreach (TcpProfileData eachTcpProfileData in TcpProfileMng.LstNavTcpProfileData)
                {
                    DmbProfile profileForDmb = new DmbProfile();
                    profileForDmb.IpAddr = eachTcpProfileData.IpAddr;
                    profileForDmb.Port = eachTcpProfileData.Port;
                    profileForDmb.Name = "ForNav";

                    this.forNaviSoc.AddProfile(profileForDmb);
                }

                this.forNaviSoc.ConnectNetSessionClient += new DmbNetSessionConnectEventHandler(forNaviSoc_ConnectNetSessionClient);
                this.forNaviSoc.RecvNetSessionClient += new DmbNetSessionRecvEventHandler(forNaviSoc_RecvNetSessionClient);
                this.forNaviSoc.CloseNetSessionClient +=new DmbNetSessionCloseEventHandler(forNaviSoc_CloseNetSessionClient);

                this.forSpSoc = new DmbNetSessionClientMng();
                this.forSpSoc.PollingDatas = DmbProtocolFactory.MakeFrame(protoP);

                foreach (TcpProfileData eachTcpProfileData in TcpProfileMng.LstSpTcpProfileData)
                {
                    DmbProfile profileForWeb = new DmbProfile();
                    profileForWeb.IpAddr = eachTcpProfileData.IpAddr;
                    profileForWeb.Port = eachTcpProfileData.Port;
                    profileForWeb.Name = "ForSp";

                    this.forSpSoc.AddProfile(profileForWeb);
                }

                this.forSpSoc.ConnectNetSessionClient += new DmbNetSessionConnectEventHandler(forSpSoc_ConnectNetSessionClient);
                this.forSpSoc.RecvNetSessionClient +=new DmbNetSessionRecvEventHandler(forSpSoc_RecvNetSessionClient);
                this.forSpSoc.CloseNetSessionClient +=new DmbNetSessionCloseEventHandler(forSpSoc_CloseNetSessionClient);

                this.forNaviSoc.StartSessionClientMng();
                this.forSpSoc.StartSessionClientMng();

                this.datamanager = DataManager.getInstance();
                this.Text +=  " " + DMBBIZ.DmbUtility.DmbUtilityMng.IDmbEtcUtility.GetVersionInfo();
                //this.Text += this.mainVersionStr;
                mainviewform = new MainViewForm();
                secession = new Secession();
                recvmng = RecvMng.getInstance();
                serialMng = SerialMng.getInstance();
                tcpcheck = new TcpCheck();

                mainviewform.MdiParent = this;
                mainviewform.Dock = DockStyle.Fill;
                MainToolStripButton.Checked = true;
                mainviewform.Show();

                this.tcpcheck.ConnectionCheckEvt += new TcpCheck.ConnectionCheckHandle(tcpcheck_ConnectionCheckEvt);
                this.tcpconnect(); //tcp통신연결 부분
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm_Load - " + ex.Message);
            }
        }
        #endregion

        #region 시도서비스 소켓이벤트
        void forSpSoc_ConnectNetSessionClient(object sender, DmbNetSessionConnectEventArgs e)
        {
            MethodInvoker invoker = delegate()
            {
                if (e.Connected)
                {
                    spToolstripStatusLB.Image = Properties.Resources.DMB_systemGreen;
                }
                else
                {
                    spToolstripStatusLB.Image = Properties.Resources.DMB_systemRed;
                }
            };

            if (this.InvokeRequired)
            {
                this.Invoke(invoker);
            }
            else
            {
                invoker();
            }
        }

        void forSpSoc_CloseNetSessionClient(object sender, DmbNetSessionCloseEventArgs e)
        {
            MethodInvoker invoker = delegate()
            {
                spToolstripStatusLB.Image = Properties.Resources.DMB_systemRed;
            };

            if (this.InvokeRequired)
            {
                this.Invoke(invoker);
            }
            else
            {
                invoker();
            }
        }

        void forSpSoc_RecvNetSessionClient(object sender, DmbNetSessionRecvEventArgs e)
        {
            try
            {
                List<DmbProtocolBase> lstDmbProtoBase = new List<DmbProtocolBase>();
                DmbNetDataReceiver.ParseDmbPacket(e.Buff, e.Len, (this.forSpSoc.GetDmbNetSessionContext(e.DmbProfile.IpAddr, e.DmbProfile.Port)).TcpClient, out lstDmbProtoBase);

                foreach (DmbProtocolBase protoBase in lstDmbProtoBase)
                {
                    if (protoBase.Cmd != DmbDefineCmd.CmdP)
                    {

                    }

                    switch (protoBase.Cmd)
                    {
                        case DmbDefineCmd.CmdA: //발령
                            DmbProtocolCmdA protoA = protoBase as DmbProtocolCmdA;
                            WMessage wMsg = new WMessage();
                            wMsg.AbortStatus = 0;
                            wMsg.BoolControl = false;
                            wMsg.BoolProcessing = true;
                            wMsg.CommitPart = WMessage.E_sendPart.All;
                            wMsg.DDateTime = protoA.OrderTime;
                            wMsg.EndpointPart = WMessage.E_sendPart.None;
                            wMsg.ID = uint.Parse(protoA.MsgId);
                            wMsg.IntControl = 0;
                            wMsg.IntProcessing = 1;
                            wMsg.Message = protoA.Message;
                            wMsg.ProtoA = protoA;
                            wMsg.RCount = (uint)protoA.RgnCount;
                            wMsg.Repeats = 1;
                            wMsg.Interval = 0;
                            wMsg.SDevice_ALL = false;
                            wMsg.SDevice_마을앰프 = true;
                            wMsg.SDevice_모니터링 = true;
                            wMsg.SDevice_상황실 = true;
                            wMsg.SDevice_유관기관 = true;
                            wMsg.SendID = 0;
                            wMsg.SendMode = (protoA.Mode == DmbDefineOrderMode.Real) ? (byte)3 : (byte)0;
                            wMsg.SMSMsg = protoA.CbsMsg;
                            wMsg.SOPT_CONTROL = false;
                            wMsg.SOPT_DMB = (protoA.MsgType == DmbDefineMsgType.Navi) ? true : false;
                            wMsg.SOPT_ETC = false;
                            wMsg.SOPT_LIVE = false;
                            wMsg.SOPT_SMS = (protoA.CbsMsg == string.Empty) ? false : true;
                            wMsg.SOPT_STOREDMESSAGE = (protoA.MsgType == DmbDefineMsgType.Sto) ? true : false;
                            wMsg.SOPT_TTS = (protoA.MsgType == DmbDefineMsgType.Tts) ? true : false;
                            wMsg.SOPT_WARNING = false;
                            wMsg.StoMsg = protoA.Message;
                            wMsg.TkPriority = 2;
                            wMsg.TkRegion = (protoA.SectionCode == DmbDefineSectionCode.National) ? (uint)1 :
                                (protoA.SectionCode == DmbDefineSectionCode.Administrative) ? (uint)3 :
                                (protoA.SectionCode == DmbDefineSectionCode.Terminal) ? (uint)4 : (uint)2;
                            wMsg.TTSMsg = protoA.Message;

                            //AL1 = 공습
                            //AL2 = 경계
                            //AL3 = 재난위험
                            //ACL = 해제
                            if (protoA.DisasterCode == "AL1" || protoA.DisasterCode == "AL2" || protoA.DisasterCode == "AL3" || protoA.DisasterCode == "ACL")
                            {
                                wMsg.SOPT_DMB = false;
                                wMsg.SOPT_SMS = false;
                                wMsg.SOPT_STOREDMESSAGE = false;
                                wMsg.SOPT_TTS = false;
                                wMsg.SOPT_WARNING = true;
                            }
                    
                            broadcast broad = null;

                            for (int i = 0; i < protoA.LstStage.Count; i++)
                            {
                                broad = new broadcast();
                                broad.BoolAborted = false;
                                broad.BoolProcess = true;
                                broad.WMessageID = uint.Parse(protoA.MsgId);
                                broad.StageID = (uint)protoA.LstStage[i] + 2;
                                wMsg.BCenterList.Add(broad);
                            }

                            mapMessageRegion mmr = null;

                            for (int i = 0; i < protoA.LstFullCode.Count; i++)
                            {
                                mmr = new mapMessageRegion();
                                mmr.DestFlag = (protoA.SectionCode == DmbDefineSectionCode.Terminal) ? (uint)1 : 0;
                                //mmr.ParentID = protoA.LstFullCode[i];
                                mmr.ParentName = protoA.LstFullCode[i];
                                mmr.WMessageID = uint.Parse(protoA.MsgId);
                                wMsg.MapTarget.Add(mmr);
                            }

                            this.mainviewform.datamanager_onWMessageUpdate(wMsg);
                            break;

                        case DmbDefineCmd.CmdF: //제어
                            DmbProtocolCmdF protoF = protoBase as DmbProtocolCmdF;
                            DmbProtocolCmdA subProtoA = DmbProtocolFactory.CreateDmbProtocol(DmbDefineCmd.CmdA) as DmbProtocolCmdA;
                            string ctrlMessage = string.Empty;

                            if (protoF.CtrlCode == DmbDefineControlCode.Reset)
                            {
                                P03 p = PMng.GetPObject("31", "1") as P03;
                                ctrlMessage = PMng.MakeFrame(p);
                            }
                            else if (protoF.CtrlCode == DmbDefineControlCode.AmpOutput)
                            {
                                //(0 : 0.1%, 1 : 25%, 2 : 50%, 3 : 75%, 4 : 90%, 5 : 100%)
                                if (protoF.LstCtrlData[0] == "1")
                                {
                                    P03 p = PMng.GetPObject("38", "0") as P03;
                                    ctrlMessage = PMng.MakeFrame(p);
                                }
                                else if (protoF.LstCtrlData[0] == "2")
                                {
                                    P03 p = PMng.GetPObject("38", "2") as P03;
                                    ctrlMessage = PMng.MakeFrame(p);
                                }
                                else if (protoF.LstCtrlData[0] == "3")
                                {
                                    P03 p = PMng.GetPObject("38", "3") as P03;
                                    ctrlMessage = PMng.MakeFrame(p);
                                }
                                else if (protoF.LstCtrlData[0] == "4")
                                {
                                    P03 p = PMng.GetPObject("38", "5") as P03;
                                    ctrlMessage = PMng.MakeFrame(p);
                                }
                            }
                            else if (protoF.CtrlCode == DmbDefineControlCode.Ars)
                            {
                                //프로토콜 없음
                            }
                            else if (protoF.CtrlCode == DmbDefineControlCode.SignBoard)
                            {
                                
                            }
                            
                            WMessage wMsgCtrl = new WMessage();
                            wMsgCtrl.AbortStatus = 0;
                            wMsgCtrl.BoolControl = true;
                            wMsgCtrl.BoolProcessing = true;
                            wMsgCtrl.CommitPart = WMessage.E_sendPart.All;
                            wMsgCtrl.DDateTime = protoF.CtrlTime;
                            wMsgCtrl.EndpointPart = WMessage.E_sendPart.None;
                            wMsgCtrl.ID = uint.Parse(protoF.MsgId);
                            wMsgCtrl.IntControl = 1;
                            wMsgCtrl.IntProcessing = 1;
                            wMsgCtrl.Message = ctrlMessage;
                            wMsgCtrl.ProtoF = protoF;
                            wMsgCtrl.RCount = (uint)protoF.RgnCount;
                            wMsgCtrl.Repeats = 1;
                            wMsgCtrl.Interval = 5;
                            wMsgCtrl.SDevice_ALL = true;
                            wMsgCtrl.SDevice_마을앰프 = true;
                            wMsgCtrl.SDevice_모니터링 = true;
                            wMsgCtrl.SDevice_상황실 = true;
                            wMsgCtrl.SDevice_유관기관 = true;
                            wMsgCtrl.SendDevice = WMessage.E_DeviceType.All;
                            wMsgCtrl.SendID = 0;
                            wMsgCtrl.SendMode = (byte)0;
                            wMsgCtrl.SMSMsg = string.Empty;
                            wMsgCtrl.SOPT_CONTROL = true;
                            wMsgCtrl.SOPT_DMB = false;
                            wMsgCtrl.SOPT_ETC = false;
                            wMsgCtrl.SOPT_LIVE = false;
                            wMsgCtrl.SOPT_SMS = false;
                            wMsgCtrl.SOPT_STOREDMESSAGE = false;
                            wMsgCtrl.SOPT_TTS = false;
                            wMsgCtrl.SOPT_WARNING = false;
                            wMsgCtrl.StoMsg = "특수수신기 제어";
                            wMsgCtrl.TkPriority = 2;
                            wMsgCtrl.TkRegion = (protoF.SectionCode == DmbDefineSectionCode.National) ? (uint)1 :
                                (protoF.SectionCode == DmbDefineSectionCode.Administrative) ? (uint)3 :
                                (protoF.SectionCode == DmbDefineSectionCode.Terminal) ? (uint)4 : (uint)2;
                            wMsgCtrl.TTSMsg = ctrlMessage;

                            broadcast broadCtrl = null;

                            for (int i = 0; i < this.datamanager.BroadList.Count; i++)
                            {
                                broadCtrl = new broadcast();
                                broadCtrl.BoolAborted = false;
                                broadCtrl.BoolProcess = true;
                                broadCtrl.WMessageID = uint.Parse(protoF.MsgId);
                                broadCtrl.StageID = (uint)this.datamanager.BroadList[i].ID;
                                wMsgCtrl.BCenterList.Add(broadCtrl);
                            }

                            mapMessageRegion mmrCtrl = null;

                            for (int i = 0; i < protoF.LstFullCode.Count; i++)
                            {
                                mmrCtrl = new mapMessageRegion();
                                mmrCtrl.DestFlag = (protoF.SectionCode == DmbDefineSectionCode.Terminal) ? (uint)1 : 0;
                                mmrCtrl.ParentName = protoF.LstFullCode[i];
                                mmrCtrl.WMessageID = uint.Parse(protoF.MsgId);
                                wMsgCtrl.MapTarget.Add(mmrCtrl);
                            }

                            subProtoA.DisasterCode = "STT";
                            subProtoA.SectionCode = protoF.SectionCode;
                            subProtoA.LstStage.Add(DmbDefineStage.Kbs);
                            subProtoA.LstStage.Add(DmbDefineStage.Mbc);
                            subProtoA.LstStage.Add(DmbDefineStage.Sbs);
                            subProtoA.LstStage.Add(DmbDefineStage.Ytn);
                            wMsgCtrl.ProtoA = subProtoA;

                            this.mainviewform.datamanager_onWMessageUpdate(wMsgCtrl);
                            break;

                        case DmbDefineCmd.CmdG: //상태요청
                            P01 p01 = new P01();
                            string allRequestData = this.AllDeviceStatusRequest(p01);
                            DmbProtocolCmdG protoG = protoBase as DmbProtocolCmdG;
                            DmbProtocolCmdA subProtoA1 = DmbProtocolFactory.CreateDmbProtocol(DmbDefineCmd.CmdA) as DmbProtocolCmdA;
                            WMessage wMsgCtrl1 = new WMessage();
                            wMsgCtrl1.AbortStatus = 0;
                            wMsgCtrl1.BoolControl = true;
                            wMsgCtrl1.BoolProcessing = true;
                            wMsgCtrl1.CommitPart = WMessage.E_sendPart.All;
                            wMsgCtrl1.DDateTime = DateTime.Now;
                            wMsgCtrl1.EndpointPart = WMessage.E_sendPart.None;
                            wMsgCtrl1.ID = 1;
                            wMsgCtrl1.IntControl = 1;
                            wMsgCtrl1.IntProcessing = 1;
                            wMsgCtrl1.Message = allRequestData;
                            wMsgCtrl1.RCount = 1;
                            wMsgCtrl1.Repeats = 1;
                            wMsgCtrl1.Interval = 5;
                            wMsgCtrl1.SDevice_ALL = true;
                            wMsgCtrl1.SDevice_마을앰프 = true;
                            wMsgCtrl1.SDevice_모니터링 = true;
                            wMsgCtrl1.SDevice_상황실 = true;
                            wMsgCtrl1.SDevice_유관기관 = true;
                            wMsgCtrl1.SendDevice = WMessage.E_DeviceType.All;
                            wMsgCtrl1.SendID = 0;
                            wMsgCtrl1.SendMode = (byte)0;
                            wMsgCtrl1.SMSMsg = string.Empty;
                            wMsgCtrl1.SOPT_CONTROL = true;
                            wMsgCtrl1.SOPT_DMB = false;
                            wMsgCtrl1.SOPT_ETC = false;
                            wMsgCtrl1.SOPT_LIVE = false;
                            wMsgCtrl1.SOPT_SMS = false;
                            wMsgCtrl1.SOPT_STOREDMESSAGE = false;
                            wMsgCtrl1.SOPT_TTS = false;
                            wMsgCtrl1.SOPT_WARNING = false;
                            wMsgCtrl1.StoMsg = "특수수신기 상태요청";
                            wMsgCtrl1.TkPriority = 2;
                            wMsgCtrl1.TkRegion = 4;
                            wMsgCtrl1.TTSMsg = allRequestData;

                            broadcast broadCtrl1 = null;

                            for (int i = 0; i < this.datamanager.BroadList.Count; i++)
                            {
                                broadCtrl1 = new broadcast();
                                broadCtrl1.BoolAborted = false;
                                broadCtrl1.BoolProcess = true;
                                broadCtrl1.WMessageID = 1;
                                broadCtrl1.StageID = (uint)this.datamanager.BroadList[i].ID;
                                wMsgCtrl1.BCenterList.Add(broadCtrl1);
                            }

                            mapMessageRegion mmrCtrl1 = null;
                                mmrCtrl1 = new mapMessageRegion();
                                mmrCtrl1.DestFlag = 1;
                                mmrCtrl1.ParentName = protoG.CdmaNumber;
                                mmrCtrl1.WMessageID = 1;
                                wMsgCtrl1.MapTarget.Add(mmrCtrl1);

                            subProtoA1.DisasterCode = "STT";
                            subProtoA1.SectionCode = DmbDefineSectionCode.Terminal;
                            subProtoA1.LstStage.Add(DmbDefineStage.Kbs);
                            subProtoA1.LstStage.Add(DmbDefineStage.Mbc);
                            subProtoA1.LstStage.Add(DmbDefineStage.Sbs);
                            subProtoA1.LstStage.Add(DmbDefineStage.Ytn);
                            wMsgCtrl1.ProtoA = subProtoA1;

                            this.mainviewform.datamanager_onWMessageUpdate(wMsgCtrl1);
                            break;
                    }
                }

                this.logMng.File_Mng(string.Format("시도(특수수신기) 서비스 => 데이터 수신시각 : {0}\n데이터 : {1}",
                    DateTime.Now.ToString(), DMBBIZ.DmbUtility.DmbUtilityMng.IDmbEtcUtility.Bytes2HexString(e.Buff)));
            }
            catch (Exception ex)
            {
                DmbLoggingMng.ILoggingException.WriteException("MainForm", "forSpSoc_RecvNetSessionClient - ", ex);
            }
        }
        #endregion

        #region 중앙서비스 소켓이벤트
        void forNaviSoc_ConnectNetSessionClient(object sender, DmbNetSessionConnectEventArgs e)
        {
            MethodInvoker invoker = delegate()
            {
                if (e.Connected)
                {
                    naviToolstripStatusLB.Image = Properties.Resources.DMB_systemGreen;
                }
                else
                {
                    naviToolstripStatusLB.Image = Properties.Resources.DMB_systemRed;
                }
            };

            if (this.InvokeRequired)
            {
                this.Invoke(invoker);
            }
            else
            {
                invoker();
            }
        }

        void forNaviSoc_CloseNetSessionClient(object sender, DmbNetSessionCloseEventArgs e)
        {
            MethodInvoker invoker = delegate()
            {
                naviToolstripStatusLB.Image = Properties.Resources.DMB_systemRed;
            };

            if (this.InvokeRequired)
            {
                this.Invoke(invoker);
            }
            else
            {
                invoker();
            }
        }

        void forNaviSoc_RecvNetSessionClient(object sender, DmbNetSessionRecvEventArgs e)
        {
            try
            {
                List<DmbProtocolBase> lstDmbProtoBase = new List<DmbProtocolBase>();
                DmbNetDataReceiver.ParseDmbPacket(e.Buff, e.Len, (this.forNaviSoc.GetDmbNetSessionContext(e.DmbProfile.IpAddr, e.DmbProfile.Port)).TcpClient, out lstDmbProtoBase);

                foreach (DmbProtocolBase protoBase in lstDmbProtoBase)
                {
                    if (protoBase.Cmd != DmbDefineCmd.CmdP)
                    {

                    }

                    switch (protoBase.Cmd)
                    {
                        case DmbDefineCmd.CmdA: //발령
                            DmbProtocolCmdA protoA = protoBase as DmbProtocolCmdA;
                            WMessage wMsg = new WMessage();
                            wMsg.AbortStatus = 0;
                            wMsg.BoolControl = false;
                            wMsg.BoolProcessing = true;
                            wMsg.CommitPart = WMessage.E_sendPart.All;
                            wMsg.DDateTime = protoA.OrderTime;
                            wMsg.EndpointPart = WMessage.E_sendPart.None;
                            wMsg.ID = uint.Parse(protoA.MsgId);
                            wMsg.IntControl = 0;
                            wMsg.IntProcessing = 1;
                            wMsg.Message = protoA.Message;
                            wMsg.ProtoA = protoA;
                            wMsg.RCount = (uint)protoA.RgnCount;
                            wMsg.Repeats = 1;
                            wMsg.SDevice_ALL = true;
                            wMsg.SDevice_마을앰프 = true;
                            wMsg.SDevice_모니터링 = true;
                            wMsg.SDevice_상황실 = true;
                            wMsg.SDevice_유관기관 = true;
                            wMsg.SendDevice = WMessage.E_DeviceType.All;
                            wMsg.SendID = 0;
                            wMsg.SendMode = (protoA.Mode == DmbDefineOrderMode.Real) ? (byte)3 : (byte)0;
                            wMsg.SMSMsg = protoA.CbsMsg;
                            wMsg.SOPT_CONTROL = false;
                            wMsg.SOPT_DMB = (protoA.MsgType == DmbDefineMsgType.Navi) ? true : false;
                            wMsg.SOPT_ETC = false;
                            wMsg.SOPT_LIVE = false;
                            wMsg.SOPT_SMS = (protoA.CbsMsg == string.Empty) ? false : true;
                            wMsg.SOPT_STOREDMESSAGE = (protoA.MsgType == DmbDefineMsgType.Sto) ? true : false;
                            wMsg.SOPT_TTS = (protoA.MsgType == DmbDefineMsgType.Tts) ? true : false;
                            wMsg.SOPT_WARNING = false;
                            wMsg.StoMsg = protoA.Message;
                            wMsg.TkPriority = 2;
                            wMsg.TkRegion = (protoA.SectionCode == DmbDefineSectionCode.National) ? (uint)1 :
                                (protoA.SectionCode == DmbDefineSectionCode.Administrative) ? (uint)3 :
                                (protoA.SectionCode == DmbDefineSectionCode.Terminal) ? (uint)4 : (uint)2;
                            wMsg.TTSMsg = protoA.Message;

                            broadcast broad = null;

                            for (int i = 0; i < protoA.LstStage.Count; i++)
                            {
                                broad = new broadcast();
                                broad.BoolAborted = false;
                                broad.BoolProcess = true;
                                broad.WMessageID = uint.Parse(protoA.MsgId);
                                broad.StageID = (uint)protoA.LstStage[i] + 2;
                                wMsg.BCenterList.Add(broad);
                            }

                            mapMessageRegion mmr = null;

                            for (int i = 0; i < protoA.LstFullCode.Count; i++)
                            {
                                mmr = new mapMessageRegion();
                                mmr.DestFlag = (protoA.SectionCode == DmbDefineSectionCode.Terminal) ? (uint)1 : 0;
                                //mmr.ParentID = protoA.LstFullCode[i];
                                mmr.ParentName = protoA.LstFullCode[i];
                                mmr.WMessageID = uint.Parse(protoA.MsgId);
                                wMsg.MapTarget.Add(mmr);
                            }

                            this.mainviewform.datamanager_onWMessageUpdate(wMsg);
                            break;
                    }
                }

                this.logMng.File_Mng(string.Format("중앙(일반수신기) 서비스 => 데이터 수신시각 : {0}\n데이터 : {1}",
                    DateTime.Now.ToString(), DMBBIZ.DmbUtility.DmbUtilityMng.IDmbEtcUtility.Bytes2HexString(e.Buff)));
            }
            catch (Exception ex)
            {
                DmbLoggingMng.ILoggingException.WriteException("MainForm", "forNaviSoc_RecvNetSessionClient - ", ex);
            }
        }
        #endregion

        #region OnClosing
        protected override void OnClosing(CancelEventArgs e)
        {
            DialogResult result = MessageBox.Show("프로그램을 종료하시겠습니까?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                this.socketClt.tcpConnectedEvt -= new SocketClient.TcpClientConnectedEventHandle(socketClt_tcpConnectedEvt);
                this.socketClt.tcpDiconnectedEvt -= new SocketClient.TcpClientDisConnectedEventHandle(socketClt_tcpDiconnectedEvt);
                this.socketClt.tcpCltRcvEvt -= new SocketClient.TcpClientRecievedMsgEventHandle(socketClt_tcpCltRcvEvt);

                if (this.socketClt.ConState && socketClt != null)
                {
                    this.socketClt.tcpClientStop();
                }

                DmbBizActivator.Inactive();

                this.Dispose(true);
            }
            else
            {
                e.Cancel = true;
            }

            base.OnClosing(e);
        }
        #endregion

        #region TCP통신 연결
        private void tcpconnect()
        {
            this.socketClt = SocketClient.getInstance();

            if (!this.socketClt.ConState)
            {
                if (!tcpCltStart())
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

        private Boolean tcpCltStart()
        {
            try
            {
                socketClt = SocketClient.getInstance();

                this.socketClt.tcpConnectedEvt += new SocketClient.TcpClientConnectedEventHandle(socketClt_tcpConnectedEvt);
                this.socketClt.tcpDiconnectedEvt += new SocketClient.TcpClientDisConnectedEventHandle(socketClt_tcpDiconnectedEvt);
                this.socketClt.tcpCltRcvEvt += new SocketClient.TcpClientRecievedMsgEventHandle(socketClt_tcpCltRcvEvt);
                BeginInvoke(new InvokeEWSTCPState(this.ewsTcpstate), false);

                if (!socketClt.socketClientInit(Properties.Settings.Default.cnfEWSIP, Properties.Settings.Default.cnfEWSPort))
                {
                    return false;
                }

                this.socketClt.recieveThread();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region UI 사용자 이벤트
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (optiondlg = new OptionDlg())
            {
                optiondlg.BroadSelect += new OptionDlg.BroadSelectedEventHandle(optiondlg_BroadSelect);
                optiondlg.SetDSType += new OptionDlg.SetDSTypeHandle(optiondlg_SetDSType);
                optiondlg.ShowDialog();
                optiondlg.BroadSelect -= new OptionDlg.BroadSelectedEventHandle(optiondlg_BroadSelect);
                optiondlg.SetDSType -= new OptionDlg.SetDSTypeHandle(optiondlg_SetDSType);
            }
        }
        
        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            versionform = new VersionForm("방송 DMB송출 시스템", this.versionStr.ToString());
            versionform.ShowDialog();
        }
        #endregion

        #region Event 관련
        void recvmng_EWSStatusEvent(string rst)
        {
            try
            {
                this.Invoke(new InvokerEWSStatusEvt(this.Recvmng_EWSStatusEvent), new object[] { rst });
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.recvmng_EWSStatusEvent - " + ex.Message);
            }
        }

        void recvmng_MUXStatusEvent(string rst)
        {
            try
            {
                this.Invoke(new InvokerMUXStatusEvt(this.Recvmng_MUXStatusEvent), new object[] { rst });
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.recvmng_MUXStatusEvent - " + ex.Message);
            }
        }

        void socketClt_tcpCltRcvEvt(byte[] rcvMsg)
        {
            try
            {
                BeginInvoke(new TcpClientRecievedMsgEventHandle(RecievedMsg), rcvMsg);
            }
            catch (Exception ex)
            {
                Console.WriteLine("socketClt_tcpCltRcvEvt - " + ex.Message);
            }
        }

        void socketClt_tcpConnectedEvt()
        {
            try
            {
                this.socketClt.ConState = true;
                String tmpMsg = String.Format("[{0} : 서버에 연결되었습니다]", Properties.Settings.Default.cnfEWSIP);
                BeginInvoke(new InvokeEWSTCPState(this.ewsTcpstate), true);

                //로그인
                Proto00 p00 = ProtoMng.GetPObject("00") as Proto00;
                byte[] body = p00.BodyMake();
                byte[] totproto = p00.totMake(body);
                socketClt.tcpClientSndMsg(totproto);

                this.Invoke(new InvokePrintsendMsg(this.PrintSendMsg), new object[] { totproto });
            }
            catch (Exception ex)
            {
                Console.WriteLine("socketClt_tcpConnectedEvt - " + ex.Message);
            }
        }

        void socketClt_tcpDiconnectedEvt()
        {
            try
            {
                this.socketClt.ConState = false;
                String tmpMsg = String.Format("[{0} : 서버연결이 종료되었습니다]", Properties.Settings.Default.cnfEWSIP);
                BeginInvoke(new TcpCliDisConnectMessageHandle(tcpClientDisconnectChange));
                BeginInvoke(new InvokeEWSTCPState(this.ewsTcpstate), false);

                this.tcpcheck.GEN = true;

                if (this.tcpcheck.FLAG != true)
                {
                    this.tcpcheck.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("socketClt_tcpDiconnectedEvt - " + ex.Message);
            }
        }

        void tcpcheck_ConnectionCheckEvt()
        {
            try
            {
                this.Invoke(new InvokeTcpCheck(this.Tcpcheck_ConnectionCheckEvt), new object[] { });
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.tcpcheck_ConnectionCheckEvt - " + ex.Message);
            }
        }

        private void Tcpcheck_ConnectionCheckEvt()
        {
            try
            {
                if (!this.socketClt.ConState)
                {
                    this.socketClt = SocketClient.getInstance();

                    if (!socketClt.socketClientInit(Properties.Settings.Default.cnfEWSIP, Properties.Settings.Default.cnfEWSPort))
                    {
                        Console.WriteLine("Tcpcheck_ConnectionCheckEvt - TCP 통신 연결실패!");
                        return;
                    }

                    this.socketClt.recieveThread();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.Tcpcheck_ConnectionCheckEvt - " + ex.Message);
            }
        }

        void optiondlg_SetDSType()
        {
            try
            {
                this.Invoke(new SetDSTypeHandle(this.Optiondlg_SetDSType), new object[] { });
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.optiondlg_SetDSType - " + ex.Message);
            }
        }

        private void Optiondlg_SetDSType()
        {
            this.Text = "방송 DMB송출 시스템" + this.mainVersionStr + " " + this.datamanager.getBroadName((uint)Properties.Settings.Default.cnfBroad);
        }

        void optiondlg_BroadSelect()
        {
            try
            {
                if (this.mainviewform.DmbIssuesLV.InvokeRequired)
                {
                    this.Invoke(new InvokerOptiondlg_BroadSelect(this.Optiondlg_BroadSelect), new object[] { });
                }
                else
                {
                    this.Optiondlg_BroadSelect();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.optiondlg_BroadSelect - " + ex.Message);
            }
        }

        private void Optiondlg_BroadSelect()
        {
            this.mainviewform.SetStatusLB();
            string tmpBroad = this.datamanager.getBroadName((uint)Properties.Settings.Default.cnfBroad);
            this.mainviewform.Notifyeffectstr = "●○ 방송 DMB송출 시스템" + tmpBroad + " ○●";
            this.mainviewform.MainNotifyEffect.Stop();
            this.mainviewform.MainNotifyEffect.DeleteAllMsg();
            this.mainviewform.MainNotifyEffect.Blink = false;
            this.mainviewform.MainNotifyEffect.AddMsg(this.mainviewform.Notifyeffectstr);
            this.mainviewform.MainNotifyEffect.Start();

            socketClt = SocketClient.getInstance();
        }
        #endregion

        #region 이벤트에 연결된 메소드 관련
        private void Recvmng_EWSStatusEvent(string rst)
        {
        }

        private void Recvmng_MUXStatusEvent(string rst)
        {
        }

        private void tcpClientDisconnectChange()
        {

        }

        private void RecievedMsg(byte[] rmsg)
        {
            string smsg = ProtoMng.Byte2Hex(rmsg);
            recvmng.ParseData(rmsg);
        }

        private void ewsTcpstate(bool sts)
        {
            if (sts)
            {
                tcptoolstripStatusLB.Image = Properties.Resources.DMB_systemGreen;
            }
            else
            {
                tcptoolstripStatusLB.Image = Properties.Resources.DMB_systemRed;
            }
        }
        #endregion

        #region 송수신 윈도우에 출력
        private void PrintSendMsg(byte[] totalbyte)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.PrintSendMsg - " + ex.Message);
            }
        }
        #endregion

        #region 서버상태 표시
        private void SetServerState()
        {
            try
            {
                naviToolstripStatusLB.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                naviToolstripStatusLB.Image = Properties.Resources.DMB_systemRed;
                naviToolstripStatusLB.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
                naviToolstripStatusLB.Spring = true;
                naviToolstripStatusLB.Text = "중앙서비스";
                naviToolstripStatusLB.Name = "navi";
                naviToolstripStatusLB.TextAlign = ContentAlignment.MiddleRight;
                naviToolstripStatusLB.TextImageRelation = TextImageRelation.ImageBeforeText;
                naviToolstripStatusLB.ImageScaling = ToolStripItemImageScaling.SizeToFit;
                this.dmbSeverStatCtrl.statusStrip1.Items.Add(naviToolstripStatusLB);

                spToolstripStatusLB.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                spToolstripStatusLB.Image = Properties.Resources.DMB_systemRed;
                spToolstripStatusLB.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
                spToolstripStatusLB.Text = "시도서비스";
                spToolstripStatusLB.Name = "sp";
                spToolstripStatusLB.TextAlign = ContentAlignment.MiddleRight;
                spToolstripStatusLB.TextImageRelation = TextImageRelation.ImageBeforeText;
                spToolstripStatusLB.ImageScaling = ToolStripItemImageScaling.SizeToFit;
                this.dmbSeverStatCtrl.statusStrip1.Items.Add(spToolstripStatusLB);

                tcptoolstripStatusLB.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                tcptoolstripStatusLB.Image = Properties.Resources.DMB_systemRed;
                tcptoolstripStatusLB.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
                tcptoolstripStatusLB.Text = "편성시스템";
                tcptoolstripStatusLB.Name = "ews";
                tcptoolstripStatusLB.TextAlign = ContentAlignment.MiddleRight;
                tcptoolstripStatusLB.TextImageRelation = TextImageRelation.ImageBeforeText;
                tcptoolstripStatusLB.ImageScaling = ToolStripItemImageScaling.SizeToFit;
                this.dmbSeverStatCtrl.statusStrip1.Items.Add(tcptoolstripStatusLB);
            }
            catch (Exception ex)
            {
                Console.WriteLine("SetServerState - " + ex.Message);
            }
        }
        #endregion

        private string AllDeviceStatusRequest(BaseProto bp)
        {
            try
            {
                StringBuilder sbd = new StringBuilder();

                for (int i = 1; i < 31; i++)
                {
                    if (i == 25 || i == 26)
                    {
                        continue;
                    }

                    if (i < 10)
                    {
                        bp = PMng.GetPObject("0" + i.ToString(), string.Empty) as P01;
                        sbd.Append(PMng.MakeFrame(bp));
                        bp = null;
                    }
                    else
                    {
                        bp = PMng.GetPObject(i.ToString(), string.Empty) as P01;
                        sbd.Append(PMng.MakeFrame(bp));
                        bp = null;
                    }
                }

                return sbd.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
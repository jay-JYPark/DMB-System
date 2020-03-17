using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.IO.Ports;
using System.Media;
using System.Threading;

using ADEng.Library.DMB;
using ADEng.Control.DMB;
using ADEng.dmbcomm;
using TcpCommon;
using DmbProtocol;

namespace DmbDSSystem
{
    public partial class MainViewForm : Form
    {
        #region Instance
        private DataManager datamanager = null;
        private OrderListSet orderlistset = null;
        private ControlSet controlset = null;
        private Secession secession = null;
        private EventLogMng eventLogMng = null;
        private RecvMng recvmng = null;
        private SerialMng serialMng = null;
        private LogManager logMng = null;
        #endregion

        #region Variable
        private string notifyeffectstr = string.Empty;        
        private List<WMessage> tmpwmsgList = null; //송출에서 관리 할 WMessage List
        private List<WMessage> wmsglistclone = new List<WMessage>();
        private List<WMessage> LoadCancleMsg = new List<WMessage>();
        private WMessage tempWmsg = null;//WMessage
        private string serialBuff = string.Empty;
        private Thread soundThread = null;
        private SoundPlayer sp = null;
        private bool isAlarm = true;
        #endregion

        #region Delegate
        private delegate void InvokeDMBOrderList(WMessage wmessage);
        private delegate void InvokeDMBFinishedList(WMessage wmsg);
        private delegate void InvokeDMBCancle(WMessage wmsg);
        private delegate void InvokeWmsgReceeied(WMessage wmsg);
        private delegate void InvokeWmsgCancleEvt(int _id, string _rst);
        private delegate void InvokeWmsgsuccess(WMessage wmsg);
        private delegate void InvokeSpcWmsgsuccess(WMessage wmsg);
        private delegate void InvokerPrintsendMsg(byte[] msg);
        private delegate void InvokeSpcWmsgFinish(WMessage wmsg);
        private delegate void InvokeNorWmsgFinish(WMessage wmsg);
        private delegate void InvokeSpcMuxRcEvt(uint _id, string _name, string _ip);
        private delegate void InvokeNorMuxRcEvt(uint _id, string _name, string _ip);
        private delegate void InvokeBroadWmsgList(List<WMessage> bwmsgList);
        private delegate void InvokeBroadAWmsgList(List<WMessage> bAwmsgList);
        private delegate void InvokeProcessing();
        private delegate void InvokeCancleWmsgEvt(WMessage wmsg);
        #endregion

        #region get, set
        public List<WMessage> TmpwmsgList
        {
            get { return this.tmpwmsgList; }
            set { this.tmpwmsgList = value; }
        }

        public List<WMessage> Wmsglistclone
        {
            get { return this.wmsglistclone; }
            set { this.wmsglistclone = value; }
        }

        public WMessage TempWmsg
        {
            get { return this.tempWmsg; }
            set { this.tempWmsg = value; }
        }

        public string Notifyeffectstr
        {
            get { return this.notifyeffectstr; }
            set { this.notifyeffectstr = value; }
        }
        #endregion

        public MainViewForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.controlset = new ControlSet();
            this.datamanager = DataManager.getInstance();
            this.serialMng = SerialMng.getInstance();
            this.InitCtrl();
            this.tmpwmsgList = new List<WMessage>();
            this.wmsglistclone = new List<WMessage>();
            this.recvmng = RecvMng.getInstance();
            this.tempWmsg = new WMessage();
            this.orderlistset = new OrderListSet();
            this.secession = new Secession();
            this.eventLogMng = new EventLogMng();
            this.logMng = new LogManager();

            this.serialMng.onSerialRecv += new SerialMng.DelegateSerialRecv(serialMng_onSerialRecv);
            this.datamanager.OnDSUseWMessageList += new DataManager.DelegateDSUseWMessageList(datamanager_OnDSUseWMessageList);
            
            this.datamanager.onWMessageUpdate += new DataManager.DelegateWMessageUpdate(datamanager_onWMessageUpdate); //이벤트 방식으로 변경
            this.datamanager.onWMessageFinished += new DataManager.DelegateWMessageFinished(datamanager_onWMessageFinished); //이벤트 방식으로 변경
            
            this.recvmng.MsgSuccessEvent += new RecvMng.MsgSuccessEventHandle(recvmng_MsgSuccessEvent);
            this.recvmng.MsgSpcSuccessEvent += new RecvMng.MsgSpcSuccessEventHandle(recvmng_MsgSpcSuccessEvent);
            this.recvmng.SpcFinishedEvent +=new RecvMng.SpcFinishedEventHandle(recvmng_SpcFinishedEvent);
            this.recvmng.NorFinishedEvent += new RecvMng.NorFinishedEventHandle(recvmng_NorFinishedEvent);
            this.recvmng.SpcMuxRcEvent += new RecvMng.SpcMuxRcEventHandle(recvmng_SpcMuxRcEvent);
            this.recvmng.NorMuxRcEvent += new RecvMng.NorMuxRcEventHandle(recvmng_NorMuxRcEvent);
            string tmpBroad = this.datamanager.getBroadName((uint)Properties.Settings.Default.cnfBroad);
            
            base.OnLoad(e);
        }

        #region 이벤트 Invoke
        /// <summary>
        /// 이벤트 방식으로 발령메시지 종료(KBS만 해당)
        /// </summary>
        /// <param name="_wmsg"></param>
        private void datamanager_onWMessageFinished(WMessage _wmsg)
        {
            string tmpBroad = this.datamanager.getBroadName((uint)Properties.Settings.Default.cnfBroad);

            try
            {
                if (tmpBroad == "<KBS>" || tmpBroad == "<MBC>" || tmpBroad == "<SBS>" || tmpBroad == "<YTN>")
                {
                    if (this.DmbIssuesLV.InvokeRequired)
                    {
                        this.Invoke(new InvokeDMBFinishedList(this.WmsgFinished), new object[] { _wmsg });
                    }
                    else
                    {
                        this.WmsgFinished(_wmsg);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainViewForm.datamanager_onWMessageFinished - " + ex.Message);
            }
        }

        /// <summary>
        /// 이벤트 방식으로 발령메시지 업데이트 (이거야)
        /// </summary>
        /// <param name="_wMessage"></param>
        public void datamanager_onWMessageUpdate(WMessage wMessage)
        {
            this.PrintSendMsg("발령시각 - " + wMessage.DDateTime.ToString() + "\r\n이벤트 들어온 직후 현재시각 - " + DateTime.Now);

            try
            {
                WMessage _wMessage = wMessage;

                if (this.DmbIssuesLV.InvokeRequired)
                {
                    this.Invoke(new InvokeWmsgReceeied(this.OnWMessageUpdate), new object[] { _wMessage });
                }
                else
                {
                    this.OnWMessageUpdate(_wMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainViewForm.datamanager_onWMessageUpdate - " + ex.Message);
            }
        }

        void datamanager_OnDSUseWMessageList(List<WMessage> _wmsgList)
        {
            try
            {
                if (this.DmbIssuesLV.InvokeRequired)
                {
                    this.Invoke(new InvokeBroadWmsgList(this.OnDSuseWMessageList), new object[] { _wmsgList });
                }
                else
                {
                    this.OnDSuseWMessageList(_wmsgList);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainViewForm.datamanager_OnDSUseWMessageList - " + ex.Message);
            }
        }

        void recvmng_SpcMuxRcEvent(uint _id, string _name, string _ip)
        {
            try
            {
                if (this.DmbIssuesLV.InvokeRequired)
                {
                    this.Invoke(new InvokeSpcMuxRcEvt(this.Recvmng_SpcMuxRcEvent), new object[] { _id, _name, _ip });
                }
                else
                {
                    this.Recvmng_SpcMuxRcEvent(_id, _name, _ip);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainViewForm.recvmng_SpcMuxRcEvent - " + ex.Message);
            }
        }

        void recvmng_NorMuxRcEvent(uint _id, string _name, string _ip)
        {
            try
            {
                if (this.DmbIssuesLV.InvokeRequired)
                {
                    this.Invoke(new InvokeNorMuxRcEvt(this.Recvmng_NorMuxRcEvent), new object[] { _id, _name, _ip });
                }
                else
                {
                    this.Recvmng_NorMuxRcEvent(_id, _name, _ip);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainViewForm.recvmng_NorMuxRcEvent" + ex.Message);
            }
        }

        void recvmng_NorFinishedEvent(WMessage _wmsg)
        {
            try
            {
                if (this.DmbIssuesLV.InvokeRequired)
                {
                    this.Invoke(new InvokeNorWmsgFinish(this.OnNorWmsgFinish), new object[] { _wmsg });
                }
                else
                {
                    this.OnNorWmsgFinish(_wmsg);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainViewForm.recvmng_NorFinishedEvent" + ex.Message);
            }
        }

        void recvmng_SpcFinishedEvent(WMessage _wmsg)
        {
            try
            {
                if (this.DmbIssuesLV.InvokeRequired)
                {
                    this.Invoke(new InvokeSpcWmsgFinish(this.OnSpcWmsgFinish), new object[] { _wmsg });
                }
                else
                {
                    this.OnSpcWmsgFinish(_wmsg);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainViewForm.recvmng_SpcFinishedEvent" + ex.Message);
            }
        }

        void recvmng_MsgSpcSuccessEvent(WMessage _wmsg)
        {
            try
            {
                if (this.DmbIssuesLV.InvokeRequired)
                {
                    this.Invoke(new InvokeSpcWmsgsuccess(OnSpcMsgSuccessEvent), new object[] { _wmsg });
                }
                else
                {
                    this.OnSpcMsgSuccessEvent(_wmsg);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("recvmng_MsgSpcSuccessEvent : {0}", ex.Message);
            }
        }

        void recvmng_MsgSuccessEvent(WMessage _wmsg)
        {
            try
            {
                if (this.DmbIssuesLV.InvokeRequired)
                {
                    this.Invoke(new InvokeWmsgsuccess(OnMsgSuccessEvent), new object[] { _wmsg });
                }
                else
                {
                    this.OnMsgSuccessEvent(_wmsg);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("recvmng_MsgSuccessEvent : {0}", ex.Message);
            }
        }
        #endregion

        #region 송출에서만 쓰이는 발령메시지 업데이트 이벤트
        /// <summary>
        /// KBS를 제외한 타 방송사에서 쓰이는 발령메시지 업데이트 이벤트
        /// </summary>
        /// <param name="_bwmsgList"></param>
        public void OnDSuseWMessageList(List<WMessage> _bwmsgList)
        {
            string tmpBroad = this.datamanager.getBroadName((uint)Properties.Settings.Default.cnfBroad);

            //============박찬호 수정 - 2013/11/19========================================
            if (!(tmpBroad == "<KBS>" || tmpBroad == "<MBC>" || tmpBroad == "<SBS>" || tmpBroad == "<YTN>"))
            //============================================================================
            {
                List<WMessage> tmpwml = new List<WMessage>(this.datamanager.BroadWmsgList);

                for (int j = 0; j < _bwmsgList.Count; j++)
                {
                    bool tmp = false;

                    for (int i = 0; i < tmpwml.Count; i++)
                    {
                        if (_bwmsgList[j].CompareTo(tmpwml[i], true) == 0) //같은거 있음
                        {
                            tmp = true;
                            break;
                        }
                        else //틀림
                        {
                            tmp = false;
                        }
                    }

                    if (tmp)
                    {
                        if (_bwmsgList[j].AbortStatus == (byte)WMessage.E_AbortStatus.Request)
                        {
                            //this.OnWMessageCancle(_bwmsgList[j]);
                        }
                    }
                    else
                    {
                        this.logMng.File_Mng(string.Format("ID - {0}, 발령시간 - {1}, 받은시간 - {2}, 송출이 발령이라고 판단한 MSG 이벤트로 던진 로그"
                            , _bwmsgList[j].ID, _bwmsgList[j].DDateTime, DateTime.Now));
                        this.OnWMessageUpdate(_bwmsgList[j]);//없으니까 발령이 들어온거다..
                    }
                }

                for (int j = 0; j < tmpwml.Count; j++)
                {
                    int count = 0;

                    for (int i = 0; i < _bwmsgList.Count; i++)
                    {
                        if (tmpwml[j].CompareTo(_bwmsgList[i], true) == 0) //같은거 있음
                        {
                            count++;
                        }
                    }

                    if (count == 0)
                    {
                        this.WmsgFinished(tmpwml[j]);
                    }
                }
            }
        }
        #endregion

        #region 리스트뷰 & NotifyEffect 초기화
        private void InitCtrl()
        {
            try
            {
                MainNotifyEffect.FontSize = 22;
                MainNotifyEffect.FontBold = true;
                MainNotifyEffect.InSpeed = 60;
                MainNotifyEffect.WaitTimeMs = 1000;
                MainNotifyEffect.BorderStyle = BorderStyle.FixedSingle;                
                MainNotifyEffect.Start();
                string tmpBroad = this.datamanager.getBroadName((uint)Properties.Settings.Default.cnfBroad);
                notifyeffectstr = "●○ 방송 DMB송출 시스템" + tmpBroad + " ○●";

                this.controlset.IssuesInitCtrl(this.DmbIssuesLV);//리스트뷰 컬럼초기화
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainViewForm.InitCtrl - " + ex.Message);
            }
        }
        #endregion

        #region UI 클릭 이벤트
        private void DmbIssuesLV_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                if (e.Column.ToString() != "0")
                {
                    if (this.DmbIssuesLV.Items.Count > 1)
                    {
                        if (this.DmbIssuesLV.Sorting == SortOrder.Ascending || DmbIssuesLV.Sorting == SortOrder.None)
                        {
                            this.DmbIssuesLV.ListViewItemSorter = new ListViewItemComparer(e.Column, "desc");
                            DmbIssuesLV.Sorting = SortOrder.Descending;
                        }
                        else
                        {
                            this.DmbIssuesLV.ListViewItemSorter = new ListViewItemComparer(e.Column, "asc");
                            DmbIssuesLV.Sorting = SortOrder.Ascending;
                        }

                        DmbIssuesLV.Sort();
                        this.controlset.SetListViewIndex(this.DmbIssuesLV);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DmbIssuesLV_ColumnClick - " + ex.Message);
            }
        }

        private void DmbIssuesLV_Click(object sender, EventArgs e)
        {
            this.SetStatusLB();

            if (this.soundThread != null)
            {
                this.soundThread.Abort();
                this.soundThread = null;
                sp.Stop();
            }
        }

        private void DmbIssuesLV_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            this.SetStatusLB();
        }

        private void MainViewForm_Activated(object sender, EventArgs e)
        {
            try
            {
                MainNotifyEffect.DeleteAllMsg();
                if (this.notifyeffectstr.Substring(0, 2) == "최근")
                {
                    MainNotifyEffect.Blink = true;
                    MainNotifyEffect.AddMsg(this.notifyeffectstr, Properties.Resources.megaphone_48x48_);
                }
                else
                {
                    MainNotifyEffect.Blink = false;
                    MainNotifyEffect.AddMsg(this.notifyeffectstr);
                }

                MainNotifyEffect.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainViewForm_Activated - " + ex.Message);
            }
        }

        private void MainViewForm_Deactivate(object sender, EventArgs e)
        {
            MainNotifyEffect.Stop();
        }

        private void EffectIcoLB_Click(object sender, EventArgs e)
        {
            this.ResetNotifyEffect();
        }

        private void MainNotifyEffect_DoubleClick(object sender, EventArgs e)
        {
            this.ResetNotifyEffect();
        }
        #endregion
        
        #region 정렬
        private class ListViewItemComparer : IComparer
        {
            private int col;
            public string sort = "asc";

            public ListViewItemComparer()
            {
                col = 0;
            }

            public ListViewItemComparer(int column, string sort)
            {
                col = column;
                this.sort = sort;
            }

            public int Compare(object x, object y)
            {
                if (sort == "asc")
                {
                    return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
                }
                else
                {
                    return String.Compare(((ListViewItem)y).SubItems[col].Text, ((ListViewItem)x).SubItems[col].Text);
                }
            }
        }
        #endregion

        #region 특수메시지 MUX응답 이벤트
        private void Recvmng_SpcMuxRcEvent(uint _id, string _name, string _ip)
        {
            this.DmbIssuesLV.Items[_id.ToString()].SubItems[6].Text = "발령 중";

            if (this.DmbIssuesLV.SelectedItems.Count > 0)
            {
                for (int j = 0; j < this.DmbIssuesLV.Items.Count; j++)
                {
                    for (int i = 0; i < this.DmbIssuesLV.SelectedItems.Count; i++)
                    {
                        string key = this.DmbIssuesLV.SelectedItems[i].Name;

                        if (this.DmbIssuesLV.Items[key].Selected == true)
                        {
                            this.DmbIssuesLV.Items[key].Selected = false;
                        }
                        if (this.DmbIssuesLV.Items[key].Focused == true)
                        {
                            this.DmbIssuesLV.Items[key].Focused = false;
                        }
                    }
                }
            }

            this.DmbIssuesLV.Items[_id.ToString()].Selected = true;
            this.DmbIssuesLV.Items[_id.ToString()].Focused = true;
            this.DmbIssuesLV.Items[_id.ToString()].EnsureVisible();
            this.SetStatusLB();
        }
        #endregion

        #region 일반메시지 MUX응답 이벤트
        private void Recvmng_NorMuxRcEvent(uint _id, string _name, string _ip)
        {
            this.DmbIssuesLV.Items[_id.ToString()].SubItems[6].Text = "발령 중";

            if (this.DmbIssuesLV.SelectedItems.Count > 0)
            {
                for (int j = 0; j < this.DmbIssuesLV.Items.Count; j++)
                {
                    for (int i = 0; i < this.DmbIssuesLV.SelectedItems.Count; i++)
                    {
                        string key = this.DmbIssuesLV.SelectedItems[i].Name;

                        if (this.DmbIssuesLV.Items[key].Selected == true)
                        {
                            this.DmbIssuesLV.Items[key].Selected = false;
                        }
                        if (this.DmbIssuesLV.Items[key].Focused == true)
                        {
                            this.DmbIssuesLV.Items[key].Focused = false;
                        }
                    }
                }
            }

            this.DmbIssuesLV.Items[_id.ToString()].Selected = true;
            this.DmbIssuesLV.Items[_id.ToString()].Focused = true;
            this.DmbIssuesLV.Items[_id.ToString()].EnsureVisible();
            this.SetStatusLB();
        }
        #endregion

        private void OnWMessageUpdateMainTD(WMessage _wmsg)
        {
            Thread mainTD = new Thread(new ParameterizedThreadStart(this.OnWMessageUpdate));
            mainTD.IsBackground = true;
            mainTD.Start((object)_wmsg);
        }

        #region 수신된 DMB메시지 이벤트 처리 (이거야)
        private void OnWMessageUpdateTD(object _wmsg)
        {
            WMessage wmessage = (WMessage)_wmsg;
            this.secession.TcpSendWmsg(wmessage);
            this.PrintSendMsg("편성으로 전송 직후 현재시각 - " + DateTime.Now);
        }

        /// <summary>
        /// 이거야
        /// </summary>
        /// <param name="_wmessage"></param>
        private void OnWMessageUpdate(object _wmessage)
        {
            try
            {
                WMessage wmessage = (WMessage)_wmessage;

                this.PrintSendMsg("OnWMessageUpdate 메소드 들어온 직후 현재시각 - " + DateTime.Now);

                for (int i = 0; i < wmessage.BCenterList.Count; i++)
                {
                    if (wmessage.BCenterList[i].StageID != (uint)Properties.Settings.Default.cnfBroad)
                    {
                        continue;
                    }

                    if (wmessage.SendPart != WMessage.E_sendPart.SMS)
                    {
                        SocketClient soc = SocketClient.getInstance();

                        this.orderlistset.PkidIndex = this.DmbIssuesLV.Items.Count;
                        this.DmbIssuesLV.Items.Add(this.orderlistset.setWmsgCollection(wmessage));
                        this.orderlistset.PkidIndex = 0;
                        this.PrintSendMsg("리스트뷰에 추가 직후 현재시각 - " + DateTime.Now);

                        if (soc.ConState == true)
                        {
                            this.PrintSendMsg("편성으로 전송 직전 현재시각 - " + DateTime.Now);
                            
                            Thread OnWMessageUpdateThread = new Thread(new ParameterizedThreadStart(OnWMessageUpdateTD));
                            OnWMessageUpdateThread.IsBackground = true;
                            OnWMessageUpdateThread.Start((object)wmessage);

                            //this.secession.TcpSendWmsg(wmessage);
                            //string userName = datamanager.GetUserName(Properties.Settings.Default.cnfUserId);
                            //eventLogMng.WriteLog("DMB_DS", EventLogEntryType.Information, wmessage.ID.ToString() + " 방송사 있는 재난메시지 발령 후", userName);
                            this.logMng.File_Mng(string.Format("ID - {0}, 발령시간 - {1}, 받은시간 - {2}, TCP연결상태 이므로 편성으로 전송하고 바로 직후(발령)"
                            , wmessage.ID, wmessage.DDateTime, DateTime.Now));
                        }
                        else
                        {
                            //string userName = datamanager.GetUserName(Properties.Settings.Default.cnfUserId);
                            //eventLogMng.WriteLog("DMB_DS", EventLogEntryType.Information, wmessage.ID.ToString() + " 방송사 있는 재난메시지 편성 연결되어 있지 않아 미전송", userName);
                            this.logMng.File_Mng(string.Format("ID - {0}, 발령시간 - {1}, 받은시간 - {2}, 방송사 있는 재난메시지 편성 연결되어 있지 않아 미전송"
                            , wmessage.ID, wmessage.DDateTime, DateTime.Now));
                        }

                        if (wmessage != null)
                        {
                            this.DmbIssuesLV.Items[wmessage.ID.ToString()].SubItems[6].Text = "방송사";
                        }

                        this.MainNotifyEffect.Stop();
                        this.MainNotifyEffect.DeleteAllMsg();
                        string tmpfact = string.Empty;
                        tmpfact = (wmessage.SendMode == (byte)0) ? "시험" : (wmessage.SendMode == (byte)1) ? "훈련" : "실제";
                        DateTime mdt = DateTime.Now;
                        this.notifyeffectstr = "최근 " + mdt.ToString("yyyy-MM-dd HH:mm:ss") + " 에 " + tmpfact + " 발령되었습니다.";
                        this.MainNotifyEffect.Blink = true;
                        this.MainNotifyEffect.AddMsg(this.notifyeffectstr, Properties.Resources.megaphone_48x48_);
                        this.MainNotifyEffect.Start();
                        this.controlset.SetListViewIndex(this.DmbIssuesLV);
                    }
                }

                if (wmessage.BCenterList.Count == 0)
                {
                    if (wmessage.SendPart != WMessage.E_sendPart.SMS)
                    {
                        SocketClient soc = SocketClient.getInstance();

                        this.orderlistset.PkidIndex = this.DmbIssuesLV.Items.Count;
                        this.DmbIssuesLV.Items.Add(this.orderlistset.setWmsgCollection(wmessage));
                        this.orderlistset.PkidIndex = 0;
                        this.PrintSendMsg("방송사 0개인 곳의 리스트뷰에 추가 직후 현재시각 - " + DateTime.Now);

                        if (soc.ConState == true)
                        {
                            this.PrintSendMsg("방송사 0개인 곳의 편성으로 전송 직전 현재시각 - " + DateTime.Now);

                            Thread OnWMessageUpdateThread = new Thread(new ParameterizedThreadStart(OnWMessageUpdateTD));
                            OnWMessageUpdateThread.IsBackground = true;
                            OnWMessageUpdateThread.Start((object)wmessage);

                            this.logMng.File_Mng(string.Format("ID - {0}, 발령시간 - {1}, 받은시간 - {2}, TCP연결상태 이므로 편성으로 전송하고 바로 직후(제어)"
                            , wmessage.ID, wmessage.DDateTime, DateTime.Now));
                        }
                        else
                        {
                            this.logMng.File_Mng(string.Format("ID - {0}, 발령시간 - {1}, 받은시간 - {2}, 방송사 없는 재난메시지 편성 연결되어 있지 않아 미전송(제어)"
                            , wmessage.ID, wmessage.DDateTime, DateTime.Now));
                        }

                        if (wmessage != null)
                        {
                            this.DmbIssuesLV.Items[wmessage.ID.ToString()].SubItems[6].Text = "방송사";
                        }

                        this.MainNotifyEffect.Stop();
                        this.MainNotifyEffect.DeleteAllMsg();
                        string tmpfact = string.Empty;
                        tmpfact = (wmessage.SendMode == (byte)0) ? "시험" : (wmessage.SendMode == (byte)1) ? "훈련" : "실제";
                        DateTime mdt1 = DateTime.Now;
                        this.notifyeffectstr = "최근 " + mdt1.ToString("yyyy-MM-dd HH:mm:ss") + " 에 " + tmpfact + " 발령되었습니다.";
                        this.MainNotifyEffect.Blink = true;
                        this.MainNotifyEffect.AddMsg(this.notifyeffectstr, Properties.Resources.megaphone_48x48_);
                        this.MainNotifyEffect.Start();
                        this.controlset.SetListViewIndex(this.DmbIssuesLV);
                    }
                }

                if (this.DmbIssuesLV.SelectedItems.Count > 0)
                {
                    for (int j = 0; j < this.DmbIssuesLV.Items.Count; j++)
                    {
                        for (int i = 0; i < this.DmbIssuesLV.SelectedItems.Count; i++)
                        {
                            string key = this.DmbIssuesLV.SelectedItems[i].Name;

                            if (this.DmbIssuesLV.Items[key].Selected == true)
                            {
                                this.DmbIssuesLV.Items[key].Selected = false;
                            }
                            if (this.DmbIssuesLV.Items[key].Focused == true)
                            {
                                this.DmbIssuesLV.Items[key].Focused = false;
                            }
                        }
                    }
                }

                if (this.DmbIssuesLV.Items.ContainsKey(wmessage.ID.ToString()))
                {
                    this.DmbIssuesLV.Items[wmessage.ID.ToString()].Selected = true;
                    this.DmbIssuesLV.Items[wmessage.ID.ToString()].Focused = true;
                    this.DmbIssuesLV.Items[wmessage.ID.ToString()].EnsureVisible();
                }

                this.SetStatusLB();
            }
            catch (Exception ex)
            {
                this.wmsglistclone.Add((WMessage)_wmessage);
                Console.WriteLine("OnWMessageUpdate - " + ex.Message);
            }
        }
        #endregion

        #region 특수메시지 종료처리 로컬이벤트
        private void OnSpcWmsgFinish(WMessage _wmsg)
        {
            this.ProcessingUpdate(_wmsg);
        }
        #endregion

        #region 일반메시지 종료처리 로컬이벤트
        private void OnNorWmsgFinish(WMessage _wmsg)
        {
            this.ProcessingUpdate(_wmsg);
        }
        #endregion

        #region 시리얼 응답 데이터 처리
        void serialMng_onSerialRecv(byte[] _recvData)
        {
            if (_recvData[0] == 0x05)
            {
                byte[] buff = { 0x06 };
                serialMng.SendDate(buff);
            }
            else if (_recvData[0] == 0x15)
            {
                if (this.serialBuff != string.Empty)
                {
                    serialMng.SendDate(Encoding.Default.GetBytes(this.serialBuff));
                }
            }
        }
        #endregion

        #region 발령메시지 프로세싱 업데이트(==0)
        private void WmsgFinished(WMessage wmsg)
        {
            try
            {
                string tmpBroad = this.datamanager.getBroadName((uint)Properties.Settings.Default.cnfBroad);

                //============박찬호 수정 - 2013/11/19========================================
                if (tmpBroad == "<KBS>" || tmpBroad == "<MBC>" || tmpBroad == "<SBS>" || tmpBroad == "<YTN>")
                //============================================================================
                {
                    WMessage tmpwmsg = new WMessage();
                    WMessageIDComparer tmpcomparer = new WMessageIDComparer();
                    tmpwmsg.ID = wmsg.ID;
                    this.datamanager.BroadWmsgList.Sort(tmpcomparer);
                    WMessage w = this.datamanager.BroadWmsgList[this.datamanager.BroadWmsgList.BinarySearch(tmpwmsg, tmpcomparer)];

                    this.datamanager.BroadWmsgList.Remove(wmsg);
                }

                this.DmbIssuesLV.Items.RemoveByKey(wmsg.ID.ToString());
                this.controlset.SetListViewIndex(this.DmbIssuesLV);

                this.MainNotifyEffect.Stop();
                this.MainNotifyEffect.DeleteAllMsg();
                string tmpfact = string.Empty;
                tmpfact = (wmsg.SendMode == (byte)0) ? "시험" : (wmsg.SendMode == (byte)1) ? "훈련" : "실제";
                DateTime dt = DateTime.Now;
                this.notifyeffectstr = "최근 " + dt.ToString("yyyy-MM-dd HH:mm:ss") + " 에 " + tmpfact + " 발령 종료되었습니다.";
                this.MainNotifyEffect.Blink = true;
                this.MainNotifyEffect.AddMsg(this.notifyeffectstr, Properties.Resources.megaphone_48x48_);
                this.MainNotifyEffect.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainViewForm.WmsgFinished - " + ex.Message);
            }
        }

        private void ProcessingUpdate(WMessage wmsg)
        {
            try
            {
                this.logMng.File_Mng(string.Format("ID - {0}, 발령시간 - {1}, 받은시간 - {2}, 편성이 송신한 종료 데이터 수신 후"
                            , wmsg.ID, wmsg.DDateTime, DateTime.Now));

                bool processingResult = false;

                string tmpBroad = this.datamanager.getBroadName((uint)Properties.Settings.Default.cnfBroad);

                //============박찬호 수정 - 2013/11/19========================================
                if (tmpBroad == "<KBS>" || tmpBroad == "<MBC>" || tmpBroad == "<SBS>" || tmpBroad == "<YTN>")
                //============================================================================
                {
                    if (wmsg.SendPart == wmsg.EndpointPart)
                    {
                        processingResult = datamanager.setWmessageFinish(wmsg, (uint)Properties.Settings.Default.cnfBroad);
                        //this.datamanager.BroadWmsgList.Remove(wmsg);
                        //this.tempWmsg = wmsg;

                        this.logMng.File_Mng(string.Format("ID - {0}, 발령시간 - {1}, 받은시간 - {2}, SF의 setWMessageFinish() 호출 후"
                            , wmsg.ID, wmsg.DDateTime, DateTime.Now));
                    }
                    else
                    {
                        return;
                    }
                }

                if (processingResult == false)
                {
                    tempWmsg = null;
                    Console.WriteLine("wmsg.SendPart와 wmsg.EndpointPart 같지 않거나 datamanager.setWmessageFinish가 False 리턴");
                    this.DmbIssuesLV.Items[wmsg.ID.ToString()].SubItems[6].Text = "종료실패";
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainViewForm.ProcessingUpdate - " + ex.Message);
            }
        }
        #endregion

        #region 일반메시지 파싱성공 이벤트 처리
        private void OnMsgSuccessEvent(WMessage wmessage)
        {
            string userName = "DS";
            eventLogMng.WriteLog("DMB_DS", EventLogEntryType.Information, "재난메시지 ID - " + wmessage.ID.ToString() + "파싱성공", userName);

            if (wmessage.SOPT_DMB != wmessage.SOPT_DMB_COMMIT)
            {
                if ((wmessage.SendPart == wmessage.CommitPart) && (wmessage.SOPT_SMS == wmessage.SOPT_SMS_COMMIT))
                {
                    this.DmbIssuesLV.Items[wmessage.ID.ToString()].SubItems[6].Text = "방송사(EWS)";

                    if (this.DmbIssuesLV.SelectedItems.Count > 0)
                    {
                        for (int j = 0; j < this.DmbIssuesLV.Items.Count; j++)
                        {
                            for (int i = 0; i < this.DmbIssuesLV.SelectedItems.Count; i++)
                            {
                                string key = this.DmbIssuesLV.SelectedItems[i].Name;

                                if (this.DmbIssuesLV.Items[key].Selected == true)
                                {
                                    this.DmbIssuesLV.Items[key].Selected = false;
                                }
                                if (this.DmbIssuesLV.Items[key].Focused == true)
                                {
                                    this.DmbIssuesLV.Items[key].Focused = false;
                                }
                            }
                        }
                    }

                    this.DmbIssuesLV.Items[wmessage.ID.ToString()].Selected = true;
                    this.DmbIssuesLV.Items[wmessage.ID.ToString()].Focused = true;
                    this.DmbIssuesLV.Items[wmessage.ID.ToString()].EnsureVisible();
                    this.SetStatusLB();
                }
            }
        }
        #endregion

        #region 특수메시지 파싱성공 이벤트 처리
        private void OnSpcMsgSuccessEvent(WMessage wmessage)
        {
            if ((wmessage.SOPT_CONTROL != wmessage.SOPT_CONTROL_COMMIT) || (wmessage.SOPT_STOREDMESSAGE != wmessage.SOPT_STOREDMESSAGE_COMMIT)
                || (wmessage.SOPT_WARNING != wmessage.SOPT_WARNING_COMMIT) || (wmessage.SOPT_TTS != wmessage.SOPT_TTS_COMMIT))
            {
                if (wmessage.SOPT_CONTROL)
                {
                    if ((wmessage.SendPart == wmessage.CommitPart) && (wmessage.SOPT_SMS == wmessage.SOPT_SMS_COMMIT))
                    {
                        this.DmbIssuesLV.Items[wmessage.ID.ToString()].SubItems[6].Text = "방송사(EWS)";

                        if (this.DmbIssuesLV.SelectedItems.Count > 0)
                        {
                            for (int j = 0; j < this.DmbIssuesLV.Items.Count; j++)
                            {
                                for (int i = 0; i < this.DmbIssuesLV.SelectedItems.Count; i++)
                                {
                                    string key = this.DmbIssuesLV.SelectedItems[i].Name;

                                    if (this.DmbIssuesLV.Items[key].Selected == true)
                                    {
                                        this.DmbIssuesLV.Items[key].Selected = false;
                                    }
                                    if (this.DmbIssuesLV.Items[key].Focused == true)
                                    {
                                        this.DmbIssuesLV.Items[key].Focused = false;
                                    }
                                }
                            }
                        }

                        this.DmbIssuesLV.Items[wmessage.ID.ToString()].Selected = true;
                        this.DmbIssuesLV.Items[wmessage.ID.ToString()].Focused = true;
                        this.DmbIssuesLV.Items[wmessage.ID.ToString()].EnsureVisible();
                        this.SetStatusLB();
                    }
                }
                else if (wmessage.SOPT_STOREDMESSAGE)
                {
                    if ((wmessage.SendPart == wmessage.CommitPart) && (wmessage.SOPT_SMS == wmessage.SOPT_SMS_COMMIT))
                    {
                        this.DmbIssuesLV.Items[wmessage.ID.ToString()].SubItems[6].Text = "방송사(EWS)";

                        if (this.DmbIssuesLV.SelectedItems.Count > 0)
                        {
                            for (int j = 0; j < this.DmbIssuesLV.Items.Count; j++)
                            {
                                for (int i = 0; i < this.DmbIssuesLV.SelectedItems.Count; i++)
                                {
                                    string key = this.DmbIssuesLV.SelectedItems[i].Name;

                                    if (this.DmbIssuesLV.Items[key].Selected == true)
                                    {
                                        this.DmbIssuesLV.Items[key].Selected = false;
                                    }
                                    if (this.DmbIssuesLV.Items[key].Focused == true)
                                    {
                                        this.DmbIssuesLV.Items[key].Focused = false;
                                    }
                                }
                            }
                        }

                        this.DmbIssuesLV.Items[wmessage.ID.ToString()].Selected = true;
                        this.DmbIssuesLV.Items[wmessage.ID.ToString()].Focused = true;
                        this.DmbIssuesLV.Items[wmessage.ID.ToString()].EnsureVisible();
                        this.SetStatusLB();
                    }
                }
                else if (wmessage.SOPT_WARNING)
                {
                    if ((wmessage.SendPart == wmessage.CommitPart) && (wmessage.SOPT_SMS == wmessage.SOPT_SMS_COMMIT))
                    {
                        this.DmbIssuesLV.Items[wmessage.ID.ToString()].SubItems[6].Text = "방송사(EWS)";

                        if (this.DmbIssuesLV.SelectedItems.Count > 0)
                        {
                            for (int j = 0; j < this.DmbIssuesLV.Items.Count; j++)
                            {
                                for (int i = 0; i < this.DmbIssuesLV.SelectedItems.Count; i++)
                                {
                                    string key = this.DmbIssuesLV.SelectedItems[i].Name;

                                    if (this.DmbIssuesLV.Items[key].Selected == true)
                                    {
                                        this.DmbIssuesLV.Items[key].Selected = false;
                                    }
                                    if (this.DmbIssuesLV.Items[key].Focused == true)
                                    {
                                        this.DmbIssuesLV.Items[key].Focused = false;
                                    }
                                }
                            }
                        }

                        this.DmbIssuesLV.Items[wmessage.ID.ToString()].Selected = true;
                        this.DmbIssuesLV.Items[wmessage.ID.ToString()].Focused = true;
                        this.DmbIssuesLV.Items[wmessage.ID.ToString()].EnsureVisible();
                        this.SetStatusLB();
                    }
                }
                else if (wmessage.SOPT_TTS)
                {
                    if ((wmessage.SendPart == wmessage.CommitPart) && (wmessage.SOPT_SMS == wmessage.SOPT_SMS_COMMIT))
                    {
                        this.DmbIssuesLV.Items[wmessage.ID.ToString()].SubItems[6].Text = "방송사(EWS)";

                        if (this.DmbIssuesLV.SelectedItems.Count > 0)
                        {
                            for (int j = 0; j < this.DmbIssuesLV.Items.Count; j++)
                            {
                                for (int i = 0; i < this.DmbIssuesLV.SelectedItems.Count; i++)
                                {
                                    string key = this.DmbIssuesLV.SelectedItems[i].Name;

                                    if (this.DmbIssuesLV.Items[key].Selected == true)
                                    {
                                        this.DmbIssuesLV.Items[key].Selected = false;
                                    }
                                    if (this.DmbIssuesLV.Items[key].Focused == true)
                                    {
                                        this.DmbIssuesLV.Items[key].Focused = false;
                                    }
                                }
                            }
                        }

                        this.DmbIssuesLV.Items[wmessage.ID.ToString()].Selected = true;
                        this.DmbIssuesLV.Items[wmessage.ID.ToString()].Focused = true;
                        this.DmbIssuesLV.Items[wmessage.ID.ToString()].EnsureVisible();
                        this.SetStatusLB();
                    }
                }
            }
        }
        #endregion

        #region MJD 변환 메소드
        /// <summary>
        /// 년, 월, 일을 받아 MJD로 변환한다.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns>
        /// 4 byte int형
        /// </returns>
        public int Date2MJD(int year, int month, int day)
        {
            int YEAR = 1900;
            int k, mjd;

            if (month == 1 || month == 2)
            {
                k = 1;
            }
            else
            {
                k = 0;
            }

            year -= YEAR;
            mjd = 14956 + day + (int)((year - k) * 365.25f) + (int)((month + 1 + k * 12) * 30.6001f);

            return mjd;
        }
        #endregion

        #region 발령정보 상세정보 표시 메서드
        public void SetStatusLB()
        {
            try
            {
                if (this.DmbIssuesLV.SelectedItems.Count == 1)
                {
                    WMessage wmsg = new WMessage();
                    WMessageIDComparer comparer = new WMessageIDComparer();
                    wmsg.ID = uint.Parse(this.DmbIssuesLV.SelectedItems[0].Name);
                    datamanager.BroadWmsgList.Sort(comparer);
                    WMessage w = datamanager.BroadWmsgList[datamanager.BroadWmsgList.BinarySearch(wmsg, comparer)];

                    //발령일시
                    this.dateTB.Text = w.DDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    //실제구분
                    string tmpfact = string.Empty;
                    tmpfact = (w.SendMode == (byte)0) ? "시험" : (w.SendMode == (byte)1) ? "훈련" : "실제";
                    this.disterTB.Text = tmpfact;
                    //발령상태
                    this.priTB.Text = this.DmbIssuesLV.SelectedItems[0].SubItems[6].Text;
                    //발령구분
                    if (w.SendPart != 0)
                    {
                        this.regionTB.Text = w.SendPart.ToString();
                    }
                    else
                    {
                        this.regionTB.Text = string.Empty;
                    }
                    //방송사
                    string tmp = string.Empty;
                    for (int i = 0; i < w.BCenterList.Count; i++)
                    {
                        tmp += datamanager.getBroadNameV2(w.BCenterList[i].StageID);
                    }
                    if (tmp != string.Empty)
                    {
                        this.partTB.Text = tmp.Substring(2, (tmp.Length - 2));
                    }
                    else
                    {
                        this.partTB.Text = tmp;
                    }
                    //DMB단문
                    if (w.BoolControl)
                    {
                        this.dmbmTB.Text = "수신기 상태/제어 요청";
                    }
                    else if (w.SendPart == WMessage.E_sendPart.STRD)
                    {
                        this.dmbmTB.Text = "수신기 저장메시지 요청";
                    }
                    else if (w.SendPart == WMessage.E_sendPart.WARN)
                    {
                        this.dmbmTB.Text = "수신기 경보사이렌 요청";
                    }
                    else
                    {
                        this.dmbmTB.Text = w.Message;
                    }
                    //TTS단문
                    if (w.BoolControl)
                    {
                        this.ttsmTB.Text = "수신기 상태/제어 요청";
                    }
                    else if (w.SOPT_STOREDMESSAGE)
                    {
                        this.ttsmTB.Text = "수신기 저장메시지 요청";
                    }
                    else if (w.SendPart == WMessage.E_sendPart.WARN)
                    {
                        this.ttsmTB.Text = "수신기 경보사이렌 요청";
                    }
                    else
                    {
                        this.ttsmTB.Text = w.TTSMsg;
                    }
                    //SMS단문
                    this.smsmTB.Text = w.SMSMsg;
                }
                else
                {
                    this.dateTB.Text = string.Empty;
                    this.disterTB.Text = string.Empty;
                    this.priTB.Text = string.Empty;
                    this.regionTB.Text = string.Empty;
                    this.partTB.Text = string.Empty;
                    this.dmbmTB.Text = string.Empty;
                    this.ttsmTB.Text = string.Empty;
                    this.smsmTB.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SetStatusLB - " + ex.Message);
            }
        }
        #endregion

        #region 송신 데이터 프린트 메서드
        private void PrintSendMsg(byte[] totalbyte)
        {
            try
            {
                string stmp = ProtoMng.Byte2Hex(totalbyte);
                string tmpstmp = " >> [" + stmp + "]\r\nSend Message Length >> " + totalbyte.Length + "\r\n";
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainViewForm.PrintSendMsg - " + ex.Message);
            }
        }

        private void PrintSendMsg(String _data)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("MainViewForm.PrintSendMsg - " + ex.Message);
            }
        }
        #endregion

        #region 정보창 초기화
        private void ResetNotifyEffect()
        {
            try
            {
                string tmpBroad = this.datamanager.getBroadName((uint)Properties.Settings.Default.cnfBroad);
                notifyeffectstr = "●○ 방송 DMB송출 시스템" + tmpBroad + " ○●";
                this.MainNotifyEffect.Stop();
                this.MainNotifyEffect.DeleteAllMsg();
                this.MainNotifyEffect.Blink = false;
                this.MainNotifyEffect.AddMsg(this.notifyeffectstr);
                this.MainNotifyEffect.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ResetNotifyEffect - " + ex.Message);
            }
        }
        #endregion
    }
}
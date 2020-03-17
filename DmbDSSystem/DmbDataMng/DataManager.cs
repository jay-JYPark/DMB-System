//#define NotKBS
#define KBS

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;

using ADEng.Control.DMB;
using ADEng.Library.DMB;

namespace ADEng.Library.DMB
{
    public class DataManager
    {
        #region 타임벨트 쓰레드
        /// <summary>
        /// 타임벨트가 돌면서 해야 되는 일이 있는 경우 메소드를 호출한다.
        /// </summary>
        protected Thread thTimeline = null;

        protected void goTimeline()
        {
            DateTime bTime = DateTime.Now;
            DateTime cTime = DateTime.Now;

            while (true)
            {
                cTime = DateTime.Now;

                if (bTime.Date != cTime.Date)
                {
                    setSystemDateTime();
                }

                bTime = cTime;
                Thread.Sleep(1000 * 60);
            }
        }
        #endregion

        #region 시간동기화를 위한 준비
        public struct SYSTEMTIME
        {
            public short wYear;
            public short wMonth;
            public short wDayOfWeek;
            public short wDay;
            public short wHour;
            public short wMinute;
            public short wSecond;
            public short wMilliseconds;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetSystemTime([In] ref SYSTEMTIME st);

        /// <summary>
        /// 주어진 시간으로 시간을 설정한다.
        /// </summary>
        /// <param name="dt">주어진시간 (반드시, UniversalTime를 이용해야 한다.)</param>
        protected void setSystemDateTime(DateTime dt)
        {
            SYSTEMTIME sysTime = new SYSTEMTIME();
            sysTime.wYear = (short)dt.Year;
            sysTime.wMonth = (short)dt.Month;
            sysTime.wDay = (short)dt.Day;
            sysTime.wHour = (short)dt.Hour;
            sysTime.wMinute = (short)dt.Minute;
            sysTime.wSecond = (short)dt.Second;
            sysTime.wMilliseconds = (short)dt.Millisecond;

            SetSystemTime(ref sysTime);
        }

        /// <summary>
        /// DB의 시간으로 시간을 설정한다.
        /// </summary>
        protected void setSystemDateTime()
        {
            if (sfConnection.ConnectStatus)
            {
                DateTime dt = sfConnection.remoteobject.getDateTime();
                setSystemDateTime(System.TimeZone.CurrentTimeZone.ToUniversalTime(dt));
            }
        }
        #endregion

        #region Variable
        private static String tr = String.Empty;
        private ADClient sfConnection = null;
        private String hostIp = String.Empty;
        private String hostPort = String.Empty;
        private static DataManager instance = null;
        private static System.Threading.Mutex mutex = new System.Threading.Mutex();
        private uint curUser = 0;
        #endregion

        #region 데이터 관리 리스트 맴버
        private List<DeviceResponse> searchResponseList = new List<DeviceResponse>();
        private List<RegionInfo> regionInfoList = new List<RegionInfo>();
        private List<TypeRegion> typeRegionList = new List<TypeRegion>();
        private List<DeviceInfo> deviceInfoList = new List<DeviceInfo>();
        private List<DeviceCheck> deviceCheckList = new List<DeviceCheck>();
        private List<SIStatus> serverCheckList = new List<SIStatus>();
        private List<User> userList = new List<User>();
        private List<Group> groupList = new List<Group>();
        private List<mapDeviceTopology> mapdevicetopology = new List<mapDeviceTopology>();
        private List<TypeDisaster> typeDisasterList = new List<TypeDisaster>();
        private List<WMessage> wMessageList = new List<WMessage>();
        private List<DeviceControl> deviceControlList = new List<DeviceControl>();
        private List<WMessage> searchWMessageList = new List<WMessage>();
        private List<TypeDisaster> getdisasterList = new List<TypeDisaster>();
        private List<TypePriority> getpriorityList = new List<TypePriority>();
        private List<TypeDevice> getTypedeviceList = new List<TypeDevice>();
        private List<TypeRegion> getregionList = new List<TypeRegion>();
        private List<WMessage> queryWMessageList = new List<WMessage>();
        private List<TypeServer> typeServerList = new List<TypeServer>();
        private List<ServerInfo> serverList = new List<ServerInfo>();
        private List<stage> broadList = new List<stage>();
        private List<WMessage> broadWmsgList = new List<WMessage>();
        #endregion

        #region 데이터 관리 속성
        /// <summary>
        /// 각 방송사의 온에어 리스트
        /// </summary>
        public List<WMessage> BroadWmsgList
        {
            get { return this.broadWmsgList; }
        }

        /// <summary>
        /// 방송중인 발령 리스트
        /// </summary>
        public List<WMessage> WMessageList
        {
            get { return wMessageList; }
        }

        /// <summary>
        /// 발령 요청한 상태/관제 리스트
        /// </summary>
        public List<DeviceControl> DeviceControlList
        {
            get { return deviceControlList; }
        }

        /// <summary>
        /// 조회한 발령 리스트
        /// </summary>
        public List<WMessage> SearchWMessageList
        {
            get { return searchWMessageList; }
        }

        /// <summary>
        /// 조회한 응답리스트
        /// </summary>
        public List<DeviceResponse> SearchResponseList
        {
            get { return searchResponseList; }
        }

        /// <summary>
        /// 지역 정보 리스트
        /// </summary>
        public List<RegionInfo> RegionInfoList
        {
            get { return regionInfoList; }
        }

        /// <summary>
        /// 지역 형식 정보 리스트
        /// </summary>
        public List<TypeRegion> TypeRegionList
        {
            get { return typeRegionList; }
        }

        /// <summary>
        /// 지역 코드 TYPE
        /// </summary>
        public List<TypeRegion> GetregionList
        {
            get { return typeRegionList; }
        }

        /// <summary>
        /// 재난 종류 정보 리스트
        /// </summary>
        public List<TypeDisaster> TypeDisasterList
        {
            get { return typeDisasterList; }
        }

        /// <summary>
        /// 재난 종류 코드 TYPE
        /// </summary>
        public List<TypeDisaster> GetdisasterList
        {
            get { return typeDisasterList; }
        }

        /// <summary>
        /// 수신기 정보 리스트
        /// </summary>
        public List<DeviceInfo> DeviceInfoList
        {
            get { return deviceInfoList; }
        }

        /// <summary>
        /// 서버 상태 정보 리스트
        /// </summary>
        public List<SIStatus> ServerCheckList
        {
            get { return serverCheckList; }
        }

        /// <summary>
        /// 수신기 상태 정보 리스트
        /// </summary>
        public List<DeviceCheck> DeviceCheckList
        {
            get { return deviceCheckList; }
        }

        /// <summary>
        /// 사용자 정보 리스트
        /// </summary>
        public List<User> UserList
        {
            get { return userList; }
        }

        /// <summary>
        /// 현재 로그인된 사용자 Key
        /// </summary>
        public uint CurUser
        {
            set { this.curUser = value; }
            get { return this.curUser; }
        }

        /// <summary>
        /// 수신기 그룹 정보 리스트
        /// </summary>
        public List<Group> GroupList
        {
            get { return groupList; }
        }

        /// <summary>
        /// 토폴로지 위에 올라간 장비 정보
        /// </summary>
        public List<mapDeviceTopology> Mapdevicetopology
        {
            get { return mapdevicetopology; }
        }

        /// <summary>
        /// 우선순위 TYPE
        /// </summary>
        public List<TypePriority> GetpriorityList
        {
            get { return getpriorityList; }
        }

        /// <summary>
        /// 수신기 TYPE
        /// </summary>
        public List<TypeDevice> GetTypedeviceList
        {
            get { return getTypedeviceList; }
        }

        /// <summary>
        /// WMessage 검색 시 사용
        /// </summary>
        public List<WMessage> QueryWMessageList
        {
            get { return queryWMessageList; }
        }

        /// <summary>
        /// 서버 정보 리스트
        /// </summary>
        public List<ServerInfo> ServerList
        {
            get { return serverList; }
        }

        /// <summary>
        /// 서버 종류 정보 리스트
        /// </summary>
        public List<TypeServer> TypeServerList
        {
            get { return typeServerList; }
        }

        /// <summary>
        /// 방송국 리스트
        /// </summary>
        public List<stage> BroadList
        {
            get { return this.broadList; }
        }
        #endregion

        #region 데이터 관리 이벤트
        public delegate void DelegateWMessageUpdate(WMessage _wMessage);
        /// <summary>발령정보 업데이트 이벤트 </summary>
        public event DelegateWMessageUpdate onWMessageUpdate = null;

        public delegate void DelegateDeviceRecievedWMessage(DeviceResponse _dr);
        /// <summary>상태/제어 요청 응답 이벤트</summary>
        public event DelegateDeviceRecievedWMessage onDeviceRecievedWMessage = null;

        public delegate void DelegateWMessageFinished(WMessage _wmsg);
        /// <summary>재난 메시지 종료 이벤트</summary>
        public event DelegateWMessageFinished onWMessageFinished = null;

        public delegate void DelegateWMessageReceived(WMessage _wmsg);
        /// <summary>발령에 대한 응답 이벤트</summary>
        public event DelegateWMessageReceived OnWMessageReceived = null;

        public delegate void DelegateDSUseWMessageList(List<WMessage> _wmsgList);
        /// <summary>송출만 쓰는 이벤트 delegate</summary>
        public event DelegateDSUseWMessageList OnDSUseWMessageList = null;

        public delegate void DelegateWMessageTCPUpdate(WMessage _wMessage);
        /// <summary>20110616_수정된 발령정보 업데이트 이벤트 </summary>
        public event DelegateWMessageTCPUpdate onWMessageTCPUpdate = null;
        #endregion

        #region 연결, 연결해제, 기초데이터 생성 등
        /// <summary>
        /// 데이터관리 클래스 생성 메소드(정적 메소드)
        /// </summary>
        /// <returns>DataManageComponent 인스턴스 반환</returns>
        /// <remarks>
        /// 데이터관리 클래스는 싱글톤으로 실행된다.
        /// 싱글톤으로 실행되기 위해 우리는 이 정적 메소드로부터 객체를 생성해야 한다.
        /// </remarks>
        public static DataManager getInstance()
        {
            mutex.WaitOne();

            if (instance == null)
            {
                instance = new DataManager();
            }

            mutex.ReleaseMutex();

            return instance;
        }

        /// <summary>
        /// 기본 생성자
        /// </summary>
        private DataManager()
        {
            stage stageKbs = new stage();
            stageKbs.ID = 3;
            stageKbs.Name = "KBS";
            stageKbs.TKCategory = 1;
            this.broadList.Add(stageKbs);

            stage stageMbc = new stage();
            stageMbc.ID = 4;
            stageMbc.Name = "MBC";
            stageMbc.TKCategory = 1;
            this.broadList.Add(stageMbc);

            stage stageSbs = new stage();
            stageSbs.ID = 5;
            stageSbs.Name = "SBS";
            stageSbs.TKCategory = 1;
            this.broadList.Add(stageSbs);

            stage stageYtn = new stage();
            stageYtn.ID = 6;
            stageYtn.Name = "YTN";
            stageYtn.TKCategory = 1;
            this.broadList.Add(stageYtn);
        }

        /// <summary>
        /// Service Framework와 연결
        /// </summary>
        /// <remarks>
        /// Service Framework와 .NET Remoting 을 이용하여 연결하고 이벤트를 등록한다.</remarks>
        /// <returns></returns>
        public Boolean setConnect(String _hostIp, String _hostPort, string _clientName, uint _serverKind)
        {
            try
            {
                if (sfConnection != null)
                {
                    sfConnection.onDeviceRecievedWMessage -= new ADClient.onDeviceResponseHandler(sfConnection_onDeviceRecievedWMessage);
                    sfConnection.onWMessageFinish -= new ADClient.onWMessageFinishHandler(sfConnection_onWMessageFinish);
                    sfConnection.onWMessageListChanged -= new EventHandler<WMessageListChangedEventArgs>(sfConnection_onWMessageListChanged);
                    sfConnection.onOrderProcessWMessageRecieved -= new ADClient.onWMessageRecievedHandler(sfConnection_onOrderProcessWMessageRecieved);
                    sfConnection.onDSWMessageRecieved -= new ADClient.onWMessageRecievedHandler(sfConnection_onDSWMessageRecieved);

                    this.sfConnection.ReConnect(_hostIp, _hostPort, "DMB", "DataConnection", "DS");
                }
                else
                {
#if KBS
                    this.sfConnection = new ADClient(_hostIp, _hostPort, "DMB", "DataConnection", "DS");
#endif

#if NotKBS
                    this.sfConnection = new ADClient(_hostIp, _hostPort, "DMB", "DataConnection", true);
#endif
                }

                sfConnection.onDeviceRecievedWMessage += new ADClient.onDeviceResponseHandler(sfConnection_onDeviceRecievedWMessage);
                sfConnection.onWMessageFinish += new ADClient.onWMessageFinishHandler(sfConnection_onWMessageFinish);
                sfConnection.onWMessageListChanged += new EventHandler<WMessageListChangedEventArgs>(sfConnection_onWMessageListChanged);
                sfConnection.onOrderProcessWMessageRecieved += new ADClient.onWMessageRecievedHandler(sfConnection_onOrderProcessWMessageRecieved);
                sfConnection.onDSWMessageRecieved += new ADClient.onWMessageRecievedHandler(sfConnection_onDSWMessageRecieved);

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DataManager.setConnect - " + ex.Message);
                return true;
            }
        }

        /// <summary>
        /// 전체기초 데이터 생성
        /// </summary>
        /// <remarks>
        /// 지역형식 정보 리스트, 특수수신기 정보 리스트, 특수 수신기 상태 정보 리스트, 각 시스템 상태 정보 리스트
        /// 사용자 정보리스트, 특수수신기 그룹정보 리스트
        /// 들을 로드하여 기초 데이터를 생성한다.</remarks>
        /// <returns></returns>
        public Boolean setInitData()
        {
            try
            {
                if (sfConnection == null)
                {
                    return false;
                }

                this.typeRegionList = sfConnection.remoteobject.getTypeRegionList();
                this.typeDisasterList = sfConnection.remoteobject.getTypeDisasterList();
                this.deviceInfoList = sfConnection.remoteobject.getDeviceInfoList();
                this.userList = sfConnection.remoteobject.getUserInfoList();
                this.getpriorityList = sfConnection.remoteobject.getTypePriorityList();
                this.getTypedeviceList = sfConnection.remoteobject.getTypeDeviceList();
                this.typeServerList = sfConnection.remoteobject.getTypeServerList();
                this.serverList = sfConnection.remoteobject.getServerInfoList();
                this.broadList = sfConnection.remoteobject.getBCenterList();
                this.regionInfoList = sfConnection.remoteobject.getRegionInfoList();

                RegionInfoIDComparer tarComp = new RegionInfoIDComparer();
                this.regionInfoList.Sort(tarComp);

                IPAddress myip = null;
                IPHostEntry hostAdd = Dns.GetHostEntry(Dns.GetHostName());
                uint tmpServerID = 0;

                foreach (IPAddress ip in hostAdd.AddressList)
                {
                    myip = ip;
                    break;
                }

                for (int i = 0; i < this.serverList.Count; i++)
                {
                    if (this.serverList[i].RealIP.ToString() == myip.ToString().Trim())
                    {
                        if (this.serverList[i].Name == "DS")
                        {
                            tmpServerID = this.serverList[i].ID;
                        }
                    }
                }

                sfConnection.ServerID = tmpServerID;

                return true;
            }
            catch (Exception err)
            {
                Console.WriteLine("datamanager.setInitData {0}", err.Message);
                return false;
            }
        }
        #endregion

        #region 서비스 프레임워크 이벤트 처리
        void sfConnection_onWMessageTcpReceived(object sender, WMessageChangedEventArgs e)
        {
            //NSF 개발로 TCP로 직접 받는 이벤트 주석 처리_201307 by JYP
            //this.onWMessageUpdate(e.WMessageInformation);
        }

        /// <summary>
        /// 송출만 쓰는 발령업데이트 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void sfConnection_onWMessageListChanged(object sender, WMessageListChangedEventArgs e)
        {
            this.OnDSUseWMessageList(e.MessageList);
        }

        /// <summary>
        /// 재난경보 발령에 대한 내용 리턴 이벤트처리
        /// </summary>
        /// <param name="_obj"></param>
        /// <param name="_wea"></param>
        void sfConnection_onWMessageRecieved(object _obj, WMessageChangedEventArgs _wea)
        {
            //NSF 개발로 발령 이벤트 변경. TCP로 직접 받는 부분을 주석 처리하고 sfConnection_onWMessageRecieved 이벤트로 발령 처리함. 201307 by JYP
            //sfConnection_onDSWMessageRecieved 이벤트 추가로 이 이벤트는 제거했음. 20130813 by JYP
            this.onWMessageUpdate(_wea.WMessageInformation);
        }

        void sfConnection_onDSWMessageRecieved(object obj, WMessageChangedEventArgs wea)
        {
            //제어에 대한 패킷때문에 송출만 쓰는 이벤트 추가..이 이벤트로 발령과 제어가 모두 들어온다. 기존의 이벤트는 제거..다른 서버들은 사용함. 20130813 by JYP
            this.onWMessageUpdate(wea.WMessageInformation);
        }

        /// <summary>
        /// 발령전달 프로세스에서 받는 발령 이벤트
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="wea"></param>
        private void sfConnection_onOrderProcessWMessageRecieved(object obj, WMessageChangedEventArgs wea)
        {
            //발령 전달 중복 처리..발령 시간과 발령 모드로 구분함..아이디가 없으니 이 정보로 처리함..
            for (int i = 0; i < this.broadWmsgList.Count; i++)
            {
                if (this.broadWmsgList[i].DDateTime == wea.WMessageInformation.DDateTime)
                {
                    if (this.broadWmsgList[i].SendPart == wea.WMessageInformation.SendPart)
                    {
                        return;
                    }
                }
            }

            this.onWMessageUpdate(wea.WMessageInformation);
        }

        /// <summary>
        /// 상태/제어 요청에 대한 수신기 응답 이벤트 처리
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="deviceResponse"></param>
        void sfConnection_onDeviceRecievedWMessage(object obj, DeviceResponseEventArgs deviceResponse)
        {
            this.onDeviceRecievedWMessage(deviceResponse.DeviceResponseInfoData);
        }

        /// <summary>
        /// 재난 메시지 종료 이벤트 처리
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="wea"></param>
        void sfConnection_onWMessageFinish(object obj, WMessageFinishedEventArgs wea)
        {
            this.onWMessageFinished(wea.wMessageInformation);
        }
        #endregion

        #region remoteobject
        /// <summary>
        /// 발령 정보를 서비스 프레임워크로 전송하고, ID 를 가지는 발령 정보를 업데이트 한다.
        /// </summary>
        /// <param name="_wMessage">발령정보 클래스</param>
        /// <returns>ID 가 포함된 발령 데이터</returns>
        public WMessage sendWMessage(WMessage _wMessage)
        {
            WMessage item = new WMessage();

            try
            {
                if (sfConnection != null)
                {
                    item = this.sfConnection.remoteobject.setwMessage(_wMessage);

                    lock (searchWMessageList)
                    {
                        this.searchWMessageList.Add(item);
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("Datamanager.sendWMessage : {0}", err.Message);
            }

            return item;
        }

        /// <summary>
        /// 상태/제어 요청 후 받은 WMessageID를 넣은 DeviceControl 클래스 리스트를 저장한다.
        /// </summary>        
        public bool sendDeviceControl(List<DeviceControl> _deviceControl)
        {
            bool boolDC = new bool();

            try
            {
                if (sfConnection != null)
                {
                    boolDC = this.sfConnection.remoteobject.SetDeviceControlList(_deviceControl);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("DataMng.sendDeviceControl : {0}", e.Message);
            }

            return boolDC;
        }

        /// <summary>
        /// 발령 메시지가 종료되면 호출하는 메소드
        /// </summary>
        /// <param name="wmsg"></param>
        /// <returns></returns>
        public bool setWmessageFinish(WMessage wmsg, uint _broad)
        {
            try
            {
                bool b = false;

                if (sfConnection != null)
                {
                    b = this.sfConnection.remoteobject.setWMessageFinish(wmsg, _broad);

                    //if (b)
                    //{
                    //    lock (this.searchWMessageList)
                    //    {
                    //        this.searchWMessageList.Remove(wmsg);
                    //    }
                    //}
                }

                return b;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Datamanager.setWmessageFinish : {0}", ex.Message);

                return false;
            }
        }

        /// <summary>
        /// 재난경보 메시지를 송출에서 EWS로 송신한 후 전송 결과를 알려주는 메소드(특수수신기에만 유효)
        /// </summary>
        /// <param name="wmsg"></param>
        /// <returns></returns>
        public bool setWmessageSendID(WMessage wmsg, int sendid)
        {
            try
            {
                if (sfConnection != null)
                {
                    bool b = this.sfConnection.remoteobject.setWMessageSendID(wmsg, sendid);

                    return b;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Datamanager.setWmessageSendID : {0}", ex.Message);

                return false;
            }
        }

        /// <summary>
        /// 사용자 ID를 받아 사용자명을 반환
        /// </summary>
        /// <param name="_userID">사용자 ID</param>
        /// <returns></returns>
        public string GetUserName(string _userID)
        {
            try
            {
                foreach (User user in userList)
                {
                    if (user.UserID == _userID)
                    {
                        return user.Name;
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetDeviceKind - " + ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// 발령 리스트로 부터 해당 ID 의 Index를 반환한다.
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public int getWMessageIndex(uint _id)
        {
            try
            {
                WMessage srcComp = new WMessage();
                WMessageIDComparer tarComp = new WMessageIDComparer();
                srcComp.ID = _id;
                this.wMessageList.Sort(tarComp);

                int idx = this.wMessageList.BinarySearch(srcComp, tarComp);

                return idx;
            }
            catch (Exception e)
            {
                Console.WriteLine("getWMessageIndex : {0}", e.Message);
                return -1;
            }
        }

        /// <summary>
        /// 서버정보 리스트로 부터 해당 ID 의 Index를 반환한다.
        /// </summary>
        /// <param name="_id">검색을 ID</param>
        /// <returns>해당ID 의 Index</returns>
        public int getServerIndex(uint _id)
        {
            int idx = int.MinValue;

            try
            {
                ServerInfo srcComm = new ServerInfo();
                ServerInfoIDComparer tarComp = new ServerInfoIDComparer();
                srcComm.ID = _id;

                lock (this.serverList)
                {
                    this.serverList.Sort(tarComp);
                    idx = this.serverList.BinarySearch(srcComm, tarComp);
                }

                return idx;
            }
            catch (Exception e)
            {
                Console.WriteLine("DataManager.getServerIndex : {0}", e.Message);
                return -1;
            }
        }

        /// <summary>
        /// 재난종류 리스트로 부터 해당 ID 의 Index를 반환한다.
        /// </summary>
        /// <param name="_id">검색할 ID</param>
        /// <returns>해당ID 의 Index</returns>
        public int getTypeDisasterIndex(uint _id)
        {
            try
            {
                TypeDisaster srcComp = new TypeDisaster();
                TypeDisasterIDComparer tarComp = new TypeDisasterIDComparer();
                srcComp.ID = _id;

                this.typeDisasterList.Sort(tarComp);
                int idx = this.typeDisasterList.BinarySearch(srcComp, tarComp);

                return idx;
            }
            catch (Exception e)
            {
                Console.WriteLine("DataMng.getTypeDisasterIndex : {0}", e.Message);

                return -1;
            }
        }

        /// <summary>
        /// TypeRegion을 Id를 받아 Code값을 반환한다.
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public int getTypeRegionCode(uint _id)
        {
            int icode = -1;

            try
            {
                foreach (TypeRegion tr in TypeRegionList)
                {
                    if (tr.ID == _id)
                    {
                        icode = (int)tr.Code;
                        return icode;
                    }
                    else
                    {
                        icode = -1;
                    }
                }

                return icode;
            }
            catch (Exception e)
            {
                Console.WriteLine("DataMng.getTypeRegionCode : {0}", e.Message);

                return -1;
            }
        }

        /// <summary>
        /// 장비 리스트로 부터 해당 장비의 전화번호를 반환한다.
        /// </summary>
        /// <param name="_id">검색할 ID</param>
        /// <returns>해당ID 의 Index</returns>
        public string getMapMessageCell(uint _id)
        {
            try
            {
                DeviceInfo di = new DeviceInfo();
                DeviceInfoIDComparer comparer = new DeviceInfoIDComparer();
                string tmp = string.Empty;

                di.ID = _id;
                this.DeviceInfoList.Sort(comparer);
                int idx = this.DeviceInfoList.BinarySearch(di, comparer);

                if (idx >= 0)
                {
                    DeviceInfo d = DeviceInfoList[idx];
                    tmp = d.CellNumber.ToString();
                }
                else
                {
                    tmp = "9999999999";
                }

                return tmp;
            }
            catch (Exception e)
            {
                Console.WriteLine("DataMng.getMapMessageCell : {0}", e.Message);

                return string.Empty;
            }
        }

        /// <summary>
        /// 방송사 ID를 받아서 방송사 이름을 반환한다_Ver 1
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public string getBroadName(uint _id)
        {
            try
            {
                string tmpstr = string.Empty;

                foreach (stage st in this.broadList)
                {
                    if (st.ID == _id)
                    {
                        tmpstr = "<" + st.Name + ">";
                    }
                }

                return tmpstr;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DataMng.getBroadName : {0}", ex.Message);

                return string.Empty;
            }
        }

        /// <summary>
        /// 방송사 ID를 받아서 방송사 이름을 반환한다_Ver 2
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public string getBroadNameV2(uint _id)
        {
            try
            {
                string tmpstr = string.Empty;

                foreach (stage st in this.broadList)
                {
                    if (st.ID == _id)
                    {
                        tmpstr = ", " + st.Name;
                    }
                }

                return tmpstr;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DataMng.getBroadName : {0}", ex.Message);

                return string.Empty;
            }
        }
        #endregion
    }
}
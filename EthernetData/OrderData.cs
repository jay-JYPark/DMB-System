using System;
using System.Collections.Generic;
using System.Text;

namespace ADEng.Library.DMB
{
    /// <summary>
    /// 발령할 때 사용하는 클래스
    /// </summary>
    [Serializable]
    public class OrderData
    {
        #region 멤버
        private short msgLength = short.MinValue;
        private int msgId = int.MinValue;
        private byte version = byte.MinValue;
        private byte organ = byte.MinValue;
        private byte deviceKind = byte.MinValue;
        private string gps = string.Empty;
        private string rev = string.Empty;
        private string disaster = string.Empty;
        private byte priority = byte.MinValue;
        private DateTime dateTime = DateTime.MaxValue;
        private byte regionType = byte.MinValue;
        private byte rCount = byte.MinValue;
        private byte division = byte.MinValue;
        private byte[] region;
        private string message = string.Empty;
        private byte repeats = byte.MinValue;
        private byte interval = byte.MinValue;
        private byte mode = byte.MinValue;
        private uint cCount = uint.MinValue;
        private List<short> cLength = new List<short>();
        private List<byte> cDivision = new List<byte>();
        private List<short> cCmd = new List<short>();
        private List<string> cData = new List<string>();
        #endregion

        #region 접근
        /// <summary>
        /// 길이
        /// msgLength를 제외한 총 길이(Byte)
        /// </summary>
        public short MsgLength
        {
            get { return this.msgLength; }
            set { this.msgLength = value; }
        }

        /// <summary>
        /// 메시지 ID
        /// </summary>
        public int MsgId
        {
            get { return this.msgId; }
            set { this.msgId = value; }
        }

        /// <summary>
        /// 버전
        /// 수신기 종류 및 지역에 따라 구분하기 위함.
        /// </summary>
        public byte Version
        {
            get { return this.version; }
            set { this.version = value; }
        }

        /// <summary>
        /// 발령 기관
        /// 0x00 - 중앙, 0x01 - 시도, 0x02 - 시군, 0x03 - 읍면동, 0x10 - 민방위 및 기타 기관
        /// </summary>
        public byte Organ
        {
            get { return this.organ; }
            set { this.organ = value; }
        }

        /// <summary>
        /// 수신기 종류
        /// Rev	Rev	Rev	Rev	모니터링 마을 유관기관 상황실
        ///  1	 1	 1	 1	   1	   1      1	      1
        /// </summary>
        public byte DeviceKind
        {
            get { return this.deviceKind; }
            set { this.deviceKind = value; }
        }

        /// <summary>
        /// GPS 정보
        /// 차후에 적용.
        /// </summary>
        public string GPS
        {
            get { return this.gps; }
            set { this.gps = value; }
        }

        /// <summary>
        /// 예비
        /// </summary>
        public string REV
        {
            get { return this.rev; }
            set { this.rev = value; }
        }

        /// <summary>
        /// 재난 종류
        /// 재난 코드 표 참조.
        /// </summary>
        public string Disaster
        {
            get { return this.disaster; }
            set { this.disaster = value; }
        }

        /// <summary>
        /// 우선 순위
        /// </summary>
        public byte Priority
        {
            get { return this.priority; }
            set { this.priority = value; }
        }

        /// <summary>
        /// 발령 시간
        /// </summary>
        public DateTime DateTime
        {
            get { return this.dateTime; }
            set { this.dateTime = value; }
        }

        /// <summary>
        /// 지역 형식
        /// </summary>
        public byte RegionType
        {
            get { return this.regionType; }
            set { this.regionType = value; }
        }

        /// <summary>
        /// 지역 수
        /// </summary>
        public byte RCount
        {
            get { return this.rCount; }
            set { this.rCount = value; }
        }

        /// <summary>
        /// 발령 구분
        /// 1 - TTS, 2 - 저장메세지, 3 - 제어, 4 - 라이브발령
        /// </summary>
        public byte Division
        {
            get { return this.division; }
            set { this.division = value; }
        }

        /// <summary>
        /// 발령 지역
        /// </summary>
        public byte[] Region
        {
            get { return this.region; }
            set { this.region = value; }
        }

        /// <summary>
        /// 단문
        /// </summary>
        public string Message
        {
            get { return this.message; }
            set { this.message = value; }
        }

        /// <summary>
        /// 발령 횟수
        /// </summary>
        public byte Repeats
        {
            get { return this.repeats; }
            set { this.repeats = value; }
        }

        /// <summary>
        /// 발령 간격
        /// </summary>
        public byte Interval
        {
            get { return this.interval; }
            set { this.interval = value; }
        }

        /// <summary>
        /// 모드
        /// 11 - 실제, 01 - 훈련, 00 - 시험
        /// </summary>
        public byte Mode
        {
            get { return this.mode; }
            set { this.mode = value; }
        }

        /// <summary>
        /// 제어 발령 카운트
        /// </summary>
        public uint CCount
        {
            get { return this.cCount; }
            set { this.cCount = value; }
        }

        /// <summary>
        /// 제어 발령 길이
        /// </summary>
        public List<short> CLength
        {
            get { return this.cLength; }
            set { this.cLength = value; }
        }

        /// <summary>
        /// 제어 발령 구분
        /// 요청, 제어, 응답
        /// </summary>
        public List<byte> CDivision
        {
            get { return this.cDivision; }
            set { this.cDivision = value; }
        }

        /// <summary>
        /// 제어 발령 커맨드
        /// </summary>
        public List<short> CCmd
        {
            get { return this.cCmd; }
            set { this.cCmd = value; }
        }

        /// <summary>
        /// 제어 발령 데이터
        /// </summary>
        public List<string> CData
        {
            get { return this.cData; }
            set { this.cData = value; }
        }
        #endregion

        /// <summary>
        /// 기본생성자
        /// </summary>
        public OrderData()
        {
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="_msgLength">
        /// 길이, msgLength를 제외한 총 길이(Byte)
        /// </param>
        /// <param name="_msgId">
        /// 메시지 ID
        /// </param>
        /// <param name="_version">
        /// 버전, 수신기 종류 및 지역에 따라 구분하기 위함.
        /// </param>
        /// <param name="_organ">
        /// 발령 기관, 0x00 - 중앙, 0x01 - 시도, 0x02 - 시군, 0x03 - 읍면동, 0x10 - 민방위 및 기타 기관
        /// </param>
        /// <param name="_deviceKind">
        /// 수신기 종류
        /// Rev	Rev	Rev	Rev	모니터링 마을 유관기관 상황실
        ///  1	 1	 1	 1	   1	   1      1	      1
        /// </param>
        /// <param name="_gps">
        /// GPS 정보, 차후에 적용.
        /// </param>
        /// <param name="_rev">
        /// 예비
        /// </param>
        /// <param name="_disaster">
        /// 재난 종류, 재난 코드 표 참조.
        /// </param>
        /// <param name="_priority">
        /// 우선 순위
        /// </param>
        /// <param name="_dateTime">
        /// 발령 시간
        /// </param>
        /// <param name="_regionType">
        /// 지역 형식
        /// </param>
        /// <param name="_rCount">
        /// 지역 수
        /// </param>
        /// <param name="_division">
        /// 발령 구분, 1 - TTS, 2 - 저장메세지, 3 - 제어
        /// </param>
        /// <param name="_region">
        /// 발령 지역
        /// </param>
        /// <param name="_message">
        /// 단문
        /// </param>
        /// <param name="_repeats">
        /// 발령 횟수
        /// </param>
        /// <param name="_interval">
        /// 발령 간격
        /// </param>
        /// <param name="_mode">
        /// 모드, 11 - 실제, 01 - 훈련, 00 - 시험
        /// </param>
        /// <param name="_cCount">
        /// 제어 발령 카운트
        /// </param>
        /// <param name="_cLength">
        /// 제어 발령 길이
        /// </param>
        /// <param name="_cDivision">
        /// 제어 발령 구분
        /// 요청, 제어, 응답
        /// </param>
        /// <param name="_cCmd">
        /// 제어 발령 커맨드
        /// </param>
        /// <param name="_cData">
        /// 제어 발령 데이터
        /// </param>
        public OrderData
            (
            short _msgLength,
            int _msgId,
            byte _version,
            byte _organ,
            byte _deviceKind,
            string _gps,
            string _rev,
            string _disaster,
            byte _priority,
            DateTime _dateTime,
            byte _regionType,
            byte _rCount,
            byte _division,
            byte[] _region,
            string _message,
            byte _repeats,
            byte _interval,
            byte _mode,
            uint _cCount,
            List<short> _cLength,
            List<byte> _cDivision,
            List<short> _cCmd,
            List<string> _cData
            )
        {
            this.msgLength = _msgLength;
            this.msgId = _msgId;
            this.version = _version;
            this.organ = _organ;
            this.deviceKind = _deviceKind;
            this.gps = _gps;
            this.rev = _rev;
            this.disaster = _disaster;
            this.priority = _priority;
            this.dateTime = _dateTime;
            this.regionType = _regionType;
            this.rCount = _rCount;
            this.division = _division;
            this.region = _region;
            this.message = _message;
            this.repeats = _repeats;
            this.interval = _interval;
            this.mode = _mode;
            this.cCount = _cCount;
            this.cLength = _cLength;
            this.cDivision = _cDivision;
            this.cCmd = _cCmd;
            this.cData = _cData;
        }

        public static byte[] GetToByte(OrderData _data)
        {
            byte[] tot = null;

            try
            {
                
                byte[] soh = new byte[] { 0x01, 0x02 };
                byte[] len = new byte[] { 0x30, 0x30, 0x30 };
                byte cmd = (byte)'O';                
                byte[] b1 = Encoding.Default.GetBytes(string.Format("{0}", _data.MsgLength));
                byte[] b2 = Encoding.Default.GetBytes(string.Format("/{0}", _data.MsgId));
                byte[] b3 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Version));
                byte[] b4 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Organ));
                byte[] b5 = Encoding.Default.GetBytes(string.Format("/{0}", _data.DeviceKind));
                byte[] b6 = Encoding.Default.GetBytes(string.Format("/{0}", _data.GPS));
                byte[] b7 = Encoding.Default.GetBytes(string.Format("/{0}", _data.REV));
                byte[] b8 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Disaster));
                byte[] b9 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Priority));
                byte[] b10 = Encoding.Default.GetBytes(string.Format("/{0}", _data.DateTime.ToString("yyyy-MM-dd HH:mm:ss")));
                byte[] b11 = Encoding.Default.GetBytes(string.Format("/{0}", _data.RegionType));
                byte[] b12 = Encoding.Default.GetBytes(string.Format("/{0}", _data.RCount));
                byte[] b13 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Division));
                byte[] b14 = Encoding.Default.GetBytes(string.Format("/{0}", Encoding.Default.GetString(_data.Region)));
                byte[] b15 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Message));
                byte[] b16 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Repeats));
                byte[] b17 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Interval));
                byte[] b18 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Mode));
                byte[] b19 = Encoding.Default.GetBytes(string.Format("/{0}", _data.CCount));                                           // 제어 시 상위에서 제어 수 만큼 제어발령을 전송하며, 수신시 항상 제어 카운트는 1로 고정함
                byte[] eoh = new byte[] { 0x03, 0x04 };

                int dataLen = b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length + b14.Length + b15.Length + b16.Length + b17.Length
                    + b18.Length + b19.Length;
                              
                //2013/04/03 HSW(전체 메세지 Length를 기존 3바이트 아스키를 3바이트 Int값으로 변경)(TTS문안이 길어지면 전체 Length가 999를 넘을수가 있기 때문에)
                //len = Encoding.Default.GetBytes(string.Format("{0:000}", dataLen));
                len[2] = (byte)((dataLen >> 16) % 256);
                len[1] = (byte)((dataLen >> 8) % 256);
                len[0] = (byte)(dataLen % 256);

                tot = new byte[8 + dataLen];

                Buffer.BlockCopy(soh, 0, tot, 0, soh.Length);
                Buffer.BlockCopy(len, 0, tot, soh.Length, len.Length);
                tot[soh.Length + len.Length] = cmd;
                Buffer.BlockCopy(b1, 0, tot, soh.Length + len.Length + 1, b1.Length);
                Buffer.BlockCopy(b2, 0, tot, soh.Length + len.Length + 1 + b1.Length, b2.Length);
                Buffer.BlockCopy(b3, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length, b3.Length);
                Buffer.BlockCopy(b4, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length, b4.Length);
                Buffer.BlockCopy(b5, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length, b5.Length);
                Buffer.BlockCopy(b6, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length, b6.Length);
                Buffer.BlockCopy(b7, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length, b7.Length);
                Buffer.BlockCopy(b8, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length, b8.Length);
                Buffer.BlockCopy(b9, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length, b9.Length);
                Buffer.BlockCopy(b10, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length, b10.Length);
                Buffer.BlockCopy(b11, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length, b11.Length);
                Buffer.BlockCopy(b12, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length, b12.Length);
                Buffer.BlockCopy(b13, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length, b13.Length);
                Buffer.BlockCopy(b14, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length, b14.Length);
                Buffer.BlockCopy(b15, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length + b14.Length, b15.Length);
                Buffer.BlockCopy(b16, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length + b14.Length + b15.Length, b16.Length);
                Buffer.BlockCopy(b17, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length + b14.Length + b15.Length + b16.Length, b17.Length);
                Buffer.BlockCopy(b18, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length + b14.Length + b15.Length + b16.Length + b17.Length, b18.Length);
                Buffer.BlockCopy(b19, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length + b14.Length + b15.Length + b16.Length + b17.Length + b18.Length, b19.Length);
                Buffer.BlockCopy(eoh, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length + b14.Length + b15.Length + b16.Length + b17.Length + b18.Length + b19.Length, eoh.Length);
            }
            catch
            {
                Console.Write(string.Format("\n[{0}] OrderData GetToByte Exception", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            }

            return tot;
        }

        //public static List<byte[]> GetToByte(OrderData _data)
        //{
        //    int cnt = 1;
        //    List<byte[]> arr = new List<byte[]>();

        //    try
        //    {
        //        if (_data.Division == 3)
        //        {
        //            cnt = (int)_data.CCount;
        //        }

        //        for (int i = 0; i < cnt; i++)
        //        {
        //            byte[] tot = null; 

        //            byte[] soh = new byte[] { 0x01, 0x02 };
        //            byte[] len = new byte[] { 0x30, 0x30, 0x30 };
        //            byte cmd = (byte)'O';
        //            byte[] b1 = Encoding.Default.GetBytes(string.Format("{0}", _data.MsgLength));
        //            byte[] b2 = Encoding.Default.GetBytes(string.Format("/{0}", _data.MsgId));
        //            byte[] b3 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Version));
        //            byte[] b4 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Organ));
        //            byte[] b5 = Encoding.Default.GetBytes(string.Format("/{0}", _data.DeviceKind));
        //            byte[] b6 = Encoding.Default.GetBytes(string.Format("/{0}", _data.GPS));
        //            byte[] b7 = Encoding.Default.GetBytes(string.Format("/{0}", _data.REV));
        //            byte[] b8 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Disaster));
        //            byte[] b9 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Priority));
        //            byte[] b10 = Encoding.Default.GetBytes(string.Format("/{0}", _data.DateTime.ToString("yyyy-MM-dd HH:mm:ss")));
        //            byte[] b11 = Encoding.Default.GetBytes(string.Format("/{0}", _data.RegionType));
        //            byte[] b12 = Encoding.Default.GetBytes(string.Format("/{0}", _data.RCount));
        //            byte[] b13 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Division));
        //            byte[] b14 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Region));
        //            byte[] b15 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Message));
        //            byte[] b16 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Repeats));
        //            byte[] b17 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Interval));
        //            byte[] b18 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Mode));
        //            byte[] b19 = Encoding.Default.GetBytes(string.Format("/{0}", _data.CCount));                                           // 제어 시 상위에서 제어 수 만큼 제어발령을 전송하며, 수신시 항상 제어 카운트는 1로 고정함
        //            byte[] b20 = null;
        //            byte[] b21 = null;
        //            byte[] b22 = null;
        //            byte[] b23 = null;
        //            if (_data.CCount > 0)
        //            {
        //                b20 = Encoding.Default.GetBytes(string.Format("/{0}", _data.CLength[i]));
        //                b21 = Encoding.Default.GetBytes(string.Format("/{0}", _data.CDivision[i]));
        //                b22 = Encoding.Default.GetBytes(string.Format("/{0}", _data.CCmd[i]));
        //                b23 = Encoding.Default.GetBytes(string.Format("/{0}", _data.CData[i]));
        //            }
        //            else
        //            {
        //                b20 = Encoding.Default.GetBytes("/0");
        //                b21 = Encoding.Default.GetBytes("/0");
        //                b22 = Encoding.Default.GetBytes("/0");
        //                b23 = Encoding.Default.GetBytes("/0");
        //            }
        //            byte[] eoh = new byte[] { 0x03, 0x04 };

        //            int dataLen = b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length + b14.Length + b15.Length + b16.Length + b17.Length
        //                + b18.Length + b19.Length + b20.Length + b21.Length + b22.Length + b23.Length;

        //            len = Encoding.Default.GetBytes(string.Format("{0:000}", dataLen));

        //            tot = new byte[8 + dataLen];

        //            Buffer.BlockCopy(soh, 0, tot, 0, soh.Length);
        //            Buffer.BlockCopy(len, 0, tot, soh.Length, len.Length);
        //            tot[soh.Length + len.Length] = cmd;
        //            Buffer.BlockCopy(b1, 0, tot, soh.Length + len.Length + 1, b1.Length);
        //            Buffer.BlockCopy(b2, 0, tot, soh.Length + len.Length + 1 + b1.Length, b2.Length);
        //            Buffer.BlockCopy(b3, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length, b3.Length);
        //            Buffer.BlockCopy(b4, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length, b4.Length);
        //            Buffer.BlockCopy(b5, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length, b5.Length);
        //            Buffer.BlockCopy(b6, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length, b6.Length);
        //            Buffer.BlockCopy(b7, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length, b7.Length);
        //            Buffer.BlockCopy(b8, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length, b8.Length);
        //            Buffer.BlockCopy(b9, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length, b9.Length);
        //            Buffer.BlockCopy(b10, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length, b10.Length);
        //            Buffer.BlockCopy(b11, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length, b11.Length);
        //            Buffer.BlockCopy(b12, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length, b12.Length);
        //            Buffer.BlockCopy(b13, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length, b13.Length);
        //            Buffer.BlockCopy(b14, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length, b14.Length);
        //            Buffer.BlockCopy(b15, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length + b14.Length, b15.Length);
        //            Buffer.BlockCopy(b16, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length + b14.Length + b15.Length, b16.Length);
        //            Buffer.BlockCopy(b17, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length + b14.Length + b15.Length + b16.Length, b17.Length);
        //            Buffer.BlockCopy(b18, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length + b14.Length + b15.Length + b16.Length + b17.Length, b18.Length);
        //            Buffer.BlockCopy(b19, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length + b14.Length + b15.Length + b16.Length + b17.Length + b18.Length, b19.Length);
        //            Buffer.BlockCopy(b20, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length + b14.Length + b15.Length + b16.Length + b17.Length + b18.Length + b19.Length, b20.Length);
        //            Buffer.BlockCopy(b21, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length + b14.Length + b15.Length + b16.Length + b17.Length + b18.Length + b19.Length + b20.Length, b21.Length);
        //            Buffer.BlockCopy(b22, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length + b14.Length + b15.Length + b16.Length + b17.Length + b18.Length + b19.Length + b20.Length + b21.Length, b22.Length);
        //            Buffer.BlockCopy(b23, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length + b14.Length + b15.Length + b16.Length + b17.Length + b18.Length + b19.Length + b20.Length + b21.Length + b22.Length, b23.Length);
        //            Buffer.BlockCopy(eoh, 0, tot, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length + b14.Length + b15.Length + b16.Length + b17.Length + b18.Length + b19.Length + b20.Length + b21.Length + b22.Length + b23.Length, eoh.Length);

        //            arr.Add(tot);
        //        }
        //    }
        //    catch
        //    {
        //        Console.Write(string.Format("\n[{0}] OrderData GetToByte Exception", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
        //        arr = null;
        //    }

        //    return arr;
        //}

        public static OrderData SetFromByte(byte[] _arr)
        {
            OrderData rst = new OrderData();
            string[] cmsg = null;

            try
            {
                string dataBady = Encoding.Default.GetString(_arr, 6, _arr.Length - 8);
                string[] datas = dataBady.Split('/');

                int i = 0;

                rst.MsgLength = short.Parse(datas[i++]);
                rst.MsgId = int.Parse(datas[i++]);
                rst.Version = byte.Parse(datas[i++]);
                rst.Organ = byte.Parse(datas[i++]);
                rst.DeviceKind = byte.Parse(datas[i++]);
                rst.gps = datas[i++];
                rst.REV = datas[i++];
                rst.Disaster = datas[i++];
                rst.Priority = byte.Parse(datas[i++]);
                rst.DateTime = DateTime.Parse(datas[i++]);
                rst.RegionType = byte.Parse(datas[i++]);
                rst.RCount = byte.Parse(datas[i++]);
                rst.Division = byte.Parse(datas[i++]);
                rst.Region = Encoding.Default.GetBytes(datas[i++]);
                rst.Message = datas[i++];
                rst.Repeats = byte.Parse(datas[i++]);
                rst.Interval = byte.Parse(datas[i++]);
                rst.Mode = byte.Parse(datas[i++]);
                rst.CCount = uint.Parse(datas[i++]);

                if (rst.CCount > 0)
                {
                    cmsg = rst.Message.Split('Y');
                }

                for (int t = 0; t < rst.CCount; t++)
                {
                    short len = short.Parse(cmsg[t].Substring(1, 2));
                    rst.CLength.Add(len);
                    rst.CDivision.Add(byte.Parse(cmsg[t].Substring(3, 1)));
                    rst.CCmd.Add(short.Parse(cmsg[t].Substring(4, 2)));
                    rst.CData.Add(cmsg[t].Substring(6, cmsg[t].Length - 14));
                }
            }
            catch
            {
                Console.Write(string.Format("\n[{0}] OrderData SetFromByte Exception", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                rst = null;
            }

            return rst;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace ADEng.Library.DMB
{
    /// <summary>
    /// 발령에 대한 응답할 때 사용하는 클래스
    /// </summary>
    [Serializable]
    public class ResponseData
    {
        /// <summary>
        /// 스피커 상태를 나타내는 enum
        /// </summary>
        [Flags]
        public enum E_SpkState
        {
            None = 0,
            SPK1 = 1 << 0,
            SPK2 = 1 << 1,
            SPK3 = 1 << 2,
            SPK4 = 1 << 3,
            SPK5 = 1 << 4,
            SPK6 = 1 << 5,
            SPK7 = 1 << 6,
            SPK8 = 1 << 7,
            ALL = 255
        }

        #region 멤버
        private DateTime recDateTime = DateTime.MaxValue;
        private uint organi = uint.MinValue;
        private uint pkid = uint.MinValue;
        private uint respResult = uint.MinValue;
        private E_SpkState spkStatus = E_SpkState.None;
        private string disaster = string.Empty;
        private string deviceVer = string.Empty;
        private string region = string.Empty;
        private byte swcStatus = byte.MinValue;
        private byte rcvMedia = byte.MinValue;
        #endregion

        #region 접근
        /// <summary>
        /// 응답한 시간
        /// </summary>
        public DateTime RecDateTime
        {
            get { return recDateTime; }
            set { recDateTime = value; }
        }

        /// <summary>
        /// 발령 기관
        /// </summary>
        public uint Organi
        {
            get { return organi; }
            set { organi = value; }
        }

        /// <summary>
        /// 메시지 ID
        /// </summary>
        public uint PKID
        {
            get { return pkid; }
            set { pkid = value; }
        }

        /// <summary>
        /// 발령에 대한 응답 결과
        /// 1 : 정상, 0 : 오류
        /// </summary>
        public uint RespResult
        {
            get { return respResult; }
            set { respResult = value; }
        }

        #region 스피커 상태
        /// <summary>
        /// 스피커 방송 상태
        /// </summary>
        public E_SpkState SPK_Status
        {
            get { return spkStatus; }
            set { spkStatus = value; }
        }

        /// <summary>
        /// 스피커 상태를 Y, N 으로 반환
        /// </summary>
        public string imgSPKStatus
        {
            get
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                (spkStatus & E_SpkState.SPK1) == E_SpkState.SPK1 ? "Y" : "N",
                (spkStatus & E_SpkState.SPK2) == E_SpkState.SPK2 ? "Y" : "N",
                (spkStatus & E_SpkState.SPK3) == E_SpkState.SPK3 ? "Y" : "N",
                (spkStatus & E_SpkState.SPK4) == E_SpkState.SPK4 ? "Y" : "N",
                (spkStatus & E_SpkState.SPK5) == E_SpkState.SPK5 ? "Y" : "N",
                (spkStatus & E_SpkState.SPK6) == E_SpkState.SPK6 ? "Y" : "N",
                (spkStatus & E_SpkState.SPK7) == E_SpkState.SPK7 ? "Y" : "N",
                (spkStatus & E_SpkState.SPK8) == E_SpkState.SPK8 ? "Y" : "N");
            }
        }

        /// <summary>
        /// 1번 스피커 상태
        /// </summary>
        public bool SPK1_Status
        {
            get { return getSPKStatus(E_SpkState.SPK1); }
            set { setSPKStatus(E_SpkState.SPK1, value); }
        }

        /// <summary>
        /// 2번 스피커 상태
        /// </summary>
        public bool SPK2_Status
        {
            get { return getSPKStatus(E_SpkState.SPK2); }
            set { setSPKStatus(E_SpkState.SPK2, value); }
        }

        /// <summary>
        /// 3번 스피커 상태
        /// </summary>
        public bool SPK3_Status
        {
            get { return getSPKStatus(E_SpkState.SPK3); }
            set { setSPKStatus(E_SpkState.SPK3, value); }
        }

        /// <summary>
        /// 4번 스피커 상태
        /// </summary>
        public bool SPK4_Status
        {
            get { return getSPKStatus(E_SpkState.SPK4); }
            set { setSPKStatus(E_SpkState.SPK4, value); }
        }

        /// <summary>
        /// 5번 스피커 상태
        /// </summary>
        public bool SPK5_Status
        {
            get { return getSPKStatus(E_SpkState.SPK5); }
            set { setSPKStatus(E_SpkState.SPK5, value); }
        }

        /// <summary>
        /// 6번 스피커 상태
        /// </summary>
        public bool SPK6_Status
        {
            get { return getSPKStatus(E_SpkState.SPK6); }
            set { setSPKStatus(E_SpkState.SPK6, value); }
        }

        /// <summary>
        /// 7번 스피커 상태
        /// </summary>
        public bool SPK7_Status
        {
            get { return getSPKStatus(E_SpkState.SPK7); }
            set { setSPKStatus(E_SpkState.SPK7, value); }
        }

        /// <summary>
        /// 8번 스피커 상태
        /// </summary>
        public bool SPK8_Status
        {
            get { return getSPKStatus(E_SpkState.SPK8); }
            set { setSPKStatus(E_SpkState.SPK8, value); }
        }
        #endregion

        /// <summary>
        /// 재난 종류
        /// </summary>
        public string Disaster
        {
            get { return disaster; }
            set { disaster = value; }
        }

        /// <summary>
        /// 장비 버전
        /// </summary>
        public string DeviceVer
        {
            get { return deviceVer; }
            set { deviceVer = value; }
        }

        /// <summary>
        /// 행정동 코드
        /// </summary>
        public string Region
        {
            get { return region; }
            set { region = value; }
        }

        /// <summary>
        /// 수신매체
        /// 1 : DMB, 2 : TCP
        /// </summary>
        public byte RcvMedia
        {
            get { return rcvMedia; }
            set { rcvMedia = value; }
        }

        /// <summary>
        /// 차단스위치 상태
        /// 1 : 정상, 0 : 오류
        /// </summary>
        public byte SwcStatus
        {
            get { return swcStatus; }
            set { swcStatus = value; }
        }
        #endregion
       
        private bool getSPKStatus(E_SpkState _st)
        {
            return (spkStatus & _st) != 0;
        }

        private void setSPKStatus(E_SpkState _st, bool _value)
        {
            if (_value)
            {
                spkStatus |= _st;
            }
            else
            {
                spkStatus &= ~_st;
            }
        }

        /// <summary>
        /// 기본생성자
        /// </summary>
        public ResponseData()
        {
        }

        /// <summary>
        /// 기본생성자
        /// </summary>
        /// <param name="_recDateTime">
        /// 응답한 시간
        /// </param>
        /// <param name="_organi">
        /// 발령 기관
        /// </param>
        /// <param name="_pkid">
        /// 메시지 ID
        /// </param>
        /// <param name="_respResult">
        /// 발령에 대한 응답 결과
        /// 1 : 정상, 0 : 오류
        /// </param>
        /// <param name="_spkStatus">
        /// 스피커 방송 상태
        /// </param>
        /// <param name="_disaster">
        /// 재난 종류
        /// </param>
        /// <param name="_deviceVer">
        /// 장비 버전
        /// </param>
        /// <param name="_region">
        /// 행정동 코드
        /// </param>
        /// <param name="_swcStatus">
        /// 차단스위치 상태
        /// 1 : 정상, 0 : 오류
        /// </param>
        /// <param name="_rcvMedia">
        /// 1 : DMB, 0 : TCP
        /// </param>
        public ResponseData(
            DateTime _recDateTime,
            uint _organi,
            uint _pkid,
            uint _respResult,
            E_SpkState _spkStatus,
            string _disaster,
            string _deviceVer,
            string _region,
            byte _swcStatus,
            byte _rcvMedia)
        {
            this.recDateTime = _recDateTime;
            this.organi = _organi;
            this.pkid = _pkid;
            this.respResult = _respResult;
            this.spkStatus = _spkStatus;
            this.disaster = _disaster;
            this.deviceVer = _deviceVer;
            this.region = _region;
            this.swcStatus = _swcStatus;
            this.rcvMedia = _rcvMedia;
        }

        public static byte[] GetToByte(ResponseData _data)
        {
            byte[] arr = null;

            try
            {
                byte[] soh = new byte[] { 0x01, 0x02 };
                byte[] len = new byte[] { 0x30, 0x30, 0x30 };
                byte cmd = (byte)'R';
                byte[] b1 = Encoding.Default.GetBytes(_data.RecDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                byte[] b2 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Organi));
                byte[] b3 = Encoding.Default.GetBytes(string.Format("/{0}", _data.PKID));
                byte[] b4 = Encoding.Default.GetBytes(string.Format("/{0}", _data.RespResult));
                byte[] b5 = Encoding.Default.GetBytes(string.Format("/{0}", (_data.SPK1_Status) ? "1" : "0"));
                byte[] b6 = Encoding.Default.GetBytes(string.Format("/{0}", (_data.SPK2_Status) ? "1" : "0"));
                byte[] b7 = Encoding.Default.GetBytes(string.Format("/{0}", (_data.SPK3_Status) ? "1" : "0"));
                byte[] b8 = Encoding.Default.GetBytes(string.Format("/{0}", (_data.SPK4_Status) ? "1" : "0"));
                byte[] b9 = Encoding.Default.GetBytes(string.Format("/{0}", (_data.SPK5_Status) ? "1" : "0"));
                byte[] b10 = Encoding.Default.GetBytes(string.Format("/{0}", (_data.SPK6_Status) ? "1" : "0"));
                byte[] b11 = Encoding.Default.GetBytes(string.Format("/{0}", (_data.SPK7_Status) ? "1" : "0"));
                byte[] b12 = Encoding.Default.GetBytes(string.Format("/{0}", (_data.SPK8_Status) ? "1" : "0"));
                byte[] b13 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Disaster));
                byte[] b14 = Encoding.Default.GetBytes(string.Format("/{0}", _data.DeviceVer));
                byte[] b15 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Region));
                byte[] b16 = Encoding.Default.GetBytes(string.Format("/{0}", _data.SwcStatus));
                byte[] b17 = Encoding.Default.GetBytes(string.Format("/{0}", _data.RcvMedia));
                byte[] eoh = new byte[] { 0x03, 0x04 };

                int dataLen = b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length + b14.Length + b15.Length + b16.Length + b17.Length;

                len = Encoding.Default.GetBytes(string.Format("{0:000}", dataLen));

                arr = new byte[8 + dataLen];

                Buffer.BlockCopy(soh, 0, arr, 0, soh.Length);
                Buffer.BlockCopy(len, 0, arr, soh.Length, len.Length);
                arr[soh.Length + len.Length] = cmd;
                Buffer.BlockCopy(b1, 0, arr, soh.Length + len.Length + 1, b1.Length);
                Buffer.BlockCopy(b2, 0, arr, soh.Length + len.Length + 1 + b1.Length, b2.Length);
                Buffer.BlockCopy(b3, 0, arr, soh.Length + len.Length + 1 + b1.Length + b2.Length, b3.Length);
                Buffer.BlockCopy(b4, 0, arr, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length, b4.Length);
                Buffer.BlockCopy(b5, 0, arr, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length, b5.Length);
                Buffer.BlockCopy(b6, 0, arr, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length, b6.Length);
                Buffer.BlockCopy(b7, 0, arr, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length, b7.Length);
                Buffer.BlockCopy(b8, 0, arr, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length, b8.Length);
                Buffer.BlockCopy(b9, 0, arr, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length, b9.Length);
                Buffer.BlockCopy(b10, 0, arr, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length, b10.Length);
                Buffer.BlockCopy(b11, 0, arr, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length, b11.Length);
                Buffer.BlockCopy(b12, 0, arr, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length, b12.Length);
                Buffer.BlockCopy(b13, 0, arr, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length, b13.Length);
                Buffer.BlockCopy(b14, 0, arr, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length, b14.Length);
                Buffer.BlockCopy(b15, 0, arr, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length + b14.Length, b15.Length);
                Buffer.BlockCopy(b16, 0, arr, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length + b14.Length + b15.Length, b16.Length);
                Buffer.BlockCopy(b17, 0, arr, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length + b14.Length + b15.Length + b16.Length, b17.Length);
                Buffer.BlockCopy(eoh, 0, arr, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length + b14.Length + b15.Length + b16.Length + b17.Length, eoh.Length);
            }
            catch(Exception ex)
            {
                Console.Write(string.Format("\n[{0}] ResponseData GetToByte Exception", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                arr = null;
            }

            return arr;
        }

        public static ResponseData SetFromByte(byte[] _arr)
        {
            ResponseData rst = new ResponseData();

            try
            {
                string dataBady = Encoding.Default.GetString(_arr, 6, _arr.Length - 8);
                string[] datas = dataBady.Split('/');

                int i = 0;

                rst.RecDateTime = DateTime.Parse(datas[i++]);
                rst.Organi = uint.Parse(datas[i++]);
                rst.PKID = uint.Parse(datas[i++]);
                rst.RespResult = uint.Parse(datas[i++]);
                rst.SPK1_Status = (datas[i++] == "1") ? true : false;
                rst.SPK2_Status = (datas[i++] == "1") ? true : false;
                rst.SPK3_Status = (datas[i++] == "1") ? true : false;
                rst.SPK4_Status = (datas[i++] == "1") ? true : false;
                rst.SPK5_Status = (datas[i++] == "1") ? true : false;
                rst.SPK6_Status = (datas[i++] == "1") ? true : false;
                rst.SPK7_Status = (datas[i++] == "1") ? true : false;
                rst.SPK8_Status = (datas[i++] == "1") ? true : false;
                rst.Disaster = datas[i++];
                rst.DeviceVer = datas[i++];
                rst.Region = datas[i++];
                rst.SwcStatus = byte.Parse(datas[i++]);
                rst.RcvMedia = byte.Parse(datas[i]);
            }
            catch
            {
                Console.Write(string.Format("\n[{0}] ResponseData SetFromByte Exception", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                rst = null;
            }

            return rst;
        }
    }
}
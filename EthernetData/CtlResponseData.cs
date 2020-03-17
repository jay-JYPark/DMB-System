using System;
using System.Collections.Generic;
using System.Text;

namespace ADEng.Library.DMB
{
    /// <summary>
    /// 장비 제어, 상태 요청에 대한 응답할 때 사용하는 클래스
    /// </summary>
    [Serializable]
    public class CtlResponseData
    {
        #region 멤버
        private byte psu1Ac = 9;
        private byte psu1Dc = 9;
        private byte psu1Fan = 9;
        private byte psu2Ac = 9;
        private byte psu2Dc = 9;
        private byte psu2Fan = 9;
        private byte psu2Use = 9;
        private byte systemDown = 9;
        private byte battUse = 9;
        private byte battState = 9;
        private byte door = 9;
        private byte reset = 9;
        private string fwVer = "####";
        private string fwDate = "########";
        private byte ewsCh = 9;
        private byte tdmbLv = 9;
        private byte sdmbLv = 9;
        private byte cdmaLv = 9;
        private byte tdmbState = 9;
        private byte sdmbState = 9;
        private byte ampSw = 9;
        private byte ampUse = 9;
        private string ampChUse = "########";
        private byte ampLv = 9;
        private string acVolt = "#####";
        private string dc5Volt = "###";
        private string dc24Volt = "####";
        private string tempo = "#####";
        private byte exAmp = 9;
        #endregion

        #region 접근
        /// <summary>
        /// PSU1 AC-FAIL
        /// 정상 = 1, 불량 = 0, 항목사용안함 = 9
        /// </summary>
        public byte Psu1Ac
        {
            get { return psu1Ac; }
            set { psu1Ac = value; }
        }

        /// <summary>
        /// PSU1 DC-FAIL
        /// 정상 = 1, 불량 = 0, 항목사용안함 = 9
        /// </summary>
        public byte Psu1Dc
        {
            get { return psu1Dc; }
            set { psu1Dc = value; }
        }

        /// <summary>
        /// PSU1 FAN-FAIL
        /// 정상 = 1, 불량 = 0, 항목사용안함 = 9
        /// </summary>
        public byte Psu1Fan
        {
            get { return psu1Fan; }
            set { psu1Fan = value; }
        }

        /// <summary>
        /// PSU2 AC-FAIL
        /// 정상 = 1, 불량 = 0, 항목사용안함 = 9
        /// </summary>
        public byte Psu2Ac
        {
            get { return psu2Ac; }
            set { psu2Ac = value; }
        }

        /// <summary>
        /// PSU2 DC-FAIL
        /// 정상 = 1, 불량 = 0, 항목사용안함 = 9
        /// </summary>
        public byte Psu2Dc
        {
            get { return psu2Dc; }
            set { psu2Dc = value; }
        }

        /// <summary>
        /// PSU2 FAN-FAIL
        /// 정상 = 1, 불량 = 0, 항목사용안함 = 9
        /// </summary>
        public byte Psu2Fan
        {
            get { return psu2Fan; }
            set { psu2Fan = value; }
        }

        /// <summary>
        /// PSU2 사용여부
        /// 사용 = 1, 사용안함 = 0, 항목사용안함 = 9
        /// </summary>
        public byte Psu2Use
        {
            get { return psu2Use; }
            set { psu2Use = value; }
        }

        /// <summary>
        /// 배터리 과방전, 시스템 다운 예고
        /// 방전 중 = 1, 차단예고 = 0, 항목사용안함 = 9
        /// </summary>
        public byte SystemDown
        {
            get { return systemDown; }
            set { systemDown = value; }
        }

        /// <summary>
        /// 배러티 사용 여부
        /// 사용 = 1, 미사용/없음 = 0, 항목사용안함 = 9
        /// </summary>
        public byte BattUse
        {
            get { return battUse; }
            set { battUse = value; }
        }

        /// <summary>
        /// 배터리 충전 상태
        /// 만충 = 1, 충전 중 = 0, 항목사용안함 = 9
        /// </summary>
        public byte BattState
        {
            get { return battState; }
            set { battState = value; }
        }

        /// <summary>
        /// 도어
        /// 닫힘 = 1, 열림 = 0, 항목사용안함 = 9
        /// </summary>
        public byte Door
        {
            get { return door; }
            set { door = value; }
        }

        /// <summary>
        /// 리셋
        /// 유지 = 1, 리셋완료 = 0, 항목사용안함 = 9
        /// </summary>
        public byte Reset
        {
            get { return reset; }
            set { reset = value; }
        }

        /// <summary>
        /// 펌웨어 버전
        /// ex) 1.01
        /// </summary>
        public string FwVer
        {
            get { return fwVer; }
            set { fwVer = value; }
        }

        /// <summary>
        /// 펌웨어 업데이트 날짜
        /// ex) 20080722
        /// </summary>
        public string FwDate
        {
            get { return fwDate; }
            set { fwDate = value; }
        }
        
        /// <summary>
        /// TDMB EWS 채널(EWS 수신되는 방송사)
        /// KBS = 0, MBC = 1, SBS = 2, YTN = 3, 기타1 = 4, 기타2 = 5, 항목사용안함 = 9
        /// </summary>
        public byte EwsCh
        {
            get { return ewsCh; }
            set { ewsCh = value; }
        }

        /// <summary>
        /// TDMB 레벨(0 ~ 6단계) -> 총 7단계
        /// 0 ~ 5(안테나 수), 6(통화권이탈), 항목사용안함 = 9
        /// </summary>
        public byte TdmbLv
        {
            get { return tdmbLv; }
            set { tdmbLv = value; }
        }

        /// <summary>
        /// SDMB 레벨(0 ~ 6단계) -> 총 7단계
        /// 0 ~ 5(안테나 수), 6(통화권이탈), 항목사용안함 = 9
        /// </summary>
        public byte SdmbLv
        {
            get { return sdmbLv; }
            set { sdmbLv = value; }
        }

        /// <summary>
        /// CDMA 레벨(0 ~ 6단계) -> 총 7단계
        /// 0 ~ 5(안테나 수), 6(통화권이탈), 항목사용안함 = 9
        /// </summary>
        public byte CdmaLv
        {
            get { return cdmaLv; }
            set { cdmaLv = value; }
        }

        /// <summary>
        /// TDMB 전송로 상태
        /// 정상 = 1, 오류 = 0, 항목사용안함 = 9
        /// </summary>
        public byte TdmbState
        {
            get { return tdmbState; }
            set { tdmbState = value; }
        }

        /// <summary>
        /// SDMB 전송로 상태
        /// 정상 = 1, 오류 = 0, 항목사용안함 = 9
        /// </summary>
        public byte SdmbState
        {
            get { return sdmbState; }
            set { sdmbState = value; }
        }

        /// <summary>
        /// 앰프 차단 스위치
        /// 정상 = 1, 차단 = 0, 항목사용안함 = 9
        /// </summary>
        public byte AmpSw
        {
            get { return ampSw; }
            set { ampSw = value; }
        }

        /// <summary>
        /// 앰프 2 사용여부
        /// 사용 = 1, 사용안함 = 0, 항목사용안함 = 9
        /// </summary>
        public byte AmpUse
        {
            get { return ampUse; }
            set { ampUse = value; }
        }

        /// <summary>
        /// 앰프 채널(스피커) 사용여부
        /// 0 : 사용안함, 1 : 사용함, 2 : 예비
        /// ex) 11012222 
        /// </summary>
        public string AmpChUse
        {
            get { return ampChUse; }
            set { ampChUse = value; }
        }

        /// <summary>
        /// 앰프 출력 레벨
        /// 1 = 0.1%, 2 = 50%, 3 = 75%, 4 = 100%, 항목사용안함 = 9
        /// </summary>
        public byte AmpLv
        {
            get { return ampLv; }
            set { ampLv = value; }
        }

        /// <summary>
        /// AC 전압
        /// ex) 220.5
        /// </summary>
        public string AcVolt
        {
            get { return acVolt; }
            set { acVolt = value; }
        }

        /// <summary>
        /// 시스템DC 전압
        /// ex) 4.7
        /// </summary>
        public string Dc5Volt
        {
            get { return dc5Volt; }
            set { dc5Volt = value; }
        }

        /// <summary>
        /// 앰프DC 전압
        /// ex) 23.5
        /// </summary>
        public string Dc24Volt
        {
            get { return dc24Volt; }
            set { dc24Volt = value; }
        }

        /// <summary>
        /// 온도
        /// ex) 025.7
        /// </summary>
        public string Tempo
        {
            get { return tempo; }
            set { tempo = value; }
        }

        /// <summary>
        /// 연동형 외부앰프 상태값(앰프전원, 앰프취명, 수동취명스위치 상태를 3비트 사용하여 표현)
        /// </summary>
        public byte ExAmp
        {
            get { return exAmp; }
            set { exAmp = value; }
        }
        #endregion

        /// <summary>
        /// 기본생성자
        /// </summary>
        public CtlResponseData()
        {
        }

        /// <summary>
        /// 기본생성자
        /// </summary>
        /// <param name="_psu1Ac">
        /// PSU1 AC-FAIL
        /// 정상 = 1, 불량 = 0, 항목사용안함 = 9
        /// </param>
        /// <param name="_psu1Dc">
        /// PSU1 DC-FAIL
        /// 정상 = 1, 불량 = 0, 항목사용안함 = 9
        /// </param>
        /// <param name="_psu1Fan">
        /// PSU1 FAN-FAIL
        /// 정상 = 1, 불량 = 0, 항목사용안함 = 9
        /// </param>
        /// <param name="_psu2Ac">
        /// PSU2 AC-FAIL
        /// 정상 = 1, 불량 = 0, 항목사용안함 = 9
        /// </param>
        /// <param name="_psu2Dc">
        /// PSU2 DC-FAIL
        /// 정상 = 1, 불량 = 0, 항목사용안함 = 9
        /// </param>
        /// <param name="_psu2Fan">
        /// PSU2 FAN-FAIL
        /// 정상 = 1, 불량 = 0, 항목사용안함 = 9
        /// </param>
        /// <param name="_psu2Use">
        /// PSU2 사용여부
        /// 사용 = 1, 사용안함 = 0, 항목사용안함 = 9
        /// </param>
        /// <param name="_systemDown">
        /// 배터리 과방전, 시스템 다운 예고
        /// 방전 중 = 1, 차단예고 = 0, 항목사용안함 = 9
        /// </param>
        /// <param name="_battUse">
        /// 배러티 사용 여부
        /// 사용 = 1, 미사용/없음 = 0, 항목사용안함 = 9
        /// </param>
        /// <param name="_battState">
        /// 배터리 충전 상태
        /// 만충 = 1, 충전 중 = 0, 항목사용안함 = 9
        /// </param>
        /// <param name="_door">
        /// 도어
        /// 닫힘 = 1, 열림 = 0, 항목사용안함 = 9
        /// </param>
        /// <param name="_reset">
        /// 리셋
        /// 유지 = 1, 리셋완료 = 0, 항목사용안함 = 9
        /// </param>
        /// <param name="_fwVer">
        /// 펌웨어 버전
        /// ex) 1.01
        /// </param>
        /// <param name="_fwDate">
        /// 펌웨어 업데이트 날짜
        /// ex) 20080722
        /// </param>
        /// <param name="_ewsCh">
        /// TDMB EWS 채널(EWS 수신되는 방송사)
        /// KBS = 0, MBC = 1, SBS = 2, YTN = 3, 기타1 = 4, 기타2 = 5, 항목사용안함 = 9
        /// </param>
        /// <param name="_tdmbLv">
        /// TDMB 레벨(0 ~ 6단계) -> 총 7단계
        /// 0 ~ 5(안테나 수), 6(통화권이탈), 항목사용안함 = 9
        /// </param>
        /// <param name="_sdmbLv">
        /// SDMB 레벨(0 ~ 6단계) -> 총 7단계
        /// 0 ~ 5(안테나 수), 6(통화권이탈), 항목사용안함 = 9
        /// </param>
        /// <param name="_cdmaLv">
        /// CDMA 레벨(0 ~ 6단계) -> 총 7단계
        /// 0 ~ 5(안테나 수), 6(통화권이탈), 항목사용안함 = 9
        /// </param>
        /// <param name="_tdmbState">
        /// TDMB 전송로 상태
        /// 정상 = 1, 오류 = 0, 항목사용안함 = 9
        /// </param>
        /// <param name="_sdmbState">
        /// SDMB 전송로 상태
        /// 정상 = 1, 오류 = 0, 항목사용안함 = 9
        /// </param>
        /// <param name="_ampSw">
        /// 앰프 차단 스위치
        /// 정상 = 1, 차단 = 0, 항목사용안함 = 9
        /// </param>
        /// <param name="_ampUse">
        /// 앰프 2 사용여부
        /// 사용 = 1, 사용안함 = 0, 항목사용안함 = 9
        /// </param>
        /// <param name="_ampChUse">
        /// 앰프 채널(스피커) 사용여부
        /// 0 : 사용안함, 1 : 사용함, 2 : 예비
        /// </param>
        /// <param name="_ampLv">
        /// 앰프 출력 레벨
        /// 1 = 0.1%, 2 = 50%, 3 = 75%, 4 = 100%, 항목사용안함 = 9
        /// </param>
        /// <param name="_acVolt">
        /// AC 전압
        /// ex) 220.5
        /// </param>
        /// <param name="_dc5Volt">
        /// 시스템DC 전압
        /// ex) 4.7
        /// </param>
        /// <param name="_dc24Volt">
        /// 앰프DC 전압
        /// ex) 23.5
        /// </param>
        /// <param name="_tempo">
        /// 온도
        /// ex) 025.7
        /// </param>
        public CtlResponseData(
            byte _psu1Ac,
            byte _psu1Dc,
            byte _psu1Fan,
            byte _psu2Ac,
            byte _psu2Dc,
            byte _psu2Fan,
            byte _psu2Use,
            byte _systemDown,
            byte _battUse,
            byte _battState,
            byte _door,
            byte _reset,
            string _fwVer,
            string _fwDate,
            byte _ewsCh,
            byte _tdmbLv,
            byte _sdmbLv,
            byte _cdmaLv,
            byte _tdmbState,
            byte _sdmbState,
            byte _ampSw,
            byte _ampUse,
            string _ampChUse,
            byte _ampLv,
            string _acVolt,
            string _dc5Volt,
            string _dc24Volt,
            string _tempo,
            byte _exAmp)
        {
            this.psu1Ac = _psu1Ac;
            this.psu1Dc = _psu1Dc;
            this.psu1Fan = _psu1Fan;
            this.psu2Ac = _psu2Ac;
            this.psu2Dc = _psu2Dc;
            this.psu2Fan = _psu2Fan;
            this.psu2Use = _psu2Use;
            this.systemDown = _systemDown;
            this.battUse = _battUse;
            this.battState = _battState;
            this.door = _door;
            this.reset = _reset;
            this.fwVer = _fwVer;
            this.fwDate = _fwDate;
            this.ewsCh = _ewsCh;
            this.tdmbLv = _tdmbLv;
            this.sdmbLv = _sdmbLv;
            this.cdmaLv = _cdmaLv;
            this.tdmbState = _tdmbState;
            this.sdmbState = _sdmbState;
            this.ampSw = _ampSw;
            this.ampUse = _ampUse;
            this.ampChUse = _ampChUse;
            this.ampLv = _ampLv;
            this.acVolt = _acVolt;
            this.dc5Volt = _dc5Volt;
            this.dc24Volt = _dc24Volt;
            this.tempo = _tempo;
            this.exAmp = _exAmp;
        }

        public static byte[] GetToByte(CtlResponseData _data)
        {
            byte[] arr = null;

            try
            {
                byte[] soh = new byte[] { 0x01, 0x02 };
                byte[] len = new byte[] { 0x30, 0x30, 0x30 };
                byte cmd = (byte)'T';
                byte[] b1 = Encoding.Default.GetBytes(string.Format("{0}", _data.Psu1Ac));
                byte[] b2 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Psu1Dc));
                byte[] b3 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Psu1Fan));
                byte[] b4 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Psu2Ac));
                byte[] b5 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Psu2Dc));
                byte[] b6 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Psu2Fan));
                byte[] b7 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Psu2Use));
                byte[] b8 = Encoding.Default.GetBytes(string.Format("/{0}", _data.SystemDown));
                byte[] b9 = Encoding.Default.GetBytes(string.Format("/{0}", _data.BattUse));
                byte[] b10 = Encoding.Default.GetBytes(string.Format("/{0}", _data.BattState));
                byte[] b11 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Door));
                byte[] b12 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Reset));
                byte[] b13 = Encoding.Default.GetBytes(string.Format("/{0}", _data.FwVer));
                byte[] b14 = Encoding.Default.GetBytes(string.Format("/{0}", _data.FwDate));
                byte[] b15 = Encoding.Default.GetBytes(string.Format("/{0}", _data.EwsCh));
                byte[] b16 = Encoding.Default.GetBytes(string.Format("/{0}", _data.TdmbLv));
                byte[] b17 = Encoding.Default.GetBytes(string.Format("/{0}", _data.SdmbLv));
                byte[] b18 = Encoding.Default.GetBytes(string.Format("/{0}", _data.CdmaLv));
                byte[] b19 = Encoding.Default.GetBytes(string.Format("/{0}", _data.TdmbState));
                byte[] b20 = Encoding.Default.GetBytes(string.Format("/{0}", _data.SdmbState));
                byte[] b21 = Encoding.Default.GetBytes(string.Format("/{0}", _data.AmpSw));
                byte[] b22 = Encoding.Default.GetBytes(string.Format("/{0}", _data.AmpUse));
                byte[] b23 = Encoding.Default.GetBytes(string.Format("/{0}", _data.AmpChUse));
                byte[] b24 = Encoding.Default.GetBytes(string.Format("/{0}", _data.AmpLv));
                byte[] b25 = Encoding.Default.GetBytes(string.Format("/{0}", _data.AcVolt));
                byte[] b26 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Dc5Volt));
                byte[] b27 = Encoding.Default.GetBytes(string.Format("/{0}", _data.Dc24Volt));
                byte[] b28 = Encoding.Default.GetBytes(string.Format("/{0}", _data.tempo));
                byte[] b29 = Encoding.Default.GetBytes(string.Format("/{0}", _data.ExAmp));
                byte[] eoh = new byte[] { 0x03, 0x04 };

                int dataLen = b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length + b14.Length + b15.Length
                    + b16.Length + b17.Length + b18.Length + b19.Length + b20.Length + b21.Length + b22.Length + b23.Length + b24.Length + b25.Length + b26.Length + b27.Length + b28.Length + b29.Length;

                len = Encoding.Default.GetBytes(string.Format("{0:000}", dataLen));

                arr = new byte[8 + dataLen];
                int cnt = 0;

                Buffer.BlockCopy(soh, 0, arr, 0, soh.Length);
                Buffer.BlockCopy(len, 0, arr, soh.Length, len.Length);
                arr[soh.Length + len.Length] = cmd;
                Buffer.BlockCopy(b1, 0, arr, soh.Length + len.Length + 1, b1.Length);
                cnt += soh.Length + len.Length + 1 + b1.Length;
                Buffer.BlockCopy(b2, 0, arr, cnt, b2.Length);
                cnt += b2.Length;
                Buffer.BlockCopy(b3, 0, arr, cnt, b3.Length);
                cnt += b3.Length;
                Buffer.BlockCopy(b4, 0, arr, cnt, b4.Length);
                cnt += b4.Length;
                Buffer.BlockCopy(b5, 0, arr, cnt, b5.Length);
                cnt += b5.Length;
                Buffer.BlockCopy(b6, 0, arr, cnt, b6.Length);
                cnt += b6.Length;
                Buffer.BlockCopy(b7, 0, arr, cnt, b7.Length);
                cnt += b7.Length;
                Buffer.BlockCopy(b8, 0, arr, cnt, b8.Length);
                cnt += b8.Length;
                Buffer.BlockCopy(b9, 0, arr, cnt, b9.Length);
                cnt += b9.Length;
                Buffer.BlockCopy(b10, 0, arr, cnt, b10.Length);
                cnt += b10.Length;
                Buffer.BlockCopy(b11, 0, arr, cnt, b11.Length);
                cnt += b11.Length;
                Buffer.BlockCopy(b12, 0, arr, cnt, b12.Length);
                cnt += b12.Length;
                Buffer.BlockCopy(b13, 0, arr, cnt, b13.Length);
                cnt += b13.Length;
                Buffer.BlockCopy(b14, 0, arr, cnt, b14.Length);
                cnt += b14.Length;
                Buffer.BlockCopy(b15, 0, arr, cnt, b15.Length);
                cnt += b15.Length;
                Buffer.BlockCopy(b16, 0, arr, cnt, b16.Length);
                cnt += b16.Length;
                Buffer.BlockCopy(b17, 0, arr, cnt, b17.Length);
                cnt += b17.Length;
                Buffer.BlockCopy(b18, 0, arr, cnt, b18.Length);
                cnt += b18.Length;
                Buffer.BlockCopy(b19, 0, arr, cnt, b19.Length);
                cnt += b19.Length;
                Buffer.BlockCopy(b20, 0, arr, cnt, b20.Length);
                cnt += b20.Length;
                Buffer.BlockCopy(b21, 0, arr, cnt, b21.Length);
                cnt += b21.Length;
                Buffer.BlockCopy(b22, 0, arr, cnt, b22.Length);
                cnt += b22.Length;
                Buffer.BlockCopy(b23, 0, arr, cnt, b23.Length);
                cnt += b23.Length;
                Buffer.BlockCopy(b24, 0, arr, cnt, b24.Length);
                cnt += b24.Length;
                Buffer.BlockCopy(b25, 0, arr, cnt, b25.Length);
                cnt += b25.Length;
                Buffer.BlockCopy(b26, 0, arr, cnt, b26.Length);
                cnt += b26.Length;
                Buffer.BlockCopy(b27, 0, arr, cnt, b27.Length);
                cnt += b27.Length;
                Buffer.BlockCopy(b28, 0, arr, cnt, b28.Length);
                cnt += b28.Length;
                Buffer.BlockCopy(b29, 0, arr, cnt, b29.Length);
                cnt += b29.Length;
                Buffer.BlockCopy(eoh, 0, arr, cnt, eoh.Length);
            }
            catch (Exception ex)
            {
                Console.Write(string.Format("\n[{0}] BattResponseData GetToByte Exception", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                arr = null;
            }

            return arr;
        }

        public static CtlResponseData SetFromByte(byte[] _arr)
        {
            CtlResponseData rst = new CtlResponseData();

            try
            {
                string dataBady = Encoding.Default.GetString(_arr, 6, _arr.Length - 8);
                string[] datas = dataBady.Split('/');

                int i = 0;

                rst.Psu1Ac = byte.Parse(datas[i++]);
                rst.Psu1Dc = byte.Parse(datas[i++]);
                rst.Psu1Fan = byte.Parse(datas[i++]);
                rst.Psu2Ac = byte.Parse(datas[i++]);
                rst.Psu2Dc = byte.Parse(datas[i++]);
                rst.Psu2Fan = byte.Parse(datas[i++]);
                rst.Psu2Use = byte.Parse(datas[i++]);
                rst.SystemDown = byte.Parse(datas[i++]);
                rst.BattUse = byte.Parse(datas[i++]);
                rst.BattState = byte.Parse(datas[i++]);
                rst.Door = byte.Parse(datas[i++]);
                rst.Reset = byte.Parse(datas[i++]);
                rst.FwVer = datas[i++];
                rst.FwDate = datas[i++];
                rst.EwsCh = byte.Parse(datas[i++]);
                rst.TdmbLv = byte.Parse(datas[i++]);
                rst.SdmbLv = byte.Parse(datas[i++]);
                rst.CdmaLv = byte.Parse(datas[i++]);
                rst.TdmbState = byte.Parse(datas[i++]);
                rst.SdmbState = byte.Parse(datas[i++]);
                rst.AmpSw = byte.Parse(datas[i++]);
                rst.AmpUse = byte.Parse(datas[i++]);
                rst.AmpChUse = datas[i++];
                rst.AmpLv = byte.Parse(datas[i++]);
                rst.AcVolt = datas[i++];
                rst.Dc5Volt = datas[i++];
                rst.Dc24Volt = datas[i++];
                rst.tempo = datas[i++];
                rst.ExAmp = byte.Parse(datas[i++]);
            }
            catch
            {
                Console.Write(string.Format("\n[{0}] CtlResponseData SetFromByte Exception", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                rst = null;
            }

            return rst;
        }
    }
}
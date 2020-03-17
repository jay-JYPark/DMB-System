using System;
using System.Collections.Generic;
using System.Text;

namespace ADEng.Library.DMB
{
    /// <summary>
    /// 앰프 사용여부를 나타내는 enum's
    /// </summary>
    public enum DState
    {
        NONE = 0,               //모두 사용안함
        AMP1USE = 1,            //앰프 1만 사용
        AMP2USE = 2,            //앰프 2만 사용
        ALLUSE = 3,             //두개 모두 사용
    }

    /// <summary>
    /// 앰프/스피커 시험 후 응답할 때 사용하는 클래스
    /// </summary>
    [Serializable]
    public class AmpResponseData
    {
        #region 멤버
        private byte stete = (byte)DState.NONE;
        private float amp1_ch1_volt = 0f;
        private float amp1_ch2_volt = 0f;
        private float amp1_ch3_volt = 0f;
        private float amp1_ch4_volt = 0f;
        private float amp2_ch1_volt = 0f;
        private float amp2_ch2_volt = 0f;
        private float amp2_ch3_volt = 0f;
        private float amp2_ch4_volt = 0f;
        private float amp1_ch1_cur = 0f;
        private float amp1_ch2_cur = 0f;
        private float amp1_ch3_cur = 0f;
        private float amp1_ch4_cur = 0f;
        private float amp2_ch1_cur = 0f;
        private float amp2_ch2_cur = 0f;
        private float amp2_ch3_cur = 0f;
        private float amp2_ch4_cur = 0f;
        #endregion

        #region 접근
        /// <summary>
        /// 앰프 사용여부 상태를 나타낸다.
        /// NONE = 0,       모두 사용안함
        /// AMP1USE = 1,    앰프 1만 사용
        /// AMP2USE = 2,    앰프 2만 사용
        /// ALLUSE = 3,     두개 모두 사용
        /// </summary>
        public byte State
        {
            get { return this.stete; }
            set { this.stete = value; }
        }

        /// <summary>
        /// 1번 앰프의 1번 스피커 전압
        /// </summary>
        public float Amp1_ch1_volt
        {
            get { return this.amp1_ch1_volt; }
            set { this.amp1_ch1_volt = value; }
        }

        /// <summary>
        /// 1번 앰프의 2번 스피커 전압
        /// </summary>
        public float Amp1_ch2_volt
        {
            get { return this.amp1_ch2_volt; }
            set { this.amp1_ch2_volt = value; }
        }

        /// <summary>
        /// 1번 앰프의 3번 스피커 전압
        /// </summary>
        public float Amp1_ch3_volt
        {
            get { return this.amp1_ch3_volt; }
            set { this.amp1_ch3_volt = value; }
        }

        /// <summary>
        /// 1번 앰프의 4번 스피커 전압
        /// </summary>
        public float Amp1_ch4_volt
        {
            get { return this.amp1_ch4_volt; }
            set { this.amp1_ch4_volt = value; }
        }

        /// <summary>
        /// 2번 앰프의 1번 스피커 전압
        /// </summary>
        public float Amp2_ch1_volt
        {
            get { return this.amp2_ch1_volt; }
            set { this.amp2_ch1_volt = value; }
        }

        /// <summary>
        /// 2번 앰프의 2번 스피커 전압
        /// </summary>
        public float Amp2_ch2_volt
        {
            get { return this.amp2_ch2_volt; }
            set { this.amp2_ch2_volt = value; }
        }

        /// <summary>
        /// 2번 앰프의 3번 스피커 전압
        /// </summary>
        public float Amp2_ch3_volt
        {
            get { return this.amp2_ch3_volt; }
            set { this.amp2_ch3_volt = value; }
        }

        /// <summary>
        /// 2번 앰프의 4번 스피커 전압
        /// </summary>
        public float Amp2_ch4_volt
        {
            get { return this.amp2_ch4_volt; }
            set { this.amp2_ch4_volt = value; }
        }

        /// <summary>
        /// 1번 앰프의 1번 스피커 전류
        /// </summary>
        public float Amp1_ch1_cur
        {
            get { return this.amp1_ch1_cur; }
            set { this.amp1_ch1_cur = value; }
        }

        /// <summary>
        /// 1번 앰프의 2번 스피커 전류
        /// </summary>
        public float Amp1_ch2_cur
        {
            get { return this.amp1_ch2_cur; }
            set { this.amp1_ch2_cur = value; }
        }

        /// <summary>
        /// 1번 앰프의 3번 스피커 전류
        /// </summary>
        public float Amp1_ch3_cur
        {
            get { return this.amp1_ch3_cur; }
            set { this.amp1_ch3_cur = value; }
        }

        /// <summary>
        /// 1번 앰프의 4번 스피커 전류
        /// </summary>
        public float Amp1_ch4_cur
        {
            get { return this.amp1_ch4_cur; }
            set { this.amp1_ch4_cur = value; }
        }

        /// <summary>
        /// 2번 앰프의 1번 스피커 전류
        /// </summary>
        public float Amp2_ch1_cur
        {
            get { return this.amp2_ch1_cur; }
            set { this.amp2_ch1_cur = value; }
        }

        /// <summary>
        /// 2번 앰프의 2번 스피커 전류
        /// </summary>
        public float Amp2_ch2_cur
        {
            get { return this.amp2_ch2_cur; }
            set { this.amp2_ch2_cur = value; }
        }

        /// <summary>
        /// 2번 앰프의 3번 스피커 전류
        /// </summary>
        public float Amp2_ch3_cur
        {
            get { return this.amp2_ch3_cur; }
            set { this.amp2_ch3_cur = value; }
        }

        /// <summary>
        /// 2번 앰프의 4번 스피커 전류
        /// </summary>
        public float Amp2_ch4_cur
        {
            get { return this.amp2_ch4_cur; }
            set { this.amp2_ch4_cur = value; }
        }
        #endregion

        /// <summary>
        /// 기본생성자
        /// </summary>
        public AmpResponseData()
        {
        }

        /// <summary>
        /// 1번 앰프만 사용하는 생성자
        /// </summary>
        /// <param name="_state">
        /// 앰프 사용여부 상태를 나타낸다.
        /// NONE = 0,       모두 사용안함
        /// AMP1USE = 1,    앰프 1만 사용
        /// AMP2USE = 2,    앰프 2만 사용
        /// ALLUSE = 3,     두개 모두 사용
        /// </param>
        /// <param name="_amp1_1_volt">
        /// 1번 앰프의 1번 스피커 전압
        /// </param>
        /// <param name="_amp1_2_volt">
        /// 1번 앰프의 2번 스피커 전압
        /// </param>
        /// <param name="_amp1_3_volt">
        /// 1번 앰프의 3번 스피커 전압
        /// </param>
        /// <param name="_amp1_4_volt">
        /// 1번 앰프의 4번 스피커 전압
        /// </param>
        /// <param name="_amp1_1_cur">
        /// 1번 앰프의 1번 스피커 전류
        /// </param>
        /// <param name="_amp1_2_cur">
        /// 1번 앰프의 2번 스피커 전류
        /// </param>
        /// <param name="_amp1_3_cur">
        /// 1번 앰프의 3번 스피커 전류
        /// </param>
        /// <param name="_amp1_4_cur">
        /// 1번 앰프의 4번 스피커 전류
        /// </param>
        public AmpResponseData
            (byte _state,
             float _amp1_1_volt,
             float _amp1_2_volt,
             float _amp1_3_volt,
             float _amp1_4_volt,
             float _amp1_1_cur,
             float _amp1_2_cur,
             float _amp1_3_cur,
             float _amp1_4_cur)
        {
            this.stete = _state;
            this.amp1_ch1_volt = _amp1_1_volt;
            this.amp1_ch2_volt = _amp1_2_volt;
            this.amp1_ch3_volt = _amp1_3_volt;
            this.amp1_ch4_volt = _amp1_4_volt;
            this.amp1_ch1_cur = _amp1_1_cur;
            this.amp1_ch2_cur = _amp1_2_cur;
            this.amp1_ch3_cur = _amp1_3_cur;
            this.amp1_ch4_cur = _amp1_4_cur;
        }

        /// <summary>
        /// 앰프를 모두 사용하는 생성자
        /// </summary>
        /// <param name="_state">
        /// 앰프 사용여부 상태를 나타낸다.
        /// NONE = 0,       모두 사용안함
        /// AMP1USE = 1,    앰프 1만 사용
        /// AMP2USE = 2,    앰프 2만 사용
        /// ALLUSE = 3,     두개 모두 사용
        /// </param>
        /// <param name="_amp1_1_volt">
        /// 1번 앰프의 1번 스피커 전압
        /// </param>
        /// <param name="_amp1_2_volt">
        /// 1번 앰프의 2번 스피커 전압
        /// </param>
        /// <param name="_amp1_3_volt">
        /// 1번 앰프의 3번 스피커 전압
        /// </param>
        /// <param name="_amp1_4_volt">
        /// 1번 앰프의 4번 스피커 전압
        /// </param>
        /// <param name="_amp1_1_cur">
        /// 1번 앰프의 1번 스피커 전류
        /// </param>
        /// <param name="_amp1_2_cur">
        /// 1번 앰프의 2번 스피커 전류
        /// </param>
        /// <param name="_amp1_3_cur">
        /// 1번 앰프의 3번 스피커 전류
        /// </param>
        /// <param name="_amp1_4_cur">
        /// 1번 앰프의 4번 스피커 전류
        /// </param>
        /// <param name="_amp2_1_volt">
        /// 2번 앰프의 1번 스피커 전압
        /// </param>
        /// <param name="_amp2_2_volt">
        /// 2번 앰프의 2번 스피커 전압
        /// </param>
        /// <param name="_amp2_3_volt">
        /// 2번 앰프의 3번 스피커 전압
        /// </param>
        /// <param name="_amp2_4_volt">
        /// 2번 앰프의 4번 스피커 전압
        /// </param>
        /// <param name="_amp2_1_cur">
        /// 2번 앰프의 1번 스피커 전류
        /// </param>
        /// <param name="_amp2_2_cur">
        /// 2번 앰프의 2번 스피커 전류
        /// </param>
        /// <param name="_amp2_3_cur">
        /// 2번 앰프의 3번 스피커 전류
        /// </param>
        /// <param name="_amp2_4_cur">
        /// 2번 앰프의 4번 스피커 전류
        /// </param>
        public AmpResponseData
            (byte _state,
             float _amp1_1_volt,
             float _amp1_2_volt,
             float _amp1_3_volt,
             float _amp1_4_volt,
             float _amp1_1_cur,
             float _amp1_2_cur,
             float _amp1_3_cur,
             float _amp1_4_cur,
             float _amp2_1_volt,
             float _amp2_2_volt,
             float _amp2_3_volt,
             float _amp2_4_volt,
             float _amp2_1_cur,
             float _amp2_2_cur,
             float _amp2_3_cur,
             float _amp2_4_cur)
        {
            this.stete = _state;
            this.amp1_ch1_volt = _amp1_1_volt;
            this.amp1_ch2_volt = _amp1_2_volt;
            this.amp1_ch3_volt = _amp1_3_volt;
            this.amp1_ch4_volt = _amp1_4_volt;
            this.amp1_ch1_cur = _amp1_1_cur;
            this.amp1_ch2_cur = _amp1_2_cur;
            this.amp1_ch3_cur = _amp1_3_cur;
            this.amp1_ch4_cur = _amp1_4_cur;
            this.amp2_ch1_volt = _amp2_1_volt;
            this.amp2_ch2_volt = _amp2_2_volt;
            this.amp2_ch3_volt = _amp2_3_volt;
            this.amp2_ch4_volt = _amp2_4_volt;
            this.amp2_ch1_cur = _amp2_1_cur;
            this.amp2_ch2_cur = _amp2_2_cur;
            this.amp2_ch3_cur = _amp2_3_cur;
            this.amp2_ch4_cur = _amp2_4_cur;
        }

        public static byte[] GetToByte(AmpResponseData _data)
        {
            byte[] arr = null;

            try
            {
                byte[] soh = new byte[] { 0x01, 0x02 };
                byte[] len = new byte[] { 0x30, 0x30, 0x30 };
                byte cmd = (byte)'M';
                byte[] b1 = Encoding.Default.GetBytes(string.Format("{0}", _data.stete));
                byte[] b2 = Encoding.Default.GetBytes(string.Format("/{0}", _data.amp1_ch1_volt));
                byte[] b3 = Encoding.Default.GetBytes(string.Format("/{0}", _data.amp1_ch2_volt));
                byte[] b4 = Encoding.Default.GetBytes(string.Format("/{0}", _data.amp1_ch3_volt));
                byte[] b5 = Encoding.Default.GetBytes(string.Format("/{0}", _data.amp1_ch4_volt));
                byte[] b6 = Encoding.Default.GetBytes(string.Format("/{0}", _data.amp1_ch1_cur));
                byte[] b7 = Encoding.Default.GetBytes(string.Format("/{0}", _data.amp1_ch2_cur));
                byte[] b8 = Encoding.Default.GetBytes(string.Format("/{0}", _data.amp1_ch3_cur));
                byte[] b9 = Encoding.Default.GetBytes(string.Format("/{0}", _data.amp1_ch4_cur));
                byte[] b10 = Encoding.Default.GetBytes(string.Format("/{0}", _data.amp2_ch1_volt));
                byte[] b11 = Encoding.Default.GetBytes(string.Format("/{0}", _data.amp2_ch2_volt));
                byte[] b12 = Encoding.Default.GetBytes(string.Format("/{0}", _data.amp2_ch3_volt));
                byte[] b13 = Encoding.Default.GetBytes(string.Format("/{0}", _data.amp2_ch4_volt));
                byte[] b14 = Encoding.Default.GetBytes(string.Format("/{0}", _data.amp2_ch1_cur));
                byte[] b15 = Encoding.Default.GetBytes(string.Format("/{0}", _data.amp2_ch2_cur));
                byte[] b16 = Encoding.Default.GetBytes(string.Format("/{0}", _data.amp2_ch3_cur));
                byte[] b17 = Encoding.Default.GetBytes(string.Format("/{0}", _data.amp2_ch4_cur));
                byte[] eoh = new byte[] { 0x03, 0x04 };

                int dataLen = 1 + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length + b11.Length + b12.Length + b13.Length + b14.Length + b15.Length + b16.Length + b17.Length;

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
            catch
            {
                Console.Write(string.Format("\n[{0}] AmpResponseData GetToByte Exception", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                arr = null;
            }

            return arr;
        }

        public static AmpResponseData SetFromByte(byte[] _arr)
        {
            AmpResponseData rst = new AmpResponseData();

            try
            {
                string dataBady = Encoding.Default.GetString(_arr, 6, _arr.Length - 8);
                string[] datas = dataBady.Split('/');

                int i = 0;

                rst.stete = byte.Parse(datas[i++]);
                rst.amp1_ch1_volt = float.Parse(datas[i++]);
                rst.amp1_ch2_volt = float.Parse(datas[i++]);
                rst.amp1_ch3_volt = float.Parse(datas[i++]);
                rst.amp1_ch4_volt = float.Parse(datas[i++]);
                rst.amp1_ch1_cur = float.Parse(datas[i++]);
                rst.amp1_ch2_cur = float.Parse(datas[i++]);
                rst.amp1_ch3_cur = float.Parse(datas[i++]);
                rst.amp1_ch4_cur = float.Parse(datas[i++]);
                rst.amp2_ch1_volt = float.Parse(datas[i++]);
                rst.amp2_ch2_volt = float.Parse(datas[i++]);
                rst.amp2_ch3_volt = float.Parse(datas[i++]);
                rst.amp2_ch4_volt = float.Parse(datas[i++]);
                rst.amp2_ch1_cur = float.Parse(datas[i++]);
                rst.amp2_ch2_cur = float.Parse(datas[i++]);
                rst.amp2_ch3_cur = float.Parse(datas[i++]);
                rst.amp2_ch4_cur = float.Parse(datas[i++]);
            }
            catch
            {
                Console.Write(string.Format("\n[{0}] AmpResponseData SetFromByte Exception", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                rst = null;
            }

            return rst;
        }
    }
}
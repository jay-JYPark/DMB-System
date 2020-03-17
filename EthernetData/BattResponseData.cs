using System;
using System.Collections.Generic;
using System.Text;

namespace ADEng.Library.DMB
{
    /// <summary>
    /// 충전기 사용여부를 나타내는 enum's
    /// </summary>
    public enum DCState
    {
        DC_1_USE = 1,            //충전기 1만 사용
        DC_2_USE = 2,            //충전기 2만 사용
        DC_ALLUSE = 3,           //충전기 모두 사용
    }

    [Serializable]
    public class BattResponseData
    {
        #region 멤버
        private byte state = (byte)DCState.DC_1_USE;
        private float psu_1_volt = 0f;
        private float psu_2_volt = 0f;
        private int psu_1_per = int.MinValue;
        private int psu_2_per = int.MinValue;
        #endregion

        #region 접근
        /// <summary>
        /// 충전기 사용여부를 나타내는 값
        /// DC_1_USE = 1,       충전기 1만 사용
        /// DC_2_USE = 2,       충전기 2만 사용
        /// DC_ALLUSE = 3,      충전기 모두 사용
        /// </summary>
        public byte State
        {
            get { return this.state; }
            set { this.state = value; }
        }

        /// <summary>
        /// 1번 PSU의 전압을 나타내는 값
        /// </summary>
        public float PSU_1_Volt
        {
            get { return this.psu_1_volt; }
            set { this.psu_1_volt = value; }
        }

        /// <summary>
        /// 2번 PSU의 전압을 나타내는 값
        /// </summary>
        public float PSU_2_Volt
        {
            get { return this.psu_2_volt; }
            set { this.psu_2_volt = value; }
        }

        /// <summary>
        /// 1번 PSU의 전압에 따른 남은 양 퍼센트를 나타내는 값
        /// </summary>
        public int PSU_1_Per
        {
            get { return this.psu_1_per; }
            set { this.psu_1_per = value; }
        }

        /// <summary>
        /// 2번 PSU의 전압에 따른 남은 양 퍼센트를 나타내는 값
        /// </summary>
        public int PSU_2_Per
        {
            get { return this.psu_2_per; }
            set { this.psu_2_per = value; }
        }
        #endregion

        public BattResponseData()
        {
        }

        /// <summary>
        /// 1번 충전기만 사용하는 경우의 생성자
        /// </summary>
        /// <param name="_psu_1">
        /// 1번 PSU의 전압 값
        /// </param>
        /// <param name="_psu_1_per">
        /// 1번 PSU의 전압에 따른 배터리의 남은 퍼센트 값
        /// </param>
        public BattResponseData(float _psu_1, int _psu_1_per)
        {
            this.psu_1_volt = _psu_1;
            this.psu_1_per = _psu_1_per;
        }

        /// <summary>
        /// 충전기를 모두 사용하는 경우의 생성자
        /// </summary>
        /// <param name="_state">
        /// 충전기 사용여부를 나타내는 값
        /// DC_1_USE = 1,       충전기 1만 사용
        /// DC_2_USE = 2,       충전기 2만 사용
        /// DC_ALLUSE = 3,      충전기 모두 사용
        /// </param>
        /// <param name="_psu_1">
        /// 1번 PSU의 전압 값
        /// </param>
        /// <param name="_psu_1_per">
        /// 1번 PSU의 전압 값에 따른 배터리의 남은 양 퍼센트 값
        /// </param>
        /// <param name="_psu_2">
        /// 2번 PSU의 전압 값
        /// </param>
        /// <param name="_psu_2_per">
        /// 2번 PSU의 전압 값에 따른 배터리의 남은 양 퍼센트 값
        /// </param>
        public BattResponseData(byte _state, float _psu_1, int _psu_1_per, float _psu_2, int _psu_2_per)
        {
            this.state = _state;
            this.psu_1_volt = _psu_1;
            this.psu_2_volt = _psu_2;
            this.psu_1_per = _psu_1_per;
            this.psu_2_per = _psu_2_per;
        }

        public static byte[] GetToByte(BattResponseData _data)
        {
            byte[] arr = null;

            try
            {
                byte[] soh = new byte[] { 0x01, 0x02 };
                byte[] len = new byte[] { 0x30, 0x30, 0x30 };
                byte cmd = (byte)'B';
                byte[] b1 = Encoding.Default.GetBytes(string.Format("{0}", _data.State));
                byte[] b2 = Encoding.Default.GetBytes(string.Format("/{0}", _data.PSU_1_Volt));
                byte[] b3 = Encoding.Default.GetBytes(string.Format("/{0}", _data.PSU_2_Volt));
                byte[] b4 = Encoding.Default.GetBytes(string.Format("/{0}", _data.PSU_1_Per));
                byte[] b5 = Encoding.Default.GetBytes(string.Format("/{0}", _data.PSU_2_Per));
                byte[] eoh = new byte[] { 0x03, 0x04 };

                int dataLen = b1.Length + b2.Length + b3.Length + b4.Length + b5.Length;

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
                Buffer.BlockCopy(eoh, 0, arr, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length, eoh.Length);
            }
            catch (Exception ex)
            {
                Console.Write(string.Format("\n[{0}] BattResponseData GetToByte Exception", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                arr = null;
            }

            return arr;
        }

        public static BattResponseData SetFromByte(byte[] _arr)
        {
            BattResponseData rst = new BattResponseData();

            try
            {
                string dataBady = Encoding.Default.GetString(_arr, 6, _arr.Length - 8);
                string[] datas = dataBady.Split('/');

                int i = 0;

                rst.State = byte.Parse(datas[i++]);
                rst.PSU_1_Volt = float.Parse(datas[i++]);
                rst.PSU_2_Volt = float.Parse(datas[i++]);
                rst.PSU_1_Per = int.Parse(datas[i++]);
                rst.PSU_2_Per = int.Parse(datas[i++]);
            }
            catch
            {
                Console.Write(string.Format("\n[{0}] BattResponseData SetFromByte Exception", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                rst = null;
            }

            return rst;
        }
    }
}
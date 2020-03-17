using System;
using System.Collections.Generic;
using System.Text;

namespace ADEng.Library.DMB
{
    /// <summary>
    /// 수신기가 ARS제어에 대한 응답할 때 사용하는 클래스
    /// </summary>
    [Serializable]
    public class ARSResponseData
    {
        #region 멤버
        private string ars_01 = "00000000000";
        private string ars_02 = "00000000000";
        private string ars_03 = "00000000000";
        private string ars_04 = "00000000000";
        private string ars_05 = "00000000000";
        private string ars_06 = "00000000000";
        private string ars_07 = "00000000000";
        private string ars_08 = "00000000000";
        private string ars_09 = "00000000000";
        private string ars_10 = "00000000000";
        #endregion

        #region 접근
        /// <summary>
        /// 1 번째 ARS 전화번호
        /// </summary>
        public string ARS_01
        {
            get { return this.ars_01; }
            set { this.ars_01 = value; }
        }

        /// <summary>
        /// 2 번째 ARS 전화번호
        /// </summary>
        public string ARS_02
        {
            get { return this.ars_02; }
            set { this.ars_02 = value; }
        }

        /// <summary>
        /// 3 번째 ARS 전화번호
        /// </summary>
        public string ARS_03
        {
            get { return this.ars_03; }
            set { this.ars_03 = value; }
        }

        /// <summary>
        /// 4 번째 ARS 전화번호
        /// </summary>
        public string ARS_04
        {
            get { return this.ars_04; }
            set { this.ars_04 = value; }
        }

        /// <summary>
        /// 5 번째 ARS 전화번호
        /// </summary>
        public string ARS_05
        {
            get { return this.ars_05; }
            set { this.ars_05 = value; }
        }

        /// <summary>
        /// 6 번째 ARS 전화번호
        /// </summary>
        public string ARS_06
        {
            get { return this.ars_06; }
            set { this.ars_06 = value; }
        }

        /// <summary>
        /// 7 번째 ARS 전화번호
        /// </summary>
        public string ARS_07
        {
            get { return this.ars_07; }
            set { this.ars_07 = value; }
        }

        /// <summary>
        /// 8 번째 ARS 전화번호
        /// </summary>
        public string ARS_08
        {
            get { return this.ars_08; }
            set { this.ars_08 = value; }
        }

        /// <summary>
        /// 9 번째 ARS 전화번호
        /// </summary>
        public string ARS_09
        {
            get { return this.ars_09; }
            set { this.ars_09 = value; }
        }

        /// <summary>
        /// 10 번째 ARS 전화번호
        /// </summary>
        public string ARS_10
        {
            get { return this.ars_10; }
            set { this.ars_10 = value; }
        }
        #endregion

        /// <summary>
        /// 기본생성자
        /// </summary>
        public ARSResponseData()
        {
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="_ars01">
        /// 1 번째 ARS 전화번호
        /// </param>
        /// <param name="_ars02">
        /// 2 번째 ARS 전화번호
        /// </param>
        /// <param name="_ars03">
        /// 3 번째 ARS 전화번호
        /// </param>
        /// <param name="_ars04">
        /// 4 번째 ARS 전화번호
        /// </param>
        /// <param name="_ars05">
        /// 5 번째 ARS 전화번호
        /// </param>
        /// <param name="_ars06">
        /// 6 번째 ARS 전화번호
        /// </param>
        /// <param name="_ars07">
        /// 7 번째 ARS 전화번호
        /// </param>
        /// <param name="_ars08">
        /// 8 번째 ARS 전화번호
        /// </param>
        /// <param name="_ars09">
        /// 9 번째 ARS 전화번호
        /// </param>
        /// <param name="_ars10">
        /// 10 번째 ARS 전화번호
        /// </param>
        public ARSResponseData
            (string _ars01,
             string _ars02,
             string _ars03,
             string _ars04,
             string _ars05,
             string _ars06,
             string _ars07,
             string _ars08,
             string _ars09,
             string _ars10)
        {
            this.ars_01 = _ars01;
            this.ars_02 = _ars02;
            this.ars_03 = _ars03;
            this.ars_04 = _ars04;
            this.ars_05 = _ars05;
            this.ars_06 = _ars06;
            this.ars_07 = _ars07;
            this.ars_08 = _ars08;
            this.ars_09 = _ars09;
            this.ars_10 = _ars10;
        }

        public static byte[] GetToByte(ARSResponseData _data)
        {
            byte[] arr = null;

            try
            {
                byte[] soh = new byte[] { 0x01, 0x02 };
                byte[] len = new byte[] { 0x30, 0x30, 0x30 };
                byte cmd = (byte)'S';
                byte[] b1 = Encoding.Default.GetBytes(string.Format("{0}", _data.ARS_01));
                byte[] b2 = Encoding.Default.GetBytes(string.Format("/{0}",_data.ARS_02));
                byte[] b3 = Encoding.Default.GetBytes(string.Format("/{0}",_data.ARS_03));
                byte[] b4 = Encoding.Default.GetBytes(string.Format("/{0}",_data.ARS_04));
                byte[] b5 = Encoding.Default.GetBytes(string.Format("/{0}",_data.ARS_05));
                byte[] b6 = Encoding.Default.GetBytes(string.Format("/{0}",_data.ARS_06));
                byte[] b7 = Encoding.Default.GetBytes(string.Format("/{0}",_data.ARS_07));
                byte[] b8 = Encoding.Default.GetBytes(string.Format("/{0}",_data.ARS_08));
                byte[] b9 = Encoding.Default.GetBytes(string.Format("/{0}",_data.ARS_09));
                byte[] b10 = Encoding.Default.GetBytes(string.Format("/{0}",_data.ARS_10));
                byte[] eoh = new byte[] { 0x03, 0x04 };

                int dataLen = b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length;

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
                Buffer.BlockCopy(eoh, 0, arr, soh.Length + len.Length + 1 + b1.Length + b2.Length + b3.Length + b4.Length + b5.Length + b6.Length + b7.Length + b8.Length + b9.Length + b10.Length, eoh.Length);
            }
            catch (Exception ex)
            {
                Console.Write(string.Format("\n[{0}] ARSResponseData GetToByte Exception", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                arr = null;
            }

            return arr;
        }

        public static ARSResponseData SetFromByte(byte[] _arr)
        {
            ARSResponseData rst = new ARSResponseData();

            try
            {
                string dataBady = Encoding.Default.GetString(_arr, 6, _arr.Length - 8);
                string[] datas = dataBady.Split('/');

                int i = 0;

                rst.ARS_01 = datas[i++];
                rst.ARS_02 = datas[i++];
                rst.ARS_03 = datas[i++];
                rst.ARS_04 = datas[i++];
                rst.ARS_05 = datas[i++];
                rst.ARS_06 = datas[i++];
                rst.ARS_07 = datas[i++];
                rst.ARS_08 = datas[i++];
                rst.ARS_09 = datas[i++];
                rst.ARS_10 = datas[i++];
            }
            catch
            {
                Console.Write(string.Format("\n[{0}] ARSResponseData SetFromByte Exception", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                rst = null;
            }

            return rst;
        }
    }
}
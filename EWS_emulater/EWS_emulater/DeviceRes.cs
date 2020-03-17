using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace EWS_emulater
{
    class DeviceRes
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public DeviceRes()
        {
        }

        /// <summary>
        /// 지역형식을 반환한다.
        /// </summary>
        /// <param name="_val1"></param>
        /// <param name="_val2"></param>
        /// <returns></returns>
        public int DeviceRegion(int _val1, int _val2)
        {
            try
            {
                int tmp = 0;
                string str1 = this.fnByteToBit(_val1);
                string str2 = this.fnByteToBit(_val2);
                string tot = string.Empty;

                str1 = str1.Substring(0, 1);
                str2 = str2.Substring(6, 2);
                tot = str2 + str1;

                if (tot[0].ToString() == "1")
                {
                    tmp += 4;
                }
                if (tot[1].ToString() == "1")
                {
                    tmp += 2;
                }
                if (tot[2].ToString() == "1")
                {
                    tmp += 1;
                }

                return tmp;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeviceRes.DeviceRegion - " + ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// 지역수를 반환한다.
        /// </summary>
        /// <param name="_val"></param>
        /// <returns></returns>
        public int DeviceRegionCount(int _val)
        {
            try
            {
                int tmp = 0;
                string str = this.fnByteToBit(_val);

                str = str.Substring(1, 4);

                if (str[0].ToString() == "1")
                {
                    tmp += 8;
                }
                if (str[1].ToString() == "1")
                {
                    tmp += 4;
                }
                if (str[2].ToString() == "1")
                {
                    tmp += 2;
                }
                if (str[3].ToString() == "1")
                {
                    tmp += 1;
                }

                return tmp;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeviceRes.DeviceRegionCount - " + ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// 비트 스트링으로 변환해서 반환한다.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string fnByteToBit(int value)
        {
            string bitString;

            if (value > 127)
            {
                bitString = "1";
                value = value - 128;
            }
            else
            {
                bitString = "0";
            }
            /////////////////////////////////
            if (value > 63)
            {
                bitString = bitString + "1";
                value = value - 64;
            }
            else
            {
                bitString = bitString + "0";
            }
            /////////////////////////////////
            if (value > 31)
            {
                bitString = bitString + "1";
                value = value - 32;
            }
            else
            {
                bitString = bitString + "0";
            }
            /////////////////////////////////
            if (value > 15)
            {
                bitString = bitString + "1";
                value = value - 16;
            }
            else
            {
                bitString = bitString + "0";
            }
            /////////////////////////////////
            if (value > 7)
            {
                bitString = bitString + "1";
                value = value - 8;
            }
            else
            {
                bitString = bitString + "0";
            }
            /////////////////////////////////
            if (value > 3)
            {
                bitString = bitString + "1";
                value = value - 4;
            }
            else
            {
                bitString = bitString + "0";
            }
            /////////////////////////////////
            if (value > 1)
            {
                bitString = bitString + "1";
                value = value - 2;
            }
            else
            {
                bitString = bitString + "0";
            }

            bitString = bitString + Convert.ToString(value);

            return bitString;
        }
    }
}
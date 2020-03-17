using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MUX
{
    public class MUXInfo
    {
        #region  멤버 변수
        public string MUXIp { get; set; }
        public string description { get; set; }
        public bool status { get; set; }
        public string MUXName { get; set; }
        #endregion


        /// <summary>
        /// 기본 생성자
        /// </summary>
        public MUXInfo()
        {
        }


        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="muxIp"></param>
        /// <param name="description"></param>
        /// <param name="status"></param>
        /// <param name="muxName"></param>
        public MUXInfo(string muxIp, string description, bool status, string muxName)
        {
            this.MUXIp = muxIp;
            this.description = description;
            this.status = status;
            this.MUXName = MUXName;
        }


    }
}

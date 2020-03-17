using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DmbDSSystem
{
    [Serializable]
    public class TcpProfileDataContainer
    {
        private List<TcpProfileData> lstTcpProfileData = new List<TcpProfileData>();

        public List<TcpProfileData> LstTcpProfileData
        {
            get { return lstTcpProfileData; }
            set { this.lstTcpProfileData = value; }
        }
    }
}
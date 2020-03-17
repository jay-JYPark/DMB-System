using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DmbDSSystem
{
    public class TcpProfileData
    {
        private string ipAddr = string.Empty;
        private int port = 0;

        /// <summary>
        /// IP 프로퍼티
        /// </summary>
        public string IpAddr
        {
            get { return this.ipAddr; }
            set { this.ipAddr = value; }
        }

        /// <summary>
        /// PORT 프로퍼티
        /// </summary>
        public int Port
        {
            get { return this.port; }
            set { this.port = value; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace ADEng.Library.DMB
{
    /// <summary>
    /// 클라이언트 정보를 가지는 클래스
    /// </summary>
    public class Soc_Client
    {
        private TcpClient client;
        private string tel_num = string.Empty;
        private bool cState = false;
        
        #region 접근
        /// <summary>
        /// 클라이언의 세션
        /// </summary>
        public TcpClient Client
        {
            get { return client; }
        }

        /// <summary>
        /// 클라이언트 수신기 전화번호
        /// </summary>
        public string TelNum
        {
            get { return tel_num; }
        }

        /// <summary>
        /// 클라이언트 쓰레드
        /// </summary>
        public bool ClientState
        {
            get { return cState; }
            set { this.cState = value; }
        }
        #endregion

        /// <summary>
        /// 기본생성자
        /// </summary>
        public Soc_Client()
        {
            
        }

        /// <summary>
        /// 기본생성자
        /// </summary>
        /// <param name="_client">
        /// 클라이언의 세션
        /// </param>
        /// <param name="_tel">
        /// 클라이언트 수신기 전화번호
        /// </param>
        public Soc_Client(TcpClient _client, string _tel, bool _st)
        {
            this.client = _client;
            this.tel_num = _tel;
            this.cState = _st;
        }
    }
}
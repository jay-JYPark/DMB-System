using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DmbProtocol;
using System.Configuration;

namespace MUX
{
    public class MUXMsg
    {
        #region 멤버 변수
        public string genId { get; set; }
        public bool D2Flag { get; set; }
        public int TcId { get; set; }
        public byte[] message { get; set; }
        public int sendCount { get; set; }
        //public bool endFlag { get; set; }
        public string sessionId { get; set; } //추후 client에 데이터 전송을 해주기 위함.
        //public int segHeader { get; set; } //segment Header
        #endregion

        /// <summary>
        /// 기본 생성자
        /// </summary>
        public MUXMsg()
        {
            //1. D2Flag
            this.D2Flag = false;

            //2. TcId
            this.TcId = Convert.ToInt32(ConfigurationManager.AppSettings["MUXTcId"]);

            //3. message
            this.message = null;

            //4. sendCount
            this.sendCount = 0;

            //5. endFlag
            //this.endFlag = false;
        }


        /// <summary>
        ///  생성자
        /// </summary>
        public MUXMsg(string sessionId, ProtocolBase pBase)
        {
            //0. 클라이언트 아이디
            this.sessionId = sessionId;

            //1. D2Flag = 0 제어 관제
            //2. D2Flag = 1 메세지

            //(특수, 일반 메세지)
            if (pBase.command.Equals(0x11))
            {
                //1.1 특수 메세지
                this.D2Flag = false;

                //3. message
                Protocol11 p11 = (Protocol11)pBase;
                this.genId = p11.generationId.ToString();
                this.message = p11.bodyData;
            }
            else if (pBase.command.Equals(0x12))
            {
                //1.1 일반 메세지
                this.D2Flag = false;

                //3. message
                Protocol12 p12 = (Protocol12)pBase;
                this.genId = p12.generationId.ToString();
                this.message = p12.bodyData;
            }
            else
            {
                //1.2 제어(상테체크) 메세지
                this.D2Flag = true;
            }

            //2. TcId
            this.TcId = 7;

            //4. sendCount
            this.sendCount = 0;

            //5. endFlag
            //this.endFlag = false;
        }


        /// <summary>
        /// MUX 메세지를 만들어서 보내준다.
        /// </summary>
        /// <returns></returns>
        public static MUXMsg MakeMUXMsg(string sessionId, ProtocolBase pBase)
        {

            //Segment Data 넣기
            MUXMsg muxMsg = new MUXMsg(sessionId, pBase);

            return muxMsg;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

using ADEng.Library.DMB;
using ADEng.Control.DMB;
using ADEng.dmbcomm;
using TcpCommon;
using DmbProtocol;

namespace DmbDSSystem
{
    class Secession
    {
        #region Instance
        DataManager datamanager = null;
        SocketClient soc = null;
        RecvMng recvmsg = null;
        LogManager logmanager = null;
        #endregion
       
        #region Variable
        private List<WMessage> inglist = null; //발령 메시지 리스트
        private int inglistNum = 0;
        #endregion

        public Secession()
        {
            datamanager = DataManager.getInstance();
            soc = SocketClient.getInstance();
            inglist = new List<WMessage>();
            recvmsg = RecvMng.getInstance();
            logmanager = new LogManager();
        }

        public List<WMessage> Inglist
        {
            get { return this.inglist; }
            set { this.inglist = value; }
        }

        public int InglistNum
        {
            get { return this.inglistNum; }
            set { this.inglistNum = value; }
        }

        #region 발령 중 리스트 초기화
        /// <summary>
        /// 발령 중 리스트 초기화
        /// </summary>
        /// <param name="ingwmsg"></param>
        public void IngWMessage(List<WMessage> ingwmsg)
        {
            foreach (WMessage wm in ingwmsg)
            {
                if ((wm.SOPT_TTS == true) && (wm.SOPT_DMB == false))
                {
                    lock (inglist)
                    {
                        inglist.Add(wm);
                        inglistNum++;
                    }
                }
                else
                {
                    lock (inglist)
                    {
                        inglist.Add(wm);                            
                        inglistNum += 2;
                    }
                }
            }
        }
        #endregion
        
        /// <summary>
        /// 강제 종료 후에 성공적이면 종류에 따라 - 시킴
        /// </summary>
        /// <param name="wm"></param>
        public void SetInglistNum(WMessage wm)
        {
            if (inglist[inglist.IndexOf(wm)].SOPT_TTS == true && inglist[inglist.IndexOf(wm)].SOPT_DMB == false)
            {
                inglistNum--;
            }
            else
            {
                inglistNum -= 2;
            }
        }

        /// <summary>
        /// 새로 들어온 발령메시지를 발령리스트 리스트에 넣는다.
        /// </summary>
        /// <param name="wm"></param>
        /// <returns></returns>
        public void InsertWMsg(WMessage wm)
        {
            if ((wm.SOPT_TTS == true) && (wm.SOPT_DMB == false))
            {
                lock (inglist)
                {
                    inglist.Add(wm);
                }
                inglistNum++;
            }
            else
            {
                lock (inglist)
                {
                    inglist.Add(wm);                        
                }
                inglistNum += 2;
            }
        }

        /// <summary>
        /// 발령메시지의 Sendflag를 받아 1과 2로 구분한다.
        /// </summary>
        /// <param name="wm"></param>
        /// <remarks>
        /// 1은 TTS와 TTS/SMS, 나머지는 2를 반환한다.
        /// </remarks>
        /// <returns></returns>
        public int SendFlagNum(WMessage wm)
        {
            int sendflagNum = 0;
            if ((wm.SOPT_TTS == true) && (wm.SOPT_DMB == false))
            {
                sendflagNum = 1;
            }
            else
            {
                sendflagNum = 2;
            }

            return sendflagNum;
        }

        /// <summary>
        /// 재난 메시지를 판단해 일반, 특수수신기, 저장메시지, 사이렌을 구분해 전송한다.
        /// </summary>
        /// <param name="wmessage"></param>
        public void TcpSendWmsg(WMessage _wmessage)
        {
            try
            {
                WMessage wmessage = _wmessage;
                this.PrintSendMsg("TcpSendWmsg 메소드 들어온 직후 - " + DateTime.Now);
                
                //logmanager.Dir_Mng();

                //#region 로그 etcData 셋팅
                //string sopt = string.Empty;

                //if (wmessage.SOPT_CONTROL) { sopt += "제어 "; }
                //if (wmessage.SOPT_DMB) { sopt += "일반메시지 "; }
                //if (wmessage.SOPT_LIVE) { sopt += "라이브 "; }
                //if (wmessage.SOPT_SMS) { sopt += "SMS "; }
                //if (wmessage.SOPT_STOREDMESSAGE) { sopt += "저장메시지 "; }
                //if (wmessage.SOPT_TTS) { sopt += "TTS "; }
                //if (wmessage.SOPT_WARNING) { sopt += "사이렌 "; }

                //string etcData = string.Format("메시지ID - {0}, 발령시간 - {1}, 송출이 받은시간 - {2}, 발령구분 - {3}",
                    //wmessage.ID, wmessage.DDateTime, DateTime.Now, sopt);
                //#endregion

                if (((wmessage.SOPT_TTS) && (wmessage.SOPT_DMB)) || ((wmessage.SOPT_STOREDMESSAGE) && (wmessage.SOPT_DMB)))
                {
                    //일반 발령
                    this.PrintSendMsg("일반 만들기 직전 - " + DateTime.Now.ToString());
                    Proto02 p02 = ProtoMng.GetPObject("02") as Proto02;
                    byte[] body = p02.BodyMake(wmessage);
                    byte[] totproto = p02.totMake(body);
                    this.PrintSendMsg("일반 만들기 직후 - " + DateTime.Now);

                    soc.tcpClientSndMsg(totproto);
                    this.PrintSendMsg("일반 만들기 후 전송 직후 - " + DateTime.Now);
                    //logmanager.File_Mng(etcData + ", EWS편성으로 전송하는 일반 발령메시지", this.LogFileWrite(totproto));

                    //특수수신기 발령
                    this.PrintSendMsg("특수 만들기 직전 - " + DateTime.Now);
                    Proto01 p01 = ProtoMng.GetPObject("01") as Proto01;
                    byte[] devbody = p01.BodyMake(wmessage);
                    byte[] devtotproto = p01.totMake(devbody);
                    this.PrintSendMsg("특수 만들기 직후 - " + DateTime.Now);

                    soc.tcpClientSndMsg(devtotproto);
                    this.PrintSendMsg("특수 만들기 후 전송 직후 - " + DateTime.Now);
                    //logmanager.File_Mng(etcData + ", EWS편성으로 전송하는 특수수신기 발령메시지", this.LogFileWrite(devtotproto));
                }
                else if (wmessage.SOPT_DMB || ((wmessage.SOPT_SMS) && (wmessage.SOPT_DMB)) || ((wmessage.SOPT_WARNING) && (wmessage.SOPT_DMB)))
                {
                    //일반 발령
                    this.PrintSendMsg("일반 만들기 직전 - " + DateTime.Now);
                    Proto02 p02 = ProtoMng.GetPObject("02") as Proto02;
                    byte[] body = p02.BodyMake(wmessage);
                    byte[] totproto = p02.totMake(body);
                    this.PrintSendMsg("일반 만들기 직후 - " + DateTime.Now);

                    soc.tcpClientSndMsg(totproto);
                    this.PrintSendMsg("일반 만들기 후 전송 직후 - " + DateTime.Now);
                    //logmanager.File_Mng(etcData + ", EWS편성으로 전송하는 일반 발령메시지", this.LogFileWrite(totproto));

                    if (wmessage.SOPT_WARNING)
                    {
                        Proto01 p01 = ProtoMng.GetPObject("01") as Proto01;
                        byte[] devbody = p01.BodyMake(wmessage);
                        byte[] devtotproto = p01.totMake(devbody);

                        soc.tcpClientSndMsg(devtotproto);
                        //logmanager.File_Mng(etcData + ", EWS편성으로 전송하는 특수수신기 발령메시지", this.LogFileWrite(devtotproto));
                    }
                }
                else if (wmessage.SOPT_CONTROL || wmessage.SOPT_STOREDMESSAGE || wmessage.SOPT_WARNING || wmessage.SOPT_TTS)
                {
                    //특수수신기 발령
                    this.PrintSendMsg("특수 만들기 직전 - " + DateTime.Now);
                    Proto01 p01 = ProtoMng.GetPObject("01") as Proto01;
                    byte[] devbody = p01.BodyMake(wmessage);
                    byte[] devtotproto = p01.totMake(devbody);
                    this.PrintSendMsg("특수 만들기 직후 - " + DateTime.Now);

                    soc.tcpClientSndMsg(devtotproto);
                    this.PrintSendMsg("특수 만들기 후 전송 직후 - " + DateTime.Now);
                    //logmanager.File_Mng(etcData + ", EWS편성으로 전송하는 특수수신기 발령메시지", this.LogFileWrite(devtotproto));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Secession.TcpSendWmsg(WMessage wmessage) - " + ex.Message);
            }
        }

        private void PrintSendMsg(byte[] totalbyte)
        {
            try
            {
                string stmp = ProtoMng.Byte2Hex(totalbyte);
                string tmpstmp = " >> [" + stmp + "]\r\nSend Message Length >> " + totalbyte.Length + "\r\n";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Secession.PrintSendMsg - " + ex.Message);
            }
        }

        private void PrintSendMsg(String _data)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("Secession.PrintSendMsg - " + ex.Message);
            }
        }

        private string LogFileWrite(byte[] totalbyte)
        {
            try
            {
                string stmp = ProtoMng.Byte2Hex(totalbyte);

                return stmp;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Secession.LogFileWrite - " + ex.Message);
                return string.Empty;
            }
        }
    }
}
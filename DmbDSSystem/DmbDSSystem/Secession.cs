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
        private List<WMessage> inglist = null; //�߷� �޽��� ����Ʈ
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

        #region �߷� �� ����Ʈ �ʱ�ȭ
        /// <summary>
        /// �߷� �� ����Ʈ �ʱ�ȭ
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
        /// ���� ���� �Ŀ� �������̸� ������ ���� - ��Ŵ
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
        /// ���� ���� �߷ɸ޽����� �߷ɸ���Ʈ ����Ʈ�� �ִ´�.
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
        /// �߷ɸ޽����� Sendflag�� �޾� 1�� 2�� �����Ѵ�.
        /// </summary>
        /// <param name="wm"></param>
        /// <remarks>
        /// 1�� TTS�� TTS/SMS, �������� 2�� ��ȯ�Ѵ�.
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
        /// �糭 �޽����� �Ǵ��� �Ϲ�, Ư�����ű�, ����޽���, ���̷��� ������ �����Ѵ�.
        /// </summary>
        /// <param name="wmessage"></param>
        public void TcpSendWmsg(WMessage _wmessage)
        {
            try
            {
                WMessage wmessage = _wmessage;
                this.PrintSendMsg("TcpSendWmsg �޼ҵ� ���� ���� - " + DateTime.Now);
                
                //logmanager.Dir_Mng();

                //#region �α� etcData ����
                //string sopt = string.Empty;

                //if (wmessage.SOPT_CONTROL) { sopt += "���� "; }
                //if (wmessage.SOPT_DMB) { sopt += "�Ϲݸ޽��� "; }
                //if (wmessage.SOPT_LIVE) { sopt += "���̺� "; }
                //if (wmessage.SOPT_SMS) { sopt += "SMS "; }
                //if (wmessage.SOPT_STOREDMESSAGE) { sopt += "����޽��� "; }
                //if (wmessage.SOPT_TTS) { sopt += "TTS "; }
                //if (wmessage.SOPT_WARNING) { sopt += "���̷� "; }

                //string etcData = string.Format("�޽���ID - {0}, �߷ɽð� - {1}, ������ �����ð� - {2}, �߷ɱ��� - {3}",
                    //wmessage.ID, wmessage.DDateTime, DateTime.Now, sopt);
                //#endregion

                if (((wmessage.SOPT_TTS) && (wmessage.SOPT_DMB)) || ((wmessage.SOPT_STOREDMESSAGE) && (wmessage.SOPT_DMB)))
                {
                    //�Ϲ� �߷�
                    this.PrintSendMsg("�Ϲ� ����� ���� - " + DateTime.Now.ToString());
                    Proto02 p02 = ProtoMng.GetPObject("02") as Proto02;
                    byte[] body = p02.BodyMake(wmessage);
                    byte[] totproto = p02.totMake(body);
                    this.PrintSendMsg("�Ϲ� ����� ���� - " + DateTime.Now);

                    soc.tcpClientSndMsg(totproto);
                    this.PrintSendMsg("�Ϲ� ����� �� ���� ���� - " + DateTime.Now);
                    //logmanager.File_Mng(etcData + ", EWS������ �����ϴ� �Ϲ� �߷ɸ޽���", this.LogFileWrite(totproto));

                    //Ư�����ű� �߷�
                    this.PrintSendMsg("Ư�� ����� ���� - " + DateTime.Now);
                    Proto01 p01 = ProtoMng.GetPObject("01") as Proto01;
                    byte[] devbody = p01.BodyMake(wmessage);
                    byte[] devtotproto = p01.totMake(devbody);
                    this.PrintSendMsg("Ư�� ����� ���� - " + DateTime.Now);

                    soc.tcpClientSndMsg(devtotproto);
                    this.PrintSendMsg("Ư�� ����� �� ���� ���� - " + DateTime.Now);
                    //logmanager.File_Mng(etcData + ", EWS������ �����ϴ� Ư�����ű� �߷ɸ޽���", this.LogFileWrite(devtotproto));
                }
                else if (wmessage.SOPT_DMB || ((wmessage.SOPT_SMS) && (wmessage.SOPT_DMB)) || ((wmessage.SOPT_WARNING) && (wmessage.SOPT_DMB)))
                {
                    //�Ϲ� �߷�
                    this.PrintSendMsg("�Ϲ� ����� ���� - " + DateTime.Now);
                    Proto02 p02 = ProtoMng.GetPObject("02") as Proto02;
                    byte[] body = p02.BodyMake(wmessage);
                    byte[] totproto = p02.totMake(body);
                    this.PrintSendMsg("�Ϲ� ����� ���� - " + DateTime.Now);

                    soc.tcpClientSndMsg(totproto);
                    this.PrintSendMsg("�Ϲ� ����� �� ���� ���� - " + DateTime.Now);
                    //logmanager.File_Mng(etcData + ", EWS������ �����ϴ� �Ϲ� �߷ɸ޽���", this.LogFileWrite(totproto));

                    if (wmessage.SOPT_WARNING)
                    {
                        Proto01 p01 = ProtoMng.GetPObject("01") as Proto01;
                        byte[] devbody = p01.BodyMake(wmessage);
                        byte[] devtotproto = p01.totMake(devbody);

                        soc.tcpClientSndMsg(devtotproto);
                        //logmanager.File_Mng(etcData + ", EWS������ �����ϴ� Ư�����ű� �߷ɸ޽���", this.LogFileWrite(devtotproto));
                    }
                }
                else if (wmessage.SOPT_CONTROL || wmessage.SOPT_STOREDMESSAGE || wmessage.SOPT_WARNING || wmessage.SOPT_TTS)
                {
                    //Ư�����ű� �߷�
                    this.PrintSendMsg("Ư�� ����� ���� - " + DateTime.Now);
                    Proto01 p01 = ProtoMng.GetPObject("01") as Proto01;
                    byte[] devbody = p01.BodyMake(wmessage);
                    byte[] devtotproto = p01.totMake(devbody);
                    this.PrintSendMsg("Ư�� ����� ���� - " + DateTime.Now);

                    soc.tcpClientSndMsg(devtotproto);
                    this.PrintSendMsg("Ư�� ����� �� ���� ���� - " + DateTime.Now);
                    //logmanager.File_Mng(etcData + ", EWS������ �����ϴ� Ư�����ű� �߷ɸ޽���", this.LogFileWrite(devtotproto));
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
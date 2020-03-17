using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using ADEng.Control.DMB;
using ADEng.Library.DMB;
using TcpCommon;
using DmbProtocol;

namespace DmbDSSystem
{
    class LostWMsgMng
    {
        #region Instance
        private DataManager datamng = null;
        private SocketClient soc = null;
        private Secession secession = null;
        #endregion

        public LostWMsgMng()
        {
            datamng = DataManager.getInstance();
            soc = SocketClient.getInstance();
            secession = new Secession();
        }

        /// <summary>
        /// 미전송 재난메시지를 전송한다.
        /// </summary>
        public void LostWMessage()
        {
            try
            {
                this.datamng.SetBroadWmsgList((uint)Properties.Settings.Default.cnfBroad);

                if (datamng.BroadWmsgList != null)
                {
                    for (int i = 0; i < datamng.BroadWmsgList.Count; i++)
                    {
                        if (datamng.BroadWmsgList[i].SendPart != datamng.BroadWmsgList[i].CommitPart)
                        {
                            if (datamng.BroadWmsgList[i].SendPart == WMessage.E_sendPart.SMS)
                            {
                                continue;
                            }
                            if (datamng.BroadWmsgList[i].AbortStatus == (byte)WMessage.E_AbortStatus.Request)
                            {
                                continue;
                            }

                            if (datamng.BroadWmsgList[i].BCenterList.Count != 0)
                            {
                                for (int h = 0; h < datamng.BroadWmsgList[i].BCenterList.Count; h++)
                                {
                                    if (datamng.BroadWmsgList[i].BCenterList[h].StageID != (uint)Properties.Settings.Default.cnfBroad)
                                    {
                                        continue;
                                    }

                                    this.secession.TcpSendWmsg(datamng.BroadWmsgList[i]);
                                }
                            }
                            else
                            {
                                this.secession.TcpSendWmsg(datamng.BroadWmsgList[i]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("LostWMsgMng.LostWMessage - " + ex.Message);
            }
        }

        /// <summary>
        /// 송출이 응답하지 못한 재난메시지를 검색해 응답한다.
        /// </summary>
        public uint DSRecvWmsg()
        {
            try
            {
                this.datamng.SetBroadWmsgList((uint)Properties.Settings.Default.cnfBroad);
                uint tmp = 0;

                if (datamng.BroadWmsgList != null)
                {
                    for (int i = 0; i < datamng.BroadWmsgList.Count; i++)
                    {
                        uint tmpsr = 0;

                        for (int j = 0; j < this.datamng.ServerList.Count; j++)
                        {
                            if (this.datamng.ServerList[j].Name == Properties.Settings.Default.cnfClientName)
                            {
                                tmpsr = this.datamng.ServerList[j].TkServer;
                            }
                        }

                        bool tmpstate = false;

                        for (int k = 0; k < datamng.BroadWmsgList[i].serverResponseList.Count; k++)
                        {
                            if (datamng.BroadWmsgList[i].serverResponseList[k].TkServer == tmpsr)
                            {
                                tmpstate = true;
                                continue;
                            }
                        }

                        if (datamng.BroadWmsgList[i].serverResponseList.Count == 0 || (tmpstate == false))
                        {
                            if (!(this.datamng.SetServerReponse(datamng.BroadWmsgList[i].ID, Properties.Settings.Default.cnfClientName, Properties.Settings.Default.cnfEWSIP)))
                            {
                                //MessageBox.Show("방송 DMB송출 시스템이 응답을 실패했습니다.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                tmp = datamng.BroadWmsgList[i].ID;
                            }
                        }
                    }
                }

                return tmp;
            }
            catch (Exception ex)
            {
                Console.WriteLine("LostWMsgMng.DSRecvWmsg - " + ex.Message);

                return 0;
            }
        }
    }
}
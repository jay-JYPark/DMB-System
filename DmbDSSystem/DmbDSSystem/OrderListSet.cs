using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using ADEng.Library.DMB;
using ADEng.Control.DMB;
using ADEng.dmbcomm;

namespace DmbDSSystem
{
    class OrderListSet
    {
        public event CancleWmsgEventHandle CancleWmsgEvt;
        public delegate void CancleWmsgEventHandle(WMessage wmsg);

        #region Instance
        DataManager datamanager = null;        
        #endregion

        #region Variable
        private int pkidIndex = 0;
        #endregion

        #region 기본생성자
        public OrderListSet()
        {
            datamanager = DataManager.getInstance();
        }
        #endregion

        public int PkidIndex
        {
            get { return this.pkidIndex; }
            set { this.pkidIndex = value; }
        }        

        #region 메시지 항목 별 셋팅 후 ListViewItem 리턴
        /// <summary>
        /// 메시지 항목 별 셋팅 후 ListViewItem 리턴
        /// </summary>
        /// <param name="wmessage"></param>
        /// <returns></returns>
        public ListViewItem setWmsgCollection(WMessage wmessage)
        {
            TypeDisaster comparee = new TypeDisaster();
            TypeDisasterIDComparer comparer = new TypeDisasterIDComparer();
            ListViewItem lvi = new ListViewItem();
            bool state = false;

            datamanager.GetdisasterList.Sort(comparer);

            if (wmessage != null)
            {
                //Name
                lvi.Name = wmessage.ID.ToString();
                //번호
                pkidIndex = pkidIndex + 1;
                lvi.Text = pkidIndex.ToString();
                //발령일시
                lvi.SubItems.Add(wmessage.DDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                //실제구분
                string tmpfact = string.Empty;
                tmpfact = (wmessage.SendMode == (byte)0) ? "시험" : (wmessage.SendMode == (byte)1) ? "훈련" : "실제";
                lvi.SubItems.Add(tmpfact);
                //재난종류
                if (wmessage.ProtoA.DisasterCode != string.Empty)
                {
                    lvi.SubItems.Add(wmessage.ProtoA.DisasterCode);
                }
                else
                {
                    lvi.SubItems.Add("재난종류 응답이상");
                }
                //지역형식
                if (wmessage.ProtoA.SectionCode == DMBBIZ.DmbDefine.DmbDefineSectionCode.National)
                {
                    lvi.SubItems.Add("전국");
                    state = true;
                }
                else if (wmessage.ProtoA.SectionCode == DMBBIZ.DmbDefine.DmbDefineSectionCode.Administrative)
                {
                    lvi.SubItems.Add("행정동");
                    state = true;
                }
                else if (wmessage.ProtoA.SectionCode == DMBBIZ.DmbDefine.DmbDefineSectionCode.Terminal)
                {
                    lvi.SubItems.Add("단말");
                    state = true;
                }
                else
                {
                    state = false;
                }

                if (state == false)
                {
                    lvi.SubItems.Add("지역형식 응답이상");
                }
                state = false;
                //발령대상 수
                if (wmessage.RCount.ToString() != null)
                {
                    lvi.SubItems.Add(wmessage.RCount.ToString());
                }
                else
                {
                    lvi.SubItems.Add("지역수 응답이상");
                }
                //발령상태
                    string tmpsendstate = "방송사";
                    
                    if (!wmessage.BoolProcessing)
                    {
                        tmpsendstate = "종료";
                    }

                    lvi.SubItems.Add(tmpsendstate);
                //발령구분
                if (wmessage.SendPart != 0)
                {
                    lvi.SubItems.Add(wmessage.SendPart.ToString());
                }
                else
                {
                    lvi.SubItems.Add("");
                }
                //방송사
                string tmp = string.Empty;
                for (int i = 0; i < wmessage.ProtoA.LstStage.Count; i++)
                {
                    if (wmessage.ProtoA.LstStage[i] == DMBBIZ.DmbDefine.DmbDefineStage.Kbs)
                    {
                        tmp += " KBS";
                    }
                    else if (wmessage.ProtoA.LstStage[i] == DMBBIZ.DmbDefine.DmbDefineStage.Mbc)
                    {
                        tmp += " MBC";
                    }
                    else if (wmessage.ProtoA.LstStage[i] == DMBBIZ.DmbDefine.DmbDefineStage.Sbs)
                    {
                        tmp += " SBS";
                    }
                    else if (wmessage.ProtoA.LstStage[i] == DMBBIZ.DmbDefine.DmbDefineStage.Ytn)
                    {
                        tmp += " YTN";
                    }
                }

                lvi.SubItems.Add(tmp);
                
                //DMB단문
                if (wmessage.Message != null)
                {
                    if (wmessage.BoolControl == true)
                    {
                        lvi.SubItems.Add("수신기 상태/제어 요청");
                    }
                    else if (wmessage.SendPart == WMessage.E_sendPart.STRD)
                    {
                        lvi.SubItems.Add("수신기 저장메시지 요청");
                    }
                    else if (wmessage.SendPart == WMessage.E_sendPart.WARN)
                    {
                        lvi.SubItems.Add("수신기 경보사이렌 요청");
                    }
                    else
                    {
                        lvi.SubItems.Add(wmessage.Message);
                    }
                }
                else
                {
                    lvi.SubItems.Add("");
                }
                //TTS단문
                if (wmessage.TTSMsg != null)
                {
                    if (wmessage.BoolControl == true)
                    {
                        lvi.SubItems.Add("수신기 상태/제어 요청");
                    }
                    else if (wmessage.SOPT_STOREDMESSAGE)
                    {
                        lvi.SubItems.Add("수신기 저장메시지 요청");
                    }
                    else if (wmessage.SendPart == WMessage.E_sendPart.WARN)
                    {
                        lvi.SubItems.Add("수신기 경보사이렌 요청");
                    }
                    else
                    {
                        lvi.SubItems.Add(wmessage.TTSMsg);
                    }
                }
                else
                {
                    lvi.SubItems.Add("");
                }
                //CBS단문
                if (wmessage.SMSMsg != null)
                {
                    lvi.SubItems.Add(wmessage.SMSMsg);
                }
                else
                {
                    lvi.SubItems.Add("");
                }
            }
           
            return lvi;
        }
        #endregion
    }
}
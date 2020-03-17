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

        #region �⺻������
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

        #region �޽��� �׸� �� ���� �� ListViewItem ����
        /// <summary>
        /// �޽��� �׸� �� ���� �� ListViewItem ����
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
                //��ȣ
                pkidIndex = pkidIndex + 1;
                lvi.Text = pkidIndex.ToString();
                //�߷��Ͻ�
                lvi.SubItems.Add(wmessage.DDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                //��������
                string tmpfact = string.Empty;
                tmpfact = (wmessage.SendMode == (byte)0) ? "����" : (wmessage.SendMode == (byte)1) ? "�Ʒ�" : "����";
                lvi.SubItems.Add(tmpfact);
                //�糭����
                if (wmessage.ProtoA.DisasterCode != string.Empty)
                {
                    lvi.SubItems.Add(wmessage.ProtoA.DisasterCode);
                }
                else
                {
                    lvi.SubItems.Add("�糭���� �����̻�");
                }
                //��������
                if (wmessage.ProtoA.SectionCode == DMBBIZ.DmbDefine.DmbDefineSectionCode.National)
                {
                    lvi.SubItems.Add("����");
                    state = true;
                }
                else if (wmessage.ProtoA.SectionCode == DMBBIZ.DmbDefine.DmbDefineSectionCode.Administrative)
                {
                    lvi.SubItems.Add("������");
                    state = true;
                }
                else if (wmessage.ProtoA.SectionCode == DMBBIZ.DmbDefine.DmbDefineSectionCode.Terminal)
                {
                    lvi.SubItems.Add("�ܸ�");
                    state = true;
                }
                else
                {
                    state = false;
                }

                if (state == false)
                {
                    lvi.SubItems.Add("�������� �����̻�");
                }
                state = false;
                //�߷ɴ�� ��
                if (wmessage.RCount.ToString() != null)
                {
                    lvi.SubItems.Add(wmessage.RCount.ToString());
                }
                else
                {
                    lvi.SubItems.Add("������ �����̻�");
                }
                //�߷ɻ���
                    string tmpsendstate = "��ۻ�";
                    
                    if (!wmessage.BoolProcessing)
                    {
                        tmpsendstate = "����";
                    }

                    lvi.SubItems.Add(tmpsendstate);
                //�߷ɱ���
                if (wmessage.SendPart != 0)
                {
                    lvi.SubItems.Add(wmessage.SendPart.ToString());
                }
                else
                {
                    lvi.SubItems.Add("");
                }
                //��ۻ�
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
                
                //DMB�ܹ�
                if (wmessage.Message != null)
                {
                    if (wmessage.BoolControl == true)
                    {
                        lvi.SubItems.Add("���ű� ����/���� ��û");
                    }
                    else if (wmessage.SendPart == WMessage.E_sendPart.STRD)
                    {
                        lvi.SubItems.Add("���ű� ����޽��� ��û");
                    }
                    else if (wmessage.SendPart == WMessage.E_sendPart.WARN)
                    {
                        lvi.SubItems.Add("���ű� �溸���̷� ��û");
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
                //TTS�ܹ�
                if (wmessage.TTSMsg != null)
                {
                    if (wmessage.BoolControl == true)
                    {
                        lvi.SubItems.Add("���ű� ����/���� ��û");
                    }
                    else if (wmessage.SOPT_STOREDMESSAGE)
                    {
                        lvi.SubItems.Add("���ű� ����޽��� ��û");
                    }
                    else if (wmessage.SendPart == WMessage.E_sendPart.WARN)
                    {
                        lvi.SubItems.Add("���ű� �溸���̷� ��û");
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
                //CBS�ܹ�
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
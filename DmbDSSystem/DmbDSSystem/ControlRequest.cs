using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using ADEng.Library.DMB;
using ADEng.Control.DMB;
using ADEng.dmbcomm;

namespace DmbDSSystem
{
    class ControlRequest
    {
        #region Instance
        DataManager controldatamng = null;
        #endregion

        #region Variable
        private WMessage wmsg = null;
        private WaitForm waitform = new WaitForm();
        private mapMessageRegion mapmsgrg = new mapMessageRegion();
        private List<mapMessageRegion> mapmsgrgList = new List<mapMessageRegion>();
        #endregion

        /// <summary>
        /// �⺻ ������
        /// </summary>
        public ControlRequest()
        {
            controldatamng = DataManager.getInstance();
        }

        /// <summary>
        /// WMessage �����.
        /// </summary>
        /// <param name="controlflag">
        /// �������̸� 1, �Ϲ� �޽����� 0
        /// </param>
        /// <param name="tkpriority">
        /// �糭 �켱���� ID
        /// </param>
        /// <param name="tkdisaster">
        /// �糭 �ڵ� ID
        /// </param>
        /// <param name="tkregion">
        /// �糭 ���� ���� �ڵ�
        /// </param>
        /// <param name="rcount">
        /// �糭 ������ ��
        /// </param>
        /// <param name="message">
        /// �޽���
        /// </param>
        /// <param name="fkuser">
        /// �߷��� �����
        /// </param>
        /// <param name="processing">
        /// ���� ���� ����(���� �� : 1, ���� ��Ȳ : 0)
        /// </param>
        /// <param name="ttsmsg">
        /// TTS�� �޽���
        /// </param>
        /// <param name="smsmsg">
        /// SMS�� �޽���
        /// </param>
        public void MakeWMSG(bool controlflag, uint tkpriority, uint tkdisaster, uint tkregion, uint rcount, string message, uint fkuser,
            bool processing, string ttsmsg, string smsmsg, DateTime datetime, uint id, List<mapMessageRegion> mapmessageregion)
        {
            try
            {
                wmsg = new WMessage();

                wmsg.BoolControl = controlflag;
                wmsg.TkPriority = tkpriority;
                wmsg.TkDisaster = tkdisaster;
                wmsg.TkRegion = tkregion;
                wmsg.RCount = rcount;
                wmsg.Message = message;
                wmsg.FkUser = fkuser;
                wmsg.BoolProcessing = processing;
                wmsg.TTSMsg = ttsmsg;
                wmsg.SMSMsg = smsmsg;
                wmsg.DDateTime = datetime;
                wmsg.ID = id;
                wmsg.MapTarget = mapmessageregion;
            }
            catch (Exception ex)
            {
                Console.WriteLine("MakeWMSG - " + ex.Message);
            }
        }

        /// <summary>
        /// ������ ��� �����ϴ� �޼ҵ�
        /// </summary>
        /// <param name="flag">
        /// 0=����, 1=�������, 2=�׷�
        /// </param>
        /// <param name="pid">
        /// �޽��� ������ destflag ������ �� 0�̸� region���̺�� ����Ű, 1�̸� device���̺�� ����Ű
        /// </param>
        /// <param name="pname">
        /// �����̸� �ּҶǴ� �̸��� ����̸� ��� �̸��� ���´�. ���� ������ ���� ����Ǿ �������� ��������
        /// </param>
        public void MakeMWMSGRG(uint flag, uint pid, string pname)
        {
            mapmsgrg.DestFlag = flag;
            mapmsgrg.ParentID = pid;
            mapmsgrg.ParentName = pname;

            this.mapmsgrgList.Add(mapmsgrg);
        }

        /// <summary>
        /// ����/���� �߷� ��û ����
        /// </summary>
        public bool Processing(string title)
        {
            try
            {
                WMessage tempmsg = new WMessage();
                bool state = false;
                
                tempmsg = controldatamng.sendWMessage(this.wmsg);

                if (tempmsg == null)
                {
                    MessageBox.Show(title + " ��û�� �����Ͽ����ϴ�.", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return false;
                }

                WaitBarMng.Start();
                WaitBarMng.Msg = "������ ������ �Դϴ�...";
                System.Threading.Thread.Sleep(1000);

                if (tempmsg.ID != (uint)0)
                {
                    WaitBarMng.Close();
                    MessageBox.Show(title + " ��û�� �����Ͽ����ϴ�.", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    state = true;
                }
                else
                {
                    WaitBarMng.Close();
                    MessageBox.Show(title + " ��û�� �����Ͽ����ϴ�.", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    state = false;
                }

                wmsg = null;

                return state;
            }
            catch (Exception ex)
            {
                WaitBarMng.Close();
                MessageBox.Show(title + " ��û�� �����Ͽ����ϴ�.", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Console.WriteLine("Processing - " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// mapmsgrgList�� count�� ��ȯ�Ѵ�. ������ ���� �ǹ��Ѵ�.
        /// </summary>
        public uint MapmsgrgList
        {
            get
            {
                return (uint)mapmsgrgList.Count;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DmbDSSystem
{
    class ControlSet
    {
        public void IssuesInitCtrl(ListView lv)
        {
            #region �߷ɳ��� ListView �ʱ�ȭ
            ColumnHeader wmsgpkid = new ColumnHeader();
            wmsgpkid.Text = "��ȣ";
            wmsgpkid.Width = 43;
            lv.Columns.Add(wmsgpkid);

            ColumnHeader wmsgDate = new ColumnHeader();
            wmsgDate.Text = "�߷��Ͻ�";
            wmsgDate.Width = 130;
            lv.Columns.Add(wmsgDate);

            ColumnHeader wmsgFact = new ColumnHeader();
            wmsgFact.Text = "��������";
            wmsgFact.Width = 60;
            lv.Columns.Add(wmsgFact);

            ColumnHeader wmsgKind = new ColumnHeader();
            wmsgKind.Text = "�糭����";
            wmsgKind.Width = 120;
            lv.Columns.Add(wmsgKind);

            ColumnHeader wmsgRegion = new ColumnHeader();
            wmsgRegion.Text = "��������";
            wmsgRegion.Width = 120;
            lv.Columns.Add(wmsgRegion);

            ColumnHeader wmsgRegionCount = new ColumnHeader();
            wmsgRegionCount.Text = "�߷ɴ�� ��";
            wmsgRegionCount.Width = 80;
            lv.Columns.Add(wmsgRegionCount);

            ColumnHeader wmsgIng = new ColumnHeader();
            wmsgIng.Text = "�߷ɻ���";
            wmsgIng.Width = 90;
            lv.Columns.Add(wmsgIng);

            ColumnHeader wmsgIssueKind = new ColumnHeader();
            wmsgIssueKind.Text = "�߷ɱ���";
            wmsgIssueKind.Width = 110;
            lv.Columns.Add(wmsgIssueKind);

            ColumnHeader wmsgBroad = new ColumnHeader();
            wmsgBroad.Text = "��ۻ�";
            wmsgBroad.Width = 150;
            lv.Columns.Add(wmsgBroad);

            ColumnHeader wmsgMessage = new ColumnHeader();
            wmsgMessage.Text = "DMB�ܹ�";
            wmsgMessage.Width = 150;
            lv.Columns.Add(wmsgMessage);

            ColumnHeader wmsgTts = new ColumnHeader();
            wmsgTts.Text = "TTS�ܹ�";
            wmsgTts.Width = 150;
            lv.Columns.Add(wmsgTts);

            ColumnHeader wmsgSms = new ColumnHeader();
            wmsgSms.Text = "CBS�ܹ�";
            wmsgSms.Width = 150;
            lv.Columns.Add(wmsgSms);

            //ColumnHeader wmsgUser = new ColumnHeader();
            //wmsgUser.Text = "�߷���";
            //wmsgUser.Width = 80;
            //lv.Columns.Add(wmsgUser);

            //ColumnHeader wmsgPriority = new ColumnHeader();
            //wmsgPriority.Text = "�켱����";
            //wmsgPriority.Width = 70;
            //lv.Columns.Add(wmsgPriority);

            //ColumnHeader wmsgId = new ColumnHeader();
            //wmsgId.Text = "PKID";
            //wmsgId.Width = 0; //����Ʈ���� �÷����δ� [12]
            //lv.Columns.Add(wmsgId);
            #endregion
        }

        /// <summary>
        /// ����Ʈ���� �ε����� ó������ �ٽ� �����Ѵ�.
        /// </summary>
        /// <param name="lv"></param>
        public void SetListViewIndex(ListView lv)
        {
            for (int i = 0; i < lv.Items.Count; i++)
            {
                int c = i + 1;
                lv.Items[i].SubItems[0].Text = c.ToString();
            }
        }
    }
}

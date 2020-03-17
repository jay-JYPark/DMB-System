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
            #region 발령내역 ListView 초기화
            ColumnHeader wmsgpkid = new ColumnHeader();
            wmsgpkid.Text = "번호";
            wmsgpkid.Width = 43;
            lv.Columns.Add(wmsgpkid);

            ColumnHeader wmsgDate = new ColumnHeader();
            wmsgDate.Text = "발령일시";
            wmsgDate.Width = 130;
            lv.Columns.Add(wmsgDate);

            ColumnHeader wmsgFact = new ColumnHeader();
            wmsgFact.Text = "실제구분";
            wmsgFact.Width = 60;
            lv.Columns.Add(wmsgFact);

            ColumnHeader wmsgKind = new ColumnHeader();
            wmsgKind.Text = "재난종류";
            wmsgKind.Width = 120;
            lv.Columns.Add(wmsgKind);

            ColumnHeader wmsgRegion = new ColumnHeader();
            wmsgRegion.Text = "지역형식";
            wmsgRegion.Width = 120;
            lv.Columns.Add(wmsgRegion);

            ColumnHeader wmsgRegionCount = new ColumnHeader();
            wmsgRegionCount.Text = "발령대상 수";
            wmsgRegionCount.Width = 80;
            lv.Columns.Add(wmsgRegionCount);

            ColumnHeader wmsgIng = new ColumnHeader();
            wmsgIng.Text = "발령상태";
            wmsgIng.Width = 90;
            lv.Columns.Add(wmsgIng);

            ColumnHeader wmsgIssueKind = new ColumnHeader();
            wmsgIssueKind.Text = "발령구분";
            wmsgIssueKind.Width = 110;
            lv.Columns.Add(wmsgIssueKind);

            ColumnHeader wmsgBroad = new ColumnHeader();
            wmsgBroad.Text = "방송사";
            wmsgBroad.Width = 150;
            lv.Columns.Add(wmsgBroad);

            ColumnHeader wmsgMessage = new ColumnHeader();
            wmsgMessage.Text = "DMB단문";
            wmsgMessage.Width = 150;
            lv.Columns.Add(wmsgMessage);

            ColumnHeader wmsgTts = new ColumnHeader();
            wmsgTts.Text = "TTS단문";
            wmsgTts.Width = 150;
            lv.Columns.Add(wmsgTts);

            ColumnHeader wmsgSms = new ColumnHeader();
            wmsgSms.Text = "CBS단문";
            wmsgSms.Width = 150;
            lv.Columns.Add(wmsgSms);

            //ColumnHeader wmsgUser = new ColumnHeader();
            //wmsgUser.Text = "발령자";
            //wmsgUser.Width = 80;
            //lv.Columns.Add(wmsgUser);

            //ColumnHeader wmsgPriority = new ColumnHeader();
            //wmsgPriority.Text = "우선순위";
            //wmsgPriority.Width = 70;
            //lv.Columns.Add(wmsgPriority);

            //ColumnHeader wmsgId = new ColumnHeader();
            //wmsgId.Text = "PKID";
            //wmsgId.Width = 0; //리스트뷰의 컬럼으로는 [12]
            //lv.Columns.Add(wmsgId);
            #endregion
        }

        /// <summary>
        /// 리스트뷰의 인덱스를 처음부터 다시 셋팅한다.
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

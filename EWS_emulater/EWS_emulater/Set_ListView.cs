using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace EWS_emulater
{
    class Set_ListView
    {
        /// <summary>
        /// 일반메시지 - 리스트뷰에 컬럼을 셋팅한다(No.1)
        /// </summary>
        /// <param name="lv"></param>
        public void SetColumn(ListView lv)
        {
            ColumnHeader wmsgpkid = new ColumnHeader();
            wmsgpkid.Text = "번호";
            wmsgpkid.Width = 40;
            lv.Columns.Add(wmsgpkid);

            ColumnHeader wmsgDate = new ColumnHeader();
            wmsgDate.Text = "발령일시";
            wmsgDate.Width = 160;
            lv.Columns.Add(wmsgDate);

            ColumnHeader wmsgKind = new ColumnHeader();
            wmsgKind.Text = "재난종류";
            wmsgKind.Width = 90;
            lv.Columns.Add(wmsgKind);

            ColumnHeader wmsgIng = new ColumnHeader();
            wmsgIng.Text = "발령상태";
            wmsgIng.Width = 110;
            lv.Columns.Add(wmsgIng);

            ColumnHeader wmsgMessage = new ColumnHeader();
            wmsgMessage.Text = "DMB단문";
            wmsgMessage.Width = 320;
            lv.Columns.Add(wmsgMessage);

            ColumnHeader wmsgId = new ColumnHeader();
            wmsgId.Text = "PKID";
            wmsgId.Width = 75;
            lv.Columns.Add(wmsgId);
        }

        /// <summary>
        /// 특수메시지 - 리스트뷰에 컬럼을 셋팅한다(No.2)
        /// </summary>
        /// <param name="lv"></param>
        public void SetColumn1(ListView lv)
        {
            ColumnHeader wmsgpkid = new ColumnHeader();
            wmsgpkid.Text = "번호";
            wmsgpkid.Width = 40;
            lv.Columns.Add(wmsgpkid);

            ColumnHeader wmsgDate = new ColumnHeader();
            wmsgDate.Text = "발령일시";
            wmsgDate.Width = 160;
            lv.Columns.Add(wmsgDate);

            ColumnHeader wmsgKind = new ColumnHeader();
            wmsgKind.Text = "재난종류";
            wmsgKind.Width = 90;
            lv.Columns.Add(wmsgKind);

            ColumnHeader wmsgIng = new ColumnHeader();
            wmsgIng.Text = "발령상태";
            wmsgIng.Width = 110;
            lv.Columns.Add(wmsgIng);

            ColumnHeader wmsgTts = new ColumnHeader();
            wmsgTts.Text = "TTS단문";
            wmsgTts.Width = 320;
            lv.Columns.Add(wmsgTts);

            ColumnHeader wmsgId = new ColumnHeader();
            wmsgId.Text = "PKID";
            wmsgId.Width = 75;
            lv.Columns.Add(wmsgId);
        }

        /// <summary>
        /// 리스트뷰의 인덱스를 셋팅한다.
        /// </summary>
        /// <param name="lv"></param>
        public void SetIndex(ListView lv)
        {
            for (int i = 0; i < lv.Items.Count; i++)
            {
                int c = i + 1;
                lv.Items[i].SubItems[0].Text = c.ToString();
            }
        }

        /// <summary>
        /// 메시지를 ListView에 아이템을 삽입한다.
        /// </summary>
        /// <param name="utc"></param>
        /// <param name="dis"></param>
        /// <param name="sts"></param>
        /// <param name="msg"></param>
        /// <param name="pkid"></param>
        public ListViewItem SetNorListView(long utc, string dis, string sts, string msg, int pkid)
        {
            ListViewItem lvi = new ListViewItem();

            lvi.Name = pkid.ToString();

            DateTime datet = new DateTime(1970, 1, 1, 9, 0, 0);
            TimeSpan timeSecond0 = new TimeSpan(datet.Ticks);
            TimeSpan timeSecond1 = TimeSpan.FromSeconds(timeSecond0.TotalSeconds + utc);
            DateTime resultTime = new DateTime(timeSecond1.Ticks);

            lvi.SubItems.Add(resultTime.ToString());
            lvi.SubItems.Add(dis);
            lvi.SubItems.Add(sts);
            lvi.SubItems.Add(msg);
            lvi.SubItems.Add(pkid.ToString());

            return lvi;
        }

        /// <summary>
        /// Key값을 받아 해당 Key로 포커싱과 스크롤을 이동한다.
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="_id"></param>
        public void SetFoucus(ListView lv, int _id)
        {
            for (int i = 0; i < lv.Items.Count; i++)
            {
                lv.Items[i].Selected = false;
                lv.Items[i].Focused = false;
            }

            lv.Items[_id.ToString()].Selected = true;
            lv.Items[_id.ToString()].Focused = true;
            lv.Items[_id.ToString()].EnsureVisible();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ADEng.Library.DMB;

namespace WMsgTest
{
    public partial class DmbTypeDisasterDlg : Form
    {
        private DataManager dataMng = null;

        public delegate void DelegateSelectDisasterListViewItem(ListViewItem _item);
        public event DelegateSelectDisasterListViewItem delegateSelectDisasterListViewItem = null;

        public DmbTypeDisasterDlg()
        {
            InitializeComponent();
        }

        private void DmbTypeDisasterDlg_Load_1(object sender, EventArgs e)
        {
            this.dataMng = DataManager.getInstance();
            this.initListView();
            this.initListViewData();
        }

        /// <summary>
        /// 재난종류 리스트 초기화
        /// </summary>
        private void initListView()
        {
            ColumnHeader id = new ColumnHeader();
            id.Width = 0;
            this.listViewDisasterList.Columns.Add(id);

            ColumnHeader guboon = new ColumnHeader();
            guboon.Width = 100;
            guboon.Text = "구분";
            this.listViewDisasterList.Columns.Add(guboon);

            ColumnHeader yac = new ColumnHeader();
            yac.Width = 50;
            yac.Text = "약어";
            this.listViewDisasterList.Columns.Add(yac);

            ColumnHeader stat = new ColumnHeader();
            stat.Width = 150;
            stat.Text = "재난상황";
            this.listViewDisasterList.Columns.Add(stat);
        }

        /// <summary>
        /// 재난종류 리스트에 데이터를 초기화한다.
        /// </summary>
        private void initListViewData()
        {
            if (this.dataMng.TypeDisasterList.Count < 1)
            {
                return;
            }

            foreach (TypeDisaster disItem in this.dataMng.TypeDisasterList)
            {
                ListViewItem item = new ListViewItem();
                item.Name = disItem.ID.ToString();

                ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem();
                subItem.Text = disItem.DisplayPart.ToString();
                item.SubItems.Add(subItem);

                subItem = new ListViewItem.ListViewSubItem();
                subItem.Text = disItem.Code.ToString();
                item.SubItems.Add(subItem);

                subItem = new ListViewItem.ListViewSubItem();
                subItem.Text = disItem.Name.ToString();
                item.SubItems.Add(subItem);

                this.listViewDisasterList.Items.Add(item);
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listViewDisasterList_DoubleClick_1(object sender, EventArgs e)
        {
            if (this.listViewDisasterList.SelectedItems.Count < 1)
            {
                MessageBox.Show("재난종류를 선택하세요", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ListViewItem item = this.listViewDisasterList.SelectedItems[0];
            delegateSelectDisasterListViewItem(item);

            this.btnClose_Click_1(sender, e);
        }
    }
}
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
    public partial class DmbTypeDevice : Form
    {
        private DataManager dataMng = null;

        public delegate void DelegateSelectDeviceListViewItem(ListViewItem _item);
        public event DelegateSelectDeviceListViewItem delegateSelectDeviceListViewItem = null;
        
        public DmbTypeDevice()
        {

            InitializeComponent();
        }

        private void DmbTypeDevice_Load(object sender, EventArgs e)
        {
            this.dataMng = DataManager.getInstance();
            this.initListView();
            this.initListViewData();
        }

        /// <summary>
        /// 수신기 리스트 초기화
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
            yac.Width = 135;
            yac.Text = "수신기명";
            this.listViewDisasterList.Columns.Add(yac);

            ColumnHeader stat = new ColumnHeader();
            stat.Width = 180;
            stat.Text = "수신기위치";
            this.listViewDisasterList.Columns.Add(stat);

            ColumnHeader cell = new ColumnHeader();
            cell.Width = 90;
            cell.Text = "전화번호";
            this.listViewDisasterList.Columns.Add(cell);
        }

        /// <summary>
        /// 수신기 리스트에 데이터를 초기화한다.
        /// </summary>
        private void initListViewData()
        {
            if (this.dataMng.TypeDisasterList.Count < 1)
            {
                return;
            }
            RegionInfo rcomparee = new RegionInfo();
            RegionInfoIDComparer rcomparer = new RegionInfoIDComparer();
            DeviceInfoIDComparer comparer = new DeviceInfoIDComparer();

            string tot = string.Empty;
            string prov = string.Empty;
            string dist = string.Empty;
            string area = string.Empty;

            dataMng.DeviceInfoList.Sort(comparer);
            dataMng.RegionInfoList.Sort(rcomparer);

            foreach (DeviceInfo disItem in this.dataMng.DeviceInfoList)
            {
                tot = string.Empty;
                prov = string.Empty;
                dist = string.Empty;
                area = string.Empty;

                rcomparee.ID = disItem.FkRegion;
                RegionInfo ri = dataMng.RegionInfoList[dataMng.RegionInfoList.BinarySearch(rcomparee, rcomparer)];
                area = ri.Name;

                foreach (RegionInfo distRi in dataMng.RegionInfoList)
                {
                    if (distRi.ID == ri.ParentRegionID)
                    {
                        dist = distRi.Name;

                        foreach (RegionInfo provRi in dataMng.RegionInfoList)
                        {
                            if (provRi.ID == distRi.ParentRegionID)
                            {
                                prov = provRi.Name;

                                break;
                            }
                        }

                        break;
                    }
                }

                tot = prov + " " + dist + " " + area;
                             
                ListViewItem item = new ListViewItem();
                item.Name = disItem.ID.ToString();
                               
                ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem();
                foreach (TypeDevice td in dataMng.GetTypedeviceList)
                {
                    if (td.ID == disItem.TkDevice)
                    {
                        subItem.Text = td.Name.ToString();
                    }
                }
                item.SubItems.Add(subItem);

                subItem = new ListViewItem.ListViewSubItem();
                subItem.Text = disItem.Name.ToString();
                item.SubItems.Add(subItem);

                subItem = new ListViewItem.ListViewSubItem();
                subItem.Text = tot.Trim();
                item.SubItems.Add(subItem);

                subItem = new ListViewItem.ListViewSubItem();
                subItem.Text = disItem.CellNumber.ToString();
                item.SubItems.Add(subItem);

                this.listViewDisasterList.Items.Add(item);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listViewDisasterList_DoubleClick(object sender, EventArgs e)
        {
            if (this.listViewDisasterList.SelectedItems.Count < 1)
            {
                MessageBox.Show("수신기를 선택하세요", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ListViewItem item = this.listViewDisasterList.SelectedItems[0];
            delegateSelectDeviceListViewItem(item);

            this.btnClose_Click(sender, e);
        }
    }
}
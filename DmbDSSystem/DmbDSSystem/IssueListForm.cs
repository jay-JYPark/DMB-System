using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using ADEng.Library.DMB;
using ADEng.Control.DMB;
using ADEng.dmbcomm;
using ADEng.CsvLib;

namespace DmbDSSystem
{
    public partial class IssueListForm : Form
    {
        #region Instance
        private CsvMake csvmake = null;
        private DataManager datamanager = DataManager.getInstance();
        private DateTime startprevDate = new DateTime();
        private DateTime endprevDate = new DateTime();
        private ControlSet controlset = null;
        private OrderListSet orderlistset = null;        
        #endregion

        #region Variable
        #endregion

        public IssueListForm()
        {
            InitializeComponent();
        }

        #region OnLoad
        protected override void OnLoad(EventArgs e)
        {
            controlset = new ControlSet();
            orderlistset = new OrderListSet();            
            this.InitCtrl();
            this.StartDateTimePicker.Value = DateTime.Now.Date;
            this.EndDateTimePicker.Value = DateTime.Now.Date.AddMinutes(1439);
            startprevDate = this.StartDateTimePicker.Value;
            endprevDate = this.EndDateTimePicker.Value;

            base.OnLoad(e);
        }
        #endregion

        #region ListView 컬럼 셋팅
        private void InitCtrl()
        {
            try
            {
                this.controlset.IssuesInitCtrl(this.DmbIssueListView);
            }
            catch (Exception ex)
            {
                Console.WriteLine("InitCtrl - " + ex.Message);
            }
        }
        #endregion

        #region UI 사용자 Event
        //조회
        private void QueryBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (StartDateTimePicker.Value > EndDateTimePicker.Value)
                {
                    MessageBox.Show("기간을 다시 설정하세요.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    this.SetIssuesUpdate();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("QueryBtn_Click - " + ex.Message);
            }
        }

        //엑셀 파일 만들기
        private void MakeCsvBtn_Click(object sender, EventArgs e)
        {
            try
            {
                csvmake = new CsvMake(this.DmbIssueListView);
                csvmake.saveCsv();
            }
            catch (Exception ex)
            {
                Console.WriteLine("MakeCsvBtn_Click - " + ex.Message);
            }
        }

        //초기화
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            this.DmbIssueListView.Items.Clear();

            this.StartDateTimePicker.Value = DateTime.Now.Date;
            this.EndDateTimePicker.Value = DateTime.Now.Date.AddMinutes(1439);
            startprevDate = this.StartDateTimePicker.Value;
            endprevDate = this.EndDateTimePicker.Value;
        }

        private void StartDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                if (this.StartDateTimePicker.Value >= this.EndDateTimePicker.Value)
                {
                    MessageBox.Show("기간을 다시 설정하세요.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.StartDateTimePicker.Value = startprevDate;
                }
                else
                {
                    startprevDate = this.StartDateTimePicker.Value;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("StartDateTimePicker_CloseUp - " + ex.Message);
            }
        }

        private void EndDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                if (this.EndDateTimePicker.Value <= this.StartDateTimePicker.Value)
                {
                    MessageBox.Show("기간을 다시 설정하세요.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.EndDateTimePicker.Value = endprevDate;
                }
                else
                {
                    endprevDate = this.EndDateTimePicker.Value;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EndDateTimePicker_CloseUp - " + ex.Message);
            }
        }

        //정렬을 위한 클릭 이벤트
        private void DmbIssueListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                if (e.Column.ToString() != "0")
                {
                    if (this.DmbIssueListView.Items.Count > 1)
                    {
                        if (this.DmbIssueListView.Sorting == SortOrder.Ascending || DmbIssueListView.Sorting == SortOrder.None)
                        {
                            this.DmbIssueListView.ListViewItemSorter = new ListViewItemComparer(e.Column, "desc");
                            DmbIssueListView.Sorting = SortOrder.Descending;
                        }
                        else
                        {
                            this.DmbIssueListView.ListViewItemSorter = new ListViewItemComparer(e.Column, "asc");
                            DmbIssueListView.Sorting = SortOrder.Ascending;
                        }

                        DmbIssueListView.Sort();

                        for (int i = 0; i < DmbIssueListView.Items.Count; i++)
                        {
                            int c = i + 1;
                            DmbIssueListView.Items[i].SubItems[0].Text = c.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DmbIssueListView_ColumnClick - " + ex.Message);
            }
        }
        #endregion

        #region 정렬 관련
        private class ListViewItemComparer : IComparer
        {
            private int col;
            public string sort = "asc";

            public ListViewItemComparer()
            {
                col = 0;
            }

            public ListViewItemComparer(int column, string sort)
            {
                col = column;
                this.sort = sort;
            }

            public int Compare(object x, object y)
            {
                if (sort == "asc")
                {
                    return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
                }
                else
                {
                    return String.Compare(((ListViewItem)y).SubItems[col].Text, ((ListViewItem)x).SubItems[col].Text);
                }
            }
        }
        #endregion

        #region 조회내역 ListView 업데이트
        public void SetIssuesUpdate()
        {
            try
            {
                WaitBarMng.Start();
                WaitBarMng.Msg = "데이터 조회중 입니다...";

                List<WMessage> datewmsgList = new List<WMessage>();
                WMessageIDComparer wcomparer = new WMessageIDComparer();

                if (this.DmbIssueListView.Items.Count != 0)
                {
                    this.DmbIssueListView.Items.Clear();
                }

                datewmsgList = datamanager.GetDateWMessageList((DateTime)this.StartDateTimePicker.Value, (DateTime)this.EndDateTimePicker.Value);
                datewmsgList.Sort(wcomparer);

                if (datewmsgList.Count != 0)
                {
                    foreach (WMessage wmessage in datewmsgList)
                    {
                        if (wmessage.SendPart == WMessage.E_sendPart.SMS)
                        {
                            continue;
                        }

                        if (wmessage.BCenterList.Count != 0)
                        {
                            foreach (broadcast broad in wmessage.BCenterList)
                            {
                                if (broad.StageID != (uint)Properties.Settings.Default.cnfBroad)
                                {
                                    continue;
                                }

                                this.DmbIssueListView.Items.Add(this.orderlistset.setWmsgCollection(wmessage));
                            }
                        }
                        else if (wmessage.BCenterList.Count == 0)//특수수신기 제어 또는 상태 요청 시
                        {
                            string broadTmp = this.datamanager.getBroadName((uint)Properties.Settings.Default.cnfBroad);

                            //============박찬호 수정 - 2013/11/19========================================
                            if (broadTmp == "<KBS>" || broadTmp == "<MBC>" || broadTmp == "<SBS>" || broadTmp == "<YTN>")
                            //============================================================================
                            {
                                this.DmbIssueListView.Items.Add(this.orderlistset.setWmsgCollection(wmessage));
                            }
                        }
                    }

                    this.orderlistset.PkidIndex = 0;
                }

                this.controlset.SetListViewIndex(this.DmbIssueListView);
                WaitBarMng.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("IssueListForm.SetIssuesUpdate - " + ex.Message);
                WaitBarMng.Close();
            }
        }
        #endregion
    }
}
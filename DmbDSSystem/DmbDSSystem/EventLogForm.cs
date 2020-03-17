using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using ADEng.Library.DMB;
//using ADEng.Control.DMB;
using ADEng.dmbcomm;
using ADEng.CsvLib;

namespace DmbDSSystem
{
    public partial class EventLogForm : Form
    {
        #region Instance
        private EventLogMng eventLogMng = null;
        private CsvMake csvmake = null;
        private DataManager datamanager = DataManager.getInstance();
        private DateTime startprevDate = new DateTime();
        private DateTime endprevDate = new DateTime();
        #endregion

        #region Variable
        #endregion

        public EventLogForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            eventLogMng = new EventLogMng();
            this.StartDateTimePicker.Value = DateTime.Now.Date;
            this.EndDateTimePicker.Value = DateTime.Now.Date.AddMinutes(1439);
            startprevDate = this.StartDateTimePicker.Value;
            endprevDate = this.EndDateTimePicker.Value;
            this.InitCtrl();

            base.OnLoad(e);
        }

        #region ListView 컬럼 셋팅
        private void InitCtrl()
        {
            ColumnHeader numcol = new ColumnHeader();
            numcol.Text = "";
            numcol.Width = 30;
            this.DmbEventLogListView.Columns.Add(numcol);
            
            ColumnHeader kindcol = new ColumnHeader();
            kindcol.Text = "종류";
            kindcol.Width = 130;
            this.DmbEventLogListView.Columns.Add(kindcol);

            ColumnHeader datecol = new ColumnHeader();
            datecol.Text = "날짜";
            datecol.Width = 105;
            this.DmbEventLogListView.Columns.Add(datecol);

            ColumnHeader timecol = new ColumnHeader();
            timecol.Text = "시간";
            timecol.Width = 95;
            this.DmbEventLogListView.Columns.Add(timecol);

            ColumnHeader msgcol = new ColumnHeader();
            msgcol.Text = "내용";
            msgcol.Width = 340;
            this.DmbEventLogListView.Columns.Add(msgcol);

            ColumnHeader comnamecol = new ColumnHeader();
            comnamecol.Text = "사용자명";
            comnamecol.Width = 140;
            this.DmbEventLogListView.Columns.Add(comnamecol);
        }
        #endregion

        #region UI 클릭 이벤트
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
                    WaitBarMng.Start();
                    WaitBarMng.Msg = "데이터 조회중 입니다...";

                    List<Log> logList = new List<Log>();

                    if (this.DmbEventLogListView.Items.Count > 0)
                    {
                        this.DmbEventLogListView.Items.Clear();
                    }

                    logList = eventLogMng.ReadLog("DMB_DS", StartDateTimePicker.Value, EndDateTimePicker.Value);
                    this.DmbEventLogListView.StateImageList = imageList1;

                    foreach (Log log in logList)
                    {
                        ListViewItem item = new ListViewItem();

                        if (log.Kind == "Error")
                        {
                            item.StateImageIndex = 1;
                        }
                        else
                        {
                            item.StateImageIndex = 0;
                        }

                        this.DmbEventLogListView.Items.Insert(0, item);

                        //종류
                        item.SubItems.Add(log.Kind);
                        //날짜                
                        item.SubItems.Add(log.Date);
                        //시간
                        item.SubItems.Add(log.Time);
                        //내용
                        item.SubItems.Add(log.Message);
                        //사용자명
                        item.SubItems.Add(log.UserName);
                    }

                    WaitBarMng.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EventLogForm.QueryBtn_Click - " + ex.Message);
                WaitBarMng.Close();
            }
        }

        private void MakeCsvBtn_Click(object sender, EventArgs e)
        {
            try
            {
                csvmake = new CsvMake(this.DmbEventLogListView);
                csvmake.saveCsv();
            }
            catch (Exception ex)
            {
                Console.WriteLine("MakeCsvBtn_Click - " + ex.Message);
            }
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            this.DmbEventLogListView.Items.Clear();

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
        #endregion
    }
}
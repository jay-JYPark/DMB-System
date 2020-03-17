using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ADEng.Library;
using System.Configuration;
using System.Globalization;
using System.Diagnostics;
using TextLog;

namespace EWSSystem
{
    public partial class HistoryForm : Form
    {
        //멤버 변수
        private ADEng.Library.oracleDAC odec = null;

        //기본 생성자
        public HistoryForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 폼이 로드될 때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HistoryForm_Load(object sender, EventArgs e)
        {
            //0. 오라클 접속 변수 얻어오기
            odec = DbConn.GetDbConn();

            //1. 일반, 특수 기본값 채워주기
            //일반으로 기본값 잡음.

            //2. 날짜 세팅
            this.dtStartTime.Value = DateTime.Now.Date;
            this.dtEndTime.Value = DateTime.Now.Date.AddMinutes(1439);

            //3. 상태 드랍박스 채워주기
            this.cbStatus.Items.Add("선택없음");
            this.cbStatus.Items.Add("대기");
            this.cbStatus.Items.Add("전송중");
            this.cbStatus.Items.Add("종료");

            this.cbStatus.SelectedIndex = 0; // "선택없음" 을 선택
        }


        /// <summary>
        /// 조회 버튼을 눌렀을 때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            ////1. 조건 받아오기
            ////1)메세지 종류 (일반 OR 특수)
            ////2) 기간 (FROM - TO)
            ////3) 상태

            //0. 결과를 저장한 DataTable
            this.lvHistoryList.Items.Clear();
            DataTable dTable = new DataTable();

            try
            {
                //1. 쿼리문 만들기
                StringBuilder sBuilder = new StringBuilder(100);
                sBuilder.Append(" SELECT msgType, msgid, sourcedept, revtime, status, tstarttime, tendtime ");
                sBuilder.Append(" FROM message ");

                // 1-1. 메세지 종류
                sBuilder.Append(" WHERE msgType = ");
                if (this.rbNormal.Checked == true)
                {
                    sBuilder.Append(" 0 ");
                }
                else
                {
                    sBuilder.Append(" 1 ");
                }

                //1-2. 수신 시간 (기존 포멧 : yyyy-MM-dd HH:mm)
                string startTime = this.dtStartTime.Text.Substring(0, 4);
                startTime += this.dtStartTime.Text.Substring(5, 2);
                startTime += this.dtStartTime.Text.Substring(8, 2);
                startTime += this.dtStartTime.Text.Substring(11, 2);
                startTime += this.dtStartTime.Text.Substring(14, 2);
                startTime += "00";

                string endTime = this.dtEndTime.Text.Substring(0, 4);
                endTime += this.dtEndTime.Text.Substring(5, 2);
                endTime += this.dtEndTime.Text.Substring(8, 2);
                endTime += this.dtEndTime.Text.Substring(11, 2);
                endTime += this.dtEndTime.Text.Substring(14, 2);
                endTime += "59";

                sBuilder.Append(" AND revtime >= '" + startTime + "' AND revtime <= '" + endTime + "'");

                //1-3. 상태

                switch (this.cbStatus.Text)
                {
                    case "대기":
                        sBuilder.Append(" AND status = 0 ");
                        break;
                    case "전송중":
                        sBuilder.Append(" AND status = 1 ");
                        break;
                    case "종료":
                        sBuilder.Append(" AND status = 2 ");
                        break;
                    default:
                        break;
                }


                if (odec.openDb())
                {
                    dTable = odec.getDataTable(sBuilder.ToString(), "messege");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EWSSystem.HistoryForm.btnSearch_Click(DB 조회)| " + ex.Message);
                Log.WriteLog("EWSSystem.HistoryForm.MainFobtnSearch_Clickrm_Load(DB조회)| " + ex.Message);
            }

            if (dTable.Rows.Count > 0)
            {
                try
                {
                    int i = 0;
                    string msgType = "일반";
                    string status = "";

                    foreach (DataRow dRow in dTable.Rows)
                    {
                        if (dRow[0].ToString().Equals("1"))
                        {
                            msgType = "특수";
                        }

                        switch (dRow[4].ToString())
                        {
                            case "0":
                                status = "대기";
                                break;
                            case "1":
                                status = "전송중";
                                break;
                            case "2":
                                status = "종료";
                                break;
                            default:
                                break;
                        }


                        string revTime = dRow[3].ToString().Substring(0, 4)
                                                + "-"
                                                + dRow[3].ToString().Substring(4, 2)
                                                + "-"
                                                + dRow[3].ToString().Substring(6, 2)
                                                + " "
                                                + dRow[3].ToString().Substring(8, 2)
                                                + ":"
                                                + dRow[3].ToString().Substring(10, 2)
                                                + ":"
                                                + dRow[3].ToString().Substring(12, 2);

                        string tStartTime = dRow[5].ToString().Substring(0, 4)
                                                + "-"
                                                + dRow[5].ToString().Substring(4, 2)
                                                + "-"
                                                + dRow[5].ToString().Substring(6, 2)
                                                + " "
                                                + dRow[5].ToString().Substring(8, 2)
                                                + ":"
                                                + dRow[5].ToString().Substring(10, 2)
                                                + ":"
                                                + dRow[5].ToString().Substring(12, 2);

                        string tEndTime = dRow[6].ToString().Substring(0, 4)
                                              + "-"
                                              + dRow[6].ToString().Substring(4, 2)
                                              + "-"
                                              + dRow[6].ToString().Substring(6, 2)
                                              + " "
                                              + dRow[6].ToString().Substring(8, 2)
                                              + ":"
                                              + dRow[6].ToString().Substring(10, 2)
                                              + ":"
                                              + dRow[6].ToString().Substring(12, 2);

                        ListViewItem lvHistoryItem = new ListViewItem(new string[] { msgType
                                                                                                               , dRow[1].ToString()
                                                                                                               , dRow[2].ToString()
                                                                                                               , revTime
                                                                                                               , status
                                                                                                               , tStartTime
                                                                                                               , tEndTime});
                        this.lvHistoryList.Items.Insert(i, lvHistoryItem);
                        i++;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("EWSSystem.HistoryForm.btnSearch_Click(리스트 출력)| " + ex.Message);
                    Log.WriteLog("EWSSystem.HistoryForm.MainFobtnSearch_Clickrm_Load(리스트 출력)| " + ex.Message);
                }

            }


        }
    }
}

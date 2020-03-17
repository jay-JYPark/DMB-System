using  System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using ADEng.Library;
using TextLog;
using System.Threading;

namespace EWSSystem
{
    public partial class MainForm : Form
    {
        #region 멤버 변수

        //폼 객체
        private MainViewForm mainviewform = null; //송출 현황폼
        private HistoryForm historyForm = null; //송출이력폼
        private OptionDialog optionForm = null;//옵션 폼
        private ProgramInfoForm programInfoForm = null;//프로그램 정보 폼

        //버젼 정보
        private StringBuilder versionStr = new StringBuilder("<2012/05/15> Ver 0.5"); //변경예정
          
          //오라클
          private ADEng.Library.oracleDAC odec = null;
        #endregion


        /// <summary>
        /// 생성자
        /// </summary>
        public MainForm()
        {

            InitializeComponent();
        }


        /// <summary>
        /// 프로그램이 로드될 때 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {

            try
            {
                mainviewform = new MainViewForm();
                historyForm = new HistoryForm();
                //optionForm = new OptionForm();
                programInfoForm = new ProgramInfoForm();

                mainviewform.MdiParent = this;
                historyForm.MdiParent = this;
                //optionForm.MdiParent = this;
                programInfoForm.MdiParent = this;

                mainviewform.Dock = DockStyle.Fill;
                historyForm.Dock = DockStyle.Fill;

                mainviewform.Show();
                this.btnMainToolStrip.Checked = true;
                this.menuItemMain.Checked = true;

                Log.WriteLog("EWSSystem.MainForm.MainForm_Load()| " + " EWS 편성 시스템이 시작되었습니다.| ");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EWSSystem.MainForm.MainForm_Load()| " + ex.Message);
                Log.WriteLog("EWSSystem.MainForm.MainForm_Load()| " + ex.Message);
            }
        }

        /// <summary>
        /// 메인 폼이 닫힐 때
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {

            Log.WriteLog("EWSSystem.MainForm.OnClosing()| " + " EWS 편성 시스템이 종료되었습니다.| ");

            this.Dispose(true);
            base.OnClosing(e);

            //EWSSystem.exe 프로세스 죽이기
            Process[] myProcesses;
            // Returns array containing all instances of Notepad.
            myProcesses = Process.GetProcessesByName("EWSSystem");
            foreach (Process myProcess in myProcesses)
            {
                //myProcess.CloseMainWindow();
                myProcess.Kill();
            }
        }


        /// <summary>
        /// 송출 현황 버튼을 눌렀을 때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMainPage_Click(object sender, EventArgs e)
        {
            mainviewform.Show();
            historyForm.Hide();

            this.btnMainToolStrip.Checked = true;
            this.btnHistoryToolStrip.Checked = false;

            this.menuItemMain.Checked = true;
            this.menuItemHistory.Checked = false;
        }


        /// <summary>
        /// 송출 이력 버튼을 눌렀을 때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHistoryToolStrip_Click(object sender, EventArgs e)
        {
            mainviewform.Hide();
            historyForm.Show();


            this.btnMainToolStrip.Checked = false;
            this.btnHistoryToolStrip.Checked = true;

            this.menuItemMain.Checked = false;
            this.menuItemHistory.Checked = true;
        }


        /// <summary>
        /// 송출 현황 메뉴 버튼 눌렀을 때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemMain_Click(object sender, EventArgs e)
        {
            mainviewform.Show();
            historyForm.Hide();

            this.btnMainToolStrip.Checked = true;
            this.btnHistoryToolStrip.Checked = false;

            this.menuItemMain.Checked = true;
            this.menuItemHistory.Checked = false;
        }



        /// <summary>
        /// 송출 이력 메뉴 버튼 눌렀을 때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemHistory_Click(object sender, EventArgs e)
        {
            mainviewform.Hide();
            historyForm.Show();


            this.btnMainToolStrip.Checked = false;
            this.btnHistoryToolStrip.Checked = true;

            this.menuItemMain.Checked = false;
            this.menuItemHistory.Checked = true;
        }



        /// <summary>
        /// 환경 설정 메뉴 버튼 눌렀을 때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 환경설정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (optionForm = new OptionDialog())
            {
                optionForm.evtMUXInfoChanged += new OptionDialog.MUXInfoChangedHandler(optionForm_evtMUXInfoChanged);

                optionForm.ShowDialog();

                optionForm.evtMUXInfoChanged -= new OptionDialog.MUXInfoChangedHandler(optionForm_evtMUXInfoChanged);
            }
        }


        /// <summary>
        /// 먹스 인포가 변경되었을 때
        /// </summary>
        void optionForm_evtMUXInfoChanged()
        {
             //0. 오라클 접속 변수 얻어오기
            odec = DbConn.GetDbConn();

            try
            {
                // MUX 인포를 DB에서 읽어 와 세팅한다.
                StringBuilder sBuilder = new StringBuilder(100);
                sBuilder.Append(" SELECT  useflag , muxid, muxdesc, status, ip ");
                sBuilder.Append(" FROM muxInfo ");

                DataTable dTable = new DataTable();

                if (odec.openDb())
                {
                    dTable = odec.getDataTable(sBuilder.ToString(), "muxInfo");
                }


                if (dTable.Rows.Count > 0)
                {
                    //초기화
                    this.mainviewform.lvMUXInfos.Items.Clear();

                    int i = 0;
                    string strUse = "사용";
                    string strStatus = "정상";
                    foreach (DataRow dRow in dTable.Rows)
                    {
                        if (dRow[0].ToString().Equals(0))
                        {
                            strUse = "미사용";
                        }
                        if (dRow[3].ToString().Equals(0))
                        {
                            strStatus = "이상";
                        }


                        ListViewItem lvItem = new ListViewItem(new string[] {  strUse
                                                                                                       ,dRow[1].ToString()
                                                                                                       , dRow[2].ToString()
                                                                                                       , strStatus
                                                                                                       , dRow[4].ToString()
                                                                                    });
                        this.mainviewform.lvMUXInfos.Items.Insert(i, lvItem);
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EWSSystem.MainForm.optionForm_evtMUXInfoChanged()| " + ex.Message);
                Log.WriteLog("EWSSystem.MainForm.optionForm_evtMUXInfoChanged()| " + ex.Message);
            }
        }


        /// <summary>
        /// 도움말 메뉴 버튼 눌렀을 때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemProgramInfo_Click(object sender, EventArgs e)
        {
            programInfoForm = new ProgramInfoForm("EWS 전송 시스템", this.versionStr.ToString());
            programInfoForm.ShowDialog();
        }


        /// <summary>
        /// 종료버튼 눌렀을 때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemExit_Click(object sender, EventArgs e)
        {
            this.Close();

            //foreach (Process process in Process.GetProcesses())
            //{
            //    if (process.ProcessName.StartsWith("EWSSystem"))
            //    {
            //        process.Kill();
            //    }
            //}

        }


    }
}

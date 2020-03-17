using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using ADEng.Library;
using System.Diagnostics;
using TextLog;

namespace EWSSystem
{
    public partial class OptionDialog : Form
    {
        //멤버 변수
        private ADEng.Library.oracleDAC odec = null;


        //이벤트
        public delegate void MUXInfoChangedHandler();
        public event MUXInfoChangedHandler evtMUXInfoChanged;


        /// <summary>
        /// 기본 생성자
        /// </summary>
        public OptionDialog()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 폼이 로드될 때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionForm_Load(object sender, EventArgs e)
        {
            //0. 오라클 접속 변수 얻어오기
            odec = DbConn.GetDbConn();

            //1. 설정창
            //TCP Server 
            this.txtPort.Text = ConfigurationManager.AppSettings["TCPPort"];
            //MUX 기본 
            this.txtMUXTcId.Text = ConfigurationManager.AppSettings["MUXTcId"];
            this.txtMUXRetry.Text = ConfigurationManager.AppSettings["MUXRetry"];

            //특수메세지
            this.txtSpcMaxId.Text = ConfigurationManager.AppSettings["SpcMaxId"];
            this.txtSpcRepeatNum.Text = ConfigurationManager.AppSettings["SpcRepeatNum"];
            this.txtSpcWaitTime.Text = ConfigurationManager.AppSettings["SpcWaitTime"];
            this.txtSpcCycle.Text = ConfigurationManager.AppSettings["SpcCycle"];

            //일반메세지
            this.txtNorMaxId.Text = ConfigurationManager.AppSettings["NorMaxId"];
            this.txtNorProcNum.Text = ConfigurationManager.AppSettings["NorProcNum"];
            this.txtNorRepeatNum.Text = ConfigurationManager.AppSettings["NorRepeatNum"];
            this.txtNorCycle.Text = ConfigurationManager.AppSettings["NorCycle"];
            this.txtNorTransTime.Text = ConfigurationManager.AppSettings["NorTransTime"];
            this.txtNorWaitTime.Text = ConfigurationManager.AppSettings["NorWaitTime"];


            //2. 먹스 관리 창
            // 먹스 정보 DB 로드
            StringBuilder sBuilder = new StringBuilder(100);
            sBuilder.Append(" SELECT muxid,  useflag , ip, muxdesc ");
            sBuilder.Append(" FROM muxInfo ");

            DataTable dTable = new DataTable();

            try
            {
                if (odec.openDb())
                {
                    dTable = odec.getDataTable(sBuilder.ToString(), "muxInfo");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EWSSystem.OptionDialog.OptionForm_Load()| " + ex.Message);
                Log.WriteLog("EWSSystem.OptionDialog.OptionForm_Load()| " + ex.Message);
            }


            if (dTable.Rows.Count > 0)
            {
                this.lvMUXList.Items.Clear();
                int num = 1;
                string strUse = "사용";
                foreach (DataRow dRow in dTable.Rows)
                {
                    if (dRow[1].ToString().Equals(0))
                    {
                        strUse = "미사용";
                    }

                    ListViewItem lvItem = new ListViewItem(new string[] {  dRow[0].ToString()
                                                                                                        , num.ToString()
                                                                                                        , strUse
                                                                                                        , dRow[2].ToString()
                                                                                                        , dRow[3].ToString()
                                                                                    });
                    this.lvMUXList.Items.Add(lvItem);
                    num++;
                }
            }

        }


        /// <summary>
        /// 추가 버튼 눌렀을 때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //0. MUX Id 구하기 (DB-Select)
            int dTableCnt = 0;
            DataTable dTable = null;

            try
            {
                StringBuilder sBuilder = new StringBuilder(100);
                sBuilder.Append(" SELECT max(muxid)+1 FROM muxinfo ");

                //DataTable dTable = new DataTable();

                if (odec.openDb())
                {
                    dTable = odec.getDataTable(sBuilder.ToString(), "muxInfo");
                    dTableCnt = Convert.ToInt32(dTable.Rows[0][0].ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EWSSystem.OptionDialog.btnAdd_Click(DB-Select)| " + ex.Message);
                Log.WriteLog("EWSSystem.OptionDialog.btnAdd_Click(DB-Select)| " + ex.Message);
            }

            //1. (DB -Insert)

            if (CheckInputValue() == true) //유효성 검사
            {
                if (odec.openDb())
                {

                    int intChecked = 1;
                    if (this.cbxIsUse.Checked == false)
                    {
                        intChecked = 0;
                    }

                    try
                    {
                        StringBuilder sBuilder = new StringBuilder();
                        sBuilder.Append("INSERT INTO muxInfo VALUES(:1, :2, :3, :4, :5) ");
                        List<oracle_parameter> inner_parameters = new List<oracle_parameter>();

                        //사용여부, 구분, IP, 설명, 상태
                        inner_parameters.Add(
                            new oracle_parameter("muxid", oracle_parameter.OracleDataType.Int32, dTableCnt, ParameterDirection.Input));
                        inner_parameters.Add(
                            new oracle_parameter("ip", oracle_parameter.OracleDataType.Varchar2, this.txtIP.Text, ParameterDirection.Input));
                        inner_parameters.Add(
                            new oracle_parameter("muxdesc", oracle_parameter.OracleDataType.Varchar2, this.txtDescription.Text, ParameterDirection.Input));
                        inner_parameters.Add(
                            new oracle_parameter("useflag", oracle_parameter.OracleDataType.Int32, intChecked, ParameterDirection.Input));
                        inner_parameters.Add(
                           new oracle_parameter("status", oracle_parameter.OracleDataType.Int32, 0, ParameterDirection.Input));

                        int i = odec.WorkSql(sBuilder.ToString(), inner_parameters, "muxInfo");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("EWSSystem.OptionDialog.btnAdd_Click(DB-Insert)| " + ex.Message);
                        Log.WriteLog("EWSSystem.OptionDialog.btnAdd_Click(DB-Insert)| " + ex.Message);
                    }
                }


                // 2. 리스트 추가
                DataTable dTable2 = new DataTable();

                try
                {
                    StringBuilder sBuilder2 = new StringBuilder(100);
                    sBuilder2.Append(" SELECT muxid,  useflag , ip, muxdesc ");
                    sBuilder2.Append(" FROM muxInfo ");

                    if (odec.openDb())
                    {
                        dTable2 = odec.getDataTable(sBuilder2.ToString(), "muxInfo");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("EWSSystem.OptionDialog.btnAdd_Click(DB-Select All 리스트 추가)| " + ex.Message);
                    Log.WriteLog("EWSSystem.OptionDialog.btnAdd_Click(DB-Select All 리스트 추가)| " + ex.Message);
                }

                if (dTable2.Rows.Count > 0)
                {
                    this.lvMUXList.Items.Clear();
                    int num = 1;
                    string strUse = "사용";
                    foreach (DataRow dRow in dTable2.Rows)
                    {
                        if (dRow[1].ToString().Equals(0))
                        {
                            strUse = "미사용";
                        }

                        ListViewItem lvItem = new ListViewItem(new string[] {  dRow[0].ToString()
                                                                                                        , num.ToString()
                                                                                                        , strUse
                                                                                                        , dRow[2].ToString()
                                                                                                        , dRow[3].ToString()
                                                                                    });
                        this.lvMUXList.Items.Add(lvItem);
                        num++;
                    }
                }


                //3. 메세지 박스 출력
                MessageBox.Show(" MUX 정보가 추가되었습니다. ");

                //4. 메세지 박스 초기화
                this.cbxIsUse.Checked = true;
                this.txtDescription.Text = "";
                this.txtIP.Text = "";
                this.txtMuxId.Text = "";

                this.evtMUXInfoChanged();
            }

        }


        /// <summary>
        /// 수정 버튼 눌렀을 땐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModify_Click(object sender, EventArgs e)
        {
            DataTable dTable = new DataTable();

            try
            {
                StringBuilder sBuilder = new StringBuilder(100);
                sBuilder.Append(" SELECT count(*) ");
                sBuilder.Append(" FROM muxInfo ");
                sBuilder.AppendFormat(" WHERE muxid =  '{0}'", this.txtMuxId.Text);

                //DataTable dTable = new DataTable();

                if (odec.openDb())
                {
                    dTable = odec.getDataTable(sBuilder.ToString(), "muxInfo");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EWSSystem.OptionDialog.btnModify_Click(DB-Select)| " + ex.Message);
                Log.WriteLog("EWSSystem.OptionDialog.btnModify_Click(DB-Select)| " + ex.Message);
            }


            if (dTable.Rows.Count > 0)
            {
                if (this.txtIP.Text != null)
                {
                    int intChecked = 1;
                    if (this.cbxIsUse.Checked == false)
                    {
                        intChecked = 0;
                    }
                    // 유효성 검사
                    if (CheckInputValue() == true)
                    {
                        try
                        {
                            //1. DB 저장
                            // UPDATE 프로시져
                            if (odec.openDb())
                            {
                                //UPDATE muxInfo SET ip = '10.10.10.10' , muxDesc='수정완료' , useFlag=0 WHERE muxid = 1

                                StringBuilder sBuilder = new StringBuilder();
                                sBuilder.Append("UPDATE muxInfo SET ip = (:1) , muxDesc=(:2) , useFlag=(:3) WHERE muxid = (:4) ");
                                List<oracle_parameter> inner_parameters = new List<oracle_parameter>();

                                inner_parameters.Add(
                                    new oracle_parameter("ip", oracle_parameter.OracleDataType.Varchar2, this.txtIP.Text, ParameterDirection.Input));
                                inner_parameters.Add(
                                    new oracle_parameter("muxdesc", oracle_parameter.OracleDataType.Varchar2, this.txtDescription.Text, ParameterDirection.Input));
                                inner_parameters.Add(
                                    new oracle_parameter("useflag", oracle_parameter.OracleDataType.Int32, intChecked, ParameterDirection.Input));
                                inner_parameters.Add(
                                    new oracle_parameter("muxid", oracle_parameter.OracleDataType.Int32
                                                                                , Convert.ToInt32(this.txtMuxId.Text), ParameterDirection.Input));

                                int i = odec.WorkSql(sBuilder.ToString(), inner_parameters, "muxInfo");
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("EWSSystem.OptionDialog.btnModify_Click(DB-Update)| " + ex.Message);
                            Log.WriteLog("EWSSystem.OptionDialog.btnModify_Click(DB-Update)| " + ex.Message);
                        }


                        //2. 리스트에 적용
                        StringBuilder sBuilder2 = new StringBuilder(100);
                        sBuilder2.Append(" SELECT muxid,  useflag , ip, muxdesc ");
                        sBuilder2.Append(" FROM muxInfo ");

                        DataTable dTable2 = new DataTable();

                        if (odec.openDb())
                        {
                            dTable2 = odec.getDataTable(sBuilder2.ToString(), "muxInfo");
                        }


                        if (dTable2.Rows.Count > 0)
                        {
                            this.lvMUXList.Items.Clear();
                            int num = 1;
                            string strUse = "사용";
                            foreach (DataRow dRow in dTable2.Rows)
                            {
                                if (dRow[1].ToString().Equals(0))
                                {
                                    strUse = "미사용";
                                }

                                ListViewItem lvItem = new ListViewItem(new string[] {  dRow[0].ToString()
                                                                                                        , num.ToString()
                                                                                                        , strUse
                                                                                                        , dRow[2].ToString()
                                                                                                        , dRow[3].ToString()
                                                                                    });
                                this.lvMUXList.Items.Add(lvItem);
                                num++;
                            }
                        }
                        //3. 메세지 박스
                        MessageBox.Show(" MUX 정보가 수정되었습니다. ");
                    }
                }
                else
                {
                    //3. 메세지 박스
                    MessageBox.Show(" 수정할 항목을 선택해 주세요. ");
                }
            }
            else
            {
                //3. 메세지 박스
                MessageBox.Show(" 수정할 항목을 선택해 주세요. ");
            }

        }



        /// <summary>
        /// 삭제 버튼 눌렀을 때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntDelete_Click(object sender, EventArgs e)
        {
            DataTable dTable = new DataTable();

            try
            {
                StringBuilder sBuilder = new StringBuilder(100);
                sBuilder.Append(" SELECT muxid,  useflag , ip, muxdesc ");
                sBuilder.Append(" FROM muxInfo ");
                sBuilder.AppendFormat(" WHERE muxid =  {0}", this.txtMuxId.Text);

                //DataTable dTable = new DataTable();

                if (odec.openDb())
                {
                    dTable = odec.getDataTable(sBuilder.ToString(), "muxInfo");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("EWSSystem.OptionDialog.bntDelete_Click(DB-Select)| " + ex.Message);
                Log.WriteLog("EWSSystem.OptionDialog.bntDelete_Click(DB-Select)| " + ex.Message);
            }


            if (dTable.Rows.Count > 0)
            {
                if (this.txtMuxId.Text != null)
                {
                    //1. 정말 삭제 하시겠습니까?
                    //예 라면
                    if (MessageBox.Show("정말 삭제하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        try
                        {
                            //2. DB 삭제
                            //Delete 프로시져
                            if (odec.openDb())
                            {
                                //DELETE muxinfo WHERE muxid = 1
                                StringBuilder sBuilder = new StringBuilder();
                                sBuilder.Append("DELETE muxinfo WHERE muxid = (:1) ");
                                List<oracle_parameter> inner_parameters = new List<oracle_parameter>();

                                inner_parameters.Add(
                                    new oracle_parameter("muxid", oracle_parameter.OracleDataType.Int32
                                                                     , Convert.ToInt32(this.txtMuxId.Text), ParameterDirection.Input));

                                int i = odec.WorkSql(sBuilder.ToString(), inner_parameters, "muxInfo");
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("EWSSystem.OptionDialog.bntDelete_Click(DB-DELETE)| " + ex.Message);
                            Log.WriteLog("EWSSystem.OptionDialog.bntDelete_Click(DB-DELETE)| " + ex.Message);
                        }

                        //3. 리스트에서 삭제
                        StringBuilder sBuilder2 = new StringBuilder(100);
                        sBuilder2.Append(" SELECT muxid,  useflag , ip, muxdesc ");
                        sBuilder2.Append(" FROM muxInfo ");

                        DataTable dTable2 = new DataTable();

                        if (odec.openDb())
                        {
                            dTable2 = odec.getDataTable(sBuilder2.ToString(), "muxInfo");
                        }


                        if (dTable2.Rows.Count > 0)
                        {
                            this.lvMUXList.Items.Clear();
                            int num = 1;
                            string strUse = "사용";
                            foreach (DataRow dRow in dTable2.Rows)
                            {
                                if (dRow[1].ToString().Equals(0))
                                {
                                    strUse = "미사용";
                                }

                                ListViewItem lvItem = new ListViewItem(new string[] {  dRow[0].ToString()
                                                                                                        , num.ToString()
                                                                                                        , strUse
                                                                                                        , dRow[2].ToString()
                                                                                                        , dRow[3].ToString()
                                                                                    });
                                this.lvMUXList.Items.Add(lvItem);
                                num++;
                            }
                        }
                        //3. 메세지 박스
                        MessageBox.Show(" 삭제되었습니다. ");
                    }

                }
                else
                {
                    //3. 메세지 박스
                    MessageBox.Show(" 삭제할 항목을 선택해 주세요. ");
                }

            }
            else
            {
                //3. 메세지 박스
                MessageBox.Show(" 삭제할 항목을 선택해 주세요. ");
            }

        }


        /// <summary>
        /// 입력값 검사
        /// </summary>
        /// <returns></returns>
        private bool CheckInputValue()
        {
            //IP 입력이 잘 되었는지
            if (this.txtIP.Text.Trim() == "")
            {
                MessageBox.Show("IP 정보를 입력해 주세요.");
                return false;
            }

            return true;
        }


        /// <summary>
        /// IP 입력 창에 Key Press 버튼을 눌렀을 때 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            int iPos = 0;               // IP 구역의 현재 위치
            int iDelimitNumber = 0;     // IP 구역의 갯수

            int iLength = txtIP.Text.Length;
            int iIndex = txtIP.Text.LastIndexOf(".");

            int iIndex2 = -1;
            while (true)
            {
                iIndex2 = txtIP.Text.IndexOf(".", iIndex2 + 1);
                if (iIndex2 == -1)
                    break;

                ++iDelimitNumber;
            }

            // 숫자키와 백스페이스, '.' 만 입력 가능
            if ((e.KeyChar < 48 || e.KeyChar > 57) &&
            e.KeyChar != 8 && e.KeyChar != '.')
            {
                MessageBox.Show("숫자만 입력 가능합니다", "오류");
                e.Handled = true;
                return;
            }

            if (e.KeyChar != 8)
            {
                if (e.KeyChar != '.')
                {
                    if (iIndex > 0)
                        iPos = iLength - iIndex;
                    else
                        iPos = iLength + 1;

                    if (iPos == 3)
                    {
                        // 255 이상 체크
                        string strTmp = txtIP.Text.Substring(iIndex + 1) + e.KeyChar;
                        if (Int32.Parse(strTmp) > 255)
                        {
                            MessageBox.Show("255를 넘길수 없습니다.", "오류");
                            e.Handled = true;
                            return;
                        }
                        else
                        {
                            // 3자리가 넘어가면 자동으로 .을 찍어준다
                            if (iDelimitNumber < 3)
                            {
                                txtIP.AppendText(e.KeyChar.ToString());
                                txtIP.AppendText(".");
                                iDelimitNumber++;
                                e.Handled = true;
                                return;
                            }
                        }
                    }

                    // IP 마지막 4자리째는 무조건 무시
                    if (iPos == 4)
                    {
                        e.Handled = true;
                        return;
                    }
                }
                else
                {
                    // 아이피가 3구역 이상 쓰였으면, 이후 키는 무시한다
                    if (iDelimitNumber + 1 > 3)
                    {
                        MessageBox.Show("IP 주소가 정확하지 않습니다.", "오류");
                        e.Handled = true;
                        return;
                    }
                    else
                    {
                        // 연속으로 .을 찍었으면 오류
                        if (txtIP.Text.EndsWith("."))
                        {
                            MessageBox.Show("IP 주소가 정확하지 않습니다.", "오류");
                            e.Handled = true;
                            return;
                        }
                        else
                            iDelimitNumber++;
                    }
                }
            }

        }

        //열이 눌렸으면
        private void lvMUXList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvMUXList.SelectedItems.Count > 0)
            {
                this.txtMuxId.Text = this.lvMUXList.SelectedItems[0].SubItems[0].Text;
                if (this.lvMUXList.SelectedItems[0].SubItems[2].Text.Equals("사용"))
                {
                    this.cbxIsUse.Checked = true;
                }
                else
                {
                    this.cbxIsUse.Checked = false;
                }

                this.txtIP.Text = this.lvMUXList.SelectedItems[0].SubItems[3].Text;
                this.txtDescription.Text = this.lvMUXList.SelectedItems[0].SubItems[4].Text;
            }

        }


        /// <summary>
        /// Tab1 -  확인 버튼 눌렀을 때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Tab1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// Tab1 -  취소 버튼 눌렀을 때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Tab1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// Tab1 - 적용 버튼을 눌렀을 때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Tab1_Click(object sender, EventArgs e)
        {
            // Get the application configuration file.
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            AppSettingsSection app = config.AppSettings;

            #region 키 값 설정
            //송출 서버시스템 포트 설정
            if (ConfigurationManager.AppSettings["TCPPort"] != this.txtPort.Text)
            {
                MessageBox.Show("프로그램 재 시작시, 변경한 포트가 적용됩니다.");
                app.Settings["TCPPort"].Value = this.txtPort.Text;
            }

            //MUX 기본 
            app.Settings["MUXTcId"].Value = this.txtMUXTcId.Text;
            app.Settings["MUXRetry"].Value = this.txtMUXRetry.Text;

            //특수메세지
            app.Settings["SpcMaxId"].Value = this.txtSpcMaxId.Text;
            app.Settings["SpcRepeatNum"].Value = this.txtSpcRepeatNum.Text;
            app.Settings["SpcWaitTime"].Value = this.txtSpcWaitTime.Text;
            app.Settings["SpcCycle"].Value = this.txtSpcCycle.Text;

            //일반메세지
            app.Settings["NorMaxId"].Value = this.txtNorMaxId.Text;
            app.Settings["NorProcNum"].Value = this.txtNorProcNum.Text;
            app.Settings["NorTransTime"].Value = this.txtNorTransTime.Text;
            app.Settings["NorRepeatNum"].Value = this.txtNorRepeatNum.Text;
            app.Settings["NorCycle"].Value = this.txtNorCycle.Text;
            app.Settings["NorWaitTime"].Value = this.txtNorWaitTime.Text;
            #endregion

            // Save the configuration file.
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");


            #region 이전 코드
            ////송출 시스템 설정
            //ConfigurationManager.AppSettings["TCPPort"] = this.txtPort.Text;

            ////먹스 설정
            //ConfigurationManager.AppSettings["MUXTcId"] = this.txtMUXTcId.Text;
            //ConfigurationManager.AppSettings["MUXRetry"] = this.txtMUXRetry.Text;

            ////특수 메세지
            //ConfigurationManager.AppSettings["SpcMaxId"] = this.txtSpcMaxId.Text;
            //ConfigurationManager.AppSettings["SpcRepeatNum"] = this.txtSpcRepeatNum.Text;
            //ConfigurationManager.AppSettings["SpcWaitTime"] = this.txtSpcWaitTime.Text;
            //ConfigurationManager.AppSettings["SpcCycle"] = this.txtSpcCycle.Text;

            ////일반 메세지
            //ConfigurationManager.AppSettings["NorMaxId"] = this.txtNorMaxId.Text;
            //ConfigurationManager.AppSettings["NorProcNum"] = this.txtNorProcNum.Text;
            //ConfigurationManager.AppSettings["NorTransTime"] = this.txtNorTransTime.Text;
            //ConfigurationManager.AppSettings["NorRepeatNum"] = this.txtNorRepeatNum.Text;
            //ConfigurationManager.AppSettings["NorCycle"] = this.txtNorCycle.Text;
            //ConfigurationManager.AppSettings["NorWaitTime"] = this.txtNorWaitTime.Text;
            #endregion
        }



        /// <summary>
        /// Tab2 - 닫기 버튼 눌렀을 때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btApply_Tab2_Click(object sender, EventArgs e)
        {
            this.evtMUXInfoChanged();
        }



        #region 텍스트 박스에 숫자만 입력해 주도록 제한을 둔다.
        private void txtPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }


        private void txtMUXTcId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void txtNorMaxId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void txtNorProcNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void txtNorTransTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void txtNorCycle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void txtNorWaitTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void txtNorNotAcceptProcTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void txtSpcMaxId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void txtSpcRepeatNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void txtSpcWaitTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void txtSpcCycle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }
        #endregion


        #region 적용 버튼 활성화 관련
        // 적용 버튼 활성화
        //private void txtPort_TextChanged(object sender, EventArgs e)
        //{
        //    this.btnApply_Tab1.Enabled = true;
        //}

        //private void txtMUXTcId_TextChanged(object sender, EventArgs e)
        //{
        //    this.btnApply_Tab1.Enabled = true;
        //}

        //private void txtMUXRetry_TextChanged(object sender, EventArgs e)
        //{
        //    this.btnApply_Tab1.Enabled = true;
        //}

        //private void txtNorMaxId_TextChanged(object sender, EventArgs e)
        //{
        //    this.btnApply_Tab1.Enabled = true;
        //}

        //private void txtNorProcNum_TextChanged(object sender, EventArgs e)
        //{
        //    this.btnApply_Tab1.Enabled = true;
        //}

        //private void txtNorTransTime_TextChanged(object sender, EventArgs e)
        //{
        //    this.btnApply_Tab1.Enabled = true;
        //}

        //private void txtNorCycle_TextChanged(object sender, EventArgs e)
        //{
        //    this.btnApply_Tab1.Enabled = true;
        //}

        //private void txtNorWaitTime_TextChanged(object sender, EventArgs e)
        //{
        //    this.btnApply_Tab1.Enabled = true;
        //}

        //private void txtNorNotAcceptProcTime_TextChanged(object sender, EventArgs e)
        //{
        //    this.btnApply_Tab1.Enabled = true;
        //}

        //private void txtSpcMaxId_TextChanged(object sender, EventArgs e)
        //{
        //    this.btnApply_Tab1.Enabled = true;
        //}

        //private void txtSpcRepeatNum_TextChanged(object sender, EventArgs e)
        //{
        //    this.btnApply_Tab1.Enabled = true;
        //}

        //private void txtSpcWaitTime_TextChanged(object sender, EventArgs e)
        //{
        //    this.btnApply_Tab1.Enabled = true;
        //}

        //private void txtSpcCycle_TextChanged(object sender, EventArgs e)
        //{
        //    this.btnApply_Tab1.Enabled = true;
        //}
        #endregion

    }
}

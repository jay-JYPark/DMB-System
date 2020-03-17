using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using ADEng.Library;

namespace EWS_emulater
{
    public partial class MainForm : Form
    {
        #region delegate
        private delegate void InvokeTcpConFromOption();
        private delegate void InvokeConnect();
        private delegate void InvokeDisConnect();
        private delegate void InvokeRecvMsg(byte[] buff);
        private delegate void Invokeparse_SetLoginEvt(byte[] buff);
        private delegate void Invokeparse_SpecialMsgEvt(byte[] buff);
        private delegate void Invokeparse_NormalMsgEvt(byte[] buff);
        private delegate void Invokeparse_EwsStateEvt(byte[] buff);
        private delegate void Invokeparse_MuxStateEvt(byte[] buff);
        private delegate void Invokeparse_MsgCancleEvt(byte[] buff);
        private delegate void InvokeNorParsingHandle(int _id);
        private delegate void InvokeNorMuxHandle(int _id);
        private delegate void InvokeNorEndHandle(int _id);
        private delegate void InvokeSpcParsingHandle(int _id);
        private delegate void InvokeSpcMuxHandle(int _id);
        private delegate void InvokeSpcEndHandle(int _id);
        private delegate void InvokeDBInsertAll(string ukey, string date, string receiver, string sender, string callback, string telco, string msg);
        private delegate void InvokeDBInsertArea(string ukey, string date, string receiver, string sender, string callback, string telco, string msg);
        #endregion

        #region Instance
        private Set_ListView set_listview = null;
        private TcpSocket socketClt = null;
        private OptionForm optionform = null;
        private Parse parse = null;
        private Set_TextBox set_textbox = null;
        private SpcMng spcmng = null;
        private NorMng normng = null;
        private oracleDAC ora = null;
        private DeviceRes deviceres = null;
        private DataInsert datainsert = null;
        #endregion

        #region Variable
        Boolean tcpSrvStat = false;
        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            set_listview = new Set_ListView();
            optionform = new OptionForm();
            parse = new Parse();
            set_textbox = new Set_TextBox();
            deviceres = new DeviceRes();

            optionform.TcpConnectEvt += new OptionForm.TcpConnectHandle(optionform_TcpConnectEvt);
            parse.SetLoginEvt += new Parse.SetLoginHandle(parse_SetLoginEvt);
            parse.SpecialMsgEvt += new Parse.SpecialMsgHandle(parse_SpecialMsgEvt);
            parse.NormalMsgEvt += new Parse.NormalMsgHandle(parse_NormalMsgEvt);
            parse.EwsStateEvt += new Parse.EwsStateHandle(parse_EwsStateEvt);
            parse.MuxStateEvt += new Parse.MuxStateHandle(parse_MuxStateEvt);
            parse.MsgCancleEvt += new Parse.MsgCancleHandle(parse_MsgCancleEvt);            

            set_listview.SetColumn(this.listView1);
            set_listview.SetColumn1(this.listView2);

            base.OnLoad(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Application.Exit();

            base.OnClosing(e);
        }

        #region ParseData Event
        void parse_SetLoginEvt(byte[] buff)
        {
            try
            {
                this.Invoke(new Invokeparse_SetLoginEvt(this.Parse_SetLoginEvt), new object[] { buff });
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.parse_SetLoginEvt - " + ex.Message);
            }
        }

        void parse_SpecialMsgEvt(byte[] buff)
        {
            try
            {
                this.Invoke(new Invokeparse_SpecialMsgEvt(this.Parse_SpecialMsgEvt), new object[] { buff });
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.parse_SpecialMsgEvt - " + ex.Message);
            }
        }

        void parse_NormalMsgEvt(byte[] buff)
        {
            try
            {
                this.Invoke(new Invokeparse_NormalMsgEvt(this.Parse_NormalMsgEvt), new object[] { buff });
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.parse_NormalMsgEvt - " + ex.Message);
            }
        }

        void parse_EwsStateEvt(byte[] buff)
        {
            try
            {
                this.Invoke(new Invokeparse_EwsStateEvt(this.Parse_EwsStateEvt), new object[] { buff });
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.parse_EwsStateEvt - " + ex.Message);
            }
        }

        void parse_MuxStateEvt(byte[] buff)
        {
            try
            {
                this.Invoke(new Invokeparse_MuxStateEvt(this.Parse_MuxStateEvt), new object[] { buff });
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.parse_MuxStateEvt - " + ex.Message);
            }
        }

        void parse_MsgCancleEvt(byte[] buff)
        {
            try
            {
                this.Invoke(new Invokeparse_MsgCancleEvt(this.Parse_MsgCancleEvt), new object[] { buff });
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.parse_MsgCancleEvt - " + ex.Message);
            }
        }

        private void Parse_SetLoginEvt(byte[] buff)
        {
            socketClt.tcpSendMsg(buff);

            string str = this.Byte2Hex(buff);
            this.richTextBox1.Text += " ◀ 송신한 Data - " + str + "(" + buff.Length + " bytes) - 송신한 시각 : " + DateTime.Now.ToString() + "\n";
            this.set_textbox.SetTBScroll(this.richTextBox1);
        }

        private void Parse_SpecialMsgEvt(byte[] buff)
        {
            this.SetSpcMsg(buff);
        }

        private void Parse_NormalMsgEvt(byte[] buff)
        {
            this.SetNorMsg(buff);
        }

        private void Parse_EwsStateEvt(byte[] buff)
        {
            socketClt.tcpSendMsg(buff);

            string str = this.Byte2Hex(buff);
            this.richTextBox1.Text += " ◀ 송신한 Data - " + str + "(" + buff.Length + " bytes) - 송신한 시각 : " + DateTime.Now.ToString() + "\n";
            this.set_textbox.SetTBScroll(this.richTextBox1);
        }

        private void Parse_MuxStateEvt(byte[] buff)
        {
            socketClt.tcpSendMsg(buff);

            string str = this.Byte2Hex(buff);
            this.richTextBox1.Text += " ◀ 송신한 Data - " + str + "(" + buff.Length + " bytes) - 송신한 시각 : " + DateTime.Now.ToString() + "\n";
            this.set_textbox.SetTBScroll(this.richTextBox1);
        }

        private void Parse_MsgCancleEvt(byte[] buff)
        {
            socketClt.tcpSendMsg(buff);

            string str = this.Byte2Hex(buff);
            this.richTextBox1.Text += " ◀ 송신한 Data - " + str + "(" + buff.Length + " bytes) - 송신한 시각 : " + DateTime.Now.ToString() + "\n";
            this.set_textbox.SetTBScroll(this.richTextBox1);
        }
        #endregion

        #region UI Event
        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 환경설정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optionform.ShowDialog();
        }

        private void ResetLB_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = string.Empty;
        }
        #endregion

        #region 옵션창 이벤트
        void optionform_TcpConnectEvt()
        {
            try
            {
                this.Invoke(new InvokeTcpConFromOption(this.Optionform_TcpConnectEvt), new object[] { });
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.optionform_TcpConnectEvt - " + ex.Message);
            }
        }

        /// <summary>
        /// 옵션창에서 발생한 이벤트 처리하는 메소드
        /// </summary>
        private void Optionform_TcpConnectEvt()
        {
            if (!tcpSrvStat)
            {
                if (!tcpSrvStart())
                {
                    MessageBox.Show("TCP 서버 대기실패", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (this.ora == null)
            {
                if (Properties.Settings.Default.db_ip == string.Empty || Properties.Settings.Default.db_port == string.Empty
                    || Properties.Settings.Default.db_id == string.Empty || Properties.Settings.Default.db_pw == string.Empty
                    || Properties.Settings.Default.db_sid == string.Empty)
                {
                    MessageBox.Show("DB 접속 실패\nDB 설정 항목을 확인하세요.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    ora = new oracleDAC(Properties.Settings.Default.db_id, Properties.Settings.Default.db_pw, Properties.Settings.Default.db_ip
                        , Properties.Settings.Default.db_port, Properties.Settings.Default.db_sid);

                    if (ora.ConnectionState == ConnectionState.Closed)
                    {
                        if (!ora.openDb())
                        {
                            MessageBox.Show("DB 접속 실패\nDB 설정 항목을 확인하세요.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }
        #endregion

        #region TCP통신 연결
        private Boolean tcpSrvStart()
        {
            try
            {
                socketClt = new TcpSocket();

                if (!socketClt.socketServerInit(int.Parse(Properties.Settings.Default.Port.ToString())))
                {
                    return false;
                }

                socketClt.tcpConnected += new TcpSocket.TcpConnectedEventHandler(socketClt_tcpConnected);
                socketClt.tcpDisconnected += new TcpSocket.TcpDisconnectedEventHandler(socketClt_tcpDisconnected);
                socketClt.tcpMsgEvt += new TcpSocket.TcpSrvMsgEventHandler(socketClt_tcpMsgEvt);
                socketClt.tcpRcvMsgEvt += new TcpSocket.TcpSrvRcvMsgEventHandler(socketClt_tcpRcvMsgEvt);
                socketClt.startLisner();
                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.tcpSrvStart - " + ex.Message);
                return false;
            }
        }

        void socketClt_tcpRcvMsgEvt(object sender, byte[] rcvMsg)
        {
            try
            {
                this.Invoke(new InvokeRecvMsg(this.SocketClt_tcpRcvMsgEvt), new object[] { rcvMsg });
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.socketClt_tcpRcvMsgEvt - " + ex.Message);
            }
        }

        void socketClt_tcpMsgEvt(object sender, string rcvMsg, bool sMsgFlag)
        {
        }

        void socketClt_tcpDisconnected(object sender)
        {
            try
            {
                this.Invoke(new InvokeDisConnect(this.SocketClt_tcpDisconnected), new object[] { });
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.socketClt_tcpDisconnected - " + ex.Message);
            }
        }

        void socketClt_tcpConnected(object sender)
        {
            try
            {
                this.Invoke(new InvokeConnect(this.SocketClt_tcpConnected), new object[] { });
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.socketClt_tcpConnected - " + ex.Message);
            }
        }

        private void SocketClt_tcpConnected()
        {
            tcpSrvStat = true;
            this.toolStripStatusLabel1.Image = Properties.Resources.DMB_systemGreen;
        }

        private void SocketClt_tcpDisconnected()
        {
            tcpSrvStat = false;
            this.toolStripStatusLabel1.Image = Properties.Resources.DMB_systemRed;
        }

        private void SocketClt_tcpRcvMsgEvt(byte[] buff)
        {
            string str = this.Byte2Hex(buff);
            this.richTextBox1.Text += " ▶ 수신받은 Data - " + str + "(" + buff.Length + " bytes) - 수신받은 시각 : " + DateTime.Now.ToString() + "\n";
            this.set_textbox.SetTBScroll(this.richTextBox1);

            parse.ParseData(buff);
        }
        #endregion

        #region Byte[] 변환 메소드
        private string Byte2Hex(byte[] bytes)
        {
            try
            {
                String hexData = String.Empty;

                foreach (byte aByte in bytes)
                {
                    hexData += (String.Format("{0:X}", aByte));
                    hexData += " ";
                }

                return hexData;
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.Byte2Hex - " + ex.Message);
                return string.Empty;
            }
        }

        private string Byte2Str(byte[] bytes)
        {
            try
            {
                String hexData = String.Empty;

                foreach (byte aByte in bytes)
                {
                    hexData += aByte;
                    hexData += " ";
                }

                return hexData;
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.Byte2Hex - " + ex.Message);
                return string.Empty;
            }
        }
        #endregion

        #region DB_Insert_Event
        void datainsert_InsertAllEvt(string ukey, string date, string receiver, string sender, string callback, string telco, string msg)
        {
            try
            {
                this.Invoke(new InvokeDBInsertAll(this.Datainsert_InsertAllEvt), new object[] { ukey, date, receiver, sender, callback, telco, msg });
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.datainsert_InsertAllEvt - " + ex.Message);
            }
        }

        void datainsert_InsertAreaEvt(string ukey, string date, string receiver, string sender, string callback, string telco, string msg)
        {
            try
            {
                this.Invoke(new InvokeDBInsertAll(this.Datainsert_InsertAreaEvt), new object[] { ukey, date, receiver, sender, callback, telco, msg });
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.datainsert_InsertAreaEvt - " + ex.Message);
            }
        }

        private void Datainsert_InsertAllEvt(string ukey, string date, string receiver, string sender, string callback, string telco, string msg)
        {
            DataTable dt = new DataTable();
            string strQuery = "INSERT INTO MO_RECEIVER ";
            strQuery += "(UKEY, MO_DATE, RECEIVER, SENDER, CALLBACK, TELCO, MESSAGE) ";
            strQuery += "VALUES ('" + ukey + "', '" + date + "', '" + receiver + "', '" + sender + "', '" + callback + "', '" + telco + "', '" + msg + "')";
            this.ora.WorkSql(strQuery, "datarcv");
        }

        private void Datainsert_InsertAreaEvt(string ukey, string date, string receiver, string sender, string callback, string telco, string msg)
        {
            DataTable dt = new DataTable();
            string strQuery = "INSERT INTO MO_RECEIVER ";
            strQuery += "(UKEY, MO_DATE, RECEIVER, SENDER, CALLBACK, TELCO, MESSAGE) ";
            strQuery += "VALUES ('" + ukey + "', '" + date + "', '" + receiver + "', '" + sender + "', '" + callback + "', '" + telco + "', '" + msg + "')";
            this.ora.WorkSql(strQuery, "datarcv");
        }
        #endregion

        #region 특수, 일반 메시지를 ListView에 셋팅하고 관리하는 클래스로 관리를 시작한다.
        private void SetSpcMsg(byte[] buff) //UTC 이상하게 뿌리고 있음.
        {
            try
            {
                if (buff.Length > 50)
                {
                    byte[] tmp01 = new byte[] { buff[10], buff[9], buff[8], buff[7], 0x00, 0x00, 0x00, 0x00 }; //UTC
                    byte[] tmp02 = new byte[] { buff[42], buff[43], buff[44] }; //재난종류
                    byte[] tmp03 = new byte[] { buff[14], buff[13], buff[12], buff[11] }; //ID
                    byte[] tmp04 = new byte[] { 0, 0, 0, buff[49], buff[48], buff[47], buff[46], buff[45] }; //복합 5byte
                    byte[] tmp05 = new byte[] { buff[18], buff[17] }; //Body 길이
                    byte[] tmp07 = new byte[] { buff[20], buff[19] }; //Body안의 길이

                    //
                    string dtTmpstr = this.deviceres.fnByteToBit(tmp04[7]);
                    dtTmpstr += this.deviceres.fnByteToBit(tmp04[6]);
                    dtTmpstr += this.deviceres.fnByteToBit(tmp04[5]);
                    dtTmpstr += this.deviceres.fnByteToBit(tmp04[4]);
                    dtTmpstr = dtTmpstr.Substring(2, 28);
                    //

                    int IntregionC = this.deviceres.DeviceRegionCount((int)tmp04[3]); //지역수
                    int Intregion = this.deviceres.DeviceRegion((int)tmp04[3], (int)tmp04[4]); //지역형식 (0=전국, 1=정부지정, 2=행정동, 7=수신기개별)

                    if (Intregion == 7 && IntregionC == 0)
                    {
                        IntregionC = 16;
                    }

                    if (Intregion == 2)
                    {
                        IntregionC++;
                    }

                    long longtmp = BitConverter.ToInt64(tmp04, 0);
                    short shorttmp = BitConverter.ToInt16(tmp05, 0);
                    short bodyinLen = BitConverter.ToInt16(tmp07, 0);

                    byte[] tmp06 = new byte[(IntregionC * 10)]; //지역
                    for (int i = 0; i < (IntregionC * 10); i++)
                    {
                        tmp06[i] = buff[i + 50];
                    }
                    string TotalRegion = Encoding.Default.GetString(tmp06); //최종 지역(행정동 코드 또는 수신기번호)

                    string TotalMessage = string.Empty;
                    int tmpline = 50 + (IntregionC * 10);
                    if (buff[tmpline] == 3)
                    {
                        int messageLen = (19 + shorttmp) - (51 + (IntregionC * 10));
                        byte[] tmp08 = new byte[messageLen]; //단문
                        for (int i = 0; i < messageLen; i++)
                        {
                            tmp08[i] = buff[i + (51 + (IntregionC * 10))];
                        }
                        TotalMessage = Encoding.Default.GetString(tmp08); //최종 단문
                    }
                    else
                    {
                        int messageLen = (19 + shorttmp) - (53 + (IntregionC * 10));
                        byte[] tmp08 = new byte[messageLen]; //단문
                        for (int i = 0; i < messageLen; i++)
                        {
                            tmp08[i] = buff[i + (53 + (IntregionC * 10))];
                        }
                        TotalMessage = Encoding.Default.GetString(tmp08); //최종 단문
                    }

                    long utc = BitConverter.ToInt64(tmp01, 0); //시간
                    string dis = Encoding.Default.GetString(tmp02); //재난종류
                    string sts = "수신성공"; //발령상태
                    int pkid = BitConverter.ToInt32(tmp03, 0); //PKID

                    ListViewItem lvi = this.set_listview.SetNorListView(utc, dis, sts, TotalMessage, pkid); //리스트뷰에 셋팅
                    this.listView2.Items.Add(lvi);
                    this.set_listview.SetIndex(this.listView2);

                    this.spcmng = new SpcMng(pkid);
                    this.spcmng.ParseSuccessEvt += new SpcMng.ParseSuccessHandle(spcmng_ParseSuccessEvt);
                    this.spcmng.MuxSuccessEvt += new SpcMng.MuxSuccessHandle(spcmng_MuxSuccessEvt);
                    this.spcmng.EndSuccessEvt += new SpcMng.EndSuccessHandle(spcmng_EndSuccessEvt);

                    BaseP bp = ProtoMng.GetPObject("01", pkid.ToString(), dis, TotalRegion) as Proto01;
                    string db_msg = ProtoMng.MakeFrame(bp);

                    if (Intregion == 0)
                    {
                        datainsert = new DataInsert();
                        datainsert.InsertAllEvt += new DataInsert.InsertAllHandle(datainsert_InsertAllEvt);
                        datainsert.InsertAreaEvt += new DataInsert.InsertAreaHandle(datainsert_InsertAreaEvt);

                        datainsert.setInit();
                        datainsert.InsertAll(db_msg, pkid, dis);
                    }
                    else
                    {
                        datainsert = new DataInsert();
                        datainsert.InsertAllEvt += new DataInsert.InsertAllHandle(datainsert_InsertAllEvt);
                        datainsert.InsertAreaEvt += new DataInsert.InsertAreaHandle(datainsert_InsertAreaEvt);

                        datainsert.setInit();
                        datainsert.InsertArea(Intregion, IntregionC, TotalRegion, "", pkid, dis);
                    }

                    if (buff.Length > (19 + shorttmp))
                    {
                        byte[] reTmpbuff = new byte[buff.Length - (19 + shorttmp)];

                        for (int i = 0; i < buff.Length - (19 + shorttmp); i++)
                        {
                            reTmpbuff[i] = buff[i + (19 + shorttmp)];
                        }

                        this.parse.ParseData(reTmpbuff);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.SetSpcMsg - " + ex.Message);
            }
        }

        void spcmng_ParseSuccessEvt(int _id)
        {
            try
            {
                this.Invoke(new InvokeSpcParsingHandle(this.Spcmng_ParseSuccessEvt), new object[] { _id });
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.spcmng_ParseSuccessEvt - " + ex.Message);
            }
        }

        void spcmng_MuxSuccessEvt(int _id)
        {
            try
            {
                this.Invoke(new InvokeSpcMuxHandle(this.Spcmng_MuxSuccessEvt), new object[] { _id });
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.spcmng_MuxSuccessEvt - " + ex.Message);
            }
        }

        void spcmng_EndSuccessEvt(int _id)
        {
            try
            {
                this.Invoke(new InvokeSpcEndHandle(this.Spcmng_EndSuccessEvt), new object[] { _id });
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.spcmng_EndSuccessEvt - " + ex.Message);
            }
        }

        private void Spcmng_ParseSuccessEvt(int _id)
        {
            Console.WriteLine("특수메시지_파싱들어옴");
            byte[] buff = this.spcmng.ParseProto(_id);
            socketClt.tcpSendMsg(buff);

            this.listView2.Items[_id.ToString()].SubItems[3].Text = "파싱성공 전송";
            this.set_listview.SetFoucus(this.listView2, _id);

            string str = this.Byte2Hex(buff);
            this.richTextBox1.Text += " ◀ 송신한 Data - " + str + "(" + buff.Length + " bytes) - 송신한 시각 : " + DateTime.Now.ToString() + "\n";
            this.set_textbox.SetTBScroll(this.richTextBox1);
        }

        private void Spcmng_MuxSuccessEvt(int _id)
        {
            Console.WriteLine("특수메시지_먹스성공들어옴");
            byte[] buff = this.spcmng.MuxProto(_id);
            socketClt.tcpSendMsg(buff);

            this.listView2.Items[_id.ToString()].SubItems[3].Text = "MUX성공 전송";
            this.set_listview.SetFoucus(this.listView2, _id);

            string str = this.Byte2Hex(buff);
            this.richTextBox1.Text += " ◀ 송신한 Data - " + str + "(" + buff.Length + " bytes) - 송신한 시각 : " + DateTime.Now.ToString() + "\n";
            this.set_textbox.SetTBScroll(this.richTextBox1);
        }

        private void Spcmng_EndSuccessEvt(int _id)
        {
            Console.WriteLine("특수메시지_종료들어옴");
            byte[] buff = this.spcmng.EndProto(_id);
            socketClt.tcpSendMsg(buff);

            this.listView2.Items.RemoveByKey(_id.ToString());
            this.set_listview.SetIndex(this.listView2);

            string str = this.Byte2Hex(buff);
            this.richTextBox1.Text += " ◀ 송신한 Data - " + str + "(" + buff.Length + " bytes) - 송신한 시각 : " + DateTime.Now.ToString() + "\n";
            this.set_textbox.SetTBScroll(this.richTextBox1);
        }

        private void SetNorMsg(byte[] buff) //UTC 이상하게 뿌리고 있음.
        {
            try
            {
                if (buff.Length > 29)
                {
                    //byte[] tmp01 = new byte[] { buff[24], buff[23], buff[22], buff[21], 0x00, 0x00, 0x00, 0x00 }; //UTC(발령시간)
                    byte[] tmp01 = new byte[] { buff[10], buff[9], buff[8], buff[7], 0x00, 0x00, 0x00, 0x00 }; //UTC(송출이 프로토콜 만든 시간)
                    byte[] tmp02 = new byte[] { buff[17], buff[18], buff[19] }; //재난종류
                    byte[] tmp03 = new byte[] { buff[14], buff[13], buff[12], buff[11] }; //ID
                    byte[] tmp04 = new byte[] { buff[26], 0x00, 0x00, 0x00 }; //재난지역수

                    int tmpint01 = 0;
                    int int01 = 0;

                    if (buff[25] == 0)
                    {
                        int01 = 26;
                    }
                    else
                    {
                        tmpint01 = BitConverter.ToInt32(tmp04, 0); //재난지역수 int로 변환
                        int01 = 26 + (10 * (tmpint01 + 1));
                    }

                    byte[] tmp05 = new byte[] { buff[int01 + 2], buff[int01 + 1] }; //재난메시지길이
                    short tmpint02 = BitConverter.ToInt16(tmp05, 0); //재난메시지길이 int로 변환

                    byte[] tmp06 = new byte[tmpint02]; //DMB단문
                    for (int i = 0; i < tmpint02; i++)
                    {
                        tmp06[i] = buff[int01 + (i + 3)];
                    }

                    long utc = BitConverter.ToInt64(tmp01, 0); //시간
                    string dis = Encoding.Default.GetString(tmp02); //재난종류
                    string sts = "수신성공"; //발령상태
                    string msg = Encoding.Default.GetString(tmp06); //단문
                    int pkid = BitConverter.ToInt32(tmp03, 0); //PKID

                    ListViewItem lvi = this.set_listview.SetNorListView(utc, dis, sts, msg, pkid);
                    this.listView1.Items.Add(lvi);
                    this.set_listview.SetIndex(this.listView1);

                    this.normng = new NorMng(pkid);
                    this.normng.ParseSuccessEvt += new NorMng.ParseSuccessHandle(normng_ParseSuccessEvt);
                    this.normng.MuxSuccessEvt += new NorMng.MuxSuccessHandle(normng_MuxSuccessEvt);
                    this.normng.EndSuccessEvt += new NorMng.EndSuccessHandle(normng_EndSuccessEvt);

                    if (buff[25] == 0)
                    {
                        if (buff.Length > 29 + tmp06.Length)
                        {
                            byte[] reTmpbuff = new byte[buff.Length - (29 + tmp06.Length)];

                            for (int i = 0; i < buff.Length - (29 + tmp06.Length); i++)
                            {
                                reTmpbuff[i] = buff[i + (29 + tmp06.Length)];
                            }

                            this.parse.ParseData(reTmpbuff);
                        }
                    }
                    else
                    {
                        if (buff.Length > 29 + (10 * (tmpint01 + 1)) + tmp06.Length)
                        {
                            byte[] reTmpbuff = new byte[buff.Length - (29 + (10 * (tmpint01 + 1)) + tmp06.Length)];

                            for (int i = 0; i < buff.Length - (29 + (10 * (tmpint01 + 1)) + tmp06.Length); i++)
                            {
                                reTmpbuff[i] = buff[i + (29 + (10 * (tmpint01 + 1)) + tmp06.Length)];
                            }

                            this.parse.ParseData(reTmpbuff);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.SetNorMsg - " + ex.Message);
            }
        }

        void normng_ParseSuccessEvt(int _id)
        {
            try
            {
                this.Invoke(new InvokeNorParsingHandle(this.Normng_ParseSuccessEvt), new object[] { _id });
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.normng_ParseSuccessEvt - " + ex.Message);
            }
        }

        void normng_MuxSuccessEvt(int _id)
        {
            try
            {
                this.Invoke(new InvokeNorMuxHandle(this.Normng_MuxSuccessEvt), new object[] { _id });
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.normng_MuxSuccessEvt - " + ex.Message);
            }
        }

        void normng_EndSuccessEvt(int _id)
        {
            try
            {
                this.Invoke(new InvokeNorEndHandle(this.Normng_EndSuccessEvt), new object[] { _id });
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.normng_EndSuccessEvt - " + ex.Message);
            }
        }

        private void Normng_ParseSuccessEvt(int _id)
        {
            Console.WriteLine("일반메시지_파싱들어옴");
            byte[] buff = this.normng.ParseProto(_id);
            socketClt.tcpSendMsg(buff);

            this.listView1.Items[_id.ToString()].SubItems[3].Text = "파싱성공 전송";
            this.set_listview.SetFoucus(this.listView1, _id);

            string str = this.Byte2Hex(buff);
            this.richTextBox1.Text += " ◀ 송신한 Data - " + str + "(" + buff.Length + " bytes) - 송신한 시각 : " + DateTime.Now.ToString() + "\n";
            this.set_textbox.SetTBScroll(this.richTextBox1);
        }

        private void Normng_MuxSuccessEvt(int _id)
        {
            Console.WriteLine("일반메시지_먹스성공들어옴");
            byte[] buff = this.normng.MuxProto(_id);
            socketClt.tcpSendMsg(buff);

            this.listView1.Items[_id.ToString()].SubItems[3].Text = "MUX성공 전송";
            this.set_listview.SetFoucus(this.listView1, _id);

            string str = this.Byte2Hex(buff);
            this.richTextBox1.Text += " ◀ 송신한 Data - " + str + "(" + buff.Length + " bytes) - 송신한 시각 : " + DateTime.Now.ToString() + "\n";
            this.set_textbox.SetTBScroll(this.richTextBox1);
        }

        private void Normng_EndSuccessEvt(int _id)
        {
            Console.WriteLine("일반메시지_종료들어옴");
            byte[] buff = this.normng.EndProto(_id);
            socketClt.tcpSendMsg(buff);

            this.listView1.Items.RemoveByKey(_id.ToString());
            this.set_listview.SetIndex(this.listView1);

            string str = this.Byte2Hex(buff);
            this.richTextBox1.Text += " ◀ 송신한 Data - " + str + "(" + buff.Length + " bytes) - 송신한 시각 : " + DateTime.Now.ToString() + "\n";
            this.set_textbox.SetTBScroll(this.richTextBox1);
        }
        #endregion
    }
}
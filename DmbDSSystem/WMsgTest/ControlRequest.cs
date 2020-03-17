using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using ADEng.Library.DMB;
using adeng.comm;

namespace WMsgTest
{
    class ControlRequest
    {
        #region Instance
        DataManager controldatamng = null;
        #endregion

        #region Variable
        private WMessage wmsg = null;
        private WaitForm waitform = new WaitForm();
        private mapMessageRegion mapmsgrg = null;
        private List<mapMessageRegion> mapmsgrgList = new List<mapMessageRegion>();
        #endregion

        /// <summary>
        /// 기본 생성자 - 아직 정의 하지 않았음..
        /// </summary>
        public ControlRequest()
        {
            controldatamng = DataManager.getInstance();
        }

        /// <summary>
        /// WMessage 만든다.
        /// </summary>
        /// <param name="controlflag">
        /// 제어명령이면 1, 일반 메시지면 0
        /// </param>
        /// <param name="tkpriority">
        /// 재난 우선순위 ID
        /// </param>
        /// <param name="tkdisaster">
        /// 재난 코드 ID
        /// </param>
        /// <param name="tkregion">
        /// 재난 지역 지정 코드
        /// </param>
        /// <param name="rcount">
        /// 재난 지역의 수
        /// </param>
        /// <param name="message">
        /// 메시지
        /// </param>
        /// <param name="fkuser">
        /// 발령한 사용자
        /// </param>
        /// <param name="processing">
        /// 현재 진행 상태(진행 중 : 1, 종료 상황 : 0)
        /// </param>
        /// <param name="ttsmsg">
        /// TTS용 메시지
        /// </param>
        /// <param name="smsmsg">
        /// SMS용 메시지
        /// </param>
        public void MakeWMSG(bool controlflag, uint tkpriority, uint tkdisaster, uint tkregion, uint rcount, string message, uint fkuser,
            bool processing, string ttsmsg, string smsmsg, bool dmb, bool tts, bool sms)
        {
            wmsg = new WMessage();

            wmsg.BoolControl = controlflag;
            wmsg.TkPriority = tkpriority;
            wmsg.TkDisaster = tkdisaster;
            wmsg.TkRegion = tkregion;
            wmsg.RCount = rcount;
            wmsg.Message = message;
            wmsg.FkUser = fkuser;
            wmsg.BoolProcessing = processing;
            wmsg.TTSMsg = ttsmsg;
            wmsg.SMSMsg = smsmsg;
            wmsg.DDateTime = DateTime.Now;
            wmsg.SendID = 0; //송출까지 전송은 0으로, 후에 송출에서 조합하여 EWS로 전송함.
            wmsg.SOPT_DMB = dmb;
            wmsg.SOPT_TTS = tts;
            wmsg.SOPT_SMS = sms;
        }

        /// <summary>
        /// 지역과 장비를 연결하는 메소드
        /// </summary>
        /// <param name="flag">
        /// 0=지역, 1=개별장비, 2=그룹
        /// </param>
        /// <param name="pid">
        /// 메시지 목적지 destflag 참조한 후 0이면 region테이블과 포린키, 1이면 device테이블과 포린키
        /// </param>
        /// <param name="pname">
        /// 지역이면 주소또는 이름이 장비이면 장비 이름이 남는다. 추후 행정동 등이 변경되어도 고정으로 나오도록
        /// </param>
        /// <param name="wmsgid">
        /// 재난 메시지의 ID
        /// </param>
        public void MakeMWMSGRG(uint flag, uint pid, string pname)
        {
            try
            {
                mapmsgrg = new mapMessageRegion();

                mapmsgrg.DestFlag = flag;
                mapmsgrg.ParentID = pid;
                mapmsgrg.ParentName = pname;

                this.mapmsgrgList.Add(mapmsgrg);
            }
            catch (Exception ex)
            {
                Console.WriteLine("MakeMWMSGRG - " + ex.Message);
            }
        }

        /// <summary>
        /// mapMessageRegion 클래스를 만들어 리턴한다.
        /// </summary>
        /// <param name="flag">
        /// 0=지역, 1=개별장비, 2=그룹
        /// </param>
        /// <param name="pid">
        /// 메시지 목적지 destflag 참조한 후 0이면 region테이블과 포린키, 1이면 device테이블과 포린키
        /// </param>
        /// <param name="pname">
        /// 지역이면 주소또는 이름이 장비이면 장비 이름이 남는다. 추후 행정동 등이 변경되어도 고정으로 나오도록
        /// </param>
        /// <returns>
        /// mapMessageRegion  클래스
        /// </returns>
        public mapMessageRegion RTMakeMWMSGRG(uint flag, uint pid, string pname)
        {
            try
            {
                mapMessageRegion mapmsgrg = new mapMessageRegion();
                mapmsgrg.DestFlag = flag;
                mapmsgrg.ParentID = pid;
                mapmsgrg.ParentName = pname;

                return mapmsgrg;
            }
            catch (Exception ex)
            {
                Console.WriteLine("RTMakeMWMSGRG - " + ex.Message);
                return new mapMessageRegion();
            }
        }

        /// <summary>
        /// 관제/제어 발령 요청 실행
        /// </summary>
        public bool Processing(string title)
        {
            try
            {
                WMessage tempmsg = new WMessage();
                bool tempdc;
                bool state = false;

                //if (Properties.Settings.Default.totalityFlag == false)
                //{
                //    WaitBarMng.Start();
                //    WaitBarMng.Msg = "데이터 전송중 입니다...";
                //    System.Threading.Thread.Sleep(500);
                //}

                wmsg.MapTarget = this.mapmsgrgList;
                tempmsg = controldatamng.sendWMessage(this.wmsg);
                
                if (tempmsg.ID != (uint)0)
                {
                    if (Properties.Settings.Default.totalityFlag == false)
                    {
                        //WaitBarMng.Close();
                        MessageBox.Show(title + " 요청을 성공하였습니다.", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    state = true;
                }
                else
                {
                    if (Properties.Settings.Default.totalityFlag == false)
                    {
                        //WaitBarMng.Close();
                        MessageBox.Show(title + " 요청을 실패하였습니다.", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    state = false;
                }

                wmsg = null;
                tempdc = false;
                mapmsgrgList.Clear();
                controldatamng.DeviceControlList.Clear();

                return state;
            }
            catch (Exception ex)
            {
                WaitBarMng.Close();
                MessageBox.Show(title + " 요청을 실패하였습니다.", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Console.WriteLine("Processing - " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// mapmsgrgList의 count를 반환한다. 지역의 수를 의미한다.
        /// </summary>
        public uint MapmsgrgList
        {
            get
            {
                return (uint)mapmsgrgList.Count;
            }
        }
    }
}
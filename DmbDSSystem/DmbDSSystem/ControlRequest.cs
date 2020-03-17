using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using ADEng.Library.DMB;
using ADEng.Control.DMB;
using ADEng.dmbcomm;

namespace DmbDSSystem
{
    class ControlRequest
    {
        #region Instance
        DataManager controldatamng = null;
        #endregion

        #region Variable
        private WMessage wmsg = null;
        private WaitForm waitform = new WaitForm();
        private mapMessageRegion mapmsgrg = new mapMessageRegion();
        private List<mapMessageRegion> mapmsgrgList = new List<mapMessageRegion>();
        #endregion

        /// <summary>
        /// 기본 생성자
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
            bool processing, string ttsmsg, string smsmsg, DateTime datetime, uint id, List<mapMessageRegion> mapmessageregion)
        {
            try
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
                wmsg.DDateTime = datetime;
                wmsg.ID = id;
                wmsg.MapTarget = mapmessageregion;
            }
            catch (Exception ex)
            {
                Console.WriteLine("MakeWMSG - " + ex.Message);
            }
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
        public void MakeMWMSGRG(uint flag, uint pid, string pname)
        {
            mapmsgrg.DestFlag = flag;
            mapmsgrg.ParentID = pid;
            mapmsgrg.ParentName = pname;

            this.mapmsgrgList.Add(mapmsgrg);
        }

        /// <summary>
        /// 관제/제어 발령 요청 실행
        /// </summary>
        public bool Processing(string title)
        {
            try
            {
                WMessage tempmsg = new WMessage();
                bool state = false;
                
                tempmsg = controldatamng.sendWMessage(this.wmsg);

                if (tempmsg == null)
                {
                    MessageBox.Show(title + " 요청을 실패하였습니다.", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return false;
                }

                WaitBarMng.Start();
                WaitBarMng.Msg = "데이터 전송중 입니다...";
                System.Threading.Thread.Sleep(1000);

                if (tempmsg.ID != (uint)0)
                {
                    WaitBarMng.Close();
                    MessageBox.Show(title + " 요청을 성공하였습니다.", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    state = true;
                }
                else
                {
                    WaitBarMng.Close();
                    MessageBox.Show(title + " 요청을 실패하였습니다.", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    state = false;
                }

                wmsg = null;

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
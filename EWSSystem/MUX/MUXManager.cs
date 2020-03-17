using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DmbProtocol;
using System.Threading;
using System.Net;
using System.IO;
using System.Diagnostics;
using TextLog;


namespace MUX
{
    public class MUXManager
    {
        #region 멤버
        public List<MUXInfo> muxInfoList { get; set; }

        Thread MUXStatusCheckTD { get; set; }

        public delegate void ChangeMUXStatus(string muxIp, bool isNormal);
        public event ChangeMUXStatus evtChangeMUXStatus;
        #endregion


        /// <summary>
        /// 기본 생성자
        /// </summary>
        public MUXManager()
        {
            //먹스 리스트를 기본으로 10개 생성한다.
            this.muxInfoList = new List<MUXInfo>(10);

            try
            {
                //Thread
                MUXStatusCheckTD = new Thread(new ThreadStart(CheckMUX));
                if (MUXStatusCheckTD != null)
                    MUXStatusCheckTD.Start();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MUX.MUXManager.MUXManager ()| " + ex.Message);
                Log.WriteLog("MUX.MUXManager.MUXManager ()| " + ex.Message);
            }
        }

        /// <summary>
        /// MUX 상태를 요청한다. (간격 1초)
        /// </summary>
        /// <returns></returns>
        public void CheckMUX()
        {
            try
            {
                while (true)
                {
                    for (int i = 0; i < muxInfoList.Count; i++)
                    {
                        //함수 요청
                        bool res = RequesMUXStatusMsgToMUX(muxInfoList[i].MUXIp);

                        if (res == true)
                        {
                            if (muxInfoList[i].status != true)
                            {
                                muxInfoList[i].status = true;
                                this.evtChangeMUXStatus(muxInfoList[i].MUXIp, true);
                            }
                        }
                        else if (res == false)
                        {
                            if (muxInfoList[i].status != false)
                            {
                                muxInfoList[i].status = false;
                                this.evtChangeMUXStatus(muxInfoList[i].MUXIp, false);
                            }
                        }
                    }
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MUX.MUXManager.CheckMUX ()| " + ex.Message);
                Log.WriteLog("MUX.MUXManager.CheckMUX ()| " + ex.Message);
            }
        }


        /// <summary>
        /// 먹스 상태 체크 한번( 송출 시스템 요청 시)
        /// </summary>
        public bool CheckMUXOnce()
        {
            try
            {
                for (int i = 0; i < muxInfoList.Count; i++)
                {
                    //함수 요청
                    bool res = RequesMUXStatusMsgToMUX(muxInfoList[i].MUXIp);

                    if (res == true)
                    {
                        if (muxInfoList[i].status != true)
                        {
                            muxInfoList[i].status = true;
                            this.evtChangeMUXStatus(muxInfoList[i].MUXIp, true);
                        }
                    }
                    else if (res == false)
                    {
                        if (muxInfoList[i].status != false)
                        {
                            muxInfoList[i].status = false;
                            this.evtChangeMUXStatus(muxInfoList[i].MUXIp, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MUX.MUXManager.CheckMUXOnce()| " + ex.Message);
                Log.WriteLog("MUX.MUXManager.CheckMUXOnce()| " + ex.Message);

                return false;
            }

            return true;
        }


        /// <summary>
        /// MUX에  재난메세지를 전송한다.
        /// </summary>
        /// <returns></returns>
        public bool RequestSendEmergencyToMUX(bool isBool, byte[] sendBytes)
        {
            try
            {
                #region 샘플 소스 (참고용)
                //// Create API proxy and init authentication header
                //Factum.DabCtrlApi api = new Factum.DabCtrlApi();
                //api.Url = "http://" + m_HostBox.Text;
                //api.AuthorizationValue = new Factum.Authorization();
                //api.AuthorizationValue.Username = m_UserBox.Text;
                //api.AuthorizationValue.Password = m_PassBox.Text;
                //api.AuthorizationValue.AccessLevel = 20;

                //bool isMsg = m_MsgCheckBox.Checked;
                //int tcid = System.Int32.Parse(m_TcIdTextBox.Text);

                //String txt = m_EwsTextBox.Text;
                //System.Byte[] ews = hexStringToByteArr(txt);

                //// Send Emergency Warning
                //api.RequestStopEmergencyWarning(isMsg, tcid, ews);

                //MessageBox.Show("Emergency warning stopped.\n");
                #endregion

                // Create API proxy and init authentication header
                Factum.DabCtrlApi api = new Factum.DabCtrlApi();
                api.Url = "http://" + "localhost";
                api.AuthorizationValue = new Factum.Authorization();
                api.AuthorizationValue.Username = "admin";
                api.AuthorizationValue.Password = "admin";
                api.AuthorizationValue.AccessLevel = 20;

                //TCID
                int tcid = System.Int32.Parse("7");

                //EWS 전송 Data
                System.Byte[] ews = sendBytes;

                //  EWS 로 전송
                api.RequestSendEmergencyWarning(isBool, tcid, ews);

            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("MUX.MUXManager.RequestSendEmergencyToMUX()| " + ex.Message);
                Log.WriteLog("MUX.MUXManager.RequestSendEmergencyToMUX()| " + ex.Message);

                return false;
            }

            return true;
        }


        /// <summary>
        /// MUX 에 MUX 상태 메세지를 요청한다.
        /// </summary>
        /// <returns></returns>
        public bool RequesMUXStatusMsgToMUX(string muxIp)
        {
            //반환 값
            bool isNormal = true;
            /////////////// Try catch //////////////
            //1. 먹스로 보내기
            //2. 리턴값 받아오기

            //3. 반환한다. ( '1'=정상, '0'-비정상)


            return isNormal;

            #region 이전 바이트 로직
            //byte[] tmpBuff = new byte[12];
            //switch (retMUX)
            //{
            //    case 0:
            //        tmpBuff = new byte[] { 0xAA, 0x55, 0x01, 0x00, 0x00, 0x00, 0x01, 0x30 };
            //        break;
            //    case 1:
            //        tmpBuff = new byte[] { 0xAA, 0x55, 0x01, 0x00, 0x00, 0x00, 0x01, 0x31 };
            //        break;
            //}
            #endregion
        }


        /// <summary>
        ///  MUX 에 발령 취소 메세지를 요청한다.
        /// </summary>
        /// <returns></returns>
        public bool RequestMsgCancelToMUX(bool isControl, byte[] rBuffer)
        {
            #region 원본 소스
            //try
            //{
            //    // Create API proxy and init authentication header
            //    Factum.DabCtrlApi api = new Factum.DabCtrlApi();
            //    api.Url = "http://" + m_HostBox.Text;
            //    api.AuthorizationValue = new Factum.Authorization();
            //    api.AuthorizationValue.Username = m_UserBox.Text;
            //    api.AuthorizationValue.Password = m_PassBox.Text;
            //    api.AuthorizationValue.AccessLevel = 20;

            //    bool isMsg = m_MsgCheckBox.Checked;
            //    int tcid = System.Int32.Parse(m_TcIdTextBox.Text);

            //    String txt = m_EwsTextBox.Text;
            //    System.Byte[] ews = hexStringToByteArr(txt);

            //    // Send Emergency Warning
            //    api.RequestStopEmergencyWarning(isMsg, tcid, ews);

            //    MessageBox.Show("Emergency warning stopped.\n");
            //}
            //catch (System.Exception ex)
            //{
            //    MessageBox.Show("Failed to stop emergency warning.\n" + ex.Message);
            //}
            #endregion

            try
            {
                // Create API proxy and init authentication header
                Factum.DabCtrlApi api = new Factum.DabCtrlApi();
                api.Url = "http://" + "localhost";
                api.AuthorizationValue = new Factum.Authorization();
                api.AuthorizationValue.Username = "";
                api.AuthorizationValue.Password = "";
                api.AuthorizationValue.AccessLevel = 20;

                bool isMsg = isControl;

                int tcid = System.Int32.Parse("7");

                System.Byte[] ews = rBuffer;

                // Send Emergency Warning
                api.RequestStopEmergencyWarning(isMsg, tcid, ews);
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("MUX.MUXManager.RequestMsgCancelToMUX()| " + ex.Message);
                Log.WriteLog("MUX.MUXManager.RequestMsgCancelToMUX()| " + ex.Message);

                return false;
            }

            return true;
        }

    }
}

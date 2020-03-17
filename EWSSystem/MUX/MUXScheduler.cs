#define debug
//#define real
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Configuration;
using System.Collections;
using System.Diagnostics;
using TextLog;

namespace MUX
{
    public class MUXScheduler
    {
        #region 멤버
        //변수
        public MUXManager muxManager { get; set; }
        public List<MUXMsg> norQueue { get; set; }
        public List<MUXMsg> spcQueue { get; set; }

        // 메세지 고유 ID
        public byte nowSendId { get; set; }

        //// 0 ~ maxSendId 까지의 간격
        //public int maxSendId { get; set; }

        #region 이전메세지 특수, 일반 maxId, sendId
        //public byte spcNowSendId { get; set; }
        //public int maxSpcSendId { get; set; }
        //public int maxNorSendId { get; set; }
        #endregion

        //Thread
        Thread spcScheduleTD { get; set; }
        Thread norScheduleTD { get; set; }

        //이벤트

        //특수
        public delegate void NowSendToMUX(string sessionId, string genId, string startDateTime);
        public event NowSendToMUX evtNowSendToMUX;

        public delegate void FinishSendToMUX(string sessionId, string genId, string endDateTime);
        public event FinishSendToMUX evtFinishSendToMUX;

        public delegate void ErrorSendToMUX(string sessionId, string genId, string endDateTime);
        public event ErrorSendToMUX evtErrorSendToMUX;


        //일반
        public delegate void NowNorSendToMUX(string sessionId, string genId, string startDateTime);
        public event NowNorSendToMUX evtNowNorSendToMUX;

        public delegate void FinishNorSendToMUX(string sessionId, string genId, string endDateTime);
        public event FinishNorSendToMUX evtFinishNorSendToMUX;

        public delegate void ErrorNorSendToMUX(string sessionId, string genId, string endDateTime);
        public event ErrorNorSendToMUX evtErrorNorSendToMUX;
        #endregion


        /// <summary>
        /// 기본 생성자
        /// </summary>
        public MUXScheduler()
        {
            //멤버 변수
            try
            {
                muxManager = new MUXManager();

                this.norQueue = new List<MUXMsg>();
                this.spcQueue = new List<MUXMsg>();

                //this.spcCycle = Convert.ToInt32(ConfigurationManager.AppSettings["SpcCycle"]);
                //this.norCycle = Convert.ToInt32(ConfigurationManager.AppSettings["NorCycle"]);

                //this.nowSendId = 0;
                //this.maxSendId = Convert.ToInt32(ConfigurationManager.AppSettings["MaxId"]);

                #region 12/04/24 이전 멤버 값 세팅
                //this.spcNowSendId = 0;
                //this.norNowSendId = 0;
                //this.maxSpcSendId = Convert.ToInt32(ConfigurationManager.AppSettings["SpcMaxId"]);
                //this.maxNorSendId = Convert.ToInt32(ConfigurationManager.AppSettings["NorMaxId"]);
                #endregion


                // Thread
                //특수 
                spcScheduleTD = new Thread(new ThreadStart(SpcSchedulling));
                if (spcScheduleTD != null)
                    spcScheduleTD.Start();

                //일반
                norScheduleTD = new Thread(new ThreadStart(NorSchedulling));
                if (norScheduleTD != null)
                    norScheduleTD.Start();

            }
            catch (Exception ex)
            {
                Debug.WriteLine("MUX.MUXScheduler.MUXScheduler()| " + ex.Message);
                Log.WriteLog("MUX.MUXScheduler.MUXScheduler()| " + ex.Message);
            }
        }


        /// <summary>
        /// 일반 메세지 스케줄링
        /// </summary>
        /// <returns></returns>
        private void NorSchedulling()
        {
            while (true)
            {
                if (norQueue.Count > 0)
                {
                    List<byte[]> segList = null;

                    for (int i = 0; i < norQueue.Count; i++)
                    {
                        try
                        {
                            segList = SplitMUXMsg(norQueue[i]);

                            if (segList.Count != 0)
                            {
                                int tickTime = Environment.TickCount;

                                this.evtNowNorSendToMUX(norQueue[i].sessionId, norQueue[i].genId, DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss"));

                                norQueue[i].sendCount++;

                                tickTime = Environment.TickCount;
                                bool isOverTime = false;

                                while (true)
                                {
                                    for (int j = 0; j < segList.Count; j++)
                                    {
                                        muxManager.RequestSendEmergencyToMUX(true, segList[j]);
                                        if (Environment.TickCount - tickTime > Convert.ToInt32(ConfigurationManager.AppSettings["NorCycle"]))
                                        {
                                            isOverTime = true;
                                            break;
                                        }
                                    }//세그먼트리스트 포문
                                    Thread.Sleep(100);

                                    if (isOverTime == true)
                                    {
                                        break;
                                    }
                                }

                                if (norQueue[i].sendCount >= Convert.ToInt32(ConfigurationManager.AppSettings["NorRepeatNum"]))
                                {
                                    this.evtFinishNorSendToMUX(norQueue[i].sessionId, norQueue[i].genId, DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss"));
                                    norQueue.RemoveAt(i);
                                    break;
                                }
                            }
                            else
                            {
                                throw new Exception();
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("MUX.MUXScheduler.NorSchedulling()| " + ex.Message);
                            Log.WriteLog("MUX.MUXScheduler.NorSchedulling()| " + ex.Message);

                            this.evtErrorNorSendToMUX(norQueue[i].sessionId, norQueue[i].genId, DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss"));
                            norQueue.RemoveAt(i);
                        }
                    }//큐 포문
                }
                Thread.Sleep(100);
            }

            #region 일반 메세지 이전 소스
            //            while (true)
            //            {
            //                if (norQueue.Count > 0)
            //                {
            //                    //몇번의 패키지가 있는지.
            //                    //마지막 패키지의 개수는 몇개인지
            //                    int totPack = (norQueue.Count / 8) + 1;
            //                    int lastPackCnt = norQueue.Count % 8;
            //                    if (lastPackCnt == 0) totPack--;
            //                    int maxCount = 8;

            //                    //bool isMsgEnd = false;
            //                    for (int i = 0; i < totPack; i++)
            //                    {
            //                        try
            //                        {
            //                            if (i == (totPack - 1)) maxCount = lastPackCnt;
            //                            else maxCount = 8;

            //                            for (int j = i * 8; j < i * 8 + maxCount; j++)
            //                            {
            //                                //if (norQueue[j].sendCount == 0)
            //                                //{

            //                                this.evtNowNorSendToMUX(norQueue[j].sessionId, norQueue[j].genId, DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss"));
            //                                //}

            //                                muxManager.RequestSendEmergencyToMUX(true, norQueue[j].message);

            //                                norQueue[j].sendCount++;
            //                            }
            //#if(debug)
            //                            Thread.Sleep(2000);
            //                            this.evtNowNorSendToMUX("", "", DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss"));
            //#elif(real)
            //                                    Thread.Sleep(7000);
            //#endif
            //                        }
            //                        catch (Exception ex)
            //                        {
            //                            this.evtErrorNorSendToMUX(norQueue[i].sessionId, norQueue[i].genId, DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss"));
            //                            norQueue.RemoveAt(i);
            //                            Console.WriteLine("MUXScheduler.NorSchedulling - " + ex.Message);
            //                        }
            //                    }

            //                    for (int i = 0; i < norQueue.Count; i++)
            //                    {
            //                        if (norQueue[i].sendCount > 2)
            //                        {
            //                            //메인의 일반메세지 끝 이벤트 부르기
            //                            this.evtFinishNorSendToMUX(norQueue[i].sessionId, norQueue[i].genId, DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss"));
            //                            norQueue.RemoveAt(i);
            //                            //isMsgEnd = true;
            //                        }
            //                    }
            //                }//if (norQueue.Count > 0)
            //                Thread.Sleep(500);
            //            }//while(true)
            #endregion

        }


        #region NorQueue 이전 로직
        //                        if (norQueue.Count > 8)
        //                        {

        //                        }
        //                        int maxCount = 8;
        //                        if (norQueue.Count < 8) maxCount = norQueue.Count;

        //                        //int tickTime = Environment.TickCount;
        //                        //bool isMsgEnd = false;

        //                        for (int i = 0; i < maxCount; i++)
        //                        {
        //                            if (norQueue[i].sendCount == 0)
        //                            {
        //                                this.evtNowNorSendToMUX(norQueue[i].sessionId, norQueue[i].genId, DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss"));
        //                            }

        //                            muxManager.RequestSendEmergencyToMUX(true, norQueue[i].message);

        //                            norQueue[i].sendCount++;
        //#if(debug)
        //                            Thread.Sleep(3000);
        //#elif(real)
        //                            Thread.Sleep(7000);
        //#endif
        //                            //if (isMsgEnd == true) break;
        //                        }
        //                    }//while 일반 Queue
        //                    Thread.Sleep(500);
        //                }//if count > 0


        //if (norQueue[i].sendCount > 2)
        //{
        //    //메인의 일반메세지 끝 이벤트 부르기
        //    this.evtFinishNorSendToMUX(norQueue[i].sessionId, norQueue[i].genId, DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss"));
        //    norQueue.RemoveAt(i);
        //    isMsgEnd = true;
        //    break;
        //}
        #endregion


        /// <summary>
        /// 특수 메세지 스케줄링
        /// </summary>
        /// <returns></returns>
        private void SpcSchedulling()
        {
            while (true)
            {
                if (spcQueue.Count > 0)
                {
                    List<byte[]> segList = null;

                    for (int i = 0; i < spcQueue.Count; i++)
                    {
                        try
                        {
                            segList = SplitMUXMsg(spcQueue[i]);

                            if (segList.Count != 0)
                            {
                                int tickTime = Environment.TickCount;

                                this.evtNowSendToMUX(spcQueue[i].sessionId, spcQueue[i].genId, DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss"));

                                spcQueue[i].sendCount++;

                                tickTime = Environment.TickCount;
                                bool isOverTime = false;

                                while (true)
                                {
                                    for (int j = 0; j < segList.Count; j++)
                                    {
                                        muxManager.RequestSendEmergencyToMUX(true, segList[j]);
#if(debug)
                                        if (Environment.TickCount - tickTime > 3000)
#elif(real)
                                            if (Environment.TickCount - tickTime > Convert.ToInt32(ConfigurationManager.AppSettings["SpcCycle"]))
#endif
                                        {
                                            isOverTime = true;
                                            break;
                                        }
                                    }//세그먼트리스트 포문
                                    Thread.Sleep(100);

                                    if (isOverTime == true)
                                    {
                                        break;
                                    }
                                }

                                if (spcQueue[i].sendCount >= Convert.ToInt32(ConfigurationManager.AppSettings["SpcRepeatNum"]))
                                {
                                    this.evtFinishSendToMUX(spcQueue[i].sessionId, spcQueue[i].genId, DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss"));
                                    spcQueue.RemoveAt(i);
                                    break;
                                }
                            }
                            else
                            {
                                throw new Exception();
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("MUX.MUXScheduler.SpcSchedulling()| " + ex.Message);
                            Log.WriteLog("MUX.MUXScheduler.SpcSchedulling()| " + ex.Message);

                            this.evtErrorSendToMUX(spcQueue[i].sessionId, spcQueue[i].genId, DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss"));
                            spcQueue.RemoveAt(i);
                        }
                    }//큐 포문
                }
                Thread.Sleep(100);
            }
        }


        /// <summary>
        /// MUX로 보낼 만큼의 byte[] 로 잘라 리스트로 반환한다.
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        private List<byte[]> SplitMUXMsg(MUXMsg muxMsg)
        {
            List<byte[]> segList = new List<byte[]>(); //반환할 세그먼트 리스트

            try
            {
                //받은 byteArray 의 개수를 26byte 로 나눈 개수
                int listCount = (muxMsg.message.Length / 26) + 1;

                // 받은 byteArray 의 개수를 26으로 나눈 나머지
                int listLeft = muxMsg.message.Length % 26;

                //복사를 위해 필요한 임시 byte 배열
                byte[] segArray = null;

                /////////////////////////////Segment Header//////////////////////////////
                int segHeader = 0;

                //1. DC(1) 
                // '1' : d2=0 세그먼트 전송 이후, D2=1 인 데이터가 전송 예정임을 나타냄.
                // '0' : d2=0 세그먼트 전송 이후, D2=1 인 데이터 전송 안됨.
                int dc = 1;
                dc = dc << 31;
                segHeader = dc | segHeader;


                //2. Valid_bit(1)
                //'1': data 에 유용한 정보가 있음.
                //'0': data 가 Null 임.
                int validBit = 1;
                validBit = validBit << 30;
                segHeader = validBit | segHeader;


                //3. Provider(3)
                // '000' :소방방재청
                int provider = 0;
                provider = provider << 27;
                segHeader = validBit | segHeader;


                //4. Version(3)
                //: '000'
                int version = 0;
                version = version << 24;
                segHeader = validBit | segHeader;

                ////5. Identification(8)
                ////메세지 고유 ID (0~255 까지)
                //int id = 0;
                //id = id << 16;
                //segHeader = id | segHeader;

                //6. Total Segment(6)////
                //메세지 전체 세그먼트 수(받은 byte 를 26byte 로 나눈 수)
                int totSeg = listCount;
                totSeg = totSeg << 10;
                segHeader = totSeg | segHeader;

                ////7. Current Segment(6)
                ////메세지의 현재 세그먼트 번호 (현재 전송중인 세그먼트)
                //int curSeg = 0;
                //curSeg = curSeg << 4;
                //curSeg = curSeg | segHeader;

                //8. Extension(4) - Version '000' : 사용 안함
                int extension = 0;
                extension = extension << 0;
                extension = extension | segHeader;


                if (muxMsg.message.Length <= 26)
                {

                    //5. Identification(8)/////////////////////////////////
                    //메세지 고유 ID (0~maxid 까지)
                    int id = nowSendId;
                    id = id << 16;
                    segHeader = id | segHeader;

                    if (this.nowSendId < Convert.ToInt32(ConfigurationManager.AppSettings["MaxId"]))
                    {
                        this.nowSendId++;
                    }
                    else
                    {
                        this.nowSendId = 0;
                    }

                    //7. Current Segment(6)
                    //메세지의 현재 세그먼트 번호 (현재 전송중인 세그먼트)
                    int curSeg = 1;
                    curSeg = curSeg << 4;
                    curSeg = curSeg | segHeader;

                    segArray = new byte[listLeft + sizeof(int)];
                    Array.Copy(BitConverter.GetBytes(segHeader), 0, segArray, 0, sizeof(int));
                    Array.Copy(muxMsg.message, 0, segArray, 0, listLeft);
                    segList.Add(segArray);
                }
                else
                {
                    //현재 세그먼트 번호
                    //전체 세그먼트 번호
                    //재난 메세지 ID
                    // byte tempSendId = 0;

                    for (byte i = 0; i < listCount; i++)
                    {
                        //5. Identification(8)
                        //메세지 고유 ID (0~maxid 까지)
                        int id = nowSendId;
                        id = id << 16;
                        segHeader = id | segHeader;

                        if (this.nowSendId < Convert.ToInt32(ConfigurationManager.AppSettings["MaxId"]))
                        {
                            this.nowSendId++;
                        }
                        else
                        {
                            this.nowSendId = 0;
                        }

                        //7. Current Segment(6)
                        //메세지의 현재 세그먼트 번호 (현재 전송중인 세그먼트)
                        int curSeg = (i + 1);
                        curSeg = curSeg << 4;
                        segHeader = curSeg | segHeader;


                        #region 비트 검증을 위한 출력 코드
                        //BitArray ba = new BitArray(8);
                        //byte[] b1 = { BitConverter.GetBytes(segHeader)[0] };
                        //byte[] b2 = { BitConverter.GetBytes(segHeader)[1] };
                        //byte[] b3 = { BitConverter.GetBytes(segHeader)[2] };
                        //byte[] b4 = { BitConverter.GetBytes(segHeader)[3] };

                        //ba = new BitArray(b1);
                        //showbits("4 of 1:", ba);
                        //ba = new BitArray(b2);
                        //showbits("4 of 2:", ba);
                        //ba = new BitArray(b3);
                        //showbits("4 of 3:", ba);
                        //ba = new BitArray(b4);
                        //showbits("4 of 4:", ba);
                        #endregion

                        /////////////////////////////Segment Data//////////////////////////////
                        if (i == (listCount - 1))
                        {
                            segArray = new byte[listLeft + sizeof(int)];
                            Array.Copy(BitConverter.GetBytes(segHeader), 0, segArray, 0, sizeof(int));
                            Array.Copy(muxMsg.message, i * 26, segArray, 2, listLeft);
                        }
                        else
                        {
                            segArray = new byte[26 + sizeof(int)];
                            Array.Copy(BitConverter.GetBytes(segHeader), 0, segArray, 0, sizeof(int));
                            Array.Copy(muxMsg.message, i * 26, segArray, 2, 26);
                        }

                        //Segment Array 에 추가
                        segList.Add(segArray);
                    }
                }
            }
            catch (Exception ex)
            {
                segList.Clear(); //세그먼트 리스트의 요소를 모두 지운다.

                Debug.WriteLine("MUX.MUXScheduler.SplitMUXMsg()| " + ex.Message);
                Log.WriteLog("MUX.MUXScheduler.SplitMUXMsg()| " + ex.Message);
            }

            return segList;
        }


        #region  비트 검증을 위한 출력 코드
        //public static void showbits(string rem, BitArray bits)
        //{
        //    Console.WriteLine(rem);
        //    for (int i = bits.Count - 1; i > -1; i--)
        //    {
        //        Console.Write("{0, -6} ", bits[i]);
        //    }
        //    Console.WriteLine("\n");
        //}
        #endregion


        /// <summary>
        /// 특수 수신기 스케줄러 큐에 쌓다
        /// </summary>
        /// <param name="muxMessage"></param>
        /// <returns></returns>
        public bool AddSpcQueue(MUXMsg muxMessage)
        {
            this.spcQueue.Add(muxMessage);

            return true;
        }


        /// <summary>
        /// 일반 수신기 스케줄러 큐에 쌓다
        /// </summary>
        /// <param name="muxMessage"></param>
        /// <returns></returns>
        public bool AddNorQueue(MUXMsg muxMessage)
        {
            this.norQueue.Add(muxMessage);

            return true;
        }
    }


}

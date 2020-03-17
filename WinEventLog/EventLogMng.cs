using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ADEng.Library.DMB
{
    public class EventLogMng
    {
        #region 필드

        #endregion

        #region 속성

        #endregion

        public EventLogMng()
        {

        }

        /// <summary>
        /// 로컬의 윈도우 이벤트로그를 기록을 읽어온다.
        /// </summary>
        /// <param name="_sourceName">이벤트 뷰어의 소스명</param>
        /// <param name="_startTime">조회할 시간의 시작</param>
        /// <param name="_endTime">조회할 시간의 종료</param>
        /// <returns></returns>
        public List<Log> ReadLog(string _sourceName, DateTime _startTime, DateTime _endTime)
        {
            List<Log> logList = new List<Log>();
            Log log = null;
            EventLog eventLog = new EventLog();

          
            if (!EventLog.SourceExists(_sourceName))
            {
                EventLog.CreateEventSource(_sourceName, _sourceName);
            }
                
            eventLog.Log = _sourceName;
            
            foreach (EventLogEntry eventLogEntry in eventLog.Entries)
            {
                //if (eventLogEntry.TimeWritten.Date.ToFileTime() >= _startTime.Date.ToFileTime()
                //    && eventLogEntry.TimeWritten.Date.ToFileTime() <= _endTime.Date.ToFileTime())
                if ((eventLogEntry.TimeWritten) >= _startTime
                   && (eventLogEntry.TimeWritten) <= _endTime)
                {
                    log = new Log();
                   
                    log.Kind = eventLogEntry.EntryType.ToString();
                    log.Date = eventLogEntry.TimeWritten.Date.ToString().Substring(0, 10);
                    log.Time = eventLogEntry.TimeWritten.TimeOfDay.ToString();
                    log.Message = eventLogEntry.Message;
                    log.UserName = Encoding.Default.GetString(eventLogEntry.Data);

                    logList.Add(log);
                }              
            }

            return logList;           

        }

        public List<Log> ReadLog(string _sourceName)
        {
            List<Log> logList = new List<Log>();
            Log log = null;
            EventLog eventLog = new EventLog();


            if (!EventLog.SourceExists(_sourceName))
            {
                EventLog.CreateEventSource(_sourceName, _sourceName);
            }

            eventLog.Log = _sourceName;

            int cnt = 1000;

            foreach (EventLogEntry eventLogEntry in eventLog.Entries)
            {
                if(cnt > 0)
                {
                    log = new Log();

                    log.Kind = eventLogEntry.EntryType.ToString();
                    log.Date = eventLogEntry.TimeWritten.Date.ToString().Substring(0, 10);
                    log.Time = eventLogEntry.TimeWritten.TimeOfDay.ToString();
                    log.Message = eventLogEntry.Message;
                    log.UserName = Encoding.Default.GetString(eventLogEntry.Data);

                    logList.Add(log);

                    cnt--;
                }
            }

            return logList;

        }

        /// <summary>
        /// 로컬에 윈도우 이벤트 로그를 기록한다.
        /// </summary>
        /// <param name="_sourceName">기록하려는 이벤트뷰어 대상 소스</param>
        /// <param name="_type">이벤트뷰어의 종류</param>
        /// <param name="_message">이벤트기록의 내용</param>
        /// <param name="_userName">로그인된 사용자이름</param>
        /// <returns></returns>
        public bool WriteLog(string _sourceName, EventLogEntryType _type, string _message, string _userName)
        {
            try
            {
                EventLog eventLog = new EventLog();
                byte[] rawData;

                if (!EventLog.SourceExists(_sourceName))
                {
                    EventLog.CreateEventSource(_sourceName, _sourceName);
                }

                eventLog.Log = _sourceName;
                eventLog.Source = _sourceName + "_event";
                rawData = Encoding.Default.GetBytes(_userName);

                eventLog.WriteEntry(_message, _type, 0, 0, rawData);
               
                return true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return false;
            }           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_sourceName"></param>
        /// <param name="_source"></param>
        /// <param name="_type"></param>
        /// <param name="_message"></param>
        /// <param name="_userName"></param>
        /// <returns></returns>
        public bool WriteLog(string _sourceName, string _source, EventLogEntryType _type, string _message, string _userName)
        {
            try
            {
                EventLog eventLog = new EventLog();
                byte[] rawData;

                if (!EventLog.SourceExists(_sourceName))
                {
                    EventLog.CreateEventSource(_sourceName, _sourceName);
                }

                eventLog.Log = _sourceName;
                eventLog.Source = _source;
                rawData = Encoding.Default.GetBytes(_userName);

                eventLog.WriteEntry(_message, _type, 0, 0, rawData);

                return true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return false;
            }
        }
       

        /// <summary>
        /// 로컬에 윈도우 이벤트 로그를 삭제한다.
        /// </summary>
        /// <param name="_sourceName">삭제하려는 이벤트뷰어 대상 소스</param>
        /// <returns></returns>
        public bool DeleteLog(string _sourceName)
        {
            try
            {
                if (EventLog.SourceExists(_sourceName))
                {
                    EventLog.Delete(_sourceName);
                }

                return true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return false;
            }
        }

    }
   
    public class Log
    {
        #region 필드
        private string kind = String.Empty;
        private string date = String.Empty;
        private string time = String.Empty;
        private string message = String.Empty;        
        private string userName = String.Empty;
        #endregion

        #region 속성
        public string Kind
        {
            get { return kind; }
            set { kind = value; }
        }

        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        public string Time
        {
            get { return time; }
            set { time = value; }
        }
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        #endregion

               
        public Log()
        {

        }


    }
}

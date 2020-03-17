using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

using ADEng.Library.DMB;

namespace DmbDSSystem
{
    public class LogManager
    {
        private DataManager dataMng = null;
        private EventLogMng eventLogMng = null;

        /// <summary>
        /// 기본생성자
        /// </summary>
        public LogManager()
        {
            dataMng = DataManager.getInstance();
            eventLogMng = new EventLogMng();
        }

        /// <summary>
        /// 디렉토리 검사 후 없으면 생성
        /// </summary>
        public void Dir_Mng()
        {
            try
            {
                if (!(Directory.Exists(Directory.GetCurrentDirectory() + "\\Log")))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Log");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("LogManager.Dir_Mng - " + ex.Message);
            }
        }

        /// <summary>
        /// 파일 검사 후 없으면 생성 후 로그
        /// </summary>
        /// <param name="msgid">발령메시지 pkid</param>
        /// <param name="date">발령메시지 발령시간</param>
        /// <param name="data">발령메시지 로우데이터(16진수)</param>
        public void File_Mng(string msgid, string date, string data)
        {
            try
            {
                string filename = DateTime.Now.ToString("yyyyMMdd") + ".txt";

                if (!(File.Exists(Directory.GetCurrentDirectory() + "\\Log\\" + filename)))
                {
                    File.Create(Directory.GetCurrentDirectory() + "\\Log\\" + filename);
                }

                File.AppendAllText(Directory.GetCurrentDirectory() + "\\Log\\" + filename
                    , Environment.NewLine + "======================================================================================");
                File.AppendAllText(Directory.GetCurrentDirectory() + "\\Log\\" + filename, Environment.NewLine + "메시지ID - " + msgid + ", 날짜/시간 - " + date);
                File.AppendAllText(Directory.GetCurrentDirectory() + "\\Log\\" + filename, Environment.NewLine + data);
                File.AppendAllText(Directory.GetCurrentDirectory() + "\\Log\\" + filename
                    , Environment.NewLine + "======================================================================================" + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("LogManager.File_Mng(string msgid, string date, string data) - " + ex.Message);
            }
        }

        /// <summary>
        /// 파일 검사 후 없으면 생성 후 로그
        /// </summary>
        /// <param name="_etcData">발령시간, 받은시간, 아이디 등</param>
        /// <param name="_data">발령메시지 로우데이터(16진수)</param>
        public void File_Mng(string _etcData, string _data)
        {
            try
            {
                string filename = DateTime.Now.ToString("yyyyMMdd") + ".txt";

                if (!(File.Exists(Directory.GetCurrentDirectory() + "\\Log\\" + filename)))
                {
                    File.Create(Directory.GetCurrentDirectory() + "\\Log\\" + filename);
                }

                File.AppendAllText(Directory.GetCurrentDirectory() + "\\Log\\" + filename
                    , Environment.NewLine + "======================================================================================");
                File.AppendAllText(Directory.GetCurrentDirectory() + "\\Log\\" + filename, Environment.NewLine + _etcData);
                File.AppendAllText(Directory.GetCurrentDirectory() + "\\Log\\" + filename, Environment.NewLine + _data);
                File.AppendAllText(Directory.GetCurrentDirectory() + "\\Log\\" + filename
                    , Environment.NewLine + "======================================================================================" + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("LogManager.File_Mng(string _etcData, string _data) - " + ex.Message);
            }
        }

        /// <summary>
        /// 파일 검사 후 없으면 생성 후 로그(송출이 받은 최대 Low 데이터)
        /// </summary>
        /// <param name="_data">
        /// 로그에 남길 데이터
        /// </param>
        public void File_Mng(string _data)
        {
            try
            {
                string filename = DateTime.Now.ToString("yyyyMMdd") + ".txt";

                if (!(File.Exists(Directory.GetCurrentDirectory() + "\\Log\\" + filename)))
                {
                    File.Create(Directory.GetCurrentDirectory() + "\\Log\\" + filename);
                }

                File.AppendAllText(Directory.GetCurrentDirectory() + "\\Log\\" + filename
                    , Environment.NewLine + ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                File.AppendAllText(Directory.GetCurrentDirectory() + "\\Log\\" + filename, Environment.NewLine + _data);
                File.AppendAllText(Directory.GetCurrentDirectory() + "\\Log\\" + filename
                    , Environment.NewLine + ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>" + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("LogManager.File_Mng(string _data) - " + ex.Message);
            }
        }
    }
}
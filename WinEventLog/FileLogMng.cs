using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace ADEng.Library.DMB
{
    /// <summary>
    /// 분석용 로그파일을 기록하기위한 클래스
    /// </summary>
    public class FileLogMng
    {       
        private string defaultDirectory = string.Empty;
        private string defaultFileName = string.Empty;

        /// <summary>
        /// 기본 생성자
        /// </summary>
        public FileLogMng()
        {
            
        }               

        /// <summary>
        /// 실행파일 root디렉토리에 실행파일명으로 로그파일을 저장한다.
        /// </summary>
        /// <param name="_message">저장할 내용</param>
        /// <returns></returns>
        public bool LogFileWrite(string _message)
        {
            try
            {
                this.defaultDirectory = AppDomain.CurrentDomain.BaseDirectory;
                this.defaultFileName = Application.ProductName;

                string path = defaultDirectory + defaultFileName + ".log";               
                
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                 
                }                
               
                File.AppendAllText(path, "[" + DateTime.Now + "]   " + _message + "\r\n" + "\r\n");             
                    
                return true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);                
                return false;
            }
        }

        /// <summary>
        /// 실행파일 디렉토리에 로그파일을 저장한다.
        /// </summary>
        /// <param name="_fileName">파일명</param>
        /// <param name="_message">저장할 내용</param>
        /// <returns></returns>
        public bool LogFileWrite(string _fileName, string _message)
        {
            try
            {
                this.defaultDirectory = AppDomain.CurrentDomain.BaseDirectory;

                string path = defaultDirectory + _fileName + ".log";

                if (!File.Exists(path))
                {
                    File.Create(path).Close();

                }
                File.AppendAllText(path, "[" + DateTime.Now + "]   " + _message + "\r\n" + "\r\n");

                return true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return false;
            }
        }
       
        /// <summary>
        /// 로그파일을 저장한다.
        /// </summary>
        /// <param name="_fileDirectory">저장할 디렉토리 경로</param>
        /// <param name="_fileName">파일명</param>
        /// <param name="_message">저장할 내용</param>
        /// <returns></returns>
        public bool LogFileWrite(string _fileDirectory, string _fileName, string _message)
        {
            try
            {
                string path = _fileDirectory + _fileName + ".log";

                if (!File.Exists(path))
                {
                    File.Create(path).Close();

                }
                File.AppendAllText(path, "[" + DateTime.Now + "]   " + _message + "\r\n" + "\r\n");

                return true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TextLog;

namespace TextLog
{
    public static class Log
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public static void WriteLog(string strMsg)
        {
            string m_strLogPrefix = @"C:\Program Files\A&D\EWSSystem\LOG\LOG";
            string m_strLogExt = @".LOG";
            DateTime dtNow = DateTime.Now;
            string strDate = dtNow.ToString("yyyyMMdd");
            string strPath = String.Format("{0}{1}{2}", m_strLogPrefix, strDate, m_strLogExt);
            string strDir = Path.GetDirectoryName(strPath);
            DirectoryInfo diDir = new DirectoryInfo(strDir);

            if (!diDir.Exists)
            {
                //디렉토리 생성
                diDir.Create();
                diDir = new DirectoryInfo(strDir);  // 아래에 있는 if (diDir.Exists)은 Directory 생성전 상태를 나타내므로 다시 DirectoryInfo object를 생성.
            }

            //로그 남기기
            if (diDir.Exists)
            {
                //스트림 생성
                System.IO.StreamWriter swStream = File.AppendText(strPath);


                string[] words = strMsg.Split('|');

                //시간
                //프로젝트명.파일명.함수명(Line)
                //에러메세지
                swStream.WriteLine("");
                string strLog = "*********************************************************************************************************************************";
                swStream.WriteLine(strLog);
                strLog = "[발생시각] : " + dtNow.ToString("hh:mm:ss");
                swStream.WriteLine(strLog);
                strLog = "[발생위치] : " + words[0];
                swStream.WriteLine(strLog);
                strLog = "[에러내용] : " + words[1];
                swStream.WriteLine(strLog);

                swStream.Close();
            }
        }

        #region 파일 사이즈 구하기
        //파일 사이즈 구하기
        //FileInfo fInfo = new FileInfo(strPath);
        //string strFileSize = GetFileSize(fInfo.Length);


        ////헤더 붙이기
        //if (strFileSize == "0 Bytes")
        //{
        //    string strHeader = "*********************************************************************************************************************************";
        //    swStream.WriteLine(strHeader);
        //    strHeader = "|시간|    프로젝트명	| 파일명		| 함수명		| Line  | 에러메세지|";
        //    swStream.WriteLine(strHeader);
        //    strHeader = "*********************************************************************************************************************************";

        //    swStream.WriteLine(strHeader);
        //}

        ///// <summary>
        ///// 파일 사이즈 구하기
        ///// </summary>
        ///// <param name="byteCount"></param>
        ///// <returns></returns>
        //private string GetFileSize(double byteCount)
        //{
        //    string size = "0 Bytes";
        //    if (byteCount >= 1073741824.0)
        //        size = String.Format("{0:##.##}", byteCount / 1073741824.0) + " GB";
        //    else if (byteCount >= 1048576.0)
        //        size = String.Format("{0:##.##}", byteCount / 1048576.0) + " MB";
        //    else if (byteCount >= 1024.0)
        //        size = String.Format("{0:##.##}", byteCount / 1024.0) + " KB";
        //    else if (byteCount > 0 && byteCount < 1024.0)
        //        size = byteCount.ToString() + " Bytes";

        //    return size;
        //}
        #endregion
    }
}

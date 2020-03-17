using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace EWS_emulater
{
    public class DataManager
    {
        private static DataManager instance = null;
        private static Mutex mutex = new Mutex();

        #region 데이터 관리 리스트 맴버
        #endregion

        /// <summary>
        /// 데이터관리 클래스 생성 메소드(정적 메소드)
        /// </summary>
        /// <returns>DataManageComponent 인스턴스 반환</returns>
        /// <remarks>
        /// 데이터관리 클래스는 싱글톤으로 실행된다.
        /// 싱글톤으로 실행되기 위해 우리는 이 정적 메소드로부터 객체를 생성해야 한다.
        /// </remarks>
        public static DataManager getInstance()
        {
            mutex.WaitOne();

            if (instance == null)
            {
                instance = new DataManager();
            }

            mutex.ReleaseMutex();

            return instance;
        }

        /// <summary>
        /// 기본 생성자
        /// </summary>
        private DataManager()
        {
        }
    }
}
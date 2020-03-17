using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace EWSSystem
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool bNew;

            Mutex mutex = new Mutex(true, Application.ProductName, out bNew);
            if (bNew)
            {
                //소유권이 부여
                //해당 프로그램이 실행되고 있지 않은 경우
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());

                // 뮤텍스 릴리즈
                mutex.ReleaseMutex();
            }
            else
            {
                //소유권이 부여되지 않음.
                MessageBox.Show("EWS 편성 시스템이 이미 실행 중입니다.");
                Application.Exit();

            }
        }
    }
}

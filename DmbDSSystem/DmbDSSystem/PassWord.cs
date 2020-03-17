using System;
using System.Collections.Generic;
using System.Text;

namespace DmbDSSystem
{
    class PassWord
    {
        /// <summary>
        /// String 인자를 받아 로컬암호와 비교해 같으면 True, 틀리면 False를 반환한다.
        /// </summary>
        /// <param name="pw"></param>
        /// <returns>
        /// 리턴값은 boolean
        /// </returns>
        public bool ConfirmPassWord(string pw)
        {
            if (Properties.Settings.Default.BroadPW == pw)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// String 인자를 받아 로컬암호로 저장한다.
        /// </summary>
        /// <param name="pw"></param>
        public void SaveConfirmPassWord(string pw)
        {
            Properties.Settings.Default.BroadPW = pw;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// 로컬에 저장되어 있는 암호를 get, set 한다.
        /// </summary>
        public string Password
        {
            get
            {
                return Properties.Settings.Default.BroadPW;
            }
            set
            {
                Properties.Settings.Default.BroadPW = value;
                Properties.Settings.Default.Save();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace EWS_emulater
{
    class Set_TextBox
    {
        /// <summary>
        /// RichTextBox의 스크롤을 항상 마지막으로 보낸다.
        /// </summary>
        /// <param name="rt"></param>
        public void SetTBScroll(RichTextBox rt)
        {
            rt.SelectionStart = rt.Text.Length;
            rt.SelectionLength = 0;
            rt.ScrollToCaret();
        }
    }
}
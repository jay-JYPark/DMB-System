using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EWSSystem
{
    public class MUXScheduler
    {

        /// <summary>
        /// 특수 수신기 스케줄러 큐에 쌓다
        /// </summary>
        /// <param name="muxMessage"></param>
        /// <returns></returns>
        public bool AddSpcQueue(MUXMessage muxMessage)
        {
            return true;
        }

        /// <summary>
        /// 특수 수신기 스케줄러 큐에 쌓다
        /// </summary>
        /// <param name="muxMessage"></param>
        /// <returns></returns>
        public bool AddNorQueue(MUXMessage muxMessage)
        {
            return true;
        }
    }
}

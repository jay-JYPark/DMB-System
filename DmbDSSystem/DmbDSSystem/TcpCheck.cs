using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DmbDSSystem
{
    class TcpCheck
    {
        public event ConnectionCheckHandle ConnectionCheckEvt;
        public delegate void ConnectionCheckHandle();

        private bool Gen = true;
        private bool Flag = false;

        public bool GEN
        {
            set { this.Gen = value; }
            get { return this.Gen; }
        }

        public bool FLAG
        {
            set { this.Flag = value; }
            get { return this.Flag; }
        }

        public TcpCheck()
        {
        }

        public void Start()
        {
            this.Flag = true;

            while (this.Gen)
            {
                Thread.Sleep(10000);

                if (this.ConnectionCheckEvt != null)
                {
                    this.ConnectionCheckEvt();
                }
            }

            this.Flag = false;
        }
    }
}
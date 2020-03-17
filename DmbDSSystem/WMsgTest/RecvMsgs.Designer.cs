namespace WMsgTest
{
    partial class RecvMsgs
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ResetLB = new System.Windows.Forms.Label();
            this.MainLB = new System.Windows.Forms.Label();
            this.MainTB = new System.Windows.Forms.RichTextBox();
            this.RecvMsgToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.MainPanel.SuspendLayout();
            this.SplitContainer1.Panel1.SuspendLayout();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.SplitContainer1);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(2, 2);
            this.MainPanel.Margin = new System.Windows.Forms.Padding(0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(874, 517);
            this.MainPanel.TabIndex = 0;
            // 
            // SplitContainer1
            // 
            this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SplitContainer1.IsSplitterFixed = true;
            this.SplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer1.Name = "SplitContainer1";
            this.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitContainer1.Panel1
            // 
            this.SplitContainer1.Panel1.BackgroundImage = global::WMsgTest.Properties.Resources.DMB_main;
            this.SplitContainer1.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SplitContainer1.Panel1.Controls.Add(this.ResetLB);
            this.SplitContainer1.Panel1.Controls.Add(this.MainLB);
            this.SplitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.MainTB);
            this.SplitContainer1.Size = new System.Drawing.Size(874, 517);
            this.SplitContainer1.SplitterDistance = 25;
            this.SplitContainer1.TabIndex = 1;
            // 
            // ResetLB
            // 
            this.ResetLB.BackColor = System.Drawing.Color.Transparent;
            this.ResetLB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ResetLB.Dock = System.Windows.Forms.DockStyle.Right;
            this.ResetLB.Image = global::WMsgTest.Properties.Resources.DMB_reset;
            this.ResetLB.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.ResetLB.Location = new System.Drawing.Point(853, 0);
            this.ResetLB.Name = "ResetLB";
            this.ResetLB.Size = new System.Drawing.Size(21, 25);
            this.ResetLB.TabIndex = 2;
            this.ResetLB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RecvMsgToolTip.SetToolTip(this.ResetLB, "초기화");
            this.ResetLB.Click += new System.EventHandler(this.ResetLB_Click);
            // 
            // MainLB
            // 
            this.MainLB.BackColor = System.Drawing.Color.Transparent;
            this.MainLB.Dock = System.Windows.Forms.DockStyle.Left;
            this.MainLB.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.MainLB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.MainLB.Location = new System.Drawing.Point(10, 0);
            this.MainLB.Name = "MainLB";
            this.MainLB.Size = new System.Drawing.Size(152, 25);
            this.MainLB.TabIndex = 1;
            this.MainLB.Text = "Receive Message";
            this.MainLB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainTB
            // 
            this.MainTB.BackColor = System.Drawing.Color.Black;
            this.MainTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTB.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.MainTB.ForeColor = System.Drawing.Color.Gold;
            this.MainTB.Location = new System.Drawing.Point(0, 0);
            this.MainTB.Name = "MainTB";
            this.MainTB.ReadOnly = true;
            this.MainTB.Size = new System.Drawing.Size(874, 488);
            this.MainTB.TabIndex = 0;
            this.MainTB.Text = "";
            // 
            // RecvMsgs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 521);
            this.Controls.Add(this.MainPanel);
            this.Location = new System.Drawing.Point(20, 134);
            this.MinimumSize = new System.Drawing.Size(200, 100);
            this.Name = "RecvMsgs";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "수신 테스트";
            this.MainPanel.ResumeLayout(false);
            this.SplitContainer1.Panel1.ResumeLayout(false);
            this.SplitContainer1.Panel2.ResumeLayout(false);
            this.SplitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.SplitContainer SplitContainer1;
        private System.Windows.Forms.Label MainLB;
        private System.Windows.Forms.Label ResetLB;
        private System.Windows.Forms.RichTextBox MainTB;
        private System.Windows.Forms.ToolTip RecvMsgToolTip;
    }
}
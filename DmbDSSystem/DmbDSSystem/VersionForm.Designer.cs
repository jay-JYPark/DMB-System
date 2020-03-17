namespace DmbDSSystem
{
    partial class VersionForm
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
            this.MainLB = new System.Windows.Forms.Label();
            this.WarningLB = new System.Windows.Forms.Label();
            this.VerTB = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // MainLB
            // 
            this.MainLB.BackColor = System.Drawing.Color.Transparent;
            this.MainLB.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.MainLB.ForeColor = System.Drawing.Color.Black;
            this.MainLB.Location = new System.Drawing.Point(26, 103);
            this.MainLB.Name = "MainLB";
            this.MainLB.Size = new System.Drawing.Size(310, 32);
            this.MainLB.TabIndex = 0;
            this.MainLB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.MainLB.Click += new System.EventHandler(this.MainLB_Click);
            // 
            // WarningLB
            // 
            this.WarningLB.BackColor = System.Drawing.Color.Transparent;
            this.WarningLB.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.WarningLB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.WarningLB.Location = new System.Drawing.Point(17, 213);
            this.WarningLB.Name = "WarningLB";
            this.WarningLB.Size = new System.Drawing.Size(332, 58);
            this.WarningLB.TabIndex = 1;
            this.WarningLB.Text = "경고 : 이 컴퓨터 프로그램은 저작권법과 국제 협약의 보호를 받습니다. 이 프로그램의 전부 또는 일부를 무단으로 복제, 배포하는 행위는 민사 및 " +
                "형사법에 의해 엄격히 규제되어 있으며, 기소 사유가 됩니다.";
            this.WarningLB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.WarningLB.Click += new System.EventHandler(this.WarningLB_Click);
            // 
            // VerTB
            // 
            this.VerTB.BackColor = System.Drawing.Color.White;
            this.VerTB.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.VerTB.Location = new System.Drawing.Point(18, 140);
            this.VerTB.Name = "VerTB";
            this.VerTB.ReadOnly = true;
            this.VerTB.Size = new System.Drawing.Size(330, 75);
            this.VerTB.TabIndex = 2;
            this.VerTB.Text = "";
            // 
            // VersionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DmbDSSystem.Properties.Resources.DMB_infoBg1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(363, 280);
            this.ControlBox = false;
            this.Controls.Add(this.VerTB);
            this.Controls.Add(this.WarningLB);
            this.Controls.Add(this.MainLB);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "VersionForm";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Click += new System.EventHandler(this.VersionForm_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label MainLB;
        private System.Windows.Forms.Label WarningLB;
        private System.Windows.Forms.RichTextBox VerTB;

    }
}
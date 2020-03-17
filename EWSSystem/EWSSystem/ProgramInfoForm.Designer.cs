namespace EWSSystem
{
    partial class ProgramInfoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.VerTB = new System.Windows.Forms.RichTextBox();
            this.WarningLB = new System.Windows.Forms.Label();
            this.MainLB = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // VerTB
            // 
            this.VerTB.BackColor = System.Drawing.Color.White;
            this.VerTB.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.VerTB.Location = new System.Drawing.Point(14, 120);
            this.VerTB.Name = "VerTB";
            this.VerTB.ReadOnly = true;
            this.VerTB.Size = new System.Drawing.Size(330, 75);
            this.VerTB.TabIndex = 4;
            this.VerTB.Text = "";
            // 
            // WarningLB
            // 
            this.WarningLB.BackColor = System.Drawing.Color.Transparent;
            this.WarningLB.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.WarningLB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.WarningLB.Location = new System.Drawing.Point(13, 193);
            this.WarningLB.Name = "WarningLB";
            this.WarningLB.Size = new System.Drawing.Size(332, 58);
            this.WarningLB.TabIndex = 3;
            this.WarningLB.Text = "경고 : 이 컴퓨터 프로그램은 저작권법과 국제 협약의 보호를 받습니다. 이 프로그램의 전부 또는 일부를 무단으로 복제, 배포하는 행위는 민사 및 " +
                "형사법에 의해 엄격히 규제되어 있으며, 기소 사유가 됩니다.";
            this.WarningLB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainLB
            // 
            this.MainLB.BackColor = System.Drawing.Color.Transparent;
            this.MainLB.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.MainLB.ForeColor = System.Drawing.Color.Black;
            this.MainLB.Location = new System.Drawing.Point(24, 85);
            this.MainLB.Name = "MainLB";
            this.MainLB.Size = new System.Drawing.Size(310, 32);
            this.MainLB.TabIndex = 0;
            this.MainLB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ProgramInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::EWSSystem.Properties.Resources.DMB_infoBg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(363, 280);
            this.Controls.Add(this.MainLB);
            this.Controls.Add(this.VerTB);
            this.Controls.Add(this.WarningLB);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ProgramInfoForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "프로그램 정보";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox VerTB;
        private System.Windows.Forms.Label WarningLB;
        private System.Windows.Forms.Label MainLB;
    }
}
namespace DmbDSSystem
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.파일ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.설정CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.도움말HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainTool = new System.Windows.Forms.ToolStrip();
            this.MainToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.MainToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.dmbSeverStatCtrl = new ADEng.DmbServerStat.DmbSeverStatCtrl();
            this.MenuStrip.SuspendLayout();
            this.MainTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.HelpToolStripMenuItem.Text = "프로그램 정보(&I)";
            this.HelpToolStripMenuItem.Click += new System.EventHandler(this.HelpToolStripMenuItem_Click);
            // 
            // MenuStrip
            // 
            this.MenuStrip.AutoSize = false;
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.파일ToolStripMenuItem,
            this.설정CToolStripMenuItem,
            this.도움말HToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.MenuStrip.Size = new System.Drawing.Size(1016, 20);
            this.MenuStrip.TabIndex = 10;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // 파일ToolStripMenuItem
            // 
            this.파일ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.ExitToolStripMenuItem});
            this.파일ToolStripMenuItem.Name = "파일ToolStripMenuItem";
            this.파일ToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.파일ToolStripMenuItem.Text = "파일(&F)";
            this.파일ToolStripMenuItem.ToolTipText = "파일";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ExitToolStripMenuItem.Text = "종료(&X)";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // 설정CToolStripMenuItem
            // 
            this.설정CToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OptionToolStripMenuItem});
            this.설정CToolStripMenuItem.Name = "설정CToolStripMenuItem";
            this.설정CToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.설정CToolStripMenuItem.Text = "설정(&C)";
            this.설정CToolStripMenuItem.ToolTipText = "설정";
            // 
            // OptionToolStripMenuItem
            // 
            this.OptionToolStripMenuItem.Image = global::DmbDSSystem.Properties.Resources.DMB_dialogue_control;
            this.OptionToolStripMenuItem.Name = "OptionToolStripMenuItem";
            this.OptionToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.OptionToolStripMenuItem.Text = "환경 설정(&O)...";
            this.OptionToolStripMenuItem.Click += new System.EventHandler(this.OptionToolStripMenuItem_Click);
            // 
            // 도움말HToolStripMenuItem
            // 
            this.도움말HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpToolStripMenuItem});
            this.도움말HToolStripMenuItem.Name = "도움말HToolStripMenuItem";
            this.도움말HToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.도움말HToolStripMenuItem.Text = "도움말(&H)";
            this.도움말HToolStripMenuItem.ToolTipText = "도움말";
            // 
            // MainTool
            // 
            this.MainTool.AutoSize = false;
            this.MainTool.BackgroundImage = global::DmbDSSystem.Properties.Resources.DMB_title;
            this.MainTool.GripMargin = new System.Windows.Forms.Padding(0);
            this.MainTool.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.MainTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainToolStripButton,
            this.MainToolStripLabel});
            this.MainTool.Location = new System.Drawing.Point(0, 20);
            this.MainTool.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.MainTool.Name = "MainTool";
            this.MainTool.Padding = new System.Windows.Forms.Padding(3);
            this.MainTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.MainTool.Size = new System.Drawing.Size(1016, 40);
            this.MainTool.Stretch = true;
            this.MainTool.TabIndex = 11;
            // 
            // MainToolStripButton
            // 
            this.MainToolStripButton.AutoSize = false;
            this.MainToolStripButton.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.MainToolStripButton.ForeColor = System.Drawing.Color.White;
            this.MainToolStripButton.Image = global::DmbDSSystem.Properties.Resources.DMB_main_caution;
            this.MainToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MainToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MainToolStripButton.Name = "MainToolStripButton";
            this.MainToolStripButton.Size = new System.Drawing.Size(100, 30);
            this.MainToolStripButton.Text = "발령 현황";
            // 
            // MainToolStripLabel
            // 
            this.MainToolStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.MainToolStripLabel.AutoSize = false;
            this.MainToolStripLabel.BackColor = System.Drawing.Color.Transparent;
            this.MainToolStripLabel.BackgroundImage = global::DmbDSSystem.Properties.Resources.DMB_title_broadcasting;
            this.MainToolStripLabel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.MainToolStripLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MainToolStripLabel.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.MainToolStripLabel.ForeColor = System.Drawing.Color.White;
            this.MainToolStripLabel.Name = "MainToolStripLabel";
            this.MainToolStripLabel.Size = new System.Drawing.Size(200, 40);
            // 
            // dmbSeverStatCtrl
            // 
            this.dmbSeverStatCtrl.AutoSize = true;
            this.dmbSeverStatCtrl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dmbSeverStatCtrl.Location = new System.Drawing.Point(0, 719);
            this.dmbSeverStatCtrl.Name = "dmbSeverStatCtrl";
            this.dmbSeverStatCtrl.Size = new System.Drawing.Size(1016, 22);
            this.dmbSeverStatCtrl.TabIndex = 14;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.dmbSeverStatCtrl);
            this.Controls.Add(this.MainTool);
            this.Controls.Add(this.MenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MinimumSize = new System.Drawing.Size(614, 460);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "방송 DMB송출 시스템";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.MainTool.ResumeLayout(false);
            this.MainTool.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip MainTool;
        private System.Windows.Forms.ToolStripButton MainToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 파일ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 설정CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OptionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 도움말HToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel MainToolStripLabel;
        private ADEng.DmbServerStat.DmbSeverStatCtrl dmbSeverStatCtrl;
    }
}


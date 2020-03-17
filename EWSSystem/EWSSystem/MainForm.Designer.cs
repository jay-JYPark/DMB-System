namespace EWSSystem
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemMain = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOption = new System.Windows.Forms.ToolStripMenuItem();
            this.환경설정ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemProgramInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnMainToolStrip = new System.Windows.Forms.ToolStripButton();
            this.btnHistoryToolStrip = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile,
            this.menuItemView,
            this.menuItemOption,
            this.menuItemHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1016, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuItemFile
            // 
            this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemExit});
            this.menuItemFile.Name = "menuItemFile";
            this.menuItemFile.Size = new System.Drawing.Size(58, 20);
            this.menuItemFile.Text = "파일(&F)";
            // 
            // menuItemExit
            // 
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.Size = new System.Drawing.Size(118, 22);
            this.menuItemExit.Text = "종료(&X)";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuItemView
            // 
            this.menuItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemMain,
            this.menuItemHistory});
            this.menuItemView.Name = "menuItemView";
            this.menuItemView.Size = new System.Drawing.Size(59, 20);
            this.menuItemView.Text = "보기(&V)";
            // 
            // menuItemMain
            // 
            this.menuItemMain.Name = "menuItemMain";
            this.menuItemMain.Size = new System.Drawing.Size(128, 22);
            this.menuItemMain.Text = "송출 현황";
            this.menuItemMain.Click += new System.EventHandler(this.MenuItemMain_Click);
            // 
            // menuItemHistory
            // 
            this.menuItemHistory.Name = "menuItemHistory";
            this.menuItemHistory.Size = new System.Drawing.Size(128, 22);
            this.menuItemHistory.Text = "송출 이력";
            this.menuItemHistory.Click += new System.EventHandler(this.menuItemHistory_Click);
            // 
            // menuItemOption
            // 
            this.menuItemOption.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.환경설정ToolStripMenuItem});
            this.menuItemOption.Name = "menuItemOption";
            this.menuItemOption.Size = new System.Drawing.Size(60, 20);
            this.menuItemOption.Text = "설정(&C)";
            // 
            // 환경설정ToolStripMenuItem
            // 
            this.환경설정ToolStripMenuItem.Name = "환경설정ToolStripMenuItem";
            this.환경설정ToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.환경설정ToolStripMenuItem.Text = "환경설정(&O)";
            this.환경설정ToolStripMenuItem.Click += new System.EventHandler(this.환경설정ToolStripMenuItem_Click);
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemProgramInfo});
            this.menuItemHelp.Name = "menuItemHelp";
            this.menuItemHelp.Size = new System.Drawing.Size(71, 20);
            this.menuItemHelp.Text = "도움말(&H)";
            // 
            // menuItemProgramInfo
            // 
            this.menuItemProgramInfo.Name = "menuItemProgramInfo";
            this.menuItemProgramInfo.Size = new System.Drawing.Size(165, 22);
            this.menuItemProgramInfo.Text = "프로그램 정보(&I)";
            this.menuItemProgramInfo.Click += new System.EventHandler(this.menuItemProgramInfo_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackgroundImage = global::EWSSystem.Properties.Resources.DMB_title_Bar;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMainToolStrip,
            this.btnHistoryToolStrip,
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1016, 40);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "MainToolStrip";
            // 
            // btnMainToolStrip
            // 
            this.btnMainToolStrip.AutoSize = false;
            this.btnMainToolStrip.AutoToolTip = false;
            this.btnMainToolStrip.ForeColor = System.Drawing.Color.White;
            this.btnMainToolStrip.Image = global::EWSSystem.Properties.Resources.DMB_main_caution;
            this.btnMainToolStrip.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnMainToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMainToolStrip.Name = "btnMainToolStrip";
            this.btnMainToolStrip.Size = new System.Drawing.Size(100, 30);
            this.btnMainToolStrip.Text = "송출 현황";
            this.btnMainToolStrip.ToolTipText = "전송 현황";
            this.btnMainToolStrip.Click += new System.EventHandler(this.btnMainPage_Click);
            // 
            // btnHistoryToolStrip
            // 
            this.btnHistoryToolStrip.AutoSize = false;
            this.btnHistoryToolStrip.AutoToolTip = false;
            this.btnHistoryToolStrip.ForeColor = System.Drawing.Color.White;
            this.btnHistoryToolStrip.Image = global::EWSSystem.Properties.Resources.DMB_main_CautionHistory;
            this.btnHistoryToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHistoryToolStrip.Name = "btnHistoryToolStrip";
            this.btnHistoryToolStrip.Size = new System.Drawing.Size(100, 30);
            this.btnHistoryToolStrip.Text = "송출 이력";
            this.btnHistoryToolStrip.ToolTipText = "전송 이력";
            this.btnHistoryToolStrip.Click += new System.EventHandler(this.btnHistoryToolStrip_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.AutoSize = false;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(191, 40);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(614, 460);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EWS 편성 시스템";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItemFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        private System.Windows.Forms.ToolStripMenuItem menuItemView;
        private System.Windows.Forms.ToolStripMenuItem menuItemMain;
        private System.Windows.Forms.ToolStripMenuItem menuItemHistory;
        private System.Windows.Forms.ToolStripMenuItem menuItemOption;
        private System.Windows.Forms.ToolStripMenuItem 환경설정ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem menuItemProgramInfo;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnMainToolStrip;
        private System.Windows.Forms.ToolStripButton btnHistoryToolStrip;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    }
}
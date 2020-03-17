namespace DmbDSSystem
{
    partial class OptionDlg
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
            this.CloseBtn = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.OkBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.TopPB = new System.Windows.Forms.PictureBox();
            this.FileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.AdminCB = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.BroadTV = new System.Windows.Forms.TreeView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtSpSfPort = new System.Windows.Forms.TextBox();
            this.txtSpSfipAddr = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtEWSPort = new System.Windows.Forms.TextBox();
            this.txtSFipAddr = new System.Windows.Forms.TextBox();
            this.txtEWSipAddr = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.TcpDisconBtn = new System.Windows.Forms.Button();
            this.TcpConBtn = new System.Windows.Forms.Button();
            this.PortLB = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SFIpLB = new System.Windows.Forms.Label();
            this.OptionTC = new System.Windows.Forms.TabControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TopPB)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.OptionTC.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseBtn
            // 
            this.CloseBtn.Location = new System.Drawing.Point(295, 324);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(75, 23);
            this.CloseBtn.TabIndex = 8;
            this.CloseBtn.Text = "취소";
            this.CloseBtn.UseVisualStyleBackColor = true;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(376, 324);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveBtn.TabIndex = 7;
            this.SaveBtn.Text = "적용";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // OkBtn
            // 
            this.OkBtn.Location = new System.Drawing.Point(214, 324);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(75, 23);
            this.OkBtn.TabIndex = 9;
            this.OkBtn.Text = "확인";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::DmbDSSystem.Properties.Resources.DMB_bar;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(462, 4);
            this.panel2.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::DmbDSSystem.Properties.Resources.DMB_dialogue_Bg;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.TopPB);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(462, 40);
            this.panel1.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(191, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "각종 설정내용을 편집/저장합니다.";
            // 
            // TopPB
            // 
            this.TopPB.BackColor = System.Drawing.Color.Transparent;
            this.TopPB.Image = global::DmbDSSystem.Properties.Resources.DMB_dialogue_control;
            this.TopPB.Location = new System.Drawing.Point(403, 0);
            this.TopPB.Name = "TopPB";
            this.TopPB.Size = new System.Drawing.Size(41, 40);
            this.TopPB.TabIndex = 0;
            this.TopPB.TabStop = false;
            // 
            // FileDialog
            // 
            this.FileDialog.DefaultExt = "wav";
            this.FileDialog.Filter = "WAV파일(*.wav)|*.wav";
            this.FileDialog.ReadOnlyChecked = true;
            this.FileDialog.Title = "알람음을 선택하세요";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.button1);
            this.tabPage4.Controls.Add(this.AdminCB);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Controls.Add(this.panel7);
            this.tabPage4.Controls.Add(this.BroadTV);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(431, 242);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "방송사";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(336, 140);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "암호변경";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AdminCB
            // 
            this.AdminCB.AutoSize = true;
            this.AdminCB.Location = new System.Drawing.Point(351, 169);
            this.AdminCB.Name = "AdminCB";
            this.AdminCB.Size = new System.Drawing.Size(60, 16);
            this.AdminCB.TabIndex = 21;
            this.AdminCB.Text = "관리자";
            this.AdminCB.UseVisualStyleBackColor = true;
            this.AdminCB.CheckedChanged += new System.EventHandler(this.AdminCB_CheckedChanged);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label7.Location = new System.Drawing.Point(6, 13);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label7.Size = new System.Drawing.Size(81, 18);
            this.label7.TabIndex = 20;
            this.label7.Text = "방송사 선택";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel7
            // 
            this.panel7.BackgroundImage = global::DmbDSSystem.Properties.Resources.DMB_subsub_Bar;
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.Location = new System.Drawing.Point(6, 13);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(419, 18);
            this.panel7.TabIndex = 19;
            // 
            // BroadTV
            // 
            this.BroadTV.CheckBoxes = true;
            this.BroadTV.HideSelection = false;
            this.BroadTV.ItemHeight = 18;
            this.BroadTV.Location = new System.Drawing.Point(19, 37);
            this.BroadTV.Name = "BroadTV";
            this.BroadTV.Size = new System.Drawing.Size(392, 97);
            this.BroadTV.TabIndex = 0;
            this.BroadTV.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.BroadTV_AfterCheck);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtSpSfPort);
            this.tabPage1.Controls.Add(this.txtSpSfipAddr);
            this.tabPage1.Controls.Add(this.txtPort);
            this.tabPage1.Controls.Add(this.txtEWSPort);
            this.tabPage1.Controls.Add(this.txtSFipAddr);
            this.tabPage1.Controls.Add(this.txtEWSipAddr);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.label18);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.panel4);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.TcpDisconBtn);
            this.tabPage1.Controls.Add(this.TcpConBtn);
            this.tabPage1.Controls.Add(this.PortLB);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.SFIpLB);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(431, 242);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "통신설정";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtSpSfPort
            // 
            this.txtSpSfPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSpSfPort.BackColor = System.Drawing.SystemColors.Window;
            this.txtSpSfPort.Location = new System.Drawing.Point(288, 72);
            this.txtSpSfPort.Margin = new System.Windows.Forms.Padding(2);
            this.txtSpSfPort.MaxLength = 50;
            this.txtSpSfPort.Name = "txtSpSfPort";
            this.txtSpSfPort.Size = new System.Drawing.Size(106, 21);
            this.txtSpSfPort.TabIndex = 26;
            // 
            // txtSpSfipAddr
            // 
            this.txtSpSfipAddr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSpSfipAddr.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtSpSfipAddr.BackColor = System.Drawing.SystemColors.Window;
            this.txtSpSfipAddr.Location = new System.Drawing.Point(288, 48);
            this.txtSpSfipAddr.Margin = new System.Windows.Forms.Padding(2);
            this.txtSpSfipAddr.MaxLength = 255;
            this.txtSpSfipAddr.Name = "txtSpSfipAddr";
            this.txtSpSfipAddr.Size = new System.Drawing.Size(106, 21);
            this.txtSpSfipAddr.TabIndex = 25;
            // 
            // txtPort
            // 
            this.txtPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPort.BackColor = System.Drawing.SystemColors.Window;
            this.txtPort.Location = new System.Drawing.Point(81, 71);
            this.txtPort.Margin = new System.Windows.Forms.Padding(2);
            this.txtPort.MaxLength = 50;
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(106, 21);
            this.txtPort.TabIndex = 1;
            this.txtPort.TextChanged += new System.EventHandler(this.txtPort_TextChanged);
            // 
            // txtEWSPort
            // 
            this.txtEWSPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEWSPort.BackColor = System.Drawing.SystemColors.Window;
            this.txtEWSPort.Location = new System.Drawing.Point(103, 185);
            this.txtEWSPort.Margin = new System.Windows.Forms.Padding(2);
            this.txtEWSPort.MaxLength = 50;
            this.txtEWSPort.Name = "txtEWSPort";
            this.txtEWSPort.Size = new System.Drawing.Size(176, 21);
            this.txtEWSPort.TabIndex = 12;
            this.txtEWSPort.TextChanged += new System.EventHandler(this.txtEWSPort_TextChanged);
            // 
            // txtSFipAddr
            // 
            this.txtSFipAddr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSFipAddr.BackColor = System.Drawing.SystemColors.Window;
            this.txtSFipAddr.Location = new System.Drawing.Point(81, 47);
            this.txtSFipAddr.Margin = new System.Windows.Forms.Padding(2);
            this.txtSFipAddr.MaxLength = 255;
            this.txtSFipAddr.Name = "txtSFipAddr";
            this.txtSFipAddr.Size = new System.Drawing.Size(106, 21);
            this.txtSFipAddr.TabIndex = 0;
            this.txtSFipAddr.TextChanged += new System.EventHandler(this.txtSFipAddr_TextChanged);
            // 
            // txtEWSipAddr
            // 
            this.txtEWSipAddr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEWSipAddr.BackColor = System.Drawing.SystemColors.Window;
            this.txtEWSipAddr.Location = new System.Drawing.Point(103, 161);
            this.txtEWSipAddr.Margin = new System.Windows.Forms.Padding(2);
            this.txtEWSipAddr.MaxLength = 255;
            this.txtEWSipAddr.Name = "txtEWSipAddr";
            this.txtEWSipAddr.Size = new System.Drawing.Size(176, 21);
            this.txtEWSipAddr.TabIndex = 11;
            this.txtEWSipAddr.TextChanged += new System.EventHandler(this.txtEWSipAddr_TextChanged);
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(225, 72);
            this.label17.Margin = new System.Windows.Forms.Padding(2);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(57, 21);
            this.label17.TabIndex = 28;
            this.label17.Text = "PORT : ";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(223, 48);
            this.label18.Margin = new System.Windows.Forms.Padding(2);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(58, 21);
            this.label18.TabIndex = 27;
            this.label18.Text = "시도 IP : ";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label5.Location = new System.Drawing.Point(6, 13);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label5.Size = new System.Drawing.Size(69, 18);
            this.label5.TabIndex = 24;
            this.label5.Text = "서비스";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = global::DmbDSSystem.Properties.Resources.DMB_subsub_Bar;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Location = new System.Drawing.Point(6, 13);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(419, 18);
            this.panel4.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label4.Location = new System.Drawing.Point(6, 127);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label4.Size = new System.Drawing.Size(45, 18);
            this.label4.TabIndex = 22;
            this.label4.Text = "EWS";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::DmbDSSystem.Properties.Resources.DMB_subsub_Bar;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Location = new System.Drawing.Point(6, 127);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(419, 18);
            this.panel3.TabIndex = 21;
            // 
            // TcpDisconBtn
            // 
            this.TcpDisconBtn.Location = new System.Drawing.Point(319, 186);
            this.TcpDisconBtn.Margin = new System.Windows.Forms.Padding(2);
            this.TcpDisconBtn.Name = "TcpDisconBtn";
            this.TcpDisconBtn.Size = new System.Drawing.Size(75, 23);
            this.TcpDisconBtn.TabIndex = 16;
            this.TcpDisconBtn.Text = "통신종료";
            this.TcpDisconBtn.UseVisualStyleBackColor = true;
            this.TcpDisconBtn.Click += new System.EventHandler(this.TcpDisconBtn_Click);
            // 
            // TcpConBtn
            // 
            this.TcpConBtn.Location = new System.Drawing.Point(319, 157);
            this.TcpConBtn.Margin = new System.Windows.Forms.Padding(2);
            this.TcpConBtn.Name = "TcpConBtn";
            this.TcpConBtn.Size = new System.Drawing.Size(75, 23);
            this.TcpConBtn.TabIndex = 15;
            this.TcpConBtn.Text = "통신연결";
            this.TcpConBtn.UseVisualStyleBackColor = true;
            this.TcpConBtn.Click += new System.EventHandler(this.TcpConBtn_Click);
            // 
            // PortLB
            // 
            this.PortLB.Location = new System.Drawing.Point(18, 71);
            this.PortLB.Margin = new System.Windows.Forms.Padding(2);
            this.PortLB.Name = "PortLB";
            this.PortLB.Size = new System.Drawing.Size(57, 21);
            this.PortLB.TabIndex = 10;
            this.PortLB.Text = "PORT : ";
            this.PortLB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(40, 161);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 21);
            this.label2.TabIndex = 13;
            this.label2.Text = "IP : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(40, 185);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 21);
            this.label1.TabIndex = 14;
            this.label1.Text = "PORT : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SFIpLB
            // 
            this.SFIpLB.Location = new System.Drawing.Point(17, 47);
            this.SFIpLB.Margin = new System.Windows.Forms.Padding(2);
            this.SFIpLB.Name = "SFIpLB";
            this.SFIpLB.Size = new System.Drawing.Size(58, 21);
            this.SFIpLB.TabIndex = 8;
            this.SFIpLB.Text = "중앙 IP : ";
            this.SFIpLB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // OptionTC
            // 
            this.OptionTC.Controls.Add(this.tabPage1);
            this.OptionTC.Controls.Add(this.tabPage4);
            this.OptionTC.Location = new System.Drawing.Point(12, 50);
            this.OptionTC.Name = "OptionTC";
            this.OptionTC.SelectedIndex = 0;
            this.OptionTC.Size = new System.Drawing.Size(439, 268);
            this.OptionTC.TabIndex = 6;
            // 
            // OptionDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 354);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.OptionTC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "환경 설정";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TopPB)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.OptionTC.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CloseBtn;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox TopPB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.OpenFileDialog FileDialog;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox AdminCB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TreeView BroadTV;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtSpSfPort;
        private System.Windows.Forms.TextBox txtSpSfipAddr;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtEWSPort;
        private System.Windows.Forms.TextBox txtSFipAddr;
        private System.Windows.Forms.TextBox txtEWSipAddr;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button TcpDisconBtn;
        private System.Windows.Forms.Button TcpConBtn;
        private System.Windows.Forms.Label PortLB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label SFIpLB;
        private System.Windows.Forms.TabControl OptionTC;
    }
}
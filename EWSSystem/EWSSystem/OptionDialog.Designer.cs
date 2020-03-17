namespace EWSSystem
{
    partial class OptionDialog
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lvMUXList = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bntDelete = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClose_Tab2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMuxId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxIsUse = new System.Windows.Forms.CheckBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnApply_Tab1 = new System.Windows.Forms.Button();
            this.btnConfirm_Tab1 = new System.Windows.Forms.Button();
            this.btnCancel_Tab1 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtMUXRetry = new System.Windows.Forms.TextBox();
            this.txtMUXTcId = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtSpcWaitTime = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtSpcCycle = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtSpcRepeatNum = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtSpcMaxId = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtNorRepeatNum = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtNorWaitTime = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtNorCycle = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNorTransTime = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtNorProcNum = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNorMaxId = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.panel2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::EWSSystem.Properties.Resources.DMB_dialogue_Bg;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(491, 40);
            this.panel2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(3, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(191, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "각종 설정내용을 편집/저장합니다.";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panel3);
            this.tabPage3.Controls.Add(this.panel1);
            this.tabPage3.Location = new System.Drawing.Point(4, 21);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(483, 334);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "MUX 정보";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lvMUXList);
            this.panel3.Controls.Add(this.bntDelete);
            this.panel3.Controls.Add(this.btnModify);
            this.panel3.Controls.Add(this.btnAdd);
            this.panel3.Controls.Add(this.btnClose_Tab2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 117);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(483, 217);
            this.panel3.TabIndex = 2;
            // 
            // lvMUXList
            // 
            this.lvMUXList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvMUXList.FullRowSelect = true;
            this.lvMUXList.GridLines = true;
            this.lvMUXList.Location = new System.Drawing.Point(10, 3);
            this.lvMUXList.Name = "lvMUXList";
            this.lvMUXList.Size = new System.Drawing.Size(462, 177);
            this.lvMUXList.TabIndex = 16;
            this.lvMUXList.UseCompatibleStateImageBehavior = false;
            this.lvMUXList.View = System.Windows.Forms.View.Details;
            this.lvMUXList.SelectedIndexChanged += new System.EventHandler(this.lvMUXList_SelectedIndexChanged);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "MUXId";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "번호";
            this.columnHeader1.Width = 40;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "사용";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "IP";
            this.columnHeader3.Width = 110;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "설명";
            this.columnHeader4.Width = 180;
            // 
            // bntDelete
            // 
            this.bntDelete.Location = new System.Drawing.Point(324, 186);
            this.bntDelete.Name = "bntDelete";
            this.bntDelete.Size = new System.Drawing.Size(75, 23);
            this.bntDelete.TabIndex = 15;
            this.bntDelete.Text = "삭제";
            this.bntDelete.UseVisualStyleBackColor = true;
            this.bntDelete.Click += new System.EventHandler(this.bntDelete_Click);
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(243, 186);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(75, 23);
            this.btnModify.TabIndex = 14;
            this.btnModify.Text = "수정";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(162, 186);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.Text = "추가";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnClose_Tab2
            // 
            this.btnClose_Tab2.Location = new System.Drawing.Point(405, 186);
            this.btnClose_Tab2.Name = "btnClose_Tab2";
            this.btnClose_Tab2.Size = new System.Drawing.Size(75, 23);
            this.btnClose_Tab2.TabIndex = 10;
            this.btnClose_Tab2.Text = "닫기";
            this.btnClose_Tab2.UseVisualStyleBackColor = true;
            this.btnClose_Tab2.Click += new System.EventHandler(this.btApply_Tab2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(483, 117);
            this.panel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtIP);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtMuxId);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbxIsUse);
            this.groupBox1.Location = new System.Drawing.Point(10, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(462, 104);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MUX 상세 정보";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(157, 74);
            this.txtDescription.MaxLength = 30;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(250, 21);
            this.txtDescription.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(118, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "설명:";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(157, 47);
            this.txtIP.MaxLength = 15;
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(250, 21);
            this.txtIP.TabIndex = 4;
            this.txtIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIP_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(131, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "IP:";
            // 
            // txtMuxId
            // 
            this.txtMuxId.Location = new System.Drawing.Point(157, 20);
            this.txtMuxId.Name = "txtMuxId";
            this.txtMuxId.Size = new System.Drawing.Size(250, 21);
            this.txtMuxId.TabIndex = 2;
            this.txtMuxId.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(105, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "MUXId:";
            this.label5.Visible = false;
            // 
            // cbxIsUse
            // 
            this.cbxIsUse.AutoSize = true;
            this.cbxIsUse.Checked = true;
            this.cbxIsUse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxIsUse.Location = new System.Drawing.Point(17, 49);
            this.cbxIsUse.Name = "cbxIsUse";
            this.cbxIsUse.Size = new System.Drawing.Size(72, 16);
            this.cbxIsUse.TabIndex = 0;
            this.cbxIsUse.Text = "사용여부";
            this.cbxIsUse.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnApply_Tab1);
            this.tabPage1.Controls.Add(this.btnConfirm_Tab1);
            this.tabPage1.Controls.Add(this.btnCancel_Tab1);
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(483, 334);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "송출시스템 / MUX 송출";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnApply_Tab1
            // 
            this.btnApply_Tab1.Location = new System.Drawing.Point(395, 303);
            this.btnApply_Tab1.Name = "btnApply_Tab1";
            this.btnApply_Tab1.Size = new System.Drawing.Size(75, 23);
            this.btnApply_Tab1.TabIndex = 16;
            this.btnApply_Tab1.Text = "적용";
            this.btnApply_Tab1.UseVisualStyleBackColor = true;
            this.btnApply_Tab1.Click += new System.EventHandler(this.btnApply_Tab1_Click);
            // 
            // btnConfirm_Tab1
            // 
            this.btnConfirm_Tab1.Location = new System.Drawing.Point(235, 303);
            this.btnConfirm_Tab1.Name = "btnConfirm_Tab1";
            this.btnConfirm_Tab1.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm_Tab1.TabIndex = 15;
            this.btnConfirm_Tab1.Text = "확인";
            this.btnConfirm_Tab1.UseVisualStyleBackColor = true;
            this.btnConfirm_Tab1.Click += new System.EventHandler(this.btnConfirm_Tab1_Click);
            // 
            // btnCancel_Tab1
            // 
            this.btnCancel_Tab1.Location = new System.Drawing.Point(316, 303);
            this.btnCancel_Tab1.Name = "btnCancel_Tab1";
            this.btnCancel_Tab1.Size = new System.Drawing.Size(75, 23);
            this.btnCancel_Tab1.TabIndex = 14;
            this.btnCancel_Tab1.Text = "취소";
            this.btnCancel_Tab1.UseVisualStyleBackColor = true;
            this.btnCancel_Tab1.Click += new System.EventHandler(this.btnCancel_Tab1_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtMUXRetry);
            this.groupBox5.Controls.Add(this.txtMUXTcId);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Location = new System.Drawing.Point(244, 9);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(226, 82);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "MUX";
            // 
            // txtMUXRetry
            // 
            this.txtMUXRetry.Enabled = false;
            this.txtMUXRetry.Location = new System.Drawing.Point(137, 47);
            this.txtMUXRetry.Name = "txtMUXRetry";
            this.txtMUXRetry.Size = new System.Drawing.Size(76, 21);
            this.txtMUXRetry.TabIndex = 9;
            // 
            // txtMUXTcId
            // 
            this.txtMUXTcId.Location = new System.Drawing.Point(137, 20);
            this.txtMUXTcId.Name = "txtMUXTcId";
            this.txtMUXTcId.Size = new System.Drawing.Size(76, 21);
            this.txtMUXTcId.TabIndex = 7;
            this.txtMUXTcId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMUXTcId_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Enabled = false;
            this.label4.Location = new System.Drawing.Point(7, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "MUX Retry 횟수 :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "MUX TcID :";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtPort);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(8, 9);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(230, 82);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "송출시스템";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(140, 20);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(76, 21);
            this.txtPort.TabIndex = 3;
            this.txtPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPort_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Listen Port :";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtSpcWaitTime);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.txtSpcCycle);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.txtSpcRepeatNum);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.txtSpcMaxId);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Location = new System.Drawing.Point(244, 106);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(226, 187);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "특수 메세지";
            // 
            // txtSpcWaitTime
            // 
            this.txtSpcWaitTime.Location = new System.Drawing.Point(137, 75);
            this.txtSpcWaitTime.Name = "txtSpcWaitTime";
            this.txtSpcWaitTime.Size = new System.Drawing.Size(76, 21);
            this.txtSpcWaitTime.TabIndex = 22;
            this.txtSpcWaitTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSpcWaitTime_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(5, 77);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(126, 12);
            this.label14.TabIndex = 21;
            this.label14.Text = "동일 ID 송출시간(초) :";
            // 
            // txtSpcCycle
            // 
            this.txtSpcCycle.Enabled = false;
            this.txtSpcCycle.Location = new System.Drawing.Point(137, 102);
            this.txtSpcCycle.Name = "txtSpcCycle";
            this.txtSpcCycle.Size = new System.Drawing.Size(76, 21);
            this.txtSpcCycle.TabIndex = 20;
            this.txtSpcCycle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSpcCycle_KeyPress);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Enabled = false;
            this.label15.Location = new System.Drawing.Point(37, 104);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(94, 12);
            this.label15.TabIndex = 19;
            this.label15.Text = "처리주기 (mS) :";
            // 
            // txtSpcRepeatNum
            // 
            this.txtSpcRepeatNum.Location = new System.Drawing.Point(137, 48);
            this.txtSpcRepeatNum.Name = "txtSpcRepeatNum";
            this.txtSpcRepeatNum.Size = new System.Drawing.Size(76, 21);
            this.txtSpcRepeatNum.TabIndex = 18;
            this.txtSpcRepeatNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSpcRepeatNum_KeyPress);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(70, 50);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(61, 12);
            this.label16.TabIndex = 17;
            this.label16.Text = "반복횟수 :";
            // 
            // txtSpcMaxId
            // 
            this.txtSpcMaxId.Location = new System.Drawing.Point(137, 21);
            this.txtSpcMaxId.Name = "txtSpcMaxId";
            this.txtSpcMaxId.Size = new System.Drawing.Size(76, 21);
            this.txtSpcMaxId.TabIndex = 16;
            this.txtSpcMaxId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSpcMaxId_KeyPress);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(67, 23);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(64, 12);
            this.label17.TabIndex = 15;
            this.label17.Text = "최대 ID 수:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtNorRepeatNum);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.txtNorWaitTime);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtNorCycle);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtNorTransTime);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtNorProcNum);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtNorMaxId);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(8, 106);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(230, 187);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "일반 메세지";
            // 
            // txtNorRepeatNum
            // 
            this.txtNorRepeatNum.Location = new System.Drawing.Point(140, 74);
            this.txtNorRepeatNum.Name = "txtNorRepeatNum";
            this.txtNorRepeatNum.Size = new System.Drawing.Size(76, 21);
            this.txtNorRepeatNum.TabIndex = 18;
            this.txtNorRepeatNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNorNotAcceptProcTime_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(73, 80);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(61, 12);
            this.label13.TabIndex = 17;
            this.label13.Text = "반복횟수 :";
            // 
            // txtNorWaitTime
            // 
            this.txtNorWaitTime.Enabled = false;
            this.txtNorWaitTime.Location = new System.Drawing.Point(140, 157);
            this.txtNorWaitTime.Name = "txtNorWaitTime";
            this.txtNorWaitTime.Size = new System.Drawing.Size(76, 21);
            this.txtNorWaitTime.TabIndex = 16;
            this.txtNorWaitTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNorWaitTime_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Enabled = false;
            this.label12.Location = new System.Drawing.Point(8, 157);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(126, 12);
            this.label12.TabIndex = 15;
            this.label12.Text = "동일ID 대기시간 (분) :";
            // 
            // txtNorCycle
            // 
            this.txtNorCycle.Location = new System.Drawing.Point(140, 101);
            this.txtNorCycle.Name = "txtNorCycle";
            this.txtNorCycle.Size = new System.Drawing.Size(76, 21);
            this.txtNorCycle.TabIndex = 14;
            this.txtNorCycle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNorCycle_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(44, 107);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 12);
            this.label11.TabIndex = 13;
            this.label11.Text = "처리주기(mS) :";
            // 
            // txtNorTransTime
            // 
            this.txtNorTransTime.Enabled = false;
            this.txtNorTransTime.Location = new System.Drawing.Point(140, 128);
            this.txtNorTransTime.Name = "txtNorTransTime";
            this.txtNorTransTime.Size = new System.Drawing.Size(76, 21);
            this.txtNorTransTime.TabIndex = 12;
            this.txtNorTransTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNorTransTime_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Enabled = false;
            this.label10.Location = new System.Drawing.Point(51, 130);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 12);
            this.label10.TabIndex = 11;
            this.label10.Text = "송출시간(초) :";
            // 
            // txtNorProcNum
            // 
            this.txtNorProcNum.Location = new System.Drawing.Point(140, 47);
            this.txtNorProcNum.Name = "txtNorProcNum";
            this.txtNorProcNum.Size = new System.Drawing.Size(76, 21);
            this.txtNorProcNum.TabIndex = 10;
            this.txtNorProcNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNorProcNum_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(73, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 12);
            this.label9.TabIndex = 9;
            this.label9.Text = "처리갯수 :";
            // 
            // txtNorMaxId
            // 
            this.txtNorMaxId.Location = new System.Drawing.Point(140, 20);
            this.txtNorMaxId.Name = "txtNorMaxId";
            this.txtNorMaxId.Size = new System.Drawing.Size(76, 21);
            this.txtNorMaxId.TabIndex = 8;
            this.txtNorMaxId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNorMaxId_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(70, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "최대 ID 수:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 40);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(491, 359);
            this.tabControl1.TabIndex = 2;
            // 
            // OptionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 399);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "OptionDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "환경설정";
            this.Load += new System.EventHandler(this.OptionForm_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListView lvMUXList;
        private System.Windows.Forms.Button bntDelete;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtMUXRetry;
        private System.Windows.Forms.TextBox txtMUXTcId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMuxId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbxIsUse;
        private System.Windows.Forms.TextBox txtNorRepeatNum;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtNorWaitTime;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtNorCycle;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNorTransTime;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtNorProcNum;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNorMaxId;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSpcWaitTime;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtSpcCycle;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtSpcRepeatNum;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtSpcMaxId;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnClose_Tab2;
        private System.Windows.Forms.Button btnCancel_Tab1;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button btnConfirm_Tab1;
        private System.Windows.Forms.Button btnApply_Tab1;

    }
}
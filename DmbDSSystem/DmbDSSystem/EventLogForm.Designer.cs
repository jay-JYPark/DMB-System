namespace DmbDSSystem
{
    partial class EventLogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventLogForm));
            this.DmbEventLogListView = new System.Windows.Forms.ListView();
            this.QueryLB1 = new System.Windows.Forms.Label();
            this.QueryLB2 = new System.Windows.Forms.Label();
            this.SplitContainer3 = new System.Windows.Forms.SplitContainer();
            this.ResetBtn = new System.Windows.Forms.Button();
            this.MakeCsvBtn = new System.Windows.Forms.Button();
            this.QueryBtn = new System.Windows.Forms.Button();
            this.StartDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.SplitContainer2 = new System.Windows.Forms.SplitContainer();
            this.EndDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.DmbEventLogListLB = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.DmbEventLogListPN = new System.Windows.Forms.Panel();
            this.SplitContainer3.Panel1.SuspendLayout();
            this.SplitContainer3.Panel2.SuspendLayout();
            this.SplitContainer3.SuspendLayout();
            this.SplitContainer1.Panel1.SuspendLayout();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            this.SplitContainer2.Panel1.SuspendLayout();
            this.SplitContainer2.Panel2.SuspendLayout();
            this.SplitContainer2.SuspendLayout();
            this.DmbEventLogListPN.SuspendLayout();
            this.SuspendLayout();
            // 
            // DmbEventLogListView
            // 
            this.DmbEventLogListView.AllowColumnReorder = true;
            this.DmbEventLogListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DmbEventLogListView.FullRowSelect = true;
            this.DmbEventLogListView.GridLines = true;
            this.DmbEventLogListView.Location = new System.Drawing.Point(0, 0);
            this.DmbEventLogListView.MultiSelect = false;
            this.DmbEventLogListView.Name = "DmbEventLogListView";
            this.DmbEventLogListView.ShowItemToolTips = true;
            this.DmbEventLogListView.Size = new System.Drawing.Size(871, 642);
            this.DmbEventLogListView.SmallImageList = this.imageList1;
            this.DmbEventLogListView.TabIndex = 0;
            this.DmbEventLogListView.UseCompatibleStateImageBehavior = false;
            this.DmbEventLogListView.View = System.Windows.Forms.View.Details;
            // 
            // QueryLB1
            // 
            this.QueryLB1.AutoSize = true;
            this.QueryLB1.Location = new System.Drawing.Point(27, 18);
            this.QueryLB1.Margin = new System.Windows.Forms.Padding(2);
            this.QueryLB1.Name = "QueryLB1";
            this.QueryLB1.Size = new System.Drawing.Size(53, 12);
            this.QueryLB1.TabIndex = 3;
            this.QueryLB1.Text = "조회기간";
            // 
            // QueryLB2
            // 
            this.QueryLB2.AutoSize = true;
            this.QueryLB2.Location = new System.Drawing.Point(241, 18);
            this.QueryLB2.Margin = new System.Windows.Forms.Padding(2);
            this.QueryLB2.Name = "QueryLB2";
            this.QueryLB2.Size = new System.Drawing.Size(14, 12);
            this.QueryLB2.TabIndex = 2;
            this.QueryLB2.Text = "~";
            // 
            // SplitContainer3
            // 
            this.SplitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.SplitContainer3.IsSplitterFixed = true;
            this.SplitContainer3.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer3.Name = "SplitContainer3";
            // 
            // SplitContainer3.Panel1
            // 
            this.SplitContainer3.Panel1.Controls.Add(this.DmbEventLogListView);
            // 
            // SplitContainer3.Panel2
            // 
            this.SplitContainer3.Panel2.Controls.Add(this.ResetBtn);
            this.SplitContainer3.Panel2.Controls.Add(this.MakeCsvBtn);
            this.SplitContainer3.Size = new System.Drawing.Size(979, 642);
            this.SplitContainer3.SplitterDistance = 871;
            this.SplitContainer3.TabIndex = 0;
            // 
            // ResetBtn
            // 
            this.ResetBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ResetBtn.Location = new System.Drawing.Point(15, 46);
            this.ResetBtn.Margin = new System.Windows.Forms.Padding(2);
            this.ResetBtn.Name = "ResetBtn";
            this.ResetBtn.Size = new System.Drawing.Size(75, 23);
            this.ResetBtn.TabIndex = 1;
            this.ResetBtn.Text = "초기화";
            this.ResetBtn.UseVisualStyleBackColor = true;
            this.ResetBtn.Click += new System.EventHandler(this.ResetBtn_Click);
            // 
            // MakeCsvBtn
            // 
            this.MakeCsvBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.MakeCsvBtn.Location = new System.Drawing.Point(15, 17);
            this.MakeCsvBtn.Margin = new System.Windows.Forms.Padding(2);
            this.MakeCsvBtn.Name = "MakeCsvBtn";
            this.MakeCsvBtn.Size = new System.Drawing.Size(75, 23);
            this.MakeCsvBtn.TabIndex = 0;
            this.MakeCsvBtn.Text = "엑셀저장";
            this.MakeCsvBtn.UseVisualStyleBackColor = true;
            this.MakeCsvBtn.Click += new System.EventHandler(this.MakeCsvBtn_Click);
            // 
            // QueryBtn
            // 
            this.QueryBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.QueryBtn.Location = new System.Drawing.Point(15, 15);
            this.QueryBtn.Name = "QueryBtn";
            this.QueryBtn.Size = new System.Drawing.Size(75, 23);
            this.QueryBtn.TabIndex = 0;
            this.QueryBtn.Text = "조 회";
            this.QueryBtn.UseVisualStyleBackColor = true;
            this.QueryBtn.Click += new System.EventHandler(this.QueryBtn_Click);
            // 
            // StartDateTimePicker
            // 
            this.StartDateTimePicker.CustomFormat = "yyyy-MM-dd HH:mm";
            this.StartDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.StartDateTimePicker.Location = new System.Drawing.Point(97, 14);
            this.StartDateTimePicker.Margin = new System.Windows.Forms.Padding(2);
            this.StartDateTimePicker.Name = "StartDateTimePicker";
            this.StartDateTimePicker.Size = new System.Drawing.Size(125, 21);
            this.StartDateTimePicker.TabIndex = 0;
            this.StartDateTimePicker.CloseUp += new System.EventHandler(this.StartDateTimePicker_CloseUp);
            // 
            // SplitContainer1
            // 
            this.SplitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SplitContainer1.IsSplitterFixed = true;
            this.SplitContainer1.Location = new System.Drawing.Point(3, 24);
            this.SplitContainer1.Name = "SplitContainer1";
            this.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitContainer1.Panel1
            // 
            this.SplitContainer1.Panel1.Controls.Add(this.SplitContainer2);
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.SplitContainer3);
            this.SplitContainer1.Size = new System.Drawing.Size(979, 695);
            this.SplitContainer1.SplitterDistance = 49;
            this.SplitContainer1.TabIndex = 217;
            // 
            // SplitContainer2
            // 
            this.SplitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.SplitContainer2.IsSplitterFixed = true;
            this.SplitContainer2.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer2.Name = "SplitContainer2";
            // 
            // SplitContainer2.Panel1
            // 
            this.SplitContainer2.Panel1.Controls.Add(this.QueryLB1);
            this.SplitContainer2.Panel1.Controls.Add(this.QueryLB2);
            this.SplitContainer2.Panel1.Controls.Add(this.EndDateTimePicker);
            this.SplitContainer2.Panel1.Controls.Add(this.StartDateTimePicker);
            // 
            // SplitContainer2.Panel2
            // 
            this.SplitContainer2.Panel2.Controls.Add(this.QueryBtn);
            this.SplitContainer2.Size = new System.Drawing.Size(979, 49);
            this.SplitContainer2.SplitterDistance = 871;
            this.SplitContainer2.TabIndex = 0;
            // 
            // EndDateTimePicker
            // 
            this.EndDateTimePicker.CustomFormat = "yyyy-MM-dd HH:mm";
            this.EndDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EndDateTimePicker.Location = new System.Drawing.Point(277, 14);
            this.EndDateTimePicker.Margin = new System.Windows.Forms.Padding(2);
            this.EndDateTimePicker.Name = "EndDateTimePicker";
            this.EndDateTimePicker.Size = new System.Drawing.Size(125, 21);
            this.EndDateTimePicker.TabIndex = 1;
            this.EndDateTimePicker.CloseUp += new System.EventHandler(this.EndDateTimePicker_CloseUp);
            // 
            // DmbEventLogListLB
            // 
            this.DmbEventLogListLB.BackColor = System.Drawing.Color.Transparent;
            this.DmbEventLogListLB.Dock = System.Windows.Forms.DockStyle.Left;
            this.DmbEventLogListLB.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DmbEventLogListLB.ForeColor = System.Drawing.Color.Black;
            this.DmbEventLogListLB.Location = new System.Drawing.Point(10, 0);
            this.DmbEventLogListLB.Name = "DmbEventLogListLB";
            this.DmbEventLogListLB.Size = new System.Drawing.Size(106, 18);
            this.DmbEventLogListLB.TabIndex = 215;
            this.DmbEventLogListLB.Text = "이벤트 로그 조회";
            this.DmbEventLogListLB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "about.png");
            this.imageList1.Images.SetKeyName(1, "error.png");
            // 
            // DmbEventLogListPN
            // 
            this.DmbEventLogListPN.BackColor = System.Drawing.Color.Transparent;
            this.DmbEventLogListPN.BackgroundImage = global::DmbDSSystem.Properties.Resources.DMB_main;
            this.DmbEventLogListPN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DmbEventLogListPN.Controls.Add(this.DmbEventLogListLB);
            this.DmbEventLogListPN.Dock = System.Windows.Forms.DockStyle.Top;
            this.DmbEventLogListPN.Location = new System.Drawing.Point(2, 2);
            this.DmbEventLogListPN.Margin = new System.Windows.Forms.Padding(0);
            this.DmbEventLogListPN.Name = "DmbEventLogListPN";
            this.DmbEventLogListPN.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.DmbEventLogListPN.Size = new System.Drawing.Size(981, 18);
            this.DmbEventLogListPN.TabIndex = 1;
            // 
            // EventLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 722);
            this.Controls.Add(this.DmbEventLogListPN);
            this.Controls.Add(this.SplitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EventLogForm";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "이벤트 조회";
            this.SplitContainer3.Panel1.ResumeLayout(false);
            this.SplitContainer3.Panel2.ResumeLayout(false);
            this.SplitContainer3.ResumeLayout(false);
            this.SplitContainer1.Panel1.ResumeLayout(false);
            this.SplitContainer1.Panel2.ResumeLayout(false);
            this.SplitContainer1.ResumeLayout(false);
            this.SplitContainer2.Panel1.ResumeLayout(false);
            this.SplitContainer2.Panel1.PerformLayout();
            this.SplitContainer2.Panel2.ResumeLayout(false);
            this.SplitContainer2.ResumeLayout(false);
            this.DmbEventLogListPN.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView DmbEventLogListView;
        private System.Windows.Forms.Label QueryLB1;
        private System.Windows.Forms.Label QueryLB2;
        private System.Windows.Forms.SplitContainer SplitContainer3;
        private System.Windows.Forms.Button ResetBtn;
        private System.Windows.Forms.Button MakeCsvBtn;
        private System.Windows.Forms.Button QueryBtn;
        private System.Windows.Forms.DateTimePicker StartDateTimePicker;
        private System.Windows.Forms.SplitContainer SplitContainer1;
        private System.Windows.Forms.SplitContainer SplitContainer2;
        private System.Windows.Forms.DateTimePicker EndDateTimePicker;
        private System.Windows.Forms.Label DmbEventLogListLB;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel DmbEventLogListPN;


    }
}
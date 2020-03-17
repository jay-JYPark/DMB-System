namespace DmbDSSystem
{
    partial class MainViewForm
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
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.MainNotifyEffect = new adeng.comm.notifyEffect();
            this.SplitContainer2 = new System.Windows.Forms.SplitContainer();
            this.DmbIssuesPN = new System.Windows.Forms.Panel();
            this.DmbIssuesLB = new System.Windows.Forms.Label();
            this.DmbIssuesLV = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTB = new System.Windows.Forms.TextBox();
            this.disterTB = new System.Windows.Forms.TextBox();
            this.priTB = new System.Windows.Forms.TextBox();
            this.partTB = new System.Windows.Forms.TextBox();
            this.regionTB = new System.Windows.Forms.TextBox();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dmbmTB = new System.Windows.Forms.RichTextBox();
            this.ttsmTB = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.smsmTB = new System.Windows.Forms.RichTextBox();
            this.DmbIssuesDetailPN = new System.Windows.Forms.Panel();
            this.DmbIssuesDetailLB = new System.Windows.Forms.Label();
            this.DmbIssuesTopPN = new System.Windows.Forms.Panel();
            this.EffectIcoLB = new System.Windows.Forms.Label();
            this.DmbIssuesTopLB = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).BeginInit();
            this.SplitContainer1.Panel1.SuspendLayout();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2)).BeginInit();
            this.SplitContainer2.Panel1.SuspendLayout();
            this.SplitContainer2.Panel2.SuspendLayout();
            this.SplitContainer2.SuspendLayout();
            this.DmbIssuesPN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.DmbIssuesDetailPN.SuspendLayout();
            this.DmbIssuesTopPN.SuspendLayout();
            this.SuspendLayout();
            // 
            // SplitContainer1
            // 
            this.SplitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SplitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SplitContainer1.IsSplitterFixed = true;
            this.SplitContainer1.Location = new System.Drawing.Point(5, 23);
            this.SplitContainer1.Name = "SplitContainer1";
            this.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitContainer1.Panel1
            // 
            this.SplitContainer1.Panel1.Controls.Add(this.MainNotifyEffect);
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.SplitContainer2);
            this.SplitContainer1.Size = new System.Drawing.Size(976, 692);
            this.SplitContainer1.SplitterDistance = 54;
            this.SplitContainer1.TabIndex = 210;
            // 
            // MainNotifyEffect
            // 
            this.MainNotifyEffect.BackColor = System.Drawing.Color.Transparent;
            this.MainNotifyEffect.Blink = false;
            this.MainNotifyEffect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainNotifyEffect.EffectKind = adeng.comm.EffectKind.effect2;
            this.MainNotifyEffect.Font = new System.Drawing.Font("굴림", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.MainNotifyEffect.ForeColor = System.Drawing.Color.Lime;
            this.MainNotifyEffect.InSpeed = 4D;
            this.MainNotifyEffect.Location = new System.Drawing.Point(0, 0);
            this.MainNotifyEffect.Name = "MainNotifyEffect";
            this.MainNotifyEffect.Size = new System.Drawing.Size(976, 54);
            this.MainNotifyEffect.TabIndex = 203;
            this.MainNotifyEffect.WaitTimeMs = 5000;
            this.MainNotifyEffect.DoubleClick += new System.EventHandler(this.MainNotifyEffect_DoubleClick);
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
            this.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitContainer2.Panel1
            // 
            this.SplitContainer2.Panel1.Controls.Add(this.DmbIssuesPN);
            this.SplitContainer2.Panel1.Controls.Add(this.DmbIssuesLV);
            // 
            // SplitContainer2.Panel2
            // 
            this.SplitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.SplitContainer2.Panel2.Controls.Add(this.DmbIssuesDetailPN);
            this.SplitContainer2.Size = new System.Drawing.Size(976, 634);
            this.SplitContainer2.SplitterDistance = 500;
            this.SplitContainer2.SplitterWidth = 2;
            this.SplitContainer2.TabIndex = 207;
            // 
            // DmbIssuesPN
            // 
            this.DmbIssuesPN.BackgroundImage = global::DmbDSSystem.Properties.Resources.DMB_sub;
            this.DmbIssuesPN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DmbIssuesPN.Controls.Add(this.DmbIssuesLB);
            this.DmbIssuesPN.Dock = System.Windows.Forms.DockStyle.Top;
            this.DmbIssuesPN.Location = new System.Drawing.Point(0, 0);
            this.DmbIssuesPN.Margin = new System.Windows.Forms.Padding(0);
            this.DmbIssuesPN.Name = "DmbIssuesPN";
            this.DmbIssuesPN.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.DmbIssuesPN.Size = new System.Drawing.Size(976, 17);
            this.DmbIssuesPN.TabIndex = 215;
            // 
            // DmbIssuesLB
            // 
            this.DmbIssuesLB.BackColor = System.Drawing.Color.Transparent;
            this.DmbIssuesLB.Dock = System.Windows.Forms.DockStyle.Left;
            this.DmbIssuesLB.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DmbIssuesLB.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DmbIssuesLB.Location = new System.Drawing.Point(10, 0);
            this.DmbIssuesLB.Name = "DmbIssuesLB";
            this.DmbIssuesLB.Size = new System.Drawing.Size(110, 17);
            this.DmbIssuesLB.TabIndex = 212;
            this.DmbIssuesLB.Text = "DMB 발령 리스트";
            this.DmbIssuesLB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DmbIssuesLV
            // 
            this.DmbIssuesLV.AllowColumnReorder = true;
            this.DmbIssuesLV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DmbIssuesLV.FullRowSelect = true;
            this.DmbIssuesLV.GridLines = true;
            this.DmbIssuesLV.HideSelection = false;
            this.DmbIssuesLV.Location = new System.Drawing.Point(4, 20);
            this.DmbIssuesLV.Name = "DmbIssuesLV";
            this.DmbIssuesLV.ShowItemToolTips = true;
            this.DmbIssuesLV.Size = new System.Drawing.Size(968, 479);
            this.DmbIssuesLV.SmallImageList = this.imageList1;
            this.DmbIssuesLV.TabIndex = 214;
            this.DmbIssuesLV.UseCompatibleStateImageBehavior = false;
            this.DmbIssuesLV.View = System.Windows.Forms.View.Details;
            this.DmbIssuesLV.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.DmbIssuesLV_ColumnClick);
            this.DmbIssuesLV.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.DmbIssuesLV_ItemSelectionChanged);
            this.DmbIssuesLV.Click += new System.EventHandler(this.DmbIssuesLV_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 17);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(976, 115);
            this.splitContainer3.SplitterDistance = 263;
            this.splitContainer3.SplitterWidth = 2;
            this.splitContainer3.TabIndex = 217;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.dateTB, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.disterTB, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.priTB, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.partTB, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.regionTB, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(263, 115);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(2, 2);
            this.label9.Margin = new System.Windows.Forms.Padding(2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 19);
            this.label9.TabIndex = 240;
            this.label9.Text = "발령일시 : ";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(2, 25);
            this.label8.Margin = new System.Windows.Forms.Padding(2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 19);
            this.label8.TabIndex = 238;
            this.label8.Text = "실제구분 : ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(2, 48);
            this.label7.Margin = new System.Windows.Forms.Padding(2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 19);
            this.label7.TabIndex = 236;
            this.label7.Text = "발령상태 : ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(2, 71);
            this.label5.Margin = new System.Windows.Forms.Padding(2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 19);
            this.label5.TabIndex = 234;
            this.label5.Text = "발령구분 : ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(2, 94);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 19);
            this.label2.TabIndex = 232;
            this.label2.Text = "방송사 : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTB
            // 
            this.dateTB.BackColor = System.Drawing.Color.FloralWhite;
            this.dateTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTB.Location = new System.Drawing.Point(80, 2);
            this.dateTB.Margin = new System.Windows.Forms.Padding(2);
            this.dateTB.Multiline = true;
            this.dateTB.Name = "dateTB";
            this.dateTB.ReadOnly = true;
            this.dateTB.Size = new System.Drawing.Size(181, 19);
            this.dateTB.TabIndex = 239;
            // 
            // disterTB
            // 
            this.disterTB.BackColor = System.Drawing.Color.FloralWhite;
            this.disterTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.disterTB.Location = new System.Drawing.Point(80, 25);
            this.disterTB.Margin = new System.Windows.Forms.Padding(2);
            this.disterTB.Multiline = true;
            this.disterTB.Name = "disterTB";
            this.disterTB.ReadOnly = true;
            this.disterTB.Size = new System.Drawing.Size(181, 19);
            this.disterTB.TabIndex = 237;
            // 
            // priTB
            // 
            this.priTB.BackColor = System.Drawing.Color.FloralWhite;
            this.priTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.priTB.Location = new System.Drawing.Point(80, 48);
            this.priTB.Margin = new System.Windows.Forms.Padding(2);
            this.priTB.Multiline = true;
            this.priTB.Name = "priTB";
            this.priTB.ReadOnly = true;
            this.priTB.Size = new System.Drawing.Size(181, 19);
            this.priTB.TabIndex = 235;
            // 
            // partTB
            // 
            this.partTB.BackColor = System.Drawing.Color.FloralWhite;
            this.partTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.partTB.Location = new System.Drawing.Point(80, 94);
            this.partTB.Margin = new System.Windows.Forms.Padding(2);
            this.partTB.Multiline = true;
            this.partTB.Name = "partTB";
            this.partTB.ReadOnly = true;
            this.partTB.Size = new System.Drawing.Size(181, 19);
            this.partTB.TabIndex = 231;
            // 
            // regionTB
            // 
            this.regionTB.BackColor = System.Drawing.Color.FloralWhite;
            this.regionTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.regionTB.Location = new System.Drawing.Point(80, 71);
            this.regionTB.Margin = new System.Windows.Forms.Padding(2);
            this.regionTB.Multiline = true;
            this.regionTB.Name = "regionTB";
            this.regionTB.ReadOnly = true;
            this.regionTB.Size = new System.Drawing.Size(181, 19);
            this.regionTB.TabIndex = 233;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer4.IsSplitterFixed = true;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer4.Panel2MinSize = 0;
            this.splitContainer4.Size = new System.Drawing.Size(711, 115);
            this.splitContainer4.SplitterDistance = 707;
            this.splitContainer4.SplitterWidth = 2;
            this.splitContainer4.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.4F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3F));
            this.tableLayoutPanel2.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.dmbmTB, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.ttsmTB, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.smsmTB, 2, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(707, 115);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(2, 2);
            this.label10.Margin = new System.Windows.Forms.Padding(2);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(232, 19);
            this.label10.TabIndex = 226;
            this.label10.Text = "일반수신기 단문메시지";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(238, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 19);
            this.label1.TabIndex = 227;
            this.label1.Text = "특수수신기 단문메시지";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dmbmTB
            // 
            this.dmbmTB.BackColor = System.Drawing.Color.FloralWhite;
            this.dmbmTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dmbmTB.Location = new System.Drawing.Point(2, 25);
            this.dmbmTB.Margin = new System.Windows.Forms.Padding(2);
            this.dmbmTB.Name = "dmbmTB";
            this.dmbmTB.ReadOnly = true;
            this.dmbmTB.Size = new System.Drawing.Size(232, 88);
            this.dmbmTB.TabIndex = 227;
            this.dmbmTB.Text = "";
            // 
            // ttsmTB
            // 
            this.ttsmTB.BackColor = System.Drawing.Color.FloralWhite;
            this.ttsmTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ttsmTB.Location = new System.Drawing.Point(238, 25);
            this.ttsmTB.Margin = new System.Windows.Forms.Padding(2);
            this.ttsmTB.Name = "ttsmTB";
            this.ttsmTB.ReadOnly = true;
            this.ttsmTB.Size = new System.Drawing.Size(231, 88);
            this.ttsmTB.TabIndex = 228;
            this.ttsmTB.Text = "";
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(473, 2);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(232, 19);
            this.label3.TabIndex = 228;
            this.label3.Text = "SMS 단문메시지";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // smsmTB
            // 
            this.smsmTB.BackColor = System.Drawing.Color.FloralWhite;
            this.smsmTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smsmTB.Location = new System.Drawing.Point(473, 25);
            this.smsmTB.Margin = new System.Windows.Forms.Padding(2);
            this.smsmTB.Name = "smsmTB";
            this.smsmTB.ReadOnly = true;
            this.smsmTB.Size = new System.Drawing.Size(232, 88);
            this.smsmTB.TabIndex = 229;
            this.smsmTB.Text = "";
            // 
            // DmbIssuesDetailPN
            // 
            this.DmbIssuesDetailPN.BackgroundImage = global::DmbDSSystem.Properties.Resources.DMB_sub;
            this.DmbIssuesDetailPN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DmbIssuesDetailPN.Controls.Add(this.DmbIssuesDetailLB);
            this.DmbIssuesDetailPN.Dock = System.Windows.Forms.DockStyle.Top;
            this.DmbIssuesDetailPN.Location = new System.Drawing.Point(0, 0);
            this.DmbIssuesDetailPN.Margin = new System.Windows.Forms.Padding(0);
            this.DmbIssuesDetailPN.Name = "DmbIssuesDetailPN";
            this.DmbIssuesDetailPN.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.DmbIssuesDetailPN.Size = new System.Drawing.Size(976, 17);
            this.DmbIssuesDetailPN.TabIndex = 216;
            // 
            // DmbIssuesDetailLB
            // 
            this.DmbIssuesDetailLB.BackColor = System.Drawing.Color.Transparent;
            this.DmbIssuesDetailLB.Dock = System.Windows.Forms.DockStyle.Left;
            this.DmbIssuesDetailLB.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DmbIssuesDetailLB.ForeColor = System.Drawing.Color.Black;
            this.DmbIssuesDetailLB.Location = new System.Drawing.Point(10, 0);
            this.DmbIssuesDetailLB.Name = "DmbIssuesDetailLB";
            this.DmbIssuesDetailLB.Size = new System.Drawing.Size(128, 17);
            this.DmbIssuesDetailLB.TabIndex = 215;
            this.DmbIssuesDetailLB.Text = "DMB 발령 상세 정보";
            this.DmbIssuesDetailLB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DmbIssuesTopPN
            // 
            this.DmbIssuesTopPN.BackgroundImage = global::DmbDSSystem.Properties.Resources.DMB_main;
            this.DmbIssuesTopPN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DmbIssuesTopPN.Controls.Add(this.EffectIcoLB);
            this.DmbIssuesTopPN.Controls.Add(this.DmbIssuesTopLB);
            this.DmbIssuesTopPN.Dock = System.Windows.Forms.DockStyle.Top;
            this.DmbIssuesTopPN.Location = new System.Drawing.Point(2, 2);
            this.DmbIssuesTopPN.Margin = new System.Windows.Forms.Padding(0);
            this.DmbIssuesTopPN.Name = "DmbIssuesTopPN";
            this.DmbIssuesTopPN.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.DmbIssuesTopPN.Size = new System.Drawing.Size(981, 18);
            this.DmbIssuesTopPN.TabIndex = 215;
            // 
            // EffectIcoLB
            // 
            this.EffectIcoLB.BackColor = System.Drawing.Color.Transparent;
            this.EffectIcoLB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EffectIcoLB.Dock = System.Windows.Forms.DockStyle.Right;
            this.EffectIcoLB.Image = global::DmbDSSystem.Properties.Resources.DMB_reset;
            this.EffectIcoLB.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.EffectIcoLB.Location = new System.Drawing.Point(957, 0);
            this.EffectIcoLB.Name = "EffectIcoLB";
            this.EffectIcoLB.Size = new System.Drawing.Size(24, 18);
            this.EffectIcoLB.TabIndex = 216;
            this.toolTip1.SetToolTip(this.EffectIcoLB, "정보창 초기화");
            this.EffectIcoLB.Click += new System.EventHandler(this.EffectIcoLB_Click);
            // 
            // DmbIssuesTopLB
            // 
            this.DmbIssuesTopLB.BackColor = System.Drawing.Color.Transparent;
            this.DmbIssuesTopLB.Dock = System.Windows.Forms.DockStyle.Left;
            this.DmbIssuesTopLB.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DmbIssuesTopLB.Location = new System.Drawing.Point(10, 0);
            this.DmbIssuesTopLB.Name = "DmbIssuesTopLB";
            this.DmbIssuesTopLB.Size = new System.Drawing.Size(97, 18);
            this.DmbIssuesTopLB.TabIndex = 215;
            this.DmbIssuesTopLB.Text = "DMB 발령 정보";
            this.DmbIssuesTopLB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 722);
            this.Controls.Add(this.DmbIssuesTopPN);
            this.Controls.Add(this.SplitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainViewForm";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "발령 현황";
            this.Activated += new System.EventHandler(this.MainViewForm_Activated);
            this.Deactivate += new System.EventHandler(this.MainViewForm_Deactivate);
            this.SplitContainer1.Panel1.ResumeLayout(false);
            this.SplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).EndInit();
            this.SplitContainer1.ResumeLayout(false);
            this.SplitContainer2.Panel1.ResumeLayout(false);
            this.SplitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2)).EndInit();
            this.SplitContainer2.ResumeLayout(false);
            this.DmbIssuesPN.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.splitContainer4.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.DmbIssuesDetailPN.ResumeLayout(false);
            this.DmbIssuesTopPN.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer SplitContainer1;
        private System.Windows.Forms.SplitContainer SplitContainer2;
        private System.Windows.Forms.Label DmbIssuesDetailLB;
        private System.Windows.Forms.Label DmbIssuesLB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox dateTB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox disterTB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox priTB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox regionTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox partTB;
        private System.Windows.Forms.Panel DmbIssuesTopPN;
        private System.Windows.Forms.Label DmbIssuesTopLB;
        private System.Windows.Forms.Panel DmbIssuesDetailPN;
        private System.Windows.Forms.Panel DmbIssuesPN;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox dmbmTB;
        private System.Windows.Forms.RichTextBox ttsmTB;
        private System.Windows.Forms.RichTextBox smsmTB;
        public adeng.comm.notifyEffect MainNotifyEffect;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label EffectIcoLB;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.ListView DmbIssuesLV;

    }
}
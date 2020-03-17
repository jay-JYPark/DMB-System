namespace DmbDSSystem
{
    partial class Confirm
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
            this.ConfirmCancleBtn = new System.Windows.Forms.Button();
            this.ConfirmOkBtn = new System.Windows.Forms.Button();
            this.ConfirmTB = new System.Windows.Forms.TextBox();
            this.ConfirmLB = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ConfirmCancleBtn
            // 
            this.ConfirmCancleBtn.Location = new System.Drawing.Point(131, 62);
            this.ConfirmCancleBtn.Name = "ConfirmCancleBtn";
            this.ConfirmCancleBtn.Size = new System.Drawing.Size(75, 23);
            this.ConfirmCancleBtn.TabIndex = 12;
            this.ConfirmCancleBtn.Text = "취소";
            this.ConfirmCancleBtn.UseVisualStyleBackColor = true;
            this.ConfirmCancleBtn.Click += new System.EventHandler(this.ConfirmCancleBtn_Click);
            // 
            // ConfirmOkBtn
            // 
            this.ConfirmOkBtn.Enabled = false;
            this.ConfirmOkBtn.Location = new System.Drawing.Point(50, 62);
            this.ConfirmOkBtn.Name = "ConfirmOkBtn";
            this.ConfirmOkBtn.Size = new System.Drawing.Size(75, 23);
            this.ConfirmOkBtn.TabIndex = 11;
            this.ConfirmOkBtn.Text = "확인";
            this.ConfirmOkBtn.UseVisualStyleBackColor = true;
            this.ConfirmOkBtn.Click += new System.EventHandler(this.ConfirmOkBtn_Click);
            // 
            // ConfirmTB
            // 
            this.ConfirmTB.Location = new System.Drawing.Point(89, 21);
            this.ConfirmTB.MaxLength = 15;
            this.ConfirmTB.Name = "ConfirmTB";
            this.ConfirmTB.PasswordChar = '*';
            this.ConfirmTB.Size = new System.Drawing.Size(142, 21);
            this.ConfirmTB.TabIndex = 10;
            this.ConfirmTB.TextChanged += new System.EventHandler(this.ConfirmTB_TextChanged);
            this.ConfirmTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ConfirmTB_KeyPress);
            // 
            // ConfirmLB
            // 
            this.ConfirmLB.AutoSize = true;
            this.ConfirmLB.Location = new System.Drawing.Point(26, 26);
            this.ConfirmLB.Name = "ConfirmLB";
            this.ConfirmLB.Size = new System.Drawing.Size(57, 12);
            this.ConfirmLB.TabIndex = 9;
            this.ConfirmLB.Text = "암호 입력";
            // 
            // Confirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 106);
            this.ControlBox = false;
            this.Controls.Add(this.ConfirmCancleBtn);
            this.Controls.Add(this.ConfirmOkBtn);
            this.Controls.Add(this.ConfirmTB);
            this.Controls.Add(this.ConfirmLB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Confirm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "관리자 확인";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConfirmCancleBtn;
        private System.Windows.Forms.Button ConfirmOkBtn;
        private System.Windows.Forms.TextBox ConfirmTB;
        private System.Windows.Forms.Label ConfirmLB;

    }
}
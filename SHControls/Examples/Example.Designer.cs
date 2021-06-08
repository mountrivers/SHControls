namespace SHControls
{
    partial class Example1
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.gradationButton1 = new SHControls.Buttons.GradationButton();
            this.SuspendLayout();
            // 
            // gradationButton1
            // 
            this.gradationButton1.BorderColor = System.Drawing.Color.Black;
            this.gradationButton1.BorderSize = 5;
            this.gradationButton1.GradatinEndColor = System.Drawing.Color.Yellow;
            this.gradationButton1.GradationStartColor = System.Drawing.Color.Green;
            this.gradationButton1.GradtionRatio = 0F;
            this.gradationButton1.Location = new System.Drawing.Point(435, 166);
            this.gradationButton1.Name = "gradationButton1";
            this.gradationButton1.Padding = new System.Windows.Forms.Padding(10);
            this.gradationButton1.Size = new System.Drawing.Size(231, 129);
            this.gradationButton1.TabIndex = 0;
            this.gradationButton1.Text = "갇겆 ㄱ저ㅑㄷ";
            this.gradationButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.gradationButton1.UseVisualStyleBackColor = true;
            // 
            // Example1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 460);
            this.Controls.Add(this.gradationButton1);
            this.Name = "Example1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Buttons.GradationButton gradationButton1;
    }
}


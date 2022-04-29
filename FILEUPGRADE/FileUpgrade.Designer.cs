namespace FILEUPGRADE
{
    partial class FileUpgrade
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileUpgrade));
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelControl1 = new System.Windows.Forms.Label();
            this.labelControl2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.White;
            this.progressBar1.Location = new System.Drawing.Point(10, 54);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(278, 11);
            this.progressBar1.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.AutoSize = true;
            this.labelControl1.ForeColor = System.Drawing.Color.Black;
            this.labelControl1.Location = new System.Drawing.Point(8, 71);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(129, 12);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "업그레이드 확인중......";
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSize = true;
            this.labelControl2.ForeColor = System.Drawing.Color.Black;
            this.labelControl2.Location = new System.Drawing.Point(11, 36);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(174, 12);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "Ftp서버에서 다운로드중입니다.";
            // 
            // FileUpgrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 94);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FileUpgrade";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FileUpgrade";
            this.Load += new System.EventHandler(this.FileUpgrade_Load);
            this.Shown += new System.EventHandler(this.FileUpgrade_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelControl1;
        private System.Windows.Forms.Label labelControl2;
    }
}


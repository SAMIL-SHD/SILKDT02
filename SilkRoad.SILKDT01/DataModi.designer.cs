namespace SilkRoad.SILKDT01
{
    partial class DataModi
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataModi));
			this.srGroupBox1 = new SilkRoad.UserControls.SRGroupBox();
			this.srPanel1 = new SilkRoad.UserControls.SRPanel();
			this.srLabel1 = new SilkRoad.UserControls.SRLabel();
			this.btn_close = new SilkRoad.UserControls.SRButton();
			this.btn_proc = new SilkRoad.UserControls.SRButton();
			this.btn_tb_c = new SilkRoad.UserControls.SRButton();
			((System.ComponentModel.ISupportInitialize)(this.srGroupBox1)).BeginInit();
			this.srGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.srPanel1)).BeginInit();
			this.srPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// srGroupBox1
			// 
			this.srGroupBox1.Appearance.Font = new System.Drawing.Font("굴림체", 9F);
			this.srGroupBox1.Appearance.Options.UseFont = true;
			this.srGroupBox1.Controls.Add(this.srPanel1);
			this.srGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.srGroupBox1.Location = new System.Drawing.Point(0, 0);
			this.srGroupBox1.Name = "srGroupBox1";
			this.srGroupBox1.Size = new System.Drawing.Size(457, 307);
			this.srGroupBox1.TabIndex = 0;
			this.srGroupBox1.Text = "DataModi";
			// 
			// srPanel1
			// 
			this.srPanel1.Controls.Add(this.srLabel1);
			this.srPanel1.Controls.Add(this.btn_close);
			this.srPanel1.Controls.Add(this.btn_proc);
			this.srPanel1.Controls.Add(this.btn_tb_c);
			this.srPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.srPanel1.Location = new System.Drawing.Point(2, 21);
			this.srPanel1.Name = "srPanel1";
			this.srPanel1.Size = new System.Drawing.Size(453, 50);
			this.srPanel1.TabIndex = 2;
			// 
			// srLabel1
			// 
			this.srLabel1.AutoSize = true;
			this.srLabel1.BackColor = System.Drawing.Color.Transparent;
			this.srLabel1.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.srLabel1.ForeColor = System.Drawing.Color.Blue;
			this.srLabel1.Location = new System.Drawing.Point(45, 32);
			this.srLabel1.Name = "srLabel1";
			this.srLabel1.Size = new System.Drawing.Size(122, 15);
			this.srLabel1.SRBulletColor = System.Drawing.Color.Empty;
			this.srLabel1.TabIndex = 275;
			this.srLabel1.Text = "테이블 생성중입니다.";
			this.srLabel1.Visible = false;
			// 
			// btn_close
			// 
			this.btn_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_close.Authority = false;
			this.btn_close.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.btn_close.Image = ((System.Drawing.Image)(resources.GetObject("btn_close.Image")));
			this.btn_close.Location = new System.Drawing.Point(387, 6);
			this.btn_close.Name = "btn_close";
			this.btn_close.Size = new System.Drawing.Size(60, 24);
			this.btn_close.SRAuthCrud = SilkRoad.UserControls.SRButton.AuthCrudType.NONE;
			this.btn_close.SRKindOf = SilkRoad.UserControls.SRButton.ButtonKindOfType.종료;
			this.btn_close.SRWidthType = SilkRoad.UserControls.SRButton.WidthType.WIDTH_60;
			this.btn_close.TabIndex = 6;
			this.btn_close.Text = "종료";
			this.btn_close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btn_close.UseVisualStyleBackColor = true;
			this.btn_close.Click += new System.EventHandler(this.btn_exit_Click);
			// 
			// btn_proc
			// 
			this.btn_proc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_proc.Authority = false;
			this.btn_proc.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.btn_proc.Image = ((System.Drawing.Image)(resources.GetObject("btn_proc.Image")));
			this.btn_proc.Location = new System.Drawing.Point(326, 6);
			this.btn_proc.Name = "btn_proc";
			this.btn_proc.Size = new System.Drawing.Size(60, 24);
			this.btn_proc.SRAuthCrud = SilkRoad.UserControls.SRButton.AuthCrudType.R;
			this.btn_proc.SRKindOf = SilkRoad.UserControls.SRButton.ButtonKindOfType.처리;
			this.btn_proc.SRWidthType = SilkRoad.UserControls.SRButton.WidthType.WIDTH_60;
			this.btn_proc.TabIndex = 2;
			this.btn_proc.Text = "처리";
			this.btn_proc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btn_proc.UseVisualStyleBackColor = true;
			this.btn_proc.Click += new System.EventHandler(this.btn_proc_Click);
			// 
			// btn_tb_c
			// 
			this.btn_tb_c.Authority = false;
			this.btn_tb_c.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.btn_tb_c.Location = new System.Drawing.Point(10, 6);
			this.btn_tb_c.Name = "btn_tb_c";
			this.btn_tb_c.Size = new System.Drawing.Size(251, 24);
			this.btn_tb_c.SRAuthCrud = SilkRoad.UserControls.SRButton.AuthCrudType.NONE;
			this.btn_tb_c.TabIndex = 0;
			this.btn_tb_c.Text = "테이블생성(SLMS Create Table)";
			this.btn_tb_c.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btn_tb_c.UseVisualStyleBackColor = true;
			this.btn_tb_c.Click += new System.EventHandler(this.btn_tb_c_Click);
			// 
			// DataModi
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(457, 307);
			this.Controls.Add(this.srGroupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(400, 39);
			this.Name = "DataModi";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "DataModification(Ms-Sql)";
			this.Load += new System.EventHandler(this.DataModi_Load);
			((System.ComponentModel.ISupportInitialize)(this.srGroupBox1)).EndInit();
			this.srGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.srPanel1)).EndInit();
			this.srPanel1.ResumeLayout(false);
			this.srPanel1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private SilkRoad.UserControls.SRGroupBox srGroupBox1;
        private SilkRoad.UserControls.SRButton btn_tb_c;
        private SilkRoad.UserControls.SRPanel srPanel1;
        private SilkRoad.UserControls.SRButton btn_proc;
        private SilkRoad.UserControls.SRButton btn_close;
        private SilkRoad.UserControls.SRLabel srLabel1;
    }
}


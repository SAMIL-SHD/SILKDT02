namespace SilkRoad.SILKDT01
{
    partial class QRcode
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QRcode));
			this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.srPanel1 = new SilkRoad.UserControls.SRPanel();
			this.txt_url = new DevExpress.XtraEditors.MemoEdit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.srPanel1)).BeginInit();
			this.srPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txt_url.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// gridColumn15
			// 
			this.gridColumn15.Caption = "코드";
			this.gridColumn15.FieldName = "CODE";
			this.gridColumn15.Name = "gridColumn15";
			this.gridColumn15.Visible = true;
			this.gridColumn15.VisibleIndex = 0;
			// 
			// gridColumn20
			// 
			this.gridColumn20.Caption = "명칭";
			this.gridColumn20.FieldName = "NAME";
			this.gridColumn20.Name = "gridColumn20";
			this.gridColumn20.Visible = true;
			this.gridColumn20.VisibleIndex = 1;
			// 
			// gridColumn21
			// 
			this.gridColumn21.Caption = "코드";
			this.gridColumn21.FieldName = "CODE";
			this.gridColumn21.Name = "gridColumn21";
			this.gridColumn21.Visible = true;
			this.gridColumn21.VisibleIndex = 0;
			// 
			// gridColumn22
			// 
			this.gridColumn22.Caption = "명칭";
			this.gridColumn22.FieldName = "NAME";
			this.gridColumn22.Name = "gridColumn22";
			this.gridColumn22.Visible = true;
			this.gridColumn22.VisibleIndex = 1;
			// 
			// gridColumn7
			// 
			this.gridColumn7.Caption = "사번";
			this.gridColumn7.FieldName = "NAME";
			this.gridColumn7.Name = "gridColumn7";
			this.gridColumn7.Visible = true;
			this.gridColumn7.VisibleIndex = 0;
			// 
			// gridColumn8
			// 
			this.gridColumn8.Caption = "이름";
			this.gridColumn8.FieldName = "NAME";
			this.gridColumn8.Name = "gridColumn8";
			this.gridColumn8.Visible = true;
			this.gridColumn8.VisibleIndex = 1;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox1.InitialImage = null;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(363, 348);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// pictureBox2
			// 
			this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox2.Image = global::SilkRoad.SILKDT01.Properties.Resources.kshp_qr;
			this.pictureBox2.InitialImage = null;
			this.pictureBox2.Location = new System.Drawing.Point(0, 0);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(363, 261);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox2.TabIndex = 2;
			this.pictureBox2.TabStop = false;
			// 
			// srPanel1
			// 
			this.srPanel1.Controls.Add(this.txt_url);
			this.srPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.srPanel1.Location = new System.Drawing.Point(0, 261);
			this.srPanel1.Name = "srPanel1";
			this.srPanel1.Size = new System.Drawing.Size(363, 87);
			this.srPanel1.TabIndex = 3;
			// 
			// txt_url
			// 
			this.txt_url.EditValue = "http://125.136.91.159:8080/kshp/home";
			this.txt_url.Location = new System.Drawing.Point(10, 9);
			this.txt_url.Name = "txt_url";
			this.txt_url.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.txt_url.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
			this.txt_url.Properties.Appearance.Options.UseBackColor = true;
			this.txt_url.Properties.Appearance.Options.UseFont = true;
			this.txt_url.Properties.ReadOnly = true;
			this.txt_url.Size = new System.Drawing.Size(343, 66);
			this.txt_url.TabIndex = 0;
			// 
			// QRcode
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(363, 348);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.srPanel1);
			this.Controls.Add(this.pictureBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "QRcode";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "KS병원-(QRcode)";
			this.Load += new System.EventHandler(this.QRcode_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.srPanel1)).EndInit();
			this.srPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.txt_url.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.PictureBox pictureBox2;
		private UserControls.SRPanel srPanel1;
		private DevExpress.XtraEditors.MemoEdit txt_url;
	}
}


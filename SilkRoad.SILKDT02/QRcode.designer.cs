namespace SilkRoad.SILKDT02
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
            this.srTabControl1 = new SilkRoad.UserControls.SRTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.mm_end = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.srPanel1)).BeginInit();
            this.srPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_url.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.srTabControl1)).BeginInit();
            this.srTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mm_end.Properties)).BeginInit();
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
            this.pictureBox1.Size = new System.Drawing.Size(436, 435);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::SilkRoad.SILKDT02.Properties.Resources.sghp_qr1;
            this.pictureBox2.InitialImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(34, 13);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(363, 261);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // srPanel1
            // 
            this.srPanel1.Controls.Add(this.txt_url);
            this.srPanel1.Location = new System.Drawing.Point(5, 291);
            this.srPanel1.Name = "srPanel1";
            this.srPanel1.Size = new System.Drawing.Size(421, 87);
            this.srPanel1.TabIndex = 3;
            // 
            // txt_url
            // 
            this.txt_url.EditValue = "http://112.216.130.162:8080/sghp";
            this.txt_url.Location = new System.Drawing.Point(7, 9);
            this.txt_url.Name = "txt_url";
            this.txt_url.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txt_url.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.txt_url.Properties.Appearance.Options.UseBackColor = true;
            this.txt_url.Properties.Appearance.Options.UseFont = true;
            this.txt_url.Properties.ReadOnly = true;
            this.txt_url.Size = new System.Drawing.Size(406, 66);
            this.txt_url.TabIndex = 0;
            // 
            // srTabControl1
            // 
            this.srTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.srTabControl1.Location = new System.Drawing.Point(0, 0);
            this.srTabControl1.Name = "srTabControl1";
            this.srTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.srTabControl1.Size = new System.Drawing.Size(436, 435);
            this.srTabControl1.TabIndex = 4;
            this.srTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Appearance.Header.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.xtraTabPage1.Appearance.Header.Options.UseFont = true;
            this.xtraTabPage1.Appearance.HeaderActive.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.xtraTabPage1.Appearance.HeaderActive.Options.UseFont = true;
            this.xtraTabPage1.Controls.Add(this.pictureBox2);
            this.xtraTabPage1.Controls.Add(this.srPanel1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(430, 405);
            this.xtraTabPage1.Text = "QR코드 및 주소";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Appearance.Header.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.xtraTabPage2.Appearance.Header.Options.UseFont = true;
            this.xtraTabPage2.Appearance.HeaderActive.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.xtraTabPage2.Appearance.HeaderActive.Options.UseFont = true;
            this.xtraTabPage2.AutoScroll = true;
            this.xtraTabPage2.Controls.Add(this.mm_end);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(430, 405);
            this.xtraTabPage2.Text = "월마감프로세스";
            // 
            // mm_end
            // 
            this.mm_end.EditValue = resources.GetString("mm_end.EditValue");
            this.mm_end.Location = new System.Drawing.Point(3, 3);
            this.mm_end.Name = "mm_end";
            this.mm_end.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9.5F);
            this.mm_end.Properties.Appearance.Options.UseFont = true;
            this.mm_end.Properties.ReadOnly = true;
            this.mm_end.Size = new System.Drawing.Size(717, 384);
            this.mm_end.TabIndex = 8;
            // 
            // QRcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 435);
            this.Controls.Add(this.srTabControl1);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "QRcode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "신가병원-(QRcode)";
            this.Load += new System.EventHandler(this.QRcode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.srPanel1)).EndInit();
            this.srPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txt_url.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.srTabControl1)).EndInit();
            this.srTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mm_end.Properties)).EndInit();
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
		private UserControls.SRTabControl srTabControl1;
		private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
		private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
		private DevExpress.XtraEditors.MemoEdit mm_end;
	}
}


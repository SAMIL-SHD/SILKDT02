namespace DUTY1000
{
    partial class duty5081
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(duty5081));
            this.btn_exit = new SilkRoad.UserControls.SRButton();
            this.srPanel1 = new SilkRoad.UserControls.SRPanel();
            this.srTitle1 = new SilkRoad.UserControls.SRTitle();
            this.srPanel3 = new SilkRoad.UserControls.SRPanel();
            this.btn_save = new SilkRoad.UserControls.SRButton();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gr_detail = new SilkRoad.UserControls.SRGroupBox();
            this.grd_sign = new DevExpress.XtraGrid.GridControl();
            this.grdv_sign = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.repositoryItemTextEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.srPanel1)).BeginInit();
            this.srPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.srPanel3)).BeginInit();
            this.srPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gr_detail)).BeginInit();
            this.gr_detail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_sign)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdv_sign)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit4)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_exit
            // 
            this.btn_exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_exit.Authority = false;
            this.btn_exit.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btn_exit.Image = ((System.Drawing.Image)(resources.GetObject("btn_exit.Image")));
            this.btn_exit.Location = new System.Drawing.Point(73, 9);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(60, 24);
            this.btn_exit.SRAuthCrud = SilkRoad.UserControls.SRButton.AuthCrudType.NONE;
            this.btn_exit.SRKindOf = SilkRoad.UserControls.SRButton.ButtonKindOfType.종료;
            this.btn_exit.SRWidthType = SilkRoad.UserControls.SRButton.WidthType.WIDTH_60;
            this.btn_exit.TabIndex = 6;
            this.btn_exit.Text = "종료";
            this.btn_exit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_exit.UseVisualStyleBackColor = true;
            // 
            // srPanel1
            // 
            this.srPanel1.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.srPanel1.Appearance.Options.UseBackColor = true;
            this.srPanel1.Controls.Add(this.srTitle1);
            this.srPanel1.Controls.Add(this.srPanel3);
            this.srPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.srPanel1.Location = new System.Drawing.Point(0, 0);
            this.srPanel1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.srPanel1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.srPanel1.Name = "srPanel1";
            this.srPanel1.Size = new System.Drawing.Size(398, 46);
            this.srPanel1.TabIndex = 0;
            // 
            // srTitle1
            // 
            this.srTitle1.BackColor = System.Drawing.SystemColors.Control;
            this.srTitle1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.srTitle1.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.srTitle1.Location = new System.Drawing.Point(2, 2);
            this.srTitle1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.srTitle1.Name = "srTitle1";
            this.srTitle1.Size = new System.Drawing.Size(182, 39);
            this.srTitle1.SRTitleTxt = "담당직함수정";
            this.srTitle1.TabIndex = 26;
            this.srTitle1.TabStop = false;
            // 
            // srPanel3
            // 
            this.srPanel3.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.srPanel3.Appearance.Options.UseBackColor = true;
            this.srPanel3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.srPanel3.Controls.Add(this.btn_save);
            this.srPanel3.Controls.Add(this.btn_exit);
            this.srPanel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.srPanel3.Location = new System.Drawing.Point(255, 3);
            this.srPanel3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.srPanel3.LookAndFeel.UseDefaultLookAndFeel = false;
            this.srPanel3.Name = "srPanel3";
            this.srPanel3.Size = new System.Drawing.Size(140, 40);
            this.srPanel3.TabIndex = 0;
            // 
            // btn_save
            // 
            this.btn_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_save.Authority = false;
            this.btn_save.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btn_save.Image = ((System.Drawing.Image)(resources.GetObject("btn_save.Image")));
            this.btn_save.Location = new System.Drawing.Point(12, 9);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(60, 24);
            this.btn_save.SRAuthCrud = SilkRoad.UserControls.SRButton.AuthCrudType.U;
            this.btn_save.SRKindOf = SilkRoad.UserControls.SRButton.ButtonKindOfType.수정;
            this.btn_save.SRWidthType = SilkRoad.UserControls.SRButton.WidthType.WIDTH_60;
            this.btn_save.TabIndex = 2;
            this.btn_save.Text = "수정";
            this.btn_save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
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
            // gr_detail
            // 
            this.gr_detail.AppearanceCaption.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.gr_detail.AppearanceCaption.Options.UseFont = true;
            this.gr_detail.Controls.Add(this.grd_sign);
            this.gr_detail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gr_detail.Location = new System.Drawing.Point(0, 46);
            this.gr_detail.Name = "gr_detail";
            this.gr_detail.Size = new System.Drawing.Size(398, 225);
            this.gr_detail.TabIndex = 1;
            this.gr_detail.Text = "내역";
            // 
            // grd_sign
            // 
            this.grd_sign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd_sign.Location = new System.Drawing.Point(2, 22);
            this.grd_sign.MainView = this.grdv_sign;
            this.grd_sign.Name = "grd_sign";
            this.grd_sign.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1,
            this.repositoryItemTextEdit4});
            this.grd_sign.Size = new System.Drawing.Size(394, 201);
            this.grd_sign.TabIndex = 10;
            this.grd_sign.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdv_sign});
            // 
            // grdv_sign
            // 
            this.grdv_sign.Appearance.HeaderPanel.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.grdv_sign.Appearance.HeaderPanel.Options.UseFont = true;
            this.grdv_sign.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grdv_sign.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grdv_sign.Appearance.Row.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.grdv_sign.Appearance.Row.Options.UseFont = true;
            this.grdv_sign.ColumnPanelRowHeight = 25;
            this.grdv_sign.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn19,
            this.gridColumn23});
            this.grdv_sign.GridControl = this.grd_sign;
            this.grdv_sign.Name = "grdv_sign";
            this.grdv_sign.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.grdv_sign.OptionsNavigation.EnterMoveNextColumn = true;
            this.grdv_sign.OptionsView.ShowGroupPanel = false;
            this.grdv_sign.RowHeight = 25;
            // 
            // gridColumn19
            // 
            this.gridColumn19.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gridColumn19.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn19.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn19.Caption = "이름";
            this.gridColumn19.FieldName = "LINE_SANM";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.OptionsColumn.AllowEdit = false;
            this.gridColumn19.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn19.OptionsColumn.ReadOnly = true;
            this.gridColumn19.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 0;
            this.gridColumn19.Width = 105;
            // 
            // gridColumn23
            // 
            this.gridColumn23.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn23.Caption = "담당직함(예:팀장)";
            this.gridColumn23.FieldName = "LINE_JIWK";
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn23.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn23.Visible = true;
            this.gridColumn23.VisibleIndex = 1;
            this.gridColumn23.Width = 115;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // repositoryItemTextEdit4
            // 
            this.repositoryItemTextEdit4.Appearance.Options.UseTextOptions = true;
            this.repositoryItemTextEdit4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.repositoryItemTextEdit4.AutoHeight = false;
            this.repositoryItemTextEdit4.Mask.EditMask = "n1";
            this.repositoryItemTextEdit4.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit4.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemTextEdit4.Name = "repositoryItemTextEdit4";
            // 
            // duty5081
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 271);
            this.Controls.Add(this.gr_detail);
            this.Controls.Add(this.srPanel1);
            this.Name = "duty5081";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "duty5081";
            this.Load += new System.EventHandler(this.duty5081_Load);
            this.Shown += new System.EventHandler(this.duty5081_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.duty5081_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.srPanel1)).EndInit();
            this.srPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.srPanel3)).EndInit();
            this.srPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gr_detail)).EndInit();
            this.gr_detail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grd_sign)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdv_sign)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SilkRoad.UserControls.SRButton btn_exit;
        private SilkRoad.UserControls.SRPanel srPanel1;
        private SilkRoad.UserControls.SRPanel srPanel3;
        private SilkRoad.UserControls.SRTitle srTitle1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private SilkRoad.UserControls.SRButton btn_save;
        private SilkRoad.UserControls.SRGroupBox gr_detail;
        private DevExpress.XtraGrid.GridControl grd_sign;
        private DevExpress.XtraGrid.Views.Grid.GridView grdv_sign;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit4;
    }
}


namespace WAGE1000
{
    partial class duty1005
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(duty1005));
            this.col_idnm = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grd_sl_dept = new SilkRoad.UserControls.SRgridLookup2();
            this.sRgridLookup21View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btn_exit = new SilkRoad.UserControls.SRButton();
            this.srPanel1 = new SilkRoad.UserControls.SRPanel();
            this.srTitle1 = new SilkRoad.UserControls.SRTitle();
            this.srPanel3 = new SilkRoad.UserControls.SRPanel();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gr_detail = new SilkRoad.UserControls.SRGroupBox();
            this.grd_dept = new DevExpress.XtraGrid.GridControl();
            this.grdv_dept = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.grd_sl_dept2 = new SilkRoad.UserControls.SRgridLookup2();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btn_dept_save = new SilkRoad.UserControls.SRButton();
            this.grd1 = new DevExpress.XtraGrid.GridControl();
            this.grdv1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_dept = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grd_chk1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.srPanel2 = new SilkRoad.UserControls.SRPanel();
            this.srPanel4 = new SilkRoad.UserControls.SRPanel();
            ((System.ComponentModel.ISupportInitialize)(this.grd_sl_dept)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sRgridLookup21View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.srPanel1)).BeginInit();
            this.srPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.srPanel3)).BeginInit();
            this.srPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gr_detail)).BeginInit();
            this.gr_detail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_dept)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdv_dept)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_sl_dept2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdv1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_chk1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.srPanel2)).BeginInit();
            this.srPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.srPanel4)).BeginInit();
            this.srPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // col_idnm
            // 
            this.col_idnm.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.col_idnm.AppearanceCell.Options.UseBackColor = true;
            this.col_idnm.AppearanceCell.Options.UseTextOptions = true;
            this.col_idnm.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_idnm.Caption = "사용자명";
            this.col_idnm.FieldName = "USERNAME";
            this.col_idnm.Name = "col_idnm";
            this.col_idnm.OptionsColumn.AllowEdit = false;
            this.col_idnm.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.col_idnm.OptionsColumn.ReadOnly = true;
            this.col_idnm.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.col_idnm.Visible = true;
            this.col_idnm.VisibleIndex = 1;
            this.col_idnm.Width = 129;
            // 
            // grd_sl_dept
            // 
            this.grd_sl_dept.AutoHeight = false;
            this.grd_sl_dept.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.grd_sl_dept.DisplayMember = "NAME";
            this.grd_sl_dept.Name = "grd_sl_dept";
            this.grd_sl_dept.NullText = "[선택하세요.]";
            this.grd_sl_dept.ValueMember = "CODE";
            this.grd_sl_dept.View = this.sRgridLookup21View;
            // 
            // sRgridLookup21View
            // 
            this.sRgridLookup21View.Appearance.HeaderPanel.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.sRgridLookup21View.Appearance.HeaderPanel.Options.UseFont = true;
            this.sRgridLookup21View.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.sRgridLookup21View.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.sRgridLookup21View.Appearance.Row.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.sRgridLookup21View.Appearance.Row.Options.UseFont = true;
            this.sRgridLookup21View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.sRgridLookup21View.Name = "sRgridLookup21View";
            this.sRgridLookup21View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.sRgridLookup21View.OptionsView.ShowGroupPanel = false;
            // 
            // btn_exit
            // 
            this.btn_exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_exit.Authority = false;
            this.btn_exit.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btn_exit.Image = ((System.Drawing.Image)(resources.GetObject("btn_exit.Image")));
            this.btn_exit.Location = new System.Drawing.Point(114, 9);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(80, 24);
            this.btn_exit.SRAuthCrud = SilkRoad.UserControls.SRButton.AuthCrudType.NONE;
            this.btn_exit.SRKindOf = SilkRoad.UserControls.SRButton.ButtonKindOfType.종료;
            this.btn_exit.SRWidthType = SilkRoad.UserControls.SRButton.WidthType.WIDTH_80;
            this.btn_exit.TabIndex = 1;
            this.btn_exit.Text = "종  료";
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
            this.srPanel1.Size = new System.Drawing.Size(961, 46);
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
            this.srTitle1.Size = new System.Drawing.Size(172, 39);
            this.srTitle1.SRTitleTxt = "사용자부서관리";
            this.srTitle1.TabIndex = 0;
            this.srTitle1.TabStop = false;
            // 
            // srPanel3
            // 
            this.srPanel3.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.srPanel3.Appearance.Options.UseBackColor = true;
            this.srPanel3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.srPanel3.Controls.Add(this.btn_dept_save);
            this.srPanel3.Controls.Add(this.btn_exit);
            this.srPanel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.srPanel3.Location = new System.Drawing.Point(754, 3);
            this.srPanel3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.srPanel3.LookAndFeel.UseDefaultLookAndFeel = false;
            this.srPanel3.Name = "srPanel3";
            this.srPanel3.Size = new System.Drawing.Size(204, 40);
            this.srPanel3.TabIndex = 1;
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
            this.gr_detail.Controls.Add(this.srPanel2);
            this.gr_detail.Controls.Add(this.srPanel4);
            this.gr_detail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gr_detail.Location = new System.Drawing.Point(0, 46);
            this.gr_detail.Name = "gr_detail";
            this.gr_detail.Size = new System.Drawing.Size(961, 588);
            this.gr_detail.TabIndex = 1;
            // 
            // grd_dept
            // 
            this.grd_dept.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd_dept.Location = new System.Drawing.Point(2, 2);
            this.grd_dept.MainView = this.grdv_dept;
            this.grd_dept.Name = "grd_dept";
            this.grd_dept.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.grd_sl_dept2,
            this.repositoryItemCheckEdit1});
            this.grd_dept.Size = new System.Drawing.Size(451, 560);
            this.grd_dept.TabIndex = 1;
            this.grd_dept.TabStop = false;
            this.grd_dept.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdv_dept});
            // 
            // grdv_dept
            // 
            this.grdv_dept.Appearance.Empty.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdv_dept.Appearance.Empty.Options.UseBackColor = true;
            this.grdv_dept.Appearance.HeaderPanel.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.grdv_dept.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.grdv_dept.Appearance.HeaderPanel.Options.UseFont = true;
            this.grdv_dept.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grdv_dept.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grdv_dept.Appearance.Row.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.grdv_dept.Appearance.Row.Options.UseFont = true;
            this.grdv_dept.Appearance.SelectedRow.BackColor = System.Drawing.Color.Transparent;
            this.grdv_dept.Appearance.SelectedRow.Options.UseBackColor = true;
            this.grdv_dept.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.grdv_dept.ColumnPanelRowHeight = 30;
            this.grdv_dept.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.gridColumn6});
            this.grdv_dept.GridControl = this.grd_dept;
            this.grdv_dept.Name = "grdv_dept";
            this.grdv_dept.NewItemRowText = "휴일을 입력하십시오.";
            this.grdv_dept.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.grdv_dept.OptionsNavigation.EnterMoveNextColumn = true;
            this.grdv_dept.OptionsView.ColumnAutoWidth = false;
            this.grdv_dept.OptionsView.ShowGroupPanel = false;
            this.grdv_dept.RowHeight = 30;
            this.grdv_dept.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.grdv_dept_MouseWheel);
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "부서";
            this.gridColumn5.FieldName = "NAME";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            this.gridColumn5.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            this.gridColumn5.Width = 159;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = " ";
            this.gridColumn6.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumn6.FieldName = "CHK";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn6.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn6.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 1;
            this.gridColumn6.Width = 77;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repositoryItemCheckEdit1.ValueChecked = "1";
            this.repositoryItemCheckEdit1.ValueUnchecked = "0";
            // 
            // grd_sl_dept2
            // 
            this.grd_sl_dept2.AutoHeight = false;
            this.grd_sl_dept2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.grd_sl_dept2.DisplayMember = "NAME";
            this.grd_sl_dept2.Name = "grd_sl_dept2";
            this.grd_sl_dept2.NullText = "[선택하세요.]";
            this.grd_sl_dept2.ValueMember = "CODE";
            this.grd_sl_dept2.View = this.gridView3;
            // 
            // gridView3
            // 
            this.gridView3.Appearance.HeaderPanel.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.gridView3.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView3.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView3.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView3.Appearance.Row.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.gridView3.Appearance.Row.Options.UseFont = true;
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // btn_dept_save
            // 
            this.btn_dept_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_dept_save.Authority = false;
            this.btn_dept_save.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btn_dept_save.Image = ((System.Drawing.Image)(resources.GetObject("btn_dept_save.Image")));
            this.btn_dept_save.Location = new System.Drawing.Point(33, 9);
            this.btn_dept_save.Name = "btn_dept_save";
            this.btn_dept_save.Size = new System.Drawing.Size(80, 24);
            this.btn_dept_save.SRAuthCrud = SilkRoad.UserControls.SRButton.AuthCrudType.U;
            this.btn_dept_save.SRKindOf = SilkRoad.UserControls.SRButton.ButtonKindOfType.수정;
            this.btn_dept_save.SRWidthType = SilkRoad.UserControls.SRButton.WidthType.WIDTH_80;
            this.btn_dept_save.TabIndex = 1;
            this.btn_dept_save.Text = "부서저장";
            this.btn_dept_save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_dept_save.UseVisualStyleBackColor = true;
            this.btn_dept_save.Click += new System.EventHandler(this.btn_dept_save_Click);
            // 
            // grd1
            // 
            this.grd1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd1.Location = new System.Drawing.Point(2, 2);
            this.grd1.MainView = this.grdv1;
            this.grd1.Name = "grd1";
            this.grd1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.grd_sl_dept,
            this.grd_chk1});
            this.grd1.Size = new System.Drawing.Size(498, 560);
            this.grd1.TabIndex = 0;
            this.grd1.TabStop = false;
            this.grd1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdv1});
            // 
            // grdv1
            // 
            this.grdv1.Appearance.Empty.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdv1.Appearance.Empty.Options.UseBackColor = true;
            this.grdv1.Appearance.HeaderPanel.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.grdv1.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.grdv1.Appearance.HeaderPanel.Options.UseFont = true;
            this.grdv1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grdv1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grdv1.Appearance.Row.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.grdv1.Appearance.Row.Options.UseFont = true;
            this.grdv1.Appearance.SelectedRow.BackColor = System.Drawing.Color.Transparent;
            this.grdv1.Appearance.SelectedRow.Options.UseBackColor = true;
            this.grdv1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.grdv1.ColumnPanelRowHeight = 30;
            this.grdv1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_id,
            this.col_idnm,
            this.col_dept,
            this.gridColumn1,
            this.gridColumn2});
            this.grdv1.GridControl = this.grd1;
            this.grdv1.Name = "grdv1";
            this.grdv1.NewItemRowText = "휴일을 입력하십시오.";
            this.grdv1.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.grdv1.OptionsNavigation.EnterMoveNextColumn = true;
            this.grdv1.OptionsView.ColumnAutoWidth = false;
            this.grdv1.OptionsView.ShowGroupPanel = false;
            this.grdv1.RowHeight = 30;
            this.grdv1.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.grdv1_RowClick);
            // 
            // col_id
            // 
            this.col_id.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.col_id.AppearanceCell.Options.UseBackColor = true;
            this.col_id.AppearanceCell.Options.UseTextOptions = true;
            this.col_id.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_id.Caption = "사용자ID";
            this.col_id.FieldName = "USERIDEN";
            this.col_id.Name = "col_id";
            this.col_id.OptionsColumn.AllowEdit = false;
            this.col_id.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.col_id.OptionsColumn.ReadOnly = true;
            this.col_id.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.col_id.Visible = true;
            this.col_id.VisibleIndex = 0;
            this.col_id.Width = 128;
            // 
            // col_dept
            // 
            this.col_dept.Caption = "부서";
            this.col_dept.ColumnEdit = this.grd_sl_dept;
            this.col_dept.FieldName = "USERDPCD";
            this.col_dept.Name = "col_dept";
            this.col_dept.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.col_dept.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.col_dept.Visible = true;
            this.col_dept.VisibleIndex = 2;
            this.col_dept.Width = 185;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "전체관리여부";
            this.gridColumn1.ColumnEdit = this.grd_chk1;
            this.gridColumn1.FieldName = "USERMSYN";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn1.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn1.Width = 99;
            // 
            // grd_chk1
            // 
            this.grd_chk1.AutoHeight = false;
            this.grd_chk1.Name = "grd_chk1";
            this.grd_chk1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.grd_chk1.ValueChecked = "1";
            this.grd_chk1.ValueUnchecked = "0";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "부서관리여부";
            this.gridColumn2.ColumnEdit = this.grd_chk1;
            this.gridColumn2.FieldName = "USERUPYN";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn2.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn2.Width = 96;
            // 
            // srPanel2
            // 
            this.srPanel2.Controls.Add(this.grd_dept);
            this.srPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.srPanel2.Location = new System.Drawing.Point(504, 22);
            this.srPanel2.Name = "srPanel2";
            this.srPanel2.Size = new System.Drawing.Size(455, 564);
            this.srPanel2.TabIndex = 2;
            // 
            // srPanel4
            // 
            this.srPanel4.Controls.Add(this.grd1);
            this.srPanel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.srPanel4.Location = new System.Drawing.Point(2, 22);
            this.srPanel4.Name = "srPanel4";
            this.srPanel4.Size = new System.Drawing.Size(502, 564);
            this.srPanel4.TabIndex = 3;
            // 
            // duty1005
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 634);
            this.Controls.Add(this.gr_detail);
            this.Controls.Add(this.srPanel1);
            this.Name = "duty1005";
            this.Text = "duty1005";
            this.Load += new System.EventHandler(this.duty1005_Load);
            this.Shown += new System.EventHandler(this.duty1005_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.duty1090_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grd_sl_dept)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sRgridLookup21View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.srPanel1)).EndInit();
            this.srPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.srPanel3)).EndInit();
            this.srPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gr_detail)).EndInit();
            this.gr_detail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grd_dept)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdv_dept)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_sl_dept2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdv1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_chk1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.srPanel2)).EndInit();
            this.srPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.srPanel4)).EndInit();
            this.srPanel4.ResumeLayout(false);
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
        private SilkRoad.UserControls.SRGroupBox gr_detail;
        private DevExpress.XtraGrid.GridControl grd1;
        private DevExpress.XtraGrid.Views.Grid.GridView grdv1;
        private DevExpress.XtraGrid.Columns.GridColumn col_dept;
		private DevExpress.XtraGrid.Columns.GridColumn col_idnm;
		private SilkRoad.UserControls.SRgridLookup2 grd_sl_dept;
		private DevExpress.XtraGrid.Views.Grid.GridView sRgridLookup21View;
		private DevExpress.XtraGrid.Columns.GridColumn col_id;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit grd_chk1;
		private DevExpress.XtraGrid.GridControl grd_dept;
		private DevExpress.XtraGrid.Views.Grid.GridView grdv_dept;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
		private SilkRoad.UserControls.SRgridLookup2 grd_sl_dept2;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
		private SilkRoad.UserControls.SRButton btn_dept_save;
        private SilkRoad.UserControls.SRPanel srPanel2;
        private SilkRoad.UserControls.SRPanel srPanel4;
    }
}


namespace DUTY1000
{
    partial class duty9010
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(duty9010));
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleExpression formatConditionRuleExpression1 = new DevExpress.XtraEditors.FormatConditionRuleExpression();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btn_exit = new SilkRoad.UserControls.SRButton();
            this.srPanel1 = new SilkRoad.UserControls.SRPanel();
            this.srTitle1 = new SilkRoad.UserControls.SRTitle();
            this.srPanel3 = new SilkRoad.UserControls.SRPanel();
            this.btn_excel = new SilkRoad.UserControls.SRButton();
            this.btn_search = new SilkRoad.UserControls.SRButton();
            this.btn_canc = new SilkRoad.UserControls.SRButton();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.dat_yymm = new SilkRoad.UserControls.SRDate();
            this.srLabel16 = new SilkRoad.UserControls.SRLabel();
            this.grd = new DevExpress.XtraGrid.GridControl();
            this.grdv = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_r_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.srPanel1)).BeginInit();
            this.srPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.srPanel3)).BeginInit();
            this.srPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dat_yymm.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dat_yymm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "근로시간(월)";
            this.gridColumn4.DisplayFormat.FormatString = "{0:#,###.##}";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "GJTMTIME";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.OptionsColumn.TabStop = false;
            this.gridColumn4.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            this.gridColumn4.Width = 100;
            // 
            // btn_exit
            // 
            this.btn_exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_exit.Authority = false;
            this.btn_exit.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btn_exit.Image = ((System.Drawing.Image)(resources.GetObject("btn_exit.Image")));
            this.btn_exit.Location = new System.Drawing.Point(257, 9);
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
            this.srPanel1.Size = new System.Drawing.Size(1132, 46);
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
            this.srTitle1.Size = new System.Drawing.Size(168, 39);
            this.srTitle1.SRTitleTxt = "사원별시급조회";
            this.srTitle1.TabIndex = 0;
            this.srTitle1.TabStop = false;
            // 
            // srPanel3
            // 
            this.srPanel3.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.srPanel3.Appearance.Options.UseBackColor = true;
            this.srPanel3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.srPanel3.Controls.Add(this.btn_exit);
            this.srPanel3.Controls.Add(this.btn_excel);
            this.srPanel3.Controls.Add(this.btn_search);
            this.srPanel3.Controls.Add(this.btn_canc);
            this.srPanel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.srPanel3.Location = new System.Drawing.Point(782, 3);
            this.srPanel3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.srPanel3.LookAndFeel.UseDefaultLookAndFeel = false;
            this.srPanel3.Name = "srPanel3";
            this.srPanel3.Size = new System.Drawing.Size(347, 40);
            this.srPanel3.TabIndex = 1;
            // 
            // btn_excel
            // 
            this.btn_excel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_excel.Authority = false;
            this.btn_excel.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btn_excel.Image = ((System.Drawing.Image)(resources.GetObject("btn_excel.Image")));
            this.btn_excel.Location = new System.Drawing.Point(176, 9);
            this.btn_excel.Name = "btn_excel";
            this.btn_excel.Size = new System.Drawing.Size(80, 24);
            this.btn_excel.SRAuthCrud = SilkRoad.UserControls.SRButton.AuthCrudType.R;
            this.btn_excel.SRKindOf = SilkRoad.UserControls.SRButton.ButtonKindOfType.엑셀변환;
            this.btn_excel.SRWidthType = SilkRoad.UserControls.SRButton.WidthType.WIDTH_80;
            this.btn_excel.TabIndex = 509;
            this.btn_excel.Text = "엑셀변환";
            this.btn_excel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_excel.UseVisualStyleBackColor = true;
            this.btn_excel.Click += new System.EventHandler(this.btn_excel_Click);
            // 
            // btn_search
            // 
            this.btn_search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_search.Authority = false;
            this.btn_search.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btn_search.Image = ((System.Drawing.Image)(resources.GetObject("btn_search.Image")));
            this.btn_search.Location = new System.Drawing.Point(14, 9);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(80, 24);
            this.btn_search.SRAuthCrud = SilkRoad.UserControls.SRButton.AuthCrudType.R;
            this.btn_search.SRKindOf = SilkRoad.UserControls.SRButton.ButtonKindOfType.조회;
            this.btn_search.SRWidthType = SilkRoad.UserControls.SRButton.WidthType.WIDTH_80;
            this.btn_search.TabIndex = 507;
            this.btn_search.Text = "조  회";
            this.btn_search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // btn_canc
            // 
            this.btn_canc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_canc.Authority = false;
            this.btn_canc.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btn_canc.Image = ((System.Drawing.Image)(resources.GetObject("btn_canc.Image")));
            this.btn_canc.Location = new System.Drawing.Point(95, 9);
            this.btn_canc.Name = "btn_canc";
            this.btn_canc.Size = new System.Drawing.Size(80, 24);
            this.btn_canc.SRAuthCrud = SilkRoad.UserControls.SRButton.AuthCrudType.NONE;
            this.btn_canc.SRKindOf = SilkRoad.UserControls.SRButton.ButtonKindOfType.취소;
            this.btn_canc.SRWidthType = SilkRoad.UserControls.SRButton.WidthType.WIDTH_80;
            this.btn_canc.TabIndex = 505;
            this.btn_canc.Text = "취  소";
            this.btn_canc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_canc.UseVisualStyleBackColor = true;
            this.btn_canc.Click += new System.EventHandler(this.btn_canc_Click);
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
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.dat_yymm);
            this.panelControl3.Controls.Add(this.srLabel16);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 46);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1132, 46);
            this.panelControl3.TabIndex = 452;
            // 
            // dat_yymm
            // 
            this.dat_yymm.EditValue = "";
            this.dat_yymm.EnterMoveNextControl = true;
            this.dat_yymm.Location = new System.Drawing.Point(75, 12);
            this.dat_yymm.Name = "dat_yymm";
            this.dat_yymm.Properties.AllowMouseWheel = false;
            this.dat_yymm.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.dat_yymm.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dat_yymm.Properties.Appearance.Options.UseFont = true;
            this.dat_yymm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dat_yymm.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dat_yymm.Properties.DisplayFormat.FormatString = "y";
            this.dat_yymm.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dat_yymm.Properties.EditFormat.FormatString = "y";
            this.dat_yymm.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dat_yymm.Properties.Mask.EditMask = "y";
            this.dat_yymm.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dat_yymm.Properties.NullDate = new System.DateTime(((long)(0)));
            this.dat_yymm.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dat_yymm.Properties.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.YearView;
            this.dat_yymm.Properties.VistaCalendarViewStyle = ((DevExpress.XtraEditors.VistaCalendarViewStyle)((DevExpress.XtraEditors.VistaCalendarViewStyle.YearView | DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView)));
            this.dat_yymm.Size = new System.Drawing.Size(112, 22);
            this.dat_yymm.TabIndex = 502;
            // 
            // srLabel16
            // 
            this.srLabel16.BackColor = System.Drawing.Color.LightCyan;
            this.srLabel16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.srLabel16.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.srLabel16.ForeColor = System.Drawing.Color.Black;
            this.srLabel16.Location = new System.Drawing.Point(10, 12);
            this.srLabel16.Name = "srLabel16";
            this.srLabel16.Size = new System.Drawing.Size(65, 22);
            this.srLabel16.SRBulletColor = System.Drawing.Color.Empty;
            this.srLabel16.TabIndex = 501;
            this.srLabel16.Text = "기준년월";
            this.srLabel16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grd
            // 
            this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd.Location = new System.Drawing.Point(0, 92);
            this.grd.MainView = this.grdv;
            this.grd.Name = "grd";
            this.grd.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit2});
            this.grd.Size = new System.Drawing.Size(1132, 615);
            this.grd.TabIndex = 453;
            this.grd.TabStop = false;
            this.grd.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdv});
            // 
            // grdv
            // 
            this.grdv.Appearance.FooterPanel.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.grdv.Appearance.FooterPanel.Options.UseFont = true;
            this.grdv.Appearance.GroupFooter.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.grdv.Appearance.GroupFooter.Options.UseFont = true;
            this.grdv.Appearance.GroupPanel.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.grdv.Appearance.GroupPanel.Options.UseFont = true;
            this.grdv.Appearance.GroupRow.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.grdv.Appearance.GroupRow.Options.UseFont = true;
            this.grdv.Appearance.HeaderPanel.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.grdv.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.grdv.Appearance.HeaderPanel.Options.UseFont = true;
            this.grdv.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grdv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grdv.Appearance.Row.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.grdv.Appearance.Row.Options.UseFont = true;
            this.grdv.Appearance.SelectedRow.BackColor = System.Drawing.Color.Transparent;
            this.grdv.Appearance.SelectedRow.Options.UseBackColor = true;
            this.grdv.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.grdv.ColumnPanelRowHeight = 30;
            this.grdv.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn10,
            this.col_r_name,
            this.gridColumn1,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn3});
            gridFormatRule1.ApplyToRow = true;
            gridFormatRule1.Column = this.gridColumn4;
            gridFormatRule1.Name = "Format0";
            formatConditionRuleExpression1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            formatConditionRuleExpression1.Appearance.Options.UseBackColor = true;
            formatConditionRuleExpression1.Expression = "[GUBN] <> \'1\'";
            gridFormatRule1.Rule = formatConditionRuleExpression1;
            this.grdv.FormatRules.Add(gridFormatRule1);
            this.grdv.GridControl = this.grd;
            this.grdv.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "SABN_NM", this.col_r_name, "{0:#,##0} 명")});
            this.grdv.Name = "grdv";
            this.grdv.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.grdv.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.grdv.OptionsFind.AlwaysVisible = true;
            this.grdv.OptionsNavigation.EnterMoveNextColumn = true;
            this.grdv.OptionsSelection.MultiSelect = true;
            this.grdv.OptionsView.ColumnAutoWidth = false;
            this.grdv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.grdv.OptionsView.ShowFooter = true;
            this.grdv.OptionsView.ShowGroupPanel = false;
            this.grdv.RowHeight = 30;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "부서";
            this.gridColumn2.FieldName = "DEPT_NM";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.OptionsColumn.TabStop = false;
            this.gridColumn2.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 143;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "사번";
            this.gridColumn10.FieldName = "SABN";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn10.OptionsColumn.ReadOnly = true;
            this.gridColumn10.OptionsColumn.TabStop = false;
            this.gridColumn10.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 1;
            this.gridColumn10.Width = 90;
            // 
            // col_r_name
            // 
            this.col_r_name.AppearanceCell.Options.UseTextOptions = true;
            this.col_r_name.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_r_name.Caption = "성명";
            this.col_r_name.FieldName = "SABN_NM";
            this.col_r_name.Name = "col_r_name";
            this.col_r_name.OptionsColumn.AllowEdit = false;
            this.col_r_name.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.col_r_name.OptionsColumn.ReadOnly = true;
            this.col_r_name.OptionsColumn.TabStop = false;
            this.col_r_name.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.col_r_name.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "SABN_NM", "{0:#,##0} 명")});
            this.col_r_name.Visible = true;
            this.col_r_name.VisibleIndex = 2;
            this.col_r_name.Width = 83;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "연봉";
            this.gridColumn1.DisplayFormat.FormatString = "{0:#,###}";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn1.FieldName = "GJSMSLAM";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 3;
            this.gridColumn1.Width = 131;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "시급";
            this.gridColumn3.DisplayFormat.FormatString = "{0:#,###}";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn3.FieldName = "T_AMT";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.ReadOnly = true;
            this.gridColumn3.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 6;
            this.gridColumn3.Width = 88;
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AllowMouseWheel = false;
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Mask.EditMask = "n0";
            this.repositoryItemTextEdit2.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit2.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "근로시간(일)";
            this.gridColumn5.DisplayFormat.FormatString = "{0:#,###.##}";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "WT_DAY";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 93;
            // 
            // duty9010
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 707);
            this.Controls.Add(this.grd);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.srPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "duty9010";
            this.Text = "duty9010";
            this.Load += new System.EventHandler(this.duty9010_Load);
            this.Shown += new System.EventHandler(this.duty9010_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.duty9010_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.srPanel1)).EndInit();
            this.srPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.srPanel3)).EndInit();
            this.srPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dat_yymm.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dat_yymm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
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
		private DevExpress.XtraEditors.PanelControl panelControl3;
		private SilkRoad.UserControls.SRDate dat_yymm;
		private SilkRoad.UserControls.SRLabel srLabel16;
		private SilkRoad.UserControls.SRButton btn_search;
		private SilkRoad.UserControls.SRButton btn_excel;
		private SilkRoad.UserControls.SRButton btn_canc;
		private DevExpress.XtraGrid.GridControl grd;
		private DevExpress.XtraGrid.Views.Grid.GridView grdv;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
		private DevExpress.XtraGrid.Columns.GridColumn col_r_name;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
    }
}


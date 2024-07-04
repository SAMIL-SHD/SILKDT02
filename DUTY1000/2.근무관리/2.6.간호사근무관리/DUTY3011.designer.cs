namespace DUTY1000
{
    partial class duty3011
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(duty3011));
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue2 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            this.col_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btn_exit = new SilkRoad.UserControls.SRButton();
            this.srPanel1 = new SilkRoad.UserControls.SRPanel();
            this.srTitle1 = new SilkRoad.UserControls.SRTitle();
            this.srPanel3 = new SilkRoad.UserControls.SRPanel();
            this.btn_proc = new SilkRoad.UserControls.SRButton();
            this.btn_save = new SilkRoad.UserControls.SRButton();
            this.btn_clear = new SilkRoad.UserControls.SRButton();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gr_detail = new SilkRoad.UserControls.SRGroupBox();
            this.grd1 = new DevExpress.XtraGrid.GridControl();
            this.grdv1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_chk = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.srPanel1)).BeginInit();
            this.srPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.srPanel3)).BeginInit();
            this.srPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gr_detail)).BeginInit();
            this.gr_detail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdv1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // col_name
            // 
            this.col_name.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.col_name.AppearanceCell.Options.UseBackColor = true;
            this.col_name.AppearanceCell.Options.UseTextOptions = true;
            this.col_name.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_name.Caption = "부서명칭";
            this.col_name.FieldName = "DEPT_NM";
            this.col_name.Name = "col_name";
            this.col_name.OptionsColumn.AllowEdit = false;
            this.col_name.OptionsColumn.AllowFocus = false;
            this.col_name.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.col_name.OptionsColumn.AllowMove = false;
            this.col_name.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.col_name.OptionsColumn.FixedWidth = true;
            this.col_name.OptionsColumn.ReadOnly = true;
            this.col_name.Visible = true;
            this.col_name.VisibleIndex = 1;
            this.col_name.Width = 164;
            // 
            // btn_exit
            // 
            this.btn_exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_exit.Authority = false;
            this.btn_exit.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btn_exit.Image = ((System.Drawing.Image)(resources.GetObject("btn_exit.Image")));
            this.btn_exit.Location = new System.Drawing.Point(195, 9);
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
            this.srPanel1.Size = new System.Drawing.Size(464, 46);
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
            this.srTitle1.Size = new System.Drawing.Size(155, 39);
            this.srTitle1.SRTitleTxt = "간호사부서설정";
            this.srTitle1.TabIndex = 26;
            this.srTitle1.TabStop = false;
            // 
            // srPanel3
            // 
            this.srPanel3.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.srPanel3.Appearance.Options.UseBackColor = true;
            this.srPanel3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.srPanel3.Controls.Add(this.btn_proc);
            this.srPanel3.Controls.Add(this.btn_save);
            this.srPanel3.Controls.Add(this.btn_exit);
            this.srPanel3.Controls.Add(this.btn_clear);
            this.srPanel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.srPanel3.Location = new System.Drawing.Point(199, 3);
            this.srPanel3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.srPanel3.LookAndFeel.UseDefaultLookAndFeel = false;
            this.srPanel3.Name = "srPanel3";
            this.srPanel3.Size = new System.Drawing.Size(262, 40);
            this.srPanel3.TabIndex = 0;
            // 
            // btn_proc
            // 
            this.btn_proc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_proc.Authority = false;
            this.btn_proc.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btn_proc.Image = ((System.Drawing.Image)(resources.GetObject("btn_proc.Image")));
            this.btn_proc.Location = new System.Drawing.Point(9, 9);
            this.btn_proc.Name = "btn_proc";
            this.btn_proc.Size = new System.Drawing.Size(60, 24);
            this.btn_proc.SRAuthCrud = SilkRoad.UserControls.SRButton.AuthCrudType.R;
            this.btn_proc.SRKindOf = SilkRoad.UserControls.SRButton.ButtonKindOfType.처리;
            this.btn_proc.SRWidthType = SilkRoad.UserControls.SRButton.WidthType.WIDTH_60;
            this.btn_proc.TabIndex = 1;
            this.btn_proc.Text = "처리";
            this.btn_proc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_proc.UseVisualStyleBackColor = true;
            this.btn_proc.Click += new System.EventHandler(this.btn_proc_Click);
            // 
            // btn_save
            // 
            this.btn_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_save.Authority = false;
            this.btn_save.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btn_save.Image = ((System.Drawing.Image)(resources.GetObject("btn_save.Image")));
            this.btn_save.Location = new System.Drawing.Point(71, 9);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(60, 24);
            this.btn_save.SRAuthCrud = SilkRoad.UserControls.SRButton.AuthCrudType.A;
            this.btn_save.SRKindOf = SilkRoad.UserControls.SRButton.ButtonKindOfType.저장;
            this.btn_save.SRWidthType = SilkRoad.UserControls.SRButton.WidthType.WIDTH_60;
            this.btn_save.TabIndex = 2;
            this.btn_save.Text = "저장";
            this.btn_save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_clear.Authority = false;
            this.btn_clear.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btn_clear.Image = ((System.Drawing.Image)(resources.GetObject("btn_clear.Image")));
            this.btn_clear.Location = new System.Drawing.Point(133, 9);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(60, 24);
            this.btn_clear.SRAuthCrud = SilkRoad.UserControls.SRButton.AuthCrudType.NONE;
            this.btn_clear.SRKindOf = SilkRoad.UserControls.SRButton.ButtonKindOfType.취소;
            this.btn_clear.SRWidthType = SilkRoad.UserControls.SRButton.WidthType.WIDTH_60;
            this.btn_clear.TabIndex = 4;
            this.btn_clear.Text = "취소";
            this.btn_clear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
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
            this.gr_detail.Controls.Add(this.grd1);
            this.gr_detail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gr_detail.Location = new System.Drawing.Point(0, 46);
            this.gr_detail.Name = "gr_detail";
            this.gr_detail.Size = new System.Drawing.Size(464, 391);
            this.gr_detail.TabIndex = 1;
            this.gr_detail.Text = "등록";
            // 
            // grd1
            // 
            this.grd1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd1.Location = new System.Drawing.Point(2, 22);
            this.grd1.MainView = this.grdv1;
            this.grd1.Name = "grd1";
            this.grd1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2,
            this.repositoryItemTextEdit3,
            this.repositoryItemCheckEdit1});
            this.grd1.Size = new System.Drawing.Size(460, 367);
            this.grd1.TabIndex = 2;
            this.grd1.TabStop = false;
            this.grd1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdv1,
            this.gridView2});
            // 
            // grdv1
            // 
            this.grdv1.Appearance.Empty.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdv1.Appearance.Empty.Options.UseBackColor = true;
            this.grdv1.Appearance.FooterPanel.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.grdv1.Appearance.FooterPanel.Options.UseFont = true;
            this.grdv1.Appearance.HeaderPanel.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.grdv1.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.grdv1.Appearance.HeaderPanel.Options.UseFont = true;
            this.grdv1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grdv1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grdv1.Appearance.Row.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.grdv1.Appearance.Row.Options.UseFont = true;
            this.grdv1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.grdv1.ColumnPanelRowHeight = 25;
            this.grdv1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_code,
            this.col_name,
            this.col_chk});
            gridFormatRule1.Column = this.col_name;
            gridFormatRule1.Name = "Format0";
            formatConditionRuleValue1.Appearance.ForeColor = System.Drawing.Color.Red;
            formatConditionRuleValue1.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Expression;
            formatConditionRuleValue1.Expression = "[DDNAME] = \'일\'";
            gridFormatRule1.Rule = formatConditionRuleValue1;
            gridFormatRule2.Column = this.col_name;
            gridFormatRule2.Name = "Format1";
            formatConditionRuleValue2.Appearance.ForeColor = System.Drawing.Color.Blue;
            formatConditionRuleValue2.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Expression;
            formatConditionRuleValue2.Expression = "[DDNAME] = \'토\'";
            gridFormatRule2.Rule = formatConditionRuleValue2;
            this.grdv1.FormatRules.Add(gridFormatRule1);
            this.grdv1.FormatRules.Add(gridFormatRule2);
            this.grdv1.GridControl = this.grd1;
            this.grdv1.Name = "grdv1";
            this.grdv1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.grdv1.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.grdv1.OptionsEditForm.ActionOnModifiedRowChange = DevExpress.XtraGrid.Views.Grid.EditFormModifiedAction.Save;
            this.grdv1.OptionsNavigation.EnterMoveNextColumn = true;
            this.grdv1.OptionsSelection.MultiSelect = true;
            this.grdv1.OptionsView.ColumnAutoWidth = false;
            this.grdv1.OptionsView.ShowGroupPanel = false;
            this.grdv1.RowHeight = 30;
            // 
            // col_code
            // 
            this.col_code.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.col_code.AppearanceCell.Options.UseBackColor = true;
            this.col_code.AppearanceCell.Options.UseTextOptions = true;
            this.col_code.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_code.Caption = "부서코드";
            this.col_code.FieldName = "DEPRCODE";
            this.col_code.Name = "col_code";
            this.col_code.OptionsColumn.AllowEdit = false;
            this.col_code.OptionsColumn.AllowFocus = false;
            this.col_code.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.col_code.OptionsColumn.AllowMove = false;
            this.col_code.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.col_code.OptionsColumn.FixedWidth = true;
            this.col_code.OptionsColumn.ReadOnly = true;
            this.col_code.Visible = true;
            this.col_code.VisibleIndex = 0;
            this.col_code.Width = 63;
            // 
            // col_chk
            // 
            this.col_chk.Caption = "선택";
            this.col_chk.ColumnEdit = this.repositoryItemCheckEdit1;
            this.col_chk.FieldName = "CHK";
            this.col_chk.Name = "col_chk";
            this.col_chk.Visible = true;
            this.col_chk.VisibleIndex = 2;
            this.col_chk.Width = 44;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repositoryItemCheckEdit1.ValueChecked = "1";
            this.repositoryItemCheckEdit1.ValueUnchecked = "0";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Mask.EditMask = "\\d\\d\\d\\d-\\d\\d-\\d\\d";
            this.repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.repositoryItemTextEdit1.Mask.SaveLiteral = false;
            this.repositoryItemTextEdit1.Mask.ShowPlaceHolders = false;
            this.repositoryItemTextEdit1.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Mask.EditMask = "(0\\d|1\\d|2[0-3])\\:[0-5]\\d";
            this.repositoryItemTextEdit2.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.repositoryItemTextEdit2.Mask.SaveLiteral = false;
            this.repositoryItemTextEdit2.Mask.ShowPlaceHolders = false;
            this.repositoryItemTextEdit2.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // repositoryItemTextEdit3
            // 
            this.repositoryItemTextEdit3.AutoHeight = false;
            this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.grd1;
            this.gridView2.Name = "gridView2";
            // 
            // duty3011
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 437);
            this.Controls.Add(this.gr_detail);
            this.Controls.Add(this.srPanel1);
            this.Name = "duty3011";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "duty3011";
            this.Load += new System.EventHandler(this.duty3011_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.duty3011_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.srPanel1)).EndInit();
            this.srPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.srPanel3)).EndInit();
            this.srPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gr_detail)).EndInit();
            this.gr_detail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grd1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdv1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
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
        private SilkRoad.UserControls.SRButton btn_clear;
        private SilkRoad.UserControls.SRButton btn_proc;
        private SilkRoad.UserControls.SRGroupBox gr_detail;
        private DevExpress.XtraGrid.GridControl grd1;
        private DevExpress.XtraGrid.Views.Grid.GridView grdv1;
        private DevExpress.XtraGrid.Columns.GridColumn col_code;
        private DevExpress.XtraGrid.Columns.GridColumn col_name;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit3;
        private DevExpress.XtraGrid.Columns.GridColumn col_chk;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
    }
}


namespace DUTY1000
{
    partial class sendemail
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
			DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
			DevExpress.XtraEditors.FormatConditionRuleExpression formatConditionRuleExpression1 = new DevExpress.XtraEditors.FormatConditionRuleExpression();
			DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
			DevExpress.XtraEditors.FormatConditionRuleExpression formatConditionRuleExpression2 = new DevExpress.XtraEditors.FormatConditionRuleExpression();
			DevExpress.XtraGrid.GridFormatRule gridFormatRule3 = new DevExpress.XtraGrid.GridFormatRule();
			DevExpress.XtraEditors.FormatConditionRuleExpression formatConditionRuleExpression3 = new DevExpress.XtraEditors.FormatConditionRuleExpression();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(sendemail));
			this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label12 = new System.Windows.Forms.Label();
			this.txt_Body = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.btn_setting = new SilkRoad.UserControls.SRButton();
			this.label1 = new System.Windows.Forms.Label();
			this.txt_sendEmail = new System.Windows.Forms.TextBox();
			this.txt_Subject = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txt_cc = new System.Windows.Forms.TextBox();
			this.srTabControl1 = new SilkRoad.UserControls.SRTabControl();
			this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
			this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
			this.grd1 = new DevExpress.XtraGrid.GridControl();
			this.grdv1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.panel5 = new System.Windows.Forms.Panel();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.btn_canc = new SilkRoad.UserControls.SRButton();
			this.btn_exit = new SilkRoad.UserControls.SRButton();
			this.btn_send = new SilkRoad.UserControls.SRButton();
			this.txt_cnt_chk = new SilkRoad.UserControls.SRTextEdit();
			this.srLabel16 = new SilkRoad.UserControls.SRLabel();
			this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
			this.panel3 = new System.Windows.Forms.Panel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.txt_fromemailaddress = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.txt_smptserverport = new System.Windows.Forms.TextBox();
			this.txt_smptpasswd = new System.Windows.Forms.TextBox();
			this.txt_sendmailaddressname = new System.Windows.Forms.TextBox();
			this.txt_smptserver = new System.Windows.Forms.TextBox();
			this.txt_serverid = new System.Windows.Forms.TextBox();
			this.srButton1 = new SilkRoad.UserControls.SRButton();
			this.btn_cancel = new SilkRoad.UserControls.SRButton();
			this.btn_save = new SilkRoad.UserControls.SRButton();
			this.label4 = new System.Windows.Forms.Label();
			this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
			this.srTabControl2 = new SilkRoad.UserControls.SRTabControl();
			this.tab_naver = new DevExpress.XtraTab.XtraTabPage();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.tab_daum = new DevExpress.XtraTab.XtraTabPage();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label11 = new System.Windows.Forms.Label();
			this.btn_close3 = new SilkRoad.UserControls.SRButton();
			this.bgWorker = new System.ComponentModel.BackgroundWorker();
			this.lb_step = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.srTabControl1)).BeginInit();
			this.srTabControl1.SuspendLayout();
			this.xtraTabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grd1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grdv1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
			this.panel5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txt_cnt_chk.Properties)).BeginInit();
			this.xtraTabPage2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.xtraTabPage3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.srTabControl2)).BeginInit();
			this.srTabControl2.SuspendLayout();
			this.tab_naver.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.tab_daum.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// repositoryItemTextEdit2
			// 
			this.repositoryItemTextEdit2.AutoHeight = false;
			this.repositoryItemTextEdit2.Mask.EditMask = "\\d\\d\\d\\d-\\d\\d-\\d\\d";
			this.repositoryItemTextEdit2.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
			this.repositoryItemTextEdit2.Mask.UseMaskAsDisplayFormat = true;
			this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
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
			// panel1
			// 
			this.panel1.Controls.Add(this.lb_step);
			this.panel1.Controls.Add(this.label12);
			this.panel1.Controls.Add(this.txt_Body);
			this.panel1.Controls.Add(this.label14);
			this.panel1.Controls.Add(this.btn_setting);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.txt_sendEmail);
			this.panel1.Controls.Add(this.txt_Subject);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.txt_cc);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(878, 168);
			this.panel1.TabIndex = 8;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(92, 80);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(49, 12);
			this.label12.TabIndex = 24;
			this.label12.Text = "본     문";
			// 
			// txt_Body
			// 
			this.txt_Body.Location = new System.Drawing.Point(145, 76);
			this.txt_Body.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txt_Body.Multiline = true;
			this.txt_Body.Name = "txt_Body";
			this.txt_Body.Size = new System.Drawing.Size(491, 53);
			this.txt_Body.TabIndex = 25;
			this.txt_Body.Text = "연차휴가 사용촉구 확인서를 첨부파일로 송부하오니\r\n확인하시기 바랍니다.";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(699, 88);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(167, 12);
			this.label14.TabIndex = 23;
			this.label14.Text = "( ; 으로 여러이메일 참조가능)";
			this.label14.Visible = false;
			// 
			// btn_setting
			// 
			this.btn_setting.Authority = false;
			this.btn_setting.Font = new System.Drawing.Font("굴림체", 9F);
			this.btn_setting.Image = global::DUTY1000.Properties.Resources.settings_work_tool;
			this.btn_setting.Location = new System.Drawing.Point(642, 17);
			this.btn_setting.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btn_setting.Name = "btn_setting";
			this.btn_setting.Size = new System.Drawing.Size(28, 30);
			this.btn_setting.SRAuthCrud = SilkRoad.UserControls.SRButton.AuthCrudType.A;
			this.btn_setting.SRWidthType = SilkRoad.UserControls.SRButton.WidthType.Custom;
			this.btn_setting.TabIndex = 17;
			this.btn_setting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btn_setting.UseVisualStyleBackColor = true;
			this.btn_setting.Click += new System.EventHandler(this.btn_setting_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(77, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "보내는사람";
			// 
			// txt_sendEmail
			// 
			this.txt_sendEmail.Enabled = false;
			this.txt_sendEmail.Location = new System.Drawing.Point(145, 22);
			this.txt_sendEmail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txt_sendEmail.Name = "txt_sendEmail";
			this.txt_sendEmail.ReadOnly = true;
			this.txt_sendEmail.Size = new System.Drawing.Size(491, 21);
			this.txt_sendEmail.TabIndex = 1;
			// 
			// txt_Subject
			// 
			this.txt_Subject.Location = new System.Drawing.Point(145, 49);
			this.txt_Subject.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txt_Subject.Name = "txt_Subject";
			this.txt_Subject.Size = new System.Drawing.Size(491, 21);
			this.txt_Subject.TabIndex = 5;
			this.txt_Subject.Text = "연차휴가사용촉구 안내";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(764, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(49, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "참     조";
			this.label2.Visible = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(92, 54);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(49, 12);
			this.label3.TabIndex = 4;
			this.label3.Text = "제     목";
			// 
			// txt_cc
			// 
			this.txt_cc.Location = new System.Drawing.Point(757, 32);
			this.txt_cc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txt_cc.Multiline = true;
			this.txt_cc.Name = "txt_cc";
			this.txt_cc.Size = new System.Drawing.Size(70, 36);
			this.txt_cc.TabIndex = 3;
			this.txt_cc.Visible = false;
			// 
			// srTabControl1
			// 
			this.srTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.srTabControl1.HeaderAutoFill = DevExpress.Utils.DefaultBoolean.True;
			this.srTabControl1.Location = new System.Drawing.Point(0, 168);
			this.srTabControl1.Name = "srTabControl1";
			this.srTabControl1.SelectedTabPage = this.xtraTabPage1;
			this.srTabControl1.Size = new System.Drawing.Size(878, 601);
			this.srTabControl1.TabIndex = 21;
			this.srTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3});
			// 
			// xtraTabPage1
			// 
			this.xtraTabPage1.Controls.Add(this.progressBarControl1);
			this.xtraTabPage1.Controls.Add(this.grd1);
			this.xtraTabPage1.Controls.Add(this.panel5);
			this.xtraTabPage1.Name = "xtraTabPage1";
			this.xtraTabPage1.Size = new System.Drawing.Size(872, 572);
			this.xtraTabPage1.Text = "xtraTabPage1";
			// 
			// progressBarControl1
			// 
			this.progressBarControl1.Location = new System.Drawing.Point(233, 202);
			this.progressBarControl1.Name = "progressBarControl1";
			this.progressBarControl1.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.progressBarControl1.Properties.ShowTitle = true;
			this.progressBarControl1.Properties.Step = 1;
			this.progressBarControl1.Size = new System.Drawing.Size(367, 33);
			this.progressBarControl1.TabIndex = 22;
			this.progressBarControl1.Visible = false;
			// 
			// grd1
			// 
			this.grd1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.grd1.Location = new System.Drawing.Point(0, 0);
			this.grd1.MainView = this.grdv1;
			this.grd1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.grd1.Name = "grd1";
			this.grd1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit2});
			this.grd1.Size = new System.Drawing.Size(872, 475);
			this.grd1.TabIndex = 15;
			this.grd1.TabStop = false;
			this.grd1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdv1});
			// 
			// grdv1
			// 
			this.grdv1.Appearance.Empty.BackColor = System.Drawing.Color.WhiteSmoke;
			this.grdv1.Appearance.Empty.Options.UseBackColor = true;
			this.grdv1.Appearance.FocusedRow.BackColor = System.Drawing.Color.Transparent;
			this.grdv1.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Transparent;
			this.grdv1.Appearance.FocusedRow.Options.UseBackColor = true;
			this.grdv1.Appearance.FocusedRow.Options.UseForeColor = true;
			this.grdv1.Appearance.FooterPanel.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.grdv1.Appearance.FooterPanel.Options.UseFont = true;
			this.grdv1.Appearance.GroupFooter.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.grdv1.Appearance.GroupFooter.Options.UseFont = true;
			this.grdv1.Appearance.GroupPanel.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.grdv1.Appearance.GroupPanel.Options.UseFont = true;
			this.grdv1.Appearance.GroupRow.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.grdv1.Appearance.GroupRow.Options.UseFont = true;
			this.grdv1.Appearance.HeaderPanel.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
			this.grdv1.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.grdv1.Appearance.HeaderPanel.Options.UseFont = true;
			this.grdv1.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.grdv1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.grdv1.Appearance.Row.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.grdv1.Appearance.Row.Options.UseFont = true;
			this.grdv1.Appearance.SelectedRow.BackColor = System.Drawing.Color.Transparent;
			this.grdv1.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Transparent;
			this.grdv1.Appearance.SelectedRow.Options.UseBackColor = true;
			this.grdv1.Appearance.SelectedRow.Options.UseForeColor = true;
			this.grdv1.ColumnPanelRowHeight = 30;
			this.grdv1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
			gridFormatRule1.Name = "Format0";
			formatConditionRuleExpression1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			formatConditionRuleExpression1.Appearance.Options.UseBackColor = true;
			formatConditionRuleExpression1.Expression = "[APPRCNG1] == \'\'";
			gridFormatRule1.Rule = formatConditionRuleExpression1;
			gridFormatRule2.Name = "Format1";
			formatConditionRuleExpression2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			formatConditionRuleExpression2.Appearance.Options.UseBackColor = true;
			formatConditionRuleExpression2.Expression = "[APPRCNG2] == \'\'";
			gridFormatRule2.Rule = formatConditionRuleExpression2;
			gridFormatRule3.Name = "Format2";
			formatConditionRuleExpression3.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			formatConditionRuleExpression3.Appearance.Options.UseBackColor = true;
			formatConditionRuleExpression3.Expression = "[APPRCNG3] == \'\'";
			gridFormatRule3.Rule = formatConditionRuleExpression3;
			this.grdv1.FormatRules.Add(gridFormatRule1);
			this.grdv1.FormatRules.Add(gridFormatRule2);
			this.grdv1.FormatRules.Add(gridFormatRule3);
			this.grdv1.GridControl = this.grd1;
			this.grdv1.GroupCount = 1;
			this.grdv1.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "SAWON_NM", this.gridColumn3, "{0:#,##0} 명")});
			this.grdv1.Name = "grdv1";
			this.grdv1.OptionsBehavior.AutoExpandAllGroups = true;
			this.grdv1.OptionsSelection.MultiSelect = true;
			this.grdv1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
			this.grdv1.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
			this.grdv1.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
			this.grdv1.OptionsView.ShowFooter = true;
			this.grdv1.OptionsView.ShowGroupedColumns = true;
			this.grdv1.OptionsView.ShowIndicator = false;
			this.grdv1.RowHeight = 20;
			this.grdv1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn1, DevExpress.Data.ColumnSortOrder.Ascending)});
			this.grdv1.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.grdv1_SelectionChanged);
			this.grdv1.DoubleClick += new System.EventHandler(this.grdv1_DoubleClick);
			// 
			// gridColumn1
			// 
			this.gridColumn1.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
			this.gridColumn1.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn1.Caption = "부서";
			this.gridColumn1.FieldName = "DEPT_NM";
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.OptionsColumn.AllowEdit = false;
			this.gridColumn1.OptionsColumn.ReadOnly = true;
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 1;
			this.gridColumn1.Width = 131;
			// 
			// gridColumn2
			// 
			this.gridColumn2.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
			this.gridColumn2.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn2.Caption = "사번";
			this.gridColumn2.FieldName = "SAWON_NO";
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.OptionsColumn.AllowEdit = false;
			this.gridColumn2.OptionsColumn.ReadOnly = true;
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 2;
			this.gridColumn2.Width = 223;
			// 
			// gridColumn3
			// 
			this.gridColumn3.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
			this.gridColumn3.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn3.Caption = "이름";
			this.gridColumn3.FieldName = "SAWON_NM";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.OptionsColumn.AllowEdit = false;
			this.gridColumn3.OptionsColumn.ReadOnly = true;
			this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "NAME", "{0:#,##0} 명")});
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 3;
			this.gridColumn3.Width = 239;
			// 
			// gridColumn4
			// 
			this.gridColumn4.Caption = "E-Mail";
			this.gridColumn4.FieldName = "GW_EMAIL";
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 4;
			this.gridColumn4.Width = 343;
			// 
			// repositoryItemCheckEdit2
			// 
			this.repositoryItemCheckEdit2.AutoHeight = false;
			this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
			this.repositoryItemCheckEdit2.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
			// 
			// panel5
			// 
			this.panel5.BackColor = System.Drawing.SystemColors.Control;
			this.panel5.Controls.Add(this.textBox1);
			this.panel5.Controls.Add(this.btn_canc);
			this.panel5.Controls.Add(this.btn_exit);
			this.panel5.Controls.Add(this.btn_send);
			this.panel5.Controls.Add(this.txt_cnt_chk);
			this.panel5.Controls.Add(this.srLabel16);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel5.Location = new System.Drawing.Point(0, 475);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(872, 97);
			this.panel5.TabIndex = 14;
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.textBox1.Location = new System.Drawing.Point(485, 39);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(379, 51);
			this.textBox1.TabIndex = 275;
			// 
			// btn_canc
			// 
			this.btn_canc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_canc.Authority = false;
			this.btn_canc.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.btn_canc.Image = ((System.Drawing.Image)(resources.GetObject("btn_canc.Image")));
			this.btn_canc.Location = new System.Drawing.Point(699, 9);
			this.btn_canc.Name = "btn_canc";
			this.btn_canc.Size = new System.Drawing.Size(80, 24);
			this.btn_canc.SRAuthCrud = SilkRoad.UserControls.SRButton.AuthCrudType.R;
			this.btn_canc.SRKindOf = SilkRoad.UserControls.SRButton.ButtonKindOfType.처리;
			this.btn_canc.SRWidthType = SilkRoad.UserControls.SRButton.WidthType.Custom;
			this.btn_canc.TabIndex = 272;
			this.btn_canc.Text = "전송취소";
			this.btn_canc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btn_canc.UseVisualStyleBackColor = true;
			this.btn_canc.Click += new System.EventHandler(this.btn_canc_Click);
			// 
			// btn_exit
			// 
			this.btn_exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_exit.Authority = false;
			this.btn_exit.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.btn_exit.Image = ((System.Drawing.Image)(resources.GetObject("btn_exit.Image")));
			this.btn_exit.Location = new System.Drawing.Point(780, 9);
			this.btn_exit.Name = "btn_exit";
			this.btn_exit.Size = new System.Drawing.Size(84, 24);
			this.btn_exit.SRAuthCrud = SilkRoad.UserControls.SRButton.AuthCrudType.NONE;
			this.btn_exit.SRKindOf = SilkRoad.UserControls.SRButton.ButtonKindOfType.종료;
			this.btn_exit.TabIndex = 270;
			this.btn_exit.Text = "종  료";
			this.btn_exit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btn_exit.UseVisualStyleBackColor = true;
			// 
			// btn_send
			// 
			this.btn_send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_send.Authority = false;
			this.btn_send.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.btn_send.Image = ((System.Drawing.Image)(resources.GetObject("btn_send.Image")));
			this.btn_send.Location = new System.Drawing.Point(618, 9);
			this.btn_send.Name = "btn_send";
			this.btn_send.Size = new System.Drawing.Size(80, 24);
			this.btn_send.SRAuthCrud = SilkRoad.UserControls.SRButton.AuthCrudType.R;
			this.btn_send.SRKindOf = SilkRoad.UserControls.SRButton.ButtonKindOfType.처리;
			this.btn_send.SRWidthType = SilkRoad.UserControls.SRButton.WidthType.Custom;
			this.btn_send.TabIndex = 271;
			this.btn_send.Text = "메일전송";
			this.btn_send.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btn_send.UseVisualStyleBackColor = true;
			this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
			// 
			// txt_cnt_chk
			// 
			this.txt_cnt_chk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txt_cnt_chk.Enabled = false;
			this.txt_cnt_chk.EnterMoveNextControl = false;
			this.txt_cnt_chk.Location = new System.Drawing.Point(539, 10);
			this.txt_cnt_chk.Name = "txt_cnt_chk";
			this.txt_cnt_chk.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.txt_cnt_chk.Properties.Appearance.Options.UseFont = true;
			this.txt_cnt_chk.Properties.Appearance.Options.UseTextOptions = true;
			this.txt_cnt_chk.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.txt_cnt_chk.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Red;
			this.txt_cnt_chk.Properties.AppearanceDisabled.Options.UseForeColor = true;
			this.txt_cnt_chk.Size = new System.Drawing.Size(76, 22);
			this.txt_cnt_chk.TabIndex = 269;
			// 
			// srLabel16
			// 
			this.srLabel16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.srLabel16.AutoSize = true;
			this.srLabel16.BackColor = System.Drawing.Color.Transparent;
			this.srLabel16.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.srLabel16.ForeColor = System.Drawing.Color.Red;
			this.srLabel16.Location = new System.Drawing.Point(482, 13);
			this.srLabel16.Name = "srLabel16";
			this.srLabel16.Size = new System.Drawing.Size(55, 15);
			this.srLabel16.TabIndex = 268;
			this.srLabel16.Text = "선택건수";
			// 
			// xtraTabPage2
			// 
			this.xtraTabPage2.Controls.Add(this.panel3);
			this.xtraTabPage2.Name = "xtraTabPage2";
			this.xtraTabPage2.Size = new System.Drawing.Size(872, 572);
			this.xtraTabPage2.Text = "xtraTabPage2";
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.SystemColors.Control;
			this.panel3.Controls.Add(this.tableLayoutPanel1);
			this.panel3.Controls.Add(this.srButton1);
			this.panel3.Controls.Add(this.btn_cancel);
			this.panel3.Controls.Add(this.btn_save);
			this.panel3.Controls.Add(this.label4);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(872, 572);
			this.panel3.TabIndex = 14;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.66256F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.33744F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label6, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label7, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.txt_fromemailaddress, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label9, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.label10, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label8, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.txt_smptserverport, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.txt_smptpasswd, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.txt_sendmailaddressname, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.txt_smptserver, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.txt_serverid, 1, 4);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(78, 66);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 6;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(649, 226);
			this.tableLayoutPanel1.TabIndex = 19;
			// 
			// label5
			// 
			this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label5.Font = new System.Drawing.Font("굴림", 9F);
			this.label5.Location = new System.Drawing.Point(3, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(193, 37);
			this.label5.TabIndex = 2;
			this.label5.Text = "① 보내는사람 이메일 주소";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label6
			// 
			this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label6.Font = new System.Drawing.Font("굴림", 9F);
			this.label6.Location = new System.Drawing.Point(3, 37);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(193, 37);
			this.label6.TabIndex = 4;
			this.label6.Text = "② 보내는사람 이름";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label7
			// 
			this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label7.Font = new System.Drawing.Font("굴림", 9F);
			this.label7.Location = new System.Drawing.Point(3, 74);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(193, 37);
			this.label7.TabIndex = 6;
			this.label7.Text = "③ SMTP 서버명";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txt_fromemailaddress
			// 
			this.txt_fromemailaddress.Location = new System.Drawing.Point(202, 4);
			this.txt_fromemailaddress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txt_fromemailaddress.Name = "txt_fromemailaddress";
			this.txt_fromemailaddress.Size = new System.Drawing.Size(444, 22);
			this.txt_fromemailaddress.TabIndex = 3;
			// 
			// label9
			// 
			this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label9.Font = new System.Drawing.Font("굴림", 9F);
			this.label9.Location = new System.Drawing.Point(3, 185);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(193, 41);
			this.label9.TabIndex = 10;
			this.label9.Text = "⑥ 비밀번호";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label10
			// 
			this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label10.Font = new System.Drawing.Font("굴림", 9F);
			this.label10.Location = new System.Drawing.Point(3, 111);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(193, 37);
			this.label10.TabIndex = 12;
			this.label10.Text = "④ SMTP포트";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label8
			// 
			this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label8.Font = new System.Drawing.Font("굴림", 9F);
			this.label8.Location = new System.Drawing.Point(3, 148);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(193, 37);
			this.label8.TabIndex = 8;
			this.label8.Text = "⑤ 아이디";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txt_smptserverport
			// 
			this.txt_smptserverport.Location = new System.Drawing.Point(202, 115);
			this.txt_smptserverport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txt_smptserverport.Name = "txt_smptserverport";
			this.txt_smptserverport.Size = new System.Drawing.Size(444, 22);
			this.txt_smptserverport.TabIndex = 6;
			// 
			// txt_smptpasswd
			// 
			this.txt_smptpasswd.Location = new System.Drawing.Point(202, 189);
			this.txt_smptpasswd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txt_smptpasswd.Name = "txt_smptpasswd";
			this.txt_smptpasswd.PasswordChar = '●';
			this.txt_smptpasswd.Size = new System.Drawing.Size(444, 22);
			this.txt_smptpasswd.TabIndex = 8;
			// 
			// txt_sendmailaddressname
			// 
			this.txt_sendmailaddressname.Location = new System.Drawing.Point(202, 41);
			this.txt_sendmailaddressname.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txt_sendmailaddressname.Name = "txt_sendmailaddressname";
			this.txt_sendmailaddressname.Size = new System.Drawing.Size(444, 22);
			this.txt_sendmailaddressname.TabIndex = 4;
			// 
			// txt_smptserver
			// 
			this.txt_smptserver.Location = new System.Drawing.Point(202, 78);
			this.txt_smptserver.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txt_smptserver.Name = "txt_smptserver";
			this.txt_smptserver.Size = new System.Drawing.Size(444, 22);
			this.txt_smptserver.TabIndex = 5;
			// 
			// txt_serverid
			// 
			this.txt_serverid.Location = new System.Drawing.Point(202, 152);
			this.txt_serverid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txt_serverid.Name = "txt_serverid";
			this.txt_serverid.Size = new System.Drawing.Size(444, 22);
			this.txt_serverid.TabIndex = 7;
			// 
			// srButton1
			// 
			this.srButton1.Authority = false;
			this.srButton1.Font = new System.Drawing.Font("굴림체", 9F);
			this.srButton1.Image = global::DUTY1000.Properties.Resources.question_mark_on_a_circular_black_background;
			this.srButton1.Location = new System.Drawing.Point(122, 15);
			this.srButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.srButton1.Name = "srButton1";
			this.srButton1.Size = new System.Drawing.Size(39, 41);
			this.srButton1.SRAuthCrud = SilkRoad.UserControls.SRButton.AuthCrudType.A;
			this.srButton1.SRWidthType = SilkRoad.UserControls.SRButton.WidthType.Custom;
			this.srButton1.TabIndex = 18;
			this.srButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.srButton1.UseVisualStyleBackColor = true;
			this.srButton1.Click += new System.EventHandler(this.srButton1_Click);
			// 
			// btn_cancel
			// 
			this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_cancel.Authority = false;
			this.btn_cancel.Font = new System.Drawing.Font("굴림체", 9F);
			this.btn_cancel.Image = ((System.Drawing.Image)(resources.GetObject("btn_cancel.Image")));
			this.btn_cancel.Location = new System.Drawing.Point(637, 299);
			this.btn_cancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new System.Drawing.Size(91, 30);
			this.btn_cancel.SRAuthCrud = SilkRoad.UserControls.SRButton.AuthCrudType.NONE;
			this.btn_cancel.SRKindOf = SilkRoad.UserControls.SRButton.ButtonKindOfType.취소;
			this.btn_cancel.SRWidthType = SilkRoad.UserControls.SRButton.WidthType.Custom;
			this.btn_cancel.TabIndex = 10;
			this.btn_cancel.Text = "취  소";
			this.btn_cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btn_cancel.UseVisualStyleBackColor = true;
			this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
			// 
			// btn_save
			// 
			this.btn_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_save.Authority = false;
			this.btn_save.Font = new System.Drawing.Font("굴림체", 9F);
			this.btn_save.Image = ((System.Drawing.Image)(resources.GetObject("btn_save.Image")));
			this.btn_save.Location = new System.Drawing.Point(540, 299);
			this.btn_save.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btn_save.Name = "btn_save";
			this.btn_save.Size = new System.Drawing.Size(91, 30);
			this.btn_save.SRAuthCrud = SilkRoad.UserControls.SRButton.AuthCrudType.A;
			this.btn_save.SRKindOf = SilkRoad.UserControls.SRButton.ButtonKindOfType.저장;
			this.btn_save.SRWidthType = SilkRoad.UserControls.SRButton.WidthType.Custom;
			this.btn_save.TabIndex = 9;
			this.btn_save.Text = "저  장";
			this.btn_save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btn_save.UseVisualStyleBackColor = true;
			this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
			this.label4.Location = new System.Drawing.Point(29, 26);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(87, 15);
			this.label4.TabIndex = 1;
			this.label4.Text = "이메일설정";
			// 
			// xtraTabPage3
			// 
			this.xtraTabPage3.Controls.Add(this.srTabControl2);
			this.xtraTabPage3.Controls.Add(this.panel2);
			this.xtraTabPage3.Name = "xtraTabPage3";
			this.xtraTabPage3.Size = new System.Drawing.Size(872, 572);
			this.xtraTabPage3.Text = "xtraTabPage3";
			// 
			// srTabControl2
			// 
			this.srTabControl2.AppearancePage.Header.Font = new System.Drawing.Font("굴림체", 9F);
			this.srTabControl2.AppearancePage.Header.Options.UseFont = true;
			this.srTabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.srTabControl2.Location = new System.Drawing.Point(0, 43);
			this.srTabControl2.Name = "srTabControl2";
			this.srTabControl2.SelectedTabPage = this.tab_naver;
			this.srTabControl2.Size = new System.Drawing.Size(872, 529);
			this.srTabControl2.TabIndex = 23;
			this.srTabControl2.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tab_naver,
            this.tab_daum});
			// 
			// tab_naver
			// 
			this.tab_naver.Controls.Add(this.pictureBox2);
			this.tab_naver.Name = "tab_naver";
			this.tab_naver.Size = new System.Drawing.Size(866, 502);
			this.tab_naver.Text = "네이버";
			// 
			// pictureBox2
			// 
			this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox2.Image = global::DUTY1000.Properties.Resources.NAVER;
			this.pictureBox2.Location = new System.Drawing.Point(0, 0);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(866, 502);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox2.TabIndex = 17;
			this.pictureBox2.TabStop = false;
			// 
			// tab_daum
			// 
			this.tab_daum.Controls.Add(this.pictureBox1);
			this.tab_daum.Name = "tab_daum";
			this.tab_daum.Size = new System.Drawing.Size(866, 502);
			this.tab_daum.Text = "다음";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(866, 502);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 16;
			this.pictureBox1.TabStop = false;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.SystemColors.Control;
			this.panel2.Controls.Add(this.label11);
			this.panel2.Controls.Add(this.btn_close3);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(872, 43);
			this.panel2.TabIndex = 16;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("굴림체", 9F);
			this.label11.Location = new System.Drawing.Point(54, 12);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(437, 12);
			this.label11.TabIndex = 20;
			this.label11.Text = "※ 메일 환경설정의 SMTP 사용함을 선택한 후 하단의 내용을 입력하여주세요.";
			// 
			// btn_close3
			// 
			this.btn_close3.Authority = false;
			this.btn_close3.Font = new System.Drawing.Font("굴림체", 9F);
			this.btn_close3.Image = global::DUTY1000.Properties.Resources.forbidden_mark;
			this.btn_close3.Location = new System.Drawing.Point(820, 6);
			this.btn_close3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btn_close3.Name = "btn_close3";
			this.btn_close3.Size = new System.Drawing.Size(28, 30);
			this.btn_close3.SRAuthCrud = SilkRoad.UserControls.SRButton.AuthCrudType.A;
			this.btn_close3.SRWidthType = SilkRoad.UserControls.SRButton.WidthType.Custom;
			this.btn_close3.TabIndex = 19;
			this.btn_close3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btn_close3.UseVisualStyleBackColor = true;
			this.btn_close3.Click += new System.EventHandler(this.btn_close3_Click);
			// 
			// bgWorker
			// 
			this.bgWorker.WorkerReportsProgress = true;
			this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
			this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
			this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
			// 
			// lb_step
			// 
			this.lb_step.AutoSize = true;
			this.lb_step.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold);
			this.lb_step.ForeColor = System.Drawing.Color.Blue;
			this.lb_step.Location = new System.Drawing.Point(12, 141);
			this.lb_step.Name = "lb_step";
			this.lb_step.Size = new System.Drawing.Size(96, 15);
			this.lb_step.TabIndex = 26;
			this.lb_step.Text = "1차연차촉진";
			// 
			// sendemail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(878, 769);
			this.Controls.Add(this.srTabControl1);
			this.Controls.Add(this.panel1);
			this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.Name = "sendemail";
			this.Text = "이메일전송";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.sendemail_FormClosed);
			this.Load += new System.EventHandler(this.SendEmail_Load);
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.srTabControl1)).EndInit();
			this.srTabControl1.ResumeLayout(false);
			this.xtraTabPage1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grd1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grdv1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
			this.panel5.ResumeLayout(false);
			this.panel5.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.txt_cnt_chk.Properties)).EndInit();
			this.xtraTabPage2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.xtraTabPage3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.srTabControl2)).EndInit();
			this.srTabControl2.ResumeLayout(false);
			this.tab_naver.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.tab_daum.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_sendEmail;
        private System.Windows.Forms.TextBox txt_Subject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_cc;
        private SilkRoad.UserControls.SRButton btn_setting;
        private SilkRoad.UserControls.SRTabControl srTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraGrid.GridControl grd1;
        private DevExpress.XtraGrid.Views.Grid.GridView grdv1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private System.Windows.Forms.Panel panel5;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private System.Windows.Forms.Panel panel3;
        private SilkRoad.UserControls.SRButton srButton1;
        private SilkRoad.UserControls.SRButton btn_cancel;
        private SilkRoad.UserControls.SRButton btn_save;
        private System.Windows.Forms.TextBox txt_smptserverport;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_smptpasswd;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_serverid;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_smptserver;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_sendmailaddressname;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_fromemailaddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private SilkRoad.UserControls.SRTabControl srTabControl2;
        private DevExpress.XtraTab.XtraTabPage tab_naver;
        private System.Windows.Forms.PictureBox pictureBox2;
        private DevExpress.XtraTab.XtraTabPage tab_daum;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label11;
        private SilkRoad.UserControls.SRButton btn_close3;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
        private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox txt_Body;
		private SilkRoad.UserControls.SRTextEdit txt_cnt_chk;
		private SilkRoad.UserControls.SRLabel srLabel16;
		private SilkRoad.UserControls.SRButton btn_exit;
		private SilkRoad.UserControls.SRButton btn_send;
		private SilkRoad.UserControls.SRButton btn_canc;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label lb_step;
	}
}


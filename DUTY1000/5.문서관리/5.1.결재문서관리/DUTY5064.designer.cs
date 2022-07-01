namespace DUTY1000
{
    partial class duty5064
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(duty5064));
			DevExpress.XtraScheduler.Printing.DailyPrintStyle dailyPrintStyle1 = new DevExpress.XtraScheduler.Printing.DailyPrintStyle();
			DevExpress.XtraScheduler.Printing.WeeklyPrintStyle weeklyPrintStyle1 = new DevExpress.XtraScheduler.Printing.WeeklyPrintStyle();
			DevExpress.XtraScheduler.Printing.MonthlyPrintStyle monthlyPrintStyle1 = new DevExpress.XtraScheduler.Printing.MonthlyPrintStyle();
			DevExpress.XtraScheduler.Printing.TriFoldPrintStyle triFoldPrintStyle1 = new DevExpress.XtraScheduler.Printing.TriFoldPrintStyle();
			DevExpress.XtraScheduler.Printing.CalendarDetailsPrintStyle calendarDetailsPrintStyle1 = new DevExpress.XtraScheduler.Printing.CalendarDetailsPrintStyle();
			DevExpress.XtraScheduler.Printing.MemoPrintStyle memoPrintStyle1 = new DevExpress.XtraScheduler.Printing.MemoPrintStyle();
			DevExpress.XtraScheduler.TimeRuler timeRuler1 = new DevExpress.XtraScheduler.TimeRuler();
			DevExpress.XtraScheduler.TimeRuler timeRuler2 = new DevExpress.XtraScheduler.TimeRuler();
			DevExpress.XtraScheduler.TimeRuler timeRuler3 = new DevExpress.XtraScheduler.TimeRuler();
			this.btn_exit = new SilkRoad.UserControls.SRButton();
			this.srPanel1 = new SilkRoad.UserControls.SRPanel();
			this.srTitle1 = new SilkRoad.UserControls.SRTitle();
			this.srPanel3 = new SilkRoad.UserControls.SRPanel();
			this.btn_proc = new SilkRoad.UserControls.SRButton();
			this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gr_detail = new SilkRoad.UserControls.SRGroupBox();
			this.srPanel4 = new SilkRoad.UserControls.SRPanel();
			this.srLabel9 = new SilkRoad.UserControls.SRLabel();
			this.srLabel8 = new SilkRoad.UserControls.SRLabel();
			this.srLabel4 = new SilkRoad.UserControls.SRLabel();
			this.srLabel3 = new SilkRoad.UserControls.SRLabel();
			this.schedulerStorage1 = new DevExpress.XtraScheduler.SchedulerStorage(this.components);
			this.schedulerControl1 = new DevExpress.XtraScheduler.SchedulerControl();
			((System.ComponentModel.ISupportInitialize)(this.srPanel1)).BeginInit();
			this.srPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.srPanel3)).BeginInit();
			this.srPanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gr_detail)).BeginInit();
			this.gr_detail.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.srPanel4)).BeginInit();
			this.srPanel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.schedulerStorage1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.schedulerControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// btn_exit
			// 
			this.btn_exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_exit.Authority = false;
			this.btn_exit.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.btn_exit.Image = ((System.Drawing.Image)(resources.GetObject("btn_exit.Image")));
			this.btn_exit.Location = new System.Drawing.Point(75, 9);
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
			this.srPanel1.Size = new System.Drawing.Size(1386, 46);
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
			this.srTitle1.SRTitleTxt = "원장단근무표";
			this.srTitle1.TabIndex = 26;
			this.srTitle1.TabStop = false;
			// 
			// srPanel3
			// 
			this.srPanel3.Appearance.BackColor = System.Drawing.SystemColors.Control;
			this.srPanel3.Appearance.Options.UseBackColor = true;
			this.srPanel3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.srPanel3.Controls.Add(this.btn_proc);
			this.srPanel3.Controls.Add(this.btn_exit);
			this.srPanel3.Dock = System.Windows.Forms.DockStyle.Right;
			this.srPanel3.Location = new System.Drawing.Point(1241, 3);
			this.srPanel3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
			this.srPanel3.LookAndFeel.UseDefaultLookAndFeel = false;
			this.srPanel3.Name = "srPanel3";
			this.srPanel3.Size = new System.Drawing.Size(142, 40);
			this.srPanel3.TabIndex = 0;
			// 
			// btn_proc
			// 
			this.btn_proc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_proc.Authority = false;
			this.btn_proc.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.btn_proc.Image = ((System.Drawing.Image)(resources.GetObject("btn_proc.Image")));
			this.btn_proc.Location = new System.Drawing.Point(14, 9);
			this.btn_proc.Name = "btn_proc";
			this.btn_proc.Size = new System.Drawing.Size(60, 24);
			this.btn_proc.SRAuthCrud = SilkRoad.UserControls.SRButton.AuthCrudType.R;
			this.btn_proc.SRKindOf = SilkRoad.UserControls.SRButton.ButtonKindOfType.조회;
			this.btn_proc.SRWidthType = SilkRoad.UserControls.SRButton.WidthType.WIDTH_60;
			this.btn_proc.TabIndex = 1;
			this.btn_proc.Text = "조회";
			this.btn_proc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btn_proc.UseVisualStyleBackColor = true;
			this.btn_proc.Click += new System.EventHandler(this.btn_proc_Click);
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
			this.gr_detail.Controls.Add(this.schedulerControl1);
			this.gr_detail.Controls.Add(this.srPanel4);
			this.gr_detail.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gr_detail.Location = new System.Drawing.Point(0, 46);
			this.gr_detail.Name = "gr_detail";
			this.gr_detail.Size = new System.Drawing.Size(1386, 668);
			this.gr_detail.TabIndex = 1;
			this.gr_detail.Text = "연차및휴가내역";
			// 
			// srPanel4
			// 
			this.srPanel4.Controls.Add(this.srLabel9);
			this.srPanel4.Controls.Add(this.srLabel8);
			this.srPanel4.Controls.Add(this.srLabel4);
			this.srPanel4.Controls.Add(this.srLabel3);
			this.srPanel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.srPanel4.Location = new System.Drawing.Point(2, 22);
			this.srPanel4.Name = "srPanel4";
			this.srPanel4.Size = new System.Drawing.Size(1382, 43);
			this.srPanel4.TabIndex = 5;
			// 
			// srLabel9
			// 
			this.srLabel9.BackColor = System.Drawing.Color.Aquamarine;
			this.srLabel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.srLabel9.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.srLabel9.ForeColor = System.Drawing.Color.Black;
			this.srLabel9.Location = new System.Drawing.Point(305, 9);
			this.srLabel9.Name = "srLabel9";
			this.srLabel9.Size = new System.Drawing.Size(92, 22);
			this.srLabel9.SRBulletColor = System.Drawing.Color.Empty;
			this.srLabel9.TabIndex = 4;
			this.srLabel9.Text = "관리자승인";
			this.srLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.srLabel9.Visible = false;
			// 
			// srLabel8
			// 
			this.srLabel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.srLabel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.srLabel8.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.srLabel8.ForeColor = System.Drawing.Color.Black;
			this.srLabel8.Location = new System.Drawing.Point(207, 9);
			this.srLabel8.Name = "srLabel8";
			this.srLabel8.Size = new System.Drawing.Size(92, 22);
			this.srLabel8.SRBulletColor = System.Drawing.Color.Empty;
			this.srLabel8.TabIndex = 3;
			this.srLabel8.Text = "취소";
			this.srLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// srLabel4
			// 
			this.srLabel4.BackColor = System.Drawing.Color.LightSkyBlue;
			this.srLabel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.srLabel4.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.srLabel4.ForeColor = System.Drawing.Color.Black;
			this.srLabel4.Location = new System.Drawing.Point(109, 9);
			this.srLabel4.Name = "srLabel4";
			this.srLabel4.Size = new System.Drawing.Size(92, 22);
			this.srLabel4.SRBulletColor = System.Drawing.Color.Empty;
			this.srLabel4.TabIndex = 2;
			this.srLabel4.Text = "승인";
			this.srLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// srLabel3
			// 
			this.srLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.srLabel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.srLabel3.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.srLabel3.ForeColor = System.Drawing.Color.Black;
			this.srLabel3.Location = new System.Drawing.Point(11, 9);
			this.srLabel3.Name = "srLabel3";
			this.srLabel3.Size = new System.Drawing.Size(92, 22);
			this.srLabel3.SRBulletColor = System.Drawing.Color.Empty;
			this.srLabel3.TabIndex = 1;
			this.srLabel3.Text = "신청";
			this.srLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// schedulerControl1
			// 
			this.schedulerControl1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Month;
			this.schedulerControl1.Appearance.AdditionalHeaderCaption.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.schedulerControl1.Appearance.AdditionalHeaderCaption.Options.UseFont = true;
			this.schedulerControl1.Appearance.AlternateHeaderCaption.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.schedulerControl1.Appearance.AlternateHeaderCaption.Options.UseFont = true;
			this.schedulerControl1.Appearance.AlternateHeaderCaptionLine.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.schedulerControl1.Appearance.AlternateHeaderCaptionLine.Options.UseFont = true;
			this.schedulerControl1.Appearance.Appointment.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.schedulerControl1.Appearance.Appointment.Options.UseFont = true;
			this.schedulerControl1.Appearance.HeaderCaption.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.schedulerControl1.Appearance.HeaderCaption.Options.UseFont = true;
			this.schedulerControl1.Appearance.HeaderCaptionLine.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.schedulerControl1.Appearance.HeaderCaptionLine.Options.UseFont = true;
			this.schedulerControl1.Appearance.NavigationButton.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.schedulerControl1.Appearance.NavigationButton.Options.UseFont = true;
			this.schedulerControl1.Appearance.NavigationButtonDisabled.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.schedulerControl1.Appearance.NavigationButtonDisabled.Options.UseFont = true;
			this.schedulerControl1.Appearance.ResourceHeaderCaption.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.schedulerControl1.Appearance.ResourceHeaderCaption.Options.UseFont = true;
			this.schedulerControl1.Appearance.ResourceHeaderCaptionLine.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.schedulerControl1.Appearance.ResourceHeaderCaptionLine.Options.UseFont = true;
			this.schedulerControl1.Appearance.Selection.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.schedulerControl1.Appearance.Selection.Options.UseFont = true;
			this.schedulerControl1.DataStorage = this.schedulerStorage1;
			this.schedulerControl1.DateNavigationBar.Visible = false;
			this.schedulerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.schedulerControl1.DragDropMode = DevExpress.XtraScheduler.DragDropMode.Manual;
			this.schedulerControl1.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.schedulerControl1.Location = new System.Drawing.Point(2, 65);
			this.schedulerControl1.Name = "schedulerControl1";
			this.schedulerControl1.OptionsCustomization.AllowAppointmentCopy = DevExpress.XtraScheduler.UsedAppointmentType.None;
			this.schedulerControl1.OptionsCustomization.AllowAppointmentCreate = DevExpress.XtraScheduler.UsedAppointmentType.None;
			this.schedulerControl1.OptionsCustomization.AllowAppointmentDelete = DevExpress.XtraScheduler.UsedAppointmentType.None;
			this.schedulerControl1.OptionsCustomization.AllowAppointmentDrag = DevExpress.XtraScheduler.UsedAppointmentType.None;
			this.schedulerControl1.OptionsCustomization.AllowAppointmentDragBetweenResources = DevExpress.XtraScheduler.UsedAppointmentType.None;
			this.schedulerControl1.OptionsCustomization.AllowAppointmentEdit = DevExpress.XtraScheduler.UsedAppointmentType.None;
			this.schedulerControl1.OptionsCustomization.AllowAppointmentMultiSelect = false;
			this.schedulerControl1.OptionsCustomization.AllowDisplayAppointmentDependencyForm = DevExpress.XtraScheduler.AllowDisplayAppointmentDependencyForm.Never;
			this.schedulerControl1.OptionsCustomization.AllowDisplayAppointmentForm = DevExpress.XtraScheduler.AllowDisplayAppointmentForm.Never;
			this.schedulerControl1.OptionsCustomization.AllowInplaceEditor = DevExpress.XtraScheduler.UsedAppointmentType.None;
			this.schedulerControl1.OptionsDragDrop.DragDropMode = DevExpress.XtraScheduler.DragDropMode.Manual;
			this.schedulerControl1.OptionsRangeControl.AllowChangeActiveView = false;
			this.schedulerControl1.OptionsRangeControl.AutoAdjustMode = false;
			this.schedulerControl1.OptionsView.NavigationButtons.Visibility = DevExpress.XtraScheduler.NavigationButtonVisibility.Never;
			monthlyPrintStyle1.AppointmentFont = new System.Drawing.Font("맑은 고딕", 12F);
			monthlyPrintStyle1.HeadingsFont = new System.Drawing.Font("맑은 고딕", 14F);
			calendarDetailsPrintStyle1.AppointmentFont = new System.Drawing.Font("맑은 고딕", 12F);
			calendarDetailsPrintStyle1.HeadingsFont = new System.Drawing.Font("맑은 고딕", 14F);
			memoPrintStyle1.AppointmentFont = new System.Drawing.Font("맑은 고딕", 12F);
			memoPrintStyle1.HeadingsFont = new System.Drawing.Font("맑은 고딕", 14F);
			this.schedulerControl1.PrintStyles.Add(dailyPrintStyle1);
			this.schedulerControl1.PrintStyles.Add(weeklyPrintStyle1);
			this.schedulerControl1.PrintStyles.Add(monthlyPrintStyle1);
			this.schedulerControl1.PrintStyles.Add(triFoldPrintStyle1);
			this.schedulerControl1.PrintStyles.Add(calendarDetailsPrintStyle1);
			this.schedulerControl1.PrintStyles.Add(memoPrintStyle1);
			this.schedulerControl1.ResourceNavigator.Visibility = DevExpress.XtraScheduler.ResourceNavigatorVisibility.Never;
			this.schedulerControl1.Size = new System.Drawing.Size(1382, 601);
			this.schedulerControl1.Start = new System.DateTime(2021, 9, 26, 0, 0, 0, 0);
			this.schedulerControl1.TabIndex = 6;
			this.schedulerControl1.Text = "schedulerControl1";
			this.schedulerControl1.Views.AgendaView.Enabled = false;
			this.schedulerControl1.Views.DayView.Enabled = false;
			this.schedulerControl1.Views.DayView.TimeRulers.Add(timeRuler1);
			this.schedulerControl1.Views.FullWeekView.TimeRulers.Add(timeRuler2);
			this.schedulerControl1.Views.GanttView.Enabled = false;
			this.schedulerControl1.Views.MonthView.ShowMoreButtons = false;
			this.schedulerControl1.Views.TimelineView.Enabled = false;
			this.schedulerControl1.Views.WeekView.Enabled = false;
			this.schedulerControl1.Views.WorkWeekView.Enabled = false;
			this.schedulerControl1.Views.WorkWeekView.TimeRulers.Add(timeRuler3);
			// 
			// duty5064
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1386, 714);
			this.Controls.Add(this.gr_detail);
			this.Controls.Add(this.srPanel1);
			this.Name = "duty5064";
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
			((System.ComponentModel.ISupportInitialize)(this.srPanel4)).EndInit();
			this.srPanel4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.schedulerStorage1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.schedulerControl1)).EndInit();
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
        private SilkRoad.UserControls.SRButton btn_proc;
        private SilkRoad.UserControls.SRGroupBox gr_detail;
		private SilkRoad.UserControls.SRPanel srPanel4;
		private SilkRoad.UserControls.SRLabel srLabel9;
		private SilkRoad.UserControls.SRLabel srLabel8;
		private SilkRoad.UserControls.SRLabel srLabel4;
		private SilkRoad.UserControls.SRLabel srLabel3;
		protected DevExpress.XtraScheduler.SchedulerStorage schedulerStorage1;
		private DevExpress.XtraScheduler.SchedulerControl schedulerControl1;
	}
}


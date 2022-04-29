namespace DUTY1200
{
    partial class duty3400_modiPlan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.srLabel1 = new SilkRoad.UserControls.SRLabel();
            this.srLabel2 = new SilkRoad.UserControls.SRLabel();
            this.btn_ok = new SilkRoad.UserControls.SRButton();
            this.btn_can = new SilkRoad.UserControls.SRButton();
            this.cmb_gtype_fr = new SilkRoad.UserControls.SRCombo();
            this.cmb_gtype_to = new SilkRoad.UserControls.SRCombo();
            this.srLabel3 = new SilkRoad.UserControls.SRLabel();
            this.srLabel4 = new SilkRoad.UserControls.SRLabel();
            this.srLabel5 = new SilkRoad.UserControls.SRLabel();
            this.lb_sawon = new SilkRoad.UserControls.SRLabel();
            this.lb_date = new SilkRoad.UserControls.SRLabel();
            this.srLabel6 = new SilkRoad.UserControls.SRLabel();
            this.srLabel7 = new SilkRoad.UserControls.SRLabel();
            this.lb_memo = new SilkRoad.UserControls.SRPanel();
            this.txt_gtype_fr = new SilkRoad.UserControls.SRTextEdit();
            this.srLabel8 = new SilkRoad.UserControls.SRLabel();
            this.srLabel9 = new SilkRoad.UserControls.SRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_gtype_fr.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_gtype_to.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_memo)).BeginInit();
            this.lb_memo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_gtype_fr.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // srLabel1
            // 
            this.srLabel1.AutoSize = true;
            this.srLabel1.BackColor = System.Drawing.Color.Transparent;
            this.srLabel1.ForeColor = System.Drawing.Color.Black;
            this.srLabel1.Location = new System.Drawing.Point(58, 151);
            this.srLabel1.Name = "srLabel1";
            this.srLabel1.Size = new System.Drawing.Size(29, 12);
            this.srLabel1.TabIndex = 0;
            this.srLabel1.Text = "신청";
            // 
            // srLabel2
            // 
            this.srLabel2.AutoSize = true;
            this.srLabel2.BackColor = System.Drawing.Color.Transparent;
            this.srLabel2.ForeColor = System.Drawing.Color.Black;
            this.srLabel2.Location = new System.Drawing.Point(58, 200);
            this.srLabel2.Name = "srLabel2";
            this.srLabel2.Size = new System.Drawing.Size(29, 12);
            this.srLabel2.TabIndex = 1;
            this.srLabel2.Text = "변경";
            // 
            // btn_ok
            // 
            this.btn_ok.Authority = false;
            this.btn_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ok.Location = new System.Drawing.Point(101, 239);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(70, 24);
            this.btn_ok.SRAuthCrud = SilkRoad.UserControls.SRButton.AuthCrudType.A;
            this.btn_ok.TabIndex = 2;
            this.btn_ok.Text = "확인";
            this.btn_ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_can
            // 
            this.btn_can.Authority = false;
            this.btn_can.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_can.Location = new System.Drawing.Point(177, 239);
            this.btn_can.Name = "btn_can";
            this.btn_can.Size = new System.Drawing.Size(70, 24);
            this.btn_can.SRAuthCrud = SilkRoad.UserControls.SRButton.AuthCrudType.NONE;
            this.btn_can.TabIndex = 3;
            this.btn_can.Text = "취소";
            this.btn_can.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_can.UseVisualStyleBackColor = true;
            // 
            // cmb_gtype_fr
            // 
            this.cmb_gtype_fr.EditValue = "";
            this.cmb_gtype_fr.EnterMoveNextControl = true;
            this.cmb_gtype_fr.Location = new System.Drawing.Point(108, 123);
            this.cmb_gtype_fr.Name = "cmb_gtype_fr";
            this.cmb_gtype_fr.Properties.Appearance.Font = new System.Drawing.Font("굴림체", 9F);
            this.cmb_gtype_fr.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.cmb_gtype_fr.Properties.Appearance.Options.UseFont = true;
            this.cmb_gtype_fr.Properties.Appearance.Options.UseForeColor = true;
            this.cmb_gtype_fr.Properties.AppearanceDropDown.Font = new System.Drawing.Font("굴림체", 9F);
            this.cmb_gtype_fr.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cmb_gtype_fr.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmb_gtype_fr.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.cmb_gtype_fr.Properties.AutoHeight = false;
            this.cmb_gtype_fr.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_gtype_fr.Properties.Items.AddRange(new object[] {
            "",
            "D",
            "E",
            "N",
            "O"});
            this.cmb_gtype_fr.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_gtype_fr.Size = new System.Drawing.Size(139, 23);
            this.cmb_gtype_fr.TabIndex = 9;
            this.cmb_gtype_fr.Visible = false;
            // 
            // cmb_gtype_to
            // 
            this.cmb_gtype_to.EditValue = "";
            this.cmb_gtype_to.EnterMoveNextControl = true;
            this.cmb_gtype_to.Location = new System.Drawing.Point(108, 195);
            this.cmb_gtype_to.Name = "cmb_gtype_to";
            this.cmb_gtype_to.Properties.Appearance.Font = new System.Drawing.Font("굴림체", 9F);
            this.cmb_gtype_to.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.cmb_gtype_to.Properties.Appearance.Options.UseFont = true;
            this.cmb_gtype_to.Properties.Appearance.Options.UseForeColor = true;
            this.cmb_gtype_to.Properties.AppearanceDropDown.Font = new System.Drawing.Font("굴림체", 9F);
            this.cmb_gtype_to.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cmb_gtype_to.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmb_gtype_to.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.cmb_gtype_to.Properties.AutoHeight = false;
            this.cmb_gtype_to.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmb_gtype_to.Properties.Items.AddRange(new object[] {
            "",
            "D",
            "E",
            "N",
            "O"});
            this.cmb_gtype_to.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmb_gtype_to.Size = new System.Drawing.Size(139, 23);
            this.cmb_gtype_to.TabIndex = 10;
            // 
            // srLabel3
            // 
            this.srLabel3.AutoSize = true;
            this.srLabel3.BackColor = System.Drawing.Color.Transparent;
            this.srLabel3.ForeColor = System.Drawing.Color.Black;
            this.srLabel3.Location = new System.Drawing.Point(168, 176);
            this.srLabel3.Name = "srLabel3";
            this.srLabel3.Size = new System.Drawing.Size(14, 12);
            this.srLabel3.TabIndex = 11;
            this.srLabel3.Text = "↓";
            // 
            // srLabel4
            // 
            this.srLabel4.AutoSize = true;
            this.srLabel4.BackColor = System.Drawing.Color.Transparent;
            this.srLabel4.ForeColor = System.Drawing.Color.Black;
            this.srLabel4.Location = new System.Drawing.Point(37, 9);
            this.srLabel4.Name = "srLabel4";
            this.srLabel4.Size = new System.Drawing.Size(29, 12);
            this.srLabel4.TabIndex = 12;
            this.srLabel4.Text = "사원";
            // 
            // srLabel5
            // 
            this.srLabel5.AutoSize = true;
            this.srLabel5.BackColor = System.Drawing.Color.Transparent;
            this.srLabel5.ForeColor = System.Drawing.Color.Black;
            this.srLabel5.Location = new System.Drawing.Point(37, 29);
            this.srLabel5.Name = "srLabel5";
            this.srLabel5.Size = new System.Drawing.Size(29, 12);
            this.srLabel5.TabIndex = 13;
            this.srLabel5.Text = "일자";
            // 
            // lb_sawon
            // 
            this.lb_sawon.AutoSize = true;
            this.lb_sawon.BackColor = System.Drawing.Color.Transparent;
            this.lb_sawon.ForeColor = System.Drawing.Color.Black;
            this.lb_sawon.Location = new System.Drawing.Point(85, 9);
            this.lb_sawon.Name = "lb_sawon";
            this.lb_sawon.Size = new System.Drawing.Size(11, 12);
            this.lb_sawon.TabIndex = 14;
            this.lb_sawon.Text = "-";
            // 
            // lb_date
            // 
            this.lb_date.AutoSize = true;
            this.lb_date.BackColor = System.Drawing.Color.Transparent;
            this.lb_date.ForeColor = System.Drawing.Color.Black;
            this.lb_date.Location = new System.Drawing.Point(85, 29);
            this.lb_date.Name = "lb_date";
            this.lb_date.Size = new System.Drawing.Size(11, 12);
            this.lb_date.TabIndex = 15;
            this.lb_date.Text = "-";
            // 
            // srLabel6
            // 
            this.srLabel6.AutoSize = true;
            this.srLabel6.BackColor = System.Drawing.Color.Transparent;
            this.srLabel6.ForeColor = System.Drawing.Color.Black;
            this.srLabel6.Location = new System.Drawing.Point(37, 51);
            this.srLabel6.Name = "srLabel6";
            this.srLabel6.Size = new System.Drawing.Size(29, 12);
            this.srLabel6.TabIndex = 16;
            this.srLabel6.Text = "메모";
            // 
            // srLabel7
            // 
            this.srLabel7.AutoSize = true;
            this.srLabel7.BackColor = System.Drawing.Color.Transparent;
            this.srLabel7.ForeColor = System.Drawing.Color.Black;
            this.srLabel7.Location = new System.Drawing.Point(13, 74);
            this.srLabel7.Name = "srLabel7";
            this.srLabel7.Size = new System.Drawing.Size(53, 12);
            this.srLabel7.TabIndex = 17;
            this.srLabel7.Text = "편집허용";
            // 
            // lb_memo
            // 
            this.lb_memo.Appearance.Font = new System.Drawing.Font("굴림체", 9F);
            this.lb_memo.Appearance.Options.UseFont = true;
            this.lb_memo.Controls.Add(this.srLabel9);
            this.lb_memo.Controls.Add(this.srLabel8);
            this.lb_memo.Controls.Add(this.srLabel4);
            this.lb_memo.Controls.Add(this.srLabel7);
            this.lb_memo.Controls.Add(this.srLabel5);
            this.lb_memo.Controls.Add(this.srLabel6);
            this.lb_memo.Controls.Add(this.lb_sawon);
            this.lb_memo.Controls.Add(this.lb_date);
            this.lb_memo.Location = new System.Drawing.Point(17, 21);
            this.lb_memo.Name = "lb_memo";
            this.lb_memo.Size = new System.Drawing.Size(293, 99);
            this.lb_memo.TabIndex = 18;
            // 
            // txt_gtype_fr
            // 
            this.txt_gtype_fr.EditValue = "";
            this.txt_gtype_fr.Enabled = false;
            this.txt_gtype_fr.EnterMoveNextControl = true;
            this.txt_gtype_fr.Location = new System.Drawing.Point(108, 147);
            this.txt_gtype_fr.Name = "txt_gtype_fr";
            this.txt_gtype_fr.Properties.Appearance.Font = new System.Drawing.Font("굴림체", 9F);
            this.txt_gtype_fr.Properties.Appearance.Options.UseFont = true;
            this.txt_gtype_fr.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_gtype_fr.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txt_gtype_fr.Properties.AutoHeight = false;
            this.txt_gtype_fr.Properties.LookAndFeel.SkinName = "Lilian";
            this.txt_gtype_fr.Properties.MaxLength = 40;
            this.txt_gtype_fr.Properties.ReadOnly = true;
            this.txt_gtype_fr.Size = new System.Drawing.Size(139, 20);
            this.txt_gtype_fr.TabIndex = 411;
            // 
            // srLabel8
            // 
            this.srLabel8.AutoSize = true;
            this.srLabel8.BackColor = System.Drawing.Color.Transparent;
            this.srLabel8.ForeColor = System.Drawing.Color.Black;
            this.srLabel8.Location = new System.Drawing.Point(85, 51);
            this.srLabel8.Name = "srLabel8";
            this.srLabel8.Size = new System.Drawing.Size(11, 12);
            this.srLabel8.TabIndex = 18;
            this.srLabel8.Text = "-";
            // 
            // srLabel9
            // 
            this.srLabel9.AutoSize = true;
            this.srLabel9.BackColor = System.Drawing.Color.Transparent;
            this.srLabel9.ForeColor = System.Drawing.Color.Black;
            this.srLabel9.Location = new System.Drawing.Point(85, 74);
            this.srLabel9.Name = "srLabel9";
            this.srLabel9.Size = new System.Drawing.Size(11, 12);
            this.srLabel9.TabIndex = 19;
            this.srLabel9.Text = "-";
            // 
            // duty3400_modiPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 314);
            this.Controls.Add(this.txt_gtype_fr);
            this.Controls.Add(this.lb_memo);
            this.Controls.Add(this.srLabel3);
            this.Controls.Add(this.cmb_gtype_to);
            this.Controls.Add(this.cmb_gtype_fr);
            this.Controls.Add(this.btn_can);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.srLabel2);
            this.Controls.Add(this.srLabel1);
            this.Name = "duty3400_modiPlan";
            this.Text = "근무수정";
            ((System.ComponentModel.ISupportInitialize)(this.cmb_gtype_fr.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmb_gtype_to.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lb_memo)).EndInit();
            this.lb_memo.ResumeLayout(false);
            this.lb_memo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_gtype_fr.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SilkRoad.UserControls.SRLabel srLabel1;
        private SilkRoad.UserControls.SRLabel srLabel2;
        private SilkRoad.UserControls.SRButton btn_ok;
        private SilkRoad.UserControls.SRButton btn_can;
        private SilkRoad.UserControls.SRCombo cmb_gtype_fr;
        private SilkRoad.UserControls.SRCombo cmb_gtype_to;
        private SilkRoad.UserControls.SRLabel srLabel3;
        private SilkRoad.UserControls.SRLabel srLabel4;
        private SilkRoad.UserControls.SRLabel srLabel5;
        private SilkRoad.UserControls.SRLabel lb_sawon;
        private SilkRoad.UserControls.SRLabel lb_date;
        private SilkRoad.UserControls.SRLabel srLabel6;
        private SilkRoad.UserControls.SRLabel srLabel7;
        private SilkRoad.UserControls.SRPanel lb_memo;
        private SilkRoad.UserControls.SRTextEdit txt_gtype_fr;
        private SilkRoad.UserControls.SRLabel srLabel9;
        private SilkRoad.UserControls.SRLabel srLabel8;
    }
}
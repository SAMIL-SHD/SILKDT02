using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace DUTY1000
{
    public partial class rpt_506a : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt_506a(string title)
        {
            InitializeComponent();

			lab_title.Text = title;
			//GroupField gf1 = new GroupField();
			//gf1.FieldName = "GUBN_NM";
			//GroupHeader1.GroupFields.Add(gf1);

			s_qt1.DataBindings.Add("Text", DataSource, "CALL_CNT", "");
			s_qt2.DataBindings.Add("Text", DataSource, "CALL_TIME", "");
		}
    }
}

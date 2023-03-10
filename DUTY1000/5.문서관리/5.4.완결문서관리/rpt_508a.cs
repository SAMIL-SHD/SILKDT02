using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace DUTY1000
{
    public partial class rpt_508a : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt_508a(string title)
        {
            InitializeComponent();

			GroupField gf1 = new GroupField();
			gf1.FieldName = "GUBN_NM";
			GroupHeader1.GroupFields.Add(gf1);

			s_c_sumbill.DataBindings.Add("Text", DataSource, "YC_DAYS", "");
		}
    }
}

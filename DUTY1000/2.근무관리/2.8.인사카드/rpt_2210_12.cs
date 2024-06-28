﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraPrinting;

namespace DUTY1000
{
    public partial class rpt_2210_12 : DevExpress.XtraReports.UI.XtraReport
    {
        SilkRoad.Common.CustomFormatter cf = new SilkRoad.Common.CustomFormatter();
        SilkRoad.Common.DrawRptSignBox dsb = new SilkRoad.Common.DrawRptSignBox();

        public rpt_2210_12()
        {
            InitializeComponent();
        }

        private void xrTableCell2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            xrTableCell2.Text = GetCurrentColumnValue("AWAIFDAT").ToString() == "" ? "" :
                                GetCurrentColumnValue("AWAIFDAT").ToString().Substring(0, 4) + "."
                              + GetCurrentColumnValue("AWAIFDAT").ToString().Substring(4, 2) + "."
                              + GetCurrentColumnValue("AWAIFDAT").ToString().Substring(6, 2) + " ~ "
                              + GetCurrentColumnValue("AWAITDAT").ToString().Substring(0, 4) + "."
                              + GetCurrentColumnValue("AWAITDAT").ToString().Substring(4, 2) + "."
                              + GetCurrentColumnValue("AWAITDAT").ToString().Substring(6, 2) ;
        }

		private void xrTable2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			if (this.GetCurrentRow().Equals(this.GetNextRow()))
			{
				xrTableCell1.Borders = BorderSide.Bottom | BorderSide.Right;
				xrTableCell2.Borders = BorderSide.Bottom | BorderSide.Right;
				xrTableCell5.Borders = BorderSide.Bottom | BorderSide.Right;
				xrTableCell7.Borders = BorderSide.Bottom | BorderSide.Right;
				xrTableCell8.Borders = BorderSide.Bottom | BorderSide.Right;
			}
		}
	}
}

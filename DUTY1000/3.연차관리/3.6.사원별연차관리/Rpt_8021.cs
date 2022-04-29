using System;
using System.Drawing;
using System.Data;
using System.IO;
using SilkRoad.Common;

namespace DUTY1000
{
	public partial class Rpt_8021 : DevExpress.XtraReports.UI.XtraReport
	{
        CommonLibrary clib = new CommonLibrary();
		//SilkRoad.Common.CustomFormatter cf = new SilkRoad.Common.CustomFormatter();
		//SilkRoad.Common.DrawRptSignBox dsb = new SilkRoad.Common.DrawRptSignBox();
		////DataSet ds;

		public Rpt_8021(string sabn, string comp_nm, DataSet ds)
		{
			InitializeComponent();
			DataRow drow = ds.Tables["SEARCH_YC"].Select("SAWON_NO = '" + sabn + "'")[0];

			xrTableCell51.Text = comp_nm; //회사명
			xrTableCell23.Text = drow["DEPT_NM"].ToString(); //부서명
			xrTableCell12.Text = drow["SAWON_NM"].ToString(); //이름
			xrTableCell14.Text = string.Format("{0:#,##0.##}", Convert.ToDecimal(drow["YC_TOTAL"].ToString())) + " 일"; //발생연차 휴가일수
			xrTableCell72.Text = string.Format("{0:#,##0.##}", Convert.ToDecimal(drow["YC_USE"].ToString())) + " 일"; //사용연차 일수
			xrTableCell27.Text = string.Format("{0:#,##0.##}", Convert.ToDecimal(drow["YC_REMAIN"].ToString())) + " 일"; //남은연차 일수

			xrTableCell24.Text = clib.DateToText((clib.TextToDate(drow["USE_TODT"].ToString()).AddMonths(-5))).Substring(4, 2) + "월";	
			xrTableCell5.Text = clib.DateToText((clib.TextToDate(drow["USE_TODT"].ToString()).AddMonths(-4))).Substring(4, 2) + "월";	
			xrTableCell10.Text = clib.DateToText((clib.TextToDate(drow["USE_TODT"].ToString()).AddMonths(-3))).Substring(4, 2) + "월";	
			xrTableCell15.Text = clib.DateToText((clib.TextToDate(drow["USE_TODT"].ToString()).AddMonths(-2))).Substring(4, 2) + "월";	

			xrTableCell2.Text = comp_nm + " 대표";			
		}
	}
}

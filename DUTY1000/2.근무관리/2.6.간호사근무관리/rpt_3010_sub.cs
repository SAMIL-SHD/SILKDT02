using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraPrinting;
using System.IO;
using SilkRoad.Common;

namespace DUTY1000
{
    public partial class rpt_3010_sub : DevExpress.XtraReports.UI.XtraReport
    {
        SilkRoad.Common.CustomFormatter cf = new SilkRoad.Common.CustomFormatter();
        SilkRoad.Common.DrawRptSignBox dsb = new SilkRoad.Common.DrawRptSignBox();
        CommonLibrary clib = new CommonLibrary();
        //DataSet ds;
        DataProcFunc df = new DataProcFunc();

        public rpt_3010_sub()//string yymm, DataTable ds)
        {
            InitializeComponent();
			//string yymm = "20220701";
			//DataTable ds = null;
			#region 요일표기
			//1.기준년월에 따른 하단 일자컬럼header 일자, 요일 설정
			//int lastday = 0; //clib.TextToInt(clib.DateToText(clib.TextToDateLast(yymm)).Substring(6, 2));			
			//string[,] days = new string[2, 32];
			//for (int i = 1; i <= lastday; i++)
			//{
			//	days[0, i] = ""; //ds.Select("H_DATE = '" + yymm.Substring(0, 6) + i.ToString().PadLeft(2, '0') + "'").Length > 0 ? "1" : ""; 
			//	days[1, i] = clib.WeekDay(clib.TextToDate(yymm.Substring(0, 6) + i.ToString().PadLeft(2, '0')));
			//}

			//c_01.BackColor = days[1, 1].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_02.BackColor = days[1, 2].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_03.BackColor = days[1, 3].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_04.BackColor = days[1, 4].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_05.BackColor = days[1, 5].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_06.BackColor = days[1, 6].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_07.BackColor = days[1, 7].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_08.BackColor = days[1, 8].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_09.BackColor = days[1, 9].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_10.BackColor = days[1, 10].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_11.BackColor = days[1, 11].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_12.BackColor = days[1, 12].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_13.BackColor = days[1, 13].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_14.BackColor = days[1, 14].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_15.BackColor = days[1, 15].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_16.BackColor = days[1, 16].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_17.BackColor = days[1, 17].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_18.BackColor = days[1, 18].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_19.BackColor = days[1, 19].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_20.BackColor = days[1, 20].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_21.BackColor = days[1, 21].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_22.BackColor = days[1, 22].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_23.BackColor = days[1, 23].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_24.BackColor = days[1, 24].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_25.BackColor = days[1, 25].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_26.BackColor = days[1, 26].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_27.BackColor = days[1, 27].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_28.BackColor = days[1, 28].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_29.BackColor = days[1, 29] == null ? Color.Transparent : days[1, 29].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_30.BackColor = days[1, 30] == null ? Color.Transparent : days[1, 30].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			//c_31.BackColor = days[1, 31] == null ? Color.Transparent : days[1, 31].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;

			//if (days[0, 1] != null && days[0, 1].ToString() == "1")
			//	c_01.BackColor = Color.MistyRose;
			//if (days[0, 2] != null && days[0, 2].ToString() == "1")
			//	c_02.BackColor = Color.MistyRose;
			//if (days[0, 3] != null && days[0, 3].ToString() == "1")
			//	c_03.BackColor = Color.MistyRose;
			//if (days[0, 4] != null && days[0, 4].ToString() == "1")
			//	c_04.BackColor = Color.MistyRose;
			//if (days[0, 5] != null && days[0, 5].ToString() == "1")
			//	c_05.BackColor = Color.MistyRose;
			//if (days[0, 6] != null && days[0, 6].ToString() == "1")
			//	c_06.BackColor = Color.MistyRose;
			//if (days[0, 7] != null && days[0, 7].ToString() == "1")
			//	c_07.BackColor = Color.MistyRose;
			//if (days[0, 8] != null && days[0, 8].ToString() == "1")
			//	c_08.BackColor = Color.MistyRose;
			//if (days[0, 9] != null && days[0, 9].ToString() == "1")
			//	c_09.BackColor = Color.MistyRose;
			//if (days[0, 10] != null && days[0, 10].ToString() == "1")
			//	c_10.BackColor = Color.MistyRose;
			//if (days[0, 11] != null && days[0, 11].ToString() == "1")
			//	c_11.BackColor = Color.MistyRose;
			//if (days[0, 12] != null && days[0, 12].ToString() == "1")
			//	c_12.BackColor = Color.MistyRose;
			//if (days[0, 13] != null && days[0, 13].ToString() == "1")
			//	c_13.BackColor = Color.MistyRose;
			//if (days[0, 14] != null && days[0, 14].ToString() == "1")
			//	c_14.BackColor = Color.MistyRose;
			//if (days[0, 15] != null && days[0, 15].ToString() == "1")
			//	c_15.BackColor = Color.MistyRose;
			//if (days[0, 16] != null && days[0, 16].ToString() == "1")
			//	c_16.BackColor = Color.MistyRose;
			//if (days[0, 17] != null && days[0, 17].ToString() == "1")
			//	c_17.BackColor = Color.MistyRose;
			//if (days[0, 18] != null && days[0, 18].ToString() == "1")
			//	c_18.BackColor = Color.MistyRose;
			//if (days[0, 19] != null && days[0, 19].ToString() == "1")
			//	c_19.BackColor = Color.MistyRose;
			//if (days[0, 20] != null && days[0, 20].ToString() == "1")
			//	c_20.BackColor = Color.MistyRose;
			//if (days[0, 21] != null && days[0, 21].ToString() == "1")
			//	c_21.BackColor = Color.MistyRose;
			//if (days[0, 22] != null && days[0, 22].ToString() == "1")
			//	c_22.BackColor = Color.MistyRose;
			//if (days[0, 23] != null && days[0, 23].ToString() == "1")
			//	c_23.BackColor = Color.MistyRose;
			//if (days[0, 24] != null && days[0, 24].ToString() == "1")
			//	c_24.BackColor = Color.MistyRose;
			//if (days[0, 25] != null && days[0, 25].ToString() == "1")
			//	c_25.BackColor = Color.MistyRose;
			//if (days[0, 26] != null && days[0, 26].ToString() == "1")
			//	c_26.BackColor = Color.MistyRose;
			//if (days[0, 27] != null && days[0, 27].ToString() == "1")
			//	c_27.BackColor = Color.MistyRose;
			//if (days[0, 28] != null && days[0, 28].ToString() == "1")
			//	c_28.BackColor = Color.MistyRose;
			//if (days[0, 29] != null && days[0, 29].ToString() == "1")
			//	c_29.BackColor = Color.MistyRose;
			//if (days[0, 30] != null && days[0, 30].ToString() == "1")
			//	c_30.BackColor = Color.MistyRose;
			//if (days[0, 31] != null && days[0, 31].ToString() == "1")
			//	c_31.BackColor = Color.MistyRose;
			#endregion

			//c_name.DataBindings.Add("Text", DataSource, "SAWON_NM", "");
			c_01.DataBindings.Add("Text", DataSource, "D01", "");
            c_02.DataBindings.Add("Text", DataSource, "D02", "");
            c_03.DataBindings.Add("Text", DataSource, "D03", "");
            c_04.DataBindings.Add("Text", DataSource, "D04", "");
            c_05.DataBindings.Add("Text", DataSource, "D05", "");
            c_06.DataBindings.Add("Text", DataSource, "D06", "");
            c_07.DataBindings.Add("Text", DataSource, "D07", "");
            c_08.DataBindings.Add("Text", DataSource, "D08", "");
            c_09.DataBindings.Add("Text", DataSource, "D09", "");
            c_10.DataBindings.Add("Text", DataSource, "D10", "");
            c_11.DataBindings.Add("Text", DataSource, "D11", "");
            c_12.DataBindings.Add("Text", DataSource, "D12", "");
            c_13.DataBindings.Add("Text", DataSource, "D13", "");
            c_14.DataBindings.Add("Text", DataSource, "D14", "");
            c_15.DataBindings.Add("Text", DataSource, "D15", "");
            c_16.DataBindings.Add("Text", DataSource, "D16", "");
            c_17.DataBindings.Add("Text", DataSource, "D17", "");
            c_18.DataBindings.Add("Text", DataSource, "D18", "");
            c_19.DataBindings.Add("Text", DataSource, "D19", "");
            c_20.DataBindings.Add("Text", DataSource, "D20", "");
            c_21.DataBindings.Add("Text", DataSource, "D21", "");
            c_22.DataBindings.Add("Text", DataSource, "D22", "");
            c_23.DataBindings.Add("Text", DataSource, "D23", "");
            c_24.DataBindings.Add("Text", DataSource, "D24", "");
            c_25.DataBindings.Add("Text", DataSource, "D25", "");
            c_26.DataBindings.Add("Text", DataSource, "D26", "");
            c_27.DataBindings.Add("Text", DataSource, "D27", "");
            c_28.DataBindings.Add("Text", DataSource, "D28", "");
            c_29.DataBindings.Add("Text", DataSource, "D29", "");
            c_30.DataBindings.Add("Text", DataSource, "D30", "");
            c_31.DataBindings.Add("Text", DataSource, "D31", "");
        }
    }
}

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
    public partial class rpt_3010 : DevExpress.XtraReports.UI.XtraReport
    {
        SilkRoad.Common.CustomFormatter cf = new SilkRoad.Common.CustomFormatter();
        SilkRoad.Common.DrawRptSignBox dsb = new SilkRoad.Common.DrawRptSignBox();
        CommonLibrary clib = new CommonLibrary();
        DataSet ds;
        DataProcFunc df = new DataProcFunc();

        public rpt_3010 (string yymm, string part, DataTable ds)
        {
            InitializeComponent();
            lb_yymm.Text = yymm.Substring(0, 4) + "-" + yymm.Substring(4, 2);
            lb_part.Text = part;

			#region 요일표기
			//1.기준년월에 따른 하단 일자컬럼header 일자, 요일 설정
			int lastday = clib.TextToInt(clib.DateToText(clib.TextToDateLast(yymm)).Substring(6, 2));
			
			if (lastday == 28)
			{
				title_29.Text = "";
				title_30.Text = "";
				title_31.Text = "";
			}
			else if (lastday == 29)
			{
				title_30.Text = "";
				title_31.Text = "";
			}
			else if (lastday == 30)
			{
				title_31.Text = "";
			}
			
			string[,] days = new string[2, 32];
			for (int i = 1; i <= lastday; i++)
			{
				days[0, i] = ds.Select("H_DATE = '" + yymm.Substring(0, 6) + i.ToString().PadLeft(2, '0') + "'").Length > 0 ? "1" : ""; 
				days[1, i] = clib.WeekDay(clib.TextToDate(yymm.Substring(0, 6) + i.ToString().PadLeft(2, '0')));
			}
			day_01.Text = days[1, 1].ToString().Trim();
			day_02.Text = days[1, 2].ToString().Trim();
			day_03.Text = days[1, 3].ToString().Trim();
			day_04.Text = days[1, 4].ToString().Trim();
			day_05.Text = days[1, 5].ToString().Trim();
			day_06.Text = days[1, 6].ToString().Trim();
			day_07.Text = days[1, 7].ToString().Trim();
			day_08.Text = days[1, 8].ToString().Trim();
			day_09.Text = days[1, 9].ToString().Trim();
			day_10.Text = days[1, 10].ToString().Trim();
			day_11.Text = days[1, 11].ToString().Trim();
			day_12.Text = days[1, 12].ToString().Trim();
			day_13.Text = days[1, 13].ToString().Trim();
			day_14.Text = days[1, 14].ToString().Trim();
			day_15.Text = days[1, 15].ToString().Trim();
			day_16.Text = days[1, 16].ToString().Trim();
			day_17.Text = days[1, 17].ToString().Trim();
			day_18.Text = days[1, 18].ToString().Trim();
			day_19.Text = days[1, 19].ToString().Trim();
			day_20.Text = days[1, 20].ToString().Trim();
			day_21.Text = days[1, 21].ToString().Trim();
			day_22.Text = days[1, 22].ToString().Trim();
			day_23.Text = days[1, 23].ToString().Trim();
			day_24.Text = days[1, 24].ToString().Trim();
			day_25.Text = days[1, 25].ToString().Trim();
			day_26.Text = days[1, 26].ToString().Trim();
			day_27.Text = days[1, 27].ToString().Trim();
			day_28.Text = days[1, 28].ToString().Trim();
			day_29.Text = days[1, 29] == null ? "" : days[1, 29].ToString().Trim();
			day_30.Text = days[1, 30] == null ? "" : days[1, 30].ToString().Trim();
			day_31.Text = days[1, 31] == null ? "" : days[1, 31].ToString().Trim();
			title_01.ForeColor = days[1, 1].ToString().Trim() == "토" ? Color.Blue : days[1, 1].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_02.ForeColor = days[1, 2].ToString().Trim() == "토" ? Color.Blue : days[1, 2].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_03.ForeColor = days[1, 3].ToString().Trim() == "토" ? Color.Blue : days[1, 3].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_04.ForeColor = days[1, 4].ToString().Trim() == "토" ? Color.Blue : days[1, 4].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_05.ForeColor = days[1, 5].ToString().Trim() == "토" ? Color.Blue : days[1, 5].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_06.ForeColor = days[1, 6].ToString().Trim() == "토" ? Color.Blue : days[1, 6].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_07.ForeColor = days[1, 7].ToString().Trim() == "토" ? Color.Blue : days[1, 7].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_08.ForeColor = days[1, 8].ToString().Trim() == "토" ? Color.Blue : days[1, 8].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_09.ForeColor = days[1, 9].ToString().Trim() == "토" ? Color.Blue : days[1, 9].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_10.ForeColor = days[1, 10].ToString().Trim() == "토" ? Color.Blue : days[1, 10].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_11.ForeColor = days[1, 11].ToString().Trim() == "토" ? Color.Blue : days[1, 11].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_12.ForeColor = days[1, 12].ToString().Trim() == "토" ? Color.Blue : days[1, 12].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_13.ForeColor = days[1, 13].ToString().Trim() == "토" ? Color.Blue : days[1, 13].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_14.ForeColor = days[1, 14].ToString().Trim() == "토" ? Color.Blue : days[1, 14].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_15.ForeColor = days[1, 15].ToString().Trim() == "토" ? Color.Blue : days[1, 15].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_16.ForeColor = days[1, 16].ToString().Trim() == "토" ? Color.Blue : days[1, 16].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_17.ForeColor = days[1, 17].ToString().Trim() == "토" ? Color.Blue : days[1, 17].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_18.ForeColor = days[1, 18].ToString().Trim() == "토" ? Color.Blue : days[1, 18].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_19.ForeColor = days[1, 19].ToString().Trim() == "토" ? Color.Blue : days[1, 19].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_20.ForeColor = days[1, 20].ToString().Trim() == "토" ? Color.Blue : days[1, 20].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_21.ForeColor = days[1, 21].ToString().Trim() == "토" ? Color.Blue : days[1, 21].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_22.ForeColor = days[1, 22].ToString().Trim() == "토" ? Color.Blue : days[1, 22].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_23.ForeColor = days[1, 23].ToString().Trim() == "토" ? Color.Blue : days[1, 23].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_24.ForeColor = days[1, 24].ToString().Trim() == "토" ? Color.Blue : days[1, 24].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_25.ForeColor = days[1, 25].ToString().Trim() == "토" ? Color.Blue : days[1, 25].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_26.ForeColor = days[1, 26].ToString().Trim() == "토" ? Color.Blue : days[1, 26].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_27.ForeColor = days[1, 27].ToString().Trim() == "토" ? Color.Blue : days[1, 27].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_28.ForeColor = days[1, 28].ToString().Trim() == "토" ? Color.Blue : days[1, 28].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_29.ForeColor = days[1, 29] == null ? Color.Black : days[1, 29].ToString().Trim() == "토" ? Color.Blue : days[1, 29].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_30.ForeColor = days[1, 30] == null ? Color.Black : days[1, 30].ToString().Trim() == "토" ? Color.Blue : days[1, 30].ToString().Trim() == "일" ? Color.Red : Color.Black;
			title_31.ForeColor = days[1, 31] == null ? Color.Black : days[1, 31].ToString().Trim() == "토" ? Color.Blue : days[1, 31].ToString().Trim() == "일" ? Color.Red : Color.Black;

			title_01.BackColor = days[1, 1].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_02.BackColor = days[1, 2].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_03.BackColor = days[1, 3].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_04.BackColor = days[1, 4].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_05.BackColor = days[1, 5].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_06.BackColor = days[1, 6].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_07.BackColor = days[1, 7].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_08.BackColor = days[1, 8].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_09.BackColor = days[1, 9].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_10.BackColor = days[1, 10].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_11.BackColor = days[1, 11].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_12.BackColor = days[1, 12].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_13.BackColor = days[1, 13].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_14.BackColor = days[1, 14].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_15.BackColor = days[1, 15].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_16.BackColor = days[1, 16].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_17.BackColor = days[1, 17].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_18.BackColor = days[1, 18].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_19.BackColor = days[1, 19].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_20.BackColor = days[1, 20].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_21.BackColor = days[1, 21].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_22.BackColor = days[1, 22].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_23.BackColor = days[1, 23].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_24.BackColor = days[1, 24].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_25.BackColor = days[1, 25].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_26.BackColor = days[1, 26].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_27.BackColor = days[1, 27].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_28.BackColor = days[1, 28].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_29.BackColor = days[1, 29] == null ? Color.Transparent : days[1, 29].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_30.BackColor = days[1, 30] == null ? Color.Transparent : days[1, 30].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			title_31.BackColor = days[1, 31] == null ? Color.Transparent : days[1, 31].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;

			day_01.ForeColor = days[1, 1].ToString().Trim() == "토" ? Color.Blue : days[1, 1].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_02.ForeColor = days[1, 2].ToString().Trim() == "토" ? Color.Blue : days[1, 2].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_03.ForeColor = days[1, 3].ToString().Trim() == "토" ? Color.Blue : days[1, 3].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_04.ForeColor = days[1, 4].ToString().Trim() == "토" ? Color.Blue : days[1, 4].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_05.ForeColor = days[1, 5].ToString().Trim() == "토" ? Color.Blue : days[1, 5].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_06.ForeColor = days[1, 6].ToString().Trim() == "토" ? Color.Blue : days[1, 6].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_07.ForeColor = days[1, 7].ToString().Trim() == "토" ? Color.Blue : days[1, 7].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_08.ForeColor = days[1, 8].ToString().Trim() == "토" ? Color.Blue : days[1, 8].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_09.ForeColor = days[1, 9].ToString().Trim() == "토" ? Color.Blue : days[1, 9].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_10.ForeColor = days[1, 10].ToString().Trim() == "토" ? Color.Blue : days[1, 10].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_11.ForeColor = days[1, 11].ToString().Trim() == "토" ? Color.Blue : days[1, 11].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_12.ForeColor = days[1, 12].ToString().Trim() == "토" ? Color.Blue : days[1, 12].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_13.ForeColor = days[1, 13].ToString().Trim() == "토" ? Color.Blue : days[1, 13].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_14.ForeColor = days[1, 14].ToString().Trim() == "토" ? Color.Blue : days[1, 14].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_15.ForeColor = days[1, 15].ToString().Trim() == "토" ? Color.Blue : days[1, 15].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_16.ForeColor = days[1, 16].ToString().Trim() == "토" ? Color.Blue : days[1, 16].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_17.ForeColor = days[1, 17].ToString().Trim() == "토" ? Color.Blue : days[1, 17].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_18.ForeColor = days[1, 18].ToString().Trim() == "토" ? Color.Blue : days[1, 18].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_19.ForeColor = days[1, 19].ToString().Trim() == "토" ? Color.Blue : days[1, 19].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_20.ForeColor = days[1, 20].ToString().Trim() == "토" ? Color.Blue : days[1, 20].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_21.ForeColor = days[1, 21].ToString().Trim() == "토" ? Color.Blue : days[1, 21].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_22.ForeColor = days[1, 22].ToString().Trim() == "토" ? Color.Blue : days[1, 22].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_23.ForeColor = days[1, 23].ToString().Trim() == "토" ? Color.Blue : days[1, 23].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_24.ForeColor = days[1, 24].ToString().Trim() == "토" ? Color.Blue : days[1, 24].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_25.ForeColor = days[1, 25].ToString().Trim() == "토" ? Color.Blue : days[1, 25].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_26.ForeColor = days[1, 26].ToString().Trim() == "토" ? Color.Blue : days[1, 26].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_27.ForeColor = days[1, 27].ToString().Trim() == "토" ? Color.Blue : days[1, 27].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_28.ForeColor = days[1, 28].ToString().Trim() == "토" ? Color.Blue : days[1, 28].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_29.ForeColor = days[1, 29] == null ? Color.Black : days[1, 29].ToString().Trim() == "토" ? Color.Blue : days[1, 29].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_30.ForeColor = days[1, 30] == null ? Color.Black : days[1, 30].ToString().Trim() == "토" ? Color.Blue : days[1, 30].ToString().Trim() == "일" ? Color.Red : Color.Black;
			day_31.ForeColor = days[1, 31] == null ? Color.Black : days[1, 31].ToString().Trim() == "토" ? Color.Blue : days[1, 31].ToString().Trim() == "일" ? Color.Red : Color.Black;
			
			day_01.BackColor = days[1, 1].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_02.BackColor = days[1, 2].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_03.BackColor = days[1, 3].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_04.BackColor = days[1, 4].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_05.BackColor = days[1, 5].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_06.BackColor = days[1, 6].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_07.BackColor = days[1, 7].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_08.BackColor = days[1, 8].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_09.BackColor = days[1, 9].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_10.BackColor = days[1, 10].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_11.BackColor = days[1, 11].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_12.BackColor = days[1, 12].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_13.BackColor = days[1, 13].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_14.BackColor = days[1, 14].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_15.BackColor = days[1, 15].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_16.BackColor = days[1, 16].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_17.BackColor = days[1, 17].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_18.BackColor = days[1, 18].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_19.BackColor = days[1, 19].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_20.BackColor = days[1, 20].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_21.BackColor = days[1, 21].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_22.BackColor = days[1, 22].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_23.BackColor = days[1, 23].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_24.BackColor = days[1, 24].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_25.BackColor = days[1, 25].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_26.BackColor = days[1, 26].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_27.BackColor = days[1, 27].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_28.BackColor = days[1, 28].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_29.BackColor = days[1, 29] == null ? Color.Transparent : days[1, 29].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_30.BackColor = days[1, 30] == null ? Color.Transparent : days[1, 30].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			day_31.BackColor = days[1, 31] == null ? Color.Transparent : days[1, 31].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;

			c_01.BackColor = days[1, 1].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_02.BackColor = days[1, 2].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_03.BackColor = days[1, 3].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_04.BackColor = days[1, 4].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_05.BackColor = days[1, 5].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_06.BackColor = days[1, 6].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_07.BackColor = days[1, 7].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_08.BackColor = days[1, 8].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_09.BackColor = days[1, 9].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_10.BackColor = days[1, 10].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_11.BackColor = days[1, 11].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_12.BackColor = days[1, 12].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_13.BackColor = days[1, 13].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_14.BackColor = days[1, 14].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_15.BackColor = days[1, 15].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_16.BackColor = days[1, 16].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_17.BackColor = days[1, 17].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_18.BackColor = days[1, 18].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_19.BackColor = days[1, 19].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_20.BackColor = days[1, 20].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_21.BackColor = days[1, 21].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_22.BackColor = days[1, 22].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_23.BackColor = days[1, 23].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_24.BackColor = days[1, 24].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_25.BackColor = days[1, 25].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_26.BackColor = days[1, 26].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_27.BackColor = days[1, 27].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_28.BackColor = days[1, 28].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_29.BackColor = days[1, 29] == null ? Color.Transparent : days[1, 29].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_30.BackColor = days[1, 30] == null ? Color.Transparent : days[1, 30].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;
			c_31.BackColor = days[1, 31] == null ? Color.Transparent : days[1, 31].ToString().Trim() == "일" ? Color.MistyRose : Color.Transparent;

			if (days[0, 1] != null && days[0, 1].ToString() == "1")
			{
				title_01.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_01.ForeColor = Color.Red;
				title_01.BackColor = Color.MistyRose;
				day_01.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_01.ForeColor = Color.Red;
				day_01.BackColor = Color.MistyRose;
				c_01.BackColor = Color.MistyRose;
			}
			if (days[0, 2] != null && days[0, 2].ToString() == "1")
			{
				title_02.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_02.ForeColor = Color.Red;
				title_02.BackColor = Color.MistyRose;
				day_02.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_02.ForeColor = Color.Red;
				day_02.BackColor = Color.MistyRose;
				c_02.BackColor = Color.MistyRose;
			}
			if (days[0, 3] != null && days[0, 3].ToString() == "1")
			{
				title_03.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_03.ForeColor = Color.Red;
				title_03.BackColor = Color.MistyRose;
				day_03.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_03.ForeColor = Color.Red;
				day_03.BackColor = Color.MistyRose;
				c_03.BackColor = Color.MistyRose;
			}
			if (days[0, 4] != null && days[0, 4].ToString() == "1")
			{
				title_04.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_04.ForeColor = Color.Red;
				title_04.BackColor = Color.MistyRose;
				day_04.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_04.ForeColor = Color.Red;
				day_04.BackColor = Color.MistyRose;
				c_04.BackColor = Color.MistyRose;
			}
			if (days[0, 5] != null && days[0, 5].ToString() == "1")
			{
				title_05.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_05.ForeColor = Color.Red;
				title_05.BackColor = Color.MistyRose;
				day_05.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_05.ForeColor = Color.Red;
				day_05.BackColor = Color.MistyRose;
				c_05.BackColor = Color.MistyRose;
			}
			if (days[0, 6] != null && days[0, 6].ToString() == "1")
			{
				title_06.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_06.ForeColor = Color.Red;
				title_06.BackColor = Color.MistyRose;
				day_06.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_06.ForeColor = Color.Red;
				day_06.BackColor = Color.MistyRose;
				c_06.BackColor = Color.MistyRose;
			}
			if (days[0, 7] != null && days[0, 7].ToString() == "1")
			{
				title_07.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_07.ForeColor = Color.Red;
				title_07.BackColor = Color.MistyRose;
				day_07.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_07.ForeColor = Color.Red;
				day_07.BackColor = Color.MistyRose;
				c_07.BackColor = Color.MistyRose;
			}
			if (days[0, 8] != null && days[0, 8].ToString() == "1")
			{
				title_08.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_08.ForeColor = Color.Red;
				title_08.BackColor = Color.MistyRose;
				day_08.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_08.ForeColor = Color.Red;
				day_08.BackColor = Color.MistyRose;
				c_08.BackColor = Color.MistyRose;
			}
			if (days[0, 9] != null && days[0, 9].ToString() == "1")
			{
				title_09.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_09.ForeColor = Color.Red;
				title_09.BackColor = Color.MistyRose;
				day_09.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_09.ForeColor = Color.Red;
				day_09.BackColor = Color.MistyRose;
				c_09.BackColor = Color.MistyRose;
			}
			if (days[0, 10] != null && days[0, 10].ToString() == "1")
			{
				title_10.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_10.ForeColor = Color.Red;
				title_10.BackColor = Color.MistyRose;
				day_10.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_10.ForeColor = Color.Red;
				day_10.BackColor = Color.MistyRose;
				c_10.BackColor = Color.MistyRose;
			}
			if (days[0, 11] != null && days[0, 11].ToString() == "1")
			{
				title_11.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_11.ForeColor = Color.Red;
				title_11.BackColor = Color.MistyRose;
				day_11.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_11.ForeColor = Color.Red;
				day_11.BackColor = Color.MistyRose;
				c_11.BackColor = Color.MistyRose;
			}
			if (days[0, 12] != null && days[0, 12].ToString() == "1")
			{
				title_12.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_12.ForeColor = Color.Red;
				title_12.BackColor = Color.MistyRose;
				day_12.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_12.ForeColor = Color.Red;
				day_12.BackColor = Color.MistyRose;
				c_12.BackColor = Color.MistyRose;
			}
			if (days[0, 13] != null && days[0, 13].ToString() == "1")
			{
				title_13.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_13.ForeColor = Color.Red;
				title_13.BackColor = Color.MistyRose;
				day_13.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_13.ForeColor = Color.Red;
				day_13.BackColor = Color.MistyRose;
				c_13.BackColor = Color.MistyRose;
			}
			if (days[0, 14] != null && days[0, 14].ToString() == "1")
			{
				title_14.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_14.ForeColor = Color.Red;
				title_14.BackColor = Color.MistyRose;
				day_14.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_14.ForeColor = Color.Red;
				day_14.BackColor = Color.MistyRose;
				c_14.BackColor = Color.MistyRose;
			}
			if (days[0, 15] != null && days[0, 15].ToString() == "1")
			{
				title_15.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_15.ForeColor = Color.Red;
				title_15.BackColor = Color.MistyRose;
				day_15.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_15.ForeColor = Color.Red;
				day_15.BackColor = Color.MistyRose;
				c_15.BackColor = Color.MistyRose;
			}
			if (days[0, 16] != null && days[0, 16].ToString() == "1")
			{
				title_16.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_16.ForeColor = Color.Red;
				title_16.BackColor = Color.MistyRose;
				day_16.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_16.ForeColor = Color.Red;
				day_16.BackColor = Color.MistyRose;
				c_16.BackColor = Color.MistyRose;
			}
			if (days[0, 17] != null && days[0, 17].ToString() == "1")
			{
				title_17.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_17.ForeColor = Color.Red;
				title_17.BackColor = Color.MistyRose;
				day_17.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_17.ForeColor = Color.Red;
				day_17.BackColor = Color.MistyRose;
				c_17.BackColor = Color.MistyRose;
			}
			if (days[0, 18] != null && days[0, 18].ToString() == "1")
			{
				title_18.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_18.ForeColor = Color.Red;
				title_18.BackColor = Color.MistyRose;
				day_18.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_18.ForeColor = Color.Red;
				day_18.BackColor = Color.MistyRose;
				c_18.BackColor = Color.MistyRose;
			}
			if (days[0, 19] != null && days[0, 19].ToString() == "1")
			{
				title_19.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_19.ForeColor = Color.Red;
				title_19.BackColor = Color.MistyRose;
				day_19.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_19.ForeColor = Color.Red;
				day_19.BackColor = Color.MistyRose;
				c_19.BackColor = Color.MistyRose;
			}
			if (days[0, 20] != null && days[0, 20].ToString() == "1")
			{
				title_20.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_20.ForeColor = Color.Red;
				title_20.BackColor = Color.MistyRose;
				day_20.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_20.ForeColor = Color.Red;
				day_20.BackColor = Color.MistyRose;
				c_20.BackColor = Color.MistyRose;
			}
			if (days[0, 21] != null && days[0, 21].ToString() == "1")
			{
				title_21.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_21.ForeColor = Color.Red;
				title_21.BackColor = Color.MistyRose;
				day_21.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_21.ForeColor = Color.Red;
				day_21.BackColor = Color.MistyRose;
				c_21.BackColor = Color.MistyRose;
			}
			if (days[0, 22] != null && days[0, 22].ToString() == "1")
			{
				title_22.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_22.ForeColor = Color.Red;
				title_22.BackColor = Color.MistyRose;
				day_22.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_22.ForeColor = Color.Red;
				day_22.BackColor = Color.MistyRose;
				c_22.BackColor = Color.MistyRose;
			}
			if (days[0, 23] != null && days[0, 23].ToString() == "1")
			{
				title_23.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_23.ForeColor = Color.Red;
				title_23.BackColor = Color.MistyRose;
				day_23.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_23.ForeColor = Color.Red;
				day_23.BackColor = Color.MistyRose;
				c_23.BackColor = Color.MistyRose;
			}
			if (days[0, 24] != null && days[0, 24].ToString() == "1")
			{
				title_24.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_24.ForeColor = Color.Red;
				title_24.BackColor = Color.MistyRose;
				day_24.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_24.ForeColor = Color.Red;
				day_24.BackColor = Color.MistyRose;
				c_24.BackColor = Color.MistyRose;
			}
			if (days[0, 25] != null && days[0, 25].ToString() == "1")
			{
				title_25.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_25.ForeColor = Color.Red;
				title_25.BackColor = Color.MistyRose;
				day_25.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_25.ForeColor = Color.Red;
				day_25.BackColor = Color.MistyRose;
				c_25.BackColor = Color.MistyRose;
			}
			if (days[0, 26] != null && days[0, 26].ToString() == "1")
			{
				title_26.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_26.ForeColor = Color.Red;
				title_26.BackColor = Color.MistyRose;
				day_26.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_26.ForeColor = Color.Red;
				day_26.BackColor = Color.MistyRose;
				c_26.BackColor = Color.MistyRose;
			}
			if (days[0, 27] != null && days[0, 27].ToString() == "1")
			{
				title_27.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_27.ForeColor = Color.Red;
				title_27.BackColor = Color.MistyRose;
				day_27.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_27.ForeColor = Color.Red;
				day_27.BackColor = Color.MistyRose;
				c_27.BackColor = Color.MistyRose;
			}
			if (days[0, 28] != null && days[0, 28].ToString() == "1")
			{
				title_28.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_28.ForeColor = Color.Red;
				title_28.BackColor = Color.MistyRose;
				day_28.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_28.ForeColor = Color.Red;
				day_28.BackColor = Color.MistyRose;
				c_28.BackColor = Color.MistyRose;
			}
			if (days[0, 29] != null && days[0, 29].ToString() == "1")
			{
				title_29.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_29.ForeColor = Color.Red;
				title_29.BackColor = Color.MistyRose;
				day_29.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_29.ForeColor = Color.Red;
				day_29.BackColor = Color.MistyRose;
				c_29.BackColor = Color.MistyRose;
			}
			if (days[0, 30] != null && days[0, 30].ToString() == "1")
			{
				title_30.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_30.ForeColor = Color.Red;
				title_30.BackColor = Color.MistyRose;
				day_30.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_30.ForeColor = Color.Red;
				day_30.BackColor = Color.MistyRose;
				c_30.BackColor = Color.MistyRose;
			}
			if (days[0, 31] != null && days[0, 31].ToString() == "1")
			{
				title_31.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				title_31.ForeColor = Color.Red;
				title_31.BackColor = Color.MistyRose;
				day_31.Font = new Font("맑은 고딕", 8, FontStyle.Underline);
				day_31.ForeColor = Color.Red;
				day_31.BackColor = Color.MistyRose;
				c_31.BackColor = Color.MistyRose;
			}

			#endregion

			//c_name.DataBindings.Add("Text", DataSource, "SAWON_NM", "");
			c_01.DataBindings.Add("Text", DataSource, "D01_NM", "");
            c_02.DataBindings.Add("Text", DataSource, "D02_NM", "");
            c_03.DataBindings.Add("Text", DataSource, "D03_NM", "");
            c_04.DataBindings.Add("Text", DataSource, "D04_NM", "");
            c_05.DataBindings.Add("Text", DataSource, "D05_NM", "");
            c_06.DataBindings.Add("Text", DataSource, "D06_NM", "");
            c_07.DataBindings.Add("Text", DataSource, "D07_NM", "");
            c_08.DataBindings.Add("Text", DataSource, "D08_NM", "");
            c_09.DataBindings.Add("Text", DataSource, "D09_NM", "");
            c_10.DataBindings.Add("Text", DataSource, "D10_NM", "");
            c_11.DataBindings.Add("Text", DataSource, "D11_NM", "");
            c_12.DataBindings.Add("Text", DataSource, "D12_NM", "");
            c_13.DataBindings.Add("Text", DataSource, "D13_NM", "");
            c_14.DataBindings.Add("Text", DataSource, "D14_NM", "");
            c_15.DataBindings.Add("Text", DataSource, "D15_NM", "");
            c_16.DataBindings.Add("Text", DataSource, "D16_NM", "");
            c_17.DataBindings.Add("Text", DataSource, "D17_NM", "");
            c_18.DataBindings.Add("Text", DataSource, "D18_NM", "");
            c_19.DataBindings.Add("Text", DataSource, "D19_NM", "");
            c_20.DataBindings.Add("Text", DataSource, "D20_NM", "");
            c_21.DataBindings.Add("Text", DataSource, "D21_NM", "");
            c_22.DataBindings.Add("Text", DataSource, "D22_NM", "");
            c_23.DataBindings.Add("Text", DataSource, "D23_NM", "");
            c_24.DataBindings.Add("Text", DataSource, "D24_NM", "");
            c_25.DataBindings.Add("Text", DataSource, "D25_NM", "");
            c_26.DataBindings.Add("Text", DataSource, "D26_NM", "");
            c_27.DataBindings.Add("Text", DataSource, "D27_NM", "");
            c_28.DataBindings.Add("Text", DataSource, "D28_NM", "");
            c_29.DataBindings.Add("Text", DataSource, "D29_NM", "");
            c_30.DataBindings.Add("Text", DataSource, "D30_NM", "");
            c_31.DataBindings.Add("Text", DataSource, "D31_NM", "");
        }

        public void writeReport(DataSet ds, int print)
        {
        }

        private void xrTableCell1_BeforePrint(string yymm, string part)
        {
            //if (GetCurrentColumnValue("사진").ToString().Trim() != "")
            //{
            //    byte[] myByte = new byte[0];

            //    myByte = (byte[])GetCurrentColumnValue("사진");
            //    MemoryStream ImgStream = new MemoryStream(myByte);
            //    xrPictureBox1.Image = Image.FromStream(ImgStream);
            //}
            //else
            //{
            //    xrPictureBox1.Image = null;
            //}
        }

        private void xrTable3_Draw(string yymm, string part, DrawEventArgs e)
        {
            InitializeComponent();
            lb_part.Text = part;
            lb_yymm.Text = yymm;
           
        }
    }
}

using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using System.Data.OleDb;
using System.Collections;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid;

namespace DUTY1000
{
    public partial class duty3060 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();
		static DataProcessing dp = new DataProcessing();

        public duty3060()
        {
            InitializeComponent();
        }

        #region 0. Initialization

        private void SetCancel()
        {
            if (ds.Tables["3060_SEARCH"] != null)
                ds.Tables["3060_SEARCH"].Clear();
            grd_gt.DataSource = null;

            df.Get2060_DANG_GNMUDatas(ds);
			grd_sl_gnmu.DataSource = ds.Tables["2060_DANG_GNMU"];
		}

        #endregion

        #region 1 Form

        private void duty3060_Load(object sender, EventArgs e)
        {
			dat_yymm.DateTime = DateTime.Now;
        }
		private void duty3060_Shown(object sender, EventArgs e)
		{
			SetCancel();
		}

		#endregion

		#region 2 Button
		
		//근무표조회
		private void btn_search_Click(object sender, EventArgs e)
		{
			if (isNoError_um(1))
			{
				df.GetSEARCH_HOLIDatas(clib.DateToText(dat_yymm.DateTime).Substring(0, 6), ds);
				//1.기준년월에 따른 하단 일자컬럼header 일자, 요일 설정
				int lastday = clib.TextToInt(clib.DateToText(clib.TextToDateLast(clib.DateToText(dat_yymm.DateTime))).Substring(6, 2));

				for (int i = 1; i <= lastday; i++)
				{
					DateTime day = clib.TextToDate(clib.DateToText(dat_yymm.DateTime).Substring(0, 6) + "01").AddDays(i - 1);
					grdv_gt.Columns["D" + i.ToString().PadLeft(2, '0')].Caption = i.ToString() + "\r\n" + clib.WeekDay(day); //일+요일. 한칸 내려서 보이도록. 엔터마냥. 
					if (clib.WeekDay(day) == "토")					
						grdv_gt.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.ForeColor = Color.Blue;
					else if (clib.WeekDay(day) == "일")					
						grdv_gt.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.ForeColor = Color.Red;
					else					
						grdv_gt.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.ForeColor = Color.Black;

					if (ds.Tables["SEARCH_HOLI"].Select("H_DATE = '" + clib.DateToText(day) + "'").Length > 0)
					{
						grdv_gt.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.ForeColor = Color.Red;
						grdv_gt.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.Font = new Font(FontFamily.GenericSansSerif, 9F, FontStyle.Underline);
						grdv_gt.Columns["D" + i.ToString().PadLeft(2, '0')].ToolTip = ds.Tables["SEARCH_HOLI"].Select("H_DATE = '" + clib.DateToText(day) + "'")[0]["H_NAME"].ToString();
					}
				}

				//남은 필드 visible = false;
				for (int k = 1; k < 32; k++)
				{
					if (k > lastday)					
						grdv_gt.Columns["D" + k.ToString().PadLeft(2, '0')].Visible = false;
					else					
						grdv_gt.Columns["D" + k.ToString().PadLeft(2, '0')].Visible = true;
				}
				
				df.Get2060_DANG_GNMUDatas(ds);
				//df.Get5010_SEARCH3_KTDatas(clib.DateToText(dat_yymm3.DateTime).Substring(0, 6), ds);

				df.Get3060_SEARCHDatas(clib.DateToText(dat_yymm.DateTime).Substring(0, 6), cmb_sq.SelectedIndex + 1, ds);
				grd_gt.DataSource = ds.Tables["3060_SEARCH"];
			}

            df.Get3060_CHK1Datas(clib.DateToText(dat_yymm.DateTime).Substring(0, 6), ds);
            grd1.DataSource = ds.Tables["3060_CHK1"];

            df.Get3060_CHK2Datas(clib.DateToText(dat_yymm.DateTime).Substring(0, 6), ds);
            grd2.DataSource = ds.Tables["3060_CHK2"];
        }
		//취소
		private void btn_canc_Click(object sender, EventArgs e)
		{
			SetCancel();
		}
		//엑셀변환
		private void btn_excel_Click(object sender, EventArgs e)
		{
            clib.gridToExcel(grdv_gt, "근무표조회_" + clib.DateToText(DateTime.Now), true);
		}

        private void btn_errchk_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region 3 EVENT

        private void duty3060_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //종료
            {
                btn_exit.PerformClick();
            }
        }

		private void grdv_gt_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
		{			
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)            
                e.Info.DisplayText = (e.RowHandle + 1).ToString();     
		}
        //근무값별 색 주기 ★
        private void grdv_gt_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name.StartsWith("grdcol_day"))
            {
				string stdt = clib.DateToText(DateTime.Now);
				string date = clib.DateToText(dat_yymm.DateTime).Substring(0, 6) + e.Column.Name.ToString().Substring(10, 2);
				string sabn = grdv_gt.GetDataRow(e.RowHandle)["SAWON_NO"].ToString().Trim();

                if (ds.Tables["2060_DANG_GNMU"].Select("G_CODE = '" + e.CellValue + "'").Length > 0)
                {
                    int colVAlue = clib.TextToInt(ds.Tables["2060_DANG_GNMU"].Select("G_CODE = '" + e.CellValue + "'")[0]["G_COLOR"].ToString());
                    e.Appearance.BackColor = Color.FromArgb(colVAlue);
                }

    //            if (ds.Tables["5010_SEARCH3_KT"].Select("SABN = '" + sabn + "' AND SLDT = '" + date + "'").Length > 0)  //출근시 체크
				//{
				//	if (ds.Tables["2060_DANG_GNMU"].Select("G_CODE = '" + e.CellValue + "'") != null && ds.Tables["2060_DANG_GNMU"].Select("G_CODE = '" + e.CellValue + "'").Length > 0)
				//	{
				//		int g_type = clib.TextToInt(ds.Tables["2060_DANG_GNMU"].Select("G_CODE = '" + e.CellValue + "'")[0]["G_TYPE"].ToString());
				//		if (g_type == 2 || g_type == 4 || g_type == 5 || g_type == 6 || g_type == 9 || g_type == 10) //근무 or 당직근무인데 출근이면 근무색상
				//		{
				//			int colVAlue = clib.TextToInt(ds.Tables["2060_DANG_GNMU"].Select("G_CODE = '" + e.CellValue + "'")[0]["G_COLOR"].ToString());
				//			e.Appearance.BackColor = Color.FromArgb(colVAlue);
				//		}
				//		else //근무가 아닌데 출근이면
				//		{
				//			if ((g_type == 8 || g_type == 12) && clib.TextToDecimal(ds.Tables["2060_DANG_GNMU"].Select("G_CODE = '" + e.CellValue + "'")[0]["YC_DAY"].ToString()) < 8) //반차or시간 출근이면
				//			{
				//				int colVAlue = clib.TextToInt(ds.Tables["2060_DANG_GNMU"].Select("G_CODE = '" + e.CellValue + "'")[0]["G_COLOR"].ToString());
				//				e.Appearance.BackColor = Color.FromArgb(colVAlue);
				//			}
				//			else
				//				e.Appearance.BackColor = Color.Red;
				//		}
				//	}
				//}
				//else  //출근하지 않았을때 체크
				//{
				//	if (ds.Tables["2060_DANG_GNMU"].Select("G_CODE = '" + e.CellValue + "'") != null && ds.Tables["2060_DANG_GNMU"].Select("G_CODE = '" + e.CellValue + "'").Length > 0)
				//	{
				//		int g_type = clib.TextToInt(ds.Tables["2060_DANG_GNMU"].Select("G_CODE = '" + e.CellValue + "'")[0]["G_TYPE"].ToString());
				//		if (g_type == 2 || g_type == 4 || g_type == 5 || g_type == 6 || g_type == 9 || g_type == 10) //근무 or 당직근무인데 출근하지 않으면 붉은색
				//		{
				//			if (clib.TextToDate(date) <= clib.TextToDate(stdt))  //현재일 이전일때 오류색상
				//				e.Appearance.BackColor = Color.Red;
				//			else  //현재일 이후는 근무색상
				//			{
				//				int colVAlue = clib.TextToInt(ds.Tables["2060_DANG_GNMU"].Select("G_CODE = '" + e.CellValue + "'")[0]["G_COLOR"].ToString());
				//				e.Appearance.BackColor = Color.FromArgb(colVAlue);
				//			}
				//		}
				//		else  // 근무가 아니면서 출근하지 않았을땐 근무색상으로
				//		{
				//			int colVAlue = clib.TextToInt(ds.Tables["2060_DANG_GNMU"].Select("G_CODE = '" + e.CellValue + "'")[0]["G_COLOR"].ToString());
				//			e.Appearance.BackColor = Color.FromArgb(colVAlue);
				//		}
				//	}
				//}
			}
        }
		
		private void grdv_gt_RowClick(object sender, RowClickEventArgs e)
		{
            if (e.RowHandle < 0)
                return;

            DataRow drow = grdv_gt.GetFocusedDataRow();
			//df.Get3010_KT_DTDatas(clib.DateToText(dat_yymm3.DateTime).Substring(0, 6), drow["SAWON_NO"].ToString().Trim(), ds);
			//grd_kt1.DataSource = ds.Tables["3010_KT_DT1"];
			//grd_kt2.DataSource = ds.Tables["3010_KT_DT2"];
		}

		#endregion		

        #region 7. Error Check

        /// <summary>
        /// 모드에 따른 컨트롤 유효성체크
        /// </summary>
        /// <param name="mode">1:처리모드(키값검사), 2:입력,수정모드 </param>
        /// <returns></returns>
        private bool isNoError_um(int mode)
        {
            bool isError = true;
            if (mode == 1) //조회
            {
                if (clib.DateToText(dat_yymm.DateTime) == "")
                {
                    MessageBox.Show("조회년월을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dat_yymm.Focus();
                    return false;
				}
                else
                {
                    isError = true;
                }
            }

            return isError;
        }

        #endregion

        #region 9. ETC

        #endregion

    }
}

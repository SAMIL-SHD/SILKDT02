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
    public partial class duty5010 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();
		static DataProcessing dp = new DataProcessing();

        public duty5010()
        {
            InitializeComponent();
        }

        #region 0. Initialization

        private void SetCancel(int stat)
		{
			if (stat == 1)
			{
				if (ds.Tables["SEARCH_KT1"] != null)
					ds.Tables["SEARCH_KT1"].Clear();
				grd1.DataSource = null;
			}
			else if (stat == 2)
			{
				if (ds.Tables["SEARCH_KT2"] != null)
					ds.Tables["SEARCH_KT2"].Clear();
				if (ds.Tables["C_KT2"] != null)
					ds.Tables["C_KT2"].Clear();
				grd2.DataSource = null;
				dat_end.Enabled = false;
				btn_wage.Enabled = false;
			}
			else if (stat == 3)
			{
				if (ds.Tables["5010_SEARCH3"] != null)
					ds.Tables["5010_SEARCH3"].Clear();
				if (ds.Tables["3010_KT_DT1"] != null)
					ds.Tables["3010_KT_DT1"].Clear();
				if (ds.Tables["3010_KT_DT2"] != null)
					ds.Tables["3010_KT_DT2"].Clear();
				grd_gt.DataSource = null;
			}
			df.GetGNMU_LISTDatas(ds);
			grd_sl_gnmu.DataSource = ds.Tables["GNMU_LIST"];
		}

        #endregion

        #region 1 Form

        private void duty5010_Load(object sender, EventArgs e)
        {
			dat_yymm.DateTime = DateTime.Now;
			
			dat_frdt.DateTime = clib.TextToDate(clib.DateToText(DateTime.Now.AddMonths(-1)).Substring(0, 6) + "21");
			dat_todt.DateTime = clib.TextToDate(clib.DateToText(DateTime.Now).Substring(0, 6) + "20");
			
			dat_yymm3.DateTime = DateTime.Now;
        }
		private void duty5010_Shown(object sender, EventArgs e)
		{
			SetCancel(1);
		}

		#endregion

		#region 2 Button
						
		//출퇴근조회
		private void btn_search_Click(object sender, EventArgs e)
		{
			if (isNoError_um(1))
			{
				df.GetSEARCH_KT1Datas(clib.DateToText(dat_yymm.DateTime).Substring(0, 6), ds);
				pv_grd1.DataSource = ds.Tables["SEARCH_KT1"];
				grd1.DataSource = ds.Tables["SEARCH_KT1"];
			}
		}
		//취소
		private void btn_canc_Click(object sender, EventArgs e)
		{
			SetCancel(1);
		}
		//엑셀변환
		private void btn_excel_Click(object sender, EventArgs e)
		{
            clib.gridToExcel(grdv1, "출퇴근내역_" + clib.DateToText(DateTime.Now), true);
		}
		
		//식수조회
		private void btn_s_search_Click(object sender, EventArgs e)
		{
			if (isNoError_um(2))
			{
				df.GetSEARCH_KT2Datas(clib.DateToText(dat_frdt.DateTime), clib.DateToText(dat_todt.DateTime), ds);
				grd2.DataSource = ds.Tables["SEARCH_KT2"];
				if (ds.Tables["SEARCH_KT2"].Rows.Count > 0)
				{
					DataTable _dt = new DataTable();
					_dt.Columns.Add("SABN"); 
					_dt.Columns.Add("USERNAME");
					_dt.Columns.Add("T_CNT", Type.GetType("System.Decimal"));
					_dt.Columns.Add("EMBS_STAT");
					dp.AddDatatable2Dataset("C_KT2", _dt, ref ds);

					foreach (DataRow drow in ds.Tables["SEARCH_KT2"].Rows)
					{
						DataRow srow;
						if (ds.Tables["C_KT2"].Select("SABN = '" + drow["SABN"].ToString().Trim() + "'").Length > 0)
						{
							srow = ds.Tables["C_KT2"].Select("SABN = '" + drow["SABN"].ToString().Trim() + "'")[0];
							srow["T_CNT"] = clib.TextToDecimal(srow["T_CNT"].ToString()) + 1;
							
						}
						else
						{
							srow = ds.Tables["C_KT2"].NewRow();
							srow["SABN"] = drow["SABN"].ToString().Trim();
							srow["USERNAME"] = drow["USERNAME"].ToString().Trim();
							srow["EMBS_STAT"] = drow["EMBS_STAT"].ToString().Trim();
							srow["T_CNT"] = 1;
							ds.Tables["C_KT2"].Rows.Add(srow);
						}
					}
					grd2_dt.DataSource = ds.Tables["C_KT2"];

					dat_end.DateTime = dat_todt.DateTime;
					dat_end.Enabled = true;
					btn_wage.Enabled = true;
				}
				else
				{
					dat_end.Enabled = false;
					btn_wage.Enabled = false;
				}
			}
		}

		//취소
		private void btn_s_canc_Click(object sender, EventArgs e)
		{
			SetCancel(2);
		}
		//엑셀변환
		private void btn_s_excel_Click(object sender, EventArgs e)
		{
			clib.gridToExcel(grdv2, "식수내역_"  + clib.DateToText(DateTime.Now), true);
		}
		//엑셀변환2
		private void btn_s_excel2_Click(object sender, EventArgs e)
		{
			clib.gridToExcel(grdv2_dt, "식수내역(횟수)_"  + clib.DateToText(DateTime.Now), true);
		}
		
		//급여전송(급여변동항목,월근태등록)
		private void btn_wage_Click(object sender, EventArgs e)
		{
			df.GetSEARCH_INFOSD02Datas(ds);
			DataRow grow = ds.Tables["SEARCH_INFOSD02"].Rows[0];
			
			if (clib.DateToText(dat_end.DateTime) == "")
			{
				MessageBox.Show("적용년월이 입력되지 않았습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			else if (ds.Tables["SEARCH_KT2"].Rows.Count < 1)
			{
				MessageBox.Show("전송할 데이터가 없습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			else if (grow["B01"].ToString().Trim() == "")
			{
				MessageBox.Show("수당산식설정의 식대공제 코드가 입력되지 않았습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			else if (clib.TextToDecimal(grow["B01_FEE01"].ToString()) == 0)
			{
				MessageBox.Show("수당산식설정의 식대금액이 입력되지 않았습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			else if (grow["C91"].ToString().Trim() == "")
			{
				MessageBox.Show("수당산식설정의 식사횟수 코드가 입력되지 않았습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			DialogResult dr = MessageBox.Show("조회된 내역을 인사급여로 전송하시겠습니까?\r\n\r\n인사기본정보에 등록되지 않은 직원은 제외됩니다.", "전송여부", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
			if (dr == DialogResult.OK)
			{
				Cursor = Cursors.WaitCursor;
				int outVal = 0;
				try
				{
					string end_yymm = clib.DateToText(dat_end.DateTime).Substring(0, 6);					

					DataRow nrow;
					for (int z = 0; z < ds.Tables["C_KT2"].Rows.Count; z++)
					{
						DataRow drow = ds.Tables["C_KT2"].Rows[z];
						
						df.GetMSTEMBSDatas(drow["SABN"].ToString().Trim(), ds);
						if (ds.Tables["MSTEMBS"].Rows.Count > 0)
						{
							df.GetMSTWGPCDatas(end_yymm, drow["SABN"].ToString().Trim(), ds);
							if (ds.Tables["MSTWGPC"].Select("WGPCYYMM = '" + end_yymm + "' AND WGPCSABN = '" + drow["SABN"].ToString().Trim() + "'").Length > 0)
							{
								nrow = ds.Tables["MSTWGPC"].Select("WGPCYYMM = '" + end_yymm + "' AND WGPCSABN = '" + drow["SABN"].ToString().Trim() + "'")[0];

								nrow["WGPCUPDT"] = gd.GetNow().Substring(0, 8);
								nrow["WGPCPSTY"] = "U";
							}
							else
							{
								nrow = ds.Tables["MSTWGPC"].NewRow();
								nrow["WGPCYYMM"] = end_yymm;
								nrow["WGPCSQNO"] = "1";
								nrow["WGPCSABN"] = drow["SABN"].ToString().Trim();
								for (int i = 1; i <= 50; i++)
								{
									nrow["WGPCSD" + i.ToString().PadLeft(2, '0')] = 0;
								}
								for (int i = 1; i <= 30; i++)
								{
									nrow["WGPCGJ" + i.ToString().PadLeft(2, '0')] = 0;
								}
								nrow["WGPCINDT"] = gd.GetNow().Substring(0, 8);
								nrow["WGPCUPDT"] = "";
								nrow["WGPCPSTY"] = "A";
								ds.Tables["MSTWGPC"].Rows.Add(nrow);
							}
							nrow["WGPCUSID"] = SilkRoad.Config.SRConfig.USID;
							if (grow["B01"].ToString().Trim() != "")
								nrow["WGPCGJ" + grow["B01"].ToString().Trim().Substring(2, 2)] = clib.TextToDecimal(drow["T_CNT"].ToString()) * clib.TextToDecimal(grow["B01_FEE01"].ToString());

							//근태 넘기기
							DataRow nrow2;
							df.GetMSTGTMMDatas(end_yymm, drow["SABN"].ToString().Trim(), ds);
							if (ds.Tables["MSTGTMM"].Select("GTMMYYMM = '" + end_yymm + "' AND GTMMSABN = '" + drow["SABN"].ToString().Trim() + "'").Length > 0)
							{
								nrow2 = ds.Tables["MSTGTMM"].Select("GTMMYYMM = '" + end_yymm + "' AND GTMMSABN = '" + drow["SABN"].ToString().Trim() + "'")[0];

								nrow2["GTMMUPDT"] = gd.GetNow().Substring(0, 8);
								nrow2["GTMMPSTY"] = "U";
							}
							else
							{
								nrow2 = ds.Tables["MSTGTMM"].NewRow();
								nrow2["GTMMYYMM"] = end_yymm;
								nrow2["GTMMSABN"] = drow["SABN"].ToString().Trim();

								for (int i = 1; i <= 30; i++)
								{
									nrow2["GTMMGT" + i.ToString().PadLeft(2, '0')] = 0;
								}

								nrow2["GTMMINDT"] = gd.GetNow().Substring(0, 8);
								nrow2["GTMMUPDT"] = "";
								nrow2["GTMMPSTY"] = "A";
								ds.Tables["MSTGTMM"].Rows.Add(nrow2);
							}

							nrow2["GTMMUSID"] = SilkRoad.Config.SRConfig.USID;
							if (grow["C91"].ToString().Trim() != "")
								nrow2["GTMMGT" + grow["C91"].ToString().Trim().Substring(2, 2)] = clib.TextToDecimal(drow["T_CNT"].ToString());

							string[] tableNames = new string[] { "MSTWGPC", "MSTGTMM" };
							SilkRoad.DbCmd_DT02.DbCmd_DT02 cmd = new SilkRoad.DbCmd_DT02.DbCmd_DT02();
							outVal += cmd.setUpdate(ref ds, tableNames, null);
						}
					}					
				}
				catch (Exception ec)
				{
					MessageBox.Show(ec.Message, "전송오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				finally
				{
					if (outVal > 0)
						MessageBox.Show("식대공제 내역이 전송되었습니다.", "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);

					//SetCancel();
					//Search();
					Cursor = Cursors.Default;
				}
			}
		}

		
		//근무표조회
		private void btn_search3_Click(object sender, EventArgs e)
		{
			if (isNoError_um(3))
			{
				df.GetSEARCH_HOLIDatas(clib.DateToText(dat_yymm3.DateTime).Substring(0, 6), ds);
				//1.기준년월에 따른 하단 일자컬럼header 일자, 요일 설정
				int lastday = clib.TextToInt(clib.DateToText(clib.TextToDateLast(clib.DateToText(dat_yymm3.DateTime))).Substring(6, 2));

				for (int i = 1; i <= lastday; i++)
				{
					DateTime day = clib.TextToDate(clib.DateToText(dat_yymm3.DateTime).Substring(0, 6) + "01").AddDays(i - 1);
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
				df.Get5010_SEARCH3_KTDatas(clib.DateToText(dat_yymm3.DateTime).Substring(0, 6), ds);

				df.Get5010_SEARCH3Datas(clib.DateToText(dat_yymm3.DateTime).Substring(0, 6), ds);
				grd_gt.DataSource = ds.Tables["5010_SEARCH3"];
			}
		}
		//취소
		private void btn_canc3_Click(object sender, EventArgs e)
		{
			SetCancel(3);
		}
		//엑셀변환
		private void btn_excel3_Click(object sender, EventArgs e)
		{
            clib.gridToExcel(grdv_gt, "근무표조회_" + clib.DateToText(DateTime.Now), true);
		}
        #endregion

        #region 3 EVENT

        private void duty5010_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //종료
            {
                btn_exit.PerformClick();
            }
        }
		
		private void pv_grd1_CellClick(object sender, DevExpress.XtraPivotGrid.PivotCellEventArgs e)
		{
            string sabn = pv_grd1.GetFieldValue(pivotGridField1, pv_grd1.Cells.FocusedCell.Y).ToString().Trim();
            string sldt = pv_grd1.GetFieldValue(pivotGridField4, pv_grd1.Cells.FocusedCell.X).ToString().Trim();

			if (ds.Tables["SEARCH_KT1"].Select("SABN = '" + sabn + "' AND SLDT = '" + sldt + "'").Length > 0)
			{
				grdv1.FocusedRowHandle = grdv1.LocateByValue("CHK_ROW", sabn+sldt, null);
				grdv1.SelectRows(grdv1.FocusedRowHandle, grdv1.FocusedRowHandle);
			}
		}
        private void pv_grd1_FieldValueDisplayText(object sender, DevExpress.XtraPivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal && e.DisplayText == "Grand Total")            
                e.DisplayText = "합계";            
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total || e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.CustomTotal)            
                e.DisplayText = "소계";            
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
				string date = clib.DateToText(dat_yymm3.DateTime).Substring(0, 6) + e.Column.Name.ToString().Substring(10, 2);
				string sabn = grdv_gt.GetDataRow(e.RowHandle)["SAWON_NO"].ToString().Trim();

				if (ds.Tables["5010_SEARCH3_KT"].Select("SABN = '" + sabn + "' AND SLDT = '" + date + "'").Length > 0)  //출근시 체크
				{
					if (ds.Tables["2060_DANG_GNMU"].Select("G_CODE = '" + e.CellValue + "'") != null && ds.Tables["2060_DANG_GNMU"].Select("G_CODE = '" + e.CellValue + "'").Length > 0)
					{
						int g_type = clib.TextToInt(ds.Tables["2060_DANG_GNMU"].Select("G_CODE = '" + e.CellValue + "'")[0]["G_TYPE"].ToString());
						if (g_type == 2 || g_type == 4 || g_type == 5 || g_type == 6 || g_type == 9 || g_type == 10) //근무 or 당직근무인데 출근이면 근무색상
						{
							int colVAlue = clib.TextToInt(ds.Tables["2060_DANG_GNMU"].Select("G_CODE = '" + e.CellValue + "'")[0]["G_COLOR"].ToString());
							e.Appearance.BackColor = Color.FromArgb(colVAlue);
						}
						else //근무가 아닌데 출근이면
						{
							if ((g_type == 8 || g_type == 12) && clib.TextToDecimal(ds.Tables["2060_DANG_GNMU"].Select("G_CODE = '" + e.CellValue + "'")[0]["YC_DAY"].ToString()) < 8) //반차or시간 출근이면
							{
								int colVAlue = clib.TextToInt(ds.Tables["2060_DANG_GNMU"].Select("G_CODE = '" + e.CellValue + "'")[0]["G_COLOR"].ToString());
								e.Appearance.BackColor = Color.FromArgb(colVAlue);
							}
							else
								e.Appearance.BackColor = Color.Red;
						}
					}
				}
				else  //출근하지 않았을때 체크
				{
					if (ds.Tables["2060_DANG_GNMU"].Select("G_CODE = '" + e.CellValue + "'") != null && ds.Tables["2060_DANG_GNMU"].Select("G_CODE = '" + e.CellValue + "'").Length > 0)
					{
						int g_type = clib.TextToInt(ds.Tables["2060_DANG_GNMU"].Select("G_CODE = '" + e.CellValue + "'")[0]["G_TYPE"].ToString());
						if (g_type == 2 || g_type == 4 || g_type == 5 || g_type == 6 || g_type == 9 || g_type == 10) //근무 or 당직근무인데 출근하지 않으면 붉은색
						{
							if (clib.TextToDate(date) <= clib.TextToDate(stdt))  //현재일 이전일때 오류색상
								e.Appearance.BackColor = Color.Red;
							else  //현재일 이후는 근무색상
							{
								int colVAlue = clib.TextToInt(ds.Tables["2060_DANG_GNMU"].Select("G_CODE = '" + e.CellValue + "'")[0]["G_COLOR"].ToString());
								e.Appearance.BackColor = Color.FromArgb(colVAlue);
							}
						}
						else  // 근무가 아니면서 출근하지 않았을땐 근무색상으로
						{
							int colVAlue = clib.TextToInt(ds.Tables["2060_DANG_GNMU"].Select("G_CODE = '" + e.CellValue + "'")[0]["G_COLOR"].ToString());
							e.Appearance.BackColor = Color.FromArgb(colVAlue);
						}
					}
				}
			}
        }
		
		private void grdv_gt_RowClick(object sender, RowClickEventArgs e)
		{
            if (e.RowHandle < 0)
                return;

            DataRow drow = grdv_gt.GetFocusedDataRow();
			df.Get3010_KT_DTDatas(clib.DateToText(dat_yymm3.DateTime).Substring(0, 6), drow["SAWON_NO"].ToString().Trim(), ds);
			grd_kt1.DataSource = ds.Tables["3010_KT_DT1"];
			grd_kt2.DataSource = ds.Tables["3010_KT_DT2"];
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
            if (mode == 1)  //조회
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
            else if (mode == 2) //조회
            {
                if (clib.DateToText(dat_frdt.DateTime) == "")
                {
                    MessageBox.Show("조회일자(FR)를 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dat_frdt.Focus();
                    return false;
				}
                else if (clib.DateToText(dat_todt.DateTime) == "")
                {
                    MessageBox.Show("조회일자(TO)를 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dat_todt.Focus();
                    return false;
				}
                else
                {
                    isError = true;
                }
            }
            else if (mode == 3) //조회
            {
                if (clib.DateToText(dat_yymm3.DateTime) == "")
                {
                    MessageBox.Show("조회년월을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dat_yymm3.Focus();
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

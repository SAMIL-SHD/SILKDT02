using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using System.Data.OleDb;
using System.Collections;

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
		}

        #endregion

        #region 1 Form

        private void duty5010_Load(object sender, EventArgs e)
        {
			dat_yymm.DateTime = DateTime.Now;
			
			dat_frdt.DateTime = clib.TextToDate(clib.DateToText(DateTime.Now.AddMonths(-1)).Substring(0, 6) + "21");
			dat_todt.DateTime = clib.TextToDate(clib.DateToText(DateTime.Now).Substring(0, 6) + "20");
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
					string end_yymm = clib.DateToText(dat_end.DateTime);					

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
							SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
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
        #endregion

        #region 3 EVENT

        private void duty5010_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //종료
            {
                btn_exit.PerformClick();
            }
        }
		
		private void grdv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{	
            DataRow drow = grdv1.GetFocusedDataRow();
            if (drow != null)
            {
				//YC_NOW_SUM_CNT YC_REMAIN_CNT
				drow["YC_NOW_SUM_CNT"] = clib.TextToDecimal(drow["YC_BF_SUM_CNT"].ToString()) + clib.TextToDecimal(drow["YC_THIS_YCNT"].ToString()) + clib.TextToDecimal(drow["YC_THIS_BCNT"].ToString());
				drow["YC_REMAIN_CNT"] = clib.TextToDecimal(drow["YC_T_CNT"].ToString()) - clib.TextToDecimal(drow["YC_NOW_SUM_CNT"].ToString());
			}

		}
		private void grdv_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{		
		}

		private void pv_grd_CustomFieldSort(object sender, DevExpress.XtraPivotGrid.PivotGridCustomFieldSortEventArgs e)
		{
			//if (e.Field == pivotGridField1)
   //         {
   //             object value1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "JONGCODE");
   //             object value2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "JONGCODE");
   //             int result = Comparer.Default.Compare(value1, value2);
   //             e.Result = result;
   //             e.Handled = true;
   //         }
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

            return isError;
        }

		#endregion

		#region 9. ETC

		#endregion
		
	}
}

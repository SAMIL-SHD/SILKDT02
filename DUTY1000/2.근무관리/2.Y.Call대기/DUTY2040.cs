using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using System.Text;
using DevExpress.XtraReports.UI;
using System.Drawing;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;

namespace DUTY1000
{
    public partial class duty2040 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        static DataProcessing dp = new DataProcessing();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();
        private string ends_yn = "";

        private string msyn = "";
        private string upyn = "";
        private string p_dpcd = "";

        public duty2040()
        {
            InitializeComponent();
        }

        #region 0. Initialization

        /// <summary>
        ///컨트롤 초기화 및 활성,비활성 설정
        /// </summary>
        /// <param name="enable"></param>
        private void SetCancel()
        {
			if (ds.Tables["SEARCH_CALL_PLAN"] != null)			
				ds.Tables["SEARCH_CALL_PLAN"].Clear();
			
			grd1.Enabled = false;
			grd2.Enabled = false;
			dat_yymm.Enabled = true;
			sl_dept.Enabled = p_dpcd == "%" ? true : false;
            sl_embs.Enabled = false;
			btn_lineadd.Enabled = false;
			btn_linedel.Enabled = false;
			btn_bf_plan.Enabled = false;
			chk_g_adt.Checked = false;
			
			Refresh_Click();
						
            for (int i = 1; i <= 31; i++)
            {
				grdv1.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.ForeColor = Color.Black;
				grdv1.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.Font = new Font(FontFamily.GenericSansSerif, 9F, FontStyle.Regular);
				grdv1.Columns["D" + i.ToString().PadLeft(2, '0')].ToolTip = "";
				grdv2.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.ForeColor = Color.Black;
				grdv2.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.Font = new Font(FontFamily.GenericSansSerif, 9F, FontStyle.Regular);
				grdv2.Columns["D" + i.ToString().PadLeft(2, '0')].ToolTip = "";
            }
			SetButtonEnable("100000");
        }

        #endregion

        #region 1 Form

        private void duty2040_Load(object sender, EventArgs e)
        {
			dat_yymm.DateTime = DateTime.Now;
			sl_dept.EditValue = null;
			sl_gnmu.EditValue = null;

			SetCancel();
		}

        private void duty2040_Shown(object sender, EventArgs e)
        {
            df.GetMSTUSER_CHKDatas(ds);  //부서관리여부
            if (ds.Tables["MSTUSER_CHK"].Rows.Count > 0)
            {
                msyn = ds.Tables["MSTUSER_CHK"].Rows[0]["USERMSYN"].ToString(); //전체조회
                upyn = ds.Tables["MSTUSER_CHK"].Rows[0]["USERUPYN"].ToString(); //부서조회
            }
            //사용자부서연결
            if (SilkRoad.Config.SRConfig.US_GUBN == "1" || SilkRoad.Config.SRConfig.USID == "SAMIL" || msyn == "1")
            {
                p_dpcd = "%";
                lb_power.Text = "전체조회 권한";
				sl_dept.Enabled = true;
            }
            else
            {
                p_dpcd = SilkRoad.Config.SRConfig.US_DPCD == null ? null : SilkRoad.Config.SRConfig.US_DPCD.Trim();
                lb_power.Text = upyn == "1" ? "부서조회 권한" : "조회권한 없음";

				if(ds.Tables["2040_CALLDEPT"].Select("CODE = '" + p_dpcd + "'").Length > 0)
					sl_dept.EditValue = p_dpcd;
				sl_dept.Enabled = false;
            }
        }
		
		private void END_CHK()
		{
			string yymm = clib.DateToText(dat_yymm.DateTime).Substring(0, 4) + "." + clib.DateToText(dat_yymm.DateTime).Substring(4, 2);
			df.Get2020_SEARCH_ENDSDatas(clib.DateToText(dat_yymm.DateTime).Substring(0, 6), ds);
			if (ds.Tables["2020_SEARCH_ENDS"].Rows.Count > 0) //마감월이 저장되어 있으면
			{
				DataRow irow = ds.Tables["2020_SEARCH_ENDS"].Rows[0];
				ends_yn = irow["CLOSE_YN"].ToString();
				lb_ends.Text = irow["CLOSE_YN"].ToString() == "Y" ? "[" + yymm + " 최종마감 완료]" : irow["CLOSE_YN"].ToString() == "N" ? "[" + yymm + " 최종마감 취소]" : "[ ]";
			}
			else
			{
				ends_yn = "";
				lb_ends.Text = "[" + yymm + " 최종마감 작업전]";
			}
		}
        #endregion

        #region 2 Button
		
        //처리
        private void btn_proc_Click(object sender, EventArgs e)
        {
			if (isNoError_um(1))
			{
                Cursor = Cursors.WaitCursor;
				END_CHK();
				grd1.Enabled = true;
				grd2.Enabled = true;
				//pl_center.Enabled = true;
				//pan_top.Enabled = true; // false;
				//srGroupBox1.Enabled = true;
				// srTextEdit2.Enabled = false;
				dat_yymm.Enabled = false;
				sl_dept.Enabled = false;

				sl_embs.Enabled = true;
				btn_lineadd.Enabled = true;
				btn_linedel.Enabled = true;
				btn_bf_plan.Enabled = true;
				df.GetLOOK_CALL_EMBSDatas(sl_dept.EditValue.ToString(), ds);
				sl_embs.Properties.DataSource = ds.Tables["LOOK_CALL_EMBS"];
				sl_embs.EditValue = null;

				df.GetSEARCH_HOLIDatas(clib.DateToText(dat_yymm.DateTime).Substring(0, 6), ds);
				//1.기준년월에 따른 하단 일자컬럼header 일자, 요일 설정
				int lastday = clib.TextToInt(clib.DateToText(clib.TextToDateLast(clib.DateToText(dat_yymm.DateTime))).Substring(6, 2));

				for (int i = 1; i <= lastday; i++)
				{
					DateTime day = clib.TextToDate(clib.DateToText(dat_yymm.DateTime).Substring(0, 6) + "01").AddDays(i - 1);
					grdv1.Columns["D" + i.ToString().PadLeft(2, '0')].Caption = i.ToString() + "\r\n" + clib.WeekDay(day); //일+요일. 한칸 내려서 보이도록. 엔터마냥. 
					if (clib.WeekDay(day) == "토")
					{
						grdv1.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.ForeColor = Color.Blue;
						grdv2.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.ForeColor = Color.Blue;
					}
					else if (clib.WeekDay(day) == "일")
					{
						grdv1.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.ForeColor = Color.Red;
						grdv2.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.ForeColor = Color.Red;
					}
					else
					{
						grdv1.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.ForeColor = Color.Black;
						grdv2.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.ForeColor = Color.Black;
					}

					if (ds.Tables["SEARCH_HOLI"].Select("H_DATE = '" + clib.DateToText(day) + "'").Length > 0)
					{
						grdv1.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.ForeColor = Color.Red;
						grdv1.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.Font = new Font(FontFamily.GenericSansSerif, 9F, FontStyle.Underline);
						grdv1.Columns["D" + i.ToString().PadLeft(2, '0')].ToolTip = ds.Tables["SEARCH_HOLI"].Select("H_DATE = '" + clib.DateToText(day) + "'")[0]["H_NAME"].ToString();

						grdv2.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.ForeColor = Color.Red;
						grdv2.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.Font = new Font(FontFamily.GenericSansSerif, 9F, FontStyle.Underline);
						grdv2.Columns["D" + i.ToString().PadLeft(2, '0')].ToolTip = ds.Tables["SEARCH_HOLI"].Select("H_DATE = '" + clib.DateToText(day) + "'")[0]["H_NAME"].ToString();
					}
				}

				//남은 필드 visible = false;
				for (int k = 1; k < 32; k++)
				{
					if (k > lastday)
					{
						grdv1.Columns["D" + k.ToString().PadLeft(2, '0')].Visible = false;
						grdv2.Columns["D" + k.ToString().PadLeft(2, '0')].Visible = false;
					}
					else
					{
						grdv1.Columns["D" + k.ToString().PadLeft(2, '0')].Visible = true;
						grdv2.Columns["D" + k.ToString().PadLeft(2, '0')].Visible = true;
					}
				}
				//근무신청 내역 조회
				//df.Get3010_SEARCH_OREQDatas(clib.DateToText(dat_yymm.DateTime).Substring(0, 6), sl_dept.EditValue.ToString(), ds);
				//근무유형 조회
				df.Get2040_CALL_GNMUDatas(ds);

				df.GetDUTY_TRSCALLDatas(2, clib.DateToText(dat_yymm.DateTime).Substring(0, 6), sl_dept.EditValue.ToString(), ds);
				if (ds.Tables["SEARCH_CALL_PLAN"].Rows.Count == 0)
				{
					df.GetSEARCH_CALL_PLANDatas(clib.DateToText(dat_yymm.DateTime).Substring(0, 6), sl_dept.EditValue.ToString(), ds);
					SetButtonEnable("011011");
				}
				else
				{
					SetButtonEnable("011111");
				}

				for (int i = 0; i < ds.Tables["SEARCH_CALL_PLAN"].Rows.Count; i++)
				{
					for (int k = 1; k <= lastday; k++)
					{
						DataRow crow = ds.Tables["SEARCH_CALL_PLAN"].Rows[i];
						if (ds.Tables["2040_CALL_GNMU"].Select("G_CODE = '" + crow["D" + k.ToString().PadLeft(2, '0')].ToString().Trim() + "'").Length > 0)
						{
							crow["D" + k.ToString().PadLeft(2, '0') + "_NM"] = ds.Tables["2040_CALL_GNMU"].Select("G_CODE = '" + crow["D" + k.ToString().PadLeft(2, '0')].ToString() + "'")[0]["G_SNM"].ToString();
						}
					}
				}

				df.GetSUM_CALL_PLANDatas(ds);
				grd2.DataSource = ds.Tables["SUM_CALL_PLAN"];
				//for (int i = 0; i < ds.Tables["SEARCH_CALL_PLAN"].Rows.Count; i++)
				//{
				//	month_calc(ds.Tables["SEARCH_CALL_PLAN"].Rows[i]["SAWON_NO"].ToString());
				//}
				sum_month_calc();
				grd1.DataSource = ds.Tables["SEARCH_CALL_PLAN"];
				grdv1.Focus();

				Cursor = Cursors.Default;
			}
		}

        //저장버튼
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (isNoError_um(2))
            {
                Cursor = Cursors.WaitCursor;
                int outVal = 0;
                try
                {
                    // 1. 해당년월에 사번의trscall 가져오기
                    df.GetDUTY_TRSCALLDatas(1, clib.DateToText(dat_yymm.DateTime).Substring(0, 6), sl_dept.EditValue.ToString(), ds);

					// 2.그리드한바퀴 글자들 합해서 넣기
					DataRow nrow = null;
					int sq = 1;
					for (int i = 0; i < ds.Tables["SEARCH_CALL_PLAN"].Rows.Count; i++)
					{
						DataRow drow = ds.Tables["SEARCH_CALL_PLAN"].Rows[i];
						if (drow.RowState != DataRowState.Deleted)
						{
							if (ds.Tables["DUTY_TRSCALL"].Select("SAWON_NO = '" + drow["SAWON_NO"] + "'").Length > 0)
							{
								nrow = ds.Tables["DUTY_TRSCALL"].Select("SAWON_NO = '" + drow["SAWON_NO"] + "'")[0];
								nrow["UPDT"] = gd.GetNow();
								nrow["USID"] = SilkRoad.Config.SRConfig.USID;
								nrow["PSTY"] = "U";
							}
							else
							{
								nrow = ds.Tables["DUTY_TRSCALL"].NewRow();
								nrow["SAWON_NO"] = drow["SAWON_NO"].ToString();
								nrow["PLANYYMM"] = clib.DateToText(dat_yymm.DateTime).Substring(0, 6);
								nrow["DEPTCODE"] = sl_dept.EditValue.ToString();
								nrow["INDT"] = gd.GetNow();
								nrow["UPDT"] = "";
								nrow["USID"] = SilkRoad.Config.SRConfig.USID;
								nrow["PSTY"] = "A";
								ds.Tables["DUTY_TRSCALL"].Rows.Add(nrow);
							}
							nrow["PLAN_SQ"] = sq;
							nrow["MM_CNT1"] = drow["MM_CNT1"];
							nrow["MM_CNT2"] = drow["MM_CNT2"];
							for (int j = 1; j <= 31; j++)
							{
								nrow["D" + j.ToString().PadLeft(2, '0')] = drow["D" + j.ToString().PadLeft(2, '0')].ToString();
							}
							sq++;
						}
						else
						{
							if (ds.Tables["DUTY_TRSCALL"].Select("SAWON_NO = '" + drow["SAWON_NO", DataRowVersion.Original].ToString().Trim() + "' ").Length > 0)
								ds.Tables["DUTY_TRSCALL"].Select("SAWON_NO = '" + drow["SAWON_NO", DataRowVersion.Original].ToString().Trim() + "' ")[0].Delete();
						}
					}

                    string[] tableNames = new string[] { "DUTY_TRSCALL" };
                    SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
                    outVal = cmd.setUpdate(ref ds, tableNames, null);
                }
                catch (Exception ec)
                {
                    MessageBox.Show(ec.Message, "저장오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    btn_refresh.PerformClick();
                    SetCancel();
                    Cursor = Cursors.Default;
                }
            }
        }

        //삭제
        private void btn_del_Click(object sender, EventArgs e)
        {
			if (isNoError_um(3))
			{
				string yymm = clib.DateToText(dat_yymm.DateTime).Substring(0, 4) + "-" + clib.DateToText(dat_yymm.DateTime).Substring(4, 2);
				DialogResult dr = MessageBox.Show(yymm + "월의 근무내역을 모두 삭제하시겠습니까?", "삭제여부", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
				if (dr == DialogResult.OK)
				{
					int outVal = 0;
					try
					{
						df.GetDUTY_TRSPLANDatas(1, clib.DateToText(dat_yymm.DateTime).Substring(0, 6), sl_dept.EditValue.ToString(), ds);
						for (int i = 0; i < ds.Tables["DUTY_TRSCALL"].Rows.Count; i++)
						{
							ds.Tables["DUTY_TRSCALL"].Rows[i].Delete();
						}

						string[] tableNames = new string[] { "DUTY_TRSCALL" };
						SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
						outVal = cmd.setUpdate(ref ds, tableNames, null);
					}
					catch (Exception ec)
					{
						MessageBox.Show(ec.Message, "삭제오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					finally
					{
						if (outVal > 0)
							MessageBox.Show("조회된 내역의 근무가 삭제되었습니다.", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);

						SetCancel();
						Cursor = Cursors.Default;
					}
				}
			}
        }

        /// <summary>취소버튼</summary>
        private void btn_clear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("해당 내역을 취소하시겠습니까?", "취소", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                               == DialogResult.OK)
            {
                SetCancel();
            }
        }
		
        //미리보기
        private void btn_preview_Click(object sender, EventArgs e)
        {
			print(1);
		}		
        // 인쇄
        private void btn_print_Click(object sender, EventArgs e)
        {
			print(2);
        }

        /// <summary>엑셀버튼</summary>
        private void btn_exel_Click(object sender, EventArgs e)
        {
            clib.gridToExcel(grdv1, this.Text + "(" + this.Name + ")_" + clib.DateToText(DateTime.Now), true);
        }
		private void print(int stat)
		{
			rpt_2040 rpt = new rpt_2040(clib.DateToText(dat_yymm.DateTime), sl_dept.Text.ToString(), ds.Tables["SEARCH_HOLI"]); // txt_yymm.Text, sl_part2.Text, ds.Tables["SEARCH_PLAN"]);
			rpt.DataSource = ds.Tables["SEARCH_CALL_PLAN"];
			if (stat == 1)
				rpt.ShowPreview();
			else if (stat == 2)
				rpt.Print();
		}
		//라인추가
		private void btn_lineadd_Click(object sender, EventArgs e)
		{
			if (sl_embs.EditValue == null)
			{
				MessageBox.Show("근무자를 선택하세요!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
				sl_embs.Focus();
			}
			else if (ds.Tables["SEARCH_CALL_PLAN"].Select("SAWON_NO = '" + sl_embs.EditValue.ToString() + "'").Length > 0)
			{
				MessageBox.Show("이미 신청된 근무자입니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
				sl_embs.Focus();
			}
			else
			{
				int max_sq = clib.TextToInt(ds.Tables["SEARCH_CALL_PLAN"].Compute("MAX(PLAN_SQ)", null).ToString());
				DataRow trow = ds.Tables["LOOK_CALL_EMBS"].Select("CODE = '" + sl_embs.EditValue.ToString() + "'")[0];
				DataRow nrow = ds.Tables["SEARCH_CALL_PLAN"].NewRow();
				nrow["SAWON_NO"] = sl_embs.EditValue.ToString();
				nrow["SAWON_NM"] = trow["NAME"].ToString();
				nrow["PLAN_SQ"] = max_sq + 1;
				ds.Tables["SEARCH_CALL_PLAN"].Rows.Add(nrow);
			}
		}
		//라인삭제
		private void btn_linedel_Click(object sender, EventArgs e)
		{
            DataRow drow = grdv1.GetFocusedDataRow();
            if (drow == null)
                return;
			
            DialogResult dr = MessageBox.Show(drow["SAWON_NM"].ToString() + "님의 근무내역을 삭제하시겠습니까?", "삭제여부", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
			if (dr == DialogResult.OK)
			{
				int now_slsq = clib.TextToInt(drow["PLAN_SQ"].ToString());
				drow.Delete();

				for (int i = 0; i < ds.Tables["SEARCH_CALL_PLAN"].Rows.Count; i++)
				{
					DataRow crow = ds.Tables["SEARCH_CALL_PLAN"].Rows[i];
					if (crow.RowState != DataRowState.Deleted)
					{
						if (clib.TextToInt(crow["PLAN_SQ"].ToString()) > now_slsq)
						{
							crow["PLAN_SQ"] = clib.TextToInt(crow["PLAN_SQ"].ToString()) - 1;
						}
					}
				}
			}
		}
		
		//부서설정
		private void btn_info_Click(object sender, EventArgs e)
		{			
			duty2041 duty2041 = new duty2041();
			duty2041.ShowDialog();

			sl_embs.EditValue = null;
			//df.GetLOOK_CALL_EMBSDatas(ds);
			//sl_embs.Properties.DataSource = ds.Tables["LOOK_CALL_EMBS"];
		}
		//전월근무조회
		private void btn_bf_plan_Click(object sender, EventArgs e)
		{
			df.GetSEARCH_BF_HOLIDatas(clib.DateToText(dat_yymm.DateTime.AddMonths(-1)).Substring(0, 6), ds);
			df.GetDUTY_BF_TRSCALLDatas(clib.DateToText(dat_yymm.DateTime.AddMonths(-1)).Substring(0, 6), sl_dept.EditValue.ToString(), ds);
			//1.기준년월에 따른 하단 일자컬럼header 일자, 요일 설정
			int lastday = clib.TextToInt(clib.DateToText(clib.TextToDateLast(clib.DateToText(dat_yymm.DateTime.AddMonths(-1)))).Substring(6, 2));
			for (int i = 0; i < ds.Tables["SEARCH_BF_CALL"].Rows.Count; i++)
			{
				for (int k = 1; k <= lastday; k++)
				{
					DataRow crow = ds.Tables["SEARCH_BF_CALL"].Rows[i];
					if (ds.Tables["2040_CALL_GNMU"].Select("G_CODE = '" + crow["D" + k.ToString().PadLeft(2, '0')].ToString() + "'").Length > 0)
					{
						crow["D" + k.ToString().PadLeft(2, '0') + "_NM"] = ds.Tables["2040_CALL_GNMU"].Select("G_CODE = '" + crow["D" + k.ToString().PadLeft(2, '0')].ToString() + "'")[0]["G_SNM"].ToString();
					}
				}
			}

			rpt_2040 rpt = new rpt_2040(clib.DateToText(dat_yymm.DateTime.AddMonths(-1)), sl_dept.Text.ToString(), ds.Tables["SEARCH_BF_HOLI"]);
			rpt.DataSource = ds.Tables["SEARCH_BF_CALL"];
			rpt.ShowPreview();
		}

        /// <summary> refresh버튼 </summary>
        private void btn_refresh_Click(object sender, EventArgs e)
        {
			Refresh_Click();
        }
        /// <summary> refresh버튼 </summary>
        private void Refresh_Click()
        {
			df.Get2040_CALLDEPTDatas(ds);
			sl_dept.Properties.DataSource = ds.Tables["2040_CALLDEPT"];
			df.Get2040_CALL_GNMUDatas(ds);
			grd_sl_gnmu.DataSource = ds.Tables["2040_CALL_GNMU"];
			sl_gnmu.Properties.DataSource = ds.Tables["2040_CALL_GNMU"];

			if (sl_dept.EditValue != null)
			{
				df.GetLOOK_CALL_EMBSDatas(sl_dept.EditValue.ToString(), ds);
				sl_embs.Properties.DataSource = ds.Tables["LOOK_CALL_EMBS"];
			}
		}

		#endregion

		#region 3 EVENT
		
		//메뉴 활성화시
		private void duty2040_Activated(object sender, EventArgs e)
		{
			END_CHK();
		}
		private void duty3010_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //취소
            {
                btn_clear.PerformClick();
            }
        }
		
		private void grdv_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
		{
            DataRow drow = grdv1.GetFocusedDataRow();
			if (drow != null)
			{
				if (grdv1.FocusedColumn.Name.ToString().Substring(0, 10) == "grdcol_day")
				{
					if (chk_g_adt.Checked == true)
					{
						e.Cancel = true;
						if (sl_gnmu.EditValue == null)
						{
							drow["D" + grdv1.FocusedColumn.Name.ToString().Substring(10, 2)] = "";
							drow["D" + grdv1.FocusedColumn.Name.ToString().Substring(10, 2)  + "_NM"] = "";
						}
						else
						{
							drow["D" + grdv1.FocusedColumn.Name.ToString().Substring(10, 2)] = sl_gnmu.EditValue;
							if (ds.Tables["2040_CALL_GNMU"].Select("G_CODE = '" + sl_gnmu.EditValue.ToString() + "'").Length > 0)
								drow["D" + grdv1.FocusedColumn.Name.ToString().Substring(10, 2) + "_NM"] = ds.Tables["2040_CALL_GNMU"].Select("G_CODE = '" + sl_gnmu.EditValue.ToString() + "'")[0]["G_SNM"].ToString();
						}	
						month_calc(drow["SAWON_NO"].ToString());
					}
				}
			}
		}

        private void grdv_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow drow = grdv1.GetFocusedDataRow();
            if (drow != null)
            {
				if (e.Column.Name.StartsWith("grdcol_day"))
				{
					if (e.Value == null)
					{
						drow["D" + e.Column.Name.ToString().Substring(10, 2)] = "";
						drow["D" + e.Column.Name.ToString().Substring(10, 2) + "_NM"] = "";
					}
					else
					{
						drow["D" + e.Column.Name.ToString().Substring(10, 2)] = e.Value.ToString();
						if (ds.Tables["2040_CALL_GNMU"].Select("G_CODE = '" + e.Value.ToString() + "'").Length > 0)
							drow["D" + e.Column.Name.ToString().Substring(10, 2) + "_NM"] = ds.Tables["2040_CALL_GNMU"].Select("G_CODE = '" + e.Value.ToString() + "'")[0]["G_SNM"].ToString();
					}
				}
				month_calc(drow["SAWON_NO"].ToString());
            }
        }
		
        //근무값별 색 주기 ★
        private void grdv_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name.StartsWith("grdcol_day"))
            {
				if (ds.Tables["2040_CALL_GNMU"].Select("G_CODE = '" + e.CellValue + "'") != null && ds.Tables["2040_CALL_GNMU"].Select("G_CODE = '" + e.CellValue + "'").Length > 0)
				{
					int colVAlue = clib.TextToInt(ds.Tables["2040_CALL_GNMU"].Select("G_CODE = '" + e.CellValue + "'")[0]["G_COLOR"].ToString());
					e.Appearance.BackColor = Color.FromArgb(colVAlue);
					//e.Appearance.ForeColor = Color.White;
				}

				string date = clib.DateToText(dat_yymm.DateTime).Substring(0, 6) + e.Column.Name.ToString().Substring(10, 2);
				string sabn = grdv1.GetDataRow(e.RowHandle)["SAWON_NO"].ToString().Trim();
			}
        }
		
		#endregion

		#region 4 Drag & Drop

        GridHitInfo downHitInfo = null;
        private void grdv_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView; //1st
            downHitInfo = null;

            GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
            if (Control.ModifierKeys != Keys.None)
                return;
            if (e.Button == MouseButtons.Left && hitInfo.InRow && hitInfo.RowHandle != GridControl.NewItemRowHandle)
                downHitInfo = hitInfo;

        }
        private void grdv_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;  //2nd
            if (e.Button == MouseButtons.Left && downHitInfo != null)
            {
                Size dragSize = SystemInformation.DragSize;
                Rectangle dragRect = new Rectangle(new Point(downHitInfo.HitPoint.X - dragSize.Width / 2,
                    downHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize);

                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {
                    view.GridControl.DoDragDrop(downHitInfo, DragDropEffects.All);
                    downHitInfo = null;
                }
            }
        }
        private void grd_DragOver(object sender, DragEventArgs e)
        {
			if (e.Data.GetDataPresent(typeof(BandedGridHitInfo)))
			{
				GridHitInfo downHitInfo = e.Data.GetData(typeof(BandedGridHitInfo)) as GridHitInfo;
				if (downHitInfo == null)
					return;

				GridControl grid = sender as GridControl;
				GridView view = grid.MainView as GridView;
				GridHitInfo hitInfo = view.CalcHitInfo(grid.PointToClient(new Point(e.X, e.Y)));
				if (hitInfo.InRow && hitInfo.RowHandle != downHitInfo.RowHandle && hitInfo.RowHandle != GridControl.NewItemRowHandle)
					e.Effect = DragDropEffects.Copy;//.Move;
				else
					e.Effect = DragDropEffects.None;
			}
		}
        private void grd_DragDrop(object sender, DragEventArgs e)
        {
            GridControl grid = sender as GridControl;
            GridView view = grid.MainView as GridView;
            GridHitInfo srcHitInfo = e.Data.GetData(typeof(BandedGridHitInfo)) as GridHitInfo;
            GridHitInfo hitInfo = view.CalcHitInfo(grid.PointToClient(new Point(e.X, e.Y)));
            int sourceRow = srcHitInfo.RowHandle;
            int targetRow = hitInfo.RowHandle;
            MoveRow(sourceRow, targetRow);
        }
        //순번재정렬
        private void MoveRow(int sourceRow, int targetRow)
        {
            if (sourceRow == targetRow || sourceRow == targetRow + 1)
                return;

            GridView view = grdv1;
            DataRow row = grdv1.GetFocusedDataRow();
			string sabn = row["SAWON_NO"].ToString();
            int now_slsq = clib.TextToInt(row["PLAN_SQ"].ToString());
            if (sourceRow < targetRow)
            {
                for (int i = sourceRow + 1; i <= targetRow; i++)
                {
                    int for_slsq = i + 1;
                    DataRow crow = ds.Tables["SEARCH_CALL_PLAN"].Select("PLAN_SQ = " + for_slsq + "")[0];
                    if (crow.RowState != DataRowState.Deleted)
                    {
                        crow["PLAN_SQ"] = clib.TextToInt(crow["PLAN_SQ"].ToString()) - 1;
                    }
                }
				
				ds.Tables["SEARCH_CALL_PLAN"].Select("SAWON_NO = '" + sabn + "'")[0]["PLAN_SQ"] = targetRow + 1;
            }
            else
            {
				int tg_slsq = targetRow < 0 ? 0 : clib.TextToInt(ds.Tables["SEARCH_CALL_PLAN"].Select("PLAN_SQ = " + (targetRow + 1))[0]["PLAN_SQ"].ToString());
				for (int i = 0; i < ds.Tables["SEARCH_CALL_PLAN"].Rows.Count; i++)
				{
					DataRow crow = ds.Tables["SEARCH_CALL_PLAN"].Rows[i];
					if (crow.RowState != DataRowState.Deleted)
					{
						if (clib.TextToInt(crow["PLAN_SQ"].ToString()) > tg_slsq && clib.TextToInt(crow["PLAN_SQ"].ToString()) <= sourceRow)
						{
							crow["PLAN_SQ"] = clib.TextToInt(crow["PLAN_SQ"].ToString()) + 1;
						}
					}
				}
				ds.Tables["SEARCH_CALL_PLAN"].Select("SAWON_NO = '" + sabn + "'")[0]["PLAN_SQ"] = targetRow < 0 ? 1 : targetRow + 2;
                //row["PLAN_SQ"] = targetRow + 2;
            }
			ds.Tables["SEARCH_CALL_PLAN"].DefaultView.Sort = "PLAN_SQ ASC";
			DataTable dt = ds.Tables["SEARCH_CALL_PLAN"].DefaultView.ToTable();
			dp.AddDatatable2Dataset("SEARCH_CALL_PLAN", dt, ref ds);
			grd1.DataSource = ds.Tables["SEARCH_CALL_PLAN"];
        }
        private void grd_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
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
            bool isError = false;
			END_CHK();

            if (mode == 1)  //처리
            {
                if (dat_yymm.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("조회년월을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dat_yymm.Focus();
                    return false;
                }
                else if (sl_dept.EditValue == null)
                {
                    MessageBox.Show("부서를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sl_dept.Focus();
                    return false;
                }
                else
                {
                    isError = true;
                }
            }
            else if (mode == 2)  //저장
            {
				if (ends_yn == "Y")
				{
					MessageBox.Show("최종마감되어 저장할 수 없습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				else
				{
					isError = true;
				}
			}
            else if (mode == 3)  //삭제
            {
				if (ends_yn == "Y")
				{
					MessageBox.Show("최종마감되어 삭제할 수 없습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

		/// <summary>
		/// 배열에따른 버튼상태설정
		/// </summary>
		/// <param name="mode"></param>
		private void SetButtonEnable(string arr)
		{
			btn_proc.Enabled = arr.Substring(0, 1) == "1" ? true : false;
			btn_clear.Enabled = arr.Substring(1, 1) == "1" ? true : false;
			btn_save.Enabled = arr.Substring(2, 1) == "1" ? true : false;
			btn_del.Enabled = arr.Substring(3, 1) == "1" ? true : false;
			btn_preview.Enabled = arr.Substring(4, 1) == "1" ? true : false;
			btn_print.Enabled = arr.Substring(5, 1) == "1" ? true : false;
		}

		private void month_calc(string sabn)
		{
			//근무유형 조회
			df.Get2040_CALL_GNMUDatas(ds);

			DataRow srow = ds.Tables["SEARCH_CALL_PLAN"].Select("SAWON_NO = '" + sabn + "'")[0];
			//1.기준년월에 따른 하단 일자컬럼header 일자, 요일 설정
			int lastday = clib.TextToInt(clib.DateToText(clib.TextToDateLast(clib.DateToText(dat_yymm.DateTime))).Substring(6, 2));
			//남은 필드 visible = false;
			int Day = 0, Wk = 0;
			for (int k = 1; k <= lastday; k++)
			{
				if (ds.Tables["2040_CALL_GNMU"].Select("G_CODE = '" + srow["D" + k.ToString().PadLeft(2, '0')].ToString() + "'").Length > 0)
				{
					if ((ds.Tables["2040_CALL_GNMU"].Select("G_CODE = '" + srow["D" + k.ToString().PadLeft(2, '0')].ToString() + "'")[0]["G_TYPE"].ToString()) == "11")
					{
						df.GetCHK_HOLIDatas(clib.DateToText(dat_yymm.DateTime).Substring(0, 6) + k.ToString().PadLeft(2, '0'), ds);
						if (ds.Tables["CHK_HOLI"].Rows.Count > 0)
						{
							Wk += 1;
						}
						else
						{
							switch (clib.TextToDate(clib.DateToText(dat_yymm.DateTime).Substring(0, 6) + k.ToString().PadLeft(2, '0')).DayOfWeek)
							{
								case DayOfWeek.Monday:
								case DayOfWeek.Tuesday:
								case DayOfWeek.Wednesday:
								case DayOfWeek.Thursday:
								case DayOfWeek.Friday:
								case DayOfWeek.Saturday:
									Day += 1;
									break;
								case DayOfWeek.Sunday:
									Wk += 1;
									break;
							}
						}
					}
				}
			}
			srow["MM_CNT1"] = Day;
			srow["MM_CNT2"] = Wk;
			srow["Y_CNT1"] = clib.TextToInt(srow["YEAR_CNT1"].ToString()) + Day;
			srow["Y_CNT2"] = clib.TextToInt(srow["YEAR_CNT2"].ToString()) + Wk;

			sum_month_calc();
		}
		private void sum_month_calc()
		{
			int T_Day = 0, T_Wk = 0;
			int lastday = clib.TextToInt(clib.DateToText(clib.TextToDateLast(clib.DateToText(dat_yymm.DateTime))).Substring(6, 2));
			for (int d = 1; d <= lastday; d++)
			{
				string dd = d.ToString().PadLeft(2, '0');
				int Day = 0;
				for (int i = 0; i < ds.Tables["SEARCH_CALL_PLAN"].Rows.Count; i++)
				{
					DataRow drow = ds.Tables["SEARCH_CALL_PLAN"].Rows[i];
					if (drow.RowState != DataRowState.Deleted)
					{
						if (ds.Tables["2040_CALL_GNMU"].Select("G_CODE = '" + drow["D" + dd].ToString() + "'").Length > 0)
						{
							if ((ds.Tables["2040_CALL_GNMU"].Select("G_CODE = '" + drow["D" + dd].ToString() + "'")[0]["G_TYPE"].ToString()) == "11")
							{
								df.GetCHK_HOLIDatas(clib.DateToText(dat_yymm.DateTime).Substring(0, 6) + dd, ds);
								if (ds.Tables["CHK_HOLI"].Rows.Count > 0)
								{
									Day += 1;
									T_Wk += 1;
								}
								else
								{
									switch (clib.TextToDate(clib.DateToText(dat_yymm.DateTime).Substring(0, 6) + dd).DayOfWeek)
									{
										case DayOfWeek.Monday:
										case DayOfWeek.Tuesday:
										case DayOfWeek.Wednesday:
										case DayOfWeek.Thursday:
										case DayOfWeek.Friday:
										case DayOfWeek.Saturday:
											Day += 1;
											T_Day += 1;
											break;
										case DayOfWeek.Sunday:
											Day += 1;
											T_Wk += 1;
											break;
									}
								}
							}
						}
					}
				}
				if (ds.Tables["SUM_CALL_PLAN"].Select("G_TYPE = 11").Length > 0)
				{
					ds.Tables["SUM_CALL_PLAN"].Select("G_TYPE = 11")[0]["D" + dd] = Day;
					ds.Tables["SUM_CALL_PLAN"].Select("G_TYPE = 11")[0]["MM_CNT1"] = T_Day;
					ds.Tables["SUM_CALL_PLAN"].Select("G_TYPE = 11")[0]["MM_CNT2"] = T_Wk;
				}
			}
		}

		#endregion

	}
}

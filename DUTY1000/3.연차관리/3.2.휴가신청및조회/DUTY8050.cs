using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using DevExpress.XtraScheduler;
using System.Drawing;

namespace DUTY1000
{
    public partial class duty8050 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();
		
        private string ends_yn = "";
        private string ends_yn2 = "";
		
		private int admin_lv = 0;
        private string msyn = "";
        private string upyn = "";
        private string p_dpcd = "";
        public duty8050()
        {
            InitializeComponent();
        }

        #region 0. Initialization

        /// <summary>
        ///컨트롤 초기화 및 활성,비활성 설정
        /// </summary>
        /// <param name="enable"></param>
        private void SetCancel(int stat)
        {
			if (stat == 0)
			{
				//dat_yymm.Enabled = true;
				//dat_yymm2.Enabled = true;
				sl_dept.Enabled = p_dpcd == "%" ? true : false;
				
				if (ds.Tables["DUTY_TRSJREQ"] != null)
					ds.Tables["DUTY_TRSJREQ"].Clear();
				grd_hg.DataSource = null;
				if (ds.Tables["SEARCH_JREQ_LIST"] != null)
					ds.Tables["SEARCH_JREQ_LIST"].Clear();
				grd_del.DataSource = null;
				schedulerStorage1.Appointments.DataSource = null;

				//SetButtonEnable("10");
			}

			if (stat == 1)
			{
				dat_ycdt.Text = "";
				dat_ycdt2.Text = "";
				sl_gnmu.EditValue = null;
				
				sl_embs.Enabled = true;
				dat_ycdt.Enabled = false;
				dat_ycdt2.Enabled = false;
				sl_gnmu.Enabled = false;
				chk_line.Enabled = false;
				
				sl_line1.EditValue = null;
				sl_line2.EditValue = null;
				sl_line3.EditValue = null;
				sl_line1.Enabled = false;
				sl_line2.Enabled = false;
				sl_line3.Enabled = false;

				SetButtonEnable2("1000");
			}

			df.GetSEARCH_DEPTDatas(ds);
			sl_dept.Properties.DataSource = ds.Tables["SEARCH_DEPT"];
			df.Get8030_SEARCH_EMBSDatas(p_dpcd, ds);
			sl_embs.Properties.DataSource = ds.Tables["8030_SEARCH_EMBS"];
			df.Get8050_SEARCH_GNMUDatas(ds);
			sl_gnmu.Properties.DataSource = ds.Tables["8050_SEARCH_GNMU"];
        }
		
        //휴가신청내역 조회
        private void baseInfoSearch()
        {
			//END_CHK();
			string dept = sl_dept.EditValue == null ? "%" : sl_dept.EditValue.ToString();
			df.GetSEARCH_JREQ_LISTDatas("C", clib.DateToText(dat_yymm.DateTime).Substring(0, 6), clib.DateToText(dat_yymm2.DateTime).Substring(0, 6), dept, ds);
			grd_hg.DataSource = ds.Tables["SEARCH_JREQ_LIST"];
			df.GetSEARCH_JREQ_LISTDatas("D", clib.DateToText(dat_yymm.DateTime).Substring(0, 6), clib.DateToText(dat_yymm2.DateTime).Substring(0, 6), dept, ds);
			grd_del.DataSource = ds.Tables["SEARCH_DEL_JREQ_LIST"];

			int start_index = 0;
			switch (clib.WeekDay(clib.TextToDateFirst(clib.DateToText(dat_yymm.DateTime))))
			{
				case "일":
				start_index = 0;
				break;
				case "월":
				start_index = 1;
				break;
				case "화":
				start_index = 2;
				break;
				case "수":
				start_index = 3;
				break;
				case "목":
				start_index = 4;
				break;
				case "금":
				start_index = 5;
				break;
				case "토":
				start_index = 6;
				break;
			}
			int row_count = start_index + clib.TextToInt(clib.DateToText(clib.TextToDateLast(clib.DateToText(dat_yymm.DateTime))).Substring(6, 2).ToString());
			row_count = row_count / 7 + 1;

			schedulerControl1.Views.MonthView.WeekCount = row_count;
			schedulerStorage1.Appointments.ResourceSharing = true;
			schedulerControl1.GroupType = SchedulerGroupType.Resource;
			schedulerControl1.Start = clib.TextToDateFirst(clib.DateToText(dat_yymm.DateTime));
			schedulerStorage1.Appointments.DataSource = ds.Tables["SEARCH_JREQ_LIST"];

			schedulerStorage1.Appointments.Mappings.Type = "TYPE";         //타입
			schedulerStorage1.Appointments.Mappings.Start = "FR_DATE";     //시작날짜
			schedulerStorage1.Appointments.Mappings.End = "TO_DATE";       //끝날짜
			schedulerStorage1.Appointments.Mappings.AllDay = "ALLDAY";         //전일
			schedulerStorage1.Appointments.Mappings.Subject = "SAWON_NM";     //주제
			schedulerStorage1.Appointments.Mappings.Location = "REMARK";     //장소
			schedulerStorage1.Appointments.Mappings.Description = "REMARK";    //설명
			schedulerStorage1.Appointments.Mappings.Status = "STATUS";         //상태
			schedulerStorage1.Appointments.Mappings.Label = "LABEL";           //라벨
		}

        #endregion

        #region 1 Form

        private void duty8050_Load(object sender, EventArgs e)
        {
			dat_yymm.DateTime = DateTime.Today;
			dat_yymm2.DateTime = DateTime.Today;
			sl_dept.EditValue = null;
			schedulerControl1.Start = clib.TextToDateFirst(clib.DateToText(DateTime.Today));
			
			sl_embs.EditValue = null;
			sl_gnmu.EditValue = null;

			#region 요일체크       
			int start_index = 0;
			switch (clib.WeekDay(clib.TextToDateFirst(clib.DateToText(dat_yymm.DateTime))))
			{
				case "일":
				start_index = 0;
				break;
				case "월":
				start_index = 1;
				break;
				case "화":
				start_index = 2;
				break;
				case "수":
				start_index = 3;
				break;
				case "목":
				start_index = 4;
				break;
				case "금":
				start_index = 5;
				break;
				case "토":
				start_index = 6;
				break;
			}
			int row_count = start_index + clib.TextToInt(clib.DateToText(clib.TextToDateLast(clib.DateToText(dat_yymm.DateTime))).Substring(6, 2).ToString());
			row_count = row_count / 7 + 1;

			schedulerControl1.Views.MonthView.WeekCount = row_count;
			#endregion
        }
		
		private void duty8050_Shown(object sender, EventArgs e)
		{
            df.GetMSTUSER_CHKDatas(ds);  //부서관리여부
            if (ds.Tables["MSTUSER_CHK"].Rows.Count > 0)
                admin_lv = clib.TextToInt(ds.Tables["MSTUSER_CHK"].Rows[0]["EMBSADGB"].ToString()); //권한레벨

			if (SilkRoad.Config.SRConfig.US_GUBN == "1" || SilkRoad.Config.SRConfig.USID == "SAMIL" || admin_lv > 1)
			{
				admin_lv = 3;
                p_dpcd = "%";
                lb_power.Text = "전체조회 권한";
				sl_dept.Enabled = true;
			}
            else if (admin_lv == 1)
            {
                p_dpcd = SilkRoad.Config.SRConfig.US_DPCD == null ? null : SilkRoad.Config.SRConfig.US_DPCD.Trim();
                lb_power.Text = "부서조회 권한";
				sl_dept.EditValue = p_dpcd;
				sl_dept.Enabled = false;
				//btn_last_save.Visible = false;
				//btn_last_canc.Visible = false;
				//col_last_chk.Visible = false;
				//col_last_c_chk.Visible = false;
			}
            else
            {
                p_dpcd = SilkRoad.Config.SRConfig.US_DPCD == null ? null : SilkRoad.Config.SRConfig.US_DPCD.Trim();
                lb_power.Text = "조회권한 없음";				
				sl_dept.EditValue = p_dpcd;
				sl_dept.Enabled = false;
				//btn_last_save.Visible = false;
				//btn_last_canc.Visible = false;
				//col_last_chk.Visible = false;
				//col_last_c_chk.Visible = false;
            }

    //        if (ds.Tables["MSTUSER_CHK"].Rows.Count > 0)
    //        {
    //            msyn = ds.Tables["MSTUSER_CHK"].Rows[0]["USERMSYN"].ToString(); //전체조회
    //            upyn = ds.Tables["MSTUSER_CHK"].Rows[0]["USERUPYN"].ToString(); //부서조회
    //        }
    //        //사용자부서연결
    //        if (SilkRoad.Config.SRConfig.USID == "SAMIL" || msyn == "1")
    //        {
    //            p_dpcd = "%";
    //            lb_power.Text = "전체조회 권한";
				//sl_dept.Enabled = true;
    //        }
    //        else
    //        {
				//p_dpcd = SilkRoad.Config.SRConfig.US_DPCD == null ? null : SilkRoad.Config.SRConfig.US_DPCD.Trim();
    //            lb_power.Text = upyn == "1" ? "부서조회 권한" : "조회권한 없음";

				//sl_dept.EditValue = p_dpcd;
				//sl_dept.Enabled = false;
				//btn_last_save.Visible = false;
				//btn_last_canc.Visible = false;
				//col_last_chk.Visible = false;
				//col_last_c_chk.Visible = false;
    //        }
			
            SetCancel(0);
            SetCancel(1);
		}

        #endregion

        #region 2 Button
		
		//조회
		private void btn_search_Click(object sender, EventArgs e)
		{
			if (isNoError_um(1))
			{
				baseInfoSearch();
			}
		}

		//휴가신청 처리
		private void btn_proc_Click(object sender, EventArgs e)
		{			
			if (sl_embs.EditValue == null)
			{
				MessageBox.Show("사원이 선택되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
				sl_embs.Focus();
			}
			else
			{				
				sl_embs.Enabled = false;
				dat_ycdt.Enabled = true;
				dat_ycdt2.Enabled = true;
				sl_gnmu.Enabled = true;
				chk_line.Enabled = true;

				sl_line1.EditValue = null;
				sl_line2.EditValue = null;
				sl_line3.EditValue = null;
				sl_line1.Enabled = true;
				sl_line2.Enabled = true;
				sl_line3.Enabled = true;
				string adgb = ds.Tables["8030_SEARCH_EMBS"].Select("CODE = '" + sl_embs.EditValue.ToString() + "'")[0]["EMBSADGB"].ToString();
				ADGB_STAT(adgb);

				SetButtonEnable2("0101");
				dat_ycdt.Focus();
			}
		}
		private void ADGB_STAT(string adgb)
		{
			string line_txt = "";
			if (adgb == "1")
			{
				df.GetGW_LINE1Datas(sl_embs.EditValue.ToString(), "'2'", ds);
				sl_line1.Properties.DataSource = ds.Tables["GW_LINE1"];
				df.GetGW_LINE2Datas(sl_embs.EditValue.ToString(), "'5','6'", ds);
				sl_line2.Properties.DataSource = ds.Tables["GW_LINE2"];

				sl_line2.Visible = true;
				line_txt = "[팀장 -> 부서장 -> 대표/담당원장]";
			}
			else if (adgb == "2")
			{
				df.GetGW_LINE1Datas(sl_embs.EditValue.ToString(), "'5','6'", ds);
				sl_line1.Properties.DataSource = ds.Tables["GW_LINE1"];

				sl_line2.Visible = false;
				line_txt = "[부서장 -> 대표/담당원장]";
			}
			else if (adgb == "3")
			{
				df.GetGW_LINE1Datas(sl_embs.EditValue.ToString(), "'5'", ds);
				sl_line1.Properties.DataSource = ds.Tables["GW_LINE1"];

				sl_line2.Visible = false;
				line_txt = "[원장단 -> 담당원장]";
			}
			else if (adgb == "4")
			{
				df.GetGW_LINE1Datas(sl_embs.EditValue.ToString(), "'6'", ds);
				sl_line1.Properties.DataSource = ds.Tables["GW_LINE1"];

				sl_line2.Visible = false;
				line_txt = "[원장단 -> 대표원장]";
			}
			else if (adgb == "5")
			{
				df.GetGW_LINE1Datas(sl_embs.EditValue.ToString(), "'5'", ds);
				sl_line1.Properties.DataSource = ds.Tables["GW_LINE1"];
				sl_line1.EditValue = sl_embs.EditValue;

				sl_line2.Visible = false;
				line_txt = "[담당원장]";
			}
			else if (adgb == "6")
			{
				df.GetGW_LINE1Datas(sl_embs.EditValue.ToString(), "'6'", ds);
				sl_line1.Properties.DataSource = ds.Tables["GW_LINE1"];
				sl_line1.EditValue = sl_embs.EditValue;

				sl_line2.Visible = false;
				line_txt = "[대표원장]";
			}
			else
			{
				df.GetGW_LINE1Datas(sl_embs.EditValue.ToString(), "'1'", ds);
				sl_line1.Properties.DataSource = ds.Tables["GW_LINE1"];

				sl_line2.Visible = false;
				line_txt = "[팀원 -> 팀장]";
			}
			lb_line.Text = line_txt;
			chk_line.Visible = adgb == "1" ? true : false;
			chk_line.Checked = false;
		}
		
		//휴가신청 취소
		private void btn_canc_Click(object sender, EventArgs e)
		{
			SetCancel(1);
		}

		//저장
		private void btn_save_Click(object sender, EventArgs e)
		{
			if (isNoError_um(5))
            {
                Cursor = Cursors.WaitCursor;
                int outVal = 0;
                try
                {
					df.GetDUTY_TRSJREQDatas(sl_embs.EditValue.ToString(), clib.DateToText(dat_ycdt.DateTime), clib.DateToText(dat_ycdt2.DateTime), ds);
					if (ds.Tables["DUTY_TRSJREQ"].Rows.Count > 0)
					{
						DataRow hrow = ds.Tables["DUTY_TRSJREQ"].Rows[0];
						hrow["PAY_YN"] = sr_pay.SelectedIndex < 1 ? 0 : sr_pay.SelectedIndex;
						hrow["HOLI_DAYS"] = df.GetHOLI_DAYS_CALCDatas(clib.DateToText(dat_ycdt.DateTime), clib.DateToText(dat_ycdt2.DateTime), ds);
						if (sl_line3.EditValue != null)
							hrow["LINE_CNT"] = 4;
						else if (sl_line2.EditValue != null)
							hrow["LINE_CNT"] = 3;
						else if (sl_line1.EditValue != null)
							hrow["LINE_CNT"] = 2;	

						hrow["GW_SABN1"] = sl_embs.EditValue.ToString();
						hrow["GW_DT1"] = gd.GetNow();
						hrow["GW_NAME1"] = ds.Tables["8030_SEARCH_EMBS"].Select("CODE ='" + sl_embs.EditValue.ToString() + "'")[0]["NAME"].ToString();
						hrow["GW_JICK1"] = ds.Tables["8030_SEARCH_EMBS"].Select("CODE ='" + sl_embs.EditValue.ToString() + "'")[0]["GRAD_NM"].ToString();
						for (int i = 2; i < 5; i++)
						{
							hrow["GW_SABN" + i.ToString()] = "";
							hrow["GW_DT" + i.ToString()] = "";
							hrow["GW_CHKID" + i.ToString()] = "";
							hrow["GW_NAME" + i.ToString()] = "";
							hrow["GW_JICK" + i.ToString()] = "";
						}
						if (sl_line1.EditValue != null)
						{
							hrow["GW_SABN2"] = sl_line1.EditValue.ToString();
							hrow["GW_NAME2"] = ds.Tables["GW_LINE1"].Select("CODE ='" + sl_line1.EditValue.ToString() + "'")[0]["NAME"].ToString();
							hrow["GW_JICK2"] = ds.Tables["GW_LINE1"].Select("CODE ='" + sl_line1.EditValue.ToString() + "'")[0]["GRAD_NM"].ToString();
						}
						if (sl_line2.EditValue != null)
						{
							hrow["GW_SABN3"] = sl_line2.EditValue.ToString();
							hrow["GW_NAME3"] = ds.Tables["GW_LINE2"].Select("CODE ='" + sl_line2.EditValue.ToString() + "'")[0]["NAME"].ToString();
							hrow["GW_JICK3"] = ds.Tables["GW_LINE2"].Select("CODE ='" + sl_line2.EditValue.ToString() + "'")[0]["GRAD_NM"].ToString();
						}		
						//hrow["SAWON_LV"] = ds.Tables["2020_SEARCH_EMBS"].Select("CODE = '" + sl_embs.EditValue.ToString() + "'")[0]["EMBSADGB"].ToString();
						//hrow["EXCEPT_MID"] = chk_line.Checked == true ? "1" : "";		
						hrow["UPDT"] = gd.GetNow();
						hrow["USID"] = SilkRoad.Config.SRConfig.USID;
						hrow["PSTY"] = "U";
					}
					else
					{
						DataRow hrow = ds.Tables["DUTY_TRSJREQ"].NewRow();
						hrow["SABN"] = sl_embs.EditValue.ToString();
						hrow["REQ_DATE"] = clib.DateToText(dat_ycdt.DateTime);
						hrow["REQ_DATE2"] = clib.DateToText(dat_ycdt2.DateTime);
						hrow["REQ_TYPE"] = sl_gnmu.EditValue.ToString();
						hrow["PAY_YN"] = sr_pay.SelectedIndex < 1 ? 0 : sr_pay.SelectedIndex;
						hrow["HOLI_DAYS"] = df.GetHOLI_DAYS_CALCDatas(clib.DateToText(dat_ycdt.DateTime), clib.DateToText(dat_ycdt2.DateTime), ds);
						
						hrow["AP_TAG"] = "";
						hrow["LINE_CNT"] = 1;
						if (sl_line3.EditValue != null)
							hrow["LINE_CNT"] = 4;
						else if (sl_line2.EditValue != null)
							hrow["LINE_CNT"] = 3;
						else if (sl_line1.EditValue != null)
							hrow["LINE_CNT"] = 2;
						
						hrow["GW_SABN1"] = sl_embs.EditValue.ToString();
						hrow["GW_DT1"] = gd.GetNow();
						hrow["GW_NAME1"] = ds.Tables["8030_SEARCH_EMBS"].Select("CODE ='" + sl_embs.EditValue.ToString() + "'")[0]["NAME"].ToString();
						hrow["GW_JICK1"] = ds.Tables["8030_SEARCH_EMBS"].Select("CODE ='" + sl_embs.EditValue.ToString() + "'")[0]["GRAD_NM"].ToString();
						for (int i = 2; i < 5; i++)
						{
							hrow["GW_SABN" + i.ToString()] = "";
							hrow["GW_DT" + i.ToString()] = "";
							hrow["GW_CHKID" + i.ToString()] = "";
							hrow["GW_NAME" + i.ToString()] = "";
							hrow["GW_JICK" + i.ToString()] = "";
						}
						if (sl_line1.EditValue != null)
						{
							hrow["GW_SABN2"] = sl_line1.EditValue.ToString();
							hrow["GW_NAME2"] = ds.Tables["GW_LINE1"].Select("CODE ='" + sl_line1.EditValue.ToString() + "'")[0]["NAME"].ToString();
							hrow["GW_JICK2"] = ds.Tables["GW_LINE1"].Select("CODE ='" + sl_line1.EditValue.ToString() + "'")[0]["GRAD_NM"].ToString();
						}
						if (sl_line2.EditValue != null)
						{
							hrow["GW_SABN3"] = sl_line2.EditValue.ToString();
							hrow["GW_NAME3"] = ds.Tables["GW_LINE2"].Select("CODE ='" + sl_line2.EditValue.ToString() + "'")[0]["NAME"].ToString();
							hrow["GW_JICK3"] = ds.Tables["GW_LINE2"].Select("CODE ='" + sl_line2.EditValue.ToString() + "'")[0]["GRAD_NM"].ToString();
						}

						hrow["INDT"] = gd.GetNow();
						hrow["UPDT"] = "";
						hrow["USID"] = SilkRoad.Config.SRConfig.USID;
						hrow["PSTY"] = "A";
						ds.Tables["DUTY_TRSJREQ"].Rows.Add(hrow);
					}
                    
                    string[] tableNames = new string[] { "DUTY_TRSJREQ" };
                    SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
                    outVal = cmd.setUpdate(ref ds, tableNames, null);
                }
                catch (Exception ec)
                {
                    MessageBox.Show(ec.Message, "저장오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (outVal > 0)
                        MessageBox.Show("저장되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
					SetCancel(1);
					baseInfoSearch();
                    Cursor = Cursors.Default;
                }
            }
		}
		//삭제
		private void btn_del_Click(object sender, EventArgs e)
		{
			if (isNoError_um(6))
			{
                Cursor = Cursors.WaitCursor;
				//DialogResult dr = MessageBox.Show("삭제하시겠습니까?", "삭제여부", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
				//if (dr == DialogResult.OK)
				//{
				int outVal = 0;
				try
				{
					df.GetDUTY_TRSJREQDatas(sl_embs.EditValue.ToString(), clib.DateToText(dat_ycdt.DateTime), clib.DateToText(dat_ycdt2.DateTime), ds);
					if (ds.Tables["DUTY_TRSJREQ"].Rows.Count > 0)
					{
						DataRow drow = ds.Tables["DUTY_TRSJREQ"].Rows[0];
						if (drow["AP_TAG"].ToString() == "1")
						{
							MessageBox.Show("승인된 내역은 삭제할 수 없습니다.", "삭제오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						else
						{
							df.GetDEL_TRSJREQDatas(ds);								
							DataRow hrow = ds.Tables["DEL_TRSJREQ"].NewRow();
							hrow["SABN"] = drow["SABN"].ToString();
							hrow["REQ_DATE"] = drow["REQ_DATE"].ToString();
							hrow["REQ_DATE2"] = drow["REQ_DATE2"].ToString();
							hrow["REQ_TYPE"] = drow["REQ_TYPE"].ToString();
							hrow["PAY_YN"] = drow["PAY_YN"].ToString();
							hrow["HOLI_DAYS"] = clib.TextToDecimal(drow["HOLI_DAYS"].ToString());
							
							hrow["AP_TAG"] = drow["AP_TAG"].ToString();
							hrow["LINE_CNT"] = clib.TextToInt(drow["LINE_CNT"].ToString());
							for (int i = 1; i < 5; i++)
							{
								hrow["GW_SABN" + i.ToString()] = drow["GW_SABN" + i.ToString()].ToString();
								hrow["GW_DT" + i.ToString()] = drow["GW_DT" + i.ToString()].ToString();
								if (i != 1)
									hrow["GW_CHKID" + i.ToString()] = drow["GW_CHKID" + i.ToString()].ToString();
								hrow["GW_NAME" + i.ToString()] = drow["GW_NAME" + i.ToString()].ToString();
								hrow["GW_JICK" + i.ToString()] = drow["GW_JICK" + i.ToString()].ToString();
							}

							//hrow["SAWON_LV"] = drow["SAWON_LV"].ToString();
							//hrow["EXCEPT_MID"] = drow["EXCEPT_MID"].ToString();
							//hrow["AP_TAG"] = drow["AP_TAG"].ToString();
							//hrow["MID_DT"] = drow["MID_DT"].ToString();
							//hrow["MID_USID"] = drow["MID_USID"].ToString();
							//hrow["AP_DT"] = drow["AP_DT"].ToString();
							//hrow["AP_USID"] = drow["AP_USID"].ToString();
							//hrow["CANC_DT"] = drow["CANC_DT"].ToString();
							//hrow["CANC_USID"] = drow["CANC_USID"].ToString();

							hrow["INDT"] = drow["INDT"].ToString();
							hrow["UPDT"] = drow["UPDT"].ToString();
							hrow["USID"] = drow["USID"].ToString();
							hrow["PSTY"] = drow["PSTY"].ToString();
							hrow["DEL_DT"] = gd.GetNow();
							hrow["DEL_ID"] = SilkRoad.Config.SRConfig.USID;
							ds.Tables["DEL_TRSJREQ"].Rows.Add(hrow);

							ds.Tables["DUTY_TRSJREQ"].Rows[0].Delete();
							string[] tableNames = new string[] { "DUTY_TRSJREQ", "DEL_TRSJREQ" };
							SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
							outVal = cmd.setUpdate(ref ds, tableNames, null);
						}
					}
					else
					{
						MessageBox.Show("등록된 휴가내역이 없어 삭제할 수 없습니다.", "삭제오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				catch (Exception ec)
				{
					MessageBox.Show(ec.Message, "삭제오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				finally
				{
					if (outVal > 0)
						MessageBox.Show("삭제되었습니다.", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);
					SetCancel(1);
					baseInfoSearch();
					Cursor = Cursors.Default;
				}
				//}
			}
		}

        /// <summary>취소버튼</summary>
        private void btn_clear_Click(object sender, EventArgs e)
        {
            SetCancel(0);
        }		

        #endregion

        #region 3 EVENT
		
		//메뉴 활성화시
		private void duty8030_Activated(object sender, EventArgs e)
		{
			//END_CHK();
		}

        private void duty8030_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //취소
            {
                btn_clear.PerformClick();
            }
        }
		//그리드 더블클릭시
		private void grdv_hg_DoubleClick(object sender, EventArgs e)
		{
			DataRow drow = grdv_hg.GetFocusedDataRow();
            if (drow == null)
                return;

			sl_embs.EditValue = grdv_hg.GetFocusedRowCellValue("SABN").ToString().Trim();
			btn_proc.PerformClick();
			df.GetDUTY_TRSJREQDatas(drow["SABN"].ToString(), drow["REQ_DATE"].ToString(), drow["REQ_DATE2"].ToString(), ds);
			if (ds.Tables["DUTY_TRSJREQ"].Rows.Count > 0)
			{
				DataRow hrow = ds.Tables["DUTY_TRSJREQ"].Rows[0];
				sl_gnmu.EditValue = hrow["REQ_TYPE"].ToString();
				dat_ycdt.DateTime = clib.TextToDate(hrow["REQ_DATE"].ToString());
				dat_ycdt2.DateTime = clib.TextToDate(hrow["REQ_DATE2"].ToString());

				if (hrow["AP_TAG"].ToString() == "1" || hrow["AP_TAG"].ToString() == "3" || hrow["AP_TAG"].ToString() == "4" || hrow["AP_TAG"].ToString() == "9")  //1승인,3완료,4진행,9정산
					SetButtonEnable2("0001");
				else
					SetButtonEnable2("0011");
			}
			dat_ycdt.Enabled = false;
			dat_ycdt2.Enabled = false;
			sl_gnmu.Enabled = false;

			string adgb = ds.Tables["8030_SEARCH_EMBS"].Select("CODE = '" + sl_embs.EditValue.ToString() + "'")[0]["EMBSADGB"].ToString();
			ADGB_STAT(adgb);
		}
		
				
		//휴가사용일자 선택시
		private void dat_ycdt_EditValueChanged(object sender, EventArgs e)
		{
			if (clib.DateToText(dat_ycdt2.DateTime) == "")
				dat_ycdt2.DateTime = dat_ycdt.DateTime;
		}
		
		private void END_CHK()
		{
			string yymm = clib.DateToText(dat_ycdt.DateTime).Substring(0, 4) + "." + clib.DateToText(dat_ycdt.DateTime).Substring(4, 2);
			df.Get2020_SEARCH_ENDSDatas(clib.DateToText(dat_ycdt.DateTime).Substring(0, 6), ds);
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

		//중간관리자 제외
		private void chk_line_CheckedChanged(object sender, EventArgs e)
		{
			if (chk_line.Checked == false)
			{
				sl_line1.EditValue = null;
				sl_line2.EditValue = null;
				df.GetGW_LINE1Datas(sl_embs.EditValue.ToString(), "'2'", ds);
				sl_line1.Properties.DataSource = ds.Tables["GW_LINE1"];
				df.GetGW_LINE2Datas(sl_embs.EditValue.ToString(), "'5','6'", ds);
				sl_line2.Properties.DataSource = ds.Tables["GW_LINE2"];

				sl_line2.Visible = true;
				lb_line.Text = "[팀장 -> 부서장 -> 대표/담당원장]";
			}
			else
			{
				sl_line1.EditValue = null;
				df.GetGW_LINE1Datas(sl_embs.EditValue.ToString(), "'5','6'", ds);
				sl_line1.Properties.DataSource = ds.Tables["GW_LINE1"];

				sl_line2.Visible = false;
				lb_line.Text = "[팀장 -> 대표/담당원장]";
			}
			//if (chk_line.Checked == false)
			//	lb_line.Text = "[팀장 -> 부서장 -> 대표원장]";
			//else 
			//	lb_line.Text = "[팀장 -> 대표원장]";
		}
		
		private void grdv_hg_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
		{			
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)            
                e.Info.DisplayText = (e.RowHandle + 1).ToString();     
		}
		private void grdv_del_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
		{
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)            
                e.Info.DisplayText = (e.RowHandle + 1).ToString();   
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
            if (mode == 1)  //조회
            {
                if (admin_lv == 0)
                {
                    MessageBox.Show("조회권한이 없습니다. 인사기본관리의 관리자구분을 확인하세요!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (clib.DateToText(dat_yymm.DateTime) == "")
                {
                    MessageBox.Show("조회년월(시작)을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dat_yymm.Focus();
                    return false;
                }
                else if (clib.DateToText(dat_yymm2.DateTime) == "")
                {
                    MessageBox.Show("조회년월(종료)을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dat_yymm2.Focus();
                    return false;
                }
                else
                {
                    isError = true;
                }
            }
            else if (mode == 5)  //등록
            {
				if (ends_yn2 == "Y")
				{
					MessageBox.Show("최종마감되어 등록할 수 없습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				else if (clib.DateToText(dat_ycdt.DateTime) == "")
				{
					MessageBox.Show("신청일자(시작)이 입력되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					dat_ycdt.Focus();
					return false;
				}
				else if (clib.DateToText(dat_ycdt2.DateTime) == "")
				{
					MessageBox.Show("신청일자(종료)가 입력되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					dat_ycdt2.Focus();
					return false;
				}
				else if (sl_gnmu.EditValue == null)
				{
					MessageBox.Show("휴가유형이 선택되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					sl_gnmu.Focus();
					return false;
				}
				else if (sl_line1.Visible == true && sl_line1.EditValue == null)
				{
					MessageBox.Show("결재자1이 선택되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					sl_line1.Focus();
					return false;
				}
				else if (sl_line2.Visible == true && sl_line2.EditValue == null)
				{
					MessageBox.Show("결재자2가 선택되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					sl_line2.Focus();
					return false;
				}
				else if (sl_line3.Visible == true && sl_line3.EditValue == null)
				{
					MessageBox.Show("결재자3이 선택되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					sl_line3.Focus();
					return false;
				}
				else
				{
					isError = true;
				}
            }
            else if (mode == 6)  //삭제
            {
				if (clib.DateToText(dat_ycdt.DateTime) == "")
				{
					MessageBox.Show("신청일자(시작)이 입력되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					dat_ycdt.Focus();
					return false;
				}
				else if (clib.DateToText(dat_ycdt2.DateTime) == "")
				{
					MessageBox.Show("신청일자(종료)가 입력되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					dat_ycdt2.Focus();
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
			btn_search.Enabled = arr.Substring(0, 1) == "1" ? true : false;
			btn_clear.Enabled = arr.Substring(1, 1) == "1" ? true : false;
		}
		
        private void SetButtonEnable2(string arr)
        {
			btn_proc.Enabled = arr.Substring(0, 1) == "1" ? true : false;
			btn_save.Enabled = arr.Substring(1, 1) == "1" ? true : false;
			btn_del.Enabled = arr.Substring(2, 1) == "1" ? true : false;
			btn_canc.Enabled = arr.Substring(3, 1) == "1" ? true : false;
		}

		#endregion

	}
}

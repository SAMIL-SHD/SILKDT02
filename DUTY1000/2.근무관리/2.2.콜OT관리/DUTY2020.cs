using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using DevExpress.XtraScheduler;
using System.Drawing;

namespace DUTY1000
{
    public partial class duty2020 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();
		
        private string ends_yn = "";
		
        private int admin_lv = 0;
        private string msyn = "";
        private string upyn = "";
        private string p_dpcd = "";
        public duty2020()
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
			if (stat == 1)
			{
				sl_dept.Enabled = p_dpcd == "%" ? true : false;

				df.GetSEARCH_DEPTDatas(ds);
				sl_dept.Properties.DataSource = ds.Tables["SEARCH_DEPT"];
				df.Get2020_SEARCH_EMBSDatas(p_dpcd, ds);
				sl_embs.Properties.DataSource = ds.Tables["2020_SEARCH_EMBS"];

				if (ds.Tables["DUTY_TRSOVTM"] != null)
					ds.Tables["DUTY_TRSOVTM"].Clear();
				if (ds.Tables["SEARCH_OVTM"] != null)
					ds.Tables["SEARCH_OVTM"].Clear();
				grd1.DataSource = null;
				grd1_ex.DataSource = null;
				schedulerStorage1.Appointments.DataSource = null;
			}
			if (stat == 2)
			{
				sl_dept2.Enabled = p_dpcd == "%" ? true : false;

				df.GetSEARCH_DEPTDatas(ds);
				sl_dept2.Properties.DataSource = ds.Tables["SEARCH_DEPT"];
				df.Get2020_SEARCH_EMBSDatas(p_dpcd, ds);
				sl_embs.Properties.DataSource = ds.Tables["2020_SEARCH_EMBS"];

				if (ds.Tables["DUTY_TRSOVTM"] != null)
					ds.Tables["DUTY_TRSOVTM"].Clear();
				if (ds.Tables["SEARCH_OVTM2"] != null)
					ds.Tables["SEARCH_OVTM2"].Clear();
				grd2.DataSource = null;
				grd2_ex.DataSource = null;
				schedulerStorage1.Appointments.DataSource = null;
			}
			if (stat == 3)
			{
				btn_save.Text = "저장";
				btn_save.Image = DUTY1000.Properties.Resources.저장;
				sl_embs.Enabled = true;
				dat_ovdt.Enabled = true;
				sr_gubn.ReadOnly = false;

				txt_cnt1.Enabled = false;
				txt_cnt2.Enabled = false;
				txt_ctime1.Enabled = false;
				txt_ctime2.Enabled = false;
				txt_ot1.Enabled = false;
				txt_ot2.Enabled = false;
				txt_time.Enabled = false;
				mm_remk.Enabled = false;
				txt_cnt1.Text = "";
				txt_cnt2.Text = "";
				txt_ctime1.Text = "";
				txt_ctime2.Text = "";
				txt_ot1.Text = "";
				txt_ot2.Text = "";
				txt_time.Text = "";
				mm_remk.Text = "";

				SetButtonEnable("100");
			}
        }

		private void END_CHK()
		{
			string yymm = clib.DateToText(dat_ovdt.DateTime).Substring(0, 4) + "." + clib.DateToText(dat_ovdt.DateTime).Substring(4, 2);
			df.Get2020_SEARCH_ENDSDatas(clib.DateToText(dat_ovdt.DateTime).Substring(0, 6), ds);
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
        //OT신청내역 조회
        private void baseInfoSearch(string gubn)
        {
			//END_CHK();
			string dt_nm = gubn == "1" ? "SEARCH_OVTM" : "SEARCH_OVTM2";
			if (gubn == "1")
			{
				string dept = sl_dept.EditValue == null ? "%" : sl_dept.EditValue.ToString();
				int t_index = grdv1.TopRowIndex;
				int r_index = grdv1.FocusedRowHandle;	
				df.GetSEARCH_OVTMDatas(gubn, clib.DateToText(dat_frmm.DateTime).Substring(0, 6), clib.DateToText(dat_tomm.DateTime).Substring(0, 6), dept, ds);
				grd1.DataSource = ds.Tables["SEARCH_OVTM"];
				grd1_ex.DataSource = ds.Tables["SEARCH_OVTM"];
				grdv1.TopRowIndex = t_index;
				grdv1.FocusedRowHandle = r_index;
			}
			else if (gubn == "2")
			{
				string dept = sl_dept2.EditValue == null ? "%" : sl_dept2.EditValue.ToString();
				int t_index = grdv2.TopRowIndex;
				int r_index = grdv2.FocusedRowHandle;				
				df.GetSEARCH_OVTMDatas(gubn, clib.DateToText(dat_frmm2.DateTime).Substring(0, 6), clib.DateToText(dat_tomm2.DateTime).Substring(0, 6), dept, ds);
				grd2.DataSource = ds.Tables["SEARCH_OVTM2"];
				grd2_ex.DataSource = ds.Tables["SEARCH_OVTM2"];
				grdv2.TopRowIndex = t_index;
				grdv2.FocusedRowHandle = r_index;
			}
			
			int start_index = 0;
			switch (clib.WeekDay(clib.TextToDateFirst(clib.DateToText(dat_frmm.DateTime))))
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
			int row_count = start_index + clib.TextToInt(clib.DateToText(clib.TextToDateLast(clib.DateToText(dat_frmm.DateTime))).Substring(6, 2).ToString());
			row_count = row_count / 7 + 1;

			schedulerControl1.Views.MonthView.WeekCount = row_count;
			schedulerStorage1.Appointments.ResourceSharing = true;
			schedulerControl1.GroupType = SchedulerGroupType.Resource;
			schedulerControl1.Start = clib.TextToDateFirst(clib.DateToText(dat_frmm.DateTime));
			schedulerStorage1.Appointments.DataSource = ds.Tables[dt_nm];

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

        private void duty2020_Load(object sender, EventArgs e)
        {
			dat_frmm.DateTime = DateTime.Today.AddMonths(-1);
			dat_tomm.DateTime = DateTime.Today.AddMonths(-1);
			sl_dept.EditValue = null;
			dat_frmm2.DateTime = DateTime.Today.AddMonths(-1);
			dat_tomm2.DateTime = DateTime.Today.AddMonths(-1);
			sl_dept2.EditValue = null;
			schedulerControl1.Start = clib.TextToDateFirst(clib.DateToText(DateTime.Today.AddMonths(-1)));

			dat_ovdt.DateTime = DateTime.Today.AddMonths(-1);
			sl_embs.EditValue = null;

			#region 요일체크       
			int start_index = 0;
			switch (clib.WeekDay(clib.TextToDateFirst(clib.DateToText(dat_frmm.DateTime))))
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
			int row_count = start_index + clib.TextToInt(clib.DateToText(clib.TextToDateLast(clib.DateToText(dat_frmm.DateTime))).Substring(6, 2).ToString());
			row_count = row_count / 7 + 1;

			schedulerControl1.Views.MonthView.WeekCount = row_count;
			#endregion
        }
		
		private void duty2020_Shown(object sender, EventArgs e)
		{
            df.GetMSTUSER_CHKDatas(ds);  //부서관리여부
            if (ds.Tables["MSTUSER_CHK"].Rows.Count > 0)
                admin_lv = clib.TextToInt(ds.Tables["MSTUSER_CHK"].Rows[0]["EMBSADGB"].ToString()); //권한레벨

			if (SilkRoad.Config.ACConfig.G_MSYN == "1" || SilkRoad.Config.SRConfig.USID == "SAMIL" || admin_lv > 1)
			{
				admin_lv = 3;
                p_dpcd = "%";
                lb_power.Text = "전체조회 권한";
				sl_dept.Enabled = true;
                lb_power2.Text = "전체조회 권한";
				sl_dept2.Enabled = true;
			}
            else //if (admin_lv == 1)
            {
                p_dpcd = SilkRoad.Config.SRConfig.US_DPCD == null ? null : SilkRoad.Config.SRConfig.US_DPCD.Trim();
                lb_power.Text = "부서조회 권한";
				sl_dept.EditValue = p_dpcd;
				sl_dept.Enabled = false;
                lb_power2.Text = "부서조회 권한";
				sl_dept2.EditValue = p_dpcd;
				sl_dept2.Enabled = false;
			}
    //        else
    //        {
    //            p_dpcd = SilkRoad.Config.SRConfig.US_DPCD == null ? null : SilkRoad.Config.SRConfig.US_DPCD.Trim();
    //            lb_power.Text = "조회권한 없음";				
				//sl_dept.EditValue = p_dpcd;
				//sl_dept.Enabled = false;
    //            lb_power2.Text = "조회권한 없음";				
				//sl_dept2.EditValue = p_dpcd;
				//sl_dept2.Enabled = false;
    //        }
			
            SetCancel(1);
			SetCancel(2);
			SetCancel(3);
		}

        #endregion

        #region 2 Button
		
		//조회
		private void btn_search_Click(object sender, EventArgs e)
		{
			if (isNoError_um(1))
			{
				//dat_yymm.Enabled = false;
				//sl_dept.Enabled = false;
				sl_dept.Enabled = p_dpcd == "%" ? true : false;
				baseInfoSearch("1");
				//SetButtonEnable("01");
			}
		}
        /// <summary>취소버튼</summary>
        private void btn_clear_Click(object sender, EventArgs e)
        {
            SetCancel(1);
        }
		
		//조회
		private void btn_search2_Click(object sender, EventArgs e)
		{
			if (isNoError_um(11))
			{
				//dat_yymm.Enabled = false;
				//sl_dept.Enabled = false;
				sl_dept2.Enabled = p_dpcd == "%" ? true : false;
				baseInfoSearch("2");
				//SetButtonEnable("01");
			}
		}
        /// <summary>취소버튼</summary>
        private void btn_clear2_Click(object sender, EventArgs e)
        {
            SetCancel(2);
        }

		//처리
		private void btn_proc_Click(object sender, EventArgs e)
		{
			if (isNoError_um(2))
			{
				END_CHK();
				sl_embs.Enabled = false;
				dat_ovdt.Enabled = false;
				sr_gubn.ReadOnly = true;
				txt_time.Enabled = true;
				mm_remk.Enabled = true;

				if (sr_gubn.SelectedIndex == 0)
				{
					txt_cnt1.Enabled = true;
					txt_cnt2.Enabled = true;
					txt_ctime1.Enabled = true;
					txt_ctime2.Enabled = true;
					txt_ot1.Enabled = false;
					txt_ot2.Enabled = false;

					txt_cnt1.Focus();
				}
				else
				{
					txt_cnt1.Enabled = false;
					txt_cnt2.Enabled = false;
					txt_ctime1.Enabled = false;
					txt_ctime2.Enabled = false;
					txt_ot1.Enabled = true;
					txt_ot2.Enabled = true;

					txt_ot1.Focus();
				}
				string sldt = clib.DateToText(dat_ovdt.DateTime);
				df.GetDUTY_TRSOVTMDatas(sl_embs.EditValue.ToString(), sldt, (sr_gubn.SelectedIndex + 1).ToString(), ds);
				if (ds.Tables["DUTY_TRSOVTM"].Rows.Count > 0)
				{
					DataRow drow = ds.Tables["DUTY_TRSOVTM"].Rows[0];
					txt_cnt1.Text = drow["CALL_CNT1"].ToString();
					txt_ctime1.Text = drow["CALL_TIME1"].ToString();
					txt_cnt2.Text = drow["CALL_CNT2"].ToString();
					txt_ctime2.Text = drow["CALL_TIME2"].ToString();
					txt_ot1.Text = drow["OT_TIME1"].ToString();
					txt_ot2.Text = drow["OT_TIME2"].ToString();
					txt_time.Text = drow["TIME_REMK"].ToString();
					mm_remk.Text = drow["REMARK"].ToString();
					btn_save.Text = "수정";
					btn_save.Image = DUTY1000.Properties.Resources.수정;
					if (drow["GW_TAG"].ToString() == "")
						SetButtonEnable("011");
					else
						SetButtonEnable("000");
				}
				else
				{
					btn_save.Text = "저장";
					btn_save.Image = DUTY1000.Properties.Resources.저장;
					SetButtonEnable("010");
				}
			}
		}
		//저장
		private void btn_save_Click(object sender, EventArgs e)
		{
			if (isNoError_um(3))
            {
                int outVal = 0;
                try
                {
					Cursor = Cursors.WaitCursor;
					DataRow hrow;
					string sldt = clib.DateToText(dat_ovdt.DateTime);
					df.GetDUTY_TRSOVTMDatas(sl_embs.EditValue.ToString(), sldt, (sr_gubn.SelectedIndex + 1).ToString(), ds);
					if (ds.Tables["DUTY_TRSOVTM"].Rows.Count > 0)
					{
						hrow = ds.Tables["DUTY_TRSOVTM"].Rows[0];
						hrow["UPDT"] = gd.GetNow(); //수정
						hrow["PSTY"] = "U";
					}
					else
					{
						hrow = ds.Tables["DUTY_TRSOVTM"].NewRow();
						hrow["SABN"] = sl_embs.EditValue.ToString();
						hrow["OT_DATE"] = clib.DateToText(dat_ovdt.DateTime);
						hrow["OT_GUBN"] = (sr_gubn.SelectedIndex + 1).ToString();
						hrow["INDT"] = gd.GetNow();
						hrow["UPDT"] = "";
						hrow["PSTY"] = "A";
						ds.Tables["DUTY_TRSOVTM"].Rows.Add(hrow);
					}
					hrow["CALL_CNT1"] = clib.TextToInt(txt_cnt1.Text.ToString());
					hrow["CALL_CNT2"] = clib.TextToInt(txt_cnt2.Text.ToString());
					hrow["CALL_TIME1"] = clib.TextToDecimal(txt_ctime1.Text.ToString());
					hrow["CALL_TIME2"] = clib.TextToDecimal(txt_ctime2.Text.ToString());
					hrow["OT_TIME1"] = clib.TextToDecimal(txt_ot1.Text.ToString());
					hrow["OT_TIME2"] = clib.TextToDecimal(txt_ot2.Text.ToString());
					hrow["TIME_REMK"] = txt_time.Text.ToString();
					hrow["REMARK"] = mm_remk.Text.ToString();
					hrow["USID"] = SilkRoad.Config.SRConfig.USID;
                    
                    string[] tableNames = new string[] { "DUTY_TRSOVTM" };
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
					SetCancel(3);
					baseInfoSearch((sr_gubn.SelectedIndex + 1).ToString());
					sl_embs.Focus();
                    Cursor = Cursors.Default;
                }
            }
		}
		//삭제
		private void btn_del_Click(object sender, EventArgs e)
		{
			if (isNoError_um(4))
			{
				DialogResult dr = MessageBox.Show("삭제하시겠습니까?", "삭제여부", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
				if (dr == DialogResult.OK)
				{
					int outVal = 0;
					try
					{
						Cursor = Cursors.WaitCursor;
						string sldt = clib.DateToText(dat_ovdt.DateTime);
						df.GetDUTY_TRSOVTMDatas(sl_embs.EditValue.ToString(), sldt, (sr_gubn.SelectedIndex + 1).ToString(), ds);
						if (ds.Tables["DUTY_TRSOVTM"].Rows.Count > 0)
						{							
							DataRow drow = ds.Tables["DUTY_TRSOVTM"].Rows[0];

							df.GetDEL_TRSOVTMDatas(ds);
							DataRow hrow = ds.Tables["DEL_TRSOVTM"].NewRow();
							hrow["SABN"] = drow["SABN"].ToString();
							hrow["OT_DATE"] = drow["OT_DATE"].ToString();
							hrow["OT_GUBN"] = drow["OT_GUBN"].ToString();
						
							hrow["CALL_CNT1"] = clib.TextToInt(drow["CALL_CNT1"].ToString());
							hrow["CALL_CNT2"] = clib.TextToInt(drow["CALL_CNT2"].ToString());
							hrow["CALL_TIME1"] = clib.TextToDecimal(drow["CALL_TIME1"].ToString());
							hrow["CALL_TIME2"] = clib.TextToDecimal(drow["CALL_TIME2"].ToString());
							hrow["OT_TIME1"] = clib.TextToDecimal(drow["OT_TIME1"].ToString());
							hrow["OT_TIME2"] = clib.TextToDecimal(drow["OT_TIME2"].ToString());
							hrow["TIME_REMK"] = drow["TIME_REMK"].ToString();
							hrow["REMARK"] = drow["REMARK"].ToString();
							hrow["INDT"] = drow["INDT"].ToString();
							hrow["UPDT"] = drow["UPDT"].ToString();
							hrow["USID"] = drow["USID"].ToString();
							hrow["PSTY"] = drow["PSTY"].ToString();
							
							hrow["DEL_DT"] = gd.GetNow();
							hrow["DEL_ID"] = SilkRoad.Config.SRConfig.USID;
							ds.Tables["DEL_TRSOVTM"].Rows.Add(hrow);

							ds.Tables["DUTY_TRSOVTM"].Rows[0].Delete();

							string[] tableNames = new string[] { "DEL_TRSOVTM", "DUTY_TRSOVTM" };
							SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
							outVal = cmd.setUpdate(ref ds, tableNames, null);
						}
						else
						{
							MessageBox.Show("등록된 OverTime이 없어 삭제할 수 없습니다.", "삭제오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
						SetCancel(3);
						baseInfoSearch((sr_gubn.SelectedIndex + 1).ToString());
						sl_embs.Focus();
						Cursor = Cursors.Default;
					}
				}
			}
		}
		//취소
		private void btn_canc_Click(object sender, EventArgs e)
		{
			SetCancel(3);
		}
        /// <summary>엑셀버튼</summary>
        private void btn_exel_Click(object sender, EventArgs e)
        {
            clib.gridToExcel(grdv1_ex, "콜조회_" + clib.DateToText(DateTime.Now), false);
        }
        /// <summary>엑셀버튼</summary>
        private void btn_exel2_Click(object sender, EventArgs e)
        {
            clib.gridToExcel(grdv2_ex, "OT조회_" + clib.DateToText(DateTime.Now), false);
        }
		
        //승인
		private void btn_ap_save_Click(object sender, EventArgs e)
		{
			if (clib.DateToText(dat_frmm.DateTime).Substring(0, 6) != clib.DateToText(dat_tomm.DateTime).Substring(0, 6))
			{
				MessageBox.Show("조회년월을 동일하게 설정해주세요!", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
				dat_tomm.Focus();
			}
			else if (sl_dept.EditValue == null)
			{
				MessageBox.Show("부서를 선택하세요!", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
				sl_dept.Focus();
			}
			else 
			{
				duty2021 duty2021 = new duty2021(clib.DateToText(dat_frmm.DateTime).Substring(0, 6), sl_dept.EditValue.ToString());
				duty2021.ShowDialog();
				
				baseInfoSearch("1");
			}
			//if (isNoError_um(2))
   //         {
   //             int outVal = 0;
   //             try
   //             {
			//		Cursor = Cursors.WaitCursor;
			//		for (int i = 0; i < ds.Tables["SEARCH_OVTM"].Rows.Count; i++)
			//		{
			//			DataRow drow = ds.Tables["SEARCH_OVTM"].Rows[i];
			//			if (drow["CHK"].ToString() == "1")
			//			{
			//				df.GetDUTY_TRSOVTMDatas(drow["SABN"].ToString(), drow["OT_DATE"].ToString(), drow["OT_GUBN"].ToString(), ds);
			//				if (ds.Tables["DUTY_TRSOVTM"].Rows.Count > 0)
			//				{
			//					if (ds.Tables["DUTY_TRSOVTM"].Rows[0]["AP_TAG"].ToString() != "1")
			//					{
			//						DataRow hrow = ds.Tables["DUTY_TRSOVTM"].Rows[0];
			//						hrow["AP_TAG"] = "1";									
			//						hrow["AP_DT"] = gd.GetNow();
			//						hrow["AP_USID"] = SilkRoad.Config.SRConfig.USID;

			//						string[] tableNames = new string[] { "DUTY_TRSOVTM" };
			//						SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
			//						outVal += cmd.setUpdate(ref ds, tableNames, null);
			//					}
			//				}
			//			}
			//		}
   //             }
   //             catch (Exception ec)
   //             {
   //                 MessageBox.Show(ec.Message, "저장오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
   //             }
   //             finally
   //             {
   //                 if (outVal > 0)
   //                     MessageBox.Show(outVal + "건의 선택된 내역이 승인처리 되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
			//		//SetCancel();
			//		baseInfoSearch("1");
   //                 Cursor = Cursors.Default;
   //             }
   //         }
		}
        //승인취소
        private void btn_ap_canc_Click(object sender, EventArgs e)
        {
			//if (isNoError_um(3))
			//{
			//	int outVal = 0;
			//	try
			//	{
			//		Cursor = Cursors.WaitCursor;
			//		for (int i = 0; i < ds.Tables["SEARCH_OVTM"].Rows.Count; i++)
			//		{
			//			DataRow drow = ds.Tables["SEARCH_OVTM"].Rows[i];
			//			if (drow["C_CHK"].ToString() == "1")
			//			{
			//				df.GetDUTY_TRSOVTMDatas(drow["SABN"].ToString(), drow["OT_DATE"].ToString(), drow["OT_GUBN"].ToString(), ds);
			//				if (ds.Tables["DUTY_TRSOVTM"].Rows.Count > 0)
			//				{
			//					if (ds.Tables["DUTY_TRSOVTM"].Rows[0]["AP_TAG"].ToString() == "1")
			//					{
			//						DataRow hrow = ds.Tables["DUTY_TRSOVTM"].Rows[0];
			//						hrow["AP_TAG"] = "2";
			//						hrow["CANC_DT"] = gd.GetNow();
			//						hrow["CANC_USID"] = SilkRoad.Config.SRConfig.USID;

			//						string[] tableNames = new string[] { "DUTY_TRSOVTM" };
			//						SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
			//						outVal += cmd.setUpdate(ref ds, tableNames, null);
			//					}
			//				}
			//			}
			//		}
			//	}
			//	catch (Exception ec)
			//	{
			//		MessageBox.Show(ec.Message, "삭제오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//	}
			//	finally
			//	{
			//		if (outVal > 0)
   //                     MessageBox.Show(outVal + "건의 선택된 내역이 승인취소 되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
			//		//SetCancel();
			//		baseInfoSearch("1");
			//		Cursor = Cursors.Default;
			//	}
			//}
        }
		
        //승인
		private void btn_ap_save2_Click(object sender, EventArgs e)
		{
			if (clib.DateToText(dat_frmm2.DateTime).Substring(0, 6) != clib.DateToText(dat_tomm2.DateTime).Substring(0, 6))
			{
				MessageBox.Show("조회년월을 동일하게 설정해주세요!", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
				dat_tomm2.Focus();
			}
			else if (sl_dept2.EditValue == null)
			{
				MessageBox.Show("부서를 선택하세요!", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
				sl_dept.Focus();
			}
			else 
			{
				duty2022 duty2022 = new duty2022(clib.DateToText(dat_frmm2.DateTime).Substring(0, 6), sl_dept2.EditValue.ToString());
				duty2022.ShowDialog();
				
				baseInfoSearch("2");
			}

			#region 기존 승인처리
			//if (isNoError_um(2))
   //         {
   //             int outVal = 0;
   //             try
   //             {
			//		Cursor = Cursors.WaitCursor;
			//		for (int i = 0; i < ds.Tables["SEARCH_OVTM2"].Rows.Count; i++)
			//		{
			//			DataRow drow = ds.Tables["SEARCH_OVTM2"].Rows[i];
			//			if (drow["CHK"].ToString() == "1")
			//			{
			//				df.GetDUTY_TRSOVTMDatas(drow["SABN"].ToString(), drow["OT_DATE"].ToString(), drow["OT_GUBN"].ToString(), ds);
			//				if (ds.Tables["DUTY_TRSOVTM"].Rows.Count > 0)
			//				{
			//					if (ds.Tables["DUTY_TRSOVTM"].Rows[0]["AP_TAG"].ToString() != "1")
			//					{
			//						DataRow hrow = ds.Tables["DUTY_TRSOVTM"].Rows[0];
			//						hrow["AP_TAG"] = "1";									
			//						hrow["AP_DT"] = gd.GetNow();
			//						hrow["AP_USID"] = SilkRoad.Config.SRConfig.USID;

			//						string[] tableNames = new string[] { "DUTY_TRSOVTM" };
			//						SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
			//						outVal += cmd.setUpdate(ref ds, tableNames, null);
			//					}
			//				}
			//			}
			//		}
   //             }
   //             catch (Exception ec)
   //             {
   //                 MessageBox.Show(ec.Message, "저장오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
   //             }
   //             finally
   //             {
   //                 if (outVal > 0)
   //                     MessageBox.Show(outVal + "건의 선택된 내역이 승인처리 되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
			//		//SetCancel();
			//		baseInfoSearch("2");
   //                 Cursor = Cursors.Default;
   //             }
   //         }
			#endregion
		}
		//승인취소
		private void btn_ap_canc2_Click(object sender, EventArgs e)
        {
			//if (isNoError_um(3))
			//{
			//	int outVal = 0;
			//	try
			//	{
			//		Cursor = Cursors.WaitCursor;
			//		for (int i = 0; i < ds.Tables["SEARCH_OVTM2"].Rows.Count; i++)
			//		{
			//			DataRow drow = ds.Tables["SEARCH_OVTM2"].Rows[i];
			//			if (drow["C_CHK"].ToString() == "1")
			//			{
			//				df.GetDUTY_TRSOVTMDatas(drow["SABN"].ToString(), drow["OT_DATE"].ToString(), drow["OT_GUBN"].ToString(), ds);
			//				if (ds.Tables["DUTY_TRSOVTM"].Rows.Count > 0)
			//				{
			//					if (ds.Tables["DUTY_TRSOVTM"].Rows[0]["AP_TAG"].ToString() == "1")
			//					{
			//						DataRow hrow = ds.Tables["DUTY_TRSOVTM"].Rows[0];
			//						hrow["AP_TAG"] = "2";
			//						hrow["CANC_DT"] = gd.GetNow();
			//						hrow["CANC_USID"] = SilkRoad.Config.SRConfig.USID;

			//						string[] tableNames = new string[] { "DUTY_TRSOVTM" };
			//						SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
			//						outVal += cmd.setUpdate(ref ds, tableNames, null);
			//					}
			//				}
			//			}
			//		}
			//	}
			//	catch (Exception ec)
			//	{
			//		MessageBox.Show(ec.Message, "삭제오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//	}
			//	finally
			//	{
			//		if (outVal > 0)
   //                     MessageBox.Show(outVal + "건의 선택된 내역이 승인취소 되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
			//		//SetCancel();
			//		baseInfoSearch("2");
			//		Cursor = Cursors.Default;
			//	}
			//}
        }

        #endregion

        #region 3 EVENT
		
		//메뉴 활성화시
		private void duty2020_Activated(object sender, EventArgs e)
		{
			//END_CHK();
		}
		
		//더블클릭시 수정
		private void grdv1_DoubleClick(object sender, EventArgs e)
		{
			DataRow drow = grdv1.GetFocusedDataRow();
            if (drow == null)
                return;

			SetCancel(3);
            sl_embs.EditValue = drow["SABN"].ToString().Trim();
			dat_ovdt.DateTime = clib.TextToDate(drow["OT_DATE"].ToString());
			sr_gubn.SelectedIndex = clib.TextToInt(drow["OT_GUBN"].ToString()) - 1;
			btn_proc.PerformClick();
		}
		//더블클릭시 수정
		private void grdv2_DoubleClick(object sender, EventArgs e)
		{
			DataRow drow = grdv2.GetFocusedDataRow();
            if (drow == null)
                return;

			SetCancel(3);
            sl_embs.EditValue = drow["SABN"].ToString().Trim();
			dat_ovdt.DateTime = clib.TextToDate(drow["OT_DATE"].ToString());
			sr_gubn.SelectedIndex = clib.TextToInt(drow["OT_GUBN"].ToString()) - 1;
			btn_proc.PerformClick();
		}

        private void duty2020_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //취소
            {
                btn_clear.PerformClick();
            }
        }
		
		private void mm_remk_Leave(object sender, EventArgs e)
		{
			btn_save.Focus();
		}
		
		private void grdv1_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
		{			
   //         DataRow drow = grdv1.GetFocusedDataRow();
			//if (drow != null)
			//{
			//	if (grdv1.FocusedColumn.Name == "col_chk")
			//	{
			//		if (ends_yn == "Y")
			//			e.Cancel = true;
			//		else if (drow["AP_TAG"].ToString() == "1")
			//			e.Cancel = true;
			//	}
			//	if (grdv1.FocusedColumn.Name == "col_c_chk")
			//	{
			//		if (ends_yn == "Y")
			//			e.Cancel = true;
			//		else if (drow["AP_TAG"].ToString() != "1")
			//			e.Cancel = true;
			//	}
			//}
		}
		private void grdv2_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
		{
   //         DataRow drow = grdv2.GetFocusedDataRow();
			//if (drow != null)
			//{
			//	if (grdv2.FocusedColumn.Name == "col_chk2")
			//	{
			//		if (ends_yn == "Y")
			//			e.Cancel = true;
			//		else if (drow["AP_TAG"].ToString() == "1")
			//			e.Cancel = true;
			//	}
			//	if (grdv2.FocusedColumn.Name == "col_c_chk2")
			//	{
			//		if (ends_yn == "Y")
			//			e.Cancel = true;
			//		else if (drow["AP_TAG"].ToString() != "1")
			//			e.Cancel = true;
			//	}
			//}
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
            if (mode == 1)  //조회1
            {
                //if (admin_lv == 0)
                //{
                //    MessageBox.Show("조회권한이 없습니다. 인사기본관리의 관리자구분을 확인하세요!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return false;
                //}
                if (clib.DateToText(dat_frmm.DateTime) == "")
                {
                    MessageBox.Show("조회년월(시작)을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dat_frmm.Focus();
                    return false;
                }
                else if (clib.DateToText(dat_tomm.DateTime) == "")
                {
                    MessageBox.Show("조회년월(종료)를 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dat_tomm.Focus();
                    return false;
                }
                else
                {
                    isError = true;
                }
            }
            else if (mode == 11)  //조회2
            {
                //if (admin_lv == 0)
                //{
                //    MessageBox.Show("조회권한이 없습니다. 인사기본관리의 관리자구분을 확인하세요!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return false;
                //}
                if (clib.DateToText(dat_frmm2.DateTime) == "")
                {
                    MessageBox.Show("조회년월(시작)을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dat_frmm2.Focus();
                    return false;
                }
                else if (clib.DateToText(dat_tomm2.DateTime) == "")
                {
                    MessageBox.Show("조회년월(종료)를 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dat_tomm2.Focus();
                    return false;
                }
                else
                {
                    isError = true;
                }
            }
            else if (mode == 2)  //처리
            {
                if (sl_embs.EditValue == null)
				{
					MessageBox.Show("근무자가 선택되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					sl_embs.Focus();
					return false;
				}
				else if (clib.DateToText(dat_ovdt.DateTime) == "")
				{
					MessageBox.Show("근무일자가 입력되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					dat_ovdt.Focus();
					return false;
				}
                else
                {
                    isError = true;
                }
            }
            else if (mode == 3)  //등록
            {
				END_CHK();
				if (ends_yn == "Y")
				{
					MessageBox.Show("최종마감되어 등록할 수 없습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				else if (clib.DateToText(dat_ovdt.DateTime) == "")
				{
					MessageBox.Show("근무일자가 입력되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					dat_ovdt.Focus();
					return false;
				}
				else
				{
					isError = true;
				}
            }
            else if (mode == 4)  //삭제
            {
				END_CHK();
				if (ends_yn == "Y")
				{
					MessageBox.Show("최종마감되어 삭제할 수 없습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				else if (clib.DateToText(dat_ovdt.DateTime) == "")
				{
					MessageBox.Show("근무일자가 입력되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					dat_ovdt.Focus();
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
			btn_save.Enabled = arr.Substring(1, 1) == "1" ? true : false;
			btn_del.Enabled = arr.Substring(2, 1) == "1" ? true : false;
		}


		#endregion
		
		private void btn_call_all_Click(object sender, EventArgs e)
		{
			duty2023 duty2023 = new duty2023();
			duty2023.ShowDialog();

			baseInfoSearch("1");
		}
		private void btn_ot_all_Click(object sender, EventArgs e)
		{
			duty2024 duty2024 = new duty2024();
			duty2024.ShowDialog();

			baseInfoSearch("2");
		}
	}
}

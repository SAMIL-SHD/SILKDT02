using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using DevExpress.XtraScheduler;
using System.Drawing;

namespace DUTY1000
{
    public partial class duty2010 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();
		
		private int admin_lv = 0;
        private string msyn = "";
        private string upyn = "";
        private string p_dpcd = "";

        public duty2010()
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
			dat_yymm.Enabled = true;
			sl_dept.Enabled = p_dpcd == "%" ? true : false;
			btn_search.Enabled = true;
			
			srLabel4.Text = "[마감체크]";

			if (ds.Tables["DUTY_TRSOREQ"] != null)
				ds.Tables["DUTY_TRSOREQ"].Clear();
			schedulerStorage1.Appointments.DataSource = null;

			df.Get2010_SEARCH_DEPTDatas(ds);
			sl_dept.Properties.DataSource = ds.Tables["2010_SEARCH_DEPT"];
        }

        //사원기본정보 및 연차정보 , 휴가사용내역, 휴일사용내역 조회
        private void baseInfoSearch()
        {
			//string yymm = clib.DateToText(dat_yymm.DateTime).Substring(0, 4) + "." + clib.DateToText(dat_yymm.DateTime).Substring(4, 2);
			//df.Get2010_SEARCH_ENDSDatas(clib.DateToText(dat_yymm.DateTime).Substring(0, 6), ds);
			//if (ds.Tables["2010_SEARCH_ENDS"].Rows.Count > 0) //마감월이 저장되어 있으면
			//{
			//	DataRow irow = ds.Tables["2010_SEARCH_ENDS"].Rows[0];
			//	srLabel4.Text = irow["CLOSE_YN"].ToString() == "Y" ? "[" + yymm + " 최종마감]" : irow["CLOSE_YN"].ToString() == "N" ? "[" + yymm + " 마감취소]" : "[ ]";
			//}
			//else
			//{
			//	srLabel4.Text = "[" + yymm + " 마감전]";
			//}

			if (sl_dept.EditValue != null)
			{
				df.Get3010_SEARCH_CLOSDatas(clib.DateToText(dat_yymm.DateTime).Substring(0, 6), sl_dept.EditValue.ToString(), ds);
				if (ds.Tables["3010_SEARCH_CLOS"].Rows.Count > 0) //마감일이 저장되어 있으면
				{
					DataRow irow = ds.Tables["3010_SEARCH_CLOS"].Rows[0];
					dat_fr.Text = irow["POS_FRDT"].ToString();
					dat_to.Text = irow["POS_TODT"].ToString();
					lb_fr.Text = "(" + clib.WeekDay(clib.TextToDate(irow["POS_FRDT"].ToString())) + ")";
					lb_to.Text = "(" + clib.WeekDay(clib.TextToDate(irow["POS_TODT"].ToString())) + ")";
					if (clib.WeekDay(clib.TextToDate(irow["POS_FRDT"].ToString())) == "토")
						lb_fr.ForeColor = Color.Blue;
					else if (clib.WeekDay(clib.TextToDate(irow["POS_FRDT"].ToString())) == "일")
						lb_fr.ForeColor = Color.Red;
					else
						lb_fr.ForeColor = Color.Black;

					if (clib.WeekDay(clib.TextToDate(irow["POS_TODT"].ToString())) == "토")
						lb_to.ForeColor = Color.Blue;
					else if (clib.WeekDay(clib.TextToDate(irow["POS_TODT"].ToString())) == "일")
						lb_to.ForeColor = Color.Red;
					else
						lb_to.ForeColor = Color.Black;

					srLabel4.Text = irow["CLOSE_YN"].ToString() == "Y" ? "[신청마감]" : "[신청중]";
				}
				else
				{
					dat_fr.Text = "";
					dat_to.Text = "";
					lb_fr.Text = "( )";
					lb_to.Text = "( )";
					srLabel4.Text = "[ ]";
				}
			}
			else
			{
				dat_fr.Text = "";
				dat_to.Text = "";
				lb_fr.Text = "( )";
				lb_to.Text = "( )";
				srLabel4.Text = "[ ]";
			}

			string deptcode = sl_dept.EditValue == null ? "%" : sl_dept.EditValue.ToString();
			df.GetSEARCH_OREQDatas(clib.DateToText(dat_yymm.DateTime).Substring(0, 6), deptcode, ds);
			grd1.DataSource = ds.Tables["DUTY_TRSOREQ"];
			
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
			schedulerStorage1.Appointments.DataSource = ds.Tables["DUTY_TRSOREQ"];
			//this.schedulerStorage1.Appointments.DataSource = ds;
			//this.schedulerStorage1.Appointments.DataMember = "DUTY_TRSOREQ"; // "MSTAPPO", "Appointments";

			schedulerStorage1.Appointments.Mappings.Type = "TYPE";         //타입
			schedulerStorage1.Appointments.Mappings.Start = "FR_DATE";     //시작날짜
			schedulerStorage1.Appointments.Mappings.End = "TO_DATE";       //끝날짜
			schedulerStorage1.Appointments.Mappings.AllDay = "ALLDAY";         //전일
			schedulerStorage1.Appointments.Mappings.Subject = "G_SNM";     //주제
			schedulerStorage1.Appointments.Mappings.Location = "SAWON_NM";     //장소
			schedulerStorage1.Appointments.Mappings.Description = "REMARK";    //설명
			schedulerStorage1.Appointments.Mappings.Status = "STATUS";         //상태
			schedulerStorage1.Appointments.Mappings.Label = "LABEL";           //라벨
		}


        #endregion

        #region 1 Form

        private void duty2010_Load(object sender, EventArgs e)
        {
			dat_yymm.DateTime = DateTime.Today;
			schedulerControl1.Start = clib.TextToDateFirst(clib.DateToText(DateTime.Today));			
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

			sl_dept.EditValue = null;
            SetCancel();
        }
				
		private void duty2010_Shown(object sender, EventArgs e)
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
			}
            else if (admin_lv == 1)
            {
                p_dpcd = SilkRoad.Config.SRConfig.US_DPCD == null ? null : SilkRoad.Config.SRConfig.US_DPCD.Trim();
                lb_power.Text = "부서조회 권한";
				sl_dept.EditValue = p_dpcd;
				sl_dept.Enabled = false;
			}
            else
            {
                p_dpcd = SilkRoad.Config.SRConfig.US_DPCD == null ? null : SilkRoad.Config.SRConfig.US_DPCD.Trim();
                lb_power.Text = "조회권한 없음";				
				sl_dept.EditValue = p_dpcd;
				sl_dept.Enabled = false;
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
    //            p_dpcd = SilkRoad.Config.SRConfig.US_DPCD == null ? null : SilkRoad.Config.SRConfig.US_DPCD.Trim();
    //            lb_power.Text = upyn == "1" ? "부서조회 권한" : "조회권한 없음";

				//sl_dept.EditValue = p_dpcd;
				//sl_dept.Enabled = false;
    //        }
		}

        #endregion

        #region 2 Button
		
		//조회
		private void btn_search_Click(object sender, EventArgs e)
		{
			if (isNoError_um(1))
			{
				dat_yymm.Enabled = false;
				sl_dept.Enabled = false;
				btn_search.Enabled = false;
				baseInfoSearch();				
			}
		}

        //저장버튼
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (isNoError_um(1))
            {
                Cursor = Cursors.WaitCursor;
                int outVal = 0;
                try
                {
                    foreach(DataRow hrow in ds.Tables["MSTAPPO"].Rows)  //테이블
                    {
                        if (hrow.RowState != DataRowState.Deleted)
                        {
                            if (hrow["UNIQUEID"].ToString().Equals("")) //인덱스값.. 신규
                            {
                                hrow["INDT"] = gd.GetNow().Substring(0, 8);
                                hrow["UPDT"] = " ";
                                hrow["PSTY"] = "A";
                            }
                            else
                            {
                                hrow["UPDT"] = gd.GetNow().Substring(0, 8); //수정
                                hrow["PSTY"] = "U";
                            }
                            hrow["USID"] = SilkRoad.Config.SRConfig.USID;
                        }
                    }
                    
                    string[] tableNames = new string[] { "MSTAPPO" };

                    SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
                    outVal = cmd.setUpdate(ref ds, tableNames, null);

                    if (outVal <= 0)
                    {
                        MessageBox.Show("저장된 내용이 없습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("저장되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ec)
                {
                    MessageBox.Show(ec.Message, "저장오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (outVal > 0) { SetCancel(); baseInfoSearch(); }
                    if (outVal == -1)
                    {
                        baseInfoSearch();
                    }
                    Cursor = Cursors.Default;
                }
            }
        }
        //삭제
        private void btn_del_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("삭제하시겠습니까?", "삭제여부", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                int outVal = 0;
                try
                {
                    if (ds.Tables["MSTAPPO"].Rows.Count > 0)
                    {
                        string[] tableNames = new string[] { "MSTAPPO" };
                        SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
                        outVal = cmd.setUpdate(ref ds, tableNames, null);

                        if (outVal <= 0)
                        {
                            MessageBox.Show("삭제된 내용이 없습니다.", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ec)
                {
                    MessageBox.Show(ec.Message, "삭제오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    SetCancel();
                    baseInfoSearch();
                    Cursor = Cursors.Default;
                }
            }
        }

        /// <summary>취소버튼</summary>
        private void btn_clear_Click(object sender, EventArgs e)
        {
            SetCancel();
        }		

        #endregion

        #region 3 EVENT
		
        private void duty2010_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //취소
            {
                btn_clear.PerformClick();
            }
        }

		//세부내역 조회 및 수정
        private void schedulerControl1_EditAppointmentFormShowing(object sender, AppointmentFormEventArgs e)
        {
			//DevExpress.XtraScheduler.SchedulerControl scheduler = ((DevExpress.XtraScheduler.SchedulerControl)(sender));
			//DUTY1000.CustomAppointmentForm form = new DUTY1000.CustomAppointmentForm(scheduler, e.Appointment, e.OpenRecurrenceForm);
			//try
			//{
			//	e.DialogResult = form.ShowDialog();
			//	e.Handled = true;
			//}
			//finally
			//{
			//	form.Dispose();
			//}
		}

        private void schedulerControl1_Click(object sender, EventArgs e)
        {

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
                    MessageBox.Show("조회년월을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dat_yymm.Focus();
                    return false;
                }
                //else if (upyn != "1" && sl_dept.Enabled == false)
                //{
                //    MessageBox.Show("부서권한이 없습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    sl_dept.Focus();
                //    return false;
                //}
				//else if (sl_dept.EditValue == null)
				//{
				//	MessageBox.Show("부서를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
				//	sl_dept.Focus();
				//	return false;
				//}
				else
                {
                    isError = true;
                }
            }
            else if (mode == 2)  //저장
            {
                isError = true;
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
            btn_save.Enabled = arr.Substring(0, 1) == "1" ? true : false;
          //  btn_del.Enabled = arr.Substring(1, 1) == "1" ? true : false;
            btn_clear.Enabled = arr.Substring(2, 1) == "1" ? true : false;
        }


		#endregion

		private void schedulerControl1_DoubleClick(object sender, EventArgs e)
		{

		}

	}
}

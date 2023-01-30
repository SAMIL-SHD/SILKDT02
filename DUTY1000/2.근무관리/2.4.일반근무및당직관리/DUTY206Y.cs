using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using DevExpress.XtraScheduler;
using System.Drawing;

namespace DUTY1000
{
    public partial class duty206y : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();
        public duty206y()
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
			dat_yymm.Enabled = true;
			
			if (ds.Tables["DUTY_TRSDREQ"] != null)
				ds.Tables["DUTY_TRSDREQ"].Clear();
			schedulerStorage1.Appointments.DataSource = null;
			
            df.GetSEARCH_DEPTDatas(ds); // 부서 리스트 룩업
            sl_dept.Properties.DataSource = ds.Tables["SEARCH_DEPT"];

			df.Get2060_DANG_GNMUDatas(ds);
			sl_gnmu.Properties.DataSource = ds.Tables["2060_DANG_GNMU"];
			//df.GetLOOK_DANG_EMBSDatas(ds);
			//sl_embs.Properties.DataSource = ds.Tables["LOOK_DANG_EMBS"];

			if (stat == 0)
			{
				sl_gnmu.Enabled = false;
				sl_embs.Enabled = false;
				cmb_day.Enabled = false;
			}
        }
		
        //당직숙직내역 조회
        private void baseInfoSearch()
        {
            string dept = sl_dept.EditValue == null ? "%" : sl_dept.EditValue.ToString();
			df.GetSEARCH_DREQDatas(clib.DateToText(dat_yymm.DateTime).Substring(0, 6), dept, ds);
			grd1.DataSource = ds.Tables["SEARCH_DREQ"];
			
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
			schedulerStorage1.Appointments.DataSource = ds.Tables["SEARCH_DREQ"];

			schedulerStorage1.Appointments.Mappings.Type = "TYPE";         //타입
			schedulerStorage1.Appointments.Mappings.Start = "FR_DATE";     //시작날짜
			schedulerStorage1.Appointments.Mappings.End = "TO_DATE";       //끝날짜
			schedulerStorage1.Appointments.Mappings.AllDay = "ALLDAY";         //전일
			schedulerStorage1.Appointments.Mappings.Subject = "G_FNM";     //주제
			schedulerStorage1.Appointments.Mappings.Location = "SAWON_NM";     //장소
			schedulerStorage1.Appointments.Mappings.Description = "REMARK";    //설명
			schedulerStorage1.Appointments.Mappings.Status = "STATUS";         //상태
			schedulerStorage1.Appointments.Mappings.Label = "LABEL";           //라벨
		}
        #endregion

        #region 1 Form

        private void duty206y_Load(object sender, EventArgs e)
        {
			dat_yymm.DateTime = DateTime.Today;
			sl_dept.EditValue = null;
			schedulerControl1.Start = clib.TextToDateFirst(clib.DateToText(DateTime.Today));

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

			sl_gnmu.EditValue = null;
			sl_embs.EditValue = null;
            SetCancel(0);
        }

        #endregion

        #region 2 Button
		
		//조회
		private void btn_search_Click(object sender, EventArgs e)
		{
			if (isNoError_um(1))
			{
				dat_yymm.Enabled = false;
				cmb_mm.SelectedIndex = clib.TextToInt(clib.DateToText(dat_yymm.DateTime).Substring(4, 2)) - 1;
                cmb_day.Properties.Items.Clear();
				for (int i = 1; i <= clib.TextToInt(clib.DateToText(clib.TextToDateLast(clib.DateToText(dat_yymm.DateTime))).Substring(6, 2)); i++)
				{
					cmb_day.Properties.Items.Add(i.ToString() + "일");
				}

				sl_gnmu.Enabled = true;
				sl_embs.Enabled = true;
				cmb_day.Enabled = true;
				cmb_day.SelectedIndex = 0;

				baseInfoSearch();
			}
		}

        //근무추가
		private void btn_add_Click(object sender, EventArgs e)
		{
			if (isNoError_um(2))
            {
                Cursor = Cursors.WaitCursor;
                int outVal = 0;
                try
                {
					string sldt = clib.DateToText(dat_yymm.DateTime).Substring(0, 6) + (cmb_day.SelectedIndex + 1).ToString().PadLeft(2, '0');
					df.GetDUTY_TRSDREQDatas(sl_embs.EditValue.ToString(), sldt, ds);
					if (ds.Tables["DUTY_TRSDREQ"].Rows.Count > 0)
					{
						DataRow hrow = ds.Tables["DUTY_TRSDREQ"].Rows[0];
						if (ds.Tables["LOOK_DANG_EMBS"].Select("CODE = '" + sl_embs.EditValue.ToString() + "'").Length > 0)
							hrow["DEPTCODE"] = ds.Tables["LOOK_DANG_EMBS"].Select("CODE = '" + sl_embs.EditValue.ToString() + "'")[0]["DEPTCODE"].ToString();
						else
							hrow["DEPTCODE"] = "";
						hrow["REQ_TYPE"] = sl_gnmu.EditValue.ToString();
						hrow["UPDT"] = gd.GetNow(); //수정
						hrow["USID"] = SilkRoad.Config.SRConfig.USID;
						hrow["PSTY"] = "U";
					}
					else
					{
						DataRow hrow = ds.Tables["DUTY_TRSDREQ"].NewRow();
						hrow["SABN"] = sl_embs.EditValue.ToString();
						hrow["REQ_DATE"] = sldt;
						if (ds.Tables["LOOK_DANG_EMBS"].Select("CODE = '" + sl_embs.EditValue.ToString() + "'").Length > 0)
							hrow["DEPTCODE"] = ds.Tables["LOOK_DANG_EMBS"].Select("CODE = '" + sl_embs.EditValue.ToString() + "'")[0]["DEPTCODE"].ToString();
						else
							hrow["DEPTCODE"] = "";
						hrow["REQ_TYPE"] = sl_gnmu.EditValue.ToString();
						hrow["INDT"] = gd.GetNow();
						hrow["UPDT"] = "";
						hrow["USID"] = SilkRoad.Config.SRConfig.USID;
						hrow["PSTY"] = "A";
						ds.Tables["DUTY_TRSDREQ"].Rows.Add(hrow);
					}
                    
                    string[] tableNames = new string[] { "DUTY_TRSDREQ" };
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
            DialogResult dr = MessageBox.Show("삭제하시겠습니까?", "삭제여부", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                int outVal = 0;
                try
                {
					string sldt = clib.DateToText(dat_yymm.DateTime).Substring(0, 6) + (cmb_day.SelectedIndex + 1).ToString().PadLeft(2, '0');
					df.GetDUTY_TRSDREQDatas(sl_embs.EditValue.ToString(), sldt, ds);
					if (ds.Tables["DUTY_TRSDREQ"].Rows.Count > 0)
					{
						ds.Tables["DUTY_TRSDREQ"].Rows[0].Delete();
						string[] tableNames = new string[] { "DUTY_TRSDREQ" };
						SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
						outVal = cmd.setUpdate(ref ds, tableNames, null);
					}
					else
					{
						MessageBox.Show("등록된 근무가 없어 삭제할 수 없습니다.", "삭제오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            }
        }

        /// <summary>취소버튼</summary>
        private void btn_clear_Click(object sender, EventArgs e)
        {
            SetCancel(0);
        }		

        #endregion

        #region 3 EVENT
		
        private void duty2060_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //취소
            {
                btn_clear.PerformClick();
            }
        }
		
		//더블클릭시 등록,수정
		private void grdv1_DoubleClick(object sender, EventArgs e)
		{
			DataRow drow = grdv1.GetFocusedDataRow();
            if (drow == null)
                return;
			
            sl_gnmu.EditValue = drow["REQ_TYPE"].ToString().Trim() == "" ? null : drow["REQ_TYPE"].ToString();
            sl_embs.EditValue = drow["SABN"].ToString().Trim() == "" ? null : drow["SABN"].ToString();
			cmb_day.SelectedIndex = clib.TextToInt(drow["REQ_DATE"].ToString().Substring(6, 2)) - 1;
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
            else if (mode == 2)  //저장
            {
                if (sl_gnmu.EditValue == null)
                {
                    MessageBox.Show("근무를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sl_gnmu.Focus();
                    return false;
                }
                else if (sl_embs.EditValue == null)
                {
                    MessageBox.Show("직원을 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sl_embs.Focus();
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
            //btn_save.Enabled = arr.Substring(0, 1) == "1" ? true : false;
          //  btn_del.Enabled = arr.Substring(1, 1) == "1" ? true : false;
            //btn_clear.Enabled = arr.Substring(2, 1) == "1" ? true : false;
        }


		#endregion

		private void schedulerControl1_DoubleClick(object sender, EventArgs e)
		{

		}

		private void btn_info_Click(object sender, EventArgs e)
		{			
			duty2061 duty2061 = new duty2061();
			duty2061.ShowDialog();
			
			sl_embs.EditValue = null;
			//df.GetLOOK_DANG_EMBSDatas(ds);
			//sl_embs.Properties.DataSource = ds.Tables["LOOK_DANG_EMBS"];
		}
	}
}

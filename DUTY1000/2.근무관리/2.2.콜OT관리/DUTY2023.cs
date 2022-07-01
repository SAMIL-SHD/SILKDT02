using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;


namespace DUTY1000
{
    public partial class duty2023 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();
        private string ends_yn = "";
        private int admin_lv = 0;
        private string p_dpcd = "";

        public duty2023()
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
			df.GetSEARCH_DEPTDatas(ds);
			sl_dept.Properties.DataSource = ds.Tables["SEARCH_DEPT"];

			dat_ovdt.Enabled = true;
			sl_dept.Enabled = true;
			txt_cnt1.Enabled = true;
			txt_cnt2.Enabled = true;
			txt_ctime1.Enabled = true;
			txt_ctime2.Enabled = true;
			txt_time.Enabled = true;
			mm_remk.Enabled = true;

			grd1.DataSource = null;
            SetButtonEnable("100");
        }

        #endregion

        #region 1 Form

        private void duty2023_Load(object sender, EventArgs e)
        {
            df.GetMSTUSER_CHKDatas(ds);  //부서관리여부
            if (ds.Tables["MSTUSER_CHK"].Rows.Count > 0)
                admin_lv = clib.TextToInt(ds.Tables["MSTUSER_CHK"].Rows[0]["EMBSADGB"].ToString()); //권한레벨

			if (SilkRoad.Config.SRConfig.US_GUBN == "1" || SilkRoad.Config.SRConfig.USID == "SAMIL" || admin_lv > 1)
			{
				admin_lv = 3;
                p_dpcd = "%";
				sl_dept.Enabled = true;
			}
            else if (admin_lv == 1)
            {
                p_dpcd = SilkRoad.Config.SRConfig.US_DPCD == null ? null : SilkRoad.Config.SRConfig.US_DPCD.Trim();
				sl_dept.EditValue = p_dpcd;
				sl_dept.Enabled = false;
			}
            else
            {
                p_dpcd = SilkRoad.Config.SRConfig.US_DPCD == null ? null : SilkRoad.Config.SRConfig.US_DPCD.Trim();
				sl_dept.EditValue = p_dpcd;
				sl_dept.Enabled = false;
            }
			
			dat_ovdt.DateTime = DateTime.Today;
			sl_dept.EditValue = null;

			SetCancel();
        }

        #endregion

        #region 2 Button

        //처리
        private void btn_proc_Click(object sender, EventArgs e)
        {
			if (isNoError_um(1))
			{
				Proc();
				
				dat_ovdt.Enabled = false;
				sl_dept.Enabled = false;
				txt_cnt1.Enabled = false;
				txt_cnt2.Enabled = false;
				txt_ctime1.Enabled = false;
				txt_ctime2.Enabled = false;
				txt_time.Enabled = false;
				mm_remk.Enabled = false;
			}
        }

        private void Proc()
        {
			df.GetSEARCH_DEPT_CALLDatas(sl_dept.EditValue.ToString(), ds);
			if (ds.Tables["SEARCH_DEPT_CALL"].Rows.Count > 0)
			{
				for (int i = 0; i < ds.Tables["SEARCH_DEPT_CALL"].Rows.Count; i++)
				{
					DataRow drow = ds.Tables["SEARCH_DEPT_CALL"].Rows[i];
					drow["DATE_NM"] = clib.DateToText(dat_ovdt.DateTime).Substring(0, 4) + "." + clib.DateToText(dat_ovdt.DateTime).Substring(4, 2) + "." + clib.DateToText(dat_ovdt.DateTime).Substring(6, 2);
					
					drow["CALL_CNT1"] = clib.TextToInt(txt_cnt1.Text.ToString());
					drow["CALL_CNT2"] = clib.TextToInt(txt_cnt2.Text.ToString());
					drow["CALL_TIME1"] = clib.TextToDecimal(txt_ctime1.Text.ToString());
					drow["CALL_TIME2"] = clib.TextToDecimal(txt_ctime2.Text.ToString());
					drow["TIME_REMK"] = txt_time.Text.ToString();
					drow["REMARK"] = mm_remk.Text.ToString();					
				}
			}
			grd1.DataSource = ds.Tables["SEARCH_DEPT_CALL"];
            SetButtonEnable("011");
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
					for (int i = 0; i < ds.Tables["SEARCH_DEPT_CALL"].Rows.Count; i++)
					{
						DataRow drow = ds.Tables["SEARCH_DEPT_CALL"].Rows[i];
						if (drow["CHK"].ToString() == "1")
						{
							DataRow hrow;
							df.GetDUTY_TRSOVTMDatas(drow["SABN"].ToString(), drow["DATE_NM"].ToString().Replace(".", ""), "1", ds);
							if (ds.Tables["DUTY_TRSOVTM"].Rows.Count > 0)
							{
								hrow = ds.Tables["DUTY_TRSOVTM"].Rows[0];
								hrow["UPDT"] = gd.GetNow(); //수정
								hrow["PSTY"] = "U";
							}
							else
							{
								hrow = ds.Tables["DUTY_TRSOVTM"].NewRow();
								hrow["SABN"] = drow["SABN"].ToString();
								hrow["OT_DATE"] = clib.DateToText(dat_ovdt.DateTime);
								hrow["OT_GUBN"] = "1";
								hrow["INDT"] = gd.GetNow();
								hrow["UPDT"] = "";
								hrow["PSTY"] = "A";
								ds.Tables["DUTY_TRSOVTM"].Rows.Add(hrow);
							}
							hrow["CALL_CNT1"] = clib.TextToInt(txt_cnt1.Text.ToString());
							hrow["CALL_CNT2"] = clib.TextToInt(txt_cnt2.Text.ToString());
							hrow["CALL_TIME1"] = clib.TextToDecimal(txt_ctime1.Text.ToString());
							hrow["CALL_TIME2"] = clib.TextToDecimal(txt_ctime2.Text.ToString());
							hrow["OT_TIME1"] = 0;
							hrow["OT_TIME2"] = 0;
							hrow["TIME_REMK"] = txt_time.Text.ToString();
							hrow["REMARK"] = mm_remk.Text.ToString();
							hrow["USID"] = SilkRoad.Config.SRConfig.USID;

							if (hrow["GW_TAG"].ToString().Trim() == "")  //그룹웨어 상신 없을때
							{
								string[] tableNames = new string[] { "DUTY_TRSOVTM" };
								SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
								outVal += cmd.setUpdate(ref ds, tableNames, null);
							}
						}
					}
				}
				catch (Exception ec)
				{
					MessageBox.Show(ec.Message, "저장오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				finally
				{
                    if (outVal > 0)
                        MessageBox.Show("저장되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
					Cursor = Cursors.Default;
					SetCancel();
				}
			}
        }

        /// <summary>취소버튼</summary>
        private void btn_clear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("해당 내역을 취소하시겠습니까?", "취소", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                SetCancel();
            }
        }

        #endregion

        #region 3 EVENT

        private void duty2023_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //취소
            {
                btn_clear.PerformClick();
            }
        }
		
		private void END_CHK()
		{
			df.Get2020_SEARCH_ENDSDatas(clib.DateToText(dat_ovdt.DateTime).Substring(0, 6), ds);
			if (ds.Tables["2020_SEARCH_ENDS"].Rows.Count > 0) //마감월이 저장되어 있으면
			{
				DataRow irow = ds.Tables["2020_SEARCH_ENDS"].Rows[0];
				ends_yn = irow["CLOSE_YN"].ToString();
			}
			else
			{
				ends_yn = "";
			}
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
				if (ends_yn == "Y")
				{
					MessageBox.Show("최종마감되어 처리 할 수 없습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				else if (clib.DateToText(dat_ovdt.DateTime) == "")
				{
					MessageBox.Show("근무일자가 입력되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					dat_ovdt.Focus();
					return false;
				}
				else if (sl_dept.EditValue == null)
				{
					MessageBox.Show("부서가 선택되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					sl_dept.Focus();
					return false;
				}
				else
				{
					isError = true;
				}
            }
			else if (mode == 2) //저장
			{
				if (ds.Tables["SEARCH_DEPT_CALL"].Select("CHK='1'").Length < 1)
				{
					MessageBox.Show("선택된 내역이 없습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            btn_clear.Enabled = arr.Substring(2, 1) == "1" ? true : false;
        }

        #endregion
    }
}

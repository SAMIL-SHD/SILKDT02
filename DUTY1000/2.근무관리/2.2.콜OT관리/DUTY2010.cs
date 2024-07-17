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
		
        private string ends_yn = "";		
        private int admin_lv = 0;

        public duty2010()
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
				df.GetSEARCH_DEPT_POWERDatas(admin_lv, ds);
				sl_dept.Properties.DataSource = ds.Tables["SEARCH_DEPT_POWER"];
				df.GetSEARCH_EMBS_POWERDatas(admin_lv, ds);
				sl_embs.Properties.DataSource = ds.Tables["SEARCH_EMBS_POWER"];

				if (ds.Tables["DUTY_TRSCALL"] != null)
					ds.Tables["DUTY_TRSCALL"].Clear();
				if (ds.Tables["SEARCH_CALL"] != null)
					ds.Tables["SEARCH_CALL"].Clear();
				grd1.DataSource = null;
			}
            if (stat == 9)
            {
                btn_save.Text = "저  장";
				btn_save.Image = DUTY1000.Properties.Resources.저장;
				sl_embs.Enabled = true;
				dat_ovdt.Enabled = true;
				sr_gubn.ReadOnly = false;

				txt_cnt.Enabled = false;
				mm_remk.Enabled = false;
				txt_cnt.Text = "";
				mm_remk.Text = "";

				SetButtonEnable("1000");
			}
        }

		private void END_CHK()
		{
			string yymm = clib.DateToText(dat_ovdt.DateTime).Substring(2, 2) + "." + clib.DateToText(dat_ovdt.DateTime).Substring(4, 2);
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
        //콜 내역 조회
        private void baseInfoSearch()
        {
            string dept = sl_dept.EditValue == null ? "%" : sl_dept.EditValue.ToString();
            int t_index = grdv1.TopRowIndex;
            int r_index = grdv1.FocusedRowHandle;
            df.GetSEARCH_CALLDatas(admin_lv, clib.DateToText(dat_frmm.DateTime).Substring(0, 6), clib.DateToText(dat_tomm.DateTime).Substring(0, 6), dept, ds);
            grd1.DataSource = ds.Tables["SEARCH_CALL"];
            if (ds.Tables["SEARCH_CALL"].Rows.Count == 0)
                MessageBox.Show("조회된 내역이 없습니다!", "조회", MessageBoxButtons.OK, MessageBoxIcon.Error);

            grdv1.TopRowIndex = t_index;
            grdv1.FocusedRowHandle = r_index;
        }

        #endregion

        #region 1 Form

        private void duty2010_Load(object sender, EventArgs e)
        {
			dat_frmm.DateTime = DateTime.Today;
			dat_tomm.DateTime = DateTime.Today;
			sl_dept.EditValue = null;

            dat_ovdt.DateTime = DateTime.Today;
			sl_embs.EditValue = null;
        }
		
		private void duty2010_Shown(object sender, EventArgs e)
		{
            df.GetMSTUSER_CHKDatas(ds);  //부서관리여부
            if (ds.Tables["MSTUSER_CHK"].Rows.Count > 0)
                admin_lv = clib.TextToInt(ds.Tables["MSTUSER_CHK"].Rows[0]["EMBSADGB"].ToString()); //권한레벨

			if (SilkRoad.Config.ACConfig.G_MSYN == "1" || SilkRoad.Config.SRConfig.USID == "SAMIL" )
			{
				admin_lv = 3;
                lb_power.Text = "전체조회 권한";
			}
            else if (admin_lv == 1)
            {
                lb_power.Text = "부서조회 권한";
			}
			
            SetCancel(1);
            SetCancel(9);
        }

        #endregion

        #region 2 Button
        
        //조회
        private void btn_search_Click(object sender, EventArgs e)
		{
			if (isNoError_um(11))
			{
				baseInfoSearch();
			}
		}
        /// <summary>취소버튼</summary>
        private void btn_clear_Click(object sender, EventArgs e)
        {
            SetCancel(1);
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

				mm_remk.Enabled = true;
                txt_cnt.Enabled = true;

                string sldt = clib.DateToText(dat_ovdt.DateTime);
				df.GetDUTY_TRSCALLDatas(sl_embs.EditValue.ToString(), sldt, (sr_gubn.SelectedIndex + 1).ToString(), ds);
				if (ds.Tables["DUTY_TRSCALL"].Rows.Count > 0)
				{
					DataRow drow = ds.Tables["DUTY_TRSCALL"].Rows[0];
					txt_cnt.Text = drow["CALL_CNT"].ToString();
					mm_remk.Text = drow["REMARK"].ToString();
					btn_save.Text = "수  정";
					btn_save.Image = DUTY1000.Properties.Resources.수정;
                    SetButtonEnable("0111");
                }
				else
				{
					btn_save.Text = "저  장";
					btn_save.Image = DUTY1000.Properties.Resources.저장;
					SetButtonEnable("0101");
                }

                txt_cnt.Focus();
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
					df.GetDUTY_TRSCALLDatas(sl_embs.EditValue.ToString(), sldt, (sr_gubn.SelectedIndex + 1).ToString(), ds);
					if (ds.Tables["DUTY_TRSCALL"].Rows.Count > 0)
					{
						hrow = ds.Tables["DUTY_TRSCALL"].Rows[0];
						hrow["UPDT"] = gd.GetNow(); //수정
						hrow["PSTY"] = "U";
					}
					else
					{
						hrow = ds.Tables["DUTY_TRSCALL"].NewRow();
						hrow["SABN"] = sl_embs.EditValue.ToString();
						hrow["CALL_DATE"] = clib.DateToText(dat_ovdt.DateTime);
						hrow["CALL_GUBN"] = (sr_gubn.SelectedIndex + 1).ToString();
						hrow["INDT"] = gd.GetNow();
						hrow["UPDT"] = "";
						hrow["PSTY"] = "A";
						ds.Tables["DUTY_TRSCALL"].Rows.Add(hrow);
					}
					hrow["CALL_CNT"] = clib.TextToInt(txt_cnt.Text.ToString());
					hrow["REMARK"] = mm_remk.Text.ToString();
					hrow["USID"] = SilkRoad.Config.SRConfig.USID;
                    
                    string[] tableNames = new string[] { "DUTY_TRSCALL" };
                    SilkRoad.DbCmd_DT02.DbCmd_DT02 cmd = new SilkRoad.DbCmd_DT02.DbCmd_DT02();
                    outVal = cmd.setUpdate(ref ds, tableNames, null);
                }
                catch (Exception ec)
                {
                    MessageBox.Show(ec.Message, "저장오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (outVal > 0)
                    {
                        MessageBox.Show("저장되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SetCancel(9);
                        baseInfoSearch();
                        sl_embs.Focus();
                    }
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
						df.GetDUTY_TRSCALLDatas(sl_embs.EditValue.ToString(), sldt, (sr_gubn.SelectedIndex + 1).ToString(), ds);
						if (ds.Tables["DUTY_TRSCALL"].Rows.Count > 0)
						{							
							DataRow drow = ds.Tables["DUTY_TRSCALL"].Rows[0];

							df.GetDEL_TRSCALLDatas(ds);
							DataRow hrow = ds.Tables["DEL_TRSCALL"].NewRow();
							hrow["SABN"] = drow["SABN"].ToString();
							hrow["CALL_DATE"] = drow["CALL_DATE"].ToString();
							hrow["CALL_GUBN"] = drow["CALL_GUBN"].ToString();
						
							hrow["CALL_CNT"] = clib.TextToInt(drow["CALL_CNT"].ToString());
							hrow["REMARK"] = drow["REMARK"].ToString();
							hrow["INDT"] = drow["INDT"].ToString();
							hrow["UPDT"] = drow["UPDT"].ToString();
							hrow["USID"] = drow["USID"].ToString();
							hrow["PSTY"] = drow["PSTY"].ToString();
							
							hrow["DEL_DT"] = gd.GetNow();
							hrow["DEL_ID"] = SilkRoad.Config.SRConfig.USID;
							ds.Tables["DEL_TRSCALL"].Rows.Add(hrow);

							ds.Tables["DUTY_TRSCALL"].Rows[0].Delete();

							string[] tableNames = new string[] { "DEL_TRSCALL", "DUTY_TRSCALL" };
							SilkRoad.DbCmd_DT02.DbCmd_DT02 cmd = new SilkRoad.DbCmd_DT02.DbCmd_DT02();
							outVal = cmd.setUpdate(ref ds, tableNames, null);
						}
						else
						{
							MessageBox.Show("등록된 콜 내역이이 없어 삭제할 수 없습니다.", "삭제오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
					catch (Exception ec)
					{
						MessageBox.Show(ec.Message, "삭제오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					finally
					{
                        if (outVal > 0)
                        {
                            MessageBox.Show("삭제되었습니다.", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SetCancel(9);
                            baseInfoSearch();
                            sl_embs.Focus();
                        }
						Cursor = Cursors.Default;
					}
				}
			}
		}
		//취소
		private void btn_canc_Click(object sender, EventArgs e)
		{
			SetCancel(9);
		}
        /// <summary>엑셀버튼</summary>
        private void btn_exel_Click(object sender, EventArgs e)
        {
            clib.gridToExcel(grdv1, "콜근무관리_" + clib.DateToText(DateTime.Now), false);
        }
        #endregion

        #region 3 EVENT
        		
		//더블클릭시 수정
		private void grdv1_DoubleClick(object sender, EventArgs e)
		{
			DataRow drow = grdv1.GetFocusedDataRow();
            if (drow == null)
                return;

			SetCancel(9);
            sl_embs.EditValue = drow["SABN"].ToString().Trim();
			dat_ovdt.DateTime = clib.TextToDate(drow["CALL_DATE"].ToString());
			sr_gubn.SelectedIndex = clib.TextToInt(drow["CALL_GUBN"].ToString()) - 1;
			btn_proc.PerformClick();
		}
		

        private void duty2010_KeyDown(object sender, KeyEventArgs e)
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
            if (mode == 11)  //조회1
            {
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
                else if (clib.TextToDecimal(txt_cnt.Text.ToString()) == 0)
                {
                    MessageBox.Show("건수가 입력되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_cnt.Focus();
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
            btn_canc.Enabled = arr.Substring(3, 1) == "1" ? true : false;
        }


        #endregion

    }
}

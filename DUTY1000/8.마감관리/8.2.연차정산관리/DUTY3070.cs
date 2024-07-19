using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using System.Data.OleDb;
using System.Collections;

namespace DUTY1000
{
    public partial class duty3070 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();
        private string ends_yn = "";

        public duty3070()
        {
            InitializeComponent();
        }

        #region 0. Initialization

        private void SetCancel(int stat)
		{
			if (stat == 1)
			{
				if (ds.Tables["S_DUTY_TRSHREQ"] != null)
					ds.Tables["S_DUTY_TRSHREQ"].Clear();
				grd_search.DataSource = null;
			}
			else if (stat == 2)
			{
				if (ds.Tables["SEARCH_HREQ"] != null)
					ds.Tables["SEARCH_HREQ"].Clear();
				grd1.DataSource = null;
				SetButtonEnable("100000");
				dat_yymm.Enabled = true;
			}
			else if (stat == 3)
			{
				if (ds.Tables["S_DUTY_TRSHREQ2"] != null)
					ds.Tables["S_DUTY_TRSHREQ2"].Clear();
				pv_grd.DataSource = null;
			}
		}

        #endregion

        #region 1 Form

        private void duty3070_Load(object sender, EventArgs e)
        {
			dat_s_yymm.DateTime = DateTime.Now;
			dat_yymm.DateTime = DateTime.Now;

			dat_frmm.DateTime = clib.TextToDate(clib.DateToText(DateTime.Now).Substring(0, 4) + "0101");
			dat_tomm.DateTime = DateTime.Now;
        }
		private void duty3070_Shown(object sender, EventArgs e)
		{
			SetCancel(2);
            btn_refresh.PerformClick();
        }

        #endregion

        #region 2 Button
		
		
		private void btn_s_search_Click(object sender, EventArgs e)
		{
			if (isNoError_um(1))
			{
                search();
			}
        }
        private void search()
        {
            df.GetS_DUTY_TRSHREQDatas(clib.DateToText(dat_s_yymm.DateTime).Substring(0, 6), ds);
            grd_search.DataSource = ds.Tables["S_DUTY_TRSHREQ"];
        }

        private void btn_s_canc_Click(object sender, EventArgs e)
		{
			SetCancel(1);
		}
		//엑셀변환
		private void btn_excel_Click(object sender, EventArgs e)
		{
            clib.gridToExcel(grdv_search, "연차정산내역_" + clib.DateToText(DateTime.Now), true);
		}

		//처리
		private void btn_proc_Click(object sender, EventArgs e)
		{
			if (isNoError_um(2))
			{
				df.GetSEARCH_HREQDatas(clib.DateToText(dat_yymm.DateTime).Substring(0, 6), ds);                
				if (ds.Tables["SEARCH_HREQ"].Rows.Count > 0)				
                    SetButtonEnable("011111");                
				else				
                    SetButtonEnable("010011");                				

				grd1.DataSource = ds.Tables["SEARCH_HREQ"];	
				dat_yymm.Enabled = false;
			}
        }
        //사원추가
        private void btn_lineadd_Click(object sender, EventArgs e)
        {
            if (sl_embs.EditValue != null)
            {
                if (ds.Tables["SEARCH_HREQ"].Select("SABN = '" + sl_embs.EditValue.ToString() + "'").Length > 0)
                {
                    MessageBox.Show(sl_embs.Text.ToString() + "님은 이미 추가되어 있습니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DataRow nrow = ds.Tables["SEARCH_HREQ"].NewRow();
                    nrow["YYMM_NM"] = clib.DateToText(dat_yymm.DateTime).Substring(0, 4) + "-" + clib.DateToText(dat_yymm.DateTime).Substring(4, 2);
                    nrow["EMBSDPCD"] = ds.Tables["8030_SEARCH_EMBS"].Select("CODE = '" + sl_embs.EditValue.ToString() + "'")[0]["EMBSDPCD"].ToString();
                    nrow["DEPT_NM"] = ds.Tables["8030_SEARCH_EMBS"].Select("CODE = '" + sl_embs.EditValue.ToString() + "'")[0]["DEPT_NM"].ToString();
                    nrow["SABN_NM"] = sl_embs.Text.ToString();
                    nrow["SEQNO"] = 0;
                    nrow["SABN"] = sl_embs.EditValue.ToString();
                    nrow["GUBN"] = "C";
                    nrow["REQ_YEAR"] = clib.DateToText(dat_yymm.DateTime).Substring(0, 4);
                    nrow["REQ_DATE"] = clib.DateToText(clib.TextToDate(clib.DateToText(dat_yymm.DateTime).Substring(0, 6) + "01").AddMonths(1).AddDays(-1));
                    nrow["REQ_DATE2"] = clib.DateToText(clib.TextToDate(clib.DateToText(dat_yymm.DateTime).Substring(0, 6) + "01").AddMonths(1).AddDays(-1));
                    nrow["REQ_TYPE"] = "";
                    nrow["REQ_TYPE2"] = "";
                    nrow["YC_DAYS"] = 0;
                    nrow["AP_TAG"] = "9";
                    nrow["AP_DT"] = "";
                    nrow["AP_USID"] = "";
                    nrow["RT_DT"] = "";
                    nrow["RT_USID"] = "";
                    nrow["LINE_CNT"] = 0;
                    nrow["LINE_MAX"] = 0;
                    nrow["LINE_REMK"] = "";
                    nrow["REMARK1"] = "";
                    nrow["REMARK2"] = "";
                    nrow["USID"] = SilkRoad.Config.SRConfig.USID;
                    nrow["PSTY"] = "A";
                    ds.Tables["SEARCH_HREQ"].Rows.Add(nrow);
                }
            }
        }
        //사원삭제
        private void btn_linedel_Click(object sender, EventArgs e)
        {
            DataRow drow = grdv1.GetFocusedDataRow();
            if (drow != null)
            {
                drow["UPDT"] = gd.GetNow();
                drow["USID"] = SilkRoad.Config.SRConfig.USID;
                drow.Delete();
            }
        }
        //저장
        private void btn_save_Click(object sender, EventArgs e)
		{
			string yymm = clib.DateToText(dat_yymm.DateTime).Substring(0, 4) + "." + clib.DateToText(dat_yymm.DateTime).Substring(4, 2);
			END_CHK(clib.DateToText(dat_yymm.DateTime).Substring(0, 6));
			if (ends_yn == "Y")
			{
				MessageBox.Show(yymm + "월은 최종 마감되어 저장할 수 없습니다.", "마감완료", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (MessageBox.Show("해당 연차정산내역을 저장하시겠습니까?", "저장", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
							== DialogResult.OK)
			{
				Cursor = Cursors.WaitCursor;
				int outVal = 0;
				try
                {
                    decimal seqno = df.GetHREQ_SEQNODatas();
                    for (int i = 0; i < ds.Tables["SEARCH_HREQ"].Rows.Count; i++)
					{
						DataRow drow = ds.Tables["SEARCH_HREQ"].Rows[i];
                        if (drow.RowState != DataRowState.Deleted)
                        {
                            if (clib.TextToDecimal(drow["SEQNO"].ToString()) == 0)
                            {
                                drow["SEQNO"] = seqno;
                                drow["INDT"] = gd.GetNow();
                                drow["UPDT"] = "";
                                seqno++;
                            }
                            else
                            {
                                drow["UPDT"] = gd.GetNow();
                                drow["USID"] = SilkRoad.Config.SRConfig.USID;
                            }
                        }
                    }

                    string[] tableNames = new string[] { "SEARCH_HREQ" };
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
                        MessageBox.Show("해당 내용이 저장되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SetCancel(2);
                        search();
                    }
					Cursor = Cursors.Default;
				}
			}
        }
        //삭제
        private void btn_del_Click(object sender, EventArgs e)
        {
            string yymm = clib.DateToText(dat_s_yymm.DateTime).Substring(0, 4) + "." + clib.DateToText(dat_s_yymm.DateTime).Substring(4, 2);
            END_CHK(clib.DateToText(dat_s_yymm.DateTime).Substring(0, 6));
            if (ends_yn == "Y")
            {
                MessageBox.Show(yymm + "월은 최종 마감되어 삭제할 수 없습니다.", "마감완료", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ds.Tables["SEARCH_HREQ"].Rows.Count > 0)
            {
                DialogResult dr = MessageBox.Show(yymm + "월의 연차정산내역을 모두 삭제하시겠습니까?", "삭제여부", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    int outVal = 0;
                    try
                    {
                        for (int i = 0; i < ds.Tables["SEARCH_HREQ"].Rows.Count; i++)
                        {
                            DataRow drow = ds.Tables["SEARCH_HREQ"].Rows[i];
                            if (drow.RowState != DataRowState.Deleted)
                            {
                                drow["UPDT"] = gd.GetNow();
                                drow["USID"] = SilkRoad.Config.SRConfig.USID;
                                drow.Delete();
                            }
                        }

                        string[] tableNames = new string[] { "SEARCH_HREQ" };
                        SilkRoad.DbCmd_DT02.DbCmd_DT02 cmd = new SilkRoad.DbCmd_DT02.DbCmd_DT02();
                        outVal = cmd.setUpdate(ref ds, tableNames, null);
                    }
                    catch (Exception ec)
                    {
                        MessageBox.Show(ec.Message, "삭제오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if (outVal > 0)
                        {
                            MessageBox.Show(yymm + "월의 연차정산내역이 삭제되었습니다.", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SetCancel(2);
                            search();
                        }
                        Cursor = Cursors.Default;
                    }
                }
            }
            else
            {
                MessageBox.Show(yymm + "월은 삭제할 내역이 없습니다.", "삭제오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //취소
        private void btn_canc_Click(object sender, EventArgs e)
		{
			SetCancel(2);
		}
		
		//연차내역조회
		private void btn_search2_Click(object sender, EventArgs e)
		{
			if (isNoError_um(3))
			{
				df.GetS_DUTY_TRSHREQ2Datas(clib.DateToText(dat_frmm.DateTime).Substring(0, 6), clib.DateToText(dat_tomm.DateTime).Substring(0, 6), ds);
				pv_grd.DataSource = ds.Tables["S_DUTY_TRSHREQ2"];
			}
		}		
		private void btn_canc2_Click(object sender, EventArgs e)
		{
			SetCancel(3);
		}
		private void btn_s_excel_Click(object sender, EventArgs e)
		{
			clib.gridToExcel(pv_grd, "연차내역조회_"  + clib.DateToText(DateTime.Now), true);
		}

        #endregion

        #region 3 EVENT

        private void duty3030_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //종료
            {
                btn_exit.PerformClick();
            }
        }

        private void grdv_search_DoubleClick(object sender, EventArgs e)
        {
            DataRow drow = grdv_search.GetFocusedDataRow();

            SetButtonEnable("100000");
            dat_yymm.DateTime = clib.TextToDate(drow["REQ_DATE"].ToString());
            btn_proc.PerformClick();
        }
        private void btn_refresh_Click(object sender, EventArgs e)
        {
            //df.GetLOOK_DANG_EMBSDatas(ds);
            //sl_embs.Properties.DataSource = ds.Tables["LOOK_DANG_EMBS"];
            df.Get8030_SEARCH_EMBSDatas("%", ds);
            sl_embs.Properties.DataSource = ds.Tables["8030_SEARCH_EMBS"];
            sl_embs.EditValue = null;
        }

        private void END_CHK(string yymm)
		{
			df.Get2020_SEARCH_ENDSDatas(yymm, ds);
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
            bool isError = true;
            if (mode == 1)  //처리내역 조회
            {
                if (clib.DateToText(dat_s_yymm.DateTime) == "")
                {
                    MessageBox.Show("조회년월을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dat_s_yymm.Focus();
                    return false;
				}
                else
                {
                    isError = true;
                }
            }
            else if (mode == 2)  //처리
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
            else if (mode == 3) //조회
            {
                if (clib.DateToText(dat_frmm.DateTime) == "")
                {
                    MessageBox.Show("조회년월(fr)을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dat_frmm.Focus();
                    return false;
				}
                else if (clib.DateToText(dat_tomm.DateTime) == "")
                {
                    MessageBox.Show("조회년월(to)을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dat_tomm.Focus();
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
		
		//private void SetButtonEnable(string arr)
		//{
		//	btn_s_search.Enabled = arr.Substring(0, 1) == "1" ? true : false;
		//	btn_s_canc.Enabled = arr.Substring(1, 1) == "1" ? true : false;
		//}

		private void SetButtonEnable(string arr)
		{
			btn_proc.Enabled = arr.Substring(0, 1) == "1" ? true : false;
			btn_save.Enabled = arr.Substring(1, 1) == "1" ? true : false;
            btn_del.Enabled = arr.Substring(2, 1) == "1" ? true : false;
            btn_linedel.Enabled = arr.Substring(3, 1) == "1" ? true : false;
            btn_canc.Enabled = arr.Substring(4, 1) == "1" ? true : false;
            btn_lineadd.Enabled = arr.Substring(5, 1) == "1" ? true : false;
        }


        #endregion

    }
}

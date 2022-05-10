using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using DevExpress.XtraScheduler;
using System.Drawing;

namespace DUTY1000
{
    public partial class duty5080 : SilkRoad.Form.Base.FormX
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
		
        private string use_frdt = "";
        private string use_todt = "";
        public duty5080()
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
				if (ds.Tables["5080_AP_YCHG_LIST"] != null)
					ds.Tables["5080_AP_YCHG_LIST"].Clear();
				grd_ap.DataSource = null;
			}
			if (stat == 1)
			{
			}
        }

		private void END_CHK()
		{
			//string yymm = clib.DateToText(dat_yymm.DateTime).Substring(0, 4) + "." + clib.DateToText(dat_yymm.DateTime).Substring(4, 2);
			//df.Get2020_SEARCH_ENDSDatas(clib.DateToText(dat_yymm.DateTime).Substring(0, 6), ds);
			//if (ds.Tables["2020_SEARCH_ENDS"].Rows.Count > 0) //마감월이 저장되어 있으면
			//{
			//	DataRow irow = ds.Tables["2020_SEARCH_ENDS"].Rows[0];
			//	ends_yn = irow["CLOSE_YN"].ToString();
			//	lb_ends.Text = irow["CLOSE_YN"].ToString() == "Y" ? "[" + yymm + " 최종마감 완료]" : irow["CLOSE_YN"].ToString() == "N" ? "[" + yymm + " 최종마감 취소]" : "[ ]";
			//}
			//else
			//{
			//	ends_yn = "";
			//	lb_ends.Text = "[" + yymm + " 최종마감 작업전]";
			//}
		}

        #endregion

        #region 1 Form

        private void duty5080_Load(object sender, EventArgs e)
        {

        }
		
		private void duty5080_Shown(object sender, EventArgs e)
		{			
            SetCancel(0);
            SetCancel(1);
			
			string type = cmb_type.SelectedIndex == 0 ? "%" : cmb_type.SelectedIndex.ToString();
			df.Get5080_AP_YCHG_LISTDatas(type, SilkRoad.Config.SRConfig.USID, ds);
			grd_ap.DataSource = ds.Tables["5080_AP_YCHG_LIST"];
		}

        #endregion

        #region 2 Button	
		
		//승인내역 조회
		private void btn_ap_search_Click(object sender, EventArgs e)
		{
			string type = cmb_type.SelectedIndex == 0 ? "%" : cmb_type.SelectedIndex.ToString();
			df.Get5080_AP_YCHG_LISTDatas(type,  SilkRoad.Config.SRConfig.USID, ds);
			grd_ap.DataSource = ds.Tables["5080_AP_YCHG_LIST"];
			if (ds.Tables["5080_AP_YCHG_LIST"].Rows.Count == 0)
				MessageBox.Show("완결된 연차/휴가내역이 없습니다!", "결재", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		//승인내역 조회clear
		private void btn_ap_clear_Click(object sender, EventArgs e)
		{
			SetCancel(0);
		}
        //연차승인
		private void btn_ap_save_Click(object sender, EventArgs e)
		{
			if (isNoError_um(1))
            {
                Cursor = Cursors.WaitCursor;
                int outVal = 0;
                try
                {
					for (int i = 0; i < ds.Tables["5080_AP_YCHG_LIST"].Rows.Count; i++)
					{
						DataRow drow = ds.Tables["5080_AP_YCHG_LIST"].Rows[i];
						if (drow["CHK"].ToString() == "1")
						{
							df.GetDUTY_TRSHREQDatas(drow["SABN"].ToString(), drow["REQ_DATE"].ToString(), ds);
							if (ds.Tables["DUTY_TRSHREQ"].Rows.Count > 0)
							{
								DataRow hrow = ds.Tables["DUTY_TRSHREQ"].Rows[0];
								if (hrow["AP_TAG"].ToString() != "1")
								{
									hrow["AP_TAG"] = "4";
									if (hrow["GW_DT2"].ToString().Trim() == "")
									{
										hrow["GW_DT2"] = gd.GetNow();
										hrow["GW_CHKID2"] = SilkRoad.Config.SRConfig.USID;
										if (hrow["GW_SABN3"].ToString().Trim() == "")
											hrow["AP_TAG"] = "1";
									}
									else if (hrow["GW_DT3"].ToString().Trim() == "")
									{
										hrow["GW_DT3"] = gd.GetNow();
										hrow["GW_CHKID3"] = SilkRoad.Config.SRConfig.USID;
										if (hrow["GW_SABN4"].ToString().Trim() == "")
											hrow["AP_TAG"] = "1";
									}
									else if (hrow["GW_DT4"].ToString().Trim() == "")
									{
										hrow["GW_DT4"] = gd.GetNow();
										hrow["GW_CHKID4"] = SilkRoad.Config.SRConfig.USID;
										hrow["AP_TAG"] = "1";
									}
									string[] tableNames = new string[] { "DUTY_TRSHREQ" };
									SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
									outVal += cmd.setUpdate(ref ds, tableNames, null);
								}
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
                        MessageBox.Show(outVal + "건의 선택된 내역이 승인처리 되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
					string type = cmb_type.SelectedIndex == 0 ? "%" : cmb_type.SelectedIndex.ToString();
					df.Get5080_AP_YCHG_LISTDatas(type, SilkRoad.Config.SRConfig.USID, ds);
					grd_ap.DataSource = ds.Tables["5080_AP_YCHG_LIST"];
                    Cursor = Cursors.Default;
                }
            }
		}
		
        //연차,휴가 승인취소
		private void btn_ap_canc_Click(object sender, EventArgs e)
		{
			if (isNoError_um(2))
            {
                Cursor = Cursors.WaitCursor;
                int outVal = 0;
                try
                {
					for (int i = 0; i < ds.Tables["5080_AP_YCHG_LIST"].Rows.Count; i++)
					{
						DataRow drow = ds.Tables["5080_AP_YCHG_LIST"].Rows[i];
						if (drow["C_CHK"].ToString() == "1")
						{
							string tb_nm = drow["GUBN"].ToString() == "1" ? "DUTY_TRSHREQ" : "DUTY_TRSJREQ";
							df.Get5060_DUTY_TRSHREQDatas(drow["GUBN"].ToString() , drow["SABN"].ToString(), drow["REQ_DATE"].ToString(), ds);
							if (ds.Tables[tb_nm].Rows.Count > 0)
							{
								DataRow hrow = ds.Tables[tb_nm].Rows[0];
								if (hrow["AP_TAG"].ToString() == "1")
								{
									hrow["AP_TAG"] = "4";
									if (hrow["GW_DT4"].ToString().Trim() != "")
									{
										hrow["GW_DT4"] = "";
										hrow["GW_CHKID4"] = SilkRoad.Config.SRConfig.USID;
									}
									else if (hrow["GW_DT3"].ToString().Trim() != "")
									{
										hrow["GW_DT3"] = "";
										hrow["GW_CHKID3"] = SilkRoad.Config.SRConfig.USID;
									}
									if (hrow["GW_DT2"].ToString().Trim() != "")
									{
										hrow["GW_DT2"] = "";
										hrow["GW_CHKID2"] = SilkRoad.Config.SRConfig.USID;
									}
									string[] tableNames = new string[] { tb_nm };
									SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
									outVal += cmd.setUpdate(ref ds, tableNames, null);
								}
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
                        MessageBox.Show(outVal + "건의 선택된 내역이 취소처리 되었습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
					string type = cmb_type.SelectedIndex == 0 ? "%" : cmb_type.SelectedIndex.ToString();
					df.Get5080_AP_YCHG_LISTDatas(type, SilkRoad.Config.SRConfig.USID, ds);
					grd_ap.DataSource = ds.Tables["5080_AP_YCHG_LIST"];
                    Cursor = Cursors.Default;
                }
            }
		}

        #endregion

        #region 3 EVENT
		
		//메뉴 활성화시
		private void duty5080_Activated(object sender, EventArgs e)
		{

		}
		
		private void grdv_ap_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
		{			
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)            
                e.Info.DisplayText = (e.RowHandle + 1).ToString();   
		}

		private void grdv_ap_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
		{
            DataRow drow = grdv_ap.GetFocusedDataRow();
			if (drow != null)
			{
				if (grdv_ap.FocusedColumn.Name.ToString() == "col_chk")
				{
					if (drow["CHK_STAT"].ToString() != "1")
						e.Cancel = true;
				}
				else if (grdv_ap.FocusedColumn.Name.ToString() == "col_c_chk")
				{
					if (drow["C_CHK_STAT"].ToString() != "1")
						e.Cancel = true;
				}
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

            if (mode == 1)  //연차,휴가승인
            {
				if (ds.Tables["5080_AP_YCHG_LIST"] != null)
				{
					ds.Tables["5080_AP_YCHG_LIST"].AcceptChanges();
					if (ds.Tables["5080_AP_YCHG_LIST"].Select("CHK='1'").Length == 0)
					{
						MessageBox.Show("선택된 내역이 없습니다!", "승인에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return false;
					}
					else
					{
						isError = true;
					}
				}
            }
			else if (mode == 2)  //승인취소
			{
				if (ds.Tables["5080_AP_YCHG_LIST"] != null)
				{
					ds.Tables["5080_AP_YCHG_LIST"].AcceptChanges();
					if (ds.Tables["5080_AP_YCHG_LIST"].Select("C_CHK='1'").Length == 0)
					{
						MessageBox.Show("선택된 내역이 없습니다!", "취소에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return false;
					}
					else
					{
						isError = true;
					}
				}
			}
            return isError;
        }

		#endregion

		#region 9. ETC

		#endregion

	}
}

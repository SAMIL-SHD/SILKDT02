using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using DevExpress.XtraScheduler;
using System.Drawing;

namespace DUTY1000
{
    public partial class duty5060 : SilkRoad.Form.Base.FormX
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
        public duty5060()
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
				if (ds.Tables["SEARCH_AP_YCHG_LIST"] != null)
					ds.Tables["SEARCH_AP_YCHG_LIST"].Clear();
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

        private void duty8030_Load(object sender, EventArgs e)
        {

        }
		
		private void duty8030_Shown(object sender, EventArgs e)
		{			
            SetCancel(0);
            SetCancel(1);
			
			df.GetSEARCH_AP_YCHG_LISTDatas(SilkRoad.Config.SRConfig.USID, ds);
			grd_ap.DataSource = ds.Tables["SEARCH_AP_YCHG_LIST"];
		}

        #endregion

        #region 2 Button	
		
		//승인내역 조회
		private void btn_ap_search_Click(object sender, EventArgs e)
		{
			df.GetSEARCH_AP_YCHG_LISTDatas(SilkRoad.Config.SRConfig.USID, ds);
			grd_ap.DataSource = ds.Tables["SEARCH_AP_YCHG_LIST"];
			if (ds.Tables["SEARCH_AP_YCHG_LIST"].Rows.Count == 0)
				MessageBox.Show("결재할 연차내역이 없습니다!", "결재", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
					for (int i = 0; i < ds.Tables["SEARCH_AP_YCHG_LIST"].Rows.Count; i++)
					{
						DataRow drow = ds.Tables["SEARCH_AP_YCHG_LIST"].Rows[i];
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
					df.GetSEARCH_AP_YCHG_LISTDatas(SilkRoad.Config.SRConfig.USID, ds);
					grd_ap.DataSource = ds.Tables["SEARCH_AP_YCHG_LIST"];
                    Cursor = Cursors.Default;
                }
            }
		}

        #endregion

        #region 3 EVENT
		
		//메뉴 활성화시
		private void duty8030_Activated(object sender, EventArgs e)
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

            if (mode == 1)  //연차,휴가승인
            {
				if (ds.Tables["SEARCH_AP_YCHG_LIST"] != null)
				{
					ds.Tables["SEARCH_AP_YCHG_LIST"].AcceptChanges();
					if (ds.Tables["SEARCH_AP_YCHG_LIST"].Select("CHK='1'").Length == 0)
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
			else if (mode == 1)  //승인
			{
				if (ds.Tables["SEARCH_AP_YC_LIST"] != null)
				{
					ds.Tables["SEARCH_AP_YC_LIST"].AcceptChanges();
					if (ds.Tables["SEARCH_AP_YC_LIST"].Select("CHK='1'").Length == 0)
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
			//        else if (mode == 3)  //승인취소
			//        {
			//if (ends_yn == "Y")
			//{
			//	MessageBox.Show("최종마감되어 승인취소 할 수 없습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//	return false;
			//}
			//if (ds.Tables["SEARCH_YC_LIST"] != null)
			//{
			//	if (ds.Tables["SEARCH_YC_LIST"].Select("C_CHK='1'").Length == 0)
			//	{
			//		MessageBox.Show("선택된 내역이 없습니다!", "취소에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//		return false;
			//	}
			//	else
			//	{
			//		isError = true;
			//	}
			//}
			//        }
            return isError;
        }

        #endregion

        #region 9. ETC
		
		#endregion

	}
}

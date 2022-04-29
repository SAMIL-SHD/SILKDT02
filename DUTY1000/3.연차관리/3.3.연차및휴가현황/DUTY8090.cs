using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using DevExpress.XtraScheduler;
using System.Drawing;

namespace DUTY1000
{
    public partial class duty8090 : SilkRoad.Form.Base.FormX
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
        public duty8090()
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
				sl_dept.Enabled = p_dpcd == "%" ? true : false;
				
				if (ds.Tables["DUTY_TRSJREQ"] != null)
					ds.Tables["DUTY_TRSJREQ"].Clear();
				if (ds.Tables["SEARCH_JREQ_LIST"] != null)
					ds.Tables["SEARCH_JREQ_LIST"].Clear();
				grd1.DataSource = null;
			}

			df.GetSEARCH_DEPTDatas(ds);
			sl_dept.Properties.DataSource = ds.Tables["SEARCH_DEPT"];
			df.Get2020_SEARCH_EMBSDatas(p_dpcd, ds);
        }
		
        //연차/휴가신청내역 조회
        private void baseInfoSearch()
        {
			//END_CHK();
			string dept = sl_dept.EditValue == null ? "%" : sl_dept.EditValue.ToString();
			df.GetSEARCH_8090_LISTDatas(clib.DateToText(dat_yymm.DateTime).Substring(0, 6), clib.DateToText(dat_yymm2.DateTime).Substring(0, 6), dept, cmb_type.SelectedIndex, ds);
			grd1.DataSource = ds.Tables["SEARCH_8090_LIST"];
		}

        #endregion

        #region 1 Form

        private void duty8090_Load(object sender, EventArgs e)
        {
			dat_yymm.DateTime = DateTime.Today;
			dat_yymm2.DateTime = DateTime.Today;
			sl_dept.EditValue = null;
        }
		
		private void duty8090_Shown(object sender, EventArgs e)
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
				//p_dpcd = SilkRoad.Config.SRConfig.US_DPCD == null ? null : SilkRoad.Config.SRConfig.US_DPCD.Trim();
    //            lb_power.Text = upyn == "1" ? "부서조회 권한" : "조회권한 없음";

				//sl_dept.EditValue = p_dpcd;
				//sl_dept.Enabled = false;
    //        }
			
            SetCancel(0);
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

        /// <summary>취소버튼</summary>
        private void btn_clear_Click(object sender, EventArgs e)
        {
            SetCancel(0);
        }		

        #endregion

        #region 3 EVENT
		
		//메뉴 활성화시
		private void duty8090_Activated(object sender, EventArgs e)
		{
			//END_CHK();
		}

        private void duty8090_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //취소
            {
                btn_clear.PerformClick();
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
		

		#endregion

	}
}

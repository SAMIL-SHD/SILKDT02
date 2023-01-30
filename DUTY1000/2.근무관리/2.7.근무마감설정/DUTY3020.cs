using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;

namespace DUTY1000
{
    public partial class duty3020 : SilkRoad.Form.Base.FormX
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

        public duty3020()
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
			if (ds.Tables["SEARCH_HOLIDAY"] != null)
				ds.Tables["SEARCH_HOLIDAY"].Clear();
            SetButtonEnable("10");
            grd_dept.DataSource = null;
            grd1.DataSource = null;

			grd_dept.Enabled = false;
			srPanel2.Enabled = false;
			dat_year.Enabled = true;

			//grd1.Enabled = false;

			lblName.Text = "_";
			lblyymm.Text = "_";
			dat_frdt.Text = "";
			dat_todt.Text = "";
        }

        #endregion

        #region 1 Form

        private void duty3020_Load(object sender, EventArgs e)
        {
            SetCancel();
			dat_year.DateTime = DateTime.Now;
            dat_year.Focus();
        }
		
        private void duty3020_Shown(object sender, EventArgs e)
        {
            df.GetMSTUSER_CHKDatas(ds);  //부서관리여부
            if (ds.Tables["MSTUSER_CHK"].Rows.Count > 0)
                admin_lv = clib.TextToInt(ds.Tables["MSTUSER_CHK"].Rows[0]["EMBSADGB"].ToString()); //권한레벨

			if (SilkRoad.Config.ACConfig.G_MSYN == "1" || SilkRoad.Config.SRConfig.USID == "SAMIL" || admin_lv > 1)
			{
				admin_lv = 3;
                p_dpcd = "%";
                lb_power.Text = "전체조회 권한";
			}
            else if (admin_lv == 1)
            {
                p_dpcd = SilkRoad.Config.SRConfig.US_DPCD == null ? null : SilkRoad.Config.SRConfig.US_DPCD.Trim();
                lb_power.Text = "부서조회 권한";
			}
            else
            {
                p_dpcd = SilkRoad.Config.SRConfig.US_DPCD == null ? null : SilkRoad.Config.SRConfig.US_DPCD.Trim();
				lb_power.Text = "조회권한 없음";
            }

            //if (ds.Tables["MSTUSER_CHK"].Rows.Count > 0)
            //{
            //    msyn = ds.Tables["MSTUSER_CHK"].Rows[0]["USERMSYN"].ToString(); //전체조회
            //    upyn = ds.Tables["MSTUSER_CHK"].Rows[0]["USERUPYN"].ToString(); //부서조회
            //}
            ////사용자부서연결
            //if (SilkRoad.Config.SRConfig.USID == "SAMIL" || msyn == "1")
            //{
            //    p_dpcd = "%";
            //    lb_power.Text = "전체조회 권한";
            //}
            //else
            //{
            //    p_dpcd = SilkRoad.Config.SRConfig.US_DPCD == null ? null : SilkRoad.Config.SRConfig.US_DPCD.Trim();
            //    lb_power.Text = upyn == "1" ? "부서조회 권한" : "조회권한 없음";
            //}
        }
        #endregion

        #region 2 Button
		
        //처리
		private void btn_search_Click(object sender, EventArgs e)
		{
			if (clib.DateToText(dat_year.DateTime) == "")
			{
                MessageBox.Show("조회년도가 입력되지 않았습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dat_year.Focus();
			}
			else
			{			
				SetButtonEnable("01");
				df.GetSET_DEPTDatas(p_dpcd, ds);
				grd_dept.DataSource = ds.Tables["SET_DEPT"];
				grd_dept.Enabled = true;
				grd1.DataSource = null;
				grd1.Enabled = true;
				srPanel2.Enabled = true;

				dat_year.Enabled = false;
			}
		}
		
		private void btn_put_Click(object sender, EventArgs e)
		{
			if (lblyymm.Text.ToString() == "_")
			{
				MessageBox.Show("마감년월이 조회되지 않았습니다!", "입력오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (clib.DateToText(dat_frdt.DateTime) == "")
			{
				MessageBox.Show("신청일자(Fr)가 입력되지 않았습니다!", "입력오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dat_frdt.Focus();
			}
			else if (clib.DateToText(dat_todt.DateTime) == "")
			{
				MessageBox.Show("신청일자(To)가 입력되지 않았습니다!", "입력오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dat_todt.Focus();
			}
			else
			{
				df.GetDUTY_MSTCLOSDatas(grdv_dept.GetFocusedDataRow()["CODE"].ToString(), lblyymm.Text.Replace("-", ""), ds);
				if (ds.Tables["DUTY_MSTCLOS"].Rows.Count > 0)
				{
					DataRow nrow = ds.Tables["DUTY_MSTCLOS"].Rows[0];
					nrow["POS_FRDT"] = clib.DateToText(dat_frdt.DateTime);
					nrow["POS_TODT"] = clib.DateToText(dat_todt.DateTime);
					nrow["CLOSE_YN"] = cmb_stat.SelectedIndex == 1 ? "Y" : "";
					nrow["UPDT"] = gd.GetNow();
					nrow["USID"] = SilkRoad.Config.SRConfig.USID;
					nrow["PSTY"] = "U";
				}
				else
				{
					DataRow nrow = ds.Tables["DUTY_MSTCLOS"].NewRow();
					nrow["DEPTCODE"] = grdv_dept.GetFocusedDataRow()["CODE"].ToString();
					nrow["PLANYYMM"] = lblyymm.Text.Replace("-", "");
					nrow["POS_FRDT"] = clib.DateToText(dat_frdt.DateTime);
					nrow["POS_TODT"] = clib.DateToText(dat_todt.DateTime);
					nrow["CLOSE_YN"] = cmb_stat.SelectedIndex == 1 ? "Y" : "";
					nrow["INDT"] = gd.GetNow();
					nrow["UPDT"] = "";
					nrow["USID"] = SilkRoad.Config.SRConfig.USID;
					nrow["PSTY"] = "A";
					ds.Tables["DUTY_MSTCLOS"].Rows.Add(nrow);
				}

				string[] tableNames = new string[] { "DUTY_MSTCLOS" };
				SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
				int outVal = cmd.setUpdate(ref ds, tableNames, null);
				
				if (outVal > 0)							
					MessageBox.Show("해당 내용이 저장되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);

				df.GetSEARCH_CLOSDatas(clib.DateToText(dat_year.DateTime).Substring(0, 4), grdv_dept.GetFocusedDataRow()["CODE"].ToString(), ds);
				grd1.DataSource = ds.Tables["SEARCH_CLOS"];
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

        private void duty3090_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //취소
            {
                btn_clear.PerformClick();
            }
        }

		private void grdv_part_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
		{			
			DataRow drow = grdv_dept.GetFocusedDataRow();
            if (drow == null)
                return;
			df.GetSEARCH_CLOSDatas(clib.DateToText(dat_year.DateTime).Substring(0, 4), drow["CODE"].ToString(), ds);
			grd1.DataSource = ds.Tables["SEARCH_CLOS"];

			lblName.Text = "_";
			lblyymm.Text = "_";
			dat_frdt.Text = "";
			dat_todt.Text = "";
		}
		
		private void grdv1_DoubleClick(object sender, EventArgs e)
		{
			DataRow drow = grdv1.GetFocusedDataRow();
            if (drow == null)
                return;

			lblName.Text = grdv_dept.GetFocusedDataRow()["NAME"].ToString();
            lblyymm.Text = drow["YYMM_NM"].ToString();
			if (drow["POS_FRDT"].ToString().Trim() != "")
				dat_frdt.DateTime = clib.TextToDate(drow["POS_FRDT"].ToString());
			else
				dat_frdt.Text = "";

			if (drow["POS_TODT"].ToString().Trim() != "")
				dat_todt.DateTime = clib.TextToDate(drow["POS_TODT"].ToString());
			else
				dat_todt.Text = "";
			cmb_stat.SelectedIndex = drow["CLOSE_YN"].ToString() == "Y" ? 1 : 0;
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

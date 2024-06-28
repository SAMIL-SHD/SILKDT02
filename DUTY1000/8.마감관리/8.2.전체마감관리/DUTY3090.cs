using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;

namespace DUTY1000
{
    public partial class duty3090 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();

        public duty3090()
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
			if (ds.Tables["SEARCH_ENDS"] != null)
				ds.Tables["SEARCH_ENDS"].Clear();
            SetButtonEnable("10");
            grd_log.DataSource = null;
            grd1.DataSource = null;

			//grd_log.Enabled = false;
			//srPanel2.Enabled = false;
			dat_year.Enabled = true;
			
			lblyymm.Text = "_";
        }

        #endregion

        #region 1 Form

        private void duty3090_Load(object sender, EventArgs e)
        {
            SetCancel();
			dat_year.DateTime = DateTime.Now;
            dat_year.Focus();
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
				df.GetSEARCH_ENDSDatas(clib.DateToText(dat_year.DateTime).Substring(0, 4), ds);
				grd1.DataSource = ds.Tables["SEARCH_ENDS"];
				//grd_log.Enabled = true;

				//srPanel2.Enabled = true;
				dat_year.Enabled = false;
			}
		}
		
		//입력
		private void btn_put_Click(object sender, EventArgs e)
		{
			if (lblyymm.Text.ToString() == "_")
			{
				MessageBox.Show("마감년월이 조회되지 않았습니다!", "입력오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				df.GetDUTY_MSTENDSDatas(lblyymm.Text.Replace("-", ""), ds);
				if (ds.Tables["DUTY_MSTENDS"].Rows.Count > 0)
				{
					DataRow nrow = ds.Tables["DUTY_MSTENDS"].Rows[0];
					nrow["CLOSE_YN"] = cmb_stat.SelectedIndex == 1 ? "Y" : "N";
					if (cmb_stat.SelectedIndex == 1)
					{
						nrow["END_DT"] = gd.GetNow();
						nrow["END_ID"] = SilkRoad.Config.SRConfig.USID;
					}
					else
					{
						nrow["CANC_DT"] = gd.GetNow();
						nrow["CANC_ID"] = SilkRoad.Config.SRConfig.USID;
					}
				}
				else
				{
					DataRow nrow = ds.Tables["DUTY_MSTENDS"].NewRow();
					nrow["END_YYMM"] = lblyymm.Text.Replace("-", "");
					nrow["CLOSE_YN"] = cmb_stat.SelectedIndex == 1 ? "Y" : "N";
					if (cmb_stat.SelectedIndex == 1)
					{
						nrow["END_DT"] = gd.GetNow();
						nrow["END_ID"] = SilkRoad.Config.SRConfig.USID;
						nrow["CANC_DT"] = "";
						nrow["CANC_ID"] = "";
					}
					else
					{
						nrow["END_DT"] = "";
						nrow["END_ID"] = "";
						nrow["CANC_DT"] = gd.GetNow();
						nrow["CANC_ID"] = SilkRoad.Config.SRConfig.USID;
					}
					ds.Tables["DUTY_MSTENDS"].Rows.Add(nrow);
				}
				
				df.GetDUTY_MSTENDS_LOGDatas(ds);
				DataRow nrow2 = ds.Tables["DUTY_MSTENDS_LOG"].NewRow();
				nrow2["END_YYMM"] = lblyymm.Text.Replace("-", "");
				nrow2["CLOSE_YN"] = cmb_stat.SelectedIndex == 1 ? "Y" : "N";
				nrow2["REG_DT"] = gd.GetNow();
				nrow2["REG_ID"] = SilkRoad.Config.SRConfig.USID;				
				ds.Tables["DUTY_MSTENDS_LOG"].Rows.Add(nrow2);

				string[] tableNames = new string[] { "DUTY_MSTENDS", "DUTY_MSTENDS_LOG" };
				SilkRoad.DbCmd_DT02.DbCmd_DT02 cmd = new SilkRoad.DbCmd_DT02.DbCmd_DT02();
				int outVal = cmd.setUpdate(ref ds, tableNames, null);
				
				if (outVal > 0)							
					MessageBox.Show("해당 내용이 저장되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);

				df.GetSEARCH_ENDSDatas(clib.DateToText(dat_year.DateTime).Substring(0, 4), ds);
				grd1.DataSource = ds.Tables["SEARCH_ENDS"];
				grd_log.DataSource = null;
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

		private void grdv1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
		{			
			DataRow drow = grdv1.GetFocusedDataRow();
            if (drow == null)
                return;
			df.GetSEARCH_ENDS_LOGDatas(drow["END_YYMM"].ToString(), ds);
			grd_log.DataSource = ds.Tables["SEARCH_ENDS_LOG"];			
		}
		
		private void grdv1_DoubleClick(object sender, EventArgs e)
		{
			DataRow drow = grdv1.GetFocusedDataRow();
            if (drow == null)
                return;
            lblyymm.Text = drow["YYMM_NM"].ToString();
			cmb_stat.SelectedIndex = drow["CLOSE_YN"].ToString() == "Y" ? 0 : 1;
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

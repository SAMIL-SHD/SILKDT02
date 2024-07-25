using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using DevExpress.XtraScheduler;
using System.Drawing;
using SilkRoad.DataProc;

namespace DUTY1000
{
    public partial class duty8030 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        static SetData sd = new SetData();
        static DataProcessing dp = new DataProcessing();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();
        private string dbname = SilkRoad.DAL.DataAccess.DBname + SilkRoad.Config.SRConfig.WorkPlaceNo;

        private string ends_yn = "";
        private string ends_yn2 = "";
		
		private int admin_lv = 0;
		
        private string use_frdt = "";
        private string use_todt = "";
        public duty8030()
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
				if (ds.Tables["DUTY_TRSHREQ"] != null)
					ds.Tables["DUTY_TRSHREQ"].Clear();
				if (ds.Tables["SEARCH_AP_YC_LIST"] != null)
					ds.Tables["SEARCH_AP_YC_LIST"].Clear();
				grd1.DataSource = null;
				if (ds.Tables["SEARCH_YC_LIST"] != null)
					ds.Tables["SEARCH_YC_LIST"].Clear();
				grd1.DataSource = null;
				if (ds.Tables["SEARCH_DEL_YC_LIST"] != null)
					ds.Tables["SEARCH_DEL_YC_LIST"].Clear();
				grd_del.DataSource = null;
				
				txt_sabn.Text = "";
				txt_name.Text = "";
				txt_bf_cnt.Text = "";
				txt_change.Text = "";
				txt_first.Text = "";
				txt_now_cnt.Text = "";
				txt_tcnt.Text = "";
				txt_use.Text = "";
				txt_rcnt.Text = "";
				
				if (ds.Tables["SEARCH_TRSHREQ"] != null)
					ds.Tables["SEARCH_TRSHREQ"].Clear();
				grd_yc.DataSource = null;
			}

			if (stat == 1)
			{
				dat_ycdt.Text = "";
				dat_ycdt2.Text = "";
                txt_remk1.Text = "";
                mm_remk2.Text = "";
                sl_gnmu.EditValue = null;
				sl_gnmu2.EditValue = null;
				
				sl_embs.Enabled = true;
				dat_ycdt.Enabled = false;
				dat_ycdt2.Enabled = false;
				sl_gnmu.Enabled = false;
				sl_gnmu2.Enabled = false;
				cmb_yc_type.Enabled = false;
                grd_sign.Enabled = false;
                txt_remk1.Enabled = false;
                mm_remk2.Enabled = false;

                sl_line.EditValue = null;
				sl_line.Enabled = false;
                btn_add.Enabled = false;

                DataTable _dt = new DataTable();
                _dt.Columns.Add("LINE_SABN");
                _dt.Columns.Add("LINE_SANM");
                _dt.Columns.Add("LINE_JIWK");
                dp.AddDatatable2Dataset("GRD_LINE", _dt, ref ds);
                grd_sign.DataSource = ds.Tables["GRD_LINE"];

                SetButtonEnable2("100");
			}
			
            use_frdt = "";
			use_todt = "";

			df.GetSEARCH_DEPT_POWERDatas(admin_lv, ds);
			sl_dept.Properties.DataSource = ds.Tables["SEARCH_DEPT_POWER"];
			df.GetSEARCH_EMBS_POWERDatas(admin_lv, ds);
			sl_embs.Properties.DataSource = ds.Tables["SEARCH_EMBS_POWER"];

			df.Get8030_SEARCH_GNMUDatas(ds);
			sl_gnmu.Properties.DataSource = ds.Tables["8030_SEARCH_GNMU"];
			sl_gnmu2.Properties.DataSource = ds.Tables["8030_SEARCH_GNMU"];
        }

        //연차신청내역 조회
        private void baseInfoSearch()
        {
			string dept = sl_dept.EditValue == null ? "%" : sl_dept.EditValue.ToString();
			df.GetSEARCH_YC_LISTDatas("A", admin_lv, clib.DateToText(dat_yymm.DateTime).Substring(0, 6), clib.DateToText(dat_yymm2.DateTime).Substring(0, 6), dept, ds);
			grd1.DataSource = ds.Tables["SEARCH_AP_YC_LIST"];
			df.GetSEARCH_YC_LISTDatas("C", admin_lv, clib.DateToText(dat_yymm.DateTime).Substring(0, 6), clib.DateToText(dat_yymm2.DateTime).Substring(0, 6), dept, ds);
			grd2.DataSource = ds.Tables["SEARCH_YC_LIST"];
			df.GetSEARCH_YC_LISTDatas("D", admin_lv, clib.DateToText(dat_yymm.DateTime).Substring(0, 6), clib.DateToText(dat_yymm2.DateTime).Substring(0, 6), dept, ds);
			grd_del.DataSource = ds.Tables["SEARCH_DEL_YC_LIST"];

            if (ds.Tables["SEARCH_AP_YC_LIST"].Rows.Count == 0)
                MessageBox.Show("조회된 연차신청내역이 없습니다!", "조회", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        #region 1 Form

        private void duty8030_Load(object sender, EventArgs e)
        {
			dat_yymm.DateTime = DateTime.Today;
			dat_yymm2.DateTime = DateTime.Today;
			sl_dept.EditValue = null;
			
			sl_embs.EditValue = null;
			sl_gnmu.EditValue = null;
			dat_ycdt.DateTime = DateTime.Today;
			lb_yc_remark.Text = "[연차사용기간 / 잔여연차]";
        }
		
		private void duty8030_Shown(object sender, EventArgs e)
		{
            df.GetMSTUSER_CHKDatas(ds);  //부서관리여부
            if (ds.Tables["MSTUSER_CHK"].Rows.Count > 0)
                admin_lv = clib.TextToInt(ds.Tables["MSTUSER_CHK"].Rows[0]["EMBSADGB"].ToString()); //권한레벨

			if (SilkRoad.Config.ACConfig.G_MSYN == "1" || SilkRoad.Config.SRConfig.USID == "SAMIL")
			{
				admin_lv = 3;
                lb_power.Text = "전체관리 권한";
			}
            else if (admin_lv == 1)
            {
                lb_power.Text = "부서조회 권한";
			}
            else
            {
                lb_power.Text = "조회권한 없음";	
            }
			
            SetCancel(0);
            SetCancel(1);
		}

        #endregion

        #region 2 Button
		
		//조회
		private void btn_search_Click(object sender, EventArgs e)
		{
			if (isNoError_um(1))
			{
				txt_sabn.Text = "";
				txt_name.Text = "";
				txt_bf_cnt.Text = "";
				txt_change.Text = "";
				txt_first.Text = "";
				txt_now_cnt.Text = "";
				txt_tcnt.Text = "";
				txt_use.Text = "";
				txt_rcnt.Text = "";
				
				if (ds.Tables["SEARCH_TRSHREQ"] != null)
					ds.Tables["SEARCH_TRSHREQ"].Clear();
				grd_yc.DataSource = null;

				baseInfoSearch();
			}
		}
				
		//연차신청 처리
		private void btn_proc_Click(object sender, EventArgs e)
		{			
			if (sl_embs.EditValue == null)
			{
				MessageBox.Show("사원이 선택되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
				sl_embs.Focus();
			}
			else
			{
				sl_embs.Enabled = false;

				dat_ycdt.Enabled = true;
				dat_ycdt2.Enabled = true;
				cmb_yc_type.Enabled = true;
				sl_gnmu.Enabled = true;
				sl_gnmu2.Enabled = true;
                grd_sign.Enabled = true;
                txt_remk1.Enabled = true;
                mm_remk2.Enabled = true;

                sl_line.EditValue = null;
				sl_line.Enabled = true;
                btn_add.Enabled = true;
                dat_ycdt.DateTime = DateTime.Today;
                dat_ycdt2.DateTime = DateTime.Today;

                GW_LINE();

                //부서 결재라인 검색
                string gw_tb_nm = "";
                df.GetDEL_GW_LINEDatas(sl_embs.EditValue.ToString(), ds); 
                if (ds.Tables["DEL_GW_LINE"].Rows.Count == 0)
                {
                    //개인별 결재라인 검색
                    df.GetDEL_GW_LINE_DEPTDatas(1, sl_embs.EditValue.ToString(), ds);
                    gw_tb_nm = "DEL_GW_LINE_DEPT";
                }
                else
                    gw_tb_nm = "DEL_GW_LINE";

                for (int i = 0; i < ds.Tables[gw_tb_nm].Rows.Count; i++)
                {
                    DataRow nrow = ds.Tables["GRD_LINE"].NewRow();
                    nrow["LINE_SABN"] = ds.Tables[gw_tb_nm].Rows[i]["LINE_SABN"].ToString();
                    nrow["LINE_SANM"] = ds.Tables[gw_tb_nm].Rows[i]["LINE_SANM"].ToString();
                    nrow["LINE_JIWK"] = ds.Tables[gw_tb_nm].Rows[i]["LINE_JIWK"].ToString();
                    ds.Tables["GRD_LINE"].Rows.Add(nrow);
                }

                SetButtonEnable2("011");
				dat_ycdt.Focus();
			}
		}
		private void GW_LINE()
		{
            df.GetGW_LINEDatas(ds);
            sl_line.Properties.DataSource = ds.Tables["GW_LINE"];          
		}
		
		//연차신청 취소
		private void btn_canc_Click(object sender, EventArgs e)
		{
			SetCancel(1);
		}

		//저장
		private void btn_save_Click(object sender, EventArgs e)
		{
			if (isNoError_um(2))
            {
                Cursor = Cursors.WaitCursor;
                int outVal = 0;
                try
                {
					string sldt = clib.DateToText(dat_ycdt.DateTime);
					string sldt2 = clib.DateToText(dat_ycdt2.DateTime);
                    string yc_year = sldt.Substring(0, 4); //df.GetYC_YEAR_CHKDatas(sl_embs.EditValue.ToString(), sldt, ds);
                    string to_gnmu = sl_gnmu2.EditValue == null ? sl_gnmu.EditValue.ToString() : sl_gnmu2.EditValue.ToString();
					//신청일자fr-to 에 대한 오류체크
					df.GetYC_DAYS_ECHKDatas(sl_embs.EditValue.ToString(), yc_year, sldt, sldt2, sl_gnmu.EditValue.ToString(), to_gnmu, ds);
					if (ds.Tables["YC_DAYS_ECHK"].Rows.Count > 0)
					{
						DataRow crow = ds.Tables["YC_DAYS_ECHK"].Rows[0];
						if (clib.TextToInt(crow["ECHK"].ToString()) < 0)
						{
							MessageBox.Show(crow["PCEROR"].ToString(), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
							outVal = -1;
							return;
						}
					}

					df.GetDUTY_TRSHREQDatas(ds);
                    DataRow hrow = ds.Tables["DUTY_TRSHREQ"].NewRow();
                    hrow["SEQNO"] = df.GetHREQ_SEQNODatas();
                    hrow["SABN"] = sl_embs.EditValue.ToString();
                    hrow["GUBN"] = cmb_yc_type.SelectedIndex == 1 ? "D" : "C";
                    hrow["REQ_YEAR"] = yc_year;
                    hrow["REQ_DATE"] = sldt;
                    hrow["REQ_DATE2"] = sldt2;
                    hrow["REQ_TYPE"] = sl_gnmu.EditValue.ToString();
                    hrow["REQ_TYPE2"] = to_gnmu;

                    hrow["YC_DAYS"] = df.GetYC_DAYS_CALC_SABNDatas(sl_embs.EditValue.ToString(), sldt, sldt2, sl_gnmu.EditValue.ToString(), to_gnmu, ds);
                    if (cmb_yc_type.SelectedIndex == 1)
                        hrow["YC_DAYS"] = -clib.TextToDecimal(hrow["YC_DAYS"].ToString());
                    hrow["AP_TAG"] = "1";

                    hrow["AP_DT"] = "";
                    hrow["AP_USID"] = "";
                    hrow["RT_DT"] = "";
                    hrow["RT_USID"] = "";

                    hrow["LINE_CNT"] = 1;
                    hrow["LINE_MAX"] = grdv_sign.RowCount + 1;
                    hrow["LINE_REMK"] = "";

                    df.GetDUTY_TRSHREQ_DTDatas(ds);
                    DataRow nrow = ds.Tables["DUTY_TRSHREQ_DT"].NewRow();
                    nrow["SEQNO"] = hrow["SEQNO"];
                    nrow["SABN"] = sl_embs.EditValue.ToString();
                    nrow["LINE_SQ"] = 1;
                    nrow["LINE_SABN"] = sl_embs.EditValue.ToString();
                    nrow["LINE_SANM"] = sl_embs.Text.ToString();
                    nrow["LINE_JIWK"] = "담당";
                    nrow["LINE_AP_DT"] = gd.GetNow();
                    ds.Tables["DUTY_TRSHREQ_DT"].Rows.Add(nrow);

                    for (int i = 0; i < grdv_sign.RowCount; i++)
                    {
                        if (grdv_sign.GetVisibleRowHandle(i) > -1 && grdv_sign.GetDataRow(grdv_sign.GetVisibleRowHandle(i))["LINE_JIWK"].ToString() == "")
                        {
                            MessageBox.Show((i + 1).ToString() + "열의 담당직함이 비어 있습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        DataRow crow = grdv_sign.GetDataRow(grdv_sign.GetVisibleRowHandle(i));
                        if (i == 0)
                            hrow["LINE_REMK"] = sl_embs.Text.ToString() + "->" + crow["LINE_SANM"].ToString();
                        else                        
                            hrow["LINE_REMK"] = hrow["LINE_REMK"].ToString() + "->" + crow["LINE_SANM"].ToString();

                        nrow = ds.Tables["DUTY_TRSHREQ_DT"].NewRow();
                        nrow["SEQNO"] = hrow["SEQNO"];
                        nrow["SABN"] = sl_embs.EditValue.ToString();
                        nrow["LINE_SQ"] = i + 2;
                        nrow["LINE_SABN"] = crow["LINE_SABN"].ToString();
                        nrow["LINE_SANM"] = crow["LINE_SANM"].ToString();
                        nrow["LINE_JIWK"] = crow["LINE_JIWK"].ToString();
                        nrow["LINE_AP_DT"] = "";
                        ds.Tables["DUTY_TRSHREQ_DT"].Rows.Add(nrow);
                    }

                    hrow["REMARK1"] = txt_remk1.Text.ToString();
                    hrow["REMARK2"] = mm_remk2.Text.ToString();

                    hrow["INDT"] = gd.GetNow();
                    hrow["UPDT"] = "";
                    hrow["USID"] = SilkRoad.Config.SRConfig.USID;
                    hrow["PSTY"] = "A";
                    ds.Tables["DUTY_TRSHREQ"].Rows.Add(hrow);


                    string[] tableNames = new string[] { "DUTY_TRSHREQ", "DUTY_TRSHREQ_DT" }; //, "DEL_GW_LINE", "DUTY_GW_LINE" };
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
                        SetCancel(1);
                        baseInfoSearch();
                    }
                    Cursor = Cursors.Default;
                }
            }
		}

        /// <summary>취소버튼</summary>
        private void btn_clear_Click(object sender, EventArgs e)
        {
            SetCancel(0);
        }
        //결재라인추가
        private void btn_add_Click(object sender, EventArgs e)
        {
            if (sl_line.EditValue == null)
            {
                MessageBox.Show("검색된 결재라인이 없습니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sl_line.Focus();
            }
            else if (ds.Tables["GRD_LINE"].Select("LINE_SABN = '" + sl_line.EditValue.ToString() + "'").Length > 0)
            {
                MessageBox.Show("이미 추가되어 있습니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sl_line.Focus();
            }
            else
            {
                DataRow nrow = ds.Tables["GRD_LINE"].NewRow();
                nrow["LINE_SABN"] = sl_line.EditValue.ToString();
                nrow["LINE_SANM"] = sl_line.Text.ToString();
                nrow["LINE_JIWK"] = ds.Tables["GW_LINE"].Select("CODE ='" + sl_line.EditValue.ToString() + "'")[0]["POSI_NM"].ToString();

                ds.Tables["GRD_LINE"].Rows.Add(nrow);
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            grdv_sign.DeleteRow(grdv_sign.FocusedRowHandle);
        }

        #endregion

        #region 3 EVENT

        //메뉴 활성화시
        private void duty8030_Activated(object sender, EventArgs e)
		{

		}

        private void duty8030_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //취소
            {
                btn_clear.PerformClick();
            }
        }

        //그리드 클릭시 연차내역 조회
        private void grdv1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            DataRow drow2 = grdv1.GetFocusedDataRow();
            if (drow2 == null)
                return;

            if (e.Button == MouseButtons.Left)
            {
                dat_year.DateTime = clib.TextToDate(grdv1.GetFocusedRowCellValue("REQ_YEAR").ToString() + "0101");
                txt_sabn.Text = grdv1.GetFocusedRowCellValue("SABN").ToString();
                df.GetDUTY_TRSDYYCDatas(grdv1.GetFocusedRowCellValue("REQ_YEAR").ToString(), grdv1.GetFocusedRowCellValue("SABN").ToString(), ds);

                if (ds.Tables["DUTY_TRSDYYC"].Rows.Count > 0)
                {
                    DataRow drow = ds.Tables["DUTY_TRSDYYC"].Rows[0];
                    txt_name.Text = drow["SAWON_NM"].ToString();
                    cmb_type.SelectedIndex = clib.TextToInt(drow["YC_TYPE"].ToString());
                    dat_indt.DateTime = clib.TextToDate(drow["IN_DATE"].ToString());
                    //txt_bf_cnt.Text = drow["YC_BASE"].ToString();
                    //txt_first.Text = drow["YC_FIRST"].ToString();
                    //txt_now_cnt.Text = drow["YC_ADD"].ToString();
                    txt_tcnt.Text = drow["YC_SUM"].ToString();

                    txt_change.Text = drow["YC_CHANGE"].ToString();
                    txt_use.Text = "";
                    txt_rcnt.Text = drow["YC_TOTAL"].ToString();

                    df.GetSEARCH_TRSHREQDatas(drow["SAWON_NO"].ToString(), drow["YC_YEAR"].ToString(), ds);
                    grd_yc.DataSource = ds.Tables["SEARCH_TRSHREQ"];

                    if (ds.Tables["SEARCH_TRSHREQ"].Rows.Count > 0)
                        txt_use.Text = ds.Tables["SEARCH_TRSHREQ"].Compute("SUM(YC_DAYS)", null).ToString();

                    txt_rcnt.Text = (clib.TextToDecimal(txt_tcnt.Text.ToString()) + clib.TextToDecimal(txt_change.Text.ToString()) - clib.TextToDecimal(txt_use.Text.ToString())).ToString();
                    //df.GetSEARCH_DUTY_MSTYCCJDatas(drow["SAWON_NO"].ToString(), drow["YC_YEAR"].ToString(), "", "", ds);
                    //grd.DataSource = ds.Tables["SEARCH_DUTY_MSTYCCJ"];

                    //SetButtonEnable("111");
                }
            }
            else if (e.Button == MouseButtons.Right && drow2["AP_TAG"].ToString() == "1")  //신청일때만 삭제가능하게 메뉴보임
            {
                int X = Cursor.Position.X;
                int Y = Cursor.Position.Y;
                contextMenuStrip1.Show(X, Y);
            }
        }
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            DataRow drow = grdv1.GetFocusedDataRow();
            decimal SeqNo = clib.TextToDecimal(drow["SEQNO"].ToString());
            
            if (e.ClickedItem.ToString() == "연차삭제")
            {
                string qry = "UPDATE DUTY_TRSHREQ SET UPDT='" + gd.GetNow() + "', USID = '" + SilkRoad.Config.SRConfig.USID + "', PSTY='D' WHERE SEQNO = " + SeqNo;
                sd.SetExecuteNonQuery(dbname, qry);

                baseInfoSearch();
            }            
        }

        private void grdv2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
		{
			DataRow drow2 = grdv2.GetFocusedDataRow();
			if (drow2 == null)
				return;

			dat_year.DateTime = clib.TextToDate(grdv2.GetFocusedRowCellValue("REQ_YEAR").ToString() + "0101");
			txt_sabn.Text = grdv2.GetFocusedRowCellValue("SABN").ToString();
			df.GetDUTY_TRSDYYCDatas(grdv2.GetFocusedRowCellValue("REQ_YEAR").ToString(), grdv2.GetFocusedRowCellValue("SABN").ToString(), ds);

			if (ds.Tables["DUTY_TRSDYYC"].Rows.Count > 0)
			{
				DataRow drow = ds.Tables["DUTY_TRSDYYC"].Rows[0];
				txt_name.Text = drow["SAWON_NM"].ToString();
				cmb_type.SelectedIndex = clib.TextToInt(drow["YC_TYPE"].ToString());
				dat_indt.DateTime = clib.TextToDate(drow["IN_DATE"].ToString());
				txt_bf_cnt.Text = drow["YC_BASE"].ToString();
				txt_first.Text = drow["YC_FIRST"].ToString();
				txt_now_cnt.Text = drow["YC_ADD"].ToString();
				txt_tcnt.Text = drow["YC_SUM"].ToString();

				txt_change.Text = drow["YC_CHANGE"].ToString();
				txt_use.Text = "";
				txt_rcnt.Text = drow["YC_TOTAL"].ToString();

				df.GetSEARCH_TRSHREQDatas(drow["SAWON_NO"].ToString(), drow["YC_YEAR"].ToString(), ds);
				grd_yc.DataSource = ds.Tables["SEARCH_TRSHREQ"];

				if (ds.Tables["SEARCH_TRSHREQ"].Rows.Count > 0)
					txt_use.Text = ds.Tables["SEARCH_TRSHREQ"].Compute("SUM(YC_DAYS)", null).ToString();

				txt_rcnt.Text = (clib.TextToDecimal(txt_tcnt.Text.ToString()) + clib.TextToDecimal(txt_change.Text.ToString()) - clib.TextToDecimal(txt_use.Text.ToString())).ToString();
			}
		}
		private void grdv_del_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
		{
			DataRow drow2 = grdv_del.GetFocusedDataRow();
            if (drow2 == null)
                return;

			dat_year.DateTime = clib.TextToDate(grdv_del.GetFocusedRowCellValue("REQ_YEAR").ToString()+"0101");
			txt_sabn.Text = grdv_del.GetFocusedRowCellValue("SABN").ToString();

			df.GetDUTY_TRSDYYCDatas(grdv_del.GetFocusedRowCellValue("REQ_YEAR").ToString(), grdv_del.GetFocusedRowCellValue("SABN").ToString(), ds);
			if (ds.Tables["DUTY_TRSDYYC"].Rows.Count > 0)
			{
				DataRow drow = ds.Tables["DUTY_TRSDYYC"].Rows[0];
				txt_name.Text = drow["SAWON_NM"].ToString();
				cmb_type.SelectedIndex = clib.TextToInt(drow["YC_TYPE"].ToString());
				dat_indt.DateTime = clib.TextToDate(drow["IN_DATE"].ToString());
				txt_bf_cnt.Text = drow["YC_BASE"].ToString();
				txt_change.Text = drow["YC_CHANGE"].ToString();
				txt_first.Text = drow["YC_FIRST"].ToString();
				txt_now_cnt.Text = drow["YC_ADD"].ToString();
				txt_tcnt.Text = drow["YC_TOTAL"].ToString();
				txt_use.Text = "";
				txt_rcnt.Text = drow["YC_TOTAL"].ToString();

				df.GetSEARCH_TRSHREQDatas(drow["SAWON_NO"].ToString(), drow["YC_YEAR"].ToString(), ds);
				grd_yc.DataSource = ds.Tables["SEARCH_TRSHREQ"];

				if (ds.Tables["SEARCH_TRSHREQ"].Rows.Count > 0)
					txt_use.Text = ds.Tables["SEARCH_TRSHREQ"].Compute("SUM(YC_DAYS)", null).ToString();
				
				txt_rcnt.Text = (clib.TextToDecimal(txt_tcnt.Text.ToString()) + clib.TextToDecimal(txt_change.Text.ToString()) - clib.TextToDecimal(txt_use.Text.ToString())).ToString();
			}
		}
				
		//연차사용일자 선택시
		private void dat_ycdt_EditValueChanged(object sender, EventArgs e)
		{
			if (clib.isDate(clib.DateToText(dat_ycdt.DateTime)) && sl_embs.EditValue != null)
				YC_REMAIN();
			else
				lb_yc_remark.Text = "[연차사용기간 / 잔여연차]";
			YC_GNMU_CHK();

			if (clib.DateToText(dat_ycdt2.DateTime) == "")
				dat_ycdt2.DateTime = dat_ycdt.DateTime;
		}
		private void dat_ycdt2_EditValueChanged(object sender, EventArgs e)
		{
			YC_GNMU_CHK();
		}

		private void YC_GNMU_CHK()
		{
			if (clib.DateToText(dat_ycdt.DateTime) != "" && clib.DateToText(dat_ycdt2.DateTime) != "")
			{
				if (clib.DateToText(dat_ycdt.DateTime) == clib.DateToText(dat_ycdt2.DateTime))
				{
					sl_gnmu2.Enabled = false;
					sl_gnmu2.EditValue = null;
				}
				else
				{
					sl_gnmu2.Enabled = true;
				}
			}
			else
			{
				sl_gnmu2.Enabled = true;
			}
		}
		private void YC_REMAIN()
		{
            string yc_year = clib.DateToText(dat_ycdt.DateTime).Substring(0, 4); // df.GetYC_YEAR_CHKDatas(sl_embs.EditValue.ToString(), clib.DateToText(dat_ycdt.DateTime), ds);
			df.GetSEARCH_YC_YEARDatas(sl_embs.EditValue.ToString(), yc_year, ds);
			if (ds.Tables["SEARCH_YC_YEAR"].Rows.Count == 0)
			{
				//[연차사용기간 : 2021.01.01 ~ 2021.12.31 / 잔여연차 : 15 일]
				//USP_DUTY8010_BASE '%', '199802015', '20211202', 'SA'
				df.GetDUTY_YC_BASEDatas(sl_embs.EditValue.ToString(), clib.DateToText(dat_ycdt.DateTime), ds);
				df.GetSEARCH_YC_YEARDatas(sl_embs.EditValue.ToString(), yc_year, ds);				
			}

			if (ds.Tables["SEARCH_YC_YEAR"].Rows.Count > 0)
			{
				DataRow drow = ds.Tables["SEARCH_YC_YEAR"].Rows[0];
				use_frdt = drow["USE_FRDT"].ToString();
				use_todt = drow["USE_TODT"].ToString();
				string frto_dt = drow["USE_FRDT"].ToString().Substring(0, 4) + "." + drow["USE_FRDT"].ToString().Substring(4, 2) + "." + drow["USE_FRDT"].ToString().Substring(6, 2) + " ~ " + drow["USE_TODT"].ToString().Substring(0, 4) + "." + drow["USE_TODT"].ToString().Substring(4, 2) + "." + drow["USE_TODT"].ToString().Substring(6, 2);
				df.GetSUM_YC_USEDatas(sl_embs.EditValue.ToString(), yc_year, ds);
				decimal yc_remain = clib.TextToDecimal(drow["YC_TOTAL"].ToString()) - clib.TextToDecimal(ds.Tables["SUM_YC_USE"].Rows[0]["YC_DAY"].ToString());
				lb_yc_remark.Text = "[연차사용기간 : " + frto_dt + " / 잔여연차 : " + yc_remain.ToString() + " 일]";

				string yymm = clib.DateToText(dat_ycdt.DateTime).Substring(0, 4) + "." + clib.DateToText(dat_ycdt.DateTime).Substring(4, 2);
				df.Get2020_SEARCH_ENDSDatas(clib.DateToText(dat_ycdt.DateTime).Substring(0, 6), ds);
				if (ds.Tables["2020_SEARCH_ENDS"].Rows.Count > 0) //마감월이 저장되어 있으면
				{
					DataRow irow = ds.Tables["2020_SEARCH_ENDS"].Rows[0];
					ends_yn2 = irow["CLOSE_YN"].ToString();
					lb_ends.Text = irow["CLOSE_YN"].ToString() == "Y" ? "[" + yymm + " 최종마감 완료]" : irow["CLOSE_YN"].ToString() == "N" ? "[" + yymm + " 최종마감 취소]" : "[ ]";
				}
				else
				{
					ends_yn2 = "";
					lb_ends.Text = "[" + yymm + " 최종마감 작업전]";
				}
			}
		}
				
		private void grdv1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
		{			
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)            
                e.Info.DisplayText = (e.RowHandle + 1).ToString();     
		}	
		private void grdv2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
		{			
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)            
                e.Info.DisplayText = (e.RowHandle + 1).ToString();     
		}
		private void grdv_del_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
		{			
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)            
                e.Info.DisplayText = (e.RowHandle + 1).ToString();     
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
                    MessageBox.Show("조회년월(종료)를 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dat_yymm2.Focus();
                    return false;
                }
                else
                {
                    isError = true;
                }
            }
            else if (mode == 2)  //등록
            {
				if (clib.DateToText(dat_ycdt.DateTime) == "")
				{
					MessageBox.Show("신청일자(시작)가 입력되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					dat_ycdt.Focus();
					return false;
				}
				else if (clib.DateToText(dat_ycdt2.DateTime) == "")
				{
					MessageBox.Show("신청일자(종료)가 입력되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					dat_ycdt2.Focus();
					return false;
				}
				else if (dat_ycdt2.DateTime > clib.TextToDate(use_todt))
				{
					MessageBox.Show("신청일자(종료)가 현재 연차 사용기간 이후의 일자입니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					dat_ycdt2.Focus();
					return false;
				}
				else if (sl_gnmu.EditValue == null)
				{
					MessageBox.Show("연차유형(시작)이 선택되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					sl_gnmu.Focus();
					return false;
				}
				else if (clib.DateToText(dat_ycdt.DateTime) != clib.DateToText(dat_ycdt2.DateTime) && sl_gnmu2.EditValue == null)
				{
					MessageBox.Show("연차유형(종료)이 선택되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					sl_gnmu2.Focus();
					return false;
				}
				else if (clib.DateToText(dat_ycdt.DateTime) != clib.DateToText(dat_ycdt2.DateTime) && sl_gnmu.EditValue.ToString() == "33")
				{
					MessageBox.Show("연차유형(시작)이 오전반차로 선택할 수 없습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					sl_gnmu.Focus();
					return false;
				}
				else if (clib.DateToText(dat_ycdt.DateTime) != clib.DateToText(dat_ycdt2.DateTime) && sl_gnmu2.EditValue.ToString() == "34")
				{
					MessageBox.Show("연차유형(종료)이 오후반차로 선택할 수 없습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					sl_gnmu2.Focus();
					return false;
				}
				else if (grdv_sign.RowCount == 0)
				{
					MessageBox.Show("결재라인이 입력되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					sl_line.Focus();
					return false;
				}
				else
				{
					isError = true;
				}
            }
            else if (mode == 3)  //삭제
            {
				//if (ends_yn2 == "Y")
				//{
				//	MessageBox.Show("최종마감되어 삭제할 수 없습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
				//	return false;
				//}
				if (clib.DateToText(dat_ycdt.DateTime) == "")
				{
					MessageBox.Show("신청일자가 입력되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					dat_ycdt.Focus();
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
		
        private void SetButtonEnable2(string arr)
        {
			btn_proc.Enabled = arr.Substring(0, 1) == "1" ? true : false;
			btn_save.Enabled = arr.Substring(1, 1) == "1" ? true : false;
			btn_canc.Enabled = arr.Substring(2, 1) == "1" ? true : false;
		}

        #endregion

    }
}

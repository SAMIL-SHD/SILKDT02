using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using DevExpress.XtraScheduler;
using System.Drawing;
using SilkRoad.DataProc;

namespace DUTY1000
{
    public partial class duty8050 : SilkRoad.Form.Base.FormX
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
        private string p_dpcd = "";
        public duty8050()
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
				grd_hg.DataSource = null;
				if (ds.Tables["SEARCH_JREQ_LIST"] != null)
					ds.Tables["SEARCH_JREQ_LIST"].Clear();
				grd_del.DataSource = null;
			}

			if (stat == 1)
			{
				dat_ycdt.Text = "";
				dat_ycdt2.Text = "";
                txt_remk1.Text = "";
                mm_remk2.Text = "";
                sl_gnmu.EditValue = null;
				
				sl_embs.Enabled = true;
				dat_ycdt.Enabled = false;
				dat_ycdt2.Enabled = false;
				cmb_yc_type.Enabled = false;
				sl_gnmu.Enabled = false;
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

			df.GetSEARCH_DEPTDatas(ds);
			sl_dept.Properties.DataSource = ds.Tables["SEARCH_DEPT"];
			df.Get8030_SEARCH_EMBSDatas(p_dpcd, ds);
			sl_embs.Properties.DataSource = ds.Tables["8030_SEARCH_EMBS"];
			df.Get8050_SEARCH_GNMUDatas(ds);
			sl_gnmu.Properties.DataSource = ds.Tables["8050_SEARCH_GNMU"];
        }
		
        //휴가신청내역 조회
        private void baseInfoSearch()
        {
			//END_CHK();
			string dept = sl_dept.EditValue == null ? "%" : sl_dept.EditValue.ToString();
			df.GetSEARCH_JREQ_LISTDatas("C", clib.DateToText(dat_yymm.DateTime).Substring(0, 6), clib.DateToText(dat_yymm2.DateTime).Substring(0, 6), dept, ds);
			grd_hg.DataSource = ds.Tables["SEARCH_JREQ_LIST"];
			df.GetSEARCH_JREQ_LISTDatas("D", clib.DateToText(dat_yymm.DateTime).Substring(0, 6), clib.DateToText(dat_yymm2.DateTime).Substring(0, 6), dept, ds);
			grd_del.DataSource = ds.Tables["SEARCH_DEL_JREQ_LIST"];
		}

        #endregion

        #region 1 Form

        private void duty8050_Load(object sender, EventArgs e)
        {
			dat_yymm.DateTime = DateTime.Today;
			dat_yymm2.DateTime = DateTime.Today;
			sl_dept.EditValue = null;
			
			sl_embs.EditValue = null;
			sl_gnmu.EditValue = null;
            dat_ycdt.DateTime = DateTime.Today;
        }
		
		private void duty8050_Shown(object sender, EventArgs e)
		{
            df.GetMSTUSER_CHKDatas(ds);  //부서관리여부
            if (ds.Tables["MSTUSER_CHK"].Rows.Count > 0)
                admin_lv = clib.TextToInt(ds.Tables["MSTUSER_CHK"].Rows[0]["EMBSADGB"].ToString()); //권한레벨

			if (SilkRoad.Config.ACConfig.G_MSYN == "1" || SilkRoad.Config.SRConfig.USID == "SAMIL")
			{
				admin_lv = 3;
                p_dpcd = "%";
                lb_power.Text = "전체관리 권한";
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
				baseInfoSearch();
			}
		}

		//휴가신청 처리
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
                grd_sign.Enabled = true;
                txt_remk1.Enabled = true;
                mm_remk2.Enabled = true;

                sl_line.EditValue = null;
                sl_line.Enabled = true;
                btn_add.Enabled = true;

                GW_LINE();

                df.GetDEL_GW_LINEDatas(sl_embs.EditValue.ToString(), ds);
                for (int i = 0; i < ds.Tables["DEL_GW_LINE"].Rows.Count; i++)
                {
                    DataRow nrow = ds.Tables["GRD_LINE"].NewRow();
                    nrow["LINE_SABN"] = ds.Tables["DEL_GW_LINE"].Rows[i]["LINE_SABN"].ToString();
                    nrow["LINE_SANM"] = ds.Tables["DEL_GW_LINE"].Rows[i]["LINE_SANM"].ToString();
                    nrow["LINE_JIWK"] = ds.Tables["DEL_GW_LINE"].Rows[i]["LINE_JIWK"].ToString();
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

        //휴가신청 취소
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
					df.GetDUTY_TRSJREQDatas(ds);
                    DataRow hrow = ds.Tables["DUTY_TRSJREQ"].NewRow();
                    hrow["SEQNO"] = df.GetJREQ_SEQNODatas();
                    hrow["SABN"] = sl_embs.EditValue.ToString();
                    hrow["GUBN"] = cmb_yc_type.SelectedIndex == 1 ? "D" : "C";
                    hrow["REQ_DATE"] = clib.DateToText(dat_ycdt.DateTime);
                    hrow["REQ_DATE2"] = clib.DateToText(dat_ycdt2.DateTime);
                    hrow["REQ_TYPE"] = sl_gnmu.EditValue.ToString();

                    hrow["HOLI_DAYS"] = df.GetHOLI_DAYS_CALCDatas(clib.DateToText(dat_ycdt.DateTime), clib.DateToText(dat_ycdt2.DateTime), sl_gnmu.EditValue.ToString(), ds);
                    if (cmb_yc_type.SelectedIndex == 1)
                        hrow["HOLI_DAYS"] = -clib.TextToDecimal(hrow["YC_DAYS"].ToString());
                    hrow["AP_TAG"] = "1";

                    hrow["AP_DT"] = "";
                    hrow["AP_USID"] = "";
                    hrow["RT_DT"] = "";
                    hrow["RT_USID"] = "";

                    hrow["LINE_CNT"] = 1;
                    hrow["LINE_MAX"] = grdv_sign.RowCount + 1;
                    hrow["LINE_REMK"] = "";

                    df.GetDUTY_TRSJREQ_DTDatas(ds);
                    DataRow nrow = ds.Tables["DUTY_TRSJREQ_DT"].NewRow();
                    nrow["SEQNO"] = hrow["SEQNO"];
                    nrow["SABN"] = sl_embs.EditValue.ToString();
                    nrow["LINE_SQ"] = 1;
                    nrow["LINE_SABN"] = sl_embs.EditValue.ToString();
                    nrow["LINE_SANM"] = sl_embs.Text.ToString();
                    nrow["LINE_JIWK"] = "담당";
                    nrow["LINE_AP_DT"] = gd.GetNow();
                    ds.Tables["DUTY_TRSJREQ_DT"].Rows.Add(nrow);

                    df.GetDUTY_GW_LINEDatas(ds);
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

                        nrow = ds.Tables["DUTY_TRSJREQ_DT"].NewRow();
                        nrow["SEQNO"] = hrow["SEQNO"];
                        nrow["SABN"] = sl_embs.EditValue.ToString();
                        nrow["LINE_SQ"] = i + 2;
                        nrow["LINE_SABN"] = crow["LINE_SABN"].ToString();
                        nrow["LINE_SANM"] = crow["LINE_SANM"].ToString();
                        nrow["LINE_JIWK"] = crow["LINE_JIWK"].ToString();
                        nrow["LINE_AP_DT"] = "";
                        ds.Tables["DUTY_TRSJREQ_DT"].Rows.Add(nrow);

                        DataRow nrow2 = ds.Tables["DUTY_GW_LINE"].NewRow();
                        nrow2["SABN"] = sl_embs.EditValue.ToString();
                        nrow2["LINE_SQ"] = i + 1;
                        nrow2["LINE_SABN"] = crow["LINE_SABN"].ToString();
                        nrow2["LINE_SANM"] = crow["LINE_SANM"].ToString();
                        nrow2["LINE_JIWK"] = crow["LINE_JIWK"].ToString();
                        ds.Tables["DUTY_GW_LINE"].Rows.Add(nrow2);
                    }

                    hrow["REMARK1"] = txt_remk1.Text.ToString();
                    hrow["REMARK2"] = mm_remk2.Text.ToString();

                    hrow["INDT"] = gd.GetNow();
                    hrow["UPDT"] = "";
                    hrow["USID"] = SilkRoad.Config.SRConfig.USID;
                    hrow["PSTY"] = "A";
                    ds.Tables["DUTY_TRSJREQ"].Rows.Add(hrow);

                    df.GetDEL_GW_LINEDatas(sl_embs.EditValue.ToString(), ds);
                    for (int i = 0; i < ds.Tables["DEL_GW_LINE"].Rows.Count; i++)
                    {
                        ds.Tables["DEL_GW_LINE"].Rows[i].Delete();
                    }

                    string[] tableNames = new string[] { "DUTY_TRSJREQ", "DUTY_TRSJREQ_DT", "DEL_GW_LINE", "DUTY_GW_LINE" };
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
        private void duty8050_Activated(object sender, EventArgs e)
		{
			//END_CHK();
		}

        private void duty8050_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //취소
            {
                btn_clear.PerformClick();
            }
        }
        //우클릭시 삭제메뉴
        private void grdv_hg_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            DataRow drow2 = grdv_hg.GetFocusedDataRow();
            if (drow2 == null)
                return;

            if (e.Button == MouseButtons.Right && drow2["AP_TAG"].ToString() == "1")  //신청일때만 삭제가능하게 메뉴보임
            {
                int X = Cursor.Position.X;
                int Y = Cursor.Position.Y;
                contextMenuStrip1.Show(X, Y);
            }
        }
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            DataRow drow = grdv_hg.GetFocusedDataRow();
            decimal SeqNo = clib.TextToDecimal(drow["SEQNO"].ToString());

            if (e.ClickedItem.ToString() == "휴가삭제")
            {
                string qry = "UPDATE DUTY_TRSJREQ SET UPDT='" + gd.GetNow() + "', USID = '" + SilkRoad.Config.SRConfig.USID + "', PSTY='D' WHERE SEQNO = " + SeqNo;
                sd.SetExecuteNonQuery(dbname, qry);

                baseInfoSearch();
            }
        }


        //휴가사용일자 선택시
        private void dat_ycdt_EditValueChanged(object sender, EventArgs e)
		{
            if (clib.DateToText(dat_ycdt2.DateTime) == "")
                dat_ycdt2.DateTime = dat_ycdt.DateTime;
        }
		
		private void END_CHK()
		{
			string yymm = clib.DateToText(dat_ycdt.DateTime).Substring(0, 4) + "." + clib.DateToText(dat_ycdt.DateTime).Substring(4, 2);
			df.Get2020_SEARCH_ENDSDatas(clib.DateToText(dat_ycdt.DateTime).Substring(0, 6), ds);
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

        //휴가종류 선택시 유,무급 자동 표기
        private void sl_gnmu_EditValueChanged(object sender, EventArgs e)
        {
            if (sl_gnmu.EditValue != null)
            {
                if (ds.Tables["8050_SEARCH_GNMU"].Select("G_CODE = '" + sl_gnmu.EditValue.ToString() + "'").Length > 0)                
                    sr_pay.SelectedIndex = ds.Tables["8050_SEARCH_GNMU"].Select("G_CODE = '" + sl_gnmu.EditValue.ToString() + "'")[0]["G_TYPE"].ToString() == "13" ? 0 : 1;                
            }
        }

        private void grdv_hg_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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
                if (clib.DateToText(dat_yymm.DateTime) == "")
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
            else if (mode == 2)  //등록
            {
				//if (ends_yn2 == "Y")
				//{
				//	MessageBox.Show("최종마감되어 등록할 수 없습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
				//	return false;
				//}
				if (clib.DateToText(dat_ycdt.DateTime) == "")
				{
					MessageBox.Show("신청일자(시작)이 입력되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					dat_ycdt.Focus();
					return false;
				}
				else if (clib.DateToText(dat_ycdt2.DateTime) == "")
				{
					MessageBox.Show("신청일자(종료)가 입력되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					dat_ycdt2.Focus();
					return false;
				}
				else if (sl_gnmu.EditValue == null)
				{
					MessageBox.Show("휴가유형이 선택되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					sl_gnmu.Focus();
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

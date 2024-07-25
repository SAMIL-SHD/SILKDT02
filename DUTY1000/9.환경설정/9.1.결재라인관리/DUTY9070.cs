using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using System.Text;
using System.Drawing;

namespace DUTY1000
{
    public partial class duty9070 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();
        static string wagedb = "WG06DB" + SilkRoad.Config.SRConfig.WorkPlaceNo;

        ClearNEnableControls cec = new ClearNEnableControls();
        static DataProcessing dp = new DataProcessing();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();

        public duty9070()
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
            if (stat == 0 || stat == 1)
            {
                sl_dept.Enabled = true;
                SetButtonEnable("1000");
                if (ds.Tables["GRD_LINE"] != null)
                    ds.Tables["GRD_LINE"].Clear();
                grd_dept.DataSource = null;
                sl_dept.EditValue = null;
                sl_line.EditValue = null;

                df.Get9070_SEARCH_DEPTDatas(ds);
                grd1.DataSource = ds.Tables["9070_SEARCH_DEPT"];
            }
            if (stat == 0 || stat == 2)
            {
                sl_embs.Enabled = true;
                SetButtonEnable2("1000");
                if (ds.Tables["GRD_LINE2"] != null)
                    ds.Tables["GRD_LINE2"].Clear();
                grd_sabn.DataSource = null;
                sl_embs.EditValue = null;
                sl_line2.EditValue = null;

                df.Get9070_SEARCH_SABNDatas(ds);
                grd2.DataSource = ds.Tables["9070_SEARCH_SABN"];
            }

            DataTable _dt = new DataTable();
            _dt.Columns.Add("LINE_SABN");
            _dt.Columns.Add("LINE_SANM");
            _dt.Columns.Add("LINE_JIWK");
            dp.AddDatatable2Dataset("GRD_LINE", _dt, ref ds);
            grd_dept.DataSource = ds.Tables["GRD_LINE"];

            DataTable _dt2 = new DataTable();
            _dt2.Columns.Add("LINE_SABN");
            _dt2.Columns.Add("LINE_SANM");
            _dt2.Columns.Add("LINE_JIWK");
            dp.AddDatatable2Dataset("GRD_LINE2", _dt2, ref ds);
            grd_sabn.DataSource = ds.Tables["GRD_LINE2"];
        }

        #endregion

        #region 1 Form
		
        private void duty9070_Load(object sender, EventArgs e)
        {
            btn_refresh_CK();

            SetCancel(0);
        }

        #endregion

        #region 2-1 부서별결재라인 Button

        private void btn_refresh_Click(object sender, EventArgs e)
		{
			btn_refresh_CK();
		}
		private void btn_refresh_CK()
		{
            df.GetSEARCH_DEPTDatas(ds);
            sl_dept.Properties.DataSource = ds.Tables["SEARCH_DEPT"];
            df.GetSEARCH_EMBS_POWERDatas(3, ds);
            sl_embs.Properties.DataSource = ds.Tables["SEARCH_EMBS_POWER"];
            df.GetGW_LINEDatas(ds);
            sl_line.Properties.DataSource = ds.Tables["GW_LINE"];
            sl_line2.Properties.DataSource = ds.Tables["GW_LINE"];
        }

        //처리
        private void btn_proc_Click(object sender, EventArgs e)
        {
            if (isNoError_um(1))
            {
                df.GetDEL_GW_LINE_DEPTDatas(2, sl_dept.EditValue.ToString(), ds);
                for (int i = 0; i < ds.Tables["DEL_GW_LINE_DEPT"].Rows.Count; i++)
                {
                    DataRow nrow = ds.Tables["GRD_LINE"].NewRow();
                    nrow["LINE_SABN"] = ds.Tables["DEL_GW_LINE_DEPT"].Rows[i]["LINE_SABN"].ToString();
                    nrow["LINE_SANM"] = ds.Tables["DEL_GW_LINE_DEPT"].Rows[i]["LINE_SANM"].ToString();
                    nrow["LINE_JIWK"] = ds.Tables["DEL_GW_LINE_DEPT"].Rows[i]["LINE_JIWK"].ToString();
                    ds.Tables["GRD_LINE"].Rows.Add(nrow);
                }

                grd_dept.DataSource = ds.Tables["GRD_LINE"];
                sl_dept.Enabled = false;
                SetButtonEnable("0111");
            }
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
                    df.GetDUTY_GW_LINE_DEPTDatas(ds);
                    for (int i = 0; i < grdv_dept.RowCount; i++)
                    {
                        DataRow crow = grdv_dept.GetDataRow(grdv_dept.GetVisibleRowHandle(i));
                        DataRow nrow2 = ds.Tables["DUTY_GW_LINE_DEPT"].NewRow();
                        nrow2["DEPT"] = sl_dept.EditValue.ToString();
                        nrow2["LINE_SQ"] = i + 1;
                        nrow2["LINE_SABN"] = crow["LINE_SABN"].ToString();
                        nrow2["LINE_SANM"] = crow["LINE_SANM"].ToString();
                        nrow2["LINE_JIWK"] = crow["LINE_JIWK"].ToString();
                        ds.Tables["DUTY_GW_LINE_DEPT"].Rows.Add(nrow2);
                    }

                    df.GetDEL_GW_LINE_DEPTDatas(2, sl_dept.EditValue.ToString(), ds);
                    for (int i = 0; i < ds.Tables["DEL_GW_LINE_DEPT"].Rows.Count; i++)
                    {
                        ds.Tables["DEL_GW_LINE_DEPT"].Rows[i].Delete();
                    }

                    string[] tableNames = new string[] { "DEL_GW_LINE_DEPT", "DUTY_GW_LINE_DEPT" };
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
                        MessageBox.Show("해당부서의 결재라인이 등록되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SetCancel(1);
                    }
                    Cursor = Cursors.Default;
                }
            }
        }
        /// <summary>취소버튼</summary>
        private void btn_canc_Click(object sender, EventArgs e)
        {
            SetCancel(1);
        }
        //결재자 추가
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
        //결재라인 삭제
        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            grdv_dept.DeleteRow(grdv_dept.FocusedRowHandle);
        }

        #endregion

        #region 2-2 개인별결재라인 Button
        

        private void btn_refresh2_Click(object sender, EventArgs e)
        {
            btn_refresh_CK();
        }

        private void btn_proc2_Click(object sender, EventArgs e)
        {
            if (isNoError_um(3))
            {
                df.GetDEL_GW_LINEDatas(sl_embs.EditValue.ToString(), ds);
                for (int i = 0; i < ds.Tables["DEL_GW_LINE"].Rows.Count; i++)
                {
                    DataRow nrow = ds.Tables["GRD_LINE2"].NewRow();
                    nrow["LINE_SABN"] = ds.Tables["DEL_GW_LINE"].Rows[i]["LINE_SABN"].ToString();
                    nrow["LINE_SANM"] = ds.Tables["DEL_GW_LINE"].Rows[i]["LINE_SANM"].ToString();
                    nrow["LINE_JIWK"] = ds.Tables["DEL_GW_LINE"].Rows[i]["LINE_JIWK"].ToString();
                    ds.Tables["GRD_LINE2"].Rows.Add(nrow);
                }

                grd_sabn.DataSource = ds.Tables["GRD_LINE2"];
                sl_embs.Enabled = false;
                SetButtonEnable2("0111");
            }
        }

        //저장
        private void btn_save2_Click(object sender, EventArgs e)
        {
            if (isNoError_um(4))
            {
                Cursor = Cursors.WaitCursor;
                int outVal = 0;
                try
                {
                    df.GetDUTY_GW_LINEDatas(ds);
                    for (int i = 0; i < grdv_sabn.RowCount; i++)
                    {
                        DataRow crow = grdv_sabn.GetDataRow(grdv_sabn.GetVisibleRowHandle(i));
                        DataRow nrow2 = ds.Tables["DUTY_GW_LINE"].NewRow();
                        nrow2["SABN"] = sl_embs.EditValue.ToString();
                        nrow2["LINE_SQ"] = i + 1;
                        nrow2["LINE_SABN"] = crow["LINE_SABN"].ToString();
                        nrow2["LINE_SANM"] = crow["LINE_SANM"].ToString();
                        nrow2["LINE_JIWK"] = crow["LINE_JIWK"].ToString();
                        ds.Tables["DUTY_GW_LINE"].Rows.Add(nrow2);
                    }
                    df.GetDEL_GW_LINEDatas(sl_embs.EditValue.ToString(), ds);
                    for (int i = 0; i < ds.Tables["DEL_GW_LINE"].Rows.Count; i++)
                    {
                        ds.Tables["DEL_GW_LINE"].Rows[i].Delete();
                    }

                    string[] tableNames = new string[] { "DEL_GW_LINE", "DUTY_GW_LINE" };
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
                        MessageBox.Show("해당직원의 결재라인이 등록되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SetCancel(2);
                    }
                    Cursor = Cursors.Default;
                }
            }
        }

        private void btn_canc2_Click(object sender, EventArgs e)
        {
            SetCancel(2);
        }
        //결재자 추가
        private void btn_add2_Click(object sender, EventArgs e)
        {
            if (sl_line2.EditValue == null)
            {
                MessageBox.Show("검색된 결재라인이 없습니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sl_line2.Focus();
            }
            else if (ds.Tables["GRD_LINE2"].Select("LINE_SABN = '" + sl_line2.EditValue.ToString() + "'").Length > 0)
            {
                MessageBox.Show("이미 추가되어 있습니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sl_line2.Focus();
            }
            else
            {
                DataRow nrow = ds.Tables["GRD_LINE2"].NewRow();
                nrow["LINE_SABN"] = sl_line2.EditValue.ToString();
                nrow["LINE_SANM"] = sl_line2.Text.ToString();
                nrow["LINE_JIWK"] = ds.Tables["GW_LINE"].Select("CODE ='" + sl_line2.EditValue.ToString() + "'")[0]["POSI_NM"].ToString();

                ds.Tables["GRD_LINE2"].Rows.Add(nrow);
            }
        }
        private void repositoryItemButtonEdit2_Click(object sender, EventArgs e)
        {
            grdv_sabn.DeleteRow(grdv_sabn.FocusedRowHandle);
        }

        #endregion

        #region 3 EVENT

        //부서별 조회
        private void grdv1_DoubleClick(object sender, EventArgs e)
        {
            DataRow drow = grdv1.GetFocusedDataRow();
            if (drow == null)
                return;

            SetCancel(1);
            sl_dept.EditValue = drow["CODE"].ToString();
            btn_proc.PerformClick();
        }

        //부서별 조회
        private void grdv2_DoubleClick(object sender, EventArgs e)
        {
            DataRow drow = grdv2.GetFocusedDataRow();
            if (drow == null)
                return;

            SetCancel(2);
            sl_embs.EditValue = drow["SABN"].ToString();
            btn_proc2.PerformClick();
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
                if (sl_dept.EditValue == null)
                {
                    MessageBox.Show("부서를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sl_dept.Focus();
                    return false;
                }
                else
                {
                    isError = true;
                }
            }
            else if (mode == 2)  //저장
            {
				if (grdv_dept.RowCount == 0)
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
            else if (mode == 3)  //조회2
            {
                if (sl_embs.EditValue == null)
                {
                    MessageBox.Show("사원을 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sl_embs.Focus();
                    return false;
                }
                else
                {
                    isError = true;
                }
            }
            else if (mode == 4)  //저장2
            {
                if (grdv_sabn.RowCount == 0)
                {
                    MessageBox.Show("결재라인이 입력되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sl_line2.Focus();
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
            btn_canc.Enabled = arr.Substring(2, 1) == "1" ? true : false;
            btn_add.Enabled = arr.Substring(3, 1) == "1" ? true : false;
        }

        private void SetButtonEnable2(string arr)
        {
            btn_proc2.Enabled = arr.Substring(0, 1) == "1" ? true : false;
            btn_save2.Enabled = arr.Substring(1, 1) == "1" ? true : false;
            btn_canc2.Enabled = arr.Substring(2, 1) == "1" ? true : false;
            btn_add2.Enabled = arr.Substring(3, 1) == "1" ? true : false;
        }

        #endregion

    }
}

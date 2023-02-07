using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;


namespace DUTY1000
{
    public partial class duty2022 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();
		string yymm = "";
		string dept = "";

        public duty2022(string _yymm, string _dept)
        {
            InitializeComponent();
			yymm = _yymm;
			dept = _dept;
        }

        #region 0. Initialization

        /// <summary>
        ///컨트롤 초기화 및 활성,비활성 설정
        /// </summary>
        /// <param name="enable"></param>
        private void SetCancel()
        {
			grd1.DataSource = null;
            //SetButtonEnable("100");
        }

        #endregion

        #region 1 Form

        private void duty2022_Load(object sender, EventArgs e)
        {			
			df.Get8030_SEARCH_EMBSDatas(dept, ds);
			sl_embs.Properties.DataSource = ds.Tables["8030_SEARCH_EMBS"];

			if (ds.Tables["8030_SEARCH_EMBS"].Select("CODE = '" + SilkRoad.Config.SRConfig.USID + "'").Length > 0)
				sl_embs.EditValue = SilkRoad.Config.SRConfig.USID;
			else
				sl_embs.EditValue = null;

			string embs = sl_embs.EditValue == null ? "%" : sl_embs.EditValue.ToString();
			df.GetGW_LINE1Datas(embs, "'2'", ds);
			sl_line1.Properties.DataSource = ds.Tables["GW_LINE1"];
			df.GetGW_LINE2Datas(embs, "'5','6'", ds);
			sl_line2.Properties.DataSource = ds.Tables["GW_LINE2"];

			sl_line1.EditValue = null;
			sl_line2.EditValue = null;
			
			dat_jsmm.DateTime = clib.TextToDate(yymm + "01");
			Proc();
        }

        #endregion

        #region 2 Button

        //처리
        private void btn_proc_Click(object sender, EventArgs e)
        {
			//Proc();
        }

        private void Proc()
        {
			df.Get2022_SEARCH_CALLDatas(yymm, dept, ds);
			grd1.DataSource = ds.Tables["2022_SEARCH_CALL"];
            //SetButtonEnable("011");
        }

        //저장버튼
        private void btn_save_Click(object sender, EventArgs e)
        {
			int stat = 0;
			decimal doc_no = 0;
			if (isNoError_um(1))
			{
				Cursor = Cursors.WaitCursor;
				int outVal = 0;
				try
				{
					df.GetDUTY_GWDOCDatas(ds);
					DataRow hrow = ds.Tables["DUTY_GWDOC"].NewRow();
					doc_no = df.GetGWDOC_NODatas(ds);

					hrow["DOC_NO"] = doc_no;
					hrow["DOC_GUBN"] = 2;
					hrow["DOC_DATE"] = yymm;
					hrow["DOC_JSMM"] = clib.DateToText(dat_jsmm.DateTime).Substring(0, 6);
					hrow["GW_TITLE"] = yymm.Substring(0,4)+"년 "+yymm.Substring(4,2)+"월 "+ds.Tables["2022_SEARCH_CALL"].Rows[0]["DEPT_NM"].ToString()+" OT 내역"; // 년월 부서 OT 내역
					hrow["GW_REMK"] = "";
					hrow["AP_TAG"] = "4";
					hrow["LINE_CNT"] = 1;
					if (sl_line2.EditValue != null)
						hrow["LINE_CNT"] = 3;
					else if (sl_line1.EditValue != null)
						hrow["LINE_CNT"] = 2;

					hrow["GW_SABN1"] = sl_embs.EditValue.ToString();
					hrow["GW_DT1"] = gd.GetNow();
					hrow["GW_NAME1"] = ds.Tables["8030_SEARCH_EMBS"].Select("CODE ='" + sl_embs.EditValue.ToString() + "'")[0]["NAME"].ToString();
					hrow["GW_JICK1"] = ds.Tables["8030_SEARCH_EMBS"].Select("CODE ='" + sl_embs.EditValue.ToString() + "'")[0]["GRAD_NM"].ToString();
					for (int i = 2; i <= 4; i++)
					{
						hrow["GW_SABN" + i.ToString()] = "";
						hrow["GW_DT" + i.ToString()] = "";
						hrow["GW_CHKID" + i.ToString()] = "";
						hrow["GW_NAME" + i.ToString()] = "";
						hrow["GW_JICK" + i.ToString()] = "";
					}
					if (sl_line1.EditValue != null)
					{
						hrow["GW_SABN2"] = sl_line1.EditValue.ToString();
						hrow["GW_NAME2"] = ds.Tables["GW_LINE1"].Select("CODE ='" + sl_line1.EditValue.ToString() + "'")[0]["NAME"].ToString();
						hrow["GW_JICK2"] = ds.Tables["GW_LINE1"].Select("CODE ='" + sl_line1.EditValue.ToString() + "'")[0]["GRAD_NM"].ToString();
					}
					if (sl_line2.EditValue != null)
					{
						hrow["GW_SABN3"] = sl_line2.EditValue.ToString();
						hrow["GW_NAME3"] = ds.Tables["GW_LINE2"].Select("CODE ='" + sl_line2.EditValue.ToString() + "'")[0]["NAME"].ToString();
						hrow["GW_JICK3"] = ds.Tables["GW_LINE2"].Select("CODE ='" + sl_line2.EditValue.ToString() + "'")[0]["GRAD_NM"].ToString();
					}		
					hrow["REG_DT"] = gd.GetNow();
					hrow["REG_ID"] = SilkRoad.Config.SRConfig.USID;
					ds.Tables["DUTY_GWDOC"].Rows.Add(hrow);

					string[] tableNames = new string[] { "DUTY_GWDOC" };
					SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
					outVal = cmd.setUpdate(ref ds, tableNames, null);

					if (outVal > 0)
					{
						df.GetGW_TRSOVTMDatas(ds);  //등록
						foreach (DataRow dr in ds.Tables["2022_SEARCH_CALL"].Rows)
						{
							if (dr["GUBN"].ToString() == "1" && dr["CHK"].ToString() == "1")
							{
								DataRow nrow = ds.Tables["GW_TRSOVTM"].NewRow();
								nrow["DOC_NO"] = doc_no;
								nrow["DOC_JSMM"] = clib.DateToText(dat_jsmm.DateTime).Substring(0, 6);
								nrow["SABN"] = dr["SABN"].ToString();
								nrow["OT_DATE"] = dr["OT_DATE"].ToString();
								nrow["OT_GUBN"] = "2";
								nrow["CALL_CNT1"] = 0;
								nrow["CALL_CNT2"] = 0;
								nrow["CALL_TIME1"] = 0;
								nrow["CALL_TIME2"] = 0;
								nrow["OT_TIME1"] = clib.TextToDecimal(dr["OT_TIME1"].ToString());
								nrow["OT_TIME2"] = clib.TextToDecimal(dr["OT_TIME2"].ToString());
								nrow["TIME_REMK"] = dr["TIME_REMK"].ToString();
								nrow["REMARK"] = dr["REMARK"].ToString();
								ds.Tables["GW_TRSOVTM"].Rows.Add(nrow);
							}
						}
						df.GetGW_TRSOVTM_JSDatas(ds);  //등록
						foreach (DataRow dr in ds.Tables["2022_SEARCH_CALL"].Rows)
						{
							if (dr["GUBN"].ToString() == "2" && dr["CHK"].ToString() == "1")
							{
								DataRow nrow = ds.Tables["GW_TRSOVTM_JS"].NewRow();
								nrow["DOC_NO"] = doc_no;
								nrow["DOC_JSMM"] = clib.DateToText(dat_jsmm.DateTime).Substring(0, 6);
								nrow["SABN"] = dr["SABN"].ToString();
								nrow["JS_DATE"] = dr["OT_DATE"].ToString();
								nrow["JS_TIME"] = clib.TextToDecimal(dr["JS_TIME"].ToString());
								nrow["CALC_TIME"] = clib.TextToDecimal(dr["CALC_TIME"].ToString());
								nrow["TIME_REMK"] = dr["TIME_REMK"].ToString();
								nrow["REMARK"] = dr["REMARK"].ToString();
								ds.Tables["GW_TRSOVTM_JS"].Rows.Add(nrow);
							}
						}

						tableNames = new string[] { "GW_TRSOVTM", "GW_TRSOVTM_JS" };
						outVal = cmd.setUpdate(ref ds, tableNames, null);

						// OT 내역에 UPDATE
						string qry1 = " UPDATE A "
									+ "    SET A.DOC_NO = '" + doc_no + "' "
									+ "   FROM DUTY_TRSOVTM A "
									+ "  INNER JOIN GW_TRSOVTM B "
									+ "     ON B.DOC_NO = '" + doc_no + "' "
									+ "    AND A.SABN = B.SABN "
									+ "    AND A.OT_DATE = B.OT_DATE "
									+ "  WHERE A.OT_GUBN = '2' "
									+ " "
									+ " UPDATE A "
									+ "    SET A.DOC_NO = '" + doc_no + "' "
									+ "   FROM DUTY_TRSOVTM_JS A "
									+ "  INNER JOIN GW_TRSOVTM_JS B "
									+ "     ON B.DOC_NO = '" + doc_no + "' "
									+ "    AND A.SABN = B.SABN "
									+ "    AND A.JS_DATE = B.JS_DATE ";

						string[] qrys = new string[] { qry1 };
						cmd.setUpdate(ref ds, null, qrys);

						stat = 2;
					}
				}
				catch (Exception ec)
				{
					stat = 1;
					MessageBox.Show(ec.Message, "저장오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				finally
				{
					//SetCancel();
					Cursor = Cursors.Default;
					if (stat == 2)
						this.Dispose();
				}
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

        private void duty2022_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //취소
            {
                //btn_clear.PerformClick();
            }
        }
		
		//중간관리자 제외
		private void chk_line_CheckedChanged(object sender, EventArgs e)
		{
			if (chk_line.Checked == false)
			{
				sl_line1.EditValue = null;
				sl_line2.EditValue = null;
				string embs = sl_embs.EditValue == null ? "%" : sl_embs.EditValue.ToString();
				df.GetGW_LINE1Datas(embs, "'2'", ds);
				sl_line1.Properties.DataSource = ds.Tables["GW_LINE1"];
				df.GetGW_LINE2Datas(embs, "'5','6'", ds);
				sl_line2.Properties.DataSource = ds.Tables["GW_LINE2"];

				sl_line2.Visible = true;
				lb_line.Text = "[팀장 -> 부서장 -> 대표/담당원장]";
			}
			else
			{
				sl_line1.EditValue = null;
				string embs = sl_embs.EditValue == null ? "%" : sl_embs.EditValue.ToString();
				df.GetGW_LINE1Datas(embs, "'5','6'", ds);
				sl_line1.Properties.DataSource = ds.Tables["GW_LINE1"];

				sl_line2.Visible = false;
				lb_line.Text = "[팀장 -> 대표/담당원장]";
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
            if (mode == 1)  //상신
            {
				if (ds.Tables["2022_SEARCH_CALL"].Select("CHK='1'").Length < 1)
				{
					MessageBox.Show("선택된 내역이 없습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				else if (sl_embs.EditValue == null)
				{
					MessageBox.Show("기안자를 선택하세요!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					sl_embs.Focus();
					return false;
				}
				else if (sl_line1.Visible == true && sl_line1.EditValue == null)
				{
					MessageBox.Show("결재자1이 선택되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					sl_line1.Focus();
					return false;
				}
				else if (sl_line2.Visible == true && sl_line2.EditValue == null)
				{
					MessageBox.Show("결재자2가 선택되지 않았습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

		#endregion
	}
}

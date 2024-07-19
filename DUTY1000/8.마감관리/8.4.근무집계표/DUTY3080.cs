using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;

namespace DUTY1000
{
	public partial class duty3080 : SilkRoad.Form.Base.FormX
	{
		CommonLibrary clib = new CommonLibrary();

		ClearNEnableControls cec = new ClearNEnableControls();
		public DataSet ds = new DataSet();
		DataProcFunc df = new DataProcFunc();
		SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();
		static DataProcessing dp = new DataProcessing();
        private string ends_yn = "", wage_ends_yn = "";

		public duty3080()
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
            if (stat == 1)
            {
                if (ds.Tables["WORK_3080"] != null)
                    ds.Tables["WORK_3080"].Clear();
                grd1.DataSource = null;

                dat_p_yymm.Enabled = true;
                cmb_gubn.Enabled = true;
                SetButtonEnable("100");
            }
            else if (stat == 2)
            {
                if (ds.Tables["SEARCH_3080"] != null)
                    ds.Tables["SEARCH_3080"].Clear();
                grd_end.DataSource = null;

                dat_yymm.Enabled = true;
                sl_embs.Enabled = true;
                SetButtonEnable2("1000");
            }
        }

		#endregion

		#region 1 Form

		private void duty3080_Load(object sender, EventArgs e)
		{
			SetCancel(1);
		}
		private void duty3080_Shown(object sender, EventArgs e)
        {
            df.GetSEARCH_EMBSDatas(ds);
            sl_embs.Properties.DataSource = ds.Tables["SEARCH_EMBS"];

            df.GetSEARCH_DEPTDatas(ds);
            sl_s_dept.Properties.DataSource = ds.Tables["SEARCH_DEPT"];

            dat_p_yymm.DateTime = DateTime.Now.AddMonths(-1);
			cmb_gubn.SelectedIndex = 0;

			dat_yymm.DateTime = DateTime.Now.AddMonths(-1);
            dat_s_frmm.DateTime = DateTime.Now.AddMonths(-1);
            dat_s_tomm.DateTime = DateTime.Now.AddMonths(-1);

            sl_embs.EditValue = null;
			sl_s_dept.EditValue = null;

            SetCancel(1);

            //info_Search();
		}

		private void info_Search(int stat)
        {
            df.GetDUTY_INFOSD06Datas(ds);
            df.GetSEARCH_INFOWAGEDatas(ds);
            if (stat == 2) 
            {
                #region 급여처리 탭에서
                //수당설정된 컬럼만 보이게 하고
                if (ds.Tables["DUTY_INFOSD06"].Rows.Count > 0)
                {
                    for (int i = 0; i < grdv_end.Columns.Count; i++)
                    {
                        if (grdv_end.Columns[i].FieldName.Substring(4, 2) == "SD")
                        {
                            grdv_end.Columns[i].Visible = false;
                            for (int j = 1; j <= 9; j++)
                            {
                                if (grdv_end.Columns[i].FieldName.Substring(4, 4) == "SD" + ds.Tables["DUTY_INFOSD06"].Select("SQ = " + j)[0]["SD_CODE"].ToString().PadLeft(4, ' ').Substring(2, 2))
                                {
                                    grdv_end.Columns[i].Visible = true;
                                    break;
                                }
                            }
                        }
                        if (grdv_end.Columns[i].FieldName.Substring(4, 2) == "GT")
                        {
                            grdv_end.Columns[i].Visible = false;
                            for (int j = 1; j <= 9; j++)
                            {
                                if (grdv_end.Columns[i].FieldName.Substring(4, 4) == "GT" + ds.Tables["DUTY_INFOSD06"].Select("SQ = " + j)[0]["GT_CODE"].ToString().PadLeft(4, ' ').Substring(2, 2))
                                {
                                    grdv_end.Columns[i].Visible = true;
                                    break;
                                }
                            }
                        }
                    }
                }

                //수당명칭 가져와서 설정
                foreach (DataRow drow in ds.Tables["SEARCH_INFOWAGE"].Rows)
                {
                    for (int i = 0; i < grdv_end.Columns.Count; i++)
                    {
                        if (grdv_end.Columns[i].Caption == drow["IFWGCODE"].ToString())
                            grdv_end.Columns[i].Caption = drow["IFWGNAME"].ToString().Trim();
                    }
                }
                #endregion
            }
            else if (stat == 3)
            {
                #region 내역조회 탭에서
                //수당설정된 컬럼만 보이게 하고
                if (ds.Tables["DUTY_INFOSD06"].Rows.Count > 0)
                {
                    for (int i = 0; i < grdv_search.Columns.Count; i++)
                    {
                        if (grdv_search.Columns[i].FieldName.Substring(4, 2) == "SD")
                        {
                            grdv_search.Columns[i].Visible = false;
                            for (int j = 1; j <= 9; j++)
                            {
                                if (grdv_search.Columns[i].FieldName.Substring(4, 4) == "SD" + ds.Tables["DUTY_INFOSD06"].Select("SQ = " + j)[0]["SD_CODE"].ToString().PadLeft(4, ' ').Substring(2, 2))
                                {
                                    grdv_search.Columns[i].Visible = true;
                                    break;
                                }
                            }
                        }
                        if (grdv_search.Columns[i].FieldName.Substring(4, 2) == "GT")
                        {
                            grdv_search.Columns[i].Visible = false;
                            for (int j = 1; j <= 9; j++)
                            {
                                if (grdv_search.Columns[i].FieldName.Substring(4, 4) == "GT" + ds.Tables["DUTY_INFOSD06"].Select("SQ = " + j)[0]["GT_CODE"].ToString().PadLeft(4,' ').Substring(2, 2))
                                {
                                    grdv_search.Columns[i].Visible = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                //수당명칭 가져와서 설정
                foreach (DataRow drow in ds.Tables["SEARCH_INFOWAGE"].Rows)
                {
                    for (int i = 0; i < grdv_search.Columns.Count; i++)
                    {
                        if (grdv_search.Columns[i].Caption == drow["IFWGCODE"].ToString())
                            grdv_search.Columns[i].Caption = drow["IFWGNAME"].ToString().Trim();
                    }
                }
                #endregion
            }
        }

		#endregion

		#region 2 Button

		//마감처리
		private void btn_proc_Click(object sender, EventArgs e)
		{
			if (isNoError_um(1))
			{
				string yymm = clib.DateToText(dat_p_yymm.DateTime).Substring(0, 6);
				string gubn = cmb_gubn.SelectedIndex.ToString();

				df.GetWORK_3080Datas(yymm, gubn, ds);
				grd1.DataSource = ds.Tables["WORK_3080"];

                if (ds.Tables["WORK_3080"].Rows.Count == 0)
                {
                    MessageBox.Show("처리할 내역이 없습니다.", "없음", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    dat_p_yymm.Enabled = false;
                    cmb_gubn.Enabled = false;
                    SetButtonEnable("011");
                }
            }
		}
		//취소
		private void btn_canc_Click(object sender, EventArgs e)
		{
            SetCancel(1);
        }
		//마감등록
		private void btn_end_Click(object sender, EventArgs e)
		{
			if (isNoError_um(2))
			{
				DialogResult dr = MessageBox.Show("조회된 내역을 마감등록하시겠습니까?", "등록여부", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
				if (dr == DialogResult.OK)
				{
					Cursor = Cursors.WaitCursor;
					int outVal = 0;
					try
					{
						if (ds.Tables["WORK_3080"].Select("END_YN = ''").Length == 0)
						{
							MessageBox.Show("마감처리할 데이터가 없습니다.", "마감오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}
						else
						{
							df.GetDUTY_INFOSD06Datas(ds);
							df.GetS_DUTY_MSTWGPCDatas(ds);

                            #region 사원별 집계
                            foreach (DataRow drow in ds.Tables["WORK_3080"].Rows)
							{
								if (drow["END_YN"].ToString() == "" && clib.TextToDecimal(drow["SD_AMT"].ToString()) != 0)  //금액이 있는것만
								{
									#region 환경설정 수당코드 가져오기
									string sd_code = "", gt_code = "";

                                    if (ds.Tables["DUTY_INFOSD06"].Select("SQ = " + clib.TextToInt(drow["GUBN"].ToString())).Length > 0)
                                    {
                                        DataRow irow = ds.Tables["DUTY_INFOSD06"].Select("SQ = " + clib.TextToInt(drow["GUBN"].ToString()))[0];
                                        sd_code = irow["SD_CODE"].ToString().Trim() == "" ? "" : irow["SD_CODE"].ToString().Substring(2, 2);
                                        gt_code = irow["GT_CODE"].ToString().Trim() == "" ? "" : irow["GT_CODE"].ToString().Substring(2, 2);
                                    }
									#endregion

									if (ds.Tables["S_DUTY_MSTWGPC"].Select("SAWON_NO = '" + drow["SAWON_NO"].ToString().Trim() + "' AND END_YYMM = '" + drow["SLDT"].ToString().Substring(0, 6) + "'").Length > 0)
									{
                                        DataRow nrow = ds.Tables["S_DUTY_MSTWGPC"].Select("SAWON_NO = '" + drow["SAWON_NO"].ToString().Trim() + "' AND END_YYMM = '" + drow["SLDT"].ToString().Substring(0, 6) + "' ")[0];

										if (sd_code != "")
										    nrow["WGPCSD" + sd_code] = clib.TextToDecimal(nrow["WGPCSD" + sd_code].ToString()) + clib.TextToDecimal(drow["SD" + drow["GUBN"].ToString().PadLeft(2, '0')].ToString());
                                        if (gt_code != "")
                                            nrow["WGPCGT" + gt_code] = clib.TextToDecimal(nrow["WGPCGT" + gt_code].ToString()) + clib.TextToDecimal(drow["GT" + drow["GUBN"].ToString().PadLeft(2, '0')].ToString());
									}
									else
									{
                                        DataRow nrow = ds.Tables["S_DUTY_MSTWGPC"].NewRow();

                                        nrow["SAWON_NO"] = drow["SAWON_NO"].ToString().Trim();
                                        nrow["END_YYMM"] = drow["SLDT"].ToString().Substring(0, 6);
                                        for (int i = 1; i <= 50; i++)
                                        {
                                            nrow["WGPCSD" + i.ToString().PadLeft(2, '0')] = 0;
                                            if (i <= 30)
                                                nrow["WGPCGT" + i.ToString().PadLeft(2, '0')] = 0;
                                        }

                                        if (sd_code != "")
                                            nrow["WGPCSD" + sd_code] = clib.TextToDecimal(drow["SD" + drow["GUBN"].ToString().PadLeft(2, '0')].ToString());
                                        if (gt_code != "")
                                            nrow["WGPCGT" + gt_code] = clib.TextToDecimal(drow["GT" + drow["GUBN"].ToString().PadLeft(2, '0')].ToString());

                                        ds.Tables["S_DUTY_MSTWGPC"].Rows.Add(nrow);
									}

                                    //사원별 수당구분별 마감여부 이력.
									df.GetDUTY_MSTWGPC_ENDDatas(drow["SLDT"].ToString().Substring(0, 6), drow["SAWON_NO"].ToString().Trim(), clib.TextToInt(drow["GUBN"].ToString()), ds);
									if (ds.Tables["DUTY_MSTWGPC_END"].Rows.Count == 0)
									{
										DataRow erow = ds.Tables["DUTY_MSTWGPC_END"].NewRow();
										erow["END_YYMM"] = clib.DateToText(dat_p_yymm.DateTime).Substring(0, 6);
										erow["SAWON_NO"] = drow["SAWON_NO"].ToString().Trim();
										erow["GUBN"] = drow["GUBN"].ToString().Trim();
										erow["REG_DT"] = gd.GetNow();
										erow["REG_ID"] = SilkRoad.Config.SRConfig.USID;

										ds.Tables["DUTY_MSTWGPC_END"].Rows.Add(erow);
										string[] tb_nm = new string[] { "DUTY_MSTWGPC_END" };
										SilkRoad.DbCmd_DT02.DbCmd_DT02 cmd0 = new SilkRoad.DbCmd_DT02.DbCmd_DT02();
										cmd0.setUpdate(ref ds, tb_nm, null);
									}								
								}
                            }
                            #endregion

                            #region 마감테이블 저장
                            foreach (DataRow drow in ds.Tables["S_DUTY_MSTWGPC"].Rows)
							{
								df.GetDUTY_MSTWGPCDatas(drow["END_YYMM"].ToString().Trim(), drow["SAWON_NO"].ToString().Trim(), ds);
								if (ds.Tables["DUTY_MSTWGPC"].Rows.Count > 0)
								{
                                    DataRow nrow2 = ds.Tables["DUTY_MSTWGPC"].Rows[0];
									for (int i = 1; i <= 50; i++)
									{
										nrow2["WGPCSD" + i.ToString().PadLeft(2, '0')] = clib.TextToDecimal(nrow2["WGPCSD" + i.ToString().PadLeft(2, '0')].ToString()) + clib.TextToDecimal(drow["WGPCSD" + i.ToString().PadLeft(2, '0')].ToString());
										if (i <= 30)
											nrow2["WGPCGT" + i.ToString().PadLeft(2, '0')] = clib.TextToDecimal(nrow2["WGPCGT" + i.ToString().PadLeft(2, '0')].ToString()) + clib.TextToDecimal(drow["WGPCGT" + i.ToString().PadLeft(2, '0')].ToString());
									}
									nrow2["UPDT"] = gd.GetNow();
                                    nrow2["USID"] = SilkRoad.Config.SRConfig.USID;
                                    nrow2["PSTY"] = "U";
								}
								else
								{
                                    DataRow nrow2 = ds.Tables["DUTY_MSTWGPC"].NewRow();
                                    nrow2["END_YYMM"] = drow["END_YYMM"].ToString().Trim();
                                    nrow2["SAWON_NO"] = drow["SAWON_NO"].ToString().Trim();
									for (int i = 1; i <= 50; i++)
									{
										nrow2["WGPCSD" + i.ToString().PadLeft(2, '0')] = clib.TextToDecimal(drow["WGPCSD" + i.ToString().PadLeft(2, '0')].ToString());
										if (i <= 30)
											nrow2["WGPCGT" + i.ToString().PadLeft(2, '0')] = clib.TextToDecimal(drow["WGPCGT" + i.ToString().PadLeft(2, '0')].ToString());
									}
									nrow2["INDT"] = gd.GetNow();
									nrow2["UPDT"] = "";
                                    nrow2["USID"] = SilkRoad.Config.SRConfig.USID;
                                    nrow2["PSTY"] = "A";
									ds.Tables["DUTY_MSTWGPC"].Rows.Add(nrow2);
								}

                                string[] tableNames = new string[] { "DUTY_MSTWGPC" };
								SilkRoad.DbCmd_DT02.DbCmd_DT02 cmd = new SilkRoad.DbCmd_DT02.DbCmd_DT02();
								outVal += cmd.setUpdate(ref ds, tableNames, null);
                            }
                            #endregion

                        }
                    }
					catch (Exception ec)
					{
						MessageBox.Show(ec.Message, "저장오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					finally
					{
                        if (outVal > 0)
                        {
                            MessageBox.Show("마감이 " + String.Format("{0:#,###}", outVal) + "건 저장되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SetCancel(1);
                        }
						Cursor = Cursors.Default;
					}
				}
			}
		}


		//조회
		private void btn_search_Click(object sender, EventArgs e)
		{
			if (isNoError_um(3))
            {
                info_Search(2);

                string yymm = clib.DateToText(dat_yymm.DateTime).Substring(0, 6);
				string sabn = sl_embs.EditValue == null ? "%" : sl_embs.EditValue.ToString();
				df.GetSEARCH_3080Datas(yymm, sabn, ds);
				grd_end.DataSource = ds.Tables["SEARCH_3080"];

                if (ds.Tables["SEARCH_3080"].Rows.Count == 0)
                {
                    MessageBox.Show("조회된 내역이 없습니다.", "없음", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    dat_yymm.Enabled = false;
                    sl_embs.Enabled = false;
                    SetButtonEnable2("0111");
                }
            }
		}
		//삭제
		private void btn_s_del_Click(object sender, EventArgs e)
		{
			if (isNoError_um(3))
			{
				DialogResult dr = MessageBox.Show("조회된 내역을 삭제하시겠습니까?", "삭제여부", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
				if (dr == DialogResult.OK)
				{
					Cursor = Cursors.WaitCursor;
					int outVal = 0;
					try
					{
						if (grdv_end.RowCount == 0)
						{
							MessageBox.Show("삭제할 데이터가 없습니다.", "삭제오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}
						else
                        {
                            df.GetDUTY_INFOSD06Datas(ds);
                            for (int i = 0; i < grdv_end.RowCount; i++)
							{
								if (grdv_end.GetVisibleRowHandle(i) > -1)
								{
									DataRow drow = grdv_end.GetDataRow(grdv_end.GetVisibleRowHandle(i));
                                    SilkRoad.DbCmd_DT02.DbCmd_DT02 cmd = new SilkRoad.DbCmd_DT02.DbCmd_DT02();

                                    #region 환경설정 수당코드 가져오기
                                    int gubn = 0;
                                    //DataRow irow = ds.Tables["SEARCH_INFOSD06"].Rows[0];
                                    for (int j = 1; j <= 50; j++)
                                    {
                                        if (clib.TextToDecimal(drow["WGPCSD" + j.ToString().PadLeft(2, '0')].ToString()) != 0)
                                        {
                                            if (ds.Tables["DUTY_INFOSD06"].Select("SD_CODE = 'A0" + j.ToString().PadLeft(2, '0') + "'").Length > 0)
                                            {
                                                gubn = clib.TextToInt(ds.Tables["DUTY_INFOSD06"].Select("SD_CODE = 'A0" + j.ToString().PadLeft(2, '0') + "'")[0]["SQ"].ToString());

                                                df.GetDUTY_MSTWGPC_ENDDatas(drow["END_YYMM"].ToString(), drow["SAWON_NO"].ToString(), gubn, ds);
                                                if (ds.Tables["DUTY_MSTWGPC_END"].Rows.Count > 0)
                                                {
                                                    ds.Tables["DUTY_MSTWGPC_END"].Rows[0].Delete();
                                                    string[] tb_nm = new string[] { "DUTY_MSTWGPC_END" };
                                                    cmd.setUpdate(ref ds, tb_nm, null);
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    df.GetDUTY_MSTWGPCDatas(drow["END_YYMM"].ToString(), drow["SAWON_NO"].ToString(), ds);
									if (ds.Tables["DUTY_MSTWGPC"].Rows.Count != 0)
										ds.Tables["DUTY_MSTWGPC"].Rows[0].Delete();

									string[] tableNames = new string[] { "DUTY_MSTWGPC" };
									outVal += cmd.setUpdate(ref ds, tableNames, null);
								}
							}
						}
					}
					catch (Exception ec)
					{
						MessageBox.Show(ec.Message, "삭제오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					finally
					{
                        if (outVal > 0)
                        {
                            MessageBox.Show(String.Format("{0:#,###}", outVal) + "건의 마감내용이 삭제되었습니다.", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SetCancel(2);
                        }
						Cursor = Cursors.Default;
					}
				}
			}
		}

        private void btn_canc2_Click(object sender, EventArgs e)
        {
            SetCancel(2);
        }
        //급여전송(급여변동항목,월근태등록)
        private void btn_wage_Click(object sender, EventArgs e)
		{
			if (isNoError_um(4))
			{
				string yymm = clib.DateToText(dat_yymm.DateTime).Substring(0, 4) + "." + clib.DateToText(dat_yymm.DateTime).Substring(4, 2);
				DialogResult dr = MessageBox.Show("조회된 " + yymm + " 내역을 인사급여로 전송하시겠습니까?", "전송여부", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
				if (dr == DialogResult.OK)
				{
					Cursor = Cursors.WaitCursor;
					int outVal = 0;
					try
					{
						if (ds.Tables["SEARCH_3080"].Rows.Count < 1)
						{
							MessageBox.Show("전송할 데이터가 없습니다.", "전송오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}
						else
						{
							DataRow srow;
							DataTable dt = ds.Tables["SEARCH_3080"].Clone();
							dp.AddDatatable2Dataset("C_3080", dt, ref ds);

							foreach (DataRow drow in ds.Tables["SEARCH_3080"].Rows)
							{
								if (ds.Tables["C_3080"].Select("END_YYMM = '" + drow["END_YYMM"].ToString() + "' AND SAWON_NO = '" + drow["SAWON_NO"].ToString().Trim() + "'").Length > 0)
								{
									srow = ds.Tables["C_3080"].Select("END_YYMM = '" + drow["END_YYMM"].ToString() + "' AND SAWON_NO = '" + drow["SAWON_NO"].ToString().Trim() + "'")[0];
									for (int i = 1; i <= 30; i++)
									{
										if (i <= 13)
											srow["WGPCGT" + i.ToString().PadLeft(2, '0')] = clib.TextToDecimal(srow["WGPCGT" + i.ToString().PadLeft(2, '0')].ToString()) + clib.TextToDecimal(drow["WGPCGT" + i.ToString().PadLeft(2, '0')].ToString());
										srow["WGPCSD" + i.ToString().PadLeft(2, '0')] = clib.TextToDecimal(srow["WGPCSD" + i.ToString().PadLeft(2, '0')].ToString()) + clib.TextToDecimal(drow["WGPCSD" + i.ToString().PadLeft(2, '0')].ToString());
									}
								}
								else
								{
									srow = ds.Tables["C_3080"].NewRow();
									srow["END_YYMM"] = drow["END_YYMM"].ToString();
									srow["SAWON_NO"] = drow["SAWON_NO"].ToString().Trim();
									for (int i = 1; i <= 30; i++)
									{
										if (i <= 13)
											srow["WGPCGT" + i.ToString().PadLeft(2, '0')] = clib.TextToDecimal(drow["WGPCGT" + i.ToString().PadLeft(2, '0')].ToString());
										srow["WGPCSD" + i.ToString().PadLeft(2, '0')] = clib.TextToDecimal(drow["WGPCSD" + i.ToString().PadLeft(2, '0')].ToString());
									}
									ds.Tables["C_3080"].Rows.Add(srow);
								}
							}

                            DataRow nrow;
							for (int z = 0; z < ds.Tables["C_3080"].Rows.Count; z++)
							{
								DataRow drow = ds.Tables["C_3080"].Rows[z];
								df.GetMSTWGPCDatas(drow["END_YYMM"].ToString(), drow["SAWON_NO"].ToString().Trim(), ds);
								if (ds.Tables["MSTWGPC"].Select("WGPCYYMM = '" + drow["END_YYMM"].ToString() + "' AND WGPCSABN = '" + drow["SAWON_NO"].ToString().Trim() + "'").Length > 0)
								{
									nrow = ds.Tables["MSTWGPC"].Select("WGPCYYMM = '" + drow["END_YYMM"].ToString() + "' AND WGPCSABN = '" + drow["SAWON_NO"].ToString().Trim() + "'")[0];

									nrow["WGPCUPDT"] = gd.GetNow().Substring(0, 8);
									nrow["WGPCPSTY"] = "U";
								}
								else
								{
									nrow = ds.Tables["MSTWGPC"].NewRow();
									nrow["WGPCYYMM"] = drow["END_YYMM"].ToString();
									nrow["WGPCSQNO"] = "1";
									nrow["WGPCSABN"] = drow["SAWON_NO"].ToString().Trim();
									for (int i = 1; i <= 50; i++)
									{
										nrow["WGPCSD" + i.ToString().PadLeft(2, '0')] = 0;
									}
									for (int i = 1; i <= 30; i++)
									{
										nrow["WGPCGJ" + i.ToString().PadLeft(2, '0')] = 0;
									}
									nrow["WGPCINDT"] = gd.GetNow().Substring(0, 8);
									nrow["WGPCUPDT"] = "";
									nrow["WGPCPSTY"] = "A";
									ds.Tables["MSTWGPC"].Rows.Add(nrow);
								}
								nrow["WGPCUSID"] = SilkRoad.Config.SRConfig.USID;
								for (int i = 0; i < grdv_end.Columns.Count; i++)
								{
									if (grdv_end.Columns[i].FieldName.Substring(4, 2) == "SD")
										nrow[grdv_end.Columns[i].FieldName.ToString()] = clib.TextToDecimal(drow[grdv_end.Columns[i].FieldName.ToString()].ToString());
								}

								//근태 넘기기
								DataRow nrow2;
								df.GetMSTGTMMDatas(drow["END_YYMM"].ToString(), drow["SAWON_NO"].ToString().Trim(), ds);
								if (ds.Tables["MSTGTMM"].Select("GTMMYYMM = '" + drow["END_YYMM"].ToString() + "' AND GTMMSABN = '" + drow["SAWON_NO"].ToString().Trim() + "'").Length > 0)
								{
									nrow2 = ds.Tables["MSTGTMM"].Select("GTMMYYMM = '" + drow["END_YYMM"].ToString() + "' AND GTMMSABN = '" + drow["SAWON_NO"].ToString().Trim() + "'")[0];

									nrow2["GTMMUPDT"] = gd.GetNow().Substring(0, 8);
									nrow2["GTMMPSTY"] = "U";
								}
								else
								{
									nrow2 = ds.Tables["MSTGTMM"].NewRow();
									nrow2["GTMMYYMM"] = drow["END_YYMM"].ToString();
									nrow2["GTMMSABN"] = drow["SAWON_NO"].ToString().Trim();

									for (int i = 1; i <= 30; i++)
									{
										nrow2["GTMMGT" + i.ToString().PadLeft(2, '0')] = 0;
									}

									nrow2["GTMMINDT"] = gd.GetNow().Substring(0, 8);
									nrow2["GTMMUPDT"] = "";
									nrow2["GTMMPSTY"] = "A";
									ds.Tables["MSTGTMM"].Rows.Add(nrow2);
								}
								nrow2["GTMMUSID"] = SilkRoad.Config.SRConfig.USID;
                                for (int i = 0; i < grdv_end.Columns.Count; i++)
                                {
                                    if (grdv_end.Columns[i].FieldName.Substring(4, 2) == "GT")
                                        nrow2["GTMM" + grdv_end.Columns[i].FieldName.Substring(4, 4)] = clib.TextToDecimal(drow[grdv_end.Columns[i].FieldName.ToString()].ToString());
                                }

								string[] tableNames = new string[] { "MSTWGPC", "MSTGTMM" };
								SilkRoad.DbCmd_DT02.DbCmd_DT02 cmd = new SilkRoad.DbCmd_DT02.DbCmd_DT02();
								outVal += cmd.setUpdate(ref ds, tableNames, null);
							}
						}
					}
					catch (Exception ec)
					{
						MessageBox.Show(ec.Message, "전송오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					finally
					{
                        if (outVal > 0)
                        {
                            MessageBox.Show("마감내용이 전송되었습니다.", "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SetCancel(2);
                        }
						//Search();
						Cursor = Cursors.Default;
					}
				}
			}
		}
		//급여전송취소(급여변동항목,월근태 삭제)
		private void btn_wage_canc_Click(object sender, EventArgs e)
		{
			//for (int i = 0; i < grdv_end.RowCount; i++)
			//{
			//	if (grdv_end.GetVisibleRowHandle(i) > -1)
			//	{
			//		if (grdv_end.GetDataRow(grdv_end.GetVisibleRowHandle(i))["IMCHTEL1"].ToString().Substring(0, 2) == "01")
			//			grdv_end.GetDataRow(grdv_end.GetVisibleRowHandle(i))["CHK"] = "1";
			//	}
			//}
		}
		//내역조회
		private void btn_s_search_Click(object sender, EventArgs e)
		{
			if (isNoError_um(5))
            {
                info_Search(3);

                string frmm = clib.DateToText(dat_s_frmm.DateTime).Substring(0, 6);
				string tomm = clib.DateToText(dat_s_tomm.DateTime).Substring(0, 6);
				string dept = sl_s_dept.EditValue == null ? "%" : sl_s_dept.EditValue.ToString();
				df.GetSEARCH_3081Datas(frmm, tomm, dept, ds);
				grd_search.DataSource = ds.Tables["SEARCH_3081"];

				if (ds.Tables["SEARCH_3081"].Rows.Count == 0)
					MessageBox.Show("조회된 내역이 없습니다.", "없음", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		//엑셀변환
		private void btn_excel_Click(object sender, EventArgs e)
		{
			clib.gridToExcel(grdv_search, this.Text + "(" + this.Name + ")_" + clib.DateToText(DateTime.Now), true);
		}

		//탭페이지 변경시
		private void srTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			//if (srTabControl1.SelectedTabPageIndex == 0 && ds.Tables["WORK_3080"] != null)
			//{
			//	if (ds.Tables["WORK_3080"].Rows.Count > 0)
			//	{
			//		string yymm = clib.DateToText(dat_p_yymm.DateTime).Substring(0, 6);
			//		df.GetWORK_3080Datas(yymm, cmb_gubn.SelectedIndex.ToString(), ds);
			//		grd1.DataSource = ds.Tables["WORK_3080"];
			//	}
			//}
		}


		#endregion

		#region 3 EVENT
				
        //듀티 최종마감체크
		private void DUTYEND_CHK()
		{
			string yymm = clib.DateToText(dat_yymm.DateTime).Substring(0, 6);

            df.Get2020_SEARCH_ENDSDatas(yymm, ds);
			if (ds.Tables["2020_SEARCH_ENDS"].Rows.Count > 0) //마감월이 저장되어 있으면
			{
				DataRow irow = ds.Tables["2020_SEARCH_ENDS"].Rows[0];
				if (irow["CLOSE_YN"].ToString() == "Y") //마감 일때		
					ends_yn = "1";				
				else
					ends_yn = "";
            }
			else
			{
				ends_yn = "";
			}
        }
        //인사급여 마감체크
        private void WAGEEND_CHK()
        {
            string yymm = clib.DateToText(dat_yymm.DateTime).Substring(0, 6);
            df.Get3080_WAGE_ENDSDatas(yymm, ds);
            if (ds.Tables["3080_WAGE_ENDS"].Rows.Count > 0) //마감월이 저장되어 있으면
            {
                DataRow irow = ds.Tables["3080_WAGE_ENDS"].Rows[0];
                if (irow["YY_CHK"].ToString() == "1") //년마감 일때		
                    wage_ends_yn = "1";
                else //년마감 아닐때 월마감 체크
                    wage_ends_yn = irow["MM_CHK"].ToString();
            }
            else
            {
                wage_ends_yn = "";
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
			if (mode == 1)  //집계처리
			{
                if (clib.DateToText(dat_p_yymm.DateTime) == "")
				{
					MessageBox.Show("근무년월 입력하세요!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					dat_p_yymm.Focus();
					return false;
                }
                else
				{
					isError = true;
				}
			}
			else if (mode == 2) //마감
            {
                DUTYEND_CHK();
                if (ends_yn != "1")
                {
                    string yymm = clib.DateToText(dat_p_yymm.DateTime).Substring(0, 4) + "." + clib.DateToText(dat_p_yymm.DateTime).Substring(4, 2);
                    MessageBox.Show(yymm + "은 최종마감되지 않았습니다!\r\n최종마감 처리 후 작업 가능합니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    isError = true;
                }
            }
			else if (mode == 3)  //급여처리조회
			{
				if (clib.DateToText(dat_yymm.DateTime) == "")
				{
					MessageBox.Show("마감년월을 입력하세요!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					dat_yymm.Focus();
					return false;
				}
				else
				{
					isError = true;
				}
			}
			else if (mode == 4)  //급여처리
			{
				WAGEEND_CHK();
				if (wage_ends_yn == "1")
				{
					string yymm = clib.DateToText(dat_yymm.DateTime).Substring(0, 4) + "." + clib.DateToText(dat_yymm.DateTime).Substring(4, 2);
					MessageBox.Show(yymm+"은 인사급여에서 마감되어 전송할 수 없습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				else
				{
					isError = true;
				}
			}
			else if (mode == 5)  //조회
			{
				if (clib.DateToText(dat_s_frmm.DateTime) == "")
				{
					MessageBox.Show("마감년월(fr)을 입력하세요!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					dat_s_frmm.Focus();
					return false;
				}
				else if (clib.DateToText(dat_s_tomm.DateTime) == "")
				{
					MessageBox.Show("마감년월(to)를 입력하세요!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					dat_s_tomm.Focus();
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

        private void SetButtonEnable(string arr)
        {
            btn_proc.Enabled = arr.Substring(0, 1) == "1" ? true : false;
            btn_canc.Enabled = arr.Substring(1, 1) == "1" ? true : false;
            btn_end.Enabled = arr.Substring(2, 1) == "1" ? true : false;
        }
        private void SetButtonEnable2(string arr)
        {
            btn_search.Enabled = arr.Substring(0, 1) == "1" ? true : false;
            btn_s_del.Enabled = arr.Substring(1, 1) == "1" ? true : false;
            btn_canc2.Enabled = arr.Substring(2, 1) == "1" ? true : false;
            btn_wage.Enabled = arr.Substring(3, 1) == "1" ? true : false;
        }

        #endregion


    }
}

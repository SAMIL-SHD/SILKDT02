using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using System.Text;


namespace DUTY1200
{
    public partial class duty2051 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataFuns.df1000 df = new DataFuns.df1000();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();
        string _Flag = "";
        
        SilkRoad.BaseCode.BaseCode bc = new SilkRoad.BaseCode.BaseCode();

        public duty2051()
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
            _Flag = "";
            
            if (ds.Tables["MSTNURS"] != null)
            {
                ds.Tables["MSTNURS"].Clear();
            }

            grdv.ActiveFilter.Clear();
            grdv.SortInfo.Clear();
            
            grdv.FocusedRowHandle = -1;
            cec.SetClearControls(srPanel9, new string[] { "" });
            //SetButtonEnable("00000");
        }
        #endregion

        #region 1 Form

        private void duty2051_Load(object sender, EventArgs e)
        {
            SetCancel();
            SetBaseData();
           
        }
        #endregion

        //기본 룩업 데이터 설정
        private void SetBaseData()
        {
            df.GetMSTPART_lookupDatas("", ref ds); // 파트 리스트 룩업
            sl_part.Properties.DataSource = ds.Tables["MSTPART_LOOKUP"];
            sl_RNpart.Properties.DataSource = ds.Tables["MSTPART_LOOKUP"];
        }


        #region 2 Button

        //저장버튼
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (isNoError_um(2))
            {
                Cursor = Cursors.WaitCursor;
                int outVal = 0;
                try
                {
                    DataRow hrow;

                    // MSTNURS 테이블에, 해당 직원이 이미 있는지 확인. 없으면 신규. 있으면 수정. 
                   if(grdv.GetFocusedDataRow()["gubn"].ToString().Equals("N"))
                    {
                        _Flag = "C";
                    }

                    if (_Flag == "C")  //신규
                    {
                        hrow = ds.Tables["MSTNURS"].NewRow();
                    }
                    else //수정
                    {
                        hrow = ds.Tables["MSTNURS"].Rows[0];
                      
                            //"REL_TYPE = '" + cmb_profic.SelectedIndex.ToString() + 
                            //"' and  REL_SANO = '" + lp_relsano.EditValue.ToString().Trim() + "'")[0];
                    }
                    
                    hrow["PARTNAME"] = lblTeam.Text.ToString();
                    hrow["SANM"]     = lblName.Text.ToString();
                    hrow["SAWON_NO"] = lblSano.Text.ToString();
                    hrow["PROFICIENCY"] = cmb_profic.SelectedIndex.ToString();
                    hrow["PRECEPTOR"] = sl_name1.EditValue == null ? "" : sl_name1.EditValue.ToString();
                    hrow["DESIG_RN"] = sl_name2.EditValue == null ? "" : sl_name2.EditValue.ToString();
                    //hrow["DESIG_YN"] = cmb_desig_yn.EditValue.ToString();
                    //hrow["DESIG_TP"] = cmb_desig_tp.EditValue.ToString();
                    //hrow["TMSLC_YN"] = cmb_tmslc_yn.EditValue.ToString();
                    //hrow["TMSLC_FR"] = srTimeFrom.EditValue.ToString();
                    //hrow["TMSLC_TO"] = srTimeTo.EditValue.ToString();
                    //hrow["SAMEFRST"] = cmb_same1st.SelectedIndex.ToString(); 
                    //hrow["MAXWK_N"]  = cmb_maxwk_n.SelectedIndex.ToString(); 
                    //hrow["MAXWK_C"]  = cmb_maxwk_c.SelectedIndex.ToString(); 
                    //hrow["ALLOWOFF"] = cmb_allowoff.SelectedIndex.ToString();
                    //hrow["HPNO"]     = txt_hpno.Text.ToString();  
                    //hrow["EMAIL_ID"] = txt_email.Text.ToString();
                    //hrow["MRD_STT"]  = cmb_mrd_stt.SelectedIndex.ToString();
                    //hrow["RNS_DT"]   = srDate_rsn_dt.EditValue.ToString(); 
                    //hrow["CHARGE"]   = cmb_charge.EditValue.ToString();                               
                    hrow["PARTCODE"] = sl_RNpart.EditValue == null ? "" : sl_RNpart.EditValue.ToString();
                    hrow["USID"] = SilkRoad.Config.SRConfig.USID;

                    if (_Flag == "C")  //신규
                    {
                        hrow["INDT"] = gd.GetNow();
                        hrow["UPDT"] = " ";
                        hrow["PSTY"] = "A";

                        ds.Tables["MSTNURS"].Rows.Add(hrow);
                    }
                    else //수정
                    {
                        hrow["UPDT"] = gd.GetNow();
                        hrow["PSTY"] = "U";
                    }

                    string[] UpQry = { "update TRSPART set partcode = '" + hrow["PARTCODE"]  + "' where sawon_no  = '" + hrow["SAWON_NO"] + "'" };


                    string[] tableNames = new string[] { "MSTNURS"};
                    SilkRoad.DbCmd_DUTY.DbCmd_DUTY cmd = new SilkRoad.DbCmd_DUTY.DbCmd_DUTY();
                    outVal = cmd.setUpdate(ds, tableNames, UpQry);

                    if (outVal <= 0)
                    {
                        MessageBox.Show("저장된 내용이 없습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("저장되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ec)
                {
                    MessageBox.Show(ec.Message, "저장오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    //btn_refresh.PerformClick();
                    SetCancel();
                    Cursor = Cursors.Default;
                    btn_search_CK();
                }
            }
        }

        //삭제버튼
        private void btn_del_Click(object sender, EventArgs e)
        {
            
            DialogResult dr = MessageBox.Show("삭제하시겠습니까?", "삭제여부", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                int outVal = 0;
                try
                {
                    DataRow drow = grdv.GetFocusedDataRow();

                    ds.Tables["MSTNURS"].Select("SAWON_NO = '"+drow["SAWON_NO"]+"'")[0].Delete();

                    string[] tableNames = new string[] { "MSTNURS" };
                        SilkRoad.DbCmd_DUTY.DbCmd_DUTY cmd = new SilkRoad.DbCmd_DUTY.DbCmd_DUTY();
                        outVal = cmd.setUpdate(ds, tableNames, null);

                        if (outVal <= 0)
                        {
                            MessageBox.Show("삭제된 내용이 없습니다.", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                }
                catch (Exception ec)
                {
                    MessageBox.Show(ec.Message, "삭제오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    SetCancel();
                    Cursor = Cursors.Default;
                    btn_search_CK();
                }
            }
            
        }

        /// <summary>취소버튼</summary>
        private void btn_clear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("해당 내역을 취소하시겠습니까?", "취소", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                               == DialogResult.OK)
            {
                SetCancel();
            }
        }

        /// <summary>엑셀버튼</summary>
        private void btn_exel_Click(object sender, EventArgs e)
        {
            //clib.gridToExcel(grdv_dt, grdv_hd.GetFocusedDataRow()["NAME"].ToString() + "_" + DateTime.Today.ToShortDateString().Replace("-", "").Replace("/", ""));
        }

        //왼쪽 간호사 리스트 조회
        private void btn_search_Click(object sender, EventArgs e)
        {

            btn_search_CK();
        }

        //처리버튼
        private void btn_proc_Click(object sender, EventArgs e)
        {
            if (isNoError_um(1))
            {
                _Flag = "C";

                lblTeam.Text = sl_part.EditValue.ToString(); // 텍스트 경우 맞음
                lblName.Text = "-";
                lblSano.Text = "-";
                cmb_profic.SelectedIndex = 0;
               ///인사 DB에서 사원번호 호출해서 srLookup_EMBS_Sabun 에 바인딩

                SetButtonEnable("11110");
            }
        }
        #endregion


        #region 3 EVENT

        //팀(파트코드) 선택 시 불러오는 이벤트
        private void sl_part_EditValueChanged(object sender, EventArgs e)
        {
            if (sl_part.EditValue != null)
            {
                cec.SetClearControls(srPanel9, new string[] { "" }); //srPanel9 내 모든항목을 클리어(초기화)
                btn_search_CK();
            }

            else
            {
                MessageBox.Show("팀을 선택하세요.");
            }
        }

        //그리드 행 선택 시 우측에 디테일 조회
        private void grdv_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            DataRow drow = grdv.GetFocusedDataRow();

            // 왼쪽 그리드에서 row 클릭한 정보 오른쪽에 뿌려주는데.. 그 선택 파트코드 안의 간호사들 불러오기. 오른쪽에서 선택하도록.  
            if (sl_part.EditValue != null)
            {
                df.GetMSTNURS_lookupDatas("", sl_part.EditValue.ToString(), ref ds); //간호사이름 리스트
                sl_name1.Properties.DataSource = ds.Tables["MSTNURS_LOOKUP"];
            
                df.GetMSTNURS_lookupDatas("", sl_part.EditValue.ToString(), ref ds); //간호사이름
                sl_name2.Properties.DataSource = ds.Tables["MSTNURS_LOOKUP"];
            }

            lblTeam.Text = drow["PARTNAME"].ToString(); 
            sl_RNpart.EditValue = drow["PARTCODE"].ToString(); 
            lblName.Text = drow["SANM"].ToString();
            lblSano.Text = drow["SAWON_NO"].ToString();
            cmb_profic.SelectedIndex = clib.TextToInt(drow["PROFICIENCY"].ToString());
            sl_name1.EditValue = drow["PRECEPTOR"].ToString();  //grid 선택간호사의 교육담당자 입력.(display 이름, value 사번)
            sl_name2.EditValue = drow["DESIG_RN"].ToString();   //grid 선택간호사의 지정간호사 입력.(display 이름, value 사번)
            //cmb_desig_yn.EditValue = drow["DESIG_YN"].ToString();
            //cmb_desig_tp.EditValue = drow["DESIG_TP"].ToString();
            //cmb_tmslc_yn.EditValue = drow["TMSLC_YN"].ToString();
            //srTimeFrom.EditValue = drow["TMSLC_FR"].ToString();
            //srTimeTo.EditValue = drow["TMSLC_TO"].ToString();
            //cmb_same1st.SelectedIndex  = clib.TextToInt(drow["SAMEFRST"].ToString());
            //cmb_maxwk_n.SelectedIndex  = clib.TextToInt(drow["MAXWK_N"].ToString());
            //cmb_maxwk_c.SelectedIndex  = clib.TextToInt(drow["MAXWK_C"].ToString());
            //cmb_allowoff.SelectedIndex = clib.TextToInt(drow["ALLOWOFF"].ToString()); 
            //txt_hpno.Text   = drow["HPNO"].ToString();     
            //txt_email.Text  = drow["EMAIL_ID"].ToString();
            //cmb_mrd_stt.SelectedIndex = clib.TextToInt(drow["MRD_STT"].ToString()); 
            //srDate_rsn_dt.EditValue = drow["RNS_DT"];
            //cmb_charge.EditValue = drow["CHARGE"].ToString();

            //cmb_rel_type.SelectedIndex = int.Parse(drow["REL_TYPE"].ToString().Substring(1, 1)); //"01" Char(2) 라서 이처럼 써는데 
            //cmb_rel_type.SelectedIndex = int.Parse(drow["REL_TYPE"].ToString()); //"1" 한자리로 줄면서 바꿈. 
            //lp_relsano.EditValue = drow["REL_SANO"];
            //dat_workdd.DateTime = clib.TextToDate(dr["WRITEDATE"].ToString());
            //displayList(grdv_master.GetDataRow(e.RowHandle)["EMBSSABN"].ToString());
            //df.getDocuTotalData(ref ds, grdvMaster.GetFocusedDataRow()["DOCUCODE"].ToString(), txt_작업년월.Text.Replace("-", ""), "");
            //grdDetail.DataSource = ds.Tables["DocuOverTime_search"];
            //grdvDetail.BestFitColumns();

            //cmb_type1.SelectedIndex = dr["TYPE1"].ToString().Equals("A") ? 0 : 1;
            //cmb_type2.Text = dr["TYPE2NAME"].ToString();

            //dat_from1.EditValue = dr["VOCADAY1"].ToString();
            //dat_to1.EditValue = dr["VOCADAY2"].ToString();
            //mm_remark.Text = dr["REMARK"].ToString().Trim();

            //= dr["VOCAKEY"].ToString();
            //df.GetTRSOFFIDate(ref ds, _VocaKey);
            //sl_edu.EditValue = dr["EDU_GUBN"].ToString();
            //// txt_type1name.Text = dr["EDU_GUBN_NAME"].ToString().Trim();
            //dat_to.EditValue = clib.TextToDate(dr["OFFI_TO"].ToString());
            //txt_eduname.Text = dr["EDU_NAME"].ToString().Trim();
            //txt_dest.Text = dr["EDU_DEST"].ToString().Trim();
            //cmb_type3.SelectedIndex = int.Parse(dr["VACC_TYPE"].ToString() == "" ? "0" : dr["VACC_TYPE"].ToString()) - 1;

            //srb_type1.SelectedIndex = int.Parse(dr["SCHO_TYPE"].ToString()) - 1;
            //dat_from.EditValue = clib.TextToDate(dr["OFFI_FROM"].ToString());
            //tb_name.Text = dr["SCHO_NAME"].ToString().Trim();
            //cmb_type3.SelectedIndex = int.Parse(dr["SCHO_AMOUNT"].ToString()) - 1;

            SetButtonEnable("11111");
        }

        private void grdv_KeyDown(object sender, KeyEventArgs e)
        {
           if (e.KeyCode == Keys.Enter)
            {
                DataRow drow = grdv.GetFocusedDataRow();

                if (sl_part.EditValue != null)
                {
                    df.GetMSTNURS_lookupDatas("", sl_part.EditValue.ToString(), ref ds); //RN리스트
                    sl_name1.Properties.DataSource = ds.Tables["MSTNURS_LOOKUP"];

                    df.GetMSTNURS_lookupDatas("", sl_part.EditValue.ToString(), ref ds); //RN리스트
                    sl_name2.Properties.DataSource = ds.Tables["MSTNURS_LOOKUP"];
                }

                lblTeam.Text = drow["PARTNAME"].ToString(); 
                lblName.Text = drow["SANM"].ToString();
                lblSano.Text = drow["SAWON_NO"].ToString();
                cmb_profic.SelectedIndex = int.Parse(drow["PROFICIENCY"].ToString());
                sl_name1.EditValue = drow["PRECEPTOR"].ToString();
                sl_name2.EditValue = drow["DESIG_RN"].ToString(); 
                //cmb_desig_yn.EditValue = drow["DESIG_YN"];
                //cmb_desig_tp.EditValue = drow["DESIG_TP"];
                //cmb_tmslc_yn.EditValue = drow["TMSLC_YN"];
                //srTimeFrom.EditValue = drow["TMSLC_FR"].ToString();
                //srTimeTo.EditValue = drow["TMSLC_TO"].ToString();
                //cmb_same1st.SelectedIndex = int.Parse(drow["SAMEFRST"].ToString());
                //cmb_maxwk_n.SelectedIndex = int.Parse(drow["MAXWK_N"].ToString());
                //cmb_maxwk_c.SelectedIndex = int.Parse(drow["MAXWK_C"].ToString());
                //cmb_allowoff.SelectedIndex = int.Parse(drow["ALLOWOFF"].ToString());
                //txt_hpno.Text = drow["HPNO"].ToString();
                //txt_email.Text = drow["EMAIL_ID"].ToString();
                //cmb_mrd_stt.SelectedIndex = int.Parse(drow["MRD_STT"].ToString());
                //srDate_rsn_dt.EditValue = drow["RNS_DT"];
                //cmb_charge.EditValue = drow["CHARGE"].ToString();
            }
        }

        private void grdv_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (grdv.FocusedRowHandle == e.RowHandle)
                e.Appearance.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
            else
                e.Appearance.BackColor = System.Drawing.Color.Transparent;
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

            if (mode == 1)  //처리 :신규로 등록할때 => 간호사 신규등록은 다른 화면에서 해야할 듯. 여기는 그냥 수정 저장만. 
            {
                if (sl_part.EditValue.ToString().Trim() == "")
                {
                    MessageBox.Show( "팀을 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sl_part.Focus();
                    return false;
                }
                else
                {
                    isError = true;
                }
            }
            else if (mode == 2)  //저장
            {
                if (lblSano.Text == null)
                {
                    MessageBox.Show("직원을 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                //이미 저장된 관계에 관계사원인지 확인
                //else if (ds.Tables["MSTREDT"].Select("REL_TYPE = '" + ("0" + cmb_rel_type.SelectedIndex.ToString()) + "' and  REL_SANO = '" + lp_relsano.EditValue.ToString().Trim() + "'") != null)
                //else if (ds.Tables["MSTNURS"].Select("PARTCODE = '" + sl_part.EditValue.ToString() + "' and  SAWON_NO = '" + lblSano.Text.ToString().Trim() + "'") != null)
                //{
                //    MessageBox.Show("해당 ( 관계+관계사원 ) 이 이미 있습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return false;
                //}
                //else
                //{
                //    isError = true;
                //}
                else { 
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
            btn_proc.Enabled  = arr.Substring(0, 1) == "1" ? true : false;
            btn_save.Enabled  = arr.Substring(1, 1) == "1" ? true : false;
            btn_del.Enabled   = arr.Substring(2, 1) == "1" ? true : false;
            btn_clear.Enabled = arr.Substring(3, 1) == "1" ? true : false;
            btn_exel.Enabled  = arr.Substring(4, 1) == "1" ? true : false;
        }

        private void btn_search_CK()    // 간호사 조회
        {
            string partcd = sl_part.EditValue == null ? "" : sl_part.EditValue.ToString();
            //팀코드, 간호사사번 리스트뿌리기
            df.GetMSTNURSDatas(partcd, "", ref ds);
            grd.DataSource = ds.Tables["MSTNURS"];
            grdv.BestFitColumns();
        }

        #endregion

        #region 예제 그리드 클릭 이벤트 호출 ========================================================================================
        ////그리드클릭시 display.
        //private void displayClickValue(DataRow dr)
        //{
        //    if (ds.Tables["TRSOFFI_SEARCH"].Rows.Count > 0)
        //    {
        //        _VocaKey = dr["VOCAKEY"].ToString();
        //        df.GetTRSOFFIDate(ref ds, _VocaKey);
        //        //                srb_type4.SelectedIndex = int.Parse(dr["SCHO_FILE"].ToString()) - 1;
        //        srb_type1.SelectedIndex = int.Parse(dr["SCHO_TYPE"].ToString()) - 1;
        //        dat_from.EditValue = clib.TextToDate(dr["OFFI_FROM"].ToString());
        //        dat_to.EditValue = clib.TextToDate(dr["OFFI_TO"].ToString());
        //        scho_from.EditValue = clib.TextToDate(dr["SCHO_FROM"].ToString());
        //        scho_to.EditValue = clib.TextToDate(dr["SCHO_TO"].ToString());
        //        tb_name.Text = dr["SCHO_NAME"].ToString().Trim();
        //        tb_dest.Text = dr["SCHO_DEST"].ToString().Trim();
        //        tb_subject.Text = dr["SCHO_SUBJECT"].ToString().Trim();
        //        cmb_type2.SelectedIndex = int.Parse(dr["SCHO_GUBNTYPE"].ToString()) - 1;
        //        cmb_type3.SelectedIndex = int.Parse(dr["SCHO_AMOUNT"].ToString()) - 1;

        //        //첨부파일 list ..
        //        listView_file.Items.Clear();
        //        for (int i = 1; i < 4; i++)
        //        {
        //            if (dr["file" + i] != null && dr["file" + i].ToString() != "")
        //            {
        //                listView_file.Items.Add(dr["file" + i].ToString());
        //            }
        //        }
        //    }
        //    if (dr["STATUS"].ToString().Equals("1") || dr["STATUS"].ToString().Equals("9") || dr["STATUS"].ToString().Equals("4") || dr["STATUS"].ToString().Equals("0") || dr["STATUS"].ToString().Equals("8"))
        //    {
        //        SetButtonEnable("1111");
        //    }
        //    else
        //    {
        //        SetButtonEnable("0010");
        //    }
        //}

        ////그리드 클릭
        //private void grdv1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        //{
        //    if (e.RowHandle < 0)
        //        return;

        //    DataRow dr = grdv1.GetDataRow(e.RowHandle);
        //    displayClickValue(dr);
        //}

        #endregion


    }
}

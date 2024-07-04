using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using System.Data.OleDb;
using System.Collections;

namespace DUTY1000
{
    public partial class duty9050 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();

        public duty9050()
        {
            InitializeComponent();
        }

        #region 0. Initialization

        private void SetCancel(int stat)
		{
			if (stat == 1)
			{
                df.GetSL_SD21Datas(ds);
                df.GetSL_SD22Datas(ds);
                grd1.DataSource = ds.Tables["SL_SD21"];
                grd2.DataSource = ds.Tables["SL_SD22"];

                btn_set.Enabled = true;
                sl_sd_list.EditValue = null;
                srSplitContainer1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;                
            }
			else if (stat == 2)
			{
				if (ds.Tables["SEARCH_INFOSD05"] != null)
					ds.Tables["SEARCH_INFOSD05"].Clear();
				pv_grd.DataSource = null;
            }
            else if (stat == 3)
            {
                if (ds.Tables["S_INFOSD05"] != null)
                    ds.Tables["S_INFOSD05"].Clear();

                grd.DataSource = null;

                dat_sldt.Enabled = true;
                sl_sd_list.Enabled = true;
                btn_save.Text = "저 장";
                btn_save.Image = Properties.Resources.저장;
                SetButtonEnable("10000");
            }
        }

        #endregion

        #region 1 Form

        private void duty9050_Load(object sender, EventArgs e)
        {
            dat_sldt.DateTime = clib.TextToDateLast(clib.DateToText(DateTime.Now.AddMonths(-1)));
            dat_stdt.DateTime = clib.TextToDateLast(clib.DateToText(DateTime.Now.AddMonths(-1)));
        }
		private void duty9050_Shown(object sender, EventArgs e)
		{
			SetCancel(1);
		}

        #endregion

        #region 2 Button

        //수당셋팅처리
        private void btn_set_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            int outVal = 0;
            try
            {
                df.GetDUTY_INFOSD05_SETDatas(ds);
                for (int i = 0; i < ds.Tables["SL_SD21"].Rows.Count; i++)
                {
                    DataRow drow = ds.Tables["SL_SD21"].Rows[i];
                    if (drow["CHK"].ToString() == "1")
                    {
                        DataRow nrow = ds.Tables["DUTY_INFOSD05_SET"].NewRow();
                        nrow["SD_CODE"] = drow["SD_CODE"].ToString();
                        nrow["CHK"] = "1";
                        ds.Tables["DUTY_INFOSD05_SET"].Rows.Add(nrow);
                    }
                }
                for (int i = 0; i < ds.Tables["SL_SD22"].Rows.Count; i++)
                {
                    DataRow drow = ds.Tables["SL_SD22"].Rows[i];
                    if (drow["CHK"].ToString() == "1")
                    {
                        DataRow nrow = ds.Tables["DUTY_INFOSD05_SET"].NewRow();
                        nrow["SD_CODE"] = drow["SD_CODE"].ToString();
                        nrow["CHK"] = "1";
                        ds.Tables["DUTY_INFOSD05_SET"].Rows.Add(nrow);
                    }
                }

                string[] qry = new string[] { "DELETE FROM DUTY_INFOSD05_SET" };

                string[] tableNames = new string[] { "DUTY_INFOSD05_SET", };
                SilkRoad.DbCmd_DT02.DbCmd_DT02 cmd = new SilkRoad.DbCmd_DT02.DbCmd_DT02();
                outVal = cmd.setUpdate(ref ds, tableNames, qry);
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message, "저장오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (outVal > 0)
                {
                    //MessageBox.Show("해당 내용이 저장되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btn_set.Enabled = false;
                    srSplitContainer1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;

                    df.GetSL_SD_LIST2Datas(ds);
                    grd_sd05.DataSource = ds.Tables["SL_SD_LIST2"];
                    sl_sd_list.Properties.DataSource = ds.Tables["SL_SD_LIST2"];

                    SetButtonEnable("10000");
                }
                Cursor = Cursors.Default;
            }
        }

        //처리
        private void btn_proc_Click(object sender, EventArgs e)
		{
			if (isNoError_um(1))
			{
				df.GetS_INFOSD05Datas(clib.DateToText(dat_sldt.DateTime), sl_sd_list.EditValue.ToString(), ds);
                grd.DataSource = ds.Tables["S_INFOSD05"];

                df.GetDUTY_INFOSD05Datas(sl_sd_list.EditValue.ToString(), ds);
                if (ds.Tables["DUTY_INFOSD05"].Rows.Count > 0)
                {
                    btn_save.Text = "수 정";
                    btn_save.Image = Properties.Resources.수정;
                    SetButtonEnable("01111");
                }
                else
                    SetButtonEnable("01011");
                
                dat_sldt.Enabled = false;
                sl_sd_list.Enabled = false;
            }
		}
        //저장
		private void btn_save_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(sl_sd_list.Text.ToString() + " 설정을 저장하시겠습니까?", "저장", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
							== DialogResult.OK)
			{
				Cursor = Cursors.WaitCursor;
				int outVal = 0;
				try
                {
                    df.GetDUTY_INFOSD05Datas(sl_sd_list.EditValue.ToString(), ds);
                    for (int i = 0; i < ds.Tables["S_INFOSD05"].Rows.Count; i++)
                    {
                        DataRow drow = ds.Tables["S_INFOSD05"].Rows[i];
                        if (ds.Tables["DUTY_INFOSD05"].Select("SD_CODE = '" + sl_sd_list.EditValue.ToString() + "' AND SABN = '" + drow["SABN"].ToString() + "'").Length > 0)
                        {
                            DataRow nrow = ds.Tables["DUTY_INFOSD05"].Select("SABN = '" + drow["SABN"].ToString() + "'")[0];
                            nrow["SD_BASE"] = clib.TextToDecimal(drow["SD_BASE"].ToString());
                            nrow["SD_ADD"] = clib.TextToDecimal(drow["SD_ADD"].ToString());
                            nrow["REG_DT"] = gd.GetNow();
                            nrow["REG_ID"] = SilkRoad.Config.SRConfig.USID;
                        }
                        else
                        {
                            DataRow nrow = ds.Tables["DUTY_INFOSD05"].NewRow();
                            nrow["SD_CODE"] = sl_sd_list.EditValue.ToString();
                            nrow["SABN"] = drow["SABN"].ToString().Trim();
                            nrow["SABN_NM"] = drow["SABN_NM"].ToString().Trim();
                            nrow["SD_BASE"] = clib.TextToDecimal(drow["SD_BASE"].ToString());
                            nrow["SD_ADD"] = clib.TextToDecimal(drow["SD_ADD"].ToString());
                            nrow["REG_DT"] = gd.GetNow();
                            nrow["REG_ID"] = SilkRoad.Config.SRConfig.USID;
                            ds.Tables["DUTY_INFOSD05"].Rows.Add(nrow);
                        }
                    }

                    string[] tableNames = new string[] { "DUTY_INFOSD05" };
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
                        MessageBox.Show(sl_sd_list.Text.ToString() + "이 저장되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SetCancel(3);
                    }
					Cursor = Cursors.Default;
				}
			}
		}
		//삭제
		private void btn_del_Click(object sender, EventArgs e)
		{
            df.GetDUTY_INFOSD05Datas(sl_sd_list.EditValue.ToString(), ds);
            if (ds.Tables["DUTY_INFOSD05"].Rows.Count > 0)
            {
                DialogResult dr = MessageBox.Show(sl_sd_list.Text.ToString() + "을 모두 삭제하시겠습니까?", "삭제여부", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    int outVal = 0;
                    try
                    {
                        for (int i = 0; i < ds.Tables["DUTY_INFOSD05"].Rows.Count; i++)
                        {
                            ds.Tables["DUTY_INFOSD05"].Rows[i].Delete();
                        }

                        string[] tableNames = new string[] { "DUTY_INFOSD05" };
                        SilkRoad.DbCmd_DT02.DbCmd_DT02 cmd = new SilkRoad.DbCmd_DT02.DbCmd_DT02();
                        outVal = cmd.setUpdate(ref ds, tableNames, null);
                    }
                    catch (Exception ec)
                    {
                        MessageBox.Show(ec.Message, "삭제오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if (outVal > 0)
                        {
                            MessageBox.Show(sl_sd_list.Text.ToString() + "이 삭제되었습니다.", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SetCancel(3);
                        }
                        Cursor = Cursors.Default;
                    }
                }
            }
            else
            {
                MessageBox.Show("삭제할 내역이 없습니다.", "삭제오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
		//취소
		private void btn_canc_Click(object sender, EventArgs e)
		{
			SetCancel(3);
		}

		//금액적용
		private void btn_adp_Click(object sender, EventArgs e)
		{
			if (ds.Tables["S_INFOSD05"] != null)
			{
				for (int i = 0; i < grdv1.RowCount; i++)
				{
					if (grdv1.GetVisibleRowHandle(i) > -1)
					{
						//if (cmb_btype.SelectedIndex == 0)
						//	grdv1.GetDataRow(grdv1.GetVisibleRowHandle(i))["MINUS_NAMT"] = clib.TextToDecimal(txt_slam.Text.ToString());
						//else
						//	grdv1.GetDataRow(grdv1.GetVisibleRowHandle(i))["PLUS_NAMT"] = clib.TextToDecimal(txt_slam.Text.ToString());
					}
				}
			}
		}
		//엑셀변환
		private void btn_excel_Click(object sender, EventArgs e)
		{
            clib.gridToExcel(grdv, "만근수당관리_" + clib.DateToText(DateTime.Now), true);
		}
		
		//엑셀업로드
		private void btn_e_up_Click(object sender, EventArgs e)
		{
			#region 엑셀 읽어오기
			System.Data.DataTable dt = null;
			System.Windows.Forms.OpenFileDialog fd = new System.Windows.Forms.OpenFileDialog();
			fd.DefaultExt = "xls | xlsx";
			fd.Filter = "Excel files (*.xls)|*.xls|Excel Files (*.xlsx)|*.xlsx";
			if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				try
				{
					OleDbConnection oledbCn = null;
					OleDbDataAdapter da = null;

					try
					{
						string type = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source={0};Extended Properties='Excel 12.0;HDR=YES'";
						oledbCn = new OleDbConnection(string.Format(type, fd.FileName));
						oledbCn.Open();

						//첫번째 시트 무조건 가지고 오기
						System.Data.DataTable worksheets = oledbCn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
						da = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}]", worksheets.Rows[0]["TABLE_NAME"]), oledbCn);

						dt = new System.Data.DataTable();
						da.Fill(dt);
					}
					catch (Exception ex)
					{
						System.Windows.Forms.MessageBox.Show("ReadExcel Err:" + ex.Message);
					}
					finally
					{
						if (da != null)
							da.Dispose();
						if (oledbCn != null)
						{
							if (oledbCn.State != ConnectionState.Closed)
								oledbCn.Close();
							oledbCn.Dispose();
						}
					}
				}
				catch (Exception ex)
				{
					System.Windows.Forms.MessageBox.Show("파일을 읽을 수 없습니다. " + ex.Message);
				}
				finally
				{
					fd.Dispose();
				}
			}

			if (dt == null)
				return;
			#endregion

			if (dt.Columns[1].ToString() != "사번")
			{
				MessageBox.Show("엑셀형식이 바르지 않습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			Cursor = Cursors.WaitCursor;
			grd1.DataSource = null;
			int outVal = 0;
			//DataRow nrow;

			foreach (DataRow drow in dt.Rows)
			{
				if (drow[1].ToString().Trim() != "")
				{
					if (ds.Tables["S_INFOSD05"].Select("SABN = '" + drow[1].ToString() + "'").Length > 0)
					{
						DataRow nrow = ds.Tables["S_INFOSD05"].Select("SABN = '" + drow[1].ToString() + "'")[0];
                        nrow["SD_BASE"] = clib.TextToDecimal(drow[3].ToString());
                        nrow["SD_ADD"] = clib.TextToDecimal(drow[4].ToString());
                        outVal++;
                    }
				}
			}
			if (outVal > 0)			
				MessageBox.Show("엑셀업로드가 완료되었습니다.", "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
			
			grd1.DataSource = ds.Tables["S_INFOSD05"];
			Cursor = Cursors.Default;
		}
		

		//건별수당조회
		private void btn_search_Click(object sender, EventArgs e)
		{
			if (isNoError_um(2))
			{
				df.GetSEARCH_INFOSD05Datas(clib.DateToText(dat_stdt.DateTime), ds);
				pv_grd.DataSource = ds.Tables["SEARCH_INFOSD05"];
			}
		}
		
		private void btn_s_canc_Click(object sender, EventArgs e)
		{
			SetCancel(2);
		}

		private void btn_s_excel_Click(object sender, EventArgs e)
		{
			clib.gridToExcel(pv_grd, "만근수당조회_"  + clib.DateToText(DateTime.Now), true);
		}

        #endregion

        #region 3 EVENT

        private void duty9040_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //취소
            {
                SetCancel(srTabControl1.SelectedTabPageIndex + 1);
                //btn_exit.PerformClick();
            }
        }
        private void srTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            btn_set.Visible = srTabControl1.SelectedTabPageIndex == 0 ? true : false;
        }

        private void grdv1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (grdv1.ActiveEditor != null)
                grdv1.CloseEditor();
        }
        private void grdv2_MouseWheel(object sender, MouseEventArgs e)
        {
            if (grdv2.ActiveEditor != null)
                grdv2.CloseEditor();
        }

        private void grdv_sd05_DoubleClick(object sender, EventArgs e)
        {
            DataRow drow = grdv_sd05.GetFocusedDataRow();
            if (drow == null)
                return;

            sl_sd_list.EditValue = drow["SD_CODE"].ToString();
            if (btn_proc.Enabled == false)            
                SetCancel(3);
            
            btn_proc.PerformClick();
        }
        private void pv_grd_CustomFieldSort(object sender, DevExpress.XtraPivotGrid.PivotGridCustomFieldSortEventArgs e)
		{
			//if (e.Field == pivotGridField1)
   //         {
   //             object value1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "JONGCODE");
   //             object value2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "JONGCODE");
   //             int result = Comparer.Default.Compare(value1, value2);
   //             e.Result = result;
   //             e.Handled = true;
   //         }
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
            bool isError = true;
            if (mode == 1)  //처리
            {
                if (clib.DateToText(dat_sldt.DateTime) == "")
                {
                    MessageBox.Show("기준일자를 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dat_sldt.Focus();
                    return false;
                }
                else if (sl_sd_list.EditValue == null)
                {
                    MessageBox.Show("수당을 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sl_sd_list.Focus();
                    return false;
                }
                else
                {
                    isError = true;
                }
            }
            else if (mode == 2) //조회
            {
                if (clib.DateToText(dat_stdt.DateTime) == "")
                {
                    MessageBox.Show("기준일자를 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dat_stdt.Focus();
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
			btn_save.Enabled = arr.Substring(1, 1) == "1" ? true : false;
			btn_del.Enabled = arr.Substring(2, 1) == "1" ? true : false;
			btn_canc.Enabled = arr.Substring(3, 1) == "1" ? true : false;
			btn_e_up.Enabled = arr.Substring(4, 1) == "1" ? true : false;
		}


        #endregion

    }
}

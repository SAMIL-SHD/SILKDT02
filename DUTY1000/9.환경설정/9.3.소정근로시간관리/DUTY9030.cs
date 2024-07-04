using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using System.Data.OleDb;
using System.Collections;

namespace DUTY1000
{
    public partial class duty9030 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();

        public duty9030()
        {
            InitializeComponent();
        }

        #region 0. Initialization

        private void SetCancel(int stat)
		{
			if (stat == 1)
			{
				if (ds.Tables["S_INFOSD03"] != null)
					ds.Tables["S_INFOSD03"].Clear();
				grd.DataSource = null;
				SetButtonEnable("10000");
			}
			else if (stat == 2)
			{
				if (ds.Tables["SEARCH_INFOSD03"] != null)
					ds.Tables["SEARCH_INFOSD03"].Clear();
				pv_grd.DataSource = null;
            }
            btn_save.Text = "저 장";
            btn_save.Image = Properties.Resources.저장;
        }

        #endregion

        #region 1 Form

        private void duty9030_Load(object sender, EventArgs e)
        {
            dat_sldt.DateTime = DateTime.Now;
        }
		private void duty9030_Shown(object sender, EventArgs e)
		{
			SetCancel(1);
		}

        #endregion

        #region 2 Button
		
		//처리
		private void btn_proc_Click(object sender, EventArgs e)
		{
			if (isNoError_um(1))
			{
				df.GetS_INFOSD03Datas(clib.DateToText(dat_sldt.DateTime), ds);
				grd.DataSource = ds.Tables["S_INFOSD03"];

                df.GetDUTY_INFOSD03Datas(ds);
                if (ds.Tables["DUTY_INFOSD03"].Rows.Count > 0)
                {
                    btn_save.Text = "수 정";
                    btn_save.Image = Properties.Resources.수정;
                    SetButtonEnable("01111");
                }
                else
				    SetButtonEnable("01011");
			}
		}
        //저장
		private void btn_save_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("근로시간 내역을 저장하시겠습니까?", "저장", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
							== DialogResult.OK)
			{
				Cursor = Cursors.WaitCursor;
				int outVal = 0;
				try
				{
					df.GetDUTY_INFOSD03Datas(ds);
					for (int i = 0; i < ds.Tables["S_INFOSD03"].Rows.Count; i++)
					{
						DataRow drow = ds.Tables["S_INFOSD03"].Rows[i];
						if (ds.Tables["DUTY_INFOSD03"].Select("SABN = '" + drow["SABN"].ToString() + "'").Length > 0)
						{
							DataRow nrow = ds.Tables["DUTY_INFOSD03"].Select("SABN = '" + drow["SABN"].ToString() + "'")[0];
                            nrow["WT_DAY"] = clib.TextToDecimal(drow["WT_DAY"].ToString());
                            nrow["WT_WEEK"] = clib.TextToDecimal(drow["WT_WEEK"].ToString());
                            nrow["WT_MON"] = clib.TextToDecimal(drow["WT_MON"].ToString());
                            nrow["REG_DT"] = gd.GetNow();
							nrow["REG_ID"] = SilkRoad.Config.SRConfig.USID;
						}
						else
						{
							DataRow nrow = ds.Tables["DUTY_INFOSD03"].NewRow();
							nrow["SABN"] = drow["SABN"].ToString().Trim();
                            nrow["SABN_NM"] = drow["SABN_NM"].ToString().Trim();
                            nrow["WT_DAY"] = clib.TextToDecimal(drow["WT_DAY"].ToString());
							nrow["WT_WEEK"] = clib.TextToDecimal(drow["WT_WEEK"].ToString());
                            nrow["WT_MON"] = clib.TextToDecimal(drow["WT_MON"].ToString());
                            nrow["REG_DT"] = gd.GetNow();
							nrow["REG_ID"] = SilkRoad.Config.SRConfig.USID;
							ds.Tables["DUTY_INFOSD03"].Rows.Add(nrow);
						}
					}

					string[] tableNames = new string[] { "DUTY_INFOSD03" };
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
                        MessageBox.Show("해당 내용이 저장되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SetCancel(1);
                    }
					Cursor = Cursors.Default;
				}
			}
		}
		//삭제
		private void btn_del_Click(object sender, EventArgs e)
		{
			df.GetDUTY_INFOSD03Datas(ds);
			if (ds.Tables["DUTY_INFOSD03"].Rows.Count > 0)
			{
				DialogResult dr = MessageBox.Show("근로시간 내역을 모두 삭제하시겠습니까?", "삭제여부", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
				if (dr == DialogResult.OK)
				{
					int outVal = 0;
					try
					{
						for (int i = 0; i < ds.Tables["DUTY_INFOSD03"].Rows.Count; i++)
						{
							ds.Tables["DUTY_INFOSD03"].Rows[i].Delete();
						}

						string[] tableNames = new string[] { "DUTY_INFOSD03" };
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
							MessageBox.Show("근로시간 내역이 삭제되었습니다.", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);

						SetCancel(1);
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
			SetCancel(1);
		}

		//엑셀변환
		private void btn_excel_Click(object sender, EventArgs e)
		{
            clib.gridToExcel(grdv, "근무시간관리_" + clib.DateToText(DateTime.Now), true);
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
			grd.DataSource = null;
			int outVal = 0;
			//DataRow nrow;

			foreach (DataRow drow in dt.Rows)
			{
				if (drow[1].ToString().Trim() != "")
				{
					if (ds.Tables["S_INFOSD03"].Select("SABN = '" + drow[1].ToString().Trim() + "'").Length > 0)
					{
						DataRow nrow = ds.Tables["S_INFOSD03"].Select("SABN = '" + drow[1].ToString().Trim() + "'")[0];
                        nrow["WT_DAY"] = clib.TextToDecimal(drow[3].ToString());
                        nrow["WT_WEEK"] = clib.TextToDecimal(drow[4].ToString());
                        nrow["WT_MON"] = clib.TextToDecimal(drow[5].ToString());
                        outVal++;
                    }
				}
			}
			if (outVal > 0)			
				MessageBox.Show("엑셀업로드가 완료되었습니다.", "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
			
			grd.DataSource = ds.Tables["S_INFOSD03"];
			Cursor = Cursors.Default;
		}
		
		//근로시간조회
		private void btn_search_Click(object sender, EventArgs e)
		{
			df.GetSEARCH_INFOSD03Datas(ds);
			pv_grd.DataSource = ds.Tables["SEARCH_INFOSD03"];			
		}
		
		private void btn_s_canc_Click(object sender, EventArgs e)
		{
			SetCancel(2);
		}

		private void btn_s_excel_Click(object sender, EventArgs e)
		{
			clib.gridToExcel(pv_grd, "근무시간조회_" + clib.DateToText(DateTime.Now), true);
		}

        #endregion

        #region 3 EVENT

        private void duty9030_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //종료
            {
                btn_exit.PerformClick();
            }
        }

        private void srTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            btn_search.Visible = srTabControl1.SelectedTabPageIndex == 0 ? false : true;
            btn_s_canc.Visible = srTabControl1.SelectedTabPageIndex == 0 ? false : true;
            btn_s_excel.Visible = srTabControl1.SelectedTabPageIndex == 0 ? false : true;
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

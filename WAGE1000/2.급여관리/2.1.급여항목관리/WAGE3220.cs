using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using System.Data.OleDb;
using System.Collections;

namespace WAGE1000
{
    public partial class wage3220 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();

        public wage3220()
        {
            InitializeComponent();
        }

        #region 0. Initialization

        private void SetCancel(int stat)
		{
			//df.GetSEARCH_DEPTDatas(ds);
			//sl_dept.Properties.DataSource = ds.Tables["SEARCH_DEPT"];

			
			df.GetWAGE_DEPRDatas(ds);
			sl_dept.Properties.DataSource = ds.Tables["WAGE_DEPR"];

			if (stat == 1)
			{
				if (ds.Tables["WGFX_LIST"] != null)
					ds.Tables["WGFX_LIST"].Clear();
				grd1.DataSource = null;
				SetButtonEnable("10000");
			}
			
			df.GetFIX_SDDatas(ds);				
			for (int i = 0; i < ds.Tables["FIX_SD"].Rows.Count; i++)
			{
				DataRow drow = ds.Tables["FIX_SD"].Rows[i];
				string sdnm = drow["IFWGCODE"].ToString().Substring(0, 1) == "A" ? "WGFXSD" + drow["IFWGCODE"].ToString().Substring(2, 2) : "WGFXGJ" + drow["IFWGCODE"].ToString().Substring(2, 2);
				grdv1.Columns[sdnm].Visible = true;
				grdv1.Columns[sdnm].Caption = drow["IFWGNAME"].ToString().Trim();
			}
		}

        #endregion

        #region 1 Form

        private void wage3220_Load(object sender, EventArgs e)
        {
			sl_dept.EditValue = null;
			dat_tsdt.DateTime = clib.TextToDateLast(clib.DateToText(DateTime.Now.AddMonths(-1)));
        }
		private void wage3220_Shown(object sender, EventArgs e)
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
				string dept = sl_dept.EditValue == null ? "%" : sl_dept.EditValue.ToString();
				df.GetWGFX_LISTDatas(dept, clib.DateToText(dat_tsdt.DateTime), ds);
				grd1.DataSource = ds.Tables["WGFX_LIST"];

				SetButtonEnable("11111");
			}
		}
        //저장
		private void btn_save_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("해당 고정항목내역을 저장하시겠습니까?", "저장", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
							== DialogResult.OK)
			{
				Cursor = Cursors.WaitCursor;
				int outVal = 0;
				try
				{
					for (int i = 0; i < ds.Tables["WGFX_LIST"].Rows.Count; i++)
					{
						DataRow drow = ds.Tables["WGFX_LIST"].Rows[i];
						df.GetMSTWGFXDatas(drow["SABN"].ToString(), ds);
						if (ds.Tables["MSTWGFX"].Rows.Count > 0)
						{
							DataRow nrow = ds.Tables["MSTWGFX"].Rows[0];
							for (int a = 1; a <= 50; a++)
							{
								nrow["WGFXSD" + a.ToString().PadLeft(2, '0')] = clib.TextToDecimal(drow["WGFXSD" + a.ToString().PadLeft(2, '0')].ToString());
							}
							for (int b = 1; b <= 30; b++)
							{
								nrow["WGFXGJ" + b.ToString().PadLeft(2, '0')] = clib.TextToDecimal(drow["WGFXGJ" + b.ToString().PadLeft(2, '0')].ToString());
							}
							nrow["WGFXUPDT"] = gd.GetNow().Substring(0, 8);
							nrow["WGFXUSID"] = SilkRoad.Config.SRConfig.USID;
							nrow["WGFXPSTY"] = "U";
						}
						else
						{
							DataRow nrow = ds.Tables["MSTWGFX"].NewRow();
							nrow["WGFXSABN"] = drow["SABN"].ToString();
							for (int a = 1; a <= 50; a++)
							{
								nrow["WGFXSD" + a.ToString().PadLeft(2, '0')] = clib.TextToDecimal(drow["WGFXSD" + a.ToString().PadLeft(2, '0')].ToString());
							}
							for (int b = 1; b <= 30; b++)
							{
								nrow["WGFXGJ" + b.ToString().PadLeft(2, '0')] = clib.TextToDecimal(drow["WGFXGJ" + b.ToString().PadLeft(2, '0')].ToString());
							}
							nrow["WGFXINDT"] = gd.GetNow().Substring(0, 8);
							nrow["WGFXUPDT"] = "";
							nrow["WGFXUSID"] = SilkRoad.Config.SRConfig.USID;
							nrow["WGFXPSTY"] = "A";
							ds.Tables["MSTWGFX"].Rows.Add(nrow);
						}
						string[] tableNames = new string[] { "MSTWGFX", };
						SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
						outVal += cmd.setUpdate(ref ds, tableNames, null);
					}

					//string[] tableNames = new string[] { "MSTWGFX", };
					//SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
					//outVal = cmd.setUpdate(ref ds, tableNames, null);
				}
				catch (Exception ec)
				{
					MessageBox.Show(ec.Message, "저장오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				finally
				{
					if (outVal > 0)
						MessageBox.Show("해당 내용이 저장되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
					Cursor = Cursors.Default;
				}
			}
		}
		//삭제
		private void btn_del_Click(object sender, EventArgs e)
		{
			if (ds.Tables["WGFX_LIST"].Rows.Count > 0)
			{
				DialogResult dr = MessageBox.Show("고정항목 내역을 모두 삭제하시겠습니까?", "삭제여부", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
				if (dr == DialogResult.OK)
				{
					int outVal = 0;
					try
					{
						for (int i = 0; i < ds.Tables["WGFX_LIST"].Rows.Count; i++)
						{
							df.GetMSTWGFXDatas(ds.Tables["WGFX_LIST"].Rows[i]["SABN"].ToString(), ds);

							if (ds.Tables["MSTWGFX"].Rows.Count > 0)
							{
								ds.Tables["MSTWGFX"].Rows[0].Delete();

								string[] tableNames = new string[] { "MSTWGFX" };
								SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
								outVal += cmd.setUpdate(ref ds, tableNames, null);
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
							MessageBox.Show("고정항목이 삭제되었습니다.", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            clib.gridToExcel(grdv1, "고정항목관리_" + clib.DateToText(DateTime.Now), true);
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

			if (dt.Columns[0].ToString() != "사번")
			{
				MessageBox.Show("엑셀형식이 바르지 않습니다.\r\n첫번째 열의 타이틀은 사번으로 작성해야합니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			Cursor = Cursors.WaitCursor;
			grd1.DataSource = null;
			int outVal = 0;

			int t_cnt = dt.Columns.Count;
			foreach (DataRow drow in dt.Rows)
			{
				if (drow[0].ToString().Trim() != "" && drow[2].ToString().Trim() != "")
				{
					if (ds.Tables["WGFX_LIST"].Select("SABN = '" + drow[0].ToString() + "'").Length > 0)
					{
						DataRow nrow = ds.Tables["WGFX_LIST"].Select("SABN = '" + drow[0].ToString() + "'")[0];

						for (int i = 3; i < t_cnt; i++)
						{
							string f_nm = grdv1.VisibleColumns[i].FieldName.ToString();
							nrow[f_nm] = clib.TextToDecimal(drow[i].ToString());
						}
						outVal++;
					}
				}
			}
			if (outVal > 0)			
				MessageBox.Show("엑셀업로드가 완료되었습니다.", "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
			
			grd1.DataSource = ds.Tables["WGFX_LIST"];
			Cursor = Cursors.Default;
		}

        #endregion

        #region 3 EVENT

        private void duty9010_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //종료
            {
                btn_exit.PerformClick();
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
            bool isError = true;

            return isError;
        }
        
        #endregion

		#region 9. ETC
		
		private void SetButtonEnable(string arr)
		{
			btn_proc.Enabled = arr.Substring(0, 1) == "1" ? true : false;
			btn_save.Enabled = arr.Substring(1, 1) == "1" ? true : false;
			btn_del.Enabled = arr.Substring(2, 1) == "1" ? true : false;
			btn_excel.Enabled = arr.Substring(3, 1) == "1" ? true : false;
			btn_e_up.Enabled = arr.Substring(4, 1) == "1" ? true : false;
		}


		#endregion
	}
}

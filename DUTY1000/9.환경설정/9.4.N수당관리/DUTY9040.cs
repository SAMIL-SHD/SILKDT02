﻿using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using System.Data.OleDb;
using System.Collections;

namespace DUTY1000
{
    public partial class duty9040 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();

        public duty9040()
        {
            InitializeComponent();
        }

        #region 0. Initialization

        private void SetCancel(int stat)
		{
			if (stat == 1)
			{
				if (ds.Tables["S_INFOSD05"] != null)
					ds.Tables["S_INFOSD05"].Clear();
				grd.DataSource = null;
				SetButtonEnable("100000");
			}
			else if (stat == 2)
			{
				if (ds.Tables["SEARCH_INFOSD05"] != null)
					ds.Tables["SEARCH_INFOSD05"].Clear();
				pv_grd.DataSource = null;
			}
		}

        #endregion

        #region 1 Form

        private void duty9040_Load(object sender, EventArgs e)
        {
			dat_yymm.DateTime = DateTime.Now.AddMonths(-1);
			dat_frmm.DateTime = clib.TextToDate(clib.DateToText(DateTime.Now).Substring(0, 4) + "0101");
			dat_tomm.DateTime = DateTime.Now;
        }
		private void duty9040_Shown(object sender, EventArgs e)
		{
			SetCancel(1);
			//df.GetSEARCH_INFO2Datas(ds);
			//grd1.DataSource = ds.Tables["SEARCH_INFO2"];

			//df.GetSL_GNMUDatas(ds);
			//grd_sl_gnmu.DataSource = ds.Tables["SL_GNMU"];
			//grd_sl_gt.DataSource = ds.Tables["SL_GT"];
		}

        #endregion

        #region 2 Button
		
		//처리
		private void btn_proc_Click(object sender, EventArgs e)
		{
			if (isNoError_um(1))
			{
				df.GetS_INFOSD05Datas(clib.DateToText(dat_yymm.DateTime), ds);
				grd.DataSource = ds.Tables["S_INFOSD05"];
				
				SetButtonEnable("111111");
			}
		}
        //저장
		private void btn_save_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("해당 시급내역을 저장하시겠습니까?", "저장", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
							== DialogResult.OK)
			{
				Cursor = Cursors.WaitCursor;
				int outVal = 0;
				try
				{
					df.GetDUTY_INFOSD05Datas(clib.DateToText(dat_yymm.DateTime).Substring(0, 6), ds);
					for (int i = 0; i < ds.Tables["S_INFOSD05"].Rows.Count; i++)
					{
						DataRow drow = ds.Tables["S_INFOSD05"].Rows[i];
						if (ds.Tables["DUTY_INFOSD05"].Select("YYMM = '" + drow["YYMM"].ToString() + "' AND SABN = '" + drow["SABN"].ToString() + "'").Length > 0)
						{
							DataRow nrow = ds.Tables["DUTY_INFOSD05"].Select("YYMM = '" + drow["YYMM"].ToString() + "' AND SABN = '" + drow["SABN"].ToString() + "'")[0];
							nrow["SABN_NM"] = drow["SABN_NM"].ToString();
							nrow["MINUS_NAMT"] = clib.TextToDecimal(drow["MINUS_NAMT"].ToString());
							nrow["PLUS_NAMT"] = clib.TextToDecimal(drow["PLUS_NAMT"].ToString());
							nrow["REG_DT"] = gd.GetNow();
							nrow["REG_ID"] = SilkRoad.Config.SRConfig.USID;
						}
						else
						{
							DataRow nrow = ds.Tables["DUTY_INFOSD05"].NewRow();
							nrow["YYMM"] = clib.DateToText(dat_yymm.DateTime);
							nrow["SABN"] = drow["SABN"].ToString();
							nrow["SABN_NM"] = drow["SABN_NM"].ToString();
							nrow["MINUS_NAMT"] = clib.TextToDecimal(drow["MINUS_NAMT"].ToString());
							nrow["PLUS_NAMT"] = clib.TextToDecimal(drow["PLUS_NAMT"].ToString());
							nrow["REG_DT"] = gd.GetNow();
							nrow["REG_ID"] = SilkRoad.Config.SRConfig.USID;
							ds.Tables["DUTY_INFOSD05"].Rows.Add(nrow);
						}
					}

					string[] tableNames = new string[] { "DUTY_INFOSD05", };
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
						MessageBox.Show("해당 내용이 저장되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
					Cursor = Cursors.Default;
				}
			}
		}
		//삭제
		private void btn_del_Click(object sender, EventArgs e)
		{
			string yymm = clib.DateToText(dat_yymm.DateTime).Substring(0, 4) + "." + clib.DateToText(dat_yymm.DateTime).Substring(4, 2);
			df.GetDUTY_INFOSD05Datas(clib.DateToText(dat_yymm.DateTime).Substring(0, 6), ds);
			if (ds.Tables["DUTY_INFOSD05"].Rows.Count > 0)
			{
				DialogResult dr = MessageBox.Show(yymm + "월의 N수당을 모두 삭제하시겠습니까?", "삭제여부", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
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
							MessageBox.Show(yymm + "월의 N수당이 삭제되었습니다.", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);

						SetCancel(1);
						Cursor = Cursors.Default;
					}
				}
			}
			else
			{
                MessageBox.Show(yymm+"월은 삭제할 내역이 없습니다.", "삭제오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//취소
		private void btn_canc_Click(object sender, EventArgs e)
		{
			SetCancel(1);
		}
		//전월가져오기
		private void btn_bring_Click(object sender, EventArgs e)
		{
			df.GetBF_INFOSD05Datas(clib.DateToText(dat_yymm.DateTime.AddMonths(-1)), ds);
			if (ds.Tables["BF_INFOSD05"].Rows.Count > 0)
			{
				Cursor = Cursors.WaitCursor;
				for (int i = 0; i < ds.Tables["BF_INFOSD05"].Rows.Count; i++)
				{
					DataRow drow = ds.Tables["BF_INFOSD05"].Rows[i];
					if (ds.Tables["S_INFOSD05"].Select("SABN = '" + drow["SABN"].ToString() + "'").Length > 0)
					{
						ds.Tables["S_INFOSD05"].Select("SABN = '" + drow["SABN"].ToString() + "'")[0]["MINUS_NAMT"] = clib.TextToDecimal(drow["MINUS_NAMT"].ToString());
						ds.Tables["S_INFOSD05"].Select("SABN = '" + drow["SABN"].ToString() + "'")[0]["PLUS_NAMT"] = clib.TextToDecimal(drow["PLUS_NAMT"].ToString());
					}
				}
				Cursor = Cursors.Default;
				MessageBox.Show("전월 N수당 불러오기가 완료 되었습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
				MessageBox.Show("전월 데이터가 없습니다.", "실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		//금액적용
		private void btn_adp_Click(object sender, EventArgs e)
		{
			if (ds.Tables["S_INFOSD05"] != null)
			{
				for (int i = 0; i < grdv.RowCount; i++)
				{
					if (grdv.GetVisibleRowHandle(i) > -1)
					{
						if (cmb_btype.SelectedIndex == 0)
							grdv.GetDataRow(grdv.GetVisibleRowHandle(i))["MINUS_NAMT"] = clib.TextToDecimal(txt_slam.Text.ToString());
						else
							grdv.GetDataRow(grdv.GetVisibleRowHandle(i))["PLUS_NAMT"] = clib.TextToDecimal(txt_slam.Text.ToString());
					}
				}
			}
			//MINUS_NAMT
		}
		//엑셀변환
		private void btn_excel_Click(object sender, EventArgs e)
		{
            clib.gridToExcel(grdv, "N수당관리_" + clib.DateToText(DateTime.Now), true);
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

			if (dt.Columns[0].ToString() != "년월")
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
				if (drow[0].ToString().Trim() != "" && drow[2].ToString().Trim() != "")
				{
					if (ds.Tables["S_INFOSD05"].Select("SABN = '" + drow[2].ToString() + "'").Length > 0)
					{
						DataRow nrow = ds.Tables["S_INFOSD05"].Select("SABN = '" + drow[2].ToString() + "'")[0];						
						nrow["MINUS_NAMT"] = clib.TextToDecimal(drow[4].ToString());
						nrow["PLUS_NAMT"] = clib.TextToDecimal(drow[5].ToString());
					}
					else
					{
						DataRow nrow = ds.Tables["S_INFOSD05"].NewRow();
						nrow["YYMM"] = clib.DateToText(dat_yymm.DateTime).Substring(0, 6);
						nrow["YYMM_NM"] = clib.DateToText(dat_yymm.DateTime).Substring(0, 4) + "-" + clib.DateToText(dat_yymm.DateTime).Substring(4, 2);
						nrow["DEPT_NM"] = df.GetDept_nmData(drow[2].ToString().Trim());
						nrow["SABN"] = drow[2].ToString();
						nrow["SABN_NM"] = df.GetSawon_nmData(drow[2].ToString().Trim()); //drow[3].ToString();
						nrow["MINUS_NAMT"] = clib.TextToDecimal(drow[4].ToString());
						nrow["PLUS_NAMT"] = clib.TextToDecimal(drow[5].ToString());
						ds.Tables["S_INFOSD05"].Rows.Add(nrow);
					}
				}
			}
			if (outVal > 0)			
				MessageBox.Show("엑셀업로드가 완료되었습니다.", "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
			
			grd.DataSource = ds.Tables["S_INFOSD05"];
			Cursor = Cursors.Default;
		}
		
		//N수당조회
		private void btn_search_Click(object sender, EventArgs e)
		{
			if (isNoError_um(2))
			{
				df.GetSEARCH_INFOSD05Datas(clib.DateToText(dat_frmm.DateTime).Substring(0, 6), clib.DateToText(dat_tomm.DateTime).Substring(0, 6), ds);
				pv_grd.DataSource = ds.Tables["SEARCH_INFOSD05"];
			}
		}
		
		private void btn_s_canc_Click(object sender, EventArgs e)
		{
			SetCancel(2);
		}

		private void btn_s_excel_Click(object sender, EventArgs e)
		{
			clib.gridToExcel(pv_grd, "N수당조회_"  + clib.DateToText(DateTime.Now), true);
		}

        #endregion

        #region 3 EVENT

        private void duty9040_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //종료
            {
                btn_exit.PerformClick();
            }
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
                if (clib.DateToText(dat_yymm.DateTime) == "")
                {
                    MessageBox.Show("조회년월을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dat_yymm.Focus();
                    return false;
				}
                else
                {
                    isError = true;
                }
            }
            else if (mode == 2) //조회
            {
                if (clib.DateToText(dat_frmm.DateTime) == "")
                {
                    MessageBox.Show("조회년월(fr)을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dat_frmm.Focus();
                    return false;
				}
                else if (clib.DateToText(dat_tomm.DateTime) == "")
                {
                    MessageBox.Show("조회년월(to)을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dat_tomm.Focus();
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
			btn_bring.Enabled = arr.Substring(3, 1) == "1" ? true : false;
			btn_excel.Enabled = arr.Substring(4, 1) == "1" ? true : false;
			btn_e_up.Enabled = arr.Substring(5, 1) == "1" ? true : false;
		}


		#endregion

	}
}

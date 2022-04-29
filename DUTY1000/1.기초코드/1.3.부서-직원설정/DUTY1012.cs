using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using System.Text;
using System.Drawing;

namespace DUTY1000
{
    public partial class duty1012 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();

        public duty1012()
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
			btn_refresh_CK();

			if (ds.Tables["SEARCH_NURS"] != null)
				ds.Tables["SEARCH_NURS"].Clear();
            grd2.DataSource = null;
			
			if (ds.Tables["S_TRSPART"] != null)
				ds.Tables["S_TRSPART"].Clear();
            grd_log.DataSource = null;

            sl_part.EditValue = null;
			txt_num.Text = "";
		}

        #endregion

        #region 1 Form
		
        private void duty1012_Load(object sender, EventArgs e)
        {
            SetCancel();
        }

        #endregion

        #region 2 Button
		
		private void btn_refresh_Click(object sender, EventArgs e)
		{
			btn_refresh_CK();
		}
		private void btn_refresh_CK()
		{
			df.GetSEARCH_MSTPARTDatas(ds);
            grd1.DataSource = ds.Tables["SEARCH_MSTPART"];

			df.GetSEARCH_PARTDatas(ds);
			sl_part.Properties.DataSource = ds.Tables["SEARCH_PART"];
		}
		
		//전체선택
		private void btn_select_all_Click(object sender, EventArgs e)
		{
			if (ds.Tables["SEARCH_NURS"] != null)
			{
				for (int i = 0; i < ds.Tables["SEARCH_NURS"].Rows.Count; i++)
				{
					ds.Tables["SEARCH_NURS"].Rows[i]["CHK"] = "1";
				}
				txt_num.Text = ds.Tables["SEARCH_NURS"].Select("CHK='1'").Length.ToString();
			}
		}
		//선택해지
		private void btn_select_del_Click(object sender, EventArgs e)
		{
			if (ds.Tables["SEARCH_NURS"] != null)
			{
				for (int i = 0; i < ds.Tables["SEARCH_NURS"].Rows.Count; i++)
				{
					ds.Tables["SEARCH_NURS"].Rows[i]["CHK"] = "0";
				}
				txt_num.Text = ds.Tables["SEARCH_NURS"].Select("CHK='1'").Length.ToString();
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
					df.GetDUTY_TRSPARTDatas(ds);
                    string sawon = "";
					for (int i = 0; i < ds.Tables["SEARCH_NURS"].Rows.Count; i++)
					{
						DataRow drow = ds.Tables["SEARCH_NURS"].Rows[i];
						if (ds.Tables["SEARCH_NURS"].Rows[i]["CHK"].ToString() == "1")
						{
							DataRow nrow = ds.Tables["DUTY_TRSPART"].NewRow();
							nrow["SAWON_NO"] = drow["SAWON_NO"].ToString();
							nrow["FR_PART"] = drow["PARTCODE"].ToString();
							nrow["TO_PART"] = sl_part.EditValue;
							nrow["REG_DT"] = gd.GetNow();
							nrow["REG_ID"] = SilkRoad.Config.SRConfig.USID;
							ds.Tables["DUTY_TRSPART"].Rows.Add(nrow);

							sawon += ",'" + drow["SAWON_NO"].ToString().Trim() + "'";
						}
					}
					
                    //DataRow[] dr = ds.Tables["SEARCH_NURS"].Select("CHK = '1'");

                    ////체크한 사원번호들을 모아서 mstnurs에서 데이터테이블로 가져옴
                    ////foreach로 돌면서 해당사번들 partcode update하기
                    //foreach (DataRow d in dr)
                    //{
                    //    d["FR_PART"] = dr["PARTCODE"].ToString();
                    //    d["TO_PART"] = sl_part.EditValue;

                    //    sawon += ",'"+d["SAWON_NO"].ToString().Trim() + "'";
                    //}

                    string[] UpQry = { "update duty_mstnurs set partcode = '" + sl_part.EditValue + "' where sawon_no in (" + sawon.Substring(1) + ")" };

					string[] tableNames = new string[] { "DUTY_TRSPART" };
					SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
					outVal = cmd.setUpdate(ref ds, tableNames, UpQry);
                    
                }
                catch (Exception ec)
                {
                    MessageBox.Show(ec.Message, "저장오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
					if (outVal <= 0)                    
                        MessageBox.Show("저장된 내용이 없습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information); 
					else
                        MessageBox.Show("선택된 직원들의 파트이동이 처리되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetCancel();
                    Cursor = Cursors.Default;
                }
            }
        }
        
		private void btn_exel_Click(object sender, EventArgs e)
		{
            clib.gridToExcel(grdv2, this.Text + "(" + this.Name + ")_" + clib.DateToText(DateTime.Now), true);
		}
        /// <summary>취소버튼</summary>
        private void btn_clear_Click(object sender, EventArgs e)
        {
            SetCancel();
        }

        #endregion

        #region 3 EVENT
		
        private void grdv1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            DataRow drow = grdv1.GetFocusedDataRow();

            df.GetSEARCH_PART_NURSDatas(drow["CODE"].ToString().Trim(), ds);
            grd2.DataSource = ds.Tables["SEARCH_NURS"];
			txt_num.Text = "";
			
			if (ds.Tables["S_TRSPART"] != null)
				ds.Tables["S_TRSPART"].Clear();
            grd_log.DataSource = null;
        }

        private void grdv2_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            //clib.gridview_CustomDrawColumnHeader_forCheck(sender, e);
        }
        private void grdv2_MouseDown(object sender, MouseEventArgs e)
        {
            //clib.gridview_MouseDown_forCheck(sender, e);
        }

        //체크한 인원수만 count되도록
		private void grdv2_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			DataRow drow = grdv2.GetFocusedDataRow();
			if (drow != null)
			{
				if (e.Column == col_CHK)  //선택       
				{
					drow["CHK"] = e.Value.ToString();
					txt_num.Text = ds.Tables["SEARCH_NURS"].Select("CHK='1'").Length.ToString();
				}
			}
		}
        private void grdv2_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
			//ds.Tables["SEARCH_NURS"].AcceptChanges();

			//if (e.IsTotalSummary)
			//{
			//	if (ds.Tables["SEARCH_NURS"].Select("CHECK = 'true'") == null || ds.Tables["SEARCH_NURS"].Select("CHECK = 'true'").Length == 0)
			//	{
			//		e.TotalValue = 0;
			//	}
			//	else
			//	{
			//		e.TotalValue = ds.Tables["SEARCH_NURS"].Select("CHECK = 'true'").Length;
			//	}
			//	txt_num.Text = e.TotalValue.ToString();
			//}
		}
        private void repositoryItemCheckEdit1_EditValueChanged(object sender, EventArgs e)
        {
            ////값변화 바로 반영
            //grdv2.PostEditor();
            //ds.Tables["SEARCH_NURS"].AcceptChanges();
        }
		
		private void grdv2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
		{
            if (e.RowHandle < 0)
                return;

            DataRow drow = grdv2.GetFocusedDataRow();
			df.GetS_TRSPARTDatas(drow["SAWON_NO"].ToString(), ds);
            grd_log.DataSource = ds.Tables["S_TRSPART"];
		}
        private void grdv2_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
			if (e.Column == col_CHK)			
				grdv2.PostEditor(); //값변화 바로 반영			
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

            if (mode == 1)  //처리
            {
              
            }
            else if (mode == 2)  //저장
            {
				if (ds.Tables["SEARCH_NURS"].Select("CHK = '1'").Length == 0)
				{
					MessageBox.Show("선택된 직원이 없습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				else if (clib.isLookupNull(sl_part.EditValue) || sl_part.EditValue.ToString().Equals("파트선택"))
				{
					MessageBox.Show("이동할 파트를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				else if (grdv1.GetFocusedDataRow()["CODE"].ToString() == sl_part.EditValue.ToString())
				{
					MessageBox.Show("이동할 파트가 기존과 동일합니다.\r\n파트를 다시 확인하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

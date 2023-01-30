using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;

namespace DUTY1000
{
    public partial class duty1040 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();

        public duty1040()
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
			if (ds.Tables["SEARCH_HOLIDAY"] != null)
				ds.Tables["SEARCH_HOLIDAY"].Clear();
            SetButtonEnable("1000");
            grd1.DataSource = null;
			grd1.Enabled = false;
			dat_year.Enabled = true;
			dat_h_dt.Text = "";
			txt_h_name.Text = "";
			srPanel2.Enabled = false;
        }

        #endregion

        #region 1 Form

        private void duty1040_Load(object sender, EventArgs e)
        {
            SetCancel();
			dat_year.DateTime = DateTime.Now;
            dat_year.Focus();
        }

        #endregion

        #region 2 Button
		
        //처리
		private void btn_search_Click(object sender, EventArgs e)
		{
			if (clib.DateToText(dat_year.DateTime) == "")
			{
                MessageBox.Show("조회년도가 입력되지 않았습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dat_year.Focus();
			}
			else
			{			
				SetButtonEnable("0111");
				df.GetSEARCH_HolidayDatas(clib.DateToText(dat_year.DateTime.AddYears(-1)).Substring(0, 4), clib.DateToText(dat_year.DateTime).Substring(0, 4), ds);
				grd1.DataSource = ds.Tables["SEARCH_HOLIDAY"];
				grd1.Enabled = true;
				dat_year.Enabled = false;
				srPanel2.Enabled = true;
                dat_h_dt.Focus();
			}
		}
		
		private void btn_put_Click(object sender, EventArgs e)
		{
			if (clib.DateToText(dat_h_dt.DateTime) == "")
			{
				MessageBox.Show("휴일일자가 입력되지 않았습니다!", "입력오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dat_h_dt.Focus();
			}
			else if (ds.Tables["SEARCH_HOLIDAY"].Select("DD = '" + dat_h_dt.DateTime.ToString().Substring(0, 10) +"'").Length > 0)
			{
				MessageBox.Show("해당일자는 이미 등록되어 있습니다!", "입력오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dat_h_dt.Focus();
			}
			else if (clib.DateToText(dat_year.DateTime).Substring(0, 4) != clib.DateToText(dat_h_dt.DateTime).Substring(0, 4))
			{
				MessageBox.Show("조회년도 범위에 포함되지 않는 날짜입니다!", "입력오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dat_h_dt.Focus();
			}
			else if (txt_h_name.Text.ToString() == "")
			{
				MessageBox.Show("휴일명이 입력되지 않았습니다!", "입력오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_h_name.Focus();
			}
			else
			{			
				DataRow nrow = ds.Tables["SEARCH_HOLIDAY"].NewRow();
				nrow["DD"] = dat_h_dt.DateTime.ToString().Substring(0, 10);
				string dd_nm = "";
				switch (clib.TextToDate(dat_h_dt.DateTime.ToString().Replace("-","").Substring(0, 8)).DayOfWeek.ToString())
				{
					case "Monday":
						dd_nm = "월";
						break;
					case "Tuesday":
						dd_nm = "화";
						break;
					case "Wednesday":
						dd_nm = "수";
						break;
					case "Thursday":
						dd_nm = "목";
						break;
					case "Friday":
						dd_nm = "금";
						break;
					case "Saturday":
						dd_nm = "토";
						break;
					case "Sunday":
						dd_nm = "일";
						break;
				}
				//System.Globalization.KoreanLunisolarCalendar kLunar = new System.Globalization.KoreanLunisolarCalendar();
				//DateTime BirthLift = new DateTime(1984, 02, 29);
				//DateTime BirthLunar = new DateTime(kLunar.GetYear(BirthLift), kLunar.GetMonth(BirthLift), kLunar.GetDayOfMonth(BirthLift));
				//string solar_dt = kLunar.ToDateTime(2021, 8, 15, 0, 0, 0, 0).ToString("yyyy-MM-dd");

				nrow["DD_NM"] = dd_nm;
				nrow["H_NAME"] = txt_h_name.Text.ToString();
				nrow["REPEAT_CHK"] = chk_repeat.Checked == true ? "1" : "0";
				nrow["GUBN"] = (cmb_type.SelectedIndex + 1).ToString();
				nrow["GUBN_NM"] = cmb_type.SelectedIndex == 0 ? "휴일" : "대체휴일";
				nrow["STAT_NM"] = "대기";
				ds.Tables["SEARCH_HOLIDAY"].Rows.Add(nrow);
				ds.Tables["SEARCH_HOLIDAY"].DefaultView.Sort = "DD";
			}
		}

        //저장
        private void btn_save_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            int outVal = 0;
            try
            {
				df.GetDUTY_MSTHOLIDatas(ds);
				df.GetD_DUTY_MSTHOLIDatas(clib.DateToText(dat_year.DateTime).Substring(0, 4), ds);
				for (int i = 0; i < ds.Tables["SEARCH_HOLIDAY"].Rows.Count; i++)
				{
					DataRow drow = ds.Tables["SEARCH_HOLIDAY"].Rows[i];
					if (drow.RowState != DataRowState.Deleted)
					{
						DataRow nrow = ds.Tables["DUTY_MSTHOLI"].NewRow();
						nrow["H_DATE"] = drow["DD"].ToString().Replace("-","").Substring(0, 8);
						nrow["H_NAME"] = drow["H_NAME"].ToString();
						nrow["REG_DT"] = gd.GetNow();
						nrow["REG_ID"] = SilkRoad.Config.SRConfig.USID;
						nrow["GUBN"] = drow["GUBN"].ToString();
						nrow["REPEAT_CHK"] = drow["REPEAT_CHK"].ToString();
						ds.Tables["DUTY_MSTHOLI"].Rows.Add(nrow);
					}
				}
				
				for (int i = 0; i < ds.Tables["D_DUTY_MSTHOLI"].Rows.Count; i++)
				{
					ds.Tables["D_DUTY_MSTHOLI"].Rows[i].Delete();
				}

				string[] tableNames = new string[] { "D_DUTY_MSTHOLI", "DUTY_MSTHOLI" };
				SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
				outVal = cmd.setUpdate(ref ds, tableNames, null);
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message, "저장오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetCancel();
                Cursor = Cursors.Default;
            }
        }

        //라인삭제
        private void btn_linedel_Click(object sender, EventArgs e)
        {
			if (grdv1.FocusedRowHandle > -1)
				grdv1.DeleteSelectedRows();
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

        private void duty1040_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //취소
            {
                btn_clear.PerformClick();
            }
        }
		
		private void grdv1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			if (e.Column == col_date)
			{
				DataRow drow = grdv1.GetFocusedDataRow();
				string dd_nm = "";
				switch (clib.TextToDate(drow["DD"].ToString().Replace("-","").Substring(0, 8)).DayOfWeek.ToString())
				{
					case "Monday":
						dd_nm = "월";
						break;
					case "Tuesday":
						dd_nm = "화";
						break;
					case "Wednesday":
						dd_nm = "수";
						break;
					case "Thursday":
						dd_nm = "목";
						break;
					case "Friday":
						dd_nm = "금";
						break;
					case "Saturday":
						dd_nm = "토";
						break;
					case "Sunday":
						dd_nm = "일";
						break;
				}
				drow["DD_NM"] = dd_nm;
			}
		}
        private void grdv1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            //InitNewRowGure(e.RowHandle);
			//ds.Tables["SEARCH_HOLIDAY"].DefaultView.Sort = "DD"; 		e.RowHandle	-2147483647	int
        }
        private void InitNewRowGure(int rowHndl)
        {
            DataRow drow = grdv1.GetDataRow(rowHndl);

            //drow["GURECLCD"] = _clientCode;
            //drow["GURECODE"] = getNextCode2(ds.Tables["GURE"], "GURECODE", "", 4);//여기가 문제 될 수 있다고?0 으로 들어간데?
            //drow["GURECHEK"] = true;

            //grdv1.ClearColumnsFilter();
            //string filteringCondition = string.Empty;
            //filteringCondition = "[" + "CLNTCODE" + "] LIKE '" + _clientCode + "%'";

            //grdv1.Columns["CLNTCODE"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, filteringCondition);
        }		
        private void grdv1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            int indexCnt = 0;
            string msgVal = string.Empty;
            string[] arrField = { "DD", "H_NAME" };
            string[] arrFldNm = { ".일자.", ".휴일명." };
            if (e.Row != null)
            {
                DataRow ecw = ((DataRowView)e.Row).Row;
                for (int i = 0; i < arrField.Length; i++)
                {
                    string tempVal = ecw.Field<string>(arrField[i]);
                    if (string.IsNullOrEmpty(tempVal))
                    {
                        msgVal += arrFldNm[i] + " ";
                        indexCnt++;
                    }
                }

				if (indexCnt > 0)
				{
					e.ErrorText = "필수입력항목 (" + msgVal + ")을 입력하십시오.\n\n";
					e.Valid = false;
				}
            }
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
            btn_save.Enabled = arr.Substring(1, 1) == "1" ? true : false;
            btn_clear.Enabled = arr.Substring(2, 1) == "1" ? true : false;
            btn_linedel.Enabled = arr.Substring(3, 1) == "1" ? true : false;
        }

		#endregion
		
		private void dateEdit1_Properties_CalendarTimeProperties_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
		{			
			//if (dateEdit1.Properties.VistaCalendarViewStyle == DevExpress.XtraEditors.VistaCalendarViewStyle.MonthView)
			//{
			//	if (dateEdit1.DateTime.Date.ToString("ddd") == "토") // || dateEdit1.DateTime.Date.ToString("ddd") == "일")
			//	{
			//		dateEdit1.ForeColor = System.Drawing.Color.Blue;
			//	}
			//}
		}
	}
}

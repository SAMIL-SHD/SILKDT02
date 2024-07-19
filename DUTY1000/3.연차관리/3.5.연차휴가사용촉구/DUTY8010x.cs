using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using DevExpress.XtraReports.UI;

namespace DUTY1000
{
	public partial class duty8010x : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();
        static DataProcessing dp = new DataProcessing();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();
		int idx = 0;
        public duty8010x()
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
			grd1.DataSource = null;
			txt_sawon_nm.Text = "";

			btn_refresh_CK();

			//mm_Contents.Text = "";
            //SetButtonEnable("10");
        }
		
        private void baseInfoSearch()
        {
        }
        #endregion

        #region 1 Form

        private void duty8010x_Load(object sender, EventArgs e)
        {
			dat_year.DateTime = DateTime.Now;
			sl_dept.EditValue = null;
            SetCancel();
        }

        #endregion

        #region 2 Button
		
		//조회
		private void btn_search_Click(object sender, EventArgs e)
		{			
			df.GetSEARCH_INFODatas(ds);
			string yc_code = "";
			if (ds.Tables["SEARCH_INFO"].Rows.Count > 0)
				yc_code = ds.Tables["SEARCH_INFO"].Rows[0]["GT_CODE1"].ToString();
			string dept = sl_dept.EditValue == null ? "%" : sl_dept.EditValue.ToString();

			if (yc_code != "")
			{
				df.GetSEARCH_EMBSDatas(clib.DateToText(dat_year.DateTime).Substring(0, 4), yc_code, dept, ds);
				grd1.DataSource = ds.Tables["SEARCH_EMBS"];
			}
			else
			{
				MessageBox.Show("환경설정 연차근태코드 입력이 되지 않았습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		//이메일 전송화면으로 이동
		private void btn_email_Click(object sender, EventArgs e)
		{
			if (isNoError_um(1))
			{
				try
				{
					int[] rows = grdv1.GetSelectedRows();

					int without_group = 0;
					for (int i = 0; i < rows.Length; i++)
					{
						if (rows[i] < 0)
						{
							without_group++;
						}
					}

					DataTable table = ds.Tables["SEARCH_EMBS"].Clone();
					for (int i = 0; i < rows.Length - without_group; i++)
						table.ImportRow(grdv1.GetDataRow(rows[i + without_group]));
					dp.AddDatatable2Dataset("COPY_SEARCH_EMBS", table, ref ds);

					sendemail sm = new sendemail(ds, 1);
					sm.StartPosition = FormStartPosition.CenterScreen;
					sm.ShowDialog();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				finally
				{
					string yc_code = "";
					if (ds.Tables["SEARCH_INFO"].Rows.Count > 0)
						yc_code = ds.Tables["SEARCH_INFO"].Rows[0]["GT_CODE5"].ToString();
					string dept = sl_dept.EditValue == null ? "%" : sl_dept.EditValue.ToString();
					df.GetSEARCH_EMBSDatas(clib.DateToText(dat_year.DateTime).Substring(0, 4), yc_code, dept, ds);
					grd1.DataSource = ds.Tables["SEARCH_EMBS"];
				}
			}
		}

        /// <summary>취소버튼</summary>
        private void btn_clear_Click(object sender, EventArgs e)
        {
            SetCancel();
        }

        #endregion

        #region 3 EVENT
		
        private void duty8010_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //취소
            {
                btn_clear.PerformClick();
            }
        }
		
		private void grdv1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
		{
			int[] rows = grdv1.GetSelectedRows();
			int without_group = 0;
			for (int i = 0; i < rows.Length; i++)
			{
				if (rows[i] < 0)
				{
					without_group++;
				}
			}
			txt_cnt_chk.Text = String.Format("{0:#,###}", grdv1.GetSelectedRows().Length - without_group);
		}

		//더블클릭시 출력
		private void grdv1_DoubleClick(object sender, EventArgs e)
		{
			DataRow drow = grdv1.GetFocusedDataRow();
			if (drow == null)
				return;

			Rpt_8010 r = new Rpt_8010(drow["SAWON_NO"].ToString(), drow["DOC_TYPE"].ToString(), drow["YC_SQ"].ToString(), SilkRoad.Config.SRConfig.WorkPlaceName, ds);
			r.DataSource = ds.Tables["SEARCH_EMBS"].Select("SAWON_NO = '" + drow["SAWON_NO"] + "' AND DOC_TYPE = '" + drow["DOC_TYPE"] + "' AND YC_SQ = " + drow["YC_SQ"] + "").CopyToDataTable();
			r.ShowPreview();			
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
            if (mode == 1)
            {				
				int[] rows = grdv1.GetSelectedRows();

				int without_group = 0;
				for (int i = 0; i < rows.Length; i++)
				{
					if (rows[i] < 0)
					{
						without_group++;
					}
				}

                if (rows.Length - without_group == 0)
                {
                    MessageBox.Show("선택된 내역이 없습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// 기초코드 룩업컨트롤에 설정하는 부분
        /// </summary>
        private void btn_refresh_CK()
        {
			//df.GetSEARCH_DEPRDatas(ds);
			//sl_dept.Properties.DataSource = ds.Tables["SEARCH_DEPR"];
        }

		/// <summary>
		/// 배열에따른 버튼상태설정
		/// </summary>
		/// <param name="mode"></param>
		private void SetButtonEnable(string arr)
		{
        }

		#endregion
	}
}

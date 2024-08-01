using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using System.Data.OleDb;
using System.Collections;

namespace DUTY1000
{
    public partial class duty9010 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();

        public duty9010()
        {
            InitializeComponent();
        }

        #region 0. Initialization

        private void SetCancel()
        {
            if (ds.Tables["SEARCH_T_AMT"] != null)
                ds.Tables["SEARCH_T_AMT"].Clear();
            grd.DataSource = null;

            dat_yymm.Enabled = true;
        }

        #endregion

        #region 1 Form

        private void duty9010_Load(object sender, EventArgs e)
        {
			dat_yymm.DateTime = DateTime.Now.AddMonths(-1);
        }
		private void duty9010_Shown(object sender, EventArgs e)
		{
			SetCancel();
		}

        #endregion

        #region 2 Button
		
        //시급조회
        private void btn_search_Click(object sender, EventArgs e)
        {
            if (isNoError_um(1))
            {
                df.GetSEARCH_T_AMTDatas(clib.DateToText(dat_yymm.DateTime).Substring(0, 6), ds);
                grd.DataSource = ds.Tables["SEARCH_T_AMT"];
            }
        }
        //취소
        private void btn_canc_Click(object sender, EventArgs e)
		{
			SetCancel();
		}
		private void btn_excel_Click(object sender, EventArgs e)
		{
            clib.gridToExcel(grdv, "사원별시급조회_" + clib.DateToText(DateTime.Now), true);
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
            if (mode == 1)  //조회
            {
                if (clib.DateToText(dat_yymm.DateTime) == "")
                {
                    MessageBox.Show("기준년월을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dat_yymm.Focus();
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

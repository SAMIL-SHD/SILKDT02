using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using System.Text;
using DevExpress.XtraGrid.Views;
using System.Drawing;
using DevExpress.XtraReports.UI;
using System.Data.OleDb;

namespace DUTY1000
{
    public partial class duty8040 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();
        static DataProcessing dp = new DataProcessing();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();

        private int admin_lv = 0;

        public duty8040()
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
            if (ds.Tables["8040_SEARCH"] != null)
                ds.Tables["8040_SEARCH"].Clear();
            grd1.DataSource = null;

            txt_sabn.Text = "";
            txt_name.Text = "";
            
            grd_ot.DataSource = null;
            grd_yc.DataSource = null;
        }

        #endregion

        #region 1 Form

        private void duty8040_Load(object sender, EventArgs e)
        {
            SetCancel();

            dat_st_year.DateTime = DateTime.Now;
            dat_st_year.Focus();
        }

        private void duty8040_Shown(object sender, EventArgs e)
        {
            df.GetMSTUSER_CHKDatas(ds);  //부서관리여부
            if (ds.Tables["MSTUSER_CHK"].Rows.Count > 0)
                admin_lv = clib.TextToInt(ds.Tables["MSTUSER_CHK"].Rows[0]["EMBSADGB"].ToString()); //권한레벨

            if (SilkRoad.Config.ACConfig.G_MSYN == "1" || SilkRoad.Config.SRConfig.USID == "SAMIL")
            {
                admin_lv = 3;
                lb_power.Text = "전체관리 권한";
            }
            else if (admin_lv == 1)
            {
                lb_power.Text = "부서관리 권한";
            }
            else
            {
                lb_power.Text = "관리권한 없음";
            }
            
            btn_search.PerformClick();
        }
        #endregion

        #region 2 Button

        private void btn_expand_Click(object sender, EventArgs e)
        {
            clib.SetExpandFold(btn_expand, srSplitContainer1, DevExpress.XtraEditors.SplitPanelVisibility.Panel1);
        }

        //조회
        private void btn_search_Click(object sender, EventArgs e)
        {
            if (isNoError_um(1))
            {
                proc();
            }
        }
        private void proc()
        {
            df.Get8040_SEARCHDatas(admin_lv, clib.DateToText(dat_st_year.DateTime), ds);
            grd1.DataSource = ds.Tables["8040_SEARCH"];
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
            clib.gridToExcel(grdv1, this.Name + "_" + clib.DateToText(DateTime.Now));
        }
        
        #endregion

        #region 3 EVENT

        private void duty8020_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //취소
                btn_clear.PerformClick();            
        }
		
		//그리드 더블클릭시 발생/사용내역 조회
		private void grdv1_DoubleClick(object sender, EventArgs e)
        {
            DataRow drow = grdv1.GetFocusedDataRow();
            if (drow == null)
                return;

            dat_year.DateTime = clib.TextToDate(drow["REQ_YEAR"].ToString() + "0101");
            txt_sabn.Text = drow["SABN"].ToString();
            txt_name.Text = drow["EMBSNAME"].ToString();

            df.Get8040_TIME1Datas(drow["SABN"].ToString(), drow["REQ_YEAR"].ToString(), ds);
            grd_ot.DataSource = ds.Tables["8040_TIME1"];

            df.Get8040_TIME2Datas(drow["SABN"].ToString(), drow["REQ_YEAR"].ToString(), ds);
            grd_yc.DataSource = ds.Tables["8040_TIME2"];
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

			if (mode == 1)  //조회
			{
				if (clib.DateToText(dat_st_year.DateTime) == "")
				{
					MessageBox.Show("기준년도를 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dat_st_year.Focus();
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

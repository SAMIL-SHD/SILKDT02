using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;

namespace WAGE1000
{
    public partial class duty1005 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();

        public duty1005()
        {
            InitializeComponent();
        }

        #region 0. Initialization

        #endregion

        #region 1 Form

        private void duty1005_Load(object sender, EventArgs e)
        {
        }
		private void duty1005_Shown(object sender, EventArgs e)
        {
            df.GetSEARCH_USERDEPTDatas(ds);
            grd1.DataSource = ds.Tables["SEARCH_USERDEPT"];

            df.GetSL_DEPTDatas(ds);
			grd_sl_dept.DataSource = ds.Tables["SL_DEPT"];
			//grd_sl_dept2.DataSource = ds.Tables["SL_DEPT"];
		}

        #endregion

        #region 2 Button
        
        private void btn_dept_save_Click(object sender, EventArgs e)
        {
            DataRow drow = grdv1.GetFocusedDataRow();
            if (drow == null)
                return;

            Cursor = Cursors.WaitCursor;
            try
            {
                df.GetDUTY_PWERDEPTDatas(ds);
                foreach (DataRow drow2 in ds.Tables["USERDEPT"].Select("CHK = '1'"))
                {
                    DataRow nrow = ds.Tables["DUTY_PWERDEPT"].NewRow();

                    nrow["SABN"] = drow["USERIDEN"].ToString().Trim();
                    nrow["DEPT"] = drow2["CODE"];
                    nrow["REG_DT"] = gd.GetNow();
                    nrow["REG_ID"] = SilkRoad.Config.SRConfig.USID;
                    ds.Tables["DUTY_PWERDEPT"].Rows.Add(nrow);
                }
                string QRY = "DELETE FROM DUTY_PWERDEPT WHERE SABN = '" + drow["USERIDEN"].ToString().Trim() + "'";

                string[] qrys = new string[] { QRY };
                string[] tableNames = new string[] { "DUTY_PWERDEPT" };
                SilkRoad.DbCmd_DT02.DbCmd_DT02 cmd = new SilkRoad.DbCmd_DT02.DbCmd_DT02();
                int outVal = cmd.setUpdate(ref ds, tableNames, qrys);
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message, "저장오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                lb_msg.Text = "저장되었습니다!";
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region 3 EVENT

        private void duty1090_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //종료
            {
                btn_exit.PerformClick();
            }
        }
        private void grdv_dept_MouseWheel(object sender, MouseEventArgs e)
        {
            if (grdv_dept.ActiveEditor != null)
                grdv_dept.CloseEditor();
        }

        private void grdv1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
		{
			DataRow drow = grdv1.GetFocusedDataRow();
            if (drow == null)
                return;

            lb_msg.Text = "";

            string sabn = drow["USERIDEN"].ToString().Trim();
			df.GetUserDeptDatas(sabn, ds);
			grd_dept.DataSource = ds.Tables["USERDEPT"];
		}

        #endregion

        #region 9. ETC

        #endregion

    }
}

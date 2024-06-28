using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;

namespace DUTY1000
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
			df.GetSL_DEPTDatas(ds);
			grd_sl_dept.DataSource = ds.Tables["SL_DEPT"];
			//grd_sl_dept2.DataSource = ds.Tables["SL_DEPT"];

			df.GetSEARCH_USERDEPTDatas(ds);
			grd1.DataSource = ds.Tables["SEARCH_USERDEPT"];
		}

        #endregion

        #region 2 Button
		
        //저장
		private void btn_save_Click(object sender, EventArgs e)
		{			
            Cursor = Cursors.WaitCursor;
            int outVal = 0;
            try
            {
				df.GetMSTUSERDatas(ds);
				DataRow drow, nrow;
				for (int i = 0; i < ds.Tables["SEARCH_USERDEPT"].Rows.Count; i++)
				{
					drow = ds.Tables["SEARCH_USERDEPT"].Rows[i];
					if (ds.Tables["MSTUSER"].Select("USERIDEN = '" + drow["USERIDEN"].ToString().Trim() + "'").Length > 0)
					{
						nrow = ds.Tables["MSTUSER"].Select("USERIDEN = '" + drow["USERIDEN"].ToString().Trim() + "'")[0];
						nrow["USERDPCD"] = drow["USERDPCD"].ToString().Trim();
						nrow["USERMSYN"] = drow["USERMSYN"].ToString().Trim();
						nrow["USERUPYN"] = drow["USERUPYN"].ToString().Trim();
						nrow["USERUPDT"] = gd.GetNow().Substring(0, 8);
						nrow["USERUSID"] = SilkRoad.Config.SRConfig.USID;
						nrow["USERPSTY"] = "U";
					}
					else
					{
						nrow = ds.Tables["MSTUSER"].NewRow();
						nrow["USERIDEN"] = drow["USERIDEN"].ToString().Trim();
						nrow["USERNAME"] = drow["USERNAME"].ToString().Trim();
						nrow["USERPSWD"] = drow["USERPSWD"].ToString().Trim();
						nrow["USERDPCD"] = drow["USERDPCD"].ToString().Trim();
						nrow["USERJWCD"] = "";
						nrow["USERPRCD"] = "";
						nrow["USERMSYN"] = drow["USERMSYN"].ToString().Trim();
						nrow["USERUPYN"] = drow["USERUPYN"].ToString().Trim();
						nrow["USERJGYN"] = "0";
						nrow["USERINDT"] = gd.GetNow().Substring(0, 8);
						nrow["USERUPDT"] = "";
						nrow["USERUSID"] = SilkRoad.Config.SRConfig.USID;
						nrow["USERPSTY"] = "A";
						ds.Tables["MSTUSER"].Rows.Add(nrow);
					}
				}

				//string[] tableNames = new string[] { "MSTUSER" };
				//SilkRoad.DbCmd_DUTY.DbCmd_DUTY cmd = new SilkRoad.DbCmd_DUTY.DbCmd_DUTY();
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
				this.Dispose();
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
		
		private void grdv1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
		{
			DataRow drow = grdv1.GetFocusedDataRow();
            if (drow == null)
                return;

			string sabn = drow["USERIDEN"].ToString().Trim();
			df.GetUserDeptDatas(sabn, ds);
			grd_dept.DataSource = ds.Tables["USERDEPT"];
		}
		
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
                Cursor = Cursors.Default;
            }
		}

		#endregion

		#region 9. ETC

		#endregion

	}
}

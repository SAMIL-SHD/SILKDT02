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
				df.GetDUTY_MSTUSERDatas(ds);
				DataRow drow, nrow;
				for (int i = 0; i < ds.Tables["SEARCH_USERDEPT"].Rows.Count; i++)
				{
					drow = ds.Tables["SEARCH_USERDEPT"].Rows[i];
					if (ds.Tables["DUTY_MSTUSER"].Select("USERIDEN = '" + drow["USERIDEN"].ToString().Trim() + "'").Length > 0)
					{
						nrow = ds.Tables["DUTY_MSTUSER"].Select("USERIDEN = '" + drow["USERIDEN"].ToString().Trim() + "'")[0];
						nrow["USERDPCD"] = drow["USERDPCD"].ToString().Trim();
						nrow["USERMSYN"] = drow["USERMSYN"].ToString().Trim();
						nrow["USERUPYN"] = drow["USERUPYN"].ToString().Trim();
						nrow["USERUPDT"] = gd.GetNow();
						nrow["USERUSID"] = SilkRoad.Config.SRConfig.USID;
						nrow["USERPSTY"] = "U";
					}
					else
					{
						nrow = ds.Tables["DUTY_MSTUSER"].NewRow();
						nrow["USERIDEN"] = drow["USERIDEN"].ToString().Trim();
						nrow["USERNAME"] = drow["USERNAME"].ToString().Trim();
						nrow["USERDPCD"] = drow["USERDPCD"].ToString().Trim();
						nrow["USERMSYN"] = drow["USERMSYN"].ToString().Trim();
						nrow["USERUPYN"] = drow["USERUPYN"].ToString().Trim();
						nrow["USERINDT"] = gd.GetNow();
						nrow["USERUPDT"] = "";
						nrow["USERUSID"] = SilkRoad.Config.SRConfig.USID;
						nrow["USERPSTY"] = "A";
						ds.Tables["DUTY_MSTUSER"].Rows.Add(nrow);
					}
				}

				string[] tableNames = new string[] { "DUTY_MSTUSER" };
				SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
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

		#endregion

		#region 9. ETC

		#endregion

	}
}

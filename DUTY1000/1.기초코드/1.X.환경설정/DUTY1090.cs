using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;

namespace DUTY1000
{
    public partial class duty1090 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();

        public duty1090()
        {
            InitializeComponent();
        }

        #region 0. Initialization

        #endregion

        #region 1 Form

        private void duty1090_Load(object sender, EventArgs e)
        {
        }
		private void duty1090_Shown(object sender, EventArgs e)
		{
			sl_gtcd1.EditValue = null;
			sl_gtcd2.EditValue = null;
			sl_gtcd3.EditValue = null;
			sl_gtcd4.EditValue = null;
			sl_gtcd5.EditValue = null;
			sl_gtcd6.EditValue = null;
			sl_gtcd7.EditValue = null;
			sl_gtcd8.EditValue = null;
			sl_gtcd9.EditValue = null;
			sl_gtcd10.EditValue = null;

			df.GetSL_GTDatas(ds);
			sl_gtcd1.Properties.DataSource = ds.Tables["SL_GT"];
			sl_gtcd2.Properties.DataSource = ds.Tables["SL_GT"];
			sl_gtcd3.Properties.DataSource = ds.Tables["SL_GT"];
			sl_gtcd4.Properties.DataSource = ds.Tables["SL_GT"];
			sl_gtcd5.Properties.DataSource = ds.Tables["SL_GT"];
			sl_gtcd6.Properties.DataSource = ds.Tables["SL_GT"];
			sl_gtcd7.Properties.DataSource = ds.Tables["SL_GT"];
			sl_gtcd8.Properties.DataSource = ds.Tables["SL_GT"];
			sl_gtcd9.Properties.DataSource = ds.Tables["SL_GT"];
			sl_gtcd10.Properties.DataSource = ds.Tables["SL_GT"];

			df.GetSEARCH_INFO1Datas(ds);
			if (ds.Tables["DUTY_INFOSD01"].Rows.Count > 0)
			{
				DataRow srow = ds.Tables["DUTY_INFOSD01"].Rows[0];
				cmb_off_next.SelectedIndex = srow["OFF_NEXT_YN"].ToString().Trim() == "" ? 0 : clib.TextToInt(srow["OFF_NEXT_YN"].ToString()) - 1;
				sl_gtcd1.EditValue = srow["GT_CODE1"].ToString().Trim() == "" ? null : srow["GT_CODE1"].ToString().Trim();
				sl_gtcd2.EditValue = srow["GT_CODE2"].ToString().Trim() == "" ? null : srow["GT_CODE2"].ToString().Trim();
				sl_gtcd3.EditValue = srow["GT_CODE3"].ToString().Trim() == "" ? null : srow["GT_CODE3"].ToString().Trim();
				sl_gtcd4.EditValue = srow["GT_CODE4"].ToString().Trim() == "" ? null : srow["GT_CODE4"].ToString().Trim();
				sl_gtcd5.EditValue = srow["GT_CODE5"].ToString().Trim() == "" ? null : srow["GT_CODE5"].ToString().Trim();
				sl_gtcd6.EditValue = srow["GT_CODE6"].ToString().Trim() == "" ? null : srow["GT_CODE6"].ToString().Trim();
				sl_gtcd7.EditValue = srow["GT_CODE7"].ToString().Trim() == "" ? null : srow["GT_CODE7"].ToString().Trim();
				sl_gtcd8.EditValue = srow["GT_CODE8"].ToString().Trim() == "" ? null : srow["GT_CODE8"].ToString().Trim();
				sl_gtcd9.EditValue = srow["GT_CODE9"].ToString().Trim() == "" ? null : srow["GT_CODE9"].ToString().Trim();
				sl_gtcd10.EditValue = srow["GT_CODE10"].ToString().Trim() == "" ? null : srow["GT_CODE10"].ToString().Trim();
			}
			//df.GetSEARCH_INFO2Datas(ds);
			//grd1.DataSource = ds.Tables["SEARCH_INFO2"];

			//df.GetSL_GNMUDatas(ds);
			//grd_sl_gnmu.DataSource = ds.Tables["SL_GNMU"];
			//grd_sl_gt.DataSource = ds.Tables["SL_GT"];
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
				df.GetSEARCH_INFO1Datas(ds);
				if (ds.Tables["DUTY_INFOSD01"].Rows.Count > 0)
				{
					DataRow nrow = ds.Tables["DUTY_INFOSD01"].Rows[0];
					nrow["OFF_NEXT_YN"] = (cmb_off_next.SelectedIndex + 1).ToString();
					nrow["GT_CODE1"] = sl_gtcd1.EditValue == null ? "" : sl_gtcd1.EditValue.ToString();
					nrow["GT_CODE2"] = sl_gtcd2.EditValue == null ? "" : sl_gtcd2.EditValue.ToString();
					nrow["GT_CODE3"] = sl_gtcd3.EditValue == null ? "" : sl_gtcd3.EditValue.ToString();
					nrow["GT_CODE4"] = sl_gtcd4.EditValue == null ? "" : sl_gtcd4.EditValue.ToString();
					nrow["GT_CODE5"] = sl_gtcd5.EditValue == null ? "" : sl_gtcd5.EditValue.ToString();
					nrow["GT_CODE6"] = sl_gtcd6.EditValue == null ? "" : sl_gtcd6.EditValue.ToString();
					nrow["GT_CODE7"] = sl_gtcd7.EditValue == null ? "" : sl_gtcd7.EditValue.ToString();
					nrow["GT_CODE8"] = sl_gtcd8.EditValue == null ? "" : sl_gtcd8.EditValue.ToString();
					nrow["GT_CODE9"] = sl_gtcd9.EditValue == null ? "" : sl_gtcd9.EditValue.ToString();
					nrow["GT_CODE10"] = sl_gtcd10.EditValue == null ? "" : sl_gtcd10.EditValue.ToString();
					nrow["UPDT"] = gd.GetNow();
					nrow["USID"] = SilkRoad.Config.SRConfig.USID;
					nrow["PSTY"] = "U";
				}
				else
				{
					DataRow nrow = ds.Tables["DUTY_INFOSD01"].NewRow();
					nrow["OFF_NEXT_YN"] = (cmb_off_next.SelectedIndex + 1).ToString();
					nrow["GT_CODE1"] = sl_gtcd1.EditValue == null ? "" : sl_gtcd1.EditValue.ToString();
					nrow["GT_CODE2"] = sl_gtcd2.EditValue == null ? "" : sl_gtcd2.EditValue.ToString();
					nrow["GT_CODE3"] = sl_gtcd3.EditValue == null ? "" : sl_gtcd3.EditValue.ToString();
					nrow["GT_CODE4"] = sl_gtcd4.EditValue == null ? "" : sl_gtcd4.EditValue.ToString();
					nrow["GT_CODE5"] = sl_gtcd5.EditValue == null ? "" : sl_gtcd5.EditValue.ToString();
					nrow["GT_CODE6"] = sl_gtcd6.EditValue == null ? "" : sl_gtcd6.EditValue.ToString();
					nrow["GT_CODE7"] = sl_gtcd7.EditValue == null ? "" : sl_gtcd7.EditValue.ToString();
					nrow["GT_CODE8"] = sl_gtcd8.EditValue == null ? "" : sl_gtcd8.EditValue.ToString();
					nrow["GT_CODE9"] = sl_gtcd9.EditValue == null ? "" : sl_gtcd9.EditValue.ToString();
					nrow["GT_CODE10"] = sl_gtcd10.EditValue == null ? "" : sl_gtcd10.EditValue.ToString();
					nrow["INDT"] = gd.GetNow();
					nrow["UPDT"] = "";
					nrow["USID"] = SilkRoad.Config.SRConfig.USID;
					nrow["PSTY"] = "A";
					ds.Tables["DUTY_INFOSD01"].Rows.Add(nrow);
				}

				//df.GetD_DUTY_INFOSD02Datas(ds);  //삭제후
				//for (int i = 0; i < ds.Tables["D_DUTY_INFOSD02"].Rows.Count; i++)
				//{
				//	ds.Tables["D_DUTY_INFOSD02"].Rows[i].Delete();
				//}

				//df.GetDUTY_INFOSD02Datas(ds);  //등록
				//foreach (DataRow dr in ds.Tables["SEARCH_INFO2"].Rows)
				//            {
				//	DataRow nrow = ds.Tables["DUTY_INFOSD02"].NewRow();
				//	nrow["GT_CODE"] = dr["GT_CODE"];
				//	nrow["G_CODE"] = dr["G_CODE"];
				//	nrow["REMARK"] = dr["REMARK"].ToString().Trim();
				//	nrow["REG_DT"] = gd.GetNow();
				//	nrow["REG_ID"] = SilkRoad.Config.SRConfig.USID;
				//	ds.Tables["DUTY_INFOSD02"].Rows.Add(nrow);
				//            }

				string[] tableNames = new string[] { "DUTY_INFOSD01", }; // "D_DUTY_INFOSD02", "DUTY_INFOSD02" };
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

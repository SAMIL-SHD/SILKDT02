using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;

namespace DUTY1000
{
    public partial class duty9060 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();

        public duty9060()
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
            df.GetSL_SDDatas(ds);
            sl_sdcd1.Properties.DataSource = ds.Tables["SL_SD"];
            sl_sdcd2.Properties.DataSource = ds.Tables["SL_SD"];
            sl_sdcd3.Properties.DataSource = ds.Tables["SL_SD"];
            sl_sdcd4.Properties.DataSource = ds.Tables["SL_SD"];
            sl_sdcd5.Properties.DataSource = ds.Tables["SL_SD"];
            sl_sdcd6.Properties.DataSource = ds.Tables["SL_SD"];
            sl_sdcd7.Properties.DataSource = ds.Tables["SL_SD"];
            sl_sdcd8.Properties.DataSource = ds.Tables["SL_SD"];
            sl_sdcd9.Properties.DataSource = ds.Tables["SL_SD"];
            sl_sdcd10.Properties.DataSource = ds.Tables["SL_SD"];

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
        }
        #endregion

        #region 1 Form

        private void duty9060_Load(object sender, EventArgs e)
        {
			sl_sdcd1.EditValue = null;
			sl_sdcd2.EditValue = null;
			sl_sdcd3.EditValue = null;
			sl_sdcd4.EditValue = null;
			sl_sdcd5.EditValue = null;
			sl_sdcd6.EditValue = null;
			sl_sdcd7.EditValue = null;
			sl_sdcd8.EditValue = null;
			sl_sdcd9.EditValue = null;
            sl_sdcd10.EditValue = null;

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
        }
		private void duty9060_Shown(object sender, EventArgs e)
		{
            SetCancel();            

			df.GetDUTY_INFOSD06Datas(ds);
            for (int i = 0; i < ds.Tables["DUTY_INFOSD06"].Rows.Count; i++)
			{
				DataRow srow = ds.Tables["DUTY_INFOSD06"].Rows[i];

                if (srow["SQ"].ToString() == "1")  //당직수당
                {
                    sl_sdcd1.EditValue = srow["SD_CODE"].ToString().Trim() == "" ? null : srow["SD_CODE"].ToString().Trim();
                    sl_gtcd1.EditValue = srow["GT_CODE"].ToString().Trim() == "" ? null : srow["GT_CODE"].ToString().Trim();
                }
                else if (srow["SQ"].ToString() == "2") //나이트수당
                {
                    sl_sdcd2.EditValue = srow["SD_CODE"].ToString().Trim() == "" ? null : srow["SD_CODE"].ToString().Trim();
                    sl_gtcd2.EditValue = srow["GT_CODE"].ToString().Trim() == "" ? null : srow["GT_CODE"].ToString().Trim();
                }
                else if (srow["SQ"].ToString() == "10") //나이트감독수당
                {
                    sl_sdcd10.EditValue = srow["SD_CODE"].ToString().Trim() == "" ? null : srow["SD_CODE"].ToString().Trim();
                    sl_gtcd10.EditValue = srow["GT_CODE"].ToString().Trim() == "" ? null : srow["GT_CODE"].ToString().Trim();
                }
                else if (srow["SQ"].ToString() == "3") //야식비
                {
                    sl_sdcd3.EditValue = srow["SD_CODE"].ToString().Trim() == "" ? null : srow["SD_CODE"].ToString().Trim();
                    sl_gtcd3.EditValue = srow["GT_CODE"].ToString().Trim() == "" ? null : srow["GT_CODE"].ToString().Trim();
                    txt_slam3.Text = srow["SD_SLAM"].ToString();
                }
                else if (srow["SQ"].ToString() == "4") //연장수당
                {
                    sl_sdcd4.EditValue = srow["SD_CODE"].ToString().Trim() == "" ? null : srow["SD_CODE"].ToString().Trim();
                    sl_gtcd4.EditValue = srow["GT_CODE"].ToString().Trim() == "" ? null : srow["GT_CODE"].ToString().Trim();
                }
                else if (srow["SQ"].ToString() == "5") //off수당
                {
                    sl_sdcd5.EditValue = srow["SD_CODE"].ToString().Trim() == "" ? null : srow["SD_CODE"].ToString().Trim();
                    sl_gtcd5.EditValue = srow["GT_CODE"].ToString().Trim() == "" ? null : srow["GT_CODE"].ToString().Trim();
                }
                else if (srow["SQ"].ToString() == "6") //휴일수당
                {
                    sl_sdcd6.EditValue = srow["SD_CODE"].ToString().Trim() == "" ? null : srow["SD_CODE"].ToString().Trim();
                    sl_gtcd6.EditValue = srow["GT_CODE"].ToString().Trim() == "" ? null : srow["GT_CODE"].ToString().Trim();
                }
                else if (srow["SQ"].ToString() == "7") //콜수당
                {
                    sl_sdcd7.EditValue = srow["SD_CODE"].ToString().Trim() == "" ? null : srow["SD_CODE"].ToString().Trim();
                    sl_gtcd7.EditValue = srow["GT_CODE"].ToString().Trim() == "" ? null : srow["GT_CODE"].ToString().Trim();
                }
                else if (srow["SQ"].ToString() == "8") //대기수당
                {
                    sl_sdcd8.EditValue = srow["SD_CODE"].ToString().Trim() == "" ? null : srow["SD_CODE"].ToString().Trim();
                    sl_gtcd8.EditValue = srow["GT_CODE"].ToString().Trim() == "" ? null : srow["GT_CODE"].ToString().Trim();
                }
                else if (srow["SQ"].ToString() == "9") //연차수당
                {
                    sl_sdcd9.EditValue = srow["SD_CODE"].ToString().Trim() == "" ? null : srow["SD_CODE"].ToString().Trim();
                    sl_gtcd9.EditValue = srow["GT_CODE"].ToString().Trim() == "" ? null : srow["GT_CODE"].ToString().Trim();
                }				
			}
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
				df.GetDUTY_INFOSD06Datas(ds);
				DataRow nrow;
                for (int i = 1; i <= 10; i++)
                {
                    if (ds.Tables["DUTY_INFOSD06"].Select("SQ = " + i).Length > 0)
                    {
                        nrow = ds.Tables["DUTY_INFOSD06"].Select("SQ = " + i)[0];
                    }
                    else
                    {
                        nrow = ds.Tables["DUTY_INFOSD06"].NewRow();
                        nrow["SQ"] = i;
                        ds.Tables["DUTY_INFOSD06"].Rows.Add(nrow);
                    }

                    if (i == 1)  //당직수당
                    {
                        nrow["SD_CODE"] = sl_sdcd1.EditValue == null ? "" : sl_sdcd1.EditValue.ToString();
                        nrow["SD_NAME"] = "당직수당";
                        nrow["GT_CODE"] = sl_gtcd1.EditValue == null ? "" : sl_gtcd1.EditValue.ToString();
                    }
                    else if (i == 2)  //나이트수당
                    {
                        nrow["SD_CODE"] = sl_sdcd2.EditValue == null ? "" : sl_sdcd2.EditValue.ToString();
                        nrow["SD_NAME"] = "나이트수당";
                        nrow["GT_CODE"] = sl_gtcd2.EditValue == null ? "" : sl_gtcd2.EditValue.ToString();
                    }
                    else if (i == 10)  //나이트감독수당
                    {
                        nrow["SD_CODE"] = sl_sdcd10.EditValue == null ? "" : sl_sdcd10.EditValue.ToString();
                        nrow["SD_NAME"] = "나이트감독수당";
                        nrow["GT_CODE"] = sl_gtcd10.EditValue == null ? "" : sl_gtcd10.EditValue.ToString();
                    }
                    else if (i == 3)  //야식비
                    {
                        nrow["SD_CODE"] = sl_sdcd3.EditValue == null ? "" : sl_sdcd3.EditValue.ToString();
                        nrow["SD_NAME"] = "야식비";
                        nrow["GT_CODE"] = sl_gtcd3.EditValue == null ? "" : sl_gtcd3.EditValue.ToString();
                    }
                    else if (i == 4)  //연장수당
                    {
                        nrow["SD_CODE"] = sl_sdcd4.EditValue == null ? "" : sl_sdcd4.EditValue.ToString();
                        nrow["SD_NAME"] = "연장수당";
                        nrow["GT_CODE"] = sl_gtcd4.EditValue == null ? "" : sl_gtcd4.EditValue.ToString();
                    }
                    else if (i == 5)  //off수당
                    {
                        nrow["SD_CODE"] = sl_sdcd5.EditValue == null ? "" : sl_sdcd5.EditValue.ToString();
                        nrow["SD_NAME"] = "off수당";
                        nrow["GT_CODE"] = sl_gtcd5.EditValue == null ? "" : sl_gtcd5.EditValue.ToString();
                    }
                    else if (i == 6)  //휴일수당
                    {
                        nrow["SD_CODE"] = sl_sdcd6.EditValue == null ? "" : sl_sdcd6.EditValue.ToString();
                        nrow["SD_NAME"] = "휴일수당";
                        nrow["GT_CODE"] = sl_gtcd6.EditValue == null ? "" : sl_gtcd6.EditValue.ToString();
                    }
                    else if (i == 7)  //콜수당
                    {
                        nrow["SD_CODE"] = sl_sdcd7.EditValue == null ? "" : sl_sdcd7.EditValue.ToString();
                        nrow["SD_NAME"] = "콜수당";
                        nrow["GT_CODE"] = sl_gtcd7.EditValue == null ? "" : sl_gtcd7.EditValue.ToString();
                    }
                    else if (i == 8)  //대기수당
                    {
                        nrow["SD_CODE"] = sl_sdcd8.EditValue == null ? "" : sl_sdcd8.EditValue.ToString();
                        nrow["SD_NAME"] = "대기수당";
                        nrow["GT_CODE"] = sl_gtcd8.EditValue == null ? "" : sl_gtcd8.EditValue.ToString();
                    }
                    else if (i == 9)  //연차수당
                    {
                        nrow["SD_CODE"] = sl_sdcd9.EditValue == null ? "" : sl_sdcd9.EditValue.ToString();
                        nrow["SD_NAME"] = "연차수당";
                        nrow["GT_CODE"] = sl_gtcd9.EditValue == null ? "" : sl_gtcd9.EditValue.ToString();
                    }
                    nrow["SD_SLAM"] = i == 3 ? clib.TextToDecimal(txt_slam3.Text.ToString()) : 0;
                    nrow["REG_DT"] = gd.GetNow();
                    nrow["REG_ID"] = SilkRoad.Config.SRConfig.USID;
                }

				string[] tableNames = new string[] { "DUTY_INFOSD06" };
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
                {
                    MessageBox.Show("해당 내용이 저장되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
            }
		}
    
        #endregion

        #region 3 EVENT

        private void duty9060_KeyDown(object sender, KeyEventArgs e)
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

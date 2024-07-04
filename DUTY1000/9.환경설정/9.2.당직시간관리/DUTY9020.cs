using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;

namespace DUTY1000
{
    public partial class duty9020 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();

        public duty9020()
        {
            InitializeComponent();
        }

        #region 0. Initialization
		
        private void SetCancel()
		{
			if (ds.Tables["S_INFOSD02"] != null)
				ds.Tables["S_INFOSD02"].Clear();
			grd.DataSource = null;
			SetButtonEnable("1000");

            dat_yymm.Enabled = true;
            btn_save.Text = "저 장";
            btn_save.Image = Properties.Resources.저장;
        }

        #endregion

        #region 1 Form

        private void duty9020_Load(object sender, EventArgs e)
        {
			dat_yymm.DateTime = DateTime.Now.AddMonths(-1);
        }
		private void duty9020_Shown(object sender, EventArgs e)
		{
			df.GetDUTY_INFOSD02_DTDatas(ds);
			if (ds.Tables["DUTY_INFOSD02_DT"].Rows.Count > 0)
			{
				DataRow srow = ds.Tables["DUTY_INFOSD02_DT"].Rows[0];
				txt_dt01.Text = srow["DT01"].ToString();
				txt_dt02.Text = srow["DT02"].ToString();
				txt_dt03.Text = srow["DT03"].ToString();
				txt_dt04.Text = srow["DT04"].ToString();
				txt_dt05.Text = srow["DT05"].ToString();
				txt_dt06.Text = srow["DT06"].ToString();
				txt_dt07.Text = srow["DT07"].ToString();
			}
		}

        #endregion

        #region 2 Button
		
		//처리
		private void btn_proc_Click(object sender, EventArgs e)
		{
			if (isNoError_um(1))
			{
				string yymm = clib.DateToText(dat_yymm.DateTime).Substring(0, 6);
				df.GetS_INFOSD02Datas(yymm, ds);

				int cnt = clib.TextToInt(clib.DateToText(clib.TextToDateLast(clib.DateToText(dat_yymm.DateTime))).Substring(6, 2));
				for (int i = 1; i < cnt + 1; i++)
				{
					string date = yymm + i.ToString().PadLeft(2, '0');
					if (ds.Tables["S_INFOSD02"].Select("DANG_DT = '" + date + "'").Length == 0)
					{
						DataRow nrow = ds.Tables["S_INFOSD02"].NewRow();
						nrow["DANG_DT"] = date;
						nrow["DANG_TIME"] = 0;
						nrow["SLDT_NM"] = date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2);
						nrow["DAY_NM"] = clib.WeekDay(clib.TextToDate(date));
						ds.Tables["S_INFOSD02"].Rows.Add(nrow);
					}
				}

				ds.Tables["S_INFOSD02"].DefaultView.Sort = "DANG_DT ASC";
				grd.DataSource = ds.Tables["S_INFOSD02"];

                df.GetDUTY_INFOSD02Datas(yymm, ds);
                if (ds.Tables["DUTY_INFOSD02"].Rows.Count > 0)
                {
                    btn_save.Text = "수 정";
                    btn_save.Image = Properties.Resources.수정;
                    SetButtonEnable("0111");
                }
                else
                    SetButtonEnable("0101");

                dat_yymm.Enabled = false;
            }
		}
		//시간 가져오기
		private void btn_time_Click(object sender, EventArgs e)
		{			
			df.GetDUTY_INFOSD02_DTDatas(ds);
			if (ds.Tables["DUTY_INFOSD02_DT"].Rows.Count > 0)
			{
				DataRow trow = ds.Tables["DUTY_INFOSD02_DT"].Rows[0];
				for (int i = 0; i < ds.Tables["S_INFOSD02"].Rows.Count; i++)
				{
					DataRow drow = ds.Tables["S_INFOSD02"].Rows[i];
					switch (drow["DAY_NM"].ToString())
					{
						case "월":
							drow["DANG_TIME"] = clib.TextToDecimal(trow["DT01"].ToString());
							break;
						case "화":
							drow["DANG_TIME"] = clib.TextToDecimal(trow["DT02"].ToString());
							break;
						case "수":
							drow["DANG_TIME"] = clib.TextToDecimal(trow["DT03"].ToString());
							break;
						case "목":
							drow["DANG_TIME"] = clib.TextToDecimal(trow["DT04"].ToString());
							break;
						case "금":
							drow["DANG_TIME"] = clib.TextToDecimal(trow["DT05"].ToString());
							break;
						case "토":
							drow["DANG_TIME"] = clib.TextToDecimal(trow["DT06"].ToString());
							break;
						case "일":
							drow["DANG_TIME"] = clib.TextToDecimal(trow["DT07"].ToString());
							break;
					}
				}
			}
		}
        //저장
		private void btn_save_Click(object sender, EventArgs e)
		{
			string yymm = clib.DateToText(dat_yymm.DateTime).Substring(0, 6);
            if (MessageBox.Show(yymm.Substring(0, 4) + "." + yymm.Substring(4, 2) + "월의 당직시간을 저장하시겠습니까?", "저장", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
							== DialogResult.OK)
			{
				Cursor = Cursors.WaitCursor;
				int outVal = 0;
				try
				{
					df.GetDUTY_INFOSD02Datas(yymm, ds);
					for (int i = 0; i < ds.Tables["S_INFOSD02"].Rows.Count; i++)
					{
						DataRow drow = ds.Tables["S_INFOSD02"].Rows[i];
						if (ds.Tables["DUTY_INFOSD02"].Select("DANG_DT = '" + drow["DANG_DT"].ToString() + "'").Length > 0)
						{
							DataRow nrow = ds.Tables["DUTY_INFOSD02"].Select("DANG_DT = '" + drow["DANG_DT"].ToString() + "'")[0];
							nrow["DANG_TIME"] = clib.TextToDecimal(drow["DANG_TIME"].ToString());
							nrow["REG_DT"] = gd.GetNow();
							nrow["REG_ID"] = SilkRoad.Config.SRConfig.USID;
						}
						else
						{
							DataRow nrow = ds.Tables["DUTY_INFOSD02"].NewRow();
							nrow["DANG_DT"] = drow["DANG_DT"].ToString();
							nrow["DANG_TIME"] = clib.TextToDecimal(drow["DANG_TIME"].ToString());
							nrow["REG_DT"] = gd.GetNow();
							nrow["REG_ID"] = SilkRoad.Config.SRConfig.USID;
							ds.Tables["DUTY_INFOSD02"].Rows.Add(nrow);
						}
					}

					string[] tableNames = new string[] { "DUTY_INFOSD02" };
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
                        SetCancel();
                    }
					Cursor = Cursors.Default;
				}
			}
		}
    
		//요일별 시간 저장
		private void btn_save2_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("요일별 당직시간을 저장하시겠습니까?", "저장", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
							== DialogResult.OK)
			{
				Cursor = Cursors.WaitCursor;
				int outVal = 0;
				try
				{
					df.GetDUTY_INFOSD02_DTDatas(ds);
					if (ds.Tables["DUTY_INFOSD02_DT"].Rows.Count > 0)
					{
						DataRow nrow = ds.Tables["DUTY_INFOSD02_DT"].Rows[0];
						nrow["DT01"] = clib.TextToDecimal(txt_dt01.Text.ToString());
						nrow["DT02"] = clib.TextToDecimal(txt_dt02.Text.ToString());
						nrow["DT03"] = clib.TextToDecimal(txt_dt03.Text.ToString());
						nrow["DT04"] = clib.TextToDecimal(txt_dt04.Text.ToString());
						nrow["DT05"] = clib.TextToDecimal(txt_dt05.Text.ToString());
						nrow["DT06"] = clib.TextToDecimal(txt_dt06.Text.ToString());
						nrow["DT07"] = clib.TextToDecimal(txt_dt07.Text.ToString());
						nrow["REG_DT"] = gd.GetNow();
						nrow["REG_ID"] = SilkRoad.Config.SRConfig.USID;
					}
					else
					{
						DataRow nrow = ds.Tables["DUTY_INFOSD02_DT"].NewRow();
						nrow["DT01"] = clib.TextToDecimal(txt_dt01.Text.ToString());
						nrow["DT02"] = clib.TextToDecimal(txt_dt02.Text.ToString());
						nrow["DT03"] = clib.TextToDecimal(txt_dt03.Text.ToString());
						nrow["DT04"] = clib.TextToDecimal(txt_dt04.Text.ToString());
						nrow["DT05"] = clib.TextToDecimal(txt_dt05.Text.ToString());
						nrow["DT06"] = clib.TextToDecimal(txt_dt06.Text.ToString());
						nrow["DT07"] = clib.TextToDecimal(txt_dt07.Text.ToString());
						nrow["REG_DT"] = gd.GetNow();
						nrow["REG_ID"] = SilkRoad.Config.SRConfig.USID;
						ds.Tables["DUTY_INFOSD02_DT"].Rows.Add(nrow);
					}

					string[] tableNames = new string[] { "DUTY_INFOSD02_DT" };
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
                        srTabControl1.SelectedTabPageIndex = 0;
                    }
					Cursor = Cursors.Default;
				}
			}
		}

		//삭제
		private void btn_del_Click(object sender, EventArgs e)
		{
			string yymm = clib.DateToText(dat_yymm.DateTime).Substring(0, 6);
			df.GetDUTY_INFOSD02Datas(yymm, ds);
			if (ds.Tables["DUTY_INFOSD02"].Rows.Count > 0)
			{
				DialogResult dr = MessageBox.Show(yymm.Substring(0, 4) + "." + yymm.Substring(4, 2) + "월의 당직시간을 모두 삭제하시겠습니까?", "삭제여부", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
				if (dr == DialogResult.OK)
				{
					int outVal = 0;
					try
					{
						for (int i = 0; i < ds.Tables["DUTY_INFOSD02"].Rows.Count; i++)
						{
							ds.Tables["DUTY_INFOSD02"].Rows[i].Delete();
						}

						string[] tableNames = new string[] { "DUTY_INFOSD02" };
						SilkRoad.DbCmd_DT02.DbCmd_DT02 cmd = new SilkRoad.DbCmd_DT02.DbCmd_DT02();
						outVal = cmd.setUpdate(ref ds, tableNames, null);
					}
					catch (Exception ec)
					{
						MessageBox.Show(ec.Message, "삭제오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					finally
					{
						if (outVal > 0)
							MessageBox.Show(yymm.Substring(0, 4) + "." + yymm.Substring(4, 2) + "월의 당직시간이 삭제되었습니다.", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);

						SetCancel();
						Cursor = Cursors.Default;
					}
				}
			}
			else
			{
                MessageBox.Show(yymm.Substring(0, 4) + "." + yymm.Substring(4, 2) + "월은 삭제할 내역이 없습니다.", "삭제오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btn_canc_Click(object sender, EventArgs e)
		{
			SetCancel();
		}

		private void btn_excel_Click(object sender, EventArgs e)
		{			
            clib.gridToExcel(grdv, "당직시간관리_" + clib.DateToText(DateTime.Now), true);
		}

        #endregion

        #region 3 EVENT

        private void duty9020_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //종료
            {
                btn_exit.PerformClick();
            }
        }
        private void srTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            btn_save2.Visible = srTabControl1.SelectedTabPageIndex == 0 ? false : true;
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
            if (mode == 1)  //처리
            {
                if (clib.DateToText(dat_yymm.DateTime) == "")
                {
                    MessageBox.Show("조회년월을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

		#region 9.ETC
		
		private void SetButtonEnable(string arr)
		{
			btn_proc.Enabled = arr.Substring(0, 1) == "1" ? true : false;
            btn_save.Enabled = arr.Substring(1, 1) == "1" ? true : false;
            btn_del.Enabled = arr.Substring(2, 1) == "1" ? true : false;
            btn_canc.Enabled = arr.Substring(3, 1) == "1" ? true : false;
		}

        #endregion

    }
}

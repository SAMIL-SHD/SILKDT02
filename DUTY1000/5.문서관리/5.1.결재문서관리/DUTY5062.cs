using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using System.Drawing;

namespace DUTY1000
{
    public partial class duty5062 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();
		string stat = "";
		string gubn = "";
		string doc_no = "";

        public duty5062(string _stat, string _gubn, string _doc_no)
        {
            InitializeComponent();
			stat = _stat;
			gubn = _gubn;
			doc_no = _doc_no;

			this.Width = clib.TextToInt(_gubn) > 4 ? 1200 : 800;
        }

        #region 0. Initialization

        /// <summary>
        ///컨트롤 초기화 및 활성,비활성 설정
        /// </summary>
        /// <param name="enable"></param>
        private void SetCancel()
        {
			grd1.DataSource = null;
            //SetButtonEnable("100");
        }

        #endregion

        #region 1 Form

        private void duty5062_Load(object sender, EventArgs e)
        {
			if (stat == "1")
			{
				btn_save.Visible = true;
				btn_return.Visible = true;
				btn_canc.Visible = true;
			}

			if (SilkRoad.Config.SRConfig.US_GUBN == "1" || SilkRoad.Config.SRConfig.USID == "SAMIL")
			{
				btn_admin_save.Visible = true;
				btn_admin_canc.Visible = true;
				btn_canc.Visible = true;
			}

			if (stat == "2")
				srTitle1.SRTitleTxt = "완료문서";

			if (gubn == "1")
				xtraTabPage1.PageVisible = true;
			else if (gubn == "2")
				xtraTabPage2.PageVisible = true;
			else if (gubn == "3")
				xtraTabPage3.PageVisible = true;
			else if (gubn == "4")
				xtraTabPage4.PageVisible = true;
			else if (gubn == "5")
				xtraTabPage5.PageVisible = true;
			else if (gubn == "6")
				xtraTabPage6.PageVisible = true;
			Proc();
        }

        #endregion

        #region 2 Button

        private void Proc()
        {
			df.Get5062_SEARCHDatas(gubn, doc_no, ds);
			if (gubn == "1")
				grd1.DataSource = ds.Tables["5062_SEARCH"];
			else if (gubn == "2")
				grd2.DataSource = ds.Tables["5062_SEARCH"];
			else if (gubn == "3")
				grd3.DataSource = ds.Tables["5062_SEARCH"];
			//else if (gubn == "4")
			//	grd4.DataSource = ds.Tables["5062_SEARCH"];
			else if (gubn == "5")
			{
				#region 요일에따른 헤더 설정
				string yymm = ds.Tables["5062_SEARCH"].Rows[0]["PLANYYMM"].ToString();
				df.GetSEARCH_HOLIDatas(yymm, ds);
				int lastday = clib.TextToInt(clib.DateToText(clib.TextToDateLast(yymm+"01")).Substring(6, 2));

				for (int i = 1; i <= lastday; i++)
				{
					DateTime day = clib.TextToDate(yymm + "01").AddDays(i - 1);
					grdv5.Columns["D" + i.ToString().PadLeft(2, '0')].Caption = i.ToString() + "\r\n" + clib.WeekDay(day); //일+요일. 한칸 내려서 보이도록. 엔터마냥. 
					if (clib.WeekDay(day) == "토")				
						grdv5.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.ForeColor = Color.Blue;				
					else if (clib.WeekDay(day) == "일")				
						grdv5.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.ForeColor = Color.Red;				
					else				
						grdv5.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.ForeColor = Color.Black;				

					if (ds.Tables["SEARCH_HOLI"].Select("H_DATE = '" + clib.DateToText(day) + "'").Length > 0)
					{
						grdv5.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.ForeColor = Color.Red;
						grdv5.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.Font = new Font(FontFamily.GenericSansSerif, 9F, FontStyle.Underline);
						grdv5.Columns["D" + i.ToString().PadLeft(2, '0')].ToolTip = ds.Tables["SEARCH_HOLI"].Select("H_DATE = '" + clib.DateToText(day) + "'")[0]["H_NAME"].ToString();
					}
				}

				//남은 필드 visible = false;
				for (int k = 1; k < 32; k++)
				{
					if (k > lastday)
						grdv5.Columns["D" + k.ToString().PadLeft(2, '0')].Visible = false;
					else
						grdv5.Columns["D" + k.ToString().PadLeft(2, '0')].Visible = true;
				}
				#endregion

				df.Get2060_DANG_GNMUDatas(ds);
				grd_sl_gnmu.DataSource = ds.Tables["2060_DANG_GNMU"];
				grd5.DataSource = ds.Tables["5062_SEARCH"];

			}
			else if (gubn == "6")
			{
				#region 요일에따른 헤더 설정
				string yymm = ds.Tables["5062_SEARCH"].Rows[0]["PLANYYMM"].ToString();
				df.GetSEARCH_HOLIDatas(yymm, ds);
				int lastday = clib.TextToInt(clib.DateToText(clib.TextToDateLast(yymm+"01")).Substring(6, 2));

				for (int i = 1; i <= lastday; i++)
				{
					DateTime day = clib.TextToDate(yymm + "01").AddDays(i - 1);
					grdv6.Columns["D" + i.ToString().PadLeft(2, '0')].Caption = i.ToString() + "\r\n" + clib.WeekDay(day); //일+요일. 한칸 내려서 보이도록. 엔터마냥. 
					if (clib.WeekDay(day) == "토")				
						grdv6.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.ForeColor = Color.Blue;				
					else if (clib.WeekDay(day) == "일")				
						grdv6.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.ForeColor = Color.Red;				
					else				
						grdv6.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.ForeColor = Color.Black;				

					if (ds.Tables["SEARCH_HOLI"].Select("H_DATE = '" + clib.DateToText(day) + "'").Length > 0)
					{
						grdv6.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.ForeColor = Color.Red;
						grdv6.Columns["D" + i.ToString().PadLeft(2, '0')].AppearanceHeader.Font = new Font(FontFamily.GenericSansSerif, 9F, FontStyle.Underline);
						grdv6.Columns["D" + i.ToString().PadLeft(2, '0')].ToolTip = ds.Tables["SEARCH_HOLI"].Select("H_DATE = '" + clib.DateToText(day) + "'")[0]["H_NAME"].ToString();
					}
				}

				//남은 필드 visible = false;
				for (int k = 1; k < 32; k++)
				{
					if (k > lastday)
						grdv6.Columns["D" + k.ToString().PadLeft(2, '0')].Visible = false;
					else
						grdv6.Columns["D" + k.ToString().PadLeft(2, '0')].Visible = true;
				}
				#endregion

				df.GetGNMU_LISTDatas(ds);
				grd_lk_gnmu.DataSource = ds.Tables["GNMU_LIST"];
				grd6.DataSource = ds.Tables["5062_SEARCH"];
			}
        }
		
		//결재
		private void btn_save_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
            int outVal = 0;
			int stat = 0;
            try
            {
				DataRow drow = ds.Tables["5062_SEARCH"].Rows[0];
				df.GetDUTY_GWDOCDatas(drow["DOC_NO"].ToString(), ds);
				if (ds.Tables["DUTY_GWDOC"].Rows.Count > 0)
				{
					DataRow hrow = ds.Tables["DUTY_GWDOC"].Rows[0];
					if (hrow["AP_TAG"].ToString() != "1")
					{
						hrow["AP_TAG"] = "4";
						if (hrow["GW_DT2"].ToString().Trim() == "" && hrow["GW_SABN2"].ToString().Trim() == SilkRoad.Config.SRConfig.USID)
						{
							stat = 1;
							hrow["GW_DT2"] = gd.GetNow();
							hrow["GW_CHKID2"] = SilkRoad.Config.SRConfig.USID;
							if (hrow["GW_SABN3"].ToString().Trim() == "")
								hrow["AP_TAG"] = "1";
						}
						else if (hrow["GW_DT3"].ToString().Trim() == "" && hrow["GW_SABN3"].ToString().Trim() == SilkRoad.Config.SRConfig.USID)
						{
							stat = 1;
							hrow["GW_DT3"] = gd.GetNow();
							hrow["GW_CHKID3"] = SilkRoad.Config.SRConfig.USID;
							if (hrow["GW_SABN4"].ToString().Trim() == "")
								hrow["AP_TAG"] = "1";
						}
						else if (hrow["GW_DT4"].ToString().Trim() == "" && hrow["GW_SABN4"].ToString().Trim() == SilkRoad.Config.SRConfig.USID)
						{
							stat = 1;
							hrow["GW_DT4"] = gd.GetNow();
							hrow["GW_CHKID4"] = SilkRoad.Config.SRConfig.USID;
							hrow["AP_TAG"] = "1";
						}

						if (stat == 1)
						{
							string[] tableNames = new string[] { "DUTY_GWDOC" };
							SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
							outVal = cmd.setUpdate(ref ds, tableNames, null);
						}
					}
				}
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message, "결재오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (outVal > 0)
                    MessageBox.Show("승인처리 되었습니다.", "결재상신", MessageBoxButtons.OK, MessageBoxIcon.Information);
				else
                    MessageBox.Show("승인할 내역이 없습니다.", "결재오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Dispose();
                Cursor = Cursors.Default;
            }
		}		
		//결재취소->디테일하게 수정예정
		private void btn_canc_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
            int outVal = 0;
			int stat = 0;
            try
            {
				DataRow drow = ds.Tables["5062_SEARCH"].Rows[0];
				df.GetDUTY_GWDOCDatas(drow["DOC_NO"].ToString(), ds);
				if (ds.Tables["DUTY_GWDOC"].Rows.Count > 0)
				{
					DataRow hrow = ds.Tables["DUTY_GWDOC"].Rows[0];
					if (hrow["AP_TAG"].ToString() != "1")
					{
						if (hrow["GW_DT2"].ToString().Trim() == "" && hrow["GW_SABN2"].ToString().Trim() == SilkRoad.Config.SRConfig.USID)
						{
							stat = 1;
							hrow["GW_DT2"] = gd.GetNow();
							hrow["GW_CHKID2"] = SilkRoad.Config.SRConfig.USID;
							hrow["AP_TAG"] = "2";
						}
						else if (hrow["GW_DT3"].ToString().Trim() == "" && hrow["GW_SABN3"].ToString().Trim() == SilkRoad.Config.SRConfig.USID)
						{
							stat = 1;
							hrow["GW_DT3"] = gd.GetNow();
							hrow["GW_CHKID3"] = SilkRoad.Config.SRConfig.USID;
							hrow["AP_TAG"] = "2";
						}
						else if (hrow["GW_DT4"].ToString().Trim() == "" && hrow["GW_SABN4"].ToString().Trim() == SilkRoad.Config.SRConfig.USID)
						{
							stat = 1;
							hrow["GW_DT4"] = gd.GetNow();
							hrow["GW_CHKID4"] = SilkRoad.Config.SRConfig.USID;
							hrow["AP_TAG"] = "2";
						}

						if (stat == 1)
						{
							string[] tableNames = new string[] { "DUTY_GWDOC" };
							SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
							outVal = cmd.setUpdate(ref ds, tableNames, null);
						}
					}
				}
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message, "취소오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (outVal > 0)
                    MessageBox.Show("취소처리 되었습니다.", "승인취소", MessageBoxButtons.OK, MessageBoxIcon.Information);
				else
                    MessageBox.Show("취소할 내역이 없습니다.", "취소오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Dispose();
                Cursor = Cursors.Default;
            }
		}
		//반려
		private void btn_return_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
            int outVal = 0;
			int stat = 0;
            try
            {
				DataRow drow = ds.Tables["5062_SEARCH"].Rows[0];
				df.GetDUTY_GWDOCDatas(drow["DOC_NO"].ToString(), ds);
				if (ds.Tables["DUTY_GWDOC"].Rows.Count > 0)
				{
					DataRow hrow = ds.Tables["DUTY_GWDOC"].Rows[0];
					if (hrow["AP_TAG"].ToString() != "1")
					{
						if (hrow["GW_DT2"].ToString().Trim() == "" && hrow["GW_SABN2"].ToString().Trim() == SilkRoad.Config.SRConfig.USID)
						{
							stat = 1;
							hrow["GW_DT2"] = gd.GetNow();
							hrow["GW_CHKID2"] = SilkRoad.Config.SRConfig.USID;
							hrow["AP_TAG"] = "5";
						}
						else if (hrow["GW_DT3"].ToString().Trim() == "" && hrow["GW_SABN3"].ToString().Trim() == SilkRoad.Config.SRConfig.USID)
						{
							stat = 1;
							hrow["GW_DT3"] = gd.GetNow();
							hrow["GW_CHKID3"] = SilkRoad.Config.SRConfig.USID;
							hrow["AP_TAG"] = "5";
						}
						else if (hrow["GW_DT4"].ToString().Trim() == "" && hrow["GW_SABN4"].ToString().Trim() == SilkRoad.Config.SRConfig.USID)
						{
							stat = 1;
							hrow["GW_DT4"] = gd.GetNow();
							hrow["GW_CHKID4"] = SilkRoad.Config.SRConfig.USID;
							hrow["AP_TAG"] = "5";
						}

						if (stat == 1)
						{
							string[] tableNames = new string[] { "DUTY_GWDOC" };
							SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
							outVal = cmd.setUpdate(ref ds, tableNames, null);
						}
					}
				}
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message, "취소오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (outVal > 0)
                    MessageBox.Show("취소처리 되었습니다.", "승인취소", MessageBoxButtons.OK, MessageBoxIcon.Information);
				else
                    MessageBox.Show("취소할 내역이 없습니다.", "취소오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Dispose();
                Cursor = Cursors.Default;
            }
		}
		
		//관리자결재
		private void btn_admin_save_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
            int outVal = 0;
            try
            {
				DataRow drow = ds.Tables["5062_SEARCH"].Rows[0];
				df.GetDUTY_GWDOCDatas(drow["DOC_NO"].ToString(), ds);
				if (ds.Tables["DUTY_GWDOC"].Rows.Count > 0)
				{
					DataRow hrow = ds.Tables["DUTY_GWDOC"].Rows[0];
					if (hrow["AP_TAG"].ToString() != "1")
					{
						hrow["AP_TAG"] = "3";
						hrow["GW_REMK"] = gd.GetNow() + " " +SilkRoad.Config.SRConfig.USID;
						
						string[] tableNames = new string[] { "DUTY_GWDOC" };
						SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
						outVal = cmd.setUpdate(ref ds, tableNames, null);
					}
				}
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message, "결재오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (outVal > 0)
                    MessageBox.Show("완료처리 되었습니다.", "결재완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
				else
                    MessageBox.Show("완료할 내역이 없습니다.", "처리오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Dispose();
                Cursor = Cursors.Default;
            }
		}		
		//관리자결재취소
		private void btn_admin_canc_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
            int outVal = 0;
            try
            {
				DataRow drow = ds.Tables["5062_SEARCH"].Rows[0];
				df.GetDUTY_GWDOCDatas(drow["DOC_NO"].ToString(), ds);
				if (ds.Tables["DUTY_GWDOC"].Rows.Count > 0)
				{
					DataRow hrow = ds.Tables["DUTY_GWDOC"].Rows[0];
					if (hrow["AP_TAG"].ToString() != "1")
					{
						hrow["AP_TAG"] = "2";
						hrow["GW_REMK"] = gd.GetNow() + " " +SilkRoad.Config.SRConfig.USID;

						string[] tableNames = new string[] { "DUTY_GWDOC" };
						SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
						outVal = cmd.setUpdate(ref ds, tableNames, null);						
					}
				}
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message, "취소오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (outVal > 0)
                    MessageBox.Show("취소처리 되었습니다.", "결재취소", MessageBoxButtons.OK, MessageBoxIcon.Information);
				else
                    MessageBox.Show("취소할 내역이 없습니다.", "취소오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Dispose();
                Cursor = Cursors.Default;
            }
		}

		#endregion

	}
}

using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;

namespace DUTY1000
{
    public partial class duty204x : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();

        public duty204x()
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
			df.GetSEARCH_PARTDatas(ds);
			sl_part.Properties.DataSource = ds.Tables["SEARCH_PART"];
			txt_num.Text = "";
		}

        #endregion

        #region 1 Form

        private void duty204x_Load(object sender, EventArgs e)
        {
            SetCancel();
        }
		private void duty204x_Shown(object sender, EventArgs e)
		{
			dat_frdt.DateTime = clib.TextToDateFirst(clib.DateToText(DateTime.Now));
			dat_todt.DateTime = DateTime.Now;
			sl_part.EditValue = null;
		}

        #endregion

        #region 2 Button
		
		//내역조회
		private void btn_s_search_Click(object sender, EventArgs e)
		{
			if (isNoError_um(1))
			{
				Search();
			}
		}
		private void Search()
		{
			string frdt = clib.DateToText(dat_frdt.DateTime);
			string todt = clib.DateToText(dat_todt.DateTime);
			df.Get2040_SEARCHDatas(frdt, todt, ds);
			grd_search.DataSource = ds.Tables["2040_SEARCH"];
		}

		//조회
		private void btn_search_Click(object sender, EventArgs e)
		{
            string part = sl_part.EditValue == null ? "%" : sl_part.EditValue.ToString();
			df.Get2040_S_NURSDatas(part, ds);
			grd1.DataSource = ds.Tables["2040_S_NURS"];
		}
		
        //call메세지 보내기
        private void btn_sendMMS_Click(object sender, EventArgs e)
        {
			if (isNoError_um(2))
			{
				Cursor = Cursors.WaitCursor;
				int outVal = 0;
				try
				{
					df.GetDUTY_TRSCALLDatas(ds);
					if (ds.Tables["2040_S_NURS"].Select("CHK = '1'").Length < 1)
					{
						MessageBox.Show("Call신청 할 직원을 선택하세요.");
						return;
					}
					else
					{
						foreach (DataRow drow in ds.Tables["2040_S_NURS"].Select("CHK = '1'"))
						{
							DataRow nrow = ds.Tables["DUTY_TRSCALL"].NewRow();
							
							nrow["SAWON_NO"] = drow["SAWON_NO"];
							nrow["CALL_DT"] = clib.DateToText(DateTime.Now);
							nrow["CALL_SQ"] = df.GetSEARCH_CALL_SQDatas(drow["SAWON_NO"].ToString(), clib.DateToText(DateTime.Now), ds);
							nrow["PARTCODE"] = drow["PARTCODE"];
							nrow["CALL_DESC"] = mm_desc.Text.ToString().Trim();
							nrow["REMARK"] = "";
							nrow["REG_DT"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
							nrow["INDT"] = gd.GetNow();
							nrow["UPDT"] = "";
							nrow["USID"] = SilkRoad.Config.SRConfig.USID;
							nrow["PSTY"] = "A";
							ds.Tables["DUTY_TRSCALL"].Rows.Add(nrow);
						}
					}

					string[] tableNames = new string[] { "DUTY_TRSCALL" };
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
                        MessageBox.Show("Call 내용이 저장되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information); 
					//sendMMS(); //MMS전송
					SetCancel();
					Search();
					Cursor = Cursors.Default;
				}
			}
        }

        //call 문자전송
        private void sendMMS()
        {
            try
            {

            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message, "저장오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }	       

        #endregion

        #region 3 EVENT
		
        //체크한 인원수만 count되도록
		private void grdv1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			DataRow drow = grdv1.GetFocusedDataRow();
			if (drow != null)
			{
				if (e.Column == col_chk)  //선택       
				{
					drow["CHK"] = e.Value.ToString();
					txt_num.Text = ds.Tables["2040_S_NURS"].Select("CHK='1'").Length.ToString();
				}
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
				if (clib.DateToText(dat_frdt.DateTime) == "")
				{
					MessageBox.Show("조회일자(fr)를 입력하세요!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dat_frdt.Focus();
					return false;
				}
				else if (clib.DateToText(dat_todt.DateTime) == "")
				{
					MessageBox.Show("조회일자(to)를 입력하세요!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dat_todt.Focus();
					return false;
				}
				else
				{
					isError = true;
				}
            }
            else if (mode == 2) //저장
            {
				if (mm_desc.Text.ToString().Trim() == "")
				{
					MessageBox.Show("Call신청 사유나 메세지를 입력하세요!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    mm_desc.Focus();
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

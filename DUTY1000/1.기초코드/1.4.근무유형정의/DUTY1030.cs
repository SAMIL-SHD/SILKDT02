using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using System.Text;
using DevExpress.XtraGrid.Views;
using System.Drawing;

namespace DUTY1000
{
    public partial class duty1030 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();
        string _Flag = "";

        public duty1030()
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
            _Flag = "";

            //if (ds.Tables["MSTBCOD"] != null)
            //{
            //    ds.Tables["MSTBCOD"].Clear();
            //}

            //grdv.ActiveFilter.Clear();
            //grdv.SortInfo.Clear();
            //selectedStat = false;

            grdv1.FocusedRowHandle = -1;
            sr_bgcolor.EditValue = null;
            cec.SetClearControls(srGroupBox5, new string[] { "" });
            cec.SetClearControls(srPanel7, new string[] { "" });

			txt_gcode.Enabled = true;
			cmb_gtype.Enabled = true;		
            srPanel7.Enabled = false;
            SetButtonEnable("1000");
            //SetButtonEnable("1111");    

            //df.GetMSTDUTYDatas(ref ds);
            //grd.DataSource = ds.Tables["MSTDUTY"];

            txt_gcode.Focus();
        }

        #endregion

        #region 1 Form

        private void duty1030_Load(object sender, EventArgs e)
        {
            SetCancel();
			df.GetSEARCH_MSTGNMUDatas(ds);
			grd1.DataSource = ds.Tables["SEARCH_MSTGNMU"];
            //btn_refresh_CK(ds.Tables["MSTDUTY"].ToString());
        }

        #endregion

        #region 2 Button

        //조회
        private void srButton1_Click(object sender, EventArgs e)
        {
            //df.GetMSTDUTYDatas(ref ds);
            //grd.DataSource = ds.Tables["MSTDUTY"];
        }

        //처리
        private void btn_proc_Click(object sender, EventArgs e)
        {
            if (isNoError_um(1))
            {
				//코드 자리수 0채우기.      2021-07-12. 10:40 근무유형코드가 그리 많지 않아서 Char(4)에서 (2)로 바꿈. 사장님께서도 확인. KHS.
				txt_gcode.Text = txt_gcode.Text.PadLeft(2, '0');

				//if (txt_gcode.Text.Length < txt_gcode.Properties.MaxLength)
				//{
				//    //txt_gcode.Text = txt_gcode.Text.PadRight(txt_gcode.Properties.MaxLength, '0');
				//    txt_gcode.Text = txt_gcode.Text.PadRight(txt_gcode.Properties.MaxLength, '2');  // 여기숫자도 4에서 2로. 
				//}

				//cec.SetEnabledStateinEachControls(srGroupBox5, true, new string[] { "txt_code" }, new string[] { "dat_lday" });

				// ★ 근무유형 약칭 한 글자만 들어가도록 할 건데, 이 부분도 이미 사용 중인지 확인이 필요함!! 
				
				srPanel7.Enabled = true;

				//왼쪽그리드에 이미 코드 있으면 정보 뿌리기
				df.GetDUTY_MSTGNMUDatas(txt_gcode.Text.ToString(), ds);
				if (ds.Tables["DUTY_MSTGNMU"].Select("G_CODE = '" + txt_gcode.Text.ToString().Trim() + "' ").Length > 0)
				{
					DataRow drow = ds.Tables["DUTY_MSTGNMU"].Select("G_CODE = '" + txt_gcode.Text.ToString().Trim() + "'")[0];

					if (drow["NS_CHK"].ToString() == "1")
					{
						cmb_gtype.Enabled = false;
						SetButtonEnable("1101");
					}
					else
					{
						cmb_gtype.Enabled = true;
						SetButtonEnable("1111");
					}
					txt_gcode.Text = drow["G_CODE"].ToString().Trim();
					txt_nam1.Text = drow["G_FNM"].ToString().Trim();
					txt_nam2.Text = drow["G_SNM"].ToString().Trim();
					txt_tmfr.Text = drow["G_FRTM"].ToString().Trim();
					txt_tmto.Text = drow["G_TOTM"].ToString().Trim();
					txt_holtm.Text = drow["G_WORK"].ToString();
					cmb_gtype.SelectedIndex = clib.TextToInt(drow["G_TYPE"].ToString()) - 1;
                    sr_bgcolor.Text = drow["G_COLOR"].ToString().Trim();	
					cmb_autoYN.EditValue = drow["AUTO_YN"].ToString();
					cmb_limit.EditValue = drow["REQ_YN"].ToString();
					cmb_dang.EditValue = drow["DANG_YN"].ToString();
					txt_yc_day.Text = drow["YC_DAY"].ToString();
					_Flag = "";
				}
				else
				{
					_Flag = "C"; //신규
					SetButtonEnable("1111");
				}
				txt_gcode.Enabled = false;
				txt_nam1.Focus();
			}
        }

        //저장버튼
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (isNoError_um(2))
            {
                Cursor = Cursors.WaitCursor;
                int outVal = 0;
                try
                {
                    DataRow hrow;

                    if (_Flag == "C")  //신규
                    {
                        hrow = ds.Tables["DUTY_MSTGNMU"].NewRow();
                        hrow["G_CODE"] = txt_gcode.Text.ToString().Trim();
                    }
                    else //수정       //GNTECODE ,GNTE_SNM ,GNTE_FNM ,DP_COLOR ,GNMUTMFR ,GNMUTMTO ,GN_HOLTM ,GNMUTYPE ,AUTO_YN ,REQ_LIMT
                    {
                        hrow = ds.Tables["DUTY_MSTGNMU"].Select("G_CODE = '" + txt_gcode.Text.ToString().Trim() + "'")[0];
                    }

                    //hrow["G_CODE"] = txt_gcode.Text.ToString().Trim();
					hrow["G_FNM"] = txt_nam1.Text.ToString().Trim();
                    hrow["G_SNM"] = txt_nam2.Text.ToString().Trim();
					hrow["G_FRTM"] = txt_tmfr.Text.ToString().Replace(":", "");
                    hrow["G_TOTM"] = txt_tmto.Text.ToString().Replace(":", "");
					hrow["G_WORK"] = clib.TextToDecimal(txt_holtm.Text.ToString());
                    hrow["G_TYPE"] = cmb_gtype.SelectedIndex + 1;
                    hrow["G_COLOR"] = sr_bgcolor.EditValue.ToString() == "0" ? -1 : sr_bgcolor.EditValue;
					//Color myColor = Color.FromArgb(clib.TextToInt(hrow["G_COLOR"].ToString()));
					//int R = myColor.R;
					//int G = myColor.G;
					//int B = myColor.B;
					//string hex = myColor.R.ToString("X2") + myColor.G.ToString("X2") + myColor.B.ToString("X2");
					hrow["G_RGB"] = sr_bgcolor.EditValue.ToString() == "0" ? "rgb(255,255,255)" : "rgb(" + sr_bgcolor.Color.R.ToString() + "," + sr_bgcolor.Color.G.ToString() + "," + sr_bgcolor.Color.B.ToString() + ")";
					hrow["G_HEXA"] = sr_bgcolor.Color.Name.Length < 8 ? "#ffffff" : ("#" + sr_bgcolor.Color.Name.Substring(2, 6));
                    hrow["AUTO_YN"] = cmb_autoYN.EditValue;
                    hrow["REQ_YN"] = cmb_limit.EditValue;        //신청제한여부
                    hrow["DANG_YN"] = cmb_dang.EditValue;
					hrow["YC_DAY"] = clib.TextToDecimal(txt_yc_day.Text.ToString());
                    hrow["USID"] = SilkRoad.Config.SRConfig.USID;

                    if (_Flag == "C")  //신규
                    {
                        hrow["INDT"] = gd.GetNow();
                        hrow["UPDT"] = "";
                        hrow["PSTY"] = "A";

                        ds.Tables["DUTY_MSTGNMU"].Rows.Add(hrow);
                    }
                    else //수정
                    {
                        hrow["UPDT"] = gd.GetNow();
                        hrow["PSTY"] = "U";
                    }

					string[] tableNames = new string[] { "DUTY_MSTGNMU" };
					SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
					outVal = cmd.setUpdate(ref ds, tableNames, null);

					if (outVal <= 0)
						MessageBox.Show("저장된 내용이 없습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
					else
						MessageBox.Show("저장되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ec)
                {
                    MessageBox.Show(ec.Message, "저장오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    btn_refresh.PerformClick();
                    SetCancel();
                    Cursor = Cursors.Default;
                }
            }
        }

        //삭제
        private void btn_del_Click(object sender, EventArgs e)
        {
			if (isNoError_um(3))
			{
				DialogResult dr = MessageBox.Show("해당 근무유형을 삭제하시겠습니까?", "삭제여부", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
				if (dr == DialogResult.OK)
				{
					int outVal = 0;
					try
					{
						ds.Tables["DUTY_MSTGNMU"].Select("G_CODE = '" + txt_gcode.Text.ToString().Trim() + "'")[0].Delete();

						string[] tableNames = new string[] { "DUTY_MSTGNMU" };
						SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
						outVal = cmd.setUpdate(ref ds, tableNames, null);

						if (outVal > 0)
							MessageBox.Show("해당 근무유형이 삭제되었습니다.", "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					catch (Exception ec)
					{
						MessageBox.Show(ec.Message, "삭제오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					finally
					{
						btn_refresh.PerformClick();
						SetCancel();
						Cursor = Cursors.Default;
					}
				}
			}
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

        /// <summary> refresh버튼 </summary>
        private void btn_refresh_Click(object sender, EventArgs e)
        {
			df.GetSEARCH_MSTGNMUDatas(ds);
			grd1.DataSource = ds.Tables["SEARCH_MSTGNMU"];
		}

        /// <summary>엑셀버튼</summary>
        private void btn_exel_Click(object sender, EventArgs e)
        {
			clib.gridToExcel(grdv1, this.Name + "_" + clib.DateToText(DateTime.Now));
		}

        #endregion

        #region 3 EVENT

        private void duty1030_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //취소
                btn_clear.PerformClick();            
        }
		
		//그리드 더블클릭시 코드 조회
		private void grdv1_DoubleClick(object sender, EventArgs e)
		{
			DataRow drow = grdv1.GetFocusedDataRow();
            if (drow == null)
                return;

			txt_gcode.Text = grdv1.GetFocusedRowCellValue("G_CODE").ToString();
			btn_proc.PerformClick();
		}		
		
		private void cmb_gtype_SelectedIndexChanged(object sender, EventArgs e)
		{
			txt_yc_day.Enabled = cmb_gtype.SelectedIndex == 7 ? true : false;
			if (cmb_gtype.SelectedIndex != 7)
				txt_yc_day.Text = "0";
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

			if (mode == 1)  //처리
			{
				if (txt_gcode.Text.ToString().Trim() == "")
				{
					MessageBox.Show(srLabel52.Text + "를 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					txt_gcode.Focus();
					return false;
				}
				else
				{
					isError = true;
				}
			}
			else if (mode == 2)  //저장
			{
				if (txt_nam1.Text.ToString().Trim() == "")
				{
					MessageBox.Show(srLabel7.Text + "을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					txt_nam1.Focus();
					return false;
				}
				else if (txt_nam2.Text.ToString().Trim() == "")
				{
					MessageBox.Show(srLabel2.Text + "을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					txt_nam2.Focus();
					return false;
				}
				//else if (ds.Tables["SEARCH_MSTGNMU"].Select("G_CODE <> '" + txt_gcode.Text.Trim() + "' and G_SNM = '" + txt_nam2.Text.ToString().Trim() + "'").Length > 0)
				//{
				//	MessageBox.Show(txt_nam2.Text.ToString().Trim() + " 약칭은 이미 사용중입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
				//	txt_nam2.Focus();
				//	return false;
				//}
				else if (Encoding.Default.GetByteCount(txt_nam1.Text.ToString().Trim()) > 40)
				{
					MessageBox.Show(srLabel7.Text + "의 길이가 40byte를 초과하였습니다.\r\n(현재 " + Encoding.Default.GetByteCount(txt_nam1.Text.ToString().Trim()) + "byte)", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					txt_nam1.Focus();
					return false;
				}
				else if (Encoding.Default.GetByteCount(txt_nam2.Text.ToString().Trim()) > 8)
				{
					MessageBox.Show(srLabel2.Text + "의 길이가 8byte를 초과하였습니다.\r\n(현재 " + Encoding.Default.GetByteCount(txt_nam2.Text.ToString().Trim()) + "byte)", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					txt_nam2.Focus();
					return false;
				}
				else
				{
					isError = true;
				}
			}
			else if (mode == 3)  //삭제
			{
				df.GetCHK_GNMUDatas(txt_gcode.Text.ToString(), ds);
				if (ds.Tables["CHK_GNMU"].Rows.Count > 0)
				{
					MessageBox.Show(txt_gcode.Text.ToString() + "[" + txt_nam1.Text.ToString() + "] 근무유형코드가 " + ds.Tables["CHK_GNMU"].Rows[0]["CHK_NM"].ToString() + " 테이블에서 사용중입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					txt_gcode.Focus();
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

        /// <summary>
        /// 배열에따른 버튼상태설정
        /// </summary>
        /// <param name="mode"></param>
        private void SetButtonEnable(string arr)
        {
            btn_proc.Enabled = arr.Substring(0, 1) == "1" ? true : false;
            btn_save.Enabled = arr.Substring(1, 1) == "1" ? true : false;
            btn_del.Enabled  = arr.Substring(2, 1) == "1" ? true : false;
            btn_clear.Enabled = arr.Substring(3, 1) == "1" ? true : false;
        }

        private void btn_search_CK()
        {
            
        }
		#endregion
	}
}

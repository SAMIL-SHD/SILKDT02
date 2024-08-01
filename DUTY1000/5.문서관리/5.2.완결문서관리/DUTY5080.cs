using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using DevExpress.XtraScheduler;
using System.Drawing;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace DUTY1000
{
    public partial class duty5080 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();

        int admin_lv = 0;

        public duty5080()
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
            if (ds.Tables["5080_AP_YCHG_LIST"] != null)
                ds.Tables["5080_AP_YCHG_LIST"].Clear();
            grd_ap.DataSource = null;
        }

		private void END_CHK()
		{
			//string yymm = clib.DateToText(dat_yymm.DateTime).Substring(0, 4) + "." + clib.DateToText(dat_yymm.DateTime).Substring(4, 2);
			//df.Get2020_SEARCH_ENDSDatas(clib.DateToText(dat_yymm.DateTime).Substring(0, 6), ds);
			//if (ds.Tables["2020_SEARCH_ENDS"].Rows.Count > 0) //마감월이 저장되어 있으면
			//{
			//	DataRow irow = ds.Tables["2020_SEARCH_ENDS"].Rows[0];
			//	ends_yn = irow["CLOSE_YN"].ToString();
			//	lb_ends.Text = irow["CLOSE_YN"].ToString() == "Y" ? "[" + yymm + " 최종마감 완료]" : irow["CLOSE_YN"].ToString() == "N" ? "[" + yymm + " 최종마감 취소]" : "[ ]";
			//}
			//else
			//{
			//	ends_yn = "";
			//	lb_ends.Text = "[" + yymm + " 최종마감 작업전]";
			//}
		}

        #endregion

        #region 1 Form

        private void duty5080_Load(object sender, EventArgs e)
        {			
			dat_year.DateTime = DateTime.Now;
			dat_year2.DateTime = DateTime.Now;

			if (SilkRoad.Config.ACConfig.G_MSYN == "1" || SilkRoad.Config.SRConfig.USID == "SAMIL")
			{
				btn_ap_canc.Visible = true;
				col_c_chk.Visible = true;
			}
        }
		
		private void duty5080_Shown(object sender, EventArgs e)
        {
            df.GetMSTUSER_CHKDatas(ds);  //부서관리여부
            if (ds.Tables["MSTUSER_CHK"].Rows.Count > 0)
                admin_lv = clib.TextToInt(ds.Tables["MSTUSER_CHK"].Rows[0]["EMBSADGB"].ToString()); //권한레벨

            if (SilkRoad.Config.ACConfig.G_MSYN == "1" || SilkRoad.Config.SRConfig.USID == "SAMIL")
            {
                admin_lv = 3;
                lb_power.Text = "전체관리 권한";
            }
            else
            {
                lb_power.Text = "";
            }
            //else if (admin_lv == 1)
            //{
            //    lb_power.Text = "부서조회 권한";
            //}
            //else
            //{
            //    lb_power.Text = "조회권한 없음";
            //}

            proc();
		}

        #endregion

        #region 2 Button	
		
		//승인내역 조회
		private void btn_ap_search_Click(object sender, EventArgs e)
        {
            if (isNoError_um(1))
            {
                proc();
                if (ds.Tables["5080_AP_YCHG_LIST"].Rows.Count == 0)
                    MessageBox.Show("조회된 연차/휴가 내역이 없습니다!", "결재", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void proc()
        {
            string fr_yy = clib.DateToText(dat_year.DateTime).Substring(0, 4);
            string to_yy = clib.DateToText(dat_year2.DateTime).Substring(0, 4);
            df.Get5080_AP_YCHG_LISTDatas(fr_yy + to_yy, cmb_type.SelectedIndex, cmb_ap.SelectedIndex, admin_lv, ds);
            grd_ap.DataSource = ds.Tables["5080_AP_YCHG_LIST"];            
        }
        //승인내역 조회clear
        private void btn_ap_clear_Click(object sender, EventArgs e)
		{
			SetCancel();
		}

        //연차,휴가 선택삭제
        private void btn_ap_canc_Click(object sender, EventArgs e)
		{
			if (isNoError_um(2))
            {
                Cursor = Cursors.WaitCursor;
                int outVal = 0;
                try
                {
					for (int i = 0; i < ds.Tables["5080_AP_YCHG_LIST"].Rows.Count; i++)
					{
						DataRow crow = ds.Tables["5080_AP_YCHG_LIST"].Rows[i];
						if (crow["CHK"].ToString() == "1")
						{
							string tb_nm = crow["TYPE"].ToString() == "1" ? "DUTY_TRSHREQ" : crow["TYPE"].ToString() == "2" ? "DUTY_TRSJREQ" : "DUTY_TRSTREQ";
							df.Get5060_DUTY_TRSHREQDatas(crow["SEQNO"].ToString(), tb_nm, ds);
                            if (ds.Tables[tb_nm].Rows.Count > 0)
                            {
                                DataRow drow = ds.Tables[tb_nm].Rows[0];
                                drow["PSTY"] = "D";
                                drow["UPDT"] = gd.GetNow();
                                drow["USID"] = SilkRoad.Config.SRConfig.USID;

                                string[] tableNames = new string[] { tb_nm };
                                SilkRoad.DbCmd_DT02.DbCmd_DT02 cmd = new SilkRoad.DbCmd_DT02.DbCmd_DT02();
                                outVal += cmd.setUpdate(ref ds, tableNames, null);
                            }					
						}
					}
                }
                catch (Exception ec)
                {
                    MessageBox.Show(ec.Message, "저장오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (outVal > 0)
                    {
                        MessageBox.Show(outVal + "건의 선택된 내역이 삭제처리 되었습니다.", "삭제성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        int iTopRow = grdv_ap.TopRowIndex;
                        proc();
                        grdv_ap.TopRowIndex = iTopRow;
                    }
                    Cursor = Cursors.Default;
                }
            }
		}

        #endregion

        #region 3 EVENT
		
		//메뉴 활성화시
		private void duty5080_Activated(object sender, EventArgs e)
		{
			Page_Refresh();
		}

		private void Page_Refresh()
		{
            proc();
        }

        private void grdv_ap_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            DataRow srow = grdv_ap.GetFocusedDataRow();

            if (e.Button == MouseButtons.Right)
            {
                if ("5,8".Contains(srow["AP_TAG"].ToString()))
                {
                    int X = Cursor.Position.X;
                    int Y = Cursor.Position.Y;
                    contextMenuStrip1.Show(X, Y);
                }
            }
        }
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            DataRow drow = grdv_ap.GetFocusedDataRow();
            string seqno = drow["SEQNO"].ToString();
            string tb_nm = drow["TYPE"].ToString() == "1" ? "DUTY_TRSHREQ" : drow["TYPE"].ToString() == "2" ? "DUTY_TRSJREQ" : "DUTY_TRSTREQ";
            
            if (e.ClickedItem.ToString() == "미리보기")
            {
                print(1, drow["GUBN"].ToString(), seqno, tb_nm);
            }
            else if (e.ClickedItem.ToString() == "인쇄")
            {
                print(2, drow["GUBN"].ToString(), seqno, tb_nm);
            }
            else if (e.ClickedItem.ToString() == "결재라인수정")
            {
                string tb_nm2 = drow["TYPE"].ToString() == "1" ? "DUTY_TRSHREQ_DT" : drow["TYPE"].ToString() == "2" ? "DUTY_TRSJREQ_DT" : "DUTY_TRSTREQ_DT";
                duty5081 s = new duty5081(tb_nm2, seqno);
                s.ShowDialog();
            }
        }

        private void print(int stat, string gubn, string seqno, string tb_nm)
        {
            df.Get5080_REPORTDatas(seqno, tb_nm, ds);
            if (ds.Tables["5080_REPORT"].Rows.Count > 0)
            {
                if (gubn == "C")  //신청
                {
                    rpt_5081 rpt = new rpt_5081(ds.Tables["5080_SIGN"], ds);
                    rpt.DataSource = ds.Tables["5080_REPORT"];
                    if (stat == 1)
                        rpt.ShowPreview();
                    else
                        rpt.Print();
                }
                else  //철회
                {
                    rpt_5082 rpt = new rpt_5082(ds.Tables["5080_SIGN"], ds);
                    rpt.DataSource = ds.Tables["5080_REPORT"];
                    if (stat == 1)
                        rpt.ShowPreview();
                    else
                        rpt.Print();
                }
            }
            else
            {
                string msg_nm = stat == 1 ? "미리보기" : "인쇄";
                MessageBox.Show(msg_nm + "할 내역이 없습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //else
            //{
            //	DataTable table = ds.Tables["5080_AP_YCHG_LIST"].Clone();
            //	for (int i = 0; i < grdv_ap.RowCount; i++)
            //	{
            //		if (grdv_ap.GetVisibleRowHandle(i) > -1)
            //			table.ImportRow(grdv_ap.GetDataRow(grdv_ap.GetVisibleRowHandle(i)));
            //	}
            //}
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            DataRow srow = grdv_ap.GetFocusedDataRow();

            string photo_nm = srow["ADD_PHOTO"].ToString();
            string dn_Path = Application.StartupPath + "\\DN_FILE\\" + photo_nm;
            string type = srow["TYPE"].ToString() == "1" ? "HREQ" : "JREQ";
            string year = srow["REQ_DATE"].ToString().Substring(0, 4);

            DialogResult dr = MessageBox.Show("해당 첨부파일을 다운로드 하시겠습니까?", "확인", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Cancel)
                return;

            try
            {
                if (!Directory.Exists(Application.StartupPath + "\\DN_FILE"))
                    Directory.CreateDirectory(Application.StartupPath + "\\DN_FILE");

                FileInfo fd = new FileInfo(dn_Path);

                if (!fd.Exists)  //파일이 존재하지 않으면 다운로드
                {
                    string uri = "http://" + SilkRoad.DAL.DataAccess.DBhost.Replace(",9245", "") + ":8080/image/" + type +"/" + year + "/" + photo_nm;
                    HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(uri);
                    HttpWebResponse ws = (HttpWebResponse)wr.GetResponse();
                    Stream str = ws.GetResponseStream();
                    byte[] inBuf = new byte[100000];
                    int bytesToRead = (int)inBuf.Length;
                    int bytesRead = 0;
                    while (bytesToRead > 0)
                    {
                        int n = str.Read(inBuf, bytesRead, bytesToRead);
                        if (n == 0)
                            break;
                        bytesRead += n;
                        bytesToRead -= n;
                    }

                    FileStream fstr = new FileStream(dn_Path, FileMode.OpenOrCreate, FileAccess.Write);
                    fstr.Write(inBuf, 0, bytesRead); //다운로드완료.
                    str.Close();
                    fstr.Close();
                }
                dr = MessageBox.Show("다운로드된 파일 위치에서 여시겠습니까?", "확인", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                    Process.Start(Application.StartupPath + "\\DN_FILE\\");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "에러", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        private void grdv_ap_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
		{			
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)            
                e.Info.DisplayText = (e.RowHandle + 1).ToString();   
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

			if (mode == 1)  //연차,휴가조회
			{
                if (admin_lv == 0)
                {
                    MessageBox.Show("조회권한이 없습니다. 인사기본관리의 관리자구분을 확인하세요!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if(clib.DateToText(dat_year.DateTime) == "")
				{
					MessageBox.Show("조회년월(시작)이 입력되지 않았습니다!", "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
					dat_year.Focus();
					return false;
				}
				else if (clib.DateToText(dat_year2.DateTime) == "")
				{
					MessageBox.Show("조회년월(종료)가 입력되지 않았습니다!", "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
					dat_year2.Focus();
					return false;
				}
				else
				{
					isError = true;
				}
            }
            else if (mode == 2)  //승인취소
            {
                if (ds.Tables["5080_AP_YCHG_LIST"] != null)
                {
                    ds.Tables["5080_AP_YCHG_LIST"].AcceptChanges();
                    if (ds.Tables["5080_AP_YCHG_LIST"].Select("CHK='1'").Length == 0)
                    {
                        MessageBox.Show("선택된 내역이 없습니다!", "취소에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else
                    {
                        isError = true;
                    }
                }
            }
            return isError;
        }

        #endregion

        #region 9. ETC

        #endregion

    }
}

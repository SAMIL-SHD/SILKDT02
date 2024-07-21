using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using DevExpress.XtraScheduler;
using System.Drawing;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace DUTY1000
{
	public partial class duty5060 : SilkRoad.Form.Base.FormX
	{
		CommonLibrary clib = new CommonLibrary();

		ClearNEnableControls cec = new ClearNEnableControls();
		public DataSet ds = new DataSet();
		DataProcFunc df = new DataProcFunc();
		SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();

        int admin_lv = 0;
        public duty5060()
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
			if (ds.Tables["5060_AP_YCHG_LIST"] != null)
				ds.Tables["5060_AP_YCHG_LIST"].Clear();
			grd_ap.DataSource = null;			
		}

		#endregion

		#region 1 Form

		private void duty5060_Load(object sender, EventArgs e)
		{
            btn_ap_save.Visible = SilkRoad.Config.SRConfig.USID == "SAMIL" || SilkRoad.Config.ACConfig.G_MSYN == "1" ? true : false;
            btn_ap_canc.Visible = SilkRoad.Config.SRConfig.USID == "SAMIL" || SilkRoad.Config.ACConfig.G_MSYN == "1" ? true : false;
            col_chk.Visible = SilkRoad.Config.SRConfig.USID == "SAMIL" || SilkRoad.Config.ACConfig.G_MSYN == "1" ? true : false;
        }

		private void duty5060_Shown(object sender, EventArgs e)
		{
            df.GetMSTUSER_CHKDatas(ds);  //부서관리여부
            if (ds.Tables["MSTUSER_CHK"].Rows.Count > 0)
                admin_lv = clib.TextToInt(ds.Tables["MSTUSER_CHK"].Rows[0]["EMBSADGB"].ToString()); //권한레벨

            if (SilkRoad.Config.ACConfig.G_MSYN == "1" || SilkRoad.Config.SRConfig.USID == "SAMIL")
            {
                admin_lv = 3;
                lb_power.Text = "전체관리 권한";
            }
            else if (admin_lv == 1)
            {
                lb_power.Text = "부서조회 권한";
            }
            else
            {
                lb_power.Text = "조회권한 없음";
            }

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
                if (ds.Tables["5060_AP_YCHG_LIST"].Rows.Count == 0)
                    MessageBox.Show("결재할 연차/휴가 내역이 없습니다!", "결재", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void proc()
        {
            df.Get5060_AP_YCHG_LISTDatas(admin_lv, ds);
            grd_ap.DataSource = ds.Tables["5060_AP_YCHG_LIST"];            
        }
        //승인내역 조회clear
        private void btn_ap_clear_Click(object sender, EventArgs e)
		{
			SetCancel();
		}
		//일괄승인
		private void btn_ap_save_Click(object sender, EventArgs e)
		{
			if (isNoError_um(2))
			{
				Cursor = Cursors.WaitCursor;
				int outVal = 0;
				try
				{
					for (int i = 0; i < ds.Tables["5060_AP_YCHG_LIST"].Rows.Count; i++)
					{
						DataRow drow = ds.Tables["5060_AP_YCHG_LIST"].Rows[i];
						if (drow["CHK"].ToString() == "1")
						{
							string tb_nm = drow["TYPE"].ToString() == "1" ? "DUTY_TRSHREQ" : "DUTY_TRSJREQ";
							df.Get5060_DUTY_TRSHREQDatas(drow["SEQNO"].ToString(), tb_nm, ds);
							if (ds.Tables[tb_nm].Rows.Count > 0)
							{
								DataRow hrow = ds.Tables[tb_nm].Rows[0];
								if ("1,3".Contains(hrow["AP_TAG"].ToString()))
								{																		
									if (SilkRoad.Config.SRConfig.USID == "SAMIL" || SilkRoad.Config.ACConfig.G_MSYN == "1")
									{
										hrow["AP_DT"] = gd.GetNow();
										hrow["AP_USID"] = SilkRoad.Config.SRConfig.USID;
										hrow["AP_TAG"] = "8";
									}
									string[] tableNames = new string[] { tb_nm };
									SilkRoad.DbCmd_DT02.DbCmd_DT02 cmd = new SilkRoad.DbCmd_DT02.DbCmd_DT02();
									outVal += cmd.setUpdate(ref ds, tableNames, null);
								}
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
                        MessageBox.Show(outVal + "건의 선택된 내역이 승인처리 되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        proc();
                    }
					Cursor = Cursors.Default;
				}
			}
		}
		//일괄취소
		private void btn_ap_canc_Click(object sender, EventArgs e)
		{
			if (isNoError_um(2))
			{
				Cursor = Cursors.WaitCursor;
				int outVal = 0;
				try
				{
					for (int i = 0; i < ds.Tables["5060_AP_YCHG_LIST"].Rows.Count; i++)
					{
						DataRow drow = ds.Tables["5060_AP_YCHG_LIST"].Rows[i];
						if (drow["CHK"].ToString() == "1")
						{
							string tb_nm = drow["TYPE"].ToString() == "1" ? "DUTY_TRSHREQ" : "DUTY_TRSJREQ";
							df.Get5060_DUTY_TRSHREQDatas(drow["SEQNO"].ToString(), tb_nm, ds);
							if (ds.Tables[tb_nm].Rows.Count > 0)
							{
								DataRow hrow = ds.Tables[tb_nm].Rows[0];
                                if ("1,3".Contains(hrow["AP_TAG"].ToString()))
                                {
                                    if (SilkRoad.Config.SRConfig.USID == "SAMIL" || SilkRoad.Config.ACConfig.G_MSYN == "1")
                                    {
                                        hrow["RT_DT"] = gd.GetNow();
                                        hrow["RT_USID"] = SilkRoad.Config.SRConfig.USID;
                                        hrow["AP_TAG"] = "2";
                                    }
                                    string[] tableNames = new string[] { tb_nm };
                                    SilkRoad.DbCmd_DT02.DbCmd_DT02 cmd = new SilkRoad.DbCmd_DT02.DbCmd_DT02();
                                    outVal += cmd.setUpdate(ref ds, tableNames, null);
                                }
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
                        MessageBox.Show(outVal + "건의 선택된 내역이 취소처리 되었습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        proc();
                    }
					Cursor = Cursors.Default;
				}
			}
		}

		#endregion

		#region 3 EVENT
        
		//메뉴 활성화시
		private void duty5060_Activated(object sender, EventArgs e)
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
                int X = Cursor.Position.X;
                int Y = Cursor.Position.Y;

                string sldt = srow["SEQNO"].ToString();
                string type = srow["TYPE"].ToString();
                string tb_nm = type == "1" ? "DUTY_TRSHREQ" : "DUTY_TRSJREQ";

                df.Get5060_DUTY_TRSHREQDatas(srow["SEQNO"].ToString(), tb_nm, ds);
                if (ds.Tables[tb_nm].Rows.Count > 0)
                {
                    //상태가 1,3 이고 다음결재자가 접속자일때
                    if ("1,3".Contains(ds.Tables[tb_nm].Rows[0]["AP_TAG"].ToString()) && ds.Tables[tb_nm].Rows[0]["LINE_SABN"].ToString().Trim() == SilkRoad.Config.SRConfig.USID.ToString().Trim())
                    {
                        결재ToolStripMenuItem.Visible = true;
                        반려ToolStripMenuItem.Visible = true;
                    }
                    else
                    {
                        결재ToolStripMenuItem.Visible = false;
                        반려ToolStripMenuItem.Visible = false;
                    }

                    //상태가 1이고 상신자가 접속자일때
                    if ("1".Contains(ds.Tables[tb_nm].Rows[0]["AP_TAG"].ToString()) && ds.Tables[tb_nm].Rows[0]["LINE_SABN"].ToString().Trim() == SilkRoad.Config.SRConfig.USID.ToString().Trim())                    
                        취소ToolStripMenuItem.Visible = true;
                    else                    
                        취소ToolStripMenuItem.Visible = false;
                    
                    contextMenuStrip1.Show(X, Y);
                }
            }
        }
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            DataRow drow = grdv_ap.GetFocusedDataRow();
            string seqno = drow["SEQNO"].ToString();
            string tb_nm = drow["TYPE"].ToString() == "1" ? "DUTY_TRSHREQ" : "DUTY_TRSJREQ";

            int iTopRow = grdv_ap.TopRowIndex;
            if (e.ClickedItem.ToString() == "결재")
            {
                df.Get5060_PROCDatas("A", seqno, tb_nm, ds);
            }
            else if (e.ClickedItem.ToString() == "반려")
            {
                df.Get5060_PROCDatas("B", seqno, tb_nm, ds);
            }
            else if (e.ClickedItem.ToString() == "취소")
            {
                df.Get5060_PROCDatas("C", seqno, tb_nm, ds);
            }
            proc();
            grdv_ap.TopRowIndex = iTopRow;
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            DataRow srow = grdv_ap.GetFocusedDataRow();

            string photo_nm = srow["ADD_PHOTO"].ToString();
            string dn_Path = Application.StartupPath + "\\DN_FILE\\" + photo_nm;

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
                    string uri = "http://" + SilkRoad.DAL.DataAccess.DBhost.Replace(",9245", ":8080") + "/image/" + photo_nm;
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
                throw new Exception("오류가 발생했습니다", ex);
            }
        }
        private void grdv_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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
            
            if (mode == 1)  //조회
            {
                if (admin_lv == 0)
                {
                    MessageBox.Show("조회권한이 없습니다. 인사기본관리의 관리자구분을 확인하세요!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    isError = true;
                }
            }
            else if(mode == 2)  //일괄승인, 일괄취소
            {
				if (ds.Tables["5060_AP_YCHG_LIST"] != null)
				{
					ds.Tables["5060_AP_YCHG_LIST"].AcceptChanges();
					if (ds.Tables["5060_AP_YCHG_LIST"].Select("CHK='1'").Length == 0)
					{
						MessageBox.Show("선택된 내역이 없습니다!", "승인에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

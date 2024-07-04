using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using System.Text;
using System.ComponentModel;
using System.IO;
using ExcelDataReader;


namespace DUTY1000
{
    public partial class duty1050 : SilkRoad.Form.Base.FormX
    {
        static DataProcessing dp = new DataProcessing();
        CommonLibrary clib = new CommonLibrary();
        static string wagedb = "WG06DB" + SilkRoad.Config.SRConfig.WorkPlaceNo;

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();
        string _Flag = "";
		private static bool ErrorFlag = false;
        
        //SilkRoad.BaseCode.BaseCode bc = new SilkRoad.BaseCode.BaseCode();

        public duty1050()
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
            
            if (ds.Tables["DUTY_MSTNURS"] != null)            
                ds.Tables["DUTY_MSTNURS"].Clear();            

            grdv.ActiveFilter.Clear();
            grdv.SortInfo.Clear();
            
            grdv.FocusedRowHandle = -1;
            cec.SetClearControls(srPanel9, new string[] { "" });
			
			lblDept.Text = "-";
			//lblPart.Text = "-";
            lblName.Text = "-";
			lblSano.Text = "-";
			lblHpno.Text = "-";
			lblEmil.Text = "-";
						
			btn_save.Text = "추 가";
			btn_save.Image = Properties.Resources.저장;
            SetBaseData();
			SetButtonEnable("000");
		}
        #endregion

        #region 1 Form

        private void duty1050_Load(object sender, EventArgs e)
        {
            SetCancel();   
			  
			try
			{
				// 작업 취소를 사용
				bgWorker.WorkerSupportsCancellation = true;

				// 작업 진행률 업데이트 보고를 사용
				bgWorker.WorkerReportsProgress = true;
				btn_all_save.Enabled = true;
				btn_save_canc.Enabled = false;
			}
			catch (Exception ex)
			{
				throw new Exception("오류가 발생했습니다", ex);
			}      
        }

        #endregion

        //기본 룩업 데이터 설정
        private void SetBaseData()
        {
            df.GetSEARCH_DEPTDatas(ds); // 부서 리스트 룩업
            sl_dept.Properties.DataSource = ds.Tables["SEARCH_DEPT"];
        }

        #region 2 Button
		

        //왼쪽 간호사 리스트 조회
        private void btn_search_Click(object sender, EventArgs e)
        {
            btn_search_CK();
			if (ds.Tables["SEARCH_MSTNURS"].Rows.Count == 0)
				MessageBox.Show("조회할 내역이 없습니다.", "없음", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

		//일괄등록
		private void btn_all_save_Click(object sender, EventArgs e)
		{
			if (ds.Tables["SEARCH_MSTNURS"] != null)
			{
				string t_cnt = ds.Tables["SEARCH_MSTNURS"].Select("GUBN='2'").Length.ToString();
				DialogResult dr = MessageBox.Show("Duty관리에 미등록된 간호사 " + t_cnt + "명을 등록하시겠습니까?", "등록여부", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
				if (dr == DialogResult.OK)
				{
					try
					{
						btn_all_save.Enabled = false;
						btn_save_canc.Enabled = true;
						btn_info.Enabled = false;
						btn_exel.Enabled = false;
						srPanel4.Enabled = false;

						this.Cursor = Cursors.WaitCursor;

						marqueeProgressBarControl1.Visible = true;
						marqueeProgressBarControl1.Properties.Stopped = false;
						// 비동기로 작업을 시작합니다. DoWork 이벤트를 발생
						this.bgWorker.RunWorkerAsync(null);
					}
					catch (Exception ex)
					{
						throw new Exception("오류가 발생했습니다", ex);
					}
				}
			}
		}
		
		//일괄등록취소
		private void btn_save_canc_Click(object sender, EventArgs e)
		{
			try
			{
				// 작업 취소요청
				this.bgWorker.CancelAsync();
			}
			catch (Exception ex)
			{
				throw new Exception("오류가 발생했습니다", ex);
			}
			finally
			{
				btn_search_CK();
			}
		}

		//간호사 직종설정
		private void btn_info_Click(object sender, EventArgs e)
		{
			duty3011 s = new duty3011();
			s.ShowDialog();
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
					df.GetDUTY_MSTNURSDatas(lblSano.Text.ToString().Trim(), ds);
					if (ds.Tables["DUTY_MSTNURS"].Rows.Count > 0)
					{
						_Flag = "U";
						hrow = ds.Tables["DUTY_MSTNURS"].Rows[0];
					}
					else                    
					{
						_Flag = "C";
						hrow = ds.Tables["DUTY_MSTNURS"].NewRow();
					}
                    
                    hrow["SAWON_NO"] = lblSano.Text.ToString().Trim();
					hrow["SAWON_NM"] = lblName.Text.ToString().Trim();
                    hrow["ALLOWOFF"] = clib.TextToInt(cmb_allowoff.SelectedIndex.ToString());
                    hrow["LIMIT_OFF"] = clib.TextToInt(cmb_limitoff.SelectedIndex.ToString());

					hrow["STAT"] = cmb_stat.SelectedIndex + 1;
					hrow["LDAY"] = clib.DateToText(dat_lday.DateTime);                          
                    hrow["USID"] = SilkRoad.Config.SRConfig.USID;

                    if (_Flag == "C")  //신규
                    {
                        hrow["INDT"] = gd.GetNow();
                        hrow["UPDT"] = " ";
                        hrow["PSTY"] = "A";

                        ds.Tables["DUTY_MSTNURS"].Rows.Add(hrow);
                    }
                    else //수정
                    {
                        hrow["UPDT"] = gd.GetNow();
                        hrow["PSTY"] = "U";
                    }

					string[] tableNames = new string[] { "DUTY_MSTNURS" };
					SilkRoad.DbCmd_DT02.DbCmd_DT02 cmd = new SilkRoad.DbCmd_DT02.DbCmd_DT02();
					outVal = cmd.setUpdate(ref ds, tableNames, null);

					if (outVal <= 0)                    
                        MessageBox.Show("저장된 내용이 없습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                    else                    
                        MessageBox.Show("해당 간호사 정보가 저장되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                }
                catch (Exception ec)
                {
                    MessageBox.Show(ec.Message, "저장오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    SetCancel();
                    Cursor = Cursors.Default;
                    btn_search_CK();
                }
            }
        }

        //삭제버튼
        private void btn_del_Click(object sender, EventArgs e)
        {            
            DialogResult dr = MessageBox.Show("해당 간호사 정보를 삭제하시겠습니까?", "삭제여부", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                int outVal = 0;
                try
                {
					df.GetDUTY_MSTNURSDatas(lblSano.Text.ToString().Trim(), ds);
					if (ds.Tables["DUTY_MSTNURS"].Rows.Count > 0)
					{
						ds.Tables["DUTY_MSTNURS"].Select("SAWON_NO = '" + lblSano.Text.ToString().Trim() + "'")[0].Delete();

						string[] tableNames = new string[] { "DUTY_MSTNURS" };
						SilkRoad.DbCmd_DT02.DbCmd_DT02 cmd = new SilkRoad.DbCmd_DT02.DbCmd_DT02();
						outVal = cmd.setUpdate(ref ds, tableNames, null);

						if (outVal > 0)						
							MessageBox.Show("해당 간호사 정보가 삭제되었습니다.", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);						
					}
					else
					{
						MessageBox.Show("해당 간호사 정보는 이미 삭제되었습니다.", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
                catch (Exception ec)
                {
                    MessageBox.Show(ec.Message, "삭제오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    SetCancel();
                    Cursor = Cursors.Default;
                    btn_search_CK();
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

        /// <summary>엑셀버튼</summary>
        private void btn_exel_Click(object sender, EventArgs e)
        {
			clib.gridToExcel(grdv, this.Text + "(" + this.Name + ")_" + clib.DateToText(DateTime.Now), true);
        }

        #endregion


        #region 3 EVENT
		
        private void btn_search_CK()    // 간호사 조회
        {
            string dept = sl_dept.EditValue == null ? "%" : sl_dept.EditValue.ToString();
            string stat = cmb_sstat.SelectedIndex == 0 ? "%" : cmb_sstat.SelectedIndex.ToString();

            //string part = sl_dept.EditValue == null ? "%" : sl_dept.EditValue.ToString();
            df.GetSEARCH_MSTNURSDatas(dept, stat, ds);
            		
			this.Invoke(new Action(delegate ()
			{
				grd.DataSource = ds.Tables["SEARCH_MSTNURS"];
			}));
        }
		
		//더블클릭시 등록,수정
		private void grdv_DoubleClick(object sender, EventArgs e)
		{
			DataRow drow2 = grdv.GetFocusedDataRow();
            if (drow2 == null)
                return;

			if (drow2["GUBN"].ToString() == "2")
			{
				_Flag = "C";
				btn_save.Text = "추 가";
				btn_save.Image = Properties.Resources.저장;
				SetButtonEnable("101");
			}
			else
			{
				_Flag = "U";
				btn_save.Text = "수 정";
				btn_save.Image = Properties.Resources.수정;
				SetButtonEnable("111");
			}
			
			lblDept.Text = drow2["DEPT_NM"].ToString();
			//lblPart.Text = drow["PARTNAME"].ToString();
            lblName.Text = drow2["SAWON_NM"].ToString();
			lblSano.Text = drow2["SAWON_NO"].ToString();
			lblHpno.Text = drow2["HPNO"].ToString();
			lblEmil.Text = drow2["EMAIL_ID"].ToString();
			
			df.GetDUTY_MSTNURSDatas(lblSano.Text.ToString().Trim(), ds);
			if (ds.Tables["DUTY_MSTNURS"].Rows.Count > 0)
			{
				DataRow drow = ds.Tables["DUTY_MSTNURS"].Rows[0];
				cmb_allowoff.SelectedIndex = clib.TextToInt(drow["ALLOWOFF"].ToString());
				cmb_limitoff.SelectedIndex = clib.TextToInt(drow["LIMIT_OFF"].ToString());

				cmb_stat.SelectedIndex = clib.TextToInt(drow["STAT"].ToString()) < 1 ? 0 : clib.TextToInt(drow["STAT"].ToString()) - 1;
				if (drow["LDAY"].ToString() != "")
					dat_lday.DateTime = clib.TextToDate(drow["LDAY"].ToString());
			}
			else
			{
				cmb_allowoff.SelectedIndex = 9;
				cmb_limitoff.SelectedIndex = 3;
				cmb_stat.SelectedIndex = 0;
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
            bool isError = false;

            if (mode == 1)  //처리 :신규로 등록할때 => 간호사 신규등록은 다른 화면에서 해야할 듯. 여기는 그냥 수정 저장만. 
            {
                if (sl_dept.EditValue.ToString().Trim() == "")
                {
                    MessageBox.Show( "팀을 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sl_dept.Focus();
                    return false;
                }
                else
                {
                    isError = true;
                }
            }
            else if (mode == 2)  //저장
            {
                if (lblSano.Text == null)
                {
                    MessageBox.Show("직원을 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
				
		#region 8. BackgroundWorker
		
		private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			this.DoWork((BackgroundWorker)sender, e);
			e.Result = null;			
		}
		private void DoWork(BackgroundWorker worker, DoWorkEventArgs e)
		{
			#region 예제
			/*
			* 실행중인 Form 과 다른 쓰레드에서 동작하므로
			* 처리할 메서드에서는 UI 객체의 속성값(Value, Text 등..)을 사용하지 못합니다.
			*
			* 작업에 필요한 값은 매개변수로 전달받아야 하고 UI객체의 상태를 변화시킬 필요가 있는 경우
			* ProgressChanged
			* RunWorkerCompleted
			* 이벤트를 사용해야 합니다.
			*/
			//double nMax = 50.0;
			//int nExe = 0;
			//while (nMax > nExe)
			//{
			//	// 작업이 취소 요청이 되었는지 검사
			//	if (worker.CancellationPending)
			//	{
			//		e.Cancel = true;
			//		break;
			//	}
			//	else
			//	{
			//		if (ErrorFlag)
			//		{
			//			ErrorFlag = false;
			//			throw new Exception("예외 발생 시뮬레이션");
			//		}

			//		// 시간이 걸리는 작업을 실행합니다.
			//		System.Threading.Thread.Sleep(200);
			//		nExe++;

			//		// 진행률 업데이트하기 위해 ProgressChanged 이벤트를 발생시킵니다.
			//		worker.ReportProgress((int)((nExe / nMax) * 100));
			//		//marqueeProgressBarControl1.Text = "일괄등록중...(" + nExe.ToString() + "/" + nMax.ToString() + ")";

			//		this.Invoke(new Action(delegate ()
			//		{
			//			marqueeProgressBarControl1.Text = "일괄등록중...(" + nExe.ToString() + "/" + nMax.ToString() + ")";
			//		}));
			//	}
			//}
			#endregion

			int outVal = 0;
			int cnt = 0;
			try
			{
				string t_cnt = ds.Tables["SEARCH_MSTNURS"].Select("GUBN='2'").Length.ToString();
				for (int i = 0; i < ds.Tables["SEARCH_MSTNURS"].Rows.Count; i++)
				{
					DataRow drow = ds.Tables["SEARCH_MSTNURS"].Rows[i];
					if (drow["GUBN"].ToString().Trim() == "2")
					{
						//df.GetDUTY_MSTNURSDatas(lblSano.Text.ToString().Trim(), ds);
                        df.GetDUTY_MSTNURSDatas(drow["SAWON_NO"].ToString().Trim(), ds);
                        if (ds.Tables["DUTY_MSTNURS"].Rows.Count == 0)
						{
							_Flag = "C";
							string sawon = drow["SAWON_NO"].ToString().Trim();
							DataRow hrow = ds.Tables["DUTY_MSTNURS"].NewRow();
							hrow["SAWON_NO"] = drow["SAWON_NO"].ToString().Trim();
							hrow["SAWON_NM"] = drow["SAWON_NM"].ToString().Trim();
							hrow["ALLOWOFF"] = 9;
							hrow["LIMIT_OFF"] = 3;
                            hrow["STAT"] = 1;
							hrow["LDAY"] = "";
							hrow["USID"] = SilkRoad.Config.SRConfig.USID;
							hrow["INDT"] = gd.GetNow();
							hrow["UPDT"] = "";
							hrow["PSTY"] = "A";

							ds.Tables["DUTY_MSTNURS"].Rows.Add(hrow);
							
							//string[] UpQry = { "UPDATE " + wagedb + ".dbo.MSTEMBS SET EMBSPSWD=LEFT(RTRIM(cast(DECRYPTBYPASSPHRASE('samilpas',EMBSPTSA) as varchar(100))),6) WHERE EMBSSABN = '" + sawon + "'" };

							string[] tableNames = new string[] { "DUTY_MSTNURS" };
							SilkRoad.DbCmd_DT02.DbCmd_DT02 cmd = new SilkRoad.DbCmd_DT02.DbCmd_DT02();
                            outVal += cmd.setUpdate(ref ds, tableNames, null); // UpQry);
							cnt++;
						}
						this.Invoke(new Action(delegate ()
						{
							marqueeProgressBarControl1.Text = "일괄등록중..." + drow["SAWON_NO"].ToString().Trim() + " " + drow["SAWON_NM"].ToString().Trim() + " (" + cnt.ToString().PadLeft(6, ' ') + "/" + t_cnt.PadLeft(6, ' ') + ")";
						}));
					}
				}
			}
			catch (Exception ec)
			{
				MessageBox.Show(ec.Message, "저장오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				if (outVal <= 0)
					MessageBox.Show("저장된 내용이 없습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
				else
					MessageBox.Show("Duty관리에 미등록된 간호사 " + cnt + "명이 모두 등록되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
				btn_search_CK();
			}
		}

		private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			//this.progressBarControl1.Position = e.ProgressPercentage;
		}

		private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (e.Error != null)    // 예외 발생
				{
                    MessageBox.Show("예외가 발생했습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (e.Cancelled)   // 작업취소
				{
                    MessageBox.Show("작업이 취소되었습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else                    // 완료
				{
                    //MessageBox.Show("작업이 완료되었습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("오류가 발생했습니다", ex);
			}
			finally
			{
				btn_all_save.Enabled = true;
				btn_save_canc.Enabled = false;
				btn_info.Enabled = true;
				btn_exel.Enabled = true;
				srPanel4.Enabled = true;
				
				marqueeProgressBarControl1.Visible = false;
				marqueeProgressBarControl1.Properties.Stopped = true;

				this.Cursor = Cursors.Default;
			}
		}

		#endregion

		#region 9. ETC

		/// <summary>
		/// 배열에따른 버튼상태설정
		/// </summary>
		/// <param name="mode"></param>
		private void SetButtonEnable(string arr)
        {
            btn_save.Enabled  = arr.Substring(0, 1) == "1" ? true : false;
            btn_del.Enabled   = arr.Substring(1, 1) == "1" ? true : false;
            btn_clear.Enabled = arr.Substring(2, 1) == "1" ? true : false;
        }
		#endregion

	}
}

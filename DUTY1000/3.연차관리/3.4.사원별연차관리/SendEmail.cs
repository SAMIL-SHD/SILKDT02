using System;
using System.Windows.Forms;
using System.IO;
using System.Data;
using SilkRoad.Common;
using DevExpress.XtraEditors;
using System.ComponentModel;
//using MailKit.Security;
//using MimeKit;
using System.Net.Mail;
using DevExpress.XtraPrinting;

namespace DUTY1000
{
	public partial class sendemail : SilkRoad.Form.Base.FormX
	{
		CommonLibrary clib = new CommonLibrary();
		public DataSet ds = new DataSet();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();
        DataProcFunc df = new DataProcFunc();
		string rpt_name = "";
		int _step = 0;
		//string[] where;
		DataRow[] getRow;
		private static bool ErrorFlag = false;

		public sendemail(DataSet ds, int step)
		{
			InitializeComponent();
			_step = step;

			//txt_Subject.Text = where[0];

			this.ds = ds;
			//this.where = where;
			//this.rpt_name = rpt_name;

			// BackgroundWorker
			//this.bgWorker.DoWork += bgWorker_DoWork;
			//this.bgWorker.ProgressChanged += bgWorker_ProgressChanged;
			//this.bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
		}

		private void SendEmail_Load(object sender, EventArgs e)
		{
			srTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

			setCancel();

			if (_step == 2)
				lb_step.Text = "2차연차촉진";

			txt_cc.Text = Properties.Settings.Default.CCEmailAddress;
			grd1.DataSource = ds.Tables["COPY_SEARCH_YC"];

			try
			{
				// 작업 취소를 사용
				this.bgWorker.WorkerSupportsCancellation = true;

				// 작업 진행률 업데이트 보고를 사용
				this.bgWorker.WorkerReportsProgress = true;
				this.btn_send.Enabled = true;
				this.btn_cancel.Enabled = false;
				//this.lblWorkCount.Text = "0";
			}
			catch (Exception ex)
			{
				throw new Exception("오류가 발생했습니다", ex);
			}
		}

		private void setCancel()
		{
			panel1.Enabled = true;
			srTabControl1.SelectedTabPageIndex = 0;

			txt_sendEmail.Text = Properties.Settings.Default.FromEmailAddress;
		}

		#region 2 Button

		//이메일 SETTING 버튼
		private void btn_setting_Click(object sender, EventArgs e)
		{
			panel1.Enabled = false;
			srTabControl1.SelectedTabPageIndex = 1;

			txt_fromemailaddress.Text = Properties.Settings.Default.FromEmailAddress;
			txt_sendmailaddressname.Text = Properties.Settings.Default.SendMailAddressName;
			txt_smptserver.Text = Properties.Settings.Default.SmptServer;
			txt_serverid.Text = Properties.Settings.Default.SmptServerID;
			txt_smptpasswd.Text = Properties.Settings.Default.SmptServerPasswd;
			txt_smptserverport.Text = Properties.Settings.Default.SmptServerPort;
		}

		//이메일 설정 저장 버튼
		private void btn_save_Click(object sender, EventArgs e)
		{
			Properties.Settings.Default.FromEmailAddress = txt_fromemailaddress.Text.Trim();
			Properties.Settings.Default.SendMailAddressName = txt_sendmailaddressname.Text.Trim();
			Properties.Settings.Default.SmptServer = txt_smptserver.Text.Trim();
			Properties.Settings.Default.SmptServerID = txt_serverid.Text.Trim();
			Properties.Settings.Default.SmptServerPasswd = txt_smptpasswd.Text.Trim();
			Properties.Settings.Default.SmptServerPort = txt_smptserverport.Text.Trim();
			Properties.Settings.Default.Save();

			setCancel();
		}

		//이메일 설정 취소 버튼
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			setCancel();
		}

		//이메일 전송버튼
		private void btn_send_Click(object sender, EventArgs e)
		{
			if (clib.TextToDecimal(txt_cnt_chk.Text.ToString()) == 0)
			{
				MessageBox.Show("선택된 내역이 없습니다!", "저장오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				DialogResult dr = MessageBox.Show(txt_cnt_chk.Text.ToString() + "건의 연차휴가사용촉구 메일을 전송하시겠습니까?", "전송여부", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
				if (dr == DialogResult.OK)
				{
					int[] rows = grdv1.GetSelectedRows();

					int without_group = 0;
					for (int i = 0; i < rows.Length; i++)
					{
						if (rows[i] < 0)
						{
							without_group++;
						}
					}
					getRow = new DataRow[rows.Length - without_group];
					for (int i = 0; i < rows.Length - without_group; i++)
						getRow[i] = (DataRow)grdv1.GetDataRow(rows[i + without_group]);

					if (rows.Length == 0)
					{
						MessageBox.Show("선택된 사번이 없습니다.");
						return;
					}
					if (txt_sendEmail.Text.Trim() == "")
					{
						MessageBox.Show("보내는 사람 이메일를 설정해 주세요.");
						txt_sendEmail.Focus();
						return;
					}
					if (txt_Subject.Text.Trim() == "")
					{
						MessageBox.Show("메일 제목을 입력해 주세요.");
						txt_Subject.Focus();
						return;
					}
					////send_email();
					//CallBackGroundWorker();

					try
					{
						this.btn_send.Enabled = false;
						this.btn_canc.Enabled = true;
						btn_exit.Enabled = false;

						this.Cursor = Cursors.WaitCursor;

						AppendMessage("작업을 시작합니다.");

						progressBarControl1.Visible = true;
						this.progressBarControl1.Position = 0;
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
		//전송취소
		private void btn_canc_Click(object sender, EventArgs e)
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
		}
		//smtp 설명버튼
		private void srButton1_Click(object sender, EventArgs e)
		{
			srTabControl1.SelectedTabPageIndex = 2;
		}

		//설명 닫기
		private void btn_close3_Click(object sender, EventArgs e)
		{
			srTabControl2.SelectedTabPageIndex = 0;
			srTabControl1.SelectedTabPageIndex = 1;
		}
		#endregion

		#region 3 EVENT

		//폼종료할때 참조 저장
		private void sendemail_FormClosed(object sender, FormClosedEventArgs e)
		{
			Properties.Settings.Default.CCEmailAddress = txt_cc.Text;
			Properties.Settings.Default.Save();
		}

		// 한 사번 더블클릭하면 미리보기
		private void grdv1_DoubleClick(object sender, EventArgs e)
		{
			DataRow row = grdv1.GetFocusedDataRow();

			switch (rpt_name)
			{
				//case "rpt_3330_email":
				//	rpt_3330_email r = new rpt_3330_email(ds, where,row);
				//	r.DataSource = ds.Tables["TRSWGPY"].Select("SABN = '" + row["SABN"] + "'").CopyToDataTable();
				//	r.ShowPreview();
				//	break;
				//case "rpt_3330_jj": //전북고속
				//	rpt_3330_jj r3 = new rpt_3330_jj(ds, where);
				//	r3.DataSource = ds.Tables["TRSWGPY"].Select("SABN = '" + row["SABN"] + "'").CopyToDataTable();
				//	r3.ShowPreview();
				//	break;
			}
		}
		
		private void grdv1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
		{
			int[] rows = grdv1.GetSelectedRows();
			int without_group = 0;
			for (int i = 0; i < rows.Length; i++)
			{
				if (rows[i] < 0)
				{
					without_group++;
				}
			}
			txt_cnt_chk.Text = String.Format("{0:#,###}", grdv1.GetSelectedRows().Length - without_group);
		}

		#endregion

		#region 8. BackgroundWorker

		private void CallBackGroundWorker()
		{
			//if (!this.bgWorker.IsBusy)
			//{
			//	this.progressBarControl1.Visible = true;
			//	this.bgWorker.RunWorkerAsync();
			//}
		}

		private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			//BackgroundWorker worker = sender as BackgroundWorker;
			//send_email(worker);

			////proc(worker);

			this.DoWork((BackgroundWorker)sender, e);
			e.Result = null;			
		}
		private void DoWork(BackgroundWorker worker, DoWorkEventArgs e)
		{
			/*
			* 실행중인 Form 과 다른 쓰레드에서 동작하므로
			* 처리할 메서드에서는 UI 객체의 속성값(Value, Text 등..)을 사용하지 못합니다.
			*
			* 작업에 필요한 값은 매개변수로 전달받아야 하고 UI객체의 상태를 변화시킬 필요가 있는 경우
			* ProgressChanged
			* RunWorkerCompleted
			* 이벤트를 사용해야 합니다.
			*/
				
			string fromadress = Properties.Settings.Default.FromEmailAddress;
			String sendAdress = Properties.Settings.Default.SendMailAddressName;
			string smtpServer = Properties.Settings.Default.SmptServer;
			string smtpId = Properties.Settings.Default.SmptServerID;
			string smtpPass = Properties.Settings.Default.SmptServerPasswd;
			string smtpPort = Properties.Settings.Default.SmptServerPort;

			double nMax = (double)getRow.Length;
			int iSendCnt = 0;
			foreach (DataRow row in getRow)
			{
				if (worker.CancellationPending)
				{
					e.Cancel = true;
					break;
				}
				else
				{
					if (ErrorFlag)
					{
						ErrorFlag = false;
						throw new Exception("예외 발생 시뮬레이션");
					}
                    // 시간이 걸리는 작업을 실행합니다.
                    //System.Threading.Thread.Sleep(200);
                    string sano = row["SAWON_NO"].ToString().Trim();
                    string sanm = row["SAWON_NM"].ToString().Trim();
                    string pdf_fnm = "연차휴가사용촉구_" + sano + "_" + sanm + ".pdf";
                    if (row["GW_EMAIL"].ToString().Trim() != "")
					{
						MailMessage mailMessage = new MailMessage();
						mailMessage.From = new MailAddress(fromadress, sendAdress, System.Text.Encoding.UTF8);
						// 받는이 메일 주소
						mailMessage.To.Add(row["GW_EMAIL"].ToString());
						//// 참조 메일 주소
						//mailMessage.CC.Add("zzz@naver.com");
						//// 비공개 참조 메일 주소
						//mailMessage.Bcc.Add("kkk@naver.com");
						// 제목
						mailMessage.Subject = txt_Subject.Text.Trim();
						// 메일 제목 인코딩 타입(UTF-8) 선택
						mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
						// 본문
						mailMessage.Body = txt_Body.Text.Trim();
						// 본문의 포맷에 따라 선택
						mailMessage.IsBodyHtml = false;
						// 본문 인코딩 타입(UTF-8) 선택
						mailMessage.BodyEncoding = System.Text.Encoding.UTF8;

						// 파일 첨부
						Rpt_8020 r = new Rpt_8020(row["SAWON_NO"].ToString(), SilkRoad.Config.SRConfig.WorkPlaceName, ds);
						r.DataSource = ds.Tables["COPY_SEARCH_YC"].Select("SAWON_NO = '" + row["SAWON_NO"] + "'").CopyToDataTable();
						
						MemoryStream stream = new MemoryStream();
						r.ExportToPdf(stream);
						byte[] bytes = stream.ToArray();
						mailMessage.Attachments.Add(new Attachment(new MemoryStream(bytes), pdf_fnm));
						
						// 파일 첨부2
						if (_step == 1)
						{
							Rpt_8021 r2 = new Rpt_8021(row["SAWON_NO"].ToString(), SilkRoad.Config.SRConfig.WorkPlaceName, "SEARCH_YC", ds);
							r2.DataSource = ds.Tables["COPY_SEARCH_YC"].Select("SAWON_NO = '" + row["SAWON_NO"] + "'").CopyToDataTable();
							string pdf_fnm2 = "연차휴가사용계획서1차_" + sano + "_" + sanm + ".pdf";
							r2.ExportToPdf(stream);
							byte[] bytes2 = stream.ToArray();
							mailMessage.Attachments.Add(new Attachment(new MemoryStream(bytes2), pdf_fnm2));
						}
						else if (_step == 2)
						{
							Rpt_8022 r2 = new Rpt_8022(row["SAWON_NO"].ToString(), SilkRoad.Config.SRConfig.WorkPlaceName, "SEARCH_YC", ds);
							r2.DataSource = ds.Tables["COPY_SEARCH_YC"].Select("SAWON_NO = '" + row["SAWON_NO"] + "'").CopyToDataTable();
							string pdf_fnm2 = "연차휴가사용계획서2차_" + sano + "_" + sanm + ".pdf";
							r2.ExportToPdf(stream);
							byte[] bytes2 = stream.ToArray();
							mailMessage.Attachments.Add(new Attachment(new MemoryStream(bytes2), pdf_fnm2));
						}

						// SMTP 서버 주소
						SmtpClient SmtpServer = new SmtpClient(smtpServer);
						// SMTP 포트
						SmtpServer.Port = clib.TextToInt(smtpPort);
						// SSL 사용 여부
						SmtpServer.EnableSsl = true;
						SmtpServer.UseDefaultCredentials = false;
						SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
						SmtpServer.Credentials = new System.Net.NetworkCredential(smtpId, smtpPass);
						SmtpServer.Send(mailMessage);
                    }

                    df.GetDUTY_MSTYCCJDatas(ds);
					DataRow nrow = ds.Tables["DUTY_MSTYCCJ"].NewRow();
					nrow["YC_YEAR"] = row["YC_YEAR"].ToString();
					nrow["DOC_TYPE"] = _step == 1 ? "202101" : "202102";
					nrow["SAWON_NO"] = row["SAWON_NO"].ToString();
					nrow["SAWON_NM"] = row["SAWON_NM"].ToString();
					nrow["YC_SQ"] = df.GetSEARCH_YCCJ_SQDatas(nrow["DOC_TYPE"].ToString(), row["YC_YEAR"].ToString(), row["SAWON_NO"].ToString(), ds);
					nrow["YC_IPDT"] = row["IN_DATE"].ToString();
					nrow["CALC_FRDT"] = row["CALC_FRDT"];
					nrow["CALC_TODT"] = row["CALC_TODT"];
					nrow["YC_STDT"] = row["YC_STDT"];
					nrow["YC_TDAY"] = row["YC_TOTAL"];
					nrow["USE_FRDT"] = row["USE_FRDT"];
					nrow["USE_TODT"] = row["USE_TODT"];
					nrow["YC_USE_DAY"] = row["YC_USE"];
					nrow["YC_REMAIN_DAY"] = row["YC_REMAIN"];
					nrow["SEND_MAIL"] = fromadress;
					nrow["SEND_DT"] = gd.GetNow();
					nrow["SEND_ID"] = SilkRoad.Config.SRConfig.USID;
					nrow["RECEIVE_MAIL"] = row["GW_EMAIL"].ToString();
					nrow["READ_YN"] = "";
					nrow["READ_DT"] = "";
					nrow["READ_ID"] = "";
					ds.Tables["DUTY_MSTYCCJ"].Rows.Add(nrow);
						
					string[] tableNames = new string[] { "DUTY_MSTYCCJ" };
					SilkRoad.DbCmd_DT02.DbCmd_DT02 cmd = new SilkRoad.DbCmd_DT02.DbCmd_DT02();
					int outval = cmd.setUpdate(ref ds, tableNames, null);
					if (outval > 0)
						AppendMessage(iSendCnt.ToString() + "." + sano + "_" + sanm + " 전송성공");

					iSendCnt++;

					// 진행률 업데이트하기 위해 ProgressChanged 이벤트를 발생시킵니다.
					worker.ReportProgress((int)((iSendCnt / nMax) * 100));
				}
			}
		}
		private void AppendMessage(string message)
		{
            this.Invoke(new Action(delegate ()
            {
				textBox1.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]: {message}{Environment.NewLine}");
				textBox1.ScrollToCaret();
            }));
		}
		private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			this.progressBarControl1.Position = e.ProgressPercentage;
		}

		private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (e.Error != null)    // 예외 발생
				{
					AppendMessage(String.Format("{0} ==> {1}", "예외가 발생했습니다.", e.Error.Message));
				}
				else if (e.Cancelled)   // 작업취소
				{
					AppendMessage("작업이 취소되었습니다.");
				}
				else                    // 완료
				{
					AppendMessage("작업이 완료되었습니다.");
					XtraMessageBox.Show("메일전송이 완료되었습니다.", "전송완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("오류가 발생했습니다", ex);
			}
			finally
			{
				btn_send.Enabled = true;
				btn_canc.Enabled = false;
				btn_exit.Enabled = true;
				this.progressBarControl1.Visible = false;
				this.Cursor = Cursors.Default;
			}
		}

		#endregion

	}

}

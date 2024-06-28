using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using DevExpress.XtraGrid.Localization;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.Xml;
using SilkRoad.Config;
using System.IO;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using SilkRoad.DAL;

namespace DUTY1000
{
    public partial class duty4010 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();
		int idx = 0;
        public duty4010()
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
			idx = 0;
			btn_refresh_CK();
			srLabel1.Text = "공지사항 등록중입니다.";
			txt_title.Text = "";
			mm_Contents.Text = "";
            SetButtonEnable("10");
        }

        //사원기본정보 및 연차정보 , 휴가사용내역, 휴일사용내역 조회
        private void baseInfoSearch()
        {
			df.GetSEARCH_TRSNOTIDatas(ds);
			grd1.DataSource = ds.Tables["SEARCH_TRSNOTI"];
        }
        #endregion

        #region 1 Form

        private void duty4010_Load(object sender, EventArgs e)
        {
			dat_sldt.DateTime = DateTime.Now;
			sl_dept.EditValue = null;
            SetCancel();
			baseInfoSearch();
        }

        #endregion

        #region 2 Button
		
        //저장버튼
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (isNoError_um(1))
            {
                Cursor = Cursors.WaitCursor;
                int outVal = 0;
                try
                {
					DataRow nrow;
					df.GetDUTY_TRSNOTIDatas(idx, ds);
					if (ds.Tables["DUTY_TRSNOTI"].Rows.Count > 0)
					{
						nrow = ds.Tables["DUTY_TRSNOTI"].Rows[0];
						nrow["DEPTCODE"] = sl_dept.EditValue == null ? "" : sl_dept.EditValue.ToString();
						nrow["NOTIDATE"] = clib.DateToText(dat_sldt.DateTime);
						nrow["TITLE"] = txt_title.Text.ToString();
						nrow["CONTENTS"] = mm_Contents.Text.ToString();
						nrow["UPDT"] = gd.GetNow();
						nrow["USID"] = SilkRoad.Config.SRConfig.USID;
						nrow["PSTY"] = "U";
					}
					else
					{
						nrow = ds.Tables["DUTY_TRSNOTI"].NewRow();

						nrow["IDX"] = df.GetIDX_DUTY_TRSNOTIDatas(ds);
						nrow["DEPTCODE"] = sl_dept.EditValue == null ? "" : sl_dept.EditValue.ToString();
						nrow["NOTIDATE"] = clib.DateToText(dat_sldt.DateTime);
						nrow["TITLE"] = txt_title.Text.ToString();
						nrow["CONTENTS"] = mm_Contents.Text.ToString();
						nrow["INDT"] = gd.GetNow();
						nrow["UPDT"] = "";
						nrow["USID"] = SilkRoad.Config.SRConfig.USID;
						nrow["PSTY"] = "A";
						ds.Tables["DUTY_TRSNOTI"].Rows.Add(nrow);
					}

                    string[] tableNames = new string[] { "DUTY_TRSNOTI" };
                    SilkRoad.DbCmd_DT02.DbCmd_DT02 cmd = new SilkRoad.DbCmd_DT02.DbCmd_DT02();
                    outVal = cmd.setUpdate(ref ds, tableNames, null);

                    if (outVal > 0)              
                        MessageBox.Show("저장되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ec)
                {
                    MessageBox.Show(ec.Message, "저장오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (outVal > 0)
						SetCancel();
					baseInfoSearch();
                    Cursor = Cursors.Default;
                }
            }
        }

        //삭제
        private void btn_del_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("해당 공지사항을 삭제하시겠습니까?", "삭제여부", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                int outVal = 0;
                try
                {
					df.GetDUTY_TRSNOTIDatas(idx, ds);
                    ds.Tables["DUTY_TRSNOTI"].Select("IDX = '" + idx + "'")[0]["UPDT"] = gd.GetNow();
                    ds.Tables["DUTY_TRSNOTI"].Select("IDX = '" + idx + "'")[0]["USID"] = SilkRoad.Config.SRConfig.USID;
                    ds.Tables["DUTY_TRSNOTI"].Select("IDX = '" + idx + "'")[0]["PSTY"] = "D";

                    string[] tableNames = new string[] { "DUTY_TRSNOTI" };
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
                        MessageBox.Show("해당 공지사항이 삭제되었습니다.", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    SetCancel();
                    baseInfoSearch();
                    Cursor = Cursors.Default;
                }
            }
        }

        /// <summary>취소버튼</summary>
        private void btn_clear_Click(object sender, EventArgs e)
        {
            SetCancel();
            baseInfoSearch();
        }

        #endregion

        #region 3 EVENT
		
        private void duty4010_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //취소
            {
                btn_clear.PerformClick();
            }
        }
		
		//더블클릭시 등록,수정
		private void grdv1_DoubleClick(object sender, EventArgs e)
		{
			DataRow drow = grdv1.GetFocusedDataRow();
            if (drow == null)
                return;
			
			srLabel1.Text = "공지사항 수정중입니다.";
			idx = clib.TextToInt(drow["IDX"].ToString());
			SetButtonEnable("11");
			
			if (drow["NOTIDATE"].ToString().Trim() != "")
				dat_sldt.DateTime = clib.TextToDate(drow["NOTIDATE"].ToString());
			
            sl_dept.EditValue = drow["DEPTCODE"].ToString().Trim() == "" ? null : drow["DEPTCODE"].ToString();
			txt_title.Text = drow["TITLE"].ToString();
            mm_Contents.Text = drow["CONTENTS"].ToString();
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
            if (mode == 1)
            {
                if (clib.DateToText(dat_sldt.DateTime) == "")
                {
                    MessageBox.Show("공지일자를 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dat_sldt.Focus();
                    return false;
				}
                else if (txt_title.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("공지사항 제목을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_title.Focus();
                    return false;
                }
                else if (mm_Contents.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("공지사항 내용을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    mm_Contents.Focus();
                    return false;
                }
				else if (Encoding.Default.GetByteCount(txt_title.Text.ToString().Trim()) > 60)
				{
					MessageBox.Show("공지사항 제목의 길이가 60 byte를 초과하였습니다.\r\n(현재 " + Encoding.Default.GetByteCount(txt_title.Text.ToString().Trim()) + "byte)", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
					txt_title.Focus();
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
        /// 기초코드 룩업컨트롤에 설정하는 부분
        /// </summary>
        private void btn_refresh_CK()
        {
			df.GetSEARCH_DEPTDatas(ds);
			sl_dept.Properties.DataSource = ds.Tables["SEARCH_DEPT"];
        }

		/// <summary>
		/// 배열에따른 버튼상태설정
		/// </summary>
		/// <param name="mode"></param>
		private void SetButtonEnable(string arr)
		{
			btn_save.Enabled = arr.Substring(0, 1) == "1" ? true : false;
			btn_del.Enabled = arr.Substring(1, 1) == "1" ? true : false;
			//int[] array = new int[] { 1, 5, 2, 6, 3, 7, 4 };
			//int[,] commands = new int[,] { { 2, 5, 3 }, { 4, 4, 1 }, { 1, 7, 3 } };
			////array[0] = { 1, 5, 2, 6, 3, 7, 4};
			////commands = [[2,5,3],[4,4,1],[1,7,3]];
			//solution(array, commands);
        }

		//public int[] solution(int[] array, int[,] commands) {
  //      int[] answer = new int[] {};
  //      return answer;
		//}

		//private int[] solution2(int[] array, int[,] commands)
		//{
		//	int[] answer = new int[] { };
		//	//newArray.sort();
		//	return answer;
		//}

        #endregion		

    }
}

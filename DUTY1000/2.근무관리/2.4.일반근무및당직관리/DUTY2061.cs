using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;


namespace DUTY1000
{
    public partial class duty2061 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();

        public duty2061()
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
			grd1.DataSource = null;
            SetButtonEnable("100");
        }

        #endregion

        #region 1 Form

        private void duty2061_Load(object sender, EventArgs e)
        {
			Proc();
        }

        #endregion

        #region 2 Button

        //처리
        private void btn_proc_Click(object sender, EventArgs e)
        {
			Proc();
        }

        private void Proc()
        {
			df.GetSEARCH_DANGDEPTDatas(ds);
			grd1.DataSource = ds.Tables["SEARCH_DANGDEPT"];
            SetButtonEnable("011");
        }

        //저장버튼
        private void btn_save_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            int outVal = 0;
            try
            {
				df.GetD_DUTY_INFODANGDatas(ds);  //삭제후
				for (int i = 0; i < ds.Tables["D_DUTY_INFODANG"].Rows.Count; i++)
				{
					ds.Tables["D_DUTY_INFODANG"].Rows[i].Delete();
				}

				df.GetDUTY_INFODANGDatas(ds);  //등록
				foreach (DataRow dr in ds.Tables["SEARCH_DANGDEPT"].Rows)
                {
					if (dr["CHK"].ToString() == "1")
					{
						DataRow nrow = ds.Tables["DUTY_INFODANG"].NewRow();
						nrow["DEPTCODE"] = dr["DEPRCODE"];
						nrow["INDT"] = gd.GetNow();
						nrow["UPDT"] = "";
						nrow["USID"] = SilkRoad.Config.SRConfig.USID;
						nrow["PSTY"] = "A";
						ds.Tables["DUTY_INFODANG"].Rows.Add(nrow);
					}
                }

				string[] tableNames = new string[] { "D_DUTY_INFODANG", "DUTY_INFODANG" };
				SilkRoad.DbCmd_DT02.DbCmd_DT02 cmd = new SilkRoad.DbCmd_DT02.DbCmd_DT02();
				outVal = cmd.setUpdate(ref ds, tableNames, null);

			}
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message, "저장오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //SetCancel();
                Cursor = Cursors.Default;
				this.Dispose();
            }
        }

        /// <summary>취소버튼</summary>
        private void btn_clear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("해당 내역을 취소하시겠습니까?", "취소", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                SetCancel();
            }
        }

        #endregion

        #region 3 EVENT

        private void duty2061_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //취소
            {
                btn_clear.PerformClick();
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
            btn_proc.Enabled = arr.Substring(0, 1) == "1" ? true : false;
            btn_save.Enabled = arr.Substring(1, 1) == "1" ? true : false;
            btn_clear.Enabled = arr.Substring(2, 1) == "1" ? true : false;
        }

        #endregion
    }
}

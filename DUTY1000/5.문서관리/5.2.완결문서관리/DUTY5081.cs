using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;


namespace DUTY1000
{
    public partial class duty5081 : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();
        string _tb_nm, _seqno;

        public duty5081()
        {
            InitializeComponent();
        }
        public duty5081(string tb_nm, string seqno)
        {
            InitializeComponent();
            _tb_nm = tb_nm;
            _seqno = seqno;
        }

        #region 0. Initialization
        
        #endregion

        #region 1 Form

        private void duty5081_Load(object sender, EventArgs e)
        {
        }
        private void duty5081_Shown(object sender, EventArgs e)
        {
            df.Get5080_LINE_DTDatas(_tb_nm, _seqno, ds);
            grd_sign.DataSource = ds.Tables[_tb_nm];
        }

        #endregion

        #region 2 Button
        
        //수정
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (isNoError_um(1))
            {
                Cursor = Cursors.WaitCursor;
                int outVal = 0;
                try
                {
                    if (ds.Tables[_tb_nm].Rows.Count > 0)
                    {
                        string[] tableNames = new string[] { _tb_nm };
                        SilkRoad.DbCmd_DT02.DbCmd_DT02 cmd = new SilkRoad.DbCmd_DT02.DbCmd_DT02();
                        outVal = cmd.setUpdate(ref ds, tableNames, null);
                    }
                }
                catch (Exception ec)
                {
                    MessageBox.Show(ec.Message, "저장오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor = Cursors.Default;
                    if (outVal > 0)
                        this.Dispose();
                }
            }
        }

        #endregion

        #region 3 EVENT

        private void duty5081_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //취소
            {
                this.Dispose();
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

            if (mode == 1)  //조회
            {
                if (ds.Tables["DUTY_TRSJREQ_DT"].Select("LINE_JIWK=''").Length > 0)
                {
                    MessageBox.Show("기준년도를 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    grdv_sign.Focus();
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

        #region 9 ETC

        #endregion

    }
}

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
using System.Data.SqlClient; // 바로 연결되는지 테스트 겸 추가
using DevExpress.XtraScheduler;

namespace DUTY1000
{
    public partial class duty206x : SilkRoad.Form.Base.FormX
    {
        CommonLibrary clib = new CommonLibrary();

        ClearNEnableControls cec = new ClearNEnableControls();
        public DataSet ds = new DataSet();
        DataProcFunc df = new DataProcFunc();
        SilkRoad.DataProc.GetData gd = new SilkRoad.DataProc.GetData();
        string _Flag = "";

        public duty206x()
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
            //_Flag = "";
            
            //cec.SetClearControls(gr_detail, new string[] { "txt_code" ,"sl_Class"});
            //cec.SetEnabledStateinEachControls(gr_detail, false, new string[] { "txt_code","sl_Class" }, new string[] {});

            //SetButtonEnable("1000");

            //df.GetDocuClassCodeName(ref ds);//문서구분
            //sl_Class.Properties.DataSource = ds.Tables["DOCUCLASS_SEARCH"];
            //if (ds.Tables["DOCUCLASS_SEARCH"] != null && ds.Tables["DOCUCLASS_SEARCH"].Rows.Count > 0)
            //{
            //    sl_Class.EditValue = ds.Tables["DOCUCLASS_SEARCH"].Rows[0]["코드"];
            //}

            //txt_code.Text = df.GetNextTypeCode();//max다음번호
            //txt_code.Focus();
        }

        //왼쪽 목록 조회
        private void Search()
        {
            if (ds.Tables["DOCUTYPE_SEARCH"] != null)
            {
                ds.Tables["DOCUTYPE_SEARCH"].Clear();
            }

            //df.GetDocuTypeCodeName(ref ds);
            //grd1.DataSource = ds.Tables["DOCUTYPE_SEARCH"];
            //grdv1.BestFitColumns();
            
            //if(ds.Tables["DOCUTYPE_SEARCH"]!=null && ds.Tables["DOCUTYPE_SEARCH"].Rows.Count>0)
            //{
            //    string next = (int.Parse(ds.Tables["DOCUTYPE_SEARCH"].Compute("max(코드)", null).ToString()) + 1).ToString().PadLeft(2, '0');

            //   // txt_code.Text = next;
            //}
        }

        #endregion


        #region 1 Form

        private void duty2060_Load(object sender, EventArgs e)
        {
            //getduty2050_LoadDatas();
            Search();
            //grd1.DataSource = df.duty2050_Load
            SetCancel();
            //txt_code.Focus();

            //string connectionSTR = @"Server=115.68.15.100;Database=FL11DB01;User Id=sa;Password=samil25";
            //using (SqlConnection connection = new SqlConnection(connectionSTR))
            //{
            //    connection.Open();
            //    SqlCommand cmd = new SqlCommand
            //        ("USE WAGEDB03 GO"
            //        + "SELECT C.PART_SEQ as SEQ, D.PART_NM as Dept_NM, A.DEPTCODE as D_code, A.SAWON_NO as SANO"
            //        + ",case when A.PROFICIENCY = '1' then '간호부장'"
            //        + "     when A.PROFICIENCY = '2' then '간호과장'"
            //        + "     when A.PROFICIENCY = '3' then '수간호사'"
            //        + "     when A.PROFICIENCY = '4' then '책임간호사'"
            //        + "     when A.PROFICIENCY = '5' then '주임간호사' "
            //        + "	 						  else '평간호사' end as PROFIC --숙련도"
            //        + "FROM     MSTNURS A"
            //        + "left outer join(SELECT B.PARTCODE, B.PART_FNM, B.PART_SNM, B.STAT, B.LDAY, C.SWITCHYM, C.PART_SEQ,         C.SAWON_NO"
            //        + "                        FROM MSTPART B left outer join TRSPART C on B.PARTCODE = c.PARTCODE"
            //        + "                 ) C on C.SAWON_NO = A.SAWON_NO AND C.PARTCODE = A.DEPTCODE"
            //        + "left outer join(select substring(PARTCODE + '-' + PART_FNM, 6, 20) as PART_NM, *from MSTPART"
            //        + "                 ) D on D.PARTCODE = A.DEPTCODE"
            //        , connection);
            //}
        }

        #endregion

        #region 2 Button


        //처리
        private void btn_proc_Click(object sender, EventArgs e)
        {
            if (isNoError_um(1))
            {
                ////코드 2자리 0채우기
                //if (txt_code.Text.Length < 2)
                //{
                //    txt_code.Text = txt_code.Text.PadLeft(2, '0');
                //}

                //_Flag = "C";
                //SetButtonEnable("0101");  //신규
                //cec.SetEnabledStateinEachControls(gr_detail, true, new string[] { "txt_code" ,"sl_Class"}, new string[] { });

                //df.GetDocuTypeData(ref ds,sl_Class.EditValue.ToString(), txt_code.Text.Trim());

                //if (ds.Tables["DOCUTYPE"] != null && ds.Tables["DOCUTYPE"].Rows.Count > 0)
                //{
                //    DataRow dr = ds.Tables["DOCUTYPE"].Rows[0];
                //    txt_name.Text = dr["TYPENAME"].ToString();
                //    cmb_formNo.EditValue = dr["DEFAULTFORM"];
                     
                //    _Flag = "U";
                //    SetButtonEnable("0111");  //신규
                //}

            }
        }

        //저장버튼
        private void btn_save_Click(object sender, EventArgs e)
        {
            //if (isNoError_um(2))
            //{
            //        Cursor = Cursors.WaitCursor;
            //        int outVal = 0;
            //        try
            //        {
            //            DataRow hrow;

            //            if (_Flag == "C")  //신규
            //            {
            //                hrow = ds.Tables["DOCUTYPE"].NewRow();
            //                hrow["CLASSCODE"] = sl_Class.EditValue;
            //                hrow["TYPECODE"] = txt_code.Text.ToString().Trim();
            //                hrow["SAVETIME"] = DateTime.Now;
            //                hrow["SAVEFLAG"] = "A";
            //            }
            //            else //수정
            //            {
            //                hrow = ds.Tables["DOCUTYPE"].Rows[0];
            //                hrow["MODITIME"] = DateTime.Now;
            //                hrow["SAVEFLAG"] = "U";
            //            }
                       
            //            hrow["TYPENAME"] = txt_name.Text.Trim();
            //            hrow["DEFAULTFORM"] = cmb_formNo.EditValue;
            //            hrow["SAVEURID"] = SRConfig.USID;

            //            if (_Flag == "C")  //신규
            //            {
            //                ds.Tables["DOCUTYPE"].Rows.Add(hrow);
            //            }

            //            string[] tableNames = new string[] { "DOCUTYPE" };
            //            SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();
            //            outVal = cmd.setUpdate(ds, tableNames, null);
            //        }
            //        catch (Exception ec)
            //        {
            //            MessageBox.Show(ec.Message, "저장오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //        finally
            //        {
            //            btn_refresh.PerformClick();
            //            SetCancel();
            //            Cursor = Cursors.Default;
            //        }
            //}
        }

        //삭제
        private void btn_del_Click(object sender, EventArgs e)
        {
            ////코드사용여부 먼저검사..

            //DialogResult dr = MessageBox.Show("삭제하시겠습니까?", "삭제여부", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            //if (dr == DialogResult.OK)
            //{
            //    int outVal = 0;
            //    try
            //    {
            //        ds.Tables["DOCUTYPE"].Rows[0].Delete();

            //        string[] tableNames = new string[] { "DOCUTYPE" };

            //        SilkRoad.DbCmd_DT01.DbCmd_DT01 cmd = new SilkRoad.DbCmd_DT01.DbCmd_DT01();

            //        outVal = cmd.setUpdate(ds, tableNames, null);
               
            //    }
            //    catch (Exception ec)
            //    {
            //        MessageBox.Show(ec.Message, "삭제오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //    finally
            //    {
            //        btn_refresh.PerformClick();
            //        SetCancel();
            //        Cursor = Cursors.Default;
            //    }
            //}
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
            clib.gridToExcel(grdv1, this.Text + "_" + DateTime.Today.ToShortDateString().Replace("-", "").Replace("/", ""));
        }

        /// <summary> refresh버튼 </summary>
        private void btn_refresh_Click(object sender, EventArgs e)
        {

            Search();
        }

        #endregion

        #region 3 EVENT

        ////그리드 클릭
        //private void grdv1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        //{
        //    if (e.RowHandle < 0)
        //        return;

        //    btn_proc.Enabled = true;
        //    //txt_code.Text = grdv1.GetFocusedDataRow()["코드"].ToString();
        //    //sl_Class.EditValue = grdv1.GetFocusedDataRow()["CLASSCODE"].ToString();
        //    btn_proc.PerformClick();
        //}


        private void duty2060_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)   //취소
            {
                btn_clear.PerformClick();
            }
        }

        private void txt_code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_proc.PerformClick();
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

            //if (mode == 1)  //처리
            //{
            //    if (txt_code.Text == "")
            //    {
            //        MessageBox.Show("코드를 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        txt_code.Focus();
            //        return false;
            //    }
            //    else
            //    {
            //        isError = true;
            //    }
            //}
            //else if (mode == 2)  //저장
            //{
            //    if (clib.isLookupNull(txt_name))
            //    {
            //        MessageBox.Show("명칭을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        txt_name.Focus();
            //        return false;
            //    }
            //    else
            //    {
            //        isError = true;
            //    }
            //}

            return isError;
        }

        #endregion

        #region 9. ETC

        /// <summary>
        /// 기초코드 룩업컨트롤에 설정하는 부분
        /// </summary>
        private void btn_refresh_CK()
        {
            //SilkRoad.BaseCode.BaseCode bc = new SilkRoad.BaseCode.BaseCode();
        }

        /// <summary>
        /// 배열에따른 버튼상태설정
        /// </summary>
        /// <param name="mode"></param>
        private void SetButtonEnable(string arr)
        {
            btn_proc.Enabled = arr.Substring(0, 1) == "1" ? true : false;
            btn_save.Enabled = arr.Substring(1, 1) == "1" ? true : false;
            btn_del.Enabled = arr.Substring(2, 1) == "1" ? true : false;
            btn_clear.Enabled = arr.Substring(3, 1) == "1" ? true : false;

        }

        #endregion

        private void btn_sample_Click(object sender, EventArgs e)
        {
            //OVER1010_sub s = new OVER1010_sub();
            //s.Show();
        }
	}
}

using System;
using System.Data;
using System.Windows.Forms;
using SilkRoad.Common;
using SilkRoad.DataProc;

namespace SilkRoad.SILKDT02
{
    public partial class DataModi : Form.Base.FormX
    {
        public frmMain frmMain;
        static GetData gd = new GetData();
        static SetData sd = new SetData();
        static DataProcessing dp = new DataProcessing();
        string qry = "";
        DataSet ds = new DataSet();
        string dbname = DAL.DataAccess.DBname + Config.SRConfig.WorkPlaceNo;

        public DataModi()
        {
            InitializeComponent();
        }

        #region 0. Initialization

        #endregion

        #region 1 Form

        private void DataModi_Load(object sender, EventArgs e)
        {
            //btn_tb_c.Enabled = Config.SRConfig.USID == "SAMIL" ? true : false;            
            getEnd_Modi();
        }

        #endregion

        #region 2 Button

        //처리
        private void btn_proc_Click(object sender, EventArgs e)
        {
			if (btn_tb_c.Enabled == true)
				btn_tb_c.PerformClick();

            //getEnd_Modi();
        }
        //종료
        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //테이블생성(DT02)
        private void btn_tb_c_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            string t_cnt = "5";  //생성 테이블 수
            srLabel1.Visible = true;

            //001.환경설정
            srLabel1.Text = "CREATE TABLE SMS_INFO(1/" + t_cnt + ")";
            qry = " SELECT * FROM sysobjects WHERE NAME = 'SMS_INFO'  ";
            DataTable dt001 = gd.GetDataInQuery(1, dbname, qry);
            dp.AddDatatable2Dataset("E_SMS_INFO", dt001, ref ds);

            if (ds.Tables["E_SMS_INFO"].Rows.Count < 1)
            {
                qry = " CREATE TABLE [dbo].[SMS_INFO]( "
                    + "     [LOGIN_ID] [varchar](20) NOT NULL, "
                    + "     [BARO_ID] [varchar](20) NOT NULL, "
                    + "     [BARO_PW] [varchar](20) NOT NULL, "
                    + "     [FROM_NO] [varchar](20) NOT NULL, "
                    + "     [REG_DT] [varchar](20) NOT NULL "
                    + " ) ON [PRIMARY] ";

                object obj = gd.GetOneData(1, dbname, qry);
            }

            //002.발신번호
            srLabel1.Text = "CREATE TABLE SMS_FROM_NO(2/" + t_cnt + ")";
            qry = " SELECT * FROM sysobjects WHERE NAME = 'SMS_FROM_NO'  ";
            DataTable dt002 = gd.GetDataInQuery(1, dbname, qry);
            dp.AddDatatable2Dataset("E_SMS_FROM_NO", dt002, ref ds);

            if (ds.Tables["E_SMS_FROM_NO"].Rows.Count < 1)
            {
                qry = " CREATE TABLE [dbo].[SMS_FROM_NO]( "
                    + "     [LOGIN_ID] [varchar](20) NOT NULL, "
                    + "     [FROM_NO] [varchar](20) NOT NULL, "
                    + "     [REG_DT] [varchar](20) NOT NULL "
                    + " ) ON [PRIMARY] ";

                object obj = gd.GetOneData(1, dbname, qry);
            }

            //003.일련번호
            srLabel1.Text = "CREATE TABLE SMS_SLNO(3/" + t_cnt + ")";
            qry = " SELECT * FROM sysobjects WHERE NAME = 'SMS_SLNO'  ";
            DataTable dt003 = gd.GetDataInQuery(1, dbname, qry);
            dp.AddDatatable2Dataset("E_SMS_SLNO", dt003, ref ds);

            if (ds.Tables["E_SMS_SLNO"].Rows.Count < 1)
            {
                qry = " CREATE TABLE [dbo].[SMS_SLNO]( "
                    + "     [SMS_SLDT] [varchar](8) NOT NULL, "
                    + "     [SMS_LAST] [varchar](8) NOT NULL "
                    + " ) ON [PRIMARY] ";

                object obj = gd.GetOneData(1, dbname, qry);
            }

            //004.작성메시지 저장
            srLabel1.Text = "CREATE TABLE SMS_RMK(4/" + t_cnt + ")";
            qry = " SELECT * FROM sysobjects WHERE NAME = 'SMS_RMK'  ";
            DataTable dt004 = gd.GetDataInQuery(1, dbname, qry);
            dp.AddDatatable2Dataset("E_SMS_RMK", dt004, ref ds);

            if (ds.Tables["E_SMS_RMK"].Rows.Count < 1)
            {
                qry = " CREATE TABLE [dbo].[SMS_RMK]( "
                    + "     [LOGIN_ID] [varchar](20) NOT NULL, "
                    + "     [TXT_SQ] [decimal](15, 0) NOT NULL, "
                    + "     [TXT_RMK] [varchar](2000) NOT NULL, "
                    + "     [TXT_BYTE] [varchar](20) NOT NULL, "
                    + "     [REG_DT] [varchar](20) NOT NULL "
                    + " ) ON [PRIMARY] ";

                object obj = gd.GetOneData(1, dbname, qry);
            }
            //005.문자전송
            srLabel1.Text = "CREATE TABLE SMS_SEND(5/" + t_cnt + ")";
            qry = " SELECT * FROM sysobjects WHERE NAME = 'SMS_SEND'  ";
            DataTable dt005 = gd.GetDataInQuery(1, dbname, qry);
            dp.AddDatatable2Dataset("E_SMS_SEND", dt005, ref ds);

            if (ds.Tables["E_SMS_SEND"].Rows.Count < 1)
            {
                qry = " CREATE TABLE [dbo].[SMS_SEND]( "
                    + "     [REF_SLNO] [char](16) NOT NULL, "
                    + "     [REF_SQNO] [char](4) NOT NULL, "
                    + "     [SEND_KEY] [varchar](30) NOT NULL, "
                    + "     [CERT_KEY] [varchar](50) NOT NULL, "
                    + "     [FROM_NO] [varchar](20) NOT NULL, "
                    + "     [TO_NO] [varchar](20) NOT NULL, "
                    + "     [TO_NAME] [varchar](20) NOT NULL, "
                    + "     [TXT_TITLE] [varchar](40) NOT NULL, "
                    + "     [TXT_RMK] [varchar](2000) NOT NULL, "
                    + "     [TXT_IMG] [varbinary](max) NULL, "
                    + "     [MMS_YN] [int] NOT NULL, "
                    + "     [SEND_GUBN] [int] NOT NULL, "
                    + "     [SEND_DT] [varchar](20) NOT NULL, "
                    + "     [SEND_STAT] [int] NULL, "
                    + "     [REG_DT] [varchar](20) NOT NULL, "
                    + "     [REG_ID] [varchar](20) NOT NULL, "
                    + " CONSTRAINT [PK_SMS_SEND] PRIMARY KEY CLUSTERED "
                    + " ( "
                    + "     [REF_SLNO] ASC, "
                    + "     [REF_SQNO] ASC "
                    + " )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] "
                    + " ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] ";

                object obj = gd.GetOneData(1, dbname, qry);
            }

            srLabel1.Text = "테이블 생성이 완료되었습니다.";
            Cursor = Cursors.Default;
            btn_tb_c.Enabled = false;

            getEnd_Modi();
        }

        #endregion

        #region 4 Etc

        //종료해도 되는지 확인 후 종료
        private void getEnd_Modi()
        {
            if (btn_tb_c.Enabled == false)            
                this.Close();            
        }
		
             
        //데이터 MODIFICATION
        private void table_modi(string stat)
        {
            Cursor = Cursors.WaitCursor;

            #region // 01.
            if (stat.Substring(0, 2) == "01")
            {
                //string qry = " SELECT * FROM sysobjects WHERE NAME = 'COSTRATE'  ";
                //DataTable dm001 = gd.GetDataInQuery(1, dbname, qry);
                //dp.AddDatatable2Dataset("E_COSTRATE", dm001, ref ds);

                //if (stat.Substring(2, 2) == "01")  //수정여부 체크하여 Enable 상태 변경
                //{
                //    if (ds.Tables["E_COSTRATE"].Rows.Count > 0)
                //    {
                //        srButton1.Enabled = false;
                //    }
                //}
                //else if (stat.Substring(2, 2) == "02") //처리시 테이블생성
                //{
                //    if (ds.Tables["MSTCLNT"].Select("colname = 'CLNTNAM1'")[0]["length"].ToString() != "120")
                //    {
                //        string qry1 = "ALTER TABLE MSTCLNT ALTER COLUMN CLNTNAM1 VARCHAR(120) NOT NULL ";
                //        object obj1 = gd.GetOneData(1, commdb, qry1);
                //    }
                //    if (ds.Tables["MSTCLNT"].Select("colname = 'CLNTNAM2'")[0]["length"].ToString() != "120")
                //    {
                //        string qry1 = " DROP INDEX [CLNTNAM2] ON [dbo].[MSTCLNT] "
                //                    + " ALTER TABLE MSTCLNT ALTER COLUMN CLNTNAM2 VARCHAR(120) NOT NULL "
                //                    + " CREATE NONCLUSTERED INDEX [CLNTNAM2] ON [dbo].[MSTCLNT] "
                //                    + " ( [CLNTNAM2] ASC) ON [PRIMARY] ";
                //        object obj1 = gd.GetOneData(1, commdb, qry1);
                //    }
                //    srButton1.Enabled = false;
                //}
            }
            #endregion

            Cursor = Cursors.Default;
        }

        #endregion
    }
}

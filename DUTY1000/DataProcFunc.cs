using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using SilkRoad.Common;
using SilkRoad.DAL;
using SilkRoad.DataProc;

namespace DUTY1000
{
    class DataProcFunc
    {
        static DataProcessing dp = new DataProcessing();
        static GetData gd = new GetData();
        static SetData sd = new SetData();
        static CommonLibrary clib = new CommonLibrary();
        static string dbname = DataAccess.DBname + SilkRoad.Config.SRConfig.WorkPlaceNo;
        static string wagedb = "WG06DB" + SilkRoad.Config.SRConfig.WorkPlaceNo;
        static string comm_db = "COMMDB" + SilkRoad.Config.SRConfig.WorkPlaceNo;
        //static SilkRoad.BaseCode.BaseCode bc = new BaseCode();

        string substring = (DataAccess.DBtype == "2") ? "SUBSTR" : "SUBSTRING"; //ORACLE(2) 과 MSSQL(1) 구문 차이
        string plus = (DataAccess.DBtype == "2") ? "||" : "+"; //ORACLE(2) 과 MSSQL(1) 구문 차이
        string isnull = (DataAccess.DBtype == "2") ? "NVL" : "ISNULL"; //ORACLE(2) 과 MSSQL(1) 구문 차이


        #region 공통
        
        //최종마감 데이터
        public void Get2020_SEARCH_ENDSDatas(string yymm, DataSet ds)
        {
            try
            {
                string qry = " SELECT * "
                           + "   FROM DUTY_MSTENDS "
                           + "  WHERE END_YYMM = '" + yymm + "' ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("2020_SEARCH_ENDS", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //전체관리,부서관리 체크
        public void GetMSTUSER_CHKDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT * "
                           + "   FROM " + wagedb + ".DBO.MSTEMBS "
                           + "  WHERE EMBSSABN = '" + SilkRoad.Config.SRConfig.USID + "' ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("MSTUSER_CHK", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                        "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //부서장의 관리부서 여부 체크
        public void GetCHK_DEPTDatas(string sabn, string dept, DataSet ds)
        {
            try
            {
                string qry = " SELECT A.*, RTRIM(X1.DEPRNAM1) DEPT_NM "
                           + "   FROM DUTY_PWERDEPT A "
                           + "   LEFT OUTER JOIN MSTDEPR X1 ON A.DEPT=X1.DEPRCODE "
                           + "  WHERE A.SABN = '" + sabn + "' AND A.DEPT LIKE '" + dept + "' ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("CHK_DEPT", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //부서코드 lookup
        public void GetSEARCH_DEPTDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT DEPRCODE CODE, RTRIM(DEPRNAM1) NAME "
                           + "   FROM " + wagedb + ".dbo.MSTDEPR "
                           + "  WHERE DEPRSTAT=1 "
                           + "  ORDER BY DEPRCODE ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SEARCH_DEPT", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void GetSEARCH_DEPT_POWERDatas(int admin_lv, DataSet ds)
        {
            try
            {
                string qry = " SELECT A.DEPRCODE CODE, RTRIM(A.DEPRNAM1) NAME "
                           + "   FROM " + wagedb + ".dbo.MSTDEPR A ";

                if (admin_lv == 1)
                    qry += " INNER JOIN DUTY_PWERDEPT X2 "
                        + "    ON A.DEPRCODE = X2.DEPT "
                        + "  AND X2.SABN = '" + SilkRoad.Config.SRConfig.USID + "' ";

                qry += "  WHERE A.DEPRSTAT=1 "
                    + "  ORDER BY A.DEPRCODE ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SEARCH_DEPT_POWER", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //부서별 직원LOOKUP
        public void Get2020_SEARCH_EMBSDatas(string dept, DataSet ds)
        {
            try
            {
                string qry = " SELECT RTRIM(A.EMBSSABN) CODE, RTRIM(A.EMBSNAME) NAME, "
                           + "        RTRIM(X1.DEPRNAM1) DEPT_NM, "
                           + "        (CASE A.EMBSSTAT WHEN 1 THEN '재직' WHEN 2 THEN '퇴직' ELSE '' END) STAT_NM, "
                           + "        ISNULL(A.EMBSADGB,'') EMBSADGB "
                           + "   FROM " + wagedb + ".dbo.MSTEMBS A "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X1 "
                           + "     ON A.EMBSDPCD = X1.DEPRCODE"
                           + "  WHERE A.EMBSDPCD LIKE '" + dept + "' " //A.EMBSSTAT='1' AND 
                           + "  ORDER BY A.EMBSSTAT, A.EMBSNAME";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("2020_SEARCH_EMBS", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //부서별 직원LOOKUP
        public void GetSEARCH_EMBS_POWERDatas(int admin_lv, DataSet ds)
        {
            try
            {
                string qry = " SELECT RTRIM(A.EMBSSABN) CODE, RTRIM(A.EMBSNAME) NAME, "
                           + "        RTRIM(X1.DEPRNAM1) DEPT_NM, "
                           + "        (CASE A.EMBSSTAT WHEN 1 THEN '재직' WHEN 2 THEN '퇴직' ELSE '' END) STAT_NM, "
                           + "        A.EMBSSTAT, ISNULL(A.EMBSADGB,'') EMBSADGB "
                           + "   FROM " + wagedb + ".dbo.MSTEMBS A "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X1 "
                           + "     ON A.EMBSDPCD = X1.DEPRCODE ";
                if (admin_lv == 1)
                    qry += " INNER JOIN DUTY_PWERDEPT X2 "
                        + "   ON A.EMBSDPCD = X2.DEPT "
                        + "  AND X2.SABN = '" + SilkRoad.Config.SRConfig.USID + "' ";

                qry += "  ORDER BY A.EMBSSTAT, A.EMBSDPCD, A.EMBSNAME";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SEARCH_EMBS_POWER", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //직원코드 lookup
        public void GetSEARCH_EMBSDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT RTRIM(EMBSSABN) CODE, RTRIM(EMBSNAME) NAME "
                           + "   FROM " + wagedb + ".DBO.MSTEMBS "
                           + "  ORDER BY EMBSSABN ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SEARCH_EMBS", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region 1050 - 간호사정보관리

        public void GetSEARCH_MSTNURSDatas(string dept, string stat, DataSet ds)
		{
			try
			{
				string qry = " SELECT A1.*, "
						   + "		  (CASE WHEN A1.GUBN='1' THEN '등록' ELSE '미등록' END) GUBN_NM, "
						   + "        (CASE WHEN A1.STAT=1 THEN '정상' ELSE '사용중지' END) STAT_NM, "
						   + "        RTRIM(ISNULL(X3.DEPRNAM1,'')) DEPT_NM, "
						   + "        RTRIM(cast(DECRYPTBYPASSPHRASE('samilpas',X2.EMBSPHPN) as varchar(100))) HPNO, RTRIM(X2.EMBSEMAL) AS EMAIL_ID "
						   + "  FROM ( "
						   + "		SELECT '1' GUBN, A.SAWON_NO, A.SAWON_NM, ISNULL(X1.EMBSDPCD,'') DEPTCODE, "
						   + "             A.ALLOWOFF, A.LIMIT_OFF, " 
						   + "			   A.STAT, A.LDAY, A.INDT, A.UPDT, A.USID, A.PSTY "
						   + "		  FROM DUTY_MSTNURS A "
						   + "        LEFT OUTER JOIN " + wagedb + ".DBO.MSTEMBS X1 ON A.SAWON_NO=X1.EMBSSABN "
						   + "     UNION ALL "
						   + "		SELECT '2' GUBN, RTRIM(A.EMBSSABN) AS SAWON_NO, RTRIM(A.EMBSNAME) AS SAWON_NM, A.EMBSDPCD AS DEPTCODE, "
						   + "             0 ALLOWOFF, 0 LIMIT_OFF, "
						   + "			   1 STAT, '' LDAY, '' INDT, '' UPDT, '' USID, '' PSTY "
						   + "		  FROM " + wagedb + ".DBO.MSTEMBS A "
						   + "        LEFT OUTER JOIN DUTY_MSTNURS X1 ON A.EMBSSABN=X1.SAWON_NO "
						   + "		 WHERE A.EMBSSTAT=1 AND A.EMBSDPCD IN (SELECT DEPTCODE FROM DUTY_INFONURS) AND X1.SAWON_NO IS NULL ) A1 "
						   + "  LEFT OUTER JOIN " + wagedb + ".DBO.MSTEMBS X2 ON A1.SAWON_NO=X2.EMBSSABN "
						   + "  LEFT OUTER JOIN " + wagedb + ".DBO.MSTDEPR X3 ON A1.DEPTCODE=X3.DEPRCODE "
						   + " WHERE A1.DEPTCODE LIKE '" + dept + "'";
				if (stat != "%")
					qry += " AND A1.STAT = " + stat + " ";
				qry += " ORDER BY A1.GUBN DESC, A1.DEPTCODE, A1.SAWON_NO ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SEARCH_MSTNURS", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//간호사코드 불러오기
		public void GetDUTY_MSTNURSDatas(string code, DataSet ds)
		{
			try
			{
				string qry = " SELECT * "
						   + "   FROM DUTY_MSTNURS "
						   + "  WHERE SAWON_NO = '" + code + "' ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("DUTY_MSTNURS", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion

		#region 1051 - 간호사직종설정

		//직종코드 불러오기
		public void GetSEARCH_MSTJONGDatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT (CASE WHEN X1.JONGCODE IS NOT NULL THEN '1' ELSE '0' END) CHK, "
						   + "        RTRIM(JONGNAM1) JONGNAME, A.*  "
						   + "   FROM " + wagedb + ".DBO.MSTJONG A "
						   + "   LEFT OUTER JOIN DUTY_INFOJONG X1 ON A.JONGCODE=X1.JONGCODE ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SEARCH_MSTJONG", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		//삭제할 직종설정 테이블 불러오기
		public void GetD_DUTY_INFOJONGDatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT * "
						   + "   FROM DUTY_INFOJONG A ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("D_DUTY_INFOJONG", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		//등록할 직종설정 테이블 불러오기
		public void GetDUTY_INFOJONGDatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT * "
						   + "   FROM DUTY_INFOJONG A ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("DUTY_INFOJONG", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion
		
		#region 1011 - 부서-직원설정

		//부서코드 불러오기
		public void GetSEARCH_MSTDEPTDatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT '' CODE, '없음' NAME "
						   + "  UNION ALL "
						   + " SELECT DEPRCODE CODE, RTRIM(DEPRNAM1) NAME "
						   + "   FROM " + wagedb + ".dbo.MSTDEPR "
						   + "  WHERE DEPRSTAT=1"
						   + "  ORDER BY CODE ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SEARCH_MSTDEPT", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//해당부서의 직원 불러오기
		public void GetSEARCH_DEPT_NURSDatas(string dept, DataSet ds)
		{
			try
			{
				string qry = " SELECT A.*, RTRIM(A.EMBSSABN) SAWON_NO, RTRIM(A.EMBSNAME) SAWON_NM, "
						   + "        A.EMBSDPCD DEPTCODE, RTRIM(X1.DEPRNAM1) DEPT_NM, '0' CHK "
						   + "   FROM " + wagedb + ".DBO.MSTEMBS A "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X1 "
						   + "     ON A.EMBSDPCD = X1.DEPRCODE "
						   + "  WHERE A.EMBSSTAT='1' AND A.EMBSDPCD LIKE '" + dept + "'";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SEARCH_NURS", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//부서이력 테이블 구조 불러오기
		public void GetDUTY_TRSDEPTDatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT * "
						   + "   FROM DUTY_TRSDEPT WHERE 1=2 ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("DUTY_TRSDEPT", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//해당직원 부서이력 불러오기
		public void GetS_TRSDEPTDatas(string sabn, DataSet ds)
		{
			try
			{
				string qry = " SELECT A.*,  "
						   + "        (CASE WHEN A.MOVE_DATE<>'' THEN LEFT(A.MOVE_DATE,4)+'.'+SUBSTRING(A.MOVE_DATE,5,2)+'.'+SUBSTRING(A.MOVE_DATE,7,2) "
						   + "              ELSE '' END) MOVE_DATE_NM,"
						   + "        A.FR_DEPT+"
						   + "        (CASE WHEN A.FR_DEPT='' THEN '' ELSE '('+RTRIM(ISNULL(X1.DEPRNAM1,''))+')' END) + ' -> '+A.TO_DEPT+ "
						   + "        (CASE WHEN A.TO_DEPT='' THEN '' ELSE '('+RTRIM(ISNULL(X2.DEPRNAM1,''))+')' END) AS DEPT_LOG, "
						   + "        (CASE WHEN X3.USERNAME IS NULL THEN A.REG_ID "
						   + "              ELSE A.REG_ID+'('+RTRIM(ISNULL(X3.USERNAME,''))+')' END) AS REG_ID_NM "
						   + "   FROM DUTY_TRSDEPT A "
						   + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X1 "
						   + "     ON A.FR_DEPT = X1.DEPRCODE "
						   + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X2 "
						   + "     ON A.TO_DEPT = X2.DEPRCODE "
						   + "   LEFT OUTER JOIN SILKDBCM.DBO.MSTUSER X3 "
						   + "     ON A.REG_ID = X3.USERIDEN "
						   + "  WHERE A.SAWON_NO = '" + sabn + "' "
						   + "  ORDER BY A.DEPT_SEQ ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("S_TRSDEPT", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//직원 부서이력 불러오기
		public void GetSEARCH_TRSDEPTDatas(string frdt, string todt, DataSet ds)
		{
			try
			{
				string qry = " SELECT A.*,  "
						   + "        (CASE WHEN A.MOVE_DATE<>'' THEN LEFT(A.MOVE_DATE,4)+'.'+SUBSTRING(A.MOVE_DATE,5,2)+'.'+SUBSTRING(A.MOVE_DATE,7,2) "
						   + "              ELSE '' END) MOVE_DATE_NM,"
						   + "        RTRIM(X1.EMBSNAME) EMBSNAME, "
						   + "        A.FR_DEPT+"
						   + "        (CASE WHEN A.FR_DEPT='' THEN '' ELSE '('+RTRIM(ISNULL(X2.DEPRNAM1,''))+')' END) + ' -> '+A.TO_DEPT+ "
						   + "        (CASE WHEN A.TO_DEPT='' THEN '' ELSE '('+RTRIM(ISNULL(X3.DEPRNAM1,''))+')' END) AS DEPT_LOG, "
						   + "        (CASE WHEN X4.USERNAME IS NULL THEN A.REG_ID "
						   + "              ELSE A.REG_ID+'('+RTRIM(ISNULL(X4.USERNAME,''))+')' END) AS REG_ID_NM "
						   + "   FROM DUTY_TRSDEPT A "
						   + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTEMBS X1 "
                           + "     ON A.SAWON_NO = X1.EMBSSABN "
						   + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X2 "
						   + "     ON A.FR_DEPT = X2.DEPRCODE "
						   + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X3 "
						   + "     ON A.TO_DEPT = X3.DEPRCODE "
						   + "   LEFT OUTER JOIN SILKDBCM.DBO.MSTUSER X4 "
						   + "     ON A.REG_ID = X4.USERIDEN "
						   + "  WHERE A.MOVE_DATE BETWEEN '" + frdt + "' AND '" + todt + "' "
						   + "  ORDER BY A.MOVE_DATE, A.SAWON_NO, A.DEPT_SEQ ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SEARCH_TRSDEPT", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion
				
		#region 1012 - 파트-직원설정

		//파트코드 불러오기
		public void GetSEARCH_MSTPARTDatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT '' CODE, '없음' NAME "
						   + "  UNION ALL "
						   + " SELECT PARTCODE CODE, RTRIM(PARTNAME) NAME "
						   + "   FROM DUTY_MSTPART ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SEARCH_MSTPART", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//해당파트의 간호사 불러오기
		public void GetSEARCH_PART_NURSDatas(string part, DataSet ds)
		{
			try
			{
				string qry = " SELECT A.*, '0' CHK, RTRIM(X1.PARTNAME) PARTNAME, "
						   + "        (CASE A.EXP_LV WHEN 1 THEN '전문가' WHEN 2 THEN '숙련가' WHEN 3 THEN '초보자' ELSE '' END) EXP_LV_NM "
						   + "   FROM DUTY_MSTNURS A "
						   + "   LEFT OUTER JOIN DUTY_MSTPART X1 "
						   + "     ON A.PARTCODE = X1.PARTCODE "
						   + "  WHERE A.PARTCODE LIKE '" + part + "'";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SEARCH_NURS", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//파트이력 테이블 구조 불러오기
		public void GetDUTY_TRSPARTDatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT * "
						   + "   FROM DUTY_TRSPART WHERE 1=2 ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("DUTY_TRSPART", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//해당직원 파트이력 불러오기
		public void GetS_TRSPARTDatas(string sabn, DataSet ds)
		{
			try
			{
				string qry = " SELECT A.*,  "
						   + "        A.FR_PART+"
						   + "        (CASE WHEN A.FR_PART='' THEN '' ELSE '('+RTRIM(ISNULL(X1.PARTNAME,''))+')' END) + ' -> '+A.TO_PART+ "
						   + "        (CASE WHEN A.TO_PART='' THEN '' ELSE '('+RTRIM(ISNULL(X2.PARTNAME,''))+')' END) AS PART_LOG, "
						   + "        (CASE WHEN X3.USERNAME IS NULL THEN A.REG_ID "
						   + "              ELSE A.REG_ID+'('+RTRIM(ISNULL(X3.USERNAME,''))+')' END) AS REG_ID_NM "
						   + "   FROM DUTY_TRSPART A "
						   + "   LEFT OUTER JOIN DUTY_MSTPART X1 "
						   + "     ON A.FR_PART = X1.PARTCODE "
						   + "   LEFT OUTER JOIN DUTY_MSTPART X2 "
						   + "     ON A.TO_PART = X2.PARTCODE "
						   + "   LEFT OUTER JOIN SILKDBCM.DBO.MSTUSER X3 "
						   + "     ON A.REG_ID = X3.USERIDEN "
						   + "  WHERE A.SAWON_NO = '" + sabn + "' "
						   + "  ORDER BY A.PART_SEQ ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("S_TRSPART", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion
				
		#region 1030 - 근무유형관리

		//근무유형 전체조회
		public void GetSEARCH_MSTGNMUDatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT A.*, "
						   + "        (CASE A.G_TYPE WHEN 1 THEN 'Day' WHEN 2 THEN 'Evening' WHEN 3 THEN 'Night' WHEN 4 THEN 'Off' "
						   + "			             WHEN 5 THEN 'Day like' WHEN 6 THEN 'Off like' WHEN 7 THEN 'N감독'"
						   + "			             WHEN 8 THEN '당직' WHEN 9 THEN 'Call' WHEN 10 THEN 'Call대기' "
                           + "			             WHEN 11 THEN '월차' WHEN 12 THEN '연차' WHEN 13 THEN '경조외(유급)'  WHEN 14 THEN '경조외(무급)' "
                           + "			             WHEN 15 THEN '시차' "
                           + "                       ELSE '' END) G_TYPE_NM "
						   + "   FROM DUTY_MSTGNMU A "
						   + "  ORDER BY A.G_CODE ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SEARCH_MSTGNMU", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//근무유형 처리
		public void GetDUTY_MSTGNMUDatas(string code, DataSet ds)
		{
			try
			{
				string qry = " SELECT * "
						   + "   FROM DUTY_MSTGNMU "
						   + "  WHERE G_CODE = '" + code + "' ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("DUTY_MSTGNMU", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//근무유형 다른테이블에서 사용중인지 체크
		public void GetCHK_GNMUDatas(string code, DataSet ds)
		{
			try
			{
				string qry = " SELECT '경조외/근무신청' CHK_NM "
						   + "   FROM DUTY_TRSOREQ "
						   + "  WHERE REQ_TYPE = '" + code + "' "
						   + "    AND PSTY<>'D' ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("CHK_GNMU", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion

		#region 1040 - 휴일설정
		
		//휴일조회
		public void GetSEARCH_HolidayDatas(string bf_year, string year, DataSet ds)
		{
			try
			{
				string qry = " SELECT H_DATE, H_NAME, REPEAT_CHK, LEFT(DATENAME(DW,H_DATE),1) AS DD_NM, "
						   + "        LEFT(H_DATE,4)+'-'+SUBSTRING(H_DATE,5,2)+'-'+SUBSTRING(H_DATE,7,2) AS DD, "
						   + "        GUBN, (CASE GUBN WHEN '1' THEN '휴일' ELSE '대체휴일' END) GUBN_NM, "
						   + "        '등록' STAT_NM "
						   + "   FROM DUTY_MSTHOLI A "
						   + "  WHERE H_DATE LIKE '" + year + "%'"
						   //+ "    AND H_DATE NOT IN ( SELECT CONVERT(CHAR(8),DATEADD(YEAR,1,H_DATE),112) FROM DUTY_MSTHOLI "
						   //+ "                         WHERE H_DATE LIKE '" + bf_year + "%' AND REPEAT_CHK = '1') "
						   + "  UNION ALL"
						   + " SELECT CONVERT(CHAR(8),DATEADD(YEAR,1,H_DATE),112) H_DATE, H_NAME, REPEAT_CHK, "
						   + "        LEFT(DATENAME(DW,CONVERT(CHAR(8),DATEADD(YEAR,1,H_DATE),112)),1) AS DD_NM, "
						   + "        LEFT(CONVERT(CHAR(8),DATEADD(YEAR,1,H_DATE),112),4)+'-'+SUBSTRING(CONVERT(CHAR(8),DATEADD(YEAR,1,H_DATE),112),5,2)+'-'+SUBSTRING(CONVERT(CHAR(8),DATEADD(YEAR,1,H_DATE),112),7,2) AS DD, "
						   + "        GUBN, (CASE GUBN WHEN '1' THEN '휴일' ELSE '대체휴일' END) GUBN_NM, "
						   + "        '미등록' STAT_NM "
						   + "   FROM DUTY_MSTHOLI A "
						   + "  WHERE H_DATE LIKE '" + bf_year + "%'"
						   + "    AND REPEAT_CHK = '1' "
						   + "    AND H_DATE NOT IN ( SELECT CONVERT(CHAR(8),DATEADD(YEAR,-1,H_DATE),112) FROM DUTY_MSTHOLI "
						   + "                         WHERE H_DATE LIKE '" + year + "%' AND REPEAT_CHK = '1') "
						   + "  ORDER BY H_DATE ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SEARCH_HOLIDAY", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//휴일설정 테이블 구조 불러오기
		public void GetDUTY_MSTHOLIDatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT * "
						   + "   FROM DUTY_MSTHOLI WHERE 1=2 ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("DUTY_MSTHOLI", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//휴일설정 기존데이터 불러오기
		public void GetD_DUTY_MSTHOLIDatas(string year, DataSet ds)
		{
			try
			{
				string qry = " SELECT * "
						   + "   FROM DUTY_MSTHOLI "
						   + "  WHERE H_DATE LIKE '" + year +"%' ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("D_DUTY_MSTHOLI", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion


		
		#region wage3220 - 고정항목관리
		
		public void GetFIX_SDDatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT A.* "
						   + "   FROM " + wagedb + ".dbo.INFOWAGE A "
						   + "   LEFT OUTER JOIN (SELECT DISTINCT IFJWCODE, IFJWJYGB FROM INFOJOWG WHERE IFJWJYGB='11') X1 "
						   + "     ON A.IFWGCODE=X1.IFJWCODE "
						   + "  WHERE (A.IFWGCRGB='1' AND A.IFWGJYGB='11') OR (A.IFWGCRGB='2' AND X1.IFJWJYGB='11') "
						   + "  ORDER BY A.IFWGCODE ";
				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("FIX_SD", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		//고정항목처리
		public void GetWGFX_LISTDatas(string dept, string tsdt, DataSet ds)
		{
			try
			{
				string qry = " SELECT RTRIM(A.WGFXSABN) AS SABN, "
					       + "        RTRIM(X1.EMBSNAME) AS EMBSNAME, "
					       + "        RTRIM(ISNULL(X2.DEPRNAM1,'')) AS DEPT_NM, A.* "
						   + "   FROM " + wagedb + ".dbo.MSTWGFX A "
						   + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTEMBS X1 ON A.WGFXSABN=X1.EMBSSABN "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTDEPR X2 ON X1.EMBSDPCD=X2.DEPRCODE "
                           + "  WHERE X1.EMBSDPCD LIKE '" + dept + "'"
						   + "    AND (X1.EMBSSTAT='1' OR X1.EMBSTSDT > '" + tsdt + "')"
						   + "  ORDER BY A.WGFXSABN ";
				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("WGFX_LIST", dt, ref ds);

				qry = " SELECT RTRIM(A.EMBSSABN) AS SABN, "
					+ "        RTRIM(A.EMBSNAME) AS SABN_NM, "
					+ "        RTRIM(ISNULL(X1.DEPRNAM1,'')) AS DEPT_NM "
					+ "   FROM " + wagedb + ".dbo.MSTEMBS A "
					+ "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTDEPR X1 ON A.EMBSDPCD=X1.DEPRCODE "
                    + "  WHERE A.EMBSDPCD LIKE '" + dept + "'"
					+ "    AND (A.EMBSSTAT='1' OR A.EMBSTSDT > '" + tsdt + "')"
					+ "  ORDER BY A.EMBSJOCD, A.EMBSDPCD, A.EMBSSABN ";

				dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("EMBS_LIST", dt, ref ds);

				for (int i = 0; i < ds.Tables["EMBS_LIST"].Rows.Count; i++)
				{
					DataRow drow = ds.Tables["EMBS_LIST"].Rows[i];
					if (ds.Tables["WGFX_LIST"].Select("SABN = '" + drow["SABN"].ToString() + "'").Length == 0)
					{
						DataRow nrow = ds.Tables["WGFX_LIST"].NewRow();
						nrow["SABN"] = drow["SABN"].ToString();
						nrow["EMBSNAME"] = drow["SABN_NM"].ToString();
						nrow["DEPT_NM"] = drow["DEPT_NM"].ToString();
						ds.Tables["WGFX_LIST"].Rows.Add(nrow);
					}
				}
				ds.Tables["WGFX_LIST"].DefaultView.Sort = "SABN ASC";
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void GetMSTWGFXDatas(string sabn, DataSet ds)
		{
			try
			{
				string qry = " SELECT A.* "
						   + "   FROM " + wagedb + ".dbo.MSTWGFX A "
						   + "  WHERE A.WGFXSABN LIKE '" + sabn + "'"
						   + "  ORDER BY A.WGFXSABN ";
				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("MSTWGFX", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion

		#region wage3230 - 변동항목관리
		
		public void GetWGPC_SDDatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT A.* "
						   + "   FROM " + wagedb + ".dbo.INFOWAGE A "
						   + "   LEFT OUTER JOIN (SELECT DISTINCT IFJWCODE, IFJWJYGB FROM INFOJOWG WHERE IFJWJYGB='12') X1 "
						   + "     ON A.IFWGCODE=X1.IFJWCODE "
						   + "  WHERE ((A.IFWGCRGB='1' AND A.IFWGJYGB IN ('12','39','40','41','43')) "
						   + "        OR (A.IFWGCRGB='2' AND X1.IFJWJYGB='12')) "
						   + "    AND A.IFWGPUSE='1' "
						   + "  ORDER BY A.IFWGCODE ";
				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("WGPC_SD", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		//고정항목처리
		public void GetWGPC_LISTDatas(string yymm, string sqno, string dept, string tsdt, DataSet ds)
		{
			try
			{
				string qry = " SELECT RTRIM(A.WGPCSABN) AS SABN, "
					       + "        RTRIM(X1.EMBSNAME) AS EMBSNAME, "
					       + "        RTRIM(ISNULL(X2.DEPRNAM1,'')) AS DEPT_NM, A.* "
						   + "   FROM " + wagedb + ".dbo.MSTWGPC A "
						   + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTEMBS X1 ON A.WGPCSABN=X1.EMBSSABN "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTDEPR X2 ON X1.EMBSDPCD=X2.DEPRCODE "
                           + "  WHERE A.WGPCYYMM = '" + yymm + "'"
						   + "    AND A.WGPCSQNO = '" + sqno + "'"
						   + "    AND X1.EMBSDPCD LIKE '" + dept + "'"
						   + "    AND (X1.EMBSSTAT='1' OR X1.EMBSTSDT > '" + tsdt + "')"
						   + "  ORDER BY A.WGPCSABN ";
				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("WGPC_LIST", dt, ref ds);

				qry = " SELECT RTRIM(A.EMBSSABN) AS SABN, "
					+ "        RTRIM(A.EMBSNAME) AS SABN_NM, "
					+ "        RTRIM(ISNULL(X1.DEPRNAM1,'')) AS DEPT_NM "
					+ "   FROM " + wagedb + ".dbo.MSTEMBS A "
					+ "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTDEPR X1 ON A.EMBSDPCD=X1.DEPRCODE "
                    + "  WHERE A.EMBSDPCD LIKE '" + dept + "'"
					+ "    AND (A.EMBSSTAT='1' OR A.EMBSTSDT > '" + tsdt + "')"
					+ "  ORDER BY A.EMBSJOCD, A.EMBSDPCD, A.EMBSSABN ";

				dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("EMBS_LIST", dt, ref ds);

				for (int i = 0; i < ds.Tables["EMBS_LIST"].Rows.Count; i++)
				{
					DataRow drow = ds.Tables["EMBS_LIST"].Rows[i];
					if (ds.Tables["WGPC_LIST"].Select("SABN = '" + drow["SABN"].ToString() + "'").Length == 0)
					{
						DataRow nrow = ds.Tables["WGPC_LIST"].NewRow();
						nrow["WGPCYYMM"] = yymm;
						nrow["WGPCSQNO"] = sqno;
						nrow["SABN"] = drow["SABN"].ToString();
						nrow["EMBSNAME"] = drow["SABN_NM"].ToString();
						nrow["DEPT_NM"] = drow["DEPT_NM"].ToString();
						ds.Tables["WGPC_LIST"].Rows.Add(nrow);
					}
				}
				ds.Tables["WGPC_LIST"].DefaultView.Sort = "SABN ASC";
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void GetMSTWGPCDatas(string yymm, string sqno, string sabn, DataSet ds)
		{
			try
			{
				string qry = " SELECT A.* "
						   + "   FROM " + wagedb + ".dbo.MSTWGPC A "
						   + "  WHERE A.WGPCYYMM = '" + yymm + "'"
						   + "    AND A.WGPCSQNO = '" + sqno + "'"
						   + "    AND A.WGPCSABN = '" + sabn + "'"
						   + "  ORDER BY A.WGPCSABN ";
				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("MSTWGPC", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        #endregion
        


        #region 4010 - 공지사항관리

        //공지사항 전체조회
        public void GetSEARCH_TRSNOTIDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT A.*, "
                           + "        RTRIM(ISNULL(X1.DEPRNAM1,'전체부서')) DEPT_NM, "
                           + "        LEFT(A.NOTIDATE,4)+'-'+SUBSTRING(A.NOTIDATE,5,2)+'-'+SUBSTRING(A.NOTIDATE,7,2) AS NOTIDATE_NM "
                           + "   FROM DUTY_TRSNOTI A "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X1 "
                           + "     ON A.DEPTCODE=X1.DEPRCODE "
                           + "  WHERE A.PSTY <> 'D' ORDER BY A.NOTIDATE DESC, A.IDX DESC ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SEARCH_TRSNOTI", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //공지사항 불러오기
        public void GetDUTY_TRSNOTIDatas(int idx, DataSet ds)
        {
            try
            {
                string qry = " SELECT * "
                           + "   FROM DUTY_TRSNOTI WHERE IDX = " + idx + " ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DUTY_TRSNOTI", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //IDX 키값 불러오기
        public decimal GetIDX_DUTY_TRSNOTIDatas(DataSet ds)
        {
            decimal idx = 0;
            try
            {
                string qry = " SELECT ISNULL(MAX(IDX),0) + 1 "
                           + "   FROM DUTY_TRSNOTI ";

                object obj = gd.GetOneData(1, dbname, qry);
                idx = clib.TextToDecimal(obj.ToString());
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return idx;
        }

        #endregion



        #region 2010 - 콜근무관리

        //콜조회
        public void GetSEARCH_CALLDatas(int admin_lv, string frmm, string tomm, string dept, DataSet ds)
        {
            try
            {
                string qry = " SELECT A.*, "
                           + "        (CASE WHEN A.CALL_GUBN='1' THEN '콜' ELSE '콜대기' END) GUBN_NM, "
                           + "		  RTRIM(ISNULL(X4.DEPRNAM1,'')) AS DEPT_NM, "
                           + "        LEFT(A.CALL_DATE,4)+'-'+SUBSTRING(A.CALL_DATE,5,2) AS CALL_YYMM, "
                           + "        LEFT(A.CALL_DATE,4)+'-'+SUBSTRING(A.CALL_DATE,5,2)+'-'+SUBSTRING(A.CALL_DATE,7,2) AS SLDT_NM, "
                           + "        RTRIM(ISNULL(X3.EMBSNAME,'')) SAWON_NM "
                           + "   FROM DUTY_TRSCALL A "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTEMBS X3 "
                           + "     ON A.SABN=X3.EMBSSABN "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X4 "
                           + "     ON X3.EMBSDPCD=X4.DEPRCODE ";
                if (admin_lv == 1)
                    qry += " INNER JOIN DUTY_PWERDEPT X5 "
                        + "     ON X3.EMBSDPCD = X5.DEPT "
                        + "    AND X5.SABN = '" + SilkRoad.Config.SRConfig.USID + "' ";

                qry += " WHERE LEFT(A.CALL_DATE,6) BETWEEN '" + frmm + "' AND '" + tomm + "' "              
                    + "    AND X3.EMBSDPCD LIKE '" + dept + "' "
                    + "  ORDER BY A.CALL_GUBN, X3.EMBSDPCD, A.SABN, A.CALL_DATE  ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SEARCH_CALL", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //콜 조회
        public void GetDUTY_TRSCALLDatas(string sabn, string sldt, string gubn, DataSet ds)
        {
            try
            {
                string qry = " SELECT A.* "
                           + "   FROM DUTY_TRSCALL A "
                           + "  WHERE A.SABN = '" + sabn + "'"
                           + "    AND A.CALL_DATE = '" + sldt + "' AND A.CALL_GUBN = '" + gubn + "'";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DUTY_TRSCALL", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //콜 삭제 테이블 구조
        public void GetDEL_TRSCALLDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT * "
                           + "   FROM DEL_TRSCALL "
                           + "  WHERE 1=2";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DEL_TRSCALL", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 2020 - OT근무관리

		//OT조회
		public void GetSEARCH_OVTMDatas(int admin_lv, string frmm, string tomm, string dept, DataSet ds)
		{
			try
			{
                string qry = " SELECT A.*, "
                           + "        (CASE A.OT_GUBN WHEN '1' THEN '연장(수당)' WHEN '2' THEN '휴일' ELSE '연장(시차)' END) GUBN_NM, "
                           + "        (CASE A.OT_GUBN WHEN '3' THEN 0 ELSE OT_TIME END) SD_TIME, "
                           + "        (CASE A.OT_GUBN WHEN '3' THEN OT_TIME * 1.5 ELSE 0 END) TREQ_TIME, " 
                           + "		  RTRIM(ISNULL(X4.DEPRNAM1,'')) AS DEPT_NM, "
                           + "        LEFT(A.OT_DATE,4)+'-'+SUBSTRING(A.OT_DATE,5,2) AS CALL_YYMM, "
                           + "        LEFT(A.OT_DATE,4)+'-'+SUBSTRING(A.OT_DATE,5,2)+'-'+SUBSTRING(A.OT_DATE,7,2) AS SLDT_NM, "
                           + "        RTRIM(ISNULL(X3.EMBSNAME,'')) SAWON_NM "
                           + "   FROM DUTY_TRSOVTM A "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTEMBS X3 "
                           + "     ON A.SABN=X3.EMBSSABN "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X4 "
                           + "     ON X3.EMBSDPCD=X4.DEPRCODE ";
                if (admin_lv == 1)
                    qry += " INNER JOIN DUTY_PWERDEPT X5 "
                        + "     ON X3.EMBSDPCD = X5.DEPT "
                        + "    AND X5.SABN = '" + SilkRoad.Config.SRConfig.USID + "' ";

                qry += " WHERE LEFT(A.OT_DATE,6) BETWEEN '" + frmm + "' AND '" + tomm + "' "
                    + "    AND X3.EMBSDPCD LIKE '" + dept + "' "
                    + "  ORDER BY A.OT_GUBN, X3.EMBSDPCD, A.SABN, A.OT_DATE  ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SEARCH_OVTM", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//OT 조회
		public void GetDUTY_TRSOVTMDatas(string sabn, string sldt, string gubn, DataSet ds)
		{
			try
			{
				string qry = " SELECT A.* "
						   + "   FROM DUTY_TRSOVTM A "
						   + "  WHERE A.SABN = '" + sabn + "'"
						   + "    AND A.OT_DATE = '" + sldt + "' AND A.OT_GUBN = '" + gubn + "'";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("DUTY_TRSOVTM", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}		
		//OT 삭제 테이블 구조
		public void GetDEL_TRSOVTMDatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT * "
						   + "   FROM DEL_TRSOVTM "
						   + "  WHERE 1=2";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("DEL_TRSOVTM", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion
                


        #region 2060 - 당직관리

        //부서코드 불러오기
        public void Get2060_DANGDEPTDatas(string yymm, DataSet ds)
        {
            try
            {
                string qry = "";
                qry = " SELECT A.DEPRCODE CODE, RTRIM(DEPRNAM1) DEPT_NM, "
                    + "        (CASE WHEN X2.DEPTCODE IS NOT NULL THEN '1' ELSE '0' END) CHK "
                    + "   FROM " + wagedb + ".DBO.MSTDEPR A "
                    + "  INNER JOIN DUTY_INFODANG X1 ON A.DEPRCODE=X1.DEPTCODE "
                    + "   LEFT OUTER JOIN (SELECT DISTINCT DEPTCODE FROM DUTY_TRSDANG WHERE PLANYYMM='" + yymm + "' AND YYMM_SQ=1 ) X2 "
                    + "     ON A.DEPRCODE=X2.DEPTCODE "
                    + "  WHERE A.DEPRSTAT=1 "
                    + "  ORDER BY A.DEPRCODE ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("2060_DANGDEPT", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //당직 근무유형 조회
        public void Get2060_DANG_GNMUDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT A.*, "
                           + "        (CASE WHEN A.G_TYPE IN (1,2,3,4,5,6,7,9,10) THEN '2.근무' "
                           + "			    WHEN A.G_TYPE IN (11,12) THEN '4.연차' WHEN A.G_TYPE IN (8) THEN '1.당직' "
                           + "			    WHEN A.G_TYPE IN (13,14) THEN '5.경조외' "
                           + "			    WHEN A.G_TYPE IN (15) THEN '3.시차' "
                           + "              ELSE '' END) G_TYPE_NM "
                           + "   FROM DUTY_MSTGNMU A "
                           + "  ORDER BY G_TYPE_NM, A.G_CODE ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("2060_DANG_GNMU", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //당직부서 직원lookup
        public void GetLOOK_DANG_EMBSDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT A.EMBSSABN CODE, RTRIM(A.EMBSNAME) NAME, "
                           + "        A.EMBSDPCD, RTRIM(X1.DEPRNAM1) AS DEPT_NM "
                           + "   FROM " + wagedb + ".dbo.MSTEMBS A "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X1 "
                           + "     ON A.EMBSDPCD = X1.DEPRCODE "
                           + "   INNER JOIN DUTY_INFODANG X2 "
                           + "     ON A.EMBSDPCD = X2.DEPTCODE "
                           + "  ORDER BY A.EMBSDPCD, A.EMBSSABN ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("LOOK_DANG_EMBS", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //결재상신 체크xxx
        public void Get2060_GW_CHKDatas(string gubn, string yymm, string dept, DataSet ds)
		{
			try
			{
				string tb_nm = gubn == "5" ? "GW_TRSDANG" : "GW_TRSPLAN";
				string qry = " SELECT A.* "
						   + "   FROM (SELECT DISTINCT DOC_NO FROM " + tb_nm +" WHERE PLANYYMM = '" + yymm + "' AND DEPTCODE = '" + dept + "') A "
						   + "   INNER JOIN DUTY_GWDOC X1 ON A.DOC_NO=X1.DOC_NO AND AP_TAG NOT IN ('2','5') AND ISNULL(X1.ETC_GUBN,'')<>'1' ";
				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("2060_GW_CHK", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//출근내역 조회xxx
		public void Get2060_SEARCH_KTDatas(string yymm, string dept, DataSet ds)
		{
			try
			{
				string where1 = dept == "A001" ? "SAWON_NO IN (SELECT SAWON_NO FROM DUTY_TRSPLAN_ETC WHERE DEPTCODE = '" + dept + "')" : "DEPTCODE = '" + dept + "'";
				string where2 = dept == "A001" ? "EMBSSABN IN (SELECT SAWON_NO FROM DUTY_TRSPLAN_ETC WHERE DEPTCODE = '" + dept + "')" : "EMBSDPCD = '" + dept + "'";

				if (dept == "A002")
				{
					where1 = "SAWON_NO IN (SELECT EMBSSABN FROM " + wagedb + ".DBO.MSTEMBS WHERE EMBSADGB = '1')";
					where2 = "EMBSADGB = '1'";
				}

				string qry = "  IF EXISTS(SELECT SAWON_NO FROM DUTY_TRSDANG WHERE PLANYYMM='" + yymm + "' AND DEPTCODE = '" + dept + "' ) "
						   + "  BEGIN "
						   + "      SELECT RIGHT(A.USERID,5) as SABN, X1.USERNAME, CONVERT(VARCHAR,WORKSTART,112) SLDT "
						   + "        FROM TB_WORKRESULT A LEFT OUTER JOIN TB_USER X1 ON A.USERID=X1.USERID "
						   + "       WHERE RIGHT(A.USERID,5) in (SELECT SAWON_NO FROM DUTY_TRSDANG WHERE PLANYYMM='" + yymm + "' AND " + where1 +") " //DEPTCODE = '" + dept + "' )"
						   + "         AND CONVERT(VARCHAR,A.WORKSTART,112) LIKE '" + yymm + "%' AND A.WORKSTART IS NOT NULL "
						   + "       ORDER BY RIGHT(A.USERID,5), A.WORKSTART "
						   + "  END "
						   + "  ELSE "
						   + "  BEGIN"
						   + "      SELECT RIGHT(A.USERID,5) as SABN, X1.USERNAME, CONVERT(VARCHAR,WORKSTART,112) SLDT "
						   + "        FROM TB_WORKRESULT A LEFT OUTER JOIN TB_USER X1 ON A.USERID=X1.USERID "
						   + "       WHERE RIGHT(A.USERID,5) in (SELECT EMBSSABN FROM " + wagedb + ".DBO.MSTEMBS WHERE EMBSSTAT='1' AND " + where2 +") " // EMBSDPCD = '" + dept + "' )"
						   + "         AND CONVERT(VARCHAR,A.WORKSTART,112) LIKE '" + yymm + "%' AND A.WORKSTART IS NOT NULL "
						   + "       ORDER BY RIGHT(A.USERID,5), A.WORKSTART "
						   + "  END ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("2060_SEARCH_KT", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
        //연차내역 조회(수정필요)
		public void GetSEARCH_DYYCDatas(string sldt, string sabn, DataSet ds)
		{
			try
			{
				string qry = " SELECT A1.*, '" + sldt + "' AS YC_STDT, "
						   + "        (SELECT ISNULL(MAX(YC_SQ),0) YC_SQ FROM DUTY_MSTYCCJ "
						   + "          WHERE YC_YEAR=A1.YC_YEAR AND SAWON_NO=A1.SAWON_NO) AS YCCJ_SQ"
						   + "   FROM ( "
						   + "         SELECT A.YC_YEAR, A.SAWON_NO SAWON_NO, RTRIM(A.SAWON_NM) SAWON_NM, RTRIM(X3.EMBSEMAL) GW_EMAIL, RTRIM(ISNULL(X4.DEPRNAM1,'')) DEPT_NM, "
						   + "                A.YC_TYPE, A.IN_DATE, A.EMBSTSDT, A.CALC_DATE, A.USE_FRDT, A.USE_TODT,"
						   + "                A.YC_FIRST, A.YC_BF, A.YC_NOW, A.YC_CHANGE, A.YC_TOTAL, "
                           + "				  (CASE A.YC_TYPE WHEN 0 THEN '회계년도' WHEN 1 THEN '입사일' "
                           + "				        WHEN 2 THEN '의사' WHEN 3 THEN '오너' ELSE '' END) YC_TYPE_NM, "
                           + "      		  LEFT(A.IN_DATE,4)+'-'+SUBSTRING(A.IN_DATE,5,2)+'-'+SUBSTRING(A.IN_DATE,7,2) AS IN_DATE_NM, "
						   //+ "                LEFT(A.CALC_FRDT,4)+'-'+SUBSTRING(A.CALC_FRDT,5,2)+'-'+SUBSTRING(A.CALC_FRDT,7,2)+' ~ '+ "
						   //+ "                LEFT(A.CALC_TODT,4)+'-'+SUBSTRING(A.CALC_TODT,5,2)+'-'+SUBSTRING(A.CALC_TODT,7,2) AS CALC_DT_NM, "
						   + "                LEFT(A.USE_FRDT,4)+'-'+SUBSTRING(A.USE_FRDT,5,2)+'-'+SUBSTRING(A.USE_FRDT,7,2) AS USE_FRDT_NM, "
						   + "                LEFT(A.USE_TODT,4)+'-'+SUBSTRING(A.USE_TODT,5,2)+'-'+SUBSTRING(A.USE_TODT,7,2) AS USE_TODT_NM, "
						   + "                A.YC_FIRST+A.YC_BF+A.YC_NOW as YC_SUM,"
                           + "                ISNULL(X1.YC_DAYS,0) AS YC_USE, "
						   + "                A.YC_TOTAL - ISNULL(X1.YC_DAYS,0) AS YC_REMAIN "
						   + "           FROM DUTY_TRSDYYC A "
						   + "           LEFT OUTER JOIN (SELECT REQ_YEAR, SABN, SUM(YC_DAYS) YC_DAYS FROM DUTY_TRSHREQ "
                           + "                             WHERE PSTY<>'D' AND AP_TAG NOT IN ('2','4') AND REQ_YEAR = '" + sldt.Substring(0, 4) + "' GROUP BY REQ_YEAR, SABN ) X1 " //D:삭제 2:취소,4:반려는 카운트 제외
                           + "             ON A.YC_YEAR=X1.REQ_YEAR AND A.SAWON_NO=X1.SABN "
						   + "           LEFT OUTER JOIN " + wagedb + ".DBO.MSTEMBS X3 "
                           + "             ON A.SAWON_NO=X3.EMBSSABN "
						   + "			 LEFT OUTER JOIN " + wagedb + ".DBO.MSTDEPR X4 "
                           + "			   ON X3.EMBSDPCD=X4.DEPRCODE "
						   + "          WHERE A.YC_YEAR='" + sldt.Substring(0, 4) + "' "
                           + "            AND A.SAWON_NO='" + sabn + "') A1 " 
						   + "  ORDER BY A1.YC_YEAR ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SEARCH_DYYC", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		//당직 등록내역조회
		public void GetDUTY_TRSDANGDatas(int gubn, string yymm, string dept, int sq, DataSet ds)
		{
			try
			{
				string qry = " SELECT '1' CHK, A.*, RTRIM(X1.EMBSNAME) SAWON_NM, "
						   + "        '' D01_NM, '' D02_NM, '' D03_NM, '' D04_NM, '' D05_NM, '' D06_NM, '' D07_NM, '' D08_NM, '' D09_NM, '' D10_NM, "
						   + "        '' D11_NM, '' D12_NM, '' D13_NM, '' D14_NM, '' D15_NM, '' D16_NM, '' D17_NM, '' D18_NM, '' D19_NM, '' D20_NM, "
						   + "        '' D21_NM, '' D22_NM, '' D23_NM, '' D24_NM, '' D25_NM, '' D26_NM, '' D27_NM, '' D28_NM, '' D29_NM, '' D30_NM, '' D31_NM "
						   + "   FROM DUTY_TRSDANG A "
						   + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTEMBS X1 "
						   + "     ON A.SAWON_NO = X1.EMBSSABN "
						   + "  WHERE A.PLANYYMM = '" + yymm + "' AND A.DEPTCODE = '" + dept + "' AND A.YYMM_SQ =" + sq
                           + "  ORDER BY A.PLAN_SQ ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				if (gubn == 1)
					dp.AddDatatable2Dataset("DUTY_TRSDANG", dt, ref ds);
				else if (gubn == 2)
					dp.AddDatatable2Dataset("SEARCH_DANG_PLAN", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//DANG조회
		public void GetSEARCH_DANG_PLANDatas(string yymm, string dept, int sq, DataSet ds)
		{
			try
			{
				string qry = " EXEC USP_DUTY2060_PRC_240701 '" + yymm + "', '" + dept + "', " + sq;

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SEARCH_DANG_PLAN", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//Sum_당직
		public void GetSUM_DANG_PLANDatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT 0 AS G_TYPE, '' G_NM, * FROM DUTY_TRSDANG WHERE 1 = 2 ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SUM_DANG_PLAN", dt, ref ds);
				
				DataRow nrow = ds.Tables["SUM_DANG_PLAN"].NewRow();
				nrow["G_TYPE"] = "8";
				nrow["G_NM"] = "당직";
				ds.Tables["SUM_DANG_PLAN"].Rows.Add(nrow);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//전월당직조회
		public void GetDUTY_BF_TRSDANGDatas(string bf_yymm, string dept, int sq, DataSet ds)
		{
			try
			{
				string bf_mm = clib.DateToText(clib.TextToDate(bf_yymm + "01").AddMonths(-1));
				string qry = " SELECT A.*, RTRIM(X1.EMBSNAME) SAWON_NM, "
						   + "        '' D01_NM, '' D02_NM, '' D03_NM, '' D04_NM, '' D05_NM, '' D06_NM, '' D07_NM, '' D08_NM, '' D09_NM, '' D10_NM, "
						   + "        '' D11_NM, '' D12_NM, '' D13_NM, '' D14_NM, '' D15_NM, '' D16_NM, '' D17_NM, '' D18_NM, '' D19_NM, '' D20_NM, "
						   + "        '' D21_NM, '' D22_NM, '' D23_NM, '' D24_NM, '' D25_NM, '' D26_NM, '' D27_NM, '' D28_NM, '' D29_NM, '' D30_NM, '' D31_NM "
						   + "   FROM DUTY_TRSDANG A "
						   + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTEMBS X1 "
						   + "     ON A.SAWON_NO = X1.EMBSSABN "
						   + "  WHERE A.PLANYYMM = '" + bf_yymm + "' AND A.DEPTCODE = '" + dept + "' "
                           + "    AND A.YYMM_SQ = " + sq
                           + "  ORDER BY A.PLAN_SQ ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SEARCH_BF_DANG", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		//당직 캘린더 조회XXX
		public void GetSEARCH_DANGDatas(string yymm, string dept, int sq, DataSet ds)
		{
			try
			{
				string qry = " EXEC USP_DUTY2060_CALENDER '" + yymm + "', '" + dept + "', " + sq;

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SEARCH_DANG", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion
				
		#region 2061 - 당직사용부서설정

		//부서코드 불러오기
		public void GetSEARCH_DANGDEPTDatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT (CASE WHEN X1.DEPTCODE IS NOT NULL THEN '1' ELSE '0' END) CHK, "
						   + "        RTRIM(DEPRNAM1) DEPT_NM, A.*  "
						   + "   FROM " + wagedb + ".DBO.MSTDEPR A "
						   + "   LEFT OUTER JOIN DUTY_INFODANG X1 ON A.DEPRCODE=X1.DEPTCODE "
						   + "  WHERE A.DEPRSTAT=1 ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SEARCH_DANGDEPT", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		//삭제할 부서설정 테이블 불러오기
		public void GetD_DUTY_INFODANGDatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT * "
						   + "   FROM DUTY_INFODANG A ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("D_DUTY_INFODANG", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		//등록할 부서설정 테이블 불러오기
		public void GetDUTY_INFODANGDatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT * "
						   + "   FROM DUTY_INFODANG A ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("DUTY_INFODANG", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion
						
		#region 2062 - 당직근무표 결재
		
		//GW_TRSPLAN 테이블 불러오기
		public void GetGW_TRSDANGDatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT * "
						   + "   FROM GW_TRSDANG WHERE 1=2 ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("GW_TRSDANG", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion

		#region 3030 - OFF신청조회
				
		//부서코드 lookup
		public void Get3030_SEARCH_DEPTDatas(int admin_lv, DataSet ds)
		{
			try
			{
                string qry = " SELECT A.DEPRCODE CODE, RTRIM(A.DEPRNAM1) NAME "
                           + "   FROM " + wagedb + ".dbo.MSTDEPR A "
                           + "  INNER JOIN DUTY_INFONURS X1 "
                           + "     ON A.DEPRCODE = X1.DEPTCODE";

                if (admin_lv == 1)
                    qry += " INNER JOIN DUTY_PWERDEPT X2 "
                        + "    ON A.DEPRCODE = X2.DEPT "
                        + "  AND X2.SABN = '" + SilkRoad.Config.SRConfig.USID + "' ";

                qry += "  WHERE A.DEPRSTAT=1 "
                    + "  ORDER BY A.DEPRCODE ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("3030_SEARCH_DEPT", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//OFF신청조회
		public void GetSEARCH_OREQDatas(string yymm, string dept, DataSet ds)
		{
			try
			{
				string qry = " SELECT A.*, X2.EMBSDPCD, "
						   + "        LEFT(A.REQ_DATE,4)+'-'+SUBSTRING(A.REQ_DATE,5,2)+'-'+SUBSTRING(A.REQ_DATE,7,2) AS REQ_DATE_NM, "
						   + "        RTRIM(X2.EMBSNAME) SAWON_NM, X1.G_FNM, X1.G_SNM, RTRIM(X3.DEPRNAM1) DEPT_NM, "
						   + "        (CASE WHEN A.EDIT_YN='1' THEN 'Y' ELSE '' END) AS EDIT_YN_NM, "
						   + "        CONVERT(DATETIME,A.REQ_DATE) FR_DATE, DATEADD(DAY,1,A.REQ_DATE) TO_DATE, "
						   + "        0 AS TYPE, 1 AS ALLDAY, (CASE WHEN A.EDIT_YN=1 THEN 1 ELSE 3 END) AS LABEL, '' REMARK "
						   + "   FROM DUTY_TRSOREQ A "
						   + "   LEFT OUTER JOIN DUTY_MSTGNMU X1 "
						   + "     ON A.REQ_TYPE=X1.G_CODE "
						   + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTEMBS X2 "
						   + "     ON A.SABN=X2.EMBSSABN "
						   + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X3 "
						   + "     ON X2.EMBSDPCD=X3.DEPRCODE "
						   + "  WHERE LEFT(A.REQ_DATE,6) = '" + yymm + "'"
						   + "    AND X2.EMBSDPCD LIKE '" + dept + "' ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("DUTY_TRSOREQ", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		#endregion

		#region 3010 - 간호사근무관리

		//부서코드 불러오기
		public void Get3010_DEPTDatas(string yymm, DataSet ds)
		{
			try
			{
                string qry = "";
				qry += " SELECT A.DEPRCODE CODE, RTRIM(DEPRNAM1) DEPT_NM, "
					+ "        (CASE WHEN X2.DEPTCODE IS NOT NULL THEN '1' ELSE '0' END) CHK "
					+ "   FROM " + wagedb + ".DBO.MSTDEPR A "
					+ "  INNER JOIN DUTY_INFONURS X1 ON A.DEPRCODE=X1.DEPTCODE "
					+ "   LEFT OUTER JOIN (SELECT DISTINCT DEPTCODE FROM DUTY_TRSPLAN WHERE PLANYYMM='" + yymm +"' AND YYMM_SQ=1 ) X2 "
					+ "     ON A.DEPRCODE=X2.DEPTCODE "
					+ "  WHERE A.DEPRSTAT=1 "
					+ "  ORDER BY A.DEPRCODE ";				

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("3010_DEPT", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		//조회년월 휴가연차 가져오기
		public void Get3010_SEARCH_HYDatas(string yymm, string dept, DataSet ds)
		{
			try
			{
				string qry = " EXEC USP_DUTY3010_HY_240701 '" + yymm + "', '" + dept + "' ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("3010_SEARCH_HY", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//조회년월 퇴사자 가져오기
		public void Get3010_S_TSDatas(string yymm, string dept, DataSet ds)
		{
			try
			{
				string qry = " SELECT EMBSSABN, "
						   + "        (CASE WHEN LEFT(EMBSTSDT,6)='" + yymm + "' THEN SUBSTRING(EMBSTSDT,7,2) "
						   + "              WHEN EMBSSTAT='2' THEN '99' ELSE '' END) AS TSDT "
						   + "   FROM " + wagedb + ".DBO.MSTEMBS "
                           + "  WHERE EMBSDPCD='" + dept + "' ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("3010_S_TS", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		//휴일조회
		public void GetSEARCH_HOLIDatas(string yymm, DataSet ds)
		{
			try
			{
				string qry = " SELECT * "
						   + "   FROM DUTY_MSTHOLI "
						   + "  WHERE LEFT(H_DATE,6) = '" + yymm + "' ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SEARCH_HOLI", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//전월휴일조회
		public void GetSEARCH_BF_HOLIDatas(string bf_yymm, DataSet ds)
		{
			try
			{
				string qry = " SELECT * "
						   + "   FROM DUTY_MSTHOLI "
						   + "  WHERE LEFT(H_DATE,6) = '" + bf_yymm + "' ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SEARCH_BF_HOLI", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//간호근무LIST
		public void GetGNMU_LISTDatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT (CASE WHEN A.G_TYPE IN (1,2,3,4,5,6,7) THEN '1.근무' "
						   + "			    WHEN A.G_TYPE IN (11,12) THEN '3.연차' "
						   + "			    WHEN A.G_TYPE IN (13,14) THEN '4.경조외' "
                           + "			    WHEN A.G_TYPE IN (15) THEN '2.시차' "
                           + "              ELSE '' END) G_TYPE_NM, "
						   + "        A.G_CODE, A.G_FNM, A.G_SNM "
						   + "   FROM DUTY_MSTGNMU A "
                           + "  WHERE A.G_TYPE NOT IN (8,9,10) "
                           + "  ORDER BY G_TYPE_NM, A.G_CODE ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("GNMU_LIST", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//3교대 근무유형 조회
		public void Get3010_GNMUDatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT A.*, "
						   + "        (CASE WHEN A.G_TYPE IN (1,2,3,4,5,6,7) THEN '1.근무' "
						   + "			    WHEN A.G_TYPE IN (11,12) THEN '3.연차' WHEN A.G_TYPE IN (8) THEN '4.당직' "
						   + "			    WHEN A.G_TYPE IN (13,14) THEN '4.경조외' "
                           + "			    WHEN A.G_TYPE IN (15) THEN '2.시차' "
                           + "              ELSE '' END) G_TYPE_NM "
						   + "   FROM DUTY_MSTGNMU A "
                           + "  WHERE A.G_TYPE NOT IN (9,10) "
                           + "  ORDER BY G_TYPE_NM, A.G_CODE ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("3010_GNMU", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}	
		//근무신청마감일자
		public void Get3010_SEARCH_CLOSDatas(string yymm, string dept, DataSet ds)
		{
			try
			{
				string qry = " SELECT * "
						   + "   FROM DUTY_MSTCLOS "
						   + "  WHERE PLANYYMM = '" + yymm + "' AND DEPTCODE = '" + dept + "' ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("3010_SEARCH_CLOS", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//근무신청내역 조회
		public void Get3010_SEARCH_OREQDatas(string yymm, string dept, DataSet ds)
		{
			try
			{
				string qry = " SELECT A.* "
						   + "   FROM DUTY_TRSOREQ A "
						   + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTEMBS X1 "
						   + "     ON A.SABN = X1.EMBSSABN "
						   + "  WHERE LEFT(A.REQ_DATE,6) = '" + yymm + "' AND X1.EMBSDPCD = '" + dept + "' ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("3010_SEARCH_OREQ", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//출근내역 조회
		public void Get3010_SEARCH_KTDatas(string yymm, string dept, DataSet ds)
		{
			try
			{
				string where1 = dept == "A001" ? "SAWON_NO IN (SELECT SAWON_NO FROM DUTY_TRSPLAN_ETC WHERE DEPTCODE = '" + dept + "')" : "DEPTCODE = '" + dept + "'";
				string where2 = dept == "A001" ? "EMBSSABN IN (SELECT SAWON_NO FROM DUTY_TRSPLAN_ETC WHERE DEPTCODE = '" + dept + "')" : "EMBSDPCD = '" + dept + "'";

				if (dept == "A002")
				{
					where1 = "SAWON_NO IN (SELECT EMBSSABN FROM " + wagedb + ".DBO.MSTEMBS WHERE EMBSADGB = '1')";
					where2 = "EMBSADGB = '1'";
				}

				string qry = "  IF EXISTS(SELECT SAWON_NO FROM DUTY_TRSPLAN WHERE PLANYYMM='" + yymm + "' AND DEPTCODE = '" + dept + "' ) "
						   + "  BEGIN "
						   + "      SELECT RIGHT(A.USERID,5) as SABN, X1.USERNAME, CONVERT(VARCHAR,WORKSTART,112) SLDT "
						   + "        FROM TB_WORKRESULT A LEFT OUTER JOIN TB_USER X1 ON A.USERID=X1.USERID "
						   + "       WHERE RIGHT(A.USERID,5) in (SELECT SAWON_NO FROM DUTY_TRSPLAN WHERE PLANYYMM='" + yymm + "' AND " + where1 + " )"
						   + "         AND CONVERT(VARCHAR,A.WORKSTART,112) LIKE '" + yymm + "%' AND A.WORKSTART IS NOT NULL "
						   + "       ORDER BY RIGHT(A.USERID,5), A.WORKSTART "
						   + "  END "
						   + "  ELSE "
						   + "  BEGIN"
						   + "      SELECT RIGHT(A.USERID,5) as SABN, X1.USERNAME, CONVERT(VARCHAR,WORKSTART,112) SLDT "
						   + "        FROM TB_WORKRESULT A LEFT OUTER JOIN TB_USER X1 ON A.USERID=X1.USERID "
						   + "       WHERE RIGHT(A.USERID,5) in (SELECT EMBSSABN FROM " + wagedb + ".DBO.MSTEMBS WHERE EMBSSTAT='1' AND " + where2 + " )"
						   + "         AND CONVERT(VARCHAR,A.WORKSTART,112) LIKE '" + yymm + "%' AND A.WORKSTART IS NOT NULL "
						   + "       ORDER BY RIGHT(A.USERID,5), A.WORKSTART "
						   + "  END ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("3010_SEARCH_KT", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//출근내역 조회
		public void Get3010_KT_DTDatas(string yymm, string sabn, DataSet ds)
		{
			try
			{
				string qry = " SELECT RIGHT(A.USERID,5) as SABN, X1.USERNAME, CONVERT(VARCHAR,WORKSTART,120) ACC_DT "
						   + "   FROM TB_WORKRESULT A LEFT OUTER JOIN TB_USER X1 ON A.USERID=X1.USERID "
						   + "  WHERE RIGHT(A.USERID,5) = '" + sabn + "'"
						   + "    AND CONVERT(VARCHAR,A.WORKSTART,112) LIKE '" + yymm + "%' AND A.WORKSTART IS NOT NULL "
						   + "  ORDER BY A.WORKSTART ";
				//string qry = " SELECT RIGHT(A.USERID,5) as SABN, A.USERNAME, CONVERT(VARCHAR,ACCESSDATE,120) ACC_DT "
				//		   + "   FROM TB_ACCESS A "
				//		   + "  WHERE RIGHT(A.USERID,5) = '" + sabn + "'"
				//		   + "    AND CONVERT(VARCHAR,A.ACCESSDATE,112) LIKE '" + yymm + "%' AND A.AUTHMODE IN ('82') AND A.AUTHMODE1 IN ('0') "
				//		   + "  ORDER BY A.ACCESSDATE ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("3010_KT_DT1", dt, ref ds);
				
				qry = " SELECT RIGHT(A.USERID,5) as SABN, X1.USERNAME, CONVERT(VARCHAR,WORKEND,120) ACC_DT "
						   + "   FROM TB_WORKRESULT A LEFT OUTER JOIN TB_USER X1 ON A.USERID=X1.USERID "
						   + "  WHERE RIGHT(A.USERID,5) = '" + sabn + "'"
						   + "    AND CONVERT(VARCHAR,A.WORKEND,112) LIKE '" + yymm + "%' AND A.WORKEND IS NOT NULL "
						   + "  ORDER BY A.WORKEND ";
				//qry = " SELECT RIGHT(A.USERID,5) as SABN, A.USERNAME, CONVERT(VARCHAR,ACCESSDATE,120) ACC_DT "
				//		   + "   FROM TB_ACCESS A "
				//		   + "  WHERE RIGHT(A.USERID,5) = '" + sabn + "'"
				//		   + "    AND CONVERT(VARCHAR,A.ACCESSDATE,112) LIKE '" + yymm + "%' AND A.AUTHMODE IN ('83') AND A.AUTHMODE1 IN ('0') "
				//		   + "  ORDER BY A.ACCESSDATE ";

				dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("3010_KT_DT2", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//근무유형 조회
		public void Get3010_SEARCH_GNMUDatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT * "
						   + "   FROM DUTY_MSTGNMU ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("3010_SEARCH_GNMU", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		//근무신청조회
		public void GetSEARCH_PLANDatas(string yymm, string dept, int sq, DataSet ds)
		{
			try
			{
				string qry = " EXEC USP_DUTY3010_PRC_240701 '" + yymm + "', '" + dept + "', " + sq;

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SEARCH_PLAN", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//근무신청내역조회
		public void GetDUTY_TRSPLANDatas(int gubn, string yymm, string dept, int sq, DataSet ds)
		{
			try
			{
				string bf_mm = clib.DateToText(clib.TextToDate(yymm + "01").AddMonths(-1));
				string qry = " SELECT A.*, '1' CHK, "
                           + "        (CASE X1A.EMBSGRCD WHEN '0002' THEN '(RN)' WHEN '0019' THEN '(AN)' ELSE '' END) + "
                           + "        RTRIM(X1.SAWON_NM) SAWON_NM, ISNULL(X1.ALLOWOFF,0) MASTER_OFF, "
                           + "        '' D01_NM, '' D02_NM, '' D03_NM, '' D04_NM, '' D05_NM, '' D06_NM, '' D07_NM, '' D08_NM, '' D09_NM, '' D10_NM, "
						   + "        '' D11_NM, '' D12_NM, '' D13_NM, '' D14_NM, '' D15_NM, '' D16_NM, '' D17_NM, '' D18_NM, '' D19_NM, '' D20_NM, "
						   + "        '' D21_NM, '' D22_NM, '' D23_NM, '' D24_NM, '' D25_NM, '' D26_NM, '' D27_NM, '' D28_NM, '' D29_NM, '' D30_NM, '' D31_NM, "
						   + "        A.MM_CNT1 + ISNULL(X3.Y_CNT1,0) AS Y_CNT1, A.MM_CNT2 + ISNULL(X3.Y_CNT2,0) AS Y_CNT2, A.MM_CNT3 + ISNULL(X3.Y_CNT3,0) AS Y_CNT3, "
						   + "        A.MM_CNT4 + ISNULL(X3.Y_CNT4,0) AS Y_CNT4, A.MM_CNT5 + ISNULL(X3.Y_CNT5,0) AS Y_CNT5, "
						   + "        ISNULL(X3.Y_CNT1,0) AS YEAR_CNT1, ISNULL(X3.Y_CNT2,0) AS YEAR_CNT2, ISNULL(X3.Y_CNT3,0) AS YEAR_CNT3, ISNULL(X3.Y_CNT4,0) AS YEAR_CNT4, ISNULL(X3.Y_CNT5,0) AS YEAR_CNT5 "
						   + "   FROM DUTY_TRSPLAN A "
						   + "   LEFT OUTER JOIN DUTY_MSTNURS X1 "
						   + "     ON A.SAWON_NO = X1.SAWON_NO "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTEMBS X1A "
                           + "     ON A.SAWON_NO = X1A.EMBSSABN "
                           + "   LEFT OUTER JOIN (SELECT SAWON_NO, "
						   + "                           SUM(CASE WHEN '" + yymm.Substring(4, 2) + "' = '01' THEN 0 ELSE MM_CNT1 END) Y_CNT1, "
						   + "                           SUM(CASE WHEN '" + yymm.Substring(4, 2) + "' = '01' THEN 0 ELSE MM_CNT2 END) Y_CNT2, "
						   + "                           SUM(CASE WHEN '" + yymm.Substring(4, 2) + "' = '01' THEN 0 ELSE MM_CNT3 END) Y_CNT3, "
						   + "                           SUM(CASE WHEN '" + yymm.Substring(4, 2) + "' = '01' THEN 0 ELSE MM_CNT4 END) Y_CNT4, "
						   + "                           SUM(CASE WHEN '" + yymm.Substring(4, 2) + "' = '01' THEN 0 ELSE MM_CNT5 END) Y_CNT5 "
						   + "		                FROM DUTY_TRSPLAN "
						   + "                     WHERE DEPTCODE = '" + dept + "' AND YYMM_SQ = " + sq
                           + "                       AND PLANYYMM BETWEEN '" + yymm.Substring(0, 4) + "01' AND '" + bf_mm.Substring(0, 6) + "' "
						   + "				       GROUP BY SAWON_NO) X3 "
						   + "     ON A.SAWON_NO=X3.SAWON_NO "
						   + "  WHERE A.PLANYYMM = '" + yymm + "' AND A.DEPTCODE = '" + dept + "' AND A.YYMM_SQ = " + sq;

                qry += "  ORDER BY A.PLAN_SQ ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				if (gubn == 1)
					dp.AddDatatable2Dataset("DUTY_TRSPLAN", dt, ref ds);
				else if (gubn == 2)
					dp.AddDatatable2Dataset("SEARCH_PLAN", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		//전월근무신청내역조회
		public void GetDUTY_BF_TRSPLANDatas(string bf_yymm, string dept, int sq, DataSet ds)
		{
			try
			{
				string bf_mm = clib.DateToText(clib.TextToDate(bf_yymm + "01").AddMonths(-1));
				string qry = " SELECT A.*, RTRIM(X1.SAWON_NM) SAWON_NM, "
						   + "        '' D01_NM, '' D02_NM, '' D03_NM, '' D04_NM, '' D05_NM, '' D06_NM, '' D07_NM, '' D08_NM, '' D09_NM, '' D10_NM, "
						   + "        '' D11_NM, '' D12_NM, '' D13_NM, '' D14_NM, '' D15_NM, '' D16_NM, '' D17_NM, '' D18_NM, '' D19_NM, '' D20_NM, "
						   + "        '' D21_NM, '' D22_NM, '' D23_NM, '' D24_NM, '' D25_NM, '' D26_NM, '' D27_NM, '' D28_NM, '' D29_NM, '' D30_NM, '' D31_NM "
						   + "   FROM DUTY_TRSPLAN A "
						   + "   LEFT OUTER JOIN DUTY_MSTNURS X1 "
						   + "     ON A.SAWON_NO = X1.SAWON_NO "
						   + "  WHERE A.PLANYYMM = '" + bf_yymm + "' AND A.DEPTCODE = '" + dept + "' AND A.YYMM_SQ = " + sq
                           + "  ORDER BY A.PLAN_SQ ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SEARCH_BF_PLAN", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//Sum_근무신청내역
		public void GetSUM_PLANDatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT 0 AS G_TYPE, '' G_NM, * FROM DUTY_TRSPLAN WHERE 1 = 2 ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SUM_PLAN", dt, ref ds);
				
				DataRow nrow = ds.Tables["SUM_PLAN"].NewRow();
				nrow["G_TYPE"] = "4";
				nrow["G_NM"] = "D";
				ds.Tables["SUM_PLAN"].Rows.Add(nrow);
				nrow = ds.Tables["SUM_PLAN"].NewRow();
				nrow["G_TYPE"] = "5";
				nrow["G_NM"] = "E";
				ds.Tables["SUM_PLAN"].Rows.Add(nrow);
				nrow = ds.Tables["SUM_PLAN"].NewRow();
				nrow["G_TYPE"] = "6";
				nrow["G_NM"] = "N";
				ds.Tables["SUM_PLAN"].Rows.Add(nrow);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//조회부서 간호사 조회
		public void Get3010_SEARCH_NURSDatas(DataSet ds) //string bf_yymm, string dept, 
		{
			try
			{
				string qry = " SELECT A.SAWON_NO CODE, RTRIM(X1.EMBSNAME) NAME, "
						   + "        RTRIM(X2.DEPRNAM1) AS DEPT_NM, "
						   + "        A.ALLOWOFF AS ALLOW_OFF "
						   + "   FROM DUTY_MSTNURS A "
						   + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTEMBS X1 "
						   + "     ON A.SAWON_NO = X1.EMBSSABN "
						   + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X2 "
						   + "     ON X1.EMBSDPCD = X2.DEPRCODE "
						   + "  ORDER BY X1.EMBSDPCD ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("3010_SEARCH_NURS", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion
				
		#region 3011 - 간호사사용부서설정

		//부서코드 불러오기
		public void GetSEARCH_NURSDEPTDatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT (CASE WHEN X1.DEPTCODE IS NOT NULL THEN '1' ELSE '0' END) CHK, "
						   + "        RTRIM(DEPRNAM1) DEPT_NM, A.*  "
						   + "   FROM " + wagedb + ".DBO.MSTDEPR A "
						   + "   LEFT OUTER JOIN DUTY_INFONURS X1 ON A.DEPRCODE=X1.DEPTCODE "
						   + "  WHERE A.DEPRSTAT=1 ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SEARCH_NURSDEPT", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		//삭제할 부서설정 테이블 불러오기
		public void GetD_DUTY_INFONURSDatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT * "
						   + "   FROM DUTY_INFONURS A ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("D_DUTY_INFONURS", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		//등록할 부서설정 테이블 불러오기
		public void GetDUTY_INFONURSDatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT * "
						   + "   FROM DUTY_INFONURS A ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("DUTY_INFONURS", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		#endregion				
				
		#region 3015 - 환경설정
		
		//당직시간 조회
		public void GetS_DUTY_TRSLOFFDatas(string dept, string yymm, DataSet ds)
		{
			try
			{
				string qry = " SELECT *, "
						   + "        LEFT(SLDT,4)+'-'+SUBSTRING(SLDT,5,2)+'-'+SUBSTRING(SLDT,7,2) AS SLDT_NM, "
						   + "        (CASE DATEPART(DW,SLDT) WHEN 1 THEN '일' WHEN 2 THEN '월' WHEN 3 THEN '화' "
						   + "         WHEN 4 THEN '수' WHEN 5 THEN '목' WHEN 6 THEN '금' WHEN 7 THEN '토' ELSE '' END) DAY_NM "
						   + "   FROM DUTY_TRSLOFF "
						   + "  WHERE DEPT = '" + dept + "' AND LEFT(SLDT,6)= '" + yymm + "' "
						   + "  ORDER BY SLDT ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("S_DUTY_TRSLOFF", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//일자별 당직시간 저장
		public void GetDUTY_TRSLOFFDatas(string dept, string yymm, DataSet ds)
		{
			try
			{
				string qry = " SELECT * "
						   + "   FROM DUTY_TRSLOFF "
						   + "  WHERE DEPT = '" + dept + "' AND LEFT(SLDT,6)= '" + yymm + "' "
						   + "  ORDER BY SLDT ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("DUTY_TRSLOFF", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		public void GetS_MSTNURSDatas(string dept, int gubn, DataSet ds)
		{
			try
			{
				string qry = " SELECT A.*,"
						   + "        (CASE WHEN A.STAT=1 THEN '정상' ELSE '사용중지' END) STAT_NM, "
						   + "        ISNULL(X1.EMBSDPCD,'') DEPTCODE, RTRIM(ISNULL(X2.DEPRNAM1,'')) DEPT_NM, "
						   + "        RTRIM(cast(DECRYPTBYPASSPHRASE('samilpas',X1.EMBSPHPN) as varchar(100))) HPNO, RTRIM(X1.EMBSEMAL) AS EMAIL_ID "
						   + "   FROM DUTY_MSTNURS A "
						   + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTEMBS X1 ON A.SAWON_NO=X1.EMBSSABN "
						   + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTDEPR X2 ON X1.EMBSDPCD=X2.DEPRCODE "
						   + "  WHERE X1.EMBSDPCD LIKE '" + dept + "'";
				if (gubn > 0)
					qry += " AND X1.EMBSSTAT = " + gubn;
				qry += "  ORDER BY A.SAWON_NO ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("S_MSTNURS", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		//베드이송 직원 조회
		public void GetS_BEDDatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT A.*, "
						   + "        RTRIM(ISNULL(X2.DEPRNAM1,'')) DEPT_NM "
						   + "   FROM DUTY_TRSPLAN_ETC A "
						   + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTEMBS X1 ON A.SAWON_NO=X1.EMBSSABN "
						   + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTDEPR X2 ON X1.EMBSDPCD=X2.DEPRCODE "
						   + "  WHERE A.PSTY<>'D' "
						   + "  ORDER BY A.SAWON_NO ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("S_BED", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//베드이송 직원 조회
		public void GetS_MSTNURS2Datas(DataSet ds)
		{
			try
			{
				string qry = " SELECT A.SAWON_NO AS CODE, RTRIM(X1.EMBSNAME) NAME, "
						   + "        A.*, "
						   + "        (CASE WHEN A.STAT=1 THEN '정상' ELSE '사용중지' END) STAT_NM, "
						   + "        (CASE A.SHIFT_WORK WHEN 1 THEN 'Y' WHEN 2 THEN 'N' ELSE '' END) SHIFT_WORK_NM, "  //교대여부
						   + "        ISNULL(X1.EMBSDPCD,'') DEPTCODE, RTRIM(ISNULL(X2.DEPRNAM1,'')) DEPT_NM, "
						   + "        RTRIM(cast(DECRYPTBYPASSPHRASE('samilpas',X1.EMBSPHPN) as varchar(100))) HPNO, RTRIM(X1.EMBSEMAL) AS EMAIL_ID "
						   + "   FROM DUTY_MSTNURS A "
						   + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTEMBS X1 ON A.SAWON_NO=X1.EMBSSABN "
						   + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTDEPR X2 ON X1.EMBSDPCD=X2.DEPRCODE "
						   + "  ORDER BY X1.EMBSDPCD, X1.EMBSNAME ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("S_MSTNURS2", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//베드이송 직원 조회
		public void GetDUTY_TRSPLAN_ETCDatas(string sabn, DataSet ds)
		{
			try
			{
				string qry = " SELECT A.* "
						   + "   FROM DUTY_TRSPLAN_ETC A "
						   + "  WHERE A.SAWON_NO = '" + sabn + "' ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("DUTY_TRSPLAN_ETC", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        #endregion

        #region 3020 - 근무마감설정

        //부서리스트
        public void GetSET_DEPTDatas(string code, DataSet ds)
        {
            try
            {
                string qry = " SELECT A.DEPRCODE CODE, RTRIM(A.DEPRNAM1) NAME "
                           + "   FROM " + wagedb + ".dbo.MSTDEPR A "
                           + "  INNER JOIN DUTY_INFONURS X1 ON A.DEPRCODE=X1.DEPTCODE "
                           + "  WHERE A.DEPRCODE LIKE '" + code + "' AND A.DEPRSTAT=1 "
                           + "  ORDER BY A.DEPRCODE ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SET_DEPT", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //년월
        public void GetSEARCH_CLOSDatas(string year, string dept, DataSet ds)
        {
            try
            {
                string qry = " SELECT A.*, RTRIM(X1.DEPRNAM1) DEPT_NM, "
                           + "        LEFT(A.PLANYYMM,4)+'-'+SUBSTRING(A.PLANYYMM,5,2) YYMM_NM, "
                           + "        LEFT(A.POS_FRDT,4)+'/'+SUBSTRING(A.POS_FRDT,5,2)+'/'+SUBSTRING(A.POS_FRDT,7,2)+' ~ '+ "
                           + "        LEFT(A.POS_TODT,4)+'/'+SUBSTRING(A.POS_TODT,5,2)+'/'+SUBSTRING(A.POS_TODT,7,2) AS FRTO_NM, "
                           + "        (CASE WHEN A.CLOSE_YN='Y' THEN '신청마감' ELSE '신청중' END) CLOSE_NM "
                           + "   FROM DUTY_MSTCLOS A "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X1 ON A.DEPTCODE=X1.DEPRCODE "
                           + "  WHERE LEFT(A.PLANYYMM,4) = '" + year + "' "
                           + "    AND A.DEPTCODE = '" + dept + "' ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SEARCH_CLOS", dt, ref ds);

                for (int i = 1; i <= 12; i++)
                {
                    if (ds.Tables["SEARCH_CLOS"].Select("PLANYYMM = '" + year.Substring(0, 4) + i.ToString().PadLeft(2, '0') + "'").Length == 0)
                    {
                        DataRow drow = ds.Tables["SEARCH_CLOS"].NewRow();
                        drow["PLANYYMM"] = year + i.ToString().PadLeft(2, '0');
                        drow["YYMM_NM"] = year + "-" + i.ToString().PadLeft(2, '0');
                        drow["FRTO_NM"] = "";
                        drow["CLOSE_NM"] = "";
                        ds.Tables["SEARCH_CLOS"].Rows.Add(drow);
                    }
                }
                ds.Tables["SEARCH_CLOS"].DefaultView.Sort = "PLANYYMM";
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //부서리스트
        public void GetDUTY_MSTCLOSDatas(string dept, string yymm, DataSet ds)
        {
            try
            {
                string qry = " SELECT * "
                           + "   FROM DUTY_MSTCLOS "
                           + "  WHERE DEPTCODE = '" + dept + "' AND PLANYYMM = '" + yymm + "' ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DUTY_MSTCLOS", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
        

        #region 인사카드

        //INFOBASE
        public void GetINFOBASEDatas(ref DataSet ds)
        {
            try
            {
                string qry = " SELECT * "
                           + "   FROM INFOBASE "
                           + "  WHERE IFBSSBCN <> 0 OR IFBSSBCN = 0";

                DataTable dt = gd.GetDataInQuery(Convert.ToInt32(DataAccess.DBtype), wagedb, qry);
                dp.AddDatatable2Dataset("INFOBASE", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //레포트 부분 - 기본정보 및 사진
        public void GetSabnSubInfo(string sabn, DataSet ds)
        {
            try
            {
                string qry = "";
                qry = "     SELECT A.EMBSNAME AS 성명, A.EMBSSABN AS 사번, A.PHOTO AS 사진, A.EMBSPOST AS 우편, "
                    + "            RTRIM(cast(DECRYPTBYPASSPHRASE('samilpas',A.EMBSPTSA) as varchar(13))) AS 주민번호, "
                    + "            RTRIM(cast(DECRYPTBYPASSPHRASE('samilpas',A.EMBSPTLN) as varchar(20))) AS 전화번호, "
                    + "            RTRIM(cast(DECRYPTBYPASSPHRASE('samilpas',A.EMBSPHPN) as varchar(20))) AS 핸드폰, "
                    + "            RTRIM(cast(DECRYPTBYPASSPHRASE('samilpas',A.EMBSPAD1) as varchar(200))) AS 주소1, "
                    + "            RTRIM(cast(DECRYPTBYPASSPHRASE('samilpas',A.EMBSPAD2) as varchar(100))) AS 주소2, "
                    + "            (CASE WHEN EMBSIPDT = '' THEN '' ELSE " + substring + "(EMBSIPDT,1,4) " + plus + " '-' " + plus + " " + substring + "(EMBSIPDT,5,2) " + plus + " '-' " + plus + " " + substring + "(EMBSIPDT,7,2) END) AS 입사일 , "
                    + "            (CASE WHEN EMBSTSDT = '' THEN '' ELSE " + substring + "(EMBSTSDT,1,4) " + plus + " '-' " + plus + " " + substring + "(EMBSTSDT,5,2) " + plus + " '-' " + plus + " " + substring + "(EMBSTSDT,7,2) END) AS 퇴사일 , "
                    + "            (CASE WHEN EMBSGRDT = '' THEN '' ELSE " + substring + "(EMBSGRDT,1,4) " + plus + " '-' " + plus + " " + substring + "(EMBSGRDT,5,2) " + plus + " '-' " + plus + " " + substring + "(EMBSGRDT,7,2) END) AS 그룹입사일 , "
                    + "            A_1.GLOVNAM2 AS 사업부, "
                    + "            A_2.DEPRNAM2 AS 부서, "
                    + "            A_3.SITENAM2 AS 현장,"
                    + "            A_4.JONGNAM2 AS 직종,"
                    + "            A_5.POSINAM2 AS 직위,"
                    + "            A_6.JDEFNAM2 AS 직무,"
                    + "            A_7.GRADNAM2 AS 직급,"
                    + "            A.EMBSHOBO AS 호봉"
                    + "   FROM MSTEMBS A"
                    + "   LEFT OUTER JOIN MSTGLOV A_1 ON A.EMBSGLCD = A_1.GLOVCODE "
                    + "   LEFT OUTER JOIN MSTDEPR A_2 ON A.EMBSDPCD = A_2.DEPRCODE"
                    + "   LEFT OUTER JOIN MSTSITE A_3 ON A.EMBSSTCD = A_3.SITECODE"
                    + "   LEFT OUTER JOIN MSTJONG A_4 ON A.EMBSJOCD = A_4.JONGCODE"
                    + "   LEFT OUTER JOIN MSTPOSI A_5 ON A.EMBSPSCD = A_5.POSICODE"
                    + "   LEFT OUTER JOIN MSTJDEF A_6 ON A.EMBSJDCD = A_6.JDEFCODE"
                    + "   LEFT OUTER JOIN MSTGRAD A_7 ON A.EMBSGRCD = A_7.GRADCODE"
                    + "  WHERE EMBSSABN = '" + sabn + "' ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), wagedb, qry);
                dp.AddDatatable2Dataset("INSA_PIC", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //레포트 부분 - 개인사항
        public void GetMstEmplDatas(string sabn, ref DataSet ds)
        {
            try
            {
                string qry = " SELECT A.*, "
                            + "       B.ENTRNAM2 AS 입사유형, C.JOKYNAM2 AS 종교, D.KUBYNAM2 AS 군별, E.BYYONAM2 AS 병역, F.KEGENAM2 AS 계급, G.JUNYNAM2 AS 전역구분, "
                            + "       H.JUGONAM2 AS 주거구분, I.CHDONAM2 AS 출신도, J_1.LANGNAM2 AS 외국어1, J_2.LANGNAM2 AS 외국어2, J_3.LANGNAM2 AS 외국어3 "
                            + "  FROM MSTEMPL A"
                            + "  LEFT OUTER JOIN (SELECT ENTRCODE, ENTRNAM2 FROM MSTENTR ) B"
                            + "    ON A.EMPLETGU = B.ENTRCODE"
                            + "  LEFT OUTER JOIN (SELECT JOKYCODE, JOKYNAM2 FROM MSTJOKY ) C"
                            + "    ON A.EMPLJONG = C.JOKYCODE"
                            + "  LEFT OUTER JOIN (SELECT KUBYCODE, KUBYNAM2 FROM MSTKUBY ) D"
                            + "    ON A.EMPLKBGU = D.KUBYCODE"
                            + "  LEFT OUTER JOIN (SELECT BYYOCODE, BYYONAM2 FROM MSTBYYO ) E"
                            + "    ON A.EMPLYJGU = E.BYYOCODE"
                            + "  LEFT OUTER JOIN (SELECT KEGECODE, KEGENAM2 FROM MSTKEGE ) F"
                            + "    ON A.EMPLKGUP = F.KEGECODE"
                            + "  LEFT OUTER JOIN (SELECT JUNYCODE, JUNYNAM2 FROM MSTJUNY ) G"
                            + "    ON A.EMPLJYGU = G.JUNYCODE"
                            + "  LEFT OUTER JOIN (SELECT JUGOCODE, JUGONAM2 FROM MSTJUGO ) H"
                            + "    ON A.EMPLHUGU = H.JUGOCODE"
                            + "  LEFT OUTER JOIN (SELECT CHDOCODE, CHDONAM2 FROM MSTCHDO ) I"
                            + "    ON A.EMPLCDCD = I.CHDOCODE"
                            + "  LEFT OUTER JOIN (SELECT LANGCODE, LANGNAM2 FROM MSTLANG) J_1"
                            + "    ON A.EMPLLAN1 = J_1.LANGCODE"
                            + "  LEFT OUTER JOIN (SELECT LANGCODE, LANGNAM2 FROM MSTLANG ) J_2"
                            + "    ON A.EMPLLAN2 = J_2.LANGCODE"
                            + "  LEFT OUTER JOIN (SELECT LANGCODE, LANGNAM2 FROM MSTLANG ) J_3"
                            + "    ON A.EMPLLAN3 = J_3.LANGCODE"
                            + " WHERE EMPLSABN LIKE '%" + sabn + "%'";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), wagedb, qry);
                dp.AddDatatable2Dataset("MSTEMPL", dt, ref ds);

                //기본 1ROW 추가
                for (int i = 0; ds.Tables["MSTEMPL"].Rows.Count < 1; i++)
                {
                    DataRow drow = ds.Tables["MSTEMPL"].NewRow();
                    ds.Tables["MSTEMPL"].Rows.Add(drow);
                }

            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        //레포트 부분 - 학력사항
        public void GetTrsHakrDatas(string sabn, ref DataSet ds)
        {
            try
            {
                string top_qry = string.Empty;
                string top_qry2 = string.Empty;

                if (DataAccess.DBtype == "1")
                {
                    top_qry = " TOP(5) ";
                }
                else
                {
                    top_qry = " ";
                    top_qry2 = " AND ROWNUM <= 5 ";
                }


                string qry = " SELECT "
                            + top_qry
                            + " A.HAKRSABN, A.HAKRIHMM, A.HAKRJYMM, A.HAKRADDR, A.HAKRINDT, A.HAKRUPDT, A.HAKRUSID, A.HAKRPSTY, "
                            + "       A.HAKRGRCD AS 학력구분, A.HAKRSCCD AS 출신학교,  A.HAKRMJCD AS 전공, A.HAKRGACD AS 학위  "
                            + " FROM TRSHAKR A "
                            + "WHERE HAKRSABN LIKE '%" + sabn + "%'"
                            + top_qry2
                            + "ORDER BY A.HAKRJYMM DESC";

                DataTable dt = gd.GetDataInQuery(Convert.ToInt32(DataAccess.DBtype), wagedb, qry);
                dp.AddDatatable2Dataset("TRSHAKR", dt, ref ds);

                //기본 7ROW 추가                
                for (int i = 0; ds.Tables["TRSHAKR"].Rows.Count < 5; i++)
                {
                    DataRow drow = ds.Tables["TRSHAKR"].NewRow();
                    ds.Tables["TRSHAKR"].Rows.Add(drow);
                }
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //레포트 부분 - 경력사항
        public void GetTrsHistDatas(string sabn, ref DataSet ds)
        {
            try
            {
                string qry = string.Empty;
                //남흥계열사이면
                if (SilkRoad.Config.WGConfig.NHYN)
                {
                    qry = " SELECT TOP(10) * "
                        + "  FROM TRSHIST"
                        + " WHERE HISTSABN LIKE '%" + sabn + "%'"
                        + " ORDER BY HISTFDAY DESC";

                    DataTable dt = gd.GetDataInQuery(Convert.ToInt32(DataAccess.DBtype), wagedb, qry);
                    dp.AddDatatable2Dataset("TRSHIST", dt, ref ds);

                    //기본 10ROW 추가
                    for (int i = 0; ds.Tables["TRSHIST"].Rows.Count < 10; i++)
                    {
                        DataRow drow = ds.Tables["TRSHIST"].NewRow();
                        ds.Tables["TRSHIST"].Rows.Add(drow);
                    }
                }
                else
                {
                    qry = " SELECT "
                        + (DataAccess.DBtype == "1" ? "TOP(5) * " : " * ")
                        + "  FROM TRSHIST"
                        + " WHERE HISTSABN LIKE '%" + sabn + "%'"
                        + (DataAccess.DBtype == "1" ? " " : " AND ROWNUM <= 5 ")
                        + " ORDER BY HISTFDAY DESC";

                    DataTable dt = gd.GetDataInQuery(Convert.ToInt32(DataAccess.DBtype), wagedb, qry);
                    dp.AddDatatable2Dataset("TRSHIST", dt, ref ds);

                    //기본 7ROW 추가
                    for (int i = 0; ds.Tables["TRSHIST"].Rows.Count < 5; i++)
                    {
                        DataRow drow = ds.Tables["TRSHIST"].NewRow();
                        ds.Tables["TRSHIST"].Rows.Add(drow);
                    }
                }
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //레포트 부분 - 자격사항
        public void GetTrsPlicDatas(string sabn, ref DataSet ds)
        {
            try
            {
                string qry = " SELECT "
                            + (DataAccess.DBtype == "1" ? "TOP(6) " : " ")
                            + " A.PLICSABN, A.PLICLCNO, A.PLICSVNO, A.PLICCDAY, A.PLICDDAY, A.PLICEDAY, A.PLICDESC, A.PLICJCYN, "
                            + "              A.PLICINDT, A.PLICUPDT, A.PLICUSID, A.PLICPSTY, "
                            + "              A.PLICLCCD AS 자격명  "
                            + " FROM TRSPLIC A "
                            + " WHERE PLICSABN LIKE '%" + sabn + "%'"
                            + (DataAccess.DBtype == "1" ? " " : " AND ROWNUM <= 6 ")
                            + " ORDER BY PLICCDAY DESC";

                DataTable dt = gd.GetDataInQuery(Convert.ToInt32(DataAccess.DBtype), wagedb, qry);
                dp.AddDatatable2Dataset("TRSPLIC", dt, ref ds);

                for (int i = 0; ds.Tables["TRSPLIC"].Rows.Count < 6; i++)
                {
                    DataRow drow = ds.Tables["TRSPLIC"].NewRow();
                    ds.Tables["TRSPLIC"].Rows.Add(drow);
                }
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
        
        #region PAGE2_1
        //레포트 부분 - 상벌사항
        public void GetTrsAwdiDatas(string sabn, string gubn, ref DataSet ds)
        {
            try
            {
                string top = "";
                string and_top = "";

                if (gubn == "1" || gubn == "2")
                {
                    top = DataAccess.DBtype == "1" ? "TOP(4) " : " ";
                    and_top = DataAccess.DBtype == "1" ? " " : " AND ROWNUM <= 4 ";
                }
                else
                {
                    top = DataAccess.DBtype == "1" ? "TOP(6) " : " ";
                    and_top = DataAccess.DBtype == "1" ? " " : " AND ROWNUM <= 5 ";
                }


                string qry = " SELECT " + top + " A.AWDISABN, A.AWDIFDAT, A.AWDITDAT, A.AWDIDESC, A.AWDIDOWN,A.AWDIDEPT, A.AWDIINDT, A.AWDIUPDT, A.AWDIUSID, A.AWDIPSTY, "
                            + "       A.AWDIAWCD AS 구분 "
                            + "  FROM TRSAWDI A "
                            + " WHERE AWDISABN LIKE '%" + sabn + "%'"
                            + and_top
                            + " ORDER BY AWDIFDAT DESC";

                DataTable dt = gd.GetDataInQuery(Convert.ToInt32(DataAccess.DBtype), wagedb, qry);

                if (gubn == "1")
                {
                    dp.AddDatatable2Dataset("TRSAWDI_1", dt, ref ds);
                    for (int i = 0; ds.Tables["TRSAWDI_1"].Rows.Count < 4; i++)
                    {
                        DataRow drow = ds.Tables["TRSAWDI_1"].NewRow();
                        ds.Tables["TRSAWDI_1"].Rows.Add(drow);
                    }
                }
                else if (gubn == "2")
                {
                    dp.AddDatatable2Dataset("TRSAWDI_2", dt, ref ds);
                    for (int i = 0; ds.Tables["TRSAWDI_2"].Rows.Count < 4; i++)
                    {
                        DataRow drow = ds.Tables["TRSAWDI_2"].NewRow();
                        ds.Tables["TRSAWDI_2"].Rows.Add(drow);
                    }
                }
                else
                {
                    dp.AddDatatable2Dataset("TRSAWDI", dt, ref ds);
                    for (int i = 0; ds.Tables["TRSAWDI"].Rows.Count < 5; i++)
                    {
                        DataRow drow = ds.Tables["TRSAWDI"].NewRow();
                        ds.Tables["TRSAWDI"].Rows.Add(drow);
                    }
                }
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //레포트 부분 - 교육사항
        public void GetTrsAwaiDatas(string sabn, ref DataSet ds)
        {
            try
            {
                string qry = " SELECT "
                            + (DataAccess.DBtype == "1" ? "TOP(6) " : " ")
                            + "A.AWAISABN, A.AWAIFDAT, A.AWAITDAT, A.AWAIDESC, A.AWAIAREA, A.AWAIDEPT, A.AWAIIAMT, A.AWAIOAMT, "
                            + "       A.AWAIINDT, A.AWAIUPDT, A.AWAIUSID, A.AWAIPSTY, A.AWAIREMK, "
                            + "       B.EDUCCODE AS AWAIEUCD "
                            + "  FROM TRSAWAI A "
                            + "  LEFT OUTER JOIN (SELECT EDUCCODE, EDUCNAM2 FROM MSTEDUC ) B "
                            + "    ON A.AWAIEUCD = B.EDUCCODE "
                            + " WHERE AWAISABN LIKE '%" + sabn + "%'"
                            + (DataAccess.DBtype == "1" ? " " : " AND ROWNUM <= 6 ")
                            + " ORDER BY AWAIFDAT DESC";

                DataTable dt = gd.GetDataInQuery(Convert.ToInt32(DataAccess.DBtype), wagedb, qry);
                dp.AddDatatable2Dataset("TRSAWAI", dt, ref ds);

                //기본 7ROW 추가
                for (int i = 0; ds.Tables["TRSAWAI"].Rows.Count < 6; i++)
                {
                    DataRow drow = ds.Tables["TRSAWAI"].NewRow();
                    ds.Tables["TRSAWAI"].Rows.Add(drow);
                }
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //레포트 부분 - 부서이동내역
        public void GetTrsDeptDatas(string sabn, ref DataSet ds)
        {
            try
            {
                string qry = " SELECT "
                           + (DataAccess.DBtype == "1" ? "TOP(6) " : " ")
                           + " A.SAWON_NO, A.MOVE_DATE, A.FR_DEPT, A.TO_DEPT, A.REMARK, B.DEPRNAM2 AS FR_DEPTNAM, C.DEPRNAM2 AS TO_DEPTNAM "
                           + "   FROM TRSDEPT A "
                           + "   LEFT OUTER JOIN (SELECT DEPRCODE, DEPRNAM2 FROM MSTDEPR ) B "
                           + "     ON A.FR_DEPT = B.DEPRCODE "
                           + "   LEFT OUTER JOIN (SELECT DEPRCODE, DEPRNAM2 FROM MSTDEPR ) C "
                           + "     ON A.TO_DEPT = C.DEPRCODE "
                           + " WHERE SAWON_NO LIKE '%" + sabn + "%'"
                           + (DataAccess.DBtype == "1" ? " " : " AND ROWNUM <= 12 ")
                           + " ORDER BY MOVE_DATE DESC";

                DataTable dt = gd.GetDataInQuery(Convert.ToInt32(DataAccess.DBtype), wagedb, qry);
                dp.AddDatatable2Dataset("TRSDEPT", dt, ref ds);

                for (int i = 0; ds.Tables["TRSDEPT"].Rows.Count < 12; i++)
                {
                    DataRow drow = ds.Tables["TRSDEPT"].NewRow();
                    ds.Tables["TRSDEPT"].Rows.Add(drow);
                }
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //레포트 부분 - 인사고과사항
        public void GetTrsEstmDatas(string sabn, ref DataSet ds)
        {
            string qry = "";
            try
            {
                if (SilkRoad.Config.SRConfig.WorkPlaceSano != "4098205251") //(주)천주의성요한수도회 이외 업체
                {
                    qry = " SELECT "
                       + (DataAccess.DBtype == "1" ? "TOP(13) * " : " * ")
                       + "  FROM TRSESTM "
                       + " WHERE ESTMSABN LIKE '%" + sabn + "%'"
                       + (DataAccess.DBtype == "1" ? " " : " AND ROWNUM <= 13 ")
                       + " ORDER BY ESTMYEAR DESC";

                    DataTable dt = gd.GetDataInQuery(Convert.ToInt32(DataAccess.DBtype), wagedb, qry);
                    dp.AddDatatable2Dataset("TRSESTM", dt, ref ds);

                    for (int i = 0; ds.Tables["TRSESTM"].Rows.Count < 13; i++)
                    {
                        DataRow drow = ds.Tables["TRSESTM"].NewRow();
                        ds.Tables["TRSESTM"].Rows.Add(drow);
                    }
                }
                else
                {
                    qry = " SELECT "
                        + DataAccess.DBtype == "1" ? "TOP(5) * " : " * "
                        + "  FROM TRSESTM "
                        + " WHERE ESTMSABN LIKE '%" + sabn + "%'"
                        + DataAccess.DBtype == "1" ? " " : " AND ROWNUM <= 5 "
                        + " ORDER BY ESTMYEAR DESC";

                    DataTable dt = gd.GetDataInQuery(Convert.ToInt32(DataAccess.DBtype), wagedb, qry);
                    dp.AddDatatable2Dataset("TRSESTM", dt, ref ds);

                    for (int i = 0; ds.Tables["TRSESTM"].Rows.Count < 5; i++)
                    {
                        DataRow drow = ds.Tables["TRSESTM"].NewRow();
                        ds.Tables["TRSESTM"].Rows.Add(drow);
                    }
                }
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //레포트 부분 - 발령사항
        public void GetTrsWorkDatas(string sabn, ref DataSet ds)
        {
            string qry = "";
            try
            {
                if (SilkRoad.Config.SRConfig.WorkPlaceSano != "4098205251") //(주)천주의성요한수도회 이외 업체
                {
                    string comp = string.Empty;

                    if (DataAccess.DBtype == "1")   //MSSQL
                    {
                        comp = ds.Tables["INFOBASE"].Rows[0]["IFBSNHYN"].ToString() == "1" ? "TOP(20)" : "TOP(10)";
                    }
                    else
                    {
                        comp = " ";
                    }


                    qry = " SELECT " + comp + " A.WORKSABN, A.WORKBDAY, RTRIM(A.WORKHBCD) AS HOBONG, "
                       + "       B.WORDNAM2 AS 발령구분, C.GLOVNAM2 AS 사업부, D.DEPRNAM2 AS 부서, E.SITENAM2 AS 현장, "
                       + "       F.JONGNAM2 AS 직종, G.POSINAM2 AS 직위, H.JDEFNAM2 AS 직무, I.GRADNAM2 AS 직급, J.AREANAM2 AS 근무지 "
                       + "  FROM TRSWORK A "
                       + "  LEFT OUTER JOIN (SELECT WORDCODE, WORDNAM2 FROM MSTWORD ) B "
                       + "    ON A.WORKWRCD = B.WORDCODE "
                       + "  LEFT OUTER JOIN (SELECT GLOVCODE, GLOVNAM2 FROM MSTGLOV) C "
                       + "    ON A.WORKGOCD = C.GLOVCODE "
                       + "  LEFT OUTER JOIN (SELECT DEPRCODE, DEPRNAM2 FROM MSTDEPR) D "
                       + "    ON A.WORKDPCD = D.DEPRCODE "
                       + "  LEFT OUTER JOIN (SELECT SITECODE, SITENAM2 FROM MSTSITE) E "
                       + "    ON A.WORKSTCD = E.SITECODE "
                       + "  LEFT OUTER JOIN (SELECT JONGCODE, JONGNAM2 FROM MSTJONG) F "
                       + "    ON A.WORKJNCD = F.JONGCODE "
                       + "  LEFT OUTER JOIN (SELECT POSICODE, POSINAM2 FROM MSTPOSI) G "
                       + "    ON A.WORKPSCD = G.POSICODE "
                       + "  LEFT OUTER JOIN (SELECT JDEFCODE, JDEFNAM2 FROM MSTJDEF) H "
                       + "    ON A.WORKJECD = H.JDEFCODE "
                       + "  LEFT OUTER JOIN (SELECT GRADCODE, GRADNAM2 FROM MSTGRAD) I "
                       + "    ON A.WORKGACD = I.GRADCODE "
                       + "  LEFT OUTER JOIN (SELECT AREACODE, AREANAM2 FROM MSTAREA ) J "
                       + "    ON A.WORKAECD = J.AREACODE "
                       + " WHERE WORKSABN LIKE '%" + sabn + "%'"
                       + (DataAccess.DBtype == "1" ? " " : " AND ROWNUM <= 10 ")
                       + " ORDER BY A.WORKBDAY DESC";

                    DataTable dt = gd.GetDataInQuery(Convert.ToInt32(DataAccess.DBtype), wagedb, qry);
                    dp.AddDatatable2Dataset("TRSWORK", dt, ref ds);

                    //남흥계열사이면
                    if (ds.Tables["INFOBASE"].Rows[0]["IFBSNHYN"].ToString() == "1")
                    {
                        for (int i = 0; ds.Tables["TRSWORK"].Rows.Count < 20; i++)
                        {
                            DataRow drow = ds.Tables["TRSWORK"].NewRow();
                            ds.Tables["TRSWORK"].Rows.Add(drow);
                        }
                    }
                    else
                    {
                        for (int i = 0; ds.Tables["TRSWORK"].Rows.Count < 10; i++)
                        {
                            DataRow drow = ds.Tables["TRSWORK"].NewRow();
                            ds.Tables["TRSWORK"].Rows.Add(drow);
                        }
                    }
                }
                else
                {
                    qry = " SELECT TOP(5) A.WORKSABN, A.WORKBDAY, RTRIM(A.WORKHOBO) AS HOBONG, "
                       + "       B.WORDNAM2 AS 발령구분, C.GLOVNAM2 AS 사업부, D.DEPRNAM2 AS 부서, E.SITENAM2 AS 현장, "
                       + "       F.JONGNAM2 AS 직종, G.POSINAM2 AS 직위, H.JDEFNAM2 AS 직무, I.GRADNAM2 AS 직급, J.AREANAM2 AS 근무지 "
                       + "  FROM TRSWORK A "
                       + "  LEFT OUTER JOIN (SELECT WORDCODE, WORDNAM2 FROM MSTWORD ) B "
                       + "    ON A.WORKWRCD = B.WORDCODE "
                       + "  LEFT OUTER JOIN (SELECT GLOVCODE, GLOVNAM2 FROM MSTGLOV) C "
                       + "    ON A.WORKGOCD = C.GLOVCODE "
                       + "  LEFT OUTER JOIN (SELECT DEPRCODE, DEPRNAM2 FROM MSTDEPR) D "
                       + "    ON A.WORKDPCD = D.DEPRCODE "
                       + "  LEFT OUTER JOIN (SELECT SITECODE, SITENAM2 FROM MSTSITE) E "
                       + "    ON A.WORKSTCD = E.SITECODE "
                       + "  LEFT OUTER JOIN (SELECT JONGCODE, JONGNAM2 FROM MSTJONG) F "
                       + "    ON A.WORKJNCD = F.JONGCODE "
                       + "  LEFT OUTER JOIN (SELECT POSICODE, POSINAM2 FROM MSTPOSI) G "
                       + "    ON A.WORKPOSI = G.POSICODE "
                       + "  LEFT OUTER JOIN (SELECT JDEFCODE, JDEFNAM2 FROM MSTJDEF) H "
                       + "    ON A.WORKJECD = H.JDEFCODE "
                       + "  LEFT OUTER JOIN (SELECT GRADCODE, GRADNAM2 FROM MSTGRAD) I "
                       + "    ON A.WORKGACD = I.GRADCODE "
                       + "  LEFT OUTER JOIN (SELECT AREACODE, AREANAM2 FROM MSTAREA ) J "
                       + "    ON A.WORKAECD = J.AREACODE "
                       + " WHERE WORKSABN LIKE '%" + sabn + "%'"
                       + " ORDER BY A.WORKBDAY DESC";

                    DataTable dt = gd.GetDataInQuery(Convert.ToInt32(DataAccess.DBtype), wagedb, qry);
                    dp.AddDatatable2Dataset("TRSWORK", dt, ref ds);

                    for (int i = 0; ds.Tables["TRSWORK"].Rows.Count < 5; i++)
                    {
                        DataRow drow = ds.Tables["TRSWORK"].NewRow();
                        ds.Tables["TRSWORK"].Rows.Add(drow);
                    }
                }
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region PAGE2_2

        //레포트 부분 - 해외연수
        public void GetTrsTraiDatas(string sabn, ref DataSet ds)
        {
            try
            {
                string qry = " SELECT "
                           + (DataAccess.DBtype == "1" ? "TOP(4) * " : " * ")
                           + "   FROM TRSTRAI"
                           + "  WHERE TRAISABN LIKE '%" + sabn + "%'"
                           + (DataAccess.DBtype == "1" ? " " : " AND ROWNUM <= 4 ")
                           + "  ORDER BY TRAIYEAR DESC";

                DataTable dt = gd.GetDataInQuery(Convert.ToInt32(DataAccess.DBtype), wagedb, qry);
                dp.AddDatatable2Dataset("TRSTRAI", dt, ref ds);

                //기본 4ROW 추가
                for (int i = 0; ds.Tables["TRSTRAI"].Rows.Count < 4; i++)
                {
                    DataRow drow = ds.Tables["TRSTRAI"].NewRow();
                    ds.Tables["TRSTRAI"].Rows.Add(drow);
                }
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //레포트 부분 - 승진급관리
        public void GetTrsUpgrDatas(string sabn, ref DataSet ds)
        {
            try
            {
                string qry = " SELECT "
                           + (DataAccess.DBtype == "1" ? "TOP(4) * " : " * ")
                           + "   FROM TRSUPGR"
                           + "  WHERE UPGRSABN LIKE '%" + sabn + "%'"
                           + (DataAccess.DBtype == "1" ? " " : " AND ROWNUM <= 4 ")
                           + "  ORDER BY UPGRBDAY DESC";

                DataTable dt = gd.GetDataInQuery(Convert.ToInt32(DataAccess.DBtype), wagedb, qry);
                dp.AddDatatable2Dataset("TRSUPGR", dt, ref ds);

                for (int i = 0; ds.Tables["TRSUPGR"].Rows.Count < 4; i++)
                {
                    DataRow drow = ds.Tables["TRSUPGR"].NewRow();
                    ds.Tables["TRSUPGR"].Rows.Add(drow);
                }
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //레포트 부분 - 휴직관리
        public void GetTrsHjshDatas(string sabn, ref DataSet ds)
        {
            try
            {
                string qry = " SELECT "
                            + (DataAccess.DBtype == "1" ? "TOP(5) * " : " * ")
                            + "  FROM TRSHJSH"
                            + " WHERE HJSHSABN LIKE '%" + sabn + "%'"
                            + (DataAccess.DBtype == "1" ? " " : " AND ROWNUM <= 5 ")
                            + " ORDER BY HJSHFDAY DESC";

                DataTable dt = gd.GetDataInQuery(Convert.ToInt32(DataAccess.DBtype), wagedb, qry);
                dp.AddDatatable2Dataset("TRSHJSH", dt, ref ds);

                //기본 20ROW 추가
                for (int i = 0; ds.Tables["TRSHJSH"].Rows.Count < 5; i++)
                {
                    DataRow drow = ds.Tables["TRSHJSH"].NewRow();
                    ds.Tables["TRSHJSH"].Rows.Add(drow);
                }
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //레포트 부분 - 병가관리
        public void GetTrsBgshDatas(string sabn, ref DataSet ds)
        {
            try
            {
                string qry = " SELECT "
                            + (DataAccess.DBtype == "1" ? "TOP(5) * " : " * ")
                            + "  FROM TRSBGSH"
                            + " WHERE BGSHSABN LIKE '%" + sabn + "%'"
                            + (DataAccess.DBtype == "1" ? " " : " AND ROWNUM <= 5 ")
                            + " ORDER BY BGSHFDAY DESC";

                DataTable dt = gd.GetDataInQuery(Convert.ToInt32(DataAccess.DBtype), wagedb, qry);
                dp.AddDatatable2Dataset("TRSBGSH", dt, ref ds);

                for (int i = 0; ds.Tables["TRSBGSH"].Rows.Count < 5; i++)
                {
                    DataRow drow = ds.Tables["TRSBGSH"].NewRow();
                    ds.Tables["TRSBGSH"].Rows.Add(drow);
                }
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //레포트 부분 - 지인사항 (남흥계열사 전용)
        public void GetTrsFrndDatas(string sabn, ref DataSet ds)
        {
            try
            {
                string qry = " SELECT * "
                           + "  FROM TRSFRND"
                           + " WHERE FRNDSABN LIKE '%" + sabn + "%'"
                           + " ORDER BY FRNDSABN, FRNDSQNO ";

                DataTable dt = gd.GetDataInQuery(Convert.ToInt32(DataAccess.DBtype), wagedb, qry);
                dp.AddDatatable2Dataset("TRSFRND", dt, ref ds);

                //기본 30ROW 추가
                for (int i = 0; ds.Tables["TRSFRND"].Rows.Count < 30; i++)
                {
                    DataRow drow = ds.Tables["TRSFRND"].NewRow();
                    ds.Tables["TRSFRND"].Rows.Add(drow);
                }
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        #region 8040 - 시차발생및사용현황

        public void Get8040_SEARCHDatas(int admin_lv, string sldt, DataSet ds)
        {
            try
            {
                string qry = " SELECT X1.EMBSDPCD, RTRIM(X2.DEPRNAM2) DEPT_NM, A.SABN, RTRIM(X1.EMBSNAME) EMBSNAME, "
                           + "        LEFT(RTRIM(cast(DECRYPTBYPASSPHRASE('samilpas',X1.EMBSPTSA) as varchar(13))),6)+'-'+ "
                           + "        SUBSTRING(RTRIM(cast(DECRYPTBYPASSPHRASE('samilpas',X1.EMBSPTSA) as varchar(13))),7,7) AS D_JMNO, "
                           + "        '" + sldt.Substring(0, 4) + "' AS REQ_YEAR, "
                           + "        SUM(A.YC_TIME) YC_TIME, SUM(A.YC_USE) YC_USE, SUM(A.YC_TIME - A.YC_USE) AS YC_REMAIN "
                           + "   FROM ( "
                           + "        SELECT SABN, (OT_TIME * 1.5) AS YC_TIME, 0.00 AS YC_USE "
                           + "          FROM DUTY_TRSOVTM "
                           + "         WHERE LEFT(OT_DATE,4)='" + sldt.Substring(0, 4) + "' AND OT_GUBN = '3' "
                           + "      UNION ALL "
                           + "        SELECT SABN, 0.00 AS YC_TIME, YC_TIME AS YC_USE "
                           + "          FROM DUTY_TRSTREQ "
                           + "         WHERE PSTY<>'D' AND REQ_YEAR='" + sldt.Substring(0, 4) + "' AND AP_TAG NOT IN ('2','4') "
                           + "         ) A "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTEMBS X1 "
                           + "     ON A.SABN=X1.EMBSSABN "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X2 "
                           + "     ON X1.EMBSDPCD=X2.DEPRCODE "
                           + "  GROUP BY X1.EMBSDPCD, X2.DEPRNAM2, A.SABN, X1.EMBSNAME, X1.EMBSPTSA "
                           + "  ORDER BY X1.EMBSDPCD, A.SABN ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("8040_SEARCH", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //발생내역
        public void Get8040_TIME1Datas(string sabn, string year, DataSet ds)
        {
            try
            {
                string qry = " SELECT *, "
                           + "        SUBSTRING(OT_DATE,3,2)+'.'+SUBSTRING(OT_DATE,5,2)+'.'+SUBSTRING(OT_DATE,7,2) AS DATE_NM, "
                           + "        (OT_TIME * 1.5) AS YC_TIME "
                           + "   FROM DUTY_TRSOVTM "
                           + "  WHERE LEFT(OT_DATE,4)='" + year + "' AND OT_GUBN = '3' "
                           + "  ORDER BY OT_DATE ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("8040_TIME1", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //사용내역
        public void Get8040_TIME2Datas(string sabn, string year, DataSet ds)
        {
            try
            {
                string qry = " SELECT A.*, "
                           + "        SUBSTRING(A.REQ_DATE,3,2)+'.'+SUBSTRING(A.REQ_DATE,5,2)+'.'+SUBSTRING(A.REQ_DATE,7,2) AS DATE_NM, "
                           + "        (CASE isnull(A.AP_TAG,'') WHEN '1' THEN '상신' WHEN '2' THEN '취소' WHEN '3' THEN '진행' "
                           + "              WHEN '4' THEN '반려' WHEN '5' THEN '승인' WHEN '8' THEN '완료' WHEN '9' THEN '정산' ELSE '' END) AP_TAG_NM "
                           + "   FROM DUTY_TRSTREQ A "
                           + "  WHERE A.SABN= '" + sabn + "' AND A.REQ_YEAR = '" + year + "' "
                           + "    AND A.PSTY<>'D' AND A.AP_TAG NOT IN ('2','4')"
                           + "  ORDER BY A.REQ_DATE ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("8040_TIME2", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 8030 - 연차신청및조회

        //연차내역 조회
        public void GetYC_DAYS_ECHKDatas(string sabn, string year, string frdt, string todt, string frgn, string togn, DataSet ds)
        {
            try
            {
                string qry = " EXEC YC_DAYS_ECHK '" + sabn + "', '" + year + "', '" + frdt + "', '" + todt + "', '" + frgn + "', '" + togn + "'";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("YC_DAYS_ECHK", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //연차근무 LOOKUP
        public void Get8030_SEARCH_GNMUDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT G_CODE, G_FNM, G_SNM "
                           + "   FROM DUTY_MSTGNMU "
                           + "  WHERE G_TYPE IN (11,12) ORDER BY G_CODE ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("8030_SEARCH_GNMU", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //부서별 직원LOOKUP
        public void Get8030_SEARCH_EMBSDatas(string dept, DataSet ds)
        {
            try
            {
                string qry = " SELECT RTRIM(A.EMBSSABN) CODE, RTRIM(A.EMBSNAME) NAME, "
                           + "        A.EMBSDPCD, RTRIM(X1.DEPRNAM1) DEPT_NM, "
                           + "        RTRIM(X2.POSINAM1) POSI_NM, ISNULL(A.EMBSADGB,'') EMBSADGB "
                           + "   FROM " + wagedb + ".dbo.MSTEMBS A "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X1 "
                           + "     ON A.EMBSDPCD = X1.DEPRCODE"
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTPOSI X2 "
                           + "     ON A.EMBSPSCD = X2.POSICODE"
                           + "  WHERE A.EMBSSTAT='1' AND A.EMBSDPCD LIKE '" + dept + "' ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("8030_SEARCH_EMBS", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //결재라인
        public void GetGW_LINEDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT RTRIM(A.EMBSSABN) CODE, RTRIM(A.EMBSNAME) NAME, "
                           + "        RTRIM(X1.DEPRNAM1) DEPT_NM, RTRIM(X2.POSINAM1) POSI_NM "
                           + "   FROM " + wagedb + ".dbo.MSTEMBS A "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X1 "
                           + "     ON A.EMBSDPCD = X1.DEPRCODE"
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTPOSI X2 "
                           + "     ON A.EMBSPSCD = X2.POSICODE"
                           + "  WHERE A.EMBSADGB ='1' ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("GW_LINE", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //연차조회
        public void GetSEARCH_YC_LISTDatas(string FLAG, int admin_lv, string fr_yymm, string to_yymm, string dept, DataSet ds)
        {
            try
            {
                string w_dt = FLAG == "D" ? "A.PSTY='D'" : "A.PSTY <>'D'";
                string dt_nm = FLAG == "D" ? "SEARCH_DEL_YC_LIST" : FLAG == "A" ? "SEARCH_AP_YC_LIST" : "SEARCH_YC_LIST";
                string qry = " SELECT A.*, '' CHK, '' C_CHK, "
                           + "		  RTRIM(X3.EMBSNAME) SAWON_NM, RTRIM(ISNULL(X4.DEPRNAM1,'')) AS DEPT_NM, "
                           + "        (CASE WHEN A.REQ_DATE=A.REQ_DATE2 THEN SUBSTRING(A.REQ_DATE,3,2)+'.'+SUBSTRING(A.REQ_DATE,5,2)+'.'+SUBSTRING(A.REQ_DATE,7,2) "
                           + "			    ELSE SUBSTRING(A.REQ_DATE,3,2)+'.'+SUBSTRING(A.REQ_DATE,5,2)+'.'+SUBSTRING(A.REQ_DATE,7,2)+'~'+ "
                           + "					 SUBSTRING(A.REQ_DATE2,3,2)+'.'+SUBSTRING(A.REQ_DATE2,5,2)+'.'+SUBSTRING(A.REQ_DATE2,7,2) END) AS DATE_NM, "
                           + "        (CASE WHEN A.REQ_DATE=A.REQ_DATE2 THEN X1.G_FNM ELSE X1.G_FNM+'~'+X2.G_FNM END) AS GNMU_NM, "
                           + "        (CASE ISNULL(A.GUBN,'C') WHEN 'C' THEN '신청' WHEN 'D' THEN '철회' ELSE '' END) GUBN_NM, "
                           + "        (CASE isnull(A.AP_TAG,'') WHEN '1' THEN '상신' WHEN '2' THEN '취소' WHEN '3' THEN '진행' "
                           + "              WHEN '4' THEN '반려' WHEN '5' THEN '승인' WHEN '8' THEN '완료' WHEN '9' THEN '정산' ELSE '' END) AP_TAG_NM, "
                           + "        REPLACE(A.LINE_REMK,X5.LINE_SANM,'['+X5.LINE_SANM+']') AS LINE_STAT, "
                           + "        (CASE WHEN A.REQ_DATE=A.REQ_DATE2 THEN X1.G_FNM ELSE X1.G_FNM+'~'+X2.G_FNM END) AS REMARK "
                           + "   FROM DUTY_TRSHREQ A "
                           + "   LEFT OUTER JOIN DUTY_MSTGNMU X1 "
                           + "     ON A.REQ_TYPE=X1.G_CODE "
                           + "   LEFT OUTER JOIN DUTY_MSTGNMU X2 "
                           + "     ON A.REQ_TYPE2=X2.G_CODE "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTEMBS X3 "
                           + "     ON A.SABN=X3.EMBSSABN "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X4 "
                           + "     ON X3.EMBSDPCD=X4.DEPRCODE "
                           + "   LEFT OUTER JOIN DUTY_TRSHREQ_DT X5 "
                           + "     ON A.SEQNO=X5.SEQNO "
                           + "    AND A.LINE_CNT+1=X5.LINE_SQ ";
                if (admin_lv == 1)
                    qry += " INNER JOIN DUTY_PWERDEPT X9 "
                        + "     ON X3.EMBSDPCD = X9.DEPT "
                        + "    AND X9.SABN = '" + SilkRoad.Config.SRConfig.USID + "' ";
                qry += "  WHERE ( (LEFT(REQ_DATE,6) BETWEEN '" + fr_yymm + "' AND '" + to_yymm + "') OR "
                    + "         (LEFT(REQ_DATE2,6) BETWEEN '" + fr_yymm + "' AND '" + to_yymm + "') OR ('" + fr_yymm + "' <= LEFT(REQ_DATE,6) AND LEFT(REQ_DATE2,6) <= '" + to_yymm + "') )"
                    + "    AND X3.EMBSDPCD LIKE '" + dept + "' AND " + w_dt;
                if (FLAG == "A")
                    qry += "  AND ISNULL(A.AP_TAG,'') IN ('1','3')";
                else if (FLAG == "C")
                    qry += "  AND ISNULL(A.AP_TAG,'') IN ('2','4','5','8')";

                qry += "  ORDER BY A.REQ_DATE DESC, X3.EMBSDPCD, A.SABN ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset(dt_nm, dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        //연차내역 신규등록
        public void GetDUTY_TRSHREQDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT A.* "
                           + "   FROM DUTY_TRSHREQ A "
                           + "  WHERE 1=2";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DUTY_TRSHREQ", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //연차신청 결재라인
        public void GetDUTY_TRSHREQ_DTDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT A.* "
                           + "   FROM DUTY_TRSHREQ_DT A "
                           + "  WHERE 1=2";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DUTY_TRSHREQ_DT", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //연차내역 조회_상세NEW
        public void GetDUTY_TRSHREQDatas(string seqno, DataSet ds)
        {
            try
            {
                string qry = " SELECT A.* "
                           + "   FROM DUTY_TRSHREQ A "
                           + "  WHERE A.SEQNO = " + clib.TextToDecimal(seqno) + "";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DUTY_TRSHREQ", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //연차내역 seq번호 가져오기
        public decimal GetHREQ_SEQNODatas()
        {
            decimal max_sq = 0;
            try
            {
                string qry = " SELECT ISNULL(MAX(SEQNO) + 1, 1) AS MAX_SQ "
                           + "   FROM DUTY_TRSHREQ ";

                object obj = gd.GetOneData(1, dbname, qry);
                max_sq = clib.TextToDecimal(obj.ToString());

            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return max_sq;
        }

        #endregion

        #region 8050 - 휴가조회및승인

        //휴가근무 LOOKUP
        public void Get8050_SEARCH_GNMUDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT G_CODE, G_FNM, G_SNM, G_TYPE, "
                           + "        (CASE WHEN G_TYPE=13 THEN '유급' ELSE '무급' END) G_TYPE_NM "
                           + "   FROM DUTY_MSTGNMU "
                           + "  WHERE G_TYPE IN (13,14) ORDER BY G_CODE ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("8050_SEARCH_GNMU", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //휴가조회
        public void GetSEARCH_JREQ_LISTDatas(string FLAG, int admin_lv, string fr_yymm, string to_yymm, string dept, DataSet ds)
        {
            try
            {
                string w_dt = FLAG == "D" ? "A.PSTY='D'" : "A.PSTY <>'D'";
                string dt_nm = FLAG == "C" ? "SEARCH_JREQ_LIST" : "SEARCH_DEL_JREQ_LIST";
                string qry = " SELECT A.*, '' CHK, '' C_CHK, "
                           + "		  RTRIM(X3.EMBSNAME) SAWON_NM, RTRIM(ISNULL(X4.DEPRNAM1,'')) AS DEPT_NM, "
                           + "        (CASE WHEN A.REQ_DATE=A.REQ_DATE2 THEN SUBSTRING(A.REQ_DATE,3,2)+'.'+SUBSTRING(A.REQ_DATE,5,2)+'.'+SUBSTRING(A.REQ_DATE,7,2) "
                           + "			    ELSE SUBSTRING(A.REQ_DATE,3,2)+'.'+SUBSTRING(A.REQ_DATE,5,2)+'.'+SUBSTRING(A.REQ_DATE,7,2)+'~'+ "
                           + "					 SUBSTRING(A.REQ_DATE2,3,2)+'.'+SUBSTRING(A.REQ_DATE2,5,2)+'.'+SUBSTRING(A.REQ_DATE2,7,2) END) AS DATE_NM, "
                           + "        X1.G_FNM AS GNMU_NM, "
                           //+ "        X1.G_FNM+'('+(CASE X1.G_TYPE WHEN 13 THEN '유급' ELSE '무급' END)+')' AS GNMU_NM, "
                           + "        (CASE ISNULL(A.GUBN,'C') WHEN 'C' THEN '신청' ELSE '철회' END) AS GUBN_NM, "
                           + "        (CASE isnull(A.AP_TAG,'') WHEN '1' THEN '상신' WHEN '2' THEN '취소' WHEN '3' THEN '진행' "
                           + "              WHEN '4' THEN '반려' WHEN '5' THEN '승인' WHEN '8' THEN '완료' WHEN '9' THEN '정산' ELSE '' END) AP_TAG_NM, "
                           + "        REPLACE(A.LINE_REMK,X5.LINE_SANM,'['+X5.LINE_SANM+']') AS LINE_STAT, "
                           + "        X1.G_FNM AS REMARK "
                           + "   FROM DUTY_TRSJREQ A "
                           + "   LEFT OUTER JOIN DUTY_MSTGNMU X1 "
                           + "     ON A.REQ_TYPE=X1.G_CODE "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTEMBS X3 "
                           + "     ON A.SABN=X3.EMBSSABN "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X4 "
                           + "     ON X3.EMBSDPCD=X4.DEPRCODE "
                           + "   LEFT OUTER JOIN DUTY_TRSJREQ_DT X5 "
                           + "     ON A.SEQNO=X5.SEQNO "
                           + "    AND A.LINE_CNT+1=X5.LINE_SQ ";
                if (admin_lv == 1)
                    qry += " INNER JOIN DUTY_PWERDEPT X9 "
                        + "     ON X3.EMBSDPCD = X9.DEPT "
                        + "    AND X9.SABN = '" + SilkRoad.Config.SRConfig.USID + "' ";
                qry += "  WHERE ( (LEFT(REQ_DATE,6) BETWEEN '" + fr_yymm + "' AND '" + to_yymm + "') OR "
                    + "         (LEFT(REQ_DATE2,6) BETWEEN '" + fr_yymm + "' AND '" + to_yymm + "') OR ('" + fr_yymm + "' <= LEFT(REQ_DATE,6) AND LEFT(REQ_DATE2,6) <= '" + to_yymm + "') )"
                    //+ "  WHERE ('" + fr_yymm + "' BETWEEN LEFT(A.REQ_DATE,6) AND LEFT(A.REQ_DATE2,6) "
                    //+ "        OR '" + to_yymm + "' BETWEEN LEFT(A.REQ_DATE,6) AND LEFT(A.REQ_DATE2,6) ) "
                    + "    AND X3.EMBSDPCD LIKE '" + dept + "' AND " + w_dt
                    + "  ORDER BY A.REQ_DATE DESC, X3.EMBSDPCD, A.SABN ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset(dt_nm, dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //휴가내역 신청
        public void GetDUTY_TRSJREQDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT A.* "
                           + "   FROM DUTY_TRSJREQ A "
                           + "  WHERE 1=2";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DUTY_TRSJREQ", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //휴가신청 결재라인
        public void GetDUTY_TRSJREQ_DTDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT A.* "
                           + "   FROM DUTY_TRSJREQ_DT A "
                           + "  WHERE 1=2";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DUTY_TRSJREQ_DT", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //휴가사용일수 조회
        public decimal GetHOLI_DAYS_CALCDatas(string frdt, string todt, string gnmu, DataSet ds)
        {
            decimal holi_days = 0;
            try
            {
                string qry = " EXEC HOLI_DAYS_CALC '" + frdt + "','" + todt + "','" + gnmu + "' ";
                object obj = gd.GetOneData(1, dbname, qry);
                holi_days = Convert.ToDecimal(obj.ToString().Trim());
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return holi_days;
        }
        //휴가내역 seq번호 가져오기
        public decimal GetJREQ_SEQNODatas()
        {
            decimal max_sq = 0;
            try
            {
                string qry = " SELECT ISNULL(MAX(SEQNO) + 1, 1) AS MAX_SQ "
                           + "   FROM DUTY_TRSJREQ ";

                object obj = gd.GetOneData(1, dbname, qry);
                max_sq = clib.TextToDecimal(obj.ToString());

            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return max_sq;
        }

        #endregion

        #region 8090 - 연차및휴가조회

        //연차휴가조회
        public void GetSEARCH_8090_LISTDatas(int admin_lv, string fr_yymm, string to_yymm, string dept, int type, DataSet ds)
        {
            try
            {
                string qry = "";

                if (type == 0 || type == 1)
                {
                    qry = " SELECT '1' TYPE, '연차' TYPE_NM, A.REQ_DATE, A.AP_TAG, "
                        + "        (CASE WHEN A.REQ_DATE=A.REQ_DATE2 THEN SUBSTRING(A.REQ_DATE,3,2)+'.'+SUBSTRING(A.REQ_DATE,5,2)+'.'+SUBSTRING(A.REQ_DATE,7,2) "
                        + "			    ELSE SUBSTRING(A.REQ_DATE,3,2)+'.'+SUBSTRING(A.REQ_DATE,5,2)+'.'+SUBSTRING(A.REQ_DATE,7,2)+'~'+ "
                        + "					 SUBSTRING(A.REQ_DATE2,3,2)+'.'+SUBSTRING(A.REQ_DATE2,5,2)+'.'+SUBSTRING(A.REQ_DATE2,7,2) END) AS DATE_NM, "
                        + "		   RTRIM(ISNULL(X4.DEPRNAM1,'')) AS DEPT_NM, RTRIM(X3.EMBSNAME) SAWON_NM , "
                        + "        (CASE WHEN A.REQ_DATE=A.REQ_DATE2 THEN X1.G_FNM ELSE X1.G_FNM+'~'+X2.G_FNM END) AS GNMU_NM, "
                        + "        A.YC_DAYS AS USE_DAYS, "
                        + "        ISNULL(A.GUBN,'C') AS GUBN, (CASE ISNULL(A.GUBN,'C') WHEN 'C' THEN '신청' ELSE '철회' END) AS GUBN_NM, "
                        + "        (CASE isnull(A.AP_TAG,'') WHEN '1' THEN '상신' WHEN '2' THEN '취소' WHEN '3' THEN '진행' "
                        + "              WHEN '4' THEN '반려' WHEN '5' THEN '승인' WHEN '8' THEN '완료' WHEN '9' THEN '정산' ELSE '' END) AP_TAG_NM, "
                        + "        REPLACE(A.LINE_REMK,X5.LINE_SANM,'['+X5.LINE_SANM+']') AS LINE_STAT, "
                        + "        A.REMARK1, A.REMARK2 "
                        + "   FROM DUTY_TRSHREQ A "
                        + "   LEFT OUTER JOIN DUTY_MSTGNMU X1 "
                        + "     ON A.REQ_TYPE=X1.G_CODE "
                        + "   LEFT OUTER JOIN DUTY_MSTGNMU X2 "
                        + "     ON A.REQ_TYPE2=X2.G_CODE "
                        + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTEMBS X3 "
                        + "     ON A.SABN=X3.EMBSSABN "
                        + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X4 "
                        + "     ON X3.EMBSDPCD=X4.DEPRCODE "
                        + "   LEFT OUTER JOIN DUTY_TRSHREQ_DT X5 "
                        + "     ON A.SEQNO=X5.SEQNO "
                        + "    AND A.LINE_CNT+1=X5.LINE_SQ ";
                    if (admin_lv == 1)
                        qry += " INNER JOIN DUTY_PWERDEPT X9 "
                            + "     ON X3.EMBSDPCD = X9.DEPT "
                            + "    AND X9.SABN = '" + SilkRoad.Config.SRConfig.USID + "' ";
                    qry += "  WHERE A.PSTY<>'D' AND LEFT(A.REQ_DATE,6) BETWEEN '" + fr_yymm + "' AND '" + to_yymm + "'"
                        + "    AND X3.EMBSDPCD LIKE '" + dept + "'";
                }
                if (type == 0)
                {
                    qry += " UNION ALL ";
                }
                if (type == 0 || type == 2)
                {
                    qry += " SELECT '2' TYPE, '경조외' TYPE_NM, A.REQ_DATE, A.AP_TAG, "
                        + "        (CASE WHEN A.REQ_DATE=A.REQ_DATE2 THEN SUBSTRING(A.REQ_DATE,3,2)+'.'+SUBSTRING(A.REQ_DATE,5,2)+'.'+SUBSTRING(A.REQ_DATE,7,2) "
                        + "			    ELSE SUBSTRING(A.REQ_DATE,3,2)+'.'+SUBSTRING(A.REQ_DATE,5,2)+'.'+SUBSTRING(A.REQ_DATE,7,2)+'~'+ "
                        + "					 SUBSTRING(A.REQ_DATE2,3,2)+'.'+SUBSTRING(A.REQ_DATE2,5,2)+'.'+SUBSTRING(A.REQ_DATE2,7,2) END) AS DATE_NM, "
                        + "		   RTRIM(ISNULL(X4.DEPRNAM1,'')) AS DEPT_NM, RTRIM(X3.EMBSNAME) SAWON_NM, "
                        + "        X1.G_FNM AS GNMU_NM, "
                        + "        A.HOLI_DAYS AS USE_DAYS, "
                        + "        ISNULL(A.GUBN,'C') AS GUBN, (CASE ISNULL(A.GUBN,'C') WHEN 'C' THEN '신청' ELSE '철회' END) AS GUBN_NM, "
                        + "        (CASE isnull(A.AP_TAG,'') WHEN '1' THEN '상신' WHEN '2' THEN '취소' WHEN '3' THEN '진행' "
                        + "              WHEN '4' THEN '반려' WHEN '5' THEN '승인' WHEN '8' THEN '완료' WHEN '9' THEN '정산' ELSE '' END) AP_TAG_NM, "
                        + "        REPLACE(A.LINE_REMK,X5.LINE_SANM,'['+X5.LINE_SANM+']') AS LINE_STAT, "
                        + "        A.REMARK1, A.REMARK2 "
                        + "   FROM DUTY_TRSJREQ A "
                        + "   LEFT OUTER JOIN DUTY_MSTGNMU X1 "
                        + "     ON A.REQ_TYPE=X1.G_CODE "
                        + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTEMBS X3 "
                        + "     ON A.SABN=X3.EMBSSABN "
                        + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X4 "
                        + "     ON X3.EMBSDPCD=X4.DEPRCODE "
                        + "   LEFT OUTER JOIN DUTY_TRSJREQ_DT X5 "
                        + "     ON A.SEQNO=X5.SEQNO "
                        + "    AND A.LINE_CNT+1=X5.LINE_SQ ";
                    if (admin_lv == 1)
                        qry += " INNER JOIN DUTY_PWERDEPT X9 "
                            + "     ON X3.EMBSDPCD = X9.DEPT "
                            + "    AND X9.SABN = '" + SilkRoad.Config.SRConfig.USID + "' ";
                    qry += "  WHERE A.PSTY<>'D' AND LEFT(A.REQ_DATE,6) BETWEEN '" + fr_yymm + "' AND '" + to_yymm + "'"
                        + "    AND X3.EMBSDPCD LIKE '" + dept + "'";
                }

                qry += " ORDER BY TYPE, REQ_DATE ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SEARCH_8090_LIST", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #region 정리중
        
        //연차사용일수 조회 -23.10.30 조수진 요청.
        public decimal GetYC_DAYS_CALC_SABNDatas(string sabn, string frdt, string todt, string fr_gnmu, string to_gnmu, DataSet ds)
        {
            decimal yc_days = 0;
            try
            {
                string qry = " EXEC YC_DAYS_CALC_SABN '" + sabn + "','" + frdt + "','" + todt + "','" + fr_gnmu + "','" + to_gnmu + "' ";
                object obj = gd.GetOneData(1, dbname, qry);
                yc_days = Convert.ToDecimal(obj.ToString().Trim());
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return yc_days;
        }
        //연차발생 조회
        public void GetSEARCH_YC_YEARDatas(string sabn, string year, DataSet ds)
        {
            try
            {
                string qry = " SELECT * "
                           + "   FROM DUTY_TRSDYYC "
                           + "  WHERE SAWON_NO='" + sabn + "' AND YC_YEAR='" + year + "' ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SEARCH_YC_YEAR", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //연차산정작업
        public void GetDUTY_YC_BASEDatas(string sabn, string sldt, DataSet ds)
        {
            try
            {
                string qry = " EXEC USP_DUTY8010_BASE '%', '" + sabn + "','" + sldt + "', '" + SilkRoad.Config.SRConfig.USID + "' ";
                object obj = gd.GetOneData(1, dbname, qry);

                //DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                //dp.AddDatatable2Dataset("DUTY_YC_BASE", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //연차사용내역
        public void GetSUM_YC_USEDatas(string sabn, string year, DataSet ds)
        {
            try
            {
                string qry = " SELECT SUM(ISNULL(A.YC_DAYS,0)) AS YC_DAY "
                           + "   FROM DUTY_TRSHREQ A "
                           //+ "   LEFT OUTER JOIN DUTY_MSTGNMU X1 "
                           //+ "     ON A.REQ_TYPE=X1.G_CODE "
                           + "  WHERE A.SABN='" + sabn + "' AND A.REQ_YEAR='" + year + "' ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SUM_YC_USE", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 8020 - 사원별연차관리

        //연차조회
        public void GetSEARCH_YCDatas(int lv, string dpcd, string sldt, DataSet ds)
		{
			try
			{
				string qry = " SELECT A1.*, '" + sldt + "' AS YC_STDT, "
						   + "        LEFT(A1.FR_H1,4)+'-'+SUBSTRING(A1.FR_H1,5,2)+'-'+SUBSTRING(A1.FR_H1,7,2)+' ~ '+ "
						   + "        LEFT(A1.TO_H1,4)+'-'+SUBSTRING(A1.TO_H1,5,2)+'-'+SUBSTRING(A1.TO_H1,7,2) AS FRTO_H1, "
						   + "        LEFT(A1.FR_H2,4)+'-'+SUBSTRING(A1.FR_H2,5,2)+'-'+SUBSTRING(A1.FR_H2,7,2)+' ~ '+ "
						   + "        LEFT(A1.TO_H2,4)+'-'+SUBSTRING(A1.TO_H2,5,2)+'-'+SUBSTRING(A1.TO_H2,7,2) AS FRTO_H2, "
						   + "        (CASE WHEN '" + sldt + "' BETWEEN A1.FR_H1 AND A1.TO_H1 THEN 1 ELSE 0 END) CHK1, "
						   + "        (CASE WHEN '" + sldt + "' BETWEEN A1.FR_H2 AND A1.TO_H2 THEN 1 ELSE 0 END) CHK2, "
						   + "        (CASE WHEN '" + sldt + "' BETWEEN A1.FR_H1 AND A1.TO_H1 THEN 'Y' ELSE '' END) CHK1_NM, "
						   + "        (CASE WHEN '" + sldt + "' BETWEEN A1.FR_H2 AND A1.TO_H2 THEN 'Y' ELSE '' END) CHK2_NM, "
						   + "        (SELECT ISNULL(MAX(YC_SQ),0) YC_SQ FROM DUTY_MSTYCCJ "
						   + "          WHERE YC_YEAR=A1.YC_YEAR AND SAWON_NO=A1.SAWON_NO) AS YCCJ_SQ"
						   + "   FROM ( "
						   + "         SELECT A.YC_YEAR, A.SAWON_NO SAWON_NO, RTRIM(A.SAWON_NM) SAWON_NM, RTRIM(X3.EMBSEMAL) GW_EMAIL, X3.EMBSDPCD, RTRIM(ISNULL(X4.DEPRNAM2,'')) DEPT_NM, "
                           + "                A.YC_TYPE, A.IN_DATE, A.EMBSTSDT, A.CALC_DATE, A.USE_FRDT, A.USE_TODT,"
						   + "                A.YC_FIRST, A.YC_BF, A.YC_NOW, A.YC_CHANGE, A.YC_TOTAL, "
                           + "				  (CASE A.YC_TYPE WHEN 0 THEN '회계년도' WHEN 1 THEN '입사일' "
                           + "				        WHEN 2 THEN '의사' WHEN 3 THEN '오너' ELSE '' END) YC_TYPE_NM, "
                           + "      		  LEFT(A.IN_DATE,4)+'-'+SUBSTRING(A.IN_DATE,5,2)+'-'+SUBSTRING(A.IN_DATE,7,2) AS IN_DATE_NM, "
						   //+ "                LEFT(A.CALC_FRDT,4)+'-'+SUBSTRING(A.CALC_FRDT,5,2)+'-'+SUBSTRING(A.CALC_FRDT,7,2)+' ~ '+ "
						   //+ "                LEFT(A.CALC_TODT,4)+'-'+SUBSTRING(A.CALC_TODT,5,2)+'-'+SUBSTRING(A.CALC_TODT,7,2) AS CALC_DT_NM, "
						   + "                LEFT(A.USE_FRDT,4)+'-'+SUBSTRING(A.USE_FRDT,5,2)+'-'+SUBSTRING(A.USE_FRDT,7,2) AS USE_FRDT_NM, "
						   + "                LEFT(A.USE_TODT,4)+'-'+SUBSTRING(A.USE_TODT,5,2)+'-'+SUBSTRING(A.USE_TODT,7,2) AS USE_TODT_NM, "
						   + "                A.YC_FIRST+A.YC_BF+A.YC_NOW as YC_SUM,"
                           + "                ISNULL(X1.YC_DAYS,0) AS YC_USE, A.YC_TOTAL - ISNULL(X1.YC_DAYS,0) AS YC_REMAIN, "
						   + "                (CASE WHEN A.YC_TYPE IN (1) AND DATEDIFF(DAY, A.IN_DATE, '" + sldt + "')<365 THEN CONVERT(CHAR,DATEADD(M,-3,DATEADD(YEAR,1,A.USE_FRDT)),112) "
						   + "                      ELSE CONVERT(CHAR,DATEADD(DAY,1,DATEADD(M,-6,A.USE_TODT)),112) END) AS FR_H1, "
                           + "                (CASE WHEN A.YC_TYPE IN (1) AND DATEDIFF(DAY, A.IN_DATE, '" + sldt + "')<365 THEN CONVERT(CHAR,DATEADD(DAY,9,DATEADD(M,-3,DATEADD(YEAR,1,A.USE_FRDT))),112) "
						   + "                      ELSE CONVERT(CHAR,DATEADD(DAY,10,DATEADD(M,-6,A.USE_TODT)),112) END) AS TO_H1, "
						   + "                (CASE WHEN A.YC_TYPE IN (1) AND DATEDIFF(DAY, A.IN_DATE, '" + sldt + "')<365 THEN CONVERT(CHAR,DATEADD(DAY,-1,DATEADD(M,-1,DATEADD(YEAR,1,A.USE_FRDT))),112) "
						   + "                      ELSE CONVERT(CHAR,DATEADD(M,-2,A.USE_TODT),112) END) AS FR_H2, "
						   + "                (CASE WHEN A.YC_TYPE IN (1) AND DATEDIFF(DAY, A.IN_DATE, '" + sldt + "')<365 THEN CONVERT(CHAR,DATEADD(DAY,8,DATEADD(M,-1,DATEADD(YEAR,1,A.USE_FRDT))),112)  "
						   + "                      ELSE CONVERT(CHAR,DATEADD(DAY,9,DATEADD(M,-2,A.USE_TODT)),112) END) AS TO_H2 "
						   + "           FROM DUTY_TRSDYYC A "
						   + "           LEFT OUTER JOIN (SELECT REQ_YEAR, SABN, SUM(YC_DAYS) YC_DAYS FROM DUTY_TRSHREQ "
                           + "                             WHERE AP_TAG NOT IN ('2','4') AND PSTY<>'D' AND REQ_YEAR = '" + sldt.Substring(0, 4) + "' GROUP BY REQ_YEAR, SABN) X1 " //취소,반려는 카운트 제외
                           + "             ON A.YC_YEAR=X1.REQ_YEAR AND A.SAWON_NO=X1.SABN  "  
						   + "           INNER JOIN " + wagedb + ".DBO.MSTEMBS X3 "
                           + "             ON A.SAWON_NO=X3.EMBSSABN AND (X3.EMBSSTAT='1' OR (X3.EMBSSTAT='2' AND X3.EMBSTSDT>='" + sldt + "')) AND X3.EMBSIPDT <> ''"
                           + "			 LEFT OUTER JOIN " + wagedb + ".DBO.MSTDEPR X4 "
                           + "			   ON X3.EMBSDPCD=X4.DEPRCODE "
                           + "        WHERE A.YC_YEAR = '" + sldt.Substring(0, 4) + "'";

                if (lv == 1)  //부서장일경우
					qry += " AND X3.EMBSDPCD IN (SELECT DEPT FROM DUTY_PWERDEPT WHERE SABN = '" + SilkRoad.Config.SRConfig.USID + "') ";
				else
					qry += " AND X3.EMBSDPCD LIKE '" + dpcd + "'";

                qry += " ) A1 ORDER BY A1.EMBSDPCD, A1.SAWON_NO ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SEARCH_YC", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//연차내역조회 TRSHREQ
		public void GetSEARCH_TRSHREQDatas(string sabn, string yc_year, DataSet ds)
		{
			try
			{
				string qry = " SELECT A.*, "
						   + "        (CASE WHEN A.REQ_DATE=A.REQ_DATE2 THEN SUBSTRING(A.REQ_DATE,3,2)+'.'+SUBSTRING(A.REQ_DATE,5,2)+'.'+SUBSTRING(A.REQ_DATE,7,2) "
						   + "			    ELSE SUBSTRING(A.REQ_DATE,3,2)+'.'+SUBSTRING(A.REQ_DATE,5,2)+'.'+SUBSTRING(A.REQ_DATE,7,2)+'~'+ "
						   + "					 SUBSTRING(A.REQ_DATE2,3,2)+'.'+SUBSTRING(A.REQ_DATE2,5,2)+'.'+SUBSTRING(A.REQ_DATE2,7,2) END) AS DATE_NM, "
                           + "        (CASE isnull(A.AP_TAG,'') WHEN '1' THEN '상신' WHEN '2' THEN '취소' WHEN '3' THEN '진행' "
                           + "              WHEN '4' THEN '반려' WHEN '5' THEN '승인' WHEN '8' THEN '완료' WHEN '9' THEN '정산' ELSE '' END) AP_TAG_NM, "
						   + "        (CASE WHEN A.REQ_DATE=A.REQ_DATE2 THEN X1.G_FNM ELSE X1.G_FNM+'~'+X2.G_FNM END) AS GNMU_NM " //, ISNULL(X1.YC_DAY,0) YC_USE "
						   + "   FROM DUTY_TRSHREQ A "
						   + "   LEFT OUTER JOIN DUTY_MSTGNMU X1 "
						   + "     ON A.REQ_TYPE=X1.G_CODE "
						   + "   LEFT OUTER JOIN DUTY_MSTGNMU X2 "
						   + "     ON A.REQ_TYPE2=X2.G_CODE "
						   + "  WHERE A.SABN= '" + sabn + "' AND A.REQ_YEAR = '" + yc_year + "' "
                           + "    AND A.PSTY<>'D' AND A.AP_TAG IN ('1','3','5','8','9')"
						   + "  ORDER BY A.REQ_DATE ";
				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SEARCH_TRSHREQ", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//연차촉진 조회
		public void GetSEARCH_DUTY_MSTYCCJDatas(string sabn, string yc_year, string doc_type, string yc_sq, DataSet ds)
		{
			try
			{
				string qry = "";

				if (yc_sq == "")
				{
					qry = " SELECT *, (CASE WHEN DOC_TYPE='202101' THEN '1차' ELSE '2차' END) TYPE_NM "
						+ "   FROM DUTY_MSTYCCJ "
						+ "  WHERE SAWON_NO= '" + sabn + "' AND YC_YEAR = '" + yc_year + "' "
						+ "  ORDER BY DOC_TYPE, YC_SQ ";
					DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
					dp.AddDatatable2Dataset("SEARCH_DUTY_MSTYCCJ", dt, ref ds);
				}
				else
				{
					qry = " SELECT * FROM DUTY_MSTYCCJ "
						+ "  WHERE SAWON_NO= '" + sabn + "' AND YC_YEAR = '" + yc_year + "'"
						+ "    AND DOC_TYPE= '" + doc_type + "' AND YC_SQ= " + yc_sq + " ";
					DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
					dp.AddDatatable2Dataset("DUTY_MSTYCCJ", dt, ref ds);
				}

			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//연차산정작업
		public void GetUSP_MAKE_YCDatas(string sldt, DataSet ds)
		{
			try
			{
				string qry = " EXEC USP_DUTY8010_BASE '%', '%', '" + sldt + "', '" + SilkRoad.Config.SRConfig.USID + "' ";
				object obj = gd.GetOneData(1, dbname, qry);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//사원별연차조회
		public void GetDUTY_TRSDYYCDatas(string year, string sabn, DataSet ds)
		{
			try
			{
				string qry = " SELECT YC_FIRST+YC_BF+YC_NOW as YC_SUM, * "
                           + "   FROM DUTY_TRSDYYC "
						   + "  WHERE YC_YEAR='" + year + "' AND SAWON_NO='" + sabn + "' ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("DUTY_TRSDYYC", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		//연차촉진 전송체크
		public void GetCHK_MSTYCCJDatas(string year, string sabn, string type, DataSet ds)
		{
			try
			{
				string qry = " SELECT SAWON_NO "
						   + "   FROM DUTY_MSTYCCJ "
						   + "  WHERE YC_YEAR='" + year + "' AND SAWON_NO='" + sabn + "'  AND DOC_TYPE='" + type + "' ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("CHK_MSTYCCJ", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion
		
		#region 8010 - 연차휴가사용촉구
				
		//연차휴가사용촉구조회 
		public void GetSEARCH_8010Datas(int admin_lv, string year, string dept, DataSet ds)
		{
			try
			{
                string qry = "  SELECT X1.*, "
                           + "         X1.YC_TDAY AS YC_TOTAL, X1.YC_USE_DAY AS YC_USE, X1.YC_REMAIN_DAY AS YC_REMAIN, "
                           + "         X2.EMBSDPCD AS DEPTCODE, RTRIM(ISNULL(X3.DEPRNAM1,'')) DEPT_NM, RTRIM(X2.EMBSEMAL) GW_EMAIL,  "
                           + "         (CASE WHEN X1.DOC_TYPE='202101' THEN '1차' ELSE '2차' END) TYPE_NM "
                           + "    FROM (SELECT YC_YEAR, SAWON_NO, DOC_TYPE, MAX(YC_SQ) YC_SQ FROM DUTY_MSTYCCJ "
                           + "           WHERE YC_YEAR='" + year + "' GROUP BY YC_YEAR, SAWON_NO, DOC_TYPE ) A "
                           + "    LEFT OUTER JOIN DUTY_MSTYCCJ X1 "
                           + "      ON A.YC_YEAR=X1.YC_YEAR AND A.SAWON_NO=X1.SAWON_NO AND A.DOC_TYPE=X1.DOC_TYPE "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTEMBS X2 "
                           + "     ON X1.SAWON_NO=X2.EMBSSABN "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X3 "
                           + "     ON X2.EMBSDPCD=X3.DEPRCODE ";
                if (admin_lv == 1)
                    qry += " INNER JOIN DUTY_PWERDEPT X5 "
                        + "     ON X2.EMBSDPCD = X5.DEPT "
                        + "    AND X5.SABN = '" + SilkRoad.Config.SRConfig.USID + "' ";
                qry += "  WHERE (X2.EMBSSTAT='1' OR (X2.EMBSSTAT='2' AND X2.EMBSTSDT <= '" + year + "1231') ) "
					+ "    AND X2.EMBSDPCD LIKE '" + dept + "'";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SEARCH_8010", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		//환경설정 조회
		public void GetSEARCH_INFODatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT * "
						   + "   FROM DUTY_INFOSD01 ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SEARCH_INFO", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//재직자 전체조회 
		public void GetSEARCH_EMBSDatas(string year, string yc_code, string dept, DataSet ds)
		{
			try
			{
				string qry = " SELECT '" + year + "' YC_YEAR, RTRIM(A.EMBSSABN) SAWON_NO, RTRIM(A.EMBSNAME) AS SAWON_NM, RTRIM(A.EMBSEMAL) GW_EMAIL, A.EMBSDPCD AS DEPTCODE, X1.PARTCODE,"
						   + "        A.EMBSIPDT AS IN_DATE,  "
						   + "        RTRIM(ISNULL(X3.DEPRNAM1,'')) DEPT_NM, RTRIM(ISNULL(X2.PARTNAME,'')) PARTNAME, "
						   + "        ISNULL(X4.YC_SQ,0) YC_SQ, X4.SAWON_SIGN, ISNULL(X4.SEND_DT,'') SEND_DT, ISNULL(X4.SEND_ID,'') SEND_ID, "
						   + "        ISNULL(X5.YC_USE_DAY,0) YC_USE_DAY, ISNULL(X6.DYYCTQTY,0) DYYCTQTY, "
						   + "        ISNULL(X6.DYYCTQTY,0) - ISNULL(X5.YC_USE_DAY,0) YC_REMAIN_DAY "
						   + "   FROM " + wagedb + ".dbo.MSTEMBS A "
						   + "   LEFT OUTER JOIN DUTY_MSTNURS X1 "
						   + "     ON A.EMBSSABN=X1.SAWON_NO "
						   + "   LEFT OUTER JOIN DUTY_MSTPART X2 "
						   + "     ON X1.PARTCODE=X2.PARTCODE "
						   + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X3 "
						   + "     ON A.EMBSDPCD=X3.DEPRCODE "
						   + "   LEFT OUTER JOIN (SELECT B.SAWON_NO, B.YC_SQ, B.SEND_DT, B.SEND_ID, B.SAWON_SIGN "
						   + "                      FROM DUTY_MSTYCCJ B"
						   + "                     INNER JOIN (SELECT YC_YEAR, SAWON_NO, MAX(YC_SQ) YC_SQ FROM DUTY_MSTYCCJ "
						   + "                                  WHERE YC_YEAR='" + year + "' GROUP BY YC_YEAR, SAWON_NO ) B1 "
						   + "                        ON B.YC_YEAR=B1.YC_YEAR AND B.SAWON_NO=B1.SAWON_NO AND B.YC_SQ=B1.YC_SQ "
						   + "                     WHERE B.YC_YEAR='" + year + "') X4 "
						   + "     ON A.EMBSSABN=X4.SAWON_NO "
						   + "   LEFT OUTER JOIN (SELECT B.SAWON_NO, SUM(B.GTMMGT"+yc_code.Substring(2,2)+") YC_USE_DAY "
						   + "                      FROM " + wagedb + ".dbo.MSTGTMM B"
						   + "                     WHERE B.GT_YYMM BETWEEN '" + year + "01' AND '" + year + "06'"
						   + "                     GROUP BY B.SAWON_NO) X5 "
						   + "     ON A.EMBSSABN=X5.SAWON_NO "
						   + "   LEFT OUTER JOIN " + wagedb + ".dbo.TRSDYYC X6 "
						   + "     ON A.EMBSSABN=X6.SAWON_NO "
						   + "    AND X6.YEAR = '" + year + "'"
						   + "  WHERE (A.EMBSSTAT='1' OR (A.EMBSSTAT='2' AND A.EMBSTSDT <= '" + year + "1231') ) "
						   + "    AND A.EMBSDPCD LIKE '" + dept + "'"
						   + "  ORDER BY A.EMBSDPCD, X1.PARTCODE, A.EMBSSABN ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("SEARCH_EMBS", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//연차휴가사용촉구 테이블구조
		public void GetDUTY_MSTYCCJDatas(DataSet ds)
		{
			try
			{
				string qry = " SELECT * "
						   + "   FROM DUTY_MSTYCCJ "
						   + "  WHERE 1=2";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("DUTY_MSTYCCJ", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		//연차촉진_SQ
		public int GetSEARCH_YCCJ_SQDatas(string type, string year, string sabn, DataSet ds)
		{
			int max_sq = 0;
			try
			{
				string qry = " SELECT ISNULL(MAX(YC_SQ) + 1, 1) AS YC_SQ "
						   + "   FROM DUTY_MSTYCCJ "
						   + "  WHERE DOC_TYPE = '" + type +"' "
						   + "    AND SAWON_NO = '" + sabn +"' "
						   + "    AND YC_YEAR = '" + year +"' ";
				
                object obj = gd.GetOneData(1, dbname, qry);
                max_sq = clib.TextToInt(obj.ToString());
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return max_sq;
		}


		#endregion



        #region 5060 - 결재문서관리

        //연차,휴가결재조회
        public void Get5060_AP_YCHG_LISTDatas(int admin_lv, DataSet ds)
		{
			try
			{
                string qry = " SELECT A1.* FROM ( "
                           + " SELECT A.SEQNO, '1' AS TYPE, '연차' AS TYPE_NM, A.AP_TAG, A.REQ_DATE, A.SABN, '' PHOTO_REMARK, '' ADD_PHOTO, '' CHK, "
                           + "        (CASE WHEN A.REQ_DATE=A.REQ_DATE2 THEN SUBSTRING(A.REQ_DATE,3,2)+'.'+SUBSTRING(A.REQ_DATE,5,2)+'.'+SUBSTRING(A.REQ_DATE,7,2) "
                           + "			    ELSE SUBSTRING(A.REQ_DATE,3,2)+'.'+SUBSTRING(A.REQ_DATE,5,2)+'.'+SUBSTRING(A.REQ_DATE,7,2)+'~'+ "
                           + "					 SUBSTRING(A.REQ_DATE2,3,2)+'.'+SUBSTRING(A.REQ_DATE2,5,2)+'.'+SUBSTRING(A.REQ_DATE2,7,2) END) AS DATE_NM, "
                           + "		  X3.EMBSDPCD, RTRIM(X3.EMBSNAME) SAWON_NM, RTRIM(ISNULL(X4.DEPRNAM1,'')) AS DEPT_NM, X5.LINE_SABN, "
                           + "        (CASE WHEN A.REQ_DATE=A.REQ_DATE2 THEN X1.G_FNM ELSE X1.G_FNM+'~'+X2.G_FNM END) AS GNMU_NM, A.YC_DAYS, "
                           + "        A.GUBN, (CASE ISNULL(A.GUBN,'C') WHEN 'C' THEN '신청' ELSE '철회' END) AS GUBN_NM, "
                           + "        (CASE isnull(A.AP_TAG,'') WHEN '1' THEN '상신' WHEN '2' THEN '취소' WHEN '3' THEN '진행' "
                           + "              WHEN '4' THEN '반려' WHEN '5' THEN '승인' WHEN '8' THEN '완료' WHEN '9' THEN '정산' ELSE '' END) AP_TAG_NM, "
                           + "        REPLACE(A.LINE_REMK,X5.LINE_SANM,'['+X5.LINE_SANM+']') AS LINE_STAT, REMARK1, REMARK2 "
                           + "   FROM DUTY_TRSHREQ A "
                           + "   LEFT OUTER JOIN DUTY_MSTGNMU X1 "
                           + "     ON A.REQ_TYPE=X1.G_CODE "
                           + "   LEFT OUTER JOIN DUTY_MSTGNMU X2 "
                           + "     ON A.REQ_TYPE2=X2.G_CODE "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTEMBS X3 "
                           + "     ON A.SABN=X3.EMBSSABN "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X4 "
                           + "     ON X3.EMBSDPCD=X4.DEPRCODE "
                           + "   LEFT OUTER JOIN DUTY_TRSHREQ_DT X5 "
                           + "     ON A.SEQNO=X5.SEQNO "
                           + "    AND A.LINE_CNT+1=X5.LINE_SQ "
                           + "  WHERE A.PSTY<>'D' AND isnull(A.AP_TAG,'') IN ('1','3') ";
                if (admin_lv < 3)
                    qry += "  AND X5.LINE_SABN = '" + SilkRoad.Config.SRConfig.USID + "' ";

                qry += " UNION ALL "
					+ "  SELECT A.SEQNO, '2' AS TYPE, '경조외' AS _NM, A.AP_TAG, A.REQ_DATE, A.SABN, "
                    + "         (CASE WHEN ISNULL(A.ADD_PHOTO,'')='' THEN '' ELSE '문서보기' END) AS PHOTO_REMARK, ISNULL(A.ADD_PHOTO,'') AS ADD_PHOTO, '' CHK, "
                    + "         (CASE WHEN A.REQ_DATE=A.REQ_DATE2 THEN SUBSTRING(A.REQ_DATE,3,2)+'.'+SUBSTRING(A.REQ_DATE,5,2)+'.'+SUBSTRING(A.REQ_DATE,7,2) "
					+ "			     ELSE SUBSTRING(A.REQ_DATE,3,2)+'.'+SUBSTRING(A.REQ_DATE,5,2)+'.'+SUBSTRING(A.REQ_DATE,7,2)+'~'+ "
					+ "					  SUBSTRING(A.REQ_DATE2,3,2)+'.'+SUBSTRING(A.REQ_DATE2,5,2)+'.'+SUBSTRING(A.REQ_DATE2,7,2) END) AS DATE_NM, "
					+ "		    X3.EMBSDPCD, RTRIM(X3.EMBSNAME) SAWON_NM, RTRIM(ISNULL(X4.DEPRNAM1,'')) AS DEPT_NM, X5.LINE_SABN, "
                    + "         X1.G_FNM+(CASE X1.G_TYPE WHEN 13 THEN '_유급' ELSE '_무급' END) AS GNMU_NM, A.HOLI_DAYS AS YC_DAYS, "
					+ "         A.GUBN, (CASE ISNULL(A.GUBN,'C') WHEN 'C' THEN '신청' ELSE '철회' END) AS GUBN_NM, "
                    + "        (CASE isnull(A.AP_TAG,'') WHEN '1' THEN '상신' WHEN '2' THEN '취소' WHEN '3' THEN '진행' "
                    + "              WHEN '4' THEN '반려' WHEN '5' THEN '승인' WHEN '8' THEN '완료' WHEN '9' THEN '정산' ELSE '' END) AP_TAG_NM, "
                    + "        REPLACE(A.LINE_REMK,X5.LINE_SANM,'['+X5.LINE_SANM+']') AS LINE_STAT, REMARK1, REMARK2 "
                    + "    FROM DUTY_TRSJREQ A "
					+ "    LEFT OUTER JOIN DUTY_MSTGNMU X1 "
					+ "      ON A.REQ_TYPE=X1.G_CODE "
					+ "    LEFT OUTER JOIN " + wagedb + ".dbo.MSTEMBS X3 "
					+ "      ON A.SABN=X3.EMBSSABN "
					+ "    LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X4 "
					+ "      ON X3.EMBSDPCD=X4.DEPRCODE "
                    + "   LEFT OUTER JOIN DUTY_TRSJREQ_DT X5 "
                    + "     ON A.SEQNO=X5.SEQNO "
                    + "    AND A.LINE_CNT+1=X5.LINE_SQ "
                    + "  WHERE A.PSTY<>'D' AND isnull(A.AP_TAG,'') IN ('1','3') ";
                if (admin_lv < 3)
                    qry += "  AND X5.LINE_SABN = '" + SilkRoad.Config.SRConfig.USID + "' ";

                qry += " ) A1 ORDER BY A1.EMBSDPCD, A1.REQ_DATE DESC, A1.TYPE, A1.SABN ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("5060_AP_YCHG_LIST", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        //연차,휴가 결재,반려,취소 처리
        public void Get5060_PROCDatas(string proc, string seqno, string tb_nm, DataSet ds)
        {
            try
            {
                string ap_dt = gd.GetNow();
                if (proc == "A") //결재
                {
                    string qry1 = " UPDATE A"
                                + "    SET A.LINE_AP_DT='" + ap_dt + "' "
                                + "   FROM " + tb_nm + "_DT A "
                                + "  INNER JOIN " + tb_nm + " X1 ON A.SEQNO=X1.SEQNO AND A.LINE_SQ=X1.LINE_CNT+1 "
                                + "  WHERE A.SEQNO = " + clib.TextToDecimal(seqno) + " AND X1.PSTY<>'D' "
                                + "    AND A.LINE_SABN = '" + SilkRoad.Config.SRConfig.USID + "'";
                    sd.SetExecuteNonQuery(dbname, qry1);

                    string qry2 = " UPDATE A "
                                + "    SET A.LINE_CNT=A.LINE_CNT+1, A.AP_DT='" + ap_dt + "', A.AP_USID='" + SilkRoad.Config.SRConfig.USID + "', "
                                + "        A.AP_TAG = (CASE WHEN A.LINE_CNT+1=A.LINE_MAX THEN '5' ELSE '3' END) "
                                + "   FROM " + tb_nm + " A "
                                + "   LEFT OUTER JOIN " + tb_nm + "_DT X1 ON A.SEQNO=X1.SEQNO AND A.LINE_CNT+1=X1.LINE_SQ "
                                + "  WHERE A.SEQNO = " + clib.TextToDecimal(seqno) + " AND A.PSTY<>'D' "
                                + "    AND X1.LINE_SABN='" + SilkRoad.Config.SRConfig.USID + "'";
                    sd.SetExecuteNonQuery(dbname, qry2);
                }
                else if (proc == "B") //반려
                {
                    string qry1 = " UPDATE A"
                                + "    SET A.LINE_AP_DT='" + ap_dt + "' "
                                + "   FROM " + tb_nm + "_DT A "
                                + "  INNER JOIN " + tb_nm + " X1 ON A.SEQNO=X1.SEQNO AND A.LINE_SQ=X1.LINE_CNT+1 "
                                + "  WHERE A.SEQNO = " + clib.TextToDecimal(seqno) + " AND X1.PSTY<>'D' "
                                + "    AND A.LINE_SABN = '" + SilkRoad.Config.SRConfig.USID + "'";
                    sd.SetExecuteNonQuery(dbname, qry1);

                    string qry2 = " UPDATE A "
                                + "    SET A.LINE_CNT=A.LINE_CNT+1, A.RT_DT='" + ap_dt + "', A.RT_USID='" + SilkRoad.Config.SRConfig.USID + "', "
                                + "        A.AP_TAG = '4' "
                                + "   FROM " + tb_nm + " A "
                                + "   LEFT OUTER JOIN " + tb_nm + "_DT X1 ON A.SEQNO=X1.SEQNO AND A.LINE_CNT+1=X1.LINE_SQ "
                                + "  WHERE A.SEQNO = " + clib.TextToDecimal(seqno) + " AND A.PSTY<>'D' "
                                + "    AND X1.LINE_SABN='" + SilkRoad.Config.SRConfig.USID + "'";
                    sd.SetExecuteNonQuery(dbname, qry2);
                }
                else if (proc == "C") //취소
                {
                    string qry2 = " UPDATE A "
                                + "    SET A.RT_DT='" + ap_dt + "', A.RT_USID='" + SilkRoad.Config.SRConfig.USID + "', "
                                + "        A.AP_TAG = '2' "
                                + "   FROM " + tb_nm + " A "
                                + "   LEFT OUTER JOIN " + tb_nm + "_DT X1 ON A.SEQNO=X1.SEQNO AND A.LINE_CNT=X1.LINE_SQ "
                                + "  WHERE A.SEQNO = " + clib.TextToDecimal(seqno) + " AND A.PSTY<>'D' "
                                + "    AND X1.LINE_SABN='" + SilkRoad.Config.SRConfig.USID + "'";
                    sd.SetExecuteNonQuery(dbname, qry2);
                }
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //연차,휴가내역 조회_상세
        public void Get5060_DUTY_TRSHREQDatas(string seqno, string tb_nm, DataSet ds)
		{
			try
			{
                string qry = " SELECT X5.LINE_SABN, A.* "
                           + "   FROM " + tb_nm + " A "
                           + "   LEFT OUTER JOIN " + tb_nm + "_DT X5 "
                           + "     ON A.SEQNO=X5.SEQNO "
                           + "    AND A.LINE_CNT+1=X5.LINE_SQ "
                           + "  WHERE A.PSTY<>'D' AND A.SEQNO = " + clib.TextToDecimal(seqno);

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset(tb_nm, dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion
		
		#region 5080 - 완결문서관리
		
		//연차,휴가 결재완료조회
		public void Get5080_AP_YCHG_LISTDatas(string _frdt, int gubn, int ap, int admin_lv, DataSet ds)
		{
			try
			{
				string fr_yy = _frdt.Substring(0, 4);
				string to_yy = _frdt.Substring(4, 4);
                string ap_tag = "('2','4','5','8')";
                if (ap == 1)
                    ap_tag = "('5','8')";
                else if (ap == 2)
                    ap_tag = "('2')";
                else if (ap == 3)
                    ap_tag = "('4')";

                string qry = " SELECT A1.* "
                           + "   FROM ( ";
                if (gubn == 0 || gubn == 1)
                {
                    qry += " SELECT A.SEQNO, '1' AS TYPE, '연차' AS TYPE_NM, A.AP_TAG, A.REQ_DATE, A.SABN, X1.G_TYPE, '' PHOTO_REMARK, '' ADD_PHOTO, '' CHK, "
                        + "        (CASE WHEN A.REQ_DATE=A.REQ_DATE2 THEN SUBSTRING(A.REQ_DATE,3,2)+'.'+SUBSTRING(A.REQ_DATE,5,2)+'.'+SUBSTRING(A.REQ_DATE,7,2) "
                        + "			    ELSE SUBSTRING(A.REQ_DATE,3,2)+'.'+SUBSTRING(A.REQ_DATE,5,2)+'.'+SUBSTRING(A.REQ_DATE,7,2)+'~'+ "
                        + "					 SUBSTRING(A.REQ_DATE2,3,2)+'.'+SUBSTRING(A.REQ_DATE2,5,2)+'.'+SUBSTRING(A.REQ_DATE2,7,2) END) AS DATE_NM, "
                        + "		  X3.EMBSDPCD, RTRIM(X3.EMBSNAME) SAWON_NM, RTRIM(ISNULL(X4.DEPRNAM1,'')) AS DEPT_NM, "
                        + "        (CASE WHEN A.REQ_DATE=A.REQ_DATE2 THEN X1.G_FNM ELSE X1.G_FNM+'~'+X2.G_FNM END) AS GNMU_NM, A.YC_DAYS, "
                        + "        A.GUBN, (CASE ISNULL(A.GUBN,'C') WHEN 'C' THEN '신청' ELSE '철회' END) AS GUBN_NM, "
                        + "        (CASE isnull(A.AP_TAG,'') WHEN '1' THEN '상신' WHEN '2' THEN '취소' WHEN '3' THEN '진행' "
                        + "              WHEN '4' THEN '반려' WHEN '5' THEN '승인' WHEN '8' THEN '완료' WHEN '9' THEN '정산' ELSE '' END) AP_TAG_NM, "
                        + "        A.LINE_REMK, A.AP_DT, A.AP_USID+(CASE A.AP_USID WHEN 'SAMIL' THEN '(SilkRoader)' ELSE '('+RTRIM(X3A.EMBSNAME)+')' END) AS AP_USID, "
                        + "        LEFT(A.INDT,4)+' 년  '+CONVERT(VARCHAR,CONVERT(INT,SUBSTRING(A.INDT,5,2)))+' 월  '+CONVERT(VARCHAR,CONVERT(INT,SUBSTRING(A.INDT,7,2)))+' 일' AS INDT_NM, "
                        + "        A.REMARK1, A.REMARK2 "
                        + "   FROM DUTY_TRSHREQ A "
                        + "   LEFT OUTER JOIN DUTY_MSTGNMU X1 "
                        + "     ON A.REQ_TYPE=X1.G_CODE "
                        + "   LEFT OUTER JOIN DUTY_MSTGNMU X2 "
                        + "     ON A.REQ_TYPE2=X2.G_CODE "
                        + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTEMBS X3 "
                        + "     ON A.SABN=X3.EMBSSABN "
                        + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTEMBS X3A "
                        + "     ON A.AP_USID=X3A.EMBSSABN "
                        + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X4 "
                        + "     ON X3.EMBSDPCD=X4.DEPRCODE "
                        + "   LEFT OUTER JOIN DUTY_TRSHREQ_DT X5 "
                        + "     ON A.SEQNO=X5.SEQNO "
                        + "    AND A.LINE_MAX=X5.LINE_SQ ";
                    //if (admin_lv == 1)
                    //    qry += " INNER JOIN DUTY_PWERDEPT P1 "
                    //        + "     ON X3.EMBSDPCD = P1.DEPT "
                    //        + "    AND P1.SABN = '" + SilkRoad.Config.SRConfig.USID + "' ";
                    qry += "  WHERE A.PSTY<>'D' AND isnull(A.AP_TAG,'') IN " + ap_tag
                               + "    AND ( (LEFT(REQ_DATE,4) BETWEEN '" + fr_yy + "' AND '" + to_yy + "') OR "
                               + "         (LEFT(REQ_DATE2,4) BETWEEN '" + fr_yy + "' AND '" + to_yy + "') OR ('" + fr_yy + "' <= LEFT(REQ_DATE,4) AND LEFT(REQ_DATE2,4) <= '" + to_yy + "') )";

                    if (admin_lv < 3)
                        qry += " AND X5.LINE_SABN = '" + SilkRoad.Config.SRConfig.USID + "' ";
                }
                if (gubn == 0)
                    qry += " UNION ALL ";

                if (gubn == 0 || gubn == 2)
                {
                    qry += " SELECT A.SEQNO, '2' AS TYPE, '경조외' AS TYPE_NM, A.AP_TAG, A.REQ_DATE, A.SABN, X1.G_TYPE, "
                        + "         (CASE WHEN ISNULL(A.ADD_PHOTO,'')='' THEN '' ELSE '문서보기' END) AS PHOTO_REMARK, '' ADD_PHOTO, '' CHK, "
                        + "        (CASE WHEN A.REQ_DATE=A.REQ_DATE2 THEN SUBSTRING(A.REQ_DATE,3,2)+'.'+SUBSTRING(A.REQ_DATE,5,2)+'.'+SUBSTRING(A.REQ_DATE,7,2) "
                        + "			     ELSE SUBSTRING(A.REQ_DATE,3,2)+'.'+SUBSTRING(A.REQ_DATE,5,2)+'.'+SUBSTRING(A.REQ_DATE,7,2)+'~'+ "
                        + "				      SUBSTRING(A.REQ_DATE2,3,2)+'.'+SUBSTRING(A.REQ_DATE2,5,2)+'.'+SUBSTRING(A.REQ_DATE2,7,2) END) AS DATE_NM, "
                        + "		   X3.EMBSDPCD, RTRIM(X3.EMBSNAME) SAWON_NM, RTRIM(ISNULL(X4.DEPRNAM1,'')) AS DEPT_NM, "
                        + "        X1.G_FNM+(CASE X1.G_TYPE WHEN 13 THEN '_유급' ELSE '_무급' END) AS GNMU_NM, A.HOLI_DAYS AS YC_DAYS, "
                        + "        A.GUBN, (CASE ISNULL(A.GUBN,'C') WHEN 'C' THEN '신청' ELSE '철회' END) AS GUBN_NM, "
                        + "        (CASE isnull(A.AP_TAG,'') WHEN '1' THEN '상신' WHEN '2' THEN '취소' WHEN '3' THEN '진행' "
                        + "              WHEN '4' THEN '반려' WHEN '5' THEN '승인' WHEN '8' THEN '완료' WHEN '9' THEN '정산' ELSE '' END) AP_TAG_NM, "
                        + "        A.LINE_REMK, A.AP_DT, A.AP_USID+(CASE A.AP_USID WHEN 'SAMIL' THEN '(SilkRoader)' ELSE '('+RTRIM(X3A.EMBSNAME)+')' END) AS AP_USID, "
                        + "        LEFT(A.INDT,4)+' 년  '+CONVERT(VARCHAR,CONVERT(INT,SUBSTRING(A.INDT,5,2)))+' 월  '+CONVERT(VARCHAR,CONVERT(INT,SUBSTRING(A.INDT,7,2)))+' 일' AS INDT_NM, "
                        + "        REMARK1, REMARK2 "
                        + "   FROM DUTY_TRSJREQ A "
                        + "   LEFT OUTER JOIN DUTY_MSTGNMU X1 "
                        + "     ON A.REQ_TYPE=X1.G_CODE "
                        + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTEMBS X3 "
                        + "     ON A.SABN=X3.EMBSSABN "
                        + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTEMBS X3A "
                        + "     ON A.AP_USID=X3A.EMBSSABN "
                        + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X4 "
                        + "     ON X3.EMBSDPCD=X4.DEPRCODE "
                        + "   LEFT OUTER JOIN DUTY_TRSJREQ_DT X5 "
                        + "     ON A.SEQNO=X5.SEQNO "
                        + "    AND A.LINE_MAX=X5.LINE_SQ ";
                    //if (admin_lv == 1)
                    //    qry += " INNER JOIN DUTY_PWERDEPT P1 "
                    //        + "     ON X3.EMBSDPCD = P1.DEPT "
                    //        + "    AND P1.SABN = '" + SilkRoad.Config.SRConfig.USID + "' ";
                    qry += "   WHERE A.PSTY<>'D' AND isnull(A.AP_TAG,'') IN " + ap_tag
                        + "     AND ( (LEFT(REQ_DATE,4) BETWEEN '" + fr_yy + "' AND '" + to_yy + "') OR "
                        + "         (LEFT(REQ_DATE2,4) BETWEEN '" + fr_yy + "' AND '" + to_yy + "') OR ('" + fr_yy + "' <= LEFT(REQ_DATE,4) AND LEFT(REQ_DATE2,4) <= '" + to_yy + "') )";

                    if (admin_lv < 3)
                        qry += " AND X5.LINE_SABN = '" + SilkRoad.Config.SRConfig.USID + "' ";
                }

				qry += " ) A1 "
					+ " ORDER BY A1.EMBSDPCD, A1.REQ_DATE DESC, A1.TYPE, A1.SABN ";

				DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
				dp.AddDatatable2Dataset("5080_AP_YCHG_LIST", dt, ref ds);
			}
			catch (System.Exception ec)
			{
				System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
													 "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        //연차,휴가내역 조회_상세
        public void Get5080_REPORTDatas(string seqno, string tb_nm, DataSet ds)
        {
            try
            {
                string c1 = tb_nm == "DUTY_TRSHREQ" ? "YC_DAYS" : "HOLI_DAYS";
                string c2 = tb_nm == "DUTY_TRSHREQ" ? "+'~'+X2.G_FNM" : "";
                string qry = " SELECT A1.*, "
                           + "        (CASE WHEN A1.G_TYPE = 11 THEN '1' ELSE '' END) CHK1, "
                           + "        (CASE WHEN A1.G_TYPE = 12 THEN '1' ELSE '' END) CHK2, "
                           + "        (CASE WHEN A1.G_TYPE IN (13,14) THEN '1' ELSE '' END) CHK3, "
                           + "        (CASE WHEN A1.G_TYPE = 11 THEN A1.GNMU_NM ELSE '' END) GNMU_NM1, "
                           + "        (CASE WHEN A1.G_TYPE = 12 THEN A1.GNMU_NM ELSE '' END) GNMU_NM2, "
                           + "        (CASE WHEN A1.G_TYPE IN (13,14) THEN A1.GNMU_NM ELSE '' END) GNMU_NM3, "
                           + "        (CASE WHEN A1.G_TYPE = 11 THEN A1.DATE_NM ELSE '' END) DATE_NM1, "
                           + "        (CASE WHEN A1.G_TYPE = 12 THEN A1.DATE_NM ELSE '' END) DATE_NM2, "
                           + "        (CASE WHEN A1.G_TYPE IN (13,14) THEN A1.DATE_NM ELSE '' END) DATE_NM3, "
                           + "        (CASE WHEN A1.G_TYPE = 11 THEN '('+CONVERT(VARCHAR,A1.YC_DAYS)+') 일간' ELSE '' END) YC_DAYS1, "
                           + "        (CASE WHEN A1.G_TYPE = 12 THEN '('+CONVERT(VARCHAR,A1.YC_DAYS)+') 일간' ELSE '' END) YC_DAYS2, "
                           + "        (CASE WHEN A1.G_TYPE IN (13,14) THEN '('+CONVERT(VARCHAR,A1.YC_DAYS)+') 일간' ELSE '' END) YC_DAYS3 "
                           + "   FROM ( ";

                qry += " SELECT A.SEQNO, A.GUBN, '1' AS TYPE, '연차' AS TYPE_NM, A.AP_TAG, A.REQ_DATE, A.SABN, X1.G_TYPE, A." + c1 + " as YC_DAYS, "
                    + "		   X3.EMBSDPCD, RTRIM(ISNULL(X4.DEPRNAM1,'')) AS DEPT_NM, RTRIM(ISNULL(X5.POSINAM1,'')) AS POSI_NM, RTRIM(X3.EMBSNAME) SAWON_NM, "
                    + "        (CASE WHEN A.REQ_DATE=A.REQ_DATE2 THEN X1.G_FNM ELSE X1.G_FNM" + c2 + " END) AS GNMU_NM, "
                    + "        (CASE WHEN A.REQ_DATE=A.REQ_DATE2 THEN LEFT(A.REQ_DATE,4)+'년 '+SUBSTRING(A.REQ_DATE,5,2)+'월 '+SUBSTRING(A.REQ_DATE,7,2)+'일' "
                    + "			     ELSE LEFT(A.REQ_DATE,4)+'년 '+SUBSTRING(A.REQ_DATE,5,2)+'월 '+SUBSTRING(A.REQ_DATE,7,2)+'일 ~ \r\n'+ "
                    + "					  LEFT(A.REQ_DATE2,4)+'년 '+SUBSTRING(A.REQ_DATE2,5,2)+'월 '+SUBSTRING(A.REQ_DATE2,7,2)+'일'  END) AS DATE_NM, "
                    + "        LEFT(A.INDT,4)+' 년  '+CONVERT(VARCHAR,CONVERT(INT,SUBSTRING(A.INDT,5,2)))+' 월  '+CONVERT(VARCHAR,CONVERT(INT,SUBSTRING(A.INDT,7,2)))+' 일' AS INDT_NM, "
                    + "        A.LINE_REMK, A.REMARK1, A.REMARK2 "
                    + "   FROM " + tb_nm + " A "
                    + "   LEFT OUTER JOIN DUTY_MSTGNMU X1 "
                    + "     ON A.REQ_TYPE=X1.G_CODE ";
                if (tb_nm == "DUTY_TRSHREQ")
                {
                    qry += "   LEFT OUTER JOIN DUTY_MSTGNMU X2 "
                        + "     ON A.REQ_TYPE2=X2.G_CODE ";
                }
                qry +="   LEFT OUTER JOIN " + wagedb + ".dbo.MSTEMBS X3 "
                    + "     ON A.SABN=X3.EMBSSABN "
                    + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X4 "
                    + "     ON X3.EMBSDPCD=X4.DEPRCODE "
                    + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTPOSI X5 "
                    + "     ON X3.EMBSPSCD=X5.POSICODE "
                    + "  WHERE A.SEQNO = " + clib.TextToDecimal(seqno);

                qry += " ) A1 ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("5080_REPORT", dt, ref ds);
                
                string qry1 = " SELECT A.*, "
                            + "        (CASE WHEN X1.AP_TAG='8' AND A.LINE_AP_DT='' THEN '전 결' ELSE A.LINE_SANM END) LINE_STAT, "
                            + "        (CASE WHEN X1.AP_TAG='8' AND A.LINE_AP_DT='' THEN SUBSTRING(X1.AP_DT,5,2)+'/'+SUBSTRING(X1.AP_DT,7,2) "
                            + "              WHEN A.LINE_AP_DT='' THEN '' ELSE SUBSTRING(A.LINE_AP_DT,5,2)+'/'+SUBSTRING(A.LINE_AP_DT,7,2) END) AP_DT "
                            + "   FROM " + tb_nm + "_DT A "
                            + "   LEFT OUTER JOIN " + tb_nm + " X1 "
                            + "     ON A.SEQNO=X1.SEQNO "
                            + "  WHERE A.SEQNO = " + clib.TextToDecimal(seqno) + " AND A.LINE_SQ>1 "
                            + "  ORDER BY A.LINE_SQ ";
                DataTable dt1 = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry1);
                dp.AddDatatable2Dataset("5080_SIGN", dt1, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //연차,휴가내역 결재라인조회_상세
        public void Get5080_LINE_DTDatas(string tb_nm, string seqno, DataSet ds)
        {
            try
            {
                string qry = " SELECT * "
                           + "   FROM " + tb_nm
                           + "  WHERE SEQNO = " + clib.TextToDecimal(seqno)
                           //+ "    AND LINE_SQ>1 "
                           + "  ORDER BY LINE_SQ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset(tb_nm, dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion



        #region 3060 - 전체근무표조회

        //근무신청내역조회
        public void Get3060_SEARCHDatas(string yymm, int sq, DataSet ds)
        {
            try
            {
                string qry = " SELECT A1.* FROM ( "
                           + " SELECT A.SAWON_NO, A.DEPTCODE, A.PLAN_SQ, "
                           + "        RTRIM(X1.EMBSNAME) SAWON_NM, RTRIM(X2.DEPRNAM1) DEPT_NM, "
                           + "        A.D01, A.D02, A.D03, A.D04, A.D05, A.D06, A.D07, A.D08, A.D09, A.D10, "
                           + "        A.D11, A.D12, A.D13, A.D14, A.D15, A.D16, A.D17, A.D18, A.D19, A.D20, "
                           + "        A.D21, A.D22, A.D23, A.D24, A.D25, A.D26, A.D27, A.D28, A.D29, A.D30, A.D31, "
                           + "        '' D01_NM, '' D02_NM, '' D03_NM, '' D04_NM, '' D05_NM, '' D06_NM, '' D07_NM, '' D08_NM, '' D09_NM, '' D10_NM, "
                           + "        '' D11_NM, '' D12_NM, '' D13_NM, '' D14_NM, '' D15_NM, '' D16_NM, '' D17_NM, '' D18_NM, '' D19_NM, '' D20_NM, "
                           + "        '' D21_NM, '' D22_NM, '' D23_NM, '' D24_NM, '' D25_NM, '' D26_NM, '' D27_NM, '' D28_NM, '' D29_NM, '' D30_NM, '' D31_NM "
                           + "   FROM DUTY_TRSDANG A "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTEMBS X1 "
                           + "     ON A.SAWON_NO = X1.EMBSSABN "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTDEPR X2 "
                           + "     ON A.DEPTCODE = X2.DEPRCODE "
                           + "  WHERE A.PLANYYMM = '" + yymm + "' AND A.YYMM_SQ = " + sq
                           + "  UNION ALL "
                           + " SELECT A.SAWON_NO, A.DEPTCODE, A.PLAN_SQ, "
                           + "        RTRIM(X1.SAWON_NM) SAWON_NM, RTRIM(X2.DEPRNAM1) DEPT_NM, "
                           + "        A.D01, A.D02, A.D03, A.D04, A.D05, A.D06, A.D07, A.D08, A.D09, A.D10, "
                           + "        A.D11, A.D12, A.D13, A.D14, A.D15, A.D16, A.D17, A.D18, A.D19, A.D20, "
                           + "        A.D21, A.D22, A.D23, A.D24, A.D25, A.D26, A.D27, A.D28, A.D29, A.D30, A.D31, "
                           + "        '' D01_NM, '' D02_NM, '' D03_NM, '' D04_NM, '' D05_NM, '' D06_NM, '' D07_NM, '' D08_NM, '' D09_NM, '' D10_NM, "
                           + "        '' D11_NM, '' D12_NM, '' D13_NM, '' D14_NM, '' D15_NM, '' D16_NM, '' D17_NM, '' D18_NM, '' D19_NM, '' D20_NM, "
                           + "        '' D21_NM, '' D22_NM, '' D23_NM, '' D24_NM, '' D25_NM, '' D26_NM, '' D27_NM, '' D28_NM, '' D29_NM, '' D30_NM, '' D31_NM "
                           + "   FROM DUTY_TRSPLAN A "
                           + "   LEFT OUTER JOIN DUTY_MSTNURS X1 "
                           + "     ON A.SAWON_NO = X1.SAWON_NO "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTDEPR X2 "
                           + "     ON A.DEPTCODE = X2.DEPRCODE "
                           + "  WHERE A.PLANYYMM = '" + yymm + "' AND A.YYMM_SQ = " + sq
                           + " ) A1 "
                           + "  ORDER BY A1.DEPTCODE, A1.PLAN_SQ ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("3060_SEARCH", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //근무표 연차,휴가내역 조회
        public void Get3060_CHK1Datas(string yymm, DataSet ds)
        {
            try
            {
                string qry = " EXEC USP_DUTY3060_END_240701 '" + yymm + "', 1";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("3060_CHK1", dt, ref ds);
                DataRow nrow = ds.Tables["3060_CHK1"].NewRow();
                nrow["ERR_CHK"] = "";
                ds.Tables["3060_CHK1"].Rows.Add(nrow);

            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //휴가원 연차,휴가내역 조회
        public void Get3060_CHK2Datas(string yymm, DataSet ds)
        {
            try
            {
                string qry = " EXEC USP_DUTY3010_HY_240701 '" + yymm + "', '%'";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("3060_CHK2", dt, ref ds);
                DataRow nrow = ds.Tables["3060_CHK2"].NewRow();
                nrow["ERR_CHK"] = "";
                ds.Tables["3060_CHK2"].Rows.Add(nrow);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //인사기본정보_상세
        public void GetMSTEMBSDatas(string sabn, DataSet ds)
        {
            try
            {
                string qry = " SELECT A.*, "
                           + "        RTRIM(cast(DECRYPTBYPASSPHRASE('samilpas',A.EMBSPTSA) as varchar(13))) AS D_JMNO, "
                           + "        RTRIM(cast(DECRYPTBYPASSPHRASE('samilpas',A.EMBSPHPN) as varchar(20))) AS D_HPNO, "
                           + "        RTRIM(cast(DECRYPTBYPASSPHRASE('samilpas',A.EMBSPTLN) as varchar(20))) AS D_TLNO, "
                           + "        RTRIM(cast(DECRYPTBYPASSPHRASE('samilpas',A.EMBSPAD1) as varchar(100))) AS D_ADR1, "
                           + "        RTRIM(cast(DECRYPTBYPASSPHRASE('samilpas',A.EMBSPAD2) as varchar(100))) AS D_ADR2 "
                           + "   FROM MSTEMBS A "
                           + "  WHERE A.EMBSSABN = '" + sabn + "'";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), wagedb, qry);
                dp.AddDatatable2Dataset("MSTEMBS", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //출퇴근조회
        public void GetSEARCH_KT1Datas(string yymm, DataSet ds)
        {
            try
            {
                string qry = " SELECT A1.* FROM ("
                           + " SELECT A.USERID, RIGHT(A.USERID,5) as SABN, X3.USERNAME, RTRIM(ISNULL(X2.DEPRNAM1,'')) AS DEPT_NM, "
                           + "        SUBSTRING(CONVERT(VARCHAR,WORKSTART,112),5,2)+'/'+SUBSTRING(CONVERT(VARCHAR,WORKSTART,112),7,2) AS SLDT, 1 as ACC_TIME, "
                           + "        RIGHT(A.USERID,5)+SUBSTRING(CONVERT(VARCHAR,WORKSTART,112),5,2)+'/'+SUBSTRING(CONVERT(VARCHAR,WORKSTART,112),7,2) AS CHK_ROW, "
                           + "        LEFT(CONVERT(VARCHAR,WORKSTART,112),4)+'-'+SUBSTRING(CONVERT(VARCHAR,WORKSTART,112),5,2)+'-'+SUBSTRING(CONVERT(VARCHAR,WORKSTART,112),7,2) SLDT_NM, "
                           + "        CONVERT(VARCHAR,A.WORKSTART,120) AS ACCESSDATE, "
                           + "        '출근' AS GUBN_NM, "
                           + "        (CASE WHEN X1.EMBSSABN IS NULL THEN '1' ELSE '' END) EMBS_STAT "
                           + "   FROM TB_WORKRESULT A LEFT OUTER JOIN TB_USER X3 ON A.USERID=X3.USERID "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTEMBS X1 ON RIGHT(A.USERID,5)=X1.EMBSSABN "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTDEPR X2 ON X1.EMBSDPCD=X2.DEPRCODE "
                           + "  WHERE CONVERT(VARCHAR,A.WORKSTART,112) LIKE '" + yymm + "%' "
                           + "    AND SUBSTRING(A.USERID,8,1)='9' AND A.WORKSTART IS NOT NULL "
                           + " UNION ALL "
                           + " SELECT A.USERID, RIGHT(A.USERID,5) as SABN, X3.USERNAME, RTRIM(ISNULL(X2.DEPRNAM1,'')) AS DEPT_NM, "
                           + "        SUBSTRING(CONVERT(VARCHAR,WORKEND,112),5,2)+'/'+SUBSTRING(CONVERT(VARCHAR,WORKEND,112),7,2) AS SLDT, 1 as ACC_TIME, "
                           + "        RIGHT(A.USERID,5)+SUBSTRING(CONVERT(VARCHAR,WORKEND,112),5,2)+'/'+SUBSTRING(CONVERT(VARCHAR,WORKEND,112),7,2) AS CHK_ROW, "
                           + "        LEFT(CONVERT(VARCHAR,WORKEND,112),4)+'-'+SUBSTRING(CONVERT(VARCHAR,WORKEND,112),5,2)+'-'+SUBSTRING(CONVERT(VARCHAR,WORKEND,112),7,2) SLDT_NM, "
                           + "        CONVERT(VARCHAR,A.WORKEND,120) AS ACCESSDATE, "
                           + "        '퇴근' AS GUBN_NM, "
                           + "        (CASE WHEN X1.EMBSSABN IS NULL THEN '1' ELSE '' END) EMBS_STAT "
                           + "   FROM TB_WORKRESULT A LEFT OUTER JOIN TB_USER X3 ON A.USERID=X3.USERID "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTEMBS X1 ON RIGHT(A.USERID,5)=X1.EMBSSABN "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTDEPR X2 ON X1.EMBSDPCD=X2.DEPRCODE "
                           + "  WHERE CONVERT(VARCHAR,A.WORKEND,112) LIKE '" + yymm + "%' "
                           + "    AND SUBSTRING(A.USERID,8,1)='9' AND A.WORKEND IS NOT NULL "
                           + " ) A1 "
                           + "  ORDER BY A1.SABN, A1.ACCESSDATE ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SEARCH_KT1", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //식수조회
        public void GetSEARCH_KT2Datas(string frdt, string todt, DataSet ds)
        {
            try
            {
                string qry = " SELECT A.USERID, RIGHT(A.USERID,5) as SABN, A.USERNAME, A.DEPARTMENTNAME AS DEPT_NM, "
                           + "        LEFT(CONVERT(VARCHAR,ACCE_DATE,112),4)+'-'+SUBSTRING(CONVERT(VARCHAR,ACCE_DATE,112),5,2)+'-'+SUBSTRING(CONVERT(VARCHAR,ACCE_DATE,112),7,2) SLDT_NM, "
                           + "        CONVERT(VARCHAR,A.ACCE_DATE,120) AS ACCESSDATE, "
                           + "        (CASE WHEN X1.EMBSSABN IS NULL THEN '1' ELSE '' END) EMBS_STAT "
                           + "   FROM TB_FOODRESULT A "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTEMBS X1 ON RIGHT(A.USERID,5)=X1.EMBSSABN "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTDEPR X2 ON X1.EMBSDPCD=X2.DEPRCODE "
                           + "  WHERE CONVERT(VARCHAR,A.ACCE_DATE,112) BETWEEN '" + frdt + "' AND '" + todt + "'"
                           + "    AND SUBSTRING(A.USERID,8,1)='9' "
                           + "  ORDER BY RIGHT(A.USERID,5), A.ACCE_DATE ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SEARCH_KT2", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //출근내역 조회
        public void Get5010_SEARCH3_KTDatas(string yymm, DataSet ds)
        {
            try
            {
                string qry = " SELECT A.USERID, RIGHT(A.USERID,5) as SABN, X1.USERNAME, "
                           + "        LEFT(CONVERT(VARCHAR,WORKSTART,112),8) AS SLDT "
                           + "   FROM TB_WORKRESULT A LEFT OUTER JOIN TB_USER X1 ON A.USERID=X1.USERID "
                           + "  WHERE CONVERT(VARCHAR,A.WORKSTART,112) LIKE '" + yymm + "%' "
                           + "    AND SUBSTRING(A.USERID,8,1)='9' AND A.WORKSTART IS NOT NULL  "
                           + "  ORDER BY RIGHT(A.USERID,5), A.WORKSTART ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("5010_SEARCH3_KT", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 3070 - 연차정산관리

        //사원리스트 조회
        public void Get3070_SEARCH_SABNDatas(string dept, DataSet ds)
        {
            try
            {
                string qry = " SELECT A.EMBSDPCD, A.EMBSSABN SABN, RTRIM(A.EMBSNAME) EMBSNAME, "
                           + "        ISNULL(X2.DEPRNAM2,'') DEPR_NM  "
                           + "   FROM " + wagedb + ".dbo.MSTEMBS A "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X2 "
                           + "     ON A.EMBSDPCD=X2.DEPRCODE"
                           + "  WHERE A.EMBSSTAT=1 "
                           + "    AND A.EMBSDPCD LIKE '" + dept + "'"
                           + "  ORDER BY A.EMBSDPCD, A.EMBSSABN ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("3070_SEARCH_SABN", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //조회
        public void GetS_DUTY_TRSHREQDatas(string yymm, DataSet ds)
        {
            try
            {
                string qry = " SELECT A.*, "
                           + "        LEFT(A.REQ_DATE,4)+SUBSTRING(A.REQ_DATE,5,2) AS YYMM_NM, "
                           + "        RTRIM(X1.EMBSNAME) AS EMBSNAME, RTRIM(ISNULL(X2.DEPRNAM1,'')) AS DEPT_NM "
                           + "   FROM DUTY_TRSHREQ A "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTEMBS X1 ON A.SABN=X1.EMBSSABN "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTDEPR X2 ON X1.EMBSDPCD=X2.DEPRCODE "
                           + "  WHERE A.PSTY<>'D' AND A.AP_TAG='9' AND LEFT(A.REQ_DATE,6) = '" + yymm + "' "
                           + "  ORDER BY X1.EMBSDPCD, X2.DEPRNAM1, A.SABN ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("S_DUTY_TRSHREQ", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //처리
        public void GetSEARCH_HREQDatas(string yymm, DataSet ds)
        {
            try
            {
                string qry = " SELECT A.*, "
                           + "        '" + yymm.Substring(0, 6) + "' AS YYMM, "
                           + "        '" + yymm.Substring(0, 4) + "-" + yymm.Substring(4, 2) + "' AS YYMM_NM, "
                           + "        X1.EMBSDPCD, RTRIM(ISNULL(X2.DEPRNAM1,'')) AS DEPT_NM, "
                           + "        RTRIM(X1.EMBSNAME) AS SABN_NM "
                           + "   FROM DUTY_TRSHREQ A "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTEMBS X1 ON A.SABN=X1.EMBSSABN "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTDEPR X2 ON X1.EMBSDPCD=X2.DEPRCODE "
                           + "  WHERE A.PSTY<>'D' AND A.AP_TAG='9' "
                           + "    AND LEFT(A.REQ_DATE,6) = '" + yymm + "' "
                           + "  ORDER BY X1.EMBSDPCD, X2.DEPRNAM1, A.SABN ";
                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SEARCH_HREQ", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //저장시 기존 연차정산내역 조회
        public void GetDUTY_TRSHREQ_JSDatas(string sabn, string sldt, DataSet ds)
        {
            try
            {
                string qry = " SELECT A.* "
                           + "   FROM DUTY_TRSHREQ A "
                           + "  WHERE A.SABN = '" + sabn + "'"
                           + "    AND A.REQ_DATE = '" + sldt + "' AND A.AP_TAG='9' ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DUTY_TRSHREQ", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //내역조회
        public void GetS_DUTY_TRSHREQ2Datas(string frmm, string tomm, DataSet ds)
        {
            try
            {
                string qry = " SELECT RTRIM(ISNULL(X1.EMBSNAME,'')) AS EMBSNAME, RTRIM(ISNULL(X2.DEPRNAM1,'')) AS DEPT_NM, "
                           + "        LEFT(A.REQ_DATE,4)+'-'+SUBSTRING(A.REQ_DATE,5,2) AS YYMM_NM, "
                           + "        A.* "
                           + "   FROM DUTY_TRSHREQ A "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTEMBS X1 ON A.SABN=X1.EMBSSABN "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTDEPR X2 ON X1.EMBSDPCD=X2.DEPRCODE "
                           + "  WHERE LEFT(A.REQ_DATE,6) BETWEEN '" + frmm + "' AND '" + tomm + "' "
                           + "    AND A.PSTY<>'D' AND A.AP_TAG='9' "
                           + "  ORDER BY X1.EMBSDPCD, X2.DEPRNAM1, A.SABN ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("S_DUTY_TRSHREQ2", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 3090 - 최종마감설정

        //최종마감현황 조회
        public void GetSEARCH_ENDSDatas(string year, DataSet ds)
        {
            try
            {
                string qry = " SELECT A.*, "
                           + "        LEFT(A.END_YYMM,4)+'-'+SUBSTRING(A.END_YYMM,5,2) YYMM_NM, "
                           + "        (CASE A.CLOSE_YN WHEN 'Y' THEN '마감완료' WHEN 'N' THEN '마감취소' ELSE '' END) CLOSE_NM, "
                           + "        (CASE WHEN A.CLOSE_YN='Y' THEN A.END_DT+' (' +A.END_ID+')' "
                           + "              WHEN A.CLOSE_YN='N' THEN A.CANC_DT+' (' +A.CANC_ID+')' ELSE '' END) REMARK "
                           + "   FROM DUTY_MSTENDS A "
                           + "  WHERE LEFT(A.END_YYMM,4) = '" + year + "' ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SEARCH_ENDS", dt, ref ds);

                for (int i = 1; i <= 12; i++)
                {
                    if (ds.Tables["SEARCH_ENDS"].Select("END_YYMM = '" + year.Substring(0, 4) + i.ToString().PadLeft(2, '0') + "'").Length == 0)
                    {
                        DataRow drow = ds.Tables["SEARCH_ENDS"].NewRow();
                        drow["END_YYMM"] = year + i.ToString().PadLeft(2, '0');
                        drow["YYMM_NM"] = year + "-" + i.ToString().PadLeft(2, '0');
                        drow["CLOSE_NM"] = "";
                        drow["REMARK"] = "";
                        ds.Tables["SEARCH_ENDS"].Rows.Add(drow);
                    }
                }
                ds.Tables["SEARCH_ENDS"].DefaultView.Sort = "END_YYMM";
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //저장시 해당년월 마감조회
        public void GetDUTY_MSTENDSDatas(string yymm, DataSet ds)
        {
            try
            {
                string qry = " SELECT * "
                           + "   FROM DUTY_MSTENDS "
                           + "  WHERE END_YYMM = '" + yymm + "' ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DUTY_MSTENDS", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //마감로그조회
        public void GetSEARCH_ENDS_LOGDatas(string yymm, DataSet ds)
        {
            try
            {
                string qry = " SELECT A.*, "
                           + "        LEFT(A.END_YYMM,4)+'-'+SUBSTRING(A.END_YYMM,5,2) YYMM_NM, "
                           + "        (CASE A.CLOSE_YN WHEN 'Y' THEN '마감완료' WHEN 'N' THEN '마감취소' ELSE '' END) CLOSE_NM, "
                           + "        A.REG_DT+' (' +A.REG_ID+')' AS REMARK "
                           + "   FROM DUTY_MSTENDS_LOG A "
                           + "  WHERE A.END_YYMM = '" + yymm + "'"
                           + "  ORDER BY A.REG_DT ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SEARCH_ENDS_LOG", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //마감로그 테이블구조
        public void GetDUTY_MSTENDS_LOGDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT * "
                           + "   FROM DUTY_MSTENDS_LOG "
                           + "  WHERE 1=2 ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DUTY_MSTENDS_LOG", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 3080 - 근무집계표

        //처리
        public void GetWORK_3080Datas(string yymm, string gubn, DataSet ds)
        {
            try
            {
                string qry = " EXEC USP_DUTY3080_PRC_240701 '" + yymm + "', " + gubn + " ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("WORK_3080", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //마감 테이블 구조
        public void GetS_DUTY_MSTWGPCDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT * "
                           + "   FROM DUTY_MSTWGPC "
                           + "  WHERE 1=2 ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("S_DUTY_MSTWGPC", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //마감 테이블 불러오기
        public void GetDUTY_MSTWGPC_ENDDatas(string end_yymm, string sabn, int gubn, DataSet ds)
        {
            try
            {
                string qry = " SELECT A.* "
                           + "   FROM DUTY_MSTWGPC_END A "
                           + "  WHERE A.END_YYMM = '" + end_yymm + "' "
                           + "    AND A.SAWON_NO = '" + sabn + "' AND A.GUBN = " + gubn + "";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DUTY_MSTWGPC_END", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        //마감내역 조회
        public void GetSEARCH_3080Datas(string yymm, string sabn, DataSet ds)
        {
            try
            {
                string qry = " SELECT A.*, "
                           + "        RTRIM(X1.EMBSNAME) SAWON_NM, RTRIM(X2.DEPRNAM1) DEPT_NM, "
                           + "        LEFT(A.END_YYMM,4)+'-'+SUBSTRING(A.END_YYMM,5,2) END_YYMM_NM, "
                           + "        '' SEND_YN "
                           + "   FROM DUTY_MSTWGPC A "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTEMBS X1 ON A.SAWON_NO=X1.EMBSSABN "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTDEPR X2 ON X1.EMBSDPCD=X2.DEPRCODE "
                           //+ "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTWGPC W1 ON A.END_YYMM=W1.WGPCYYMM AND W1.WGPCSQNO='1' AND A.SAWON_NO=W1.WGPCSABN "
                           //+ "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTGTMM W2 ON A.END_YYMM=W2.GTMMYYMM AND A.SAWON_NO=W2.GTMMSABN "
                           + "  WHERE A.END_YYMM = '" + yymm + "' AND A.SAWON_NO LIKE '" + sabn + "'"
                           + "  ORDER BY A.END_YYMM, X1.EMBSDPCD, A.SAWON_NO ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SEARCH_3080", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //조회
        public void GetSEARCH_3081Datas(string frmm, string tomm, string dept, DataSet ds)
        {
            try
            {
                string qry = " SELECT A.*, "
                           + "        RTRIM(X1.EMBSNAME) SAWON_NM, RTRIM(X2.DEPRNAM1) DEPT_NM, "
                           + "        LEFT(A.END_YYMM,4)+'-'+SUBSTRING(A.END_YYMM,5,2) END_YYMM_NM "
                           + "   FROM DUTY_MSTWGPC A "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTEMBS X1 ON A.SAWON_NO=X1.EMBSSABN "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X2 ON X1.EMBSDPCD=X2.DEPRCODE "
                           + "  WHERE A.END_YYMM BETWEEN '" + frmm + "' AND '" + tomm + "' AND X1.EMBSDPCD LIKE '" + dept + "'"
                           + "  ORDER BY A.END_YYMM, X1.EMBSDPCD, A.SAWON_NO ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SEARCH_3081", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //근태조회 -> 수당조회
        public void GetSEARCH_INFOWAGEDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT A.* "
                           + "   FROM " + wagedb + ".dbo.INFOWAGE A "
                           + "  ORDER BY A.IFWGCODE ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SEARCH_INFOWAGE", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //마감 테이블 불러오기
        public void GetDUTY_MSTWGPCDatas(string end_yymm, string sabn, DataSet ds)
        {
            try
            {
                string qry = " SELECT A.* "
                           + "   FROM DUTY_MSTWGPC A "
                           + "  WHERE A.END_YYMM = '" + end_yymm + "' "
                           + "    AND A.SAWON_NO LIKE '" + sabn + "' ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DUTY_MSTWGPC", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //인사급여 마감 데이터
        public void Get3080_WAGE_ENDSDatas(string yymm, DataSet ds)
        {
            try
            {
                string qry = " SELECT CLSEYN" + yymm.Substring(4, 2) + " AS MM_CHK, "
                           + "        CLSEYN00 AS YY_CHK"
                           + "   FROM MSTCLSE "
                           + "  WHERE CLSEYEAR = '" + yymm.Substring(0, 4) + "' ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), wagedb, qry);
                dp.AddDatatable2Dataset("3080_WAGE_ENDS", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //인사급여 테이블 불러오기
        public void GetMSTWGPCDatas(string yymm, string sabn, DataSet ds)
        {
            try
            {
                string qry = " SELECT * "
                           + "   FROM MSTWGPC "
                           + "  WHERE WGPCYYMM = '" + yymm.Substring(0, 6) + "' AND WGPCSABN LIKE '" + sabn + "' AND WGPCSQNO='1'";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("MSTWGPC", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //인사급여 테이블 불러오기
        public void GetMSTGTMMDatas(string yymm, string sabn, DataSet ds)
        {
            try
            {
                string qry = " SELECT * "
                           + "   FROM MSTGTMM "
                           + "  WHERE GTMMYYMM = '" + yymm.Substring(0, 6) + "' AND GTMMSABN LIKE '" + sabn + "' ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("MSTGTMM", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion



        #region 9070 - 결재라인관리

        //부서리스트 조회
        public void Get9070_SEARCH_DEPTDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT A.DEPRCODE CODE, RTRIM(A.DEPRNAM1) NAME, "
                           + "        (CASE WHEN ISNULL(X1.DEPT,'') = '' THEN '' ELSE '1' END) CHK "
                           + "   FROM " + wagedb + ".dbo.MSTDEPR A "
                           + "   LEFT OUTER JOIN (SELECT DISTINCT DEPT FROM DUTY_GW_LINE_DEPT ) X1 "
                           + "     ON A.DEPRCODE=X1.DEPT"
                           + "  WHERE A.DEPRSTAT=1 "
                           + "  ORDER BY A.DEPRCODE ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("9070_SEARCH_DEPT", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //사원리스트 조회
        public void Get9070_SEARCH_SABNDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT A.EMBSDPCD, A.EMBSSABN SABN, RTRIM(A.EMBSNAME) EMBSNAME, "
                           + "        ISNULL(X2.DEPRNAM2,'') DEPR_NM,  "
                           + "        (CASE WHEN ISNULL(X1.SABN,'') = '' THEN '' ELSE '1' END) CHK "
                           + "   FROM " + wagedb + ".dbo.MSTEMBS A "
                           + "   LEFT OUTER JOIN (SELECT DISTINCT SABN FROM DUTY_GW_LINE ) X1 "
                           + "     ON A.EMBSSABN=X1.SABN"
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X2 "
                           + "     ON A.EMBSDPCD=X2.DEPRCODE"
                           + "  WHERE A.EMBSSTAT=1 "
                           + "  ORDER BY A.EMBSDPCD, A.EMBSSABN ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("9070_SEARCH_SABN", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //연차신청 결재라인_부서_등록용
        public void GetDUTY_GW_LINE_DEPTDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT A.* "
                           + "   FROM DUTY_GW_LINE_DEPT A "
                           + "  WHERE 1=2";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DUTY_GW_LINE_DEPT", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //연차신청 결재라인_개인_등록용
        public void GetDUTY_GW_LINEDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT A.* "
                           + "   FROM DUTY_GW_LINE A "
                           + "  WHERE 1=2";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DUTY_GW_LINE", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //연차신청 결재라인_삭제용
        public void GetDEL_GW_LINEDatas(string sabn, DataSet ds)
        {
            try
            {
                string qry = " SELECT * "
                           + "   FROM DUTY_GW_LINE "
                           + "  WHERE SABN = '" + sabn + "'"
                           + "  ORDER BY LINE_SQ ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DEL_GW_LINE", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //연차신청 결재라인_부서_삭제용
        public void GetDEL_GW_LINE_DEPTDatas(int gubn, string code, DataSet ds)
        {
            try
            {
                string w_dt = gubn == 1 ? "(SELECT EMBSDPCD FROM " + wagedb + ".dbo.MSTEMBS WHERE EMBSSABN='" + code + "')" : "'" + code + "'";
                string qry = " SELECT * "
                           + "   FROM DUTY_GW_LINE_DEPT "
                           + "  WHERE DEPT = " + w_dt
                           + "  ORDER BY LINE_SQ ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DEL_GW_LINE_DEPT", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 9010 - 시급관리

        //시급처리
        public void GetS_INFOSD01Datas(string yymm, DataSet ds)
        {
            try
            {
                string qry = " SELECT '" + yymm.Substring(0, 6) + "' AS YYMM, "
                           + "        '" + yymm.Substring(0, 4) + "-" + yymm.Substring(4, 2) + "' AS YYMM_NM, "
                           + "        RTRIM(A.EMBSSABN) AS SABN, "
                           + "        RTRIM(A.EMBSNAME) AS SABN_NM, "
                           + "        RTRIM(ISNULL(X1.DEPRNAM1,'')) AS DEPT_NM, "
                           + "        ISNULL(X2.YY_AMT,0) AS YY_AMT, "
                           + "        ISNULL(X2.T_AMT,0) AS T_AMT "
                           + "   FROM " + wagedb + ".dbo.MSTEMBS A "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTDEPR X1 ON A.EMBSDPCD=X1.DEPRCODE "
                           + "   LEFT OUTER JOIN DUTY_INFOSD01 X2 ON A.EMBSSABN=X2.SABN "
                           + "    AND X2.YYMM = '" + yymm.Substring(0, 6) + "' "
                           + "  WHERE A.EMBSSTAT=1 OR (A.EMBSSTAT=2 AND LEFT(A.EMBSTSDT,6)>='" + yymm.Substring(0, 6) + "')"
                           + "  ORDER BY A.EMBSJOCD, A.EMBSDPCD, A.EMBSSABN ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("S_INFOSD01", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //전월시급조회
        public void GetBF_INFOSD01Datas(string bfmm, DataSet ds)
        {
            try
            {
                string qry = " SELECT '" + bfmm.Substring(0, 6) + "' AS YYMM, "
                           + "        '" + bfmm.Substring(0, 4) + "-" + bfmm.Substring(4, 2) + "' AS YYMM_NM, "
                           + "        RTRIM(X1.EMBSSABN) AS SABN, "
                           + "        RTRIM(X1.EMBSNAME) AS SABN_NM, "
                           + "        RTRIM(ISNULL(X2.DEPRNAM1,'')) AS DEPT_NM, "
                           + "        ISNULL(A.YY_AMT,0) AS YY_AMT, "
                           + "        ISNULL(A.T_AMT,0) AS T_AMT "
                           + "   FROM DUTY_INFOSD01 A "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTEMBS X1 ON A.SABN=X1.EMBSSABN "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTDEPR X2 ON X1.EMBSDPCD=X2.DEPRCODE "
                           + "  WHERE A.YYMM = '" + bfmm.Substring(0, 6) + "' "
                           + "  ORDER BY X1.EMBSJOCD, X1.EMBSDPCD, X1.EMBSSABN ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("BF_INFOSD01", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //시급 불러오기
        public void GetDUTY_INFOSD01Datas(string yymm, DataSet ds)
        {
            try
            {
                string qry = " SELECT * "
                           + "   FROM DUTY_INFOSD01 WHERE YYMM = '" + yymm + "' ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DUTY_INFOSD01", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //부서명칭 가져오기
        public string GetDept_nmData(string sabn)
        {
            string dept_nm = "";
            try
            {
                string qry = " SELECT RTRIM(ISNULL(X1.DEPRNAM1,'')) AS NAME "
                           + "   FROM " + wagedb + ".DBO.MSTEMBS A "
                           + "  WHERE A.EMBSSABN = '" + sabn + "'"
                           + "   LEFT OUTER JOIN MSTDEPR X1 ON A.EMBSDPCD=X1.DEPRCODE ";

                object obj = gd.GetOneData(1, dbname, qry);
                dept_nm = obj == null ? "" : obj.ToString();
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dept_nm;
        }
        //사원명칭 가져오기
        public string GetSawon_nmData(string sabn)
        {
            string Sawon_nm = "";
            try
            {
                string qry = " SELECT RTRIM(A.EMBSNAME) AS NAME "
                           + "   FROM " + wagedb + ".DBO.MSTEMBS A "
                           + "  WHERE A.EMBSSABN = '" + sabn + "'";

                object obj = gd.GetOneData(1, dbname, qry);
                Sawon_nm = obj == null ? "" : obj.ToString();
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Sawon_nm;
        }
        //시급 조회
        public void GetSEARCH_INFOSD01Datas(string frmm, string tomm, DataSet ds)
        {
            try
            {
                string qry = " SELECT RTRIM(ISNULL(X2.DEPRNAM1,'')) AS DEPT_NM, "
                           + "        RTRIM(X1.EMBSNAME) AS EMBSNAME, "
                           + "        LEFT(A.YYMM,4)+'-'+SUBSTRING(A.YYMM,5,2) AS YYMM_NM, "
                           + "        A.* "
                           + "   FROM DUTY_INFOSD01 A "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTEMBS X1 ON A.SABN=X1.EMBSSABN "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTDEPR X2 ON X1.EMBSDPCD=X2.DEPRCODE "
                           + "  WHERE A.YYMM BETWEEN '" + frmm + "' AND '" + tomm + "' "
                           + "    AND (A.YY_AMT <> 0 OR A.T_AMT <> 0)"
                           + "  ORDER BY X1.EMBSJOCD, X1.EMBSDPCD ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SEARCH_INFOSD01", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 9020 - 당직시간관리

        //당직시간 조회
        public void GetS_INFOSD02Datas(string yymm, DataSet ds)
        {
            try
            {
                string qry = " SELECT *, "
                           + "        LEFT(DANG_DT,4)+'-'+SUBSTRING(DANG_DT,5,2)+'-'+SUBSTRING(DANG_DT,7,2) AS SLDT_NM, "
                           + "        (CASE DATEPART(DW,DANG_DT) WHEN 1 THEN '일' WHEN 2 THEN '월' WHEN 3 THEN '화' "
                           + "         WHEN 4 THEN '수' WHEN 5 THEN '목' WHEN 6 THEN '금' WHEN 7 THEN '토' ELSE '' END) DAY_NM "
                           + "   FROM DUTY_INFOSD02 "
                           + "  WHERE LEFT(DANG_DT,6)= '" + yymm + "' "
                           + "  ORDER BY DANG_DT ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("S_INFOSD02", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //일자별 당직시간 저장
        public void GetDUTY_INFOSD02Datas(string yymm, DataSet ds)
        {
            try
            {
                string qry = " SELECT * "
                           + "   FROM DUTY_INFOSD02 "
                           + "  WHERE LEFT(DANG_DT,6)= '" + yymm + "' "
                           + "  ORDER BY DANG_DT ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DUTY_INFOSD02", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //요일별설정
        public void GetDUTY_INFOSD02_DTDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT * "
                           + "   FROM DUTY_INFOSD02_DT";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DUTY_INFOSD02_DT", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 9030 - 근로시간관리

        //근로시간 처리
        public void GetS_INFOSD03Datas(string sldt, DataSet ds)
        {
            try
            {
                string qry = " SELECT RTRIM(X2.DEPRNAM2) AS DEPT_NM, "
                           + "        RTRIM(A.EMBSSABN) AS SABN, RTRIM(A.EMBSNAME) AS SABN_NM, "
                           + "        ISNULL(X1.WT_DAY,0) AS WT_DAY, "
                           + "        ISNULL(X1.WT_WEEK,0) AS WT_WEEK, "
                           + "        ISNULL(X1.WT_MON,0) AS WT_MON, "
                           + "        A.* "
                           + "   FROM " + wagedb + ".dbo.MSTEMBS A "
                           + "   LEFT OUTER JOIN DUTY_INFOSD03 X1 "
                           + "     ON A.EMBSSABN=X1.SABN "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X2 ON A.EMBSDPCD=X2.DEPRCODE "
                           + "  WHERE A.EMBSSTAT='1' OR (A.EMBSSTAT='2' AND A.EMBSTSDT>= '" + sldt + "')"
                           + "  ORDER BY A.EMBSDPCD, A.EMBSSABN ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("S_INFOSD03", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //근로시간 불러오기
        public void GetDUTY_INFOSD03Datas(DataSet ds)
        {
            try
            {
                string qry = " SELECT * "
                           + "   FROM DUTY_INFOSD03 ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DUTY_INFOSD03", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //근로시간 조회
        public void GetSEARCH_INFOSD03Datas(DataSet ds)
        {
            try
            {
                string qry = " SELECT RTRIM(X2.DEPRNAM2) AS DEPT_NM, X1.EMBSNAME, A.* "
                           + "   FROM DUTY_INFOSD03 A "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTEMBS X1 ON A.SABN=X1.EMBSSABN "
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X2 ON X1.EMBSDPCD=X2.DEPRCODE "
                           + "  ORDER BY X1.EMBSDPCD, A.SABN ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SEARCH_INFOSD03", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 9040 - 건별수당관리

        //수당종류1 LOOKUP
        public void GetSL_SD1Datas(DataSet ds)
        {
            try
            {
                string qry = " SELECT A.IFWGCODE SD_CODE, RTRIM(A.IFWGNAME) SD_NAME, "
                           + "        ISNULL(X1.CHK,'0') AS CHK "
                           + "   FROM " + wagedb + ".DBO.INFOWAGE A "
                           + "   LEFT OUTER JOIN DUTY_INFOSD04_SET X1 ON A.IFWGCODE=X1.SD_CODE "
                           + "  WHERE LEFT(A.IFWGCODE,1)='A' AND A.IFWGCODE<='A020'"
                           + "    AND A.IFWGCRGB = '1' AND A.IFWGJYGB IN ('12') "
                           + "  ORDER BY A.IFWGCODE ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SL_SD1", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //수당종류2 LOOKUP
        public void GetSL_SD2Datas(DataSet ds)
        {
            try
            {
                string qry = " SELECT A.IFWGCODE SD_CODE, RTRIM(A.IFWGNAME) SD_NAME, "
                           + "        ISNULL(X1.CHK,'0') AS CHK "
                           + "   FROM " + wagedb + ".DBO.INFOWAGE A "
                           + "   LEFT OUTER JOIN DUTY_INFOSD04_SET X1 ON A.IFWGCODE=X1.SD_CODE "
                           + "  WHERE LEFT(A.IFWGCODE,1)='A' AND A.IFWGCODE>'A020'"
                           + "    AND A.IFWGCRGB = '1' AND A.IFWGJYGB IN ('12') "
                           + "  ORDER BY A.IFWGCODE ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SL_SD2", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //건별수당설정 내역 불러오기
        public void GetDUTY_INFOSD04_SETDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT * "
                           + "   FROM DUTY_INFOSD04_SET WHERE 1=2 ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DUTY_INFOSD04_SET", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //수당종류 LOOKUP
        public void GetSL_SD_LISTDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT A.SD_CODE, RTRIM(X1.IFWGNAME) SD_NAME "
                           + "   FROM DUTY_INFOSD04_SET A"
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.INFOWAGE X1 "
                           + "     ON A.SD_CODE = X1.IFWGCODE "
                           + "  ORDER BY A.SD_CODE ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SL_SD_LIST", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //건별수당 불러오기
        public void GetS_INFOSD04Datas(string sldt, string sd_code, DataSet ds)
        {
            try
            {
                string qry = " SELECT RTRIM(X2.DEPRNAM2) AS DEPT_NM, "
                           + "        RTRIM(A.EMBSSABN) AS SABN, RTRIM(A.EMBSNAME) AS SABN_NM, "
                           + "        ISNULL(X1.SD_FIX,0) AS SD_FIX, "
                           + "        ISNULL(X1.SD_CNT,0) AS SD_CNT, "
                           + "        ISNULL(X1.SD_NIGHT_SPV,0) AS SD_NIGHT_SPV, "
                           + "        A.* "
                           + "   FROM " + wagedb + ".dbo.MSTEMBS A "
                           + "   LEFT OUTER JOIN DUTY_INFOSD04 X1 "
                           + "     ON A.EMBSSABN=X1.SABN AND X1.SD_CODE ='" + sd_code + "'"
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X2 ON A.EMBSDPCD=X2.DEPRCODE "
                           + "  WHERE A.EMBSSTAT='1' OR (A.EMBSSTAT='2' AND A.EMBSTSDT>= '" + sldt + "')"
                           + "  ORDER BY A.EMBSDPCD, A.EMBSSABN ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("S_INFOSD04", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void GetDUTY_INFOSD04Datas(string sd_code, DataSet ds)
        {
            try
            {
                string qry = " SELECT * "
                           + "   FROM DUTY_INFOSD04 WHERE SD_CODE = '" + sd_code + "' ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DUTY_INFOSD04", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //건별수당 조회
        public void GetSEARCH_INFOSD04Datas(string stdt, DataSet ds)
        {
            try
            {
                string qry = " SELECT RTRIM(ISNULL(X1.EMBSNAME,'')) AS EMBSNAME, "
                           + "        RTRIM(ISNULL(X2.DEPRNAM2,'')) AS DEPT_NM, "
                           + "        RTRIM(ISNULL(X3.IFWGNAME,'')) AS SD_NAME, "
                           + "        A.* "
                           + "   FROM DUTY_INFOSD04 A "
                           + "  INNER JOIN " + wagedb + ".DBO.MSTEMBS X1 ON A.SABN=X1.EMBSSABN "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTDEPR X2 ON X1.EMBSDPCD=X2.DEPRCODE "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.INFOWAGE X3 ON A.SD_CODE=X3.IFWGCODE "
                           + "  WHERE A.SD_FIX <> 0 OR A.SD_CNT <> 0 OR A.SD_NIGHT_SPV <> 0 "
                           //+ "  WHERE X1.EMBSSTAT='1' OR (X1.EMBSSTAT='2' AND X1.EMBSTSDT>= '" + stdt + "')"
                           + "  ORDER BY A.SD_CODE, X1.EMBSDPCD, A.SABN ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SEARCH_INFOSD04", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 9050 - 만근수당관리

        //수당종류1 LOOKUP
        public void GetSL_SD21Datas(DataSet ds)
        {
            try
            {
                string qry = " SELECT A.IFWGCODE SD_CODE, RTRIM(A.IFWGNAME) SD_NAME, "
                           + "        ISNULL(X1.CHK,'0') AS CHK "
                           + "   FROM " + wagedb + ".DBO.INFOWAGE A "
                           + "   LEFT OUTER JOIN DUTY_INFOSD05_SET X1 ON A.IFWGCODE=X1.SD_CODE "
                           + "  WHERE LEFT(A.IFWGCODE,1)='A' AND A.IFWGCODE<='A020'"
                           + "    AND A.IFWGCRGB = '1' AND A.IFWGJYGB IN ('12') "
                           + "  ORDER BY A.IFWGCODE ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SL_SD21", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //수당종류2 LOOKUP
        public void GetSL_SD22Datas(DataSet ds)
        {
            try
            {
                string qry = " SELECT A.IFWGCODE SD_CODE, RTRIM(A.IFWGNAME) SD_NAME, "
                           + "        ISNULL(X1.CHK,'0') AS CHK "
                           + "   FROM " + wagedb + ".DBO.INFOWAGE A "
                           + "   LEFT OUTER JOIN DUTY_INFOSD05_SET X1 ON A.IFWGCODE=X1.SD_CODE "
                           + "  WHERE LEFT(A.IFWGCODE,1)='A' AND A.IFWGCODE>'A020'"
                           + "    AND A.IFWGCRGB = '1' AND A.IFWGJYGB IN ('12') "
                           + "  ORDER BY A.IFWGCODE ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SL_SD22", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //수당종류 LOOKUP
        public void GetSL_SD_LIST2Datas(DataSet ds)
        {
            try
            {
                string qry = " SELECT A.SD_CODE, RTRIM(X1.IFWGNAME) SD_NAME "
                           + "   FROM DUTY_INFOSD05_SET A"
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.INFOWAGE X1 "
                           + "     ON A.SD_CODE = X1.IFWGCODE "
                           + "  ORDER BY A.SD_CODE ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SL_SD_LIST2", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //만근수당설정 내역 불러오기
        public void GetDUTY_INFOSD05_SETDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT * "
                           + "   FROM DUTY_INFOSD05_SET WHERE 1=2 ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DUTY_INFOSD05_SET", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //만근수당 불러오기
        public void GetS_INFOSD05Datas(string sldt, string sd_code, DataSet ds)
        {
            try
            {
                string qry = " SELECT RTRIM(X2.DEPRNAM2) AS DEPT_NM, "
                           + "        RTRIM(A.EMBSSABN) AS SABN, RTRIM(A.EMBSNAME) AS SABN_NM, "
                           + "        ISNULL(X1.SD_BASE,0) AS SD_BASE, "
                           + "        ISNULL(X1.SD_ADD,0) AS SD_ADD, "
                           + "        A.* "
                           + "   FROM " + wagedb + ".dbo.MSTEMBS A "
                           + "   LEFT OUTER JOIN DUTY_INFOSD05 X1 "
                           + "     ON A.EMBSSABN=X1.SABN AND X1.SD_CODE ='" + sd_code + "'"
                           + "   LEFT OUTER JOIN " + wagedb + ".dbo.MSTDEPR X2 ON A.EMBSDPCD=X2.DEPRCODE "
                           + "  WHERE A.EMBSSTAT='1' OR (A.EMBSSTAT='2' AND A.EMBSTSDT>= '" + sldt + "')"
                           + "  ORDER BY A.EMBSDPCD, A.EMBSSABN ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("S_INFOSD05", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void GetDUTY_INFOSD05Datas(string sd_code, DataSet ds)
        {
            try
            {
                string qry = " SELECT * "
                           + "   FROM DUTY_INFOSD05 WHERE SD_CODE = '" + sd_code + "' ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DUTY_INFOSD05", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //만근수당 조회
        public void GetSEARCH_INFOSD05Datas(string stdt, DataSet ds)
        {
            try
            {
                string qry = " SELECT RTRIM(ISNULL(X1.EMBSNAME,'')) AS EMBSNAME, "
                           + "        RTRIM(ISNULL(X2.DEPRNAM2,'')) AS DEPT_NM, "
                           + "        RTRIM(ISNULL(X3.IFWGNAME,'')) AS SD_NAME, "
                           + "        A.* "
                           + "   FROM DUTY_INFOSD05 A "
                           + "  INNER JOIN " + wagedb + ".DBO.MSTEMBS X1 ON A.SABN=X1.EMBSSABN "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.MSTDEPR X2 ON X1.EMBSDPCD=X2.DEPRCODE "
                           + "   LEFT OUTER JOIN " + wagedb + ".DBO.INFOWAGE X3 ON A.SD_CODE=X3.IFWGCODE "
                           + "  WHERE A.SD_BASE <> 0 OR A.SD_ADD <> 0 "
                           //+ "  WHERE X1.EMBSSTAT='1' OR (X1.EMBSSTAT='2' AND X1.EMBSTSDT>= '" + stdt + "')"
                           + "  ORDER BY A.SD_CODE, X1.EMBSDPCD, A.SABN ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SEARCH_INFOSD05", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 9060 - 수당근태코드연결

        public void GetDUTY_INFOSD06Datas(DataSet ds)
        {
            try
            {
                string qry = " SELECT * "
                           + "   FROM DUTY_INFOSD06 ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("DUTY_INFOSD06", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //수당종류 LOOKUP
        public void GetSL_SDDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT IFWGCODE SD_CODE, RTRIM(IFWGNAME) SD_NAME "
                           + "   FROM " + wagedb + ".DBO.INFOWAGE "
                           + "  WHERE LEFT(IFWGCODE,1)='A' "
                           + "  ORDER BY IFWGCODE ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SL_SD", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //근태종류 LOOKUP
        public void GetSL_GTDatas(DataSet ds)
        {
            try
            {
                string qry = " SELECT IFWGCODE GT_CODE, RTRIM(IFWGNAME) GT_NAME "
                           + "   FROM " + wagedb + ".DBO.INFOWAGE "
                           + "  WHERE LEFT(IFWGCODE,1)='C' "
                           + "  ORDER BY IFWGCODE ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("SL_GT", dt, ref ds);
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region 환경설정-결재라인관리
        /// <summary>
        /// 결재라인관리
        /// </summary>
        public void GetSIGNPath(DataSet ds)
        {
            try
            {
                string qry = " SELECT * "
                           + "  FROM INFO_SIGN "
                           + " ORDER BY SQ";

                DataTable dt = gd.GetDataInQuery(clib.TextToInt(DataAccess.DBtype), dbname, qry);
                dp.AddDatatable2Dataset("INFO_SIGN", dt, ref ds);

                ds.Tables["INFO_SIGN"].Columns["SIGN_SIZE"].DefaultValue = 1;
            }
            catch (System.Exception ec)
            {
                System.Windows.Forms.MessageBox.Show("자료를 가져오는중 오류가 발생했습니다. : " + ec.Message,
                                                     "조회오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
		
    }
}

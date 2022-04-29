using System.Data;
using SilkRoad.DataProc;
using System.Data.Common;
using System.Data.SqlClient;

namespace SilkRoad.DbCmd_DT01
{
    public partial class DbCmd_DT01
    {
        SetData sd = new SetData();
        string dbname = SilkRoad.DAL.DataAccess.DBname;
        static string comm_db = "COMMDB" + SilkRoad.Config.SRConfig.WorkPlaceNo;
        static string wage_db = "WAGEDB" + SilkRoad.Config.SRConfig.WorkPlaceNo;

        /// <summary>
        /// db에 command로 저장
        /// </summary>
        /// <param name="ds"> 데이터셋 </param>
        /// <param name="tablename"> 저장할 테이블명들..command </param>
        /// <param name="qry"></param>
        public int setUpdate(ref DataSet ds, string[] tablenames, string[] qrys)
        {
            DbCommand[] insCmd = null;
            DbCommand[] uptCmd = null;
            DbCommand[] delCmd = null;

            if (tablenames != null)
            {
                insCmd = new DbCommand[tablenames.Length];
                uptCmd = new DbCommand[tablenames.Length];
                delCmd = new DbCommand[tablenames.Length];

                for (int i = 0; i < tablenames.Length; i++)
                {
                    #region

                    if (tablenames[i] == "DUTY_MSTUSER")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_MSTUSERInCmd();
                        uptCmd[i] = (DbCommand)GetDUTY_MSTUSERUpCmd();
                        delCmd[i] = (DbCommand)GetDUTY_MSTUSERDelCmd();
                    }
                    else if (tablenames[i] == "MSTUSER")
                    {
                        insCmd[i] = (DbCommand)GetMSTUSERInCmd();
                    }
                    else if (tablenames[i] == "MSTEMBS")
                    {
                        insCmd[i] = (DbCommand)GetMSTEMBSInCmd();
                        uptCmd[i] = (DbCommand)GetMSTEMBSUpCmd();
                    }
                    else if (tablenames[i] == "DUTY_MSTPART")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_MSTPARTInCmd();
                        uptCmd[i] = (DbCommand)GetDUTY_MSTPARTUpCmd();
                        delCmd[i] = (DbCommand)GetDUTY_MSTPARTDelCmd();
                    }
                    else if (tablenames[i] == "DUTY_MSTNURS")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_MSTNURSInCmd();
                        uptCmd[i] = (DbCommand)GetDUTY_MSTNURSUpCmd();
                        delCmd[i] = (DbCommand)GetDUTY_MSTNURSDelCmd();
                    }
                    else if (tablenames[i] == "D_DUTY_INFOJONG")
                    {
                        delCmd[i] = (DbCommand)GetD_DUTY_INFOJONGDelCmd();
                    }
                    else if (tablenames[i] == "DUTY_INFOJONG")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_INFOJONGInCmd();
                    }
					else if (tablenames[i] == "DUTY_TRSDEPT")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_TRSDEPTInCmd();
                    }
					else if (tablenames[i] == "DUTY_TRSPART")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_TRSPARTInCmd();
                    }
                    else if (tablenames[i] == "DUTY_MSTGNMU")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_MSTGNMUInCmd();
                        uptCmd[i] = (DbCommand)GetDUTY_MSTGNMUUpCmd();
                        delCmd[i] = (DbCommand)GetDUTY_MSTGNMUDelCmd();
                    }   
                    else if (tablenames[i] == "D_DUTY_MSTHOLI")
                    {
                        delCmd[i] = (DbCommand)GetD_DUTY_MSTHOLIDelCmd();
                    }
                    else if (tablenames[i] == "DUTY_MSTHOLI")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_MSTHOLIInCmd();
                    }
                    else if (tablenames[i] == "DUTY_INFOSD01")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_INFOSD01InCmd();
                        uptCmd[i] = (DbCommand)GetDUTY_INFOSD01UpCmd();
                        delCmd[i] = (DbCommand)GetDUTY_INFOSD01DelCmd();
                    }
                    else if (tablenames[i] == "DUTY_INFOSD02")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_INFOSD02InCmd();
                        uptCmd[i] = (DbCommand)GetDUTY_INFOSD02UpCmd();
                    }
                    else if (tablenames[i] == "DUTY_INFOSD03")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_INFOSD03InCmd();
                        uptCmd[i] = (DbCommand)GetDUTY_INFOSD03UpCmd();
                    }
                    else if (tablenames[i] == "DUTY_INFOSD04")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_INFOSD04InCmd();
                        uptCmd[i] = (DbCommand)GetDUTY_INFOSD04UpCmd();
                        delCmd[i] = (DbCommand)GetDUTY_INFOSD04DelCmd();
                    }
                    else if (tablenames[i] == "DUTY_INFOSD05")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_INFOSD05InCmd();
                        uptCmd[i] = (DbCommand)GetDUTY_INFOSD05UpCmd();
                        delCmd[i] = (DbCommand)GetDUTY_INFOSD05DelCmd();
                    }
                    else if (tablenames[i] == "DUTY_TRSOVTM")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_TRSOVTMInCmd();
                        uptCmd[i] = (DbCommand)GetDUTY_TRSOVTMUpCmd();
                        delCmd[i] = (DbCommand)GetDUTY_TRSOVTMDelCmd();
                    }
                    else if (tablenames[i] == "DEL_TRSOVTM")
                    {
                        insCmd[i] = (DbCommand)GetDEL_TRSOVTMInCmd();
                    }
                    else if (tablenames[i] == "DUTY_TRSCALL")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_TRSCALLInCmd();
                        uptCmd[i] = (DbCommand)GetDUTY_TRSCALLUpCmd();
                        delCmd[i] = (DbCommand)GetDUTY_TRSCALLDelCmd();
                    }
                    else if (tablenames[i] == "D_DUTY_INFOCALL")
                    {
                        delCmd[i] = (DbCommand)GetD_DUTY_INFOCALLDelCmd();
                    }
                    else if (tablenames[i] == "DUTY_INFOCALL")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_INFOCALLInCmd();
                    }
                    else if (tablenames[i] == "DUTY_TRSDANG")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_TRSDANGInCmd();
                        uptCmd[i] = (DbCommand)GetDUTY_TRSDANGUpCmd();
                        delCmd[i] = (DbCommand)GetDUTY_TRSDANGDelCmd();
                    }
                    else if (tablenames[i] == "D_DUTY_INFODANG")
                    {
                        delCmd[i] = (DbCommand)GetD_DUTY_INFODANGDelCmd();
                    }
                    else if (tablenames[i] == "DUTY_INFODANG")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_INFODANGInCmd();
                    }
                    else if (tablenames[i] == "D_DUTY_INFOFXOT")
                    {
                        delCmd[i] = (DbCommand)GetD_DUTY_INFOFXOTDelCmd();
                    }
                    else if (tablenames[i] == "DUTY_INFOFXOT")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_INFOFXOTInCmd();
                    }
                    else if (tablenames[i] == "DUTY_TRSPLAN")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_TRSPLANInCmd();
                        uptCmd[i] = (DbCommand)GetDUTY_TRSPLANUpCmd();
                        delCmd[i] = (DbCommand)GetDUTY_TRSPLANDelCmd();
                    }
                    else if (tablenames[i] == "D_DUTY_INFONURS")
                    {
                        delCmd[i] = (DbCommand)GetD_DUTY_INFONURSDelCmd();
                    }
                    else if (tablenames[i] == "DUTY_INFONURS")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_INFONURSInCmd();
                    }
                    else if (tablenames[i] == "DUTY_MSTCLOS")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_MSTCLOSInCmd();
                        uptCmd[i] = (DbCommand)GetDUTY_MSTCLOSUpCmd();
                        delCmd[i] = (DbCommand)GetDUTY_MSTCLOSDelCmd();
                    }
                    else if (tablenames[i] == "DUTY_TRSYCMI")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_TRSYCMIInCmd();
                        uptCmd[i] = (DbCommand)GetDUTY_TRSYCMIUpCmd();
                        delCmd[i] = (DbCommand)GetDUTY_TRSYCMIDelCmd();
                    }
                    else if (tablenames[i] == "DUTY_MSTENDS")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_MSTENDSInCmd();
                        uptCmd[i] = (DbCommand)GetDUTY_MSTENDSUpCmd();
                        delCmd[i] = (DbCommand)GetDUTY_MSTENDSDelCmd();
                    }
                    else if (tablenames[i] == "DUTY_MSTENDS_LOG")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_MSTENDS_LOGInCmd();
                    }
                    else if (tablenames[i] == "DUTY_MSTWGPC")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_MSTWGPCInCmd();
                        uptCmd[i] = (DbCommand)GetDUTY_MSTWGPCUpCmd();
                        delCmd[i] = (DbCommand)GetDUTY_MSTWGPCDelCmd();
                    }
                    else if (tablenames[i] == "MSTWGPC")
                    {
                        insCmd[i] = (DbCommand)GetMSTWGPCInCmd();
                        uptCmd[i] = (DbCommand)GetMSTWGPCUpCmd();
                        delCmd[i] = (DbCommand)GetMSTWGPCDelCmd();
                    }
                    else if (tablenames[i] == "MSTGTMM")
                    {
                        insCmd[i] = (DbCommand)GetMSTGTMMInCmd();
                        uptCmd[i] = (DbCommand)GetMSTGTMMUpCmd();
                        delCmd[i] = (DbCommand)GetMSTGTMMDelCmd();
                    }
                    else if (tablenames[i] == "DUTY_TRSNOTI")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_TRSNOTIInCmd();
                        uptCmd[i] = (DbCommand)GetDUTY_TRSNOTIUpCmd();
                        delCmd[i] = (DbCommand)GetDUTY_TRSNOTIDelCmd();
                    }
                    else if (tablenames[i] == "DUTY_TRSDYYC")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_TRSDYYCInCmd();
                        uptCmd[i] = (DbCommand)GetDUTY_TRSDYYCUpCmd();
                        delCmd[i] = (DbCommand)GetDUTY_TRSDYYCDelCmd();
                    }
                    else if (tablenames[i] == "DUTY_TRSHREQ")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_TRSHREQInCmd();
                        uptCmd[i] = (DbCommand)GetDUTY_TRSHREQUpCmd();
                        delCmd[i] = (DbCommand)GetDUTY_TRSHREQDelCmd();
                    }
                    else if (tablenames[i] == "DEL_TRSHREQ")
                    {
                        insCmd[i] = (DbCommand)GetDEL_TRSHREQInCmd();
                    }
                    else if (tablenames[i] == "DUTY_TRSJREQ")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_TRSJREQInCmd();
                        uptCmd[i] = (DbCommand)GetDUTY_TRSJREQUpCmd();
                        delCmd[i] = (DbCommand)GetDUTY_TRSJREQDelCmd();
                    }
                    else if (tablenames[i] == "DEL_TRSJREQ")
                    {
                        insCmd[i] = (DbCommand)GetDEL_TRSJREQInCmd();
                    }
                    else if (tablenames[i] == "DUTY_MSTYCCJ")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_MSTYCCJInCmd();
                    }
                    else if (tablenames[i] == "DEL_DUTY_TRSHREQ")
                    {
                        delCmd[i] = (DbCommand)GetDEL_DUTY_TRSHREQDelCmd();
                    }
                    else if (tablenames[i] == "DUTY_MSTGTMM")
                    {
                        insCmd[i] = (DbCommand)GetDUTY_MSTGTMMInCmd();
                        uptCmd[i] = (DbCommand)GetDUTY_MSTGTMMUpCmd();
                        delCmd[i] = (DbCommand)GetDUTY_MSTGTMMDelCmd();
                    }
                    

                    #endregion
                }
            }
            int res = sd.NewUpdateTableByCommand(ds, tablenames, insCmd, uptCmd, delCmd, qrys);
            return res;
        }

		#region command
			

		#region DUTY_MSTUSER insert, update, delete command

        private SqlCommand GetDUTY_MSTUSERInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_MSTUSER(USERIDEN, USERNAME, USERDPCD, USERUPYN, USERMSYN, USERINDT, USERUPDT, USERUSID, USERPSTY"
                                   + ") VALUES (@USERIDEN, @USERNAME, @USERDPCD, @USERUPYN, @USERMSYN, @USERINDT, @USERUPDT, @USERUSID, @USERPSTY"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@USERIDEN", SqlDbType.VarChar, 20, "USERIDEN");
            ocm.Parameters.Add("@USERNAME", SqlDbType.VarChar, 20, "USERNAME");
            ocm.Parameters.Add("@USERDPCD", SqlDbType.Char, 4, "USERDPCD");
            ocm.Parameters.Add("@USERUPYN", SqlDbType.Char, 1, "USERUPYN");
            ocm.Parameters.Add("@USERMSYN", SqlDbType.Char, 1, "USERMSYN");
            ocm.Parameters.Add("@USERINDT", SqlDbType.VarChar, 20, "USERINDT");
            ocm.Parameters.Add("@USERUPDT", SqlDbType.VarChar, 20, "USERUPDT");
            ocm.Parameters.Add("@USERUSID", SqlDbType.VarChar, 20, "USERUSID");
            ocm.Parameters.Add("@USERPSTY", SqlDbType.Char, 1, "USERPSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_MSTUSERUpCmd()
        {
            string queryStatements = "UPDATE DBO.DUTY_MSTUSER SET "
                                   + "   USERNAME = @USERNAME, "
                                   + "   USERDPCD = @USERDPCD, "
                                   + "   USERUPYN = @USERUPYN, "
                                   + "   USERMSYN = @USERMSYN, "
                                   + "   USERINDT = @USERINDT, "
                                   + "   USERUPDT = @USERUPDT, "
                                   + "   USERUSID = @USERUSID, "
                                   + "   USERPSTY = @USERPSTY "
                                   + " WHERE USERIDEN = @USERIDEN"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@USERIDEN", SqlDbType.VarChar, 20, "USERIDEN");
            ocm.Parameters.Add("@USERNAME", SqlDbType.VarChar, 20, "USERNAME");
            ocm.Parameters.Add("@USERDPCD", SqlDbType.Char, 4, "USERDPCD");
            ocm.Parameters.Add("@USERUPYN", SqlDbType.Char, 1, "USERUPYN");
            ocm.Parameters.Add("@USERMSYN", SqlDbType.Char, 1, "USERMSYN");
            ocm.Parameters.Add("@USERINDT", SqlDbType.VarChar, 20, "USERINDT");
            ocm.Parameters.Add("@USERUPDT", SqlDbType.VarChar, 20, "USERUPDT");
            ocm.Parameters.Add("@USERUSID", SqlDbType.VarChar, 20, "USERUSID");
            ocm.Parameters.Add("@USERPSTY", SqlDbType.Char, 1, "USERPSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_MSTUSERDelCmd()
        {
            string queryStatements = "DELETE DBO.DUTY_MSTUSER"
                                   + " WHERE USERIDEN = @USERIDEN"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;
			
            ocm.Parameters.Add("@USERIDEN", SqlDbType.VarChar, 20, "USERIDEN").SourceVersion = DataRowVersion.Original;
            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion

        #region MSTUSER insert command

        private SqlCommand GetMSTUSERInCmd()
        {
            string queryStatements = "USE SILKDBCM "
                                   + ""
							       + "INSERT INTO DBO.MSTUSER(USERIDEN, USERNAME, USERPSWD, USERUPYN, USERMSYN, USERRDAT, USERINDT, USERUPDT, USERUSID, USERPSTY"
                                   + ") VALUES (@USERIDEN, @USERNAME, @USERPSWD, @USERUPYN, @USERMSYN, @USERRDAT, @USERINDT, @USERUPDT, @USERUSID, @USERPSTY"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;
			
            ocm.Parameters.Add("@USERIDEN", SqlDbType.Char, 20, "USERIDEN");
            ocm.Parameters.Add("@USERNAME", SqlDbType.Char, 20, "USERNAME");
            ocm.Parameters.Add("@USERPSWD", SqlDbType.Char, 20, "USERPSWD");
            ocm.Parameters.Add("@USERUPYN", SqlDbType.Char, 1, "USERUPYN");
            ocm.Parameters.Add("@USERMSYN", SqlDbType.Char, 1, "USERMSYN");
            ocm.Parameters.Add("@USERRDAT", SqlDbType.Char, 8, "USERRDAT");
            ocm.Parameters.Add("@USERINDT", SqlDbType.Char, 8, "USERINDT");
            ocm.Parameters.Add("@USERUPDT", SqlDbType.Char, 8, "USERUPDT");
            ocm.Parameters.Add("@USERUSID", SqlDbType.Char, 20, "USERUSID");
            ocm.Parameters.Add("@USERPSTY", SqlDbType.Char, 1, "USERPSTY");

            return ocm;
        }

        #endregion

        #region MSTEMBS insert, update,delete command

        private SqlCommand GetMSTEMBSInCmd()
        {
            string queryStatements = "USE " + wage_db + " "
                                   + ""
							       + "INSERT INTO DBO.MSTEMBS(EMBSSABN, EMBSNAME, EMBSCNAM, EMBSENAM, EMBSJMNO, EMBSTLNO, EMBSHPNO, EMBSADR1, EMBSADR2, EMBSPOST, EMBSSTAT, EMBSIPDT, EMBSGRDT, EMBSTSDT, EMBSGLCD, EMBSDPCD, EMBSSTCD, EMBSJOCD, EMBSPSCD, EMBSJDCD, EMBSGRCD, EMBSHOBO, EMBSSHDT, EMBSDF01, EMBSDF02, EMBSDF03, EMBSDF04, EMBSDF05, EMBSFRD1, EMBSTOD1, EMBSFRD2, EMBSTOD2, EMBSFRD3, EMBSTOD3, EMBSGSYN, EMBSPICT, EMBSIDEN, EMBSPSWD, EMBSEMAL, EMBSDTGB, EMBSGSDT, EMBSGMCD, EMBSREMK, PHOTO, YC_TYPE, EMBSADGB, EMBSPTSA, EMBSPTLN, EMBSPHPN, EMBSPAD1, EMBSPAD2, EMBSSIG1, EMBSSIG2, EMBSSIG3, EMBSSIG4, EMBSSIG5, EMBSINDT, EMBSUPDT, EMBSUSID, EMBSPSTY"
                                   + ") VALUES (@EMBSSABN, @EMBSNAME, '', '', '', '', '', '', '', @EMBSPOST, @EMBSSTAT, @EMBSIPDT, '', @EMBSTSDT, @EMBSGLCD, @EMBSDPCD, '', @EMBSJOCD, '', '', @EMBSGRCD, @EMBSHOBO, '', '','','','','', '','','','','','', '','', @EMBSIDEN, @EMBSPSWD, @EMBSEMAL, 0, '','', @EMBSREMK, @PHOTO, @YC_TYPE, @EMBSADGB,"
								   + " ENCRYPTBYPASSPHRASE('samilpas',CAST(LTRIM(RTRIM(@EMBSJMNO)) AS VARCHAR(20))), ENCRYPTBYPASSPHRASE('samilpas',CAST(LTRIM(RTRIM(@EMBSTLNO)) AS VARCHAR(20))), ENCRYPTBYPASSPHRASE('samilpas',CAST(LTRIM(RTRIM(@EMBSHPNO)) AS VARCHAR(20))), ENCRYPTBYPASSPHRASE('samilpas',CAST(LTRIM(RTRIM(@EMBSADR1)) AS VARCHAR(100))), ENCRYPTBYPASSPHRASE('samilpas',CAST(LTRIM(RTRIM(@EMBSADR2)) AS VARCHAR(100))), '','','','','', @EMBSINDT, @EMBSUPDT, @EMBSUSID, @EMBSPSTY"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;
			
            ocm.Parameters.Add("@EMBSSABN", SqlDbType.VarChar, 15, "EMBSSABN");
            ocm.Parameters.Add("@EMBSNAME", SqlDbType.VarChar, 20, "EMBSNAME");
            ocm.Parameters.Add("@EMBSJMNO", SqlDbType.Char, 13, "EMBSJMNO");
            ocm.Parameters.Add("@EMBSTLNO", SqlDbType.VarChar, 20, "EMBSTLNO");
            ocm.Parameters.Add("@EMBSHPNO", SqlDbType.VarChar, 20, "EMBSHPNO");
            ocm.Parameters.Add("@EMBSADR1", SqlDbType.VarChar, 100, "EMBSADR1");
            ocm.Parameters.Add("@EMBSADR2", SqlDbType.VarChar, 100, "EMBSADR2");
            ocm.Parameters.Add("@EMBSPOST", SqlDbType.VarChar, 6, "EMBSPOST");
            ocm.Parameters.Add("@EMBSSTAT", SqlDbType.Decimal, 1, "EMBSSTAT");
            ocm.Parameters.Add("@EMBSIPDT", SqlDbType.Char, 8, "EMBSIPDT");
            ocm.Parameters.Add("@EMBSTSDT", SqlDbType.Char, 8, "EMBSTSDT");
            ocm.Parameters.Add("@EMBSGLCD", SqlDbType.Char, 4, "EMBSGLCD");
            ocm.Parameters.Add("@EMBSDPCD", SqlDbType.Char, 4, "EMBSDPCD");
            ocm.Parameters.Add("@EMBSJOCD", SqlDbType.Char, 4, "EMBSJOCD");
            ocm.Parameters.Add("@EMBSGRCD", SqlDbType.Char, 4, "EMBSGRCD");
            ocm.Parameters.Add("@EMBSHOBO", SqlDbType.Char, 3, "EMBSHOBO");
            ocm.Parameters.Add("@EMBSIDEN", SqlDbType.VarChar, 15, "EMBSIDEN");
            ocm.Parameters.Add("@EMBSPSWD", SqlDbType.VarChar, 10, "EMBSPSWD");
            ocm.Parameters.Add("@EMBSEMAL", SqlDbType.VarChar, 40, "EMBSEMAL");
            ocm.Parameters.Add("@EMBSREMK", SqlDbType.VarChar, 400, "EMBSREMK");
            ocm.Parameters.Add("@PHOTO", SqlDbType.VarBinary, -1, "PHOTO");
            ocm.Parameters.Add("@YC_TYPE", SqlDbType.Int, 1, "YC_TYPE");
            ocm.Parameters.Add("@EMBSADGB", SqlDbType.Char, 1, "EMBSADGB");
            ocm.Parameters.Add("@EMBSINDT", SqlDbType.VarChar, 20, "EMBSINDT");
            ocm.Parameters.Add("@EMBSUPDT", SqlDbType.VarChar, 20, "EMBSUPDT");
            ocm.Parameters.Add("@EMBSUSID", SqlDbType.VarChar, 20, "EMBSUSID");
            ocm.Parameters.Add("@EMBSPSTY", SqlDbType.Char, 1, "EMBSPSTY");

            return ocm;
        }

        private SqlCommand GetMSTEMBSUpCmd()

        {
            string queryStatements = "USE " + wage_db + " "
                                   + ""
							       + "UPDATE DBO.MSTEMBS SET "
                                   + "   EMBSNAME = @EMBSNAME, "
                                   + "   EMBSPTSA = ENCRYPTBYPASSPHRASE('samilpas', @EMBSJMNO), "
                                   + "   EMBSPTLN = ENCRYPTBYPASSPHRASE('samilpas', @EMBSTLNO), "
                                   + "   EMBSPHPN = ENCRYPTBYPASSPHRASE('samilpas', @EMBSHPNO), "
                                   + "   EMBSPAD1 = ENCRYPTBYPASSPHRASE('samilpas', @EMBSADR1), "
                                   + "   EMBSPAD2 = ENCRYPTBYPASSPHRASE('samilpas', @EMBSADR2), "
                                   + "   EMBSPOST = @EMBSPOST, "
                                   + "   EMBSSTAT = @EMBSSTAT, "
                                   + "   EMBSIPDT = @EMBSIPDT, "
                                   + "   EMBSTSDT = @EMBSTSDT, "
                                   + "   EMBSGLCD = @EMBSGLCD, "
                                   + "   EMBSDPCD = @EMBSDPCD, "
                                   + "   EMBSJOCD = @EMBSJOCD, "
                                   + "   EMBSGRCD = @EMBSGRCD, "
                                   + "   EMBSHOBO = @EMBSHOBO, "
                                   + "   EMBSIDEN = @EMBSIDEN, "
                                   + "   EMBSPSWD = @EMBSPSWD, "
                                   + "   EMBSEMAL = @EMBSEMAL, "
                                   + "   EMBSREMK = @EMBSREMK, "
                                   + "   PHOTO = @PHOTO, "
                                   + "   YC_TYPE = @YC_TYPE, "
                                   + "   EMBSADGB = @EMBSADGB, "
                                   + "   EMBSINDT = @EMBSINDT, "
                                   + "   EMBSUPDT = @EMBSUPDT, "
                                   + "   EMBSUSID = @EMBSUSID, "
                                   + "   EMBSPSTY = @EMBSPSTY"
                                   + " WHERE EMBSSABN = @EMBSSABN"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@EMBSSABN", SqlDbType.VarChar, 15, "EMBSSABN");
            ocm.Parameters.Add("@EMBSNAME", SqlDbType.VarChar, 20, "EMBSNAME");
            ocm.Parameters.Add("@EMBSJMNO", SqlDbType.Char, 13, "EMBSJMNO");
            ocm.Parameters.Add("@EMBSTLNO", SqlDbType.VarChar, 20, "EMBSTLNO");
            ocm.Parameters.Add("@EMBSHPNO", SqlDbType.VarChar, 20, "EMBSHPNO");
            ocm.Parameters.Add("@EMBSADR1", SqlDbType.VarChar, 100, "EMBSADR1");
            ocm.Parameters.Add("@EMBSADR2", SqlDbType.VarChar, 100, "EMBSADR2");
            ocm.Parameters.Add("@EMBSPOST", SqlDbType.VarChar, 6, "EMBSPOST");
            ocm.Parameters.Add("@EMBSSTAT", SqlDbType.Decimal, 1, "EMBSSTAT");
            ocm.Parameters.Add("@EMBSIPDT", SqlDbType.Char, 8, "EMBSIPDT");
            ocm.Parameters.Add("@EMBSTSDT", SqlDbType.Char, 8, "EMBSTSDT");
            ocm.Parameters.Add("@EMBSGLCD", SqlDbType.Char, 4, "EMBSGLCD");
            ocm.Parameters.Add("@EMBSDPCD", SqlDbType.Char, 4, "EMBSDPCD");
            ocm.Parameters.Add("@EMBSJOCD", SqlDbType.Char, 4, "EMBSJOCD");
            ocm.Parameters.Add("@EMBSGRCD", SqlDbType.Char, 4, "EMBSGRCD");
            ocm.Parameters.Add("@EMBSHOBO", SqlDbType.Char, 3, "EMBSHOBO");
            ocm.Parameters.Add("@EMBSIDEN", SqlDbType.VarChar, 15, "EMBSIDEN");
            ocm.Parameters.Add("@EMBSPSWD", SqlDbType.VarChar, 10, "EMBSPSWD");
            ocm.Parameters.Add("@EMBSEMAL", SqlDbType.VarChar, 40, "EMBSEMAL");
            ocm.Parameters.Add("@EMBSREMK", SqlDbType.VarChar, 400, "EMBSREMK");
            ocm.Parameters.Add("@PHOTO", SqlDbType.VarBinary, -1, "PHOTO");
            ocm.Parameters.Add("@YC_TYPE", SqlDbType.Int, 1, "YC_TYPE");
            ocm.Parameters.Add("@EMBSADGB", SqlDbType.Char, 1, "EMBSADGB");
            ocm.Parameters.Add("@EMBSINDT", SqlDbType.VarChar, 20, "EMBSINDT");
            ocm.Parameters.Add("@EMBSUPDT", SqlDbType.VarChar, 20, "EMBSUPDT");
            ocm.Parameters.Add("@EMBSUSID", SqlDbType.VarChar, 20, "EMBSUSID");
            ocm.Parameters.Add("@EMBSPSTY", SqlDbType.Char, 1, "EMBSPSTY");

            return ocm;
        }

        private SqlCommand GetMSTEMBSDelCmd()

        {
            string queryStatements = "DELETE DBO.MSTEMBS"
                                   + " WHERE EMBSSABN = @EMBSSABN"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@EMBSSABN", SqlDbType.VarChar, 15, "EMBSSABN").SourceVersion = DataRowVersion.Original;
            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion
		
		#region DUTY_MSTPART insert, update,delete command

        private SqlCommand GetDUTY_MSTPARTInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_MSTPART(PARTCODE, DEPRCODE, PARTNAME, STAT, LDAY, INDT, UPDT, USID, PSTY"
                                   + ") VALUES (@PARTCODE, @DEPRCODE, @PARTNAME, @STAT, @LDAY, @INDT, @UPDT, @USID, @PSTY"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@PARTCODE", SqlDbType.Char, 4, "PARTCODE");
            ocm.Parameters.Add("@DEPRCODE", SqlDbType.Char, 4, "DEPRCODE");
            ocm.Parameters.Add("@PARTNAME", SqlDbType.VarChar, 80, "PARTNAME");
            ocm.Parameters.Add("@STAT", SqlDbType.Int, 4, "STAT");
            ocm.Parameters.Add("@LDAY", SqlDbType.Char, 8, "LDAY");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_MSTPARTUpCmd()

        {
            string queryStatements = "UPDATE DBO.DUTY_MSTPART SET "
                                   + "   DEPRCODE = @DEPRCODE, "
                                   + "   PARTNAME = @PARTNAME, "
                                   + "   STAT = @STAT, "
                                   + "   LDAY = @LDAY, "
                                   + "   INDT = @INDT, "
                                   + "   UPDT = @UPDT, "
                                   + "   USID = @USID, "
                                   + "   PSTY = @PSTY"
                                   + " WHERE PARTCODE = @PARTCODE"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@PARTCODE", SqlDbType.Char, 4, "PARTCODE");
            ocm.Parameters.Add("@DEPRCODE", SqlDbType.Char, 4, "DEPRCODE");
            ocm.Parameters.Add("@PARTNAME", SqlDbType.VarChar, 80, "PARTNAME");
            ocm.Parameters.Add("@STAT", SqlDbType.Int, 4, "STAT");
            ocm.Parameters.Add("@LDAY", SqlDbType.Char, 8, "LDAY");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_MSTPARTDelCmd()

        {
            string queryStatements = "DELETE DBO.DUTY_MSTPART"
                                   + " WHERE PARTCODE = @PARTCODE"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@PARTCODE", SqlDbType.Char, 4, "PARTCODE").SourceVersion = DataRowVersion.Original;
            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion
		
		#region DUTY_MSTNURS insert, update,delete command

        private SqlCommand GetDUTY_MSTNURSInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_MSTNURS(SAWON_NO, SAWON_NM, PARTCODE, EXP_LV, PRE_RN, RSP_YN, RSP_GNMU, SHIFT_WORK, TM_YN, TM_FR, TM_TO, FIRST_GNMU, MAX_NCNT, MAX_CCNT, ALLOWOFF, LIMIT_OFF, RETURN_DT, CHARGE_YN, STAT, LDAY, INDT, UPDT, USID, PSTY"
                                   + ") VALUES (@SAWON_NO, @SAWON_NM, @PARTCODE, @EXP_LV, @PRE_RN, @RSP_YN, @RSP_GNMU, @SHIFT_WORK, @TM_YN, @TM_FR, @TM_TO, @FIRST_GNMU, @MAX_NCNT, @MAX_CCNT, @ALLOWOFF, @LIMIT_OFF, @RETURN_DT, @CHARGE_YN, @STAT, @LDAY, @INDT, @UPDT, @USID, @PSTY"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@SAWON_NO", SqlDbType.VarChar, 15, "SAWON_NO");
            ocm.Parameters.Add("@SAWON_NM", SqlDbType.VarChar, 40, "SAWON_NM");
            ocm.Parameters.Add("@PARTCODE", SqlDbType.Char, 4, "PARTCODE");
            ocm.Parameters.Add("@EXP_LV", SqlDbType.Int, 4, "EXP_LV");
            ocm.Parameters.Add("@PRE_RN", SqlDbType.VarChar, 15, "PRE_RN");
            ocm.Parameters.Add("@RSP_YN", SqlDbType.Char, 1, "RSP_YN");
            ocm.Parameters.Add("@RSP_GNMU", SqlDbType.Char, 2, "RSP_GNMU");
            ocm.Parameters.Add("@SHIFT_WORK", SqlDbType.Int, 4, "SHIFT_WORK");
            ocm.Parameters.Add("@TM_YN", SqlDbType.Char, 1, "TM_YN");
            ocm.Parameters.Add("@TM_FR", SqlDbType.Char, 4, "TM_FR");
            ocm.Parameters.Add("@TM_TO", SqlDbType.Char, 4, "TM_TO");
            ocm.Parameters.Add("@FIRST_GNMU", SqlDbType.Char, 1, "FIRST_GNMU");
            ocm.Parameters.Add("@MAX_NCNT", SqlDbType.Int, 4, "MAX_NCNT");
            ocm.Parameters.Add("@MAX_CCNT", SqlDbType.Int, 4, "MAX_CCNT");
            ocm.Parameters.Add("@ALLOWOFF", SqlDbType.Int, 4, "ALLOWOFF");
            ocm.Parameters.Add("@LIMIT_OFF", SqlDbType.Int, 4, "LIMIT_OFF");
            ocm.Parameters.Add("@RETURN_DT", SqlDbType.Char, 8, "RETURN_DT");
            ocm.Parameters.Add("@CHARGE_YN", SqlDbType.Char, 1, "CHARGE_YN");
            ocm.Parameters.Add("@STAT", SqlDbType.Int, 1, "STAT");
            ocm.Parameters.Add("@LDAY", SqlDbType.Char, 8, "LDAY");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_MSTNURSUpCmd()

        {
            string queryStatements = "UPDATE DBO.DUTY_MSTNURS SET "
                                   + "   SAWON_NM = @SAWON_NM, "
                                   + "   PARTCODE = @PARTCODE, "
                                   + "   EXP_LV = @EXP_LV, "
                                   + "   PRE_RN = @PRE_RN, "
                                   + "   RSP_YN = @RSP_YN, "
                                   + "   RSP_GNMU = @RSP_GNMU, "
                                   + "   SHIFT_WORK = @SHIFT_WORK, "
                                   + "   TM_YN = @TM_YN, "
                                   + "   TM_FR = @TM_FR, "
                                   + "   TM_TO = @TM_TO, "
                                   + "   FIRST_GNMU = @FIRST_GNMU, "
                                   + "   MAX_NCNT = @MAX_NCNT, "
                                   + "   MAX_CCNT = @MAX_CCNT, "
                                   + "   ALLOWOFF = @ALLOWOFF, "
                                   + "   LIMIT_OFF = @LIMIT_OFF, "
                                   + "   RETURN_DT = @RETURN_DT, "
                                   + "   CHARGE_YN = @CHARGE_YN, "
                                   + "   STAT = @STAT, "
                                   + "   LDAY = @LDAY, "
                                   + "   INDT = @INDT, "
                                   + "   UPDT = @UPDT, "
                                   + "   USID = @USID, "
                                   + "   PSTY = @PSTY"
                                   + " WHERE SAWON_NO = @SAWON_NO"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@SAWON_NO", SqlDbType.VarChar, 15, "SAWON_NO");
            ocm.Parameters.Add("@SAWON_NM", SqlDbType.VarChar, 40, "SAWON_NM");
            ocm.Parameters.Add("@PARTCODE", SqlDbType.Char, 4, "PARTCODE");
            ocm.Parameters.Add("@EXP_LV", SqlDbType.Int, 4, "EXP_LV");
            ocm.Parameters.Add("@PRE_RN", SqlDbType.VarChar, 15, "PRE_RN");
            ocm.Parameters.Add("@RSP_YN", SqlDbType.Char, 1, "RSP_YN");
            ocm.Parameters.Add("@RSP_GNMU", SqlDbType.Char, 2, "RSP_GNMU");
            ocm.Parameters.Add("@SHIFT_WORK", SqlDbType.Int, 4, "SHIFT_WORK");
            ocm.Parameters.Add("@TM_YN", SqlDbType.Char, 1, "TM_YN");
            ocm.Parameters.Add("@TM_FR", SqlDbType.Char, 4, "TM_FR");
            ocm.Parameters.Add("@TM_TO", SqlDbType.Char, 4, "TM_TO");
            ocm.Parameters.Add("@FIRST_GNMU", SqlDbType.Char, 1, "FIRST_GNMU");
            ocm.Parameters.Add("@MAX_NCNT", SqlDbType.Int, 4, "MAX_NCNT");
            ocm.Parameters.Add("@MAX_CCNT", SqlDbType.Int, 4, "MAX_CCNT");
            ocm.Parameters.Add("@ALLOWOFF", SqlDbType.Int, 4, "ALLOWOFF");
            ocm.Parameters.Add("@LIMIT_OFF", SqlDbType.Int, 4, "LIMIT_OFF");
            ocm.Parameters.Add("@RETURN_DT", SqlDbType.Char, 8, "RETURN_DT");
            ocm.Parameters.Add("@CHARGE_YN", SqlDbType.Char, 1, "CHARGE_YN");
            ocm.Parameters.Add("@STAT", SqlDbType.Int, 1, "STAT");
            ocm.Parameters.Add("@LDAY", SqlDbType.Char, 8, "LDAY");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_MSTNURSDelCmd()

        {
            string queryStatements = "DELETE DBO.DUTY_MSTNURS"
                                   + " WHERE SAWON_NO = @SAWON_NO"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;
			
            ocm.Parameters.Add("@SAWON_NO", SqlDbType.VarChar, 15, "SAWON_NO").SourceVersion = DataRowVersion.Original;
            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion

		#region D_DUTY_INFOJONG delete command

        private SqlCommand GetD_DUTY_INFOJONGDelCmd()

        {
            string queryStatements = "DELETE DBO.DUTY_INFOJONG"
                                   + " WHERE JONGCODE = @JONGCODE"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;
			
            ocm.Parameters.Add("@JONGCODE", SqlDbType.Char, 4, "JONGCODE").SourceVersion = DataRowVersion.Original;
            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion

		#region DUTY_INFOJONG insert command

        private SqlCommand GetDUTY_INFOJONGInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_INFOJONG(JONGCODE, INDT, UPDT, USID, PSTY"
                                   + ") VALUES (@JONGCODE, @INDT, @UPDT, @USID, @PSTY"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@JONGCODE", SqlDbType.Char, 4, "JONGCODE");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }		

        #endregion

		#region DUTY_TRSDEPT insert command

        private SqlCommand GetDUTY_TRSDEPTInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_TRSDEPT(SAWON_NO, FR_DEPT, TO_DEPT, REG_DT, REG_ID"
                                   + ") VALUES (@SAWON_NO, @FR_DEPT, @TO_DEPT, @REG_DT, @REG_ID"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;
			
            ocm.Parameters.Add("@SAWON_NO", SqlDbType.VarChar, 15, "SAWON_NO");
            ocm.Parameters.Add("@FR_DEPT", SqlDbType.Char, 4, "FR_DEPT");
            ocm.Parameters.Add("@TO_DEPT", SqlDbType.Char, 4, "TO_DEPT");
            ocm.Parameters.Add("@REG_DT", SqlDbType.VarChar, 20, "REG_DT");
            ocm.Parameters.Add("@REG_ID", SqlDbType.VarChar, 20, "REG_ID");

            return ocm;
        }

        #endregion
				
		#region DUTY_TRSPART insert command

        private SqlCommand GetDUTY_TRSPARTInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_TRSPART(SAWON_NO, FR_PART, TO_PART, REG_DT, REG_ID"
                                   + ") VALUES (@SAWON_NO, @FR_PART, @TO_PART, @REG_DT, @REG_ID"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;
			
            ocm.Parameters.Add("@SAWON_NO", SqlDbType.VarChar, 15, "SAWON_NO");
            ocm.Parameters.Add("@FR_PART", SqlDbType.Char, 4, "FR_PART");
            ocm.Parameters.Add("@TO_PART", SqlDbType.Char, 4, "TO_PART");
            ocm.Parameters.Add("@REG_DT", SqlDbType.VarChar, 20, "REG_DT");
            ocm.Parameters.Add("@REG_ID", SqlDbType.VarChar, 20, "REG_ID");

            return ocm;
        }

        #endregion

		#region DUTY_MSTGNMU insert, update,delete command

        private SqlCommand GetDUTY_MSTGNMUInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_MSTGNMU(G_CODE, G_FNM, G_SNM, G_FRTM, G_TOTM, G_WORK, G_TYPE, G_COLOR, G_RGB, G_HEXA, AUTO_YN, REQ_YN, DANG_YN, YC_DAY, INDT, UPDT, USID, PSTY"
                                   + ") VALUES (@G_CODE, @G_FNM, @G_SNM, @G_FRTM, @G_TOTM, @G_WORK, @G_TYPE, @G_COLOR, @G_RGB, @G_HEXA, @AUTO_YN, @REQ_YN, @DANG_YN, @YC_DAY, @INDT, @UPDT, @USID, @PSTY"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@G_CODE", SqlDbType.Char, 2, "G_CODE");
            ocm.Parameters.Add("@G_FNM", SqlDbType.VarChar, 40, "G_FNM");
            ocm.Parameters.Add("@G_SNM", SqlDbType.VarChar, 8, "G_SNM");
            ocm.Parameters.Add("@G_FRTM", SqlDbType.Char, 4, "G_FRTM");
            ocm.Parameters.Add("@G_TOTM", SqlDbType.Char, 4, "G_TOTM");
            ocm.Parameters.Add("@G_WORK", SqlDbType.Decimal, 5, "G_WORK");
            ocm.Parameters.Add("@G_TYPE", SqlDbType.Int, 5, "G_TYPE");
            ocm.Parameters.Add("@G_COLOR", SqlDbType.Int, 20, "G_COLOR");
            ocm.Parameters.Add("@G_RGB", SqlDbType.VarChar, 20, "G_RGB");
            ocm.Parameters.Add("@G_HEXA", SqlDbType.VarChar, 20, "G_HEXA");
            ocm.Parameters.Add("@AUTO_YN", SqlDbType.Char, 1, "AUTO_YN");
            ocm.Parameters.Add("@REQ_YN", SqlDbType.Char, 1, "REQ_YN");
            ocm.Parameters.Add("@DANG_YN", SqlDbType.Char, 1, "DANG_YN");
            ocm.Parameters.Add("@YC_DAY", SqlDbType.Decimal, 3, "YC_DAY");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_MSTGNMUUpCmd()
        {
            string queryStatements = "UPDATE DBO.DUTY_MSTGNMU SET "
                                   + "   G_FNM = @G_FNM, "
                                   + "   G_SNM = @G_SNM, "
                                   + "   G_FRTM = @G_FRTM, "
                                   + "   G_TOTM = @G_TOTM, "
                                   + "   G_WORK = @G_WORK, "
                                   + "   G_TYPE = @G_TYPE, "
                                   + "   G_COLOR = @G_COLOR, "
                                   + "   G_RGB = @G_RGB, "
                                   + "   G_HEXA = @G_HEXA, "
                                   + "   AUTO_YN = @AUTO_YN, "
                                   + "   REQ_YN = @REQ_YN, "
                                   + "   DANG_YN = @DANG_YN, "
                                   + "   YC_DAY = @YC_DAY, "
                                   + "   INDT = @INDT, "
                                   + "   UPDT = @UPDT, "
                                   + "   USID = @USID, "
                                   + "   PSTY = @PSTY"
                                   + " WHERE G_CODE = @G_CODE"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;
			
            ocm.Parameters.Add("@G_CODE", SqlDbType.Char, 2, "G_CODE");
            ocm.Parameters.Add("@G_FNM", SqlDbType.VarChar, 40, "G_FNM");
            ocm.Parameters.Add("@G_SNM", SqlDbType.VarChar, 8, "G_SNM");
            ocm.Parameters.Add("@G_FRTM", SqlDbType.Char, 4, "G_FRTM");
            ocm.Parameters.Add("@G_TOTM", SqlDbType.Char, 4, "G_TOTM");
            ocm.Parameters.Add("@G_WORK", SqlDbType.Decimal, 5, "G_WORK");
            ocm.Parameters.Add("@G_TYPE", SqlDbType.Int, 5, "G_TYPE");
            ocm.Parameters.Add("@G_COLOR", SqlDbType.Int, 20, "G_COLOR");
            ocm.Parameters.Add("@G_RGB", SqlDbType.VarChar, 20, "G_RGB");
            ocm.Parameters.Add("@G_HEXA", SqlDbType.VarChar, 20, "G_HEXA");
            ocm.Parameters.Add("@AUTO_YN", SqlDbType.Char, 1, "AUTO_YN");
            ocm.Parameters.Add("@REQ_YN", SqlDbType.Char, 1, "REQ_YN");
            ocm.Parameters.Add("@DANG_YN", SqlDbType.Char, 1, "DANG_YN");
            ocm.Parameters.Add("@YC_DAY", SqlDbType.Decimal, 3, "YC_DAY");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_MSTGNMUDelCmd()
        {
            string queryStatements = "DELETE DBO.DUTY_MSTGNMU"
                                   + " WHERE G_CODE = @G_CODE"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@G_CODE", SqlDbType.Char, 2, "G_CODE").SourceVersion = DataRowVersion.Original;
            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion		

		#region D_DUTY_MSTHOLI delete command

        private SqlCommand GetD_DUTY_MSTHOLIDelCmd()

        {
            string queryStatements = "DELETE DBO.DUTY_MSTHOLI"
                                   + " WHERE H_DATE = @H_DATE"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;
			
            ocm.Parameters.Add("@H_DATE", SqlDbType.Char, 8, "H_DATE").SourceVersion = DataRowVersion.Original;
            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion

		#region DUTY_MSTHOLI insert command

        private SqlCommand GetDUTY_MSTHOLIInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_MSTHOLI(H_DATE, H_NAME, REG_DT, REG_ID, GUBN, REPEAT_CHK"
                                   + ") VALUES (@H_DATE, @H_NAME, @REG_DT, @REG_ID, @GUBN, @REPEAT_CHK"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@H_DATE", SqlDbType.Char, 8, "H_DATE");
            ocm.Parameters.Add("@H_NAME", SqlDbType.VarChar, 40, "H_NAME");
            ocm.Parameters.Add("@REG_DT", SqlDbType.VarChar, 20, "REG_DT");
            ocm.Parameters.Add("@REG_ID", SqlDbType.VarChar, 20, "REG_ID");
            ocm.Parameters.Add("@GUBN", SqlDbType.Char, 1, "GUBN");
            ocm.Parameters.Add("@REPEAT_CHK", SqlDbType.Char, 1, "REPEAT_CHK");

            return ocm;
        }		

        #endregion

		#region DUTY_INFOSD01 insert, update,delete command

        private SqlCommand GetDUTY_INFOSD01InCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_INFOSD01(YYMM, SABN, SABN_NM, YY_AMT, T_AMT, REG_DT, REG_ID"
                                   + ") VALUES (@YYMM, @SABN, @SABN_NM, @YY_AMT, @T_AMT, @REG_DT, @REG_ID"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@YYMM", SqlDbType.Char, 6, "YYMM");
            ocm.Parameters.Add("@SABN", SqlDbType.VarChar, 20, "SABN");
            ocm.Parameters.Add("@SABN_NM", SqlDbType.VarChar, 20, "SABN_NM");
            ocm.Parameters.Add("@YY_AMT", SqlDbType.Decimal, 14, "YY_AMT");
            ocm.Parameters.Add("@T_AMT", SqlDbType.Decimal, 14, "T_AMT");
            ocm.Parameters.Add("@REG_DT", SqlDbType.VarChar, 20, "REG_DT");
            ocm.Parameters.Add("@REG_ID", SqlDbType.VarChar, 20, "REG_ID");

            return ocm;
        }

        private SqlCommand GetDUTY_INFOSD01UpCmd()

        {
            string queryStatements = "UPDATE DBO.DUTY_INFOSD01 SET "
                                   + "   SABN_NM = @SABN_NM, "
                                   + "   YY_AMT = @YY_AMT, "
                                   + "   T_AMT = @T_AMT, "
                                   + "   REG_DT = @REG_DT, "
                                   + "   REG_ID = @REG_ID "
                                   + " WHERE YYMM = @YYMM "
                                   + "   AND SABN = @SABN "
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@YYMM", SqlDbType.Char, 6, "YYMM");
            ocm.Parameters.Add("@SABN", SqlDbType.VarChar, 20, "SABN");
            ocm.Parameters.Add("@SABN_NM", SqlDbType.VarChar, 20, "SABN_NM");
            ocm.Parameters.Add("@YY_AMT", SqlDbType.Decimal, 14, "YY_AMT");
            ocm.Parameters.Add("@T_AMT", SqlDbType.Decimal, 14, "T_AMT");
            ocm.Parameters.Add("@REG_DT", SqlDbType.VarChar, 20, "REG_DT");
            ocm.Parameters.Add("@REG_ID", SqlDbType.VarChar, 20, "REG_ID");

            return ocm;
        }

        private SqlCommand GetDUTY_INFOSD01DelCmd()

        {
            string queryStatements = "DELETE DBO.DUTY_INFOSD01"
                                   + " WHERE YYMM = @YYMM "
                                   + "   AND SABN = @SABN "
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;
			
            ocm.Parameters.Add("@YYMM", SqlDbType.Char, 6, "YYMM").SourceVersion = DataRowVersion.Original;
            ocm.Parameters.Add("@SABN", SqlDbType.VarChar, 20, "SABN").SourceVersion = DataRowVersion.Original;
            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion

		#region DUTY_INFOSD02 insert, update command

        private SqlCommand GetDUTY_INFOSD02InCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_INFOSD02(A01, A01_DFEE, A01_D01, A01_D02, A01_NFEE, A01_N01, A01_N02, A11, A12, A13, A14, A02, A02_INSU01, A02_INSU02, A21, A22, A03, A03_INSU01, A03_INSU02, A31, A04, A04_INSU01, A04_INSU02, A04_INSU11, A04_INSU12, A41, A05, A51, A06, A06_INSU01, A61, A07, A07_INSU01, A07_INSU02, A07_INSU11, A07_INSU12, A71, A72, A08, A08_INSU01, A81, REG_DT, REG_ID"
                                   + ") VALUES (@A01, @A01_DFEE, @A01_D01, @A01_D02, @A01_NFEE, @A01_N01, @A01_N02, @A11, @A12, @A13, @A14, @A02, @A02_INSU01, @A02_INSU02, @A21, @A22, @A03, @A03_INSU01, @A03_INSU02, @A31, @A04, @A04_INSU01, @A04_INSU02, @A04_INSU11, @A04_INSU12, @A41, @A05, @A51, @A06, @A06_INSU01, @A61, @A07, @A07_INSU01, @A07_INSU02, @A07_INSU11, @A07_INSU12, @A71, @A72, @A08, @A08_INSU01, @A81, @REG_DT, @REG_ID"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@A01", SqlDbType.Char, 4, "A01");
            ocm.Parameters.Add("@A01_DFEE", SqlDbType.Decimal, 5, "A01_DFEE");
            ocm.Parameters.Add("@A01_D01", SqlDbType.Decimal, 5, "A01_D01");
            ocm.Parameters.Add("@A01_D02", SqlDbType.Decimal, 5, "A01_D02");
            ocm.Parameters.Add("@A01_NFEE", SqlDbType.Decimal, 5, "A01_NFEE");
            ocm.Parameters.Add("@A01_N01", SqlDbType.Decimal, 5, "A01_N01");
            ocm.Parameters.Add("@A01_N02", SqlDbType.Decimal, 5, "A01_N02");
            ocm.Parameters.Add("@A11", SqlDbType.Char, 4, "A11");
            ocm.Parameters.Add("@A12", SqlDbType.Char, 4, "A12");
            ocm.Parameters.Add("@A13", SqlDbType.Char, 4, "A13");
            ocm.Parameters.Add("@A14", SqlDbType.Char, 4, "A14");
            ocm.Parameters.Add("@A02", SqlDbType.Char, 4, "A02");
            ocm.Parameters.Add("@A02_INSU01", SqlDbType.Decimal, 5, "A02_INSU01");
            ocm.Parameters.Add("@A02_INSU02", SqlDbType.Decimal, 5, "A02_INSU02");
            ocm.Parameters.Add("@A21", SqlDbType.Char, 4, "A21");
            ocm.Parameters.Add("@A22", SqlDbType.Char, 4, "A22");
            ocm.Parameters.Add("@A03", SqlDbType.Char, 4, "A03");
            ocm.Parameters.Add("@A03_INSU01", SqlDbType.Decimal, 5, "A03_INSU01");
            ocm.Parameters.Add("@A03_INSU02", SqlDbType.Decimal, 5, "A03_INSU02");
            ocm.Parameters.Add("@A31", SqlDbType.Char, 4, "A31");
            ocm.Parameters.Add("@A04", SqlDbType.Char, 4, "A04");
            ocm.Parameters.Add("@A04_INSU01", SqlDbType.Decimal, 5, "A04_INSU01");
            ocm.Parameters.Add("@A04_INSU02", SqlDbType.Decimal, 5, "A04_INSU02");
            ocm.Parameters.Add("@A04_INSU11", SqlDbType.Decimal, 5, "A04_INSU11");
            ocm.Parameters.Add("@A04_INSU12", SqlDbType.Decimal, 5, "A04_INSU12");
            ocm.Parameters.Add("@A41", SqlDbType.Char, 4, "A41");
            ocm.Parameters.Add("@A05", SqlDbType.Char, 4, "A05");
            ocm.Parameters.Add("@A51", SqlDbType.Char, 4, "A51");
            ocm.Parameters.Add("@A06", SqlDbType.Char, 4, "A06");
            ocm.Parameters.Add("@A61", SqlDbType.Char, 4, "A61");
            ocm.Parameters.Add("@A06_INSU01", SqlDbType.Decimal, 5, "A06_INSU01");
            ocm.Parameters.Add("@A07", SqlDbType.Char, 4, "A07");
            ocm.Parameters.Add("@A07_INSU01", SqlDbType.Decimal, 5, "A07_INSU01");
            ocm.Parameters.Add("@A07_INSU02", SqlDbType.Decimal, 5, "A07_INSU02");
            ocm.Parameters.Add("@A07_INSU11", SqlDbType.Decimal, 5, "A07_INSU11");
            ocm.Parameters.Add("@A07_INSU12", SqlDbType.Decimal, 5, "A07_INSU12");
            ocm.Parameters.Add("@A71", SqlDbType.Char, 4, "A71");
            ocm.Parameters.Add("@A72", SqlDbType.Char, 4, "A72");
            ocm.Parameters.Add("@A08", SqlDbType.Char, 4, "A08");
            ocm.Parameters.Add("@A08_INSU01", SqlDbType.Decimal, 5, "A08_INSU01");
            ocm.Parameters.Add("@A81", SqlDbType.Char, 4, "A81");
            ocm.Parameters.Add("@B01", SqlDbType.Char, 4, "B01");
            ocm.Parameters.Add("@B01_FEE01", SqlDbType.Decimal, 5, "B01_FEE01");
            ocm.Parameters.Add("@C91", SqlDbType.Char, 4, "C91");
            ocm.Parameters.Add("@REG_DT", SqlDbType.VarChar, 20, "REG_DT");
            ocm.Parameters.Add("@REG_ID", SqlDbType.VarChar, 20, "REG_ID");

            return ocm;
        }

        private SqlCommand GetDUTY_INFOSD02UpCmd()
        {
            string queryStatements = "UPDATE DBO.DUTY_INFOSD02 SET "
                                   + "   A01 = @A01, "
                                   + "   A01_DFEE = @A01_DFEE, "
                                   + "   A01_D01 = @A01_D01, "
                                   + "   A01_D02 = @A01_D02, "
                                   + "   A01_NFEE = @A01_NFEE, "
                                   + "   A01_N01 = @A01_N01, "
                                   + "   A01_N02 = @A01_N02, "
                                   + "   A11 = @A11, "
                                   + "   A12 = @A12, "
                                   + "   A13 = @A13, "
                                   + "   A14 = @A14, "
                                   + "   A02 = @A02, "
                                   + "   A02_INSU01 = @A02_INSU01, "
                                   + "   A02_INSU02 = @A02_INSU02, "
                                   + "   A21 = @A21, "
                                   + "   A22 = @A22, "
                                   + "   A03 = @A03, "
                                   + "   A03_INSU01 = @A03_INSU01, "
                                   + "   A03_INSU02 = @A03_INSU02, "
                                   + "   A31 = @A31, "
                                   + "   A04 = @A04, "
                                   + "   A04_INSU01 = @A04_INSU01, "
                                   + "   A04_INSU02 = @A04_INSU02, "
                                   + "   A04_INSU11 = @A04_INSU11, "
                                   + "   A04_INSU12 = @A04_INSU12, "
                                   + "   A41 = @A41, "
                                   + "   A05 = @A05, "
                                   + "   A51 = @A51, "
                                   + "   A06 = @A06, "
                                   + "   A06_INSU01 = @A06_INSU01, "
                                   + "   A61 = @A61, "
                                   + "   A07 = @A07, "
                                   + "   A07_INSU01 = @A07_INSU01, "
                                   + "   A07_INSU02 = @A07_INSU02, "
                                   + "   A07_INSU11 = @A07_INSU11, "
                                   + "   A07_INSU12 = @A07_INSU12, "
                                   + "   A71 = @A71, "
                                   + "   A72 = @A72, "
                                   + "   A08 = @A08, "
                                   + "   A08_INSU01 = @A08_INSU01, "
                                   + "   A81 = @A81, "
                                   + "   B01 = @B01, "
                                   + "   B01_FEE01 = @B01_FEE01, "
                                   + "   C91 = @C91, "
                                   + "   REG_DT = @REG_DT, "
                                   + "   REG_ID = @REG_ID "
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@A01", SqlDbType.Char, 4, "A01");
            ocm.Parameters.Add("@A01_DFEE", SqlDbType.Decimal, 5, "A01_DFEE");
            ocm.Parameters.Add("@A01_D01", SqlDbType.Decimal, 5, "A01_D01");
            ocm.Parameters.Add("@A01_D02", SqlDbType.Decimal, 5, "A01_D02");
            ocm.Parameters.Add("@A01_NFEE", SqlDbType.Decimal, 5, "A01_NFEE");
            ocm.Parameters.Add("@A01_N01", SqlDbType.Decimal, 5, "A01_N01");
            ocm.Parameters.Add("@A01_N02", SqlDbType.Decimal, 5, "A01_N02");
            ocm.Parameters.Add("@A11", SqlDbType.Char, 4, "A11");
            ocm.Parameters.Add("@A12", SqlDbType.Char, 4, "A12");
            ocm.Parameters.Add("@A13", SqlDbType.Char, 4, "A13");
            ocm.Parameters.Add("@A14", SqlDbType.Char, 4, "A14");
            ocm.Parameters.Add("@A02", SqlDbType.Char, 4, "A02");
            ocm.Parameters.Add("@A02_INSU01", SqlDbType.Decimal, 5, "A02_INSU01");
            ocm.Parameters.Add("@A02_INSU02", SqlDbType.Decimal, 5, "A02_INSU02");
            ocm.Parameters.Add("@A21", SqlDbType.Char, 4, "A21");
            ocm.Parameters.Add("@A22", SqlDbType.Char, 4, "A22");
            ocm.Parameters.Add("@A03", SqlDbType.Char, 4, "A03");
            ocm.Parameters.Add("@A03_INSU01", SqlDbType.Decimal, 5, "A03_INSU01");
            ocm.Parameters.Add("@A03_INSU02", SqlDbType.Decimal, 5, "A03_INSU02");
            ocm.Parameters.Add("@A31", SqlDbType.Char, 4, "A31");
            ocm.Parameters.Add("@A04", SqlDbType.Char, 4, "A04");
            ocm.Parameters.Add("@A04_INSU01", SqlDbType.Decimal, 5, "A04_INSU01");
            ocm.Parameters.Add("@A04_INSU02", SqlDbType.Decimal, 5, "A04_INSU02");
            ocm.Parameters.Add("@A04_INSU11", SqlDbType.Decimal, 5, "A04_INSU11");
            ocm.Parameters.Add("@A04_INSU12", SqlDbType.Decimal, 5, "A04_INSU12");
            ocm.Parameters.Add("@A41", SqlDbType.Char, 4, "A41");
            ocm.Parameters.Add("@A05", SqlDbType.Char, 4, "A05");
            ocm.Parameters.Add("@A51", SqlDbType.Char, 4, "A51");
            ocm.Parameters.Add("@A06", SqlDbType.Char, 4, "A06");
            ocm.Parameters.Add("@A61", SqlDbType.Char, 4, "A61");
            ocm.Parameters.Add("@A06_INSU01", SqlDbType.Decimal, 5, "A06_INSU01");
            ocm.Parameters.Add("@A07", SqlDbType.Char, 4, "A07");
            ocm.Parameters.Add("@A07_INSU01", SqlDbType.Decimal, 5, "A07_INSU01");
            ocm.Parameters.Add("@A07_INSU02", SqlDbType.Decimal, 5, "A07_INSU02");
            ocm.Parameters.Add("@A07_INSU11", SqlDbType.Decimal, 5, "A07_INSU11");
            ocm.Parameters.Add("@A07_INSU12", SqlDbType.Decimal, 5, "A07_INSU12");
            ocm.Parameters.Add("@A71", SqlDbType.Char, 4, "A71");
            ocm.Parameters.Add("@A72", SqlDbType.Char, 4, "A72");
            ocm.Parameters.Add("@A08", SqlDbType.Char, 4, "A08");
            ocm.Parameters.Add("@A08_INSU01", SqlDbType.Decimal, 5, "A08_INSU01");
            ocm.Parameters.Add("@A81", SqlDbType.Char, 4, "A81");
            ocm.Parameters.Add("@B01", SqlDbType.Char, 4, "B01");
            ocm.Parameters.Add("@B01_FEE01", SqlDbType.Decimal, 5, "B01_FEE01");
            ocm.Parameters.Add("@C91", SqlDbType.Char, 4, "C91");
            ocm.Parameters.Add("@REG_DT", SqlDbType.VarChar, 20, "REG_DT");
            ocm.Parameters.Add("@REG_ID", SqlDbType.VarChar, 20, "REG_ID");

            return ocm;
        }

        #endregion		

		#region DUTY_INFOSD03 insert, update command

        private SqlCommand GetDUTY_INFOSD03InCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_INFOSD03(DT01, DT02, DT03, DT04, DT05, DT06, DT07, DT11, DT12, DT13, DT14, DT15, DT16, DT17, REG_DT, REG_ID"
                                   + ") VALUES (@DT01, @DT02, @DT03, @DT04, @DT05, @DT06, @DT07, @DT11, @DT12, @DT13, @DT14, @DT15, @DT16, @DT17, @REG_DT, @REG_ID"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@DT01", SqlDbType.Decimal, 5, "DT01");
            ocm.Parameters.Add("@DT02", SqlDbType.Decimal, 5, "DT02");
            ocm.Parameters.Add("@DT03", SqlDbType.Decimal, 5, "DT03");
            ocm.Parameters.Add("@DT04", SqlDbType.Decimal, 5, "DT04");
            ocm.Parameters.Add("@DT05", SqlDbType.Decimal, 5, "DT05");
            ocm.Parameters.Add("@DT06", SqlDbType.Decimal, 5, "DT06");
            ocm.Parameters.Add("@DT07", SqlDbType.Decimal, 5, "DT07");
            ocm.Parameters.Add("@DT11", SqlDbType.Decimal, 5, "DT11");
            ocm.Parameters.Add("@DT12", SqlDbType.Decimal, 5, "DT12");
            ocm.Parameters.Add("@DT13", SqlDbType.Decimal, 5, "DT13");
            ocm.Parameters.Add("@DT14", SqlDbType.Decimal, 5, "DT14");
            ocm.Parameters.Add("@DT15", SqlDbType.Decimal, 5, "DT15");
            ocm.Parameters.Add("@DT16", SqlDbType.Decimal, 5, "DT16");
            ocm.Parameters.Add("@DT17", SqlDbType.Decimal, 5, "DT17");
            ocm.Parameters.Add("@REG_DT", SqlDbType.VarChar, 20, "REG_DT");
            ocm.Parameters.Add("@REG_ID", SqlDbType.VarChar, 20, "REG_ID");

            return ocm;
        }

        private SqlCommand GetDUTY_INFOSD03UpCmd()

        {
            string queryStatements = "UPDATE DBO.DUTY_INFOSD03 SET "
                                   + "   DT01 = @DT01, "
                                   + "   DT02 = @DT02, "
                                   + "   DT03 = @DT03, "
                                   + "   DT04 = @DT04, "
                                   + "   DT05 = @DT05, "
                                   + "   DT06 = @DT06, "
                                   + "   DT07 = @DT07, "
                                   + "   DT11 = @DT11, "
                                   + "   DT12 = @DT12, "
                                   + "   DT13 = @DT13, "
                                   + "   DT14 = @DT14, "
                                   + "   DT15 = @DT15, "
                                   + "   DT16 = @DT16, "
                                   + "   DT17 = @DT17, "
                                   + "   REG_DT = @REG_DT, "
                                   + "   REG_ID = @REG_ID "
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@DT01", SqlDbType.Decimal, 5, "DT01");
            ocm.Parameters.Add("@DT02", SqlDbType.Decimal, 5, "DT02");
            ocm.Parameters.Add("@DT03", SqlDbType.Decimal, 5, "DT03");
            ocm.Parameters.Add("@DT04", SqlDbType.Decimal, 5, "DT04");
            ocm.Parameters.Add("@DT05", SqlDbType.Decimal, 5, "DT05");
            ocm.Parameters.Add("@DT06", SqlDbType.Decimal, 5, "DT06");
            ocm.Parameters.Add("@DT07", SqlDbType.Decimal, 5, "DT07");
            ocm.Parameters.Add("@DT11", SqlDbType.Decimal, 5, "DT11");
            ocm.Parameters.Add("@DT12", SqlDbType.Decimal, 5, "DT12");
            ocm.Parameters.Add("@DT13", SqlDbType.Decimal, 5, "DT13");
            ocm.Parameters.Add("@DT14", SqlDbType.Decimal, 5, "DT14");
            ocm.Parameters.Add("@DT15", SqlDbType.Decimal, 5, "DT15");
            ocm.Parameters.Add("@DT16", SqlDbType.Decimal, 5, "DT16");
            ocm.Parameters.Add("@DT17", SqlDbType.Decimal, 5, "DT17");
            ocm.Parameters.Add("@REG_DT", SqlDbType.VarChar, 20, "REG_DT");
            ocm.Parameters.Add("@REG_ID", SqlDbType.VarChar, 20, "REG_ID");

            return ocm;
        }
		

        #endregion

		#region DUTY_INFOSD04 insert, update, delete command

        private SqlCommand GetDUTY_INFOSD04InCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_INFOSD04(DANG_DT, NIGHT_TIME, DAY_TIME, REG_DT, REG_ID"
                                   + ") VALUES (@DANG_DT, @NIGHT_TIME, @DAY_TIME, @REG_DT, @REG_ID"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@DANG_DT", SqlDbType.Char, 8, "DANG_DT");
            ocm.Parameters.Add("@NIGHT_TIME", SqlDbType.Decimal, 5, "NIGHT_TIME");
            ocm.Parameters.Add("@DAY_TIME", SqlDbType.Decimal, 5, "DAY_TIME");
            ocm.Parameters.Add("@REG_DT", SqlDbType.VarChar, 20, "REG_DT");
            ocm.Parameters.Add("@REG_ID", SqlDbType.VarChar, 20, "REG_ID");

            return ocm;
        }

        private SqlCommand GetDUTY_INFOSD04UpCmd()

        {
            string queryStatements = "UPDATE DBO.DUTY_INFOSD04 SET "
                                   + "   NIGHT_TIME = @NIGHT_TIME, "
                                   + "   DAY_TIME = @DAY_TIME, "
                                   + "   REG_DT = @REG_DT, "
                                   + "   REG_ID = @REG_ID "
                                   + " WHERE DANG_DT = @DANG_DT"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@DANG_DT", SqlDbType.Char, 8, "DANG_DT");
            ocm.Parameters.Add("@NIGHT_TIME", SqlDbType.Decimal, 5, "NIGHT_TIME");
            ocm.Parameters.Add("@DAY_TIME", SqlDbType.Decimal, 5, "DAY_TIME");
            ocm.Parameters.Add("@REG_DT", SqlDbType.VarChar, 20, "REG_DT");
            ocm.Parameters.Add("@REG_ID", SqlDbType.VarChar, 20, "REG_ID");

            return ocm;
        }

        private SqlCommand GetDUTY_INFOSD04DelCmd()

        {
            string queryStatements = "DELETE DBO.DUTY_INFOSD04"
                                   + " WHERE DANG_DT = @DANG_DT"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;
			
            ocm.Parameters.Add("@DANG_DT", SqlDbType.Char, 8, "DANG_DT").SourceVersion = DataRowVersion.Original;
            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion

		#region DUTY_INFOSD05 insert, update,delete command

        private SqlCommand GetDUTY_INFOSD05InCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_INFOSD05(YYMM, SABN, SABN_NM, MINUS_NAMT, PLUS_NAMT, REG_DT, REG_ID"
                                   + ") VALUES (@YYMM, @SABN, @SABN_NM, @MINUS_NAMT, @PLUS_NAMT, @REG_DT, @REG_ID"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@YYMM", SqlDbType.Char, 6, "YYMM");
            ocm.Parameters.Add("@SABN", SqlDbType.VarChar, 20, "SABN");
            ocm.Parameters.Add("@SABN_NM", SqlDbType.VarChar, 20, "SABN_NM");
            ocm.Parameters.Add("@MINUS_NAMT", SqlDbType.Decimal, 5, "MINUS_NAMT");
            ocm.Parameters.Add("@PLUS_NAMT", SqlDbType.Decimal, 5, "PLUS_NAMT");
            ocm.Parameters.Add("@REG_DT", SqlDbType.VarChar, 20, "REG_DT");
            ocm.Parameters.Add("@REG_ID", SqlDbType.VarChar, 20, "REG_ID");

            return ocm;
        }

        private SqlCommand GetDUTY_INFOSD05UpCmd()

        {
            string queryStatements = "UPDATE DBO.DUTY_INFOSD05 SET "
                                   + "   SABN_NM = @SABN_NM, "
                                   + "   MINUS_NAMT = @MINUS_NAMT, "
                                   + "   PLUS_NAMT = @PLUS_NAMT, "
                                   + "   REG_DT = @REG_DT, "
                                   + "   REG_ID = @REG_ID "
                                   + " WHERE YYMM = @YYMM "
                                   + "   AND SABN = @SABN "
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@YYMM", SqlDbType.Char, 6, "YYMM");
            ocm.Parameters.Add("@SABN", SqlDbType.VarChar, 20, "SABN");
            ocm.Parameters.Add("@SABN_NM", SqlDbType.VarChar, 20, "SABN_NM");
            ocm.Parameters.Add("@MINUS_NAMT", SqlDbType.Decimal, 5, "MINUS_NAMT");
            ocm.Parameters.Add("@PLUS_NAMT", SqlDbType.Decimal, 5, "PLUS_NAMT");
            ocm.Parameters.Add("@REG_DT", SqlDbType.VarChar, 20, "REG_DT");
            ocm.Parameters.Add("@REG_ID", SqlDbType.VarChar, 20, "REG_ID");

            return ocm;
        }

        private SqlCommand GetDUTY_INFOSD05DelCmd()

        {
            string queryStatements = "DELETE DBO.DUTY_INFOSD05"
                                   + " WHERE YYMM = @YYMM "
                                   + "   AND SABN = @SABN "
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;
			
            ocm.Parameters.Add("@YYMM", SqlDbType.Char, 6, "YYMM").SourceVersion = DataRowVersion.Original;
            ocm.Parameters.Add("@SABN", SqlDbType.VarChar, 20, "SABN").SourceVersion = DataRowVersion.Original;
            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion

		#region DUTY_TRSOVTM insert, update,delete command

        private SqlCommand GetDUTY_TRSOVTMInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_TRSOVTM(SABN, OT_DATE, OT_GUBN, CALL_CNT1, CALL_CNT2, CALL_TIME1, CALL_TIME2, OT_TIME1, OT_TIME2, AP_TAG, AP_DT, AP_USID, CANC_DT, CANC_USID, TIME_REMK, REMARK, INDT, UPDT, USID, PSTY"
                                   + ") VALUES (@SABN, @OT_DATE, @OT_GUBN, @CALL_CNT1, @CALL_CNT2, @CALL_TIME1, @CALL_TIME2, @OT_TIME1, @OT_TIME2, @AP_TAG, @AP_DT, @AP_USID, @CANC_DT, @CANC_USID, @TIME_REMK, @REMARK, @INDT, @UPDT, @USID, @PSTY"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@SABN", SqlDbType.VarChar, 20, "SABN");
            ocm.Parameters.Add("@OT_DATE", SqlDbType.Char, 8, "OT_DATE");
            ocm.Parameters.Add("@OT_GUBN", SqlDbType.Char, 1, "OT_GUBN");
            ocm.Parameters.Add("@CALL_CNT1", SqlDbType.Int, 5, "CALL_CNT1");
            ocm.Parameters.Add("@CALL_CNT2", SqlDbType.Int, 5, "CALL_CNT2");
            ocm.Parameters.Add("@CALL_TIME1", SqlDbType.Decimal, 5, "CALL_TIME1");
            ocm.Parameters.Add("@CALL_TIME2", SqlDbType.Decimal, 5, "CALL_TIME2");
            ocm.Parameters.Add("@OT_TIME1", SqlDbType.Decimal, 5, "OT_TIME1");
            ocm.Parameters.Add("@OT_TIME2", SqlDbType.Decimal, 5, "OT_TIME2");
            ocm.Parameters.Add("@AP_TAG", SqlDbType.Char, 1, "AP_TAG");
            ocm.Parameters.Add("@AP_DT", SqlDbType.VarChar, 20, "AP_DT");
            ocm.Parameters.Add("@AP_USID", SqlDbType.VarChar, 20, "AP_USID");
            ocm.Parameters.Add("@CANC_DT", SqlDbType.VarChar, 20, "CANC_DT");
            ocm.Parameters.Add("@CANC_USID", SqlDbType.VarChar, 20, "CANC_USID");
            ocm.Parameters.Add("@TIME_REMK", SqlDbType.VarChar, 80, "TIME_REMK");
            ocm.Parameters.Add("@REMARK", SqlDbType.VarChar, 200, "REMARK");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_TRSOVTMUpCmd()

        {
            string queryStatements = "UPDATE DBO.DUTY_TRSOVTM SET "
                                   + "   CALL_CNT1 = @CALL_CNT1, "
                                   + "   CALL_CNT2 = @CALL_CNT2, "
                                   + "   CALL_TIME1 = @CALL_TIME1, "
                                   + "   CALL_TIME2 = @CALL_TIME2, "
                                   + "   OT_TIME1 = @OT_TIME1, "
                                   + "   OT_TIME2 = @OT_TIME2, "
                                   + "   AP_TAG = @AP_TAG, "
                                   + "   AP_DT = @AP_DT, "
                                   + "   AP_USID = @AP_USID, "
                                   + "   CANC_DT = @CANC_DT, "
                                   + "   CANC_USID = @CANC_USID, "
                                   + "   TIME_REMK = @TIME_REMK, "
                                   + "   REMARK = @REMARK, "
                                   + "   INDT = @INDT, "
                                   + "   UPDT = @UPDT, "
                                   + "   USID = @USID, "
                                   + "   PSTY = @PSTY"
                                   + " WHERE SABN = @SABN"
                                   + "   AND OT_DATE = @OT_DATE"
                                   + "   AND OT_GUBN = @OT_GUBN"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@SABN", SqlDbType.VarChar, 20, "SABN");
            ocm.Parameters.Add("@OT_DATE", SqlDbType.Char, 8, "OT_DATE");
            ocm.Parameters.Add("@OT_GUBN", SqlDbType.Char, 1, "OT_GUBN");
            ocm.Parameters.Add("@CALL_CNT1", SqlDbType.Int, 5, "CALL_CNT1");
            ocm.Parameters.Add("@CALL_CNT2", SqlDbType.Int, 5, "CALL_CNT2");
            ocm.Parameters.Add("@CALL_TIME1", SqlDbType.Decimal, 5, "CALL_TIME1");
            ocm.Parameters.Add("@CALL_TIME2", SqlDbType.Decimal, 5, "CALL_TIME2");
            ocm.Parameters.Add("@OT_TIME1", SqlDbType.Decimal, 5, "OT_TIME1");
            ocm.Parameters.Add("@OT_TIME2", SqlDbType.Decimal, 5, "OT_TIME2");
            ocm.Parameters.Add("@AP_TAG", SqlDbType.Char, 1, "AP_TAG");
            ocm.Parameters.Add("@AP_DT", SqlDbType.VarChar, 20, "AP_DT");
            ocm.Parameters.Add("@AP_USID", SqlDbType.VarChar, 20, "AP_USID");
            ocm.Parameters.Add("@CANC_DT", SqlDbType.VarChar, 20, "CANC_DT");
            ocm.Parameters.Add("@CANC_USID", SqlDbType.VarChar, 20, "CANC_USID");
            ocm.Parameters.Add("@TIME_REMK", SqlDbType.VarChar, 80, "TIME_REMK");
            ocm.Parameters.Add("@REMARK", SqlDbType.VarChar, 200, "REMARK");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_TRSOVTMDelCmd()

        {
            string queryStatements = "DELETE DBO.DUTY_TRSOVTM"
                                   + " WHERE SABN = @SABN"
                                   + "   AND OT_DATE = @OT_DATE"
                                   + "   AND OT_GUBN = @OT_GUBN"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@SABN", SqlDbType.VarChar, 20, "SABN").SourceVersion = DataRowVersion.Original;
            ocm.Parameters.Add("@OT_DATE", SqlDbType.Char, 8, "OT_DATE").SourceVersion = DataRowVersion.Original;
            ocm.Parameters.Add("@OT_GUBN", SqlDbType.Char, 1, "OT_GUBN").SourceVersion = DataRowVersion.Original;

            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion

		#region DEL_TRSOVTM insert command

        private SqlCommand GetDEL_TRSOVTMInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DEL_TRSOVTM(SABN, OT_DATE, OT_GUBN, CALL_CNT1, CALL_CNT2, CALL_TIME1, CALL_TIME2, OT_TIME1, OT_TIME2, AP_TAG, AP_DT, AP_USID, CANC_DT, CANC_USID, TIME_REMK, REMARK, INDT, UPDT, USID, PSTY, DEL_DT, DEL_ID"
                                   + ") VALUES (@SABN, @OT_DATE, @OT_GUBN, @CALL_CNT1, @CALL_CNT2, @CALL_TIME1, @CALL_TIME2, @OT_TIME1, @OT_TIME2, @AP_TAG, @AP_DT, @AP_USID, @CANC_DT, @CANC_USID, @TIME_REMK, @REMARK, @INDT, @UPDT, @USID, @PSTY, @DEL_DT, @DEL_ID"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@SABN", SqlDbType.VarChar, 20, "SABN");
            ocm.Parameters.Add("@OT_DATE", SqlDbType.Char, 8, "OT_DATE");
            ocm.Parameters.Add("@OT_GUBN", SqlDbType.Char, 1, "OT_GUBN");
            ocm.Parameters.Add("@CALL_CNT1", SqlDbType.Int, 5, "CALL_CNT1");
            ocm.Parameters.Add("@CALL_CNT2", SqlDbType.Int, 5, "CALL_CNT2");
            ocm.Parameters.Add("@CALL_TIME1", SqlDbType.Decimal, 5, "CALL_TIME1");
            ocm.Parameters.Add("@CALL_TIME2", SqlDbType.Decimal, 5, "CALL_TIME2");
            ocm.Parameters.Add("@OT_TIME1", SqlDbType.Decimal, 5, "OT_TIME1");
            ocm.Parameters.Add("@OT_TIME2", SqlDbType.Decimal, 5, "OT_TIME2");
            ocm.Parameters.Add("@AP_TAG", SqlDbType.Char, 1, "AP_TAG");
            ocm.Parameters.Add("@AP_DT", SqlDbType.VarChar, 20, "AP_DT");
            ocm.Parameters.Add("@AP_USID", SqlDbType.VarChar, 20, "AP_USID");
            ocm.Parameters.Add("@CANC_DT", SqlDbType.VarChar, 20, "CANC_DT");
            ocm.Parameters.Add("@CANC_USID", SqlDbType.VarChar, 20, "CANC_USID");
            ocm.Parameters.Add("@TIME_REMK", SqlDbType.VarChar, 80, "TIME_REMK");
            ocm.Parameters.Add("@REMARK", SqlDbType.VarChar, 200, "REMARK");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");
            ocm.Parameters.Add("@DEL_DT", SqlDbType.VarChar, 20, "DEL_DT");
            ocm.Parameters.Add("@DEL_ID", SqlDbType.VarChar, 20, "DEL_ID");

            return ocm;
        }		

        #endregion

		#region DUTY_TRSCALL insert, update,delete command
		
        private SqlCommand GetDUTY_TRSCALLInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_TRSCALL(SAWON_NO, PLANYYMM, PLAN_SQ, DEPTCODE, MM_CNT1, MM_CNT2, D01, D02, D03, D04, D05, D06, D07, D08, D09, D10, D11, D12, D13, D14, D15, D16, D17, D18, D19, D20, D21, D22, D23, D24, D25, D26, D27, D28, D29, D30, D31, INDT, UPDT, USID, PSTY"
                                   + ") VALUES (@SAWON_NO, @PLANYYMM, @PLAN_SQ, @DEPTCODE, @MM_CNT1, @MM_CNT2, @D01, @D02, @D03, @D04, @D05, @D06, @D07, @D08, @D09, @D10, @D11, @D12, @D13, @D14, @D15, @D16, @D17, @D18, @D19, @D20, @D21, @D22, @D23, @D24, @D25, @D26, @D27, @D28, @D29, @D30, @D31, @INDT, @UPDT, @USID, @PSTY"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@SAWON_NO", SqlDbType.VarChar, 15, "SAWON_NO");
            ocm.Parameters.Add("@PLANYYMM", SqlDbType.Char, 6, "PLANYYMM");
            ocm.Parameters.Add("@PLAN_SQ", SqlDbType.Int, 4, "PLAN_SQ");
            ocm.Parameters.Add("@DEPTCODE", SqlDbType.Char, 4, "DEPTCODE");
            ocm.Parameters.Add("@MM_CNT1", SqlDbType.Int, 4, "MM_CNT1");
            ocm.Parameters.Add("@MM_CNT2", SqlDbType.Int, 4, "MM_CNT2");
            ocm.Parameters.Add("@D01", SqlDbType.Char, 2, "D01");
            ocm.Parameters.Add("@D02", SqlDbType.Char, 2, "D02");
            ocm.Parameters.Add("@D03", SqlDbType.Char, 2, "D03");
            ocm.Parameters.Add("@D04", SqlDbType.Char, 2, "D04");
            ocm.Parameters.Add("@D05", SqlDbType.Char, 2, "D05");
            ocm.Parameters.Add("@D06", SqlDbType.Char, 2, "D06");
            ocm.Parameters.Add("@D07", SqlDbType.Char, 2, "D07");
            ocm.Parameters.Add("@D08", SqlDbType.Char, 2, "D08");
            ocm.Parameters.Add("@D09", SqlDbType.Char, 2, "D09");
            ocm.Parameters.Add("@D10", SqlDbType.Char, 2, "D10");
            ocm.Parameters.Add("@D11", SqlDbType.Char, 2, "D11");
            ocm.Parameters.Add("@D12", SqlDbType.Char, 2, "D12");
            ocm.Parameters.Add("@D13", SqlDbType.Char, 2, "D13");
            ocm.Parameters.Add("@D14", SqlDbType.Char, 2, "D14");
            ocm.Parameters.Add("@D15", SqlDbType.Char, 2, "D15");
            ocm.Parameters.Add("@D16", SqlDbType.Char, 2, "D16");
            ocm.Parameters.Add("@D17", SqlDbType.Char, 2, "D17");
            ocm.Parameters.Add("@D18", SqlDbType.Char, 2, "D18");
            ocm.Parameters.Add("@D19", SqlDbType.Char, 2, "D19");
            ocm.Parameters.Add("@D20", SqlDbType.Char, 2, "D20");
            ocm.Parameters.Add("@D21", SqlDbType.Char, 2, "D21");
            ocm.Parameters.Add("@D22", SqlDbType.Char, 2, "D22");
            ocm.Parameters.Add("@D23", SqlDbType.Char, 2, "D23");
            ocm.Parameters.Add("@D24", SqlDbType.Char, 2, "D24");
            ocm.Parameters.Add("@D25", SqlDbType.Char, 2, "D25");
            ocm.Parameters.Add("@D26", SqlDbType.Char, 2, "D26");
            ocm.Parameters.Add("@D27", SqlDbType.Char, 2, "D27");
            ocm.Parameters.Add("@D28", SqlDbType.Char, 2, "D28");
            ocm.Parameters.Add("@D29", SqlDbType.Char, 2, "D29");
            ocm.Parameters.Add("@D30", SqlDbType.Char, 2, "D30");
            ocm.Parameters.Add("@D31", SqlDbType.Char, 2, "D31");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_TRSCALLUpCmd()
        {
            string queryStatements = "UPDATE DBO.DUTY_TRSCALL SET "
                                   + "   PLAN_SQ = @PLAN_SQ, "
                                   + "   MM_CNT1 = @MM_CNT1, "
                                   + "   MM_CNT2 = @MM_CNT2, "
                                   + "   D01 = @D01, "
                                   + "   D02 = @D02, "
                                   + "   D03 = @D03, "
                                   + "   D04 = @D04, "
                                   + "   D05 = @D05, "
                                   + "   D06 = @D06, "
                                   + "   D07 = @D07, "
                                   + "   D08 = @D08, "
                                   + "   D09 = @D09, "
                                   + "   D10 = @D10, "
                                   + "   D11 = @D11, "
                                   + "   D12 = @D12, "
                                   + "   D13 = @D13, "
                                   + "   D14 = @D14, "
                                   + "   D15 = @D15, "
                                   + "   D16 = @D16, "
                                   + "   D17 = @D17, "
                                   + "   D18 = @D18, "
                                   + "   D19 = @D19, "
                                   + "   D20 = @D20, "
                                   + "   D21 = @D21, "
                                   + "   D22 = @D22, "
                                   + "   D23 = @D23, "
                                   + "   D24 = @D24, "
                                   + "   D25 = @D25, "
                                   + "   D26 = @D26, "
                                   + "   D27 = @D27, "
                                   + "   D28 = @D28, "
                                   + "   D29 = @D29, "
                                   + "   D30 = @D30, "
                                   + "   D31 = @D31, "
                                   + "   INDT = @INDT, "
                                   + "   UPDT = @UPDT, "
                                   + "   USID = @USID, "
                                   + "   PSTY = @PSTY"
                                   + " WHERE SAWON_NO = @SAWON_NO"
                                   + "   AND PLANYYMM = @PLANYYMM"
                                   + "   AND DEPTCODE = @DEPTCODE"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@SAWON_NO", SqlDbType.VarChar, 15, "SAWON_NO");
            ocm.Parameters.Add("@PLANYYMM", SqlDbType.Char, 6, "PLANYYMM");
            ocm.Parameters.Add("@PLAN_SQ", SqlDbType.Int, 4, "PLAN_SQ");
            ocm.Parameters.Add("@DEPTCODE", SqlDbType.Char, 4, "DEPTCODE");
            ocm.Parameters.Add("@MM_CNT1", SqlDbType.Int, 4, "MM_CNT1");
            ocm.Parameters.Add("@MM_CNT2", SqlDbType.Int, 4, "MM_CNT2");
            ocm.Parameters.Add("@D01", SqlDbType.Char, 2, "D01");
            ocm.Parameters.Add("@D02", SqlDbType.Char, 2, "D02");
            ocm.Parameters.Add("@D03", SqlDbType.Char, 2, "D03");
            ocm.Parameters.Add("@D04", SqlDbType.Char, 2, "D04");
            ocm.Parameters.Add("@D05", SqlDbType.Char, 2, "D05");
            ocm.Parameters.Add("@D06", SqlDbType.Char, 2, "D06");
            ocm.Parameters.Add("@D07", SqlDbType.Char, 2, "D07");
            ocm.Parameters.Add("@D08", SqlDbType.Char, 2, "D08");
            ocm.Parameters.Add("@D09", SqlDbType.Char, 2, "D09");
            ocm.Parameters.Add("@D10", SqlDbType.Char, 2, "D10");
            ocm.Parameters.Add("@D11", SqlDbType.Char, 2, "D11");
            ocm.Parameters.Add("@D12", SqlDbType.Char, 2, "D12");
            ocm.Parameters.Add("@D13", SqlDbType.Char, 2, "D13");
            ocm.Parameters.Add("@D14", SqlDbType.Char, 2, "D14");
            ocm.Parameters.Add("@D15", SqlDbType.Char, 2, "D15");
            ocm.Parameters.Add("@D16", SqlDbType.Char, 2, "D16");
            ocm.Parameters.Add("@D17", SqlDbType.Char, 2, "D17");
            ocm.Parameters.Add("@D18", SqlDbType.Char, 2, "D18");
            ocm.Parameters.Add("@D19", SqlDbType.Char, 2, "D19");
            ocm.Parameters.Add("@D20", SqlDbType.Char, 2, "D20");
            ocm.Parameters.Add("@D21", SqlDbType.Char, 2, "D21");
            ocm.Parameters.Add("@D22", SqlDbType.Char, 2, "D22");
            ocm.Parameters.Add("@D23", SqlDbType.Char, 2, "D23");
            ocm.Parameters.Add("@D24", SqlDbType.Char, 2, "D24");
            ocm.Parameters.Add("@D25", SqlDbType.Char, 2, "D25");
            ocm.Parameters.Add("@D26", SqlDbType.Char, 2, "D26");
            ocm.Parameters.Add("@D27", SqlDbType.Char, 2, "D27");
            ocm.Parameters.Add("@D28", SqlDbType.Char, 2, "D28");
            ocm.Parameters.Add("@D29", SqlDbType.Char, 2, "D29");
            ocm.Parameters.Add("@D30", SqlDbType.Char, 2, "D30");
            ocm.Parameters.Add("@D31", SqlDbType.Char, 2, "D31");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_TRSCALLDelCmd()
        {
            string queryStatements = "DELETE DBO.DUTY_TRSCALL"
                                   + " WHERE SAWON_NO = @SAWON_NO"
                                   + "   AND PLANYYMM = @PLANYYMM"
                                   + "   AND DEPTCODE = @DEPTCODE"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@SAWON_NO", SqlDbType.VarChar, 15, "SAWON_NO").SourceVersion = DataRowVersion.Original;
            ocm.Parameters.Add("@PLANYYMM", SqlDbType.Char, 6, "PLANYYMM").SourceVersion = DataRowVersion.Original;
            ocm.Parameters.Add("@PLANYYMM", SqlDbType.Char, 6, "PLANYYMM").SourceVersion = DataRowVersion.Original;

            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion
				
		#region D_DUTY_INFOCALL delete command

        private SqlCommand GetD_DUTY_INFOCALLDelCmd()

        {
            string queryStatements = "DELETE DBO.DUTY_INFOCALL"
                                   + " WHERE DEPTCODE = @DEPTCODE"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;
			
            ocm.Parameters.Add("@DEPTCODE", SqlDbType.Char, 4, "DEPTCODE").SourceVersion = DataRowVersion.Original;
            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion

		#region DUTY_INFOCALL insert command

        private SqlCommand GetDUTY_INFOCALLInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_INFOCALL(DEPTCODE, INDT, UPDT, USID, PSTY"
                                   + ") VALUES (@DEPTCODE, @INDT, @UPDT, @USID, @PSTY"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@DEPTCODE", SqlDbType.Char, 4, "DEPTCODE");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }		

        #endregion

		#region DUTY_TRSDANG insert, update,delete command
		
        private SqlCommand GetDUTY_TRSDANGInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_TRSDANG(SAWON_NO, PLANYYMM, PLAN_SQ, DEPTCODE, MM_CNT1, MM_CNT2, MM_CNT3, MM_CNT4, MM_CNT5, D01, D02, D03, D04, D05, D06, D07, D08, D09, D10, D11, D12, D13, D14, D15, D16, D17, D18, D19, D20, D21, D22, D23, D24, D25, D26, D27, D28, D29, D30, D31, INDT, UPDT, USID, PSTY"
                                   + ") VALUES (@SAWON_NO, @PLANYYMM, @PLAN_SQ, @DEPTCODE, @MM_CNT1, @MM_CNT2, @MM_CNT3, @MM_CNT4, @MM_CNT5, @D01, @D02, @D03, @D04, @D05, @D06, @D07, @D08, @D09, @D10, @D11, @D12, @D13, @D14, @D15, @D16, @D17, @D18, @D19, @D20, @D21, @D22, @D23, @D24, @D25, @D26, @D27, @D28, @D29, @D30, @D31, @INDT, @UPDT, @USID, @PSTY"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@SAWON_NO", SqlDbType.VarChar, 15, "SAWON_NO");
            ocm.Parameters.Add("@PLANYYMM", SqlDbType.Char, 6, "PLANYYMM");
            ocm.Parameters.Add("@PLAN_SQ", SqlDbType.Int, 4, "PLAN_SQ");
            ocm.Parameters.Add("@DEPTCODE", SqlDbType.Char, 4, "DEPTCODE");
            ocm.Parameters.Add("@MM_CNT1", SqlDbType.Int, 4, "MM_CNT1");
            ocm.Parameters.Add("@MM_CNT2", SqlDbType.Int, 4, "MM_CNT2");
            ocm.Parameters.Add("@MM_CNT3", SqlDbType.Int, 4, "MM_CNT3");
            ocm.Parameters.Add("@MM_CNT4", SqlDbType.Int, 4, "MM_CNT4");
            ocm.Parameters.Add("@MM_CNT5", SqlDbType.Decimal, 5, "MM_CNT5");
            ocm.Parameters.Add("@D01", SqlDbType.Char, 2, "D01");
            ocm.Parameters.Add("@D02", SqlDbType.Char, 2, "D02");
            ocm.Parameters.Add("@D03", SqlDbType.Char, 2, "D03");
            ocm.Parameters.Add("@D04", SqlDbType.Char, 2, "D04");
            ocm.Parameters.Add("@D05", SqlDbType.Char, 2, "D05");
            ocm.Parameters.Add("@D06", SqlDbType.Char, 2, "D06");
            ocm.Parameters.Add("@D07", SqlDbType.Char, 2, "D07");
            ocm.Parameters.Add("@D08", SqlDbType.Char, 2, "D08");
            ocm.Parameters.Add("@D09", SqlDbType.Char, 2, "D09");
            ocm.Parameters.Add("@D10", SqlDbType.Char, 2, "D10");
            ocm.Parameters.Add("@D11", SqlDbType.Char, 2, "D11");
            ocm.Parameters.Add("@D12", SqlDbType.Char, 2, "D12");
            ocm.Parameters.Add("@D13", SqlDbType.Char, 2, "D13");
            ocm.Parameters.Add("@D14", SqlDbType.Char, 2, "D14");
            ocm.Parameters.Add("@D15", SqlDbType.Char, 2, "D15");
            ocm.Parameters.Add("@D16", SqlDbType.Char, 2, "D16");
            ocm.Parameters.Add("@D17", SqlDbType.Char, 2, "D17");
            ocm.Parameters.Add("@D18", SqlDbType.Char, 2, "D18");
            ocm.Parameters.Add("@D19", SqlDbType.Char, 2, "D19");
            ocm.Parameters.Add("@D20", SqlDbType.Char, 2, "D20");
            ocm.Parameters.Add("@D21", SqlDbType.Char, 2, "D21");
            ocm.Parameters.Add("@D22", SqlDbType.Char, 2, "D22");
            ocm.Parameters.Add("@D23", SqlDbType.Char, 2, "D23");
            ocm.Parameters.Add("@D24", SqlDbType.Char, 2, "D24");
            ocm.Parameters.Add("@D25", SqlDbType.Char, 2, "D25");
            ocm.Parameters.Add("@D26", SqlDbType.Char, 2, "D26");
            ocm.Parameters.Add("@D27", SqlDbType.Char, 2, "D27");
            ocm.Parameters.Add("@D28", SqlDbType.Char, 2, "D28");
            ocm.Parameters.Add("@D29", SqlDbType.Char, 2, "D29");
            ocm.Parameters.Add("@D30", SqlDbType.Char, 2, "D30");
            ocm.Parameters.Add("@D31", SqlDbType.Char, 2, "D31");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_TRSDANGUpCmd()
        {
            string queryStatements = "UPDATE DBO.DUTY_TRSDANG SET "
                                   + "   PLAN_SQ = @PLAN_SQ, "
                                   + "   MM_CNT1 = @MM_CNT1, "
                                   + "   MM_CNT2 = @MM_CNT2, "
                                   + "   MM_CNT3 = @MM_CNT3, "
                                   + "   MM_CNT4 = @MM_CNT4, "
                                   + "   MM_CNT5 = @MM_CNT5, "
                                   + "   D01 = @D01, "
                                   + "   D02 = @D02, "
                                   + "   D03 = @D03, "
                                   + "   D04 = @D04, "
                                   + "   D05 = @D05, "
                                   + "   D06 = @D06, "
                                   + "   D07 = @D07, "
                                   + "   D08 = @D08, "
                                   + "   D09 = @D09, "
                                   + "   D10 = @D10, "
                                   + "   D11 = @D11, "
                                   + "   D12 = @D12, "
                                   + "   D13 = @D13, "
                                   + "   D14 = @D14, "
                                   + "   D15 = @D15, "
                                   + "   D16 = @D16, "
                                   + "   D17 = @D17, "
                                   + "   D18 = @D18, "
                                   + "   D19 = @D19, "
                                   + "   D20 = @D20, "
                                   + "   D21 = @D21, "
                                   + "   D22 = @D22, "
                                   + "   D23 = @D23, "
                                   + "   D24 = @D24, "
                                   + "   D25 = @D25, "
                                   + "   D26 = @D26, "
                                   + "   D27 = @D27, "
                                   + "   D28 = @D28, "
                                   + "   D29 = @D29, "
                                   + "   D30 = @D30, "
                                   + "   D31 = @D31, "
                                   + "   INDT = @INDT, "
                                   + "   UPDT = @UPDT, "
                                   + "   USID = @USID, "
                                   + "   PSTY = @PSTY"
                                   + " WHERE SAWON_NO = @SAWON_NO"
                                   + "   AND PLANYYMM = @PLANYYMM"
                                   + "   AND DEPTCODE = @DEPTCODE"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@SAWON_NO", SqlDbType.VarChar, 15, "SAWON_NO");
            ocm.Parameters.Add("@PLANYYMM", SqlDbType.Char, 6, "PLANYYMM");
            ocm.Parameters.Add("@PLAN_SQ", SqlDbType.Int, 4, "PLAN_SQ");
            ocm.Parameters.Add("@DEPTCODE", SqlDbType.Char, 4, "DEPTCODE");
            ocm.Parameters.Add("@MM_CNT1", SqlDbType.Int, 4, "MM_CNT1");
            ocm.Parameters.Add("@MM_CNT2", SqlDbType.Int, 4, "MM_CNT2");
            ocm.Parameters.Add("@MM_CNT3", SqlDbType.Int, 4, "MM_CNT3");
            ocm.Parameters.Add("@MM_CNT4", SqlDbType.Int, 4, "MM_CNT4");
            ocm.Parameters.Add("@MM_CNT5", SqlDbType.Decimal, 5, "MM_CNT5");
            ocm.Parameters.Add("@D01", SqlDbType.Char, 2, "D01");
            ocm.Parameters.Add("@D02", SqlDbType.Char, 2, "D02");
            ocm.Parameters.Add("@D03", SqlDbType.Char, 2, "D03");
            ocm.Parameters.Add("@D04", SqlDbType.Char, 2, "D04");
            ocm.Parameters.Add("@D05", SqlDbType.Char, 2, "D05");
            ocm.Parameters.Add("@D06", SqlDbType.Char, 2, "D06");
            ocm.Parameters.Add("@D07", SqlDbType.Char, 2, "D07");
            ocm.Parameters.Add("@D08", SqlDbType.Char, 2, "D08");
            ocm.Parameters.Add("@D09", SqlDbType.Char, 2, "D09");
            ocm.Parameters.Add("@D10", SqlDbType.Char, 2, "D10");
            ocm.Parameters.Add("@D11", SqlDbType.Char, 2, "D11");
            ocm.Parameters.Add("@D12", SqlDbType.Char, 2, "D12");
            ocm.Parameters.Add("@D13", SqlDbType.Char, 2, "D13");
            ocm.Parameters.Add("@D14", SqlDbType.Char, 2, "D14");
            ocm.Parameters.Add("@D15", SqlDbType.Char, 2, "D15");
            ocm.Parameters.Add("@D16", SqlDbType.Char, 2, "D16");
            ocm.Parameters.Add("@D17", SqlDbType.Char, 2, "D17");
            ocm.Parameters.Add("@D18", SqlDbType.Char, 2, "D18");
            ocm.Parameters.Add("@D19", SqlDbType.Char, 2, "D19");
            ocm.Parameters.Add("@D20", SqlDbType.Char, 2, "D20");
            ocm.Parameters.Add("@D21", SqlDbType.Char, 2, "D21");
            ocm.Parameters.Add("@D22", SqlDbType.Char, 2, "D22");
            ocm.Parameters.Add("@D23", SqlDbType.Char, 2, "D23");
            ocm.Parameters.Add("@D24", SqlDbType.Char, 2, "D24");
            ocm.Parameters.Add("@D25", SqlDbType.Char, 2, "D25");
            ocm.Parameters.Add("@D26", SqlDbType.Char, 2, "D26");
            ocm.Parameters.Add("@D27", SqlDbType.Char, 2, "D27");
            ocm.Parameters.Add("@D28", SqlDbType.Char, 2, "D28");
            ocm.Parameters.Add("@D29", SqlDbType.Char, 2, "D29");
            ocm.Parameters.Add("@D30", SqlDbType.Char, 2, "D30");
            ocm.Parameters.Add("@D31", SqlDbType.Char, 2, "D31");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_TRSDANGDelCmd()
        {
            string queryStatements = "DELETE DBO.DUTY_TRSDANG"
                                   + " WHERE SAWON_NO = @SAWON_NO"
                                   + "   AND PLANYYMM = @PLANYYMM"
                                   + "   AND DEPTCODE = @DEPTCODE"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@SAWON_NO", SqlDbType.VarChar, 15, "SAWON_NO").SourceVersion = DataRowVersion.Original;
            ocm.Parameters.Add("@PLANYYMM", SqlDbType.Char, 6, "PLANYYMM").SourceVersion = DataRowVersion.Original;
            ocm.Parameters.Add("@DEPTCODE", SqlDbType.Char, 4, "DEPTCODE").SourceVersion = DataRowVersion.Original;

            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion
				
		#region D_DUTY_INFODANG delete command

        private SqlCommand GetD_DUTY_INFODANGDelCmd()

        {
            string queryStatements = "DELETE DBO.DUTY_INFODANG"
                                   + " WHERE DEPTCODE = @DEPTCODE"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;
			
            ocm.Parameters.Add("@DEPTCODE", SqlDbType.Char, 4, "DEPTCODE").SourceVersion = DataRowVersion.Original;
            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion

		#region DUTY_INFODANG insert command

        private SqlCommand GetDUTY_INFODANGInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_INFODANG(DEPTCODE, INDT, UPDT, USID, PSTY"
                                   + ") VALUES (@DEPTCODE, @INDT, @UPDT, @USID, @PSTY"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@DEPTCODE", SqlDbType.Char, 4, "DEPTCODE");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }		

        #endregion
				
		#region D_DUTY_INFOFXOT delete command

        private SqlCommand GetD_DUTY_INFOFXOTDelCmd()

        {
            string queryStatements = "DELETE DBO.DUTY_INFOFXOT"
                                   + " WHERE DEPTCODE = @DEPTCODE"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;
			
            ocm.Parameters.Add("@DEPTCODE", SqlDbType.Char, 4, "DEPTCODE").SourceVersion = DataRowVersion.Original;
            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion

		#region DUTY_INFOFXOT insert command

        private SqlCommand GetDUTY_INFOFXOTInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_INFOFXOT(DEPTCODE, INDT, UPDT, USID, PSTY"
                                   + ") VALUES (@DEPTCODE, @INDT, @UPDT, @USID, @PSTY"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@DEPTCODE", SqlDbType.Char, 4, "DEPTCODE");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }		

        #endregion

		#region DUTY_TRSPLAN insert, update,delete command

        private SqlCommand GetDUTY_TRSPLANInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_TRSPLAN(SAWON_NO, PLANYYMM, PLAN_SQ, DEPTCODE, BF_NIGHT, BF_OFF, MAX_NCNT, ALLOW_OFF, REMAIN_NIGHT, REMAIN_OFF, MM_CNT1, MM_CNT2, MM_CNT3, MM_CNT4, MM_CNT5, MM_CNT6, D01, D02, D03, D04, D05, D06, D07, D08, D09, D10, D11, D12, D13, D14, D15, D16, D17, D18, D19, D20, D21, D22, D23, D24, D25, D26, D27, D28, D29, D30, D31, INDT, UPDT, USID, PSTY"
                                   + ") VALUES (@SAWON_NO, @PLANYYMM, @PLAN_SQ, @DEPTCODE, @BF_NIGHT, @BF_OFF, @MAX_NCNT, @ALLOW_OFF, @REMAIN_NIGHT, @REMAIN_OFF, @MM_CNT1, @MM_CNT2, @MM_CNT3, @MM_CNT4, @MM_CNT5, @MM_CNT6, @D01, @D02, @D03, @D04, @D05, @D06, @D07, @D08, @D09, @D10, @D11, @D12, @D13, @D14, @D15, @D16, @D17, @D18, @D19, @D20, @D21, @D22, @D23, @D24, @D25, @D26, @D27, @D28, @D29, @D30, @D31, @INDT, @UPDT, @USID, @PSTY"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@SAWON_NO", SqlDbType.VarChar, 15, "SAWON_NO");
            ocm.Parameters.Add("@PLANYYMM", SqlDbType.Char, 6, "PLANYYMM");
            ocm.Parameters.Add("@PLAN_SQ", SqlDbType.Int, 4, "PLAN_SQ");
            ocm.Parameters.Add("@DEPTCODE", SqlDbType.Char, 4, "DEPTCODE");
            ocm.Parameters.Add("@BF_NIGHT", SqlDbType.Int, 4, "BF_NIGHT");
            ocm.Parameters.Add("@BF_OFF", SqlDbType.Int, 4, "BF_OFF");
            ocm.Parameters.Add("@MAX_NCNT", SqlDbType.Int, 4, "MAX_NCNT");
            ocm.Parameters.Add("@ALLOW_OFF", SqlDbType.Int, 4, "ALLOW_OFF");
            ocm.Parameters.Add("@REMAIN_NIGHT", SqlDbType.Int, 4, "REMAIN_NIGHT");
            ocm.Parameters.Add("@REMAIN_OFF", SqlDbType.Int, 4, "REMAIN_OFF");
            ocm.Parameters.Add("@MM_CNT1", SqlDbType.Decimal, 9, "MM_CNT1");
            ocm.Parameters.Add("@MM_CNT2", SqlDbType.Decimal, 9, "MM_CNT2");
            ocm.Parameters.Add("@MM_CNT3", SqlDbType.Decimal, 9, "MM_CNT3");
            ocm.Parameters.Add("@MM_CNT4", SqlDbType.Decimal, 9, "MM_CNT4");
            ocm.Parameters.Add("@MM_CNT5", SqlDbType.Decimal, 9, "MM_CNT5");
            ocm.Parameters.Add("@MM_CNT6", SqlDbType.Decimal, 9, "MM_CNT6");
            ocm.Parameters.Add("@D01", SqlDbType.Char, 2, "D01");
            ocm.Parameters.Add("@D02", SqlDbType.Char, 2, "D02");
            ocm.Parameters.Add("@D03", SqlDbType.Char, 2, "D03");
            ocm.Parameters.Add("@D04", SqlDbType.Char, 2, "D04");
            ocm.Parameters.Add("@D05", SqlDbType.Char, 2, "D05");
            ocm.Parameters.Add("@D06", SqlDbType.Char, 2, "D06");
            ocm.Parameters.Add("@D07", SqlDbType.Char, 2, "D07");
            ocm.Parameters.Add("@D08", SqlDbType.Char, 2, "D08");
            ocm.Parameters.Add("@D09", SqlDbType.Char, 2, "D09");
            ocm.Parameters.Add("@D10", SqlDbType.Char, 2, "D10");
            ocm.Parameters.Add("@D11", SqlDbType.Char, 2, "D11");
            ocm.Parameters.Add("@D12", SqlDbType.Char, 2, "D12");
            ocm.Parameters.Add("@D13", SqlDbType.Char, 2, "D13");
            ocm.Parameters.Add("@D14", SqlDbType.Char, 2, "D14");
            ocm.Parameters.Add("@D15", SqlDbType.Char, 2, "D15");
            ocm.Parameters.Add("@D16", SqlDbType.Char, 2, "D16");
            ocm.Parameters.Add("@D17", SqlDbType.Char, 2, "D17");
            ocm.Parameters.Add("@D18", SqlDbType.Char, 2, "D18");
            ocm.Parameters.Add("@D19", SqlDbType.Char, 2, "D19");
            ocm.Parameters.Add("@D20", SqlDbType.Char, 2, "D20");
            ocm.Parameters.Add("@D21", SqlDbType.Char, 2, "D21");
            ocm.Parameters.Add("@D22", SqlDbType.Char, 2, "D22");
            ocm.Parameters.Add("@D23", SqlDbType.Char, 2, "D23");
            ocm.Parameters.Add("@D24", SqlDbType.Char, 2, "D24");
            ocm.Parameters.Add("@D25", SqlDbType.Char, 2, "D25");
            ocm.Parameters.Add("@D26", SqlDbType.Char, 2, "D26");
            ocm.Parameters.Add("@D27", SqlDbType.Char, 2, "D27");
            ocm.Parameters.Add("@D28", SqlDbType.Char, 2, "D28");
            ocm.Parameters.Add("@D29", SqlDbType.Char, 2, "D29");
            ocm.Parameters.Add("@D30", SqlDbType.Char, 2, "D30");
            ocm.Parameters.Add("@D31", SqlDbType.Char, 2, "D31");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_TRSPLANUpCmd()
        {
            string queryStatements = "UPDATE DBO.DUTY_TRSPLAN SET "
                                   + "   PLAN_SQ = @PLAN_SQ, "
                                   + "   BF_NIGHT = @BF_NIGHT, "
                                   + "   BF_OFF = @BF_OFF, "
                                   + "   MAX_NCNT = @MAX_NCNT, "
                                   + "   ALLOW_OFF = @ALLOW_OFF, "
                                   + "   REMAIN_NIGHT = @REMAIN_NIGHT, "
                                   + "   REMAIN_OFF = @REMAIN_OFF, "
                                   + "   MM_CNT1 = @MM_CNT1, "
                                   + "   MM_CNT2 = @MM_CNT2, "
                                   + "   MM_CNT3 = @MM_CNT3, "
                                   + "   MM_CNT4 = @MM_CNT4, "
                                   + "   MM_CNT5 = @MM_CNT5, "
                                   + "   MM_CNT6 = @MM_CNT6, "
                                   + "   D01 = @D01, "
                                   + "   D02 = @D02, "
                                   + "   D03 = @D03, "
                                   + "   D04 = @D04, "
                                   + "   D05 = @D05, "
                                   + "   D06 = @D06, "
                                   + "   D07 = @D07, "
                                   + "   D08 = @D08, "
                                   + "   D09 = @D09, "
                                   + "   D10 = @D10, "
                                   + "   D11 = @D11, "
                                   + "   D12 = @D12, "
                                   + "   D13 = @D13, "
                                   + "   D14 = @D14, "
                                   + "   D15 = @D15, "
                                   + "   D16 = @D16, "
                                   + "   D17 = @D17, "
                                   + "   D18 = @D18, "
                                   + "   D19 = @D19, "
                                   + "   D20 = @D20, "
                                   + "   D21 = @D21, "
                                   + "   D22 = @D22, "
                                   + "   D23 = @D23, "
                                   + "   D24 = @D24, "
                                   + "   D25 = @D25, "
                                   + "   D26 = @D26, "
                                   + "   D27 = @D27, "
                                   + "   D28 = @D28, "
                                   + "   D29 = @D29, "
                                   + "   D30 = @D30, "
                                   + "   D31 = @D31, "
                                   + "   INDT = @INDT, "
                                   + "   UPDT = @UPDT, "
                                   + "   USID = @USID, "
                                   + "   PSTY = @PSTY"
                                   + " WHERE SAWON_NO = @SAWON_NO"
                                   + "   AND PLANYYMM = @PLANYYMM"
                                   + "   AND DEPTCODE = @DEPTCODE"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@SAWON_NO", SqlDbType.VarChar, 15, "SAWON_NO");
            ocm.Parameters.Add("@PLANYYMM", SqlDbType.Char, 6, "PLANYYMM");
            ocm.Parameters.Add("@PLAN_SQ", SqlDbType.Int, 4, "PLAN_SQ");
            ocm.Parameters.Add("@DEPTCODE", SqlDbType.Char, 4, "DEPTCODE");
            ocm.Parameters.Add("@BF_NIGHT", SqlDbType.Int, 4, "BF_NIGHT");
            ocm.Parameters.Add("@BF_OFF", SqlDbType.Int, 4, "BF_OFF");
            ocm.Parameters.Add("@MAX_NCNT", SqlDbType.Int, 4, "MAX_NCNT");
            ocm.Parameters.Add("@ALLOW_OFF", SqlDbType.Int, 4, "ALLOW_OFF");
            ocm.Parameters.Add("@REMAIN_NIGHT", SqlDbType.Int, 4, "REMAIN_NIGHT");
            ocm.Parameters.Add("@REMAIN_OFF", SqlDbType.Int, 4, "REMAIN_OFF");
            ocm.Parameters.Add("@MM_CNT1", SqlDbType.Decimal, 9, "MM_CNT1");
            ocm.Parameters.Add("@MM_CNT2", SqlDbType.Decimal, 9, "MM_CNT2");
            ocm.Parameters.Add("@MM_CNT3", SqlDbType.Decimal, 9, "MM_CNT3");
            ocm.Parameters.Add("@MM_CNT4", SqlDbType.Decimal, 9, "MM_CNT4");
            ocm.Parameters.Add("@MM_CNT5", SqlDbType.Decimal, 9, "MM_CNT5");
            ocm.Parameters.Add("@MM_CNT6", SqlDbType.Decimal, 9, "MM_CNT6");
            ocm.Parameters.Add("@D01", SqlDbType.Char, 2, "D01");
            ocm.Parameters.Add("@D02", SqlDbType.Char, 2, "D02");
            ocm.Parameters.Add("@D03", SqlDbType.Char, 2, "D03");
            ocm.Parameters.Add("@D04", SqlDbType.Char, 2, "D04");
            ocm.Parameters.Add("@D05", SqlDbType.Char, 2, "D05");
            ocm.Parameters.Add("@D06", SqlDbType.Char, 2, "D06");
            ocm.Parameters.Add("@D07", SqlDbType.Char, 2, "D07");
            ocm.Parameters.Add("@D08", SqlDbType.Char, 2, "D08");
            ocm.Parameters.Add("@D09", SqlDbType.Char, 2, "D09");
            ocm.Parameters.Add("@D10", SqlDbType.Char, 2, "D10");
            ocm.Parameters.Add("@D11", SqlDbType.Char, 2, "D11");
            ocm.Parameters.Add("@D12", SqlDbType.Char, 2, "D12");
            ocm.Parameters.Add("@D13", SqlDbType.Char, 2, "D13");
            ocm.Parameters.Add("@D14", SqlDbType.Char, 2, "D14");
            ocm.Parameters.Add("@D15", SqlDbType.Char, 2, "D15");
            ocm.Parameters.Add("@D16", SqlDbType.Char, 2, "D16");
            ocm.Parameters.Add("@D17", SqlDbType.Char, 2, "D17");
            ocm.Parameters.Add("@D18", SqlDbType.Char, 2, "D18");
            ocm.Parameters.Add("@D19", SqlDbType.Char, 2, "D19");
            ocm.Parameters.Add("@D20", SqlDbType.Char, 2, "D20");
            ocm.Parameters.Add("@D21", SqlDbType.Char, 2, "D21");
            ocm.Parameters.Add("@D22", SqlDbType.Char, 2, "D22");
            ocm.Parameters.Add("@D23", SqlDbType.Char, 2, "D23");
            ocm.Parameters.Add("@D24", SqlDbType.Char, 2, "D24");
            ocm.Parameters.Add("@D25", SqlDbType.Char, 2, "D25");
            ocm.Parameters.Add("@D26", SqlDbType.Char, 2, "D26");
            ocm.Parameters.Add("@D27", SqlDbType.Char, 2, "D27");
            ocm.Parameters.Add("@D28", SqlDbType.Char, 2, "D28");
            ocm.Parameters.Add("@D29", SqlDbType.Char, 2, "D29");
            ocm.Parameters.Add("@D30", SqlDbType.Char, 2, "D30");
            ocm.Parameters.Add("@D31", SqlDbType.Char, 2, "D31");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_TRSPLANDelCmd()
        {
            string queryStatements = "DELETE DBO.DUTY_TRSPLAN"
                                   + " WHERE SAWON_NO = @SAWON_NO"
                                   + "   AND PLANYYMM = @PLANYYMM"
                                   + "   AND DEPTCODE = @DEPTCODE"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@SAWON_NO", SqlDbType.VarChar, 15, "SAWON_NO").SourceVersion = DataRowVersion.Original;
            ocm.Parameters.Add("@PLANYYMM", SqlDbType.Char, 6, "PLANYYMM").SourceVersion = DataRowVersion.Original;
            ocm.Parameters.Add("@DEPTCODE", SqlDbType.Char, 6, "DEPTCODE").SourceVersion = DataRowVersion.Original;

            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion
						
		#region D_DUTY_INFONURS delete command

        private SqlCommand GetD_DUTY_INFONURSDelCmd()

        {
            string queryStatements = "DELETE DBO.DUTY_INFONURS"
                                   + " WHERE DEPTCODE = @DEPTCODE"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;
			
            ocm.Parameters.Add("@DEPTCODE", SqlDbType.Char, 4, "DEPTCODE").SourceVersion = DataRowVersion.Original;
            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion

		#region DUTY_INFONURS insert command

        private SqlCommand GetDUTY_INFONURSInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_INFONURS(DEPTCODE, INDT, UPDT, USID, PSTY"
                                   + ") VALUES (@DEPTCODE, @INDT, @UPDT, @USID, @PSTY"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@DEPTCODE", SqlDbType.Char, 4, "DEPTCODE");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }		

        #endregion

		#region DUTY_MSTCLOS insert, update,delete command

        private SqlCommand GetDUTY_MSTCLOSInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_MSTCLOS(DEPTCODE, PLANYYMM, POS_FRDT, POS_TODT, CLOSE_YN, INDT, UPDT, USID, PSTY"
                                   + ") VALUES (@DEPTCODE, @PLANYYMM, @POS_FRDT, @POS_TODT, @CLOSE_YN, @INDT, @UPDT, @USID, @PSTY"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@DEPTCODE", SqlDbType.Char, 4, "DEPTCODE");
            ocm.Parameters.Add("@PLANYYMM", SqlDbType.Char, 6, "PLANYYMM");
            ocm.Parameters.Add("@POS_FRDT", SqlDbType.Char, 8, "POS_FRDT");
            ocm.Parameters.Add("@POS_TODT", SqlDbType.Char, 8, "POS_TODT");
            ocm.Parameters.Add("@CLOSE_YN", SqlDbType.Char, 1, "CLOSE_YN");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_MSTCLOSUpCmd()

        {
            string queryStatements = "UPDATE DBO.DUTY_MSTCLOS SET "
                                   + "   POS_FRDT = @POS_FRDT, "
                                   + "   POS_TODT = @POS_TODT, "
                                   + "   CLOSE_YN = @CLOSE_YN, "
                                   + "   INDT = @INDT, "
                                   + "   UPDT = @UPDT, "
                                   + "   USID = @USID, "
                                   + "   PSTY = @PSTY"
                                   + " WHERE DEPTCODE = @DEPTCODE"
                                   + "   AND PLANYYMM = @PLANYYMM"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@DEPTCODE", SqlDbType.Char, 4, "DEPTCODE");
            ocm.Parameters.Add("@PLANYYMM", SqlDbType.Char, 6, "PLANYYMM");
            ocm.Parameters.Add("@POS_FRDT", SqlDbType.Char, 8, "POS_FRDT");
            ocm.Parameters.Add("@POS_TODT", SqlDbType.Char, 8, "POS_TODT");
            ocm.Parameters.Add("@CLOSE_YN", SqlDbType.Char, 1, "CLOSE_YN");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_MSTCLOSDelCmd()

        {
            string queryStatements = "DELETE DBO.DUTY_MSTCLOS"
                                   + " WHERE DEPTCODE = @DEPTCODE"
                                   + "   AND PLANYYMM = @PLANYYMM"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@DEPTCODE", SqlDbType.Char, 4, "DEPTCODE").SourceVersion = DataRowVersion.Original;
            ocm.Parameters.Add("@PLANYYMM", SqlDbType.Char, 6, "PLANYYMM").SourceVersion = DataRowVersion.Original;

            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion

		#region DUTY_TRSYCMI insert, update, delete command

        private SqlCommand GetDUTY_TRSYCMIInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_TRSYCMI(YYMM, SABN, SABN_NM, YC_T_CNT, YC_BF_SUM_CNT, YC_THIS_YCNT, YC_THIS_BCNT, YC_NOW_SUM_CNT, YC_REMAIN_CNT, YC_MI_CNT, REG_DT, REG_ID"
                                   + ") VALUES (@YYMM, @SABN, @SABN_NM, @YC_T_CNT, @YC_BF_SUM_CNT, @YC_THIS_YCNT, @YC_THIS_BCNT, @YC_NOW_SUM_CNT, @YC_REMAIN_CNT, @YC_MI_CNT, @REG_DT, @REG_ID"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@YYMM", SqlDbType.Char, 6, "YYMM");
            ocm.Parameters.Add("@SABN", SqlDbType.VarChar, 20, "SABN");
            ocm.Parameters.Add("@SABN_NM", SqlDbType.VarChar, 20, "SABN_NM");
            ocm.Parameters.Add("@YC_T_CNT", SqlDbType.Decimal, 5, "YC_T_CNT");
            ocm.Parameters.Add("@YC_BF_SUM_CNT", SqlDbType.Decimal, 5, "YC_BF_SUM_CNT");
            ocm.Parameters.Add("@YC_THIS_YCNT", SqlDbType.Decimal, 5, "YC_THIS_YCNT");
            ocm.Parameters.Add("@YC_THIS_BCNT", SqlDbType.Decimal, 5, "YC_THIS_BCNT");
            ocm.Parameters.Add("@YC_NOW_SUM_CNT", SqlDbType.Decimal, 5, "YC_NOW_SUM_CNT");
            ocm.Parameters.Add("@YC_REMAIN_CNT", SqlDbType.Decimal, 5, "YC_REMAIN_CNT");
            ocm.Parameters.Add("@YC_MI_CNT", SqlDbType.Decimal, 5, "YC_MI_CNT");
            ocm.Parameters.Add("@REG_DT", SqlDbType.VarChar, 20, "REG_DT");
            ocm.Parameters.Add("@REG_ID", SqlDbType.VarChar, 20, "REG_ID");

            return ocm;
        }

        private SqlCommand GetDUTY_TRSYCMIUpCmd()

        {
            string queryStatements = "UPDATE DBO.DUTY_TRSYCMI SET "
                                   + "   SABN_NM = @SABN_NM, "
                                   + "   YC_T_CNT = @YC_T_CNT, "
                                   + "   YC_BF_SUM_CNT = @YC_BF_SUM_CNT, "
                                   + "   YC_THIS_YCNT = @YC_THIS_YCNT, "
                                   + "   YC_THIS_BCNT = @YC_THIS_BCNT, "
                                   + "   YC_NOW_SUM_CNT = @YC_NOW_SUM_CNT, "
                                   + "   YC_REMAIN_CNT = @YC_REMAIN_CNT, "
                                   + "   YC_MI_CNT = @YC_MI_CNT, "
                                   + "   REG_DT = @REG_DT, "
                                   + "   REG_ID = @REG_ID "
                                   + " WHERE YYMM = @YYMM"
                                   + "   AND SABN = @SABN"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@YYMM", SqlDbType.Char, 6, "YYMM");
            ocm.Parameters.Add("@SABN", SqlDbType.VarChar, 20, "SABN");
            ocm.Parameters.Add("@SABN_NM", SqlDbType.VarChar, 20, "SABN_NM");
            ocm.Parameters.Add("@YC_T_CNT", SqlDbType.Decimal, 5, "YC_T_CNT");
            ocm.Parameters.Add("@YC_BF_SUM_CNT", SqlDbType.Decimal, 5, "YC_BF_SUM_CNT");
            ocm.Parameters.Add("@YC_THIS_YCNT", SqlDbType.Decimal, 5, "YC_THIS_YCNT");
            ocm.Parameters.Add("@YC_THIS_BCNT", SqlDbType.Decimal, 5, "YC_THIS_BCNT");
            ocm.Parameters.Add("@YC_NOW_SUM_CNT", SqlDbType.Decimal, 5, "YC_NOW_SUM_CNT");
            ocm.Parameters.Add("@YC_REMAIN_CNT", SqlDbType.Decimal, 5, "YC_REMAIN_CNT");
            ocm.Parameters.Add("@YC_MI_CNT", SqlDbType.Decimal, 5, "YC_MI_CNT");
            ocm.Parameters.Add("@REG_DT", SqlDbType.VarChar, 20, "REG_DT");
            ocm.Parameters.Add("@REG_ID", SqlDbType.VarChar, 20, "REG_ID");

            return ocm;
        }

        private SqlCommand GetDUTY_TRSYCMIDelCmd()

        {
            string queryStatements = "DELETE DBO.DUTY_TRSYCMI"
                                   + " WHERE YYMM = @YYMM"
                                   + "   AND SABN = @SABN"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@YYMM", SqlDbType.Char, 6, "YYMM").SourceVersion = DataRowVersion.Original;
            ocm.Parameters.Add("@SABN", SqlDbType.VarChar, 20, "SABN").SourceVersion = DataRowVersion.Original;

            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion

		#region DUTY_MSTENDS insert, update,delete command

        private SqlCommand GetDUTY_MSTENDSInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_MSTENDS(END_YYMM, CLOSE_YN, END_DT, END_ID, CANC_DT, CANC_ID"
                                   + ") VALUES (@END_YYMM, @CLOSE_YN, @END_DT, @END_ID, @CANC_DT, @CANC_ID"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@END_YYMM", SqlDbType.Char, 6, "END_YYMM");
            ocm.Parameters.Add("@CLOSE_YN", SqlDbType.Char, 1, "CLOSE_YN");
            ocm.Parameters.Add("@END_DT", SqlDbType.VarChar, 20, "END_DT");
            ocm.Parameters.Add("@END_ID", SqlDbType.VarChar, 20, "END_ID");
            ocm.Parameters.Add("@CANC_DT", SqlDbType.VarChar, 20, "CANC_DT");
            ocm.Parameters.Add("@CANC_ID", SqlDbType.VarChar, 20, "CANC_ID");

            return ocm;
        }

        private SqlCommand GetDUTY_MSTENDSUpCmd()

        {
            string queryStatements = "UPDATE DBO.DUTY_MSTENDS SET "
                                   + "   CLOSE_YN = @CLOSE_YN, "
                                   + "   END_DT = @END_DT, "
                                   + "   END_ID = @END_ID, "
                                   + "   CANC_DT = @CANC_DT, "
                                   + "   CANC_ID = @CANC_ID "
                                   + " WHERE END_YYMM = @END_YYMM"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@END_YYMM", SqlDbType.Char, 6, "END_YYMM");
            ocm.Parameters.Add("@CLOSE_YN", SqlDbType.Char, 1, "CLOSE_YN");
            ocm.Parameters.Add("@END_DT", SqlDbType.VarChar, 20, "END_DT");
            ocm.Parameters.Add("@END_ID", SqlDbType.VarChar, 20, "END_ID");
            ocm.Parameters.Add("@CANC_DT", SqlDbType.VarChar, 20, "CANC_DT");
            ocm.Parameters.Add("@CANC_ID", SqlDbType.VarChar, 20, "CANC_ID");

            return ocm;
        }

        private SqlCommand GetDUTY_MSTENDSDelCmd()

        {
            string queryStatements = "DELETE DBO.DUTY_MSTENDS"
                                   + " WHERE END_YYMM = @END_YYMM"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@END_YYMM", SqlDbType.Char, 6, "END_YYMM").SourceVersion = DataRowVersion.Original;

            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion

		#region DUTY_MSTENDS_LOG insert command

        private SqlCommand GetDUTY_MSTENDS_LOGInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_MSTENDS_LOG(END_YYMM, CLOSE_YN, REG_DT, REG_ID"
                                   + ") VALUES (@END_YYMM, @CLOSE_YN, @REG_DT, @REG_ID"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@END_YYMM", SqlDbType.Char, 6, "END_YYMM");
            ocm.Parameters.Add("@CLOSE_YN", SqlDbType.Char, 1, "CLOSE_YN");
            ocm.Parameters.Add("@REG_DT", SqlDbType.VarChar, 20, "REG_DT");
            ocm.Parameters.Add("@REG_ID", SqlDbType.VarChar, 20, "REG_ID");

            return ocm;
        }

        #endregion

		#region DUTY_MSTWGPC insert, update, delete command

        private SqlCommand GetDUTY_MSTWGPCInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_MSTWGPC(END_YYMM, SAWON_NO, YYMM, TIME_AMT, NIGHT_AMT, WGPCSD01, WGPCSD02, WGPCSD03, WGPCSD04, WGPCSD05, WGPCSD06, WGPCSD07, WGPCSD08, WGPCSD09, WGPCSD10, WGPCSD11, WGPCSD12, WGPCSD13, WGPCSD14, WGPCSD15, WGPCSD16, WGPCSD17, WGPCSD18, WGPCSD19, WGPCSD20, WGPCSD21, WGPCSD22, WGPCSD23, WGPCSD24, WGPCSD25, WGPCSD26, WGPCSD27, WGPCSD28, WGPCSD29, WGPCSD30, WGPCSD31, WGPCSD32, WGPCSD33, WGPCSD34, WGPCSD35, WGPCSD36, WGPCSD37, WGPCSD38, WGPCSD39, WGPCSD40, WGPCSD41, WGPCSD42, WGPCSD43, WGPCSD44, WGPCSD45, WGPCSD46, WGPCSD47, WGPCSD48, WGPCSD49, WGPCSD50, "
								   + "                             WGPCGT01, WGPCGT02, WGPCGT03, WGPCGT04, WGPCGT05, WGPCGT06, WGPCGT07, WGPCGT08, WGPCGT09, WGPCGT10, WGPCGT11, WGPCGT12, WGPCGT13, WGPCGT14, WGPCGT15, WGPCGT16, WGPCGT17, WGPCGT18, WGPCGT19, WGPCGT20, WGPCGT21, WGPCGT22, WGPCGT23, WGPCGT24, WGPCGT25, WGPCGT26, WGPCGT27, WGPCGT28, WGPCGT29, WGPCGT30, INDT, UPDT, USID, PSTY"
                                   + ") VALUES (@END_YYMM, @SAWON_NO, @YYMM, @TIME_AMT, @NIGHT_AMT, @WGPCSD01, @WGPCSD02, @WGPCSD03, @WGPCSD04, @WGPCSD05, @WGPCSD06, @WGPCSD07, @WGPCSD08, @WGPCSD09, @WGPCSD10, @WGPCSD11, @WGPCSD12, @WGPCSD13, @WGPCSD14, @WGPCSD15, @WGPCSD16, @WGPCSD17, @WGPCSD18, @WGPCSD19, @WGPCSD20, @WGPCSD21, @WGPCSD22, @WGPCSD23, @WGPCSD24, @WGPCSD25, @WGPCSD26, @WGPCSD27, @WGPCSD28, @WGPCSD29, @WGPCSD30, @WGPCSD31, @WGPCSD32, @WGPCSD33, @WGPCSD34, @WGPCSD35, @WGPCSD36, @WGPCSD37, @WGPCSD38, @WGPCSD39, @WGPCSD40, @WGPCSD41, @WGPCSD42, @WGPCSD43, @WGPCSD44, @WGPCSD45, @WGPCSD46, @WGPCSD47, @WGPCSD48, @WGPCSD49, @WGPCSD50, "
								   + "          @WGPCGT01, @WGPCGT02, @WGPCGT03, @WGPCGT04, @WGPCGT05, @WGPCGT06, @WGPCGT07, @WGPCGT08, @WGPCGT09, @WGPCGT10, @WGPCGT11, @WGPCGT12, @WGPCGT13, @WGPCGT14, @WGPCGT15, @WGPCGT16, @WGPCGT17, @WGPCGT18, @WGPCGT19, @WGPCGT20, @WGPCGT21, @WGPCGT22, @WGPCGT23, @WGPCGT24, @WGPCGT25, @WGPCGT26, @WGPCGT27, @WGPCGT28, @WGPCGT29, @WGPCGT30, @INDT, @UPDT, @USID, @PSTY"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;
			
            ocm.Parameters.Add("@END_YYMM", SqlDbType.VarChar, 6, "END_YYMM");
            ocm.Parameters.Add("@SAWON_NO", SqlDbType.VarChar, 15, "SAWON_NO");
            ocm.Parameters.Add("@YYMM", SqlDbType.VarChar, 6, "YYMM");
            ocm.Parameters.Add("@TIME_AMT", SqlDbType.Decimal, 9, "TIME_AMT");
            ocm.Parameters.Add("@NIGHT_AMT", SqlDbType.Decimal, 9, "NIGHT_AMT");
            ocm.Parameters.Add("@WGPCSD01", SqlDbType.Decimal, 9, "WGPCSD01");
            ocm.Parameters.Add("@WGPCSD02", SqlDbType.Decimal, 9, "WGPCSD02");
            ocm.Parameters.Add("@WGPCSD03", SqlDbType.Decimal, 9, "WGPCSD03");
            ocm.Parameters.Add("@WGPCSD04", SqlDbType.Decimal, 9, "WGPCSD04");
            ocm.Parameters.Add("@WGPCSD05", SqlDbType.Decimal, 9, "WGPCSD05");
            ocm.Parameters.Add("@WGPCSD06", SqlDbType.Decimal, 9, "WGPCSD06");
            ocm.Parameters.Add("@WGPCSD07", SqlDbType.Decimal, 9, "WGPCSD07");
            ocm.Parameters.Add("@WGPCSD08", SqlDbType.Decimal, 9, "WGPCSD08");
            ocm.Parameters.Add("@WGPCSD09", SqlDbType.Decimal, 9, "WGPCSD09");
            ocm.Parameters.Add("@WGPCSD10", SqlDbType.Decimal, 9, "WGPCSD10");
            ocm.Parameters.Add("@WGPCSD11", SqlDbType.Decimal, 9, "WGPCSD11");
            ocm.Parameters.Add("@WGPCSD12", SqlDbType.Decimal, 9, "WGPCSD12");
            ocm.Parameters.Add("@WGPCSD13", SqlDbType.Decimal, 9, "WGPCSD13");
            ocm.Parameters.Add("@WGPCSD14", SqlDbType.Decimal, 9, "WGPCSD14");
            ocm.Parameters.Add("@WGPCSD15", SqlDbType.Decimal, 9, "WGPCSD15");
            ocm.Parameters.Add("@WGPCSD16", SqlDbType.Decimal, 9, "WGPCSD16");
            ocm.Parameters.Add("@WGPCSD17", SqlDbType.Decimal, 9, "WGPCSD17");
            ocm.Parameters.Add("@WGPCSD18", SqlDbType.Decimal, 9, "WGPCSD18");
            ocm.Parameters.Add("@WGPCSD19", SqlDbType.Decimal, 9, "WGPCSD19");
            ocm.Parameters.Add("@WGPCSD20", SqlDbType.Decimal, 9, "WGPCSD20");
            ocm.Parameters.Add("@WGPCSD21", SqlDbType.Decimal, 9, "WGPCSD21");
            ocm.Parameters.Add("@WGPCSD22", SqlDbType.Decimal, 9, "WGPCSD22");
            ocm.Parameters.Add("@WGPCSD23", SqlDbType.Decimal, 9, "WGPCSD23");
            ocm.Parameters.Add("@WGPCSD24", SqlDbType.Decimal, 9, "WGPCSD24");
            ocm.Parameters.Add("@WGPCSD25", SqlDbType.Decimal, 9, "WGPCSD25");
            ocm.Parameters.Add("@WGPCSD26", SqlDbType.Decimal, 9, "WGPCSD26");
            ocm.Parameters.Add("@WGPCSD27", SqlDbType.Decimal, 9, "WGPCSD27");
            ocm.Parameters.Add("@WGPCSD28", SqlDbType.Decimal, 9, "WGPCSD28");
            ocm.Parameters.Add("@WGPCSD29", SqlDbType.Decimal, 9, "WGPCSD29");
            ocm.Parameters.Add("@WGPCSD30", SqlDbType.Decimal, 9, "WGPCSD30");
            ocm.Parameters.Add("@WGPCSD31", SqlDbType.Decimal, 9, "WGPCSD31");
            ocm.Parameters.Add("@WGPCSD32", SqlDbType.Decimal, 9, "WGPCSD32");
            ocm.Parameters.Add("@WGPCSD33", SqlDbType.Decimal, 9, "WGPCSD33");
            ocm.Parameters.Add("@WGPCSD34", SqlDbType.Decimal, 9, "WGPCSD34");
            ocm.Parameters.Add("@WGPCSD35", SqlDbType.Decimal, 9, "WGPCSD35");
            ocm.Parameters.Add("@WGPCSD36", SqlDbType.Decimal, 9, "WGPCSD36");
            ocm.Parameters.Add("@WGPCSD37", SqlDbType.Decimal, 9, "WGPCSD37");
            ocm.Parameters.Add("@WGPCSD38", SqlDbType.Decimal, 9, "WGPCSD38");
            ocm.Parameters.Add("@WGPCSD39", SqlDbType.Decimal, 9, "WGPCSD39");
            ocm.Parameters.Add("@WGPCSD40", SqlDbType.Decimal, 9, "WGPCSD40");
            ocm.Parameters.Add("@WGPCSD41", SqlDbType.Decimal, 9, "WGPCSD41");
            ocm.Parameters.Add("@WGPCSD42", SqlDbType.Decimal, 9, "WGPCSD42");
            ocm.Parameters.Add("@WGPCSD43", SqlDbType.Decimal, 9, "WGPCSD43");
            ocm.Parameters.Add("@WGPCSD44", SqlDbType.Decimal, 9, "WGPCSD44");
            ocm.Parameters.Add("@WGPCSD45", SqlDbType.Decimal, 9, "WGPCSD45");
            ocm.Parameters.Add("@WGPCSD46", SqlDbType.Decimal, 9, "WGPCSD46");
            ocm.Parameters.Add("@WGPCSD47", SqlDbType.Decimal, 9, "WGPCSD47");
            ocm.Parameters.Add("@WGPCSD48", SqlDbType.Decimal, 9, "WGPCSD48");
            ocm.Parameters.Add("@WGPCSD49", SqlDbType.Decimal, 9, "WGPCSD49");
            ocm.Parameters.Add("@WGPCSD50", SqlDbType.Decimal, 9, "WGPCSD50");
            ocm.Parameters.Add("@WGPCGT01", SqlDbType.Decimal, 9, "WGPCGT01");
            ocm.Parameters.Add("@WGPCGT02", SqlDbType.Decimal, 9, "WGPCGT02");
            ocm.Parameters.Add("@WGPCGT03", SqlDbType.Decimal, 9, "WGPCGT03");
            ocm.Parameters.Add("@WGPCGT04", SqlDbType.Decimal, 9, "WGPCGT04");
            ocm.Parameters.Add("@WGPCGT05", SqlDbType.Decimal, 9, "WGPCGT05");
            ocm.Parameters.Add("@WGPCGT06", SqlDbType.Decimal, 9, "WGPCGT06");
            ocm.Parameters.Add("@WGPCGT07", SqlDbType.Decimal, 9, "WGPCGT07");
            ocm.Parameters.Add("@WGPCGT08", SqlDbType.Decimal, 9, "WGPCGT08");
            ocm.Parameters.Add("@WGPCGT09", SqlDbType.Decimal, 9, "WGPCGT09");
            ocm.Parameters.Add("@WGPCGT10", SqlDbType.Decimal, 9, "WGPCGT10");
            ocm.Parameters.Add("@WGPCGT11", SqlDbType.Decimal, 9, "WGPCGT11");
            ocm.Parameters.Add("@WGPCGT12", SqlDbType.Decimal, 9, "WGPCGT12");
            ocm.Parameters.Add("@WGPCGT13", SqlDbType.Decimal, 9, "WGPCGT13");
            ocm.Parameters.Add("@WGPCGT14", SqlDbType.Decimal, 9, "WGPCGT14");
            ocm.Parameters.Add("@WGPCGT15", SqlDbType.Decimal, 9, "WGPCGT15");
            ocm.Parameters.Add("@WGPCGT16", SqlDbType.Decimal, 9, "WGPCGT16");
            ocm.Parameters.Add("@WGPCGT17", SqlDbType.Decimal, 9, "WGPCGT17");
            ocm.Parameters.Add("@WGPCGT18", SqlDbType.Decimal, 9, "WGPCGT18");
            ocm.Parameters.Add("@WGPCGT19", SqlDbType.Decimal, 9, "WGPCGT19");
            ocm.Parameters.Add("@WGPCGT20", SqlDbType.Decimal, 9, "WGPCGT20");
            ocm.Parameters.Add("@WGPCGT21", SqlDbType.Decimal, 9, "WGPCGT21");
            ocm.Parameters.Add("@WGPCGT22", SqlDbType.Decimal, 9, "WGPCGT22");
            ocm.Parameters.Add("@WGPCGT23", SqlDbType.Decimal, 9, "WGPCGT23");
            ocm.Parameters.Add("@WGPCGT24", SqlDbType.Decimal, 9, "WGPCGT24");
            ocm.Parameters.Add("@WGPCGT25", SqlDbType.Decimal, 9, "WGPCGT25");
            ocm.Parameters.Add("@WGPCGT26", SqlDbType.Decimal, 9, "WGPCGT26");
            ocm.Parameters.Add("@WGPCGT27", SqlDbType.Decimal, 9, "WGPCGT27");
            ocm.Parameters.Add("@WGPCGT28", SqlDbType.Decimal, 9, "WGPCGT28");
            ocm.Parameters.Add("@WGPCGT29", SqlDbType.Decimal, 9, "WGPCGT29");
            ocm.Parameters.Add("@WGPCGT30", SqlDbType.Decimal, 9, "WGPCGT30");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_MSTWGPCUpCmd()

        {
            string queryStatements = "UPDATE DBO.DUTY_MSTWGPC SET "
                                   + "   TIME_AMT = @TIME_AMT, "
                                   + "   NIGHT_AMT = @NIGHT_AMT, "
                                   + "   WGPCSD01 = @WGPCSD01, "
                                   + "   WGPCSD02 = @WGPCSD02, "
                                   + "   WGPCSD03 = @WGPCSD03, "
                                   + "   WGPCSD04 = @WGPCSD04, "
                                   + "   WGPCSD05 = @WGPCSD05, "
                                   + "   WGPCSD06 = @WGPCSD06, "
                                   + "   WGPCSD07 = @WGPCSD07, "
                                   + "   WGPCSD08 = @WGPCSD08, "
                                   + "   WGPCSD09 = @WGPCSD09, "
                                   + "   WGPCSD10 = @WGPCSD10, "
                                   + "   WGPCSD11 = @WGPCSD11, "
                                   + "   WGPCSD12 = @WGPCSD12, "
                                   + "   WGPCSD13 = @WGPCSD13, "
                                   + "   WGPCSD14 = @WGPCSD14, "
                                   + "   WGPCSD15 = @WGPCSD15, "
                                   + "   WGPCSD16 = @WGPCSD16, "
                                   + "   WGPCSD17 = @WGPCSD17, "
                                   + "   WGPCSD18 = @WGPCSD18, "
                                   + "   WGPCSD19 = @WGPCSD19, "
                                   + "   WGPCSD20 = @WGPCSD20, "
                                   + "   WGPCSD21 = @WGPCSD21, "
                                   + "   WGPCSD22 = @WGPCSD22, "
                                   + "   WGPCSD23 = @WGPCSD23, "
                                   + "   WGPCSD24 = @WGPCSD24, "
                                   + "   WGPCSD25 = @WGPCSD25, "
                                   + "   WGPCSD26 = @WGPCSD26, "
                                   + "   WGPCSD27 = @WGPCSD27, "
                                   + "   WGPCSD28 = @WGPCSD28, "
                                   + "   WGPCSD29 = @WGPCSD29, "
                                   + "   WGPCSD30 = @WGPCSD30, "
                                   + "   WGPCSD31 = @WGPCSD31, "
                                   + "   WGPCSD32 = @WGPCSD32, "
                                   + "   WGPCSD33 = @WGPCSD33, "
                                   + "   WGPCSD34 = @WGPCSD34, "
                                   + "   WGPCSD35 = @WGPCSD35, "
                                   + "   WGPCSD36 = @WGPCSD36, "
                                   + "   WGPCSD37 = @WGPCSD37, "
                                   + "   WGPCSD38 = @WGPCSD38, "
                                   + "   WGPCSD39 = @WGPCSD39, "
                                   + "   WGPCSD40 = @WGPCSD40, "
                                   + "   WGPCSD41 = @WGPCSD41, "
                                   + "   WGPCSD42 = @WGPCSD42, "
                                   + "   WGPCSD43 = @WGPCSD43, "
                                   + "   WGPCSD44 = @WGPCSD44, "
                                   + "   WGPCSD45 = @WGPCSD45, "
                                   + "   WGPCSD46 = @WGPCSD46, "
                                   + "   WGPCSD47 = @WGPCSD47, "
                                   + "   WGPCSD48 = @WGPCSD48, "
                                   + "   WGPCSD49 = @WGPCSD49, "
                                   + "   WGPCSD50 = @WGPCSD50, "
                                   + "   WGPCGT01 = @WGPCGT01, "
                                   + "   WGPCGT02 = @WGPCGT02, "
                                   + "   WGPCGT03 = @WGPCGT03, "
                                   + "   WGPCGT04 = @WGPCGT04, "
                                   + "   WGPCGT05 = @WGPCGT05, "
                                   + "   WGPCGT06 = @WGPCGT06, "
                                   + "   WGPCGT07 = @WGPCGT07, "
                                   + "   WGPCGT08 = @WGPCGT08, "
                                   + "   WGPCGT09 = @WGPCGT09, "
                                   + "   WGPCGT10 = @WGPCGT10, "
                                   + "   WGPCGT11 = @WGPCGT11, "
                                   + "   WGPCGT12 = @WGPCGT12, "
                                   + "   WGPCGT13 = @WGPCGT13, "
                                   + "   WGPCGT14 = @WGPCGT14, "
                                   + "   WGPCGT15 = @WGPCGT15, "
                                   + "   WGPCGT16 = @WGPCGT16, "
                                   + "   WGPCGT17 = @WGPCGT17, "
                                   + "   WGPCGT18 = @WGPCGT18, "
                                   + "   WGPCGT19 = @WGPCGT19, "
                                   + "   WGPCGT20 = @WGPCGT20, "
                                   + "   WGPCGT21 = @WGPCGT21, "
                                   + "   WGPCGT22 = @WGPCGT22, "
                                   + "   WGPCGT23 = @WGPCGT23, "
                                   + "   WGPCGT24 = @WGPCGT24, "
                                   + "   WGPCGT25 = @WGPCGT25, "
                                   + "   WGPCGT26 = @WGPCGT26, "
                                   + "   WGPCGT27 = @WGPCGT27, "
                                   + "   WGPCGT28 = @WGPCGT28, "
                                   + "   WGPCGT29 = @WGPCGT29, "
                                   + "   WGPCGT30 = @WGPCGT30, "
                                   + "   INDT = @INDT, "
                                   + "   UPDT = @UPDT, "
                                   + "   USID = @USID, "
                                   + "   PSTY = @PSTY"
                                   + " WHERE END_YYMM = @END_YYMM "
                                   + "   AND SAWON_NO = @SAWON_NO "
                                   + "   AND YYMM = @YYMM "
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@END_YYMM", SqlDbType.VarChar, 6, "END_YYMM");
            ocm.Parameters.Add("@SAWON_NO", SqlDbType.VarChar, 15, "SAWON_NO");
            ocm.Parameters.Add("@YYMM", SqlDbType.VarChar, 6, "YYMM");
            ocm.Parameters.Add("@TIME_AMT", SqlDbType.Decimal, 9, "TIME_AMT");
            ocm.Parameters.Add("@NIGHT_AMT", SqlDbType.Decimal, 9, "NIGHT_AMT");
            ocm.Parameters.Add("@WGPCSD01", SqlDbType.Decimal, 9, "WGPCSD01");
            ocm.Parameters.Add("@WGPCSD02", SqlDbType.Decimal, 9, "WGPCSD02");
            ocm.Parameters.Add("@WGPCSD03", SqlDbType.Decimal, 9, "WGPCSD03");
            ocm.Parameters.Add("@WGPCSD04", SqlDbType.Decimal, 9, "WGPCSD04");
            ocm.Parameters.Add("@WGPCSD05", SqlDbType.Decimal, 9, "WGPCSD05");
            ocm.Parameters.Add("@WGPCSD06", SqlDbType.Decimal, 9, "WGPCSD06");
            ocm.Parameters.Add("@WGPCSD07", SqlDbType.Decimal, 9, "WGPCSD07");
            ocm.Parameters.Add("@WGPCSD08", SqlDbType.Decimal, 9, "WGPCSD08");
            ocm.Parameters.Add("@WGPCSD09", SqlDbType.Decimal, 9, "WGPCSD09");
            ocm.Parameters.Add("@WGPCSD10", SqlDbType.Decimal, 9, "WGPCSD10");
            ocm.Parameters.Add("@WGPCSD11", SqlDbType.Decimal, 9, "WGPCSD11");
            ocm.Parameters.Add("@WGPCSD12", SqlDbType.Decimal, 9, "WGPCSD12");
            ocm.Parameters.Add("@WGPCSD13", SqlDbType.Decimal, 9, "WGPCSD13");
            ocm.Parameters.Add("@WGPCSD14", SqlDbType.Decimal, 9, "WGPCSD14");
            ocm.Parameters.Add("@WGPCSD15", SqlDbType.Decimal, 9, "WGPCSD15");
            ocm.Parameters.Add("@WGPCSD16", SqlDbType.Decimal, 9, "WGPCSD16");
            ocm.Parameters.Add("@WGPCSD17", SqlDbType.Decimal, 9, "WGPCSD17");
            ocm.Parameters.Add("@WGPCSD18", SqlDbType.Decimal, 9, "WGPCSD18");
            ocm.Parameters.Add("@WGPCSD19", SqlDbType.Decimal, 9, "WGPCSD19");
            ocm.Parameters.Add("@WGPCSD20", SqlDbType.Decimal, 9, "WGPCSD20");
            ocm.Parameters.Add("@WGPCSD21", SqlDbType.Decimal, 9, "WGPCSD21");
            ocm.Parameters.Add("@WGPCSD22", SqlDbType.Decimal, 9, "WGPCSD22");
            ocm.Parameters.Add("@WGPCSD23", SqlDbType.Decimal, 9, "WGPCSD23");
            ocm.Parameters.Add("@WGPCSD24", SqlDbType.Decimal, 9, "WGPCSD24");
            ocm.Parameters.Add("@WGPCSD25", SqlDbType.Decimal, 9, "WGPCSD25");
            ocm.Parameters.Add("@WGPCSD26", SqlDbType.Decimal, 9, "WGPCSD26");
            ocm.Parameters.Add("@WGPCSD27", SqlDbType.Decimal, 9, "WGPCSD27");
            ocm.Parameters.Add("@WGPCSD28", SqlDbType.Decimal, 9, "WGPCSD28");
            ocm.Parameters.Add("@WGPCSD29", SqlDbType.Decimal, 9, "WGPCSD29");
            ocm.Parameters.Add("@WGPCSD30", SqlDbType.Decimal, 9, "WGPCSD30");
            ocm.Parameters.Add("@WGPCSD31", SqlDbType.Decimal, 9, "WGPCSD31");
            ocm.Parameters.Add("@WGPCSD32", SqlDbType.Decimal, 9, "WGPCSD32");
            ocm.Parameters.Add("@WGPCSD33", SqlDbType.Decimal, 9, "WGPCSD33");
            ocm.Parameters.Add("@WGPCSD34", SqlDbType.Decimal, 9, "WGPCSD34");
            ocm.Parameters.Add("@WGPCSD35", SqlDbType.Decimal, 9, "WGPCSD35");
            ocm.Parameters.Add("@WGPCSD36", SqlDbType.Decimal, 9, "WGPCSD36");
            ocm.Parameters.Add("@WGPCSD37", SqlDbType.Decimal, 9, "WGPCSD37");
            ocm.Parameters.Add("@WGPCSD38", SqlDbType.Decimal, 9, "WGPCSD38");
            ocm.Parameters.Add("@WGPCSD39", SqlDbType.Decimal, 9, "WGPCSD39");
            ocm.Parameters.Add("@WGPCSD40", SqlDbType.Decimal, 9, "WGPCSD40");
            ocm.Parameters.Add("@WGPCSD41", SqlDbType.Decimal, 9, "WGPCSD41");
            ocm.Parameters.Add("@WGPCSD42", SqlDbType.Decimal, 9, "WGPCSD42");
            ocm.Parameters.Add("@WGPCSD43", SqlDbType.Decimal, 9, "WGPCSD43");
            ocm.Parameters.Add("@WGPCSD44", SqlDbType.Decimal, 9, "WGPCSD44");
            ocm.Parameters.Add("@WGPCSD45", SqlDbType.Decimal, 9, "WGPCSD45");
            ocm.Parameters.Add("@WGPCSD46", SqlDbType.Decimal, 9, "WGPCSD46");
            ocm.Parameters.Add("@WGPCSD47", SqlDbType.Decimal, 9, "WGPCSD47");
            ocm.Parameters.Add("@WGPCSD48", SqlDbType.Decimal, 9, "WGPCSD48");
            ocm.Parameters.Add("@WGPCSD49", SqlDbType.Decimal, 9, "WGPCSD49");
            ocm.Parameters.Add("@WGPCSD50", SqlDbType.Decimal, 9, "WGPCSD50");
            ocm.Parameters.Add("@WGPCGT01", SqlDbType.Decimal, 9, "WGPCGT01");
            ocm.Parameters.Add("@WGPCGT02", SqlDbType.Decimal, 9, "WGPCGT02");
            ocm.Parameters.Add("@WGPCGT03", SqlDbType.Decimal, 9, "WGPCGT03");
            ocm.Parameters.Add("@WGPCGT04", SqlDbType.Decimal, 9, "WGPCGT04");
            ocm.Parameters.Add("@WGPCGT05", SqlDbType.Decimal, 9, "WGPCGT05");
            ocm.Parameters.Add("@WGPCGT06", SqlDbType.Decimal, 9, "WGPCGT06");
            ocm.Parameters.Add("@WGPCGT07", SqlDbType.Decimal, 9, "WGPCGT07");
            ocm.Parameters.Add("@WGPCGT08", SqlDbType.Decimal, 9, "WGPCGT08");
            ocm.Parameters.Add("@WGPCGT09", SqlDbType.Decimal, 9, "WGPCGT09");
            ocm.Parameters.Add("@WGPCGT10", SqlDbType.Decimal, 9, "WGPCGT10");
            ocm.Parameters.Add("@WGPCGT11", SqlDbType.Decimal, 9, "WGPCGT11");
            ocm.Parameters.Add("@WGPCGT12", SqlDbType.Decimal, 9, "WGPCGT12");
            ocm.Parameters.Add("@WGPCGT13", SqlDbType.Decimal, 9, "WGPCGT13");
            ocm.Parameters.Add("@WGPCGT14", SqlDbType.Decimal, 9, "WGPCGT14");
            ocm.Parameters.Add("@WGPCGT15", SqlDbType.Decimal, 9, "WGPCGT15");
            ocm.Parameters.Add("@WGPCGT16", SqlDbType.Decimal, 9, "WGPCGT16");
            ocm.Parameters.Add("@WGPCGT17", SqlDbType.Decimal, 9, "WGPCGT17");
            ocm.Parameters.Add("@WGPCGT18", SqlDbType.Decimal, 9, "WGPCGT18");
            ocm.Parameters.Add("@WGPCGT19", SqlDbType.Decimal, 9, "WGPCGT19");
            ocm.Parameters.Add("@WGPCGT20", SqlDbType.Decimal, 9, "WGPCGT20");
            ocm.Parameters.Add("@WGPCGT21", SqlDbType.Decimal, 9, "WGPCGT21");
            ocm.Parameters.Add("@WGPCGT22", SqlDbType.Decimal, 9, "WGPCGT22");
            ocm.Parameters.Add("@WGPCGT23", SqlDbType.Decimal, 9, "WGPCGT23");
            ocm.Parameters.Add("@WGPCGT24", SqlDbType.Decimal, 9, "WGPCGT24");
            ocm.Parameters.Add("@WGPCGT25", SqlDbType.Decimal, 9, "WGPCGT25");
            ocm.Parameters.Add("@WGPCGT26", SqlDbType.Decimal, 9, "WGPCGT26");
            ocm.Parameters.Add("@WGPCGT27", SqlDbType.Decimal, 9, "WGPCGT27");
            ocm.Parameters.Add("@WGPCGT28", SqlDbType.Decimal, 9, "WGPCGT28");
            ocm.Parameters.Add("@WGPCGT29", SqlDbType.Decimal, 9, "WGPCGT29");
            ocm.Parameters.Add("@WGPCGT30", SqlDbType.Decimal, 9, "WGPCGT30");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_MSTWGPCDelCmd()

        {
            string queryStatements = "DELETE DBO.DUTY_MSTWGPC"
                                   + " WHERE END_YYMM = @END_YYMM"
                                   + "   AND SAWON_NO = @SAWON_NO "
                                   + "   AND YYMM = @YYMM"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@END_YYMM", SqlDbType.VarChar, 6, "END_YYMM").SourceVersion = DataRowVersion.Original;
            ocm.Parameters.Add("@SAWON_NO", SqlDbType.VarChar, 15, "SAWON_NO").SourceVersion = DataRowVersion.Original;
            ocm.Parameters.Add("@YYMM", SqlDbType.VarChar, 6, "YYMM").SourceVersion = DataRowVersion.Original;

            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion

		#region MSTWGPC insert, update,delete command

        private SqlCommand GetMSTWGPCInCmd()
        {
            string queryStatements = "INSERT INTO DBO.MSTWGPC(WGPCYYMM, WGPCSQNO, WGPCSABN, WGPCSD01, WGPCSD02, WGPCSD03, WGPCSD04, WGPCSD05, WGPCSD06, WGPCSD07, WGPCSD08, WGPCSD09, WGPCSD10, WGPCSD11, WGPCSD12, WGPCSD13, WGPCSD14, WGPCSD15, WGPCSD16, WGPCSD17, WGPCSD18, WGPCSD19, WGPCSD20, WGPCSD21, WGPCSD22, WGPCSD23, WGPCSD24, WGPCSD25, WGPCSD26, WGPCSD27, WGPCSD28, WGPCSD29, WGPCSD30, WGPCSD31, WGPCSD32, WGPCSD33, WGPCSD34, WGPCSD35, WGPCSD36, WGPCSD37, WGPCSD38, WGPCSD39, WGPCSD40, WGPCSD41, WGPCSD42, WGPCSD43, WGPCSD44, WGPCSD45, WGPCSD46, WGPCSD47, WGPCSD48, WGPCSD49, WGPCSD50, "
							       + "                        WGPCGJ01, WGPCGJ02, WGPCGJ03, WGPCGJ04, WGPCGJ05, WGPCGJ06, WGPCGJ07, WGPCGJ08, WGPCGJ09, WGPCGJ10, WGPCGJ11, WGPCGJ12, WGPCGJ13, WGPCGJ14, WGPCGJ15, WGPCGJ16, WGPCGJ17, WGPCGJ18, WGPCGJ19, WGPCGJ20, WGPCGJ21, WGPCGJ22, WGPCGJ23, WGPCGJ24, WGPCGJ25, WGPCGJ26, WGPCGJ27, WGPCGJ28, WGPCGJ29, WGPCGJ30, WGPCINDT, WGPCUPDT, WGPCUSID, WGPCPSTY"
                                   + ") VALUES (@WGPCYYMM, @WGPCSQNO, @WGPCSABN, @WGPCSD01, @WGPCSD02, @WGPCSD03, @WGPCSD04, @WGPCSD05, @WGPCSD06, @WGPCSD07, @WGPCSD08, @WGPCSD09, @WGPCSD10, @WGPCSD11, @WGPCSD12, @WGPCSD13, @WGPCSD14, @WGPCSD15, @WGPCSD16, @WGPCSD17, @WGPCSD18, @WGPCSD19, @WGPCSD20, @WGPCSD21, @WGPCSD22, @WGPCSD23, @WGPCSD24, @WGPCSD25, @WGPCSD26, @WGPCSD27, @WGPCSD28, @WGPCSD29, @WGPCSD30, @WGPCSD31, @WGPCSD32, @WGPCSD33, @WGPCSD34, @WGPCSD35, @WGPCSD36, @WGPCSD37, @WGPCSD38, @WGPCSD39, @WGPCSD40, @WGPCSD41, @WGPCSD42, @WGPCSD43, @WGPCSD44, @WGPCSD45, @WGPCSD46, @WGPCSD47, @WGPCSD48, @WGPCSD49, @WGPCSD50, "
								   + "          @WGPCGJ01, @WGPCGJ02, @WGPCGJ03, @WGPCGJ04, @WGPCGJ05, @WGPCGJ06, @WGPCGJ07, @WGPCGJ08, @WGPCGJ09, @WGPCGJ10, @WGPCGJ11, @WGPCGJ12, @WGPCGJ13, @WGPCGJ14, @WGPCGJ15, @WGPCGJ16, @WGPCGJ17, @WGPCGJ18, @WGPCGJ19, @WGPCGJ20, @WGPCGJ21, @WGPCGJ22, @WGPCGJ23, @WGPCGJ24, @WGPCGJ25, @WGPCGJ26, @WGPCGJ27, @WGPCGJ28, @WGPCGJ29, @WGPCGJ30, @WGPCINDT, @WGPCUPDT, @WGPCUSID, @WGPCPSTY"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@WGPCYYMM", SqlDbType.Char, 6, "WGPCYYMM");
            ocm.Parameters.Add("@WGPCSQNO", SqlDbType.Char, 1, "WGPCSQNO");
            ocm.Parameters.Add("@WGPCSABN", SqlDbType.VarChar, 15, "WGPCSABN");
            ocm.Parameters.Add("@WGPCSD01", SqlDbType.Decimal, 11, "WGPCSD01");
            ocm.Parameters.Add("@WGPCSD02", SqlDbType.Decimal, 11, "WGPCSD02");
            ocm.Parameters.Add("@WGPCSD03", SqlDbType.Decimal, 11, "WGPCSD03");
            ocm.Parameters.Add("@WGPCSD04", SqlDbType.Decimal, 11, "WGPCSD04");
            ocm.Parameters.Add("@WGPCSD05", SqlDbType.Decimal, 11, "WGPCSD05");
            ocm.Parameters.Add("@WGPCSD06", SqlDbType.Decimal, 11, "WGPCSD06");
            ocm.Parameters.Add("@WGPCSD07", SqlDbType.Decimal, 11, "WGPCSD07");
            ocm.Parameters.Add("@WGPCSD08", SqlDbType.Decimal, 11, "WGPCSD08");
            ocm.Parameters.Add("@WGPCSD09", SqlDbType.Decimal, 11, "WGPCSD09");
            ocm.Parameters.Add("@WGPCSD10", SqlDbType.Decimal, 11, "WGPCSD10");
            ocm.Parameters.Add("@WGPCSD11", SqlDbType.Decimal, 11, "WGPCSD11");
            ocm.Parameters.Add("@WGPCSD12", SqlDbType.Decimal, 11, "WGPCSD12");
            ocm.Parameters.Add("@WGPCSD13", SqlDbType.Decimal, 11, "WGPCSD13");
            ocm.Parameters.Add("@WGPCSD14", SqlDbType.Decimal, 11, "WGPCSD14");
            ocm.Parameters.Add("@WGPCSD15", SqlDbType.Decimal, 11, "WGPCSD15");
            ocm.Parameters.Add("@WGPCSD16", SqlDbType.Decimal, 11, "WGPCSD16");
            ocm.Parameters.Add("@WGPCSD17", SqlDbType.Decimal, 11, "WGPCSD17");
            ocm.Parameters.Add("@WGPCSD18", SqlDbType.Decimal, 11, "WGPCSD18");
            ocm.Parameters.Add("@WGPCSD19", SqlDbType.Decimal, 11, "WGPCSD19");
            ocm.Parameters.Add("@WGPCSD20", SqlDbType.Decimal, 11, "WGPCSD20");
            ocm.Parameters.Add("@WGPCSD21", SqlDbType.Decimal, 11, "WGPCSD21");
            ocm.Parameters.Add("@WGPCSD22", SqlDbType.Decimal, 11, "WGPCSD22");
            ocm.Parameters.Add("@WGPCSD23", SqlDbType.Decimal, 11, "WGPCSD23");
            ocm.Parameters.Add("@WGPCSD24", SqlDbType.Decimal, 11, "WGPCSD24");
            ocm.Parameters.Add("@WGPCSD25", SqlDbType.Decimal, 11, "WGPCSD25");
            ocm.Parameters.Add("@WGPCSD26", SqlDbType.Decimal, 11, "WGPCSD26");
            ocm.Parameters.Add("@WGPCSD27", SqlDbType.Decimal, 11, "WGPCSD27");
            ocm.Parameters.Add("@WGPCSD28", SqlDbType.Decimal, 11, "WGPCSD28");
            ocm.Parameters.Add("@WGPCSD29", SqlDbType.Decimal, 11, "WGPCSD29");
            ocm.Parameters.Add("@WGPCSD30", SqlDbType.Decimal, 11, "WGPCSD30");
            ocm.Parameters.Add("@WGPCSD31", SqlDbType.Decimal, 11, "WGPCSD31");
            ocm.Parameters.Add("@WGPCSD32", SqlDbType.Decimal, 11, "WGPCSD32");
            ocm.Parameters.Add("@WGPCSD33", SqlDbType.Decimal, 11, "WGPCSD33");
            ocm.Parameters.Add("@WGPCSD34", SqlDbType.Decimal, 11, "WGPCSD34");
            ocm.Parameters.Add("@WGPCSD35", SqlDbType.Decimal, 11, "WGPCSD35");
            ocm.Parameters.Add("@WGPCSD36", SqlDbType.Decimal, 11, "WGPCSD36");
            ocm.Parameters.Add("@WGPCSD37", SqlDbType.Decimal, 11, "WGPCSD37");
            ocm.Parameters.Add("@WGPCSD38", SqlDbType.Decimal, 11, "WGPCSD38");
            ocm.Parameters.Add("@WGPCSD39", SqlDbType.Decimal, 11, "WGPCSD39");
            ocm.Parameters.Add("@WGPCSD40", SqlDbType.Decimal, 11, "WGPCSD40");
            ocm.Parameters.Add("@WGPCSD41", SqlDbType.Decimal, 11, "WGPCSD41");
            ocm.Parameters.Add("@WGPCSD42", SqlDbType.Decimal, 11, "WGPCSD42");
            ocm.Parameters.Add("@WGPCSD43", SqlDbType.Decimal, 11, "WGPCSD43");
            ocm.Parameters.Add("@WGPCSD44", SqlDbType.Decimal, 11, "WGPCSD44");
            ocm.Parameters.Add("@WGPCSD45", SqlDbType.Decimal, 11, "WGPCSD45");
            ocm.Parameters.Add("@WGPCSD46", SqlDbType.Decimal, 11, "WGPCSD46");
            ocm.Parameters.Add("@WGPCSD47", SqlDbType.Decimal, 11, "WGPCSD47");
            ocm.Parameters.Add("@WGPCSD48", SqlDbType.Decimal, 11, "WGPCSD48");
            ocm.Parameters.Add("@WGPCSD49", SqlDbType.Decimal, 11, "WGPCSD49");
            ocm.Parameters.Add("@WGPCSD50", SqlDbType.Decimal, 11, "WGPCSD50");
            ocm.Parameters.Add("@WGPCGJ01", SqlDbType.Decimal, 11, "WGPCGJ01");
            ocm.Parameters.Add("@WGPCGJ02", SqlDbType.Decimal, 11, "WGPCGJ02");
            ocm.Parameters.Add("@WGPCGJ03", SqlDbType.Decimal, 11, "WGPCGJ03");
            ocm.Parameters.Add("@WGPCGJ04", SqlDbType.Decimal, 11, "WGPCGJ04");
            ocm.Parameters.Add("@WGPCGJ05", SqlDbType.Decimal, 11, "WGPCGJ05");
            ocm.Parameters.Add("@WGPCGJ06", SqlDbType.Decimal, 11, "WGPCGJ06");
            ocm.Parameters.Add("@WGPCGJ07", SqlDbType.Decimal, 11, "WGPCGJ07");
            ocm.Parameters.Add("@WGPCGJ08", SqlDbType.Decimal, 11, "WGPCGJ08");
            ocm.Parameters.Add("@WGPCGJ09", SqlDbType.Decimal, 11, "WGPCGJ09");
            ocm.Parameters.Add("@WGPCGJ10", SqlDbType.Decimal, 11, "WGPCGJ10");
            ocm.Parameters.Add("@WGPCGJ11", SqlDbType.Decimal, 11, "WGPCGJ11");
            ocm.Parameters.Add("@WGPCGJ12", SqlDbType.Decimal, 11, "WGPCGJ12");
            ocm.Parameters.Add("@WGPCGJ13", SqlDbType.Decimal, 11, "WGPCGJ13");
            ocm.Parameters.Add("@WGPCGJ14", SqlDbType.Decimal, 11, "WGPCGJ14");
            ocm.Parameters.Add("@WGPCGJ15", SqlDbType.Decimal, 11, "WGPCGJ15");
            ocm.Parameters.Add("@WGPCGJ16", SqlDbType.Decimal, 11, "WGPCGJ16");
            ocm.Parameters.Add("@WGPCGJ17", SqlDbType.Decimal, 11, "WGPCGJ17");
            ocm.Parameters.Add("@WGPCGJ18", SqlDbType.Decimal, 11, "WGPCGJ18");
            ocm.Parameters.Add("@WGPCGJ19", SqlDbType.Decimal, 11, "WGPCGJ19");
            ocm.Parameters.Add("@WGPCGJ20", SqlDbType.Decimal, 11, "WGPCGJ20");
            ocm.Parameters.Add("@WGPCGJ21", SqlDbType.Decimal, 11, "WGPCGJ21");
            ocm.Parameters.Add("@WGPCGJ22", SqlDbType.Decimal, 11, "WGPCGJ22");
            ocm.Parameters.Add("@WGPCGJ23", SqlDbType.Decimal, 11, "WGPCGJ23");
            ocm.Parameters.Add("@WGPCGJ24", SqlDbType.Decimal, 11, "WGPCGJ24");
            ocm.Parameters.Add("@WGPCGJ25", SqlDbType.Decimal, 11, "WGPCGJ25");
            ocm.Parameters.Add("@WGPCGJ26", SqlDbType.Decimal, 11, "WGPCGJ26");
            ocm.Parameters.Add("@WGPCGJ27", SqlDbType.Decimal, 11, "WGPCGJ27");
            ocm.Parameters.Add("@WGPCGJ28", SqlDbType.Decimal, 11, "WGPCGJ28");
            ocm.Parameters.Add("@WGPCGJ29", SqlDbType.Decimal, 11, "WGPCGJ29");
            ocm.Parameters.Add("@WGPCGJ30", SqlDbType.Decimal, 11, "WGPCGJ30");
            ocm.Parameters.Add("@WGPCINDT", SqlDbType.Char, 8, "WGPCINDT");
            ocm.Parameters.Add("@WGPCUPDT", SqlDbType.Char, 8, "WGPCUPDT");
            ocm.Parameters.Add("@WGPCUSID", SqlDbType.Char, 20, "WGPCUSID");
            ocm.Parameters.Add("@WGPCPSTY", SqlDbType.Char, 1, "WGPCPSTY");

            return ocm;
        }

        private SqlCommand GetMSTWGPCUpCmd()

        {
            string queryStatements = "UPDATE DBO.MSTWGPC SET "
                                   + "   WGPCSD01 = @WGPCSD01, "
                                   + "   WGPCSD02 = @WGPCSD02, "
                                   + "   WGPCSD03 = @WGPCSD03, "
                                   + "   WGPCSD04 = @WGPCSD04, "
                                   + "   WGPCSD05 = @WGPCSD05, "
                                   + "   WGPCSD06 = @WGPCSD06, "
                                   + "   WGPCSD07 = @WGPCSD07, "
                                   + "   WGPCSD08 = @WGPCSD08, "
                                   + "   WGPCSD09 = @WGPCSD09, "
                                   + "   WGPCSD10 = @WGPCSD10, "
                                   + "   WGPCSD11 = @WGPCSD11, "
                                   + "   WGPCSD12 = @WGPCSD12, "
                                   + "   WGPCSD13 = @WGPCSD13, "
                                   + "   WGPCSD14 = @WGPCSD14, "
                                   + "   WGPCSD15 = @WGPCSD15, "
                                   + "   WGPCSD16 = @WGPCSD16, "
                                   + "   WGPCSD17 = @WGPCSD17, "
                                   + "   WGPCSD18 = @WGPCSD18, "
                                   + "   WGPCSD19 = @WGPCSD19, "
                                   + "   WGPCSD20 = @WGPCSD20, "
                                   + "   WGPCSD21 = @WGPCSD21, "
                                   + "   WGPCSD22 = @WGPCSD22, "
                                   + "   WGPCSD23 = @WGPCSD23, "
                                   + "   WGPCSD24 = @WGPCSD24, "
                                   + "   WGPCSD25 = @WGPCSD25, "
                                   + "   WGPCSD26 = @WGPCSD26, "
                                   + "   WGPCSD27 = @WGPCSD27, "
                                   + "   WGPCSD28 = @WGPCSD28, "
                                   + "   WGPCSD29 = @WGPCSD29, "
                                   + "   WGPCSD30 = @WGPCSD30, "
                                   + "   WGPCSD31 = @WGPCSD31, "
                                   + "   WGPCSD32 = @WGPCSD32, "
                                   + "   WGPCSD33 = @WGPCSD33, "
                                   + "   WGPCSD34 = @WGPCSD34, "
                                   + "   WGPCSD35 = @WGPCSD35, "
                                   + "   WGPCSD36 = @WGPCSD36, "
                                   + "   WGPCSD37 = @WGPCSD37, "
                                   + "   WGPCSD38 = @WGPCSD38, "
                                   + "   WGPCSD39 = @WGPCSD39, "
                                   + "   WGPCSD40 = @WGPCSD40, "
                                   + "   WGPCSD41 = @WGPCSD41, "
                                   + "   WGPCSD42 = @WGPCSD42, "
                                   + "   WGPCSD43 = @WGPCSD43, "
                                   + "   WGPCSD44 = @WGPCSD44, "
                                   + "   WGPCSD45 = @WGPCSD45, "
                                   + "   WGPCSD46 = @WGPCSD46, "
                                   + "   WGPCSD47 = @WGPCSD47, "
                                   + "   WGPCSD48 = @WGPCSD48, "
                                   + "   WGPCSD49 = @WGPCSD49, "
                                   + "   WGPCSD50 = @WGPCSD50, "
                                   + "   WGPCINDT = @WGPCINDT, "
                                   + "   WGPCUPDT = @WGPCUPDT, "
                                   + "   WGPCUSID = @WGPCUSID, "
                                   + "   WGPCPSTY = @WGPCPSTY"
                                   + " WHERE WGPCYYMM = @WGPCYYMM"
                                   + "   AND WGPCSQNO = @WGPCSQNO"
                                   + "   AND WGPCSABN = @WGPCSABN"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@WGPCYYMM", SqlDbType.Char, 6, "WGPCYYMM");
            ocm.Parameters.Add("@WGPCSQNO", SqlDbType.Char, 1, "WGPCSQNO");
            ocm.Parameters.Add("@WGPCSABN", SqlDbType.VarChar, 15, "WGPCSABN");
            ocm.Parameters.Add("@WGPCSD01", SqlDbType.Decimal, 11, "WGPCSD01");
            ocm.Parameters.Add("@WGPCSD02", SqlDbType.Decimal, 11, "WGPCSD02");
            ocm.Parameters.Add("@WGPCSD03", SqlDbType.Decimal, 11, "WGPCSD03");
            ocm.Parameters.Add("@WGPCSD04", SqlDbType.Decimal, 11, "WGPCSD04");
            ocm.Parameters.Add("@WGPCSD05", SqlDbType.Decimal, 11, "WGPCSD05");
            ocm.Parameters.Add("@WGPCSD06", SqlDbType.Decimal, 11, "WGPCSD06");
            ocm.Parameters.Add("@WGPCSD07", SqlDbType.Decimal, 11, "WGPCSD07");
            ocm.Parameters.Add("@WGPCSD08", SqlDbType.Decimal, 11, "WGPCSD08");
            ocm.Parameters.Add("@WGPCSD09", SqlDbType.Decimal, 11, "WGPCSD09");
            ocm.Parameters.Add("@WGPCSD10", SqlDbType.Decimal, 11, "WGPCSD10");
            ocm.Parameters.Add("@WGPCSD11", SqlDbType.Decimal, 11, "WGPCSD11");
            ocm.Parameters.Add("@WGPCSD12", SqlDbType.Decimal, 11, "WGPCSD12");
            ocm.Parameters.Add("@WGPCSD13", SqlDbType.Decimal, 11, "WGPCSD13");
            ocm.Parameters.Add("@WGPCSD14", SqlDbType.Decimal, 11, "WGPCSD14");
            ocm.Parameters.Add("@WGPCSD15", SqlDbType.Decimal, 11, "WGPCSD15");
            ocm.Parameters.Add("@WGPCSD16", SqlDbType.Decimal, 11, "WGPCSD16");
            ocm.Parameters.Add("@WGPCSD17", SqlDbType.Decimal, 11, "WGPCSD17");
            ocm.Parameters.Add("@WGPCSD18", SqlDbType.Decimal, 11, "WGPCSD18");
            ocm.Parameters.Add("@WGPCSD19", SqlDbType.Decimal, 11, "WGPCSD19");
            ocm.Parameters.Add("@WGPCSD20", SqlDbType.Decimal, 11, "WGPCSD20");
            ocm.Parameters.Add("@WGPCSD21", SqlDbType.Decimal, 11, "WGPCSD21");
            ocm.Parameters.Add("@WGPCSD22", SqlDbType.Decimal, 11, "WGPCSD22");
            ocm.Parameters.Add("@WGPCSD23", SqlDbType.Decimal, 11, "WGPCSD23");
            ocm.Parameters.Add("@WGPCSD24", SqlDbType.Decimal, 11, "WGPCSD24");
            ocm.Parameters.Add("@WGPCSD25", SqlDbType.Decimal, 11, "WGPCSD25");
            ocm.Parameters.Add("@WGPCSD26", SqlDbType.Decimal, 11, "WGPCSD26");
            ocm.Parameters.Add("@WGPCSD27", SqlDbType.Decimal, 11, "WGPCSD27");
            ocm.Parameters.Add("@WGPCSD28", SqlDbType.Decimal, 11, "WGPCSD28");
            ocm.Parameters.Add("@WGPCSD29", SqlDbType.Decimal, 11, "WGPCSD29");
            ocm.Parameters.Add("@WGPCSD30", SqlDbType.Decimal, 11, "WGPCSD30");
            ocm.Parameters.Add("@WGPCSD31", SqlDbType.Decimal, 11, "WGPCSD31");
            ocm.Parameters.Add("@WGPCSD32", SqlDbType.Decimal, 11, "WGPCSD32");
            ocm.Parameters.Add("@WGPCSD33", SqlDbType.Decimal, 11, "WGPCSD33");
            ocm.Parameters.Add("@WGPCSD34", SqlDbType.Decimal, 11, "WGPCSD34");
            ocm.Parameters.Add("@WGPCSD35", SqlDbType.Decimal, 11, "WGPCSD35");
            ocm.Parameters.Add("@WGPCSD36", SqlDbType.Decimal, 11, "WGPCSD36");
            ocm.Parameters.Add("@WGPCSD37", SqlDbType.Decimal, 11, "WGPCSD37");
            ocm.Parameters.Add("@WGPCSD38", SqlDbType.Decimal, 11, "WGPCSD38");
            ocm.Parameters.Add("@WGPCSD39", SqlDbType.Decimal, 11, "WGPCSD39");
            ocm.Parameters.Add("@WGPCSD40", SqlDbType.Decimal, 11, "WGPCSD40");
            ocm.Parameters.Add("@WGPCSD41", SqlDbType.Decimal, 11, "WGPCSD41");
            ocm.Parameters.Add("@WGPCSD42", SqlDbType.Decimal, 11, "WGPCSD42");
            ocm.Parameters.Add("@WGPCSD43", SqlDbType.Decimal, 11, "WGPCSD43");
            ocm.Parameters.Add("@WGPCSD44", SqlDbType.Decimal, 11, "WGPCSD44");
            ocm.Parameters.Add("@WGPCSD45", SqlDbType.Decimal, 11, "WGPCSD45");
            ocm.Parameters.Add("@WGPCSD46", SqlDbType.Decimal, 11, "WGPCSD46");
            ocm.Parameters.Add("@WGPCSD47", SqlDbType.Decimal, 11, "WGPCSD47");
            ocm.Parameters.Add("@WGPCSD48", SqlDbType.Decimal, 11, "WGPCSD48");
            ocm.Parameters.Add("@WGPCSD49", SqlDbType.Decimal, 11, "WGPCSD49");
            ocm.Parameters.Add("@WGPCSD50", SqlDbType.Decimal, 11, "WGPCSD50");
            ocm.Parameters.Add("@WGPCINDT", SqlDbType.Char, 8, "WGPCINDT");
            ocm.Parameters.Add("@WGPCUPDT", SqlDbType.Char, 8, "WGPCUPDT");
            ocm.Parameters.Add("@WGPCUSID", SqlDbType.Char, 20, "WGPCUSID");
            ocm.Parameters.Add("@WGPCPSTY", SqlDbType.Char, 1, "WGPCPSTY");

            return ocm;
        }

        private SqlCommand GetMSTWGPCDelCmd()

        {
            string queryStatements = "DELETE DBO.MSTWGPC"
                                   + " WHERE WGPCYYMM = @WGPCYYMM"
                                   + "   AND WGPCSQNO = @WGPCSQNO"
                                   + "   AND WGPCSABN = @WGPCSABN"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@WGPCYYMM", SqlDbType.Char, 6, "WGPCYYMM").SourceVersion = DataRowVersion.Original;
            ocm.Parameters.Add("@WGPCSQNO", SqlDbType.Char, 1, "WGPCSQNO").SourceVersion = DataRowVersion.Original;
            ocm.Parameters.Add("@WGPCSABN", SqlDbType.VarChar, 15, "WGPCSABN").SourceVersion = DataRowVersion.Original;

            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion

		#region MSTGTMM insert, update,delete command

        private SqlCommand GetMSTGTMMInCmd()
        {
            string queryStatements = "INSERT INTO DBO.MSTGTMM(GTMMYYMM, GTMMSABN, GTMMGT01, GTMMGT02, GTMMGT03, GTMMGT04, GTMMGT05, GTMMGT06, GTMMGT07, GTMMGT08, GTMMGT09, GTMMGT10, GTMMGT11, GTMMGT12, GTMMGT13, GTMMGT14, GTMMGT15, GTMMGT16, GTMMGT17, GTMMGT18, GTMMGT19, GTMMGT20, GTMMGT21, GTMMGT22, GTMMGT23, GTMMGT24, GTMMGT25, GTMMGT26, GTMMGT27, GTMMGT28, GTMMGT29, GTMMGT30, GTMMINDT, GTMMUPDT, GTMMUSID, GTMMPSTY"
                                   + ") VALUES (@GTMMYYMM, @GTMMSABN, @GTMMGT01, @GTMMGT02, @GTMMGT03, @GTMMGT04, @GTMMGT05, @GTMMGT06, @GTMMGT07, @GTMMGT08, @GTMMGT09, @GTMMGT10, @GTMMGT11, @GTMMGT12, @GTMMGT13, @GTMMGT14, @GTMMGT15, @GTMMGT16, @GTMMGT17, @GTMMGT18, @GTMMGT19, @GTMMGT20, @GTMMGT21, @GTMMGT22, @GTMMGT23, @GTMMGT24, @GTMMGT25, @GTMMGT26, @GTMMGT27, @GTMMGT28, @GTMMGT29, @GTMMGT30, @GTMMINDT, @GTMMUPDT, @GTMMUSID, @GTMMPSTY"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@GTMMYYMM", SqlDbType.Char, 6, "GTMMYYMM");
            ocm.Parameters.Add("@GTMMSABN", SqlDbType.VarChar, 15, "GTMMSABN");
            ocm.Parameters.Add("@GTMMGT01", SqlDbType.Decimal, 5, "GTMMGT01");
            ocm.Parameters.Add("@GTMMGT02", SqlDbType.Decimal, 5, "GTMMGT02");
            ocm.Parameters.Add("@GTMMGT03", SqlDbType.Decimal, 5, "GTMMGT03");
            ocm.Parameters.Add("@GTMMGT04", SqlDbType.Decimal, 5, "GTMMGT04");
            ocm.Parameters.Add("@GTMMGT05", SqlDbType.Decimal, 5, "GTMMGT05");
            ocm.Parameters.Add("@GTMMGT06", SqlDbType.Decimal, 5, "GTMMGT06");
            ocm.Parameters.Add("@GTMMGT07", SqlDbType.Decimal, 5, "GTMMGT07");
            ocm.Parameters.Add("@GTMMGT08", SqlDbType.Decimal, 5, "GTMMGT08");
            ocm.Parameters.Add("@GTMMGT09", SqlDbType.Decimal, 5, "GTMMGT09");
            ocm.Parameters.Add("@GTMMGT10", SqlDbType.Decimal, 5, "GTMMGT10");
            ocm.Parameters.Add("@GTMMGT11", SqlDbType.Decimal, 5, "GTMMGT11");
            ocm.Parameters.Add("@GTMMGT12", SqlDbType.Decimal, 5, "GTMMGT12");
            ocm.Parameters.Add("@GTMMGT13", SqlDbType.Decimal, 5, "GTMMGT13");
            ocm.Parameters.Add("@GTMMGT14", SqlDbType.Decimal, 5, "GTMMGT14");
            ocm.Parameters.Add("@GTMMGT15", SqlDbType.Decimal, 5, "GTMMGT15");
            ocm.Parameters.Add("@GTMMGT16", SqlDbType.Decimal, 5, "GTMMGT16");
            ocm.Parameters.Add("@GTMMGT17", SqlDbType.Decimal, 5, "GTMMGT17");
            ocm.Parameters.Add("@GTMMGT18", SqlDbType.Decimal, 5, "GTMMGT18");
            ocm.Parameters.Add("@GTMMGT19", SqlDbType.Decimal, 5, "GTMMGT19");
            ocm.Parameters.Add("@GTMMGT20", SqlDbType.Decimal, 5, "GTMMGT20");
            ocm.Parameters.Add("@GTMMGT21", SqlDbType.Decimal, 5, "GTMMGT21");
            ocm.Parameters.Add("@GTMMGT22", SqlDbType.Decimal, 5, "GTMMGT22");
            ocm.Parameters.Add("@GTMMGT23", SqlDbType.Decimal, 5, "GTMMGT23");
            ocm.Parameters.Add("@GTMMGT24", SqlDbType.Decimal, 5, "GTMMGT24");
            ocm.Parameters.Add("@GTMMGT25", SqlDbType.Decimal, 5, "GTMMGT25");
            ocm.Parameters.Add("@GTMMGT26", SqlDbType.Decimal, 5, "GTMMGT26");
            ocm.Parameters.Add("@GTMMGT27", SqlDbType.Decimal, 5, "GTMMGT27");
            ocm.Parameters.Add("@GTMMGT28", SqlDbType.Decimal, 5, "GTMMGT28");
            ocm.Parameters.Add("@GTMMGT29", SqlDbType.Decimal, 5, "GTMMGT29");
            ocm.Parameters.Add("@GTMMGT30", SqlDbType.Decimal, 5, "GTMMGT30");
            ocm.Parameters.Add("@GTMMINDT", SqlDbType.Char, 8, "GTMMINDT");
            ocm.Parameters.Add("@GTMMUPDT", SqlDbType.Char, 8, "GTMMUPDT");
            ocm.Parameters.Add("@GTMMUSID", SqlDbType.Char, 20, "GTMMUSID");
            ocm.Parameters.Add("@GTMMPSTY", SqlDbType.Char, 1, "GTMMPSTY");

            return ocm;
        }

        private SqlCommand GetMSTGTMMUpCmd()

        {
            string queryStatements = "UPDATE DBO.MSTGTMM SET "
                                   + "   GTMMGT01 = @GTMMGT01, "
                                   + "   GTMMGT02 = @GTMMGT02, "
                                   + "   GTMMGT03 = @GTMMGT03, "
                                   + "   GTMMGT04 = @GTMMGT04, "
                                   + "   GTMMGT05 = @GTMMGT05, "
                                   + "   GTMMGT06 = @GTMMGT06, "
                                   + "   GTMMGT07 = @GTMMGT07, "
                                   + "   GTMMGT08 = @GTMMGT08, "
                                   + "   GTMMGT09 = @GTMMGT09, "
                                   + "   GTMMGT10 = @GTMMGT10, "
                                   + "   GTMMGT11 = @GTMMGT11, "
                                   + "   GTMMGT12 = @GTMMGT12, "
                                   + "   GTMMGT13 = @GTMMGT13, "
                                   + "   GTMMGT14 = @GTMMGT14, "
                                   + "   GTMMGT15 = @GTMMGT15, "
                                   + "   GTMMGT16 = @GTMMGT16, "
                                   + "   GTMMGT17 = @GTMMGT17, "
                                   + "   GTMMGT18 = @GTMMGT18, "
                                   + "   GTMMGT19 = @GTMMGT19, "
                                   + "   GTMMGT20 = @GTMMGT20, "
                                   + "   GTMMGT21 = @GTMMGT21, "
                                   + "   GTMMGT22 = @GTMMGT22, "
                                   + "   GTMMGT23 = @GTMMGT23, "
                                   + "   GTMMGT24 = @GTMMGT24, "
                                   + "   GTMMGT25 = @GTMMGT25, "
                                   + "   GTMMGT26 = @GTMMGT26, "
                                   + "   GTMMGT27 = @GTMMGT27, "
                                   + "   GTMMGT28 = @GTMMGT28, "
                                   + "   GTMMGT29 = @GTMMGT29, "
                                   + "   GTMMGT30 = @GTMMGT30, "
                                   + "   GTMMINDT = @GTMMINDT, "
                                   + "   GTMMUPDT = @GTMMUPDT, "
                                   + "   GTMMUSID = @GTMMUSID, "
                                   + "   GTMMPSTY = @GTMMPSTY"
                                   + " WHERE GTMMYYMM = @GTMMYYMM"
                                   + "   AND GTMMSABN = @GTMMSABN"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@GTMMYYMM", SqlDbType.Char, 6, "GTMMYYMM");
            ocm.Parameters.Add("@GTMMSABN", SqlDbType.VarChar, 15, "GTMMSABN");
            ocm.Parameters.Add("@GTMMGT01", SqlDbType.Decimal, 5, "GTMMGT01");
            ocm.Parameters.Add("@GTMMGT02", SqlDbType.Decimal, 5, "GTMMGT02");
            ocm.Parameters.Add("@GTMMGT03", SqlDbType.Decimal, 5, "GTMMGT03");
            ocm.Parameters.Add("@GTMMGT04", SqlDbType.Decimal, 5, "GTMMGT04");
            ocm.Parameters.Add("@GTMMGT05", SqlDbType.Decimal, 5, "GTMMGT05");
            ocm.Parameters.Add("@GTMMGT06", SqlDbType.Decimal, 5, "GTMMGT06");
            ocm.Parameters.Add("@GTMMGT07", SqlDbType.Decimal, 5, "GTMMGT07");
            ocm.Parameters.Add("@GTMMGT08", SqlDbType.Decimal, 5, "GTMMGT08");
            ocm.Parameters.Add("@GTMMGT09", SqlDbType.Decimal, 5, "GTMMGT09");
            ocm.Parameters.Add("@GTMMGT10", SqlDbType.Decimal, 5, "GTMMGT10");
            ocm.Parameters.Add("@GTMMGT11", SqlDbType.Decimal, 5, "GTMMGT11");
            ocm.Parameters.Add("@GTMMGT12", SqlDbType.Decimal, 5, "GTMMGT12");
            ocm.Parameters.Add("@GTMMGT13", SqlDbType.Decimal, 5, "GTMMGT13");
            ocm.Parameters.Add("@GTMMGT14", SqlDbType.Decimal, 5, "GTMMGT14");
            ocm.Parameters.Add("@GTMMGT15", SqlDbType.Decimal, 5, "GTMMGT15");
            ocm.Parameters.Add("@GTMMGT16", SqlDbType.Decimal, 5, "GTMMGT16");
            ocm.Parameters.Add("@GTMMGT17", SqlDbType.Decimal, 5, "GTMMGT17");
            ocm.Parameters.Add("@GTMMGT18", SqlDbType.Decimal, 5, "GTMMGT18");
            ocm.Parameters.Add("@GTMMGT19", SqlDbType.Decimal, 5, "GTMMGT19");
            ocm.Parameters.Add("@GTMMGT20", SqlDbType.Decimal, 5, "GTMMGT20");
            ocm.Parameters.Add("@GTMMGT21", SqlDbType.Decimal, 5, "GTMMGT21");
            ocm.Parameters.Add("@GTMMGT22", SqlDbType.Decimal, 5, "GTMMGT22");
            ocm.Parameters.Add("@GTMMGT23", SqlDbType.Decimal, 5, "GTMMGT23");
            ocm.Parameters.Add("@GTMMGT24", SqlDbType.Decimal, 5, "GTMMGT24");
            ocm.Parameters.Add("@GTMMGT25", SqlDbType.Decimal, 5, "GTMMGT25");
            ocm.Parameters.Add("@GTMMGT26", SqlDbType.Decimal, 5, "GTMMGT26");
            ocm.Parameters.Add("@GTMMGT27", SqlDbType.Decimal, 5, "GTMMGT27");
            ocm.Parameters.Add("@GTMMGT28", SqlDbType.Decimal, 5, "GTMMGT28");
            ocm.Parameters.Add("@GTMMGT29", SqlDbType.Decimal, 5, "GTMMGT29");
            ocm.Parameters.Add("@GTMMGT30", SqlDbType.Decimal, 5, "GTMMGT30");
            ocm.Parameters.Add("@GTMMINDT", SqlDbType.Char, 8, "GTMMINDT");
            ocm.Parameters.Add("@GTMMUPDT", SqlDbType.Char, 8, "GTMMUPDT");
            ocm.Parameters.Add("@GTMMUSID", SqlDbType.Char, 20, "GTMMUSID");
            ocm.Parameters.Add("@GTMMPSTY", SqlDbType.Char, 1, "GTMMPSTY");

            return ocm;
        }

        private SqlCommand GetMSTGTMMDelCmd()

        {
            string queryStatements = "DELETE DBO.MSTGTMM"
                                   + " WHERE GTMMYYMM = @GTMMYYMM"
                                   + "   AND GTMMSABN = @GTMMSABN"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@GTMMYYMM", SqlDbType.Char, 6, "GTMMYYMM").SourceVersion = DataRowVersion.Original;
            ocm.Parameters.Add("@GTMMSABN", SqlDbType.VarChar, 15, "GTMMSABN").SourceVersion = DataRowVersion.Original;

            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion



		#region DUTY_TRSNOTI insert, update,delete command

        private SqlCommand GetDUTY_TRSNOTIInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_TRSNOTI(IDX, DEPTCODE, NOTIDATE, TITLE, CONTENTS, INDT, UPDT, USID, PSTY"
                                   + ") VALUES (@IDX, @DEPTCODE, @NOTIDATE, @TITLE, @CONTENTS, @INDT, @UPDT, @USID, @PSTY"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@IDX", SqlDbType.Int, 4, "IDX");
            ocm.Parameters.Add("@DEPTCODE", SqlDbType.Char, 4, "DEPTCODE");
            ocm.Parameters.Add("@NOTIDATE", SqlDbType.Char, 8, "NOTIDATE");
            ocm.Parameters.Add("@TITLE", SqlDbType.VarChar, 60, "TITLE");
            ocm.Parameters.Add("@CONTENTS", SqlDbType.VarChar, 500, "CONTENTS");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_TRSNOTIUpCmd()

        {
            string queryStatements = "UPDATE DBO.DUTY_TRSNOTI SET "
                                   + "   DEPTCODE = @DEPTCODE, "
                                   + "   NOTIDATE = @NOTIDATE, "
                                   + "   TITLE = @TITLE, "
                                   + "   CONTENTS = @CONTENTS, "
                                   + "   INDT = @INDT, "
                                   + "   UPDT = @UPDT, "
                                   + "   USID = @USID, "
                                   + "   PSTY = @PSTY"
                                   + " WHERE IDX = @IDX"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@IDX", SqlDbType.Int, 4, "IDX");
            ocm.Parameters.Add("@DEPTCODE", SqlDbType.Char, 4, "DEPTCODE");
            ocm.Parameters.Add("@NOTIDATE", SqlDbType.Char, 8, "NOTIDATE");
            ocm.Parameters.Add("@TITLE", SqlDbType.VarChar, 60, "TITLE");
            ocm.Parameters.Add("@CONTENTS", SqlDbType.VarChar, 500, "CONTENTS");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_TRSNOTIDelCmd()

        {
            string queryStatements = "DELETE DBO.DUTY_TRSNOTI"
                                   + " WHERE IDX = @IDX"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;
			
            ocm.Parameters.Add("@IDX", SqlDbType.Int, 4, "IDX").SourceVersion = DataRowVersion.Original;
            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion

		#region DUTY_TRSDYYC insert, update,delete command

        private SqlCommand GetDUTY_TRSDYYCInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_TRSDYYC(YC_YEAR, YC_TYPE, PROC_DT, SAWON_NO, SAWON_NM, IN_DATE, CALC_FRDT, CALC_TODT, USE_FRDT, USE_TODT, YC_BASE, YC_CHANGE, YC_FIRST, YC_ADD, YC_TOTAL, REG_DT, REG_ID, MOD_DT, MOD_ID"
                                   + ") VALUES (@YC_YEAR, @YC_TYPE, @PROC_DT, @SAWON_NO, @SAWON_NM, @IN_DATE, @CALC_FRDT, @CALC_TODT, @USE_FRDT, @USE_TODT, @YC_BASE, @YC_CHANGE, @YC_FIRST, @YC_ADD, @YC_TOTAL, @REG_DT, @REG_ID, @MOD_DT, @MOD_ID"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@YC_YEAR", SqlDbType.Char, 4, "YC_YEAR");
            ocm.Parameters.Add("@YC_TYPE", SqlDbType.Int, 4, "YC_TYPE");
            ocm.Parameters.Add("@PROC_DT", SqlDbType.Char, 8, "PROC_DT");
            ocm.Parameters.Add("@SAWON_NO", SqlDbType.VarChar, 15, "SAWON_NO");
            ocm.Parameters.Add("@SAWON_NM", SqlDbType.VarChar, 40, "SAWON_NM");
            ocm.Parameters.Add("@IN_DATE", SqlDbType.Char, 8, "IN_DATE");
            ocm.Parameters.Add("@CALC_FRDT", SqlDbType.Char, 8, "CALC_FRDT");
            ocm.Parameters.Add("@CALC_TODT", SqlDbType.Char, 8, "CALC_TODT");
            ocm.Parameters.Add("@USE_FRDT", SqlDbType.Char, 8, "USE_FRDT");
            ocm.Parameters.Add("@USE_TODT", SqlDbType.Char, 8, "USE_TODT");
            ocm.Parameters.Add("@YC_BASE", SqlDbType.Decimal, 5, "YC_BASE");
            ocm.Parameters.Add("@YC_CHANGE", SqlDbType.Decimal, 5, "YC_CHANGE");
            ocm.Parameters.Add("@YC_FIRST", SqlDbType.Decimal, 5, "YC_FIRST");
            ocm.Parameters.Add("@YC_ADD", SqlDbType.Decimal, 5, "YC_ADD");
            ocm.Parameters.Add("@YC_TOTAL", SqlDbType.Decimal, 5, "YC_TOTAL");
            ocm.Parameters.Add("@REG_DT", SqlDbType.VarChar, 20, "REG_DT");
            ocm.Parameters.Add("@REG_ID", SqlDbType.VarChar, 20, "REG_ID");
            ocm.Parameters.Add("@MOD_DT", SqlDbType.VarChar, 20, "MOD_DT");
            ocm.Parameters.Add("@MOD_ID", SqlDbType.VarChar, 20, "MOD_ID");

            return ocm;
        }

        private SqlCommand GetDUTY_TRSDYYCUpCmd()

        {
            string queryStatements = "UPDATE DBO.DUTY_TRSDYYC SET "
                                   + "   YC_TYPE = @YC_TYPE, "
                                   + "   PROC_DT = @PROC_DT, "
                                   + "   SAWON_NM = @SAWON_NM, "
                                   + "   IN_DATE = @IN_DATE, "
                                   + "   CALC_FRDT = @CALC_FRDT, "
                                   + "   CALC_TODT = @CALC_TODT, "
                                   + "   USE_FRDT = @USE_FRDT, "
                                   + "   USE_TODT = @USE_TODT, "
                                   + "   YC_BASE = @YC_BASE, "
                                   + "   YC_CHANGE = @YC_CHANGE, "
                                   + "   YC_FIRST = @YC_FIRST, "
                                   + "   YC_ADD = @YC_ADD, "
                                   + "   YC_TOTAL = @YC_TOTAL, "
                                   + "   REG_DT = @REG_DT, "
                                   + "   REG_ID = @REG_ID, "
                                   + "   MOD_DT = @MOD_DT, "
                                   + "   MOD_ID = @MOD_ID "
                                   + " WHERE YC_YEAR = @YC_YEAR"
                                   + "   AND SAWON_NO = @SAWON_NO"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@YC_YEAR", SqlDbType.Char, 4, "YC_YEAR");
            ocm.Parameters.Add("@YC_TYPE", SqlDbType.Int, 4, "YC_TYPE");
            ocm.Parameters.Add("@PROC_DT", SqlDbType.Char, 8, "PROC_DT");
            ocm.Parameters.Add("@SAWON_NO", SqlDbType.VarChar, 15, "SAWON_NO");
            ocm.Parameters.Add("@SAWON_NM", SqlDbType.VarChar, 40, "SAWON_NM");
            ocm.Parameters.Add("@IN_DATE", SqlDbType.Char, 8, "IN_DATE");
            ocm.Parameters.Add("@CALC_FRDT", SqlDbType.Char, 8, "CALC_FRDT");
            ocm.Parameters.Add("@CALC_TODT", SqlDbType.Char, 8, "CALC_TODT");
            ocm.Parameters.Add("@USE_FRDT", SqlDbType.Char, 8, "USE_FRDT");
            ocm.Parameters.Add("@USE_TODT", SqlDbType.Char, 8, "USE_TODT");
            ocm.Parameters.Add("@YC_BASE", SqlDbType.Decimal, 5, "YC_BASE");
            ocm.Parameters.Add("@YC_CHANGE", SqlDbType.Decimal, 5, "YC_CHANGE");
            ocm.Parameters.Add("@YC_FIRST", SqlDbType.Decimal, 5, "YC_FIRST");
            ocm.Parameters.Add("@YC_ADD", SqlDbType.Decimal, 5, "YC_ADD");
            ocm.Parameters.Add("@YC_TOTAL", SqlDbType.Decimal, 5, "YC_TOTAL");
            ocm.Parameters.Add("@REG_DT", SqlDbType.VarChar, 20, "REG_DT");
            ocm.Parameters.Add("@REG_ID", SqlDbType.VarChar, 20, "REG_ID");
            ocm.Parameters.Add("@MOD_DT", SqlDbType.VarChar, 20, "MOD_DT");
            ocm.Parameters.Add("@MOD_ID", SqlDbType.VarChar, 20, "MOD_ID");

            return ocm;
        }

        private SqlCommand GetDUTY_TRSDYYCDelCmd()

        {
            string queryStatements = "DELETE DBO.DUTY_TRSDYYC"
                                   + " WHERE YC_YEAR = @YC_YEAR"
                                   + "   AND SAWON_NO = @SAWON_NO"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@YC_YEAR", SqlDbType.Char, 4, "YC_YEAR").SourceVersion = DataRowVersion.Original;
            ocm.Parameters.Add("@SAWON_NO", SqlDbType.VarChar, 15, "SAWON_NO").SourceVersion = DataRowVersion.Original;

            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion

		#region DUTY_TRSHREQ insert, update,delete command

        private SqlCommand GetDUTY_TRSHREQInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_TRSHREQ(SABN, REQ_YEAR, REQ_DATE, REQ_DATE2, REQ_TYPE, REQ_TYPE2, YC_DAYS, AP_TAG, LINE_CNT, GW_SABN1, GW_DT1, GW_NAME1, GW_JICK1, GW_SABN2, GW_DT2, GW_CHKID2, GW_NAME2, GW_JICK2, GW_SABN3, GW_DT3, GW_CHKID3, GW_NAME3, GW_JICK3, GW_SABN4, GW_DT4, GW_CHKID4, GW_NAME4, GW_JICK4, INDT, UPDT, USID, PSTY"
                                   + ") VALUES (@SABN, @REQ_YEAR, @REQ_DATE, @REQ_DATE2, @REQ_TYPE, @REQ_TYPE2, @YC_DAYS, @AP_TAG, @LINE_CNT, @GW_SABN1, @GW_DT1, @GW_NAME1, @GW_JICK1, @GW_SABN2, @GW_DT2, @GW_CHKID2, @GW_NAME2, @GW_JICK2, @GW_SABN3, @GW_DT3, @GW_CHKID3, @GW_NAME3, @GW_JICK3, @GW_SABN4, @GW_DT4, @GW_CHKID4, @GW_NAME4, @GW_JICK4, @INDT, @UPDT, @USID, @PSTY"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@SABN", SqlDbType.VarChar, 20, "SABN");
            ocm.Parameters.Add("@REQ_YEAR", SqlDbType.Char, 4, "REQ_YEAR");
            ocm.Parameters.Add("@REQ_DATE", SqlDbType.Char, 8, "REQ_DATE");
            ocm.Parameters.Add("@REQ_DATE2", SqlDbType.Char, 8, "REQ_DATE2");
            ocm.Parameters.Add("@REQ_TYPE", SqlDbType.VarChar, 10, "REQ_TYPE");
            ocm.Parameters.Add("@REQ_TYPE2", SqlDbType.VarChar, 10, "REQ_TYPE2");
            ocm.Parameters.Add("@YC_DAYS", SqlDbType.Decimal, 7, "YC_DAYS");
            ocm.Parameters.Add("@AP_TAG", SqlDbType.Char, 1, "AP_TAG");
            ocm.Parameters.Add("@LINE_CNT", SqlDbType.Int, 1, "LINE_CNT");
            ocm.Parameters.Add("@GW_SABN1", SqlDbType.VarChar, 20, "GW_SABN1");
            ocm.Parameters.Add("@GW_DT1", SqlDbType.VarChar, 20, "GW_DT1");
            ocm.Parameters.Add("@GW_NAME1", SqlDbType.VarChar, 20, "GW_NAME1");
            ocm.Parameters.Add("@GW_JICK1", SqlDbType.VarChar, 20, "GW_JICK1");
            ocm.Parameters.Add("@GW_SABN2", SqlDbType.VarChar, 20, "GW_SABN2");
            ocm.Parameters.Add("@GW_DT2", SqlDbType.VarChar, 20, "GW_DT2");
            ocm.Parameters.Add("@GW_CHKID2", SqlDbType.VarChar, 20, "GW_CHKID2");
            ocm.Parameters.Add("@GW_NAME2", SqlDbType.VarChar, 20, "GW_NAME2");
            ocm.Parameters.Add("@GW_JICK2", SqlDbType.VarChar, 20, "GW_JICK2");
            ocm.Parameters.Add("@GW_SABN3", SqlDbType.VarChar, 20, "GW_SABN3");
            ocm.Parameters.Add("@GW_DT3", SqlDbType.VarChar, 20, "GW_DT3");
            ocm.Parameters.Add("@GW_CHKID3", SqlDbType.VarChar, 20, "GW_CHKID3");
            ocm.Parameters.Add("@GW_NAME3", SqlDbType.VarChar, 20, "GW_NAME3");
            ocm.Parameters.Add("@GW_JICK3", SqlDbType.VarChar, 20, "GW_JICK3");
            ocm.Parameters.Add("@GW_SABN4", SqlDbType.VarChar, 20, "GW_SABN4");
            ocm.Parameters.Add("@GW_DT4", SqlDbType.VarChar, 20, "GW_DT4");
            ocm.Parameters.Add("@GW_CHKID4", SqlDbType.VarChar, 20, "GW_CHKID4");
            ocm.Parameters.Add("@GW_NAME4", SqlDbType.VarChar, 20, "GW_NAME4");
            ocm.Parameters.Add("@GW_JICK4", SqlDbType.VarChar, 20, "GW_JICK4");
			//ocm.Parameters.Add("@SAWON_LV", SqlDbType.Char, 1, "SAWON_LV");
			//ocm.Parameters.Add("@EXCEPT_MID", SqlDbType.Char, 1, "EXCEPT_MID");
			//ocm.Parameters.Add("@MID_DT", SqlDbType.VarChar, 20, "MID_DT");
			//ocm.Parameters.Add("@MID_USID", SqlDbType.VarChar, 20, "MID_USID");
			//ocm.Parameters.Add("@AP_DT", SqlDbType.VarChar, 20, "AP_DT");
			//ocm.Parameters.Add("@AP_USID", SqlDbType.VarChar, 20, "AP_USID");
			//ocm.Parameters.Add("@CANC_DT", SqlDbType.VarChar, 20, "CANC_DT");
			//ocm.Parameters.Add("@CANC_USID", SqlDbType.VarChar, 20, "CANC_USID");
			ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_TRSHREQUpCmd()

        {
            string queryStatements = "UPDATE DBO.DUTY_TRSHREQ SET "
                                   + "   REQ_DATE2 = @REQ_DATE2, "
                                   + "   REQ_TYPE = @REQ_TYPE, "
                                   + "   REQ_TYPE2 = @REQ_TYPE2, "
                                   + "   YC_DAYS = @YC_DAYS, "
                                   + "   AP_TAG = @AP_TAG, "
                                   + "   LINE_CNT = @LINE_CNT, "
                                   + "   GW_SABN1 = @GW_SABN1, "
                                   + "   GW_DT1 = @GW_DT1, "
                                   + "   GW_NAME1 = @GW_NAME1, "
                                   + "   GW_JICK1 = @GW_JICK1, "
                                   + "   GW_SABN2 = @GW_SABN2, "
                                   + "   GW_DT2 = @GW_DT2, "
                                   + "   GW_CHKID2 = @GW_CHKID2, "
                                   + "   GW_NAME2 = @GW_NAME2, "
                                   + "   GW_JICK2 = @GW_JICK2, "
                                   + "   GW_SABN3 = @GW_SABN3, "
                                   + "   GW_DT3 = @GW_DT3, "
                                   + "   GW_CHKID3 = @GW_CHKID3, "
                                   + "   GW_NAME3 = @GW_NAME3, "
                                   + "   GW_JICK3 = @GW_JICK3, "
                                   + "   GW_SABN4 = @GW_SABN4, "
                                   + "   GW_DT4 = @GW_DT4, "
                                   + "   GW_CHKID4 = @GW_CHKID4, "
                                   + "   GW_NAME4 = @GW_NAME4, "
                                   + "   GW_JICK4 = @GW_JICK4, "
                                   + "   INDT = @INDT, "
                                   + "   UPDT = @UPDT, "
                                   + "   USID = @USID, "
                                   + "   PSTY = @PSTY"
                                   + " WHERE SABN = @SABN"
                                   + "   AND REQ_YEAR = @REQ_YEAR"
                                   + "   AND REQ_DATE = @REQ_DATE"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@SABN", SqlDbType.VarChar, 20, "SABN");
            ocm.Parameters.Add("@REQ_YEAR", SqlDbType.Char, 4, "REQ_YEAR");
            ocm.Parameters.Add("@REQ_DATE", SqlDbType.Char, 8, "REQ_DATE");
            ocm.Parameters.Add("@REQ_DATE2", SqlDbType.Char, 8, "REQ_DATE2");
            ocm.Parameters.Add("@REQ_TYPE", SqlDbType.VarChar, 10, "REQ_TYPE");
            ocm.Parameters.Add("@REQ_TYPE2", SqlDbType.VarChar, 10, "REQ_TYPE2");
            ocm.Parameters.Add("@YC_DAYS", SqlDbType.Decimal, 7, "YC_DAYS");
            ocm.Parameters.Add("@AP_TAG", SqlDbType.Char, 1, "AP_TAG");
            ocm.Parameters.Add("@LINE_CNT", SqlDbType.Int, 1, "LINE_CNT");
            ocm.Parameters.Add("@GW_SABN1", SqlDbType.VarChar, 20, "GW_SABN1");
            ocm.Parameters.Add("@GW_DT1", SqlDbType.VarChar, 20, "GW_DT1");
            ocm.Parameters.Add("@GW_NAME1", SqlDbType.VarChar, 20, "GW_NAME1");
            ocm.Parameters.Add("@GW_JICK1", SqlDbType.VarChar, 20, "GW_JICK1");
            ocm.Parameters.Add("@GW_SABN2", SqlDbType.VarChar, 20, "GW_SABN2");
            ocm.Parameters.Add("@GW_DT2", SqlDbType.VarChar, 20, "GW_DT2");
            ocm.Parameters.Add("@GW_CHKID2", SqlDbType.VarChar, 20, "GW_CHKID2");
            ocm.Parameters.Add("@GW_NAME2", SqlDbType.VarChar, 20, "GW_NAME2");
            ocm.Parameters.Add("@GW_JICK2", SqlDbType.VarChar, 20, "GW_JICK2");
            ocm.Parameters.Add("@GW_SABN3", SqlDbType.VarChar, 20, "GW_SABN3");
            ocm.Parameters.Add("@GW_DT3", SqlDbType.VarChar, 20, "GW_DT3");
            ocm.Parameters.Add("@GW_CHKID3", SqlDbType.VarChar, 20, "GW_CHKID3");
            ocm.Parameters.Add("@GW_NAME3", SqlDbType.VarChar, 20, "GW_NAME3");
            ocm.Parameters.Add("@GW_JICK3", SqlDbType.VarChar, 20, "GW_JICK3");
            ocm.Parameters.Add("@GW_SABN4", SqlDbType.VarChar, 20, "GW_SABN4");
            ocm.Parameters.Add("@GW_DT4", SqlDbType.VarChar, 20, "GW_DT4");
            ocm.Parameters.Add("@GW_CHKID4", SqlDbType.VarChar, 20, "GW_CHKID4");
            ocm.Parameters.Add("@GW_NAME4", SqlDbType.VarChar, 20, "GW_NAME4");
            ocm.Parameters.Add("@GW_JICK4", SqlDbType.VarChar, 20, "GW_JICK4");
			//ocm.Parameters.Add("@MID_DT", SqlDbType.VarChar, 20, "MID_DT");
			//ocm.Parameters.Add("@MID_USID", SqlDbType.VarChar, 20, "MID_USID");
			//ocm.Parameters.Add("@AP_DT", SqlDbType.VarChar, 20, "AP_DT");
			//ocm.Parameters.Add("@AP_USID", SqlDbType.VarChar, 20, "AP_USID");
			//ocm.Parameters.Add("@CANC_DT", SqlDbType.VarChar, 20, "CANC_DT");
			//ocm.Parameters.Add("@CANC_USID", SqlDbType.VarChar, 20, "CANC_USID");
			ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_TRSHREQDelCmd()

        {
            string queryStatements = "DELETE DBO.DUTY_TRSHREQ"
                                   + " WHERE SABN = @SABN"
                                   + "   AND REQ_YEAR = @REQ_YEAR"
                                   + "   AND REQ_DATE = @REQ_DATE"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@SABN", SqlDbType.VarChar, 20, "SABN").SourceVersion = DataRowVersion.Original;
            ocm.Parameters.Add("@REQ_YEAR", SqlDbType.Char, 4, "REQ_YEAR").SourceVersion = DataRowVersion.Original;
            ocm.Parameters.Add("@REQ_DATE", SqlDbType.Char, 8, "REQ_DATE").SourceVersion = DataRowVersion.Original;

            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion

		#region DEL_TRSHREQ insert command

        private SqlCommand GetDEL_TRSHREQInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DEL_TRSHREQ(SABN, REQ_YEAR, REQ_DATE, REQ_DATE2, REQ_TYPE, REQ_TYPE2, YC_DAYS, AP_TAG, LINE_CNT, GW_SABN1, GW_DT1, GW_NAME1, GW_JICK1, GW_SABN2, GW_DT2, GW_CHKID2, GW_NAME2, GW_JICK2, GW_SABN3, GW_DT3, GW_CHKID3, GW_NAME3, GW_JICK3, GW_SABN4, GW_DT4, GW_CHKID4, GW_NAME4, GW_JICK4, INDT, UPDT, USID, PSTY, DEL_DT, DEL_ID"
                                   + ") VALUES (@SABN, @REQ_YEAR, @REQ_DATE, @REQ_DATE2, @REQ_TYPE, @REQ_TYPE2, @YC_DAYS, @AP_TAG, @LINE_CNT, @GW_SABN1, @GW_DT1, @GW_NAME1, @GW_JICK1, @GW_SABN2, @GW_DT2, @GW_CHKID2, @GW_NAME2, @GW_JICK2, @GW_SABN3, @GW_DT3, @GW_CHKID3, @GW_NAME3, @GW_JICK3, @GW_SABN4, @GW_DT4, @GW_CHKID4, @GW_NAME4, @GW_JICK4, @INDT, @UPDT, @USID, @PSTY, @DEL_DT, @DEL_ID"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@SABN", SqlDbType.VarChar, 20, "SABN");
            ocm.Parameters.Add("@REQ_YEAR", SqlDbType.Char, 4, "REQ_YEAR");
            ocm.Parameters.Add("@REQ_DATE", SqlDbType.Char, 8, "REQ_DATE");
            ocm.Parameters.Add("@REQ_DATE2", SqlDbType.Char, 8, "REQ_DATE2");
            ocm.Parameters.Add("@REQ_TYPE", SqlDbType.VarChar, 10, "REQ_TYPE");
            ocm.Parameters.Add("@REQ_TYPE2", SqlDbType.VarChar, 10, "REQ_TYPE2");
            ocm.Parameters.Add("@YC_DAYS", SqlDbType.Decimal, 7, "YC_DAYS");
            ocm.Parameters.Add("@AP_TAG", SqlDbType.Char, 1, "AP_TAG");
            ocm.Parameters.Add("@LINE_CNT", SqlDbType.Int, 1, "LINE_CNT");
            ocm.Parameters.Add("@GW_SABN1", SqlDbType.VarChar, 20, "GW_SABN1");
            ocm.Parameters.Add("@GW_DT1", SqlDbType.VarChar, 20, "GW_DT1");
            ocm.Parameters.Add("@GW_NAME1", SqlDbType.VarChar, 20, "GW_NAME1");
            ocm.Parameters.Add("@GW_JICK1", SqlDbType.VarChar, 20, "GW_JICK1");
            ocm.Parameters.Add("@GW_SABN2", SqlDbType.VarChar, 20, "GW_SABN2");
            ocm.Parameters.Add("@GW_DT2", SqlDbType.VarChar, 20, "GW_DT2");
            ocm.Parameters.Add("@GW_CHKID2", SqlDbType.VarChar, 20, "GW_CHKID2");
            ocm.Parameters.Add("@GW_NAME2", SqlDbType.VarChar, 20, "GW_NAME2");
            ocm.Parameters.Add("@GW_JICK2", SqlDbType.VarChar, 20, "GW_JICK2");
            ocm.Parameters.Add("@GW_SABN3", SqlDbType.VarChar, 20, "GW_SABN3");
            ocm.Parameters.Add("@GW_DT3", SqlDbType.VarChar, 20, "GW_DT3");
            ocm.Parameters.Add("@GW_CHKID3", SqlDbType.VarChar, 20, "GW_CHKID3");
            ocm.Parameters.Add("@GW_NAME3", SqlDbType.VarChar, 20, "GW_NAME3");
            ocm.Parameters.Add("@GW_JICK3", SqlDbType.VarChar, 20, "GW_JICK3");
            ocm.Parameters.Add("@GW_SABN4", SqlDbType.VarChar, 20, "GW_SABN4");
            ocm.Parameters.Add("@GW_DT4", SqlDbType.VarChar, 20, "GW_DT4");
            ocm.Parameters.Add("@GW_CHKID4", SqlDbType.VarChar, 20, "GW_CHKID4");
            ocm.Parameters.Add("@GW_NAME4", SqlDbType.VarChar, 20, "GW_NAME4");
            ocm.Parameters.Add("@GW_JICK4", SqlDbType.VarChar, 20, "GW_JICK4");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");
            ocm.Parameters.Add("@DEL_DT", SqlDbType.VarChar, 20, "DEL_DT");
            ocm.Parameters.Add("@DEL_ID", SqlDbType.VarChar, 20, "DEL_ID");

            return ocm;
        }

        #endregion

		#region DUTY_TRSJREQ insert, update,delete command

        private SqlCommand GetDUTY_TRSJREQInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_TRSJREQ(SABN, REQ_DATE, REQ_DATE2, REQ_TYPE, HOLI_DAYS, PAY_YN, SAWON_LV, EXCEPT_MID, AP_TAG, MID_DT, MID_USID, AP_DT, AP_USID, CANC_DT, CANC_USID, INDT, UPDT, USID, PSTY"
                                   + ") VALUES (@SABN, @REQ_DATE, @REQ_DATE2, @REQ_TYPE, @HOLI_DAYS, @PAY_YN, @SAWON_LV, @EXCEPT_MID, @AP_TAG, @MID_DT, @MID_USID, @AP_DT, @AP_USID, @CANC_DT, @CANC_USID, @INDT, @UPDT, @USID, @PSTY"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@SABN", SqlDbType.VarChar, 20, "SABN");
            ocm.Parameters.Add("@REQ_DATE", SqlDbType.Char, 8, "REQ_DATE");
            ocm.Parameters.Add("@REQ_DATE2", SqlDbType.Char, 8, "REQ_DATE2");
            ocm.Parameters.Add("@REQ_TYPE", SqlDbType.VarChar, 10, "REQ_TYPE");
            ocm.Parameters.Add("@HOLI_DAYS", SqlDbType.Decimal, 7, "HOLI_DAYS");
            ocm.Parameters.Add("@PAY_YN", SqlDbType.Int, 4, "PAY_YN");
            ocm.Parameters.Add("@SAWON_LV", SqlDbType.Char, 1, "SAWON_LV");
            ocm.Parameters.Add("@EXCEPT_MID", SqlDbType.Char, 1, "EXCEPT_MID");
            ocm.Parameters.Add("@AP_TAG", SqlDbType.Char, 1, "AP_TAG");
            ocm.Parameters.Add("@MID_DT", SqlDbType.VarChar, 20, "MID_DT");
            ocm.Parameters.Add("@MID_USID", SqlDbType.VarChar, 20, "MID_USID");
            ocm.Parameters.Add("@AP_DT", SqlDbType.VarChar, 20, "AP_DT");
            ocm.Parameters.Add("@AP_USID", SqlDbType.VarChar, 20, "AP_USID");
            ocm.Parameters.Add("@CANC_DT", SqlDbType.VarChar, 20, "CANC_DT");
            ocm.Parameters.Add("@CANC_USID", SqlDbType.VarChar, 20, "CANC_USID");
            //ocm.Parameters.Add("@LAST_TAG", SqlDbType.Char, 1, "LAST_TAG");
            //ocm.Parameters.Add("@LAST_DT", SqlDbType.VarChar, 20, "LAST_DT");
            //ocm.Parameters.Add("@LAST_USID", SqlDbType.VarChar, 20, "LAST_USID");
            //ocm.Parameters.Add("@LAST_CANC_DT", SqlDbType.VarChar, 20, "LAST_CANC_DT");
            //ocm.Parameters.Add("@LAST_CANC_USID", SqlDbType.VarChar, 20, "LAST_CANC_USID");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_TRSJREQUpCmd()

        {
            string queryStatements = "UPDATE DBO.DUTY_TRSJREQ SET "
                                   + "   REQ_TYPE = @REQ_TYPE, "
                                   + "   HOLI_DAYS = @HOLI_DAYS, "
                                   + "   PAY_YN = @PAY_YN, "
                                   + "   SAWON_LV = @SAWON_LV, "
                                   + "   EXCEPT_MID = @EXCEPT_MID, "
                                   + "   AP_TAG = @AP_TAG, "
                                   + "   MID_DT = @MID_DT, "
                                   + "   MID_USID = @MID_USID, "
                                   + "   AP_DT = @AP_DT, "
                                   + "   AP_USID = @AP_USID, "
                                   + "   CANC_DT = @CANC_DT, "
                                   + "   CANC_USID = @CANC_USID, "
                                   //+ "   LAST_TAG = @LAST_TAG, "
                                   //+ "   LAST_DT = @LAST_DT, "
                                   //+ "   LAST_USID = @LAST_USID, "
                                   //+ "   LAST_CANC_DT = @LAST_CANC_DT, "
                                   //+ "   LAST_CANC_USID = @LAST_CANC_USID, "
                                   + "   INDT = @INDT, "
                                   + "   UPDT = @UPDT, "
                                   + "   USID = @USID, "
                                   + "   PSTY = @PSTY"
                                   + " WHERE SABN = @SABN"
                                   + "   AND REQ_DATE = @REQ_DATE "
                                   + "   AND REQ_DATE2 = @REQ_DATE2 "
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@SABN", SqlDbType.VarChar, 20, "SABN");
            ocm.Parameters.Add("@REQ_DATE", SqlDbType.Char, 8, "REQ_DATE");
            ocm.Parameters.Add("@REQ_DATE2", SqlDbType.Char, 8, "REQ_DATE2");
            ocm.Parameters.Add("@REQ_TYPE", SqlDbType.VarChar, 10, "REQ_TYPE");
            ocm.Parameters.Add("@HOLI_DAYS", SqlDbType.Decimal, 7, "HOLI_DAYS");
            ocm.Parameters.Add("@PAY_YN", SqlDbType.Int, 4, "PAY_YN");
            ocm.Parameters.Add("@SAWON_LV", SqlDbType.Char, 1, "SAWON_LV");
            ocm.Parameters.Add("@EXCEPT_MID", SqlDbType.Char, 1, "EXCEPT_MID");
            ocm.Parameters.Add("@AP_TAG", SqlDbType.Char, 1, "AP_TAG");
            ocm.Parameters.Add("@MID_DT", SqlDbType.VarChar, 20, "MID_DT");
            ocm.Parameters.Add("@MID_USID", SqlDbType.VarChar, 20, "MID_USID");
            ocm.Parameters.Add("@AP_DT", SqlDbType.VarChar, 20, "AP_DT");
            ocm.Parameters.Add("@AP_USID", SqlDbType.VarChar, 20, "AP_USID");
            ocm.Parameters.Add("@CANC_DT", SqlDbType.VarChar, 20, "CANC_DT");
            ocm.Parameters.Add("@CANC_USID", SqlDbType.VarChar, 20, "CANC_USID");
            //ocm.Parameters.Add("@LAST_TAG", SqlDbType.Char, 1, "LAST_TAG");
            //ocm.Parameters.Add("@LAST_DT", SqlDbType.VarChar, 20, "LAST_DT");
            //ocm.Parameters.Add("@LAST_USID", SqlDbType.VarChar, 20, "LAST_USID");
            //ocm.Parameters.Add("@LAST_CANC_DT", SqlDbType.VarChar, 20, "LAST_CANC_DT");
            //ocm.Parameters.Add("@LAST_CANC_USID", SqlDbType.VarChar, 20, "LAST_CANC_USID");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_TRSJREQDelCmd()

        {
            string queryStatements = "DELETE DBO.DUTY_TRSJREQ"
                                   + " WHERE SABN = @SABN"
                                   + "   AND REQ_DATE = @REQ_DATE"
                                   + "   AND REQ_DATE2 = @REQ_DATE2 "
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@SABN", SqlDbType.VarChar, 20, "SABN").SourceVersion = DataRowVersion.Original;
            ocm.Parameters.Add("@REQ_DATE", SqlDbType.Char, 8, "REQ_DATE").SourceVersion = DataRowVersion.Original;
            ocm.Parameters.Add("@REQ_DATE2", SqlDbType.Char, 8, "REQ_DATE2").SourceVersion = DataRowVersion.Original;

            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion

		#region DEL_TRSJREQ insert command

        private SqlCommand GetDEL_TRSJREQInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DEL_TRSJREQ(SABN, REQ_DATE, REQ_DATE2, REQ_TYPE, HOLI_DAYS, PAY_YN, SAWON_LV, EXCEPT_MID, AP_TAG, MID_DT, MID_USID, AP_DT, AP_USID, CANC_DT, CANC_USID, INDT, UPDT, USID, PSTY, DEL_DT, DEL_ID"
                                   + ") VALUES (@SABN, @REQ_DATE, @REQ_DATE2, @REQ_TYPE, @HOLI_DAYS, @PAY_YN, @SAWON_LV, @EXCEPT_MID, @AP_TAG, @MID_DT, @MID_USID, @AP_DT, @AP_USID, @CANC_DT, @CANC_USID, @INDT, @UPDT, @USID, @PSTY, @DEL_DT, @DEL_ID"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@SABN", SqlDbType.VarChar, 20, "SABN");
            ocm.Parameters.Add("@REQ_DATE", SqlDbType.Char, 8, "REQ_DATE");
            ocm.Parameters.Add("@REQ_DATE2", SqlDbType.Char, 8, "REQ_DATE2");
            ocm.Parameters.Add("@REQ_TYPE", SqlDbType.VarChar, 10, "REQ_TYPE");
            ocm.Parameters.Add("@HOLI_DAYS", SqlDbType.Decimal, 7, "HOLI_DAYS");
            ocm.Parameters.Add("@PAY_YN", SqlDbType.Int, 4, "PAY_YN");
            ocm.Parameters.Add("@SAWON_LV", SqlDbType.Char, 1, "SAWON_LV");
            ocm.Parameters.Add("@EXCEPT_MID", SqlDbType.Char, 1, "EXCEPT_MID");
            ocm.Parameters.Add("@AP_TAG", SqlDbType.Char, 1, "AP_TAG");
            ocm.Parameters.Add("@MID_DT", SqlDbType.VarChar, 20, "MID_DT");
            ocm.Parameters.Add("@MID_USID", SqlDbType.VarChar, 20, "MID_USID");
            ocm.Parameters.Add("@AP_DT", SqlDbType.VarChar, 20, "AP_DT");
            ocm.Parameters.Add("@AP_USID", SqlDbType.VarChar, 20, "AP_USID");
            ocm.Parameters.Add("@CANC_DT", SqlDbType.VarChar, 20, "CANC_DT");
            ocm.Parameters.Add("@CANC_USID", SqlDbType.VarChar, 20, "CANC_USID");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");
            ocm.Parameters.Add("@DEL_DT", SqlDbType.VarChar, 20, "DEL_DT");
            ocm.Parameters.Add("@DEL_ID", SqlDbType.VarChar, 20, "DEL_ID");

            return ocm;
        }

        #endregion

		#region DUTY_MSTYCCJ insert, update,delete command

        private SqlCommand GetDUTY_MSTYCCJInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_MSTYCCJ(YC_YEAR, DOC_TYPE, SAWON_NO, SAWON_NM, YC_SQ, YC_IPDT, CALC_FRDT, CALC_TODT, YC_STDT, YC_TDAY, USE_FRDT, USE_TODT, YC_USE_DAY, YC_REMAIN_DAY, SEND_MAIL, SEND_DT, SEND_ID, RECEIVE_MAIL, READ_YN, READ_DT, READ_ID"
                                   + ") VALUES (@YC_YEAR, @DOC_TYPE, @SAWON_NO, @SAWON_NM, @YC_SQ, @YC_IPDT, @CALC_FRDT, @CALC_TODT, @YC_STDT, @YC_TDAY, @USE_FRDT, @USE_TODT, @YC_USE_DAY, @YC_REMAIN_DAY, @SEND_MAIL, @SEND_DT, @SEND_ID, @RECEIVE_MAIL, @READ_YN, @READ_DT, @READ_ID"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@YC_YEAR", SqlDbType.Char, 4, "YC_YEAR");
            ocm.Parameters.Add("@DOC_TYPE", SqlDbType.Char, 6, "DOC_TYPE");
            ocm.Parameters.Add("@SAWON_NO", SqlDbType.VarChar, 15, "SAWON_NO");
            ocm.Parameters.Add("@SAWON_NM", SqlDbType.VarChar, 40, "SAWON_NM");
            ocm.Parameters.Add("@YC_SQ", SqlDbType.Int, 4, "YC_SQ");
            ocm.Parameters.Add("@YC_IPDT", SqlDbType.Char, 8, "YC_IPDT");
            ocm.Parameters.Add("@CALC_FRDT", SqlDbType.Char, 8, "CALC_FRDT");
            ocm.Parameters.Add("@CALC_TODT", SqlDbType.Char, 8, "CALC_TODT");
            ocm.Parameters.Add("@YC_STDT", SqlDbType.Char, 8, "YC_STDT");
            ocm.Parameters.Add("@YC_TDAY", SqlDbType.Int, 4, "YC_TDAY");
            ocm.Parameters.Add("@USE_FRDT", SqlDbType.Char, 8, "USE_FRDT");
            ocm.Parameters.Add("@USE_TODT", SqlDbType.Char, 8, "USE_TODT");
            ocm.Parameters.Add("@YC_USE_DAY", SqlDbType.Decimal, 5, "YC_USE_DAY");
            ocm.Parameters.Add("@YC_REMAIN_DAY", SqlDbType.Decimal, 5, "YC_REMAIN_DAY");
            ocm.Parameters.Add("@SEND_MAIL", SqlDbType.VarChar, 40, "SEND_MAIL");
            ocm.Parameters.Add("@SEND_DT", SqlDbType.VarChar, 20, "SEND_DT");
            ocm.Parameters.Add("@SEND_ID", SqlDbType.VarChar, 20, "SEND_ID");
            ocm.Parameters.Add("@RECEIVE_MAIL", SqlDbType.VarChar, 40, "RECEIVE_MAIL");
            ocm.Parameters.Add("@READ_YN", SqlDbType.Char, 1, "READ_YN");
            ocm.Parameters.Add("@READ_DT", SqlDbType.VarChar, 20, "READ_DT");
            ocm.Parameters.Add("@READ_ID", SqlDbType.VarChar, 20, "READ_ID");

            return ocm;
        }

        #endregion
		
		#region DEL_DUTY_TRSHREQ delete command

        private SqlCommand GetDEL_DUTY_TRSHREQDelCmd()

        {
            string queryStatements = "DELETE DBO.DUTY_TRSHREQ"
                                   + " WHERE SABN = @SABN"
                                   + "   AND REQ_YEAR = @REQ_YEAR"
                                   + "   AND REQ_DATE = @REQ_DATE"
                                   + "   AND AP_TAG='9' ";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@SABN", SqlDbType.VarChar, 20, "SABN").SourceVersion = DataRowVersion.Original;
            ocm.Parameters.Add("@REQ_YEAR", SqlDbType.Char, 4, "REQ_YEAR").SourceVersion = DataRowVersion.Original;
            ocm.Parameters.Add("@REQ_DATE", SqlDbType.Char, 8, "REQ_DATE").SourceVersion = DataRowVersion.Original;

            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion

		#region DUTY_MSTGTMM insert, update,delete command

        private SqlCommand GetDUTY_MSTGTMMInCmd()
        {
            string queryStatements = "INSERT INTO DBO.DUTY_MSTGTMM(GT_YYMM, SAWON_NO, DEPTCODE, GTMMGT01, GTMMGT02, GTMMGT03, GTMMGT04, GTMMGT05, GTMMGT06, GTMMGT07, GTMMGT08, GTMMGT09, GTMMGT10, GTMMGT11, GTMMGT12, GTMMGT13, GTMMGT14, GTMMGT15, GTMMGT16, GTMMGT17, GTMMGT18, GTMMGT19, GTMMGT20, GTMMGT21, GTMMGT22, GTMMGT23, GTMMGT24, GTMMGT25, GTMMGT26, GTMMGT27, GTMMGT28, GTMMGT29, GTMMGT30, GTMMGT31, GTMMGT32, GTMMGT33, GTMMGT34, GTMMGT35, GTMMGT36, GTMMGT37, GTMMGT38, GTMMGT39, GTMMGT40, GTMMGT41, GTMMGT42, GTMMGT43, GTMMGT44, GTMMGT45, GTMMGT46, GTMMGT47, GTMMGT48, GTMMGT49, GTMMGT50, INDT, UPDT, USID, PSTY"
                                   + ") VALUES (@GT_YYMM, @SAWON_NO, @DEPTCODE, @GTMMGT01, @GTMMGT02, @GTMMGT03, @GTMMGT04, @GTMMGT05, @GTMMGT06, @GTMMGT07, @GTMMGT08, @GTMMGT09, @GTMMGT10, @GTMMGT11, @GTMMGT12, @GTMMGT13, @GTMMGT14, @GTMMGT15, @GTMMGT16, @GTMMGT17, @GTMMGT18, @GTMMGT19, @GTMMGT20, @GTMMGT21, @GTMMGT22, @GTMMGT23, @GTMMGT24, @GTMMGT25, @GTMMGT26, @GTMMGT27, @GTMMGT28, @GTMMGT29, @GTMMGT30, @GTMMGT31, @GTMMGT32, @GTMMGT33, @GTMMGT34, @GTMMGT35, @GTMMGT36, @GTMMGT37, @GTMMGT38, @GTMMGT39, @GTMMGT40, @GTMMGT41, @GTMMGT42, @GTMMGT43, @GTMMGT44, @GTMMGT45, @GTMMGT46, @GTMMGT47, @GTMMGT48, @GTMMGT49, @GTMMGT50, @INDT, @UPDT, @USID, @PSTY"
                                   + ")";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@GT_YYMM", SqlDbType.Char, 6, "GT_YYMM");
            ocm.Parameters.Add("@SAWON_NO", SqlDbType.VarChar, 15, "SAWON_NO");
            ocm.Parameters.Add("@DEPTCODE", SqlDbType.Char, 4, "DEPTCODE");
            ocm.Parameters.Add("@GTMMGT01", SqlDbType.Decimal, 5, "GTMMGT01");
            ocm.Parameters.Add("@GTMMGT02", SqlDbType.Decimal, 5, "GTMMGT02");
            ocm.Parameters.Add("@GTMMGT03", SqlDbType.Decimal, 5, "GTMMGT03");
            ocm.Parameters.Add("@GTMMGT04", SqlDbType.Decimal, 5, "GTMMGT04");
            ocm.Parameters.Add("@GTMMGT05", SqlDbType.Decimal, 5, "GTMMGT05");
            ocm.Parameters.Add("@GTMMGT06", SqlDbType.Decimal, 5, "GTMMGT06");
            ocm.Parameters.Add("@GTMMGT07", SqlDbType.Decimal, 5, "GTMMGT07");
            ocm.Parameters.Add("@GTMMGT08", SqlDbType.Decimal, 5, "GTMMGT08");
            ocm.Parameters.Add("@GTMMGT09", SqlDbType.Decimal, 5, "GTMMGT09");
            ocm.Parameters.Add("@GTMMGT10", SqlDbType.Decimal, 5, "GTMMGT10");
            ocm.Parameters.Add("@GTMMGT11", SqlDbType.Decimal, 5, "GTMMGT11");
            ocm.Parameters.Add("@GTMMGT12", SqlDbType.Decimal, 5, "GTMMGT12");
            ocm.Parameters.Add("@GTMMGT13", SqlDbType.Decimal, 5, "GTMMGT13");
            ocm.Parameters.Add("@GTMMGT14", SqlDbType.Decimal, 5, "GTMMGT14");
            ocm.Parameters.Add("@GTMMGT15", SqlDbType.Decimal, 5, "GTMMGT15");
            ocm.Parameters.Add("@GTMMGT16", SqlDbType.Decimal, 5, "GTMMGT16");
            ocm.Parameters.Add("@GTMMGT17", SqlDbType.Decimal, 5, "GTMMGT17");
            ocm.Parameters.Add("@GTMMGT18", SqlDbType.Decimal, 5, "GTMMGT18");
            ocm.Parameters.Add("@GTMMGT19", SqlDbType.Decimal, 5, "GTMMGT19");
            ocm.Parameters.Add("@GTMMGT20", SqlDbType.Decimal, 5, "GTMMGT20");
            ocm.Parameters.Add("@GTMMGT21", SqlDbType.Decimal, 5, "GTMMGT21");
            ocm.Parameters.Add("@GTMMGT22", SqlDbType.Decimal, 5, "GTMMGT22");
            ocm.Parameters.Add("@GTMMGT23", SqlDbType.Decimal, 5, "GTMMGT23");
            ocm.Parameters.Add("@GTMMGT24", SqlDbType.Decimal, 5, "GTMMGT24");
            ocm.Parameters.Add("@GTMMGT25", SqlDbType.Decimal, 5, "GTMMGT25");
            ocm.Parameters.Add("@GTMMGT26", SqlDbType.Decimal, 5, "GTMMGT26");
            ocm.Parameters.Add("@GTMMGT27", SqlDbType.Decimal, 5, "GTMMGT27");
            ocm.Parameters.Add("@GTMMGT28", SqlDbType.Decimal, 5, "GTMMGT28");
            ocm.Parameters.Add("@GTMMGT29", SqlDbType.Decimal, 5, "GTMMGT29");
            ocm.Parameters.Add("@GTMMGT30", SqlDbType.Decimal, 5, "GTMMGT30");
            ocm.Parameters.Add("@GTMMGT31", SqlDbType.Decimal, 5, "GTMMGT31");
            ocm.Parameters.Add("@GTMMGT32", SqlDbType.Decimal, 5, "GTMMGT32");
            ocm.Parameters.Add("@GTMMGT33", SqlDbType.Decimal, 5, "GTMMGT33");
            ocm.Parameters.Add("@GTMMGT34", SqlDbType.Decimal, 5, "GTMMGT34");
            ocm.Parameters.Add("@GTMMGT35", SqlDbType.Decimal, 5, "GTMMGT35");
            ocm.Parameters.Add("@GTMMGT36", SqlDbType.Decimal, 5, "GTMMGT36");
            ocm.Parameters.Add("@GTMMGT37", SqlDbType.Decimal, 5, "GTMMGT37");
            ocm.Parameters.Add("@GTMMGT38", SqlDbType.Decimal, 5, "GTMMGT38");
            ocm.Parameters.Add("@GTMMGT39", SqlDbType.Decimal, 5, "GTMMGT39");
            ocm.Parameters.Add("@GTMMGT40", SqlDbType.Decimal, 5, "GTMMGT40");
            ocm.Parameters.Add("@GTMMGT41", SqlDbType.Decimal, 5, "GTMMGT41");
            ocm.Parameters.Add("@GTMMGT42", SqlDbType.Decimal, 5, "GTMMGT42");
            ocm.Parameters.Add("@GTMMGT43", SqlDbType.Decimal, 5, "GTMMGT43");
            ocm.Parameters.Add("@GTMMGT44", SqlDbType.Decimal, 5, "GTMMGT44");
            ocm.Parameters.Add("@GTMMGT45", SqlDbType.Decimal, 5, "GTMMGT45");
            ocm.Parameters.Add("@GTMMGT46", SqlDbType.Decimal, 5, "GTMMGT46");
            ocm.Parameters.Add("@GTMMGT47", SqlDbType.Decimal, 5, "GTMMGT47");
            ocm.Parameters.Add("@GTMMGT48", SqlDbType.Decimal, 5, "GTMMGT48");
            ocm.Parameters.Add("@GTMMGT49", SqlDbType.Decimal, 5, "GTMMGT49");
            ocm.Parameters.Add("@GTMMGT50", SqlDbType.Decimal, 5, "GTMMGT50");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_MSTGTMMUpCmd()

        {
            string queryStatements = "UPDATE DBO.DUTY_MSTGTMM SET "
                                   + "   DEPTCODE = @DEPTCODE, "
                                   + "   GTMMGT01 = @GTMMGT01, "
                                   + "   GTMMGT02 = @GTMMGT02, "
                                   + "   GTMMGT03 = @GTMMGT03, "
                                   + "   GTMMGT04 = @GTMMGT04, "
                                   + "   GTMMGT05 = @GTMMGT05, "
                                   + "   GTMMGT06 = @GTMMGT06, "
                                   + "   GTMMGT07 = @GTMMGT07, "
                                   + "   GTMMGT08 = @GTMMGT08, "
                                   + "   GTMMGT09 = @GTMMGT09, "
                                   + "   GTMMGT10 = @GTMMGT10, "
                                   + "   GTMMGT11 = @GTMMGT11, "
                                   + "   GTMMGT12 = @GTMMGT12, "
                                   + "   GTMMGT13 = @GTMMGT13, "
                                   + "   GTMMGT14 = @GTMMGT14, "
                                   + "   GTMMGT15 = @GTMMGT15, "
                                   + "   GTMMGT16 = @GTMMGT16, "
                                   + "   GTMMGT17 = @GTMMGT17, "
                                   + "   GTMMGT18 = @GTMMGT18, "
                                   + "   GTMMGT19 = @GTMMGT19, "
                                   + "   GTMMGT20 = @GTMMGT20, "
                                   + "   GTMMGT21 = @GTMMGT21, "
                                   + "   GTMMGT22 = @GTMMGT22, "
                                   + "   GTMMGT23 = @GTMMGT23, "
                                   + "   GTMMGT24 = @GTMMGT24, "
                                   + "   GTMMGT25 = @GTMMGT25, "
                                   + "   GTMMGT26 = @GTMMGT26, "
                                   + "   GTMMGT27 = @GTMMGT27, "
                                   + "   GTMMGT28 = @GTMMGT28, "
                                   + "   GTMMGT29 = @GTMMGT29, "
                                   + "   GTMMGT30 = @GTMMGT30, "
                                   + "   GTMMGT31 = @GTMMGT31, "
                                   + "   GTMMGT32 = @GTMMGT32, "
                                   + "   GTMMGT33 = @GTMMGT33, "
                                   + "   GTMMGT34 = @GTMMGT34, "
                                   + "   GTMMGT35 = @GTMMGT35, "
                                   + "   GTMMGT36 = @GTMMGT36, "
                                   + "   GTMMGT37 = @GTMMGT37, "
                                   + "   GTMMGT38 = @GTMMGT38, "
                                   + "   GTMMGT39 = @GTMMGT39, "
                                   + "   GTMMGT40 = @GTMMGT40, "
                                   + "   GTMMGT41 = @GTMMGT41, "
                                   + "   GTMMGT42 = @GTMMGT42, "
                                   + "   GTMMGT43 = @GTMMGT43, "
                                   + "   GTMMGT44 = @GTMMGT44, "
                                   + "   GTMMGT45 = @GTMMGT45, "
                                   + "   GTMMGT46 = @GTMMGT46, "
                                   + "   GTMMGT47 = @GTMMGT47, "
                                   + "   GTMMGT48 = @GTMMGT48, "
                                   + "   GTMMGT49 = @GTMMGT49, "
                                   + "   GTMMGT50 = @GTMMGT50, "
                                   + "   INDT = @INDT, "
                                   + "   UPDT = @UPDT, "
                                   + "   USID = @USID, "
                                   + "   PSTY = @PSTY"
                                   + " WHERE GT_YYMM = @GT_YYMM"
                                   + "   AND SAWON_NO = @SAWON_NO"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@GT_YYMM", SqlDbType.Char, 6, "GT_YYMM");
            ocm.Parameters.Add("@SAWON_NO", SqlDbType.VarChar, 15, "SAWON_NO");
            ocm.Parameters.Add("@DEPTCODE", SqlDbType.Char, 4, "DEPTCODE");
            ocm.Parameters.Add("@GTMMGT01", SqlDbType.Decimal, 5, "GTMMGT01");
            ocm.Parameters.Add("@GTMMGT02", SqlDbType.Decimal, 5, "GTMMGT02");
            ocm.Parameters.Add("@GTMMGT03", SqlDbType.Decimal, 5, "GTMMGT03");
            ocm.Parameters.Add("@GTMMGT04", SqlDbType.Decimal, 5, "GTMMGT04");
            ocm.Parameters.Add("@GTMMGT05", SqlDbType.Decimal, 5, "GTMMGT05");
            ocm.Parameters.Add("@GTMMGT06", SqlDbType.Decimal, 5, "GTMMGT06");
            ocm.Parameters.Add("@GTMMGT07", SqlDbType.Decimal, 5, "GTMMGT07");
            ocm.Parameters.Add("@GTMMGT08", SqlDbType.Decimal, 5, "GTMMGT08");
            ocm.Parameters.Add("@GTMMGT09", SqlDbType.Decimal, 5, "GTMMGT09");
            ocm.Parameters.Add("@GTMMGT10", SqlDbType.Decimal, 5, "GTMMGT10");
            ocm.Parameters.Add("@GTMMGT11", SqlDbType.Decimal, 5, "GTMMGT11");
            ocm.Parameters.Add("@GTMMGT12", SqlDbType.Decimal, 5, "GTMMGT12");
            ocm.Parameters.Add("@GTMMGT13", SqlDbType.Decimal, 5, "GTMMGT13");
            ocm.Parameters.Add("@GTMMGT14", SqlDbType.Decimal, 5, "GTMMGT14");
            ocm.Parameters.Add("@GTMMGT15", SqlDbType.Decimal, 5, "GTMMGT15");
            ocm.Parameters.Add("@GTMMGT16", SqlDbType.Decimal, 5, "GTMMGT16");
            ocm.Parameters.Add("@GTMMGT17", SqlDbType.Decimal, 5, "GTMMGT17");
            ocm.Parameters.Add("@GTMMGT18", SqlDbType.Decimal, 5, "GTMMGT18");
            ocm.Parameters.Add("@GTMMGT19", SqlDbType.Decimal, 5, "GTMMGT19");
            ocm.Parameters.Add("@GTMMGT20", SqlDbType.Decimal, 5, "GTMMGT20");
            ocm.Parameters.Add("@GTMMGT21", SqlDbType.Decimal, 5, "GTMMGT21");
            ocm.Parameters.Add("@GTMMGT22", SqlDbType.Decimal, 5, "GTMMGT22");
            ocm.Parameters.Add("@GTMMGT23", SqlDbType.Decimal, 5, "GTMMGT23");
            ocm.Parameters.Add("@GTMMGT24", SqlDbType.Decimal, 5, "GTMMGT24");
            ocm.Parameters.Add("@GTMMGT25", SqlDbType.Decimal, 5, "GTMMGT25");
            ocm.Parameters.Add("@GTMMGT26", SqlDbType.Decimal, 5, "GTMMGT26");
            ocm.Parameters.Add("@GTMMGT27", SqlDbType.Decimal, 5, "GTMMGT27");
            ocm.Parameters.Add("@GTMMGT28", SqlDbType.Decimal, 5, "GTMMGT28");
            ocm.Parameters.Add("@GTMMGT29", SqlDbType.Decimal, 5, "GTMMGT29");
            ocm.Parameters.Add("@GTMMGT30", SqlDbType.Decimal, 5, "GTMMGT30");
            ocm.Parameters.Add("@GTMMGT31", SqlDbType.Decimal, 5, "GTMMGT31");
            ocm.Parameters.Add("@GTMMGT32", SqlDbType.Decimal, 5, "GTMMGT32");
            ocm.Parameters.Add("@GTMMGT33", SqlDbType.Decimal, 5, "GTMMGT33");
            ocm.Parameters.Add("@GTMMGT34", SqlDbType.Decimal, 5, "GTMMGT34");
            ocm.Parameters.Add("@GTMMGT35", SqlDbType.Decimal, 5, "GTMMGT35");
            ocm.Parameters.Add("@GTMMGT36", SqlDbType.Decimal, 5, "GTMMGT36");
            ocm.Parameters.Add("@GTMMGT37", SqlDbType.Decimal, 5, "GTMMGT37");
            ocm.Parameters.Add("@GTMMGT38", SqlDbType.Decimal, 5, "GTMMGT38");
            ocm.Parameters.Add("@GTMMGT39", SqlDbType.Decimal, 5, "GTMMGT39");
            ocm.Parameters.Add("@GTMMGT40", SqlDbType.Decimal, 5, "GTMMGT40");
            ocm.Parameters.Add("@GTMMGT41", SqlDbType.Decimal, 5, "GTMMGT41");
            ocm.Parameters.Add("@GTMMGT42", SqlDbType.Decimal, 5, "GTMMGT42");
            ocm.Parameters.Add("@GTMMGT43", SqlDbType.Decimal, 5, "GTMMGT43");
            ocm.Parameters.Add("@GTMMGT44", SqlDbType.Decimal, 5, "GTMMGT44");
            ocm.Parameters.Add("@GTMMGT45", SqlDbType.Decimal, 5, "GTMMGT45");
            ocm.Parameters.Add("@GTMMGT46", SqlDbType.Decimal, 5, "GTMMGT46");
            ocm.Parameters.Add("@GTMMGT47", SqlDbType.Decimal, 5, "GTMMGT47");
            ocm.Parameters.Add("@GTMMGT48", SqlDbType.Decimal, 5, "GTMMGT48");
            ocm.Parameters.Add("@GTMMGT49", SqlDbType.Decimal, 5, "GTMMGT49");
            ocm.Parameters.Add("@GTMMGT50", SqlDbType.Decimal, 5, "GTMMGT50");
            ocm.Parameters.Add("@INDT", SqlDbType.VarChar, 20, "INDT");
            ocm.Parameters.Add("@UPDT", SqlDbType.VarChar, 20, "UPDT");
            ocm.Parameters.Add("@USID", SqlDbType.VarChar, 20, "USID");
            ocm.Parameters.Add("@PSTY", SqlDbType.Char, 1, "PSTY");

            return ocm;
        }

        private SqlCommand GetDUTY_MSTGTMMDelCmd()

        {
            string queryStatements = "DELETE DBO.DUTY_MSTGTMM"
                                   + " WHERE GT_YYMM = @GT_YYMM"
                                   + "   AND SAWON_NO = @SAWON_NO"
                                   + "";

            SqlCommand ocm = new SqlCommand();
            ocm.CommandText = queryStatements;
            ocm.CommandType = CommandType.Text;

            ocm.Parameters.Add("@GT_YYMM", SqlDbType.Char, 6, "GT_YYMM").SourceVersion = DataRowVersion.Original;
            ocm.Parameters.Add("@SAWON_NO", SqlDbType.VarChar, 15, "SAWON_NO").SourceVersion = DataRowVersion.Original;

            ocm.UpdatedRowSource = UpdateRowSource.None;

            return ocm;
        }

        #endregion

		//ocm.Parameters.Add("@IDX", SqlDbType.Int, 4, "IDX").SourceVersion = DataRowVersion.Original;

        #endregion

    }
}
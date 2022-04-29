using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Ionic.Zip;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace FILEUPGRADE
{
    public partial class FileUpgrade : Form
    {
        private string[] _arrArgs;
        private string[] _arrFileList;
        private int _arrayCount { get; set; }
        private int _arrayIndex { get; set; }

        //개발자별 프로젝트별 설정하는 명칭*************************************

        /// <summary>
        /// 디버그모드일때 사용하는 startup path 
        /// 소스상에 3번정도 사용되니..여기서 선언해버리고..
        /// </summary>
        string Debug_StartPath = @"D:\SILKROAD\PROGDT01";
       
        /// <summary>소스상 사용할 실행파일명 </summary>
        string ExeFileName = "SILKDT01.EXE";

        /// <summary> 압축파일명 </summary>
        string ZipFileName = "Silkdt01Upgrade.zip";
		string ZipFileName2 = "Dev16.zip";

        /// <summary> 부모폴더 찾기위한 실행파일의 폴더 소문자여야함  </summary>
        string FolderName = "progdt01";
        string VersiontxtName = "dt01chk.txt";
        string VersiontxtName2 = "dev16chk.txt";

        DataSet ds = new DataSet();
        private long fileSize;
        string hostName = "ftp://115.68.15.121";
        //string userName = "";//"samil2010";
        //string password = "";//"samil35!";
        string bar_condition = "2";
        string ftp_msg = "";
        //***********************************************************************

        public FileUpgrade()
        {
            InitializeComponent();
        }

        public FileUpgrade(string[] args)
        {
            InitializeComponent();
            _arrArgs = (string[])args.Clone();

            if (args.Length > 0)
            {
                string test = string.Empty;
                for (int i = 0; i < args.Length; i++)
                {
                    test += args[i].Trim() + ", ";
                }
            }
        }

        private void FileUpgrade_Load(object sender, EventArgs e)
        {
            //if (ds.Tables["Info_Ftp"] != null)
            //    ds.Tables["Info_Ftp"].Clear();
            //string constr = "Data Source=115.68.15.121;Initial Catalog=MS07DB01;User Id=sa;Password=samil25;";  //ftp서버 리스트 조회
            //SqlConnection conn = new SqlConnection(constr);
            //conn.Open();
            //SqlCommand comm = conn.CreateCommand();
            //comm.CommandTimeout = 600;
            //comm.CommandText = "SELECT * FROM INFO_FTP";
            //SqlDataAdapter Adap = new SqlDataAdapter(comm);
            //Adap.Fill(ds, "Info_Ftp");
            //conn.Close();

            //if (ds.Tables["Info_Ftp"].Rows.Count > 0)
            //{
            //    userName = ds.Tables["Info_Ftp"].Rows[0]["FTP_ID"].ToString();//"samil2010";
            //    password = ds.Tables["Info_Ftp"].Rows[0]["FTP_PASSWORD"].ToString();//"samil35!";
            //}
        }
        private void FileUpgrade_Shown(object sender, EventArgs e)
        {
            CallBackGroundWorker();
        }

        private void FtpDownForm(BackgroundWorker worker)
        {
            try
            {
                fileSize = 0;
                if (ds.Tables["FtpDown"] != null)
                    ds.Tables["FtpDown"].Clear();
                string constr = "Data Source=115.68.15.121;Initial Catalog=MS07DB01;User Id=sa;Password=samil25;";  //ftp서버 리스트 조회
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                SqlCommand comm = conn.CreateCommand();
                comm.CommandTimeout = 600;
                comm.CommandText = " SELECT * FROM LIST_UPGRADE "
                                 + "  WHERE FILENAME NOT IN ('FILEUPGRADE.EXE','Ionic.Zip.dll') AND STAT = '1' AND PROG_NO = 'DT01' ";
                SqlDataAdapter Adap = new SqlDataAdapter(comm);
                Adap.Fill(ds, "FtpDown");
                conn.Close();

                string fullPath = Application.StartupPath;//"D:\\silkroad\\progms07";//

                if (ds.Tables["FtpDown"].Rows.Count > 0)
                {
                    _arrayCount = ds.Tables["FtpDown"].Rows.Count;
                    _arrayIndex = 0;
                    string msgVal = string.Empty;

                    for (int i = 0; i < ds.Tables["FtpDown"].Rows.Count; i++)
                    {
                        DataRow irow = ds.Tables["FtpDown"].Rows[i];
                        //password = irow["PASSWORD"].ToString();
                        int percentageValue = 0;
                        ++_arrayIndex;
                        percentageValue = (int)(((float)_arrayIndex / (float)_arrayCount * 100));

                        //1.진행율 = 현재다운진행파일수 / 다운로드받을 파일수
                        //Download("silkplus/silkplus/progse09", irow["FILENAME"].ToString(), irow["FILESIZE"].ToString().Replace(",", ""), fullPath);
                        msgVal = "Ftp Download..." + irow["FILENAME"].ToString() + "  " + _arrayIndex + "/" + (_arrayCount);
                        worker.ReportProgress(percentageValue, msgVal);                       
                    }
                }
            }
            catch (Exception ie)
            {
                MessageBox.Show(ie.Message, "Fn: FtpDownForm Error");
            }
        }
        public void Download(string ftpDirectoryName, string downFileName, string downFileSize, string localPath)
        {
            //try
            //{
            //    Uri ftpUri = new Uri(hostName + "/" + ftpDirectoryName + "/" + downFileName);
            //    string fullPath = Application.StartupPath + "\\" + downFileName;
            //    FileInfo fd = new FileInfo(fullPath);

            //    string fdsize = fd.Exists.ToString() == "False" ? "" : fd.Length.ToString();

            //    if (fdsize != downFileSize)  //파일사이즈가 다르면 ftp다운
            //    {
            //        using (WebClient request = new WebClient())
            //        {
            //            request.Credentials = new NetworkCredential(userName, password);
            //            request.DownloadFile(ftpUri, localPath + @"/" + downFileName);
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.ToString());
            //}
        }

        //localPath저장할 경로 progress프로그래스 바 showCompleted완료 메시지 보이기
        public void Download(string ftpDirectoryName, string downFileName, string downFileSize, string localPath, ProgressBar progressBar, bool showCompleted)
        {
            //try
            //{
            //    this.progressBar1 = progressBar;
            //    Uri ftpUri = new Uri(hostName + "/" + ftpDirectoryName + "/" + downFileName);

            //    string fullPath = Application.StartupPath + "\\" + downFileName;//"D:\\silkroad\\progms07\\" + downFileName;//
            //    FileInfo fd = new FileInfo(fullPath);
            //    string fdsize = fd.Exists.ToString() == "False" ? "" : fdsize = fd.Length.ToString();

            //    if (fdsize != downFileSize)
            //    {
            //        // 파일 사이즈
            //        FtpWebRequest reqFtp = (FtpWebRequest)WebRequest.Create(ftpUri);
            //        reqFtp.Method = WebRequestMethods.Ftp.GetFileSize;
            //        reqFtp.Credentials = new NetworkCredential(userName, password);
            //        FtpWebResponse resFtp = (FtpWebResponse)reqFtp.GetResponse();
            //        //fileSize = resFtp.ContentLength;
            //        resFtp.Close();

            //        ftp_msg = "Ftp Download......" + downFileName;
            //        using (WebClient request = new WebClient())
            //        {
            //            request.Credentials = new NetworkCredential(userName, password);
            //            request.DownloadProgressChanged += request_DownloadProgressChanged;
            //            // 다운로드가 완료 된 후 메시지 보이기
            //            if (showCompleted)
            //            {
            //                request.DownloadFileCompleted += request_DownloadFileCompleted;
            //            }
            //            // 다운로드 시작
            //            request.DownloadFileAsync(ftpUri, @localPath + "/" + downFileName);
            //        }
            //    }
            //    else
            //    {
            //        //labelControl2.Text = "현재 최신버전입니다.";
            //    }
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.ToString());
            //}
        }
        void request_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = Convert.ToInt32(Convert.ToDouble(e.BytesReceived) / Convert.ToDouble(fileSize) * 100);
        }
        void request_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            labelControl2.Text = "완료되었습니다!";
        }

        private void LoadForm(object sender)
        {
            try
            {
                //fileupgrade 압축파일 확인
                _arrayCount = 0;
                _arrayIndex = 0;   

				bar_condition = "2";
                bool isExist = ExistUpgradeZipFile(); //실행파일경로에 zip 파일있는지 확인..
                if (isExist)
                {
                    BackgroundWorker worker = sender as BackgroundWorker;
                    UpgradeZipFile(worker);
                }
				
				bar_condition = "3";
                isExist = ExistUpgradeZipFile2("1"); //실행파일경로에 zip 파일있는지 확인..
                if (isExist)
                {
                    bool txt_Exist = ExistUpgradeZipFile2("2"); //version.txt 파일 있는지 확인

                    string strFullPath = Application.StartupPath;
#if DEBUG
                    strFullPath = Debug_StartPath;
#endif
                    string versiontxt_Path = strFullPath + "\\" + VersiontxtName2;
                    if (txt_Exist) //버젼txt 파일 있으면
                    {	
						bool dev16chk = true;
						var info2 = new FileInfo(strFullPath + "\\" + ZipFileName2);
						string a = "";
						string[] lines = File.ReadAllLines(versiontxt_Path);
						for (int i = 0; i < lines.Length; i++) //데이터가 존재하는 라인일 때에만, label에 출력한다.
						{
							a = lines[i].Substring(lines[i].IndexOf('=') + 1, lines[i].Length - lines[i].IndexOf('=') - 1);
							if (i == 0)
								dev16chk = info2.Length.ToString() == a ? false : true;
						}
						if (dev16chk)
						{
                            BackgroundWorker worker = sender as BackgroundWorker;
                            UpgradeZipFile2(worker);
						}
                    }
                    else
                    {
                        //메모장 파일 생성하기
                        StreamWriter wr = new StreamWriter(versiontxt_Path);
                        wr.Close();

                        BackgroundWorker worker = sender as BackgroundWorker;
                        UpgradeZipFile2(worker);
                    }
                }
				bar_condition = "9";
            }
            catch(Exception ie)
            {
                MessageBox.Show(ie.Message, "Fn: LoadForm Error");
            }
        }
        
        /// <summary>
        /// 파일이 실행된 폴더의 부모경로 
        /// </summary>
        /// <returns></returns>
        private string GetParentPath()
        {
            string strFullPath = Application.StartupPath;

            if (!Directory.Exists(strFullPath))
            {
                return string.Empty;
            }
#if DEBUG
            strFullPath = Debug_StartPath;
#endif
            int stringLoc = strFullPath.ToLower().LastIndexOf(FolderName);
            string strParentPath = strFullPath.Substring(0, stringLoc - 1);

            return strParentPath;
        }

        /// <summary>
        /// 실행파일의 부모경로에 zip파일 존재여부확인
        /// </summary>
        /// <returns></returns>
        private bool ExistUpgradeZipFile()
        {
            bool resVal = false;

            try
            {
                string strFullPath = Application.StartupPath;

                if (!Directory.Exists(strFullPath))                
                    return false;                
#if DEBUG
                strFullPath = Debug_StartPath;
#endif
                string upgradeZipFile = Path.Combine(strFullPath, ZipFileName); //부모경로말고...실행파일 경로에 zip파일있나..
                if (File.Exists(upgradeZipFile))                
                    resVal = true;                
            }
            catch
            {
                resVal = false;
            }
            return resVal;
        }
        private bool ExistUpgradeZipFile2(string gubn)
        {
            bool resVal = false;

            try
            {
                string strFullPath = Application.StartupPath;

                if (!Directory.Exists(strFullPath))                
                    return false;                
#if DEBUG
                strFullPath = Debug_StartPath;
#endif
                string name = gubn == "1" ? ZipFileName2 : VersiontxtName2;
                string upgradeZipFile = Path.Combine(strFullPath, name); //부모경로말고...실행파일 경로에 zip파일있나..
                if (File.Exists(upgradeZipFile))                
                    resVal = true;                
            }
            catch
            {
                resVal = false;
            }
            return resVal;
        }

        /// <summary>
        /// 부모경로에서 zip 파일찾고. 부모경로에 temp 폴더에풀어서..
        /// </summary>
        /// <param name="worker"></param>
        private void UpgradeZipFile(BackgroundWorker worker)
        {
            string tempTargetDir = "";
            try
            {
                string strFullPath = Application.StartupPath;
                if (!Directory.Exists(strFullPath))                
                    Directory.CreateDirectory(strFullPath);                
#if DEBUG
                strFullPath = Debug_StartPath;
#endif

                string upgradeZipFile = Path.Combine(Application.StartupPath, ZipFileName); // zip 파일 찾아서 
                //File.Copy(Application.StartupPath, strFullPath, true); //다르면 덮어쓴다..

                int eCount = 0;
             
                tempTargetDir = Path.Combine(strFullPath, "TEMP"); //부모경로에 temp 폴더 만들고

                Directory.CreateDirectory(tempTargetDir);
                //일단 temp에 풀어놓고 비교..
                using (ZipFile zipfile = ZipFile.Read(upgradeZipFile))
                {
                    foreach (ZipEntry e in zipfile)
                    {
                        eCount++;
                    }
                    int eIndex = 0;
                    _arrayCount = eCount;

                    foreach (ZipEntry e in zipfile)
                    {//버전으로 안풀고 작성시간으로 풀기
                        int percentageValue = 0;
                        string msgVal = string.Empty;
                        string fnm = e.FileName;
                        FileInfo fi = new FileInfo(fnm);
                        e.FileName = fi.Name;
                        e.Extract(tempTargetDir, ExtractExistingFileAction.OverwriteSilently); //temp 폴더에 파일 압축푼뒤.

                        string targetFileName = Path.Combine(strFullPath, fi.Name); //원본파일
                        string unzipFileName = Path.Combine(tempTargetDir, fi.Name); //temp 폴더의 업그레이드할 파일

                        if (fi.Extension.ToLower().Contains("dll") | fi.Extension.ToLower().Contains("exe") | fi.Extension.ToLower().Contains("chm"))
                        {
                            if (File.Exists(targetFileName))
                            {
                                FileInfo fi1 = new FileInfo(targetFileName);
                                FileInfo fi2 = new FileInfo(unzipFileName);
                                //파일의 마지막쓴시간 비교...
                                int versionDef = fi2.LastWriteTime.CompareTo(fi1.LastWriteTime);
                                if (versionDef == -1 | versionDef == 0)
                                {
                                    ++eIndex;
                                    _arrayIndex = eIndex;
                                    percentageValue = (int)((float)eIndex / (float)eCount * 100);
                                    msgVal = "Exist Same Version... " + eIndex + "/" + eCount;
                                    worker.ReportProgress(percentageValue, msgVal);
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            if (File.Exists(targetFileName))
                            {
                                FileInfo fs = new FileInfo(targetFileName);
                                FileInfo fd = new FileInfo(unzipFileName);

                                long sizeDef = fd.Length - fs.Length;
                                if (sizeDef <= 0)
                                {
                                    ++eIndex;
                                    _arrayIndex = eIndex;
                                    percentageValue = (int)((float)eIndex / (float)eCount * 100);
                                    msgVal = "Exist Same Version... " +  eIndex + "/" + eCount;
                                    worker.ReportProgress(percentageValue, msgVal);
                                    continue;
                                }
                            }
                        }

                        ++eIndex;
                        _arrayIndex = eIndex;
                        percentageValue = (int)((float)eIndex / (float)eCount * 100);
                        File.Copy(unzipFileName, targetFileName, true); //다르면 덮어쓴다..

                        msgVal = "Copy... " + eIndex + "/" + eCount;
                        worker.ReportProgress(percentageValue, msgVal);
                    }
                }
            }
            catch (Exception ie)
            {
                MessageBox.Show(ie.Message, "Fn: UpgradeZipFile Error");
            }
            finally
            {
                //if (Directory.Exists(tempTargetDir))                
                //    Directory.Delete(tempTargetDir, true);                
            }
        }
		
        private void UpgradeZipFile2(BackgroundWorker worker)
        {
            string tempTargetDir = "";
            try
            {
                string strFullPath = Application.StartupPath;
                if (!Directory.Exists(strFullPath))                
                    Directory.CreateDirectory(strFullPath);                
#if DEBUG
                strFullPath = Debug_StartPath;
#endif

                string upgradeZipFile = Path.Combine(Application.StartupPath, ZipFileName2); // zip 파일 찾아서 
                //File.Copy(Application.StartupPath, strFullPath, true); //다르면 덮어쓴다..

                int eCount = 0;
             
                tempTargetDir = Path.Combine(strFullPath, "DevTemp"); //부모경로에 temp 폴더 만들고
                Directory.CreateDirectory(tempTargetDir);

                //일단 temp에 풀어놓고 비교..
                using (ZipFile zipfile = ZipFile.Read(upgradeZipFile))
                {
                    foreach (ZipEntry e in zipfile)
                    {
                        eCount++;
                    }
                    int eIndex = 0;
                    _arrayCount = eCount;

                    foreach (ZipEntry e in zipfile)
                    {//버전으로 안풀고 작성시간으로 풀기
                        int percentageValue = 0;
                        string msgVal = string.Empty;
                        string fnm = e.FileName;
                        FileInfo fi = new FileInfo(fnm);
                        e.FileName = fi.Name;
                        e.Extract(tempTargetDir, ExtractExistingFileAction.OverwriteSilently); //temp 폴더에 파일 압축푼뒤.

                        string targetFileName = Path.Combine(strFullPath, fi.Name); //원본파일
                        string unzipFileName = Path.Combine(tempTargetDir, fi.Name); //temp 폴더의 업그레이드할 파일

                        if (fi.Extension.ToLower().Contains("dll") | fi.Extension.ToLower().Contains("exe") | fi.Extension.ToLower().Contains("chm"))
                        {
                            if (File.Exists(targetFileName))
                            {
                                FileInfo fi1 = new FileInfo(targetFileName);
                                FileInfo fi2 = new FileInfo(unzipFileName);
                                //파일의 마지막쓴시간 비교...
                                int versionDef = fi2.LastWriteTime.CompareTo(fi1.LastWriteTime);
                                if (versionDef == -1 | versionDef == 0)
                                {
                                    ++eIndex;
                                    _arrayIndex = eIndex;
                                    percentageValue = (int)((float)eIndex / (float)eCount * 100);
                                    msgVal = "Exist Same Version... " + eIndex + "/" + eCount;
                                    worker.ReportProgress(percentageValue, msgVal);
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            if (File.Exists(targetFileName))
                            {
                                FileInfo fs = new FileInfo(targetFileName);
                                FileInfo fd = new FileInfo(unzipFileName);

                                long sizeDef = fd.Length - fs.Length;
                                if (sizeDef <= 0)
                                {
                                    ++eIndex;
                                    _arrayIndex = eIndex;
                                    percentageValue = (int)((float)eIndex / (float)eCount * 100);
                                    msgVal = "Exist Same Version... " +  eIndex + "/" + eCount;
                                    worker.ReportProgress(percentageValue, msgVal);
                                    continue;
                                }
                            }
                        }

                        ++eIndex;
                        _arrayIndex = eIndex;
                        percentageValue = (int)((float)eIndex / (float)eCount * 100);
                        File.Copy(unzipFileName, targetFileName, true); //다르면 덮어쓴다..

                        msgVal = "Copy... " + eIndex + "/" + eCount;
                        worker.ReportProgress(percentageValue, msgVal);
                    }
                }
            }
            catch (Exception ie)
            {
                MessageBox.Show(ie.Message, "Fn: UpgradeZipFile Error");
            }
            finally
            {
                //if (Directory.Exists(tempTargetDir))                
                //    Directory.Delete(tempTargetDir, true);                
            }
        }
      
        #region BackGroudWorker
        private void CallBackGroundWorker()
        {
            progressBar1.Visible = true;
            this.bgWorker.RunWorkerAsync();
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            LoadForm(worker);                      
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //labelControl2.Text = bar_condition == "1" ? "Ftp서버에서 다운로드중입니다." : "압축을 해제중입니다.";
			
			if (bar_condition == "1")
				labelControl2.Text = "Ftp서버에서 다운로드중입니다.";
			else if (bar_condition == "2")
				labelControl2.Text = "SilkDt01.Zip 압축을 해제중입니다.";
			else if (bar_condition == "3")
				labelControl2.Text = "Dev16.Zip 압축을 해제중입니다.";
			else
				labelControl2.Text = "압축을 해제중입니다.";
            progressBar1.Value = e.ProgressPercentage;
          
            string line = (string)e.UserState + "\n";
            labelControl1.Text = line.Trim();
        }
        
        //업그레이드 완료후 ..진짜실행파일 실행
        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (bar_condition == "9")
            {
                string line = "완료되었습니다.\n";
                labelControl1.Text = line.Trim();

                string startExeFile = Path.Combine(Application.StartupPath, ExeFileName);
                string arguments = string.Empty;

                for (int i = 0; i < _arrArgs.Length; i++)
                {
                    arguments += i == 0 ? _arrArgs[i] : " " + _arrArgs[i];
                }

                string version_SartPath = Application.StartupPath;
    #if DEBUG
                version_SartPath = Debug_StartPath;
    #endif
                //현재 zip파일 크기 읽어서 version.txt에 쓰기				
                string file = version_SartPath + "\\" + ZipFileName2;
                var info = new FileInfo(file);
                System.IO.File.WriteAllText(version_SartPath + "\\" + VersiontxtName2, info.Length.ToString());

    #if DEBUG
                startExeFile = Debug_StartPath + "\\" + ExeFileName; //실제 실행되어야할 프로그램 실행파일
    #endif
                progressBar1.Visible = false;
                Process.Start(startExeFile, arguments);
                this.Dispose();
            }
            else
            {
                CallBackGroundWorker();
            }
        }

        #endregion

    }    
}

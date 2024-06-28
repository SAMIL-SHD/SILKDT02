using System;
using System.IO;
using System.Windows.Forms;

namespace FILEUPGRADE
{
    static class Program
    {
        
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            int thisID = System.Diagnostics.Process.GetCurrentProcess().Id; // 현재 기동한 프로그램 id 
            //실행중인 프로그램중 현재 기동한 프로그램과 같은 프로그램들 수집
            System.Diagnostics.Process[] p = System.Diagnostics.Process.GetProcessesByName("fileupgrade.exe");

            if (p.Length > 1)
            {
                for (int i = 0; i < p.Length; i++)
                {
                    if (p[i].Id == thisID) continue; //똑같은 넘이 있다면!
                    p[i].Kill(); // 프로세스 강제 종료
                }
            }
            else if (p.Length == 1)
            {
                MessageBox.Show("본 프로그램이 이미 실행중입니다.!", "프로그램 중복실행", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
#if DEBUG
            //args = new string[9];
            //args[0] = "1";
            //args[1] = "0001"; // 사업장번호
            //args[2] = "115.68.15.121";
            //args[3] = "";
            //args[4] = "sa";
            //args[5] = "samil25";
            //args[6] = "PM50";
            //args[7] = "08";
            //args[8] = "SAMIL";
#endif            
            ExistFile ef = new ExistFile();
            ef.PreOperation(); //압축풀어주는 Ionic.zip.dll파일 준비
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new FileUpgrade(args)); //[0] =TAXADB, [1] = 01  이런거 파라미터로 보냄.
        }
    }

    public class ExistFile
    {
        //개발자별 프로젝트별 설정하는 명칭***********************************

        /// <summary>
        /// 디버그모드일때 사용하는 startup path 
        /// </summary>
        string Debug_StartPath = @"D:\SILKROAD\PROGDT02";

        /// <summary> 부모폴더 찾기위한 실행파일의 폴더 *소문자여야함* </summary>
        string FolderName = "progdt02"; //소문자여야함

        //*******************************************************************

        /// <summary>
        /// 압축풀어주는 Ionic.zip.dll파일 사전에 있는지 확인.       
        /// </summary>
        /// <returns></returns>
        public bool PreOperation()
        {
            bool resVal = false;
            bool isExist = ExistZipLib();    
            if (isExist)            
                resVal = true;            
            else            
                resVal = false;
            
            return resVal;
        }

        /// <summary>
        /// 압축풀어주는 Ionic.zip.dll 파일존재여부 확인해서 없으면..밖에서 복사해서 만들고..
        /// </summary>
        /// <returns></returns>
        private bool ExistZipLib()
        {
            bool resVal = false;
            string startupPath = Application.StartupPath; //현재 실행파일의 실행경로

#if DEBUG //DEBUG모드에서는 경로를 고정..
            startupPath = Debug_StartPath;
#endif
            string zlibPath = Path.Combine(startupPath, "Ionic.Zip.dll"); //압축파일 풀어주는 dll 꼭필요

            if (File.Exists(zlibPath)) //ionic 있으면..true 반환
            {
                resVal = true;
            }
            else //없으면..복사해서 넣어준후.
            {
                MessageBox.Show("업그레이드를 받으십시오.(Ionic.Zip.dll파일없음)", "Zip Library 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;              
            }
            return resVal;
        }
    }
}

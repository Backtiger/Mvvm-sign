using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mvvmsign.Util
{
    public partial class HWP : Form
    {
        public HWP()
        {
            InitializeComponent();
        }

        public string ImgConvert(string filePath, string outputPath, int dpi)
        {
            string path = null;
            // 해당 경로에 파일이 존재하는지 확인
            if (System.IO.File.Exists(filePath) && filePath.Contains(".hwp"))
            {
                string HNCRoot = @"HKEY_Current_User\Software\HNC\HwpCtrl\Modules";

                string[] filename = filePath.Replace(".hwp","").Split('\\');

                try
                {
                    // 보안모듈 레지스트리에 등록되어 있는지 확인
                    if (Microsoft.Win32.Registry.GetValue(HNCRoot, "FilePathCheckerModuleExample", "Not Exist").Equals("Not Exist"))
                    {
                        // 등록되어 있지 않을경우 레지스트리에 등록
                        Microsoft.Win32.Registry.SetValue(HNCRoot, "FilePathCheckerModuleExample", Environment.CurrentDirectory + "\\FilePathCheckerModuleExample.dll");
                    }

                    // 불러옵니다.
                    axHwpCtrl1.RegisterModule("FilePathCheckDLL", "FilePathCheckerModuleExample");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                bool isHwpOpen = axHwpCtrl1.Open(filePath, "HWP", "forceopen:true;versionwarning:false");
                if (isHwpOpen == false)
                {
                    // 한글 문서 열기 실패
                    return path;
                }

                int totalPageCount = axHwpCtrl1.PageCount;

                for (int i = 0; i < totalPageCount; i++)
                {
                   // string imageFilePath = System.IO.Path.Combine(outputPath, String.Format("page{0:0000}.gif", i + 1));
                    string imageFilePath = System.IO.Path.Combine(outputPath, String.Format("{0}.gif",filename[filename.Length-1]));
                    path = imageFilePath;
                    // 이미지 생성
                    axHwpCtrl1.CreatePageImage(imageFilePath, i.ToString(), dpi.ToString(), "24", "gif");
                }
            }

            return path;
        }
    }
}

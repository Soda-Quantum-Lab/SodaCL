using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SodaCL.Toolkits.Logger;
using System.Security.Cryptography.X509Certificates;
using SodaCL.Launcher;

namespace SodaCL.Core
{
    internal class MCVersionList
    {
        public static string[] GetVersionList() 
        {
            // 留着，以后有用
            //DirectoryInfo mcVersionsFolder = new DirectoryInfo(SodaCL.Launcher.LauncherInfo.mcVersionsDir);
            //DirectoryInfo[] mcVersionsArray = mcVersionsFolder.GetDirectories();
            //int[] mcVersionsArrayLength = new int[] { 1, 2, 3 };
            //foreach (var item in mcVersionsArray)
            //{
            //    string versionFolderPath = item.ToString;
            //    string versionFolderName = Path.GetFileName(versionFolderPath);
            //    Log(ModuleList.IO, LogInfo.Info, item.ToString());
            //    Console.WriteLine(item.ToString());
            //}
            if (Directory.Exists(LauncherInfo.mcVersionsDir))
            {
                string[] dir = Directory.GetDirectories(LauncherInfo.mcVersionsDir);
                string[] names = new string[dir.Length];
                Log(ModuleList.IO, LogInfo.Info, "查找到 versions 文件夹内核心文件夹: ");
                for (int i = 0; i < dir.Length; i++)
                {
                    names[i] = Path.GetFileName(dir[i]);
                    Log(ModuleList.IO, LogInfo.Info, names[i]);
                }
                return names;
            }
            else
            {
                Log(ModuleList.IO, LogInfo.Error, "versions 目录不存在, 可能是初始化阶段出现了异常导致 versions 文件夹未成功创建");
                return null;
            }
        }
        
        
    }
}

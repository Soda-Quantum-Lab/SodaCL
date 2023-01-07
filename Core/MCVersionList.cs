using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SodaCL.Toolkits.Logger;

namespace SodaCL.Core
{
    internal class MCVersionList
    {
        public static void GetVersionList() 
        {
            DirectoryInfo mcVersionsFolder = new DirectoryInfo(SodaCL.Launcher.LauncherInfo.mcVersionsDir);
            DirectoryInfo[] mcVersionsArray = mcVersionsFolder.GetDirectories();
            int[] mcVersionsArrayLength = new int[] { 1, 2, 3 };
            foreach (var item in mcVersionsArray)
            {
                Log(ModuleList.IO, LogInfo.Info, item.ToString());
                Console.WriteLine(item.ToString());
            }

        }
        
        
    }
}

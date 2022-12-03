using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace SodaCL.Launcher
{
    public class LauncherInfo
    {
        static public string _SodaCLBasePath = @".\SodaCL";
        static public string _versionListSavePath = _SodaCLBasePath + @".\versions.json";
        static public string _launcherInfoSavePath = _SodaCLBasePath + @".\launcher.json";
        static public string _MCDir = @".\.minecraft";
        public int launchTime;
        string version;

        public LauncherInfo()
        {
            launchTime = 0;
            version = "0.0.1";
        }
        public void addLaunchTime()
        {
            launchTime += 1;
        }
    }

}

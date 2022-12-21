using System;

namespace SodaCL.Launcher

{
    public class LauncherInfo
    {
        static public string currentDir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        static public string SodaCLBasePath = currentDir + @"\SodaCL";
        static public string SodaCLLogPath = currentDir + @"\SodaCL\logs";
        static public string versionListSavePath = SodaCLBasePath + @"\versions.json";
        static public string launcherInfoSavePath = SodaCLBasePath + @"\launcher.json";
        static public string MCDir = currentDir + @"\.minecraft";
        static public int launchTime;
        string version;

        public LauncherInfo()
        {
            launchTime = 0;
            version = "0.0.1";
        }
        /// <summary>
        /// 增加启动次数 
        /// </summary>
        public void addLaunchTime()
        {
            launchTime += 1;
        }
    }

}

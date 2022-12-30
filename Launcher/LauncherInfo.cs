using System;

namespace SodaCL.Launcher
{
    public class LauncherInfo
    {
        public static string currentDir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        public static string sodaCLBasePath = currentDir + @"\SodaCL";
        public static string sodaCLLogPath = currentDir + @"\SodaCL\logs";
        public static string versionListSavePath = sodaCLBasePath + @"\versions.json";
        public static string launcherInfoSavePath = sodaCLBasePath + @"\launcher.json";
        public static string mcDir = currentDir + @"\.minecraft";
        public static int launchTime;
        private string version;

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
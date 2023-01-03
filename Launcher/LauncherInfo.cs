using System;

namespace SodaCL.Launcher
{
    public class LauncherInfo
    {
        public static string currentDir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        public static string sodaCLBasePath = currentDir + @"\SodaCL";
        public static string sodaCLLogPath = currentDir + @"\SodaCL\logs";
        public static string sodaCLConfigPath = sodaCLBasePath + @"\Config.ini";
        public static string mcDir = currentDir + @"\.minecraft";
        public static int launchTime;
        private string version;
    }
}
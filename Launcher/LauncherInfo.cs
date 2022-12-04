namespace SodaCL.Launcher
{
    public class LauncherInfo
    {
        static public string SodaCLBasePath = MainWindow.currentDir + @"\SodaCL";
        static public string versionListSavePath = SodaCLBasePath + @"\versions.json";
        static public string launcherInfoSavePath = SodaCLBasePath + @"\launcher.json";
        static public string MCDir = MainWindow.currentDir + @"\.minecraft";
        static public int launchTime;
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

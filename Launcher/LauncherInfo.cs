using System;

namespace SodaCL.Launcher
{
	public class LauncherInfo
	{
		public static string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SodaCL";
		public static string currentDir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
		public static int launchTime;
		public static string mcDir = currentDir + @".minecraft";
		public static string mcVersionsDir = mcDir + @"\versions";
		public static string sodaCLBasePath = currentDir + @"SodaCL";
		public static string sodaCLConfigPath = sodaCLBasePath + @"\Config.ini";
		public static string sodaCLLogPath = currentDir + @"SodaCL\logs";
	}
}
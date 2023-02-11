using System;
using System.IO;
using System.IO.Compression;
using SodaCL.Toolkits;

namespace SodaCL.Launcher
{
	public class LauncherInfo
	{
		public static string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SodaCL";
		public static string currentDir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
		public static string mcDir = currentDir + @".minecraft";
		public static string mcVersionsDir = mcDir + @"\versions";
		public static string sodaCLConfigFilePath = currentDir + @"SodaCL\Config.ini";
		public static string sodaCLForderPath = currentDir + @"SodaCL";

		//public static string sodaCLFontsForderPath = appDataDir + @"\Fonts";
		public static string sodaCLLogForderPath = currentDir + @"SodaCL\logs";

		public static string sodaCLTempForderPath = Path.GetTempPath() + @"SodaCL\\";
	}
}
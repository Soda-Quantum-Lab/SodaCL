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
		public static string SODA_MC_DIR = currentDir + @".minecraft";
		public static string SODA_MC_VERSIONS_DIR = SODA_MC_DIR + @"\versions";
		public static string SODACL_BASE_PATH = currentDir + @"SodaCL\Config.ini";
		public static string SODACL_FOLDER_PATH = currentDir + @"SodaCL";

		//public static string sodaCLFontsForderPath = appDataDir + @"\Fonts";
		public static string SODACL_LOG_FOLDER_PATH = currentDir + @"SodaCL\logs";

		public static string SODACL_TEMP_FOLDER_PATH = Path.GetTempPath() + "SodaCL";
	}
}
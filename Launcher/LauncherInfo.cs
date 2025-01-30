using System;
using System.IO;

namespace SodaCL.Launcher {

	public class LauncherInfo {
		public static string APP_DATA_DIR = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SodaCL";
		public static string CURRENT_DIR = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
		public static string SODA_MC_DIR = CURRENT_DIR + @".minecraft";
		public static string SODA_MC_VERSIONS_DIR = SODA_MC_DIR + @"\versions";
		public static string SODACL_FOLDER_PATH = CURRENT_DIR + @"SodaCL";

		//public static string sodaCLFontsForderPath = APP_DATA_DIR + @"\Fonts";
		public static string SODACL_LOG_FOLDER_PATH = CURRENT_DIR + @"SodaCL\logs";

		public static string SODACL_TEMP_FOLDER_PATH = Path.GetTempPath() + "SodaCL";

		public static string SODACL_SETTINGS = CURRENT_DIR + @"SodaCL\Config.xml";
	}
}
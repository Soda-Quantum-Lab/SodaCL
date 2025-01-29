using System.Runtime.InteropServices;
using System.Text;

namespace SodaCL.Toolkits {
	public static class IniFile {
		public static void DeleteKey(string Key, string Section = null) {
			Write(Key, null, Section ?? "SodaCL");
		}

		public static void DeleteSection(string Section = null) {
			Write(null, null, Section ?? "SodaCL");
		}

		public static bool KeyExists(string Key, string Section = null) {
			return Read(Key, Section).Length > 0;
		}

		public static string Read(string Key, string Section = null) {
			var RetVal = new StringBuilder(255);
			GetPrivateProfileString(Section ?? "SodaCL", Key, "", RetVal, 255, Launcher.LauncherInfo.SODACL_BASE_PATH);
			return RetVal.ToString();
		}

		public static void Write(string Key, string Value, string Section = null) {
			WritePrivateProfileString(Section ?? "SodaCL", Key, Value, Launcher.LauncherInfo.SODACL_BASE_PATH);
		}

		[DllImport("kernel32", CharSet = CharSet.Unicode)]
		private static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

		[DllImport("kernel32", CharSet = CharSet.Unicode)]
		private static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);
	}
}
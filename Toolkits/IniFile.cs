using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace SodaCL.Toolkits
{
    internal class IniFile
    {
        private static string EXE = "SodaCL";

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public static string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Launcher.LauncherInfo.sodaCLConfigPath);
            return RetVal.ToString();
        }

        public static void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, Launcher.LauncherInfo.sodaCLConfigPath);
        }

        public static void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }

        public static void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }

        public static bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }
    }
}
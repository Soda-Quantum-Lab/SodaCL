using System;
using System.IO;
using SodaCL.Launcher;
using static SodaCL.Toolkits.Logger;

namespace SodaCL.Toolkits
{
    public static class InitFolder
    {
        public static void InitBasicFolder()
        {
            try
            {
                if (!File.Exists(LauncherInfo.sodaCLBasePath))
                    Directory.CreateDirectory(LauncherInfo.sodaCLBasePath);
                if (!File.Exists(LauncherInfo.mcDir))
                    Directory.CreateDirectory(LauncherInfo.mcDir);
                if (!File.Exists(LauncherInfo.sodaCLLogPath))
                    Directory.CreateDirectory(LauncherInfo.sodaCLLogPath);
                if (!File.Exists(LauncherInfo.appDataDir))
                    Directory.CreateDirectory(LauncherInfo.appDataDir);
            }
            catch (Exception ex)
            {
                Log(true, ModuleList.Main, LogInfo.Error, ex: ex);
            }
        }
    }
}
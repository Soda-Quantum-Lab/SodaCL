using System;
using System.IO;
using static SodaCL.Toolkits.Logger;

namespace SodaCL.Launcher
{
    internal static class LauncherInit
    {
        /// <summary>
        /// 新建MC及启动器文件
        /// </summary>
        public static void InitNewFolder()
        {
            try
            {
                Directory.CreateDirectory(LauncherInfo.sodaCLBasePath);
                Directory.CreateDirectory(LauncherInfo.mcDir);
                Directory.CreateDirectory(LauncherInfo.sodaCLLogPath);
                Directory.CreateDirectory(LauncherInfo.mcVersionsDir);
                if (!File.Exists(LauncherInfo.sodaCLConfigPath))
                {
                    File.Create(LauncherInfo.sodaCLConfigPath);
                }
            }
            catch (Exception ex)
            {
                Log(ModuleList.IO, LogInfo.Error, ex.Message, ex.StackTrace);
            }
        }
    }
}
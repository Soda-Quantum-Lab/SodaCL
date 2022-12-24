using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Newtonsoft.Json;
using SodaCL.Core.Minecraft;
using static SodaCL.Launcher.LauncherLogging;


namespace SodaCL.Launcher
{
    static class LauncherInit
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                if (!File.Exists(LauncherInfo.versionListSavePath))
                {
                    FileStream fileStream = new(LauncherInfo.versionListSavePath, FileMode.Create, FileAccess.ReadWrite);
                    fileStream.Close();
                    Log(ModuleList.IO, LogInfo.Info, "新建版本文件");
                }
                else
                {
                    MainWindow.clients = JsonConvert.DeserializeObject<List<MCClient>>(File.ReadAllText(LauncherInfo.versionListSavePath));
                }

                if (!File.Exists(LauncherInfo.launcherInfoSavePath))
                {
                    FileStream fileStream = new(LauncherInfo.launcherInfoSavePath, FileMode.Create, FileAccess.ReadWrite);
                    fileStream.Close();
                    Log(ModuleList.IO, LogInfo.Info, "新建启动器文件");
                    MainWindow.launcherInfo = new LauncherInfo();
                }
                else
                {
                    MainWindow.launcherInfo = JsonConvert.DeserializeObject<LauncherInfo>(File.ReadAllText(LauncherInfo.launcherInfoSavePath));
                }
                MainWindow.launcherInfo.addLaunchTime(); // 启动器启动次数统计
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log(ModuleList.IO, LogInfo.Error, ex.Message, ex.StackTrace);
            }
        }
    }
}

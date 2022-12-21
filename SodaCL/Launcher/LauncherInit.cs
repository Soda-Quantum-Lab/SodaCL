using Newtonsoft.Json;
using SodaCL.Core.Minecraft;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SodaCL.Launcher.LauncherLogging;
using static SodaCL.Launcher.LauncherInfo;
using static SodaCL.MainWindow;
using System.Windows;

namespace SodaCL.Launcher
{
    class LauncherInit
    {
        public LauncherInfo launcherInfo;

        public List<MCClient> clients = new();
        public void InitNewFolder()
        {
            try
            {
                Directory.CreateDirectory(LauncherInfo.SodaCLBasePath);
                Directory.CreateDirectory(LauncherInfo.MCDir);
                Directory.CreateDirectory(LauncherInfo.SodaCLLogPath);
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
                   clients = JsonConvert.DeserializeObject<List<MCClient>>(File.ReadAllText(LauncherInfo.versionListSavePath));
                }

            if (!File.Exists(LauncherInfo.launcherInfoSavePath))
                {
                    FileStream fileStream = new(LauncherInfo.launcherInfoSavePath, FileMode.Create, FileAccess.ReadWrite);
                    fileStream.Close();
                    Log(ModuleList.IO, LogInfo.Info, "新建启动器文件");
                    this.launcherInfo = new LauncherInfo();
                }
            else
                {
                    this.launcherInfo = JsonConvert.DeserializeObject<LauncherInfo>(File.ReadAllText(LauncherInfo.launcherInfoSavePath));
                }
                this.launcherInfo.addLaunchTime(); // 启动器启动次数统计
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log(ModuleList.IO, LogInfo.Error, ex.Message, ex.StackTrace);
            }
        }
    }

}

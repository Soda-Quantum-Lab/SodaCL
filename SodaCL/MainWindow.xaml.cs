using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Newtonsoft.Json;
using SodaCL.Core.Minecraft;
using SodaCL.Launcher;
using static SodaCL.Launcher.LauncherLogging;

namespace SodaCL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public string currentDir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        private List<MCClient> clients = new();
        LauncherInfo launcherInfo;
        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
        }
        #region 自定义标题栏
        // 退出按钮
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(LauncherInfo.versionListSavePath))
            {
                FileStream fileStream = new(LauncherInfo.versionListSavePath, FileMode.Create, FileAccess.ReadWrite);
                fileStream.Close();
            }
            File.WriteAllText(LauncherInfo.versionListSavePath, JsonConvert.SerializeObject(clients));

            if (!File.Exists(LauncherInfo.launcherInfoSavePath))
            {
                FileStream fileStream = new(LauncherInfo.launcherInfoSavePath, FileMode.Create, FileAccess.ReadWrite);
                fileStream.Close();
            }
            File.WriteAllText(LauncherInfo.launcherInfoSavePath, JsonConvert.SerializeObject(launcherInfo));
            this.Close();
        }
        private void MiniSizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        #endregion
        private void Window_Initialized(object sender, EventArgs e)
        {
            Log(ModuleList.Main, LogInfo.Info, "主窗体加载完毕");
            InitNewFolder();

        }
        /// <summary>
        /// 新建MC及启动器文件
        /// </summary>
        private void InitNewFolder()
        {
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
        private void Window_Closed(object sender, EventArgs e)
        {
            Log(ModuleList.Main, LogInfo.Info, "程序退出");
            Trace.WriteLine("-------- SodaCL 程序日志记录结束 --------\n");

        }
        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFram.Source = new Uri("\\Pages\\Settings\\Set_About.xaml", UriKind.Relative);
        }

        private void IssuesBtn_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer", "https://github.com/SodaCL-Launcher/SodaCL/issues");
        }
    }
}

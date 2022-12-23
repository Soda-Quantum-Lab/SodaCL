using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
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
        public static LauncherInfo launcherInfo;
        public static List<MCClient> clients = new();

        public MainWindow()
        {
            InitializeComponent();
        }
        #region 自定义标题栏

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        // 退出按钮
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
        //最小化按钮
        private void MiniSizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        #endregion
        #region 初次启动        

        private void Window_Initialized(object sender, EventArgs e)
        {
            Log(ModuleList.Main, LogInfo.Info, "主窗体加载完毕");
            LauncherInit.InitNewFolder();
        }

        #endregion
        #region 事件
        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFram.Source = new Uri("\\Pages\\Settings\\Set_About.xaml", UriKind.Relative);
            DoubleAnimation titleBarAni = new();
        }

        private void IssuesBtn_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer", "https://github.com/SodaCL-Launcher/SodaCL/issues");
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Log(ModuleList.Main, LogInfo.Info, "程序退出");
            Trace.WriteLine("-------- SodaCL 程序日志记录结束 --------\n");
        }
        #endregion
    }
}

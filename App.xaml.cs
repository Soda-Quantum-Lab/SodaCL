using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using SodaCL.Launcher;
using static SodaCL.Launcher.LauncherLogging;

namespace SodaCL
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                Trace.Listeners.Add(new TextWriterTraceListener(LauncherInfo._SodaCLLogPath + "\\log-" + LogTime() + ".txt"));
                Trace.AutoFlush = true;
                Trace.WriteLine(" -------- SodaCL 程序日志记录开始 --------");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            SplashScreen splashScreen = new SplashScreen("/Resources/Images/Dev.ico");
            splashScreen.Show(true);
            Log(moduleList.Main, logInfo.Info, "显示启动画面");
            Directory.CreateDirectory(LauncherInfo._SodaCLBasePath);
            Directory.CreateDirectory(LauncherInfo._MCDir);
            Directory.CreateDirectory(LauncherInfo._SodaCLLogPath);
            base.OnStartup(e);
            Log(moduleList.Main, logInfo.Info, "开始加载主窗体");
        }

    }
}

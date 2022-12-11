using System;
using System.Diagnostics;
using System.IO;
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
                Directory.CreateDirectory(LauncherInfo._SodaCLBasePath);
                Directory.CreateDirectory(LauncherInfo._MCDir);
                Directory.CreateDirectory(LauncherInfo._SodaCLLogPath);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            int _fileNum = GetFileNum();
            SortAsFileCreationTime(ref _logFiles);
            if (_fileNum == 5)
            {
                File.Delete(_logFiles[4].ToString());
            }
            if (_fileNum > 5)
            {
                for (int i; _fileNum >= 5; _fileNum--)
                    File.Delete(_logFiles[_fileNum - 1].ToString());
            }
            try
            {
                Trace.Listeners.Add(new TextWriterTraceListener($"{LauncherInfo._SodaCLLogPath}\\[{DateTime.Now.Month.ToString()}.{DateTime.Now.Day.ToString()}]SodaCL_Log.txt"));
                Trace.AutoFlush = true;
                Trace.WriteLine(" -------- SodaCL 程序日志记录开始 --------");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            SplashScreen splashScreen = new SplashScreen("/Resources/Images/Dev.ico");
            splashScreen.Show(true,true);
            Log(moduleList.Main, logInfo.Info, "显示启动画面");
            SodaCL.Launcher.Languages.MultiLanguages();
            Log(moduleList.Main, logInfo.Info, "加载语言文件");
            base.OnStartup(e);
            Log(moduleList.Main, logInfo.Info, "开始加载主窗体");
        }

    }
}

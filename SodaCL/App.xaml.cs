using System;
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
            for (int i = 0; i < e.Args.Length; i++)
            {
                if (e.Args[i] == "--langs")
                {
                    //留个接口先
                    HandyControl.Controls.MessageBox.Show("您正处于翻译人员模式");
                    //bool isTranslator = true;
                }
            }
            try
            {
                Directory.CreateDirectory(LauncherInfo.SodaCLBasePath);
                Directory.CreateDirectory(LauncherInfo.MCDir);
                Directory.CreateDirectory(LauncherInfo.SodaCLLogPath);
            }
            catch (Exception ex)
            {
                HandyControl.Controls.MessageBox.Show(ex.Message);
            }
            LogStart();

            SplashScreen splashScreen = new SplashScreen("/Resources/Images/Dev.ico");
            splashScreen.Show(true, true);
            Log(ModuleList.Main, LogInfo.Info, "显示启动画面");
            base.OnStartup(e);
            Log(ModuleList.Main, LogInfo.Info, "开始加载主窗体");
            Languages.MultiLanguages();
            Log(ModuleList.Main, LogInfo.Info, "加载语言文件");

        }

    }
}

using System;
using System.IO;
using System.Windows;
using SodaCL.Launcher;
using static SodaCL.Toolkits.Logger;

namespace SodaCL
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Toolkits.AppCenterManager.StartAppCenter();
            for (int i = 0; i < e.Args.Length; i++)
            {
                if (e.Args[i] == "--langs")
                {
                    //留个接口先
                    MessageBox.Show("您正处于翻译人员模式");
                    //bool isTranslator = true;
                }
            }
            try
            {
                Directory.CreateDirectory(LauncherInfo.sodaCLBasePath);
                Directory.CreateDirectory(LauncherInfo.mcDir);
                Directory.CreateDirectory(LauncherInfo.sodaCLLogPath);
            }
            catch (Exception ex)
            {
                Log(ModuleList.Main, ex, ex.Message, ex.StackTrace);
            }
            LogStart();
            SplashScreen splashScreen = new SplashScreen("/Resources/Images/Dev.ico");
            splashScreen.Show(true, true);
            Log(ModuleList.Main, LogInfo.Info, "显示启动画面");
            Languages.MultiLanguages();
            Log(ModuleList.Main, LogInfo.Info, "加载语言文件");
        }
    }
}
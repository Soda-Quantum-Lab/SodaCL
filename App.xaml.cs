using System;
using System.Windows;
using SodaCL.Launcher;
using SodaCL.Toolkits;
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
            for (int i = 0; i < e.Args.Length; i++)
            {
                if (e.Args[i] == "--langs")
                {
                    //留个接口先
                    MessageBox.Show("您正处于翻译人员模式");
                    //bool isTranslator = true;
                }
            }
            InitFolder.InitBasicFolder();
            LogStart();
            AppCenterManager.StartAppCenter();
            SplashScreen splashScreen = new SplashScreen("/Resources/Images/Dev.ico");
            splashScreen.Show(true, true);
            base.OnStartup(e);
            Log(false, ModuleList.Main, LogInfo.Info, "显示启动画面");
            Languages.MultiLanguages();
            Log(false, ModuleList.Main, LogInfo.Info, "加载语言文件");
        }
    }
}
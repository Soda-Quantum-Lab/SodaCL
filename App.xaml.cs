using System.Windows;
using SodaCL.Launcher;
using SodaCL.Toolkits;
using static SodaCL.Toolkits.Logger;

namespace SodaCL
{
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			foreach (var t in e.Args)
			{
				if (t == "--langs")
				{
					//留个接口先
					MessageBox.Show("您正处于翻译人员模式");
					//bool isTranslator = true;
				}
			}
			LauncherInit.InitNewFolder();
			LogStart();
			AppCenterManager.StartAppCenter();
#if RELEASE
			var splashScreen = new SplashScreen("/Resources/Images/Dev.ico");
			splashScreen.Show(true, true);
#endif
			base.OnStartup(e);
			Log(false, ModuleList.Main, LogInfo.Info, "显示启动画面");
			Languages.MultiLanguages();
			Log(false, ModuleList.Main, LogInfo.Info, "加载语言文件");
		}
	}
}
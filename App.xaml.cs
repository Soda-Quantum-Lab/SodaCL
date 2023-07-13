using SodaCL.Core.Java;
using SodaCL.Launcher;
using SodaCL.Toolkits;
using System;
using System.Threading.Tasks;
using System.Windows;
using static SodaCL.Toolkits.Logger;

namespace SodaCL
{
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			StartExceptionCatcher();
			foreach (var t in e.Args)
			{
				if (t == "--Langs")
				{
					//留个接口先
					MessageBox.Show("您正处于翻译人员模式");
					//bool isTranslator = true;
				}
			}
			LauncherInit.Setup();
			LauncherInit.DeleteTempFolder();
			Task.Run(() => { JavaFindingAndSelecting.AutoJavaFinding(); });
			//LauncherInit.InitMiSansFonts();
			LogStart();
#if RELEASE
			AppCenterManager.StartAppCenter();
			var splashScreen = new SplashScreen("/Resources/Images/Dev.ico");
			splashScreen.Show(true, true);
#endif
			base.OnStartup(e);
			Log(false, ModuleList.Main, LogInfo.Info, "显示启动画面");
			Languages.MultiLanguages();
			Log(false, ModuleList.Main, LogInfo.Info, "加载语言文件");
		}

		private void StartExceptionCatcher()
		{
			//Task线程内未捕获异常处理事件
			TaskScheduler.UnobservedTaskException += (sender, e) =>
			{
				var ex = e.Exception;
				Log(true, ModuleList.Unknown, LogInfo.Unhandled,
					GetResources.GetText("ExceptionCatcher_Dialog_CatchedMessage_Start") +
					$"{ex.Message}\r\n{ex.StackTrace}" +
					GetResources.GetText("ExceptionCatcher_Dialog_CatchedMessage_End"), ex);
			};

			//UI线程未捕获异常处理事件（UI主线程）
			DispatcherUnhandledException += (sender, e) =>
			{
				var ex = e.Exception;
				Log(true, ModuleList.Unknown, LogInfo.Unhandled,
					GetResources.GetText("ExceptionCatcher_Dialog_CatchedMessage_Start") +
					$"{ex.Message}\r\n{ex.StackTrace}" +
					GetResources.GetText("ExceptionCatcher_Dialog_CatchedMessage_End"), ex); ;
			};

			//非UI线程未捕获异常处理事件(例如自己创建的一个子线程)
			AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
			{
				var ex = e.ExceptionObject as Exception;
				Log(true, ModuleList.Unknown, LogInfo.Unhandled,
					GetResources.GetText("ExceptionCatcher_Dialog_CatchedMessage_Start") +
					$"{ex.Message}\r\n{ex.StackTrace}" +
					GetResources.GetText("ExceptionCatcher_Dialog_CatchedMessage_End"), ex);
			};
		}
	}
}
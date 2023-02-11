using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SodaCL.Controls;
using SodaCL.Controls.Dialogs;
using SodaCL.Core.Auth;
using SodaCL.Core.Game;
using SodaCL.Core.Java;
using SodaCL.Launcher;
using SodaCL.Toolkits;
using static SodaCL.Toolkits.GetResources;
using static SodaCL.Toolkits.Logger;

namespace SodaCL.Pages
{
	/// <summary>
	/// MainPage.xaml 的交互逻辑
	/// </summary>

	public partial class MainPage : Page
	{
		public static MainPage mainPage;

		//登录Task取消Token
		public CancellationTokenSource loginTsCancelSrc;

		private string yiYanText;

		#region 初始化

		public MainPage()
		{
			InitializeComponent();
			mainPage = this;
		}

		private async void Page_Loaded(object sender, RoutedEventArgs e)
		{
			SayHello();
			TextAni();
			await GetYiyanAsync();
			var da = new DoubleAnimation(1, TimeSpan.FromSeconds(0.5));
			YiYanTxb.BeginAnimation(OpacityProperty, da);
		}

		#endregion 初始化

		#region 动画

		public void TextAni()
		{
			var storyboard = new Storyboard();
			var doubleAnimation = new DoubleAnimation(0.0, 1.0, TimeSpan.FromSeconds(0.8));
			Storyboard.SetTarget(doubleAnimation, SayHelloUsernameTxb);
			Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));
			storyboard.Children.Add(doubleAnimation);
			var doubleAnimation2 = new DoubleAnimation(0.0, 1.0, TimeSpan.FromSeconds(0.8));
			Storyboard.SetTarget(doubleAnimation2, SayHelloTimeTxb);
			Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath("Opacity"));
			storyboard.Children.Add(doubleAnimation2);
			var thicknessAnimation = new ThicknessAnimation(new Thickness(0.0, 0.0, 0.0, 0.0), TimeSpan.FromSeconds(0.4))
			{
				BeginTime = TimeSpan.FromSeconds(0.2),
				EasingFunction = new CubicEase
				{
					EasingMode = EasingMode.EaseInOut
				}
			};
			Storyboard.SetTarget(thicknessAnimation, LaunchBar);
			Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath("Margin"));
			storyboard.Children.Add(thicknessAnimation);
			storyboard.Begin();
		}

		#endregion 动画

		#region 事件

		private void DownloadButtonClick(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("笨蛋 xiaohu 还在搓天杀的控件，你先别急。");
			//try
			//{
			//	if (!File.Exists(LauncherInfo.sodaCLForderPath + "\\BakaXL.exe"))
			//	{
			//		var down = new
			//		FileDownloader("http://jk-insider.bakaxl.com:8888/job/BakaXL%20Insider%20Parrot/lastSuccessfulBuild/artifact/BakaXL_Public/bin/Jenkins%20Release/BakaXL_Secure/BakaXL.exe",
			//		LauncherInfo.sodaCLForderPath + "\\BakaXL.exe");
			//		down.DownloaderProgressFinished += (sender, e) =>
			//		{
			//			Log(true, ModuleList.IO, LogInfo.Info, "成功启动 BakaXL ！");
			//		};
			//		await down.Start();
			//		MessageBox.Show("正在下载 BakaXL");
			//	}
			//	else
			//	{
			//		Process.Start(LauncherInfo.sodaCLForderPath + "\\BakaXL.exe");
			//	}
			//}
			//catch (Exception ex)
			//{
			//	Log(true, ModuleList.Main, LogInfo.Warning, "BakaXL 未能正常启动，可能是下载的文件不完整", ex);
			//	File.Delete(LauncherInfo.sodaCLForderPath + "\\BakaXL.exe");
			//}
		}

		private void EnvironmentCheckButtonClick(object sender, RoutedEventArgs e)
		{
			var ed = new SodaLauncherErrorDialog("Test");
			MainWindow.mainWindow.Grid_DialogArea.Children.Add(ed);
			MinecraftVersion.GetVersionList();
			Log(false, ModuleList.IO, LogInfo.Info, "--------------------------------");
			Task.Run(() => { JavaFinding.AutoJavaFinding(true); });
		}

		private void LogFolderOpenerButtonClick(object sender, RoutedEventArgs e)
		{
			Process.Start("explorer.exe", ".\\SodaCL\\logs");
		}

		private void StartBtn_Click(object sender, RoutedEventArgs e)
		{
			var dE = new SodaLauncherErrorDialog("笨蛋 xiaohu 还在搓天杀的控件，你先别急。");
			MainWindow.mainWindow.Grid_DialogArea.Children.Add(dE);
			MinecraftLaunch.LaunchGame();
		}

		#endregion 事件

		#region 一言及问好处理

		private async Task GetYiyanAsync()
		{
			string text;
			try
			{
				if (yiYanText == null)
				{
					Log(false, ModuleList.Network, LogInfo.Info, "正在获取一言");
					do
					{
						using var client = new HttpClient();
						var yiYanApiAdd = "https://v1.hitokoto.cn/?c=c&c=a&encode=json&charset=utf-8&maxlength=10";
						client.Timeout = TimeSpan.FromSeconds(5);
						var jsonResponse = await client.GetStringAsync(yiYanApiAdd);
						var jObj = JsonConvert.DeserializeObject<JObject>(jsonResponse);
						var yiYan = (string)jObj["hitokoto"];
						string space;
						string endSpace;
						if (yiYan.EndsWith("。") || yiYan.EndsWith("？") || yiYan.EndsWith("！"))
						{
							space = "  ";
							endSpace = "";
						}
						else
						{
							space = "  ";
							endSpace = "  ";
						}
						YiYanTxb.Margin = new Thickness(10, 0, 0, 0);
						text = $"「{space + yiYan + endSpace}」 ——  {(string)jObj["from"]}";
					}
					while (text.Length > 35);
					YiYanTxb.Text = text;
					yiYanText = YiYanTxb.Text;
					Log(false, ModuleList.Network, LogInfo.Info, "一言获取成功");
				}
				else
				{
					YiYanTxb.Text = yiYanText;
				}
			}
			catch (Exception ex)
			{
				YiYanTxb.Text = "一言获取失败";
				Log(false, ModuleList.Network, LogInfo.Warning, "一言获取失败", ex);
			}
		}

		private void SayHello()
		{
			try
			{
				SayHelloUsernameTxb.Text = Environment.UserName;

				var hour = DateTime.Now.Hour;
				switch (hour)
				{
					case int n when (n >= 3 && n < 5):
						SayHelloTimeTxb.Text = "凌晨好!";
						break;

					case int n when (n >= 5 && n < 11):
						SayHelloTimeTxb.Text = "上午好!";
						break;

					case int n when (n >= 11 && n < 13):
						SayHelloTimeTxb.Text = "中午好!";
						break;

					case int n when (n >= 13 && n < 17):
						SayHelloTimeTxb.Text = "下午好!";
						break;

					case int n when (n >= 17 && n < 19):
						SayHelloTimeTxb.Text = "傍晚好!";
						break;

					case int n when ((n <= 23 && n >= 19) || n > 23):
						SayHelloTimeTxb.Text = "晚上好!";
						break;

					case int n when (n >= 0 && n < 3):
						SayHelloTimeTxb.Text = "午夜好!";
						break;
				}
			}
			catch (Exception ex)
			{
				Log(true, ModuleList.IO, LogInfo.Error, ex: ex);
			}
		}

		/// <summary>
		/// 向Api接口获取一言并做出处理
		/// </summary>

		#endregion 一言及问好处理
	}
}
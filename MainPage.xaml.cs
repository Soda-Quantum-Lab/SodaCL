using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SodaCL.Core.Auth;
using SodaCL.Core.Auth.Model;
using SodaCL.Core.Game;
using SodaCL.Core.Java;
using static SodaCL.Toolkits.Dialog;
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
		private string yiYanText;

		#region 初始化

		public MainPage()
		{
			InitializeComponent();
			mainPage = this;
		}

		/// <summary>
		/// 向Api接口获取一言并做出处理
		/// </summary>
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

		private async void Page_Initialized(object sender, EventArgs e)
		{
			SayHello();
			TextAni();
			await GetYiyanAsync();
			var da = new DoubleAnimation(1, TimeSpan.FromSeconds(0.5));
			YiYanTxb.BeginAnimation(OpacityProperty, da);
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

		private void TextAni()
		{
			var storyboard = new Storyboard();
			var doubleAnimation = new DoubleAnimation(0.0, 1.0, TimeSpan.FromSeconds(0.5));
			Storyboard.SetTarget(doubleAnimation, SayHelloUsernameTxb);
			Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));
			storyboard.Children.Add(doubleAnimation);
			var doubleAnimation2 = new DoubleAnimation(0.0, 1.0, TimeSpan.FromSeconds(0.5));
			Storyboard.SetTarget(doubleAnimation2, SayHelloTimeTxb);
			Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath("Opacity"));
			storyboard.Children.Add(doubleAnimation2);
			//DoubleAnimation doubleAnimation3 = new DoubleAnimation(0.0, 1.0, TimeSpan.FromSeconds(0.5))
			//{
			//    BeginTime = TimeSpan.FromSeconds(0.2)
			//};
			//Storyboard.SetTarget(doubleAnimation3, YiYanTxb);
			//Storyboard.SetTargetProperty(doubleAnimation3, new PropertyPath("Opacity"));
			//storyboard.Children.Add(doubleAnimation3);
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

		#endregion 初始化

		#region 事件

		//登录Task取消Token
		public CancellationTokenSource loginTsCancelSrc;

		private void DownloadButtonClick(object sender, RoutedEventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start(".\\SodaCL\\BakaXL.exe");
				Log(true, ModuleList.Main, LogInfo.Info, "BakaXL 已启动");
			}
			catch (Exception ex)
			{
				Log(true, ModuleList.Main, LogInfo.Warning, "BakaXL 未能正常启动，可能是下载的文件不完整", ex);
			}
		}

		private void EnvironmentCheckButtonClick(object sender, RoutedEventArgs e)
		{
			MinecraftVersionList.GetVersionList();
			Log(false, ModuleList.IO, LogInfo.Info, "--------------------------------");
			JavaFinding.AutoJavaFinding(true);
		}

		private void LogFolderOpenerButtonClick(object sender, RoutedEventArgs e)
		{
			Process.Start("explorer.exe", ".\\SodaCL\\logs");
		}

		private async void MSOAuth_OpenWindows(object sender, (MicrosoftAuth.WindowsTypes, string) e)
		{
			if (e.Item1.Equals(MicrosoftAuth.WindowsTypes.OpenInBrowser))
			{
				Dispatcher.Invoke(new Action(() => { ChangeDialog(); }));

				await OpenOpenInBrowserWindow(e.Item2);
			}
			switch (e)
			{
				case (MicrosoftAuth.WindowsTypes.StartLogin, null):
					{
						await Dispatcher.InvokeAsync(() =>
						{
							OpenDialog();
							MainWindow.mainWindow.DialogStackPan.Children.Add(new TextBlock() { Text = "正在初始化微软登录服务", FontSize = 18, TextAlignment = TextAlignment.Center });
							MainWindow.mainWindow.DialogStackPan.Children.Add(new ProgressBar() { IsIndeterminate = true, Height = 10, Width = 300, Margin = new Thickness(0, 30, 0, 0) });
						});
						break;
					}
			}
		}

		private async void StartBtn_Click(object sender, RoutedEventArgs e)
		{
			MicrosoftAuth msOAuth = new();
			msOAuth.OpenWindows += MSOAuth_OpenWindows;
			loginTsCancelSrc = new CancellationTokenSource();

			MicrosoftAccount msAccount;
			try
			{
				msAccount = await Task.Run<MicrosoftAccount>(async () =>
			{
				return await msOAuth.StartAuthAsync(GetText("OAuth2Token"));
			}, loginTsCancelSrc.Token);
			}
			catch (MicrosoftAuthException ex)
			{
				string errorMsg;
				switch (ex.ErrorType)
				{
					case MicrosoftAuth.MsAuthErrorType.AuthDeclined:
						errorMsg = GetText("Login_Microsoft_Error_AuthDeclined");
						Log(true, ModuleList.Login, LogInfo.Warning, "最终用户拒绝了授权请求", ex);
						break;

					case MicrosoftAuth.MsAuthErrorType.ExpiredToken:
						errorMsg = GetText("Login_Microsoft_Error_ExpiredToken");
						Log(true, ModuleList.Login, LogInfo.Warning, "登录超时", ex);
						break;

					case MicrosoftAuth.MsAuthErrorType.NoXboxAccount:
						errorMsg = GetText("Login_Microsoft_Error_NoXboxAccount");
						Log(true, ModuleList.Login, LogInfo.Warning, "用户未创建Xbox账户", ex);
						break;

					case MicrosoftAuth.MsAuthErrorType.XboxDisable:
						errorMsg = GetText("Login_Microsoft_Error_XboxDisable");
						Log(true, ModuleList.Login, LogInfo.Warning, " Xbox Live 不可用/禁止的国家/地区", ex);
						break;

					case MicrosoftAuth.MsAuthErrorType.NeedAdultAuth:
						errorMsg = GetText("Login_Microsoft_Error_NeedAdultAuth");
						Log(true, ModuleList.Login, LogInfo.Warning, "需要在 Xbox 页面上进行成人验证", ex);
						break;

					case MicrosoftAuth.MsAuthErrorType.NeedJoiningInFamily:
						errorMsg = GetText("Login_Microsoft_Error_NeedJoiningInFamily");
						Log(true, ModuleList.Login, LogInfo.Warning, "需要在 Xbox 页面上进行成人验证", ex);
						break;

					case MicrosoftAuth.MsAuthErrorType.NoGame:
						errorMsg = GetText("Login_Microsoft_Error_NoGame");
						Log(true, ModuleList.Login, LogInfo.Warning, "该帐户是儿童账户", ex);
						break;
				};
				OpenDialog();
			}
			catch (OperationCanceledException)
			{
				Log(false, ModuleList.Login, LogInfo.Warning, "登录操作已取消");
			}
			catch (Exception ex)
			{
				Dispatcher.Invoke(new Action(() => { CloseDialog(); }));
				Log(true, ModuleList.Network, LogInfo.Error, ex: ex);
			}
			finally
			{
				loginTsCancelSrc.Dispose();
			}
		}

		private void StartTestButtonClick(object sender, RoutedEventArgs e)
		{
			try
			{
				MinecraftLaunch.McLaunching("1.12.2", "4096M", "SodaCL_Test");
				Log(false, ModuleList.Main, LogInfo.Info, "启动游戏成功");
			}
			catch (Exception ex)
			{
				Log(true, ModuleList.Main, LogInfo.Info, "SodaCL 在启动游戏时发生了错误: \n" + ex);
			}
		}

		#endregion 事件

		/// <summary>
		/// 打开登录说明界面
		/// </summary>
		/// <param name="deviceCode">显示的登录代码</param>
		private async Task OpenOpenInBrowserWindow(string deviceCode)
		{
			await Dispatcher.InvokeAsync(() =>
			{
				var StackPan = new StackPanel { Margin = new Thickness(10, 20, 10, 0), Orientation = Orientation.Horizontal };
				var iconBor = new Border
				{
					Height = 32,
					Width = 32,
					Margin = new Thickness(5, 0, 0, 0),
					Background = GetBrush("Color1"),
					CornerRadius = new CornerRadius(16),
					Child = new System.Windows.Controls.Image
					{
						Width = 20,
						Height = 20,
						Source = GetSvg("Svg_Information"),
					}
				};
				var exitButton = new Button
				{
					Margin = new Thickness(120, 0, 0, 0),
					Height = 32,
					Width = 32,
					Style = GetStyle("Btn_NoBackground"),
					Content = new System.Windows.Controls.Image
					{
						Width = 20,
						Height = 20,
						Source = GetSvg("Svg_Close")
					}
				};
				var okButton = new Button
				{
					Margin = new Thickness(270, 0, 0, 0),
					Content = GetText("Butten_OK"),
					Style = GetStyle("Btn_Main")
				};
				exitButton.Click += (s, e) =>
				{
					loginTsCancelSrc.Cancel();
					Dispatcher.Invoke(new Action(() => { CloseDialog(); }));
					Log(false, ModuleList.Login, LogInfo.Warning, "用户取消了登录操作");
				};
				okButton.Click += (s, be) =>
					{
						Clipboard.SetText(deviceCode);
						Log(false, ModuleList.IO, LogInfo.Info, "成功将DeviceCode复制到剪切板");
						Process.Start("explorer", "https://www.microsoft.com/link");
						Log(false, ModuleList.IO, LogInfo.Info, "成功打开浏览器");
					};
				StackPan.Children.Add(iconBor);
				StackPan.Children.Add(new TextBlock
				{
					Height = 28,
					Margin = new Thickness(10, 0, 0, 0),
					Padding = new Thickness(0, 3, 0, 0),
					Style = GetStyle("Text_Bold"),
					Text = GetText("Login_Microsoft_MessageBox_OpenInBrowser_Title")
				});
				StackPan.Children.Add(exitButton);
				MainWindow.mainWindow.DialogStackPan.Children.Add(StackPan);
				MainWindow.mainWindow.DialogStackPan.Children.Add(new TextBlock
				{
					Margin = new Thickness(57, 10, 20, 0),
					Text = GetText("Login_Microsoft_MessageBox_OpenInBrowser_Text_Tip")
				});
				MainWindow.mainWindow.DialogStackPan.Children.Add(new TextBlock
				{
					Margin = new Thickness(56, 10, 20, 0),
					Style = GetStyle("Text_Bold"),
					Text = GetText("Login_Microsoft_MessageBox_OpenInBrowser_Text_YourLoginCode")
				});
				MainWindow.mainWindow.DialogStackPan.Children.Add(new TextBlock
				{
					Margin = new Thickness(55, 5, 20, 0),
					Text = deviceCode,
					FontSize = 24,
				});
				MainWindow.mainWindow.DialogStackPan.Children.Add(okButton);
			});
		}
	}
}
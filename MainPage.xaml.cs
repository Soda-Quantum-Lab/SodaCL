using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SodaCL.Core.Game;
using SodaCL.Core.Java;
using SodaCL.Toolkits;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using static SodaCL.Toolkits.HttpHelper;
using static SodaCL.Toolkits.Logger;

namespace SodaCL.Pages {
	/// <summary>
	/// MainPage.xaml 的交互逻辑
	/// </summary>

	public partial class MainPage : Page {
		public static MainPage mainPage;

		//登录 Task 取消 Token
		public CancellationTokenSource loginTsCancelSrc;

		private string yiYanText;

		#region 初始化

		public MainPage() {
			InitializeComponent();
			mainPage = this;
		}

		private async void Page_Loaded(object sender, RoutedEventArgs e) {
			SayHello();
			TextAni();
			await GetYiyanAsync();
			var da = new DoubleAnimation(1, TimeSpan.FromSeconds(0.5));
			YiYanTxb.BeginAnimation(OpacityProperty, da);
		}

		#endregion 初始化

		#region 动画

		public void TextAni() {
			var storyboard = new Storyboard();
			var doubleAnimation = new DoubleAnimation(0.0, 1.0, TimeSpan.FromSeconds(0.8));
			Storyboard.SetTarget(doubleAnimation, SayHelloUsernameTxb);
			Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));
			storyboard.Children.Add(doubleAnimation);
			var doubleAnimation2 = new DoubleAnimation(0.0, 1.0, TimeSpan.FromSeconds(0.8));
			Storyboard.SetTarget(doubleAnimation2, SayHelloTimeTxb);
			Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath("Opacity"));
			storyboard.Children.Add(doubleAnimation2);
			var thicknessAnimation = new ThicknessAnimation(new Thickness(0.0, 0.0, 0.0, 0.0), TimeSpan.FromSeconds(0.4)) {
				BeginTime = TimeSpan.FromSeconds(0.2),
				EasingFunction = new CubicEase {
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

		private void EnvironmentCheckButtonClick(object sender, RoutedEventArgs e) {
			MinecraftVersion.GetVersionList();
			Log(false, ModuleList.IO, LogInfo.Info, "--------------------------------");
			Task.Run(() => { JavaFindingAndSelecting.AutoJavaFinding(); });
		}

		private void LogFolderOpenerButtonClick(object sender, RoutedEventArgs e) {
			Process.Start("explorer.exe", ".\\SodaCL\\logs");
		}

		private void StartBtn_Click(object sender, RoutedEventArgs e) {
			//MinecraftLaunch.LaunchGame();
			//var dE = new SodaLauncherErrorDialog("笨蛋 xiaohu 还在搓天杀的控件，你先别急。人活着哪有不疯的？硬撑罢了！\r\n\r\n人活着哪有不疯的？硬撑罢了！\r\n\r\n人活着哪有不疯的？硬撑罢了！\r\n\r\n人活着哪有不疯的？硬撑罢了！\r\n\r\n妈的，忍不了，一拳把地球打爆！\r\n\r\n妈的，忍不了，一拳把地球打爆！\r\n\r\n妈的，忍不了，一拳把地球打爆！\r\n妈的，忍不了，一拳把地球打爆！\r\n妈的，忍不了，一拳把地球打爆！\r\n妈的，忍不了，一拳把地球打爆！\r\n妈的，忍不了，一拳把地球打爆！\r\n妈的，忍不了，一拳把地球打爆！\r\n妈的，忍不了，一拳把地球打爆！\r\n妈的，忍不了，一拳把地球打爆！\r\n妈的，忍不了，一拳把地球打爆！\r\n妈的，忍不了，一拳把地球打爆！\r\n妈的，忍不了，一拳把地球打爆！\r\n妈的，忍不了，一拳把地球打爆！\r\n妈的，忍不了，一拳把地球打爆！\r\n妈的，忍不了，一拳把地球打爆！\r\n妈的，忍不了，一拳把地球打爆！\r\n妈的，忍不了，一拳把地球打爆！\r\n他奶奶的鸡蛋六舅的哈密瓜妹妹的大窝瓜爷爷的大鸡腿婶婶的大葡萄妈妈的黄瓜菜爸爸的大面包三舅姥爷的大李子二婶的桃子三叔的西瓜七舅姥爷的小荔枝二舅姥爷的火龙果姑姑的猕猴桃祖爷爷的车厘子祖姥爷的大菠萝祖奶奶的大榴莲二爷的小草莓他三婶姥姥的大白菜他哥哥的大面条妹妹的小油菜弟弟的西葫芦姐姐的大土豆姐夫的大青椒爷爷的大茄子嗯啊，杀杀杀！\r\n！\r\n好可怕杀杀杀杀杀杀上勾拳！\r\n下勾拳！\r\n左勾拳！\r\n右勾拳！\r\n扫堂腿！\r\n回旋踢！\r\n这是蜘蛛吃耳屎，这是龙卷风摧毁停车场！\r\n这是羚羊蹬，这是山羊跳！\r\n乌鸦坐飞机！\r\n老鼠走迷宫！\r\n大象踢腿！\r\n愤怒的章鱼！\r\n巨斧砍大树！\r\n彻底疯狂！\r\n彻底疯狂！\r\n彻底疯狂！\r\n彻底疯狂！\r\n彻底疯狂！\r\n彻底疯狂！\r\n彻底疯狂！\r\n彻底疯狂！\r\n彻底疯狂！\r\n杀！\r\n杀！\r\n杀！\r\n杀！\r\n杀！\r\n杀！\r\n杀！\r\n杀！\r\n杀！\r\n杀！\r\n杀！\r\n杀！\r\n杀！\r\n杀！\r\n杀！\r\n杀！\r\n杀！\r\n杀！\r\n杀！\r\n杀！\r\n杀！\r\n杀！\r\n彻底疯狂！\r\n彻底疯狂！\r\n彻底疯狂！\r\n彻底疯狂！\r\n彻底疯狂！\r\n彻底疯狂！\r\n彻底疯狂！\r\n彻底疯狂！\r\n彻底疯狂！\r\n彻底疯狂！\r\n");

			//var sl = JsonConvert.DeserializeObject<AssetModel>(await GetStringResponseAsync("https://bmclapi2.bangbang93.com/mc/game/version_manifest_v2.json"));
			//MessageBox.Show(sl.ToString());

			RegEditor.SetKeyValue(Registry.CurrentUser, "LoginType", "0", RegistryValueKind.String);
		}

		#endregion 事件

		#region 一言及问好处理

		private async Task GetYiyanAsync() {
			string text;
			try {
				if (yiYanText == null) {
					Log(false, ModuleList.Network, LogInfo.Info, "正在获取一言");
					do {
						var jsonResponse = await GetStringResponseAsync("https://v1.hitokoto.cn/?c=c&c=a&encode=json&charset=utf-8&maxlength=10");
						var jObj = JsonConvert.DeserializeObject<JObject>(jsonResponse);
						var yiYan = (string)jObj["hitokoto"];
						string space;
						string endSpace;
						if (yiYan.EndsWith("。") || yiYan.EndsWith("？") || yiYan.EndsWith("！\r\n")) {
							space = "  ";
							endSpace = "";
						}
						else {
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
				else {
					YiYanTxb.Text = yiYanText;
				}
			}
			catch (Exception ex) {
				YiYanTxb.Text = "一言获取失败";
				Log(false, ModuleList.Network, LogInfo.Warning, "一言获取失败", ex);
			}
		}

		private void SayHello() {
			try {
				SayHelloUsernameTxb.Text = Environment.UserName;

				var hour = DateTime.Now.Hour;
				switch (hour) {
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
			catch (Exception ex) {
				Log(true, ModuleList.IO, LogInfo.Error, ex: ex);
			}
		}

		/// <summary>
		/// 向Api接口获取一言并做出处理
		/// </summary>

		#endregion 一言及问好处理
	}
}
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SodaCL.Core.Auth;
using SodaCL.Core.Download;
using static SodaCL.Core.Auth.MSAuth;
using static SodaCL.Launcher.LauncherLogging;
using static SodaCL.Toolkits.GetResources;

namespace SodaCL.Pages
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>

    public partial class MainPage : Page
    {
        #region 初始化

        public MainPage()
        {
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            SayHello();
            GetYiyanAsync();
        }

        private void SayHello()
        {
            try
            {
                SayHelloUsernameTxb.Text = Environment.UserName;

                int hour = DateTime.Now.Hour;
                switch (hour)
                {
                    case int n when (n >= 3 && n < 5):
                        SayHelloTimeTxb.Text = "凌晨好!";
                        break;

                    case int n when (n >= 5 && n < 11):
                        SayHelloTimeTxb.Text = "清晨好!";
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
                Log(ModuleList.IO, LogInfo.Error, ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 向Api接口获取一言并做出处理
        /// </summary>
        private async void GetYiyanAsync()
        {
            Log(ModuleList.Network, LogInfo.Info, "正在获取一言");
            try
            {
                using (var client = new HttpClient())
                {
                    var yiYanApiAdd = "https://v1.hitokoto.cn/?c=c&c=a&encode=json&charset=utf-8&maxlength=10";
                    client.Timeout = TimeSpan.FromSeconds(5);
                    string jsonResponse = await client.GetStringAsync(yiYanApiAdd);
                    JObject jObj = JsonConvert.DeserializeObject<JObject>(jsonResponse);
                    string yiYan = (string)jObj["hitokoto"];
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
                    YiYanTxb.Text = $"「{space + yiYan + endSpace}」 ——  {(string)jObj["from"]}";
                    Log(ModuleList.Network, LogInfo.Info, "一言获取成功");
                }
            }
            catch (HttpRequestException ex)
            {
                Log(ModuleList.Network, LogInfo.Error, ex.Message, ex.StackTrace);
            }
        }

        #endregion 初始化

        #region 事件

        private void DownloadTestButtonClick(object sender, RoutedEventArgs e)
        {
            MultiDownload multiDownload = new(8, "http://jk-insider.bakaxl.com:8888/job/BakaXL%20Insider%20Parrot/lastSuccessfulBuild/artifact/BakaXL_Public/bin/Jenkins%20Release/BakaXL_Secure/BakaXL.exe", ".\\SodaCL\\BakaXL.exe");
            multiDownload.Start();
            MessageBox.Show("下载开始，请等待大约 30s 后点击启动按钮\n若启动器崩溃请重新打开启动器并执行下载");
            Log(ModuleList.Network, LogInfo.Info, "下载线程已启动");
            for (int i = 0; i < 1; i--)
            {
                if (multiDownload.IsComplete)
                {
                    MessageBox.Show("下载完成");
                    Log(ModuleList.Network, LogInfo.Info, "下载已完成");
                    break;
                }
            }
        }

        private void LogFolderOpenerButtonClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", ".\\SodaCL\\logs");
        }

        private void BakaXLStartUpBtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(".\\SodaCL\\BakaXL.exe");
                Log(ModuleList.Main, LogInfo.Info, "BakaXL 已启动");
            }
            catch (Exception ex)
            {
                Log(ModuleList.Main, LogInfo.Error, "BakaXL 未能正常启动，可能是下载的文件不完整" + ex.Message, ex.StackTrace);
            }
        }

        private async void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MSAuth msOAuth = new();
                msOAuth.OpenWindows += MSOAuth_OpenWindows;
                var mcAccount = await msOAuth.StartAuthAsync("7cb6044b-138a-48e9-994c-54d682ad1e34");
            }
            catch (Exception ex)
            {
                CloseDialog();
                Log(ModuleList.Network, LogInfo.Error, ex.Message, ex.StackTrace);
            }
        }

        #endregion 事件

        private async void MSOAuth_OpenWindows(object sender, (MSAuth.WindowsTypes, string) e)
        {
            if (e.Item1.Equals(WindowsTypes.OpenInBrowser))
            {
                ChangeDialog();
                await OpenOpenInBrowserWindow(e.Item2);
            }
            switch (e)
            {
                case (WindowsTypes.StartLogin, null):
                    {
                        await Dispatcher.InvokeAsync(() =>
                        {
                            OpenDialog();
                            DialogStackPan.Children.Add(new TextBlock() { Text = "正在初始化微软登录服务", FontSize = 18, TextAlignment = TextAlignment.Center });
                            DialogStackPan.Children.Add(new ProgressBar() { IsIndeterminate = true, Height = 10, Width = 300, Margin = new Thickness(0, 30, 0, 0) });
                        });
                        break;
                    }
            }
        }

        /// <summary>
        /// 打开一个登录说明界面
        /// </summary>
        /// <param name="deviceCode">显示的登陆代码</param>
        private async Task OpenOpenInBrowserWindow(string deviceCode)
        {
            await Dispatcher.InvokeAsync(() =>
            {
                var StackPan = new StackPanel { Margin = new Thickness(10, 10, 10, 0), Orientation = Orientation.Horizontal };
                var iconBor = new Border
                {
                    Height = 32,
                    Width = 32,
                    Background = GetBrush("Color1"),
                    CornerRadius = new CornerRadius(16),
                    Child = new Image
                    {
                        Width = 20,
                        Height = 20,
                        Source = GetSvg("Svg_Information"),
                    }
                };
                var exitButton = new Button
                {
                    Margin = new Thickness(125, 0, 0, 0),
                    Height = 32,
                    Width = 32,
                    Style = GetStyle("Btn_NoBackground"),
                    Content = new Image
                    {
                        Width = 20,
                        Height = 20,
                        Source = GetSvg("Svg_Close")
                    }
                };
                var okButton = new Button
                {
                    Margin = new Thickness(270, 0, 35, 0),
                    Content = GetI18NText("Butten_OK"),
                    Style = GetStyle("Btn_Main")
                };
                exitButton.Click += (s, e) =>
                {
                    CloseDialog();
                };
                okButton.Click += (s, be) =>
                    {
                        Clipboard.SetText(deviceCode);
                        Log(ModuleList.IO, LogInfo.Info, "成功将DeviceCode复制到剪切板");
                        Process.Start("explorer", "https://www.microsoft.com/link");
                        Log(ModuleList.IO, LogInfo.Info, "成功打开浏览器");
                    };
                StackPan.Children.Add(iconBor);
                StackPan.Children.Add(new TextBlock
                {
                    Height = 28,
                    Margin = new Thickness(10, 0, 0, 0),
                    Padding = new Thickness(0, 3, 0, 0),
                    Style = GetStyle("Text_Bold"),
                    Text = GetI18NText("Login_Microsoft_MessageBox_OpenInBrowser_Title")
                });
                StackPan.Children.Add(exitButton);
                DialogStackPan.Children.Add(StackPan);
                DialogStackPan.Children.Add(new TextBlock
                {
                    Margin = new Thickness(52, 10, 20, 0),
                    Text = GetI18NText("Login_Microsoft_MessageBox_OpenInBrowser_Text_Tip")
                });
                DialogStackPan.Children.Add(new TextBlock
                {
                    Margin = new Thickness(50, 10, 20, 0),
                    Style = GetStyle("Text_Bold"),
                    Text = GetI18NText("Login_Microsoft_MessageBox_OpenInBrowser_Text_YourLoginCode")
                });
                DialogStackPan.Children.Add(new TextBlock
                {
                    Margin = new Thickness(50, 5, 20, 0),
                    Text = deviceCode,
                    FontSize = 24,
                });
                DialogStackPan.Children.Add(okButton);
            });
        }

        private void OpenDialog()
        {
            FrontGrid.Visibility = Visibility.Visible;
            var easingFunc = new CubicEase
            {
                EasingMode = EasingMode.EaseInOut
            };
            var diaSbBig = new Storyboard();
            var rectBigOpacAni = new DoubleAnimation(0.6, TimeSpan.FromSeconds(1));
            Storyboard.SetTarget(rectBigOpacAni, DialogRect);
            Storyboard.SetTargetProperty(rectBigOpacAni, new PropertyPath("Opacity"));
            var borderBigWidthAni = new DoubleAnimation(400, TimeSpan.FromSeconds(1));
            borderBigWidthAni.EasingFunction = easingFunc;
            Storyboard.SetTarget(borderBigWidthAni, DialogBorder);
            Storyboard.SetTargetProperty(borderBigWidthAni, new PropertyPath("Width"));
            var borderBigHeightAni = new DoubleAnimation(250, TimeSpan.FromSeconds(1));
            borderBigHeightAni.EasingFunction = easingFunc;
            Storyboard.SetTarget(borderBigHeightAni, DialogBorder);
            Storyboard.SetTargetProperty(borderBigHeightAni, new PropertyPath("Height"));
            diaSbBig.Children.Add(rectBigOpacAni);
            diaSbBig.Children.Add(borderBigWidthAni);
            diaSbBig.Children.Add(borderBigHeightAni);
            diaSbBig.Begin();
        }

        private void ChangeDialog()
        {
            DialogStackPan.Children.Clear();
            FrontBorder.Visibility = Visibility.Visible;
            DialogBorder.Visibility = Visibility.Hidden;
            var easingFunc = new CubicEase
            {
                EasingMode = EasingMode.EaseInOut
            };
            var froSbSmall = new Storyboard();

            var borderSmallWidthAni = new DoubleAnimation(400, 0, TimeSpan.FromSeconds(0.5));
            borderSmallWidthAni.EasingFunction = easingFunc;

            Storyboard.SetTarget(borderSmallWidthAni, FrontBorder);
            Storyboard.SetTargetProperty(borderSmallWidthAni, new PropertyPath("Width"));

            var borderSmallHeightAni = new DoubleAnimation(250, 0, TimeSpan.FromSeconds(0.5));
            borderSmallHeightAni.EasingFunction = easingFunc;

            Storyboard.SetTarget(borderSmallHeightAni, FrontBorder);
            Storyboard.SetTargetProperty(borderSmallHeightAni, new PropertyPath("Height"));

            froSbSmall.Children.Add(borderSmallWidthAni);
            froSbSmall.Children.Add(borderSmallHeightAni);

            var diaSbBig = new Storyboard();

            var forBorderBigWidthAni = new DoubleAnimation(0, 400, TimeSpan.FromSeconds(1));
            forBorderBigWidthAni.EasingFunction = easingFunc;
            Storyboard.SetTarget(forBorderBigWidthAni, DialogBorder);
            Storyboard.SetTargetProperty(forBorderBigWidthAni, new PropertyPath("Width"));

            var forBorderBigHeightAni = new DoubleAnimation(0, 250, TimeSpan.FromSeconds(1));
            forBorderBigHeightAni.EasingFunction = easingFunc;
            Storyboard.SetTarget(forBorderBigHeightAni, DialogBorder);
            Storyboard.SetTargetProperty(forBorderBigHeightAni, new PropertyPath("Height"));
            diaSbBig.Children.Add(forBorderBigWidthAni);
            diaSbBig.Children.Add(forBorderBigHeightAni);

            froSbSmall.Completed += (object sender, EventArgs e) =>
            {
                FrontBorder.Visibility = Visibility.Hidden;
                DialogBorder.Visibility = Visibility.Visible;

                diaSbBig.Begin();
                Trace.WriteLine(FrontBorder.Width + FrontBorder.Width);
            };
            froSbSmall.Begin();
        }

        private void CloseDialog()
        {
            var easingFunc = new CubicEase
            {
                EasingMode = EasingMode.EaseInOut
            };
            var diaSbSmall = new Storyboard();
            var rectSmallOpacAni = new DoubleAnimation(0, TimeSpan.FromSeconds(0.6));
            Storyboard.SetTarget(rectSmallOpacAni, DialogRect);
            Storyboard.SetTargetProperty(rectSmallOpacAni, new PropertyPath("Opacity"));
            var borderSmallWidthAni = new DoubleAnimation(0, TimeSpan.FromSeconds(1));
            borderSmallWidthAni.EasingFunction = easingFunc;
            Storyboard.SetTarget(borderSmallWidthAni, DialogBorder);
            Storyboard.SetTargetProperty(borderSmallWidthAni, new PropertyPath("Width"));
            var borderSmallHeightAni = new DoubleAnimation(0, TimeSpan.FromSeconds(1));
            borderSmallHeightAni.EasingFunction = easingFunc;
            Storyboard.SetTarget(borderSmallHeightAni, DialogBorder);
            Storyboard.SetTargetProperty(borderSmallHeightAni, new PropertyPath("Height"));
            diaSbSmall.Children.Add(rectSmallOpacAni);
            diaSbSmall.Children.Add(borderSmallWidthAni);
            diaSbSmall.Children.Add(borderSmallHeightAni);
            diaSbSmall.Completed += (object sender, EventArgs e) =>
            {
                FrontGrid.Visibility = Visibility.Hidden;
                DialogStackPan.Children.Clear();
            };
            diaSbSmall.Begin();
        }
    }
}
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SodaCL.Core.Auth;
using SodaCL.Core.Download;
using static SodaCL.Launcher.LauncherLogging;

namespace SodaCL.Pages
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page
    {
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
                MessageBox.Show("发生错误:" + ex.Message + "\n" + ex.StackTrace);
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
                    var yiYanApiAdd = "https://v1.hitokoto.cn/?c=c&c=a&encode=json&charset=utf-8&maxlength=20";
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
                    YiYanTxb.Text = $"「{space + yiYan + endSpace}」—  {(string)jObj["from"]}";
                    Log(ModuleList.Network, LogInfo.Info, "一言获取成功");
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("发生错误:" + ex.Message + "\n" + ex.StackTrace);
                Log(ModuleList.Network, LogInfo.Error, ex.Message, ex.StackTrace);
            }
        }

        #region 事件

        private void DownloadTestButtonClick(object sender, RoutedEventArgs e)
        {
            MultiDownload multiDownload = new(8, "https://contents.baka.zone/Release/BakaXL_Public_Ver_3.2.3.2.exe", ".\\SodaCL\\BakaXL.exe");
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
            catch (Exception)
            {
                Log(ModuleList.Main, LogInfo.Error, "BakaXL 未能正常启动，可能是下载的文件不完整");
                throw;
            }
        }

        #endregion 事件

        private async void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrontGrid.Visibility = Visibility.Visible;
                DialogStackPan.Children.Add(new TextBlock() { Text = "正在初始化微软登录服务", FontSize = 18, TextAlignment = TextAlignment.Center });
                DialogStackPan.Children.Add(new ProgressBar() { IsIndeterminate = true, Height = 10, Width = 300, Margin = new Thickness(0, 30, 0, 0) });
                MSAuth msOAuth = new();
                await msOAuth.GetDeviceCode();
                DialogStackPan.Children.Clear();
                Button NormalBtn = new() { Width = 70, Height = 30, Content = "继续" };
                NormalBtn.Click += async (_, e) =>
                {
                    Process.Start("explorer", "https://www.microsoft.com/link");
                    Log(ModuleList.Login, LogInfo.Info, "在浏览器中打开https://www.microsoft.com/link");
                    Clipboard.SetText(msOAuth.UserCode);
                    Log(ModuleList.Login, LogInfo.Info, "将UserID复制到剪切板");
                    await msOAuth.GetAccessToken();
                    if (msOAuth.IsGetAccessTokenSuccess)
                    {
                        XboxAuth xboxAuth = new();
                        await xboxAuth.GetXboxXBLToken(msOAuth.AccessToken);
                        await xboxAuth.GetXboxXSTSToken();
                        MessageBox.Show(xboxAuth.XboxXSTSToken);
                    }
                    else
                    {
                        switch (msOAuth.AccessTokenErrorMsg)
                        {
                            case string errorMsg when (errorMsg.Equals("最终用户拒绝了授权请求")):
                                MessageBox.Show(errorMsg);
                                break;
                        }
                        //TODO:错误处理
                    }
                };
                DialogStackPan.Children.Add(new TextBlock() { Text = "SodaCL即将为您打开登录网页", FontSize = 18, TextAlignment = TextAlignment.Center });
                DialogStackPan.Children.Add(NormalBtn);
            }
            catch (Exception ex)
            {
                MessageBox.Show("发生错误:" + ex.Message + "\n" + ex.StackTrace);
                Log(ModuleList.Network, LogInfo.Error, ex.Message, ex.StackTrace);
            }
        }

        private void Rectangle_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FrontGrid.Visibility = Visibility.Hidden;
            DialogStackPan.Children.Clear();
        }
    }
}
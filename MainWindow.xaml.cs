using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SodaCL.Core.Downloader;
using SodaCL.Core.Minecraft;
using SodaCL.Launcher;
using SodaCL.Pages;
using static SodaCL.Launcher.LauncherLogging;

namespace SodaCL
{
    /// <summary>
    /// Interaction logic for CoreWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public string currentDir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        private List<MCClient> clients = new();
        LauncherInfo launcherInfo;
        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
        }
        #region 自定义标题栏
        // 退出按钮
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void LabelClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!File.Exists(LauncherInfo._versionListSavePath))
            {
                FileStream fileStream = new(LauncherInfo._versionListSavePath, FileMode.Create, FileAccess.ReadWrite);
                fileStream.Close();
            }
            File.WriteAllText(LauncherInfo._versionListSavePath, JsonConvert.SerializeObject(clients));

            if (!File.Exists(LauncherInfo._launcherInfoSavePath))
            {
                FileStream fileStream = new(LauncherInfo._launcherInfoSavePath, FileMode.Create, FileAccess.ReadWrite);
                fileStream.Close();
            }
            File.WriteAllText(LauncherInfo._launcherInfoSavePath, JsonConvert.SerializeObject(launcherInfo));
            this.Close();
        }
        private void LabelClose_MouseEnter(object sender, MouseEventArgs e)
        {
            ExitBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2C9C9"));
        }
        private void LabelClose_MouseLeave(object sender, MouseEventArgs e)
        {
            ExitBorder.Background = Brushes.Transparent;
        }
        // 最小化按钮
        private void LabelMin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void LabelMin_MouseEnter(object sender, MouseEventArgs e)
        {
            MinBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2C9C9"));
        }
        private void LabelMin_MouseLeave(object sender, MouseEventArgs e)
        {
            MinBorder.Background = Brushes.Transparent;
        }
        #endregion
        private void Window_Initialized(object sender, EventArgs e)
        {
            Log(ModuleList.Main, LogInfo.Info, "主窗体加载完毕");
            InitNewFolder();
            SayHello();
            GetYiyanAsync();

        }
        /// <summary>
        /// 新建MC及启动器文件
        /// </summary>
        private void InitNewFolder()
        {
            try
            {
                if (!File.Exists(LauncherInfo._versionListSavePath))
                {
                    FileStream fileStream = new(LauncherInfo._versionListSavePath, FileMode.Create, FileAccess.ReadWrite);
                    fileStream.Close();
                    Log(ModuleList.IO, LogInfo.Info, "新建版本文件");
                }
                else
                {
                    clients = JsonConvert.DeserializeObject<List<MCClient>>(File.ReadAllText(LauncherInfo._versionListSavePath));
                }

                if (!File.Exists(LauncherInfo._launcherInfoSavePath))
                {
                    FileStream fileStream = new(LauncherInfo._launcherInfoSavePath, FileMode.Create, FileAccess.ReadWrite);
                    fileStream.Close();
                    Log(ModuleList.IO, LogInfo.Info, "新建启动器文件");
                    this.launcherInfo = new LauncherInfo();
                }
                else
                {
                    this.launcherInfo = JsonConvert.DeserializeObject<LauncherInfo>(File.ReadAllText(LauncherInfo._launcherInfoSavePath));
                }
                this.launcherInfo.addLaunchTime(); // 启动器启动次数统计
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log(ModuleList.IO, LogInfo.Error, ex.Message);
            }
        }
        private void SayHello()
        {
            try
            {
                SayHelloUsername.Text = Environment.UserName;

                int hour = DateTime.Now.Hour;
                switch (hour)
                {
                    case int n when (n >= 3 && n < 5):
                        SayHelloTime.Text = "凌晨好!";
                        break;
                    case int n when (n >= 5 && n < 11):
                        SayHelloTime.Text = "清晨好!";
                        break;
                    case int n when (n >= 11 && n < 13):
                        SayHelloTime.Text = "中午好!";
                        break;
                    case int n when (n >= 13 && n < 17):
                        SayHelloTime.Text = "下午好!";
                        break;
                    case int n when (n >= 17 && n < 19):
                        SayHelloTime.Text = "傍晚好!";
                        break;
                    case int n when (n <= 23 && n >= 19 || n > 23):
                        SayHelloTime.Text = "晚上好!";
                        break;
                    case int n when (n >= 0 && n < 3):
                        SayHelloTime.Text = "午夜好!";
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log(ModuleList.IO, LogInfo.Error, ex.Message);
            }
        }
        /// <summary>
        /// 向Api接口获取一言
        /// </summary>
        private async void GetYiyanAsync()
        {
            Log(ModuleList.Network, LogInfo.Info, "正在获取一言");
            try
            {
                string _yiYanAPIAdd = "https://v1.hitokoto.cn/?c=c&c=a&encode=json&charset=utf-8&max_length=20";
                HttpClient client = new();
                client.Timeout = TimeSpan.FromSeconds(5);
                string _jsonResponse = await client.GetStringAsync(_yiYanAPIAdd);
                JObject jObj = JsonConvert.DeserializeObject<JObject>(_jsonResponse);
                YiYan.Text = $"「{(string)jObj["hitokoto"]}」—  {(string)jObj["from"]}";
                Log(ModuleList.Network, LogInfo.Info, "一言获取成功");

            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
                Log(ModuleList.Network, LogInfo.Error, ex.Message);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MSLogin MSLogin = new();
            MSLogin.Show();
        }

        private void DownloadTestButtonClick(object sender, RoutedEventArgs e)
        {
            MultiDownload multiDownload = new(1, "http://jk-insider.bakaxl.com:8888/job/BakaXL%20Insider%20Parrot/lastSuccessfulBuild/artifact/BakaXL_Public/bin/Jenkins%20Release/BakaXL_Secure/BakaXL.exe", ".\\SodaCL");
            MessageBox.Show("Download Started");
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Log(ModuleList.Main, LogInfo.Info, "程序退出");
            Trace.WriteLine("-------- SodaCL 程序日志记录结束 --------\n");

        }

        private void HelpBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void HelpBorder_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void HelpBorder_MouseLeave(object sender, MouseEventArgs e)
        {

        }
    }
}

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
using SodaCL.Core.Download;
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
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(LauncherInfo.versionListSavePath))
            {
                FileStream fileStream = new(LauncherInfo.versionListSavePath, FileMode.Create, FileAccess.ReadWrite);
                fileStream.Close();
            }
            File.WriteAllText(LauncherInfo.versionListSavePath, JsonConvert.SerializeObject(clients));

            if (!File.Exists(LauncherInfo.launcherInfoSavePath))
            {
                FileStream fileStream = new(LauncherInfo.launcherInfoSavePath, FileMode.Create, FileAccess.ReadWrite);
                fileStream.Close();
            }
            File.WriteAllText(LauncherInfo.launcherInfoSavePath, JsonConvert.SerializeObject(launcherInfo));
            this.Close();
        }
        private void MiniSizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
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
                if (!File.Exists(LauncherInfo.versionListSavePath))
                {
                    FileStream fileStream = new(LauncherInfo.versionListSavePath, FileMode.Create, FileAccess.ReadWrite);
                    fileStream.Close();
                    Log(ModuleList.IO, LogInfo.Info, "新建版本文件");
                }
                else
                {
                    clients = JsonConvert.DeserializeObject<List<MCClient>>(File.ReadAllText(LauncherInfo.versionListSavePath));
                }

                if (!File.Exists(LauncherInfo.launcherInfoSavePath))
                {
                    FileStream fileStream = new(LauncherInfo.launcherInfoSavePath, FileMode.Create, FileAccess.ReadWrite);
                    fileStream.Close();
                    Log(ModuleList.IO, LogInfo.Info, "新建启动器文件");
                    this.launcherInfo = new LauncherInfo();
                }
                else
                {
                    this.launcherInfo = JsonConvert.DeserializeObject<LauncherInfo>(File.ReadAllText(LauncherInfo.launcherInfoSavePath));
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
                    case int n when (n <= 23 && n >= 19 || n > 23):
                        SayHelloTimeTxb.Text = "晚上好!";
                        break;
                    case int n when (n >= 0 && n < 3):
                        SayHelloTimeTxb.Text = "午夜好!";
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
        /// 向Api接口获取一言并做出处理
        /// </summary>
        private async void GetYiyanAsync()
        {
            Log(ModuleList.Network, LogInfo.Info, "正在获取一言");
            try
            {
                string yiYanAPIAdd = "https://v1.hitokoto.cn/?c=c&c=a&encode=json&charset=utf-8&maxlength=20";
                HttpClient client = new();
                client.Timeout = TimeSpan.FromSeconds(5);
                string jsonResponse = await client.GetStringAsync(yiYanAPIAdd);
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
                    space = "";
                    endSpace = "";
                }
                YiYanTxb.Text = $"「{space + yiYan}」—  {(string)jObj["from"] + endSpace}";
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
            MSLogin msLogin = new();
            msLogin.Show();
        }
        private void DownloadTestButtonClick(object sender, RoutedEventArgs e)
        {
            MultiDownload multiDownload = new(16, "https://contents.baka.zone/Release/BakaXL_Public_Ver_3.2.2.1.exe", ".\\SodaCL\\BakaXL.exe");
            multiDownload.Start();
            MessageBox.Show("Download Started");
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
            }
            catch(Exception ex)
            {
                MessageBox.Show("您还没有使用SodaCL下载BakaXL");
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Log(ModuleList.Main, LogInfo.Info, "程序退出");
            Trace.WriteLine("-------- SodaCL 程序日志记录结束 --------\n");

        }
        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void IssuesBtn_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer","https://github.com/SodaCL-Launcher/SodaCL/issues");
        }
    }
}

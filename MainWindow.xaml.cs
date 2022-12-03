using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Newtonsoft.Json;
using SodaCL.Core.Minecraft;
using SodaCL.Launcher;

namespace SodaCL
{
    /// <summary>
    /// Interaction logic for CoreWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
            try
            {
                if (!Directory.Exists(LauncherInfo._SodaCLBasePath))
                {
                    Directory.CreateDirectory(LauncherInfo._SodaCLBasePath);
                }

                if (!Directory.Exists(LauncherInfo._MCDir))
                {
                    Directory.CreateDirectory(LauncherInfo._MCDir);
                }

                if (!File.Exists(LauncherInfo._versionListSavePath))
                {
                    FileStream fileStream = new(LauncherInfo._versionListSavePath, FileMode.Create, FileAccess.ReadWrite);
                    fileStream.Close();
                }
                else
                {
                    clients = JsonConvert.DeserializeObject<List<MCClient>>(File.ReadAllText(LauncherInfo._versionListSavePath));
                }

                if (!File.Exists(LauncherInfo._launcherInfoSavePath))
                {
                    FileStream fileStream = new(LauncherInfo._launcherInfoSavePath, FileMode.Create, FileAccess.ReadWrite);
                    fileStream.Close();
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
            }
            try
            {
                SayHelloUsername.Text = Environment.UserName;

                int hour = DateTime.Now.Hour;
                switch (hour)
                {
                    case int n when (n >= 0 && n < 5):
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
                    case int n when (n >= 5 && n <= 10):
                        SayHelloTime.Text = "清晨好!";
                        break;
                    case int n when (n < 0 && n >= 19):
                        SayHelloTime.Text = "晚上好!";
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MCDownload.GetManifest();
        }

    }
}

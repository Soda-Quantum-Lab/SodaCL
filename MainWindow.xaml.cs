using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using SodaCL.Main.Minecraft;
using SodaCL.Main.Downloader;

namespace SodaCL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<MCClient> clients = new();
        static private string _SodaCLBasePath = @".\SodaCL";
        static private string _versionListSavePath = _SodaCLBasePath + @"\versions.json";
        static private string _launcherInfoSavePath = _SodaCLBasePath + @".\launcher.json";
        static private string _MCDir = @".\.minecraft";
        LauncherInfo launcherInfo;
        public MainWindow()
        {
            InitializeComponent();
        }
        #region 自定义标题栏
        // 退出按钮
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void LabelClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!File.Exists(_versionListSavePath))
            {
                FileStream fileStream = new(_versionListSavePath, FileMode.Create, FileAccess.ReadWrite);
                fileStream.Close();
            }
            File.WriteAllText(_versionListSavePath, JsonConvert.SerializeObject(clients));

            if (!File.Exists(_launcherInfoSavePath))
            {
                FileStream fileStream = new(_launcherInfoSavePath, FileMode.Create, FileAccess.ReadWrite);
                fileStream.Close();
            }
            File.WriteAllText(_launcherInfoSavePath, JsonConvert.SerializeObject(launcherInfo));
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
            this.WindowState = System.Windows.WindowState.Minimized;
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
                if (!Directory.Exists(_SodaCLBasePath))
                {
                    Directory.CreateDirectory(_SodaCLBasePath);
                }

                if (!Directory.Exists(_MCDir))
                {
                    Directory.CreateDirectory(_MCDir);
                }

                if (!File.Exists(_versionListSavePath))
                {
                    FileStream fileStream = new(_versionListSavePath, FileMode.Create, FileAccess.ReadWrite);
                    fileStream.Close();
                }
                else
                {
                    clients = JsonConvert.DeserializeObject<List<MCClient>>(File.ReadAllText(_versionListSavePath));
                }

                if (!File.Exists(_launcherInfoSavePath))
                {
                    FileStream fileStream = new(_launcherInfoSavePath, FileMode.Create, FileAccess.ReadWrite);
                    fileStream.Close();
                    this.launcherInfo = new LauncherInfo();
                }
                else
                {
                    this.launcherInfo = JsonConvert.DeserializeObject<LauncherInfo>(File.ReadAllText(_launcherInfoSavePath));
                }
                this.launcherInfo.addLaunchTime(); // 启动器启动次数统计
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

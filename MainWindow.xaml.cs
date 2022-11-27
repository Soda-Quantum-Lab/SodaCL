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

/*
1>C:\Users\lu_yu\SDL\20221126\SodaCL-master\MainWindow.xaml(18,44,18,110): error CS0123: “Label1_MouseEnter”没有与委托“MouseEventHandler”匹配的重载
1>C:\Users\lu_yu\SDL\20221126\SodaCL-master\MainWindow.xaml(18,44,18,110): error CS0123: “Label1_MouseLeave”没有与委托“MouseEventHandler”匹配的重载
 */

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

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
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

        private void Label1_MouseEnter(object sender, MouseEventArgs e)
        {
            ExitBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2C9C9"));  
        }

        private void Label1_MouseLeave(object sender, MouseEventArgs e)
        {
            ExitBorder.Background = Brushes.Transparent;
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

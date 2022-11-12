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
using Sodium_Launcher.Main.Minecraft;

namespace Sodium_Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<MCClient> clients = new List<MCClient>();
        private string _versionListSavePath = @".\SDL\versions.json";
        private string _launcherInfoSavePath = @".\SDL\launcher.json";
        LauncherInfo launcherInf;
        public MainWindow()
        {
            InitializeComponent();
        }
        #region 自定义标题栏
        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            if(!File.Exists(_versionListSavePath))
            {
                FileStream fileStream = new FileStream(_versionListSavePath, FileMode.Create, FileAccess.ReadWrite);
                fileStream.Close();
            }
            File.WriteAllText(_versionListSavePath, JsonConvert.SerializeObject(clients));

            if(!File.Exists(_launcherInfoSavePath))
            {
                FileStream fileStream = new FileStream(_launcherInfoSavePath, FileMode.Create, FileAccess.ReadWrite);
                fileStream.Close();
            }
            File.WriteAllText(_launcherInfoSavePath, JsonConvert.SerializeObject(launcherInf));
            this.Close();
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion

        private void Window_Initialized(object sender, EventArgs e)
        {
            string _SDLDir = @".\SDL";
            string _MCDir = @".\.minecraft";
            try
            {
                if (!Directory.Exists(_SDLDir))
                {
                    Directory.CreateDirectory(_SDLDir);
                }

                if (!!Directory.Exists(_MCDir))
                {
                    Directory.CreateDirectory(_MCDir);
                }

                if (!File.Exists(_versionListSavePath))
                {
                    FileStream fileStream = new FileStream(_versionListSavePath, FileMode.Create, FileAccess.ReadWrite);
                    fileStream.Close();
                }
                else
                {
                    clients = JsonConvert.DeserializeObject<List<MCClient>>(File.ReadAllText(_versionListSavePath));
                }

                if (!File.Exists(_launcherInfoSavePath))
                {
                    FileStream fileStream = new FileStream(_launcherInfoSavePath, FileMode.Create, FileAccess.ReadWrite);
                    fileStream.Close();
                    this.launcherInf = new LauncherInfo();
                }
                else
                {
                    this.launcherInf = JsonConvert.DeserializeObject<LauncherInfo>(File.ReadAllText(_launcherInfoSavePath));
                }
                this.launcherInf.addLaunchTime(); // 启动器启动次数统计
            }
            catch(Exception ex)
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

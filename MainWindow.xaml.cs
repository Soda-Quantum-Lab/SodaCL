using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
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
using Sodium_Launcher.Main.Minecraft;

namespace Sodium_Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        #region 自定义标题栏
        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
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
                if (!Directory.Exists(_SDLDir) || !Directory.Exists(_MCDir))
                {
                    Directory.CreateDirectory(_SDLDir);
                    Directory.CreateDirectory(_MCDir);
                }
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

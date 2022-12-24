using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using SodaCL.Core.Minecraft;
using static SodaCL.Launcher.LauncherLogging;
namespace SodaCL.Windows.Login
{
    /// <summary>
    /// GetDeviceCode.xaml 的交互逻辑
    /// </summary>
    public partial class GetDeviceCode : Window
    {
        public GetDeviceCode()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, EventArgs e)
        {
            MCLogin login = new();
            try
            {
                login.DeviceFlowAuthAsync();
                this.Close();
            }
            catch (Exception ex)
            {
                HandyControl.Controls.MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                Log(ModuleList.Login, LogInfo.Error, ex.Message, ex.StackTrace);
                this.Close();
            }
        }
    }
}

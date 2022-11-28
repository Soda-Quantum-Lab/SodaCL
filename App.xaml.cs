using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SodaCL
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            SplashScreen splashScreen = new SplashScreen("/Resources/Images/Dev.ico");
            splashScreen.Show(true);
            base.OnStartup(e);

        }
    }
}

using System.IO;
using System.Windows;
using SodaCL.Launcher;

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
            Directory.CreateDirectory(LauncherInfo.SodaCLBasePath);
            Directory.CreateDirectory(LauncherInfo.MCDir);
            base.OnStartup(e);
        }

    }
}

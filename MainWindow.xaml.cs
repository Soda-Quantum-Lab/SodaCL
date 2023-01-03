using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using SodaCL.Core;
using SodaCL.Launcher;
using static SodaCL.Launcher.LauncherLogging;

namespace SodaCL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow mainWindow;
        public static LauncherInfo launcherInfo;
        public static List<MCClient> clients = new();
        private bool IsThisPage;

        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;
        }

        #region 自定义标题栏

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        // 退出按钮
        private void TitleBar_ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Toolkits.IniFile.Write("LaunchTime", Convert.ToString(int.Parse(Toolkits.IniFile.Read("LaunchTime")??"0") + 1 ));
            }
            catch(Exception ex)
            {
                Log(ModuleList.Main, LogInfo.Error, ex.Message, ex.StackTrace);
            }
            finally
            {
                this.Close();
            }
        }

        //最小化按钮
        private void TitleBar_MiniSizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        #endregion 自定义标题栏

        #region 初次启动

        private void Window_Initialized(object sender, EventArgs e)
        {
            LauncherInit.InitNewFolder();

            Log(ModuleList.Main, LogInfo.Info, "主窗体加载完毕");
        }

        #endregion 初次启动

        #region 事件

        private void TitleBar_SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFram.Navigate(new Uri("\\Pages\\Settings\\Set_About.xaml", UriKind.Relative));
            DoubleAnimation titleBarAni = new();
        }

        private void TitleBar_IssuesBtn_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer", "https://github.com/Soda-Quantum-Lab/SodaCL/issues");
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Log(ModuleList.Main, LogInfo.Info, "程序退出");
            Trace.WriteLine("-------- SodaCL 程序日志记录结束 --------\n");
        }

        #endregion 事件

        private void TitleBar_GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFram.GoBack();
        }

        private void MainFram_Navigated(object sender, NavigationEventArgs e)
        {
            var easingFunc = new CubicEase
            {
                EasingMode = EasingMode.EaseInOut
            };
            var AniTime = TimeSpan.FromSeconds(0.4);
            if (MainFram.CanGoBack == true && !IsThisPage)

            {
                var goBackSb = new Storyboard();

                var goBackBtnAni = new ThicknessAnimation(new Thickness(-50, 0, 0, 0), new Thickness(20, 0, 0, 0), AniTime);
                goBackBtnAni.EasingFunction = easingFunc;
                Storyboard.SetTarget(goBackBtnAni, TitleBar_GoBackBtn);
                Storyboard.SetTargetProperty(goBackBtnAni, new PropertyPath("Margin"));
                goBackSb.Children.Add(goBackBtnAni);
                var goBackPanAni = new ThicknessAnimation(new Thickness(5, 0, 0, 0), new Thickness(-240, 6, 0, 0), AniTime);
                goBackPanAni.EasingFunction = easingFunc;
                Storyboard.SetTarget(goBackPanAni, TitleBar_TitlePan);
                Storyboard.SetTargetProperty(goBackPanAni, new PropertyPath("Margin"));
                goBackSb.Children.Add(goBackPanAni);
                goBackSb.Begin();
                TitleBar_GoBackBtn.Visibility = Visibility.Visible;
            }
            else if (MainFram.CanGoBack == false)
            {
                var goBackSb = new Storyboard();
                var goBackBtnAni = new ThicknessAnimation(new Thickness(20, 0, 0, 0), new Thickness(-50, 0, 0, 0), AniTime);
                goBackBtnAni.EasingFunction = easingFunc;
                Storyboard.SetTarget(goBackBtnAni, TitleBar_GoBackBtn);
                Storyboard.SetTargetProperty(goBackBtnAni, new PropertyPath("Margin"));
                goBackSb.Children.Add(goBackBtnAni);
                var goBackPanAni = new ThicknessAnimation(new Thickness(-240, 0, 0, 0), new Thickness(5, 0, 0, 0), AniTime);
                goBackPanAni.EasingFunction = easingFunc;
                Storyboard.SetTarget(goBackPanAni, TitleBar_TitlePan);
                Storyboard.SetTargetProperty(goBackPanAni, new PropertyPath("Margin"));
                goBackSb.Children.Add(goBackPanAni);
                goBackSb.Completed += (object sender, EventArgs e) =>
                {
                    TitleBar_GoBackBtn.Visibility = Visibility.Hidden;
                };
                goBackSb.Begin();
            }
        }

        private void MainFram_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.Uri == MainFram.CurrentSource)
                IsThisPage = true;
            else
                IsThisPage = false;
        }
    }
}
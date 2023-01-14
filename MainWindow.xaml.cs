using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using SodaCL.Core.Game;
using SodaCL.Launcher;
using static SodaCL.Toolkits.Logger;

namespace SodaCL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow mainWindow;
        public static LauncherInfo launcherInfo;
        public static List<MC_Client> clients = new();
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
            catch (Exception ex)
            {
                Log(true, ModuleList.Main, LogInfo.Error, ex: ex);
            }
            finally
            {
                var opcAni = new DoubleAnimation(0, TimeSpan.FromSeconds(0.1));
                opcAni.Completed += (sender, r) => { this.Close(); };
                this.BeginAnimation(OpacityProperty, opcAni);
            }
        }

        //最小化按钮
        private void TitleBar_MiniSizeBtn_Click(object sender, RoutedEventArgs e)
        {
            var opcAni = new DoubleAnimation(0, TimeSpan.FromSeconds(0.1));
            opcAni.Completed += (sender, r) => { this.WindowState = WindowState.Minimized; };
            this.BeginAnimation(OpacityProperty, opcAni);
        }

        #endregion 自定义标题栏

        #region 初次启动

        private void Window_Initialized(object sender, EventArgs e)
        {
            LauncherInit.InitNewFolder();

            Log(false, ModuleList.Main, LogInfo.Info, "主窗体加载完毕");
        }

        #endregion 初次启动

        #region 事件

        private void TitleBar_SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFram.Navigate(new Uri("\\Pages\\Settings\\Set_About.xaml", UriKind.Relative));
        }

        private void TitleBar_IssuesBtn_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer", "https://github.com/Soda-Quantum-Lab/SodaCL/issues");
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            var opcAni = new DoubleAnimation(1, TimeSpan.FromSeconds(0.1));
            this.BeginAnimation(OpacityProperty, opcAni);
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Log(false, ModuleList.Main, LogInfo.Info, "程序退出");
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
            var AniTime = TimeSpan.FromSeconds(0.3);
            if (MainFram.CanGoBack == true && !IsThisPage)
            {
                var goBackSb = new Storyboard();

                var goBackPanAni = new ThicknessAnimation(new Thickness(5, 0, 0, 0), new Thickness(-240, 6, 0, 0), AniTime);
                goBackPanAni.EasingFunction = easingFunc;
                Storyboard.SetTarget(goBackPanAni, TitleBar_TitlePan);
                Storyboard.SetTargetProperty(goBackPanAni, new PropertyPath("Margin"));
                goBackSb.Children.Add(goBackPanAni);

                var goBackBtnAni = new ThicknessAnimation(new Thickness(-50, 0, 0, 0), new Thickness(10, 0, 0, 0), AniTime);
                goBackBtnAni.EasingFunction = easingFunc;
                goBackBtnAni.BeginTime = TimeSpan.FromSeconds(0.2);
                Storyboard.SetTarget(goBackBtnAni, TitleBar_GoBackBtn);
                Storyboard.SetTargetProperty(goBackBtnAni, new PropertyPath("Margin"));
                goBackSb.Children.Add(goBackBtnAni);

                var goBackOpacAni = new DoubleAnimation(1, TimeSpan.FromSeconds(0.3));
                Storyboard.SetTarget(goBackOpacAni, MainFram);
                Storyboard.SetTargetProperty(goBackOpacAni, new PropertyPath("Opacity"));
                goBackSb.Children.Add(goBackOpacAni);

                goBackSb.Begin();
                TitleBar_GoBackBtn.Visibility = Visibility.Visible;
            }
            else if (MainFram.CanGoBack == false)
            {
                var goBackSb = new Storyboard();
                var goBackBtnAni = new ThicknessAnimation(new Thickness(10, 0, 0, 0), new Thickness(-50, 0, 0, 0), AniTime);
                goBackBtnAni.EasingFunction = easingFunc;
                Storyboard.SetTarget(goBackBtnAni, TitleBar_GoBackBtn);
                Storyboard.SetTargetProperty(goBackBtnAni, new PropertyPath("Margin"));
                goBackSb.Children.Add(goBackBtnAni);

                var goBackPanAni = new ThicknessAnimation(new Thickness(-240, 0, 0, 0), new Thickness(5, 0, 0, 0), AniTime);
                goBackPanAni.EasingFunction = easingFunc;
                goBackPanAni.BeginTime = TimeSpan.FromSeconds(0.2);
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
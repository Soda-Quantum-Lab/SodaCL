using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using static SodaCL.Toolkits.GetResources;

namespace SodaCL.Toolkits
{
    public class Dialog
    {
        private static TimeSpan DialogAniSpeed { get; } = TimeSpan.FromSeconds(0.5);
        private static TimeSpan OpacAniSpeed { get; } = TimeSpan.FromSeconds(0.7);
        private static CubicEase EasingFunc { get; set; } = new CubicEase { EasingMode = EasingMode.EaseInOut };

        public static void OpenDialog()
        {
            GlobalVariable.IsDialogOpen = true;
            MainWindow.mainWindow.TitleBar_SettingsBtn.IsEnabled = false;
            MainWindow.mainWindow.FrontGrid.Visibility = Visibility.Visible;
            var diaSbBig = new Storyboard();
            var rectBigOpacAni = new DoubleAnimation(0.6, OpacAniSpeed);
            Storyboard.SetTarget(rectBigOpacAni, MainWindow.mainWindow.DialogRect);
            Storyboard.SetTargetProperty(rectBigOpacAni, new PropertyPath("Opacity"));
            var borderBigWidthAni = new DoubleAnimation(0, 400, DialogAniSpeed);
            borderBigWidthAni.EasingFunction = EasingFunc;
            Storyboard.SetTarget(borderBigWidthAni, MainWindow.mainWindow.DialogBorder);
            Storyboard.SetTargetProperty(borderBigWidthAni, new PropertyPath("Width"));
            var borderBigHeightAni = new DoubleAnimation(0, 250, DialogAniSpeed);
            borderBigHeightAni.EasingFunction = EasingFunc;
            Storyboard.SetTarget(borderBigHeightAni, MainWindow.mainWindow.DialogBorder);
            Storyboard.SetTargetProperty(borderBigHeightAni, new PropertyPath("Height"));
            diaSbBig.Children.Add(rectBigOpacAni);
            diaSbBig.Children.Add(borderBigWidthAni);
            diaSbBig.Children.Add(borderBigHeightAni);
            diaSbBig.Begin();
        }
        public static void OpenErrorDialog(string message,string stack)
        {
            OpenDialog();
            var StackPan = new StackPanel { Margin = new Thickness(10, 20, 10, 0), Orientation = Orientation.Horizontal };
            var iconBor = new Border
            {
                Height = 32,
                Width = 32,
                Margin = new Thickness(5, 0, 0, 0),
                Background = GetBrush("Color1"),
                CornerRadius = new CornerRadius(16),
                Child = new System.Windows.Controls.Image
                {
                    Width = 20,
                    Height = 20,
                    Source = GetSvg("Svg_Information"),
                }
            };
            var okButton = new Button
            {
                Margin = new Thickness(270, 0, 0, 0),
                Content = GetI18NText("Butten_OK"),
                Style = GetStyle("Btn_Main")
            };
            okButton.Click += (s, be) =>
            {
                CloseDialog();
            };
            StackPan.Children.Add(iconBor);
            StackPan.Children.Add(new TextBlock
            {
                Height = 28,
                Margin = new Thickness(10, 0, 0, 0),
                Padding = new Thickness(0, 3, 0, 0),
                Style = GetStyle("Text_Bold"),
                Text = GetI18NText("Login_Microsoft_MessageBox_OpenInBrowser_Title")
            });
            MainWindow.mainWindow.DialogStackPan.Children.Add(StackPan);
            MainWindow.mainWindow.DialogStackPan.Children.Add(new TextBlock
            {
                Margin = new Thickness(57, 10, 20, 0),
                Text = GetI18NText("Error")
            });
            MainWindow.mainWindow.DialogStackPan.Children.Add(new TextBlock
            {
                Margin = new Thickness(56, 10, 20, 0),
                Style = GetStyle("Text_Bold"),
                Text = message + "\n" + stack
            });
            MainWindow.mainWindow.DialogStackPan.Children.Add(okButton);
        }

        public static void ChangeDialog()
        {
            MainWindow.mainWindow.DialogStackPan.Children.Clear();
            MainWindow.mainWindow.FrontBorder.Visibility = Visibility.Visible;
            MainWindow.mainWindow.DialogBorder.Visibility = Visibility.Hidden;
            var froSbSmall = new Storyboard();

            var borderSmallWidthAni = new DoubleAnimation(400, 0, DialogAniSpeed);
            borderSmallWidthAni.EasingFunction = EasingFunc;

            Storyboard.SetTarget(borderSmallWidthAni, MainWindow.mainWindow.FrontBorder);
            Storyboard.SetTargetProperty(borderSmallWidthAni, new PropertyPath("Width"));

            var borderSmallHeightAni = new DoubleAnimation(250, 0, DialogAniSpeed);
            borderSmallHeightAni.EasingFunction = EasingFunc;

            Storyboard.SetTarget(borderSmallHeightAni, MainWindow.mainWindow.FrontBorder);
            Storyboard.SetTargetProperty(borderSmallHeightAni, new PropertyPath("Height"));

            froSbSmall.Children.Add(borderSmallWidthAni);
            froSbSmall.Children.Add(borderSmallHeightAni);

            var diaSbBig = new Storyboard();

            var forBorderBigWidthAni = new DoubleAnimation(0, 400, DialogAniSpeed);
            forBorderBigWidthAni.EasingFunction = EasingFunc;
            Storyboard.SetTarget(forBorderBigWidthAni, MainWindow.mainWindow.DialogBorder);
            Storyboard.SetTargetProperty(forBorderBigWidthAni, new PropertyPath("Width"));

            var forBorderBigHeightAni = new DoubleAnimation(0, 250, DialogAniSpeed);
            forBorderBigHeightAni.EasingFunction = EasingFunc;
            Storyboard.SetTarget(forBorderBigHeightAni, MainWindow.mainWindow.DialogBorder);
            Storyboard.SetTargetProperty(forBorderBigHeightAni, new PropertyPath("Height"));
            diaSbBig.Children.Add(forBorderBigWidthAni);
            diaSbBig.Children.Add(forBorderBigHeightAni);

            froSbSmall.Completed += (object sender, EventArgs e) =>
            {
                MainWindow.mainWindow.FrontBorder.Visibility = Visibility.Hidden;
                MainWindow.mainWindow.DialogBorder.Visibility = Visibility.Visible;

                diaSbBig.Begin();
                Trace.WriteLine(MainWindow.mainWindow.FrontBorder.Width + MainWindow.mainWindow.FrontBorder.Width);
            };
            froSbSmall.Begin();
        }

        public static void CloseDialog()
        {
            MainWindow.mainWindow.TitleBar_SettingsBtn.IsEnabled = true;
            var diaSbSmall = new Storyboard();
            var rectSmallOpacAni = new DoubleAnimation(0, OpacAniSpeed);
            Storyboard.SetTarget(rectSmallOpacAni, MainWindow.mainWindow.DialogRect);
            Storyboard.SetTargetProperty(rectSmallOpacAni, new PropertyPath("Opacity"));
            var borderSmallWidthAni = new DoubleAnimation(400, 0, DialogAniSpeed);
            borderSmallWidthAni.EasingFunction = EasingFunc;
            Storyboard.SetTarget(borderSmallWidthAni, MainWindow.mainWindow.DialogBorder);
            Storyboard.SetTargetProperty(borderSmallWidthAni, new PropertyPath("Width"));
            var borderSmallHeightAni = new DoubleAnimation(250, 0, DialogAniSpeed);
            borderSmallHeightAni.EasingFunction = EasingFunc;
            Storyboard.SetTarget(borderSmallHeightAni, MainWindow.mainWindow.DialogBorder);
            Storyboard.SetTargetProperty(borderSmallHeightAni, new PropertyPath("Height"));
            diaSbSmall.Children.Add(rectSmallOpacAni);
            diaSbSmall.Children.Add(borderSmallWidthAni);
            diaSbSmall.Children.Add(borderSmallHeightAni);
            diaSbSmall.Completed += (object sender, EventArgs e) =>
            {
                MainWindow.mainWindow.FrontGrid.Visibility = Visibility.Hidden;
                MainWindow.mainWindow.DialogStackPan.Children.Clear();
            };
            diaSbSmall.Begin();
            GlobalVariable.IsDialogOpen = false;
        }
    }
}
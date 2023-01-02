using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;

namespace SodaCL.Toolkits
{
    public class Dialog
    {
        public static void OpenDialog()
        {
            MainWindow.mainWindow.FrontGrid.Visibility = Visibility.Visible;
            var easingFunc = new CubicEase
            {
                EasingMode = EasingMode.EaseInOut
            };
            var diaSbBig = new Storyboard();
            var rectBigOpacAni = new DoubleAnimation(0.6, TimeSpan.FromSeconds(1));
            Storyboard.SetTarget(rectBigOpacAni, MainWindow.mainWindow.DialogRect);
            Storyboard.SetTargetProperty(rectBigOpacAni, new PropertyPath("Opacity"));
            var borderBigWidthAni = new DoubleAnimation(400, TimeSpan.FromSeconds(1));
            borderBigWidthAni.EasingFunction = easingFunc;
            Storyboard.SetTarget(borderBigWidthAni, MainWindow.mainWindow.DialogBorder);
            Storyboard.SetTargetProperty(borderBigWidthAni, new PropertyPath("Width"));
            var borderBigHeightAni = new DoubleAnimation(250, TimeSpan.FromSeconds(1));
            borderBigHeightAni.EasingFunction = easingFunc;
            Storyboard.SetTarget(borderBigHeightAni, MainWindow.mainWindow.DialogBorder);
            Storyboard.SetTargetProperty(borderBigHeightAni, new PropertyPath("Height"));
            diaSbBig.Children.Add(rectBigOpacAni);
            diaSbBig.Children.Add(borderBigWidthAni);
            diaSbBig.Children.Add(borderBigHeightAni);
            diaSbBig.Begin();
        }

        public static void ChangeDialog()
        {
            MainWindow.mainWindow.DialogStackPan.Children.Clear();
            MainWindow.mainWindow.FrontBorder.Visibility = Visibility.Visible;
            MainWindow.mainWindow.DialogBorder.Visibility = Visibility.Hidden;
            var easingFunc = new CubicEase
            {
                EasingMode = EasingMode.EaseInOut
            };
            var froSbSmall = new Storyboard();

            var borderSmallWidthAni = new DoubleAnimation(400, 0, TimeSpan.FromSeconds(0.5));
            borderSmallWidthAni.EasingFunction = easingFunc;

            Storyboard.SetTarget(borderSmallWidthAni, MainWindow.mainWindow.FrontBorder);
            Storyboard.SetTargetProperty(borderSmallWidthAni, new PropertyPath("Width"));

            var borderSmallHeightAni = new DoubleAnimation(250, 0, TimeSpan.FromSeconds(0.5));
            borderSmallHeightAni.EasingFunction = easingFunc;

            Storyboard.SetTarget(borderSmallHeightAni, MainWindow.mainWindow.FrontBorder);
            Storyboard.SetTargetProperty(borderSmallHeightAni, new PropertyPath("Height"));

            froSbSmall.Children.Add(borderSmallWidthAni);
            froSbSmall.Children.Add(borderSmallHeightAni);

            var diaSbBig = new Storyboard();

            var forBorderBigWidthAni = new DoubleAnimation(0, 400, TimeSpan.FromSeconds(1));
            forBorderBigWidthAni.EasingFunction = easingFunc;
            Storyboard.SetTarget(forBorderBigWidthAni, MainWindow.mainWindow.DialogBorder);
            Storyboard.SetTargetProperty(forBorderBigWidthAni, new PropertyPath("Width"));

            var forBorderBigHeightAni = new DoubleAnimation(0, 250, TimeSpan.FromSeconds(1));
            forBorderBigHeightAni.EasingFunction = easingFunc;
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
            var easingFunc = new CubicEase
            {
                EasingMode = EasingMode.EaseInOut
            };
            var diaSbSmall = new Storyboard();
            var rectSmallOpacAni = new DoubleAnimation(0, TimeSpan.FromSeconds(0.6));
            Storyboard.SetTarget(rectSmallOpacAni, MainWindow.mainWindow.DialogRect);
            Storyboard.SetTargetProperty(rectSmallOpacAni, new PropertyPath("Opacity"));
            var borderSmallWidthAni = new DoubleAnimation(0, TimeSpan.FromSeconds(1));
            borderSmallWidthAni.EasingFunction = easingFunc;
            Storyboard.SetTarget(borderSmallWidthAni, MainWindow.mainWindow.DialogBorder);
            Storyboard.SetTargetProperty(borderSmallWidthAni, new PropertyPath("Width"));
            var borderSmallHeightAni = new DoubleAnimation(0, TimeSpan.FromSeconds(1));
            borderSmallHeightAni.EasingFunction = easingFunc;
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
        }
    }
}

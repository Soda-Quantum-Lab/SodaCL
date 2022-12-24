using System.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using Newtonsoft.Json;
using SodaCL.Core.Minecraft;
using SodaCL.Launcher;
using static SodaCL.Launcher.LauncherLogging;

namespace SodaCL.Pages.Settings
{
    /// <summary>
    /// Set_About.xaml 的交互逻辑
    /// </summary>
    public partial class Set_About : Page
    {
        public Set_About()
        {
            InitializeComponent();

        }
        //private void BackBtn_Click(object sender, RoutedEventArgs e)
        //{
            //MainFram.Source = new Uri("\\Pages\\Settings\\Set_About.xaml", UriKind.Relative);
            //DoubleAnimation titleBarAni = new();
        //}
    }
}

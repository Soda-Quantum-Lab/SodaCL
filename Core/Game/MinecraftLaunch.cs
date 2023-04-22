﻿using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Win32;
using Newtonsoft.Json;
using static SodaCL.Launcher.LauncherInfo;
using SodaCL.Toolkits;
using static SodaCL.Toolkits.DeviceInfo;
using static SodaCL.Toolkits.DataTool;
using System;
using SodaCL.Core.Models;

namespace SodaCL.Core.Game
{
	public class MinecraftLaunch
	{
		private MinecraftLaunch()
		{ }

		private static List<string> LaunchArguments { get; set; }

		public static void LaunchGame()
		{
			SpliceArgumentsMain();
		}

		public static void SpliceArgumentsMain()
		{
			SpliceBasicArguments();
		}

		public static string SpliceBasicArguments()
		{
			//TODO:JavaPath
			var javaPathJson = RegEditor.GetKeyValue(Registry.CurrentUser, "JavaList");
			var javaPath = JsonConvert.DeserializeObject<JavaModel>(javaPathJson);

			//LaunchArguments.Add(javaPath[0].ToString());
			LaunchArguments.Add($" -Xmx2048");
			LaunchArguments.Add($" -Xmx2048");
			LaunchArguments.Add($" -Xmn256m");
			LaunchArguments.Add(" -XX:+UseG1GC");
			LaunchArguments.Add(" -XX:-UseAdaptiveSizePolicy");
			LaunchArguments.Add(" -XX:-OmitStackTraceInFastThrow");
			LaunchArguments.Add($" -Dos.name=Windows {GetOsMajorNumber()} ");
			LaunchArguments.Add($" -Dminecraft.launcher.brand=SodaCL");
			LaunchArguments.Add($" -Dminecraft.launcher.version={Assembly.GetExecutingAssembly().GetName().Version.ToString()}");
			return SplitListAndToString(LaunchArguments, " ");
		}
		public static void SpliceMcArguments()
		{
			LaunchArguments.Add($"--username {"HRxiaohu"}");
			LaunchArguments.Add($"--version {"1.7.0"}");
			LaunchArguments.Add($"--gameDir {sodaMcDir}");
			LaunchArguments.Add($"--assetsDir ");
			LaunchArguments.Add($"--assetIndex ");
			LaunchArguments.Add($"--uuid {Guid.NewGuid().ToString("N")}");
			LaunchArguments.Add($"--accessToken ");
			LaunchArguments.Add($"--userType "); 
			LaunchArguments.Add($"--versionType ");
			LaunchArguments.Add($"--width ");
			LaunchArguments.Add($"--height ");
		}
		public static void CreateLaunchScript(string script)
		{
			var targetPath = DirConverter(sodaCLForderPath) + "LaunchBat.bat";
			File.CreateText(targetPath);
			File.WriteAllText(targetPath, script);
		}
	}
}
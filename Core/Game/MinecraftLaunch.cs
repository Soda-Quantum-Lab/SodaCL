using System.Collections.Generic;
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
using SodaCL.Launcher;

namespace SodaCL.Core.Game
{
	public class MinecraftLaunch
	{
		private static CoreModel _CoreInfo;
		private static string _Uuid;

		private MinecraftLaunch()
		{
		}

		private static List<string> LaunchArguments { get; set; }

		public static void LaunchGame()
		{
			_CoreInfo = JsonConvert.DeserializeObject<CoreModel>(RegEditor.GetKeyValue(Registry.CurrentUser, "CurrentGameInfo"));
			SpliceArgumentsMain();
		}

		public static void SpliceArgumentsMain()
		{
			switch (RegEditor.GetKeyValue(Registry.CurrentUser, "LoginType"))
			{
				case "0":
					_Uuid = new Guid().ToString("N");

					break;
			}
			SpliceBasicArguments();
		}

		public static string SpliceBasicArguments()
		{
			//TODO:JavaPath
			var javaPathJson = RegEditor.GetKeyValue(Registry.CurrentUser, "CacheJavaList");
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
			LaunchArguments.Add($"--username {RegEditor.GetKeyValue(Registry.CurrentUser, "UserName")}");
			LaunchArguments.Add($"--version {_CoreInfo.Version}");
			LaunchArguments.Add($"--gameDir {DirConverter(_CoreInfo.GameDir)}");
			LaunchArguments.Add($"--assetsDir ");
			LaunchArguments.Add($"--assetIndex ");
			LaunchArguments.Add($"--uuid {_Uuid}");
			LaunchArguments.Add($"--accessToken ");
			LaunchArguments.Add($"--userType ");
			LaunchArguments.Add($"--versionType ");
			LaunchArguments.Add($"--width 854");
			LaunchArguments.Add($"--height 480");
		}

		public static void CreateLaunchScript(string script)
		{
			var targetPath = DirConverter(SODACL_FOLDER_PATH) + "LaunchBat.bat";
			File.CreateText(targetPath);
			File.WriteAllText(targetPath, script);
		}
	}
}
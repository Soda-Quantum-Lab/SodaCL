using Microsoft.Win32;
using Newtonsoft.Json;
using SodaCL.Core.Models;
using SodaCL.Toolkits;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using static SodaCL.Launcher.LauncherInfo;
using static SodaCL.Toolkits.DataTool;
using static SodaCL.Toolkits.DeviceInfo;

namespace SodaCL.Core.Game
{
	public class MinecraftLaunch
	{
		private static CoreModel _coreInfo;
		private static string _uuid;
		private static string _assetsDir;
		private static string _assetsIndex;
		private static string _accessToken;
		private static string _userType;
		private static string _versionType;

		private MinecraftLaunch()
		{
		}

		private static List<string> LaunchArguments { get; set; }

		public static void LaunchGame()
		{
			_coreInfo = JsonConvert.DeserializeObject<CoreModel>(RegEditor.GetKeyValue(Registry.CurrentUser, "CurrentGameInfo"));
			SpliceArgumentsMain();
		}

		public static void SpliceArgumentsMain()
		{
			switch (RegEditor.GetKeyValue(Registry.CurrentUser, "LoginType"))
			{
				case "0":
					_uuid = new Guid().ToString("N");
					_userType = "Legacy";

					break;
			}
			SpliceBasicArguments();
		}

		public static string SpliceBasicArguments()
		{
			//TODO:JavaPath
			var javaPathJson = RegEditor.GetKeyValue(Registry.CurrentUser, "CacheJavaList");
			var javaPath = JsonConvert.DeserializeObject<JavaModel>(javaPathJson);

			//LaunchArguments.Add();
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
			LaunchArguments.Add($"--version {_coreInfo.Version}");
			LaunchArguments.Add($"--gameDir {DirConverter(_coreInfo.GameDir)}");
			LaunchArguments.Add($"--assetsDir {_assetsDir}");
			LaunchArguments.Add($"--assetIndex {_assetsIndex}");
			LaunchArguments.Add($"--uuid {_uuid}");
			LaunchArguments.Add($"--accessToken {_accessToken}");
			LaunchArguments.Add($"--userType {_userType}");
			LaunchArguments.Add($"--versionType {_versionType}");
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
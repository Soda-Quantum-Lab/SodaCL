using Microsoft.Win32;
using Newtonsoft.Json;
using SodaCL.Core.Models;
using SodaCL.Toolkits;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Xps;
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

		private static List<string> BasicArguments { get; set; }
		private static List<string> LibrariesArguments { get; set; }
		private static List<string> McArguments { get; set; }

		public static void LaunchGame()
		{
			_coreInfo = JsonConvert.DeserializeObject<CoreModel>(RegEditor.GetKeyValue(Registry.CurrentUser, "CurrentGameInfo"));
			SpliceArgumentsMain();

			var p = new Process();
			p.StartInfo.FileName = "C:\\Program Files\\Zulu\\zulu-17\\bin\\java.exe";  //小胡的 Java 选择器捏
			p.StartInfo.Arguments = "11";  //TODO
			p.StartInfo.CreateNoWindow = false;
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardInput = true;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.RedirectStandardError = true;

			p.Start();
		}

		public static void SpliceArgumentsMain()
		{
			switch (RegEditor.GetKeyValue(Registry.CurrentUser, "LoginType"))
			{
				case "0":
					_uuid = new Guid().ToString("N");
					_userType = "legacy";

					break;
				case "1":
					_uuid = "111";  //TODO
					_userType = "msa";

					break;
			}

			var basicArguments = SpliceBasicArguments();
			var libArguments = SpliceLibrariesArguments();
			var mcArguments = SpliceMcArguments();
		}

		public static string SpliceBasicArguments()
		{
			//TODO:JavaPath
			var javaPathJson = RegEditor.GetKeyValue(Registry.CurrentUser, "CacheJavaList");
			var javaPath = JsonConvert.DeserializeObject<JavaModel>(javaPathJson);

			//TODO: Args Modify (内存修改, GC 回收器修改) 

			//TODO: Natives 文件处理 (-Djava.library.path="E:\Minecraft\.minecraft\$natives")

			//BasicArguments.Add();
			BasicArguments.Add($" -Xmx2048M");
			BasicArguments.Add($" -Xms2048M");
			BasicArguments.Add($" -Xmn256M");
			BasicArguments.Add(" -XX:+UseG1GC");
			BasicArguments.Add(" -XX:-UseAdaptiveSizePolicy");
			BasicArguments.Add(" -XX:-OmitStackTraceInFastThrow");
			BasicArguments.Add($" -Dos.name=Windows {GetOsMajorNumber()} ");
			BasicArguments.Add($" -Dminecraft.launcher.brand=SodaCL");
			BasicArguments.Add($" -Dminecraft.launcher.version={Assembly.GetExecutingAssembly().GetName().Version.ToString()}");
			return SplitListAndToString(BasicArguments, " ");
		}

		public static string SpliceLibrariesArguments()
		{
			//TODO

			var versionJsonFile = "E:\\Minecraft\\.minecraft\\versions\\1.20.1\\1.20.1.json";
			//var versionJson = JsonConvert.DeserializeObject<JavaModel>(javaPathJson);

			//LibrariesArguments.Add();
			return SplitListAndToString(LibrariesArguments, " ");
		}

		public static string SpliceMcArguments()
		{
			//McArguments.Add();
			McArguments.Add($"--username {RegEditor.GetKeyValue(Registry.CurrentUser, "UserName")}");
			McArguments.Add($"--version {_coreInfo.Version}");
			McArguments.Add($"--gameDir {DirConverter(_coreInfo.GameDir)}");
			McArguments.Add($"--assetsDir {_assetsDir}");
			McArguments.Add($"--assetIndex {_assetsIndex}");
			McArguments.Add($"--uuid {_uuid}");
			McArguments.Add($"--accessToken {_accessToken}");
			McArguments.Add($"--userType {_userType}");
			McArguments.Add($"--versionType {_versionType}");
			McArguments.Add($"--width 854");
			McArguments.Add($"--height 480");
			return SplitListAndToString(McArguments, " ");
		}

		public static void CreateLaunchScript(string script)
		{
			var targetPath = DirConverter(SODACL_FOLDER_PATH) + "LaunchBat.bat";
			File.CreateText(targetPath);
			File.WriteAllText(targetPath, script);
		}

	}
}
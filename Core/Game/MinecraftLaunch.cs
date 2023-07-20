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
		private static AssetModel _assetInfo;
		private static string _uuid;
		private static string _assetsDir;
		private static string _assetsIndex;
		private static string _accessToken;
		private static string _xuid;
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
			_assetInfo = JsonConvert.DeserializeObject<AssetModel>(_coreInfo.GameDir + "\\" + _coreInfo.VersionName);
			var StartArgs = SpliceArgumentsMain();

			var p = new Process();
			p.StartInfo.FileName = "C:\\Program Files\\Zulu\\zulu-17\\bin\\java.exe";  // Java 选择器暂时还没搓
			p.StartInfo.Arguments = StartArgs;
			p.StartInfo.CreateNoWindow = false;
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardInput = true;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.RedirectStandardError = true;

			p.Start();
		}

		public static string SpliceArgumentsMain()
		{
			switch (RegEditor.GetKeyValue(Registry.CurrentUser, "LoginType"))
			{
				// 后续离线账户可能默认用 Authlib-Injector 了

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
			return basicArguments + libArguments + mcArguments;
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
			//LibrariesArguments.Add();
			LibrariesArguments.Add(" -cp ");
			foreach (var libraries in _assetInfo.Downloads.Client.Path)
			{
				var libProcessed = PathConverter(libraries.ToString());
				var libPath = SODA_MC_VERSIONS_DIR + "\\" + libProcessed;
				if (!File.Exists(libPath))
				{
					Logger.Log(false, Logger.ModuleList.IO, Logger.LogInfo.Warning, "启动 Minecraft 所需的 Libraries 文件不存在: " + libPath);
					// 这里应该需要一个自动下载的逻辑，但是多线程下载器鸽秋还不会用
				}
				LibrariesArguments.Add(libPath + ";");
			}
			return SplitListAndToString(LibrariesArguments, " ");
		}

		public static string SpliceMcArguments()
		{
			//McArguments.Add();
			McArguments.Add($"--username {RegEditor.GetKeyValue(Registry.CurrentUser, "UserName")}");
			McArguments.Add($"--version {_coreInfo.VersionName}");
			McArguments.Add($"--gameDir {DirConverter(_coreInfo.GameDir)}");
			McArguments.Add($"--assetsDir " + SODA_MC_DIR + "\\assets");
			McArguments.Add($"--assetIndex " + _assetInfo.AssetIndex);
			McArguments.Add($"--uuid {_uuid}");
			McArguments.Add($"--accessToken {_accessToken}");

			// 微软账户特有 xuid 处理
			if (RegEditor.GetKeyValue(Registry.CurrentUser, "LoginType") == "1")
			{
				McArguments.Add($"--xuid {_xuid}");
			}

			McArguments.Add($"--userType {_userType}");
			McArguments.Add($"--versionType {_versionType}");
			McArguments.Add($"--width 854");
			McArguments.Add($"--height 480");
			return SplitListAndToString(McArguments, " ");
		}

		public static void CreateLaunchScript(string script)
		{
			var targetPath = DirConverter(SODACL_FOLDER_PATH) + "Launch.bat";
			File.CreateText(targetPath);
			File.WriteAllText(targetPath, script);
		}

	}
}
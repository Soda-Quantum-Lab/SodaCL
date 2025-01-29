using Microsoft.Win32;
using Newtonsoft.Json;
using SodaCL.Models.Core.Game;
using SodaCL.Toolkits;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using static SodaCL.Launcher.LauncherInfo;
using static SodaCL.Toolkits.DataTool;
using static SodaCL.Toolkits.DeviceInfo;
using static SodaCL.Toolkits.Logger;
using static SodaCL.Core.Java.JavaProcess;

namespace SodaCL.Core.Game {

	public class MinecraftLaunch {
		private static CoreModel _coreInfo;
		private static AssetModel _assetInfo;
		private static string _uuid;
		private static string _assetsDir;
		private static string _assetsIndex;
		private static string _accessToken;
		private static string _xuid;
		private static string _userType;
		private static string _versionType;

		private MinecraftLaunch() {
		}

		public static void LaunchGame() {
			// _coreInfo = JsonConvert.DeserializeObject<CoreModel>(RegEditor.GetKeyValue(Registry.CurrentUser, "CurrentGameInfo"));
			// _assetInfo = JsonConvert.DeserializeObject<AssetModel>(_coreInfo.GameDir + "\\" + _coreInfo.VersionName);
			var json = File.ReadAllText($"{SODA_MC_DIR}\\versions\\1.21.4\\1.21.4.json");
			_assetInfo = JsonConvert.DeserializeObject<AssetModel>(json);
			var StartArgs = SpliceArgumentsMain();
			Log(false, ModuleList.IO, LogInfo.Info, StartArgs);

			var javaPath = "";
			// javaPath = JavaAutoSelector("1.21.4");
			javaPath = "C:\\Program Files\\Zulu\\zulu-21\\bin\\java.exe";

			var p = new Process();
			p.StartInfo = new ProcessStartInfo {
				FileName = $"\"{javaPath}\"",
				Arguments = StartArgs,
				CreateNoWindow = true,
				UseShellExecute = false,
				RedirectStandardInput = true,
				RedirectStandardOutput = true,
				RedirectStandardError = true,
			};
			p.Start();
		}

		public static string SpliceArgumentsMain() {
			switch (RegEditor.GetKeyValue(Registry.CurrentUser, "LoginType")) {
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
			var mcArguments = SpliceMcArguments(0);
			return basicArguments + libArguments + mcArguments;
		}

		public static string SpliceBasicArguments() {
			// var javaPath = JavaFindingAndSelecting.JavaAutoSelector(_coreInfo.MajorVersion);
			//if (javaPath == "核心版本非法") {
			//	Logger.Log(false, Logger.ModuleList.IO, Logger.LogInfo.Warning, "核心版本非法，无法确定所需 Java ");
			//}

			//TODO: Args Modify (内存修改, GC 回收器修改)

			//TODO: Natives 文件处理 (-Djava.library.path="E:\Minecraft\.minecraft\$natives")

			var BasicArguments = "";
			BasicArguments += $" -Xmx2048M";
			BasicArguments += $" -Xms2048M";
			BasicArguments += $" -Xmn256M";
			BasicArguments += " -XX:+UseG1GC";
			BasicArguments += " -XX:-UseAdaptiveSizePolicy";
			BasicArguments += " -XX:-OmitStackTraceInFastThrow";
			BasicArguments += " -Dlog4j2.formatMsgNoLookups=true";
			BasicArguments += " -Dos.name=Windows";
			BasicArguments += " -Dminecraft.launcher.brand=SodaCL";
			BasicArguments += $" -Dminecraft.launcher.version={Assembly.GetExecutingAssembly().GetName().Version.ToString()}";
			return BasicArguments;
		}

		public static string SpliceLibrariesArguments() {
			var LibrariesArguments = "";
			LibrariesArguments += " -cp ";
			foreach (var library in _assetInfo.Libraries) {
				var path = library.Downloads.Artifact.Path;
				var libProcessed = PathConverter(path.ToString());
				var libPath = SODA_MC_DIR + "\\libraries\\" + libProcessed;
				if (libPath.Contains("macos") || libPath.Contains("linux") || libPath.Contains("arm64"))
				{
					continue;
				}
				if (!File.Exists(libPath)) {
					Logger.Log(false, Logger.ModuleList.IO, Logger.LogInfo.Warning, "启动 Minecraft 所需的 Libraries 文件不存在: " + libPath);
					// 这里应该需要一个自动下载的逻辑，但是多线程下载器鸽秋还不会用
				}
				LibrariesArguments += libPath + ";";
			}
			LibrariesArguments += "E:\\Code\\SodaCL\\bin\\Debug\\net8.0-windows\\.minecraft\\versions\\1.21.4\\1.21.4.jar";
			return LibrariesArguments;
		}

		/// <summary>
		/// 获取 MC 档案及资源相关启动信息
		/// </summary>
		/// <param name="accountType">档案类型，0 为离线验证，1 为正版验证</param>
		public static string SpliceMcArguments(int accountType) {
			var McArguments = $" {_assetInfo.MainClass}";
			//McArguments += $" --username {RegEditor.GetKeyValue(Registry.CurrentUser, "UserName")}";
			// McArguments.Add($"--version {_coreInfo.VersionName}");
			//McArguments.Add($"--gameDir {DirConverter(_coreInfo.GameDir)}");
			McArguments += $" --username SodaCLTest";
			McArguments += $" --version 1.21.4"	;
			McArguments += $" --gameDir {SODA_MC_DIR}\\versions\\1.21.4";
			McArguments += $" --assetsDir {SODA_MC_DIR}\\assets";
			McArguments += $" --assetIndex {_assetInfo.AssetIndex.Id}";
			//McArguments += $" --uuid {_uuid}";
			//McArguments += $" --accessToken {_accessToken}";
			McArguments += $" --uuid 000000000000300A9D840A3AC868D84A";
			McArguments += $" --accessToken 000000000000300A9D840A3AC868D84A";

			// 微软账户特有 xuid 处理
			//if (RegEditor.GetKeyValue(Registry.CurrentUser, "LoginType") == "1") {
			//	McArguments += $" --xuid {_xuid}";
			//}

			var loginType = "";
			switch (accountType) {
				case 0:
					loginType = "legacy";
					break;
				case 1:
					loginType = "msa";
					break;
			}
			McArguments += $" --userType {loginType}";
			McArguments += $" --versionType SodaCL";
			McArguments += $" --width 854";
			McArguments += $" --height 480";
			return McArguments;
		}

		public static void CreateLaunchScript(string script) {
			var targetPath = DirConverter(SODACL_FOLDER_PATH) + "Launch.bat";
			File.CreateText(targetPath);
			File.WriteAllText(targetPath, script);
		}
	}
}
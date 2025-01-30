using Microsoft.Win32;
using SodaCL.Toolkits;
using System;
using System.Globalization;
using System.IO;
using static SodaCL.Toolkits.Logger;
using static SodaCL.Launcher.LauncherInfo;

namespace SodaCL.Launcher {

	public static class LauncherInit {

		public static void DeleteTempFolder() {
			if (Directory.GetFiles(SODACL_TEMP_FOLDER_PATH).Length > 0) {
				var tempDir = new DirectoryInfo(SODACL_TEMP_FOLDER_PATH);
				var tempFiles = tempDir.GetFiles();
				foreach (var files in tempFiles) {
					File.Delete(files.FullName);
				}
				Log(false, ModuleList.IO, LogInfo.Debug, "正在清空缓存");
			}
		}

		/// <summary>
		/// 新建Minecraft及启动器文件
		/// </summary>
		public static void Setup() {
			try {
				if (!Directory.Exists(SODACL_FOLDER_PATH))
					Directory.CreateDirectory(SODACL_FOLDER_PATH);

				if (!Directory.Exists(SODA_MC_DIR))
					Directory.CreateDirectory(SODA_MC_DIR);

				if (!Directory.Exists(SODACL_LOG_FOLDER_PATH))
					Directory.CreateDirectory(SODACL_LOG_FOLDER_PATH);

				//if (!Directory.Exists(sodaCLFontsForderPath))
				//	Directory.CreateDirectory(sodaCLFontsForderPath);

				if (!Directory.Exists(SODA_MC_VERSIONS_DIR))
					Directory.CreateDirectory(SODA_MC_VERSIONS_DIR);

				if (!Directory.Exists(APP_DATA_DIR))
					Directory.CreateDirectory(APP_DATA_DIR);

				if (!File.Exists(SODACL_SETTINGS))
					DataTool.ExtractFile("SodaCL.Resources.DefaultSetting.xml", SODACL_SETTINGS);

				if (!Directory.Exists(SODACL_TEMP_FOLDER_PATH))
					Directory.CreateDirectory(SODACL_TEMP_FOLDER_PATH);

				if (Registry.CurrentUser.OpenSubKey(@"Software\SodaCL") == null)
					RegEditor.CreateSubKey(Registry.CurrentUser, @"Software\SodaCL");

				if (RegEditor.GetKeyValue(Registry.CurrentUser, "IsSetuped") != "True") {
					FirstSetup();
				}
			}
			catch (Exception ex) {
				Log(true, ModuleList.IO, LogInfo.Info, ex: ex);
			}
		}

		//TODO:做不做捏？
		//public static async Task InitMiSansFonts()
		//{
		//	if (new DirectoryInfo(sodaCLFontsForderPath).GetFiles().Length != 11)
		//	{
		//		try
		//		{
		//			var zipPath = sodaCLFontsForderPath + "\\MiSans.zip";
		//			var md = new FileDownloader("https://cdn.cnbj1.fds.api.mi-img.com/vipmlmodel/font/MiSans/MiSans.zip", zipPath);
		//			md.DownloaderProgressFinished += (sender, e) =>
		//			{
		//				ZipFile.ExtractToDirectory(zipPath, sodaCLFontsForderPath);
		//			};
		//			await md.Start();
		//		}
		//		catch (Exception ex)
		//		{
		//			Log(true, ModuleList.Main, LogInfo.Warning, GetResources.GetText("Error_Init_Fonts_CannotLoad"), ex);
		//		}
		//	}
		//}
		public static void FirstSetup() {

			#region 注册表

			if (RegionInfo.CurrentRegion.Name == "CN")
				RegEditor.SetKeyValue(Registry.CurrentUser, "DownloadSource", "2", RegistryValueKind.String);
			else
				RegEditor.SetKeyValue(Registry.CurrentUser, "DownloadSource", "0", RegistryValueKind.String);
			RegEditor.SetKeyValue(Registry.CurrentUser, "IsSetuped", "True", RegistryValueKind.String);

			#endregion 注册表
		}
	}
}
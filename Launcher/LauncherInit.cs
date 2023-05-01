using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using Microsoft.Win32;
using SodaCL.Toolkits;
using static SodaCL.Toolkits.Logger;

namespace SodaCL.Launcher
{
	public static class LauncherInit
	{
		public static void DeleteTempFolder()
		{
			if (Directory.GetFiles(LauncherInfo.sodaCLTempForderPath).Length > 0)
			{
				var tempDir = new DirectoryInfo(LauncherInfo.sodaCLTempForderPath);
				var tempFiles = tempDir.GetFiles();
				foreach (var files in tempFiles)
				{
					File.Delete(files.FullName);
				}
				Log(false, ModuleList.IO, LogInfo.Debug, "正在清空缓存");
			}
		}

		/// <summary>
		/// 新建Minecraft及启动器文件
		/// </summary>
		public static void Setup()
		{
			try
			{
				if (!Directory.Exists(LauncherInfo.sodaCLForderPath))
					Directory.CreateDirectory(LauncherInfo.sodaCLForderPath);
				if (!Directory.Exists(LauncherInfo.sodaMcDir))
					Directory.CreateDirectory(LauncherInfo.sodaMcDir);
				if (!Directory.Exists(LauncherInfo.sodaCLLogForderPath))
					Directory.CreateDirectory(LauncherInfo.sodaCLLogForderPath);
				//if (!Directory.Exists(LauncherInfo.sodaCLFontsForderPath))
				//	Directory.CreateDirectory(LauncherInfo.sodaCLFontsForderPath);
				if (!Directory.Exists(LauncherInfo.SodaMcVersionsDir))
					Directory.CreateDirectory(LauncherInfo.SodaMcVersionsDir);
				if (!Directory.Exists(LauncherInfo.appDataDir))
					Directory.CreateDirectory(LauncherInfo.appDataDir);
				if (!File.Exists(LauncherInfo.sodaCLConfigFilePath))
					File.Create(LauncherInfo.sodaCLConfigFilePath);
				if (!Directory.Exists(LauncherInfo.sodaCLTempForderPath))
					Directory.CreateDirectory(LauncherInfo.sodaCLTempForderPath);
				if (Registry.CurrentUser.OpenSubKey(@"Software\SodaCL") == null)
					RegEditor.CreateSubKey(Registry.CurrentUser, @"Software\SodaCL");
				if (RegEditor.GetKeyValue(Registry.CurrentUser, "IsSetuped") != "True")
				{
					FirstSetup();
				}
			}
			catch (Exception ex)
			{
				Log(true, ModuleList.IO, LogInfo.Info, ex: ex);
			}
		}

		//TODO:做不做捏？
		//public static async Task InitMiSansFonts()
		//{
		//	if (new DirectoryInfo(LauncherInfo.sodaCLFontsForderPath).GetFiles().Length != 11)
		//	{
		//		try
		//		{
		//			var zipPath = LauncherInfo.sodaCLFontsForderPath + "\\MiSans.zip";
		//			var md = new FileDownloader("https://cdn.cnbj1.fds.api.mi-img.com/vipmlmodel/font/MiSans/MiSans.zip", zipPath);
		//			md.DownloaderProgressFinished += (sender, e) =>
		//			{
		//				ZipFile.ExtractToDirectory(zipPath, LauncherInfo.sodaCLFontsForderPath);
		//			};
		//			await md.Start();
		//		}
		//		catch (Exception ex)
		//		{
		//			Log(true, ModuleList.Main, LogInfo.Warning, GetResources.GetText("Error_Init_Fonts_CannotLoad"), ex);
		//		}
		//	}
		//}
		public static void FirstSetup()
		{
			#region 注册表
			if (RegionInfo.CurrentRegion.Name == "CN")
				RegEditor.SetKeyValue(Registry.CurrentUser, "Software\\SodaCL", "DownloadSource", "2", RegistryValueKind.String);
			else
				RegEditor.SetKeyValue(Registry.CurrentUser, "Software\\SodaCL", "DownloadSource", "0", RegistryValueKind.String);
			RegEditor.SetKeyValue(Registry.CurrentUser, "Software\\SodaCL", "IsSetuped", "True", RegistryValueKind.String);
			#endregion
		}
	}
}
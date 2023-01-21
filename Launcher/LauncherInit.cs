using System;
using System.IO;
using Microsoft.Win32;
using SodaCL.Toolkits;
using static SodaCL.Toolkits.Logger;

namespace SodaCL.Launcher
{
	public static class LauncherInit
	{
		/// <summary>
		/// 新建Minecraft及启动器文件
		/// </summary>
		public static void InitNewFolder()
		{
			try
			{
				if (!File.Exists(LauncherInfo.sodaCLBasePath))
					Directory.CreateDirectory(LauncherInfo.sodaCLBasePath);
				if (!File.Exists(LauncherInfo.mcDir))
					Directory.CreateDirectory(LauncherInfo.mcDir);
				if (!File.Exists(LauncherInfo.sodaCLLogPath))
					Directory.CreateDirectory(LauncherInfo.sodaCLLogPath);
				if (!File.Exists(LauncherInfo.appDataDir))
					Directory.CreateDirectory(LauncherInfo.appDataDir);
				if (!File.Exists(LauncherInfo.sodaCLConfigPath))
					File.Create(LauncherInfo.sodaCLConfigPath);
				if (Registry.CurrentUser.OpenSubKey(@"Software\SodaCL") == null)
					RegEditor.CreateSubKey(Registry.CurrentUser, @"Software\SodaCL");

			}
			catch (Exception ex)
			{
				Log(true, ModuleList.IO, LogInfo.Info, ex: ex);
			}
		}
	}
}
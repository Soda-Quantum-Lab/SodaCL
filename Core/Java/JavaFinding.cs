using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using Microsoft.Win32;
using Newtonsoft.Json;
using SodaCL.Core.Models;
using SodaCL.Toolkits;
using static SodaCL.Toolkits.DataTool;
using static SodaCL.Toolkits.Logger;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Documents;

namespace SodaCL.Core.Java
{
	public class JavaFinding
	{
		/// <summary>
		/// 自动 Java 查找
		/// </summary>

		public static void AutoJavaFinding()
		{
			const string javaExeName = "java.exe";
			const string javawExeName = "javaw.exe";

			var javaList = new List<JavaModel>();
			//查找环境变量中的 Java
			var envPathOrigin = Environment.GetEnvironmentVariable("Path");
			var envPathArray = envPathOrigin.Split(";");
			foreach (var item in envPathArray)
			{
				if (File.Exists(DirConverter(item) + "java.exe"))
					javaList.Add(new JavaModel()
					{
						DirPath = item,
						JavaPath = DirConverter(item) + javaExeName,
						JavawPath = DirConverter(item) + javawExeName
					});
			}

			//查找 AppData 中的 Java
			SearchJavaInFolder(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ref javaList);

			//查找 Disk 中的 Java
			foreach (var disk in DriveInfo.GetDrives())
			{
				if (disk.DriveType == DriveType.Fixed)
					SearchJavaInFolder(disk.Name, ref javaList);
			}

			//List 去重
			javaList = javaList.GroupBy(d => new { d.DirPath }).Select(d => d.FirstOrDefault()).ToList();
			Log(false, ModuleList.IO, LogInfo.Info, $"成功搜索到 {javaList.Count} 个 Java: ");

			//获取 Java 版本
			RegEditor.SetKeyValue(Registry.CurrentUser, @"Software\SodaCL", "JavaList", JsonConvert.SerializeObject(javaList), RegistryValueKind.String);
			foreach (var java in javaList)
			{
				var p = new Process();

				p.StartInfo.FileName = java.JavaPath.ToString();
				p.StartInfo.Arguments = " -version";
				p.StartInfo.CreateNoWindow = true;
				p.StartInfo.UseShellExecute = false;
				p.StartInfo.RedirectStandardInput = true;
				p.StartInfo.RedirectStandardOutput = true;
				p.StartInfo.RedirectStandardError = true;

				p.Start();

				var Output = p.StandardError.ReadToEnd();

				var rg = new Regex("(?<=(version \"))[.\\s\\S]*?(?=(\"))", RegexOptions.Multiline | RegexOptions.Singleline);
				var match = rg.Match(Output);

				java.Version = match.Value;
				java.Is64Bit = Output.Contains("64-Bit");

				p.WaitForExit();
				p.Close();

				if (Output != "")
				{

				}
				else
				{
					Log(false, ModuleList.IO, LogInfo.Warning, "[Java] 尝试运行该 Java 失败");
				}

				Log(false, ModuleList.IO, LogInfo.Info, "版本: " + java.Version.ToString() + " 是否为 64 位: " + java.Is64Bit.ToString() + " 路径: " + java.DirPath.ToString());

				App.Current.Dispatcher.BeginInvoke(new Action(() =>
				{
					var mainPage = new Pages.MainPage();
					mainPage.JavaComboBoxResetter();
					mainPage.JavaComboBoxItemAdder(java.Version.ToString(), java.Is64Bit, java.DirPath.ToString());
				}));
				
				
			}
		}

		public static void SearchJavaInFolder(string targetDir, ref List<JavaModel> javaList)
		{
			if (!Directory.Exists(targetDir))
			{
				Log(false, ModuleList.IO, LogInfo.Warning, $"SodaCL 找不到文件夹 {targetDir}");
				return;
			}
			try
			{
				if (File.Exists(DirConverter(targetDir) + "java.exe"))
				{
					javaList.Add(new JavaModel()
					{
						DirPath = targetDir,
						JavaPath = DirConverter(targetDir) + "java.exe",
						JavawPath = DirConverter(targetDir) + "javaw.exe",
					});
				}
				foreach (var item in new DirectoryInfo(targetDir).EnumerateDirectories())
				{
					if (item.Attributes.HasFlag(FileAttributes.ReparsePoint))
						continue;
					var searchKey = item.Name.ToLower();
					// 搜索条件来自 PCL2 ，作者爱发电地址 https://afdian.net/a/ltCAT.
					if (item.Parent.Name.ToLower().Equals("users") || searchKey.Contains("java") || searchKey.Contains("jdk") || searchKey.Contains("env") ||

							searchKey.Contains("环境") || searchKey.Contains("run") || searchKey.Contains("软件") ||

							searchKey.Contains("jre") || searchKey.Equals("bin") || searchKey.Contains("mc") ||

							searchKey.Contains("software") || searchKey.Contains("cache") || searchKey.Contains("temp") ||

							searchKey.Contains("corretto") || searchKey.Contains("roaming") || searchKey.Contains("users") ||

							searchKey.Contains("craft") || searchKey.Contains("program") || searchKey.Contains("世界") ||

							searchKey.Contains("net") || searchKey.Contains("游戏") || searchKey.Contains("oracle") ||

							searchKey.Contains("game") || searchKey.Contains("file") || searchKey.Contains("data") ||

							searchKey.Contains("jvm") || searchKey.Contains("服务") || searchKey.Contains("server") ||

							searchKey.Contains("客户") || searchKey.Contains("client") || searchKey.Contains("整合") ||

							searchKey.Contains("应用") || searchKey.Contains("运行") || searchKey.Contains("前置") ||

							searchKey.Contains("mojang") || searchKey.Contains("官启") || searchKey.Contains("新建文件夹") ||

							searchKey.Contains("eclipse") || searchKey.Contains("microsoft") || searchKey.Contains("hotspot") ||

							searchKey.Contains("runtime") || searchKey.Contains("x86") || searchKey.Contains("x64") ||

							searchKey.Contains("forge") || searchKey.Contains("原版") || searchKey.Contains("optifine") ||

							searchKey.Contains("官方") || searchKey.Contains("启动") || searchKey.Contains("hmcl") ||

							searchKey.Contains("mod") || searchKey.Contains("高清") || searchKey.Contains("download") ||

							searchKey.Contains("launch") || searchKey.Contains("程序") || searchKey.Contains("path") ||

							searchKey.Contains("国服") || searchKey.Contains("网易") || searchKey.Contains("ext") ||

							searchKey.Contains("netease") || searchKey.Contains("1.") || searchKey.Contains("启动") ||

							searchKey.Contains("bakaxl") || searchKey.Contains("zulu") || searchKey.Contains("liberica") ||

							searchKey.Contains("minecraft") || searchKey.Contains("adoptium") || searchKey.Contains("lib"))
						SearchJavaInFolder(item.FullName, ref javaList);
				}
			}
			catch (UnauthorizedAccessException ex)
			{
				Log(false, ModuleList.IO, LogInfo.Warning, $"SodaCL 遇到没有权限访问的文件夹 {targetDir}", ex);
			}
			catch (Exception ex)
			{
				Log(false, ModuleList.IO, LogInfo.Error, ex: ex);
			}
		}
	}
}
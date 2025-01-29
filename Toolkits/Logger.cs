using Microsoft.AppCenter.Crashes;
using SodaCL.Controls.Dialogs;
using SodaCL.Launcher;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace SodaCL.Toolkits {
	public static class Logger {
		/// <summary>
		/// Log文件夹
		/// </summary>
		public static DirectoryInfo logDir = new(LauncherInfo.SODACL_LOG_FOLDER_PATH);

		/// <summary>
		/// Log目录下的所有文件
		/// </summary>
		public static FileInfo[] logFiles = logDir.GetFiles();

		// Log 模块介绍: 第一位 bool 参数: 控制是否显示 MsgBox , 若为 true 则显示 第二位 ModuleList 参数: 控制 Log 输出的模块类型 第三位
		// LogInfo 参数: 控制日志等级
		// 第四位参数: Log 输出的内容
		/// <summary>
		/// 日志级别枚举
		/// </summary>
		public enum LogInfo {
			Info,
			Warning,
			Debug,
			Error,
			Unhandled
		}

		/// <summary>
		/// 模块位置枚举
		/// </summary>
		public enum ModuleList {
			Main,
			Control,
			Network,
			IO,
			Login,
			Unknown
		}

		public static int GetFileNum() {
			try {
				var fileNum = 0;
				foreach (var f in logDir.GetFiles())
					fileNum++;
				return fileNum;
			}
			catch (Exception ex) {
				Log(true, ModuleList.IO, LogInfo.Error, "Log 文件夹或文件异常", ex);
				throw;
			}
		}

		/// <summary>
		/// 写入 Log
		/// </summary>
		/// <param name="module">写入Log的模块位置</param>
		/// <param name="LogInfo">Log级别</param>
		/// <param name="message ">需要写入的Log信息或自定义错误信息</param>
		public static void Log(bool isOpenDialog, ModuleList module, LogInfo LogInfo, string message = null, Exception ex = null) {
			string moduleText = null;
			string logContent = null;
			switch (module) {
				case ModuleList.Main:
					moduleText = "Main";
					break;

				case ModuleList.Control:
					moduleText = "Control";
					break;

				case ModuleList.Network:
					moduleText = "Network";
					break;

				case ModuleList.IO:
					moduleText = "IO";
					break;

				case ModuleList.Login:
					moduleText = "Login";
					break;
			}
			if (ex != null) {
				if (message != null) {
					logContent = $"发生错误： {message}";
				}
				else {
					logContent = $"发生错误： {ex.Message}\n{ex.StackTrace}";
				}
				Crashes.TrackError(ex);
			}
			else {
				logContent = message;
			}
			if (isOpenDialog) {
				if (ex != null) {
					var dE = new SodaLauncherErrorDialog(logContent);
					MainWindow.mainWindow.Grid_DialogArea.Children.Add(dE);
				}
				else {
					MessageBox.Show(logContent);
				}
			}
			Trace.WriteLine($"[{DateTime.Now}] [{moduleText}] [{LogInfo}] {logContent}");
		}

		public static void LogStart() {
			var fileNum = GetFileNum();
			SortAsFileCreationTime(ref logFiles);
			if (fileNum == 5) {
				File.Delete(logFiles[4].ToString());
			}
			if (fileNum > 5) {
				for (; fileNum >= 5; fileNum--)
					File.Delete(logFiles[fileNum - 1].ToString());
			}
			try {
				System.Diagnostics.Trace.Listeners.Add(new TextWriterTraceListener($"{LauncherInfo.SODACL_LOG_FOLDER_PATH}\\[{DateTime.Now.Month}.{DateTime.Now.Day}]SodaCL_Log.txt"));
				Trace.AutoFlush = true;
				Trace.WriteLine(" -------- SodaCL 程序日志记录开始 --------");
			}
			catch (Exception ex) {
				Log(true, ModuleList.IO, LogInfo.Error, "SodaCL无法访问Log文件夹，这可能是您打开多个SodaCL实例造成的", ex);
			}
		}

		public static void SortAsFileCreationTime(ref FileInfo[] logFiles) {
			Array.Sort(logFiles, delegate (FileInfo x, FileInfo y) {
				return y.CreationTime.CompareTo(x.CreationTime);
			});
		}
	}
}
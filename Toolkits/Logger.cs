using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Microsoft.AppCenter.Crashes;
using SodaCL.Launcher;

namespace SodaCL.Toolkits
{
    public class Logger
    {
        /// <summary>
        /// 日志级别枚举(不包括Error！)
        /// </summary>
        public enum LogInfo
        {
            Info,
            Warning,
            Debug,
            Error,
        }

        /// <summary>
        /// 模块位置枚举
        /// </summary>
        public enum ModuleList
        {
            Main,
            Animation,
            Network,
            IO,
            Login
        }

        /// <summary>
        /// Log文件夹
        /// </summary>
        public static DirectoryInfo logDir = new(LauncherInfo.sodaCLLogPath);

        /// <summary>
        /// Log目录下的所有文件
        /// </summary>
        public static FileInfo[] logFiles = logDir.GetFiles();

        public static void LogStart()
        {
            var fileNum = GetFileNum();
            SortAsFileCreationTime(ref logFiles);
            if (fileNum == 5)
            {
                File.Delete(logFiles[4].ToString());
            }
            if (fileNum > 5)
            {
                for (; fileNum >= 5; fileNum--)
                    File.Delete(logFiles[fileNum - 1].ToString());
            }
            try
            {
                Trace.Listeners.Add(new TextWriterTraceListener($"{LauncherInfo.sodaCLLogPath}\\[{DateTime.Now.Month}.{DateTime.Now.Day}]SodaCL_Log.txt"));
                Trace.AutoFlush = true;
                Trace.WriteLine(" -------- SodaCL 程序日志记录开始 --------");
            }
            catch (Exception ex)
            {
                Log(true, ModuleList.IO, LogInfo.Error, "SodaCL无法访问Log文件夹，这可能是您打开多个SodaCL实例造成的", ex);
            }
        }

        /// <summary>
        /// 写入非ErrorLog
        /// </summary>
        /// <param name="module">写入Log的模块位置</param>
        /// <param name="LogInfo">Log级别</param>
        /// <param name="message
        /// ">需要写入的Log信息或自定义错误信息</param>
        public static void Log(bool isOpenDialog, ModuleList module, LogInfo LogInfo, string message = null, Exception ex = null)
        {
            var moduleText = "";
            var logContent = "";
            switch (module)
            {
                case ModuleList.Main:
                    moduleText = "Main";
                    break;

                case ModuleList.Animation:
                    moduleText = "Animation";
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
            if (ex != null)
            {
                if (logContent != null)
                {
                    logContent = $"发生错误 {message}";
                }
                else
                {
                    logContent = $"发生错误 {ex.Message}\n{ex.StackTrace}";
                }
                Crashes.TrackError(ex);
            }
            else
            {
                logContent = message;
            }
            if (isOpenDialog)
            {
                MessageBox.Show(logContent);
            }
            Trace.WriteLine($"[{DateTime.Now}] [{moduleText}] [{LogInfo}] {logContent}");
        }

        public static int GetFileNum()
        {
            try
            {
                var fileNum = 0;
                foreach (var f in logDir.GetFiles())
                    fileNum++;
                return fileNum;
            }
            catch (Exception ex)
            {
                Log(true, ModuleList.IO, LogInfo.Error, "Log 文件夹或文件异常", ex);
                throw;
            }
        }

        public static void SortAsFileCreationTime(ref FileInfo[] logFiles)
        {
            Array.Sort(logFiles, delegate (FileInfo x, FileInfo y)
            {
                return y.CreationTime.CompareTo(x.CreationTime);
            });
        }
    }
}
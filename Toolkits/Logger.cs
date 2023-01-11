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
            Debug
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
            int fileNum = GetFileNum();
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
                Log(ModuleList.IO, ex, "SodaCL无法访问Log文件夹，这可能是您打开多个SodaCL实例造成的");
            }
        }

        /// <summary>
        /// 写入非ErrorLog
        /// </summary>
        /// <param name="module">写入Log的模块位置</param>
        /// <param name="LogInfo">Log级别</param>
        /// <param name="logContent">需要写入的Log信息,如果写入为错误信息请直接传入ex.Message</param>
        public static void Log(ModuleList module, LogInfo LogInfo, string logContent)
        {
            string moduleText = "";
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

            Trace.WriteLine($"[{DateTime.Now}] [{moduleText}] [{LogInfo}] {logContent}");
        }

        /// <summary>
        /// 输出普通错误Log
        /// </summary>
        /// <param name="module">模块位置</param>
        /// <param name="ex">错误体</param>
        /// <param name="exMessage">错误信息 请传入ex.Message</param>
        /// <param name="exStack">错误堆栈信息 请传入ex.StackTrace</param>
        public static void Log(ModuleList module, Exception ex, string exMessage, string exStack)
        {
            var moduleText = "";
            var logContent = "出现错误:" + exMessage + "\n" + exStack;
            MessageBox.Show(logContent);
            Crashes.TrackError(ex);

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

            Trace.WriteLine($"[{DateTime.Now}] [{moduleText}] [Error] {logContent}");
        }

        /// <summary>
        /// 输出自定义错误Log
        /// </summary>
        /// <param name="module">模块位置</param>
        /// <param name="ex">错误体</param>
        /// <param name="exContent">自定义错误信息</param>
        public static void Log(ModuleList module, Exception ex, string exContent)
        {
            var moduleText = "";
            var logContent = "出现错误:" + exContent;
            MessageBox.Show(logContent);
            Crashes.TrackError(ex);

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

            Trace.WriteLine($"[{DateTime.Now}] [{moduleText}] [Error] {logContent}");
        }

        public static int GetFileNum()
        {
            try
            {
                int fileNum = 0;
                foreach (FileInfo f in logDir.GetFiles())
                    fileNum++;
                return fileNum;
            }
            catch (Exception ex)
            {
                Log(ModuleList.IO, ex, "Log 文件夹或文件异常");
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
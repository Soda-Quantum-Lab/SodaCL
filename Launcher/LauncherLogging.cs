using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace SodaCL.Launcher
{
    public class LauncherLogging
    {
        /// <summary>
        /// 日志级别枚举
        /// </summary>
        public enum LogInfo
        {
            Info,
            Warning,
            Error,
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
            //不需要目录处理，C#自动处理，别加了。
            try
            {
                Trace.Listeners.Add(new TextWriterTraceListener($"{LauncherInfo.sodaCLLogPath}\\[{DateTime.Now.Month}.{DateTime.Now.Day}]SodaCL_Log.txt"));
                Trace.AutoFlush = true;
                Trace.WriteLine(" -------- SodaCL 程序日志记录开始 --------");
            }
            catch (Exception ex)
            {
                Log(ModuleList.IO, LogInfo.Error, "SodaCL无法访问Log文件夹，这可能是您打开多个SodaCL实例造成的"+ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 写入Log
        /// </summary>
        /// <param name="module">写入Log的模块位置</param>
        /// <param name="LogInfo">Log级别</param>
        /// <param name="logContent">需要写入的Log信息,如果写入为错误信息请直接传入ex.Message</param>
        public static void Log(ModuleList module, LogInfo LogInfo, string logContent, string exStack = "")
        {
            string moduleText = "";
            if (LogInfo == LogInfo.Error)
            {
                logContent = "出现错误:" + logContent + exStack;
                MessageBox.Show(logContent);
            }
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
                Log(ModuleList.IO, LogInfo.Error, "Log 文件夹或文件异常" + ex.Message, ex.StackTrace);
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
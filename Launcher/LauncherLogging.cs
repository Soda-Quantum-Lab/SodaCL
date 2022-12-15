using System;
using System.Diagnostics;
using System.IO;
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
            Error
        }
        /// <summary>
        /// 模块位置枚举
        /// </summary>
        public enum ModuleList
        {
            Main,
            Animation,
            Network,
            IO
        }
        /// <summary>
        /// Log文件夹
        /// </summary>
        public static DirectoryInfo _logDir = new(LauncherInfo._SodaCLLogPath);
        /// <summary>
        /// Log目录下的所有文件
        /// </summary>
        public static FileInfo[] _logFiles = _logDir.GetFiles();
        /// <summary>
        /// 写入Log
        /// </summary>
        /// <param name="_module">写入Log的模块位置</param>
        /// <param name="_LogInfo">Log级别</param>
        /// <param name="_logContent">需要写入的Log信息,如果写入为错误信息请直接传入ex.Message</param>
        public static void Log(ModuleList _module, LogInfo _LogInfo, string _logContent)
        {
            string _moduleText = "";
            if (_LogInfo == LogInfo.Error)
            {
                _logContent = "出现错误:" + _logContent;
            }
            switch (_module)
            {
                case ModuleList.Main:
                    _moduleText = "Main";
                    break;
                case ModuleList.Animation:
                    _moduleText = "Animation";
                    break;
                case ModuleList.Network:
                    _moduleText = "Network";
                    break;
                case ModuleList.IO:
                    _moduleText = "IO";
                    break;
            }

            Trace.WriteLine($"[{DateTime.Now}] [{_moduleText}] [{_LogInfo}] {_logContent}");
        }
        public static int GetFileNum()
        {
            int _fileNum = 0;
            foreach (FileInfo f in _logDir.GetFiles()) _fileNum++;
            return _fileNum;
        }
        public static void SortAsFileCreationTime(ref FileInfo[] _logFiles)
        {
            Array.Sort(_logFiles, delegate (FileInfo x, FileInfo y)
            {
                return y.CreationTime.CompareTo(x.CreationTime);
            });
        }
    }

}


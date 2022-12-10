using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Shapes;
using SodaCL.Launcher;
namespace SodaCL.Launcher
{
    public class LauncherLogging
    {
        /// <summary>
        /// 日志级别枚举
        /// </summary>
        public enum logInfo
        {
            Info,
            Warning,
            Error
        }
        /// <summary>
        /// 模块位置枚举
        /// </summary>
        public enum moduleList
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
        /// <param name="_loginfo">Log级别</param>
        /// <param name="_logContent">需要写入的Log信息,如果写入为错误信息请直接传入ex.Message</param>
        public static void Log(moduleList _module, logInfo _loginfo, string _logContent)
        {
            string _moduleText = "";
            if (_loginfo == logInfo.Error)
            {
                _logContent = "出现错误:" + _logContent;
            }
            switch (_module)
            {
                case moduleList.Main:
                    _moduleText = "Main";
                    break;
                case moduleList.Animation:
                    _moduleText = "Animation";
                    break;
                case moduleList.Network:
                    _moduleText = "Network";
                    break;
                case moduleList.IO:
                    _moduleText = "IO";
                    break;
            }

            string _stringDate = $"{DateTime.Now.Month.ToString()}.{DateTime.Now.Day.ToString()}";
            string _logFilePath = ".\\SodaCL\\[" + _stringDate + "]SodaCL_Log.txt";
            string _logOutput = $"[{DateTime.Now.ToString()}] [{_moduleText}] [{_loginfo}] {_logContent}";
            FileStream fs;
            StreamWriter sw;
            if (File.Exists(_logFilePath))
            {
                fs = new FileStream(_logFilePath, FileMode.Append, FileAccess.Write);
            }
            else
            {
                fs = new FileStream(_logFilePath, FileMode.Create, FileAccess.Write);
            }
            sw = new StreamWriter(fs);
            sw.WriteLine(_logOutput);
            sw.Close();
            fs.Close();
            Trace.WriteLine(_logOutput);

        }
        /// <summary>
        /// 以MM.DD-HHhMMm的格式返回字符串格式的当前时间
        /// </summary>
        /// <returns>返回的字符串格式时间</returns>
        public static string LogTime()
        {
            string _stringTime = $"{DateTime.Now.Month.ToString()}.{DateTime.Now.Day.ToString()}-{DateTime.Now.Hour.ToString()}h{DateTime.Now.Minute.ToString()}m";
            return _stringTime;
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


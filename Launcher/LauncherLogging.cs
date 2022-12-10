using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using SodaCL.Launcher;
namespace SodaCL.Launcher
{
    public class LauncherLogging
    {
        public enum logInfo
        {
            Info,
            Warning,
            Error
        }

        public enum moduleList
        {
            Main,
            Animation,
            Network,
            IO
        }

        public static int _logNum = 0;
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
        /// 以MM.DD-HH_MM的格式返回字符串格式的当前时间
        /// </summary>
        /// <returns>返回的字符串时间</returns>
        public static string LogTime()
        {
            string _stringTime = $"{DateTime.Now.Month.ToString()}.{DateTime.Now.Day.ToString()}-{DateTime.Now.Hour.ToString()}_{DateTime.Now.Minute.ToString()}";
            return _stringTime;
        }
    }
}
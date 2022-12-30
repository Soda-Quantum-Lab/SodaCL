using System;
using System.Diagnostics;

namespace SodaCL.Core.Toolkits
{
    public class Logger
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

        public static void Log(ModuleList module, LogInfo LogInfo, string logContent, string exStack = "")
        {
            string moduleText = "";
            if (LogInfo == LogInfo.Error)
            {
                logContent = "出现错误:" + logContent + exStack;
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
    }
}
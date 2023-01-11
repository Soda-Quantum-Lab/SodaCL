using System;
using static SodaCL.Toolkits.Logger;

namespace SodaCL.Core
{
    internal class JavaFinding
    {
        public static void AutoJavaFinding()
        {
            string javaInJavaHome = Environment.GetEnvironmentVariable("JAVA_HOME");
            string pathContent = Environment.GetEnvironmentVariable("Path");
            Log(ModuleList.IO, LogInfo.Info, "获取到环境变量信息: ");
            Log(ModuleList.IO, LogInfo.Info, javaInJavaHome);
            Log(ModuleList.IO, LogInfo.Info, pathContent);
        }
    }
}

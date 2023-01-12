using System;
using Microsoft.Win32;
using static SodaCL.Toolkits.Logger;

namespace SodaCL.Core.Java
{
    internal class JavaFinding
    {
        // 若 bool 值为 true , 则选择 javaw.exe , 反之则选择 java.exe
        public static void AutoJavaFinding(bool javaOrJavaw)
        {
            string[] javaPath = { "java", "java", "java" };
            string javaExeName = null;
            if (javaOrJavaw)
            {
                javaExeName = "javaw.exe";
            }
            else
            {
                javaExeName = "java.exe";
            }
            // 从环境变量获取 Java 路径
            try
            {
                string javahomeContent = Environment.GetEnvironmentVariable("JAVA_HOME");
                string pathContent = Environment.GetEnvironmentVariable("Path");
                Log(ModuleList.IO, LogInfo.Info, "获取到环境变量信息: ");
                Log(ModuleList.IO, LogInfo.Info, "JAVA_HOME: " + javahomeContent);
                Log(ModuleList.IO, LogInfo.Info, "Path: " + pathContent);
                javaPath[0] = javahomeContent + "bin\\" + javaExeName;
                Log(ModuleList.IO, LogInfo.Debug, "Java 列表数组第一位: " + javaPath[0]);
            }
            catch (Exception)
            {
                Log(ModuleList.IO, LogInfo.Warning, "在执行自动查找 Java (从环境变量) 时发生错误");
                throw;
            }

            // 从注册表获取 Java 路径
            string strKeyName = "JavaHome";
            string jdkRegPath = @"SOFTWARE\JavaSoft\Java Development Kit";
            string jreRegPath = @"SOFTWARE\JavaSoft\Java Runtime Environment";
            RegistryKey regKey = Registry.LocalMachine;
            RegistryKey regSubKey = regKey.OpenSubKey(jdkRegPath, false);

            //try
            //{
            //    object javaRegPath = regSubKey.GetValue(strKeyName);
            //    Log(ModuleList.IO, LogInfo.Info, "获取到注册表内的 Java 信息: ");
            //    Log(ModuleList.IO, LogInfo.Info, javaRegPath.ToString());
            //}
            //catch (Exception)
            //{
            //    Log(ModuleList.IO, LogInfo.Warning, "在执行自动查找 Java (从注册表) 时发生错误");
            //    throw;
            //}

            //RegistryValueKind regValueKind = regSubKey.GetValueKind(strKeyName);
            //if (regValueKind == RegistryValueKind.String)
            //{
            //    Log(ModuleList.IO, LogInfo.Debug, javaPath.ToString());
            //}

        }
    }
}

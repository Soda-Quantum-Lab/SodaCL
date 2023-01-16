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
            string[] javaPath = { "null", "null", "null", "null", "null", "null", "null" };
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
                var javahomeContent = Environment.GetEnvironmentVariable("JAVA_HOME");
                var pathContent = Environment.GetEnvironmentVariable("Path");
                Log(false, ModuleList.IO, LogInfo.Info, "获取到环境变量信息: ");
                Log(false, ModuleList.IO, LogInfo.Info, "JAVA_HOME: " + javahomeContent);
                Log(false, ModuleList.IO, LogInfo.Info, "Path: " + pathContent);
                javaPath[0] = javahomeContent + "bin\\" + javaExeName;
                Log(false, ModuleList.IO, LogInfo.Debug, "Java 列表数组第一位: " + javaPath[0]);
            }
            catch (Exception)
            {
                Log(false, ModuleList.IO, LogInfo.Warning, "在执行自动查找 Java (从环境变量) 时发生错误");
                throw;
            }

            // 从注册表获取 Java 路径
            var strKeyName = "JavaHome";
            var jdkRegPath = @"SOFTWARE\JavaSoft\Java Development Kit\";
            var jreRegPath = @"SOFTWARE\JavaSoft\Java Runtime Environment\";
            var javaVersion8 = "1.8";
            var javaVersion11 = "1.11";
            var javaVersion17 = "1.17";
            var regKey = Registry.LocalMachine;

            try
            {
                var regSubKey8 = regKey.OpenSubKey(jdkRegPath + javaVersion8, false);
                if (regSubKey8 == null)
                {
                    Log(false, ModuleList.IO, LogInfo.Warning, "JDK 8 不存在 (从注册表) ");
                }
                else
                {
                    var javaRegPath = regSubKey8.GetValue(strKeyName);
                    Log(false, ModuleList.IO, LogInfo.Info, "获取到注册表内的 JDK 8 位置信息: ");
                    Log(false, ModuleList.IO, LogInfo.Info, javaRegPath.ToString());
                    var javaExePath = javaRegPath.ToString() + "bin\\" + javaExeName;
                    javaPath[1] = javaExePath;
                }
            }
            catch (Exception ex)
            {
                Log(false, ModuleList.IO, LogInfo.Warning, "在执行自动查找 JDK 8 (从注册表) 时发生错误: ");
                Log(false, ModuleList.IO, LogInfo.Warning, ex.ToString());
            }

            try
            {
                var regSubKey11 = regKey.OpenSubKey(jdkRegPath + javaVersion11, false);
                if (regSubKey11 == null)
                {
                    Log(false, ModuleList.IO, LogInfo.Warning, "JDK 11 不存在 (从注册表) ");
                }
                else
                {
                    var javaRegPath = regSubKey11.GetValue(strKeyName);
                    Log(false, ModuleList.IO, LogInfo.Info, "获取到注册表内的 JDK 11 位置信息: ");
                    Log(false, ModuleList.IO, LogInfo.Info, javaRegPath.ToString());
                    var javaExePath = javaRegPath.ToString() + "bin\\" + javaExeName;
                    javaPath[2] = javaExePath;
                }
            }
            catch (Exception ex)
            {
                Log(false, ModuleList.IO, LogInfo.Warning, "在执行自动查找 JDK 11 (从注册表) 时发生错误: ");
                Log(false, ModuleList.IO, LogInfo.Warning, ex.ToString());
            }

            try
            {
                var regSubKey17 = regKey.OpenSubKey(jdkRegPath + javaVersion17, false);
                if (regSubKey17 == null)
                {
                    Log(false, ModuleList.IO, LogInfo.Warning, "JDK 17 不存在 (从注册表) ");
                }
                else
                {
                    var javaRegPath = regSubKey17.GetValue(strKeyName);
                    Log(false, ModuleList.IO, LogInfo.Info, "获取到注册表内的 JDK 17 位置信息: ");
                    Log(false, ModuleList.IO, LogInfo.Info, javaRegPath.ToString());
                    var javaExePath = javaRegPath.ToString() + "bin\\" + javaExeName;
                    javaPath[3] = javaExePath;
                }
            }
            catch (Exception ex)
            {
                Log(false, ModuleList.IO, LogInfo.Warning, "在执行自动查找 JDK 17 (从注册表) 时发生错误: ");
                Log(false, ModuleList.IO, LogInfo.Warning, ex.ToString());
            }

            try
            {
                var regSubKey8 = regKey.OpenSubKey(jreRegPath + javaVersion8, false);
                if (regSubKey8 == null)
                {
                    Log(false, ModuleList.IO, LogInfo.Warning, "JRE 8 不存在 (从注册表) ");
                }
                else
                {
                    var javaRegPath = regSubKey8.GetValue(strKeyName);
                    Log(false, ModuleList.IO, LogInfo.Info, "获取到注册表内的 JRE 8 位置信息: ");
                    Log(false, ModuleList.IO, LogInfo.Info, javaRegPath.ToString());
                    var javaExePath = javaRegPath.ToString() + "bin\\" + javaExeName;
                    javaPath[4] = javaExePath;
                }
            }
            catch (Exception ex)
            {
                Log(false, ModuleList.IO, LogInfo.Warning, "在执行自动查找 JRE 8 (从注册表) 时发生错误: ");
                Log(false, ModuleList.IO, LogInfo.Warning, ex.ToString());
            }

            try
            {
                var regSubKey11 = regKey.OpenSubKey(jreRegPath + javaVersion11, false);
                if (regSubKey11 == null)
                {
                    Log(false, ModuleList.IO, LogInfo.Warning, "JRE 11 不存在 (从注册表) ");
                }
                else
                {
                    var javaRegPath = regSubKey11.GetValue(strKeyName);
                    Log(false, ModuleList.IO, LogInfo.Info, "获取到注册表内的 JRE 11 位置信息: ");
                    Log(false, ModuleList.IO, LogInfo.Info, javaRegPath.ToString());
                    var javaExePath = javaRegPath.ToString() + "bin\\" + javaExeName;
                    javaPath[5] = javaExePath;
                }
            }
            catch (Exception ex)
            {
                Log(false, ModuleList.IO, LogInfo.Warning, "在执行自动查找 JRE 11 (从注册表) 时发生错误: ");
                Log(false, ModuleList.IO, LogInfo.Warning, ex.ToString());
            }

            try
            {
                var regSubKey17 = regKey.OpenSubKey(jreRegPath + javaVersion17, false);
                if (regSubKey17 == null)
                {
                    Log(false, ModuleList.IO, LogInfo.Warning, "JRE 17 不存在 (从注册表) ");
                }
                else
                {
                    var javaRegPath = regSubKey17.GetValue(strKeyName);
                    Log(false, ModuleList.IO, LogInfo.Info, "获取到注册表内的 JRE 17 位置信息: ");
                    Log(false, ModuleList.IO, LogInfo.Info, javaRegPath.ToString());
                    var javaExePath = javaRegPath.ToString() + "bin\\" + javaExeName;
                    javaPath[6] = javaExePath;
                }
            }
            catch (Exception ex)
            {
                Log(false, ModuleList.IO, LogInfo.Warning, "在执行自动查找 JRE 17 (从注册表) 时发生错误: ");
                Log(false, ModuleList.IO, LogInfo.Warning, ex.ToString());
            }

            Log(false, ModuleList.IO, LogInfo.Info, "获取到 Java 列表: ");
            for (var i = 0; i < javaPath.Length; i++)
            {
                if (javaPath[i] == null)
                {
                }
                else
                {
                    Log(false, ModuleList.IO, LogInfo.Info, javaPath[i]);
                }
            }
        }
    }
}
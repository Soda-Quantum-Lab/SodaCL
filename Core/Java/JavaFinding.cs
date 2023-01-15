using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Documents;
using Microsoft.Win32;
using static SodaCL.Toolkits.Logger;

namespace SodaCL.Core.Java
{
    internal class JavaFinding
    {
        // 若 bool 值为 true , 则选择 javaw.exe , 反之则选择 java.exe
        public static Array AutoJavaFinding(bool javaOrJavaw)
        {
            List<string> javaPath = new List<string>(100);
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
                Log(false, ModuleList.IO, LogInfo.Info, "获取到环境变量信息: ");
                Log(false, ModuleList.IO, LogInfo.Info, "JAVA_HOME: " + javahomeContent);
                Log(false, ModuleList.IO, LogInfo.Info, "Path: " + pathContent);
                string javaExePath = javahomeContent + "bin\\" + javaExeName;
                javaPath.Add(javaExePath);
            }
            catch (Exception)
            {
                Log(false, ModuleList.IO, LogInfo.Warning, "在执行自动查找 Java (从环境变量) 时发生错误");
                throw;
            }

            Log(false, ModuleList.IO, LogInfo.Info, "--------------------------------");

            // 从注册表获取 Java 路径
            string strKeyName = "JavaHome";
            string jdkRegPath = @"SOFTWARE\JavaSoft\Java Development Kit\";
            string jreRegPath = @"SOFTWARE\JavaSoft\Java Runtime Environment\";
            string javaVersion8 = "1.8";
            string javaVersion11 = "1.11";
            string javaVersion17 = "1.17";
            RegistryKey regKey = Registry.LocalMachine;

            try
            {
                RegistryKey regSubKey8 = regKey.OpenSubKey(jdkRegPath + javaVersion8, false);
                if (regSubKey8 == null)
                {
                    Log(false, ModuleList.IO, LogInfo.Warning, "JDK 8 不存在 (从注册表) ");
                }
                else
                {
                    object javaRegPath = regSubKey8.GetValue(strKeyName);
                    Log(false, ModuleList.IO, LogInfo.Info, "获取到注册表内的 JDK 8 位置信息: " + javaRegPath.ToString());
                    string javaExePath = javaRegPath.ToString() + "bin\\" + javaExeName;
                    javaPath.Add(javaExePath);
                }
            }
            catch (Exception ex)
            {
                Log(false, ModuleList.IO, LogInfo.Warning, "在执行自动查找 JDK 8 (从注册表) 时发生错误: ");
                Log(false, ModuleList.IO, LogInfo.Warning, ex.ToString());
            }

            try
            {
                RegistryKey regSubKey11 = regKey.OpenSubKey(jdkRegPath + javaVersion11, false);
                if (regSubKey11 == null)
                {
                    Log(false, ModuleList.IO, LogInfo.Warning, "JDK 11 不存在 (从注册表) ");
                }
                else
                {
                    object javaRegPath = regSubKey11.GetValue(strKeyName);
                    Log(false, ModuleList.IO, LogInfo.Info, "获取到注册表内的 JDK 11 位置信息: " + javaRegPath.ToString());
                    string javaExePath = javaRegPath.ToString() + "bin\\" + javaExeName;
                    javaPath.Add(javaExePath);
                }
            }
            catch (Exception ex)
            {
                Log(false, ModuleList.IO, LogInfo.Warning, "在执行自动查找 JDK 11 (从注册表) 时发生错误: ");
                Log(false, ModuleList.IO, LogInfo.Warning, ex.ToString());
            }

            try
            {
                RegistryKey regSubKey17 = regKey.OpenSubKey(jdkRegPath + javaVersion17, false);
                if (regSubKey17 == null)
                {
                    Log(false, ModuleList.IO, LogInfo.Warning, "JDK 17 不存在 (从注册表) ");
                }
                else
                {
                    object javaRegPath = regSubKey17.GetValue(strKeyName);
                    Log(false, ModuleList.IO, LogInfo.Info, "获取到注册表内的 JDK 17 位置信息: " + javaRegPath.ToString());
                    string javaExePath = javaRegPath.ToString() + "bin\\" + javaExeName;
                    javaPath.Add(javaExePath);
                }
            }
            catch (Exception ex)
            {
                Log(false, ModuleList.IO, LogInfo.Warning, "在执行自动查找 JDK 17 (从注册表) 时发生错误: ");
                Log(false, ModuleList.IO, LogInfo.Warning, ex.ToString());
            }

            try
            {
                RegistryKey regSubKey8 = regKey.OpenSubKey(jreRegPath + javaVersion8, false);
                if (regSubKey8 == null)
                {
                    Log(false, ModuleList.IO, LogInfo.Warning, "JRE 8 不存在 (从注册表) ");
                }
                else
                {
                    object javaRegPath = regSubKey8.GetValue(strKeyName);
                    Log(false, ModuleList.IO, LogInfo.Info, "获取到注册表内的 JRE 8 位置信息: " + javaRegPath.ToString());
                    string javaExePath = javaRegPath.ToString() + "bin\\" + javaExeName;
                    javaPath.Add(javaExePath);
                }
            }
            catch (Exception ex)
            {
                Log(false, ModuleList.IO, LogInfo.Warning, "在执行自动查找 JRE 8 (从注册表) 时发生错误: ");
                Log(false, ModuleList.IO, LogInfo.Warning, ex.ToString());
            }

            try
            {
                RegistryKey regSubKey11 = regKey.OpenSubKey(jreRegPath + javaVersion11, false);
                if (regSubKey11 == null)
                {
                    Log(false, ModuleList.IO, LogInfo.Warning, "JRE 11 不存在 (从注册表) ");
                }
                else
                {
                    object javaRegPath = regSubKey11.GetValue(strKeyName);
                    Log(false, ModuleList.IO, LogInfo.Info, "获取到注册表内的 JRE 11 位置信息: " + javaRegPath.ToString());
                    string javaExePath = javaRegPath.ToString() + "bin\\" + javaExeName;
                    javaPath.Add(javaExePath);
                }
            }
            catch (Exception ex)
            {
                Log(false, ModuleList.IO, LogInfo.Warning, "在执行自动查找 JRE 11 (从注册表) 时发生错误: ");
                Log(false, ModuleList.IO, LogInfo.Warning, ex.ToString());
            }

            try
            {
                RegistryKey regSubKey17 = regKey.OpenSubKey(jreRegPath + javaVersion17, false);
                if (regSubKey17 == null)
                {
                    Log(false, ModuleList.IO, LogInfo.Warning, "JRE 17 不存在 (从注册表) ");
                }
                else
                {
                    object javaRegPath = regSubKey17.GetValue(strKeyName);
                    Log(false, ModuleList.IO, LogInfo.Info, "获取到注册表内的 JRE 17 位置信息: " + javaRegPath.ToString());
                    string javaExePath = javaRegPath.ToString() + "bin\\" + javaExeName;
                    javaPath.Add(javaExePath);
                }
            }
            catch (Exception ex)
            {
                Log(false, ModuleList.IO, LogInfo.Warning, "在执行自动查找 JRE 17 (从注册表) 时发生错误: ");
                Log(false, ModuleList.IO, LogInfo.Warning, ex.ToString());
            }

            Log(false, ModuleList.IO, LogInfo.Info, "--------------------------------");
            Log(false, ModuleList.IO, LogInfo.Info, "获取到 Java 列表: ");
            javaPath.ForEach(java => Log(false, ModuleList.IO, LogInfo.Info, java.ToString()));
            return javaPath.ToArray();
        }
    }
}
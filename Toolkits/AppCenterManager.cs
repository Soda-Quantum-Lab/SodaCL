using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using SodaCL.Launcher;
using static SodaCL.Toolkits.GetResources;
using static SodaCL.Toolkits.Logger;

namespace SodaCL.Toolkits
{
    public static class AppCenterManager
    {
        public static void StartAppCenter()
        {
            try
            {
                Log(false, ModuleList.Main, LogInfo.Info, "正在释放AppCenter依赖文件");
                var arch = Environment.Is64BitOperatingSystem ? "x64" : "x86";
                var acRuntimeDir = $@"{LauncherInfo.appDataDir}\Runtime\AppCenter";
                var acRuntimeFiles = new List<string> { $"{arch}\\e_sqlite3.dll", "SQLite-net.dll", "SQLitePCLRaw.batteries_green.dll", "SQLitePCLRaw.batteries_v2.dll", "SQLitePCLRaw.core.dll", "SQLitePCLRaw.provider.e_sqlite3.dll" };
                var assm = Assembly.GetExecutingAssembly();
                var appName = Assembly.GetExecutingAssembly().GetName().Name.ToString();
                if (!Directory.Exists(acRuntimeDir))
                    Directory.CreateDirectory(acRuntimeDir);
                if (!Directory.Exists($@"{acRuntimeDir}\{arch}"))
                    Directory.CreateDirectory($@"{acRuntimeDir}\{arch}");
                foreach (var file in acRuntimeFiles)
                {
                    if (!File.Exists(acRuntimeDir + "\\" + file))
                    {
                        using (Stream sm = assm.GetManifestResourceStream($@"{appName}.Resources.AppCenter.{file.Replace("\\", ".")}"))
                        {
                            var buffer = new byte[sm.Length];
                            using (var fs = new FileStream(acRuntimeDir + "\\" + file, FileMode.Create))
                            {
                                sm.Read(buffer, 0, buffer.Length);
                                using (BinaryWriter bw = new BinaryWriter(fs))
                                {
                                    bw.Write(buffer, 0, buffer.Length);
                                    bw.Close();
                                }
                            }
                        }
                    }
                    if (!file.Contains("x86") && !file.Contains("x64"))
                    {
                        Assembly.LoadFile(acRuntimeDir + "\\" + file);
                        Log(false, ModuleList.Main, LogInfo.Info, $"成功加载AppCenter依赖文件 {file}");
                    }
                }
            }
            catch (Exception ex)
            {
                Log(true, ModuleList.Main, LogInfo.Error, ex: ex);
            }
            AppCenter.SetEnabledAsync(true);
            AppCenter.Start(GetText("AppCenterToken"), typeof(Analytics), typeof(Crashes));
            AppCenter.SetCountryCode(RegionInfo.CurrentRegion.TwoLetterISORegionName);
        }
    }
}
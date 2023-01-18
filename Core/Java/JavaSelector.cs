using Microsoft.AppCenter.Ingestion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SodaCL.Core.Java.JavaFinding;
using static SodaCL.Toolkits.Logger;

namespace SodaCL.Core.Java
{
    internal class JavaSelector
    {
        // 传入 Assets Index 版本即可
        public static Array SelectJavaByVersion(int gameVersion)
        {
            string[] javaPathArray = (string[])AutoJavaFinding(true);
            if (javaPathArray == null)
            {
                Log(true, ModuleList.IO, LogInfo.Info, "SodaCL 找不到 Java 。\n请检查你的计算机上是否安装了 Java 。");
                return null;
            }
            else
            {
                if (gameVersion < 1.17)
                {
                    if (gameVersion == 1.16)
                    {
                        IEnumerable<string> javaIsCompatiable11 = from java in javaPathArray
                                                                where java.Contains("11")
                                                                select java;
                        if (javaIsCompatiable11 == null)
                        {
                            IEnumerable<string> javaIsCompatiable8 = from java in javaPathArray
                                                                    where java.Contains("8")
                                                                    select java;
                            if (javaIsCompatiable8 == null)
                            {
                                Log(true, ModuleList.IO, LogInfo.Info, "SodaCL 找不到符合条件的 Java 。");
                                return null;
                            }
                            else
                            {
                                string[] javaIsCompatiableArray = javaIsCompatiable8.ToArray();
                                Log(false, ModuleList.IO, LogInfo.Info, "SodaCL 找到了以下符合核心版本要求的 Java : ");
                                for (int i = 0; i < javaIsCompatiableArray.Length; i++)
                                {
                                    Log(false, ModuleList.IO, LogInfo.Info, javaIsCompatiableArray[i]);
                                }
                                return javaIsCompatiableArray;
                            }
                        }
                        else
                        {
                            string[] javaIsCompatiableArray = javaIsCompatiable11.ToArray();
                            Log(false, ModuleList.IO, LogInfo.Info, "SodaCL 找到了以下符合核心版本要求的 Java : ");
                            for (int i = 0; i < javaIsCompatiableArray.Length; i++)
                            {
                                Log(false, ModuleList.IO, LogInfo.Info, javaIsCompatiableArray[i]);
                            }
                            return javaIsCompatiableArray;
                        }
                    }
                    else
                    {
                        IEnumerable<string> javaIsCompatiable = from java in javaPathArray
                                                                where java.Contains("8")
                                                                select java;
                        string[] javaIsCompatiableArray = javaIsCompatiable.ToArray();
                        Log(false, ModuleList.IO, LogInfo.Info, "SodaCL 找到了以下符合核心版本要求的 Java : ");
                        for (int i = 0; i < javaIsCompatiableArray.Length; i++)
                        {
                            Log(false, ModuleList.IO, LogInfo.Info, javaIsCompatiableArray[i]);
                        }
                        return javaIsCompatiableArray;
                    }
                }
                else
                {
                    IEnumerable<string> javaIsCompatiable17 = from java in javaPathArray
                                                            where java.Contains("17")
                                                            select java;
                    if (javaIsCompatiable17 == null)
                    {
                        Log(true, ModuleList.IO, LogInfo.Info, "SodaCL 找不到符合条件的 Java 。");
                        return null;
                    }
                    string[] javaIsCompatiableArray = (string[])javaIsCompatiable17;
                    Log(false, ModuleList.IO, LogInfo.Info, "SodaCL 找到了以下符合核心版本要求的 Java : ");
                    for (int i = 0; i < javaIsCompatiableArray.Length; i++)
                    {
                        Log(false, ModuleList.IO, LogInfo.Info, javaIsCompatiableArray[i]);
                    }
                    return javaIsCompatiableArray;
                }
            }
        }
    }
}

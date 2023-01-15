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
        public static Array SelectJavaByVersion(int gameVersion)
        {
            string[] javaPathArray = (string[])AutoJavaFinding(true);
            if (gameVersion < 1.17)
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
            else
            {
                IEnumerable<string> javaIsCompatiable = from java in javaPathArray
                                                        where java.Contains("17")
                                                        select java;
                string[] javaIsCompatiableArray = (string[])javaIsCompatiable;
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

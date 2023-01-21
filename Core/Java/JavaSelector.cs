using System.Collections.Generic;
using System.Linq;
using static SodaCL.Toolkits.Logger;

namespace SodaCL.Core.Java
{
	internal class JavaSelector
	{
		// 传入 Assets Index 版本即可
		public static List<JavaModel> SelectJavaByVersion(List<JavaModel> javaList, int gameVersion)
		{
			if (gameVersion < 1.17)
			{
				if (gameVersion == 1.16)
				{
					var javaIsCompatiable11 = from java in javaList
											  where java.Version == 11
											  select java;
					if (javaIsCompatiable11 == null)
					{
						var javaIsCompatiable8 = from java in javaList
												 where java.Version == 8
												 select java;
						if (javaIsCompatiable8 == null)
						{
							Log(true, ModuleList.IO, LogInfo.Info, "SodaCL 找不到符合条件的 Java 。");
							return null;
						}
						else
						{
							Log(false, ModuleList.IO, LogInfo.Info, "SodaCL 找到了以下符合核心版本要求的 Java : ");
							foreach (var java in javaIsCompatiable8)
							{
								Log(false, ModuleList.IO, LogInfo.Info, java.Path);
							}
							return javaIsCompatiable8.ToList<JavaModel>();
						}
					}
					else
					{
						Log(false, ModuleList.IO, LogInfo.Info, "SodaCL 找到了以下符合核心版本要求的 Java : ");
						foreach (var java in javaIsCompatiable11)
						{
							Log(false, ModuleList.IO, LogInfo.Info, java.Path);
						}
						return javaIsCompatiable11.ToList<JavaModel>();
					}
				}
				else
				{
					var javaIsCompatiable = from java in javaList
											where java.Version == 8
											select java;
					Log(false, ModuleList.IO, LogInfo.Info, "SodaCL 找到了以下符合核心版本要求的 Java : ");
					foreach (var java in javaIsCompatiable)
					{
						Log(false, ModuleList.IO, LogInfo.Info, java.Path);
					}
					return javaIsCompatiable.ToList<JavaModel>();
				}
			}
			else
			{
				var javaIsCompatiable17 = from java in javaList
										  where java.Version == 8
										  select java;
				if (javaIsCompatiable17 == null)
				{
					Log(true, ModuleList.IO, LogInfo.Info, "SodaCL 找不到符合条件的 Java 。");
					return null;
				}
				Log(false, ModuleList.IO, LogInfo.Info, "SodaCL 找到了以下符合核心版本要求的 Java : ");
				foreach (var java in javaIsCompatiable17)
				{
					Log(false, ModuleList.IO, LogInfo.Info, java.Path);
				}
				return javaIsCompatiable17.ToList<JavaModel>();
			}
		}
	}
}
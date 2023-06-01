using Microsoft.Win32;
using Newtonsoft.Json;
using SodaCL.Toolkits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaCL.Core.Java
{
	class JavaSelectClass
	{
		//TODO
		public string javaSelected = null;
		public static void JavaSelector(bool IsAuto, double TargetMinecraftVersion)
		{
			var javaListJson = RegEditor.GetKeyValue(Registry.CurrentUser, "CacheJavaList");
			var javaList = JsonConvert.DeserializeObject(javaListJson);
			Logger.Log(false, Logger.ModuleList.IO, Logger.LogInfo.Info, javaList.ToString());

			//if (IsAuto)
			//{
			//	if (TargetMinecraftVersion >= 1.17)
			//	{
			//		foreach (var java in javaList)
			//		{
			//			if (java.Version.Contains("17"))
			//			{
			//				RegEditor.SetKeyValue(Registry.CurrentUser, @"Software\SodaCL", "CacheTargetJava", java.JavaPath, RegistryValueKind.String);
			//			}
			//		}
			//	}
			//	else
			//	{
			//		foreach (var java in javaList)
			//		{
			//			if (java.Version.Contains("8"))
			//			{
			//				RegEditor.SetKeyValue(Registry.CurrentUser, @"Software\SodaCL", "CacheTargetJava", java.JavaPath, RegistryValueKind.String);
			//			}
			//		}
			//	}
			//}
			//else
			//{
				
			//}
		}
	}
}

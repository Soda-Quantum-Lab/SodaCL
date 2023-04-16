using System.Collections.Generic;
using System.Reflection;
using Microsoft.Win32;
using Newtonsoft.Json;
using SodaCL.Core.Java;
using SodaCL.Toolkits;
using static SodaCL.Toolkits.DeviceInfo;

namespace SodaCL.Core.Game
{
	public class MinecraftLaunch
	{
		private MinecraftLaunch()
		{ }

		private static List<string> LaunchArguments { get; set; }

		public static void LaunchGame()
		{
			SpliceArgumentsMain();
		}

		public static void SpliceArgumentsMain()
		{
			SpliceBasicArguments();
		}

		public static string SpliceBasicArguments()
		{
			//TODO:JavaPath
			var javaPathJson = RegEditor.GetKeyValue(Registry.CurrentUser, "JavaList");
			var javaPath = JsonConvert.DeserializeObject<JavaModel>(javaPathJson);

			//LaunchArguments.Add(javaPath[0].ToString());
			LaunchArguments.Add($" -Xmx2048");
			LaunchArguments.Add($" -Xmx2048");
			LaunchArguments.Add($" -Xmn256m");
			LaunchArguments.Add(" -XX:+UseG1GC");
			LaunchArguments.Add(" -XX:-UseAdaptiveSizePolicy");
			LaunchArguments.Add(" -XX:-OmitStackTraceInFastThrow");
			LaunchArguments.Add($" -Dos.name=Windows {GetOsMajorNumber()} ");
			LaunchArguments.Add($" -Dminecraft.launcher.brand=SodaCL");
			LaunchArguments.Add($" -Dminecraft.launcher.version={Assembly.GetExecutingAssembly().GetName().Version.ToString()}");
			return DataTool.SplitListAndToString(LaunchArguments, " ");
		}
	}
}
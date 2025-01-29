using SodaCL.Launcher;
using System.IO;
using static SodaCL.Toolkits.Logger;

namespace SodaCL.Core.Game {

	public class MinecraftVersion {

		public static string[] GetVersionList() {
			// 留着，以后有用
			//DirectoryInfo mcVersionsFolder = new DirectoryInfo(SodaCL.Launcher.LauncherInfo.mcVersionsDir);
			//DirectoryInfo[] mcVersionsArray = mcVersionsFolder.GetDirectories();
			//int[] mcVersionsArrayLength = new int[] { 1, 2, 3 };
			//foreach (var item in mcVersionsArray)
			//{
			//    string versionFolderPath = item.ToString;
			//    string versionFolderName = Path.GetFileName(versionFolderPath);
			//    Log(ModuleList.IO, LogInfo.Info, item.ToString());
			//    Console.WriteLine(item.ToString());
			//}
			if (Directory.Exists(LauncherInfo.SODA_MC_VERSIONS_DIR)) {
				var dir = Directory.GetDirectories(LauncherInfo.SODA_MC_VERSIONS_DIR);
				var names = new string[dir.Length];
				Log(false, ModuleList.IO, LogInfo.Info, "查找到 versions 文件夹内核心文件夹: ");
				for (var i = 0; i < dir.Length; i++) {
					names[i] = Path.GetFileName(dir[i]);
					Log(false, ModuleList.IO, LogInfo.Info, names[i]);
				}
				return names;
			}
			else {
				Log(true, ModuleList.IO, LogInfo.Warning, "versions 目录不存在, 可能是初始化阶段出现了异常导致 versions 文件夹未成功创建");
				return null;
			}
		}
	}
}
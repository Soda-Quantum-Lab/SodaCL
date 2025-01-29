using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SodaCL.Launcher.LauncherInfo;

namespace SodaCL.Toolkits {

	/// <summary>
	/// 写入 SodaCL 配置
	/// </summary>
	public class Setting {
		/// <summary>
		/// Ð´Èë配置
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		//public static void WriteSetting(string key, string value) {
		//	var settings = JsonConvert.DeserializeObject<>(File.ReadAllText(SODACL_SETTINGS));
		//	if (File.Exists(SODACL_SETTINGS) || File.ReadAllText(SODACL_SETTINGS) == null) {
		//		File.Create(SODACL_SETTINGS);
		//		File.WriteAllLines(SODACL_SETTINGS, null);
		//	}
		//}
	}
}
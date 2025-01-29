using Newtonsoft.Json;
using SodaCL.Controls.Dialogs;
using SodaCL.Models.Launcher.Toolkits;
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
		/// ð´èë配置
		/// </summary>

		/// <private param name = "key" ></ param >
		/// < param name="value"></param>

		public static void WriteSetting(string key, string value) {
			try {
				var settings = JsonConvert.DeserializeObject<SodaSetting>(File.ReadAllText(SODACL_SETTINGS));
				if (settings == null) {
					throw new Exception();
				}
			}
			catch {
				var dE = new SodaDialog(SodaDialog.DialogType.Error, "读取配置文件出错，程序自动退出。");
				dE.CloseEvent += () => {
					App.Current.Shutdown();
					return null;
				};
			}
		}
	}
}
using SodaCL.Controls.Dialogs;
using SodaCL.Models.Launcher.Toolkits;
using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SodaCL.Launcher.LauncherInfo;
using SodaCL.Launcher;
using System.Diagnostics;

namespace SodaCL.Toolkits {

	/// <summary>
	/// 写入 SodaCL 配置
	/// </summary>
	public class Setting {

		/// <summary>
		/// 写入配置
		/// </summary>
		///
		/// <private param name = "key" ></ param >
		/// < param name="value"></param>
		public static void SettingInit() {
			try {
				DataTool.ExtractFile("SodaCL.Resources.DefaultSetting.xml", SODACL_SETTINGS);
				var xml = new XmlTool(SODACL_SETTINGS);
				xml.CreateNode("Settings");
			}
			catch {
				throw;
			}
		}

		public static void WriteSetting(string key, string value) {
			try {
				var xml = new XmlTool(SODACL_SETTINGS);
				xml.SetNodeValue(key, value);
			}
			catch (Exception ex) {
				SettingInit();
				var dE = new SodaDialog(SodaDialog.DialogType.Error, null, null, $"读取配置文件出错，程序自动退出。\r错误信息：{ex}");
				dE.CloseEvent += () => {
					App.Current.Shutdown();
					return null;
				};
			}
		}
	}
}
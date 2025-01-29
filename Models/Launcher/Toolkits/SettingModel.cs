using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SodaCL.Models.Launcher.Toolkits {

	/// <summary>
	/// SodaCL 配置文件
	/// </summary>
	public class SettingModel {

		[JsonProperty("sodaSetting")]
		public SodaSetting SodaSetting { get; set; }

		[JsonProperty("gameInfo")]
		public GameInfo GameInfo { get; set; }
	}

	public class SodaSetting {
	}

	public class GameInfo {
	}
}
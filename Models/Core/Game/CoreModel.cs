﻿using Newtonsoft.Json;

namespace SodaCL.Models.Core.Game {

	public class CoreModel {

		[JsonProperty("version")]
		public string Version { get; set; }

		[JsonProperty("majorVersion")]
		public int MajorVersion { get; set; }

		[JsonProperty("versionName")]
		public string VersionName { get; set; }

		[JsonProperty("gameDir")]
		public string GameDir { get; set; }

		[JsonProperty("isForgeInstalled")]
		public bool IsForgeInstalled { get; set; }

		[JsonProperty("isOptiFineInstalled")]
		public bool IsOptifineInstalled { get; set; }

		[JsonProperty("isQuiltInstalled")]
		public bool IsQuiltInstalled { get; set; }

		[JsonProperty("isFabricInstalled")]
		public bool IsFabricInstalled { get; set; }
	}
}
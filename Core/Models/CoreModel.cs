﻿using Newtonsoft.Json;

namespace SodaCL.Core.Models
{
	public class CoreModel
	{
		[JsonProperty("version")]
		public string Version { get; set; }

		[JsonProperty("gameDir")]
		public string GameDir { get; set; }

		[JsonProperty("isForgeInstalled")]
		public bool IsForgeInstalled { get; set; }

		[JsonProperty("isOptifineInstalled")]
		public bool IsOptifineInstalled { get; set; }

		[JsonProperty("isQuiltInstalled")]
		public bool IsQuiltInstalled { get; set; }

		[JsonProperty("isFabricInstalled")]
		public bool IsFabricInstalled { get; set; }
	}
}
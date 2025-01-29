using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace SodaCL.Models.Core.Game {

	public class AssetModel {

		//Minecraft 资源信息
		[JsonProperty("assetIndex")]
		public AssetIndexModel AssetIndex { get; set; }

		//Minecraft 版本号
		[JsonProperty("assets")]
		public string Assets { get; set; }

		[JsonProperty("complianceLevel")]
		public short ComplianceLevel { get; set; }

		//下载文件的信息
		[JsonProperty("downloads")]
		public AssetDownladModel Downloads { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("javaVersion")]
		public AssetJavaVersionModel JavaVersion { get; set; }

		[JsonProperty("libraries")]
		public AssetLibraryModel[] Libraries { get; set; }

		[JsonProperty("logging")]
		public AssetLoggingModel Logging { get; set; }

		[JsonProperty("mainClass")]
		public string MainClass { get; set; }

		[JsonProperty("minecraftArguments")]
		public string MinecraftArguments { get; set; }

		[JsonProperty("minimumLauncherVersion")]
		public short MinimumLauncherVersion { get; set; }

		[JsonProperty("releaseTime")]
		public string ReleaseTime { get; set; }

		[JsonProperty("time")]
		public string Time { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }
	}

	public class AssetFileInfoModel {

		[JsonPropertyName("path")]
		public string Path { get; set; }

		[JsonProperty("sha1")]
		public string Sha1 { get; set; }

		[JsonProperty("size")]
		public long Size { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }
	}

	public class AssetDownladModel {

		[JsonProperty("client")]
		public AssetFileInfoModel Client { get; set; }

		[JsonProperty("server")]
		public AssetFileInfoModel Server { get; set; }
	}

	public class AssetIndexModel {

		[JsonProperty("id")]
		public string Id { get; set; }

		//Sha1 值
		[JsonProperty("sha1")]
		public string Sha1 { get; set; }

		//文件大小
		[JsonProperty("size")]
		public int Size { get; set; }

		[JsonProperty("totalSize")]
		public int TotalSize { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }
	}

	public class AssetJavaVersionModel {

		[JsonProperty("component")]
		public string Component { get; set; }

		[JsonProperty("majorVersion")]
		public short majorVersion { get; set; }
	}

	public class AssetLibraryModel {

		[JsonProperty("downloads")]
		public AssetLibraryDownloadModel Downloads { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }
	}

	public class AssetLibraryDownloadModel {

		[JsonProperty("artifact")]
		public AssetFileInfoModel Artifact { get; set; }
	}

	public class AssetLoggingModel {

		[JsonProperty("argument")]
		public string Argument { get; set; }

		[JsonProperty("file")]
		public AssetFileInfoModel File { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }
	}
}
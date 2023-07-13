using Newtonsoft.Json;

namespace SodaCL.Core.Models
{
	/// <summary>
	/// Minecraft 版本类型
	/// </summary>
	public enum VersionType
	{
		Snapshot,
		Release
	}

	public class VersionManifestModel
	{
		/// <summary>
		/// 最近的 Minecraft 版本号
		/// </summary>
		[JsonProperty("latest")]
		public LatestModel Latest { get; set; }

		[JsonProperty("versions")]
		public VersionModel[] Versions { get; set; }
	}

	public class LatestModel
	{
		/// <summary>
		/// 最近的 Minecraft 正式版版本号
		/// </summary>
		[JsonProperty("release")]
		public string Release { get; set; }

		/// <summary>
		/// 最近的 Minecraft 快照版版本号
		/// </summary>
		[JsonProperty("snapshot")]
		public string Snapshot { get; set; }
	}

	public class VersionModel
	{
		/// <summary>
		/// Minecraft 版本 Id
		/// </summary>
		[JsonProperty("id")]
		public string Id { get; set; }

		/// <summary>
		/// Minecraft 版本类型
		/// </summary>
		[JsonProperty("type")]
		public VersionType Type { get; set; }

		/// <summary>
		/// Minecraft 配置文件地址
		/// </summary>
		[JsonProperty("url")]
		public string Url { get; set; }

		/// <summary>
		/// Minecraft 发布时间 (ISO 8601-1:2019 格式)
		/// </summary>
		[JsonProperty("time")]
		public string Time { get; set; }

		/// <summary>
		/// Minecraft 构建时间 (ISO 8601-1:2019 格式)
		/// </summary>
		[JsonProperty("releaseTime")]
		public string ReleaseTime { get; set; }

		/// <summary>
		/// Minecraft Sha1 字符串
		/// </summary>
		[JsonProperty("sha1")]
		public string Sha1 { get; set; }

		/// <summary>
		/// TODO:弄清楚这是啥
		/// </summary>
		[JsonProperty("complianceLevel")]
		public short ComplianceLevel { get; set; }
	}
}
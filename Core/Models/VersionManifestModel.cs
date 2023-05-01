using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SodaCL.Core.Models
{
	public enum VersionType
	{
		Snapshot,
		Release
	}
	public class VersionManifestModel
	{
		[JsonProperty("latest")]
		public LatestModel Latest { get; set; }
		[JsonProperty("versions")]
		public VersionModel[] Versions { get; set; }
	}
	public class LatestModel
	{
		[JsonProperty("release")]
		public string Release { get; set; }
		[JsonProperty("snapshot")]
		public string Snapshot { get; set; }
	}
	public class VersionModel
	{
		[JsonProperty("id")]
		public string Id { get; set; }
		[JsonProperty("type")]
		public VersionType Type { get; set; }

	}
}

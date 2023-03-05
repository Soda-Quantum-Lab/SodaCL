using Newtonsoft.Json;

namespace SodaCL.Core.Java
{
	public class JavaModel
	{
		[JsonProperty("is64Bit")]
		public bool Is64Bit { get; set; }

		[JsonProperty("exePath")]
		public string ExePath { get; set; }

		[JsonProperty("path")]
		public string Path { get; set; }

		[JsonProperty("version")]
		public int Version { get; set; }
	}
}
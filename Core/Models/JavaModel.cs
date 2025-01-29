using Newtonsoft.Json;
using System;

namespace SodaCL.Core.Models {
	public class JavaModel : IEquatable<JavaModel> {
		[JsonProperty("is64Bit")]
		public bool Is64Bit { get; set; }

		[JsonProperty("javaPath")]
		public string JavaPath { get; set; }

		[JsonProperty("javawPath")]
		public string JavawPath { get; set; }

		[JsonProperty("dirPath")]
		public string DirPath { get; set; }

		[JsonProperty("version")]
		public string Version { get; set; }

		[JsonProperty("majorVersion")]
		public string MajorVersion { get; set; }

		public bool Equals(JavaModel other) {
			return DirPath == other.DirPath;
		}

		public override int GetHashCode() {
			return DirPath.GetHashCode();
		}
	}
}
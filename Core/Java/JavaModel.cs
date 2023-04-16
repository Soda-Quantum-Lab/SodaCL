using System;
using Newtonsoft.Json;

namespace SodaCL.Core.Java
{
	public class JavaModel:IEquatable<JavaModel>

	{
		[JsonProperty("is64Bit")]
		public bool Is64Bit { get; set; }

		[JsonProperty("javaPath")]
		public string JavaPath { get; set; }

		[JsonProperty("javawPath")]
		public string JavawPath { get; set; }

		[JsonProperty("dirPath")]
		public string DirPath { get; set; }

		[JsonProperty("version")]
		public int Version { get; set; }
		public bool Equals(JavaModel other)
		{
			return this.DirPath == other.DirPath;
		}
		public override int GetHashCode()
		{
			return DirPath.GetHashCode();
		}
	}
}
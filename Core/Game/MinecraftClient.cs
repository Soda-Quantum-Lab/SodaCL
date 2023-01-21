using System;
using System.Collections.Generic;

namespace SodaCL.Core.Game
{
	public class MinecraftClient
	{
		public int addition;
		public bool apart;
		public DateTime createTime, lstChangeTime;
		public bool fabricInCore = false;

		// forge, fabric, opti, ...
		public bool forgeInCore = false;

		public List<MinecraftMod> modList;
		public bool optifineInCore = false;
		public string path;
		public bool quiltInCore = false;
		public string version;
		public string versionJson, clientJar;

		public MinecraftClient(string version)
		{
			this.version = version;
			createTime = DateTime.Now;
			lstChangeTime = DateTime.Now;
		}
	}
}
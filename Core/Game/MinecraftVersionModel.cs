namespace SodaCL.Core.Game
{
	public class MinecraftVersionModel
	{
		#region 枚举

		/// <summary>
		/// Minecraft 版本类别枚举
		/// </summary>
		public enum MinecraftVersionState
		{
			Vanilla, //原版
			Snapshot,//快照
			Forge,
			Optfine,
			Fabric,
			Quilt
		}

		#endregion 枚举

		#region 属性

		/// <summary>
		/// Minecraft 路径
		/// </summary>
		public string Path { get; set; }

		/// <summary>
		/// Minecraft 版本号
		/// </summary>
		public string Version { get; set; }

		/// <summary>
		/// Minecraft 版本类别
		/// </summary>
		public MinecraftVersionState VersionState { get; set; }

		#endregion 属性
	}
}
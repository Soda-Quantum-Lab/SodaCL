using System;

namespace SodaCL
{
	public static class GlobalVariable
	{
		public static bool IsDialogOpen { get; set; } = false;
		public static TimeSpan HttpTimeout { get; } = TimeSpan.FromSeconds(20);

		public enum LoginType
		{
			Offline,
			Microsoft,
		}
	}
}
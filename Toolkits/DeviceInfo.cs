using System;

namespace SodaCL.Toolkits {
	public static class DeviceInfo {
		/// <summary>
		/// 返回 OS 主版本号
		/// </summary>
		public static int GetOsMajorNumber() {
			var osMajorNum = Environment.OSVersion.Version.Major;
			return osMajorNum;
		}

		/// <summary>
		/// 返回 OS 修订版本号
		/// </summary>
		/// <returns></returns>
		public static int GetOsRevisionNumber() {
			var osRevisionNum = Environment.OSVersion.Version.Revision;
			return osRevisionNum;
		}
	}
}
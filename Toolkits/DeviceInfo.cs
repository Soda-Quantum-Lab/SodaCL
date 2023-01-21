using System;

namespace SodaCL.Toolkits
{
	public static class DeviceInfo
	{
		public static int GetOsMajorNumber()
		{
			var osMajorNum = Environment.OSVersion.Version.Major;
			return osMajorNum;
		}

		public static int GetOsRevisionNumber()
		{
			var osRevisionNum = Environment.OSVersion.Version.Revision;
			return osRevisionNum;
		}
	}
}
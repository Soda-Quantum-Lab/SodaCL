using System.Globalization;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using static SodaCL.Toolkits.GetResources;

namespace SodaCL.Toolkits
{
	public static class AppCenterManager
	{
		public static void StartAppCenter()
		{
			AppCenter.Start(GetText("AppCenterToken"), typeof(Analytics), typeof(Crashes));
			AppCenter.LogLevel = LogLevel.None;
			AppCenter.SetCountryCode(RegionInfo.CurrentRegion.TwoLetterISORegionName);
		}
	}
}
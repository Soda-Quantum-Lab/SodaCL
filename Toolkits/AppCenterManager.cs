using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System.Globalization;
using static SodaCL.Toolkits.GetResources;

namespace SodaCL.Toolkits {

	public static class AppCenterManager {

		/// <summary>
		/// 启动 AppCenter 服务
		/// </summary>
		public static void StartAppCenter() {
			AppCenter.Start(GetText("AppCenterToken"), typeof(Analytics), typeof(Crashes));
			AppCenter.LogLevel = LogLevel.None;
			AppCenter.SetCountryCode(RegionInfo.CurrentRegion.TwoLetterISORegionName);
		}
	}
}
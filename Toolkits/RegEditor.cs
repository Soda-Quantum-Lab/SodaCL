using Microsoft.Win32;

namespace SodaCL.Toolkits
{
	public static class RegEditor
	{
		public enum RegDomain
		{
		}

		/// <summary>
		/// 创建注册表子项
		/// </summary>
		/// <param name="targetDomain">目标主域</param>
		/// <param name="targetSubKey">目标子项(目录格式)</param>
		public static void CreateSubKey(RegistryKey targetDomain, string targetSubKey)
		{
			var createSubKey = targetDomain.CreateSubKey(targetSubKey);
			targetDomain.Close();
			createSubKey.Close();
		}

		/// <summary>
		/// 删除注册表子项
		/// </summary>
		/// <param name="targetDomain">目标主域</param>
		/// <param name="targetDomain">目标子项(目录格式)</param>
		public static void DeleteKey(RegistryKey targetDomain, string targetSubKey)
		{
			targetDomain.DeleteSubKey(targetSubKey, true);
			targetDomain.Close();
		}

		/// <summary>
		/// 删除注册表键值
		/// </summary>
		/// <param name="targetDomain">目标主域</param>
		/// <param name="targetSubKey">目标子项(目录格式)</param>
		/// <param name="name">键名称</param>
		public static void DeleteKeyValue(RegistryKey targetDomain, string targetSubKey, string name)
		{
			var regDomain = targetDomain.OpenSubKey(targetSubKey, true);
			regDomain.DeleteValue(name, true);
			targetDomain.Close();
			regDomain.Close();
		}

		/// <summary>
		/// 返回注册表键值，若无目标键值返回 <see langword="null"/>
		/// </summary>
		/// <param name="regDomain"></param>
		/// <param name="targetSubKey"></param>
		/// <param name="name"></param>
		/// <returns> <see langword="string"/> 格式的目标值 </returns>
		public static string? GetKeyValue(RegistryKey regDomain, string name, string targetSubKey = @"Software\SodaCL")
		{
			var openedSubKey = regDomain.OpenSubKey(targetSubKey);
			var getValue = openedSubKey == null ? openedSubKey.GetValue(name).ToString() : null;
			regDomain.Close();
			openedSubKey.Close();
			return getValue;
		}

		/// <summary>
		/// 设置注册表键值
		/// </summary>
		/// <param name="targetDomain">目标主域</param>
		/// <param name="targetSubKey">目标子项(目录格式)</param>
		/// <param name="name">键名称</param>
		/// <param name="value">设置的值</param>
		/// <param name="valueKind">值的类别</param>
		public static void SetKeyValue(RegistryKey targetDomain, string targetSubKey, string name, string value, RegistryValueKind valueKind)
		{
			var registryKey = targetDomain.OpenSubKey(targetSubKey, true);
			registryKey.SetValue(name, value, valueKind);
			targetDomain.Close();
			registryKey.Close();
		}
	}
}
using Microsoft.Win32;
using System;
using System.Security.Cryptography;
using System.Text;

namespace SodaCL.Toolkits
{
	public class Encryption
	{
		public string machineSecret;

		public Encryption()
		{
			machineSecret = RegEditor.GetKeyValue(Registry.CurrentUser, "MachineSecret");
			if (machineSecret == null)
			{
				machineSecret = FingerPrint.Value();
			}
		}

		/// <summary>
		/// AES 加密
		/// </summary>
		/// <param name="str">明文（待加密）</param>
		/// <param name="key">密文</param>
		/// <returns></returns>
		public string AesEncrypt(string str)
		{
			if (string.IsNullOrEmpty(str)) return null;
			var toEncryptArray = Encoding.UTF8.GetBytes(str);

			var rm = new RijndaelManaged
			{
				Key = Encoding.UTF8.GetBytes(machineSecret),
				Mode = CipherMode.ECB,
				Padding = PaddingMode.PKCS7
			};

			var cTransform = rm.CreateEncryptor();
			var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
			return Convert.ToBase64String(resultArray);
		}

		/// <summary>
		/// AES 解密
		/// </summary>
		/// <param name="str">明文（待解密）</param>
		/// <param name="key">密文</param>
		/// <returns></returns>
		public string AesDecrypt(string str)
		{
			if (string.IsNullOrEmpty(str)) return null;
			var toEncryptArray = Convert.FromBase64String(str);

			var rm = new RijndaelManaged
			{
				Key = Encoding.UTF8.GetBytes(machineSecret),
				Mode = CipherMode.ECB,
				Padding = PaddingMode.PKCS7
			};

			var cTransform = rm.CreateDecryptor();
			var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

			return Encoding.UTF8.GetString(resultArray);
		}
	}
}
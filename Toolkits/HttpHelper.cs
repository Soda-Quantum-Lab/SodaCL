using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;

namespace SodaCL.Toolkits
{
	public static class HttpHelper
	{
		public async static Task<string> GetStringResponseAsync(string url)
		{
			using var client = new HttpClient();
			client.Timeout = GlobalVariable.HttpTimeout;
			return await client.GetStringAsync(url);
		}
		public async static Task<string> PostAsync(string url)
		{
			using var client = new HttpClient();
			client.Timeout = GlobalVariable.HttpTimeout;
			return await client.GetStringAsync(url);
		}
	}
}
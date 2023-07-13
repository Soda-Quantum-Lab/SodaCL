using System.Net.Http;
using System.Threading.Tasks;

namespace SodaCL.Toolkits
{
	public static class HttpHelper
	{
		public static async Task<string> GetStringResponseAsync(string url)
		{
			using var client = new HttpClient();
			client.Timeout = GlobalVariable.HttpTimeout;
			return await client.GetStringAsync(url);
		}

		public static async Task<string> PostAsync(string url)
		{
			using var client = new HttpClient();
			client.Timeout = GlobalVariable.HttpTimeout;
			return await client.GetStringAsync(url);
		}
	}
}
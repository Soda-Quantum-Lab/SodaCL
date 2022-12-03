using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SodaCL.Core.Minecraft
{
    class MCDownload
    {
        //private static string _version = "1.7.10";
        //private static string _type = "client";
        private static string _BMCLapiUrl = "https://bmclapi2.bangbang93.com/";
        //public static async Task GetVersion()
        //{
        //    //测试用
        //    string _version = "1.7.10";
        //    string _type = "client";
        //    string _apiUrl = "https://bmclapi2.bangbang93.com/version/";
        //    string _fullApiUrl = _apiUrl + _version + "/" + _type;
        //    HttpClient client = new();
        //    string response = await client.GetStringAsync(_fullApiUrl);
        //    MessageBox.Show(response);

        //}
        public static async Task GetManifest(string _targetPath)
        {
            HttpClient client = new();
            string response = await client.GetStringAsync(_BMCLapiUrl + "mc/game/version_manifest.json");
            File.WriteAllText(_targetPath, response);
        }
    }
}

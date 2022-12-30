using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SodaCL.Core.Minecraft
{
    public class McDownload
    {
        //private static string version = "1.7.10";
        //private static string type = "client";
        private static string BMCLapiUrl = "https://bmclapi2.bangbang93.com/";

        //public static async Task GetVersion()
        //{
        //    //测试用
        //    string version = "1.7.10";
        //    string type = "client";
        //    string apiUrl = "https://bmclapi2.bangbang93.com/version/";
        //    string fullApiUrl = apiUrl + version + "/" + type;
        //    HttpClient client = new();
        //    string response = await client.GetStringAsync(fullApiUrl);
        //    HandyControl.Controls.MessageBox.Show(response);

        //}
        public static async Task GetManifest(string targetPath)
        {
            HttpClient client = new();
            string response = await client.GetStringAsync(BMCLapiUrl + "mc/game/versionmanifest.json");
            File.WriteAllText(targetPath, response);
        }
    }
}
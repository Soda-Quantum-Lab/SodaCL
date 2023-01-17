using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SodaCL.Core.Game
{
    internal class MC_JsonParser
    {
        //如果好用，请收藏地址，帮忙分享。
        public class AssetIndex
        {
            /// <summary>
            /// 
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string sha1 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int size { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int totalSize { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string url { get; set; }
        }

        public class Client
        {
            /// <summary>
            /// 
            /// </summary>
            public string sha1 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int size { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string url { get; set; }
        }

        public class Server
        {
            /// <summary>
            /// 
            /// </summary>
            public string sha1 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int size { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string url { get; set; }
        }

        public class Downloads
        {
            /// <summary>
            /// 
            /// </summary>
            public Client client { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Server server { get; set; }
        }

        public class JavaVersion
        {
            /// <summary>
            /// 
            /// </summary>
            public string component { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int majorVersion { get; set; }
        }

        public class Artifact
        {
            /// <summary>
            /// 
            /// </summary>
            public string path { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string sha1 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int size { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string url { get; set; }
        }

        public class Downloads
        {
            /// <summary>
            /// 
            /// </summary>
            public Artifact artifact { get; set; }
        }

        public class LibrariesItem
        {
            /// <summary>
            /// 
            /// </summary>
            public Downloads downloads { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string name { get; set; }
        }

        public class File
        {
            /// <summary>
            /// 
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string sha1 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int size { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string url { get; set; }
        }

        public class Client
        {
            /// <summary>
            /// 
            /// </summary>
            public string argument { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public File file { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string type { get; set; }
        }

        public class Logging
        {
            /// <summary>
            /// 
            /// </summary>
            public Client client { get; set; }
        }

        public class Root
        {
            /// <summary>
            /// 
            /// </summary>
            public AssetIndex assetIndex { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string assets { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int complianceLevel { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Downloads downloads { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public JavaVersion javaVersion { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<LibrariesItem> libraries { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Logging logging { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string mainClass { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string minecraftArguments { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int minimumLauncherVersion { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string releaseTime { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string type { get; set; }
        }

        public static string MCJsonParser(string mcVersionJson)
        {
            Root rt = JsonConvert.DeserializeObject<Root>(mcVersionJson);
            return rt.ToString();
        }
        
    }
}

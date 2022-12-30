using System;
using System.Collections.Generic;

namespace SodaCL.Core.Minecraft
{
    public class MC_Mod
    {
        private string version;
        private string id, name;
        private List<string> author;
        private List<string> depend;
        private string platf;// forge/fabric/...
        private string gamePath = ".minecraft";//ÓÎÏ·Â·¾¶
    }

    public class MCClient
    {
        public string version;
        public int addition;// forge, fabric, opti, ...
        public bool forgeInCore = false;
        public bool fabricInCore = false;
        public bool quiltInCore = false;
        public bool optifineInCore = false;
        public bool apart;

        public List<MC_Mod> modList;
        public DateTime createTime, lstChangeTime;
        public string path;
        public string versionJson, clientJar;

        public MCClient(string version)
        {
            this.version = version;
            this.createTime = DateTime.Now;
            this.lstChangeTime = DateTime.Now;
        }
    }
}
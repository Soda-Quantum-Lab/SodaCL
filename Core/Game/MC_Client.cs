using System;
using System.Collections.Generic;

namespace SodaCL.Core.Game
{
    public class MC_Client
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

        public MC_Client(string version)
        {
            this.version = version;
            createTime = DateTime.Now;
            lstChangeTime = DateTime.Now;
        }
    }
}
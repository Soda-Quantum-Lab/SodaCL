using System.Collections.Generic;

namespace SodaCL.Core.Game
{
    public class MC_Mod
    {
        private string version;
        private string id, name;
        private List<string> author;
        private List<string> depend;
        private string platf;// forge/fabric/...
        private string gamePath = ".minecraft";
    }
}
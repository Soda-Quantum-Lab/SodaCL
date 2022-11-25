using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace SodaCL.Main.Minecraft
{
    public class LauncherInfo
    {
        public int launchTime;
        string version;

        public LauncherInfo()
        {
            this.launchTime = 0;
            this.version = "0.0.1";
        }
        public void addLaunchTime()
        { 
            this.launchTime += 1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

public class LauncherLogging
{
    public static void Log(string _logContent)
    {
        using (StreamWriter launcherlogging = new StreamWriter(".\\SodaCL\\Log.txt"))
        {
            launcherlogging.WriteLine(_logContent);
        }
        //FileStream fs = null;
        //string _logPath = ".\\SodaCL\\Log.txt";
        //byte[] bytes = Encoder.
        //fs = File.OpenWrite(_logPath);
        //fs.Position = fs.Length;
        //fs.Write(_logContent);
    }
}
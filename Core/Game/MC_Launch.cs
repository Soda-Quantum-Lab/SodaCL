using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;
using System.Security.Cryptography;
using System.Windows;
using static SodaCL.Core.Java.JavaSelector;
using static SodaCL.Toolkits.Logger;

namespace SodaCL.Core.Game
{
    internal class MC_Launch
    {
        public static void MCLaunching(string versionName, string ramMaxSize, string username)
        {
            string indexVersion = "1.12";
            int indexVersionInt;
            string[] javaSelectedPath = new string[100];
            string launcherVersion = "0.0.4.5";
            string uuid = "4a24d080533d310e9f4fab2a59b541fe";
            string accessToken = "c7c2a2390685472da58d2838c6f6dfbc";
            string windowWidth = "756";
            string windowHeight = "480";

            int.TryParse(indexVersion, out indexVersionInt);

            javaSelectedPath = (string[])SelectJavaByVersion(indexVersionInt);
            Log(false, ModuleList.Main, LogInfo.Info, "SodaCL 已选择了合适的 Java 版本: ");
            Log(false, ModuleList.Main, LogInfo.Info, javaSelectedPath[0]);

            Process.Start(javaSelectedPath[0], "-Dfile.encoding = GB18030 " + "-Dminecraft.client.jar =" + ".\\.minecraft\\versions\\" + versionName + "\\" + versionName + ".jar "  + "- XX:+UnlockExperimentalVMOptions - XX:+UseG1GC - XX:G1NewSizePercent = 20 - XX:G1ReservePercent = 20 - XX:MaxGCPauseMillis = 50 - XX:G1HeapRegionSize = 16m - XX:-UseAdaptiveSizePolicy - XX:-OmitStackTraceInFastThrow - XX:-DontCompileHugeMethods - Xmn128m " + "- Xmx" + ramMaxSize + " - Dfml.ignoreInvalidMinecraftCertificates = true - Dfml.ignorePatchDiscrepancies = true - Djava.rmi.server.useCodebaseOnly = true - Dcom.sun.jndi.rmi.object.trustURLCodebase = false - Dcom.sun.jndi.cosnaming.object.trustURLCodebase = false - Dlog4j2.formatMsgNoLookups = true - XX:HeapDumpPath = MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump - Djava.library.path =.\\.minecraft\\$natives " + "- Dminecraft.launcher.brand = SodaCL " + "- Dminecraft.launcher.version = " + launcherVersion + " - cp .\\.minecraft\\libraries\\com\\mojang\\patchy\\1.2.3\\patchy - 1.2.3.jar;.\\.minecraft\\libraries\\oshi - project\\oshi - core\\1.1\\oshi - core - 1.1.jar;.\\.minecraft\\libraries\\net\\java\\dev\\jna\\jna\\4.4.0\\jna - 4.4.0.jar;.\\.minecraft\\libraries\\net\\java\\dev\\jna\\platform\\3.4.0\\platform - 3.4.0.jar;.\\.minecraft\\libraries\\com\\ibm\\icu\\icu4j - core - mojang\\51.2\\icu4j - core - mojang - 51.2.jar;.\\.minecraft\\libraries\\net\\sf\\jopt - simple\\jopt - simple\\5.0.3\\jopt - simple - 5.0.3.jar;.\\.minecraft\\libraries\\com\\paulscode\\codecjorbis\\20101023\\codecjorbis - 20101023.jar;.\\.minecraft\\libraries\\com\\paulscode\\codecwav\\20101023\\codecwav - 20101023.jar;.\\.minecraft\\libraries\\com\\paulscode\\libraryjavasound\\20101123\\libraryjavasound - 20101123.jar;.\\.minecraft\\libraries\\com\\paulscode\\librarylwjglopenal\\20100824\\librarylwjglopenal - 20100824.jar;.\\.minecraft\\libraries\\com\\paulscode\\soundsystem\\20120107\\soundsystem - 20120107.jar;.\\.minecraft\\libraries\\io\\netty\\netty - all\\4.1.9.Final\\netty - all - 4.1.9.Final.jar;.\\.minecraft\\libraries\\com\\google\\guava\\guava\\21.0\\guava - 21.0.jar;.\\.minecraft\\libraries\\org\\apache\\commons\\commons - lang3\\3.5\\commons - lang3 - 3.5.jar;.\\.minecraft\\libraries\\commons - io\\commons - io\\2.5\\commons - io - 2.5.jar;.\\.minecraft\\libraries\\commons - codec\\commons - codec\\1.10\\commons - codec - 1.10.jar;.\\.minecraft\\libraries\\net\\java\\jinput\\jinput\\2.0.5\\jinput - 2.0.5.jar;.\\.minecraft\\libraries\\net\\java\\jutils\\jutils\\1.0.0\\jutils - 1.0.0.jar;.\\.minecraft\\libraries\\com\\google\\code\\gson\\gson\\2.8.0\\gson - 2.8.0.jar;.\\.minecraft\\libraries\\com\\mojang\\authlib\\1.5.25\\authlib - 1.5.25.jar;.\\.minecraft\\libraries\\com\\mojang\\realms\\1.10.22\\realms - 1.10.22.jar;.\\.minecraft\\libraries\\org\\apache\\commons\\commons - compress\\1.8.1\\commons - compress - 1.8.1.jar;.\\.minecraft\\libraries\\org\\apache\\httpcomponents\\httpclient\\4.3.3\\httpclient - 4.3.3.jar;.\\.minecraft\\libraries\\commons - logging\\commons - logging\\1.1.3\\commons - logging - 1.1.3.jar;.\\.minecraft\\libraries\\org\\apache\\httpcomponents\\httpcore\\4.3.2\\httpcore - 4.3.2.jar;.\\.minecraft\\libraries\\it\\unimi\\dsi\\fastutil\\7.1.0\\fastutil - 7.1.0.jar;.\\.minecraft\\libraries\\org\\apache\\logging\\log4j\\log4j-api\\2.8.1\\log4j-api-2.8.1.jar;.\\.minecraft\\libraries\\org\\apache\\logging\\log4j\\log4j-core\\2.8.1\\log4j-core-2.8.1.jar;.\\.minecraft\\libraries\\org\\lwjgl\\lwjgl\\lwjgl\\2.9.4-nightly-20150209\\lwjgl-2.9.4-nightly-20150209.jar;.\\.minecraft\\libraries\\org\\lwjgl\\lwjgl\\lwjgl_util\\2.9.4-nightly-20150209\\lwjgl_util-2.9.4-nightly-20150209.jar;.\\.minecraft\\libraries\\com\\mojang\\text2speech\\1.10.3\\text2speech-1.10.3.jar;.\\.minecraft\\versions\\" + versionName + "\\" + versionName + ".jar " + "net.minecraft.client.main.Main " + "--username " + username + " --version " + versionName + " --gameDir .\\ --assetsDir .\\.minecraft\\assets --assetIndex " + indexVersion + " --uuid " + uuid + " --accessToken " + accessToken + " --userType mojang --versionType \"SodaCL\" " + "--width " + windowWidth + " --height " + windowHeight);


        }
    }
}

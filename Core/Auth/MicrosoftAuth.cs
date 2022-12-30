using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SodaCL.Core.Auth.Models;
using static SodaCL.Core.Toolkits.I18N;
using static SodaCL.Core.Toolkits.Logger;

namespace SodaCL.Core.Auth
{
    public class MSAuth
    {
        public event EventHandler<WindowsTypes> OpenWindows;

        public bool IsSuccess { get; private set; } = false;

        public enum WindowsTypes
        {
            StartLogin,
            OpenInBrowser,
            GettingXboxXBLToken,
            GettingXboxXSTSToken,
            GettingMCProfile,
            NoProfile
        }

        private string OAuth2AccessToken { get; set; }
        private XboxXSTSResModel xboxXSTSResModel;
        private XboxXSTSErrModel xboxXSTSErrModel;
        private MinecraftProfileResModel minecraftProfileResModel;

        public async Task<MicrosoftAccount> StartAuthAsync(string clientId)
        {
            #region 获取 DeviceCode

            OpenWindows?.Invoke(this, WindowsTypes.StartLogin);
            var deviceCodeClient = new HttpClient();
            var deviceCodeContent = new Dictionary<string, string>();
            deviceCodeContent.Add("client_id", clientId);
            deviceCodeContent.Add("scope", "XboxLive.signin");
            deviceCodeClient.BaseAddress = new Uri("https://login.microsoftonline.com/consumers/oauth2/v2.0/");
            deviceCodeClient.Timeout = TimeSpan.FromSeconds(10);
            Log(ModuleList.Login, LogInfo.Info, "开始微软设备流登录");
            var oAuthPostResponse = await deviceCodeClient.PostAsync("devicecode", new FormUrlEncodedContent(deviceCodeContent));
            oAuthPostResponse.EnsureSuccessStatusCode();
            deviceCodeClient.Dispose();
            Log(ModuleList.Login, LogInfo.Info, "成功获取DeviceID");
            var microsoftOAuth2ResModel = JsonConvert.DeserializeObject<MicrosoftOAuth2ResModel>(await oAuthPostResponse.Content.ReadAsStringAsync());

            #endregion 获取 DeviceCode

            #region 获取 OAuth2 Access Token

            OpenWindows?.Invoke(this, WindowsTypes.OpenInBrowser);
            var AccessTokenClient = new HttpClient();
            AccessTokenClient.BaseAddress = new Uri("https://login.microsoftonline.com/consumers/oauth2/v2.0/");
            AccessTokenClient.Timeout = TimeSpan.FromSeconds(10);
            var oAuthPostContent = new Dictionary<string, string>();
            oAuthPostContent.Add("grant_type", "urn:ietf:params:oauth:grant-type:device_code");
            oAuthPostContent.Add("client_id", clientId);
            oAuthPostContent.Add("device_code", microsoftOAuth2ResModel.DeviceCode);
            var stopwatch = Stopwatch.StartNew();
            Log(ModuleList.Login, LogInfo.Info, "开始轮询");
            while (stopwatch.Elapsed < TimeSpan.FromSeconds(microsoftOAuth2ResModel.ExpiresIn))
            {
                await Task.Delay(microsoftOAuth2ResModel.Interval * 1000);

                var pollingPostRes = await AccessTokenClient.PostAsync("token", new FormUrlEncodedContent(oAuthPostContent));
                Log(ModuleList.Login, LogInfo.Info, "POST请求成功");
                if (pollingPostRes.IsSuccessStatusCode)
                {
                    var pollingPostResModel = JsonConvert.DeserializeObject<PollingPostResModel>(await pollingPostRes.Content.ReadAsStringAsync());
                    Log(ModuleList.Login, LogInfo.Info, "成功获取OAuth2Token");
                    OAuth2AccessToken = pollingPostResModel.AccessToken;
                    break;
                }
            }
            stopwatch.Stop();
            AccessTokenClient.Dispose();

            #endregion 获取 OAuth2 Access Token

            #region 获取Xbox XBL Token

            OpenWindows?.Invoke(this, WindowsTypes.GettingXboxXBLToken);
            var xboxXBLClient = new HttpClient();
            xboxXBLClient.Timeout = TimeSpan.FromSeconds(10);
            var xblJsonContent = "{ \"Properties\": { \"AuthMethod\": \"RPS\", \"SiteName\": \"user.auth.xboxlive.com\", \"RpsTicket\": \"d=" + OAuth2AccessToken + "\"}, \"RelyingParty\": \"http://auth.xboxlive.com\", \"TokenType\": \"JWT\" }";
            var xblPostContent = new StringContent(xblJsonContent, Encoding.UTF8, "application/json");
            var xblResponse = await xboxXBLClient.PostAsync("https://user.auth.xboxlive.com/user/authenticate", xblPostContent);
            xblResponse.EnsureSuccessStatusCode();
            var xboxXBLResModel = JsonConvert.DeserializeObject<XboxXBLResModel>(await xblResponse.Content.ReadAsStringAsync());
            xboxXBLClient.Dispose();

            #endregion 获取Xbox XBL Token

            #region 获取 Xbox XSTS Token

            OpenWindows?.Invoke(this, WindowsTypes.GettingXboxXSTSToken);
            var xboxXSTSClient = new HttpClient();
            xboxXSTSClient.Timeout = TimeSpan.FromSeconds(10);
            var xstsJsonContent = @"{""Properties"": { ""SandboxId"": ""RETAIL"", ""UserTokens"": [ """ + xboxXBLResModel.XboxXBLToken + @"""] }, ""RelyingParty"": ""rp://api.minecraftservices.com/"", ""TokenType"": ""JWT""}";
            var xstsPostContent = new StringContent(xstsJsonContent, Encoding.UTF8, "application/json");
            var xstsResponse = await xboxXSTSClient.PostAsync("https://xsts.auth.xboxlive.com/xsts/authorize", xstsPostContent);
            if (xstsResponse.StatusCode == (System.Net.HttpStatusCode)200)
            {
                xboxXSTSResModel = JsonConvert.DeserializeObject<XboxXSTSResModel>(await xstsResponse.Content.ReadAsStringAsync());
            }
            else if (xstsResponse.StatusCode == (System.Net.HttpStatusCode)401)
            {
                xboxXSTSErrModel = JsonConvert.DeserializeObject<XboxXSTSErrModel>(await xstsResponse.Content.ReadAsStringAsync());
                switch (xboxXSTSErrModel.XErr)
                {
                    case string Err when Err.Equals("2148916233"):
                        return new MicrosoftAccount
                        {
                            ErrorMessage = GetI18NText("Login_NoXboxAccount")
                        };

                    case string Err when Err.Equals("2148916235"):
                        return new MicrosoftAccount
                        {
                            ErrorMessage = GetI18NText("Login_XboxDisable")
                        };

                    case string Err when Err.Equals("2148916236") || Err.Equals("2148916237"):
                        return new MicrosoftAccount
                        {
                            ErrorMessage = GetI18NText("Login_NeedAdultAuth")
                        };

                    case string Err when Err.Equals("2148916233"):
                        return new MicrosoftAccount
                        {
                            ErrorMessage = GetI18NText("Login_NeedJoinInFamily")
                        };
                }
                xstsResponse.EnsureSuccessStatusCode();
                xboxXSTSResModel = JsonConvert.DeserializeObject<XboxXSTSResModel>(await xstsResponse.Content.ReadAsStringAsync());
                xboxXSTSClient.Dispose();
            }

            #endregion 获取 Xbox XSTS Token

            #region 获取 Minecraft Access Token

            OpenWindows?.Invoke(this, WindowsTypes.GettingXboxXSTSToken);

            var mcClient = new HttpClient();
            mcClient.Timeout = TimeSpan.FromSeconds(10);

            var mcJsonContent = $"{{ \"identityToken\": \"XBL3.0 x={xboxXSTSResModel.DisplayClaims.Xui[0]["uhs"]};{xboxXSTSResModel.XboxXSTSToken}\"}}";
            var mcPostContent = new StringContent(mcJsonContent, Encoding.UTF8, "application/json");
            var mcResponse = await mcClient.PostAsync("https://api.minecraftservices.com/authentication/login_with_xbox", mcPostContent);
            string mcAccessToken = (string)JObject.Parse(await mcResponse.Content.ReadAsStringAsync())["access_token"];

            #endregion 获取 Minecraft Access Token

            #region 获取 User Profile

            OpenWindows?.Invoke(this, WindowsTypes.GettingXboxXSTSToken);
            var userProfileClient = new HttpClient();
            userProfileClient.Timeout = TimeSpan.FromSeconds(10);
            userProfileClient.DefaultRequestHeaders.Add("Authorization", "Bearer" + " " + mcAccessToken);
            var userProfileRes = await userProfileClient.GetAsync("https://api.minecraftservices.com/minecraft/profile");
            if (userProfileRes.IsSuccessStatusCode)
            {
                minecraftProfileResModel = JsonConvert.DeserializeObject<MinecraftProfileResModel>(await userProfileRes.Content.ReadAsStringAsync());
                IsSuccess = true;
            }
            else if (userProfileRes.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                OpenWindows?.Invoke(this, WindowsTypes.GettingMCProfile);
                return new MicrosoftAccount
                {
                    ErrorMessage = GetI18NText("Login_NoGame")
                };
            }

            #endregion 获取 User Profile

            return new MicrosoftAccount
            {
                AccessToken = mcAccessToken,
                UserName = minecraftProfileResModel.Name,
                Uuid = Guid.Parse(minecraftProfileResModel.Id),
            };
        }
    }
}

//public class MCAuth
//{
//    public async Task GetMcToken()
//    {
//        using (var client = new HttpClient())
//        {
//            client.Timeout = TimeSpan.FromSeconds(10);
//            var jsonContent = "{ \"identityToken\": \"XBL3.0 x={uhs};{XSTSToken}\"}";
//            var postContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
//            try
//            {
//                var response = await client.PostAsync("https://api.minecraftservices.com/authentication/login_with_xbox", postContent);
//            }
//            catch (Exception ex)
//            {
//            }
//        }
//    }
//}
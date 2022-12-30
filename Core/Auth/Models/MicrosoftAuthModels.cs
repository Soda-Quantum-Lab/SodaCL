using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SodaCL.Core.Auth.Models
{
    public class DisplayClaimsModel
    {
        [JsonProperty("xui")]
        public List<JObject> Xui { get; set; }
    }

    public class MicrosoftOAuth2ResModel
    {
        [JsonProperty("device_code")]
        public string DeviceCode { get; set; }

        [JsonProperty("user_code")]
        public string UserCode { get; set; }

        [JsonProperty("verification_uri")]
        public Uri VerificationUri { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("interval")]
        public int Interval { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class PollingPostResModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }

    public class XboxXBLResModel
    {
        [JsonProperty("Token")]
        public string XboxXBLToken { get; set; }

        [JsonProperty("DisplayClaims")]
        public DisplayClaimsModel DisplayClaims { get; set; }
    }

    public class XboxXSTSResModel
    {
        [JsonProperty("Token")]
        public string XboxXSTSToken { get; set; }

        [JsonProperty("DisplayClaims")]
        public DisplayClaimsModel DisplayClaims { get; set; }
    }

    public class XboxXSTSErrModel
    {
        [JsonProperty("Identity")]
        public string Identity { get; set; }

        [JsonProperty("XErr")]
        public string XErr { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("Redirect")]
        public string Redirect { get; set; }
    }

    public class MinecraftProfileResModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("skins")]
        public List<SkinModel> Skins { get; set; }

        [JsonProperty("capes")]
        public JArray Capes { get; set; }
    }

    public class SkinModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("variant")]
        public string Variant { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }
    }
}
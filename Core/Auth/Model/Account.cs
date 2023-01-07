namespace SodaCL.Core.Auth.Model
{
    public abstract class Account
    {
        public System.Guid Uuid { get; set; }
    }

    public class MicrosoftAccount : Account
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
    }
}
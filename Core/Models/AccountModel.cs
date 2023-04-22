namespace SodaCL.Core.Models
{
	public abstract class AccountModel
	{
		public System.Guid Uuid { get; set; }
	}

	public class MicrosoftAccount : AccountModel
	{
		public string AccessToken { get; set; }
		public string UserName { get; set; }
	}
}
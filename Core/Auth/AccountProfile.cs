using SodaCL.Controls.Dialogs;
using SodaCL.Models.Core.Auth;
using SodaCL.Toolkits;

namespace SodaCL.Core.Profile {

	public class AccountProfile {

		public static void SaveProfile(AccountModel account) {
			var enc = new Encryption();
			var encryptionedProfile = enc.AesEncrypt(account.ToString());
			var sodaMsg = new SodaLauncherErrorDialog(encryptionedProfile);
		}
	}
}
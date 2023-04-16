using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SodaCL.Controls.Dialogs;
using SodaCL.Core.Auth;
using SodaCL.Toolkits;

namespace SodaCL.Core.Profile
{
	public class AccountProfile
	{
		public static void SaveProfile(AccountModel account)
		{
			var enc = new Encryption();
			var encryptionedProfile = enc.AesEncrypt(account.ToString());
			var sodaMsg = new SodaLauncherErrorDialog(encryptionedProfile);
		}
	}
}

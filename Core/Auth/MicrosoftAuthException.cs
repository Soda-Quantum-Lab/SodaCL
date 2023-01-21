using System;

namespace SodaCL.Core.Auth
{
	[Serializable]
	public class MicrosoftAuthException : Exception
	{
		public MicrosoftAuthException(MicrosoftAuth.MsAuthErrorType errorType, string message = null) : base(message)
		{
		}

		public MicrosoftAuth.MsAuthErrorType ErrorType { get; private set; }
	}
}
using System;

namespace SodaCL.Core.Auth
{
	[Serializable]
	public class MicrosoftAuthException : Exception
	{
		public MicrosoftAuthException(MicrosoftAuth.MsAuthErrorTypes errorType, string message = null) : base(message)
		{
		}

		public MicrosoftAuth.MsAuthErrorTypes ErrorType { get; private set; }
	}
}
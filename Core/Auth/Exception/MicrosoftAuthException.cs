using System;
using SodaCL.Core.Auth.Enum;

namespace SodaCL.Core.Auth.Exception
{
    [Serializable]
    public class MicrosoftAuthException : System.Exception
    {
        public MsAuthErrorType ErrorType { get; private set; }

        public MicrosoftAuthException(MsAuthErrorType errorType, string message = null) : base(message)
        {
        }
    }
}
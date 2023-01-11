using System;

namespace SodaCL.Core.Auth;

[Serializable]
public class MicrosoftAuthException : System.Exception
{
    public MsAuthErrorType ErrorType { get; private set; }

    public MicrosoftAuthException(MsAuthErrorType errorType, string message = null) : base(message)
    {
    }
}
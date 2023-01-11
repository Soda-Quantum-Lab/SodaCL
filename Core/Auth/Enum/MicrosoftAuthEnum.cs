namespace SodaCL.Core.Auth
{
    public enum WindowsTypes
    {
        StartLogin,
        OpenInBrowser,
        GettingXboxXBLToken,
        GettingXboxXSTSToken,
        GettingMCProfile,
        NoProfile
    }

    public enum MsAuthErrorType
    {
        AuthDeclined,
        ExpiredToken,
        NoXboxAccount,
        XboxDisable,
        NeedAdultAuth,
        NeedJoiningInFamily,
        NoGame,
    }
}
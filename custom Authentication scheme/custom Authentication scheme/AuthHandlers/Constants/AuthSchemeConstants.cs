namespace custom_Authentication_scheme.Authentication.Data
{
    public class AuthSchemeConstants
    {
        public const string MyAuthScheme = "MAS";
        public const string MyToken = $"{MyAuthScheme} (?<token>.*)";
    }
}

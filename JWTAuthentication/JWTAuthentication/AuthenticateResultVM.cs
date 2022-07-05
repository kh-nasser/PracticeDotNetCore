namespace JWTAuthentication
{
    public class AuthenticateResultVM
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        //public DateTime ExpireAt { get; set; }
    }
}

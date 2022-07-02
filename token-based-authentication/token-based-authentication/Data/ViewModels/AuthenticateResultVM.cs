namespace token_based_authentication.Data.ViewModels
{
    public class AuthenticateResultVM
    {
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}

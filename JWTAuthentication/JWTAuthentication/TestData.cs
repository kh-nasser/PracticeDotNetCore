using System.IdentityModel.Tokens.Jwt;

namespace JWTAuthentication
{
    public class TestData
    {
        private readonly Dictionary<UserVM, RefreshTokenEntity> _RefreshToken;
        private readonly Dictionary<UserVM, JwtSecurityToken> _AccessToken;
        public TestData()
        {
            _RefreshToken = new Dictionary<UserVM, RefreshTokenEntity>();
            _AccessToken = new Dictionary<UserVM, JwtSecurityToken>();
        }

        public Dictionary<UserVM, RefreshTokenEntity> RefreshToken()
        {
            return _RefreshToken;
        }

        public Dictionary<UserVM, JwtSecurityToken> AccessToken()
        {
            return _AccessToken;
        }
    }
}

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthentication
{
    public class JSONWebToken
    {
        private readonly IConfiguration _config;
        private readonly TestData _testData;
        public JSONWebToken(IConfiguration config, TestData testData)
        {
            _config = config;
            _testData = testData;
        }

        //public string GenerateJSONWebToken() {

        //    try
        //    {
        //        // reading the content of a private key PEM file, PKCS8 encoded 
        //        string privateKeyPem = File.ReadAllText("...");

        //        // keeping only the payload of the key 
        //        privateKeyPem = privateKeyPem.Replace("-----BEGIN PRIVATE KEY-----", "");
        //        privateKeyPem = privateKeyPem.Replace("-----END PRIVATE KEY-----", "");

        //        byte[] privateKeyRaw = Convert.FromBase64String(privateKeyPem);

        //        // creating the RSA key 
        //        RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
        //        provider.ImportPkcs8PrivateKey(new ReadOnlySpan<byte>(privateKeyRaw), out _);
        //        RsaSecurityKey rsaSecurityKey = new RsaSecurityKey(provider);

        //        // Generating the token 
        //        var now = DateTime.UtcNow;

        //        var claims = new[] {
        //            new Claim(JwtRegisteredClaimNames.Sub, "YOUR_CLIENTID"),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //        };

        //        var handler = new JwtSecurityTokenHandler();

        //        var token = new JwtSecurityToken
        //        (
        //            "YOUR_CLIENTID",
        //            "https://AAAS_PLATFORM/idp/YOUR_TENANT/authn/token",
        //            claims,
        //            now.AddMilliseconds(-30),
        //            now.AddMinutes(60),
        //            new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256)
        //        );

        //        // handler.WriteToken(token) returns the token ready to send to AaaS !
        //        Console.WriteLine(handler.WriteToken(token));

        //    }

        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //        Console.WriteLine(
        //             new System.Diagnostics.StackTrace().ToString()
        //        );
        //    }
        //}


        public async Task<AuthenticateResultVM> GenerateJSONWebTokenAsync(UserVM userInfo, RefreshTokenEntity? storedRefreshToken = null)
        {
            var token = CreateAccessToken(userInfo);
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            if (storedRefreshToken != null)
            {
                var rTokenResponse = new AuthenticateResultVM()
                {
                    Token = jwtToken,
                    RefreshToken = storedRefreshToken.Token,
                    //ExpireAt = token.ValidTo,
                };

                //todo await update access token
                _testData.AccessToken().Remove(userInfo);
                _testData.AccessToken().Add(userInfo, token);
                return rTokenResponse;
            }

            var newRefreshToken = new RefreshTokenEntity()
            {
                JwtId = token.Id,
                IsRevoked = false,
                UserId = userInfo.UserName,
                DateAdded = DateTime.UtcNow,
                DateExpire = DateTime.UtcNow.AddMonths(6),
                Token = $"{Guid.NewGuid()}-{Guid.NewGuid()}",
            };

            //construct reponse
            var response = new AuthenticateResultVM()
            {
                Token = jwtToken,
                RefreshToken = newRefreshToken.Token,
                //ExpireAt = token.ValidTo,
            };

            //await update refresh token
            _testData.RefreshToken().Remove(userInfo);
            _testData.RefreshToken().Add(userInfo, newRefreshToken);

            //todo await update access token
            _testData.AccessToken().Remove(userInfo);
            _testData.AccessToken().Add(userInfo, token);

            return response;
        }

        private JwtSecurityToken CreateAccessToken(UserVM userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim(JwtRegisteredClaimNames.Email, "EmailAddress"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Date", DateTime.Now.ToString("yyyy-MM-dd"))
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials);

            return token;
        }


        public bool ValidateToken(TokenValidationParameters tokenValidationParameters, string AccessToken)
        {
            bool isValid = false;
            try
            {
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var tokenCheckResult = jwtTokenHandler.ValidateToken(AccessToken, tokenValidationParameters, out var validateToken);
                isValid = true;
                //Generate-AccessToken
            }
            catch (SecurityTokenException)
            {

                //if ((storedRefreshTokenDto.DateExpire >= DateTime.UtcNow) && (!storedRefreshTokenDto.IsRevoked))
                //{
                //    //Generate-AccessToken
                //}
                //else
                //{
                //    //Generate-RefreshToken
                //    //Generate-AccessToken
                //}
            }
            return isValid;
        }

        public JwtSecurityToken? Decode(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwt);
            var token = jsonToken as JwtSecurityToken;
            return token;
        }

        public string? GetClaim(string JWT, string ClaimType)
        {
            var claim = Decode(JWT)?.Claims.First(claim => claim.Type == ClaimType).Value;
            return claim;
        }

        public string? GetClaim(string JWT, JwtRegisteredClaimNames ClaimType)
        {
            var claim = GetClaim(JWT, ClaimType);
            return claim;
        }
    }
}
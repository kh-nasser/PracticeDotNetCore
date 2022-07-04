using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthentication
{
    public class JSONWebToken
    {
        public IConfiguration _config;
        public JSONWebToken(IConfiguration config)
        {
            _config = config;
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
        public string GenerateJSONWebToken(UserModel userInfo)
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
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

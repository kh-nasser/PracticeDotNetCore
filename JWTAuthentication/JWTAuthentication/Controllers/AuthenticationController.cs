using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace JWTAuthentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration _config;
        private readonly TokenValidationParameters tokenValidationParameters;
        private readonly TestData testData;

        public AuthenticationController(IConfiguration config, ILogger<WeatherForecastController> logger, TokenValidationParameters tokenValidationParameters, TestData testData)
        {
            _logger = logger;
            _config = config;
            this.tokenValidationParameters = tokenValidationParameters;
            this.testData = testData;
        }

        [AllowAnonymous]
        [HttpPost("Authentication")]
        public async Task<IActionResult> AuthenticationAsync([FromBody] UserVM model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
                //Unauthorized();
            }

            var token = await new JSONWebToken(_config,testData).GenerateJSONWebTokenAsync(model);

            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] TokenRequestVM model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var token = await VerifyAndGenerateTokenAsync(model);

            return Ok(token);
        }

        private async Task<AuthenticateResultVM> VerifyAndGenerateTokenAsync(TokenRequestVM model)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var storedRefreshToken = testData.RefreshToken().FirstOrDefault(x => x.Value.Token == model.RefreshToken); 

            try
            {
                var tokenCheckResult = jwtTokenHandler.ValidateToken(model.Token, tokenValidationParameters, out var validateToken);
                return await new JSONWebToken(_config, testData).GenerateJSONWebTokenAsync(storedRefreshToken.Key, storedRefreshToken.Value);
            }
            catch (SecurityTokenException)
            {
                if ((storedRefreshToken.Value.DateExpire >= DateTime.UtcNow) && (!storedRefreshToken.Value.IsRevoked))
                {
                    return await new JSONWebToken(_config, testData).GenerateJSONWebTokenAsync(storedRefreshToken.Key, storedRefreshToken.Value);
                }
                else
                {
                    return await new JSONWebToken(_config, testData).GenerateJSONWebTokenAsync(storedRefreshToken.Key, null);
                }
            }
        }

    }
}
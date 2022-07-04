using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration _config;

        public AuthenticationController(IConfiguration config, ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
                //Unauthorized();
            }


            var tokenString = new JSONWebToken(_config).GenerateJSONWebToken(model);
            return Ok(new { token = tokenString });
        }
    }
}
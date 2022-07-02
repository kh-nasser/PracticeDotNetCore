using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using token_based_authentication.Data;
using token_based_authentication.Data.Models;
using token_based_authentication.Data.ViewModels;

namespace token_based_authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly AppDbContext context;
        private readonly IConfiguration configuration;

        public AuthenticationController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
            this.configuration = configuration;
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> Register([FromBody] RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please, provide all the required fields");
            }

            var userExist = await userManager.FindByEmailAsync(model.EmailAddrress);
            if (userExist != null)
            {
                return BadRequest($"User, {model.EmailAddrress} already exist");
            }

            var newUser = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.EmailAddrress,
                SecurityStamp = Guid.NewGuid().ToString(),
                Custom = ""
            };

            var result = await userManager.CreateAsync(newUser, model.Password);
            var userInserted = await userManager.FindByEmailAsync(model.EmailAddrress);

            if (result.Succeeded && userInserted != null) return Ok("User created");
            return BadRequest(result.Errors == null ?
                "User could not be created" :
                $"{JsonSerializer.Serialize(result.Errors.ToArray())}");
        }

        [HttpPost("login-user")]
        public async Task<IActionResult> Login([FromBody] LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please, provide all required field");
            }

            var userExist = await userManager.FindByEmailAsync(model.EmailAddrress);
            if (userExist != null && await userManager.CheckPasswordAsync(userExist, model.Password))
            {
                var tokenValue = await GenerateJWTTokenAsync(userExist);
                return Ok(tokenValue);
            }

            return Unauthorized();
        }

        private async Task<AuthenticateResultVM> GenerateJWTTokenAsync(ApplicationUser user)
        {
            //adding claim
            var authClaims = new List<Claim>() {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            //get secret key
            var authSigingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]));
            //define token
            var token = new JwtSecurityToken(issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                expires: DateTime.UtcNow.AddMinutes(5),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigingKey, SecurityAlgorithms.HmacSha256));
            //generate jwt-token from token
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                IsRevoked = false,
                UserId = user.Id,
                DateAdded = DateTime.UtcNow,
                DateExpire = DateTime.UtcNow.AddMonths(6),
                Token = $"{Guid.NewGuid()}-{Guid.NewGuid()}",
            };

            await context.RefreshTokens.AddAsync(refreshToken);
            await context.SaveChangesAsync();

            //construct reponse
            var response = new AuthenticateResultVM()
            {
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
                ExpireAt = token.ValidTo,
            };

            return response;
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using token_based_authentication.Data;
using token_based_authentication.Data.Helper;
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
        private readonly TokenValidationParameters tokenValidationParameters;

        public AuthenticationController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context, IConfiguration configuration, TokenValidationParameters tokenValidationParameters)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
            this.configuration = configuration;
            this.tokenValidationParameters = tokenValidationParameters;
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

            if (result.Succeeded)
            {
                //Add user role

                switch (model.Role)
                {
                    case UserRole.Manager:
                        await userManager.AddToRoleAsync(newUser, UserRole.Manager);
                        break;
                    case UserRole.Student:
                        await userManager.AddToRoleAsync(newUser, UserRole.Student);
                        break;
                    default:
                        break;
                }

                return Ok("User created");
            }
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
                var tokenValue = await GenerateJWTTokenAsync(userExist, null);
                return Ok(tokenValue);
            }

            return Unauthorized();
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequestVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please, provide all required field");
            }

            var result = await VerifyAndGenerateTokenAsync(model);
            return Ok(result);
        }

        private async Task<AuthenticateResultVM> VerifyAndGenerateTokenAsync(TokenRequestVM model)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var storedToken = await context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == model.RefreshToken);
            var dbUser = await userManager.FindByIdAsync(storedToken?.UserId);

            try
            {
                var tokenCheckResult = jwtTokenHandler.ValidateToken(model.Token, tokenValidationParameters, out var validateToken);
                return await GenerateJWTTokenAsync(dbUser, storedToken);
            }
            catch (SecurityTokenException)
            {
                if ((storedToken.DateExpire >= DateTime.UtcNow) && (!storedToken.IsRevoked))
                {
                    return await GenerateJWTTokenAsync(dbUser, storedToken);
                }
                else
                {
                    return await GenerateJWTTokenAsync(dbUser, null);
                }
            }
        }

        private async Task<AuthenticateResultVM> GenerateJWTTokenAsync(ApplicationUser user, RefreshToken storedRefreshToken)
        {
            //adding claim
            var authClaims = new List<Claim>() {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            //Add User Role Claims
            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles) {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            //get secret key
            var authSigingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]));
            //define token
            var token = new JwtSecurityToken(issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                expires: DateTime.UtcNow.AddMinutes(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigingKey, SecurityAlgorithms.HmacSha256));
            //generate jwt-token from token
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            if (storedRefreshToken != null)
            {
                var rTokenResponse = new AuthenticateResultVM()
                {
                    Token = jwtToken,
                    RefreshToken = storedRefreshToken.Token,
                    ExpireAt = token.ValidTo,
                };

                return rTokenResponse;
            }

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

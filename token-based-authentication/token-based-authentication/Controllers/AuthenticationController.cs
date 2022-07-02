using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly AppDomain context;
        private readonly IConfiguration configuration;

        public AuthenticationController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AppDomain context, IConfiguration configuration)
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
            };

            var result = await userManager.CreateAsync(newUser, model.Password);
            if (result.Succeeded) return Ok("User created");
            return BadRequest("User could not be created");
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
                return Ok("User signed in");
            }

            return Unauthorized();
        }
    }
}

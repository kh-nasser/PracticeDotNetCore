using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using token_based_authentication.Data.Helper;

namespace token_based_authentication.Controllers
{
    [Authorize(Roles = UserRole.Manager)]
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementController : ControllerBase
    {
        public ManagementController()
        {

        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Welcome to ManagementController");
        }
    }
}

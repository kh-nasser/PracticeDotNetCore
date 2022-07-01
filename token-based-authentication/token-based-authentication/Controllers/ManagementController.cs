using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace token_based_authentication.Controllers
{
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace token_based_authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {

        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Welcome to HomeControler");
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using token_based_authentication.Data.Helper;

namespace token_based_authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRole.Manager + "," + UserRole.Student)]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {

        }

        [HttpGet("student")]
        public IActionResult GetStudent()
        {
            return Ok("Student, Welcome to HomeControler");
        }
        [HttpGet("manager")]
        public IActionResult GetManager()
        {
            return Ok("Manager, Welcome to HomeControler");
        }
    }
}

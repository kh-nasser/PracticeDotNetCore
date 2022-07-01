using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestWebApplication.Models;

namespace TestWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly WebRepository.Cookies _cookies;
        public HomeController(IHttpContextAccessor httpContextAccessor, ILogger<HomeController> logger, WebRepository.Cookies cookies)
        {
            this._httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _cookies= cookies;

        }
     
        public IActionResult Index()
        {
            //read cookie from Request object  
            string cookieValueFromReq = _cookies.Get(Request,"cookie1");
            //set the key value in Cookie  
            _cookies.Set(Response, "cookie1", "Hello from cookie", 10);
            //Delete the cookie object  
            //_cookies.Remove(Response, "cookie1");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
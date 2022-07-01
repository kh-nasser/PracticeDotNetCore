using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class AuthController : Controller
    {
        private IHttpClientFactory _httpClientFactory;
        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;//EshopCLient
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var _client = _httpClientFactory.CreateClient("EshopCLient");

            var jsonBody = JsonConvert.SerializeObject(login);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var response = _client.PostAsync("/api/auth/login", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var token = response.Content.ReadAsAsync<TokenModel>().Result;
                //var responseJson = response.Content.ReadAsStringAsync().Result;
                //var token = JsonConvert.DeserializeObject<TokenModel>(responseJson);

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, login.UserName),
                    new Claim(ClaimTypes.Name, login.UserName),
                    new Claim("AccessToken", token.Token)
                };
                //ser cookie
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                //configs
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                };
                //authenticate user
                HttpContext.SignInAsync(principal, properties);
                return Redirect("/home");

            }
            else
            {
                ModelState.AddModelError("UserName", "User not valid");
                return View(login);
            }
        }
    }
}

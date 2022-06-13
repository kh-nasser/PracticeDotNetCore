﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLayer.Services.Users;
using CoreLayer.ViewModels.Auth;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace signalRTest_chat.Controllers
{
    public class AuthController : Controller
    {
        private IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }
            var result = await _userService.RegisterUser(model);
            if (!result)
            {
                ModelState.AddModelError(model.UserName,"نام کاربری تکراری است");
                return View("Index", model);
            }
            return Redirect("/auth#login");
        }


        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            var user = await _userService.LoginUser(loginModel);
            if (user == null)
            {
                ModelState.AddModelError(loginModel.UserName, "کاربری با مشخصات وارد شده یافت نشد");
                return View("Index", loginModel);
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName.ToString()),
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = true
            };
            await HttpContext.SignInAsync(principal, properties);
            return Redirect("/");
        }
    }
}

using eshop_webapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eshop_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("Login")]
        public IActionResult Login([FromBody] Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The model is not valid");
            }

            if (login == null
                || login.UserName.ToLower() != "test"
                || login.Password.ToLower() != "test"
                )
            {
                return Unauthorized();
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authnetication"));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOption = new JwtSecurityToken(
                issuer: "http://localhost:3962",
                claims: new List<Claim> {
                new Claim(ClaimTypes.Name, login.UserName),//login.identity.name,User.Identity.Name
                new Claim(ClaimTypes.Role, "Admin")
                }
                , expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signingCredentials
                );

            //create token
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOption);

            return Ok(new { tokenString });
        }
    }
}

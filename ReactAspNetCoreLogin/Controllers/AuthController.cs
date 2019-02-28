using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactAspNetCoreLogin.Models;
using ReactAspNetCoreLogin.Service;

namespace ReactAspNetCoreLogin.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class AuthController : Controller
    {
        private readonly TokenService tokenService;

        public AuthController(TokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        // GET: api/Auth
        [Authorize]
        [HttpGet("secure")]
        public IActionResult Get()
        {
            var user = HttpContext.User;
            
            return Ok($"Hello {user.Identity.Name} with id:{user.FindFirst(ClaimTypes.NameIdentifier).Value}, this is your secure message at {DateTime.Now}");
        }

        [HttpPost("token")]
        public IActionResult Post([FromBody] UserLoginViewModel user)
        {
            if (string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Password))
                return BadRequest("Username or password is incorrect");

            user.Id = new Random().Next(1, 99);
            var (token, validTo) = tokenService.GetToken(user);

            return Ok(new
                      {
                          token,
                          expiration = validTo
                      });
        }
    }
}
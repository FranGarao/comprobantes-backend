using comprobantes_back.Models;
using comprobantes_back.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace comprobantes_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILoginService<User> _userService;
        public UsersController(ILoginService<User> service)
        {
            _userService = service;
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User userData)
        {
            if (userData == null)
            {
                return BadRequest("No se encontraron datos");
            }

            var token = await _userService.Login(userData);

            if (token == null)
            {
                return Unauthorized();
            }

            //var cookieOptions = new CookieOptions
            //{
            //    HttpOnly = false,
            //    Secure = false, // Asegúrate de usar HTTPS
            //    SameSite = SameSiteMode.Strict,
            //    //SameSite = SameSiteMode.Stritc,
            //    Expires = DateTime.UtcNow.AddHours(1)
            //};

            //Response.Cookies.Append("AuthToken", token, cookieOptions);

            return Ok(new {_token =  token });
        }
    }
}

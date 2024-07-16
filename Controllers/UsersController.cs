using comprobantes_back.Models;
using comprobantes_back.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<User>> Login(User userData)
        {
            if (userData == null)
            {
                return BadRequest("No se encontraron datos");
            }

            var user = await _userService.Login(userData);

            if (user == null)
            {
                return BadRequest("Fallo en el inicio de sesion");
            }
            return Ok(user);
        }
    }
}

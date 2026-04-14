using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApiProject.Services;

namespace MyApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            if (!_authService.ValidateUser(dto.Username, dto.Password))
                return Unauthorized("Kullanıcı hatalı");

            var token = _authService.GenerateToken(dto.Username);

            return Ok(new { token });
        }


    }
}

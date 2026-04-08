using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NutriCook_AI_WebAPI.DTOs.Users;
using NutriCook_AI_WebAPI.Interfaces.IServices;

namespace NutriCook_AI_WebAPI.Controllers
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

        [HttpPost("Signup")]
        public async Task<IActionResult> SignupUser([FromBody] UserCreateDTO user)
        {
            var result = await _authService.SignupUser(user);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Loginuser([FromBody] UserLoginDTO user)
        {
            var result = await _authService.LoginUser(user);
            return Ok(result);
        }
    }
}

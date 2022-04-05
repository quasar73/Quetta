using Common.DTO;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("AuthenticateWithGoogle")]
        public async Task<IActionResult> AuthenticateWithGoogle(GoogleUserDto googleUser)
        {
            var token = await authService.AuthenticateGoogleUserAsync(googleUser);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new TokenDto
            {
                Token = token,
            });
        }

        [HttpPost("RegisterGoogleUser")]
        public async Task<IActionResult> RegisterGoogleUser(RegisterGoogleUserDto registerGoogleUser)
        {
            var token = await authService.RegisterGoogleUserAsync(registerGoogleUser);

            if (token == null)
            {
                return BadRequest();
            }

            return Ok(new TokenDto
            {
                Token = token,
            });
        }

        [HttpGet("CheckOutUsername")]
        public async Task<IActionResult> CheckOutUsername([FromQuery] string username)
        {
            var result = await authService.CheckOutUsername(username);

            return Ok(result);
        }
    }
}

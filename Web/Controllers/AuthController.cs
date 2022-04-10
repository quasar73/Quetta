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

        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("AuthenticateWithGoogle")]
        public async Task<IActionResult> AuthenticateWithGoogle([FromBody] GoogleUserDto googleUser)
        {
            var token = await authService.AuthenticateGoogleUserAsync(googleUser);

            return Ok(token);
        }

        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("RegisterGoogleUser")]
        public async Task<IActionResult> RegisterGoogleUser([FromBody] RegisterGoogleUserDto registerGoogleUser)
        {
            var token = await authService.RegisterGoogleUserAsync(registerGoogleUser);

            return Ok(token);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("CheckOutUsername")]
        public async Task<IActionResult> CheckOutUsername([FromQuery] string username)
        {
            var result = await authService.CheckOutUsername(username);

            return Ok(result);
        }

        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("Refresh")]
        public async Task<IActionResult> RefreshToken([FromQuery] string refreshToken)
        {
            var result = await authService.RefreshToken(refreshToken);

            return Ok(result);
        }
    }
}

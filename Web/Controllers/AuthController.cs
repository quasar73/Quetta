using Common.Models;
using Common.Models.Commands;
using Common.Models.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        [ProducesResponseType(typeof(TokenModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("AuthenticateWithGoogle")]
        public async Task<IActionResult> AuthenticateWithGoogle([FromBody] AuthenticateGoogleUserCommand request)
        {
            var token = await mediator.Send(request);

            return Ok(token);
        }

        [ProducesResponseType(typeof(TokenModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("RegisterGoogleUser")]
        public async Task<IActionResult> RegisterGoogleUser([FromBody] RegisterGoogleUserCommand request)
        {
            var token = await mediator.Send(request);

            return Ok(token);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("CheckOutUsername")]
        public async Task<IActionResult> CheckOutUsername([FromQuery] string username)
        {
            var result = await mediator.Send(new CheckOutUsernameQuery(username));

            return Ok(result);
        }

        [ProducesResponseType(typeof(TokenModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("Refresh")]
        public async Task<IActionResult> RefreshToken([FromQuery] string refreshToken)
        {
            var result = await mediator.Send(new RefreshTokenQuery(refreshToken));

            return Ok(result);
        }
    }
}

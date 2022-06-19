using Common.Models.Commands;
using Common.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvitesController : ControllerBase
    {
        private readonly IMediator mediator;
        
        public InvitesController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> SendInvite([FromBody] SendInviteRequest request)
        {
            var senderId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (senderId == null)
            {
                return Unauthorized();
            }

            await mediator.Send(new SendInviteCommand(request, senderId));

            return Ok();
        }
    }
}

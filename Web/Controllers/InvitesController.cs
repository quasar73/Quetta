using Common.Models.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> SendInvite([FromBody] SendInviteCommand request)
        {
            await mediator.Send(request);

            return Ok();
        }
    }
}

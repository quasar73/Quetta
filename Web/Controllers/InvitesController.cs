using Common.Models.Commands;
using Common.Models.Queries;
using Common.Models.Requests;
using Logic.Notifications;
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
            var senderId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            await mediator.Send(new SendInviteCommand(request, senderId));
            await mediator.Publish(new NewNotification(request.ReceiverUsername));

            return Ok();
        }

        [HttpGet("Any")]
        public async Task<IActionResult> IsAnyNotifications()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var hasNotifications = await mediator.Send(new IsAnyNotificationsQuery(userId));

            return Ok(hasNotifications);
        }
    }
}

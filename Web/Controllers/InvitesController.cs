using Common.Models.Commands;
using Common.Models.Queries;
using Common.Models.Requests;
using Common.Models.Responses;
using Data.Models;
using Logic.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Quetta.Common.Models.Commands;
using Quetta.Common.Models.Requests;
using System.Security.Claims;

namespace Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvitesController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly UserManager<User> userManager;
        
        public InvitesController(IMediator mediator, UserManager<User> userManager)
        {
            this.mediator = mediator;
            this.userManager = userManager;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<IActionResult> SendInvite([FromBody] SendInviteRequest request)
        {
            var senderId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            await mediator.Send(new SendInviteCommand(request, senderId));
            await mediator.Publish(new NewNotification(request.ReceiverUsername));

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("Accept")]
        public async Task<IActionResult> AcceptInvite([FromBody] RespondInviteRequest request)
        {
            var receiverId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var receiverUserName = (await userManager.FindByIdAsync(receiverId)).UserName;

            await mediator.Send(new AcceptInviteCommand(request.InviteId, receiverId));
            await mediator.Publish(new NewNotification(receiverUserName));

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("Decline")]
        public async Task<IActionResult> DeclineInvite([FromBody] RespondInviteRequest request)
        {
            var receiverId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var receiverUserName = (await userManager.FindByIdAsync(receiverId)).UserName;

            await mediator.Send(new DeclineInviteCommand(request.InviteId, receiverId));
            await mediator.Publish(new NewNotification(receiverUserName));

            return Ok();
        }

        [ProducesResponseType(typeof(List<InviteResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult> GetUserInvites()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            var invites = await mediator.Send(new GetInvitesQuery(user.Id));

            return Ok(invites);
        }
    }
}

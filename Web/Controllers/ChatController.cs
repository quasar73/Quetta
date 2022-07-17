using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quetta.Common.Models.Queries;
using Quetta.Common.Models.Responses;
using System.Security.Claims;

namespace Quetta.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IMediator mediator; 

        public ChatController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [ProducesResponseType(typeof(ICollection<ChatItemResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public async Task<IActionResult> GetChats()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var chats = await mediator.Send(new GetChatsQuery(userId));

            return Ok(chats);
        }
    }
}

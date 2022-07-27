using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quetta.Common.Models.Requests;
using Quetta.Common.Models.Commands;
using System.Security.Claims;
using Quetta.Common.Models.Queries;

namespace Quetta.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMediator mediator;

        public MessagesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageReqeust reqeust)
        {
            var senderId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var command = new SendMessageCommand
            {
                SenderId = senderId,
                Text = reqeust.Text,
                Date = DateTime.UtcNow,
                ChatId = reqeust.ChatId,
            };

            await mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages([FromQuery] string chatId)
        {
            var messages = await mediator.Send(new GetMessagesQuery(chatId));
            return Ok(messages);
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quetta.Common.Models.Requests;
using Quetta.Common.Models.Commands;
using System.Security.Claims;
using Quetta.Common.Models.Queries;
using Quetta.Common.Models.Notifications;
using Quetta.Common.Models.Responses;

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

        [ProducesResponseType(typeof(MessageAddedResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

            var messageId = await mediator.Send(command);
            await mediator.Publish(
                new MessageNotification { ChatId = reqeust.ChatId, MessageId = messageId, }
            );

            return Ok(new MessageAddedResponse(messageId));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete]
        public async Task<IActionResult> DeleteMessages([FromQuery] DeleteMessagesRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            await mediator.Send(new DeleteMessagesCommand(userId, request.MessageIds));
            return Ok();
        }

        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("Read")]
        public async Task<IActionResult> ReadMessages([FromQuery] string messageId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var command = new ReadMessagesCommand(messageId, userId);
            var response = await mediator.Send(command);
            await mediator.Publish(new ReadNotification(response));

            return Ok();
        }

        [ProducesResponseType(typeof(ICollection<MessageResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public async Task<IActionResult> GetMessages([FromQuery] GetMessagesRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var command = new GetMessagesQuery(request, userId);
            var messages = await mediator.Send(command);
            return Ok(messages);
        }
    }
}

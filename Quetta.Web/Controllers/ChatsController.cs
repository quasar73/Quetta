﻿using MediatR;
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
    public class ChatsController : ControllerBase
    {
        private readonly IMediator mediator; 

        public ChatsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [ProducesResponseType(typeof(ChatInfoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("info/{chatId}")]
        public async Task<IActionResult> GetChatInfo([FromRoute] string chatId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var chatInfo = await mediator.Send(new GetChatInfoQuery
            {
                ChatId = chatId,
                UserId = userId,
            });

            return Ok(chatInfo);
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

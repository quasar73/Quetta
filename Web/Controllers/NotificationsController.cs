using Common.Models.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IMediator mediator;

        public NotificationsController(IMediator mediator)
        {
            this.mediator = mediator;
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

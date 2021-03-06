using Common.Enums;
using Data;
using Microsoft.AspNetCore.SignalR;

namespace Logic.Hubs
{
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var id = Context.UserIdentifier;
            await Groups.AddToGroupAsync(Context.ConnectionId, id);
            await base.OnConnectedAsync();
        }
    }
}

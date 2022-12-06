using Microsoft.AspNetCore.SignalR;

namespace Quetta.Logic.Hubs
{
    public class SidebarHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var id = Context.UserIdentifier!;
            await Groups.AddToGroupAsync(Context.ConnectionId, id);
            await base.OnConnectedAsync();
        }
    }
}

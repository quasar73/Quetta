using Microsoft.AspNetCore.SignalR;

namespace Quetta.Logic.Hubs
{
    public class MessageHub : Hub
    {
        public async Task SetChat(string chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }
    }
}

using Microsoft.AspNetCore.SignalR;

namespace Quetta.Logic.Hubs
{
    public class ReadHub : Hub
    {
        public async Task SetChat(string chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }
    }
}

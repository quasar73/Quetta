using MediatR;
using Quetta.Common.Models.Responses;

namespace Quetta.Common.Models.Queries
{
    public class GetMessagesQuery : IRequest<ICollection<MessageResponse>>
    {
        public string ChatId { get; set; }

        public string? LastMessageId { get; set; }

        public string UserId { get; set; }

        public int Amount { get; set; }

        public GetMessagesQuery(string chatId, string? lastMessageId, int amount, string userId) 
        {
            ChatId = chatId;
            LastMessageId = lastMessageId;
            Amount = amount;
            UserId = userId;
        }

        public GetMessagesQuery() { }
    }
}

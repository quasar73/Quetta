using MediatR;
using Quetta.Common.Models.Requests;
using Quetta.Common.Models.Responses;

namespace Quetta.Common.Models.Queries
{
    public class GetMessagesQuery : IRequest<ICollection<MessageResponse>>
    {
        public string ChatId { get; set; }

        public string? LastMessageId { get; set; }

        public string UserId { get; set; }

        public int Amount { get; set; }

        public GetMessagesQuery(GetMessagesRequest request, string userId) 
        {
            ChatId = request.ChatId;
            LastMessageId = request.LastMessageId;
            Amount = request.Amount;
            UserId = userId;
        }

        public GetMessagesQuery() { }
    }
}

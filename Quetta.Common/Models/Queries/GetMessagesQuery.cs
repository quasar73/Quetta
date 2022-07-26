using MediatR;
using Quetta.Common.Models.Responses;

namespace Quetta.Common.Models.Queries
{
    public class GetMessagesQuery : IRequest<ICollection<MessageResponse>>
    {
        public string ChatId { get; set; }

        public GetMessagesQuery(string chatId) 
        {
            this.ChatId = chatId;
        }
    }
}

using MediatR;
using Quetta.Common.Models.Responses;

namespace Quetta.Common.Models.Queries
{
    public class GetChatInfoQuery : IRequest<ChatInfoResponse>
    {
        public string ChatId { get; set; }

        public string UserId { get; set; }
    }
}

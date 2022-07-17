using MediatR;
using Quetta.Common.Models.Responses;

namespace Quetta.Common.Models.Queries
{
    public class GetChatsQuery : IRequest<ICollection<ChatItemResponse>>
    {
        public string UserId { get; set; }

        public GetChatsQuery(string userId)
        {
            UserId = userId;
        }
    }
}

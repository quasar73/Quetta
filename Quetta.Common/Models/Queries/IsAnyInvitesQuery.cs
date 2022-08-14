using Quetta.Common.Models.Responses;
using MediatR;

namespace  Quetta.Common.Models.Queries
{
    public class IsAnyInvitesQuery : IRequest<IsAnyInvitesResponse>
    {
        public string UserId { get; set; }

        public IsAnyInvitesQuery(string userId)
        {
            UserId = userId;
        }
    }
}

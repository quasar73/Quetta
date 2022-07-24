using Quetta.Common.Models.Responses;
using MediatR;

namespace  Quetta.Common.Models.Queries
{
    public class IsAnyNotificationsQuery : IRequest<IsAnyNotificationsResponse>
    {
        public string UserId { get; set; }

        public IsAnyNotificationsQuery(string userId)
        {
            UserId = userId;
        }
    }
}

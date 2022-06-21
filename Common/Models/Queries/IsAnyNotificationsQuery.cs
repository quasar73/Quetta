using Common.Models.Responses;
using MediatR;

namespace Common.Models.Queries
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

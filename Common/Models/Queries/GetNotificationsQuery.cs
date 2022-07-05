
using Common.Models.Responses;
using MediatR;

namespace Common.Models.Queries
{
    public class GetNotificationsQuery : IRequest<NotificationResponse>
    {
        public string UserId { get; set; }

        public GetNotificationsQuery(string userId)
        {
            UserId = userId;
        }
    }
}

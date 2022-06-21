using Common.Models.Queries;
using Common.Models.Responses;
using Data;
using MediatR;

namespace Logic.Handlers.Queries
{
    public class IsAnyNotificationsHandler : IRequestHandler<IsAnyNotificationsQuery, IsAnyNotificationsResponse>
    {
        private readonly QuettaDbContext dbContext;

        public IsAnyNotificationsHandler(QuettaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IsAnyNotificationsResponse> Handle(IsAnyNotificationsQuery request, CancellationToken cancellationToken)
        {
            var hasNotifications = dbContext.Invites.Any(i => i.ReceiverId == request.UserId);

            return new IsAnyNotificationsResponse
            {
                HasNotifications = hasNotifications,
            };
        }
    }
}

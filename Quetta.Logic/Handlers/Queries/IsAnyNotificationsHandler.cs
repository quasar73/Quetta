﻿using Quetta.Common.Enums;
using Quetta.Common.Models.Queries;
using Quetta.Common.Models.Responses;
using Quetta.Data;
using MediatR;

namespace Quetta.Logic.Handlers.Queries
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
            var hasNotifications = dbContext.Invites.Any(i => i.ReceiverId == request.UserId && i.Status == InviteStatus.Pending);

            return new IsAnyNotificationsResponse
            {
                HasNotifications = hasNotifications,
            };
        }
    }
}

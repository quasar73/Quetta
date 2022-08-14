using Quetta.Common.Enums;
using Quetta.Common.Models.Queries;
using Quetta.Common.Models.Responses;
using Quetta.Data;
using MediatR;

namespace Quetta.Logic.Handlers.Queries
{
    public class IsAnyInvitesHandler : IRequestHandler<IsAnyInvitesQuery, IsAnyInvitesResponse>
    {
        private readonly QuettaDbContext dbContext;

        public IsAnyInvitesHandler(QuettaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IsAnyInvitesResponse> Handle(IsAnyInvitesQuery request, CancellationToken cancellationToken)
        {
            var hasInvites = dbContext.Invites.Any(i => i.ReceiverId == request.UserId && i.Status == InviteStatus.Pending);

            return new IsAnyInvitesResponse
            {
                HasInvites = hasInvites,
            };
        }
    }
}

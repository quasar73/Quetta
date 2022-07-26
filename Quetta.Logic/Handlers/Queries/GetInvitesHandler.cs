using AutoMapper;
using Quetta.Common.Enums;
using Quetta.Common.Models.Queries;
using Quetta.Common.Models.Responses;
using Quetta.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Quetta.Logic.Handlers.Queries
{
    public class GetInvitesHandler : IRequestHandler<GetInvitesQuery, ICollection<InviteResponse>>
    {
        private readonly QuettaDbContext dbContext;
        private readonly IMapper mapper;

        public GetInvitesHandler(QuettaDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ICollection<InviteResponse>> Handle(GetInvitesQuery request, CancellationToken cancellationToken)
        {
            var invites = dbContext.Invites
                .Include(i => i.Sender)
                .Where(i => i.ReceiverId == request.UserId && i.Status == InviteStatus.Pending)
                .OrderBy(i => i.DateTime)
                .ToList();
            var mappedInvites = mapper.Map<List<InviteResponse>>(invites);

            return mappedInvites;
        }
    }
}

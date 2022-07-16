using AutoMapper;
using Common.Models.Queries;
using Common.Models.Responses;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logic.Handlers.Queries
{
    public class GetInvitesHandler : IRequestHandler<GetInvitesQuery, List<InviteResponse>>
    {
        private readonly QuettaDbContext dbContext;
        private readonly IMapper mapper;

        public GetInvitesHandler(QuettaDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<List<InviteResponse>> Handle(GetInvitesQuery request, CancellationToken cancellationToken)
        {
            var invites = dbContext.Invites
                .Include(i => i.Sender)
                .Where(i => i.ReceiverId == request.UserId)
                .OrderBy(i => i.DateTime)
                .ToList();
            var mappedInvites = mapper.Map<List<InviteResponse>>(invites);

            return mappedInvites;
        }
    }
}

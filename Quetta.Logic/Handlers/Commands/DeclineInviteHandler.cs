using Quetta.Common.Enums;
using Quetta.Common.Exceptions;
using Quetta.Data;
using MediatR;
using Quetta.Common.Models.Commands;

namespace Quetta.Logic.Handlers.Commands
{
    public class DeclineInviteHandler : IRequestHandler<DeclineInviteCommand, Unit>
    {
        private readonly QuettaDbContext dbContext;

        public DeclineInviteHandler(QuettaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeclineInviteCommand request, CancellationToken cancellationToken)
        {
            var invite = dbContext.Invites.FirstOrDefault(i => i.Id == request.InviteId);

            if (invite == null || invite.ReceiverId != request.ReceiverId)
            {
                throw new EntityNotFoundException();
            }

            invite.Status = InviteStatus.Declined;
            dbContext.Invites.Update(invite);

            await dbContext.SaveChangesAsync();
            
            return Unit.Value;
        }
    }
}

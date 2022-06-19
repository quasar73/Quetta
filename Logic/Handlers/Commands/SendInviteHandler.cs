using Common.Models.Commands;
using Data;
using MediatR;

namespace Logic.Handlers.Commands
{
    public class SendInviteHandler : IRequestHandler<SendInviteCommand>
    {
        private readonly QuettaDbContext dbContext;

        public SendInviteHandler(QuettaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<Unit> Handle(SendInviteCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

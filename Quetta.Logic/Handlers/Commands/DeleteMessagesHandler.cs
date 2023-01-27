using MediatR;
using Microsoft.EntityFrameworkCore;
using Quetta.Common.Exceptions;
using Quetta.Common.Models.Commands;
using Quetta.Data;

namespace Quetta.Logic.Handlers.Commands
{
    public class DeleteMessagesHandler : IRequestHandler<DeleteMessagesCommand, Unit>
    {
        private readonly QuettaDbContext dbContext;

        public DeleteMessagesHandler(QuettaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(
            DeleteMessagesCommand request,
            CancellationToken cancellationToken
        )
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    request.MessageIds.ForEach(
                        (messageId) =>
                        {
                            var message = dbContext.Messages
                                .Include(m => m.Chat)
                                    .ThenInclude(c => c.Users)
                                .FirstOrDefault(m => m.Id == messageId);

                            if (message == null)
                            {
                                throw new EntityNotFoundException();
                            }

                            if (!message.Chat.Users.Any(u => u.Id == request.UserId))
                            {
                                throw new AccessDeniedException();
                            }

                            dbContext.Messages.Remove(message);
                        }
                    );

                    await dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return Unit.Value;
        }
    }
}

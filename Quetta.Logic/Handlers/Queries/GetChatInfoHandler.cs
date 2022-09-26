using MediatR;
using Microsoft.EntityFrameworkCore;
using Quetta.Common.Exceptions;
using Quetta.Common.Models.Queries;
using Quetta.Common.Models.Responses;
using Quetta.Data;

namespace Quetta.Logic.Handlers.Queries
{
    public class GetChatInfoHandler : IRequestHandler<GetChatInfoQuery, ChatInfoResponse>
    {
        private readonly QuettaDbContext dbContext;

        public GetChatInfoHandler(QuettaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ChatInfoResponse> Handle(GetChatInfoQuery request, CancellationToken cancellationToken)
        {
            var chat = dbContext.Chats
                .Include(c => c.Users)
                .FirstOrDefault(c => c.Id == request.ChatId);

            if (chat == null)
            {
                throw new EntityNotFoundException();
            }

            if (chat.Users.Any(u => u.Id == request.UserId))
            {
                var title = chat.IsGroup
                    ? chat.Title
                    : chat.Users
                        .Where(u => u.Id != request.UserId)
                        .Select(u => $"{u.FirstName} {u.LastName}")
                        .First();

                var info = new ChatInfoResponse
                {
                    IsGroup = chat.IsGroup,
                    Members = chat.Users.Count(),
                    Title = title!
                };

                return info;
            }

            throw new AccessDeniedException();
        }
    }
}

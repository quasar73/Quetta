using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Quetta.Common.Exceptions;
using Quetta.Common.Models.Queries;
using Quetta.Common.Models.Responses;
using Quetta.Data;
using Quetta.Data.Models;

namespace Quetta.Logic.Handlers.Queries
{
    public class GetMessagesHandler
        : IRequestHandler<GetMessagesQuery, ICollection<MessageResponse>>
    {
        private readonly IMapper mapper;
        private readonly QuettaDbContext dbContext;

        public GetMessagesHandler(QuettaDbContext dbContext, IMapper mapper)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<ICollection<MessageResponse>> Handle(
            GetMessagesQuery request,
            CancellationToken cancellationToken
        )
        {
            var allMessages = dbContext.Messages
                .Include(m => m.User)
                .Include(m => m.Chat)
                .ThenInclude(c => c.Users)
                .AsNoTracking()
                .Where(m => m.ChatId == request.ChatId)
                .OrderByDescending(m => m.Date)
                .ToList();

            allMessages.ForEach(m =>
            {
                if (!m.Chat.Users.Any(u => u.Id == request.UserId))
                {
                    throw new AccessDeniedException();
                }
            });

            var loadedMessages = new List<Message>();

            if (string.IsNullOrEmpty(request.LastMessageId))
            {
                loadedMessages = allMessages.Take(request.Amount).ToList();
            }
            else
            {
                var lastMessageIndex = allMessages.FindIndex(m => m.Id == request.LastMessageId);
                loadedMessages = allMessages
                    .Skip(lastMessageIndex + 1)
                    .Take(request.Amount)
                    .ToList();
            }

            var mappedMessages = mapper.Map<List<MessageResponse>>(loadedMessages.OrderBy(m => m.Date).ToList());

            return mappedMessages;
        }
    }
}

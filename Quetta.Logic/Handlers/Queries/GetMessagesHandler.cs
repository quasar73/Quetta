using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quetta.Common.Exceptions;
using Quetta.Common.Models.Queries;
using Quetta.Common.Models.Responses;
using Quetta.Data;
using Quetta.Data.Models;
using Quetta.Logic.Interfaces;

namespace Quetta.Logic.Handlers.Queries
{
    public class GetMessagesHandler
        : IRequestHandler<GetMessagesQuery, ICollection<MessageResponse>>
    {
        private readonly IMapper mapper;
        private readonly QuettaDbContext dbContext;
        private readonly IBaseEncryptingService encryptingService;
        private readonly UserManager<User> userManager;

        public GetMessagesHandler(
            QuettaDbContext dbContext,
            IMapper mapper,
            IBaseEncryptingService encryptingService,
            UserManager<User> userManager
        )
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.encryptingService = encryptingService;
            this.userManager = userManager;
        }

        public async Task<ICollection<MessageResponse>> Handle(
            GetMessagesQuery request,
            CancellationToken cancellationToken
        )
        {
            var allMessages = dbContext.Messages
                .Include(m => m.User)
                .Include(m => m.WhoRead)
                .Include(m => m.Chat)
                .ThenInclude(c => c.Users)
                .AsNoTracking()
                .Where(m => m.ChatId == request.ChatId)
                .OrderByDescending(m => m.Date)
                .ToList();

            allMessages.ForEach(async m =>
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

            var user = await userManager.FindByIdAsync(request.UserId);
            var mappedMessages = mapper.Map<List<MessageResponse>>(
                loadedMessages.OrderBy(m => m.Date).ToList(),
                opt =>
                    opt.AfterMap(
                        (src, dest) =>
                        {
                            var tuple = (src as List<Message>)!
                                .Zip(
                                    dest,
                                    (x, y) => new Tuple<Message, MessageResponse>(x, y)
                                )
                                .ToList();
                            tuple.ForEach(async t =>
                            {
                                var message = t.Item1;
                                var messageResponse = t.Item2;
                                var text = await encryptingService.Decrypt(message.Text, message.SecretVersion);

                                messageResponse.IsSupported = !string.IsNullOrEmpty(text);
                                messageResponse.IsOwner = messageResponse.Username == user.UserName;
                                messageResponse.Text = text ?? "";
                            });
                        }
                    )
            );

            return mappedMessages;
        }
    }
}

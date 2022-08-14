using AutoMapper;
using Quetta.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Quetta.Common.Models.Queries;
using Quetta.Common.Models.Responses;
using Quetta.Common.Enums;

namespace Quetta.Logic.Handlers.Queries
{
    public class GetChatsHandler : IRequestHandler<GetChatsQuery, ICollection<ChatItemResponse>>
    {
        private readonly QuettaDbContext dbContext;
        private readonly IMapper mapper;

        public GetChatsHandler(QuettaDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ICollection<ChatItemResponse>> Handle(GetChatsQuery request, CancellationToken cancellationToken)
        {
            var userChats = dbContext.Chats
                            .Include(c => c.Users)
                            .Include(c => c.Messages)
                            .Where(c => c.Users.Any(u => u.Id == request.UserId)).ToList();

            var mappedChats = mapper.Map<IList<ChatItemResponse>>(userChats, opt => opt.AfterMap((src, dest) =>
            {
                dest.ToList().ForEach((chat) =>
                {
                    if (chat.ChatType == ChatType.PersonalChat)
                    {
                        var user = dbContext.Chats.First(c => c.Id == chat.Id)?.Users.FirstOrDefault(u => u.Id != request.UserId);
                        chat.Title = $"{user?.FirstName} {user?.LastName}";
                    }
                    else
                    {
                        chat.Title = dbContext.Chats.First(c => c.Id == chat.Id).Title!;
                    }
                });
            }));

            return mappedChats;
        }
    }
}

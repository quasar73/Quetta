using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Quetta.Common.Models.Queries;
using Quetta.Common.Models.Responses;
using Quetta.Data;

namespace Quetta.Logic.Handlers.Queries
{
    public class GetMessagesHandler : IRequestHandler<GetMessagesQuery, ICollection<MessageResponse>>
    {
        private readonly IMapper mapper;
        private readonly QuettaDbContext dbContext;
        
        public GetMessagesHandler(IMapper mapper, QuettaDbContext dbContext)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<ICollection<MessageResponse>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = dbContext.Messages.Include(m => m.User).AsNoTracking().Where(m => m.ChatId == request.ChatId).ToList();
            var mappedMessages = mapper.Map<List<MessageResponse>>(messages);

            return mappedMessages;
        }
    }
}

using AutoMapper;
using FluentAssertions;
using Quetta.Common.Enums;
using Quetta.Common.Models.Queries;
using Quetta.Common.Models.Responses;
using Quetta.Data;
using Quetta.Data.Mapping;
using Quetta.Logic.Handlers.Queries;
using Quetta.Tests.Handlers.Data;

namespace Quetta.Tests.Handlers.Queries
{
    public class GetChatsHandlerTests
    {
        [Fact]
        public async void GetChats_ShouldReturnChatsList()
        {
            using (var dbContextProvider = new TestQuettaDbContextProvider())
            {
                var dbContext = dbContextProvider.DbContext;
                await InitializeDatabase(dbContext);
                var expectedResult = new List<ChatItemResponse>()
                {
                    new ChatItemResponse()
                    {
                        Id = ("00000000-0000-0000-0000-000000000000"),
                        ChatType = ChatType.PersonalChat,
                        LastMessage = "This is last message 1",
                        Title = "First Last",
                    },
                    new ChatItemResponse()
                    {
                        Id = ("00000000-0000-0000-0000-000000000001"),
                        ChatType = ChatType.GroupChat,
                        LastMessage = "This is last message 2",
                        Title = "Test Group",
                    },
                };

                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new ChatProfile());
                });
                var mapper = mockMapper.CreateMapper();
                var handler = new GetChatsHandler(dbContext, mapper);
                var query = new GetChatsQuery("00000000-0000-0000-0000-000000000000");

                var actualResult = await handler.Handle(query, new CancellationToken());

                actualResult.Should().BeEquivalentTo(expectedResult);
            }
        }

        private async Task InitializeDatabase(QuettaDbContext dbContext)
        {
            var messages = ChatsTestData.TestMessages;
            var chats = ChatsTestData.TestChats;
            var users = ChatsTestData.TestUsers;

            var pairs = messages.Zip(chats, (m, c) => Tuple.Create(m, c));

            await dbContext.Users.AddRangeAsync(users);

            foreach (var pair in pairs)
            {
                var chat = pair.Item2;
                var message = pair.Item1;

                message.User = users.First();
                chat.Messages.Add(message);
                users.ForEach((user) => chat.Users.Add(user));

                await dbContext.Messages.AddAsync(message);
                await dbContext.Chats.AddAsync(chat);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}

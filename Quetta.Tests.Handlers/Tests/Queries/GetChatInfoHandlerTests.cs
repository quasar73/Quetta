using FluentAssertions;
using Quetta.Common.Exceptions;
using Quetta.Common.Models.Queries;
using Quetta.Common.Models.Responses;
using Quetta.Data;
using Quetta.Data.Models;
using Quetta.Logic.Handlers.Queries;

namespace Quetta.Tests.Handlers.Queries
{
    public class GetChatInfoHandlerTests
    {
        private List<User> testUsers = new List<User>()
        {
            new User()
            {
                Id = "00000000-0000-0000-0000-000000000000",
                UserName = "username",
                FirstName = "First",
                LastName = "Last",
                Email = "username@email.com"
            },
            new User()
            {
                Id = "00000000-0000-0000-0000-000000000001",
                UserName = "username2",
                FirstName = "First",
                LastName = "Last",
                Email = "username2@mail.com"
            }
        };

        [Fact]
        public async void GetChatInfoQuery_RerurnsChatInfo()
        {
            using (var dbContextProvider = new TestQuettaDbContextProvider())
            {
                var dbContext = dbContextProvider.DbContext;
                await IntializeDatabase(dbContext);
                var expectedResult = new ChatInfoResponse()
                {
                    IsGroup = false,
                    Members = 2,
                    Title = "First Last"
                };
                var handler = new GetChatInfoHandler(dbContext);
                var query = new GetChatInfoQuery()
                {
                    ChatId = "00000000-0000-0000-0000-000000000000",
                    UserId = "00000000-0000-0000-0000-000000000000",
                };

                var actualResult = await handler.Handle(query, new CancellationToken());

                actualResult.Should().BeEquivalentTo(expectedResult);
            }
        }

        [Fact]
        public async void GetChatInfoQueryWithInvalidChatId_ShouldThrowsEntityNotFoundException()
        {
            using (var dbContextProvider = new TestQuettaDbContextProvider())
            {
                var dbContext = dbContextProvider.DbContext;
                await IntializeDatabase(dbContext);
                var expectedResult = new ChatInfoResponse()
                {
                    IsGroup = false,
                    Members = 2,
                    Title = "First Last"
                };
                var handler = new GetChatInfoHandler(dbContext);
                var query = new GetChatInfoQuery()
                {
                    ChatId = "00000000-0000-0000-0000-000000000001",
                    UserId = "00000000-0000-0000-0000-000000000000",
                };


                await Assert.ThrowsAsync<EntityNotFoundException>(async () => await handler.Handle(query, new CancellationToken()));
            }
        }

        [Fact]
        public async void GetChatInfoQueryWithInvalidUserId_ShouldThrowsAccessDeniedException()
        {
            using (var dbContextProvider = new TestQuettaDbContextProvider())
            {
                var dbContext = dbContextProvider.DbContext;
                await IntializeDatabase(dbContext);
                var expectedResult = new ChatInfoResponse()
                {
                    IsGroup = false,
                    Members = 2,
                    Title = "First Last"
                };
                var handler = new GetChatInfoHandler(dbContext);
                var query = new GetChatInfoQuery()
                {
                    ChatId = "00000000-0000-0000-0000-000000000000",
                    UserId = "00000000-0000-0000-0000-000000000002",
                };


                await Assert.ThrowsAsync<AccessDeniedException>(async () => await handler.Handle(query, new CancellationToken()));
            }
        }

        private async Task IntializeDatabase(QuettaDbContext dbContext)
        {
            await dbContext.Users.AddRangeAsync(testUsers);
            var testChat = new Chat()
            {
                Id = "00000000-0000-0000-0000-000000000000",
                Users = testUsers,
                IsGroup = false,
            };
            await dbContext.Chats.AddAsync(testChat);
            await dbContext.SaveChangesAsync();
        }
    }
}

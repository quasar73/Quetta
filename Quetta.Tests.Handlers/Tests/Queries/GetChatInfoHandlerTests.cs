using Newtonsoft.Json;
using Quetta.Common.Exceptions;
using Quetta.Common.Models.Queries;
using Quetta.Common.Models.Responses;
using Quetta.Data.Models;
using Quetta.Logic.Handlers.Queries;

namespace Quetta.Tests.Handlers.Queries
{
    public class GetChatInfoHandlerTests
    {
        private TestQuettaDbContextFactory factory;
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
                Id = "10000000-0000-0000-0000-000000000000",
                UserName = "username2",
                FirstName = "First",
                LastName = "Last",
                Email = "username2@mail.com"
            }
        };

        public GetChatInfoHandlerTests()
        {
            factory = new TestQuettaDbContextFactory();

            using (var dbContext = factory.Create())
            {
                var chat = new Chat()
                {
                    Id = "00000000-0000-0000-0000-000000000000",
                    Users = testUsers,
                    IsGroup = false,
                };
                dbContext.AddRange(testUsers);
                dbContext.Add(chat);
                dbContext.SaveChanges();
            }
        }

        [Fact]
        public async void GetChatInfoQuery_RerurnsChatInfo()
        {
            using(var dbContext = factory.Create())
            {
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

                var result = await handler.Handle(query, new CancellationToken());

                var expectedResultStr = JsonConvert.SerializeObject(expectedResult);
                var resultStr = JsonConvert.SerializeObject(result);
                Assert.Equal(expectedResultStr, resultStr);
                dbContext.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async void GetChatInfoQueryWithInvalidChatId_ShouldThrowsEntityNotFoundException()
        {
            using (var dbContext = factory.Create())
            {
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
                dbContext.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async void GetChatInfoQueryWithInvalidUserId_ShouldThrowsAccessDeniedException()
        {
            using (var dbContext = factory.Create())
            {
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
                    UserId = "00000000-0000-0000-0000-000000000001",
                };


                await Assert.ThrowsAsync<AccessDeniedException>(async () => await handler.Handle(query, new CancellationToken()));
                dbContext.Database.EnsureDeleted();
            }
        }
    }
}

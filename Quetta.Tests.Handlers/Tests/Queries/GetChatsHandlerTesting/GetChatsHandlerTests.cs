using AutoMapper;
using FluentAssertions;
using Quetta.Data.Mapping;
using Quetta.Logic.Handlers.Queries;

namespace Quetta.Tests.Handlers.Queries
{
    public class GetChatsHandlerTests
    {
        private readonly IMapper mapper;

        public GetChatsHandlerTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ChatProfile());
            });
            mapper = mockMapper.CreateMapper();
        }

        [Theory]
        [ClassData(typeof(ChatsIsNotFoundTestCase))]
        [ClassData(typeof(ReturnedChatsTestCase))]
        public async void GetChats_ShouldReturnChatsList(GetChatsTestCaseModel model)
        {
            using (var dbContextProvider = new TestQuettaDbContextProvider())
            {
                // Arrange
                var dbContext = dbContextProvider.DbContext;
                dbContext.Users.AddRange(model.UsersToAdd);
                dbContext.Chats.AddRange(model.ChatsToAdd);
                dbContext.Messages.AddRange(model.MessagesToAdd);
                await dbContext.SaveChangesAsync();

                // Act
                var handler = new GetChatsHandler(dbContext, mapper);
                var actualResult = await handler.Handle(model.IncomingQuery, new CancellationToken());

                // Assert
                actualResult.Should().BeEquivalentTo(model.ExpectedReturnedChats);
            }
        }
    }
}

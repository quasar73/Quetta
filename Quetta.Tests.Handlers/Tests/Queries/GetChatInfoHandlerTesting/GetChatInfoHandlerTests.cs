using FluentAssertions;
using Quetta.Logic.Handlers.Queries;

namespace Quetta.Tests.Handlers.Queries
{
    public class GetChatInfoHandlerTests
    {
        [Theory]
        [ClassData(typeof(GetChatInfoTestCases))]
        public async void GetChatInfoQuery_RerurnsChatInfo(GetChatInfoTestCaseModel model)
        {
            using (var dbContextProvider = new TestQuettaDbContextProvider())
            {
                // Arrange
                var dbContext = dbContextProvider.DbContext;
                dbContext.Users.AddRange(model.UsersToAdd);
                dbContext.Chats.Add(model.ChatToAdd);
                await dbContext.SaveChangesAsync();
                var handler = new GetChatInfoHandler(dbContext);

                // Act
                var actualResult = await handler.Handle(model.IncomingQuery, new CancellationToken());

                // Assert
                actualResult.Should().BeEquivalentTo(model.ExpectedChatInfo);
            }
        }

        [Theory]
        [ClassData(typeof(GetChatInfoExceptionTestCases))]
        public async void GetChatInfoQuery_ShouldThrowsException(GetChatInfoTestExceptionModel model)
        {
            using (var dbContextProvider = new TestQuettaDbContextProvider())
            {
                // Arrange
                var dbContext = dbContextProvider.DbContext;
                dbContext.Users.AddRange(model.UsersToAdd);
                dbContext.Chats.Add(model.ChatToAdd);
                await dbContext.SaveChangesAsync();
                var handler = new GetChatInfoHandler(dbContext);

                // Act
                var act = async () => await handler.Handle(model.IncomingQuery, new CancellationToken());

                // Asset
                await act.Should().ThrowAsync<Exception>()
                    .Where(ex => ex.GetType() == model.ExceptionType)
                    .WithMessage(model.ExceptionMessage);
            }
        }
    }
}

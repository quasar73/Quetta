using FluentAssertions;
using Quetta.Logic.Handlers.Commands;

namespace Quetta.Tests.Handlers.Commands
{
    public class SendMessageHandlerTests
    {
        [Theory]
        [ClassData(typeof(SendMessageTestCase))]
        public async void SendMessage_ShouldCreateNewMessage(SendMessageTestCaseModel model)
        {
            using(var dbContextProvider = new TestQuettaDbContextProvider())
            {
                // Arrange
                var dbContext = dbContextProvider.DbContext;
                var handler = new SendMessageHandler(dbContext);

                // Act
                var id = await handler.Handle(model.IncomingCommand, new CancellationToken());
                var message = dbContext.Messages.FirstOrDefault(m => m.Id == id);

                // Assert
                message.Should().NotBeNull();
                model.ExpectedResult.Should().BeEquivalentTo(message, options => options
                    .Excluding(m => m!.Id)
                    .Excluding(m => m!.Chat)
                    .Excluding(m => m!.User));
            }
        }
    }
}

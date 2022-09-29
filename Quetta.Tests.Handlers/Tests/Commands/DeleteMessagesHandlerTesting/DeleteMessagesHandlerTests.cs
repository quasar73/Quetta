using FluentAssertions;
using Quetta.Logic.Handlers.Commands;

namespace Quetta.Tests.Handlers.Commands
{
    public class DeleteMessagesHandlerTests
    {
        [Theory]
        [ClassData(typeof(DeleteMessagesTestCase))]
        public async void DeleteMessages_ShouldDeleteMessages(DeleteMessagesTestCaseModel model)
        {
            using (var dbContextProvider = new TestQuettaDbContextProvider())
            {
                // Arrange
                var dbContext = dbContextProvider.DbContext;
                dbContext.Users.AddRange(model.UsersToAdd);
                dbContext.Chats.AddRange(model.ChatsToAdd);
                dbContext.Messages.AddRange(model.MessagesToAdd);
                await dbContext.SaveChangesAsync();
                var handler = new DeleteMessagesHandler(dbContext);

                // Act
                await handler.Handle(model.IncomingCommand, new CancellationToken());
                var remainingMessages = dbContext.Messages.ToList();

                // Assert
                remainingMessages.Should().NotBeNull();
                remainingMessages!.Count.Should().Be(model.ExpectedAmount);
            }
        }

        [Theory]
        [ClassData(typeof(DeleteMessagesExcetpionTestCase))]
        public async void DeleteMessages_ShouldThrowsException(DeleteMessagesExceptionCaseModel model)
        {
            using (var dbContextProvider = new TestQuettaDbContextProvider())
            {
                // Arrange
                var dbContext = dbContextProvider.DbContext;
                dbContext.Users.AddRange(model.UsersToAdd);
                dbContext.Chats.AddRange(model.ChatsToAdd);
                dbContext.Messages.AddRange(model.MessagesToAdd);
                await dbContext.SaveChangesAsync();
                var handler = new DeleteMessagesHandler(dbContext);

                // Act
                var act = async () => await handler.Handle(model.IncomingCommand, new CancellationToken());

                // Assert
                await act.Should().ThrowAsync<Exception>()
                    .Where(ex => ex.GetType() == model.ExceptionType)
                    .WithMessage(model.ExceptionMessage);
            }
        }
    }
}

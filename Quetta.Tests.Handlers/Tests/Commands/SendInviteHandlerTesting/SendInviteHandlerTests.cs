using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using Quetta.Data.Models;
using Quetta.Logic.Handlers.Commands;

namespace Quetta.Tests.Handlers.Commands
{
    public class SendInviteHandlerTests
    {
        private readonly UserManager<User> userManager;

        public SendInviteHandlerTests()
        {
            var userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((string username) =>
            {
                return username == "username2" ? new User()
                {
                    Id = "00000000-0000-0000-0000-000000000001",
                    UserName = username,
                    FirstName = "First",
                    LastName = "Last",
                } : null;
            });
            userManager = userManagerMock.Object;
        }

        [Theory]
        [ClassData(typeof(SendInviteTestCase))]
        public async void SendInvite_ShouldCreateNewInvite(SendInviteTestCaseModel model)
        {
            using(var dbContextProvider = new TestQuettaDbContextProvider())
            {
                // Arrange
                var dbContext = dbContextProvider.DbContext;
                var handler = new SendInviteHandler(dbContext, userManager);

                // Act
                await handler.Handle(model.IncomingCommand, new CancellationToken());
                var invite = dbContext.Invites.FirstOrDefault();

                // Assert
                invite.Should().NotBeNull();
                invite!.SenderId.Should().Be(model.SenderId);
                invite!.ReceiverId.Should().Be(model.ReceiverId);
            }
        }

        [Theory]
        [ClassData(typeof(SendInviteExceptionTestCase))]
        public async void SendInvite_ShouldThrowsException(SendInviteExceptionCaseModel model)
        {
            using (var dbContextProvider = new TestQuettaDbContextProvider())
            {
                // Arrange
                var dbContext = dbContextProvider.DbContext;
                dbContext.Invites.AddRange(model.InvitesToAdd);
                await dbContext.SaveChangesAsync();
                var handler = new SendInviteHandler(dbContext, userManager);

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

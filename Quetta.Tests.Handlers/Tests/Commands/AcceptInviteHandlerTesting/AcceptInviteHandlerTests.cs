using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Quetta.Common.Enums;
using Quetta.Common.Exceptions;
using Quetta.Data.Models;
using Quetta.Logic.Handlers.Commands;

namespace Quetta.Tests.Handlers.Commands
{
    public class AcceptInviteHandlerTests
    {
        private readonly UserManager<User> userManager;

        public AcceptInviteHandlerTests()
        {
            var userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((string id) =>
            {
                return AcceptInviteTestUsers.TestUsers.FirstOrDefault(u => u.Id == id);
            });
            userManager = userManagerMock.Object;
        }

        [Theory]
        [ClassData(typeof(AcceptInviteForPersonalChatTestCase))]
        public async void AcceptInviteForPersonalChat_ShouldCreateNewChat(AcceptInviteTestCaseModel model)
        {
            using(var dbContextProvider = new TestQuettaDbContextProvider())
            {
                // Arrange
                var dbContext = dbContextProvider.DbContext;
                dbContext.Users.AddRange(model.UsersToAdd);
                dbContext.Invites.AddRange(model.InvitesToAdd);
                await dbContext.SaveChangesAsync();
                var handler = new AcceptInviteHandler(dbContext, userManager);

                // Act
                await handler.Handle(model.IncomingCommand, new CancellationToken());
                var acceptedInvite = dbContext.Invites.FirstOrDefault(i => i.Id == model.AcceptedInviteId);
                var createdChat = dbContext.Chats.Include(c => c.Users).FirstOrDefault();

                // Assert
                acceptedInvite.Should().NotBeNull();
                acceptedInvite!.Status.Should().Be(InviteStatus.Accepted);
                createdChat.Should().NotBeNull();
                createdChat!.Users.Should().BeEquivalentTo(AcceptInviteTestUsers.TestUsers);
            }
        }

        [Theory]
        [ClassData(typeof(AcceptInviteExpcetionTestCase))]
        public async void AcceptInvite_ShouldThrowsException(AcceptInviteTestCaseModel model)
        {
            using (var dbContextProvider = new TestQuettaDbContextProvider())
            {
                // Arrange
                var dbContext = dbContextProvider.DbContext;
                dbContext.Users.AddRange(model.UsersToAdd);
                dbContext.Invites.AddRange(model.InvitesToAdd);
                await dbContext.SaveChangesAsync();
                var handler = new AcceptInviteHandler(dbContext, userManager);

                // Act
                var act = async () => await handler.Handle(model.IncomingCommand, new CancellationToken());

                // Assert
                await act.Should().ThrowAsync<EntityNotFoundException>()
                    .WithMessage("Entity not found.");
            }
        }
    }
}

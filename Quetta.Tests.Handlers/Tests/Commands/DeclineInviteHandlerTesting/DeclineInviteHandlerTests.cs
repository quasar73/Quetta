using FluentAssertions;
using Quetta.Common.Enums;
using Quetta.Common.Exceptions;
using Quetta.Logic.Handlers.Commands;

namespace Quetta.Tests.Handlers.Commands
{
    public class DeclineInviteHandlerTests
    {
        [Theory]
        [ClassData(typeof(DeclineInviteForPersonalChatTestCase))]
        public async void AcceptInviteForPersonalChat_ShouldCreateNewChat(DeclineInviteTestCaseModel model)
        {
            using(var dbContextProvider = new TestQuettaDbContextProvider())
            {
                // Arrange
                var dbContext = dbContextProvider.DbContext;
                dbContext.Invites.AddRange(model.InvitesToAdd);
                await dbContext.SaveChangesAsync();
                var handler = new DeclineInviteHandler(dbContext);

                // Act
                await handler.Handle(model.IncomingCommand, new CancellationToken());
                var acceptedInvite = dbContext.Invites.FirstOrDefault(i => i.Id == model.DeclinedInviteId);

                // Assert
                acceptedInvite.Should().NotBeNull();
                acceptedInvite!.Status.Should().Be(InviteStatus.Declined);
            }
        }

        [Theory]
        [ClassData(typeof(DeclineInviteExpcetionTestCase))]
        public async void AcceptInvite_ShouldThrowsException(DeclineInviteTestCaseModel model)
        {
            using (var dbContextProvider = new TestQuettaDbContextProvider())
            {
                // Arrange
                var dbContext = dbContextProvider.DbContext;
                dbContext.Invites.AddRange(model.InvitesToAdd);
                await dbContext.SaveChangesAsync();
                var handler = new DeclineInviteHandler(dbContext);

                // Act
                var act = async () => await handler.Handle(model.IncomingCommand, new CancellationToken());

                // Assert
                await act.Should().ThrowAsync<EntityNotFoundException>()
                    .WithMessage("Entity not found.");
            }
        }
    }
}

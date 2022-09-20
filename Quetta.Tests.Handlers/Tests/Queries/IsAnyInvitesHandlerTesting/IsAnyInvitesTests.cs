using FluentAssertions;
using Quetta.Logic.Handlers.Queries;

namespace Quetta.Tests.Handlers.Queries
{
    public class IsAnyInvitesTests
    {
        [Theory]
        [ClassData(typeof(HasInvitesTestCase))]
        [ClassData(typeof(HasNoInvitesTestCase))]
        public async void IsAnyInvites_ShouldReturnsResult(IsAnyInvitesTestCaseModel model)
        {
            using(var dbContextProvider = new TestQuettaDbContextProvider())
            {
                // Arrange
                var dbContext = dbContextProvider.DbContext;
                dbContext.Invites.AddRange(model.InvitesToAdd);
                await dbContext.SaveChangesAsync();
                var handler = new IsAnyInvitesHandler(dbContext);

                // Act
                var actualResult = await handler.Handle(model.IncomingQuery, new CancellationToken());

                // Assert
                actualResult.Should().BeEquivalentTo(model.ExpectedResult);
            }
        }
    }
}

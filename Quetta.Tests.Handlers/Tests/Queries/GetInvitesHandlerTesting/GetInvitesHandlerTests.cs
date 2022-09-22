using AutoMapper;
using FluentAssertions;
using Quetta.Data.Mapping;
using Quetta.Logic.Handlers.Queries;

namespace Quetta.Tests.Handlers.Queries
{
    public class GetInvitesHandlerTests
    {
        private readonly IMapper mapper;

        public GetInvitesHandlerTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new InviteProfile());
            });
            mapper = mockMapper.CreateMapper();
        }

        [Theory]
        [ClassData(typeof(GetInvitesTestCases))]
        public async void GetInvites_ShouldReturnsInvitesList(GetInvitesTestCaseModel model)
        {
            using (var dbContextProvider = new TestQuettaDbContextProvider())
            {
                // Arrange
                var dbContext = dbContextProvider.DbContext;
                dbContext.Users.AddRange(model.UsersToAdd);
                dbContext.Invites.AddRange(model.InvitesToAdd);
                await dbContext.SaveChangesAsync();
                var handler = new GetInvitesHandler(dbContext, mapper);

                // Act
                var actualResult = await handler.Handle(model.IncomingQuery, new CancellationToken());

                // Assert
                actualResult.Should().BeEquivalentTo(model.ExpectedReturnedInvites);
            }
        }
    }
}

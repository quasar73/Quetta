using AutoMapper;
using FluentAssertions;
using Quetta.Data.Mapping;
using Quetta.Logic.Handlers.Queries;

namespace Quetta.Tests.Handlers.Queries
{
    public class GetMessagesHandlerTests
    {
        private readonly IMapper mapper;

        public GetMessagesHandlerTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MessageProfile());
            });
            mapper = mockMapper.CreateMapper();
        }

        [Theory]
        [ClassData(typeof(GetMessagesTestCases))]
        public async void GetMessages_ShouldReturnsMessagesList(GetMessagesTestCaseModel model)
        {
            using (var dbContextProvider = new TestQuettaDbContextProvider())
            {
                // Arrange
                var dbContext = dbContextProvider.DbContext;
                dbContext.Users.AddRange(model.UsersToAdd);
                dbContext.Messages.AddRange(model.MessagesToAdd);
                await dbContext.SaveChangesAsync();
                var handler = new GetMessagesHandler(dbContext, mapper);

                // Act
                var actualResult = await handler.Handle(model.IncomingQuery, new CancellationToken());

                // Assert
                actualResult.Should().BeEquivalentTo(model.ExpectedReturnedMessages);
            }
        }
    }
}

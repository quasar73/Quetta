using AutoMapper;
using FluentAssertions;
using Moq;
using Quetta.Data.Mapping;
using Quetta.Logic.Handlers.Queries;
using Quetta.Logic.Interfaces;

namespace Quetta.Tests.Handlers.Queries
{
    public class GetMessagesHandlerTests
    {
        private readonly IMapper mapper;
        private readonly IBaseEncryptingService encryptingService;

        public GetMessagesHandlerTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MessageProfile());
            });
            mapper = mockMapper.CreateMapper();
            var encryptingServiceMock = new Mock<IBaseEncryptingService>();
            encryptingServiceMock.Setup(x => x.Decrypt(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(() =>
            {
                return "Decrypted message.";
            });
            encryptingService = encryptingServiceMock.Object;
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
                var handler = new GetMessagesHandler(dbContext, mapper, encryptingService);

                // Act
                var actualResult = await handler.Handle(model.IncomingQuery, new CancellationToken());

                // Assert
                actualResult.Should().BeEquivalentTo(model.ExpectedReturnedMessages);
            }
        }
    }
}

using AutoMapper;
using FluentAssertions;
using Moq;
using Quetta.Data.Mapping;
using Quetta.Logic.Handlers.Queries;
using Quetta.Logic.Interfaces;

namespace Quetta.Tests.Handlers.Queries
{
    public class GetChatsHandlerTests
    {
        private readonly IMapper mapper;
        private readonly IBaseEncryptingService encryptingService;

        public GetChatsHandlerTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ChatProfile());
            });
            mapper = mockMapper.CreateMapper();

            var encryptingServiceMock = new Mock<IBaseEncryptingService>();
            encryptingServiceMock
                .Setup(x => x.Decrypt(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() =>
                {
                    return "Decrypted message.";
                });
            encryptingService = encryptingServiceMock.Object;
        }

        [Theory]
        [ClassData(typeof(ChatsIsNotFoundTestCase))]
        [ClassData(typeof(ReturnedChatsTestCase))]
        public async void GetChats_ShouldReturnChatsList(GetChatsTestCaseModel model)
        {
            using (var dbContextProvider = new TestQuettaDbContextProvider())
            {
                // Arrange
                var dbContext = dbContextProvider.DbContext;
                dbContext.Users.AddRange(model.UsersToAdd);
                dbContext.Chats.AddRange(model.ChatsToAdd);
                dbContext.Messages.AddRange(model.MessagesToAdd);
                await dbContext.SaveChangesAsync();
                var handler = new GetChatsHandler(dbContext, mapper, encryptingService);

                // Act
                var actualResult = await handler.Handle(model.IncomingQuery, new CancellationToken());

                // Assert
                actualResult.Should().BeEquivalentTo(model.ExpectedReturnedChats);
            }
        }
    }
}

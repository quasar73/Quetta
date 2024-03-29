﻿using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Quetta.Logic.Handlers.Commands;
using Quetta.Logic.Interfaces;

namespace Quetta.Tests.Handlers.Commands
{
    public class SendMessageHandlerTests
    {
        private readonly IBaseEncryptingService encryptingService;
        private readonly IConfiguration configuration;

        public SendMessageHandlerTests()
        {
            var encryptingServiceMock = new Mock<IBaseEncryptingService>();
            encryptingServiceMock
                .Setup(x => x.Encrypt(It.IsAny<string>()))
                .ReturnsAsync(() =>
                {
                    return "Encrypted message.";
                });
            encryptingService = encryptingServiceMock.Object;

            var config = new Dictionary<string, string>
            {
                { "Crypt:ActualSecret", "actualSecrete" },
            };
            configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
        }

        [Theory]
        [ClassData(typeof(SendMessageTestCase))]
        public async void SendMessage_ShouldCreateNewMessage(SendMessageTestCaseModel model)
        {
            using (var dbContextProvider = new TestQuettaDbContextProvider())
            {
                // Arrange
                var dbContext = dbContextProvider.DbContext;
                var handler = new SendMessageHandler(dbContext, encryptingService, configuration);

                // Act
                var id = await handler.Handle(model.IncomingCommand, new CancellationToken());
                var message = dbContext.Messages.FirstOrDefault(m => m.Id == id);

                // Assert
                message.Should().NotBeNull();
                model.ExpectedResult
                    .Should()
                    .BeEquivalentTo(
                        message,
                        options =>
                            options
                                .Excluding(m => m!.Id)
                                .Excluding(m => m!.Chat)
                                .Excluding(m => m!.User)
                    );
            }
        }
    }
}

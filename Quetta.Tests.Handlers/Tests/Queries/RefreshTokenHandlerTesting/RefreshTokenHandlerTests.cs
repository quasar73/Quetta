using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using Quetta.Common.Models;
using Quetta.Data.Models;
using Quetta.Logic.Handlers.Queries;
using Quetta.Logic.Interfaces;

namespace Quetta.Tests.Handlers.Queries
{
    public class RefreshTokenHandlerTests
    {
        private readonly UserManager<User> userManager;
        private readonly ITokenGenerator tokenGenerator;

        public RefreshTokenHandlerTests()
        {
            var userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((string id) =>
            {
                return id == "00000000-0000-0000-0000-000000000000" ? new User()
                {
                    Id = id,
                    UserName = "username",
                    FirstName = "First",
                    LastName = "Last",
                } : null;
            });
            userManager = userManagerMock.Object;

            var tokenGeneratorMock = new Mock<ITokenGenerator>();
            tokenGeneratorMock.Setup(x => x.GetToken(It.IsAny<User>())).ReturnsAsync(() =>
            {
                return new TokenModel()
                {
                    AccessToken = "SOMEACCESSTOKEN",
                    RefreshToken = "SOMEREFRESHTOKEN",
                };
            });
            tokenGenerator = tokenGeneratorMock.Object;
        }

        [Theory]
        [ClassData(typeof(RefreshTokenTestCase))]
        public async void RefreshToken_ShouldReturnNewTokens(RefreshTokenTestCaseModel model)
        {
            using(var dbContextProvider = new TestQuettaDbContextProvider())
            {
                // Arrange
                var dbContext = dbContextProvider.DbContext;
                dbContext.RefreshTokens.AddRange(model.RefreshTokensToAdd);
                await dbContext.SaveChangesAsync();
                var handler = new RefreshTokenHandler(userManager, dbContext, tokenGenerator);

                // Act
                var actualResult = await handler.Handle(model.IncomingQuery, new CancellationToken());

                // Assert
                actualResult.Should().BeEquivalentTo(model.ExpectedTokens);
            }
        }

        [Theory]
        [ClassData(typeof(RefreshTokenExceptionTestCases))]
        public async void RefreshToken_ShouldThrowsException(RefreshTokenExceptionCaseModel model)
        {
            using (var dbContextProvider = new TestQuettaDbContextProvider())
            {
                // Arrange
                var dbContext = dbContextProvider.DbContext;
                dbContext.RefreshTokens.AddRange(model.RefreshTokensToAdd);
                await dbContext.SaveChangesAsync();
                var handler = new RefreshTokenHandler(userManager, dbContext, tokenGenerator);

                // Act
                var act = async () => await handler.Handle(model.IncomingQuery, new CancellationToken());

                // Asset
                await act.Should().ThrowAsync<Exception>()
                    .Where(ex => ex.GetType() == model.ExceptionType)
                    .WithMessage(model.ExceptionMessage);
            }
        }
    }
}

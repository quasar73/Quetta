using Microsoft.AspNetCore.Identity;
using Moq;
using Quetta.Common.Models.Queries;
using Quetta.Data.Models;
using Quetta.Logic.Handlers.Queries;

namespace Quetta.Tests.Handlers.Queries
{
    public class CheckOutUsernameHandlerTests
    {
        [Theory]
        [InlineData("username1", true)]
        [InlineData("username2", false)]
        public async void CheckOutUsernameQuery_RerurnsIsUsernameAvailable(string username, bool isAvailableExpected)
        {
            var userManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((string username) =>
            {
                return username == "username1" ? new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "username1",
                    Email = "username1@mail.com"
                } : null;
            });
            var handler = new CheckOutUsernameHandler(userManager.Object);
            var query = new CheckOutUsernameQuery(username);

            var isAvailable = await handler.Handle(query, new CancellationToken());

            Assert.Equal(isAvailableExpected, isAvailable);
        }
    }
}

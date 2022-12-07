using FluentValidation.TestHelper;
using Quetta.Common.Models.Queries;
using Quetta.Common.Validators.Queries;

namespace Quetta.Tests.Validators.Queries
{
    public class GetChatsValidatorTests
    {
        private readonly GetChatsValidator validator = new GetChatsValidator();

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void GivenAnInvalidUserIdValue_ShouldHaveValidationError(string id)
        {
            // Arrange
            var model = new GetChatsQuery(id);

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.UserId);
        }

        [Fact]
        public void GivenAnValidUserIdValue_ShouldNotHaveValidationError()
        {
            // Arrange
            var model = new GetChatsQuery("someuserid");

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(model => model.UserId);
        }
    }
}

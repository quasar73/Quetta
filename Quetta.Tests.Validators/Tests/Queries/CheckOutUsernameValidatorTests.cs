using Quetta.Common.Models.Queries;
using Quetta.Common.Validators.Queries;
using FluentValidation.TestHelper;

namespace Quetta.Tests.Validators.Queries
{
    public class CheckOutUsernameValidatorTests
    {
        private readonly CheckOutUsernameValidator validator = new CheckOutUsernameValidator();

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        [InlineData("sh")]
        [InlineData("veryverylonglastnameover20chars")]
        public void GivenAnInvalidUsernameValue_ShouldHaveValidationError(string usernmae)
        {
            // Arrange
            var model = new CheckOutUsernameQuery(usernmae);

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.Username);
        }

        [Fact]
        public void GivenAnValidUsernameValue_ShouldNotHaveValidationError()
        {
            // Arrange
            var model = new CheckOutUsernameQuery("username");

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(model => model.Username);
        }
    }
}

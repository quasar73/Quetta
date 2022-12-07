using Quetta.Common.Validators.Commands;
using FluentValidation.TestHelper;
using Quetta.Common.Models.Commands;

namespace Quetta.Tests.Validators.Commands
{
    public class AuthenticateGoogleUserValidatorTests
    {
        private readonly AuthenticateGoogleUserValidator validator = new AuthenticateGoogleUserValidator();

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GivenAnInvalidIdTokenValue_ShouldHaveValidationError(string idToken)
        {
            // Arrange
            var model = new AuthenticateGoogleUserCommand { IdToken = idToken };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.IdToken);
        }

        [Fact]
        public void GivenAnValidIdTokenValue_ShouldNotHaveValidationError()
        {
            // Arrange
            var model = new AuthenticateGoogleUserCommand { IdToken = "someidtoken" };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(model => model.IdToken);
        }
    }
}

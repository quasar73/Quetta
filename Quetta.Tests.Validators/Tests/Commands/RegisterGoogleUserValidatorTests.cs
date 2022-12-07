using Quetta.Common.Models.Commands;
using Quetta.Common.Validators.Commands;
using FluentValidation.TestHelper;

namespace Quetta.Tests.Validators.Commands
{
    public class RegisterGoogleUserValidatorTests
    {
        private readonly RegisterGoogleUserValidator validator = new RegisterGoogleUserValidator();

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GivenAnInvalidIdTokenValue_ShouldHaveValidationError(string idToken)
        {
            // Arrange
            var model = new RegisterGoogleUserCommand { IdToken = idToken };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.IdToken);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("veryverylongfirstnameover20chars")]
        public void GivenAnInvalidFirstNameValue_ShouldHaveValidationError(string firstName)
        {
            // Arrange
            var model = new RegisterGoogleUserCommand { FirstName = firstName };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.FirstName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("veryverylonglastnameover20chars")]
        public void GivenAnInvalidLastNameValue_ShouldHaveValidationError(string lastName)
        {
            // Arrange
            var model = new RegisterGoogleUserCommand { LastName = lastName };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.LastName);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        [InlineData("sh")]
        [InlineData("veryverylongusernameover20chars")]
        public void GivenAnInvalidUserNameValue_ShouldHaveValidationError(string username)
        {
            // Arrange
            var model = new RegisterGoogleUserCommand { Username = username };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.Username);
        }

        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationError()
        {
            // Arrange
            var model = new RegisterGoogleUserCommand
            {
                IdToken = "someidtoken",
                FirstName = "FirstName",
                LastName = "LastName",
                Username = "Username",
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

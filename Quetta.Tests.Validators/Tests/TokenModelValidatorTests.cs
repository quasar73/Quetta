using Quetta.Common.Models;
using Quetta.Common.Validators;
using FluentValidation.TestHelper;

namespace Quetta.Tests.Validators
{
    public class TokenModelValidatorTests
    {
        private readonly TokenModelValidator validator = new TokenModelValidator();

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void GivenAnInvalidAccessTokenValue_ShouldHaveValidationError(string accessToken)
        {
            // Arrange
            var model = new TokenModel()
            {
                AccessToken = accessToken
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.AccessToken);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void GivenAnInvaliRefreshTokenValue_ShouldHaveValidationError(string refreshToken)
        {
            // Arrange
            var model = new TokenModel()
            {
                RefreshToken = refreshToken
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.RefreshToken);
        }

        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationErrors()
        {
            // Arange
            var model = new TokenModel()
            {
                RefreshToken = "refreshToken",
                AccessToken = "accessToken"
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

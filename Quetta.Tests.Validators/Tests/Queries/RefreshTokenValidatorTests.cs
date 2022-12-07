using Quetta.Common.Models.Queries;
using Quetta.Common.Validators.Queries;
using FluentValidation.TestHelper;

namespace Quetta.Tests.Validators.Queries
{
    public class RefreshTokenValidatorTests
    {
        private readonly RefreshTokenValidator validator = new RefreshTokenValidator();

        [Theory]
        [InlineData(null)]
        [InlineData("  ")]
        [InlineData("")]
        public void GivenAnInvalidRefreshTokenValue_ShouldHaveValidationError(string refreshToken)
        {
            // Arrange
            var model = new RefreshTokenQuery(refreshToken);

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.RefreshToken);
        }

        [Fact]
        public void GivenAValidRefreshTokenValue_ShouldNotHaveValidationError()
        {
            // Arrange
            var model = new RefreshTokenQuery("refreshtoken");

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(model => model.RefreshToken);
        }
    }
}

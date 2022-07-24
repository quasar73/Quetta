using Quetta.Common.Models.Queries;
using Quetta.Common.Validators.Queries;
using FluentValidation.TestHelper;

namespace Quetta.Validators.Tests.Queries
{
    public class RefreshTokenValidatorTests
    {
        private readonly RefreshTokenValidator validator = new RefreshTokenValidator();

        [Theory]
        [InlineData(null)]
        [InlineData("  ")]
        [InlineData("")]
        public void GivenAnInvalidRefreshTokenValue_ShouldNotHaveValidationError(string refreshToken)
        {
            var model = new RefreshTokenQuery(refreshToken);
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.RefreshToken);
        }

        [Fact]
        public void GivenAValidRefreshTokenValue_ShouldNotHaveValidationError()
        {
            var model = new RefreshTokenQuery("refreshtoken");
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(model => model.RefreshToken);
        }
    }
}

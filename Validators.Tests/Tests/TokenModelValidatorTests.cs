using Common.Models;
using Common.Validators;
using FluentValidation.TestHelper;

namespace Quetta.Validators.Tests.Tests
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
            var model = new TokenModel()
            {
                AccessToken = accessToken
            };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.AccessToken);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void GivenAnInvaliRefreshTokenValue_ShouldHaveValidationError(string refreshToken)
        {
            var model = new TokenModel()
            {
                RefreshToken = refreshToken
            };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.RefreshToken);
        }

        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationErrors()
        {
            var model = new TokenModel()
            {
                RefreshToken = "refreshToken",
                AccessToken = "accessToken"
            };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

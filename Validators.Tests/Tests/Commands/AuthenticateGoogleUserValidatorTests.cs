using Common.Validators.Commands;
using FluentValidation.TestHelper;
using Common.Models.Commands;

namespace Validators.Tests.Tests.Commands
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
            var model = new AuthenticateGoogleUserCommand { IdToken = idToken };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.IdToken);
        }

        [Fact]
        public void GivenAnValidIdTokenValue_ShouldNotHaveValidationError()
        {
            var model = new AuthenticateGoogleUserCommand { IdToken = "someidtoken" };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(model => model.IdToken);
        }
    }
}

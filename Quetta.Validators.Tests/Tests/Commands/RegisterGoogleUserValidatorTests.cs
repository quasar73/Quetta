using Quetta.Common.Models.Commands;
using Quetta.Common.Validators.Commands;
using FluentValidation.TestHelper;

namespace Quetta.Validators.Tests.Commands
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
            var model = new RegisterGoogleUserCommand { IdToken = idToken };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.IdToken);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("veryverylongfirstnameover20chars")]
        public void GivenAnInvalidFirstNameValue_ShouldHaveValidationError(string firstName)
        {
            var model = new RegisterGoogleUserCommand { FirstName = firstName };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.FirstName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("veryverylonglastnameover20chars")]
        public void GivenAnInvalidLastNameValue_ShouldHaveValidationError(string lastName)
        {
            var model = new RegisterGoogleUserCommand { LastName = lastName };
            var result = validator.TestValidate(model);
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
            var model = new RegisterGoogleUserCommand { Username = username };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.Username);
        }

        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationError()
        {
            var model = new RegisterGoogleUserCommand
            {
                IdToken = "someidtoken",
                FirstName = "FirstName",
                LastName = "LastName",
                Username = "Username",
            };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

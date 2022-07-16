using Common.Models.Queries;
using Common.Validators.Queries;
using FluentValidation.TestHelper;

namespace Quetta.Validators.Tests.Tests.Queries
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
            var model = new CheckOutUsernameQuery(usernmae);
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.Username);
        }

        [Fact]
        public void GivenAnValidUsernameValue_ShouldNotHaveValidationError()
        {
            var model = new CheckOutUsernameQuery("username");
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(model => model.Username);
        }
    }
}

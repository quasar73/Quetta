using Common.Models.Requests;
using Common.Validators.Requests;
using FluentValidation.TestHelper;

namespace Quetta.Validators.Tests.Tests.Requests
{
    public class SendInviteValidatorTests
    {
        private readonly SendInviteValidator validator = new SendInviteValidator();

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GivenAnInvalidReceiverUsernameValue_ShouldHaveValidationError(string username)
        {
            var model = new SendInviteRequest() { ReceiverUsername = username };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.ReceiverUsername);
        }

        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationError()
        {
            var model = new SendInviteRequest() { ReceiverUsername = "Username" };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

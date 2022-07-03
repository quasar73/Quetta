using Common.Models.Commands;
using Common.Models.Requests;
using Common.Validators.Commands;
using FluentValidation.TestHelper;

namespace Validators.Tests.Tests.Commands
{
    public class SendInviteValidatorTests
    {
        private readonly SendInviteValidator validator = new SendInviteValidator();

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GivenAnInvalidSenderIdValue_ShouldHaveValidationError(string senderId)
        {
            var model = new SendInviteCommand { SenderId = senderId };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.SenderId);
        }

        [Fact]
        public void GivenAnInvalidSendInviteRequestValue_ShouldHaveValidationError()
        {
            var model = new SendInviteCommand { SendInviteRequest = null };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.SendInviteRequest);
        }

        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationError()
        {
            var sendInviteRequest = new SendInviteRequest();
            var model = new SendInviteCommand(sendInviteRequest, "senderId");
            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

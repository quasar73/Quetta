using Quetta.Common.Models.Commands;
using Quetta.Common.Models.Requests;
using Quetta.Common.Validators.Commands;
using FluentValidation.TestHelper;

namespace Quetta.Tests.Validators.Commands
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
            // Arrange
            var model = new SendInviteCommand { SenderId = senderId };

            // Act
            var result = validator.TestValidate(model);

            // Arrange
            result.ShouldHaveValidationErrorFor(model => model.SenderId);
        }

        [Fact]
        public void GivenAnInvalidSendInviteRequestValue_ShouldHaveValidationError()
        {
            // Arrange
            var model = new SendInviteCommand { SendInviteRequest = null };

            // Act
            var result = validator.TestValidate(model);

            // Arrange
            result.ShouldHaveValidationErrorFor(model => model.SendInviteRequest);
        }

        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationError()
        {
            // Arrange
            var sendInviteRequest = new SendInviteRequest();
            var model = new SendInviteCommand(sendInviteRequest, "senderId");

            // Act
            var result = validator.TestValidate(model);

            // Arrange
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

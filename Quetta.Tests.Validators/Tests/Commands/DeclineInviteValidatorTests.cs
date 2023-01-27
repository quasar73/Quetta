using FluentValidation.TestHelper;
using Quetta.Common.Models.Commands;
using Quetta.Common.Validators.Commands;

namespace Quetta.Tests.Validators.Commands
{
    public class DeclineInviteValidatorTests
    {
        private readonly DeclineInviteValidator validator = new DeclineInviteValidator();

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GivenAnInvalidInviteIdValue_ShouldHaveValidationError(string id)
        {
            // Arrange
            var model = new DeclineInviteCommand() { InviteId = id };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.InviteId);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GivenAnInvalidReceiverIdValue_ShouldHaveValidationError(string id)
        {
            // Arrange
            var model = new DeclineInviteCommand() { ReceiverId = id };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.ReceiverId);
        }

        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationError()
        {
            // Arrange
            var model = new DeclineInviteCommand()
            {
                InviteId = "someinviteid",
                ReceiverId = "somereceiverid"
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}


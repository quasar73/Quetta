using FluentValidation.TestHelper;
using Quetta.Common.Models.Commands;
using Quetta.Common.Validators.Commands;

namespace Quetta.Validators.Tests.Tests.Commands
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
            var model = new DeclineInviteCommand() { InviteId = id };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.InviteId);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GivenAnInvalidReceiverIdValue_ShouldHaveValidationError(string id)
        {
            var model = new DeclineInviteCommand() { ReceiverId = id };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.ReceiverId);
        }

        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationError()
        {
            var model = new DeclineInviteCommand()
            {
                InviteId = "someinviteid",
                ReceiverId = "somereceiverid"
            };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}


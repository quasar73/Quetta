using FluentValidation.TestHelper;
using Quetta.Common.Models.Commands;
using Quetta.Common.Validators.Commands;

namespace Quetta.Tests.Validators.Commands
{
    public class AcceptInviteValidatorTests
    {
        private readonly AcceptInviteValidator validator = new AcceptInviteValidator();

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GivenAnInvalidInviteIdValue_ShouldHaveValidationError(string id)
        {
            var model = new AcceptInviteCommand() { InviteId = id };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.InviteId);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GivenAnInvalidReceiverIdValue_ShouldHaveValidationError(string id)
        {
            var model = new AcceptInviteCommand() { ReceiverId = id };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.ReceiverId);
        }

        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationError()
        {
            var model = new AcceptInviteCommand() 
            { 
                InviteId = "someinviteid",
                ReceiverId = "somereceiverid"
            };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}


using FluentValidation.TestHelper;
using Quetta.Common.Models.Requests;
using Quetta.Common.Validators.Requests;

namespace Quetta.Validators.Tests.Tests.Requests
{
    public class RespondInviteValidatorTests
    {
        private readonly RespondInviteValidator validator = new RespondInviteValidator();

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GivenAnInvalidInviteIdValue_ShouldHaveValidationError(string id)
        {
            var model = new RespondInviteRequest() { InviteId = id };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.InviteId);
        }

        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationError()
        {
            var model = new RespondInviteRequest() { InviteId = "someinviteid" };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

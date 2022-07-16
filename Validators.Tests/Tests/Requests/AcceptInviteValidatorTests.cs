using FluentValidation.TestHelper;
using Quetta.Common.Models.Requests;
using Quetta.Common.Validators.Requests;

namespace Quetta.Validators.Tests.Tests.Requests
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
            var model = new AcceptInviteRequest() { InviteId = id };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.InviteId);
        }

        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationError()
        {
            var model = new AcceptInviteRequest() { InviteId = "someinviteid" };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

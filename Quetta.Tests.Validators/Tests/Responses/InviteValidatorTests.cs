using Quetta.Common.Models.Responses;
using FluentValidation.TestHelper;
using Quetta.Common.Validators.Responses;

namespace Quetta.Tests.Validators.Responses
{
    public class InviteValidatorTests
    {
        private readonly InviteValidator validator = new InviteValidator();

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GivenAnInvalidSenderUsernameValue_ShouldHaveValidationError(string username)
        {
            var model = new InviteResponse() { SenderUsername = username };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.SenderUsername);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GivenAnInvalidInviteIdValue_ShouldHaveValidationError(string id)
        {
            var model = new InviteResponse() { InviteId = id };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.InviteId);
        }

        [Fact]
        public void GivenAnInvalidDateValue_ShouldHaveValidationError()
        {
            var model = new InviteResponse();
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.DateTime);
        }

        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationError()
        {
            var model = new InviteResponse() 
            {
                InviteId = "someid",
                SenderUsername = "username",
                DateTime = DateTime.Now,
            };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

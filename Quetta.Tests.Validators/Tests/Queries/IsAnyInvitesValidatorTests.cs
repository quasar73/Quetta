using Quetta.Common.Models.Queries;
using Quetta.Common.Validators.Queries;
using FluentValidation.TestHelper;

namespace Quetta.Tests.Validators.Queries
{
    public class IsAnyInvitesValidatorTests
    {
        private readonly IsAnyInvitesValidator validator = new IsAnyInvitesValidator();

        [Theory]
        [InlineData(null)]
        [InlineData("  ")]
        [InlineData("")]
        public void GivenAnInvalidUserIdValue_ShouldHaveValidationError(string userId)
        {
            var model = new IsAnyInvitesQuery(userId);
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.UserId);
        }

        [Fact]
        public void GivenAValidUserIdValue_ShouldNotHaveValidationError()
        {
            var model = new IsAnyInvitesQuery("someuserid");
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(model => model.UserId);
        }
    }
}

using Common.Models.Queries;
using Common.Validators.Queries;
using FluentValidation.TestHelper;

namespace Quetta.Validators.Tests.Tests.Queries
{
    public class IsAnyNotificationValidatorTests
    {
        private readonly IsAnyNotificationValidator validator = new IsAnyNotificationValidator();

        [Theory]
        [InlineData(null)]
        [InlineData("  ")]
        [InlineData("")]
        public void GivenAnInvalidUserIdValue_ShouldHaveValidationError(string userId)
        {
            var model = new IsAnyNotificationsQuery(userId);
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.UserId);
        }

        [Fact]
        public void GivenAValidUserIdValue_ShouldNotHaveValidationError()
        {
            var model = new IsAnyNotificationsQuery("someuserid");
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(model => model.UserId);
        }
    }
}

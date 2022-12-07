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
            // Arrange
            var model = new IsAnyInvitesQuery(userId);

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.UserId);
        }

        [Fact]
        public void GivenAValidUserIdValue_ShouldNotHaveValidationError()
        {
            // Arrange
            var model = new IsAnyInvitesQuery("someuserid");

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(model => model.UserId);
        }
    }
}

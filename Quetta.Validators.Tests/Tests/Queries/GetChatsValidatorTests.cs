using FluentValidation.TestHelper;
using Quetta.Common.Models.Queries;
using Quetta.Common.Validators.Queries;

namespace Quetta.Validators.Tests.Queries
{
    public class GetChatsValidatorTests
    {
        private readonly GetChatsValidator validator = new GetChatsValidator();

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void GivenAnInvalidUserIdValue_ShouldHaveValidationError(string id)
        {
            var model = new GetChatsQuery(id);
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.UserId);
        }

        [Fact]
        public void GivenAnValidUserIdValue_ShouldNotHaveValidationError()
        {
            var model = new GetChatsQuery("someuserid");
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(model => model.UserId);
        }
    }
}

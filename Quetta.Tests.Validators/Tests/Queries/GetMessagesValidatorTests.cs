using FluentValidation.TestHelper;
using Quetta.Common.Models.Queries;
using Quetta.Common.Validators.Queries;

namespace Quetta.Tests.Validators.Queries
{
    public class GetMessagesValidatorTests
    {
        private readonly GetMessagesValidator validator = new GetMessagesValidator();

        [Theory]
        [InlineData(null)]
        [InlineData("  ")]
        [InlineData("")]
        public void GivenAnInvalidChatIdValue_ShouldHaveValidationError(string id)
        {
            var model = new GetMessagesQuery(id);
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.ChatId);
        }

        [Fact]
        public void GivenAValidValue_ShouldNotHaveValidationError()
        {
            var model = new GetMessagesQuery("somechatid");
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(model => model.ChatId);
        }
    }
}

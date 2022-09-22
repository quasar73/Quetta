using FluentValidation.TestHelper;
using Quetta.Common.Models.Queries;
using Quetta.Common.Validators.Queries;

namespace Quetta.Tests.Validators.Queries
{
    public class GetChatInfoValidatorTests
    {
        private readonly GetChatInfoValidator validator = new GetChatInfoValidator();

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void GivenAnInvalidChatIdValue_ShouldHaveValidationError(string chatId)
        {
            var model = new GetChatInfoQuery
            {
                ChatId = chatId
            };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.ChatId);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void GivenAnInvalidUserIdValue_ShouldHaveValidationError(string userId)
        {
            var model = new GetChatInfoQuery
            {
                UserId = userId
            };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.UserId);
        }

        [Fact]
        public void GivenAValidValues_ShouldNotHaveValidationErrors()
        {
            var model = new GetChatInfoQuery
            {
                UserId = "someuserid",
                ChatId = "somechatid"
            };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

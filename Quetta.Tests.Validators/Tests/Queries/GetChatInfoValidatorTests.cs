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
            // Arrange
            var model = new GetChatInfoQuery
            {
                ChatId = chatId
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.ChatId);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void GivenAnInvalidUserIdValue_ShouldHaveValidationError(string userId)
        {
            // Arrange
            var model = new GetChatInfoQuery
            {
                UserId = userId
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.UserId);
        }

        [Fact]
        public void GivenAValidValues_ShouldNotHaveValidationErrors()
        {
            // Arrange
            var model = new GetChatInfoQuery
            {
                UserId = "someuserid",
                ChatId = "somechatid"
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

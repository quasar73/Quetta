using FluentValidation.TestHelper;
using Quetta.Common.Models.Requests;
using Quetta.Common.Validators.Requests;

namespace Quetta.Tests.Validators.Requests
{
    public class GetMessagesValidatorTests
    {
        private readonly GetMessagesValidator validator = new GetMessagesValidator();

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GivenAnInvalidChatIdValue_ShouldHaveValidationError(string chatId)
        {
            // Arrange
            var model = new GetMessagesRequest() { ChatId = chatId };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.ChatId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-999)]
        public void GivenAnInvalidAmountValue_ShouldHaveValidationError(int amount)
        {
            // Arrange
            var model = new GetMessagesRequest() { Amount = amount };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.Amount);
        }

        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationError()
        {
            // Act
            var model = new GetMessagesRequest()
            {
                ChatId = "somechatid",
                Amount = 1,
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

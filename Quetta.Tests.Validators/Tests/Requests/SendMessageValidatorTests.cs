using FluentValidation.TestHelper;
using Quetta.Common.Models.Requests;
using Quetta.Common.Validators.Requests;

namespace Quetta.Tests.Validators.Requests
{
    public class SendMessageValidatorTests
    {
        public static IEnumerable<object[]> TextData => new List<object[]>
        {
            new object[] { "" },
            new object[] { " " },
            new object[] { null },
            new object[] { new String('a', 2001) },
        };
        private readonly SendMessageValidator validator = new SendMessageValidator();

        [Theory]
        [MemberData(nameof(TextData))]
        public void GivenAnInvalidTextValue_ShouldHaveValidationError(string text)
        {
            // Arrange
            var model = new SendMessageReqeust() { Text = text };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.Text);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GivenAnInvalidChatIdValue_ShouldHaveValidationError(string id)
        {
            // Arrange
            var model = new SendMessageReqeust() { ChatId = id };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.ChatId);
        }

        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationError()
        {
            // Arrange
            var model = new SendMessageReqeust()
            {
                Text = "some valid text",
                ChatId = "somechatid",
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

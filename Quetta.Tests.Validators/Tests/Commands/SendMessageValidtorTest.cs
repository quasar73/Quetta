using FluentValidation.TestHelper;
using Quetta.Common.Models.Commands;
using Quetta.Common.Validators.Commands;

namespace Quetta.Tests.Validators.Commands
{
    public class SendMessageValidtorTest
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
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GivenAnInvalidSenderIdValue_ShouldHaveValidationError(string id)
        {
            // Arrange
            var model = new SendMessageCommand() { SenderId = id };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.SenderId);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GivenAnInvalidChatIdValue_ShouldHaveValidationError(string id)
        {
            // Arrange
            var model = new SendMessageCommand() { ChatId = id };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.ChatId);
        }

        [Theory]
        [MemberData(nameof(TextData))]
        public void GivenAnInvalidTextValue_ShouldHaveValidationError(string text)
        {
            // Arrange
            var model = new SendMessageCommand() { Text = text };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.Text);
        }

        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationError()
        {
            // Arrange
            var model = new SendMessageCommand()
            {
                SenderId = "somesenderid",
                ChatId = "somechatid",
                Text = "some valid text",
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

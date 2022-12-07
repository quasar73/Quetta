using FluentValidation.TestHelper;
using Quetta.Common.Models.Responses;
using Quetta.Common.Validators.Responses;

namespace Quetta.Tests.Validators.Responses
{
    public class SidebarValidatorTests
    {
        private readonly SidebarValidator validator = new SidebarValidator();

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void GivenAnInvalidChatIdValue_ShouldHaveValidationError(string chatId)
        {
            // Arrange
            var model = new SidebarResponse() { ChatId = chatId };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.ChatId);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void GivenAnInvalidUsernameValue_ShouldHaveValidationError(string username)
        {
            // Arrange
            var model = new SidebarResponse() { Username = username };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.Username);
        }


        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationError()
        {
            // Arrange
            var model = new SidebarResponse()
            {
                Username = "username",
                ChatId = "somechatid"
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

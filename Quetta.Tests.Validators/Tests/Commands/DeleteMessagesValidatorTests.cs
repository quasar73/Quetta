using FluentValidation.TestHelper;
using Quetta.Common.Models.Commands;
using Quetta.Common.Validators.Commands;

namespace Quetta.Tests.Validators.Commands
{
    public class DeleteMessagesValidatorTests
    {
        private readonly DeleteMessagesValidator validator = new DeleteMessagesValidator();

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GivenAnInvalidUserIdValue_ShouldHaveValidationError(string userId)
        {
            // Arrange
            var model = new DeleteMessagesCommand() { UserId = userId };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.UserId);
        }

        [Fact]
        public void GivenAnInvalidMessageIdsValue_ShouldHaveValidationError()
        {
            // Arrange
            var model = new DeleteMessagesCommand() { MessageIds = new() };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.UserId);
        }

        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationError()
        {
            // Act
            var model = new DeleteMessagesCommand()
            {
                UserId = "someuserid",
                MessageIds = new()
                {
                    "somemessageid"
                }
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

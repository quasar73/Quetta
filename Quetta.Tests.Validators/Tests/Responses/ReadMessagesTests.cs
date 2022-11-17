using FluentValidation.TestHelper;
using Quetta.Common.Models.Responses;
using Quetta.Common.Validators.Responses;

namespace Quetta.Tests.Validators.Responses
{
    public class ReadMessagesTests
    {
        private readonly ReadMessagesValidator validator = new ReadMessagesValidator();

        [Fact]
        public void GivenAnInvalidMessagesIdValue_ShouldHaveValidationError()
        {
            // Arrange
            var model = new ReadMessagesResponse() { MessageIds = new string[] { } };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.MessageIds);
        }

        [Fact]
        public void GivenAnInvalidReaderValue_ShouldHaveValidationError()
        {
            // Arrange
            var model = new ReadMessagesResponse() { Reader = null };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.Reader);
        }

        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationError()
        {
            // Act
            var model = new ReadMessagesResponse()
            {
                MessageIds = new string[] { "somemessageid" },
                Reader = new()
                {
                    AvatarUrl = "URL",
                    FullName = "Full Name"
                },
            };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

using FluentValidation.TestHelper;
using Quetta.Common.Models.Requests;
using Quetta.Common.Validators.Requests;

namespace Quetta.Tests.Validators.Requests
{
    public class DeleteMessagesValidatorTests
    {
        private readonly DeleteMessagesValidator validator = new DeleteMessagesValidator();

        [Fact]
        public void GivenAnInvalidMessageIdsValue_ShouldHaveValidationError()
        {
            // Arrange
            var model = new DeleteMessagesRequest() { MessageIds = new string[] { } };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.MessageIds);
        }

        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationError()
        {
            // Act
            var model = new DeleteMessagesRequest()
            {
                MessageIds = new string[]
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

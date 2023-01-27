using FluentValidation.TestHelper;
using Quetta.Common.Enums;
using Quetta.Common.Models.Responses;
using Quetta.Common.Validators.Responses;

namespace Quetta.Tests.Validators.Responses
{
    public class ChatItemValidatorTests
    {
        private readonly ChatItemValidator validator = new ChatItemValidator();

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void GivenAnInvalidIdValue_ShouldHaveValidationError(string id)
        {
            // Arrange
            var model = new ChatItemResponse { Id = id };

            // Act
            var result = validator.TestValidate(model);

            // Asssert
            result.ShouldHaveValidationErrorFor(model => model.Id);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void GivenAnInvalidTitleValue_ShouldHaveValidationError(string title)
        {
            // Arrange
            var model = new ChatItemResponse { Title = title };

            // Act
            var result = validator.TestValidate(model);

            // Asssert
            result.ShouldHaveValidationErrorFor(model => model.Title);
        }

        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationError()
        {
            // Arrange
            var model = new ChatItemResponse()
            {
                Id = "someid",
                Title = "Some Title",
                ChatType = ChatType.PersonalChat
            };

            // Act
            var result = validator.TestValidate(model);

            // Asssert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

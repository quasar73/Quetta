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
            var model = new ChatItemResponse
            {
                Id = id
            };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.Id);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void GivenAnInvalidTitleValue_ShouldHaveValidationError(string title)
        {
            var model = new ChatItemResponse
            {
                Title = title   
            };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.Title);
        }

        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationError()
        {
            var model = new ChatItemResponse()
            {
                Id = "someid",
                Title = "Some Title",
                ChatType = ChatType.PersonalChat
            };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

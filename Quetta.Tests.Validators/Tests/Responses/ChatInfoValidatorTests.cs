using FluentValidation.TestHelper;
using Quetta.Common.Models.Responses;
using Quetta.Common.Validators.Responses;

namespace Quetta.Tests.Validators.Responses
{
    public class ChatInfoValidatorTests
    {
        private readonly ChatInfoValidator validator = new ChatInfoValidator();

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void GivenAnInvalidTitleValue_ShouldHaveValidationError(string title)
        {
            var model = new ChatInfoResponse
            {
                Title = title
            };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.Title);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void GivenAnInvalidMembersValue_ShouldHaveValidationError(int members)
        {
            var model = new ChatInfoResponse
            {
                Members = members
            };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.Members);
        }

        [Fact]
        public void GivenAValidValues_ShouldNotHaveValidationErrors()
        {
            var model = new ChatInfoResponse
            {
                Title = "some title",
                Members = 2
            };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

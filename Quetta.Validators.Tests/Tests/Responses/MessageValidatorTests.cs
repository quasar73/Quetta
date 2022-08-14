using FluentValidation.TestHelper;
using Quetta.Common.Models.Responses;
using Quetta.Common.Validators.Responses;

namespace Quetta.Validators.Tests.Responses
{
    public class MessageValidatorTests
    {
        public static IEnumerable<object[]> TextData => new List<object[]>
        {
            new object[] { "" },
            new object[] { " " },
            new object[] { null },
            new object[] { new String('a', 2001) },
        };
        private readonly MessageValidator validator = new MessageValidator();

        [Theory]
        [MemberData(nameof(TextData))]
        public void GivenAnInvalidTextValue_ShouldHaveValidationError(string text)
        {
            var model = new MessageResponse
            {
                Text = text,
            };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.Text);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GivenAnInvalidUsernameValue_ShouldHaveValidationError(string username)
        {
            var model = new MessageResponse
            {
                Username = username,
            };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.Username);
        }

        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationError()
        {
            var model = new MessageResponse
            {
                Text = "some text",
                Username = "someusername"
            };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

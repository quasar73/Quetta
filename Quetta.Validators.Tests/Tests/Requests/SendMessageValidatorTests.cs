using FluentValidation.TestHelper;
using Quetta.Common.Models.Requests;
using Quetta.Common.Validators.Requests;

namespace Quetta.Validators.Tests.Requests
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
            var model = new SendMessageReqeust() { Text = text };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.Text);
        }

        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationError()
        {
            var model = new SendMessageReqeust()
            {
                Text = "some valid text"
            };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

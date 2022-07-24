using FluentValidation.TestHelper;
using Quetta.Common.Models.Commands;
using Quetta.Common.Validators.Commands;

namespace Quetta.Validators.Tests.Commands
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
            var model = new SendMessageCommand() { SenderId = id };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.SenderId);
        }

        [Theory]
        [MemberData(nameof(TextData))]
        public void GivenAnInvalidTextValue_ShouldHaveValidationError(string text)
        {
            var model = new SendMessageCommand() { Text = text };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(model => model.Text);
        }

        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationError()
        {
            var model = new SendMessageCommand()
            {
                SenderId = "somesenderid",
                Text = "some valid text"
            };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

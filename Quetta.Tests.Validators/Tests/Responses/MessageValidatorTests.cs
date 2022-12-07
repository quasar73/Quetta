using FluentValidation.TestHelper;
using Quetta.Common.Models.Responses;
using Quetta.Common.Validators.Responses;

namespace Quetta.Tests.Validators.Responses
{
    public class MessageValidatorTests
    {
        public static IEnumerable<object[]> TextData =>
            new List<object[]>
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
            // Arrange
            var model = new MessageResponse { Text = text, };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.Text);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GivenAnInvalidUsernameValue_ShouldHaveValidationError(string username)
        {
            // Arrange
            var model = new MessageResponse { Username = username, };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.Username);
        }

        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationError()
        {
            // Arrange
            var model = new MessageResponse { Text = "some text", Username = "someusername" };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

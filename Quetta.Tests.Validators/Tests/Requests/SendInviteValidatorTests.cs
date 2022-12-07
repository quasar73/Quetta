using Quetta.Common.Models.Requests;
using Quetta.Common.Validators.Requests;
using FluentValidation.TestHelper;

namespace Quetta.Tests.Validators.Requests
{
    public class SendInviteValidatorTests
    {
        private readonly SendInviteValidator validator = new SendInviteValidator();

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GivenAnInvalidReceiverUsernameValue_ShouldHaveValidationError(string username)
        {
            // Arrange
            var model = new SendInviteRequest() { ReceiverUsername = username };

            // Act
            var result = validator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(model => model.ReceiverUsername);
        }

        [Fact]
        public void GivenValidValues_ShouldNotHaveValidationError()
        {
            // Arrange
            var model = new SendInviteRequest() { ReceiverUsername = "Username" };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

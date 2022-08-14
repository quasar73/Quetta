using FluentValidation;
using Quetta.Common.Models.Responses;

namespace Quetta.Common.Validators.Responses
{
    public class MessageValidator : AbstractValidator<MessageResponse>
    {
        public MessageValidator()
        {
            RuleFor(model => model.Username).NotEmpty();
            RuleFor(model => model.Text).NotEmpty().Length(1, 2000);
        }
    }
}

using FluentValidation;
using Quetta.Common.Models.Commands;

namespace Quetta.Common.Validators.Commands
{
    public class SendMessageValidator : AbstractValidator<SendMessageCommand>
    {
        public SendMessageValidator()
        {
            RuleFor(model => model.SenderId).NotEmpty();
            RuleFor(model => model.ChatId).NotEmpty();
            RuleFor(model => model.Text).NotEmpty().Length(1, 2000);
        }
    }
}

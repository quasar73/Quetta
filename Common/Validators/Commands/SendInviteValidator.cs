using Common.Models.Commands;
using FluentValidation;

namespace Common.Validators.Commands
{
    public class SendInviteValidator : AbstractValidator<SendInviteCommand>
    {
        public SendInviteValidator()
        {
            RuleFor(model => model.SendInviteRequest).NotNull();
            RuleFor(model => model.SenderId).NotEmpty();
        }
    }
}

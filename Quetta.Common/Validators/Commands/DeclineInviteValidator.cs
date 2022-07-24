using FluentValidation;
using Quetta.Common.Models.Commands;

namespace Quetta.Common.Validators.Commands
{
    public class DeclineInviteValidator : AbstractValidator<DeclineInviteCommand>
    {
        public DeclineInviteValidator()
        {
            RuleFor(model => model.InviteId).NotEmpty();
            RuleFor(model => model.ReceiverId).NotEmpty();
        }
    }
}

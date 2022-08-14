using FluentValidation;
using Quetta.Common.Models.Commands;

namespace Quetta.Common.Validators.Commands
{
    public class AcceptInviteValidator : AbstractValidator<AcceptInviteCommand>
    {
        public AcceptInviteValidator()
        {
            RuleFor(model => model.InviteId).NotEmpty();
            RuleFor(model => model.ReceiverId).NotEmpty();
        }
    }
}

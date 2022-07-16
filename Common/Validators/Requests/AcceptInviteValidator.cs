using FluentValidation;
using Quetta.Common.Models.Requests;

namespace Quetta.Common.Validators.Requests
{
    public class AcceptInviteValidator : AbstractValidator<AcceptInviteRequest>
    {
        public AcceptInviteValidator()
        {
            RuleFor(model => model.InviteId).NotEmpty();
        }
    }
}

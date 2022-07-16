using FluentValidation;
using Quetta.Common.Models.Requests;

namespace Quetta.Common.Validators.Requests
{
    public class RespondInviteValidator : AbstractValidator<RespondInviteRequest>
    {
        public RespondInviteValidator()
        {
            RuleFor(model => model.InviteId).NotEmpty();
        }
    }
}

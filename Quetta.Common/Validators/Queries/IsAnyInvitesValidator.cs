using Quetta.Common.Models.Queries;
using FluentValidation;

namespace  Quetta.Common.Validators.Queries
{
    public class IsAnyInvitesValidator : AbstractValidator<IsAnyInvitesQuery>
    {
        public IsAnyInvitesValidator()
        {
            RuleFor(model => model.UserId).NotEmpty();
        }
    }
}

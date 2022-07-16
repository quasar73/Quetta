using Common.Models.Responses;
using FluentValidation;

namespace Quetta.Common.Validators.Responses
{
    public class InviteValidator : AbstractValidator<InviteResponse>
    {
        public InviteValidator()
        {
            RuleFor(model => model.InviteId).NotEmpty();
            RuleFor(model => model.SenderUsername).NotEmpty();
            RuleFor(model => model.DateTime).NotEmpty();
        }
    }
}

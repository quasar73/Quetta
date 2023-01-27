using FluentValidation;
using Quetta.Common.Models.Responses;

namespace Quetta.Common.Validators.Responses
{
    public class SidebarValidator : AbstractValidator<SidebarResponse>
    {
        public SidebarValidator()
        {
            RuleFor(model => model.ChatId).NotEmpty();
            RuleFor(model => model.Username).NotEmpty();
        }
    }
}

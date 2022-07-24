using FluentValidation;
using Quetta.Common.Models.Responses;

namespace Quetta.Common.Validators.Responses
{
    public class ChatItemValidator : AbstractValidator<ChatItemResponse>
    {
        public ChatItemValidator()
        {
            RuleFor(model => model.Id).NotEmpty();
            RuleFor(model => model.Title).NotEmpty();
        }
    }
}

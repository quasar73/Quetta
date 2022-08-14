using FluentValidation;
using Quetta.Common.Models.Responses;

namespace Quetta.Common.Validators.Responses
{
    public class ChatInfoValidator : AbstractValidator<ChatInfoResponse>
    {
        public ChatInfoValidator()
        {
            RuleFor(model => model.Title).NotEmpty();
            RuleFor(model => model.Members).GreaterThan(0);
        }
    }
}

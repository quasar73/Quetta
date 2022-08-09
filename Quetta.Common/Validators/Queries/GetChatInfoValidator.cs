using FluentValidation;
using Quetta.Common.Models.Queries;

namespace Quetta.Common.Validators.Queries
{
    public class GetChatInfoValidator : AbstractValidator<GetChatInfoQuery>
    {
        public GetChatInfoValidator()
        {
            RuleFor(model => model.ChatId).NotEmpty();
            RuleFor(model => model.UserId).NotEmpty();
        }
    }
}

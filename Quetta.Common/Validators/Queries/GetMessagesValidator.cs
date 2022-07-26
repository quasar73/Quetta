using FluentValidation;
using Quetta.Common.Models.Queries;

namespace Quetta.Common.Validators.Queries
{
    public class GetMessagesValidator : AbstractValidator<GetMessagesQuery>
    {
        public GetMessagesValidator()
        {
            RuleFor(model => model.ChatId).NotEmpty();
        }
    }
}

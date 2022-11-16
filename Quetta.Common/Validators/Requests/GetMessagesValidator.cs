using FluentValidation;
using Quetta.Common.Models.Requests;

namespace Quetta.Common.Validators.Requests
{
    public class GetMessagesValidator : AbstractValidator<GetMessagesRequest>
    {
        public GetMessagesValidator()
        {
            RuleFor(model => model.ChatId).NotEmpty();
            RuleFor(model => model.Amount).GreaterThan(0);
        }
    }
}

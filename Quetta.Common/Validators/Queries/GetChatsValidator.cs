using FluentValidation;
using Quetta.Common.Models.Queries;

namespace Quetta.Common.Validators.Queries
{
    public class GetChatsValidator : AbstractValidator<GetChatsQuery>
    {
        public GetChatsValidator()
        {
            RuleFor(model => model.UserId).NotEmpty();
        }
    }
}

using Quetta.Common.Models.Queries;
using FluentValidation;

namespace  Quetta.Common.Validators.Queries
{
    public class CheckOutUsernameValidator : AbstractValidator<CheckOutUsernameQuery>
    {
        public CheckOutUsernameValidator()
        {
            RuleFor(model => model.Username).NotEmpty().Length(3, 20);
        }
    }
}

using Quetta.Common.Models.Queries;
using FluentValidation;

namespace  Quetta.Common.Validators.Queries
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenQuery>
    {
        public RefreshTokenValidator()
        {
            RuleFor(model => model.RefreshToken).NotEmpty();
        }
    }
}

using Common.Models.Queries;
using FluentValidation;

namespace Common.Validators.Queries
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenQuery>
    {
        public RefreshTokenValidator()
        {
            RuleFor(model => model.RefreshToken).NotEmpty();
        }
    }
}

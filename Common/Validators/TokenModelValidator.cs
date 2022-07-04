using Common.Models;
using FluentValidation;

namespace Common.Validators
{
    public class TokenModelValidator : AbstractValidator<TokenModel>
    {
        public TokenModelValidator()
        {
            RuleFor(model => model.RefreshToken).NotEmpty();
            RuleFor(model => model.AccessToken).NotEmpty();
        }
    }
}

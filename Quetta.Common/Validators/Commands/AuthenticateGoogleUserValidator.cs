using Quetta.Common.Models.Commands;
using FluentValidation;

namespace  Quetta.Common.Validators.Commands
{
    public class AuthenticateGoogleUserValidator : AbstractValidator<AuthenticateGoogleUserCommand>
    {
        public AuthenticateGoogleUserValidator()
        {
            RuleFor(model => model.IdToken).NotEmpty();
        }
    }
}

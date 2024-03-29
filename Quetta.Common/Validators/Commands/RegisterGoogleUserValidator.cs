﻿using Quetta.Common.Models.Commands;
using FluentValidation;

namespace  Quetta.Common.Validators.Commands
{
    public class RegisterGoogleUserValidator : AbstractValidator<RegisterGoogleUserCommand>
    {
        public RegisterGoogleUserValidator()
        {
            RuleFor(model => model.IdToken).NotEmpty();
            RuleFor(model => model.FirstName).NotEmpty().MaximumLength(20);
            RuleFor(model => model.LastName).NotEmpty().MaximumLength(20);
            RuleFor(model => model.Username).NotEmpty().Length(3, 20);
        }
    }
}

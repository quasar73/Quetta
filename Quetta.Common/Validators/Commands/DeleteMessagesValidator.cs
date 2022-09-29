using FluentValidation;
using Quetta.Common.Models.Commands;

namespace Quetta.Common.Validators.Commands
{
    public class DeleteMessagesValidator : AbstractValidator<DeleteMessagesCommand>
    {
        public DeleteMessagesValidator()
        {
            RuleFor(model => model.MessageIds).NotEmpty();
            RuleFor(model => model.UserId).NotEmpty();
        }
    }
}

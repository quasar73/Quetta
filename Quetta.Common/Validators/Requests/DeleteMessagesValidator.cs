using FluentValidation;
using Quetta.Common.Models.Requests;

namespace Quetta.Common.Validators.Requests
{
    public class DeleteMessagesValidator : AbstractValidator<DeleteMessagesRequest>
    {
        public DeleteMessagesValidator()
        {
            RuleFor(model => model.MessageIds).NotEmpty();
        }
    }
}

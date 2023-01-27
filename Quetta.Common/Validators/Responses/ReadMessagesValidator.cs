using FluentValidation;
using Quetta.Common.Models.Responses;

namespace Quetta.Common.Validators.Responses
{
    public class ReadMessagesValidator : AbstractValidator<ReadMessagesResponse>
    {
        public ReadMessagesValidator()
        {
            RuleFor(model => model.MessageIds).NotEmpty();
            RuleFor(model => model.Reader).NotNull();
        }
    }
}


using Quetta.Common.Models.Requests;
using FluentValidation;

namespace  Quetta.Common.Validators.Requests
{
    public class SendInviteValidator : AbstractValidator<SendInviteRequest>
    {
        public SendInviteValidator()
        {
            RuleFor(model => model.ReceiverUsername).NotEmpty();
        }
    }
}

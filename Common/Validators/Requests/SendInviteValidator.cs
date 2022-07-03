
using Common.Models.Requests;
using FluentValidation;

namespace Common.Validators.Requests
{
    public class SendInviteValidator : AbstractValidator<SendInviteRequest>
    {
        public SendInviteValidator()
        {
            RuleFor(model => model.ReceiverUsername).NotEmpty();
        }
    }
}

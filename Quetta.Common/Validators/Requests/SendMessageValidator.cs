using FluentValidation;
using Quetta.Common.Models.Requests;

namespace Quetta.Common.Validators.Requests
{
    public class SendMessageValidator : AbstractValidator<SendMessageReqeust>
    {
        public SendMessageValidator()
        {
            RuleFor(model => model.Text).NotEmpty().Length(1, 2000);
            RuleFor(model => model.ChatId).NotEmpty();
        }
    }
}

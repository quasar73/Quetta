using Quetta.Common.Models.Queries;
using FluentValidation;

namespace  Quetta.Common.Validators.Queries
{
    public class IsAnyNotificationValidator : AbstractValidator<IsAnyNotificationsQuery>
    {
        public IsAnyNotificationValidator()
        {
            RuleFor(model => model.UserId).NotEmpty();
        }
    }
}

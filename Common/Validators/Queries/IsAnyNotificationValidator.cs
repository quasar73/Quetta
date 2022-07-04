using Common.Models.Queries;
using FluentValidation;

namespace Common.Validators.Queries
{
    public class IsAnyNotificationValidator : AbstractValidator<IsAnyNotificationsQuery>
    {
        public IsAnyNotificationValidator()
        {
            RuleFor(model => model.UserId).NotEmpty();
        }
    }
}

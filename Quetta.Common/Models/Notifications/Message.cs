using MediatR;

namespace Quetta.Common.Models.Notifications
{
    public class Message : INotification
    {
        public string ChatId { get; set; }

        public string MessageId { get; set; }

        public string Code { get; set; }
    }
}

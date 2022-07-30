using MediatR;

namespace Quetta.Common.Models.Notifications
{
    public class NewNotification : INotification 
    {
        public string ReceiverUsername { get; set; }

        public NewNotification(string receiverUsername)
        {
            ReceiverUsername = receiverUsername;
        }
    }
}

using MediatR;

namespace Quetta.Logic.Notifications
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

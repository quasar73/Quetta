using MediatR;

namespace Quetta.Common.Models.Notifications
{
    public class NewInviteNotification : INotification 
    {
        public string ReceiverUsername { get; set; }

        public NewInviteNotification(string receiverUsername)
        {
            ReceiverUsername = receiverUsername;
        }
    }
}

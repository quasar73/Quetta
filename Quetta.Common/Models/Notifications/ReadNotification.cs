using MediatR;
using Quetta.Common.Models.Responses;

namespace Quetta.Common.Models.Notifications
{
    public class ReadNotification : INotification
    {
        public string ChatId { get; set; }

        public string[] MessageIds { get; set; }

        public ReadNotification(ReadMessagesResponse model)
        {
            MessageIds = model.MessageIds;
            ChatId = model.ChatId;
        }
    }
}

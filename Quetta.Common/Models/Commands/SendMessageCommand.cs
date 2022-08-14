using MediatR;

namespace Quetta.Common.Models.Commands
{
    public class SendMessageCommand : IRequest<string>
    {
        public string Text { get; set; }

        public DateTime Date { get; set; }

        public string SenderId { get; set; }

        public string ChatId { get; set; }
    }
}

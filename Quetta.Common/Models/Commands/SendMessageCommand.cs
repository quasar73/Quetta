using MediatR;

namespace Quetta.Common.Models.Commands
{
    public class SendMessageCommand : IRequest<Unit>
    {
        public string Text { get; set; }

        public DateTime Date { get; set; }

        public string SenderId { get; set; }

        public string ChatId { get; set; }
    }
}

using Quetta.Common.Models.Commands;

namespace Quetta.Tests.Handlers.Commands
{
    public class SendInviteTestCaseModel
    {
        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public SendInviteCommand IncomingCommand { get; set; }
    }
}

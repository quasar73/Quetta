using Quetta.Common.Models.Commands;
using Quetta.Data.Models;

namespace Quetta.Tests.Handlers.Commands
{
    public class SendMessageTestCaseModel
    {
        public SendMessageCommand IncomingCommand { get; set; }

        public Message ExpectedResult { get; set; }
    }
}

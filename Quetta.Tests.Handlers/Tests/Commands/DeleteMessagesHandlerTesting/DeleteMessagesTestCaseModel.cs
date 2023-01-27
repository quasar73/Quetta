using Quetta.Common.Models.Commands;
using Quetta.Data.Models;

namespace Quetta.Tests.Handlers.Commands
{
    public class DeleteMessagesTestCaseModel
    {
        public Message[] MessagesToAdd { get; set; }

        public User[] UsersToAdd { get; set; }

        public Chat[] ChatsToAdd { get; set; }

        public DeleteMessagesCommand IncomingCommand { get; set; }

        public int ExpectedAmount { get; set; }
    }
}

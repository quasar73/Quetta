using Quetta.Common.Models.Queries;
using Quetta.Common.Models.Responses;
using Quetta.Data.Models;

namespace Quetta.Tests.Handlers.Queries
{
    public class GetChatsTestCaseModel
    {
        public User[] UsersToAdd { get; set; }

        public Chat[] ChatsToAdd { get; set; }

        public Message[] MessagesToAdd { get; set; }

        public GetChatsQuery IncomingQuery { get; set; }

        public ChatItemResponse[] ExpectedReturnedChats { get; set; }
    }
}

using Quetta.Common.Models.Queries;
using Quetta.Common.Models.Responses;
using Quetta.Data.Models;

namespace Quetta.Tests.Handlers.Queries
{
    public class GetMessagesTestCaseModel
    {
        public User[] UsersToAdd { get; set; }

        public Message[] MessagesToAdd { get; set; }

        public GetMessagesQuery IncomingQuery { get;  set; }

        public MessageResponse[] ExpectedReturnedMessages { get; set; }
    }
}

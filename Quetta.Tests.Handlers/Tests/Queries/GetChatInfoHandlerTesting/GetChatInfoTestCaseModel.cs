using Quetta.Common.Models.Queries;
using Quetta.Common.Models.Responses;
using Quetta.Data.Models;

namespace Quetta.Tests.Handlers.Queries
{
    public class GetChatInfoTestCaseModel
    {
        public Chat ChatToAdd { get; set; }

        public User[] UsersToAdd { get; set; }

        public GetChatInfoQuery IncomingQuery { get; set; }

        public ChatInfoResponse ExpectedChatInfo { get; set; }
    }
}

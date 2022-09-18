using Quetta.Common.Models.Queries;
using Quetta.Data.Models;

namespace Quetta.Tests.Handlers.Queries
{
    public class GetChatInfoTestExceptionModel
    {
        public Chat ChatToAdd { get; set; }

        public User[] UsersToAdd { get; set; }

        public GetChatInfoQuery IncomingQuery { get; set; }

        public Type ExceptionType { get; set; }

        public string ExceptionMessage { get; set; }
    }
}

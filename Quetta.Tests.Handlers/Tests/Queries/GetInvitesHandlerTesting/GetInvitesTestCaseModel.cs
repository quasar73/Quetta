using Quetta.Common.Models.Queries;
using Quetta.Common.Models.Responses;
using Quetta.Data.Models;

namespace Quetta.Tests.Handlers.Queries
{
    public class GetInvitesTestCaseModel
    {
        public Invite[] InvitesToAdd { get; set; }

        public User[] UsersToAdd { get; set; }

        public GetInvitesQuery IncomingQuery { get; set; }

        public InviteResponse[] ExpectedReturnedInvites { get; set; }
    }
}

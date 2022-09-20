using Quetta.Common.Models.Queries;
using Quetta.Common.Models.Responses;
using Quetta.Data.Models;

namespace Quetta.Tests.Handlers.Queries
{
    public class IsAnyInvitesTestCaseModel
    {
        public Invite[] InvitesToAdd { get; set; }

        public IsAnyInvitesQuery IncomingQuery { get; set; }

        public IsAnyInvitesResponse ExpectedResult { get; set; }
    }
}

using Quetta.Common.Models.Commands;
using Quetta.Data.Models;

namespace Quetta.Tests.Handlers.Commands
{
    public class AcceptInviteTestCaseModel
    {
        public Invite[] InvitesToAdd { get; set; }

        public User[] UsersToAdd { get; set; }

        public string AcceptedInviteId { get; set; }

        public AcceptInviteCommand IncomingCommand { get; set; }
    }
}

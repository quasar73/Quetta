using Quetta.Common.Models.Commands;
using Quetta.Data.Models;

namespace Quetta.Tests.Handlers.Commands
{
    public class DeclineInviteTestCaseModel
    {
        public Invite[] InvitesToAdd { get; set; }

        public string DeclinedInviteId { get; set; }

        public DeclineInviteCommand IncomingCommand { get; set; }
    }
}

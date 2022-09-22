using Quetta.Common.Models.Commands;
using Quetta.Data.Models;

namespace Quetta.Tests.Handlers.Commands
{
    public class SendInviteExceptionCaseModel
    {
        public Invite[] InvitesToAdd { get; set; }

        public SendInviteCommand IncomingCommand { get; set; }

        public Type ExceptionType { get; set; }

        public string ExceptionMessage { get; set; }
    }
}

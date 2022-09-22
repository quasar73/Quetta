using Quetta.Common.Enums;
using Quetta.Data.Models;
using System.Collections;

namespace Quetta.Tests.Handlers.Commands
{
    public class AcceptInviteForPersonalChatTestCase : IEnumerable<object[]>
    {
        private const string TargetedInviteId = "00000000-0000-0000-0000-000000000000";
        private const string NotTargetedtInviteId = "00000000-0000-0000-0000-000000000001";

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new AcceptInviteTestCaseModel()
                {
                    InvitesToAdd = new Invite[]
                    {
                        new Invite()
                        {
                            Id = TargetedInviteId,
                            Sender = AcceptInviteTestUsers.TestUsers.Last(),
                            Receiver = AcceptInviteTestUsers.TestUsers.First(),
                            IsGroupChat = false,
                            ChatId = null,
                            Status = InviteStatus.Pending,
                        },
                        new Invite()
                        {
                            Id = NotTargetedtInviteId,
                            Sender = AcceptInviteTestUsers.TestUsers.Last(),
                            Receiver = AcceptInviteTestUsers.TestUsers.First(),
                            IsGroupChat = false,
                            ChatId = null,
                            Status = InviteStatus.Pending,
                        },
                    },
                    AcceptedInviteId = TargetedInviteId,
                    UsersToAdd = AcceptInviteTestUsers.TestUsers,
                    IncomingCommand = new(TargetedInviteId, AcceptInviteTestUsers.TestUsers.First().Id),
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

using Quetta.Common.Enums;
using Quetta.Data.Models;
using System.Collections;

namespace Quetta.Tests.Handlers.Commands
{
    public class AcceptInviteExpcetionTestCase : IEnumerable<object[]>
    {
        private const string ExistingInviteId = "00000000-0000-0000-0000-000000000000";
        private const string NonexistentInviteId = "00000000-0000-0000-0000-000000000001";

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
                            Id = ExistingInviteId,
                            Sender = AcceptInviteTestUsers.TestUsers.Last(),
                            Receiver = AcceptInviteTestUsers.TestUsers.First(),
                            IsGroupChat = false,
                            ChatId = null,
                            Status = InviteStatus.Pending,
                        },
                    },
                    AcceptedInviteId = ExistingInviteId,
                    UsersToAdd = AcceptInviteTestUsers.TestUsers,
                    IncomingCommand = new(NonexistentInviteId, AcceptInviteTestUsers.TestUsers.First().Id),
                }
            };
            yield return new object[]
            {
                new AcceptInviteTestCaseModel()
                {
                    InvitesToAdd = new Invite[]
                    {
                        new Invite()
                        {
                            Id = "00000000-0000-0000-0000-000000000002",
                            Sender = AcceptInviteTestUsers.TestUsers.Last(),
                            Receiver = AcceptInviteTestUsers.TestUsers.First(),
                            IsGroupChat = false,
                            ChatId = null,
                            Status = InviteStatus.Pending,
                        },
                    },
                    AcceptedInviteId = "00000000-0000-0000-0000-000000000002",
                    UsersToAdd = AcceptInviteTestUsers.TestUsers,
                    IncomingCommand = new("00000000-0000-0000-0000-000000000002", AcceptInviteTestUsers.TestUsers.Last().Id),
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

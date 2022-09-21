using Quetta.Common.Enums;
using Quetta.Data.Models;
using System.Collections;

namespace Quetta.Tests.Handlers.Commands
{
    public class AcceptInviteExpcetionTestCase : IEnumerable<object[]>
    {
        private const string ExistingInviteId = "00000000-0000-0000-0000-000000000002";
        private const string NonexistentInviteId = "00000000-0000-0000-0000-000000000003";
        private const string InviteId = "00000000-0000-0000-0000-000000000004";

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
                            Id = InviteId ,
                            Sender = AcceptInviteTestUsers.TestUsers.Last(),
                            Receiver = AcceptInviteTestUsers.TestUsers.First(),
                            IsGroupChat = false,
                            ChatId = null,
                            Status = InviteStatus.Pending,
                        },
                    },
                    AcceptedInviteId = InviteId ,
                    UsersToAdd = AcceptInviteTestUsers.TestUsers,
                    IncomingCommand = new(InviteId , AcceptInviteTestUsers.TestUsers.Last().Id),
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

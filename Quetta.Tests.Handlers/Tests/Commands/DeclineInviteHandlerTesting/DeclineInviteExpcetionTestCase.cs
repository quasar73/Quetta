using Quetta.Common.Enums;
using Quetta.Data.Models;
using System.Collections;

namespace Quetta.Tests.Handlers.Commands
{
    public class DeclineInviteExpcetionTestCase : IEnumerable<object[]>
    {
        private const string ExistingInviteId = "00000000-0000-0000-0000-000000000000";
        private const string NonexistentInviteId = "00000000-0000-0000-0000-000000000001";
        private const string InviteId = "00000000-0000-0000-0000-000000000001";
        private const string ReceiverId = "00000000-0000-0000-0000-000000000000";
        private const string NotReceiverId = "00000000-0000-0000-0000-000000000001";
        private const string SenderId = "00000000-0000-0000-0000-000000000002";

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new DeclineInviteTestCaseModel()
                {
                    InvitesToAdd = new Invite[]
                    {
                        new Invite()
                        {
                            Id = ExistingInviteId,
                            ReceiverId = ReceiverId,
                            SenderId = SenderId,
                            IsGroupChat = false,
                            ChatId = null,
                            Status = InviteStatus.Pending,
                        },
                    },
                    DeclinedInviteId = ExistingInviteId,
                    IncomingCommand = new(NonexistentInviteId, ReceiverId),
                }
            };
            yield return new object[]
            {
                new DeclineInviteTestCaseModel()
                {
                    InvitesToAdd = new Invite[]
                    {
                        new Invite()
                        {
                            Id = InviteId,
                            ReceiverId = ReceiverId,
                            SenderId = SenderId,
                            IsGroupChat = false,
                            ChatId = null,
                            Status = InviteStatus.Pending,
                        },
                    },
                    DeclinedInviteId = InviteId,
                    IncomingCommand = new(InviteId, NotReceiverId),
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

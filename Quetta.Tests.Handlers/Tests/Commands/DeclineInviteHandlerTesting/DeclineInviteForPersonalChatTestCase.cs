using Quetta.Common.Enums;
using Quetta.Data.Models;
using System.Collections;

namespace Quetta.Tests.Handlers.Commands
{
    public class DeclineInviteForPersonalChatTestCase : IEnumerable<object[]>
    {
        private const string TargetedInviteId = "00000000-0000-0000-0000-000000000000";
        private const string NotTargetedtInviteId = "00000000-0000-0000-0000-000000000001";
        private const string ReceiverId = "00000000-0000-0000-0000-000000000000";
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
                            Id = TargetedInviteId,
                            ReceiverId = ReceiverId,
                            SenderId = SenderId,
                            IsGroupChat = false,
                            ChatId = null,
                            Status = InviteStatus.Pending,
                        },
                        new Invite()
                        {
                            Id = NotTargetedtInviteId,
                            ReceiverId = ReceiverId,
                            SenderId = SenderId,
                            IsGroupChat = false,
                            ChatId = null,
                            Status = InviteStatus.Pending,
                        },
                    },
                    DeclinedInviteId = TargetedInviteId,
                    IncomingCommand = new(TargetedInviteId, ReceiverId),
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
